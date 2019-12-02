using Helper.Settings;

namespace DropboxProvider
{
    public class DropboxSettings : SettingsBase
    {
        public string AccessToken { get; set; }
        public string Uid { get; set; }
    }
}