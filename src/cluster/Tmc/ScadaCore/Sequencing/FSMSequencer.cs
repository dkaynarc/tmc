#region Header

/// FileName: FSMSequencer.cs
/// Author: Denis Kaynarca (denis@dkaynarca.com)

#endregion Header

using Appccelerate.StateMachine;
using Appccelerate.StateMachine.Extensions;
using Appccelerate.StateMachine.Machine;
using System;
using System.Diagnostics;
using Tmc.Common;
using System.Collections.Generic;

namespace Tmc.Scada.Core.Sequencing
{
    public enum OperationMode
    {
        SortOnly,
        AssembleOnly,
        Normal
    }

    public class StateLoggerExtension : ExtensionBase<State, Trigger>
    {
        public IState<State, Trigger> CurrentState { get; private set; }
        public IState<State, Trigger> PreviousState { get; private set; }

        public override void SwitchedState(IStateMachineInformation<State, Trigger> stateMachine, 
            Appccelerate.StateMachine.Machine.IState<State, Trigger> oldState, 
            Appccelerate.StateMachine.Machine.IState<State, Trigger> newState)
        {
            this.CurrentState = newState;
            this.PreviousState = oldState;
            base.SwitchedState(stateMachine, oldState, newState);
        }
    }

    public class FSMSequencer : ISequencer
    {
        public bool IsRunning { get; private set; }

        public OperationMode Mode { get; set; }

        public string Name { get; set; }
        private Assembler _assembler;
        private ConveyorController _conveyorController;
        private ScadaEngine _engine;
        private PassiveStateMachine<State, Trigger> _fsm;
        private Loader _loader;
        private OrderConsumer _orderConsumer;
        private Sorter _sorter;
        private TrayVerifier _trayVerifier;
        public StateLoggerExtension TransitionLogger { get; private set; }
        private List<IHardware> _hardware;

        public FSMSequencer(ScadaEngine engine)
        {
            this._engine = engine;

            Debug.Assert(this._engine != null);

            this._conveyorController = _engine.ClusterConfig.Controllers[typeof(ConveyorController)] as ConveyorController;
            this._assembler = _engine.ClusterConfig.Controllers[typeof(Assembler)] as Assembler;
            this._loader = _engine.ClusterConfig.Controllers[typeof(Loader)] as Loader;
            this._sorter = _engine.ClusterConfig.Controllers[typeof(Sorter)] as Sorter;
            this._trayVerifier = _engine.ClusterConfig.Controllers[typeof(TrayVerifier)] as TrayVerifier;
            this._orderConsumer = _engine.OrderConsumer;
            this._hardware = _engine.ClusterConfig.GetAllHardware();

            Debug.Assert(this._conveyorController != null);
            Debug.Assert(this._assembler != null);
            Debug.Assert(this._loader != null);
            Debug.Assert(this._sorter != null);
            Debug.Assert(this._trayVerifier != null);

            _fsm = new PassiveStateMachine<State, Trigger>();
            TransitionLogger = new StateLoggerExtension();
            _fsm.AddExtension(TransitionLogger);
            this.Create();
        }

        #region Public Methods

        public void FireResumeTrigger()
        {
            if (IsRunning)
            {
                _fsm.Fire(Trigger.Resume);
            }
        }

        public void FireShutdownTrigger()
        {
            if (IsRunning)
            {
                _fsm.Fire(Trigger.Shutdown);
            }
        }

        public void FireStartTrigger()
        {
            if (IsRunning)
            {
                _fsm.Fire(Trigger.Start);
            }
        }

        public void FireStopTrigger()
        {
            if (IsRunning)
            {
                _fsm.Fire(Trigger.Stop);
            }
        }

        public void StartSequencing()
        {
            _fsm.Start();
            IsRunning = true;
        }

        public void StopSequencing()
        {
            _fsm.Stop();
            IsRunning = false;
        }
        #endregion Public Methods

        #region State machine

        private void Create()
        {
            CreateSortingStates();
            CreateAssemblingStates();
            CreateGlobalStates();

            _fsm.Initialize(State.Shutdown);
        }

        private void CreateAssemblingStates()
        {
            _fsm.In(State.Idle)
                .ExecuteOnEntry(() => _orderConsumer.OrdersAvailable += Idle_Completed)
                .ExecuteOnExit(() => _orderConsumer.OrdersAvailable -= Idle_Completed)
                .On(Trigger.Completed)
                    .Goto(State.LoadingTray)
                .On(Trigger.Stop)
                    .Goto(State.Stopped)
                .On(Trigger.Shutdown)
                    .Goto(State.Shutdown);

            _fsm.In(State.LoadingTray)
                .ExecuteOnEntry(() =>
                {
                    _loader.Begin(new LoaderParams
                    {
                        Action = LoaderAction.LoadToConveyor,
                        Sender = this
                    });
                })
                .On(Trigger.Completed)
                    .Goto(State.AssemblyConveyorMovingForward)
                .On(Trigger.Stop)
                    .Goto(State.Stopped)
                .On(Trigger.Shutdown)
                    .Goto(State.Shutdown);

            _fsm.In(State.AssemblyConveyorMovingForward)
                .ExecuteOnEntry(() =>
                {
                    _conveyorController.Begin(new ConveyorControllerParams
                    {
                        ConveyorType = ConveyorType.Assembly,
                        ConveyorAction = ConveyorAction.MoveForward,
                        Sender = this
                    });
                })
                .On(Trigger.Completed)
                    .If(() => _conveyorController.CanMoveForward(ConveyorType.Assembly))
                        .Goto(State.VerifyingTray)
                    .Otherwise()
                        .Goto(State.Assembling)
                .On(Trigger.Stop)
                    .Goto(State.Stopped)
                .On(Trigger.Shutdown)
                    .Goto(State.Shutdown);

            _fsm.In(State.VerifyingTray)
                .ExecuteOnEntry(() =>
                {
                    Tray<Tablet> trayToVerify = _assembler.LastOrderTray;
                    _trayVerifier.Begin(new TrayVerifierParams()
                    {
                        TraySpecification = trayToVerify,
                        VerificationMode = VerificationMode.Product,
                        Sender = this
                    });
                })
                .On(Trigger.Valid).If<VerificationMode>((mode) => mode == VerificationMode.Tray)
                    .Goto(State.AssemblyConveyorMovingForward)
                .On(Trigger.Valid).If<VerificationMode>((mode) => mode == VerificationMode.Product)
                    .Goto(State.AssemblyConveyorMovingBackward)
                .On(Trigger.Invalid)
                    .Goto(State.AssemblyConveyorMovingBackward)
                .On(Trigger.Stop)
                    .Goto(State.Stopped)
                .On(Trigger.Shutdown)
                    .Goto(State.Shutdown);

            _fsm.In(State.Assembling)
                .ExecuteOnEntry(() =>
                {
                    var order = _orderConsumer.GetNextOrder();
                    _assembler.Begin(new AssemblerParams
                    {
                        OrderConfiguration = order.Configuration,
                        Magazine = _engine.TabletMagazine,
                        Action = AssemblerAction.Assemble,
                        Sender = this
                    });
                })
                .On(Trigger.Completed)
                    .Goto(State.AssemblyConveyorMovingBackward)
                .On(Trigger.Stop)
                    .Goto(State.Stopped)
                .On(Trigger.Shutdown)
                    .Goto(State.Shutdown);

            _fsm.In(State.AssemblyConveyorMovingBackward)
                .ExecuteOnEntry(() =>
                {
                    _conveyorController.Begin(new ConveyorControllerParams
                    {
                        ConveyorType = ConveyorType.Assembly,
                        ConveyorAction = ConveyorAction.MoveBackward,
                        Sender = this
                    });
                })
                .On(Trigger.Completed)
                    .If(() => _conveyorController.CanMoveBackward(ConveyorType.Assembly))
                        .Goto(State.VerifyingTray)
                    .Otherwise()
                        .Goto(State.PlacingTrayInBuffer)
                .On(Trigger.Stop)
                    .Goto(State.Stopped)
                .On(Trigger.Shutdown)
                    .Goto(State.Shutdown);

            _fsm.In(State.PlacingTrayInBuffer)
                .ExecuteOnEntry(() =>
                {
                    _loader.Begin(new LoaderParams()
                    {
                        Action = LoaderAction.LoadToPalletiser,
                        Sender = this
                    });

                })
                .On(Trigger.Completed)
                    .Goto(State.OrderComplete)
                .On(Trigger.Stop)
                    .Goto(State.Stopped)
                .On(Trigger.Shutdown)
                    .Goto(State.Shutdown);

            _fsm.In(State.OrderComplete)
                .ExecuteOnEntry(() =>
                {
                    _orderConsumer.CompleteOrder();
                })
                .On(Trigger.Completed)
                    .Goto(State.Idle)
                .On(Trigger.Stop)
                    .Goto(State.Stopped)
                .On(Trigger.Shutdown)
                    .Goto(State.Shutdown);
        }

        private void CreateGlobalStates()
        {
            _fsm.In(State.Startup)
                .On(Trigger.Completed)
                    .If(() => Mode == OperationMode.Normal)
                        .Goto(State.PlacingTabletMagazineInSortingBuffer)
                    .If(() => Mode == OperationMode.AssembleOnly)
                        .Goto(State.Idle)
                    .If(() => Mode == OperationMode.SortOnly)
                        .Goto(State.PlacingTabletMagazineInSortingBuffer);

            _fsm.In(State.Shutdown)
                .ExecuteOnEntry(() =>
                {
                    foreach (var hardware in _hardware)
                    {
                        hardware.Shutdown();
                    }
                })
                .On(Trigger.Start)
                    .Goto(State.Startup);

            _fsm.In(State.Stopped)
                .On(Trigger.Shutdown)
                    .Goto(State.Shutdown)
                .On(Trigger.Resume)
                    .Goto(State.Running);

            _fsm.DefineHierarchyOn(State.Running)
                .WithHistoryType(HistoryType.Deep)
                .WithInitialSubState(State.Startup)
                .WithSubState(State.Idle)
                .WithSubState(State.LoadingTray)
                .WithSubState(State.AssemblyConveyorMovingForward)
                .WithSubState(State.VerifyingTray)
                .WithSubState(State.Assembling)
                .WithSubState(State.AssemblyConveyorMovingBackward)
                .WithSubState(State.PlacingTrayInBuffer)
                .WithSubState(State.Palletising)
                .WithSubState(State.Sorting)
                .WithSubState(State.PlacingTabletMagazineInSortingBuffer)
                .WithSubState(State.PlacingTabletMagazineInAssemblyBuffer)
                .WithSubState(State.PlacingTabletMagazineOnSortingConveyorFromSorter)
                .WithSubState(State.PlacingTabletMagazineOnSortingConveyorFromAssembler)
                .WithSubState(State.SortingConveyorMovingForward)
                .WithSubState(State.SortingConveyorMovingBackward);
        }

        private void CreateSortingStates()
        {
            _fsm.In(State.Sorting)
                .ExecuteOnEntry(() =>
                {
                    _sorter.Begin(new SorterParams
                    {
                        Action = SorterAction.Sort,
                        Magazine = _engine.TabletMagazine
                    });
                })
                .On(Trigger.Completed)
                    .Goto(State.PlacingTabletMagazineOnSortingConveyorFromSorter)
                .On(Trigger.Stop)
                    .Goto(State.Stopped)
                    .Execute(() => _sorter.Cancel())
                .On(Trigger.Shutdown)
                    .Goto(State.Shutdown);

            _fsm.In(State.PlacingTabletMagazineOnSortingConveyorFromSorter)
                .ExecuteOnEntry(() =>
                {
                    _sorter.Begin(new SorterParams
                    {
                        Action = SorterAction.LoadToConveyor
                    });
                })
                .On(Trigger.Completed)
                    .Goto(State.SortingConveyorMovingBackward)
                .On(Trigger.Stop)
                    .Goto(State.Stopped)
                .On(Trigger.Shutdown)
                    .Goto(State.Shutdown);

            _fsm.In(State.SortingConveyorMovingBackward)
                .ExecuteOnEntry(() =>
                {
                    _conveyorController.Begin(new ConveyorControllerParams
                    {
                        ConveyorType = ConveyorType.Sorting,
                        ConveyorAction = ConveyorAction.MoveBackward
                    });
                })
                .On(Trigger.Completed)
                    .Goto(State.PlacingTabletMagazineInAssemblyBuffer)
                .On(Trigger.Stop)
                    .Goto(State.Stopped)
                .On(Trigger.Shutdown)
                    .Goto(State.Shutdown);

            _fsm.In(State.PlacingTabletMagazineInAssemblyBuffer)
                .ExecuteOnEntry(() =>
                {
                    _assembler.Begin(new AssemblerParams
                    {
                        Action = AssemblerAction.GetTabletMagazine
                    });
                })
                .On(Trigger.Completed)
                    .Goto(State.Idle)
                .On(Trigger.Stop)
                    .Goto(State.Stopped)
                .On(Trigger.Shutdown)
                    .Goto(State.Shutdown);

            _fsm.In(State.PlacingTabletMagazineOnSortingConveyorFromAssembler)
                .ExecuteOnEntry(() =>
                {
                    _assembler.Begin(new AssemblerParams
                    {
                        Action = AssemblerAction.ReturnTabletMagazine
                    });
                })
                .On(Trigger.Completed)
                    .Goto(State.SortingConveyorMovingForward)
                .On(Trigger.Stop)
                    .Goto(State.Stopped)
                .On(Trigger.Shutdown)
                    .Goto(State.Shutdown);

            _fsm.In(State.SortingConveyorMovingForward)
                .ExecuteOnEntry(() =>
                {
                    _conveyorController.Begin(new ConveyorControllerParams
                    {
                        ConveyorType = ConveyorType.Sorting,
                        ConveyorAction = ConveyorAction.MoveForward
                    });
                })
                .On(Trigger.Completed)
                    .Goto(State.PlacingTabletMagazineInSortingBuffer)
                .On(Trigger.Stop)
                    .Goto(State.Stopped)
                .On(Trigger.Shutdown)
                    .Goto(State.Shutdown);

            _fsm.In(State.PlacingTabletMagazineInSortingBuffer)
                .ExecuteOnEntry(() =>
                {
                    _sorter.Begin(new SorterParams
                    {
                        Action = SorterAction.LoadToBuffer
                    });
                })
                .On(Trigger.Completed)
                    .Goto(State.Sorting)
                .On(Trigger.Stop)
                    .Goto(State.Stopped)
                .On(Trigger.Shutdown)
                    .Goto(State.Shutdown);
        }
        #endregion State machine

        #region Event handlers

        private void Assembler_Completed(object sender, ControllerEventArgs e)
        {
            _fsm.Fire(Trigger.Completed);
        }

        private void BindEvents()
        {
            _assembler.Completed += Assembler_Completed;
            _conveyorController.Completed += ConveyorController_Completed;
            _loader.Completed += Loader_Completed;
            _sorter.Completed += Sorter_Completed;
            _trayVerifier.Completed += TrayVerifier_Completed;
            // The Idle_Completed handler is not registered here. It is registered/unregistered
            // in the Entry/Exit callbacks for the Idle event.
        }
        private void ConveyorController_Completed(object sender, ControllerEventArgs e)
        {
            _fsm.Fire(Trigger.Completed);
        }

        private void Idle_Completed(object sender, EventArgs e)
        {
            _fsm.Fire(Trigger.Completed);
        }

        private void Loader_Completed(object sender, ControllerEventArgs e)
        {
            _fsm.Fire(Trigger.Completed);
        }

        private void Sorter_Completed(object sender, ControllerEventArgs e)
        {
            var args = e as SorterCompletedEventArgs;
            _fsm.Fire(Trigger.Completed, args.Action);
        }

        private void TrayVerifier_Completed(object sender, ControllerEventArgs e)
        {
            var args = e as OnVerificationCompleteEventArgs;

            if (args.VerificationResult == VerificationResult.Valid)
            {
                _fsm.Fire(Trigger.Valid, args.VerificationMode);
            }
            else if (args.VerificationResult == VerificationResult.Invalid)
            {
                _fsm.Fire(Trigger.Invalid, args.VerificationMode);
            }
        }
        #endregion Event handlers
    }
}