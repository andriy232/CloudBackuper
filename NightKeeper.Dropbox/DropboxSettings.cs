using NightKeeper.Helper.Settings;

namespace NightKeeper.Dropbox
{
    public class DropboxSettings : SettingsBase
    {
        public string AccessToken { get; set; }
        public string Uid { get; set; }
    }
}