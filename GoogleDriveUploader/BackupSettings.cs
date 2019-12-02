using Helper.Settings;

namespace GDriveProvider
{
    public class BackupSettings : SettingsBase
    {
        public string TargetForUpload { get; set; }
        public string DriveFolderId { get; set; }

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}