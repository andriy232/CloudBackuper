using DataGuardian.Helper.Settings;

namespace DataGuardian.GoogleDrive
{
    public class GoogleDriveSettings : SettingsBase
    {
        public string DriveFolderId { get; set; }

        public string CredentialsPath { get; set; }

        public string AuthInfoPath { get; set; }
    }
}