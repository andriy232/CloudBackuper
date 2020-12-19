using Newtonsoft.Json;

namespace DataGuardian.Plugins
{
    public class SettingsBase
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}