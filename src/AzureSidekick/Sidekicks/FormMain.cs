// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Sidekicks.FormMain
// Assembly: Attrice.TeamFoundation.Sidekicks.12, Version=6.0.0.0, Culture=neutral, PublicKeyToken=d1ea2fdd7e98265b
// MVID: 9FC50433-A771-4AD0-B465-1FCE84AFBC29
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Sidekicks.12.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Attrice.TeamFoundation.Configuration;
using Attrice.TeamFoundation.Controllers;
using Attrice.TeamFoundation.Controls;

namespace Attrice.TeamFoundation.Sidekicks
{
  public class FormMain : Form
  {
    private TfsController _controller;
    private List<BaseSidekickControl> _controls;
    private List<ToolStripMenuItem> _menuItems;
    private List<ToolStripButton> _buttons;
    private IContainer components;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem helpToolStripMenuItem;
    private StatusStrip statusStrip1;
    private ToolStripMenuItem toolStripMenuItem2;
    private ToolStripButton toolStripButton1;
    private ToolStripButton toolStripButton2;
    private ToolStripStatusLabel toolStripStatusLabel;
    private ToolStripMenuItem exitToolStripMenuItem;
    private ToolStripMenuItem aboutToolStripMenuItem;
    private ToolStripMenuItem toolStripMenuItemView;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem toolStripMenuItem3;
    private ToolStripMenuItem toolsToolbarToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripMenuItem toolStripMenuItemStatusBar;
    private BackgroundWorker backgroundWorker1;
    private ToolStripMenuItem optionsToolStripMenuItem;
    private ToolStripStatusLabel toolStripStatusUsers;
    private ImageList imageList1;
    private Timer timer1;
    private ToolStrip toolStrip1;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripButton toolStripButtonConnect;
    private Panel panel1;
    private ToolStripMenuItem addSidekickToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator4;

    public FormMain()
    {
      InitializeComponent();
      toolStripStatusLabel.Text = "Not connected";
      _controls = new List<BaseSidekickControl>();
      _menuItems = new List<ToolStripMenuItem>();
      _buttons = new List<ToolStripButton>();
      SetMenuStatus(false);
      statusStrip1.Visible = ApplicationMain.Configuration.ViewStatusBar;
      toolStripMenuItemStatusBar.Checked = ApplicationMain.Configuration.ViewStatusBar;
      toolStrip1.Visible = ApplicationMain.Configuration.ViewMainToolbar;
      toolsToolbarToolStripMenuItem.Checked = ApplicationMain.Configuration.ViewMainToolbar;
      statusStrip1.ImageList = imageList1;
      toolStripStatusUsers.ImageIndex = 1;
      LoadSidekicks();
    }

    private BaseSidekickControl LoadSidekick(Type type, StringDictionary settings)
    {
      var control = Activator.CreateInstance(type) as BaseSidekickControl;
      if (settings != null)
        control.Settings = settings;
      _controls.Add(control);
      var toolStripMenuItem = new ToolStripMenuItem(control.Name);
      var toolStripButton = new ToolStripButton(control.Name);
      if (control.Image != null)
      {
        toolStripMenuItem.Image = control.Image;
        toolStripButton.Image = control.Image;
      }
      toolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      EventHandler eventHandler = (o, e) => ShowControl(control);
      toolStripMenuItem.Click += eventHandler;
      toolStripButton.Click += eventHandler;
      toolStripMenuItem2.DropDownItems.Insert(_controls.Count - 1, toolStripMenuItem);
      toolStrip1.Items.Insert(_controls.Count - 1, toolStripButton);
      _menuItems.Add(toolStripMenuItem);
      _buttons.Add(toolStripButton);
      return control;
    }

    private void LoadSidekicks()
    {
      var sidekicks = ApplicationMain.Configuration.Sidekicks;
      if (sidekicks == null)
        return;
      foreach (SidekickConfiguration sidekickConfiguration in sidekicks)
      {
        try
        {
          var type = GetType().Assembly .GetType(sidekickConfiguration.Type);
          StringDictionary settings = null;
          if (sidekickConfiguration.Settings != null)
          {
            settings = new StringDictionary();
            foreach (KeyValueConfigurationElement setting in sidekickConfiguration.Settings)
              settings.Add(setting.Key, setting.Value);
          }

          if (type != null) LoadSidekick(type, settings);
          Trace.TraceInformation("Loaded sidekick {0}", sidekickConfiguration.Type);
        }
        catch (Exception ex)
        {
          Trace.TraceError("Error while loading sidekick {0}: {1}\r\n{2}", sidekickConfiguration.Type, ex.Message, ex.StackTrace);
        }
      }
      SetMenuStatus(false);
    }

    private void SetMenuStatus(bool enable)
    {
      foreach (ToolStripItem menuItem in _menuItems)
        menuItem.Enabled = enable;
      foreach (ToolStripItem button in _buttons)
        button.Enabled = enable;
    }

    private void SetControlsForUpdate()
    {
      foreach (Control control in _controls)
        control.Tag = null;
    }

    private void ShowControl(BaseSidekickControl control)
    {
      SuspendLayout();
      panel1.SuspendLayout();
      control.SuspendLayout();
      panel1.Controls.Clear();
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        if (control.Tag == null)
        {
          control.Initialize(_controller);
          control.Tag = true;
        }
        panel1.Controls.Add(control);
        control.Dock = DockStyle.Fill;
        Text = $"Team Foundation Sidekicks - {(object)control.Name}";
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to initialize control." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        Text = "Team Foundation Sidekicks";
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
      control.ResumeLayout();
      panel1.ResumeLayout();
      ResumeLayout();
    }

    public void Initialize()
    {
      if (ApplicationMain.Configuration.TfsServer == "")
        return;
      if (ApplicationMain.Configuration.CustomCredentials)
        return;
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        _controller = null;
        _controller = new TfsController(ApplicationMain.Configuration.TfsServer);//, "3pfcn5ox6udvozqet5cqwqeingbbj34gbxppry72ttou4fisesba");
        GetUsers();
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to establish connection." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
    }

    private void FormMain_Shown(object sender, EventArgs e)
    {
      if (_controller == null && !DoLogin())
        return;
      InitializeLoginControls();
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Close();

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      var num = (int) new FormSplash(true).ShowDialog();
    }

    private void InitializeLoginControls()
    {
      panel1.Controls.Clear();
      SetControlsForUpdate();
      if (_controller == null)
      {
        toolStripStatusLabel.Text = "Not connected";
        SetMenuStatus(false);
      }
      else
      {
        toolStripStatusLabel.Text =
            $@"Connected as {(object)_controller.UserDisplayName} to {(object)_controller.Server.ServerName}";
        SetMenuStatus(true);
      }
    }

    private void GetUsers()
    {
      if (ApplicationMain.Configuration.RetrieveUsers == RetrieveUsersOption.Login)
      {
        try
        {
            var controllerUsers = _controller.Users;
        }
        catch (Exception ex)
        {
          var num = (int) MessageBox.Show("Failed to retrieve users." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
      else
      {
        if (ApplicationMain.Configuration.RetrieveUsers != RetrieveUsersOption.Background)
          return;
        SetUsersRetrievalStatus(true);
        backgroundWorker1.RunWorkerAsync();
      }
    }

    private bool DoLogin()
    {
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        var formLogin = new FormLogin();
        formLogin.Initialize(ApplicationMain.Configuration.TfsServer, ApplicationMain.Configuration.CustomCredentials, ApplicationMain.Configuration.TfsUserName);
        if (formLogin.ShowDialog() != DialogResult.OK)
          return false;
        _controller = null;
        _controller = new TfsController(formLogin.Server);
        ApplicationMain.Configuration.TfsServer = formLogin.Server.ServerUri;
        ApplicationMain.Configuration.CustomCredentials = formLogin.CustomCredentials;
        ApplicationMain.Configuration.TfsUserName = !ApplicationMain.Configuration.CustomCredentials ? "" : formLogin.UserName;
        ApplicationMain.Configuration.Save();
        GetUsers();
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to establish connection." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
      return true;
    }

    private void toolStripMenuItemConnection_Click(object sender, EventArgs e)
    {
      if (!DoLogin())
        return;
      InitializeLoginControls();
    }

    private void toolsToolbarToolStripMenuItem_Click(object sender, EventArgs e)
    {
      toolStrip1.Visible = toolsToolbarToolStripMenuItem.Checked;
      ApplicationMain.Configuration.ViewMainToolbar = toolStrip1.Visible;
    }

    private void toolStripMenuItemStatusBar_Click(object sender, EventArgs e)
    {
      statusStrip1.Visible = toolStripMenuItemStatusBar.Checked;
      ApplicationMain.Configuration.ViewStatusBar = statusStrip1.Visible;
    }

    private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
    {
      try
      {
          var controllerUsers = _controller.Users;
      }
      catch (Exception ex)
      {
        SetUsersRetrievalStatus(false);
        var num = (int) MessageBox.Show("Failed to retrieve users." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      SetUsersRetrievalStatus(false);
      if (backgroundWorker1.IsBusy)
        backgroundWorker1.CancelAsync();
      foreach (var control in _controls)
      {
        if (control.Settings != null)
        {
          var settings = new KeyValueConfigurationCollection();
          foreach (DictionaryEntry setting in control.Settings)
            settings.Add(setting.Key.ToString(), setting.Value.ToString());
          ApplicationMain.Configuration.UpdateSidekickSettings(control.Name, settings);
        }
      }
    }

    private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      SetUsersRetrievalStatus(false);
      SetControlsForUpdate();
      if (panel1.Controls.Count != 1)
        return;
      (panel1.Controls[0] as BaseSidekickControl).LoadUsers(_controller);
    }

    private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      var formUsers = new FormUsers();
      formUsers.Initialize(ApplicationMain.Configuration.RetrieveUsers);
      if (formUsers.ShowDialog(this) != DialogResult.OK)
        return;
      ApplicationMain.Configuration.RetrieveUsers = formUsers.Option;
      if (_controller == null)
        return;
      var num = (int) MessageBox.Show("The changes to configuration will be effective upon application restart.", "Configuration update", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void SetUsersRetrievalStatus(bool start)
    {
      if (start)
      {
        toolStripStatusUsers.Text = "Retrieving users ";
        toolStripStatusUsers.ToolTipText = "Retrieving list of users ...";
        timer1.Enabled = true;
        timer1.Start();
      }
      else
      {
        toolStripStatusUsers.ImageIndex = 1;
        toolStripStatusUsers.Text = "";
        toolStripStatusUsers.ToolTipText = "";
        timer1.Stop();
        timer1.Enabled = false;
      }
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      if (toolStripStatusUsers.ImageIndex < 1)
        toolStripStatusUsers.ImageIndex = 1;
      else
        toolStripStatusUsers.ImageIndex = 0;
      toolStripStatusUsers.Invalidate();
    }

    private void addSidekickToolStripMenuItem_Click(object sender, EventArgs e)
    {
      var openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "Assemblies (*.dll)|*.dll|All Files (*.*)|*.*";
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      AddSidekick(openFileDialog.FileName);
    }

    private void AddSidekick(string fileName)
    {
      try
      {
        var assembly = Assembly.LoadFrom(fileName);
        if (assembly == null)
          return;
        var sidekicksCollection = ApplicationMain.Configuration.Sidekicks;
        foreach (var exportedType in assembly.GetExportedTypes())
        {
          if (typeof (BaseSidekickControl).IsAssignableFrom(exportedType) && !(exportedType == typeof (BaseSidekickControl)))
          {
            var baseSidekickControl = LoadSidekick(exportedType, null);
            if (sidekicksCollection == null)
              sidekicksCollection = new SidekicksCollection();
            sidekicksCollection.Add(new SidekickConfiguration(baseSidekickControl.Name, exportedType.FullName, fileName));
          }
        }
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show(this, "Failed to add sidekick assembly " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        Trace.TraceError("Failed to add sidekick assembly {0} {1}\r\n {2}", fileName, ex.Message, ex.StackTrace);
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
      components = new Container();
      var componentResourceManager = new ComponentResourceManager(typeof (FormMain));
      menuStrip1 = new MenuStrip();
      fileToolStripMenuItem = new ToolStripMenuItem();
      exitToolStripMenuItem = new ToolStripMenuItem();
      toolStripMenuItemView = new ToolStripMenuItem();
      toolsToolbarToolStripMenuItem = new ToolStripMenuItem();
      toolStripSeparator2 = new ToolStripSeparator();
      toolStripMenuItemStatusBar = new ToolStripMenuItem();
      toolStripMenuItem2 = new ToolStripMenuItem();
      toolStripSeparator1 = new ToolStripSeparator();
      toolStripMenuItem3 = new ToolStripMenuItem();
      optionsToolStripMenuItem = new ToolStripMenuItem();
      helpToolStripMenuItem = new ToolStripMenuItem();
      aboutToolStripMenuItem = new ToolStripMenuItem();
      statusStrip1 = new StatusStrip();
      toolStripStatusLabel = new ToolStripStatusLabel();
      toolStripStatusUsers = new ToolStripStatusLabel();
      backgroundWorker1 = new BackgroundWorker();
      timer1 = new Timer(components);
      imageList1 = new ImageList(components);
      toolStripButton1 = new ToolStripButton();
      toolStripButton2 = new ToolStripButton();
      toolStrip1 = new ToolStrip();
      toolStripSeparator3 = new ToolStripSeparator();
      toolStripButtonConnect = new ToolStripButton();
      panel1 = new Panel();
      addSidekickToolStripMenuItem = new ToolStripMenuItem();
      toolStripSeparator4 = new ToolStripSeparator();
      menuStrip1.SuspendLayout();
      statusStrip1.SuspendLayout();
      toolStrip1.SuspendLayout();
      SuspendLayout();
      menuStrip1.Items.AddRange(new ToolStripItem[4]
      {
        fileToolStripMenuItem,
        toolStripMenuItemView,
        toolStripMenuItem2,
        helpToolStripMenuItem
      });
      menuStrip1.Location = new Point(0, 0);
      menuStrip1.Name = "menuStrip1";
      menuStrip1.Size = new Size(599, 24);
      menuStrip1.TabIndex = 0;
      menuStrip1.Text = "menuStrip1";
      fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[1]
      {
        exitToolStripMenuItem
      });
      fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      fileToolStripMenuItem.Size = new Size(35, 20);
      fileToolStripMenuItem.Text = "&File";
      exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      exitToolStripMenuItem.Size = new Size(103, 22);
      exitToolStripMenuItem.Text = "E&xit";
      exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
      toolStripMenuItemView.DropDownItems.AddRange(new ToolStripItem[3]
      {
        toolsToolbarToolStripMenuItem,
        toolStripSeparator2,
        toolStripMenuItemStatusBar
      });
      toolStripMenuItemView.Name = "toolStripMenuItemView";
      toolStripMenuItemView.Size = new Size(41, 20);
      toolStripMenuItemView.Text = "&View";
      toolsToolbarToolStripMenuItem.Checked = true;
      toolsToolbarToolStripMenuItem.CheckOnClick = true;
      toolsToolbarToolStripMenuItem.CheckState = CheckState.Checked;
      toolsToolbarToolStripMenuItem.Name = "toolsToolbarToolStripMenuItem";
      toolsToolbarToolStripMenuItem.Size = new Size(135, 22);
      toolsToolbarToolStripMenuItem.Text = "Tools Bar";
      toolsToolbarToolStripMenuItem.Click += toolsToolbarToolStripMenuItem_Click;
      toolStripSeparator2.Name = "toolStripSeparator2";
      toolStripSeparator2.Size = new Size(132, 6);
      toolStripMenuItemStatusBar.Checked = true;
      toolStripMenuItemStatusBar.CheckOnClick = true;
      toolStripMenuItemStatusBar.CheckState = CheckState.Checked;
      toolStripMenuItemStatusBar.Name = "toolStripMenuItemStatusBar";
      toolStripMenuItemStatusBar.Size = new Size(135, 22);
      toolStripMenuItemStatusBar.Text = "Status Bar";
      toolStripMenuItemStatusBar.Click += toolStripMenuItemStatusBar_Click;
      toolStripMenuItem2.DropDownItems.AddRange(new ToolStripItem[5]
      {
        toolStripSeparator1,
        addSidekickToolStripMenuItem,
        toolStripSeparator4,
        toolStripMenuItem3,
        optionsToolStripMenuItem
      });
      toolStripMenuItem2.Name = "toolStripMenuItem2";
      toolStripMenuItem2.Size = new Size(44, 20);
      toolStripMenuItem2.Text = "&Tools";
      toolStripSeparator1.Name = "toolStripSeparator1";
      toolStripSeparator1.Size = new Size(172, 6);
      toolStripMenuItem3.Image = (Image) componentResourceManager.GetObject("toolStripMenuItem3.Image");
      toolStripMenuItem3.Name = "toolStripMenuItem3";
      toolStripMenuItem3.Size = new Size(175, 22);
      toolStripMenuItem3.Text = "Co&nnect To Server";
      toolStripMenuItem3.Click += toolStripMenuItemConnection_Click;
      optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
      optionsToolStripMenuItem.Size = new Size(175, 22);
      optionsToolStripMenuItem.Text = "&Options....";
      optionsToolStripMenuItem.Click += optionsToolStripMenuItem_Click;
      helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[1]
      {
        aboutToolStripMenuItem
      });
      helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      helpToolStripMenuItem.Size = new Size(40, 20);
      helpToolStripMenuItem.Text = "&Help";
      aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      aboutToolStripMenuItem.Size = new Size(114, 22);
      aboutToolStripMenuItem.Text = "&About";
      aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
      statusStrip1.Items.AddRange(new ToolStripItem[2]
      {
        toolStripStatusLabel,
        toolStripStatusUsers
      });
      statusStrip1.Location = new Point(0, 325);
      statusStrip1.Name = "statusStrip1";
      statusStrip1.Size = new Size(599, 22);
      statusStrip1.TabIndex = 1;
      statusStrip1.Text = "statusStrip1";
      toolStripStatusLabel.Name = "toolStripStatusLabel";
      toolStripStatusLabel.Size = new Size(584, 17);
      toolStripStatusLabel.Spring = true;
      toolStripStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
      toolStripStatusUsers.ImageAlign = ContentAlignment.BottomRight;
      toolStripStatusUsers.ImageScaling = ToolStripItemImageScaling.None;
      toolStripStatusUsers.ImageTransparentColor = Color.Magenta;
      toolStripStatusUsers.Name = "toolStripStatusUsers";
      toolStripStatusUsers.Size = new Size(0, 17);
      toolStripStatusUsers.TextAlign = ContentAlignment.MiddleLeft;
      toolStripStatusUsers.TextImageRelation = TextImageRelation.TextBeforeImage;
      backgroundWorker1.WorkerReportsProgress = true;
      backgroundWorker1.WorkerSupportsCancellation = true;
      backgroundWorker1.DoWork += backgroundWorker1_DoWork;
      backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
      timer1.Interval = 450;
      timer1.Tick += timer1_Tick;
      imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
      imageList1.TransparentColor = Color.Transparent;
      imageList1.Images.SetKeyName(0, "search_users.gif");
      imageList1.Images.SetKeyName(1, "search_users11.gif");
      toolStripButton1.CheckOnClick = true;
      toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButton1.Image = (Image) componentResourceManager.GetObject("toolStripButton1.Image");
      toolStripButton1.ImageTransparentColor = Color.Magenta;
      toolStripButton1.Name = "toolStripButton1";
      toolStripButton1.Size = new Size(23, 22);
      toolStripButton1.Text = "toolStripButton1";
      toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButton2.Image = (Image) componentResourceManager.GetObject("toolStripButton2.Image");
      toolStripButton2.ImageTransparentColor = Color.Magenta;
      toolStripButton2.Name = "toolStripButton2";
      toolStripButton2.Size = new Size(23, 22);
      toolStripButton2.Text = "toolStripButton2";
      toolStrip1.Items.AddRange(new ToolStripItem[2]
      {
        toolStripSeparator3,
        toolStripButtonConnect
      });
      toolStrip1.Location = new Point(0, 24);
      toolStrip1.Name = "toolStrip1";
      toolStrip1.Size = new Size(599, 25);
      toolStrip1.TabIndex = 10;
      toolStrip1.Text = "toolStrip1";
      toolStripSeparator3.Name = "toolStripSeparator3";
      toolStripSeparator3.Size = new Size(6, 25);
      toolStripButtonConnect.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonConnect.Image = (Image) componentResourceManager.GetObject("toolStripButtonConnect.Image");
      toolStripButtonConnect.ImageTransparentColor = Color.Magenta;
      toolStripButtonConnect.Name = "toolStripButtonConnect";
      toolStripButtonConnect.Size = new Size(23, 22);
      toolStripButtonConnect.Text = "toolStripButton3";
      toolStripButtonConnect.ToolTipText = "Connect To TFS Server";
      toolStripButtonConnect.Click += toolStripMenuItemConnection_Click;
      panel1.BackColor = SystemColors.AppWorkspace;
      panel1.Dock = DockStyle.Fill;
      panel1.ForeColor = SystemColors.ControlText;
      panel1.Location = new Point(0, 49);
      panel1.Name = "panel1";
      panel1.Size = new Size(599, 276);
      panel1.TabIndex = 11;
      addSidekickToolStripMenuItem.Name = "addSidekickToolStripMenuItem";
      addSidekickToolStripMenuItem.Size = new Size(175, 22);
      addSidekickToolStripMenuItem.Text = "&Add Sidekick...";
      addSidekickToolStripMenuItem.Click += addSidekickToolStripMenuItem_Click;
      toolStripSeparator4.Name = "toolStripSeparator4";
      toolStripSeparator4.Size = new Size(172, 6);
      AutoScaleDimensions = new SizeF(6f, 13f);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(599, 347);
      Controls.Add(panel1);
      Controls.Add(toolStrip1);
      Controls.Add(statusStrip1);
      Controls.Add(menuStrip1);
      Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      MainMenuStrip = menuStrip1;
      Name = nameof (FormMain);
      StartPosition = FormStartPosition.CenterScreen;
      Text = "Team Foundation Sidekicks";
      WindowState = FormWindowState.Maximized;
      Shown += FormMain_Shown;
      FormClosing += FormMain_FormClosing;
      menuStrip1.ResumeLayout(false);
      menuStrip1.PerformLayout();
      statusStrip1.ResumeLayout(false);
      statusStrip1.PerformLayout();
      toolStrip1.ResumeLayout(false);
      toolStrip1.PerformLayout();
      ResumeLayout(false);
      PerformLayout();
    }
  }
}
