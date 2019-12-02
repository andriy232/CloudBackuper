using System;
using Helper.Settings;

namespace Helper
{
    public abstract class ProviderBase<T> where T : SettingsBase
    {
        protected T GetSettings(Guid id)
        {
            return Core.Database.ReadSettings<T>(id);
        }

        protected void SaveSettings(Guid id, T settings)
        {
            Core.Database.SaveSettings(id, settings);
        }
    }
}