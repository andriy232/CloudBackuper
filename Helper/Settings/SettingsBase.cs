using Newtonsoft.Json;

namespace Helper.Settings
{
    public class SettingsBase
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}