// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Sidekicks.FormUsers
// Assembly: Attrice.TeamFoundation.Sidekicks.12, Version=6.0.0.0, Culture=neutral, PublicKeyToken=d1ea2fdd7e98265b
// MVID: 9FC50433-A771-4AD0-B465-1FCE84AFBC29
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Sidekicks.12.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Attrice.TeamFoundation.Configuration;

namespace Attrice.TeamFoundation.Sidekicks
{
  public class FormUsers : Form
  {
    private IContainer components;
    private Button button1;
    private Button button2;
    private ComboBox comboBox1;
    private Label label1;

    public FormUsers()
    {
      InitializeComponent();
      comboBox1.Items.Add("Retrieve users list in the background");
      comboBox1.Items.Add("Retrieve users list upon login");
      comboBox1.Items.Add("Do not retrieve users list");
    }

    public RetrieveUsersOption Option => (RetrieveUsersOption) comboBox1.SelectedIndex;

    public void Initialize(RetrieveUsersOption option)
    {
      comboBox1.SelectedIndex = (int) option;
    }

    private void button2_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      Close();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
      Close();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && components != null)
        components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      button1 = new Button();
      button2 = new Button();
      comboBox1 = new ComboBox();
      label1 = new Label();
      SuspendLayout();
      button1.Location = new Point(209, 54);
      button1.Name = "button1";
      button1.Size = new Size(75, 23);
      button1.TabIndex = 0;
      button1.Text = "OK";
      button1.UseVisualStyleBackColor = true;
      button1.Click += button1_Click;
      button2.DialogResult = DialogResult.Cancel;
      button2.Location = new Point(290, 54);
      button2.Name = "button2";
      button2.Size = new Size(75, 23);
      button2.TabIndex = 1;
      button2.Text = "Cancel";
      button2.UseVisualStyleBackColor = true;
      button2.Click += button2_Click;
      comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
      comboBox1.FormattingEnabled = true;
      comboBox1.Location = new Point(132, 14);
      comboBox1.Name = "comboBox1";
      comboBox1.Size = new Size(234, 21);
      comboBox1.TabIndex = 3;
      label1.AutoSize = true;
      label1.Location = new Point(12, 17);
      label1.Name = "label1";
      label1.Size = new Size(114, 13);
      label1.TabIndex = 4;
      label1.Text = "Retrieve Users Option:";
      AcceptButton = button1;
      AutoScaleDimensions = new SizeF(6f, 13f);
      AutoScaleMode = AutoScaleMode.Font;
      CancelButton = button2;
      ClientSize = new Size(378, 89);
      Controls.Add(label1);
      Controls.Add(comboBox1);
      Controls.Add(button2);
      Controls.Add(button1);
      FormBorderStyle = FormBorderStyle.FixedToolWindow;
      Name = nameof (FormUsers);
      StartPosition = FormStartPosition.CenterScreen;
      Text = "Team Foundation Sidekicks Options";
      ResumeLayout(false);
      PerformLayout();
    }
  }
}
