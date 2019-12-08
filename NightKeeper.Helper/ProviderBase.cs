using System;
using NightKeeper.Helper.Settings;

namespace NightKeeper.Helper
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