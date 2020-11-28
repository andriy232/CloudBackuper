using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Core;
using DataGuardian.Plugins.Plugins;

namespace DataGuardian.Windows
{
    public partial class WndSelectCloudProvider : Form
    {
        public WndSelectCloudProvider()
        {
            InitializeComponent();
        }

        public ICloudStorageProvider ActiveCloudProvider => dgvData.SelectedRows.Count > 0
            ? dgvData.SelectedRows[0].Tag as ICloudStorageProvider
            : null;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            FillData(CoreStatic.Instance.CloudStorageProviders);
        }

        private void FillData(IEnumerable<ICloudStorageProvider> cloudProviders)
        {
            dgvData.Rows.Clear();

            dgvData.RowTemplate.Height = 150;
            clmIcon.ImageLayout = DataGridViewImageCellLayout.Zoom;
            clmDescription.DefaultCellStyle.Font = new Font(Font.Name, 12f);
            clmName.DefaultCellStyle.Font = new Font(Font.Name, 12f);

            foreach (var provider in cloudProviders)
            {
                var row = dgvData.Rows[dgvData.Rows.Add()];
                row.Cells[clmName.Index].Value = provider.Name;
                row.Cells[clmDescription.Index].Value = provider.Description;
                row.Cells[clmIcon.Index].Value = provider.Logo;
                row.Tag = provider;
            }

            dgvData.ClearSelection();
            dgvData.Rows[dgvData.RowCount - 1].Cells[1].Selected = true;
        }
    }
}