using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Backups;

namespace DataGuardian.Controls
{
    public partial class WndRemoteBackups : Form
    {
        private ICore _core;

        public WndRemoteBackups(ICore core)
        {
            InitializeComponent();
            _core = core;
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
                _core.Logger.Log(ex);
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
                _core.Logger.Log(ex);
            }
        }

        private void InitItems(IEnumerable<RemoteBackupsState> states)
        {
            lvData.Items.Clear();

            foreach (var state in states.OrderBy(x => x.BackupName))
            {
                var group = new ListViewGroup(state.BackupName);
                lvData.Groups.Add(@group);

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
                        Group = @group
                    };
                    lvData.Items.Add(item);
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
    }
}