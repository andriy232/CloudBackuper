using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DataGuardian.Plugins.Core;
using DataGuardian.Plugins.Plugins;
using DataGuardian.Windows;

namespace DataGuardian.Controls
{
    public partial class CtlCloudAccounts : UserControl
    {
        public CtlCloudAccounts()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
                FillData(CoreStatic.Instance.CloudAccountsManager.Accounts);
        }

        private void FillData(IEnumerable<ICloudProviderAccount> accounts)
        {
            dgvData.Rows.Clear();

            foreach (var account in accounts)
            {
                var row = dgvData.Rows[dgvData.Rows.Add()];
                row.Cells[clmName.Index].Value = account.Name;
                row.Cells[clmImage.Index].Value = account.CloudStorageProvider.Logo;
                row.Cells[clmType.Index].Value = account.CloudStorageProvider.Name;
            }
        }

        public ICloudProviderAccount SelectedCloudProvider
        {
            get => dgvData.SelectedRows.Cast<DataGridViewRow>()?.FirstOrDefault()?.Tag as ICloudProviderAccount;
            set
            {
                if (value != null)
                {
                    var row = dgvData.Rows.Cast<DataGridViewRow>()
                        .FirstOrDefault(x => x.Tag is ICloudProviderAccount bs && bs == value);

                    dgvData.ClearSelection();
                    if (row != null)
                        dgvData.Rows[row.Index].Selected = true;
                }
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            CoreStatic.Instance.CloudAccountsManager.AddAccount(this);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            CoreStatic.Instance.CloudAccountsManager.RemoveAccount(SelectedCloudProvider);
        }
    }
}