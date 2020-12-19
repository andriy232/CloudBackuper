using DataGuardian.Controls;
using DataGuardian.GUI;
using DataGuardian.Impl;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
                    bs.Name = BackupName;
                    bs.TargetPath = TargetPath;
                    bs.Steps = CurrentStepsControl.Select(x => x.Step).ToList();
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
                foreach (var step in value.Steps)
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
            get => ctlPath.SelectedPath;
            set => ctlPath.SelectedPath = value;
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
            }
        }

        private void TryApplyNewWindowSize()
        {
            var workingArea = Screen.PrimaryScreen.WorkingArea;

            Height = Math.Min(workingArea.Bottom - Top, tlpRoot.PreferredSize.Height + tlpRoot.Margin.Vertical + Margin.Vertical + 50);
        }

        private void InitFirstStep()
        {
            SetName(_createScript.Name, _createScript.TargetPath);

            AddNewStep(new BackupStep
            {
                TargetPath = _createScript.TargetPath,
                BackupFileName = Path.GetFileNameWithoutExtension(_createScript.TargetPath)
            });
        }

        private void SetName(string name, string path)
        {
            BackupName = name;
            TargetPath = path;
        }

        private void btnAddStep_Click(object sender, EventArgs e)
        {
            try
            {
                var count = CurrentStepsControl.Count;
                if (count > 0)
                    AddNewStep(CurrentStepsControl[count - 1].Step.Clone() as IBackupStep);
                else
                    AddNewStep(new BackupStep
                    {
                        TargetPath = _createScript.TargetPath,
                        BackupFileName = Path.GetFileNameWithoutExtension(_createScript.TargetPath)
                    });
            }
            catch (Exception ex)
            {
                GuiHelper.ShowMessage(ex);
            }
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
                BackgroundImage = Plugins.Properties.Resources.Img_Close,
                BackgroundImageLayout = ImageLayout.Zoom,
                Size = new Size(50, 50),
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Name = $"btn{tlpRoot.RowCount}"
            };
            btn.Click += OnBtnRemoveClick;

            tlpRoot.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tlpRoot.RowCount += 1;
            var lastRowIndex = tlpRoot.RowCount - 1;
            var generalRowHeight = GetCtrlHeight(stepCtrl) + 10;

            // set row styles
            if (tlpRoot.RowStyles.Count < lastRowIndex + 1)
                tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, generalRowHeight));
            for (var index = 2; index < tlpRoot.RowStyles.Count; index++)
            {
                if (tlpRoot.GetControlFromPosition(1, index) != null)
                    tlpRoot.RowStyles[index].Height = generalRowHeight;
            }

            // move control buttons to the bottom
            tlpRoot.Controls.Remove(btnCancel);
            tlpRoot.Controls.Remove(btnAddStep);
            tlpRoot.Controls.Remove(btnSave);
            tlpRoot.Controls.Add(btnCancel, 0, lastRowIndex);
            tlpRoot.Controls.Add(btnAddStep, 1, lastRowIndex);
            tlpRoot.Controls.Add(btnSave, 2, lastRowIndex);

            // add new step controls
            tlpRoot.Controls.Add(lblNumber, 0, tlpRoot.RowCount - 2);
            tlpRoot.Controls.Add(stepCtrl, 1, tlpRoot.RowCount - 2);
            tlpRoot.Controls.Add(btn, 2, tlpRoot.RowCount - 2);

            TryApplyNewWindowSize();
        }

        private static int GetCtrlHeight(Control stepCtrl)
        {
            return stepCtrl.MinimumSize.Height + stepCtrl.Margin.Horizontal;
        }

        private void OnBtnRemoveClick(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button btn)
                {
                    var cellPosition = tlpRoot.GetCellPosition(btn);

                    var lbl = tlpRoot.GetControlFromPosition(0, cellPosition.Row);
                    var ctlStep = tlpRoot.GetControlFromPosition(1, cellPosition.Row);

                    btn.Dispose();
                    tlpRoot.Controls.Remove(btn);
                    lbl.Dispose();
                    tlpRoot.Controls.Remove(lbl);
                    ctlStep.Dispose();
                    tlpRoot.Controls.Remove(ctlStep);

                    tlpRoot.RowStyles[cellPosition.Row].Height = 0;

                    TryApplyNewWindowSize();
                }
            }
            catch (Exception ex)
            {
                GuiHelper.ShowMessage(ex);
            }
        }
    }
}