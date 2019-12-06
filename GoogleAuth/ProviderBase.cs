using System;
using Helper.Settings;

namespace Helper
{
    public abstract class ProviderBase<T> where T : SettingsBase
    {
        public T GetSettings(Guid id)
        {
            return Core.Core.Database.ReadSettings<T>(id);
        }

        protected void SaveSettings(Guid id, T settings)
        {
            Core.Core.Database.SaveSettings(id, settings);
        }
    }
}