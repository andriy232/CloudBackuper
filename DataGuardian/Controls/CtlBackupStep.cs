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
    public partial class CtlBackupStep : UserControl
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
                : BackupAction.Copy;
            set => cmbAction.SelectedItem = value;
        }

        public string SelectedActionParameter
        {
            get => txtActionParameter.Text?.Trim();
            set => txtActionParameter.Text = value?.Trim();
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

        public ICloudProviderAccount CloudAccount
        {
            get => cmbAccount.SelectedItem as ICloudProviderAccount;
            set => cmbAccount.SelectedItem = value;
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
                    st.Account = SelectedAccount;

                    st.Action = SelectedBackupAction;
                    st.ActionParameter = SelectedActionParameter;

                    st.Period = SelectedBackupPeriod;
                    st.PeriodParameters = SelectedPeriodParameters;
                    st.StartDate = SelectedStartDate;
                    st.RecurEvery = SelectedRecurEvery;
                    return _originalStep;
                }

                return new BackupStep
                {
                    TargetPath = SelectedPath,
                    Account = SelectedAccount,

                    Action = SelectedBackupAction,
                    ActionParameter = SelectedActionParameter,

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

                SelectedBackupPeriod = value.Period;
                SelectedActionParameter = value.ActionParameter;
                SelectedRecurEvery = value.RecurEvery;
                SelectedStartDate = value.StartDate;
            }
        }

        public ICloudProviderAccount SelectedAccount { get; set; }

        public CtlBackupStep(IEnumerable<ICloudProviderAccount> accounts)
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
            SelectedActionParameter = string.Empty;

            switch (SelectedBackupAction)
            {
                case BackupAction.BackupTo:
                    ShowCtrs(lblSelectAccount, cmbAccount);
                    HideCtrs(lblActionParameter, txtActionParameter);
                    break;
                case BackupAction.Copy:
                    HideCtrs(lblSelectAccount, cmbAccount);
                    ShowCtrs(lblActionParameter, txtActionParameter);
                    break;
                case BackupAction.SendToEmail:
                    HideCtrs(lblSelectAccount, cmbAccount);
                    ShowCtrs(lblActionParameter, txtActionParameter);
                    break;
                case BackupAction.Archive:
                    HideCtrs(lblSelectAccount, cmbAccount);
                    ShowCtrs(lblActionParameter, txtActionParameter);
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
                    break;
                case BackupPeriod.Daily:
                    ShowCtrs(lsbPeriodParameters, nudRecurEvery, lblRecurEvery, lblPeriodParameters);
                    break;
                case BackupPeriod.Weekly:
                    ShowCtrs(nudRecurEvery, lblRecurEvery);
                    HideCtrs(lsbPeriodParameters, lblPeriodParameters);
                    break;
                case BackupPeriod.Monthly:
                    ShowCtrs(lsbPeriodParameters, nudRecurEvery, lblRecurEvery, lblPeriodParameters);
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