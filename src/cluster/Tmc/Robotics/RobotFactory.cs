namespace Tmc.Robotics
{
    using System;
    using System.Collections.Generic;

    public static class RobotFactory
    {
        public static T CreateRobot<T>() where T : class, IRobot
        {
            var caseSwitch = new Dictionary<Type, IRobot>
            {
                {typeof(LoaderRobot),     BuildLoader()},
                {typeof(SorterRobot),     BuildSorter()},
                {typeof(AssemblerRobot),  BuildAssembler()},
                {typeof(PalletiserRobot), BuildPalletiser()}
            };
            
            return caseSwitch[typeof(T)] as T;
        }

        private static AssemblerRobot BuildAssembler()
        {
            throw new NotImplementedException();
        }

        private static LoaderRobot BuildLoader()
        {
            throw new NotImplementedException();
        }

        private static PalletiserRobot BuildPalletiser()
        {
            throw new NotImplementedException();
        }

        private static SorterRobot BuildSorter()
        {
            throw new NotImplementedException();
        }
    }
}
