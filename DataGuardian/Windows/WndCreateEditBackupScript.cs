using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataGuardian.Controls;
using DataGuardian.Impl;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Core;
using DataGuardian.Properties;

namespace DataGuardian.Windows
{
    public partial class WndCreateEditBackupScript : Form
    {
        private readonly CreateScriptParameters _createScript;
        private readonly IBackupScript _editScript;

        private List<CtlBackupStep> CurrentStepsControl => tlpRoot.Controls.OfType<CtlBackupStep>().ToList();

        public IBackupScript NewBackupScript
        {
            get
            {
                if (_editScript is BackupScript bs)
                {
                    bs.Name = Text;
                    bs.BackupFileName = Path.GetFileName(bs.TargetPath);
                    bs.TargetPath = Text;
                    bs.Steps.Clear();
                    bs.Steps.AddRange(CurrentStepsControl.Select(x => x.Step));
                    return _editScript;
                }

                return new BackupScript(
                    _createScript?.Name ?? _editScript.Name,
                    _createScript?.TargetPath ?? _editScript.TargetPath,
                    Path.GetFileName(_createScript?.TargetPath ?? _editScript.TargetPath),
                    CurrentStepsControl.Select(x => x.Step));
            }
            private set
            {
                BackupName = value.Name;
                TargetPath = value.TargetPath;
                foreach(var step in value.Steps)
                    AddNewStep(step);
            }
        }

        public string BackupName
        {
            get => txtName.Text.Trim();
            set => txtName.Text = value?.Trim();
        }

        public string TargetPath
        {
            get => ctlPath1.SelectedPath;
            set => ctlPath1.SelectedPath = value;
        }

        public WndCreateEditBackupScript(IBackupScript backupScript, CreateScriptParameters createScriptParameters)
        {
            InitializeComponent();

            if (backupScript == null)
            {
                if (string.IsNullOrWhiteSpace(createScriptParameters.TargetPath))
                    throw new ArgumentException(nameof(createScriptParameters.TargetPath));

                Text = "Create script";
                _createScript = createScriptParameters;
                InitFirstStep();
            }
            else
            {
                Text = "Edit script";
                _editScript = backupScript;
                NewBackupScript = backupScript;
                InitControlsForScript();
            }
        }

        private void InitFirstStep()
        {
            SetName(_createScript.Name, _createScript.TargetPath);

            AddNewStep(new BackupStep {TargetPath = _createScript.TargetPath});
        }

        private void SetName(string name, string path)
        {
            Text = _createScript != null ? $"Create backup '{name}', [{path}]" : $"Edit backup '{name}', [{path}]";
        }

        private void InitControlsForScript()
        {
        }

        private void btnAddStep_Click(object sender, EventArgs e)
        {
            var count = CurrentStepsControl.Count();
            if (count > 0)
                AddNewStep(CurrentStepsControl[count - 1].Step.Clone() as IBackupStep);
            else
                AddNewStep(new BackupStep {TargetPath = _createScript.TargetPath});
        }

        private void AddNewStep(IBackupStep step)
        {
            var stepCtrl = new CtlBackupStep(CoreStatic.Instance.CloudAccountsManager.Accounts)
            {
                Dock = DockStyle.Fill,
                Name = $"stepCtrl{tlpRoot.RowCount}",
                Step = step
            };
            var lblNumber = new Label
            {
                Text = (CurrentStepsControl.Count + 1).ToString(),
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false,
                Name = $"lblNumber{tlpRoot.RowCount}"
            };
            var btn = new Button
            {
                BackgroundImage = Resources.Img_Close,
                BackgroundImageLayout = ImageLayout.Zoom,
                Size = new Size(50, 50),
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Name = $"btn{tlpRoot.RowCount}"
            };

            tlpRoot.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tlpRoot.RowCount += 1;
            var lastRowIndex = tlpRoot.RowCount - 1;
            var generalRowHeight = stepCtrl.MinimumSize.Height + stepCtrl.Margin.Horizontal;

            if (tlpRoot.RowStyles.Count < lastRowIndex + 1)
                tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, generalRowHeight));
            else
                tlpRoot.RowStyles[lastRowIndex] = new RowStyle(SizeType.Absolute, generalRowHeight);

            if (Bottom + generalRowHeight < Screen.PrimaryScreen.WorkingArea.Bottom)
                Height += (int) generalRowHeight;

            tlpRoot.Controls.Remove(btnAddStep);
            tlpRoot.Controls.Add(btnAddStep, 1, lastRowIndex);

            tlpRoot.Controls.Add(lblNumber, 0, tlpRoot.RowCount - 2);
            tlpRoot.Controls.Add(stepCtrl, 1, tlpRoot.RowCount - 2);
            tlpRoot.Controls.Add(btn, 2, tlpRoot.RowCount - 2);
        }
    }
}