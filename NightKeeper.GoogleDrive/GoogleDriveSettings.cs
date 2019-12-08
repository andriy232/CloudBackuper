using Helper.Settings;

namespace GDriveProvider
{
    public class GoogleDriveSettings : SettingsBase
    {
        public string DriveFolderId { get; set; }

        public string CredentialsPath { get; set; }

        public string AuthInfoPath { get; set; }
    }
}