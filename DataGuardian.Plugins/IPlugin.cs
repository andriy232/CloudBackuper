namespace DataGuardian.Plugins
{
    public interface IPlugin
    {
        void Init(ICore core);

        string Name { get; }
    }
}