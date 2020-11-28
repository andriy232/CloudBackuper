namespace DataGuardian.Plugins.Core
{
    public static class CoreStatic
    {
        private static ICore _instance;
        public static ICore Instance => _instance;

        public static void SetCore(ICore core)
        {
            _instance = core;
        }
    }
}