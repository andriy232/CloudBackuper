using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DataGuardian.GUI.Controls;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Core;

namespace DataGuardian.Controls
{
    public partial class CtlLog : UserControlBase
    {
        public CtlLog()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode)
                return;

            CoreStatic.Instance.Logger.NewLog += OnNewLog;
            LoadLogs();
            dgvData.ClearSelection();
        }

        private void LoadLogs()
        {
            FillLogs(CoreStatic.Instance.Logger.ReadLogs());
        }

        private void FillLogs(IEnumerable<LogEntry> readLogs)
        {
            try
            {
                foreach (var log in readLogs.OrderBy(x => x.Timestamp))
                {
                    AddLogToDgv(log);
                }
            }
            catch (Exception ex)
            {
                CoreStatic.Instance.Logger.Log(nameof(FillLogs), ex);
            }
        }

        private void AddLogToDgv(LogEntry log)
        {
            try
            {
                if (log.Level == InfoLogLevel.Debug)
                    return;

                var row = dgvData.Rows[dgvData.Rows.Add()];
                row.Cells[clmTime.Index].Value = log.Timestamp;
                row.Cells[clmMessage.Index].Value = log.Message;
                row.Cells[clmSource.Index].Value = log.SavedSource;
                row.DefaultCellStyle.BackColor = GetColor(log.Level);

                dgvData.FirstDisplayedScrollingRowIndex = dgvData.RowCount - 1;
            }
            catch
            {
                // ignored
            }
        }

        private static Color GetColor(InfoLogLevel logType)
        {
            switch (logType)
            {
                case InfoLogLevel.Information:
                    return Color.LightGray;
                case InfoLogLevel.Error:
                    return Color.Tomato;
                case InfoLogLevel.Warning:
                    return Color.Yellow;
                case InfoLogLevel.Debug:
                    return Color.Green;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logType), logType, null);
            }
        }

        private void OnNewLog(object sender, NewLogEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action<LogEntry>) AddLogToDgv, e.Log);
            }
            else
            {
                AddLogToDgv(e.Log);
            }
        }
    }
}