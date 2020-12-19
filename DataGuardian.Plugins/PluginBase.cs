namespace DataGuardian.Plugins
{
    public abstract class PluginBase : IPlugin
    {
        public abstract string Name { get; }

        protected ICore Core { get; private set; }

        public virtual void Init(ICore core)
        {
            Core = core;
        }
    }
}