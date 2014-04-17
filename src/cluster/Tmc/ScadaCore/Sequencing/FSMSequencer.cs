using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Appccelerate.StateMachine;

namespace Tmc.Scada.Core.Sequencing
{
    public class FSMSequencer : ISequencer
    {
        public string Name { get; set; }
        public bool Enabled { get; private set; }

        private PassiveStateMachine<State, StateEvent> _fsm;

        public FSMSequencer()
        {
            _fsm = new PassiveStateMachine<State, StateEvent>();
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
                .On(StateEvent.Completed)
                    .Goto(State.Sorting);

            _fsm.In(State.Idle)
                .On(StateEvent.Completed)
                    .Goto(State.LoadingTray);

            _fsm.In(State.Shutdown)
                .On(StateEvent.Start)
                    .Goto(State.Startup);
        }

        private void CreateSortingStates()
        {
            _fsm.In(State.Sorting)
                .On(StateEvent.Completed)
                    .Goto(State.PlacingTabletMagazineOnSortingConveyorFromSorter);

            _fsm.In(State.PlacingTabletMagazineOnSortingConveyorFromSorter)
                .On(StateEvent.Completed)
                    .Goto(State.SortingConveyorMovingBackward);

            _fsm.In(State.SortingConveyorMovingBackward)
                .On(StateEvent.Completed)
                    .Goto(State.PlacingTabletMagazineInAssemblyBuffer);

            _fsm.In(State.PlacingTabletMagazineInAssemblyBuffer)
                .On(StateEvent.Completed)
                    .Goto(State.Idle);

            _fsm.In(State.PlacingTabletMagazineOnSortingConveyorFromAssembler)
                .On(StateEvent.Completed)
                    .Goto(State.SortingConveyorMovingForward);

            _fsm.In(State.SortingConveyorMovingForward)
                .On(StateEvent.Completed)
                    .Goto(State.PlacingTabletMagazineInSortingBuffer);

            _fsm.In(State.PlacingTabletMagazineInSortingBuffer)
                .On(StateEvent.Completed)
                    .Goto(State.Sorting);
        }

        private void CreateAssemblingStates()
        {
            _fsm.In(State.LoadingTray)
                .On(StateEvent.Completed)
                    .Goto(State.AssemblyConveyorMovingForward);

            _fsm.In(State.AssemblyConveyorMovingForward)
                .On(StateEvent.Completed)
                    .If(() => true)
                        .Goto(State.Assembling)
                    .If(() => false)
                        .Goto(State.VerifyingTray);

            _fsm.In(State.VerifyingTray)
                .On(StateEvent.Completed)
                    .If(() => true)
                        .Goto(State.AssemblyConveyorMovingForward)
                    .If(() => false)
                        .Goto(State.AssemblyConveyorMovingBackward);

            _fsm.In(State.Assembling)
                .On(StateEvent.Completed)
                    .Goto(State.AssemblyConveyorMovingBackward);

            _fsm.In(State.AssemblyConveyorMovingBackward)
                .On(StateEvent.Completed)
                    .If(() => true)
                        .Goto(State.VerifyingTray)
                    .If(() => false)
                        .Goto(State.PlacingTrayInBuffer);

            _fsm.In(State.PlacingTrayInBuffer)
                .On(StateEvent.Completed)
                    .Goto(State.Palletising);

            _fsm.In(State.Palletising)
                .On(StateEvent.Completed)
                    .Goto(State.Idle);
        }
    }
}
