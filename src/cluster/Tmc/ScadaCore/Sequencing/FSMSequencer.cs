using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Diagnostics;
using Appccelerate.StateMachine;

namespace Tmc.Scada.Core.Sequencing
{
    public class FSMSequencer : ISequencer
    {
        public string Name { get; set; }
        public bool Enabled { get; private set; }

        private PassiveStateMachine<State, Trigger> _fsm;
        private ScadaEngine _engine;
        private ConveyorController _conveyorController;
        private Assembler _assembler;
        private Loader _loader;
        private Sorter _sorter;
        private TrayVerifier _trayVerifier;
        private Palletiser _palletiser;

        public FSMSequencer(ScadaEngine engine)
        {
            this._engine = engine;

            Debug.Assert(this._engine != null);

            this._conveyorController = _engine.ClusterConfig.Controllers[typeof(ConveyorController)] as ConveyorController;
            this._assembler = _engine.ClusterConfig.Controllers[typeof(Assembler)] as Assembler;
            this._loader = _engine.ClusterConfig.Controllers[typeof(Loader)] as Loader;
            this._sorter = _engine.ClusterConfig.Controllers[typeof(Sorter)] as Sorter;
            this._trayVerifier = _engine.ClusterConfig.Controllers[typeof(TrayVerifier)] as TrayVerifier;
            this._palletiser = _engine.ClusterConfig.Controllers[typeof(Palletiser)] as Palletiser;

            Debug.Assert(this._conveyorController != null);
            Debug.Assert(this._assembler != null);
            Debug.Assert(this._loader != null);
            Debug.Assert(this._sorter != null);
            Debug.Assert(this._trayVerifier != null);
            Debug.Assert(this._palletiser != null);

            _fsm = new PassiveStateMachine<State, Trigger>();
            Create();
        }

        private void Create()
        {
            CreateSortingStates();
            CreateAssemblingStates();
            CreateGlobalStates();

            _fsm.Initialize(State.Shutdown);
        }

        private void CreateGlobalStates()
        {
            _fsm.In(State.Startup)
                .On(Trigger.Completed)
                    .Goto(State.Sorting);

            _fsm.In(State.Shutdown)
                .On(Trigger.Start)
                    .Goto(State.Startup);

            _fsm.In(State.Stopped)
                .On(Trigger.Shutdown)
                    .Goto(State.Shutdown);

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
                .On(Trigger.Completed)
                    .Goto(State.PlacingTabletMagazineOnSortingConveyorFromSorter)
                .On(Trigger.Stop)
                    .Goto(State.Stopped)
                .On(Trigger.Shutdown)
                    .Goto(State.Shutdown);

            _fsm.In(State.PlacingTabletMagazineOnSortingConveyorFromSorter)
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
                .On(Trigger.Completed)
                    .Goto(State.Idle)
                .On(Trigger.Stop)
                    .Goto(State.Stopped)
                .On(Trigger.Shutdown)
                    .Goto(State.Shutdown);

            _fsm.In(State.PlacingTabletMagazineOnSortingConveyorFromAssembler)
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
                .On(Trigger.Completed)
                    .Goto(State.Sorting)
                .On(Trigger.Stop)
                    .Goto(State.Stopped)
                .On(Trigger.Shutdown)
                    .Goto(State.Shutdown);
        }

        private void CreateAssemblingStates()
        {
            // TODO: Review the necessity of this state.
            _fsm.In(State.Idle)
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
                .ExecuteOnEntry(() => { 
                    _conveyorController.Begin(new ConveyorControllerParams
                        {
                            ConveyorType = ConveyorType.Assembly,
                            ConveyorAction = ConveyorAction.MoveForward,
                            Sender = this
                        }); })
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
                .On(Trigger.TrayVerificationCompleted)
                    .Goto(State.AssemblyConveyorMovingForward)
                .On(Trigger.ProductVerificationCompleted)
                    .Goto(State.AssemblyConveyorMovingBackward)
                .On(Trigger.Stop)
                    .Goto(State.Stopped)
                .On(Trigger.Shutdown)
                    .Goto(State.Shutdown);

            _fsm.In(State.Assembling)
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
                        ConveyorAction = ConveyorAction.MoveBackward
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
                .On(Trigger.Completed)
                    .Goto(State.Palletising)
                .On(Trigger.Stop)
                    .Goto(State.Stopped)
                .On(Trigger.Shutdown)
                    .Goto(State.Shutdown);

            _fsm.In(State.Palletising)
                .On(Trigger.Completed)
                    .Goto(State.Idle)
                .On(Trigger.Stop)
                    .Goto(State.Stopped)
                .On(Trigger.Shutdown)
                    .Goto(State.Shutdown);
        }
    }
}
