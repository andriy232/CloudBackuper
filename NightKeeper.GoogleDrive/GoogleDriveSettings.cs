using NightKeeper.Helper.Settings;

namespace NightKeeper.GoogleDrive
{
    public class GoogleDriveSettings : SettingsBase
    {
        public string DriveFolderId { get; set; }

        public string CredentialsPath { get; set; }

        public string AuthInfoPath { get; set; }
    }
}