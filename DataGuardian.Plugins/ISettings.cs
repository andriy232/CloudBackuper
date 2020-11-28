namespace DataGuardian.Plugins
{
    public interface ISettings:IPlugin
    {
        string ConnectionString { get; }
        string DataDirectory { get; }
    }
}