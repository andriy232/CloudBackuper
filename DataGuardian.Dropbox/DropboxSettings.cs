
using DataGuardian.Plugins;
using Newtonsoft.Json;

namespace DataGuardian.Dropbox
{
    public class DropboxSettings : SettingsBase
    {
        public string AccessToken { get; set; }
        public string Uid { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}