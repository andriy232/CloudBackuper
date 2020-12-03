using DataGuardian.Plugins;
using DataGuardian.Plugins.Core;
using DataGuardian.Plugins.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DataGuardian.GUI;
using DataGuardian.GUI.Controls;

namespace DataGuardian.Controls
{
    public partial class CtlCloudAccounts : UserControlBase
    {
        public CtlCloudAccounts()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                FillData(Core.CloudAccountsManager.Accounts);
                Core.CloudAccountsManager.AccountsChanged += OnAccountsChanged;
                ctlFilter.FilterChanged += OnFilterChanged;
            }
        }

        private void OnFilterChanged(object sender, string filter)
        {
            GuiHelper.FilterChanged(dgvData, filter);
        }

        private void OnAccountsChanged(object sender, AccountsChangedEventArgs e)
        {
            FillData(e.Accounts);
        }

        private void FillData(IEnumerable<IAccount> accounts)
        {
            try
            {
                dgvData.Rows.Clear();

                foreach (var account in accounts)
                {
                    var row = dgvData.Rows[dgvData.Rows.Add()];
                    row.Cells[clmName.Index].Value = account.Name;
                    row.Cells[clmImage.Index].Value = account.CloudStorageProvider.Logo;
                    row.Cells[clmType.Index].Value = account.CloudStorageProvider.Name;
                    row.Tag = account;
                }
            }
            catch (Exception ex)
            {
                Core.Logger.Log("FillData", ex);
            }
        }

        public IAccount SelectedCloudProvider
        {
            get =>
                dgvData.SelectedCells.Cast<DataGridViewCell>()?.FirstOrDefault()?.OwningRow.Tag as IAccount
            ;
            set
            {
                if (value != null)
                {
                    var row = dgvData.Rows.Cast<DataGridViewRow>()
                        .FirstOrDefault(x => x.Tag is IAccount bs && bs == value);

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