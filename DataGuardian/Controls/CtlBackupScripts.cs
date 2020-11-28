using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DataGuardian.GUI;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Core;
using DataGuardian.Windows;

namespace DataGuardian.Controls
{
    public partial class CtlBackupScripts : UserControl
    {
        public CtlBackupScripts()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode)
                return;

            FillData(CoreStatic.Instance.BackupManager.BackupScripts);

            CoreStatic.Instance.BackupManager.BackupScriptsChanged += OnBackupScriptsChanged;
        }

        private void OnBackupScriptsChanged(object sender, IEnumerable<IBackupScript> e)
        {
            FillData(e);
        }

        private void FillData(IEnumerable<IBackupScript> backupScripts)
        {
            dgvData.Rows.Clear();

            foreach (var backupScript in backupScripts.OrderBy(x => x.LastPerformTime).ThenBy(x => x.Name))
            {
                var row = dgvData.Rows[dgvData.Rows.Add()];
                row.Cells[clmCreateTime.Index].Value = backupScript.CreateTime;
                row.Cells[clmEnabled.Index].Value = backupScript.Enabled;
                row.Cells[clmLastPerform.Index].Value = backupScript.LastPerformTime;
                row.Cells[clmLastState.Index].Value = backupScript.LastPerformState;
                row.Cells[clmName.Index].Value = backupScript.Name;
                row.Cells[clmTarget.Index].Value = backupScript.TargetPath;
            }
        }

        public IBackupScript SelectedBackupScript
        {
            get => dgvData.SelectedRows.Cast<DataGridViewRow>()?.FirstOrDefault()?.Tag as IBackupScript;
            set
            {
                if (value != null)
                {
                    var row = dgvData.Rows.Cast<DataGridViewRow>()
                        .FirstOrDefault(x => x.Tag is IBackupScript bs && bs == value);
                    dgvData.ClearSelection();
                    if (row != null)
                        dgvData.Rows[row.Index].Selected = true;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedBackupScript != null)
                {
                    CoreStatic.Instance.BackupManager.RemoveBackupScript(SelectedBackupScript);
                    GuiHelper.ShowMessage("Backup script removed");
                }
            }
            catch (Exception ex)
            {
                GuiHelper.ShowMessage(ex);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedBackupScript != null)
                    CoreStatic.Instance.BackupManager.EditBackupScript(SelectedBackupScript);
            }
            catch (Exception ex)
            {
                GuiHelper.ShowMessage(ex);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                CoreStatic.Instance.BackupManager.RequestNewBackupScriptDialog();
            }
            catch (Exception ex)
            {
                GuiHelper.ShowMessage(ex);
            }
        }
    }
}