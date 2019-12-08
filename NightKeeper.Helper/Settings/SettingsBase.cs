using Newtonsoft.Json;

namespace NightKeeper.Helper.Settings
{
    public class SettingsBase
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}