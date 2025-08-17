// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.Forms.FormDomain
// Assembly: Attrice.TeamFoundation.Controls.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=1a24c887ab53aa6e
// MVID: 393BE899-3C29-4A70-86D7-B4B965E5A8F5
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controls.12.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Attrice.TeamFoundation.Controls.Forms
{
  public class FormDomain : Form
  {
    private IContainer components;
    private Button button1;
    private Button button2;
    private CheckBox checkBox1;
    private TextBox textBox1;
    private Label label1;

    public FormDomain() => InitializeComponent();

    public bool Lookup => checkBox1.Checked;

    public string DomainName => textBox1.Text;

    public void Initialize(bool lookup, string domainName)
    {
      textBox1.Text = domainName;
      checkBox1.Checked = lookup;
      textBox1.Enabled = lookup;
    }

    private void button2_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      Close();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (checkBox1.Checked && textBox1.TextLength == 0)
      {
        var num = (int) MessageBox.Show("Please specify domain", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        textBox1.Focus();
      }
      else
      {
        DialogResult = DialogResult.OK;
        Close();
      }
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
      textBox1.Enabled = checkBox1.Checked;
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
      checkBox1 = new CheckBox();
      textBox1 = new TextBox();
      label1 = new Label();
      SuspendLayout();
      button1.Location = new Point(68, 82);
      button1.Name = "button1";
      button1.Size = new Size(75, 23);
      button1.TabIndex = 0;
      button1.Text = "OK";
      button1.UseVisualStyleBackColor = true;
      button1.Click += button1_Click;
      button2.DialogResult = DialogResult.Cancel;
      button2.Location = new Point(149, 82);
      button2.Name = "button2";
      button2.Size = new Size(75, 23);
      button2.TabIndex = 1;
      button2.Text = "Cancel";
      button2.UseVisualStyleBackColor = true;
      button2.Click += button2_Click;
      checkBox1.AutoSize = true;
      checkBox1.Location = new Point(22, 12);
      checkBox1.Name = "checkBox1";
      checkBox1.Size = new Size(162, 17);
      checkBox1.TabIndex = 2;
      checkBox1.Text = "Lookup computers in domain";
      checkBox1.UseVisualStyleBackColor = true;
      checkBox1.CheckedChanged += checkBox1_CheckedChanged;
      textBox1.Location = new Point(68, 44);
      textBox1.Name = "textBox1";
      textBox1.Size = new Size(193, 20);
      textBox1.TabIndex = 3;
      label1.AutoSize = true;
      label1.Location = new Point(19, 47);
      label1.Name = "label1";
      label1.Size = new Size(43, 13);
      label1.TabIndex = 4;
      label1.Text = "Domain";
      AcceptButton = button1;
      AutoScaleDimensions = new SizeF(6f, 13f);
      AutoScaleMode = AutoScaleMode.Font;
      CancelButton = button2;
      ClientSize = new Size(292, 123);
      Controls.Add(label1);
      Controls.Add(textBox1);
      Controls.Add(checkBox1);
      Controls.Add(button2);
      Controls.Add(button1);
      FormBorderStyle = FormBorderStyle.FixedToolWindow;
      Name = nameof (FormDomain);
      StartPosition = FormStartPosition.CenterScreen;
      Text = "Computer Lookup Setup";
      ResumeLayout(false);
      PerformLayout();
    }
  }
}
