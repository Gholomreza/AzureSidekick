// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Sidekicks.FormSplash
// Assembly: Attrice.TeamFoundation.Sidekicks.12, Version=6.0.0.0, Culture=neutral, PublicKeyToken=d1ea2fdd7e98265b
// MVID: 9FC50433-A771-4AD0-B465-1FCE84AFBC29
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Sidekicks.12.exe

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Attrice.TeamFoundation.Sidekicks
{
  public class FormSplash : Form
  {
    private IContainer components;
    private Button buttonOk;
    private Label labelVersion;
    private Label labelDisclaimer;

    public FormSplash(bool aboutMode)
    {
      InitializeComponent();
      buttonOk.Visible = aboutMode;
      labelVersion.Text = "Version " + ApplicationMain.GetVersion();
      labelDisclaimer.Visible = aboutMode;
    }

    private void buttonOk_Click(object sender, EventArgs e) => Close();

    private void labelVersion_Click(object sender, EventArgs e)
    {
      Process.Start("http://www.attrice.info/cm/tfs/index.htm");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && components != null)
        components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      var componentResourceManager = new ComponentResourceManager(typeof (FormSplash));
      buttonOk = new Button();
      labelVersion = new Label();
      labelDisclaimer = new Label();
      SuspendLayout();
      buttonOk.BackColor = Color.FromArgb(235, 205, 205);
      buttonOk.FlatStyle = FlatStyle.Flat;
      buttonOk.Location = new Point(313, 232);
      buttonOk.Name = "buttonOk";
      buttonOk.Size = new Size(75, 23);
      buttonOk.TabIndex = 0;
      buttonOk.Text = "OK";
      buttonOk.UseVisualStyleBackColor = false;
      buttonOk.Click += buttonOk_Click;
      labelVersion.AutoSize = true;
      labelVersion.BackColor = Color.White;
      labelVersion.Cursor = Cursors.Hand;
      labelVersion.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
      labelVersion.Location = new Point(12, 237);
      labelVersion.Name = "labelVersion";
      labelVersion.Size = new Size(49, 13);
      labelVersion.TabIndex = 1;
      labelVersion.Text = "Version";
      labelVersion.Click += labelVersion_Click;
      labelDisclaimer.BackColor = Color.White;
      labelDisclaimer.Location = new Point(12, 124);
      labelDisclaimer.Name = "labelDisclaimer";
      labelDisclaimer.Size = new Size(376, 90);
      labelDisclaimer.TabIndex = 7;
      labelDisclaimer.Text = componentResourceManager.GetString("labelDisclaimer.Text");
      AutoScaleDimensions = new SizeF(6f, 13f);
      AutoScaleMode = AutoScaleMode.Font;
      BackgroundImage = (Image) componentResourceManager.GetObject("$this.BackgroundImage");
      BackgroundImageLayout = ImageLayout.Center;
      ClientSize = new Size(400, 267);
      Controls.Add(labelDisclaimer);
      Controls.Add(labelVersion);
      Controls.Add(buttonOk);
      FormBorderStyle = FormBorderStyle.None;
      Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      Name = nameof (FormSplash);
      StartPosition = FormStartPosition.CenterScreen;
      Text = "Team Foundation Sidekicks";
      ResumeLayout(false);
      PerformLayout();
    }
  }
}
