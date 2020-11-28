namespace DataGuardian.Plugins
{
    public class PluginBase : IPlugin
    {
        protected ICore Core { get; private set; }

        public virtual void Init(ICore core)
        {
            Core = core;
        }
    }
}