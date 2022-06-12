namespace DodoWorkshop.GameKit.Demos.Bloc
{
	public interface IDemoDataBlocCommand { }

    public static class DemoDataBlocCommands{
        public struct IncrementCount : IDemoDataBlocCommand { }

        public struct ChangeName : IDemoDataBlocCommand
        {
            public string Name { get; private set; }

            public ChangeName(string name)
            {
                Name = name;
            }
        }
    }
}
