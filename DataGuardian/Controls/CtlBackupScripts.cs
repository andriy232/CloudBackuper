using DataGuardian.GUI;
using DataGuardian.GUI.UserControls;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataGuardian.Impl;

namespace DataGuardian.Controls
{
    public partial class CtlBackupScripts : UserControlBase
    {
        public CtlBackupScripts()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            cmdDelete.Click += OnCmdDelete;
            cmdEdit.Click += OnCmdEdit;
            cmdToggleDisable.Click += OnCmdDisable;
            cmdPerformNow.Click += OnCmdPerform;

            if (DesignMode)
                return;

            FillData(Core.BackupManager.BackupScripts);

            Core.BackupManager.BackupScriptsChanged += OnBackupScriptsChanged;
        }

        private async void OnCmdPerform(object sender, EventArgs e)
        {
            try
            {
                if (SelectedBackupScript is BackupScript script)
                {
                    if (script.CurrentState == BackupCurrentState.Processing)
                        return;

                    await Core.BackupManager.Perform(script);
                }
            }
            catch (Exception ex)
            {
                Core.Logger.Log(nameof(Core.BackupManager.Perform), ex);
            }
        }

        private void OnCmdEdit(object sender, EventArgs e)
        {
            OnBtnEditClick(sender, e);
        }

        private void OnCmdDisable(object sender, EventArgs e)
        {
            try
            {
                if (SelectedBackupScript is BackupScript script)
                {
                    if (script.CurrentState == BackupCurrentState.Processing)
                        return;

                    script.Enabled = script.Enabled!;
                    Core.BackupManager.EditBackupScript(script, script);
                }
            }
            catch (Exception ex)
            {
                Core.Logger.Log(nameof(Core.BackupManager.EditBackupScript), ex);
            }
        }

        private void OnCmdDelete(object sender, EventArgs e)
        {
            btnDelete_Click(sender, e);
        }

        private void OnBackupScriptsChanged(object sender, IEnumerable<IBackupScript> e)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action<IEnumerable<IBackupScript>>) FillData, e);
            }
            else
            {
                FillData(e);
            }
        }

        private void FillData(IEnumerable<IBackupScript> backupScripts)
        {
            try
            {
                dgvData.Rows.Clear();

                foreach (var backupScript in backupScripts.OrderBy(x => x.LastPerformTime).ThenBy(x => x.Name))
                {
                    var row = dgvData.Rows[dgvData.Rows.Add()];
                    row.Cells[clmCreateTime.Index].Value = backupScript.CreateTimestamp;
                    row.Cells[clmEnabled.Index].Value = backupScript.Enabled;
                    row.Cells[clmLastPerform.Index].Value = backupScript.LastPerformTime;
                    row.Cells[clmNextPerformTime.Index].Value = backupScript.NextPerformTime;
                    row.Cells[clmLastState.Index].Value = backupScript.LastPerformState;
                    row.Cells[clmName.Index].Value = backupScript.Name;
                    row.Cells[clmTarget.Index].Value = backupScript.TargetPath;
                    SetCurrentState(row, backupScript);
                    row.Tag = backupScript;

                    backupScript.CurrentStateChanged += OnScriptStateChanged;
                }
            }
            catch (Exception ex)
            {
                Core.Logger.Log("FillData", ex);
            }
        }

        private void SetCurrentState(DataGridViewRow row, IBackupScript backupScript)
        {
            row.Cells[clmCurrentState.Index].Value = backupScript.LastPerformTime == DateTime.MinValue
                ? BackupCurrentState.NotStarted
                : backupScript.CurrentState;
        }

        private void OnScriptStateChanged(object sender, BackupCurrentState e)
        {
            try
            {
                if (sender is IBackupScript script)
                {
                    var row = dgvData.Rows.OfType<DataGridViewRow>().FirstOrDefault(x => x.Tag == script);
                    if (row != null)
                        SetCurrentState(row, script);
                }
            }
            catch
            {
                // ignored
            }
        }

        public IBackupScript SelectedBackupScript
        {
            get => dgvData.SelectedCells.Cast<DataGridViewCell>()?.FirstOrDefault()?.OwningRow.Tag as IBackupScript;
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
                Core.Logger.Log(nameof(btnDelete_Click), ex);
                GuiHelper.ShowMessage(ex);
            }
        }

        private void OnBtnEditClick(object sender, EventArgs e)
        {
            try
            {
                if (SelectedBackupScript != null)
                {
                    if (SelectedBackupScript.CurrentState == BackupCurrentState.Processing)
                    {
                        GuiHelper.ShowMessage("Current script processing. Please wait");
                    }
                    else
                    {
                        Core.BackupManager.EditBackupScriptGui(SelectedBackupScript);
                    }
                }
            }
            catch (Exception ex)
            {
                Core.Logger.Log(nameof(Core.BackupManager.EditBackupScriptGui), ex);
                GuiHelper.ShowMessage(ex);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                Core.BackupManager.CreateBackupScriptGui();
            }
            catch (Exception ex)
            {
                Core.Logger.Log(nameof(btnCreate_Click), ex);
                GuiHelper.ShowMessage(ex);
            }
        }

        private void dgvData_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OnBtnEditClick(sender, e);
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == clmEnabled.Index)
                {
                    var dataGridView = (DataGridView) sender;
                    var cell = dataGridView[clmEnabled.Index, e.RowIndex];

                    if (SelectedBackupScript is BackupScript script)
                    {
                        if (script.CurrentState == BackupCurrentState.Processing)
                            return;

                        cell.Value = !(bool) cell.Value;

                        script.Enabled = (bool) cell.Value;
                        Core.BackupManager.EditBackupScript(script, script);
                    }
                }
            }
            catch (Exception ex)
            {
                Core.Logger.Log(nameof(Core.BackupManager.EditBackupScript), ex);
            }
        }
    }
}