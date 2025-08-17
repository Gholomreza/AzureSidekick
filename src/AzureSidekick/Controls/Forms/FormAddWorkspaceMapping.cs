// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.Forms.FormAddWorkspaceMapping
// Assembly: Attrice.TeamFoundation.Controls.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=1a24c887ab53aa6e
// MVID: 393BE899-3C29-4A70-86D7-B4B965E5A8F5
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controls.12.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Attrice.TeamFoundation.Controls.Forms
{
  public class FormAddWorkspaceMapping : Form
  {
    private IContainer components;
    private GroupBox groupBox1;
    private TextBox textBoxSourceComputer;
    private Label label6;
    private TextBox textBoxSourceUserName;
    private Label label2;
    private TextBox textBoxSourceName;
    private Label label1;
    private TextBox textBoxLocalFolder;
    private TextBox textBoxSourceFolder;
    private Label label3;
    private Label label4;
    private Button buttonSelectSourceFolder;
    private Button buttonSelectLocalFolder;
    private Button buttonOk;
    private Button buttonCancel;
    private FolderBrowserDialog folderBrowserDialog1;

    public FormAddWorkspaceMapping() => InitializeComponent();

    public string WorkspaceName
    {
      get => textBoxSourceName.Text;
      set => textBoxSourceName.Text = value;
    }

    public string WorkspaceOwner
    {
      get => textBoxSourceUserName.Text;
      set => textBoxSourceUserName.Text = value;
    }

    public string WorkspaceComputer
    {
      get => textBoxSourceComputer.Text;
      set => textBoxSourceComputer.Text = value;
    }

    public string ServerPath
    {
      get => textBoxSourceFolder.Text;
      set => textBoxSourceFolder.Text = value;
    }

    public string LocalPath
    {
      get => textBoxLocalFolder.Text;
      set => textBoxLocalFolder.Text = value;
    }

    private void buttonSelectLocalFolder_Click(object sender, EventArgs e)
    {
      if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
        return;
      textBoxSourceFolder.Text = folderBrowserDialog1.SelectedPath;
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      Close();
    }

    private void buttonOk_Click(object sender, EventArgs e)
    {
      if (textBoxSourceFolder.TextLength == 0)
      {
        var num = (int) MessageBox.Show("Please select source control folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        textBoxSourceFolder.Focus();
      }
      else if (textBoxSourceFolder.Text.IndexOf("$/") != 0 || textBoxSourceFolder.TextLength < 3)
      {
        var num = (int) MessageBox.Show("Please select valid source control folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        textBoxSourceFolder.Focus();
      }
      else if (textBoxLocalFolder.TextLength == 0)
      {
        var num = (int) MessageBox.Show("Please select local folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        textBoxLocalFolder.Focus();
      }
      else
      {
        DialogResult = DialogResult.OK;
        Close();
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && components != null)
        components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      var componentResourceManager = new ComponentResourceManager(typeof (FormAddWorkspaceMapping));
      groupBox1 = new GroupBox();
      textBoxSourceComputer = new TextBox();
      label6 = new Label();
      textBoxSourceUserName = new TextBox();
      label2 = new Label();
      textBoxSourceName = new TextBox();
      label1 = new Label();
      textBoxLocalFolder = new TextBox();
      textBoxSourceFolder = new TextBox();
      label3 = new Label();
      label4 = new Label();
      buttonSelectSourceFolder = new Button();
      buttonSelectLocalFolder = new Button();
      buttonOk = new Button();
      buttonCancel = new Button();
      folderBrowserDialog1 = new FolderBrowserDialog();
      groupBox1.SuspendLayout();
      SuspendLayout();
      groupBox1.Controls.Add(textBoxSourceComputer);
      groupBox1.Controls.Add(label6);
      groupBox1.Controls.Add(textBoxSourceUserName);
      groupBox1.Controls.Add(label2);
      groupBox1.Controls.Add(textBoxSourceName);
      groupBox1.Controls.Add(label1);
      groupBox1.Dock = DockStyle.Top;
      groupBox1.Location = new Point(0, 0);
      groupBox1.Name = "groupBox1";
      groupBox1.Size = new Size(373, 118);
      groupBox1.TabIndex = 7;
      groupBox1.TabStop = false;
      groupBox1.Text = " Workspace ";
      textBoxSourceComputer.Location = new Point(123, 78);
      textBoxSourceComputer.Name = "textBoxSourceComputer";
      textBoxSourceComputer.ReadOnly = true;
      textBoxSourceComputer.Size = new Size(239, 20);
      textBoxSourceComputer.TabIndex = 1000;
      textBoxSourceComputer.TabStop = false;
      label6.AutoSize = true;
      label6.Location = new Point(12, 81);
      label6.Name = "label6";
      label6.Size = new Size(55, 13);
      label6.TabIndex = 4;
      label6.Text = "Computer:";
      textBoxSourceUserName.Location = new Point(123, 52);
      textBoxSourceUserName.Name = "textBoxSourceUserName";
      textBoxSourceUserName.ReadOnly = true;
      textBoxSourceUserName.Size = new Size(239, 20);
      textBoxSourceUserName.TabIndex = 1000;
      textBoxSourceUserName.TabStop = false;
      label2.AutoSize = true;
      label2.Location = new Point(12, 55);
      label2.Name = "label2";
      label2.Size = new Size(32, 13);
      label2.TabIndex = 2;
      label2.Text = "User:";
      textBoxSourceName.Location = new Point(123, 26);
      textBoxSourceName.Name = "textBoxSourceName";
      textBoxSourceName.ReadOnly = true;
      textBoxSourceName.Size = new Size(239, 20);
      textBoxSourceName.TabIndex = 1000;
      textBoxSourceName.TabStop = false;
      label1.AutoSize = true;
      label1.Location = new Point(12, 28);
      label1.Name = "label1";
      label1.Size = new Size(94, 13);
      label1.TabIndex = 0;
      label1.Text = "Workspace name:";
      textBoxLocalFolder.Location = new Point(123, 156);
      textBoxLocalFolder.Name = "textBoxLocalFolder";
      textBoxLocalFolder.Size = new Size(207, 20);
      textBoxLocalFolder.TabIndex = 3;
      textBoxSourceFolder.Location = new Point(123, 130);
      textBoxSourceFolder.Name = "textBoxSourceFolder";
      textBoxSourceFolder.Size = new Size(207, 20);
      textBoxSourceFolder.TabIndex = 1;
      label3.AutoSize = true;
      label3.Location = new Point(12, 137);
      label3.Name = "label3";
      label3.Size = new Size(105, 13);
      label3.TabIndex = 8;
      label3.Text = "Source control folder";
      label4.AutoSize = true;
      label4.Location = new Point(12, 163);
      label4.Name = "label4";
      label4.Size = new Size(62, 13);
      label4.TabIndex = 9;
      label4.Text = "Local folder";
      buttonSelectSourceFolder.Enabled = false;
      buttonSelectSourceFolder.Location = new Point(335, 128);
      buttonSelectSourceFolder.Name = "buttonSelectSourceFolder";
      buttonSelectSourceFolder.Size = new Size(26, 23);
      buttonSelectSourceFolder.TabIndex = 2;
      buttonSelectSourceFolder.Text = "...";
      buttonSelectSourceFolder.UseVisualStyleBackColor = true;
      buttonSelectLocalFolder.Location = new Point(335, 154);
      buttonSelectLocalFolder.Name = "buttonSelectLocalFolder";
      buttonSelectLocalFolder.Size = new Size(26, 23);
      buttonSelectLocalFolder.TabIndex = 4;
      buttonSelectLocalFolder.Text = "...";
      buttonSelectLocalFolder.UseVisualStyleBackColor = true;
      buttonSelectLocalFolder.Click += buttonSelectLocalFolder_Click;
      buttonOk.Location = new Point(108, 193);
      buttonOk.Name = "buttonOk";
      buttonOk.Size = new Size(75, 23);
      buttonOk.TabIndex = 5;
      buttonOk.Text = "OK";
      buttonOk.UseVisualStyleBackColor = true;
      buttonOk.Click += buttonOk_Click;
      buttonCancel.DialogResult = DialogResult.Cancel;
      buttonCancel.Location = new Point(189, 193);
      buttonCancel.Name = "buttonCancel";
      buttonCancel.Size = new Size(75, 23);
      buttonCancel.TabIndex = 6;
      buttonCancel.Text = "Cancel";
      buttonCancel.UseVisualStyleBackColor = true;
      buttonCancel.Click += buttonCancel_Click;
      folderBrowserDialog1.Description = "Select local folder";
      AcceptButton = buttonOk;
      AutoScaleDimensions = new SizeF(6f, 13f);
      AutoScaleMode = AutoScaleMode.Font;
      CancelButton = buttonCancel;
      ClientSize = new Size(373, 225);
      Controls.Add(buttonCancel);
      Controls.Add(buttonOk);
      Controls.Add(buttonSelectLocalFolder);
      Controls.Add(buttonSelectSourceFolder);
      Controls.Add(label4);
      Controls.Add(label3);
      Controls.Add(textBoxSourceFolder);
      Controls.Add(textBoxLocalFolder);
      Controls.Add(groupBox1);
      FormBorderStyle = FormBorderStyle.FixedDialog;
      Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      Name = nameof (FormAddWorkspaceMapping);
      StartPosition = FormStartPosition.CenterScreen;
      Text = "Add new working folder";
      groupBox1.ResumeLayout(false);
      groupBox1.PerformLayout();
      ResumeLayout(false);
      PerformLayout();
    }
  }
}
