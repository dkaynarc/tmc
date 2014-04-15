namespace Tmc.Robotics
{
    using System;
    using System.Collections.Generic;

   


    public static class RobotFactory
    {
        public static T CreateRobot<T>() where T : class, IRobot
        {
            var caseSwitch = new Dictionary<Type, Func<T>>
            {
                {typeof(LoaderRobot),     () => { return BuildLoader() as T; }},
                {typeof(SorterRobot),     () => { return BuildSorter() as T; }},
                {typeof(AssemblerRobot),  () => { return BuildAssembler() as T; }},
                {typeof(PalletiserRobot), () => { return BuildPalletiser() as T; }}
            };
            
            return caseSwitch[typeof(T)]();
        }

        public static IRobot CreateRobot(Type type)
        {
            var caseSwitch = new Dictionary<Type, Func<IRobot>>
            {
                {typeof(LoaderRobot),     () => { return BuildLoader(); }},
                {typeof(SorterRobot),     () => { return BuildSorter(); }},
                {typeof(AssemblerRobot),  () => { return BuildAssembler(); }},
                {typeof(PalletiserRobot), () => { return BuildPalletiser(); }}
            };

            return caseSwitch[type]();
        }

        private static AssemblerRobot BuildAssembler()
        {
            return new AssemblerRobot() { Name = "Assembler" };
            throw new NotImplementedException();
        }

        private static LoaderRobot BuildLoader()
        {
            return new LoaderRobot() { Name = "Loader" };
            throw new NotImplementedException();
        }

        private static PalletiserRobot BuildPalletiser()
        {
            return new PalletiserRobot() { Name = "Palletiser" };
            throw new NotImplementedException();
        }

        private static SorterRobot BuildSorter()
        {
            return new SorterRobot() { Name = "Sorter" };
            throw new NotImplementedException();
        }
    }
}
