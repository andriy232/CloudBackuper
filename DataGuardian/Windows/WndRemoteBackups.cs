using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataGuardian.GUI;
using DataGuardian.GUI.Controls;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Backups;
using DataGuardian.Properties;

namespace DataGuardian.Windows
{
    public partial class WndRemoteBackups : FormBase
    {
        public WndRemoteBackups(ICore core) : base(core)
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                InitColumns();
            }
            catch (Exception ex)
            {
                Core.Logger.Log(ex);
            }
        }

        public void FillData(IEnumerable<RemoteBackupsState> states)
        {
            try
            {
                InitItems(states);
            }
            catch (Exception ex)
            {
                Core.Logger.Log(ex);
            }
        }

        private void InitItems(IEnumerable<RemoteBackupsState> states)
        {
            lvData.Items.Clear();

            foreach (var state in states.OrderBy(x => x.BackupName))
            {
                var group = new ListViewGroup($"[{state.BackupName}] at [{state.CloudStorageProvider}]")
                {
                    Tag = state
                };
                lvData.Groups.Add(@group);

                if (state.Backups.Count == 0)
                {
                    var item = new ListViewItem(new[]
                    {
                        "-",
                        "-",
                        "-",
                        "-"
                    })
                    {
                        Group = @group,
                    };
                    lvData.Items.Add(item);
                }
                else
                {
                    foreach (var backup in state.Backups.OrderBy(x => x.ModifiedDate))
                    {
                        var item = new ListViewItem(new[]
                        {
                            "-",
                            backup.UniqueId,
                            backup.BackupName,
                            backup.ModifiedDate.ToString(CultureInfo.CurrentCulture)
                        })
                        {
                            Group = @group,
                            Tag = backup
                        };
                        lvData.Items.Add(item);
                    }
                }
            }

            lvData.Items[lvData.Items.Count - 1].EnsureVisible();
        }

        private void InitColumns()
        {
            lvData.Columns.Clear();
            var maxWidth = lvData.Width - 30;

            var index = lvData.Columns.Add(new ColumnHeader());
            lvData.Columns[index].Text = "Backup Name";
            lvData.Columns[index].Width = maxWidth / 100 * 15;

            index = lvData.Columns.Add(new ColumnHeader());
            lvData.Columns[index].Text = "Id";
            lvData.Columns[index].Width = maxWidth / 100 * 35;

            index = lvData.Columns.Add(new ColumnHeader());
            lvData.Columns[index].Text = "Backup FileName";
            lvData.Columns[index].Width = maxWidth / 100 * 35;

            index = lvData.Columns.Add(new ColumnHeader());
            lvData.Columns[index].Text = "Last Modify Timestamp";
            lvData.Columns[index].Width = maxWidth / 100 * 15;
        }

        private async void OnCmdDeleteBackupClick(object sender, EventArgs e)
        {
            try
            {
                if (lvData.SelectedItems.Count == 0)
                    return;

                await DeleteRemote(new[] {lvData.SelectedItems.Cast<ListViewItem>().FirstOrDefault()});
            }
            catch (Exception ex)
            {
                GuiHelper.ShowMessage(string.Format(Resources.Str_WndRemoteBackups_CannotDeleteRemote, ex));
            }
        }

        private async void OnCmdDeleteGroupBackupsClick(object sender, EventArgs e)
        {
            try
            {
                if (lvData.SelectedItems.Count == 0)
                    return;

                await DeleteRemote(lvData.SelectedItems.Cast<ListViewItem>());
            }
            catch (Exception ex)
            {
                GuiHelper.ShowMessage(string.Format(Resources.Str_WndRemoteBackups_CannotDeleteRemote, ex));
            }
        }

        private async Task DeleteRemote(IEnumerable<ListViewItem> items)
        {
            foreach (var listViewItem in items)
            {
                var remoteBackup = listViewItem.Tag as RemoteBackup;
                var remoteBackupsState = listViewItem.Group.Tag as RemoteBackupsState;

                if (remoteBackup == null || remoteBackupsState == null)
                    return;

                await Core.BackupManager.DeleteRemoteBackup(remoteBackupsState.Parent, remoteBackup);
                
                lvData.Items.Remove(listViewItem);
            }
        }
    }
}