// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.Forms.FormConfigureStatus
// Assembly: Attrice.TeamFoundation.Controls.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=1a24c887ab53aa6e
// MVID: 393BE899-3C29-4A70-86D7-B4B965E5A8F5
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controls.12.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Attrice.TeamFoundation.Controls.Forms
{
  public class FormConfigureStatus : Form
  {
    private IContainer components;
    private GroupBox groupBox1;
    private CheckBox checkBoxServerPath;
    private CheckBox checkBoxLockType;
    private CheckBox checkBoxItemType;
    private CheckBox checkBoxChangeDate;
    private CheckBox checkBoxLocalPath;
    private CheckBox checkBoxComputerName;
    private CheckBox checkBoxChangeset;
    private CheckBox checkBoxOwnerName;
    private CheckBox checkBoxWorkspace;
    private CheckBox checkBoxChangeType;
    private CheckBox checkBoxItemName;
    private Button buttonCancel;
    private Button buttonOk;

    public FormConfigureStatus() => InitializeComponent();

    private void buttonOk_Click(object sender, EventArgs e)
    {
      StatusViewConfiguration.Instance.ColumnChangeType = checkBoxChangeType.Checked;
      StatusViewConfiguration.Instance.ColumnLockType = checkBoxLockType.Checked;
      StatusViewConfiguration.Instance.ColumnItemName = checkBoxItemName.Checked;
      StatusViewConfiguration.Instance.ColumnItemType = checkBoxItemType.Checked;
      StatusViewConfiguration.Instance.ColumnOwnerName = checkBoxOwnerName.Checked;
      StatusViewConfiguration.Instance.ColumnWorkspace = checkBoxWorkspace.Checked;
      StatusViewConfiguration.Instance.ColumnComputerName = checkBoxComputerName.Checked;
      StatusViewConfiguration.Instance.ColumnVersion = checkBoxChangeset.Checked;
      StatusViewConfiguration.Instance.ColumnLocalPath = checkBoxLocalPath.Checked;
      StatusViewConfiguration.Instance.ColumnServerPath = checkBoxServerPath.Checked;
      StatusViewConfiguration.Instance.ColumnChangeDate = checkBoxChangeDate.Checked;
      if (!checkBoxChangeType.Checked && !checkBoxLockType.Checked && !checkBoxItemName.Checked && !checkBoxItemType.Checked && !checkBoxOwnerName.Checked && !checkBoxWorkspace.Checked && !checkBoxComputerName.Checked && !checkBoxChangeset.Checked && !checkBoxLocalPath.Checked && !checkBoxServerPath.Checked && !checkBoxChangeDate.Checked)
        StatusViewConfiguration.Instance.ColumnItemName = true;
      DialogResult = DialogResult.OK;
      Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      Close();
    }

    private void FormConfigureStatus_Shown(object sender, EventArgs e)
    {
      checkBoxChangeType.Checked = StatusViewConfiguration.Instance.ColumnChangeType;
      checkBoxLockType.Checked = StatusViewConfiguration.Instance.ColumnLockType;
      checkBoxItemName.Checked = StatusViewConfiguration.Instance.ColumnItemName;
      checkBoxItemType.Checked = StatusViewConfiguration.Instance.ColumnItemType;
      checkBoxOwnerName.Checked = StatusViewConfiguration.Instance.ColumnOwnerName;
      checkBoxWorkspace.Checked = StatusViewConfiguration.Instance.ColumnWorkspace;
      checkBoxComputerName.Checked = StatusViewConfiguration.Instance.ColumnComputerName;
      checkBoxChangeset.Checked = StatusViewConfiguration.Instance.ColumnVersion;
      checkBoxLocalPath.Checked = StatusViewConfiguration.Instance.ColumnLocalPath;
      checkBoxServerPath.Checked = StatusViewConfiguration.Instance.ColumnServerPath;
      checkBoxChangeDate.Checked = StatusViewConfiguration.Instance.ColumnChangeDate;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && components != null)
        components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      var componentResourceManager = new ComponentResourceManager(typeof (FormConfigureStatus));
      groupBox1 = new GroupBox();
      checkBoxServerPath = new CheckBox();
      checkBoxLockType = new CheckBox();
      checkBoxItemType = new CheckBox();
      checkBoxChangeDate = new CheckBox();
      checkBoxLocalPath = new CheckBox();
      checkBoxComputerName = new CheckBox();
      checkBoxChangeset = new CheckBox();
      checkBoxOwnerName = new CheckBox();
      checkBoxWorkspace = new CheckBox();
      checkBoxChangeType = new CheckBox();
      checkBoxItemName = new CheckBox();
      buttonCancel = new Button();
      buttonOk = new Button();
      groupBox1.SuspendLayout();
      SuspendLayout();
      groupBox1.Controls.Add(checkBoxServerPath);
      groupBox1.Controls.Add(checkBoxLockType);
      groupBox1.Controls.Add(checkBoxItemType);
      groupBox1.Controls.Add(checkBoxChangeDate);
      groupBox1.Controls.Add(checkBoxLocalPath);
      groupBox1.Controls.Add(checkBoxComputerName);
      groupBox1.Controls.Add(checkBoxChangeset);
      groupBox1.Controls.Add(checkBoxOwnerName);
      groupBox1.Controls.Add(checkBoxWorkspace);
      groupBox1.Controls.Add(checkBoxChangeType);
      groupBox1.Controls.Add(checkBoxItemName);
      groupBox1.Location = new Point(14, 12);
      groupBox1.Name = "groupBox1";
      groupBox1.Size = new Size(301, 238);
      groupBox1.TabIndex = 28;
      groupBox1.TabStop = false;
      groupBox1.Text = "Status columns to display ";
      checkBoxServerPath.AutoSize = true;
      checkBoxServerPath.FlatStyle = FlatStyle.System;
      checkBoxServerPath.Location = new Point(180, 171);
      checkBoxServerPath.Name = "checkBoxServerPath";
      checkBoxServerPath.Size = new Size(87, 18);
      checkBoxServerPath.TabIndex = 36;
      checkBoxServerPath.Text = "Server path";
      checkBoxServerPath.UseVisualStyleBackColor = true;
      checkBoxLockType.AutoSize = true;
      checkBoxLockType.FlatStyle = FlatStyle.System;
      checkBoxLockType.Location = new Point(22, 99);
      checkBoxLockType.Name = "checkBoxLockType";
      checkBoxLockType.Size = new Size(79, 18);
      checkBoxLockType.TabIndex = 35;
      checkBoxLockType.Text = "Lock type";
      checkBoxLockType.UseVisualStyleBackColor = true;
      checkBoxItemType.AutoSize = true;
      checkBoxItemType.FlatStyle = FlatStyle.System;
      checkBoxItemType.Location = new Point(22, 171);
      checkBoxItemType.Name = "checkBoxItemType";
      checkBoxItemType.Size = new Size(75, 18);
      checkBoxItemType.TabIndex = 34;
      checkBoxItemType.Text = "Item type";
      checkBoxItemType.UseVisualStyleBackColor = true;
      checkBoxChangeDate.AutoSize = true;
      checkBoxChangeDate.FlatStyle = FlatStyle.System;
      checkBoxChangeDate.Location = new Point(22, 27);
      checkBoxChangeDate.Name = "checkBoxChangeDate";
      checkBoxChangeDate.Size = new Size(93, 18);
      checkBoxChangeDate.TabIndex = 33;
      checkBoxChangeDate.Text = "Change date";
      checkBoxChangeDate.UseVisualStyleBackColor = true;
      checkBoxLocalPath.AutoSize = true;
      checkBoxLocalPath.FlatStyle = FlatStyle.System;
      checkBoxLocalPath.Location = new Point(180, 135);
      checkBoxLocalPath.Name = "checkBoxLocalPath";
      checkBoxLocalPath.Size = new Size(82, 18);
      checkBoxLocalPath.TabIndex = 32;
      checkBoxLocalPath.Text = "Local path";
      checkBoxLocalPath.UseVisualStyleBackColor = true;
      checkBoxComputerName.AutoSize = true;
      checkBoxComputerName.FlatStyle = FlatStyle.System;
      checkBoxComputerName.Location = new Point(180, 99);
      checkBoxComputerName.Name = "checkBoxComputerName";
      checkBoxComputerName.Size = new Size(106, 18);
      checkBoxComputerName.TabIndex = 31;
      checkBoxComputerName.Text = "Computer name";
      checkBoxComputerName.UseVisualStyleBackColor = true;
      checkBoxChangeset.AutoSize = true;
      checkBoxChangeset.FlatStyle = FlatStyle.System;
      checkBoxChangeset.Location = new Point(180, 27);
      checkBoxChangeset.Name = "checkBoxChangeset";
      checkBoxChangeset.Size = new Size(67, 18);
      checkBoxChangeset.TabIndex = 30;
      checkBoxChangeset.Text = "Version";
      checkBoxChangeset.UseVisualStyleBackColor = true;
      checkBoxOwnerName.AutoSize = true;
      checkBoxOwnerName.FlatStyle = FlatStyle.System;
      checkBoxOwnerName.Location = new Point(22, 207);
      checkBoxOwnerName.Name = "checkBoxOwnerName";
      checkBoxOwnerName.Size = new Size(92, 18);
      checkBoxOwnerName.TabIndex = 29;
      checkBoxOwnerName.Text = "Owner name";
      checkBoxOwnerName.UseVisualStyleBackColor = true;
      checkBoxWorkspace.AutoSize = true;
      checkBoxWorkspace.FlatStyle = FlatStyle.System;
      checkBoxWorkspace.Location = new Point(180, 63);
      checkBoxWorkspace.Name = "checkBoxWorkspace";
      checkBoxWorkspace.Size = new Size(87, 18);
      checkBoxWorkspace.TabIndex = 28;
      checkBoxWorkspace.Text = "Workspace";
      checkBoxWorkspace.UseVisualStyleBackColor = true;
      checkBoxChangeType.AutoSize = true;
      checkBoxChangeType.FlatStyle = FlatStyle.System;
      checkBoxChangeType.Location = new Point(22, 63);
      checkBoxChangeType.Name = "checkBoxChangeType";
      checkBoxChangeType.Size = new Size(92, 18);
      checkBoxChangeType.TabIndex = 27;
      checkBoxChangeType.Text = "Change type";
      checkBoxChangeType.UseVisualStyleBackColor = true;
      checkBoxItemName.AutoSize = true;
      checkBoxItemName.FlatStyle = FlatStyle.System;
      checkBoxItemName.Location = new Point(22, 135);
      checkBoxItemName.Name = "checkBoxItemName";
      checkBoxItemName.Size = new Size(81, 18);
      checkBoxItemName.TabIndex = 26;
      checkBoxItemName.Text = "Item name";
      checkBoxItemName.UseVisualStyleBackColor = true;
      buttonCancel.DialogResult = DialogResult.Cancel;
      buttonCancel.FlatStyle = FlatStyle.System;
      buttonCancel.Location = new Point(167, 267);
      buttonCancel.Name = "buttonCancel";
      buttonCancel.Size = new Size(75, 23);
      buttonCancel.TabIndex = 30;
      buttonCancel.Text = "Cancel";
      buttonCancel.UseVisualStyleBackColor = true;
      buttonCancel.Click += buttonCancel_Click;
      buttonOk.DialogResult = DialogResult.Cancel;
      buttonOk.FlatStyle = FlatStyle.System;
      buttonOk.Location = new Point(86, 267);
      buttonOk.Name = "buttonOk";
      buttonOk.Size = new Size(75, 23);
      buttonOk.TabIndex = 29;
      buttonOk.Text = "OK";
      buttonOk.UseVisualStyleBackColor = true;
      buttonOk.Click += buttonOk_Click;
      AcceptButton = buttonOk;
      AutoScaleDimensions = new SizeF(6f, 13f);
      AutoScaleMode = AutoScaleMode.Font;
      CancelButton = buttonCancel;
      ClientSize = new Size(329, 300);
      Controls.Add(buttonCancel);
      Controls.Add(buttonOk);
      Controls.Add(groupBox1);
      FormBorderStyle = FormBorderStyle.FixedDialog;
      Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      Name = nameof (FormConfigureStatus);
      StartPosition = FormStartPosition.CenterScreen;
      Text = "Configure list columns";
      Shown += FormConfigureStatus_Shown;
      groupBox1.ResumeLayout(false);
      groupBox1.PerformLayout();
      ResumeLayout(false);
    }
  }
}
