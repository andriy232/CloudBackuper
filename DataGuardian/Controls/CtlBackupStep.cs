using DataGuardian.GUI.UserControls;
using DataGuardian.Impl;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Plugins;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DataGuardian.Controls
{
    public partial class CtlBackupStep : UserControlBase
    {
        private IBackupStep _originalStep;

        public string SelectedPath
        {
            get => ctlPath.SelectedPath;
            set => ctlPath.SelectedPath = value;
        }

        public BackupAction SelectedBackupAction
        {
            get => Enum.TryParse<BackupAction>(cmbAction.SelectedValue.ToString(), out var action)
                ? action
                : BackupAction.CopyTo;
            set => cmbAction.SelectedItem = value;
        }

        public string SelectedBackupFileName
        {
            get => txtBackupFileName.Text?.Trim();
            set => txtBackupFileName.Text = value?.Trim();
        }

        public string SelectedActionParameter
        {
            get => ctlBackupPath.Text?.Trim();
            set => ctlBackupPath.Text = value?.Trim();
        }

        public BackupPeriod SelectedBackupPeriod
        {
            get => Enum.TryParse<BackupPeriod>(cmbPeriod.SelectedValue.ToString(), out var period)
                ? period
                : BackupPeriod.Daily;
            set => cmbPeriod.SelectedItem = value;
        }

        public int SelectedRecurEvery
        {
            get => Convert.ToInt32(nudRecurEvery.Value);
            set => nudRecurEvery.Value = value;
        }

        public DateTime SelectedStartDate
        {
            get => dtpStartTime.Value;
            set => dtpStartTime.Value = value;
        }

        public IEnumerable<string> SelectedPeriodParameters
        {
            get => lsbPeriodParameters.CheckedItems.Cast<string>();
            set
            {
                if (value == null)
                    return;

                foreach (var item in value)
                {
                    var itemIndex = lsbPeriodParameters.Items.Cast<string>().ToList().IndexOf(item);
                    if (itemIndex >= 0)
                        lsbPeriodParameters.SetItemCheckState(itemIndex, CheckState.Checked);
                }
            }
        }

        public string State
        {
            get => lblState.Text?.Trim();
            set => lblState.Text = value?.Trim();
        }

        public IBackupStep Step
        {
            get
            {
                if (_originalStep is BackupStep st)
                {
                    st.TargetPath = SelectedPath;
                    st.Account = (CloudProviderAccount) SelectedAccount;

                    st.Action = SelectedBackupAction;
                    st.ActionParameter = SelectedActionParameter;
                    st.BackupFileName = SelectedBackupFileName;

                    st.Period = SelectedBackupPeriod;
                    st.PeriodParameters = SelectedPeriodParameters;
                    st.StartDate = SelectedStartDate;
                    st.RecurEvery = SelectedRecurEvery;
                    return _originalStep;
                }

                return new BackupStep
                {
                    TargetPath = SelectedPath,
                    Account = (CloudProviderAccount)SelectedAccount,

                    Action = SelectedBackupAction,
                    ActionParameter = SelectedActionParameter,
                    BackupFileName = SelectedBackupFileName,

                    Period = SelectedBackupPeriod,
                    PeriodParameters = SelectedPeriodParameters,
                    RecurEvery = SelectedRecurEvery,
                    StartDate = SelectedStartDate,
                };
            }
            set
            {
                _originalStep = value;
                SelectedPath = value.TargetPath;
                SelectedAccount = value.Account;

                SelectedBackupAction = value.Action;
                SelectedActionParameter = value.ActionParameter;
                SelectedBackupFileName = value.BackupFileName;

                SelectedBackupPeriod = value.Period;
                SelectedPeriodParameters = value.PeriodParameters;
                SelectedRecurEvery = value.RecurEvery;
                SelectedStartDate = value.StartDate;
                State = string.IsNullOrWhiteSpace(value.LastState) ? "Not performed yet" : value.LastState;
            }
        }

        public IAccount SelectedAccount
        {
            get => cmbAccount.SelectedItem as IAccount;
            set => cmbAccount.SelectedItem = value;
        }

        public CtlBackupStep(IEnumerable<IAccount> accounts)
        {
            InitializeComponent();

            cmbAction.DataSource = Enum.GetValues(typeof(BackupAction));
            cmbPeriod.DataSource = Enum.GetValues(typeof(BackupPeriod));
            cmbAccount.DataSource = accounts?.ToList();

            foreach (var label in tlpRoot.Controls.OfType<Label>())
                label.BackColor = Color.Transparent;
        }

        protected override void OnLoad(EventArgs e)
        {
            var r = new Random();
            BackColor = Color.FromArgb(100, r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));

            dtpStartTime.Value = DateTime.Today.AddDays(1);
            dtpStartTime.MinDate = DateTime.Today;

            base.OnLoad(e);
        }

        private void cmbAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (SelectedBackupAction)
            {
                case BackupAction.BackupTo:
                    lblActionParameter.Text = "Enter backup FileName";

                    HideCtrs(lblEnterPath, ctlBackupPath);
                    ShowCtrs(lblSelectAccount, cmbAccount, lblActionParameter, txtBackupFileName);

                    cmbAccount.DataSource = Core.CloudAccountsManager.Accounts.ToList();
                    break;
                case BackupAction.RestoreTo:
                    lblActionParameter.Text = "Enter backup FileName";

                    ShowCtrs(lblSelectAccount, cmbAccount, lblActionParameter, txtBackupFileName, lblEnterPath, ctlBackupPath);

                    cmbAccount.DataSource = Core.CloudAccountsManager.Accounts.ToList();
                    break;
                case BackupAction.CopyTo:
                    HideCtrs(lblSelectAccount, cmbAccount, lblActionParameter, txtBackupFileName);
                    ShowCtrs(lblEnterPath, ctlBackupPath);
                    break;
                case BackupAction.SendToEmail:
                    HideCtrs(lblSelectAccount, cmbAccount, lblEnterPath, ctlBackupPath);
                    ShowCtrs(lblActionParameter, txtBackupFileName);
                    break;
                case BackupAction.Archive:
                    HideCtrs(lblSelectAccount, cmbAccount, lblActionParameter, txtBackupFileName);
                    ShowCtrs(lblEnterPath, ctlBackupPath);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void cmbPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (SelectedBackupPeriod)
            {
                case BackupPeriod.OneTime:
                    HideCtrs(lsbPeriodParameters, nudRecurEvery, lblRecurEvery, lblPeriodParameters);
                    lsbPeriodParameters.DataSource = null;
                    break;
                case BackupPeriod.Daily:
                    HideCtrs(lsbPeriodParameters, lblPeriodParameters);
                    ShowCtrs(nudRecurEvery, lblRecurEvery);
                    lsbPeriodParameters.DataSource = null;
                    break;
                case BackupPeriod.Weekly:
                    ShowCtrs(nudRecurEvery, lblRecurEvery, lsbPeriodParameters, lblPeriodParameters);
                    lsbPeriodParameters.DataSource = Enum.GetNames(typeof(DayOfWeek)).ToList();
                    break;
                case BackupPeriod.Monthly:
                    ShowCtrs(lsbPeriodParameters, nudRecurEvery, lblRecurEvery, lblPeriodParameters);
                    lsbPeriodParameters.DataSource = Enum.GetNames(typeof(Month)).ToList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HideCtrs(params Control[] controls)
        {
            foreach (var ctrl in controls)
                ctrl.Visible = false;
        }

        private void ShowCtrs(params Control[] controls)
        {
            foreach (var ctrl in controls)
                ctrl.Visible = true;
        }
    }
}