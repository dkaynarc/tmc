using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Appccelerate.StateMachine;
using Appccelerate.StateMachine.Extensions;

namespace Tmc.Scada.Core.Sequencing
{
    public class FSMSequencer : ISequencer
    {
        public string Name { get; set; }
        public bool Enabled { get; private set; }

        private PassiveStateMachine<State, Trigger> _fsm;

        public FSMSequencer()
        {
            _fsm = new PassiveStateMachine<State, Trigger>();
            Create();
        }

        private void Create()
        {
            CreateSortingStates();
            CreateAssemblingStates();
            CreateGlobalStates();
        }

        private void CreateGlobalStates()
        {
            _fsm.In(State.Startup)
                .On(Trigger.Completed)
                    .Goto(State.Sorting);

            _fsm.In(State.Idle)
                .On(Trigger.Completed)
                    .Goto(State.LoadingTray)
                .On(Trigger.Stop)
                    .Goto(State.Stopped)
                .On(Trigger.Shutdown)
                    .Goto(State.Shutdown);

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

        public void Resume()
        {
            _fsm.Fire(Trigger.Resume);
        }

        private void TestStop()
        {
            _fsm.Fire(Trigger.Stop);
            _fsm.Fire(Trigger.Resume);
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
            _fsm.In(State.LoadingTray)
                .On(Trigger.Completed)
                    .Goto(State.AssemblyConveyorMovingForward)
                .On(Trigger.Stop)
                    .Goto(State.Stopped)
                .On(Trigger.Shutdown)
                    .Goto(State.Shutdown);

            _fsm.In(State.AssemblyConveyorMovingForward)
                .On(Trigger.Completed)
                    .If(() => true)
                        .Goto(State.Assembling)
                    .If(() => false)
                        .Goto(State.VerifyingTray)
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
                .On(Trigger.Completed)
                    .If(() => true)
                        .Goto(State.VerifyingTray)
                    .If(() => false)
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
