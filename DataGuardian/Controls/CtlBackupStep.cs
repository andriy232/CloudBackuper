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
            set => cmbAction.SelectedValue = value;
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

        public CtlBackupStep()
        {
            InitializeComponent();

            cmbAction.DataSource = Enum.GetValues(typeof(BackupAction));
            cmbPeriod.DataSource = Enum.GetValues(typeof(BackupPeriod));

            foreach (var label in tlpRoot.Controls.OfType<Label>())
                label.BackColor = Color.Transparent;
        }

        protected override void OnLoad(EventArgs e)
        {
            var r = new Random();
            BackColor = Color.FromArgb(100, r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));

            base.OnLoad(e);
        }

        private void cmbAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedActionParameter = string.Empty;
        }

        private void cmbPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (SelectedBackupPeriod)
            {
                case BackupPeriod.OneTime:
                    lsbPeriodParameters.Visible = false;
                    break;
                case BackupPeriod.Daily:
                    break;
                case BackupPeriod.Weekly:
                    break;
                case BackupPeriod.Monthly:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}