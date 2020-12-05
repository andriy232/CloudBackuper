namespace DataGuardian.Server.Plugins
{
    public interface IAuthManager
    {
        string GetUserUid(string computerId);

        string GetUserDirectory(string uid);
    }
}