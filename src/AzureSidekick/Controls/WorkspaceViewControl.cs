// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.WorkspaceViewControl
// Assembly: Attrice.TeamFoundation.Controls.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=1a24c887ab53aa6e
// MVID: 393BE899-3C29-4A70-86D7-B4B965E5A8F5
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controls.12.dll

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Attrice.TeamFoundation.Common;
using Attrice.TeamFoundation.Controllers;
using Attrice.TeamFoundation.Controls.Forms;
using Attrice.TeamFoundation.Controls.Properties;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace Attrice.TeamFoundation.Controls
{
  public class WorkspaceViewControl : BaseSidekickControl
  {
    private IContainer components;
    private SplitContainer splitContainer1;
    private TableLayoutPanel tableLayoutPanel1;
    private ListView listViewDetailed;
    private ColumnHeader columnName;
    private ColumnHeader columnOwner;
    private ColumnHeader columnComputer;
    private ToolStrip toolStripView;
    private ToolStripButton toolStripButtonDetailed;
    private ToolStripButton toolStripButtonGroupedByOwner;
    private ToolStripButton toolStripButtonGroupedByComputer;
    private ToolStripButton toolStripButtonGroupedByName;
    private ListView listViewGrouped;
    private ColumnHeader columnGroupedData1;
    private ColumnHeader columnGroupedData2;
    private Panel panelSearch;
    private GroupBox groupBox1;
    private Label label4;
    private Label label3;
    private Label label2;
    private Label label1;
    private ComboBox comboBoxUserName;
    private ComboBox comboBoxComputer;
    private TeamFoundationDateTimePicker datePickerTo;
    private TeamFoundationDateTimePicker datePickerFrom;
    private Button buttonClear;
    private Button buttonSearch;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripButton toolStripButtonDeleteWorkspace;
    private ToolStripButton toolStripButtonUpdateWorkspaceUser;
    private ToolStripButton toolStripButtonWorkspaceDuplicate;
    private GroupBox groupBox3;
    private GroupBox groupBox2;
    private PropertyGrid propertyGridWorkspace;
    private ListView listViewMappings;
    private ToolStrip toolStrip1;
    private ToolStripButton toolStripButtonNewMapping;
    private ToolStripButton toolStripButtonDeleteMapping;
    private ToolStripButton toolStripButtonCloakMapping;
    private ColumnHeader columnStatus;
    private ColumnHeader columnServerFolder;
    private ColumnHeader columnLocalFolder;
    private ToolStripButton toolStripButtonActivateMapping;
    private Button buttonDomain;
    private ToolTip toolTip1;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripButton toolStripButtonSaveToFile;
    private ColumnHeader columnLastAccess;
    private WorkspaceViewController _controller;
    private DataTable _dtUsers;
    private DataTable _dtComputers;

    public WorkspaceViewControl()
    {
      InitializeComponent();
      listViewDetailed.ListViewItemSorter = new CustomListSorter();
      listViewDetailed.ListViewItemSorter = new CustomListSorter();
      splitContainer1.Panel1MinSize = 350;
      splitContainer1.Panel2MinSize = 350;
      panelSearch.BackColor = SystemColors.Control;
      splitContainer1.BackColor = SystemColors.Control;
      datePickerTo.CustomFormat = datePickerFrom.CustomFormat = "dd-MMM-yyyy";
      datePickerTo.Format = datePickerFrom.Format = DateTimePickerFormat.Custom;
      Name = "Workspace Sidekick";
    }

    public override Image Image => Resources.WorkspaceImage;

    private void toolStripButtonDetailed_Click(object sender, EventArgs e)
    {
      if (toolStripButtonDetailed.Checked)
        return;
      toolStripButtonDetailed.Checked = true;
      toolStripButtonGroupedByComputer.Checked = toolStripButtonGroupedByName.Checked = toolStripButtonGroupedByOwner.Checked = false;
      ViewDetailedList();
      SelectWorkspace();
    }

    private void toolStripButtonGroupedByOwner_Click(object sender, EventArgs e)
    {
      if (toolStripButtonGroupedByOwner.Checked)
        return;
      toolStripButtonGroupedByOwner.Checked = true;
      toolStripButtonDetailed.Checked = toolStripButtonGroupedByName.Checked = toolStripButtonGroupedByComputer.Checked = false;
      ViewGroupedList(WorkspaceGrouping.ByOwner);
      SelectWorkspace();
    }

    private void toolStripButtonGroupedByComputer_Click(object sender, EventArgs e)
    {
      if (toolStripButtonGroupedByComputer.Checked)
        return;
      toolStripButtonGroupedByComputer.Checked = true;
      toolStripButtonDetailed.Checked = toolStripButtonGroupedByName.Checked = toolStripButtonGroupedByOwner.Checked = false;
      ViewGroupedList(WorkspaceGrouping.ByComputer);
      SelectWorkspace();
    }

    private void toolStripButtonGroupedByName_Click(object sender, EventArgs e)
    {
      if (toolStripButtonGroupedByName.Checked)
        return;
      toolStripButtonGroupedByName.Checked = true;
      toolStripButtonDetailed.Checked = toolStripButtonGroupedByComputer.Checked = toolStripButtonGroupedByOwner.Checked = false;
      ViewGroupedList(WorkspaceGrouping.ByName);
      SelectWorkspace();
    }

    private void listViewWorkspace_ItemSelectionChanged(
      object sender,
      ListViewItemSelectionChangedEventArgs e)
    {
      SelectWorkspace();
    }

    private void listViewMappings_ItemSelectionChanged(
      object sender,
      ListViewItemSelectionChangedEventArgs e)
    {
      if ((sender as ListView).SelectedIndices.Count == 0)
        toolStripButtonDeleteMapping.Enabled = toolStripButtonActivateMapping.Enabled = toolStripButtonCloakMapping.Enabled = false;
      else
        toolStripButtonDeleteMapping.Enabled = toolStripButtonActivateMapping.Enabled = toolStripButtonCloakMapping.Enabled = true;
    }

    private void buttonSearch_Click(object sender, EventArgs e)
    {
      ClearWorkspaces();
      ClearWorkspaceDetails();
      SearchWorkspaces();
    }

    private void buttonClear_Click(object sender, EventArgs e)
    {
      ClearWorkspaces();
      ClearWorkspaceDetails();
      DefaultSearchParameters();
    }

    private void toolStripButtonDeleteWorkspace_Click(object sender, EventArgs e)
    {
      if (!DeleteWorkspace())
        return;
      SearchWorkspaces();
    }

    private void toolStripButtonUpdateWorkspaceUser_Click(object sender, EventArgs e)
    {
      UpdateComputer();
    }

    private void toolStripButtonWorkspaceDuplicate_Click(object sender, EventArgs e)
    {
      if (!DuplicateWorkspace())
        return;
      SearchWorkspaces();
    }

    private void toolStripButtonDeleteMapping_Click(object sender, EventArgs e)
    {
      DeleteWorkspaceMapping();
      UpdateSelectedWorkspace(GetSelectedWorkspace());
      SelectWorkspace();
    }

    private void toolStripButtonNewMapping_Click(object sender, EventArgs e)
    {
      if (!AddWorkspaceMapping())
        return;
      UpdateSelectedWorkspace(GetSelectedWorkspace());
      SelectWorkspace();
    }

    private void toolStripButtonCloakMapping_Click(object sender, EventArgs e)
    {
      CloakWorkspaceMapping(true);
      UpdateSelectedWorkspace(GetSelectedWorkspace());
      SelectWorkspace();
    }

    private void toolStripButtonActivateMapping_Click(object sender, EventArgs e)
    {
      CloakWorkspaceMapping(false);
      UpdateSelectedWorkspace(GetSelectedWorkspace());
      SelectWorkspace();
    }

    private void listViewDetailed_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      var listView = sender as ListView;
      (listView.ListViewItemSorter as CustomListSorter).SetColumn(listView.Columns[e.Column]);
      listView.Sort();
    }

    private void buttonDomain_Click(object sender, EventArgs e) => SetDomainLookup();

    public override StringDictionary Settings
    {
      get
      {
        _settings = new StringDictionary();
        _settings.Add("DomainName", WorkspaceViewConfiguration.Instance.DomainName);
        _settings.Add("PerformDomainLookup", WorkspaceViewConfiguration.Instance.PerformDomainLookup.ToString());
        return _settings;
      }
      set
      {
        base.Settings = value;
        if (_settings == null)
          return;
        if (_settings["DomainName"] != null)
          WorkspaceViewConfiguration.Instance.DomainName = _settings["DomainName"];
        var result = false;
        if (_settings["PerformDomainLookup"] != null)
          bool.TryParse(_settings["PerformDomainLookup"], out result);
        WorkspaceViewConfiguration.Instance.PerformDomainLookup = result;
      }
    }

    private void toolStripButtonSaveToFile_Click(object sender, EventArgs e) => SaveToFile();

    protected override void Dispose(bool disposing)
    {
      if (disposing && components != null)
        components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      components = new Container();
      var componentResourceManager = new ComponentResourceManager(typeof (WorkspaceViewControl));
      splitContainer1 = new SplitContainer();
      tableLayoutPanel1 = new TableLayoutPanel();
      listViewGrouped = new ListView();
      columnGroupedData1 = new ColumnHeader();
      columnGroupedData2 = new ColumnHeader();
      listViewDetailed = new ListView();
      columnName = new ColumnHeader();
      columnOwner = new ColumnHeader();
      columnComputer = new ColumnHeader();
      toolStripView = new ToolStrip();
      toolStripButtonDetailed = new ToolStripButton();
      toolStripButtonGroupedByOwner = new ToolStripButton();
      toolStripButtonGroupedByComputer = new ToolStripButton();
      toolStripButtonGroupedByName = new ToolStripButton();
      toolStripSeparator1 = new ToolStripSeparator();
      toolStripButtonDeleteWorkspace = new ToolStripButton();
      toolStripButtonUpdateWorkspaceUser = new ToolStripButton();
      toolStripButtonWorkspaceDuplicate = new ToolStripButton();
      panelSearch = new Panel();
      buttonDomain = new Button();
      buttonClear = new Button();
      buttonSearch = new Button();
      groupBox1 = new GroupBox();
      label4 = new Label();
      label3 = new Label();
      label2 = new Label();
      label1 = new Label();
      comboBoxUserName = new ComboBox();
      comboBoxComputer = new ComboBox();
      datePickerTo = new TeamFoundationDateTimePicker();
      datePickerFrom = new TeamFoundationDateTimePicker();
      groupBox3 = new GroupBox();
      listViewMappings = new ListView();
      columnStatus = new ColumnHeader();
      columnServerFolder = new ColumnHeader();
      columnLocalFolder = new ColumnHeader();
      toolStrip1 = new ToolStrip();
      toolStripButtonNewMapping = new ToolStripButton();
      toolStripButtonDeleteMapping = new ToolStripButton();
      toolStripButtonCloakMapping = new ToolStripButton();
      toolStripButtonActivateMapping = new ToolStripButton();
      groupBox2 = new GroupBox();
      propertyGridWorkspace = new PropertyGrid();
      toolTip1 = new ToolTip(components);
      toolStripButtonSaveToFile = new ToolStripButton();
      toolStripSeparator2 = new ToolStripSeparator();
      columnLastAccess = new ColumnHeader();
      splitContainer1.Panel1.SuspendLayout();
      splitContainer1.Panel2.SuspendLayout();
      splitContainer1.SuspendLayout();
      tableLayoutPanel1.SuspendLayout();
      toolStripView.SuspendLayout();
      panelSearch.SuspendLayout();
      groupBox1.SuspendLayout();
      groupBox3.SuspendLayout();
      toolStrip1.SuspendLayout();
      groupBox2.SuspendLayout();
      SuspendLayout();
      splitContainer1.BorderStyle = BorderStyle.Fixed3D;
      splitContainer1.Dock = DockStyle.Fill;
      splitContainer1.FixedPanel = FixedPanel.Panel1;
      splitContainer1.Location = new Point(0, 0);
      splitContainer1.Name = "splitContainer1";
      splitContainer1.Panel1.Controls.Add(tableLayoutPanel1);
      splitContainer1.Panel1.Controls.Add(toolStripView);
      splitContainer1.Panel1.Controls.Add(panelSearch);
      splitContainer1.Panel2.Controls.Add(groupBox3);
      splitContainer1.Panel2.Controls.Add(groupBox2);
      splitContainer1.Panel2.Padding = new Padding(3, 3, 0, 0);
      splitContainer1.Size = new Size(741, 501);
      splitContainer1.SplitterDistance = 387;
      splitContainer1.SplitterWidth = 2;
      splitContainer1.TabIndex = 0;
      tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      tableLayoutPanel1.ColumnCount = 2;
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0.0f));
      tableLayoutPanel1.Controls.Add(listViewGrouped, 1, 0);
      tableLayoutPanel1.Controls.Add(listViewDetailed, 0, 0);
      tableLayoutPanel1.Dock = DockStyle.Fill;
      tableLayoutPanel1.Location = new Point(0, 202);
      tableLayoutPanel1.Margin = new Padding(0);
      tableLayoutPanel1.Name = "tableLayoutPanel1";
      tableLayoutPanel1.RowCount = 1;
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      tableLayoutPanel1.Size = new Size(383, 295);
      tableLayoutPanel1.TabIndex = 0;
      listViewGrouped.BorderStyle = BorderStyle.None;
      listViewGrouped.Columns.AddRange(new ColumnHeader[2]
      {
        columnGroupedData1,
        columnGroupedData2
      });
      listViewGrouped.Dock = DockStyle.Fill;
      listViewGrouped.FullRowSelect = true;
      listViewGrouped.HeaderStyle = ColumnHeaderStyle.Nonclickable;
      listViewGrouped.HideSelection = false;
      listViewGrouped.Location = new Point(384, 2);
      listViewGrouped.Margin = new Padding(1, 2, 1, 1);
      listViewGrouped.Name = "listViewGrouped";
      listViewGrouped.Size = new Size(1, 292);
      listViewGrouped.TabIndex = 1;
      listViewGrouped.UseCompatibleStateImageBehavior = false;
      listViewGrouped.View = View.Details;
      listViewGrouped.ColumnClick += listViewDetailed_ColumnClick;
      listViewGrouped.ItemSelectionChanged += listViewWorkspace_ItemSelectionChanged;
      columnGroupedData1.Width = 140;
      columnGroupedData2.Width = 140;
      listViewDetailed.BorderStyle = BorderStyle.None;
      listViewDetailed.Columns.AddRange(new ColumnHeader[4]
      {
        columnName,
        columnOwner,
        columnComputer,
        columnLastAccess
      });
      listViewDetailed.Dock = DockStyle.Fill;
      listViewDetailed.FullRowSelect = true;
      listViewDetailed.GridLines = true;
      listViewDetailed.HideSelection = false;
      listViewDetailed.Location = new Point(1, 1);
      listViewDetailed.Margin = new Padding(1);
      listViewDetailed.Name = "listViewDetailed";
      listViewDetailed.ShowGroups = false;
      listViewDetailed.Size = new Size(381, 293);
      listViewDetailed.TabIndex = 0;
      listViewDetailed.UseCompatibleStateImageBehavior = false;
      listViewDetailed.View = View.Details;
      listViewDetailed.ColumnClick += listViewDetailed_ColumnClick;
      listViewDetailed.ItemSelectionChanged += listViewWorkspace_ItemSelectionChanged;
      columnName.Text = "Name";
      columnName.Width = 120;
      columnOwner.Text = "Owner";
      columnOwner.Width = 120;
      columnComputer.Text = "Computer";
      columnComputer.Width = 100;
      toolStripView.AllowMerge = false;
      toolStripView.CanOverflow = false;
      toolStripView.GripMargin = new Padding(1);
      toolStripView.GripStyle = ToolStripGripStyle.Hidden;
      toolStripView.Items.AddRange(new ToolStripItem[10]
      {
        toolStripButtonDetailed,
        toolStripButtonGroupedByOwner,
        toolStripButtonGroupedByComputer,
        toolStripButtonGroupedByName,
        toolStripSeparator1,
        toolStripButtonDeleteWorkspace,
        toolStripButtonUpdateWorkspaceUser,
        toolStripButtonWorkspaceDuplicate,
        toolStripSeparator2,
        toolStripButtonSaveToFile
      });
      toolStripView.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
      toolStripView.Location = new Point(0, 177);
      toolStripView.Name = "toolStripView";
      toolStripView.Padding = new Padding(3, 1, 1, 1);
      toolStripView.RenderMode = ToolStripRenderMode.System;
      toolStripView.Size = new Size(383, 25);
      toolStripView.Stretch = true;
      toolStripView.TabIndex = 1;
      toolStripButtonDetailed.Checked = true;
      toolStripButtonDetailed.CheckState = CheckState.Checked;
      toolStripButtonDetailed.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonDetailed.Image = (Image) componentResourceManager.GetObject("toolStripButtonDetailed.Image");
      toolStripButtonDetailed.ImageTransparentColor = Color.Magenta;
      toolStripButtonDetailed.Name = "toolStripButtonDetailed";
      toolStripButtonDetailed.Size = new Size(23, 20);
      toolStripButtonDetailed.Text = "toolStripButtonDetailed";
      toolStripButtonDetailed.ToolTipText = "View workspaces detailed list";
      toolStripButtonDetailed.Click += toolStripButtonDetailed_Click;
      toolStripButtonGroupedByOwner.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonGroupedByOwner.Image = (Image) componentResourceManager.GetObject("toolStripButtonGroupedByOwner.Image");
      toolStripButtonGroupedByOwner.ImageTransparentColor = Color.Magenta;
      toolStripButtonGroupedByOwner.Name = "toolStripButtonGroupedByOwner";
      toolStripButtonGroupedByOwner.Size = new Size(23, 20);
      toolStripButtonGroupedByOwner.Text = "toolStripButtonGroupedByOwner";
      toolStripButtonGroupedByOwner.ToolTipText = "View workspaces grouped by owner";
      toolStripButtonGroupedByOwner.Click += toolStripButtonGroupedByOwner_Click;
      toolStripButtonGroupedByComputer.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonGroupedByComputer.Image = (Image) componentResourceManager.GetObject("toolStripButtonGroupedByComputer.Image");
      toolStripButtonGroupedByComputer.ImageTransparentColor = Color.Magenta;
      toolStripButtonGroupedByComputer.Name = "toolStripButtonGroupedByComputer";
      toolStripButtonGroupedByComputer.Size = new Size(23, 20);
      toolStripButtonGroupedByComputer.Text = "toolStripButtonGroupedByComputer";
      toolStripButtonGroupedByComputer.ToolTipText = "View workspaces grouped by computer";
      toolStripButtonGroupedByComputer.Click += toolStripButtonGroupedByComputer_Click;
      toolStripButtonGroupedByName.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonGroupedByName.Image = (Image) componentResourceManager.GetObject("toolStripButtonGroupedByName.Image");
      toolStripButtonGroupedByName.ImageTransparentColor = Color.Magenta;
      toolStripButtonGroupedByName.Name = "toolStripButtonGroupedByName";
      toolStripButtonGroupedByName.Size = new Size(23, 20);
      toolStripButtonGroupedByName.Text = "toolStripButtonGroupedByName";
      toolStripButtonGroupedByName.ToolTipText = "View workspaces grouped by name";
      toolStripButtonGroupedByName.Click += toolStripButtonGroupedByName_Click;
      toolStripSeparator1.Name = "toolStripSeparator1";
      toolStripSeparator1.Padding = new Padding(3, 0, 3, 0);
      toolStripSeparator1.Size = new Size(6, 23);
      toolStripButtonDeleteWorkspace.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonDeleteWorkspace.Image = (Image) componentResourceManager.GetObject("toolStripButtonDeleteWorkspace.Image");
      toolStripButtonDeleteWorkspace.ImageTransparentColor = Color.Magenta;
      toolStripButtonDeleteWorkspace.Name = "toolStripButtonDeleteWorkspace";
      toolStripButtonDeleteWorkspace.Size = new Size(23, 20);
      toolStripButtonDeleteWorkspace.Text = "toolStripButton1";
      toolStripButtonDeleteWorkspace.ToolTipText = "Delete workspace";
      toolStripButtonDeleteWorkspace.Click += toolStripButtonDeleteWorkspace_Click;
      toolStripButtonUpdateWorkspaceUser.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonUpdateWorkspaceUser.Image = (Image) componentResourceManager.GetObject("toolStripButtonUpdateWorkspaceUser.Image");
      toolStripButtonUpdateWorkspaceUser.ImageTransparentColor = Color.Magenta;
      toolStripButtonUpdateWorkspaceUser.Name = "toolStripButtonUpdateWorkspaceUser";
      toolStripButtonUpdateWorkspaceUser.Size = new Size(23, 20);
      toolStripButtonUpdateWorkspaceUser.Text = "toolStripButton2";
      toolStripButtonUpdateWorkspaceUser.ToolTipText = "Update workspace computer name";
      toolStripButtonUpdateWorkspaceUser.Click += toolStripButtonUpdateWorkspaceUser_Click;
      toolStripButtonWorkspaceDuplicate.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonWorkspaceDuplicate.Image = (Image) componentResourceManager.GetObject("toolStripButtonWorkspaceDuplicate.Image");
      toolStripButtonWorkspaceDuplicate.ImageTransparentColor = Color.Magenta;
      toolStripButtonWorkspaceDuplicate.Name = "toolStripButtonWorkspaceDuplicate";
      toolStripButtonWorkspaceDuplicate.Size = new Size(23, 20);
      toolStripButtonWorkspaceDuplicate.Text = "toolStripButton3";
      toolStripButtonWorkspaceDuplicate.ToolTipText = "Duplicate workspace definitions";
      toolStripButtonWorkspaceDuplicate.Click += toolStripButtonWorkspaceDuplicate_Click;
      panelSearch.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      panelSearch.Controls.Add(buttonDomain);
      panelSearch.Controls.Add(buttonClear);
      panelSearch.Controls.Add(buttonSearch);
      panelSearch.Controls.Add(groupBox1);
      panelSearch.Dock = DockStyle.Top;
      panelSearch.Location = new Point(0, 0);
      panelSearch.Margin = new Padding(0);
      panelSearch.Name = "panelSearch";
      panelSearch.Size = new Size(383, 177);
      panelSearch.TabIndex = 2;
      buttonDomain.Image = (Image) componentResourceManager.GetObject("buttonDomain.Image");
      buttonDomain.Location = new Point(348, 143);
      buttonDomain.Name = "buttonDomain";
      buttonDomain.Size = new Size(26, 23);
      buttonDomain.TabIndex = 3;
      toolTip1.SetToolTip(buttonDomain, "Set Computer Lookup Options");
      buttonDomain.UseVisualStyleBackColor = true;
      buttonDomain.Click += buttonDomain_Click;
      buttonClear.Location = new Point(85, 143);
      buttonClear.Name = "buttonClear";
      buttonClear.Size = new Size(75, 23);
      buttonClear.TabIndex = 2;
      buttonClear.Text = "Clear";
      buttonClear.UseVisualStyleBackColor = true;
      buttonClear.Click += buttonClear_Click;
      buttonSearch.Location = new Point(4, 143);
      buttonSearch.Name = "buttonSearch";
      buttonSearch.Size = new Size(75, 23);
      buttonSearch.TabIndex = 1;
      buttonSearch.Text = "Search";
      buttonSearch.UseVisualStyleBackColor = true;
      buttonSearch.Click += buttonSearch_Click;
      groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      groupBox1.Controls.Add(label4);
      groupBox1.Controls.Add(label3);
      groupBox1.Controls.Add(label2);
      groupBox1.Controls.Add(label1);
      groupBox1.Controls.Add(comboBoxUserName);
      groupBox1.Controls.Add(comboBoxComputer);
      groupBox1.Controls.Add(datePickerTo);
      groupBox1.Controls.Add(datePickerFrom);
      groupBox1.Location = new Point(3, 3);
      groupBox1.Name = "groupBox1";
      groupBox1.Size = new Size(377, 134);
      groupBox1.TabIndex = 0;
      groupBox1.TabStop = false;
      groupBox1.Text = " Search criteria";
      label4.AutoSize = true;
      label4.Location = new Point(8, 104);
      label4.Name = "label4";
      label4.Size = new Size(106, 13);
      label4.TabIndex = 15;
      label4.Text = "Last access date (to)";
      label3.AutoSize = true;
      label3.Location = new Point(8, 77);
      label3.Name = "label3";
      label3.Size = new Size(117, 13);
      label3.TabIndex = 14;
      label3.Text = "Last access date (from)";
      label2.AutoSize = true;
      label2.Location = new Point(8, 50);
      label2.Name = "label2";
      label2.Size = new Size(81, 13);
      label2.TabIndex = 13;
      label2.Text = "Computer name";
      label1.AutoSize = true;
      label1.Location = new Point(8, 23);
      label1.Name = "label1";
      label1.Size = new Size(67, 13);
      label1.TabIndex = 12;
      label1.Text = "Owner name";
      comboBoxUserName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      comboBoxUserName.AutoCompleteMode = AutoCompleteMode.Suggest;
      comboBoxUserName.AutoCompleteSource = AutoCompleteSource.ListItems;
      comboBoxUserName.FormattingEnabled = true;
      comboBoxUserName.IntegralHeight = false;
      comboBoxUserName.ItemHeight = 13;
      comboBoxUserName.Location = new Point(139, 18);
      comboBoxUserName.MaxDropDownItems = 32;
      comboBoxUserName.Name = "comboBoxUserName";
      comboBoxUserName.Size = new Size(232, 21);
      comboBoxUserName.TabIndex = 1;
      comboBoxComputer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      comboBoxComputer.AutoCompleteMode = AutoCompleteMode.Suggest;
      comboBoxComputer.AutoCompleteSource = AutoCompleteSource.ListItems;
      comboBoxComputer.FormattingEnabled = true;
      comboBoxComputer.IntegralHeight = false;
      comboBoxComputer.Location = new Point(139, 45);
      comboBoxComputer.MaxDropDownItems = 32;
      comboBoxComputer.Name = "comboBoxComputer";
      comboBoxComputer.Size = new Size(232, 21);
      comboBoxComputer.TabIndex = 2;
      datePickerTo.AlternateBackgroundColor = SystemColors.Control;
      datePickerTo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      datePickerTo.CustomFormat = "";
      datePickerTo.Location = new Point(139, 98);
      datePickerTo.Name = "datePickerTo";
      datePickerTo.Size = new Size(232, 20);
      datePickerTo.TabIndex = 4;
      datePickerTo.UseAlternateBackgroundColor = false;
      datePickerFrom.AlternateBackgroundColor = SystemColors.Control;
      datePickerFrom.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      datePickerFrom.CustomFormat = "";
      datePickerFrom.Location = new Point(139, 72);
      datePickerFrom.Name = "datePickerFrom";
      datePickerFrom.Size = new Size(232, 20);
      datePickerFrom.TabIndex = 3;
      datePickerFrom.UseAlternateBackgroundColor = false;
      groupBox3.Controls.Add(listViewMappings);
      groupBox3.Controls.Add(toolStrip1);
      groupBox3.Dock = DockStyle.Fill;
      groupBox3.Location = new Point(3, 243);
      groupBox3.Name = "groupBox3";
      groupBox3.Size = new Size(345, 254);
      groupBox3.TabIndex = 1;
      groupBox3.TabStop = false;
      groupBox3.Text = "Working folders";
      listViewMappings.BorderStyle = BorderStyle.None;
      listViewMappings.Columns.AddRange(new ColumnHeader[3]
      {
        columnStatus,
        columnServerFolder,
        columnLocalFolder
      });
      listViewMappings.Dock = DockStyle.Fill;
      listViewMappings.FullRowSelect = true;
      listViewMappings.GridLines = true;
      listViewMappings.HeaderStyle = ColumnHeaderStyle.Nonclickable;
      listViewMappings.HideSelection = false;
      listViewMappings.Location = new Point(3, 41);
      listViewMappings.Name = "listViewMappings";
      listViewMappings.ShowGroups = false;
      listViewMappings.Size = new Size(339, 210);
      listViewMappings.TabIndex = 0;
      listViewMappings.UseCompatibleStateImageBehavior = false;
      listViewMappings.View = View.Details;
      listViewMappings.ItemSelectionChanged += listViewMappings_ItemSelectionChanged;
      columnStatus.Text = "Status";
      columnServerFolder.Text = "Source Control Folder";
      columnServerFolder.Width = 120;
      columnLocalFolder.Text = "Local Folder";
      columnLocalFolder.Width = 120;
      toolStrip1.AllowMerge = false;
      toolStrip1.CanOverflow = false;
      toolStrip1.GripMargin = new Padding(1);
      toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
      toolStrip1.Items.AddRange(new ToolStripItem[4]
      {
        toolStripButtonNewMapping,
        toolStripButtonDeleteMapping,
        toolStripButtonCloakMapping,
        toolStripButtonActivateMapping
      });
      toolStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
      toolStrip1.Location = new Point(3, 16);
      toolStrip1.Name = "toolStrip1";
      toolStrip1.Padding = new Padding(3, 1, 1, 1);
      toolStrip1.RenderMode = ToolStripRenderMode.System;
      toolStrip1.Size = new Size(339, 25);
      toolStrip1.Stretch = true;
      toolStrip1.TabIndex = 2;
      toolStripButtonNewMapping.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonNewMapping.Image = (Image) componentResourceManager.GetObject("toolStripButtonNewMapping.Image");
      toolStripButtonNewMapping.ImageTransparentColor = Color.Magenta;
      toolStripButtonNewMapping.Name = "toolStripButtonNewMapping";
      toolStripButtonNewMapping.Size = new Size(23, 20);
      toolStripButtonNewMapping.Text = "toolStripButtonDetailed";
      toolStripButtonNewMapping.ToolTipText = "Add new working folder";
      toolStripButtonNewMapping.Click += toolStripButtonNewMapping_Click;
      toolStripButtonDeleteMapping.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonDeleteMapping.Image = (Image) componentResourceManager.GetObject("toolStripButtonDeleteMapping.Image");
      toolStripButtonDeleteMapping.ImageTransparentColor = Color.Magenta;
      toolStripButtonDeleteMapping.Name = "toolStripButtonDeleteMapping";
      toolStripButtonDeleteMapping.Size = new Size(23, 20);
      toolStripButtonDeleteMapping.Text = "toolStripButton1";
      toolStripButtonDeleteMapping.ToolTipText = "Delete working folder";
      toolStripButtonDeleteMapping.Click += toolStripButtonDeleteMapping_Click;
      toolStripButtonCloakMapping.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonCloakMapping.Image = (Image) componentResourceManager.GetObject("toolStripButtonCloakMapping.Image");
      toolStripButtonCloakMapping.ImageTransparentColor = Color.Magenta;
      toolStripButtonCloakMapping.Name = "toolStripButtonCloakMapping";
      toolStripButtonCloakMapping.Size = new Size(23, 20);
      toolStripButtonCloakMapping.Text = "toolStripButton5";
      toolStripButtonCloakMapping.ToolTipText = "Cloak active working folder";
      toolStripButtonCloakMapping.Visible = false;
      toolStripButtonCloakMapping.Click += toolStripButtonCloakMapping_Click;
      toolStripButtonActivateMapping.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonActivateMapping.Image = (Image) componentResourceManager.GetObject("toolStripButtonActivateMapping.Image");
      toolStripButtonActivateMapping.ImageTransparentColor = Color.Magenta;
      toolStripButtonActivateMapping.Name = "toolStripButtonActivateMapping";
      toolStripButtonActivateMapping.Size = new Size(23, 20);
      toolStripButtonActivateMapping.Text = "toolStripButton1";
      toolStripButtonActivateMapping.ToolTipText = "Activate cloaked working folder";
      toolStripButtonActivateMapping.Visible = false;
      toolStripButtonActivateMapping.Click += toolStripButtonActivateMapping_Click;
      groupBox2.Controls.Add(propertyGridWorkspace);
      groupBox2.Dock = DockStyle.Top;
      groupBox2.Location = new Point(3, 3);
      groupBox2.Name = "groupBox2";
      groupBox2.Size = new Size(345, 240);
      groupBox2.TabIndex = 0;
      groupBox2.TabStop = false;
      groupBox2.Text = "Workspace properties";
      propertyGridWorkspace.Dock = DockStyle.Fill;
      propertyGridWorkspace.Location = new Point(3, 16);
      propertyGridWorkspace.Name = "propertyGridWorkspace";
      propertyGridWorkspace.Size = new Size(339, 221);
      propertyGridWorkspace.TabIndex = 0;
      propertyGridWorkspace.ToolbarVisible = false;
      toolStripButtonSaveToFile.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonSaveToFile.Image = (Image) componentResourceManager.GetObject("toolStripButtonSaveToFile.Image");
      toolStripButtonSaveToFile.ImageTransparentColor = Color.Black;
      toolStripButtonSaveToFile.Name = "toolStripButtonSaveToFile";
      toolStripButtonSaveToFile.Size = new Size(23, 20);
      toolStripButtonSaveToFile.Text = "toolStripButton5";
      toolStripButtonSaveToFile.ToolTipText = "Save list to file";
      toolStripButtonSaveToFile.Click += toolStripButtonSaveToFile_Click;
      toolStripSeparator2.Name = "toolStripSeparator2";
      toolStripSeparator2.Size = new Size(6, 23);
      columnLastAccess.Text = "Last Access Date";
      columnLastAccess.Width = 80;
      AutoScaleDimensions = new SizeF(6f, 13f);
      AutoScaleMode = AutoScaleMode.Font;
      Controls.Add(splitContainer1);
      Name = nameof (WorkspaceViewControl);
      Size = new Size(741, 501);
      splitContainer1.Panel1.ResumeLayout(false);
      splitContainer1.Panel1.PerformLayout();
      splitContainer1.Panel2.ResumeLayout(false);
      splitContainer1.ResumeLayout(false);
      tableLayoutPanel1.ResumeLayout(false);
      toolStripView.ResumeLayout(false);
      toolStripView.PerformLayout();
      panelSearch.ResumeLayout(false);
      groupBox1.ResumeLayout(false);
      groupBox1.PerformLayout();
      groupBox3.ResumeLayout(false);
      groupBox3.PerformLayout();
      toolStrip1.ResumeLayout(false);
      toolStrip1.PerformLayout();
      groupBox2.ResumeLayout(false);
      ResumeLayout(false);
    }

    public override void Initialize(TfsController controller)
    {
      _controller = new WorkspaceViewController(controller);
      _dtUsers = _controller.Users.UsersTable.Copy();
      ListTable.AddAllRow(_dtUsers);
      _dtComputers = !WorkspaceViewConfiguration.Instance.PerformDomainLookup ? _controller.GetComputers(null) : _controller.GetComputers(WorkspaceViewConfiguration.Instance.DomainName);
      ListTable.AddAllRow(_dtComputers);
      LoadSearchParameters();
      DefaultSearchParameters();
      ClearWorkspaces();
      ClearWorkspaceDetails();
    }

    public override void LoadUsers(TfsController controller)
    {
      var text = comboBoxUserName.Text;
      _dtUsers = controller.Users.UsersTable.Copy();
      ListTable.AddAllRow(_dtUsers);
      ListTable.LoadTable(comboBoxUserName, _dtUsers, _controller.UserFullName);
      if (string.IsNullOrEmpty(text))
        comboBoxUserName.SelectedValue = _controller.UserFullName;
      else
        comboBoxUserName.SelectedValue = text;
    }

    private void ViewDetailedList()
    {
      tableLayoutPanel1.SuspendLayout();
      tableLayoutPanel1.ColumnStyles[0].Width = 100f;
      tableLayoutPanel1.ColumnStyles[1].Width = 0.0f;
      tableLayoutPanel1.ResumeLayout();
    }

    private void ViewGroupedList(WorkspaceGrouping grouping)
    {
      tableLayoutPanel1.SuspendLayout();
      tableLayoutPanel1.ColumnStyles[1].Width = 100f;
      tableLayoutPanel1.ColumnStyles[0].Width = 0.0f;
      listViewGrouped.Groups.Clear();
      listViewGrouped.Items.Clear();
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        listViewGrouped.BeginUpdate();
        switch (grouping)
        {
          case WorkspaceGrouping.ByName:
            columnGroupedData1.Text = "Owner";
            columnGroupedData2.Text = "Computer";
            break;
          case WorkspaceGrouping.ByOwner:
            columnGroupedData1.Text = "Workspace name";
            columnGroupedData2.Text = "Computer";
            break;
          case WorkspaceGrouping.ByComputer:
            columnGroupedData1.Text = "Workspace name";
            columnGroupedData2.Text = "Owner";
            break;
        }
        foreach (ListViewItem listViewItem in listViewDetailed.Items)
        {
          var tag = listViewItem.Tag as Workspace;
          var str1 = tag.Name;
          var str2 = Utilities.GetTableValueByID(_dtUsers, tag.OwnerName);
          var str3 = tag.Computer;
          switch (grouping)
          {
            case WorkspaceGrouping.ByOwner:
              str1 = Utilities.GetTableValueByID(_dtUsers, tag.OwnerName);
              str2 = tag.Name;
              str3 = tag.Computer;
              break;
            case WorkspaceGrouping.ByComputer:
              str1 = tag.Computer;
              str2 = tag.Name;
              str3 = Utilities.GetTableValueByID(_dtUsers, tag.OwnerName);
              break;
          }
          listViewGrouped.Items.Add(new ListViewItem(listViewGrouped.Groups[str1] ?? AddGroupSorted(listViewGrouped.Groups, str1))
          {
            Text = str2,
            SubItems = {
              str3
            },
            Tag = listViewItem.Tag
          });
        }
      }
      finally
      {
        listViewGrouped.EndUpdate();
        tableLayoutPanel1.ResumeLayout();
        Cursor.Current = Cursors.Default;
      }
    }

    private ListViewGroup AddGroupSorted(ListViewGroupCollection groups, string groupName)
    {
      var index = 0;
      foreach (ListViewGroup group in groups)
      {
        if (string.Compare(group.Name, groupName) <= 0)
          ++index;
        else
          break;
      }
      var str = groupName;
      var group1 = new ListViewGroup(str, str);
      groups.Insert(index, group1);
      return group1;
    }

    private void DefaultSearchParameters()
    {
      comboBoxComputer.SelectedValue = _controller.Server.WorkstationName;
      comboBoxUserName.SelectedValue = _controller.UserFullName;
      datePickerTo.Value = DateTime.Now;
      datePickerFrom.Value = DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0));
    }

    private void ClearWorkspaceDetails()
    {
      propertyGridWorkspace.SelectedObject = null;
      listViewMappings.Items.Clear();
      toolStripButtonNewMapping.Enabled = toolStripButtonDeleteMapping.Enabled = toolStripButtonActivateMapping.Enabled = toolStripButtonCloakMapping.Enabled = false;
    }

    private void LoadWorkspaceDetails(Workspace workspace)
    {
      ClearWorkspaceDetails();
      propertyGridWorkspace.SelectedObject = new WorkspaceProperties(workspace, Utilities.GetTableValueByID(_dtUsers, workspace.OwnerName));
      toolStripButtonNewMapping.Enabled = true;
      listViewMappings.BeginUpdate();
      try
      {
        foreach (var folder in workspace.Folders)
          listViewMappings.Items.Add(new ListViewItem(folder.IsCloaked ? "Cloaked" : "Active", -1)
          {
            SubItems = {
              folder.ServerItem,
              folder.LocalItem
            }
          });
      }
      finally
      {
        listViewMappings.EndUpdate();
      }
    }

    private void ClearWorkspaces()
    {
      listViewDetailed.Items.Clear();
      listViewGrouped.Items.Clear();
      listViewGrouped.Groups.Clear();
      toolStripButtonDeleteWorkspace.Enabled = toolStripButtonUpdateWorkspaceUser.Enabled = toolStripButtonWorkspaceDuplicate.Enabled = false;
    }

    private SearchParameters GetSearchParameters()
    {
      var userName = comboBoxUserName.SelectedValue == null ? comboBoxUserName.Text : comboBoxUserName.SelectedValue.ToString();
      var computerName = comboBoxComputer.SelectedValue == null ? comboBoxComputer.Text : comboBoxComputer.SelectedValue.ToString();
      if (userName == ListTable.cAllID || userName == "")
        userName = null;
      if (computerName == ListTable.cAllID || computerName == "")
        computerName = null;
      var dateTime1 = datePickerFrom.Value;
      var dateTime2 = datePickerTo.Value;
      return new SearchParameters(userName, computerName, null, new DateTime(dateTime1.Year, dateTime1.Month, dateTime1.Day), new DateTime(dateTime2.Year, dateTime2.Month, dateTime2.Day, 23, 59, 59));
    }

    private void SearchWorkspaces()
    {
      ClearWorkspaces();
      listViewDetailed.BeginUpdate();
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        foreach (var searchWorkspace in _controller.SearchWorkspaces(GetSearchParameters()))
          listViewDetailed.Items.Add(new ListViewItem(searchWorkspace.Name)
          {
            SubItems = {
              Utilities.GetTableValueByID(_dtUsers, searchWorkspace.OwnerName),
              searchWorkspace.Computer,
              searchWorkspace.LastAccessDate.ToShortDateString()
            },
            Tag = searchWorkspace
          });
        if (toolStripButtonDetailed.Checked)
          return;
        if (toolStripButtonGroupedByComputer.Checked)
          ViewGroupedList(WorkspaceGrouping.ByComputer);
        else if (toolStripButtonGroupedByOwner.Checked)
        {
          ViewGroupedList(WorkspaceGrouping.ByOwner);
        }
        else
        {
          if (!toolStripButtonGroupedByName.Checked)
            return;
          ViewGroupedList(WorkspaceGrouping.ByName);
        }
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to search workspaces." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        listViewDetailed.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
    }

    private void SelectWorkspace()
    {
      var listView = listViewDetailed;
      if (!toolStripButtonDetailed.Checked)
        listView = listViewGrouped;
      if (listView.SelectedIndices.Count == 0)
      {
        toolStripButtonDeleteWorkspace.Enabled = toolStripButtonUpdateWorkspaceUser.Enabled = toolStripButtonWorkspaceDuplicate.Enabled = false;
        ClearWorkspaceDetails();
      }
      else
      {
        toolStripButtonDeleteWorkspace.Enabled = toolStripButtonUpdateWorkspaceUser.Enabled = true;
        if (listView.SelectedIndices.Count == 1)
        {
          toolStripButtonWorkspaceDuplicate.Enabled = true;
          LoadWorkspaceDetails(listView.SelectedItems[0].Tag as Workspace);
        }
        else
        {
          toolStripButtonWorkspaceDuplicate.Enabled = false;
          ClearWorkspaceDetails();
        }
      }
    }

    private void LoadSearchParameters()
    {
      ListTable.LoadTable(comboBoxUserName, _dtUsers, _controller.UserFullName);
      ListTable.LoadTable(comboBoxComputer, _dtComputers, _controller.Server.WorkstationName);
    }

    private bool DeleteWorkspace()
    {
      var listView = listViewDetailed;
      if (!toolStripButtonDetailed.Checked)
        listView = listViewGrouped;
      if (MessageBox.Show("Do you want to delete selected workspace(s)?", "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return false;
      var formResults = new FormResults();
      formResults.Initialize("Delete workspace");
      Cursor.Current = Cursors.WaitCursor;
      try
      {
        foreach (ListViewItem selectedItem in listView.SelectedItems)
        {
          var tag = selectedItem.Tag as Workspace;
          try
          {
            _controller.DeleteWorkspace(tag);
            formResults.AddResult(true, "Workspace " + tag.Name, "Workspace deleted");
          }
          catch (Exception ex)
          {
            formResults.AddResult(false, "Workspace " + tag.Name, ex.Message);
          }
        }
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
      if (formResults.HasErrors)
      {
        var num = (int) formResults.ShowDialog(this);
      }
      return true;
    }

    private Workspace GetSelectedWorkspace()
    {
      var listView = listViewDetailed;
      if (!toolStripButtonDetailed.Checked)
        listView = listViewGrouped;
      return listView.SelectedItems[0].Tag as Workspace;
    }

    private void UpdateSelectedWorkspace(Workspace workspace)
    {
      var listView = listViewDetailed;
      if (!toolStripButtonDetailed.Checked)
        listView = listViewGrouped;
      listView.SelectedItems[0].Tag = workspace;
    }

    private void UpdateComputer()
    {
      var listView = listViewDetailed;
      if (!toolStripButtonDetailed.Checked)
        listView = listViewGrouped;
      var formResults = new FormResults();
      formResults.Initialize("Update computer name");
      Cursor.Current = Cursors.WaitCursor;
      try
      {
        foreach (ListViewItem selectedItem in listView.SelectedItems)
        {
          var tag = selectedItem.Tag as Workspace;
          try
          {
            _controller.UpdateWorkspaceComputer(tag);
            formResults.AddResult(true, "Workspace " + tag.Name, "Computer updated");
          }
          catch (Exception ex)
          {
            formResults.AddResult(false, "Workspace " + tag.Name, ex.Message);
          }
        }
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
      if (!formResults.HasErrors)
        return;
      var num = (int) formResults.ShowDialog(this);
    }

    private void DeleteWorkspaceMapping()
    {
      var selectedWorkspace = GetSelectedWorkspace();
      Cursor.Current = Cursors.WaitCursor;
      var formResults = new FormResults();
      formResults.Initialize("Delete mappings");
      try
      {
        foreach (ListViewItem selectedItem in listViewMappings.SelectedItems)
        {
          try
          {
            _controller.DeleteWorkspaceMapping(selectedWorkspace, selectedItem.SubItems[1].Text);
            formResults.AddResult(true, "Mapping " + selectedItem.SubItems[1].Text, "Mapping deleted");
          }
          catch (Exception ex)
          {
            formResults.AddResult(false, "Mapping " + selectedItem.SubItems[1].Text, ex.Message);
          }
        }
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
      if (!formResults.HasErrors)
        return;
      var num = (int) formResults.ShowDialog(this);
    }

    private bool AddWorkspaceMapping()
    {
      var selectedWorkspace = GetSelectedWorkspace();
      try
      {
        var workspaceMapping = new FormAddWorkspaceMapping();
        workspaceMapping.WorkspaceComputer = selectedWorkspace.Computer;
        workspaceMapping.WorkspaceName = selectedWorkspace.Name;
        workspaceMapping.WorkspaceOwner = Utilities.GetTableValueByID(_dtUsers, selectedWorkspace.OwnerName);
        if (workspaceMapping.ShowDialog() != DialogResult.OK)
          return false;
        try
        {
          _controller.AddWorkspaceMapping(selectedWorkspace, workspaceMapping.ServerPath, workspaceMapping.LocalPath);
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to add new mapping." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      return true;
    }

    private void CloakWorkspaceMapping(bool cloak)
    {
      var selectedWorkspace = GetSelectedWorkspace();
      foreach (ListViewItem selectedItem in listViewMappings.SelectedItems)
      {
        try
        {
          _controller.CloakWorkspaceMapping(selectedWorkspace, selectedItem.SubItems[1].Text, cloak);
        }
        catch (Exception ex)
        {
          var num = (int) MessageBox.Show("Failed to cloak mapping." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
    }

    private bool DuplicateWorkspace()
    {
      var selectedWorkspace = GetSelectedWorkspace();
      var duplicateWorkspaces = new FormDuplicateWorkspaces();
      duplicateWorkspaces.Workspace = selectedWorkspace;
      duplicateWorkspaces.Users = _dtUsers;
      duplicateWorkspaces.Computers = _dtComputers;
      if (duplicateWorkspaces.ShowDialog() != DialogResult.OK)
        return false;
      var formResults = new FormResults();
      formResults.Initialize("Duplicate workspaces");
      foreach (var newWorkspace in duplicateWorkspaces.NewWorkspaces())
      {
        try
        {
          _controller.DuplicateWorkspace(selectedWorkspace, newWorkspace.Name, newWorkspace.OwnerNameRaw, newWorkspace.Computer, "Workspace created by Workspace Sidekick");
          formResults.AddResult(true,
              $"Workspace {(object)newWorkspace.Name} at {(object)newWorkspace.Computer} for user {(object)newWorkspace.OwnerName}", "Workspace created");
        }
        catch (Exception ex)
        {
          formResults.AddResult(false,
              $"Workspace {(object)newWorkspace.Name} at {(object)newWorkspace.Computer} for user {(object)newWorkspace.OwnerName}", ex.Message);
        }
      }
      if (formResults.HasErrors)
      {
        var num = (int) formResults.ShowDialog(this);
      }
      return true;
    }

    private void SetDomainLookup()
    {
      var formDomain = new FormDomain();
      formDomain.Initialize(WorkspaceViewConfiguration.Instance.PerformDomainLookup, WorkspaceViewConfiguration.Instance.DomainName);
      if (formDomain.ShowDialog(this) != DialogResult.OK)
        return;
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        _dtComputers = !formDomain.Lookup ? _controller.GetComputers(null) : _controller.GetComputers(formDomain.DomainName);
        ListTable.AddAllRow(_dtComputers);
        ListTable.LoadTable(comboBoxComputer, _dtComputers, _controller.Server.WorkstationName);
        WorkspaceViewConfiguration.Instance.DomainName = formDomain.DomainName;
        WorkspaceViewConfiguration.Instance.PerformDomainLookup = formDomain.Lookup;
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to lookup computers." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
    }

    private void SaveToFile()
    {
      Utilities.SaveListViewToFile(saveFileDialog, listViewDetailed,
          $"List of workspaces as of {(object)DateTime.Now}");
    }

    private enum WorkspaceGrouping
    {
      Undefined,
      ByName,
      ByOwner,
      ByComputer,
    }
  }
}
