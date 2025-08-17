// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Sidekicks.FormLogin
// Assembly: Attrice.TeamFoundation.Sidekicks.12, Version=6.0.0.0, Culture=neutral, PublicKeyToken=d1ea2fdd7e98265b
// MVID: 9FC50433-A771-4AD0-B465-1FCE84AFBC29
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Sidekicks.12.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using Attrice.TeamFoundation.Controllers;

namespace Attrice.TeamFoundation.Sidekicks
{
  public class FormLogin : Form
  {
    private ServerWrapper _server;
    private IContainer components;
    private Button button1;
    private Label label3;
    private ComboBox comboBoxServers;
    private TextBox textBox1;
    private TextBox textBox2;
    private CheckBox checkBox1;
    private Label label1;
    private Label label2;
    private Button button2;
    private Label label4;
    private TableLayoutPanel tableLayoutPanel1;
    private PictureBox pictureBox1;
    private Label labelStatus;

    public ServerWrapper Server => _server;

    public bool CustomCredentials => !checkBox1.Checked;

    public string UserName => textBox1.Text;

    public FormLogin()
    {
      InitializeComponent();
      comboBoxServers.Items.Clear();
      comboBoxServers.Items.AddRange(ServerWrapper.GetRegisteredServers());
      comboBoxServers.Items.Insert(0, string.Empty);
      _server = null;
    }

    public void Initialize(string defaultServerName, bool customCredentials, string userName)
    {
      var num = -1;
      if (defaultServerName != null)
        num = comboBoxServers.FindString(defaultServerName);
      comboBoxServers.SelectedIndex = num;
      checkBox1.Checked = !customCredentials;
      if (checkBox1.Checked)
        return;
      textBox1.Text = userName;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      _server = null;
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        if (comboBoxServers.Text.Length == 0)
        {
          var num = (int) MessageBox.Show("Please specify server name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          comboBoxServers.Focus();
        }
        else if (!checkBox1.Checked && textBox1.TextLength == 0)
        {
          var num = (int) MessageBox.Show("Please specify user name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          textBox1.Focus();
        }
        else if (!checkBox1.Checked && textBox1.Text.Contains("\\"))
        {
          var num = (int) MessageBox.Show("Please specify user name without domain", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          textBox1.Focus();
        }
        else
        {
          _server = !checkBox1.Checked ? new ServerWrapper(comboBoxServers.Text, textBox1.Text, textBox2.Text) : new ServerWrapper(comboBoxServers.Text);
          DialogResult = DialogResult.OK;
          Close();
        }
      }
      catch (Exception ex)
      {
        SystemSounds.Beep.Play();
        labelStatus.Text = "Failed to connect to a Team Foundation Server.\r\n" + ex.Message;
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
      textBox2.Enabled = textBox1.Enabled = !checkBox1.Checked;
    }

    private void button2_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
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
      var componentResourceManager = new ComponentResourceManager(typeof (FormLogin));
      button1 = new Button();
      label3 = new Label();
      comboBoxServers = new ComboBox();
      textBox1 = new TextBox();
      textBox2 = new TextBox();
      checkBox1 = new CheckBox();
      label1 = new Label();
      label2 = new Label();
      button2 = new Button();
      label4 = new Label();
      tableLayoutPanel1 = new TableLayoutPanel();
      labelStatus = new Label();
      pictureBox1 = new PictureBox();
      tableLayoutPanel1.SuspendLayout();
      ((ISupportInitialize) pictureBox1).BeginInit();
      SuspendLayout();
      button1.Location = new Point(159, 244);
      button1.Name = "button1";
      button1.Size = new Size(75, 23);
      button1.TabIndex = 18;
      button1.Text = "Connect";
      button1.UseVisualStyleBackColor = true;
      button1.Click += button1_Click;
      label3.AutoSize = true;
      label3.Location = new Point(22, 135);
      label3.Name = "label3";
      label3.Size = new Size(67, 13);
      label3.TabIndex = 17;
      label3.Text = "Server name";
      comboBoxServers.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      comboBoxServers.AutoCompleteMode = AutoCompleteMode.Suggest;
      comboBoxServers.AutoCompleteSource = AutoCompleteSource.ListItems;
      comboBoxServers.FormattingEnabled = true;
      comboBoxServers.IntegralHeight = false;
      comboBoxServers.ItemHeight = 13;
      comboBoxServers.Location = new Point(132, 132);
      comboBoxServers.MaxDropDownItems = 32;
      comboBoxServers.MaxLength = 512;
      comboBoxServers.Name = "comboBoxServers";
      comboBoxServers.Size = new Size(291, 21);
      comboBoxServers.TabIndex = 16;
      textBox1.Enabled = false;
      textBox1.Location = new Point(132, 182);
      textBox1.MaxLength = 64;
      textBox1.Name = "textBox1";
      textBox1.Size = new Size(291, 20);
      textBox1.TabIndex = 19;
      textBox2.Enabled = false;
      textBox2.Location = new Point(132, 208);
      textBox2.MaxLength = 64;
      textBox2.Name = "textBox2";
      textBox2.PasswordChar = '*';
      textBox2.Size = new Size(291, 20);
      textBox2.TabIndex = 20;
      textBox2.UseSystemPasswordChar = true;
      checkBox1.AutoSize = true;
      checkBox1.Checked = true;
      checkBox1.CheckState = CheckState.Checked;
      checkBox1.Location = new Point(132, 159);
      checkBox1.Name = "checkBox1";
      checkBox1.Size = new Size(135, 17);
      checkBox1.TabIndex = 21;
      checkBox1.Text = "Use current credentials";
      checkBox1.UseVisualStyleBackColor = true;
      checkBox1.CheckedChanged += checkBox1_CheckedChanged;
      label1.AutoSize = true;
      label1.Location = new Point(22, 182);
      label1.Name = "label1";
      label1.Size = new Size(58, 13);
      label1.TabIndex = 22;
      label1.Text = "User name";
      label2.AutoSize = true;
      label2.Location = new Point(22, 211);
      label2.Name = "label2";
      label2.Size = new Size(53, 13);
      label2.TabIndex = 23;
      label2.Text = "Password";
      button2.DialogResult = DialogResult.Cancel;
      button2.Location = new Point(240, 244);
      button2.Name = "button2";
      button2.Size = new Size(75, 23);
      button2.TabIndex = 24;
      button2.Text = "Cancel";
      button2.UseVisualStyleBackColor = true;
      button2.Click += button2_Click;
      label4.Dock = DockStyle.Top;
      label4.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
      label4.Location = new Point(0, 0);
      label4.Name = "label4";
      label4.Size = new Size(473, 48);
      label4.TabIndex = 26;
      label4.Text = "Connect to a Team Foundation Server";
      label4.TextAlign = ContentAlignment.MiddleCenter;
      tableLayoutPanel1.BackColor = SystemColors.Info;
      tableLayoutPanel1.ColumnCount = 2;
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50f));
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel1.Controls.Add(pictureBox1, 0, 0);
      tableLayoutPanel1.Controls.Add(labelStatus, 1, 0);
      tableLayoutPanel1.Dock = DockStyle.Top;
      tableLayoutPanel1.Location = new Point(0, 48);
      tableLayoutPanel1.Margin = new Padding(0);
      tableLayoutPanel1.Name = "tableLayoutPanel1";
      tableLayoutPanel1.RowCount = 1;
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      tableLayoutPanel1.Size = new Size(473, 65);
      tableLayoutPanel1.TabIndex = 29;
      labelStatus.BackColor = SystemColors.Info;
      labelStatus.Dock = DockStyle.Fill;
      labelStatus.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
      labelStatus.Location = new Point(50, 0);
      labelStatus.Margin = new Padding(0, 0, 1, 0);
      labelStatus.Name = "labelStatus";
      labelStatus.Padding = new Padding(5);
      labelStatus.Size = new Size(422, 65);
      labelStatus.TabIndex = 31;
      labelStatus.Text = "Please provide credentials to connect to Team Foundation Server";
      labelStatus.TextAlign = ContentAlignment.MiddleLeft;
      pictureBox1.BackColor = SystemColors.Info;
      pictureBox1.BackgroundImage = (Image) componentResourceManager.GetObject("pictureBox1.BackgroundImage");
      pictureBox1.BackgroundImageLayout = ImageLayout.Center;
      pictureBox1.Dock = DockStyle.Fill;
      pictureBox1.InitialImage = (Image) componentResourceManager.GetObject("pictureBox1.InitialImage");
      pictureBox1.Location = new Point(1, 0);
      pictureBox1.Margin = new Padding(1, 0, 1, 0);
      pictureBox1.Name = "pictureBox1";
      pictureBox1.Size = new Size(48, 65);
      pictureBox1.TabIndex = 29;
      pictureBox1.TabStop = false;
      AcceptButton = button1;
      AutoScaleDimensions = new SizeF(6f, 13f);
      AutoScaleMode = AutoScaleMode.Font;
      CancelButton = button2;
      ClientSize = new Size(473, 279);
      Controls.Add(tableLayoutPanel1);
      Controls.Add(label4);
      Controls.Add(button2);
      Controls.Add(label2);
      Controls.Add(label1);
      Controls.Add(checkBox1);
      Controls.Add(textBox2);
      Controls.Add(textBox1);
      Controls.Add(button1);
      Controls.Add(label3);
      Controls.Add(comboBoxServers);
      FormBorderStyle = FormBorderStyle.FixedDialog;
      Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      Name = nameof (FormLogin);
      StartPosition = FormStartPosition.CenterScreen;
      Text = "Connect to a Team Foundation Server";
      tableLayoutPanel1.ResumeLayout(false);
      ((ISupportInitialize) pictureBox1).EndInit();
      ResumeLayout(false);
      PerformLayout();
    }
  }
}
