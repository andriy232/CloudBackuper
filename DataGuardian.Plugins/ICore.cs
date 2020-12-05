using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DataGuardian.Plugins.Plugins;

namespace DataGuardian.Plugins
{
    public interface ICore
    {
        ISettings Settings { get; }
        ISingleLogger Logger { get; }
        IBackupManager BackupManager { get; }
        ICloudAccountsManager CloudAccountsManager { get; }
        IEnumerable<ICloudStorageProvider> CloudStorageProviders { get; }
        IGuiManager GuiManager { get; }
    }

    public interface IGuiManager
    {
        bool InvokeRequired { get; }
        Form MainWindow { get; }

        void SetWindow(Form form);
        T Invoke<T>(Func<T> func);

        event EventHandler GuiLoaded;
    }
}