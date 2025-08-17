// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.LabelViewControl
// Assembly: Attrice.TeamFoundation.Controls.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=1a24c887ab53aa6e
// MVID: 393BE899-3C29-4A70-86D7-B4B965E5A8F5
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controls.12.dll

using System;
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
  public class LabelViewControl : BaseSidekickControl
  {
    private IContainer components;
    private Panel panelSearch;
    private Button buttonClear;
    private Button buttonSearch;
    private GroupBox groupBox1;
    private TextBox textBoxLabelName;
    private Label label5;
    private Label label4;
    private Label label3;
    private Label label2;
    private Label label1;
    private ComboBox comboBoxUserName;
    private ComboBox comboBoxProject;
    private TeamFoundationDateTimePicker datePickerTo;
    private TeamFoundationDateTimePicker datePickerFrom;
    private VirtualListView listViewLabels;
    private ColumnHeader columnHeader1;
    private SplitContainer splitContainer1;
    private ColumnHeader columnHeader2;
    private ColumnHeader columnHeader3;
    private ToolStrip toolStrip2;
    private ToolStripButton toolStripButtonDeleteLabel;
    private TabControl tabControl1;
    private TabPage tabPageProperties;
    private TabPage tabPageItems;
    private GroupBox groupBox3;
    private VirtualListView listViewItems;
    private ColumnHeader columnItem;
    private ColumnHeader columnChangeset;
    private ColumnHeader columnCheckinDate;
    private ColumnHeader columnType;
    private ToolStrip toolStrip1;
    private ToolStripButton toolStripButtonUnlabel;
    private GroupBox groupBox2;
    private PropertyGrid propertyGridLabel;
    private VirtualListView listViewWorkItems;
    private ColumnHeader columnHeader4;
    private ColumnHeader columnHeader5;
    private ColumnHeader columnHeader6;
    private ColumnHeader columnHeader7;
    private TabPage tabPageChangesets;
    private ColumnHeader columnHeader8;
    private ColumnHeader columnHeader9;
    private ColumnHeader columnHeader10;
    private ColumnHeader columnHeader11;
    private ColumnHeader columnHeader16;
    private ColumnHeader columnHeader17;
    private ToolTip toolTip1;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem compareToolStripMenuItem;
    private ToolStripMenuItem compareLabelWithLatestToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripButton toolStripButtonCompareLatest;
    private ToolStripButton toolStripButtonCompareTwo;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripButton toolStripButtonCompareVersion;
    private ContextMenuStrip contextMenuStrip2;
    private ToolStripMenuItem compareWithLatestToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripButton toolStripButtonSaveToFile;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripButton toolStripButtonSaveToFileLabels;
    private ColumnHeader columnHeader18;
    private ContextMenuStrip contextMenuStripChangesets;
    private ToolStripMenuItem viewChangesetDetailsToolStripMenuItem;
    private ToolStripMenuItem viewChangesetDetailsToolStripMenuItem1;
    private ToolStripButton toolStripButtonViewChangesetDetails;
    private VirtualListView listViewChangesets;
    private ColumnHeader columnHeader12;
    private ColumnHeader columnHeader13;
    private ColumnHeader columnHeader14;
    private ColumnHeader columnHeader15;
    private ToolStrip toolStrip4;
    private ToolStripButton toolStripButtonViewChangesetDetails1;
    private ToolStrip toolStrip3;
    private ToolStripButton toolStripButtonSaveWorkItems;
    private ToolStripButton toolStripButtonSaveChangesets;
    private LabelViewController _controller;
    private DataTable _dtUsers;
    private DataTable _dtProjects;

    public LabelViewControl()
    {
      InitializeComponent();
      splitContainer1.Panel1MinSize = 350;
      splitContainer1.Panel2MinSize = 350;
      panelSearch.BackColor = SystemColors.Control;
      splitContainer1.BackColor = SystemColors.Control;
      datePickerTo.CustomFormat = datePickerFrom.CustomFormat = "dd-MMM-yyyy";
      datePickerTo.Format = datePickerFrom.Format = DateTimePickerFormat.Custom;
      toolStripButtonCompareLatest.Enabled = false;
      toolStripButtonCompareTwo.Enabled = false;
      listViewLabels.Sorted = true;
      listViewItems.Sorted = true;
      listViewChangesets.Sorted = true;
      listViewWorkItems.Sorted = true;
      listViewLabels.VirtualItemsSelectionRangeChanged += listViewLabels_VirtualItemsSelectionRangeChanged;
      Name = "Label Sidekick";
    }

    public override Image Image => Resources.LabelsImage;

    private void buttonSearch_Click(object sender, EventArgs e) => SearchLabels();

    private void buttonClear_Click(object sender, EventArgs e)
    {
      DefaultSearchParameters();
      ClearLabelsList();
    }

    private void listViewLabels_SelectedIndexChanged(object sender, EventArgs e)
    {
      SelectLabel();
    }

    private void listViewLabels_VirtualItemsSelectionRangeChanged(
      object sender,
      ListViewVirtualItemsSelectionRangeChangedEventArgs e)
    {
      SelectLabel();
    }

    private void listViewItems_SelectedIndexChanged(object sender, EventArgs e)
    {
      SelectItem();
    }

    private void toolStripButtonUnlabel_Click(object sender, EventArgs e) => UnlabelItem();

    private void toolStripButtonDeleteLabel_Click(object sender, EventArgs e)
    {
      if (!DeleteLabel())
        return;
      SearchLabels();
    }

    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) => ChangeTab();

    private void compareToolStripMenuItem_Click(object sender, EventArgs e) => CompareLabels();

    private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
    {
      compareLabelWithLatestToolStripMenuItem.Enabled = listViewLabels.SelectedIndices.Count == 1;
      compareToolStripMenuItem.Enabled = listViewLabels.SelectedIndices.Count == 2;
    }

    private void toolStripButtonCompareVersion_Click(object sender, EventArgs e)
    {
      CompareVersion();
    }

    private void compareWithLatestToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CompareVersion();
    }

    private void toolStripButtonUnlabel_Click_1(object sender, EventArgs e) => UnlabelItem();

    private void toolStripButtonSaveToFile_Click(object sender, EventArgs e)
    {
      SaveItemsToFile();
    }

    private void toolStripButtonSaveToFileLabels_Click(object sender, EventArgs e)
    {
      SaveLabelsToFile();
    }

    private void contextMenuStripChangesets_Opening(object sender, CancelEventArgs e)
    {
      e.Cancel = listViewChangesets.SelectedIndices.Count != 1;
    }

    private void viewChangesetDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      ViewAssociatedChangesetDetails();
    }

    private void toolStripButtonViewChangesetDetails_Click(object sender, EventArgs e)
    {
      ViewAssociatedChangesetDetails();
    }

    private void listViewChangesets_SelectedIndexChanged(object sender, EventArgs e)
    {
      toolStripButtonViewChangesetDetails1.Enabled = listViewChangesets.SelectedIndices.Count == 1;
    }

    private void viewChangesetDetailsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ViewSelectedChangesetDetails();
    }

    private void toolStripButtonViewChangesetDetails1_Click(object sender, EventArgs e)
    {
      ViewSelectedChangesetDetails();
    }

    private void toolStripButtonSaveWorkItems_Click(object sender, EventArgs e)
    {
      SaveWorkItemsToFile();
    }

    private void toolStripButtonSaveChangesets_Click(object sender, EventArgs e)
    {
      SaveChangesetsToFile();
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
      var componentResourceManager = new ComponentResourceManager(typeof (LabelViewControl));
      panelSearch = new Panel();
      buttonClear = new Button();
      buttonSearch = new Button();
      groupBox1 = new GroupBox();
      textBoxLabelName = new TextBox();
      label5 = new Label();
      label4 = new Label();
      label3 = new Label();
      label2 = new Label();
      label1 = new Label();
      comboBoxUserName = new ComboBox();
      comboBoxProject = new ComboBox();
      datePickerTo = new TeamFoundationDateTimePicker();
      datePickerFrom = new TeamFoundationDateTimePicker();
      listViewLabels = new VirtualListView();
      columnHeader1 = new ColumnHeader();
      columnHeader2 = new ColumnHeader();
      columnHeader3 = new ColumnHeader();
      columnHeader18 = new ColumnHeader();
      contextMenuStrip1 = new ContextMenuStrip(components);
      compareLabelWithLatestToolStripMenuItem = new ToolStripMenuItem();
      compareToolStripMenuItem = new ToolStripMenuItem();
      splitContainer1 = new SplitContainer();
      toolStrip2 = new ToolStrip();
      toolStripButtonDeleteLabel = new ToolStripButton();
      toolStripSeparator1 = new ToolStripSeparator();
      toolStripButtonCompareLatest = new ToolStripButton();
      toolStripButtonCompareTwo = new ToolStripButton();
      toolStripSeparator4 = new ToolStripSeparator();
      toolStripButtonSaveToFileLabels = new ToolStripButton();
      tabControl1 = new TabControl();
      tabPageProperties = new TabPage();
      groupBox3 = new GroupBox();
      listViewItems = new VirtualListView();
      columnItem = new ColumnHeader();
      columnChangeset = new ColumnHeader();
      columnCheckinDate = new ColumnHeader();
      columnType = new ColumnHeader();
      contextMenuStrip2 = new ContextMenuStrip(components);
      compareWithLatestToolStripMenuItem = new ToolStripMenuItem();
      viewChangesetDetailsToolStripMenuItem1 = new ToolStripMenuItem();
      toolStrip1 = new ToolStrip();
      toolStripButtonUnlabel = new ToolStripButton();
      toolStripSeparator2 = new ToolStripSeparator();
      toolStripButtonCompareVersion = new ToolStripButton();
      toolStripButtonViewChangesetDetails = new ToolStripButton();
      toolStripSeparator3 = new ToolStripSeparator();
      toolStripButtonSaveToFile = new ToolStripButton();
      groupBox2 = new GroupBox();
      propertyGridLabel = new PropertyGrid();
      tabPageChangesets = new TabPage();
      listViewChangesets = new VirtualListView();
      columnHeader12 = new ColumnHeader();
      columnHeader13 = new ColumnHeader();
      columnHeader14 = new ColumnHeader();
      columnHeader15 = new ColumnHeader();
      contextMenuStripChangesets = new ContextMenuStrip(components);
      viewChangesetDetailsToolStripMenuItem = new ToolStripMenuItem();
      toolStrip4 = new ToolStrip();
      toolStripButtonViewChangesetDetails1 = new ToolStripButton();
      toolStripButtonSaveChangesets = new ToolStripButton();
      tabPageItems = new TabPage();
      listViewWorkItems = new VirtualListView();
      columnHeader4 = new ColumnHeader();
      columnHeader5 = new ColumnHeader();
      columnHeader6 = new ColumnHeader();
      columnHeader7 = new ColumnHeader();
      columnHeader16 = new ColumnHeader();
      columnHeader17 = new ColumnHeader();
      toolStrip3 = new ToolStrip();
      toolStripButtonSaveWorkItems = new ToolStripButton();
      columnHeader8 = new ColumnHeader();
      columnHeader9 = new ColumnHeader();
      columnHeader10 = new ColumnHeader();
      columnHeader11 = new ColumnHeader();
      toolTip1 = new ToolTip(components);
      panelSearch.SuspendLayout();
      groupBox1.SuspendLayout();
      contextMenuStrip1.SuspendLayout();
      splitContainer1.Panel1.SuspendLayout();
      splitContainer1.Panel2.SuspendLayout();
      splitContainer1.SuspendLayout();
      toolStrip2.SuspendLayout();
      tabControl1.SuspendLayout();
      tabPageProperties.SuspendLayout();
      groupBox3.SuspendLayout();
      contextMenuStrip2.SuspendLayout();
      toolStrip1.SuspendLayout();
      groupBox2.SuspendLayout();
      tabPageChangesets.SuspendLayout();
      contextMenuStripChangesets.SuspendLayout();
      toolStrip4.SuspendLayout();
      tabPageItems.SuspendLayout();
      toolStrip3.SuspendLayout();
      SuspendLayout();
      panelSearch.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      panelSearch.Controls.Add(buttonClear);
      panelSearch.Controls.Add(buttonSearch);
      panelSearch.Controls.Add(groupBox1);
      panelSearch.Dock = DockStyle.Top;
      panelSearch.Location = new Point(0, 0);
      panelSearch.Margin = new Padding(0);
      panelSearch.Name = "panelSearch";
      panelSearch.Size = new Size(346, 204);
      panelSearch.TabIndex = 2;
      buttonClear.Location = new Point(85, 174);
      buttonClear.Name = "buttonClear";
      buttonClear.Size = new Size(75, 23);
      buttonClear.TabIndex = 2;
      buttonClear.Text = "Clear";
      buttonClear.UseVisualStyleBackColor = true;
      buttonClear.Click += buttonClear_Click;
      buttonSearch.Location = new Point(4, 174);
      buttonSearch.Name = "buttonSearch";
      buttonSearch.Size = new Size(75, 23);
      buttonSearch.TabIndex = 1;
      buttonSearch.Text = "Search";
      buttonSearch.UseVisualStyleBackColor = true;
      buttonSearch.Click += buttonSearch_Click;
      groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      groupBox1.Controls.Add(textBoxLabelName);
      groupBox1.Controls.Add(label5);
      groupBox1.Controls.Add(label4);
      groupBox1.Controls.Add(label3);
      groupBox1.Controls.Add(label2);
      groupBox1.Controls.Add(label1);
      groupBox1.Controls.Add(comboBoxUserName);
      groupBox1.Controls.Add(comboBoxProject);
      groupBox1.Controls.Add(datePickerTo);
      groupBox1.Controls.Add(datePickerFrom);
      groupBox1.Location = new Point(3, 3);
      groupBox1.Name = "groupBox1";
      groupBox1.Size = new Size(340, 165);
      groupBox1.TabIndex = 0;
      groupBox1.TabStop = false;
      groupBox1.Text = " Search criteria";
      textBoxLabelName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      textBoxLabelName.Location = new Point(139, 16);
      textBoxLabelName.Name = "textBoxLabelName";
      textBoxLabelName.Size = new Size(195, 20);
      textBoxLabelName.TabIndex = 17;
      toolTip1.SetToolTip(textBoxLabelName, "Search string may contain both exact label names and names with wildcards * or ?");
      label5.AutoSize = true;
      label5.Location = new Point(9, 21);
      label5.Name = "label5";
      label5.Size = new Size(62, 13);
      label5.TabIndex = 16;
      label5.Text = "Label name";
      label4.AutoSize = true;
      label4.Location = new Point(8, 128);
      label4.Name = "label4";
      label4.Size = new Size(86, 13);
      label4.TabIndex = 15;
      label4.Text = "Change date (to)";
      label3.AutoSize = true;
      label3.Location = new Point(8, 101);
      label3.Name = "label3";
      label3.Size = new Size(97, 13);
      label3.TabIndex = 14;
      label3.Text = "Change date (from)";
      label2.AutoSize = true;
      label2.Location = new Point(8, 74);
      label2.Name = "label2";
      label2.Size = new Size(69, 13);
      label2.TabIndex = 13;
      label2.Text = "Project name";
      label1.AutoSize = true;
      label1.Location = new Point(8, 47);
      label1.Name = "label1";
      label1.Size = new Size(58, 13);
      label1.TabIndex = 12;
      label1.Text = "User name";
      comboBoxUserName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      comboBoxUserName.AutoCompleteMode = AutoCompleteMode.Suggest;
      comboBoxUserName.AutoCompleteSource = AutoCompleteSource.ListItems;
      comboBoxUserName.FormattingEnabled = true;
      comboBoxUserName.IntegralHeight = false;
      comboBoxUserName.ItemHeight = 13;
      comboBoxUserName.Location = new Point(139, 42);
      comboBoxUserName.MaxDropDownItems = 32;
      comboBoxUserName.Name = "comboBoxUserName";
      comboBoxUserName.Size = new Size(195, 21);
      comboBoxUserName.TabIndex = 1;
      comboBoxProject.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      comboBoxProject.AutoCompleteMode = AutoCompleteMode.Suggest;
      comboBoxProject.AutoCompleteSource = AutoCompleteSource.ListItems;
      comboBoxProject.FormattingEnabled = true;
      comboBoxProject.IntegralHeight = false;
      comboBoxProject.Location = new Point(139, 69);
      comboBoxProject.MaxDropDownItems = 32;
      comboBoxProject.Name = "comboBoxProject";
      comboBoxProject.Size = new Size(195, 21);
      comboBoxProject.TabIndex = 2;
      datePickerTo.AlternateBackgroundColor = SystemColors.Control;
      datePickerTo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      datePickerTo.CustomFormat = "";
      datePickerTo.Location = new Point(139, 122);
      datePickerTo.Name = "datePickerTo";
      datePickerTo.Size = new Size(195, 20);
      datePickerTo.TabIndex = 4;
      datePickerTo.UseAlternateBackgroundColor = false;
      datePickerFrom.AlternateBackgroundColor = SystemColors.Control;
      datePickerFrom.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      datePickerFrom.CustomFormat = "";
      datePickerFrom.Location = new Point(139, 96);
      datePickerFrom.Name = "datePickerFrom";
      datePickerFrom.Size = new Size(195, 20);
      datePickerFrom.TabIndex = 3;
      datePickerFrom.UseAlternateBackgroundColor = false;
      listViewLabels.Columns.AddRange(new ColumnHeader[4]
      {
        columnHeader1,
        columnHeader2,
        columnHeader3,
        columnHeader18
      });
      listViewLabels.ContextMenuStrip = contextMenuStrip1;
      listViewLabels.Dock = DockStyle.Fill;
      listViewLabels.FullRowSelect = true;
      listViewLabels.GridLines = true;
      listViewLabels.HideSelection = false;
      listViewLabels.Location = new Point(0, 229);
      listViewLabels.Name = "listViewLabels";
      listViewLabels.Size = new Size(346, 280);
      listViewLabels.Sorted = false;
      listViewLabels.TabIndex = 3;
      listViewLabels.UseCompatibleStateImageBehavior = false;
      listViewLabels.View = View.Details;
      listViewLabels.SelectedIndexChanged += listViewLabels_SelectedIndexChanged;
      columnHeader1.Text = "Label";
      columnHeader1.Width = 140;
      columnHeader2.Text = "Owner";
      columnHeader2.Width = 100;
      columnHeader3.Text = "Scope";
      columnHeader3.Width = 100;
      columnHeader18.Text = "Comment";
      contextMenuStrip1.Items.AddRange(new ToolStripItem[2]
      {
        compareLabelWithLatestToolStripMenuItem,
        compareToolStripMenuItem
      });
      contextMenuStrip1.Name = "contextMenuStrip1";
      contextMenuStrip1.Size = new Size(216, 48);
      contextMenuStrip1.Opening += contextMenuStrip1_Opening;
      compareLabelWithLatestToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("compareLabelWithLatestToolStripMenuItem.Image");
      compareLabelWithLatestToolStripMenuItem.Name = "compareLabelWithLatestToolStripMenuItem";
      compareLabelWithLatestToolStripMenuItem.Size = new Size(215, 22);
      compareLabelWithLatestToolStripMenuItem.Text = "Compare Label With Latest...";
      compareLabelWithLatestToolStripMenuItem.Click += compareToolStripMenuItem_Click;
      compareToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("compareToolStripMenuItem.Image");
      compareToolStripMenuItem.Name = "compareToolStripMenuItem";
      compareToolStripMenuItem.Size = new Size(215, 22);
      compareToolStripMenuItem.Text = "Compare Two Labels...";
      compareToolStripMenuItem.Click += compareToolStripMenuItem_Click;
      splitContainer1.BorderStyle = BorderStyle.Fixed3D;
      splitContainer1.Dock = DockStyle.Fill;
      splitContainer1.FixedPanel = FixedPanel.Panel1;
      splitContainer1.Location = new Point(0, 0);
      splitContainer1.Name = "splitContainer1";
      splitContainer1.Panel1.Controls.Add(listViewLabels);
      splitContainer1.Panel1.Controls.Add(toolStrip2);
      splitContainer1.Panel1.Controls.Add(panelSearch);
      splitContainer1.Panel2.Controls.Add(tabControl1);
      splitContainer1.Panel2.Padding = new Padding(3, 3, 0, 0);
      splitContainer1.Size = new Size(731, 513);
      splitContainer1.SplitterDistance = 350;
      splitContainer1.SplitterWidth = 2;
      splitContainer1.TabIndex = 2;
      toolStrip2.AllowMerge = false;
      toolStrip2.CanOverflow = false;
      toolStrip2.GripMargin = new Padding(1);
      toolStrip2.GripStyle = ToolStripGripStyle.Hidden;
      toolStrip2.Items.AddRange(new ToolStripItem[6]
      {
        toolStripButtonDeleteLabel,
        toolStripSeparator1,
        toolStripButtonCompareLatest,
        toolStripButtonCompareTwo,
        toolStripSeparator4,
        toolStripButtonSaveToFileLabels
      });
      toolStrip2.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
      toolStrip2.Location = new Point(0, 204);
      toolStrip2.Name = "toolStrip2";
      toolStrip2.Padding = new Padding(3, 1, 1, 1);
      toolStrip2.RenderMode = ToolStripRenderMode.System;
      toolStrip2.Size = new Size(346, 25);
      toolStrip2.Stretch = true;
      toolStrip2.TabIndex = 4;
      toolStripButtonDeleteLabel.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonDeleteLabel.Image = (Image) componentResourceManager.GetObject("toolStripButtonDeleteLabel.Image");
      toolStripButtonDeleteLabel.ImageTransparentColor = Color.Magenta;
      toolStripButtonDeleteLabel.Name = "toolStripButtonDeleteLabel";
      toolStripButtonDeleteLabel.Size = new Size(23, 20);
      toolStripButtonDeleteLabel.Text = "toolStripButton1";
      toolStripButtonDeleteLabel.ToolTipText = "Delete Label(s)";
      toolStripButtonDeleteLabel.Click += toolStripButtonDeleteLabel_Click;
      toolStripSeparator1.Name = "toolStripSeparator1";
      toolStripSeparator1.Size = new Size(6, 23);
      toolStripButtonCompareLatest.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonCompareLatest.Image = (Image) componentResourceManager.GetObject("toolStripButtonCompareLatest.Image");
      toolStripButtonCompareLatest.ImageTransparentColor = Color.Magenta;
      toolStripButtonCompareLatest.Name = "toolStripButtonCompareLatest";
      toolStripButtonCompareLatest.Size = new Size(23, 20);
      toolStripButtonCompareLatest.Text = "toolStripButton1";
      toolStripButtonCompareLatest.ToolTipText = "Compare selected label items with latest version items";
      toolStripButtonCompareLatest.Click += compareToolStripMenuItem_Click;
      toolStripButtonCompareTwo.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonCompareTwo.Image = (Image) componentResourceManager.GetObject("toolStripButtonCompareTwo.Image");
      toolStripButtonCompareTwo.ImageTransparentColor = Color.Magenta;
      toolStripButtonCompareTwo.Name = "toolStripButtonCompareTwo";
      toolStripButtonCompareTwo.Size = new Size(23, 20);
      toolStripButtonCompareTwo.Text = "toolStripButton2";
      toolStripButtonCompareTwo.ToolTipText = "Compare items from two selected labels";
      toolStripButtonCompareTwo.Click += compareToolStripMenuItem_Click;
      toolStripSeparator4.Name = "toolStripSeparator4";
      toolStripSeparator4.Size = new Size(6, 23);
      toolStripButtonSaveToFileLabels.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonSaveToFileLabels.Image = (Image) componentResourceManager.GetObject("toolStripButtonSaveToFileLabels.Image");
      toolStripButtonSaveToFileLabels.ImageTransparentColor = Color.Black;
      toolStripButtonSaveToFileLabels.Name = "toolStripButtonSaveToFileLabels";
      toolStripButtonSaveToFileLabels.Size = new Size(23, 20);
      toolStripButtonSaveToFileLabels.Text = "toolStripButton5";
      toolStripButtonSaveToFileLabels.ToolTipText = "Save list to file";
      toolStripButtonSaveToFileLabels.Click += toolStripButtonSaveToFileLabels_Click;
      tabControl1.Controls.Add(tabPageProperties);
      tabControl1.Controls.Add(tabPageChangesets);
      tabControl1.Controls.Add(tabPageItems);
      tabControl1.Dock = DockStyle.Fill;
      tabControl1.Location = new Point(3, 3);
      tabControl1.Name = "tabControl1";
      tabControl1.SelectedIndex = 0;
      tabControl1.Size = new Size(372, 506);
      tabControl1.TabIndex = 3;
      tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
      tabPageProperties.BackColor = SystemColors.Control;
      tabPageProperties.Controls.Add(groupBox3);
      tabPageProperties.Controls.Add(groupBox2);
      tabPageProperties.Location = new Point(4, 22);
      tabPageProperties.Name = "tabPageProperties";
      tabPageProperties.Padding = new Padding(3);
      tabPageProperties.Size = new Size(364, 480);
      tabPageProperties.TabIndex = 0;
      tabPageProperties.Text = "Properties";
      groupBox3.Controls.Add(listViewItems);
      groupBox3.Controls.Add(toolStrip1);
      groupBox3.Dock = DockStyle.Fill;
      groupBox3.Location = new Point(3, 218);
      groupBox3.Name = "groupBox3";
      groupBox3.Size = new Size(358, 259);
      groupBox3.TabIndex = 3;
      groupBox3.TabStop = false;
      groupBox3.Text = " Items";
      listViewItems.BorderStyle = BorderStyle.None;
      listViewItems.Columns.AddRange(new ColumnHeader[4]
      {
        columnItem,
        columnChangeset,
        columnCheckinDate,
        columnType
      });
      listViewItems.ContextMenuStrip = contextMenuStrip2;
      listViewItems.Dock = DockStyle.Fill;
      listViewItems.FullRowSelect = true;
      listViewItems.GridLines = true;
      listViewItems.HideSelection = false;
      listViewItems.Location = new Point(3, 41);
      listViewItems.Name = "listViewItems";
      listViewItems.ShowGroups = false;
      listViewItems.Size = new Size(352, 215);
      listViewItems.Sorted = false;
      listViewItems.TabIndex = 0;
      listViewItems.UseCompatibleStateImageBehavior = false;
      listViewItems.View = View.Details;
      listViewItems.SelectedIndexChanged += listViewItems_SelectedIndexChanged;
      columnItem.Text = "Item";
      columnItem.Width = 230;
      columnChangeset.Text = "Changeset";
      columnChangeset.Width = 80;
      columnCheckinDate.Text = "Checkin Date";
      columnCheckinDate.Width = 130;
      columnType.Text = "Item type";
      contextMenuStrip2.Items.AddRange(new ToolStripItem[2]
      {
        compareWithLatestToolStripMenuItem,
        viewChangesetDetailsToolStripMenuItem1
      });
      contextMenuStrip2.Name = "contextMenuStrip2";
      contextMenuStrip2.Size = new Size(188, 48);
      compareWithLatestToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("compareWithLatestToolStripMenuItem.Image");
      compareWithLatestToolStripMenuItem.Name = "compareWithLatestToolStripMenuItem";
      compareWithLatestToolStripMenuItem.Size = new Size(187, 22);
      compareWithLatestToolStripMenuItem.Text = "Compare With Latest...";
      compareWithLatestToolStripMenuItem.Click += compareWithLatestToolStripMenuItem_Click;
      viewChangesetDetailsToolStripMenuItem1.Image = (Image) componentResourceManager.GetObject("viewChangesetDetailsToolStripMenuItem1.Image");
      viewChangesetDetailsToolStripMenuItem1.Name = "viewChangesetDetailsToolStripMenuItem1";
      viewChangesetDetailsToolStripMenuItem1.Size = new Size(187, 22);
      viewChangesetDetailsToolStripMenuItem1.Text = "View Changeset Details";
      viewChangesetDetailsToolStripMenuItem1.Click += viewChangesetDetailsToolStripMenuItem1_Click;
      toolStrip1.AllowMerge = false;
      toolStrip1.CanOverflow = false;
      toolStrip1.GripMargin = new Padding(1);
      toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
      toolStrip1.Items.AddRange(new ToolStripItem[6]
      {
        toolStripButtonUnlabel,
        toolStripSeparator2,
        toolStripButtonCompareVersion,
        toolStripButtonViewChangesetDetails,
        toolStripSeparator3,
        toolStripButtonSaveToFile
      });
      toolStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
      toolStrip1.Location = new Point(3, 16);
      toolStrip1.Name = "toolStrip1";
      toolStrip1.Padding = new Padding(3, 1, 1, 1);
      toolStrip1.RenderMode = ToolStripRenderMode.System;
      toolStrip1.Size = new Size(352, 25);
      toolStrip1.Stretch = true;
      toolStrip1.TabIndex = 2;
      toolStripButtonUnlabel.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonUnlabel.Image = (Image) componentResourceManager.GetObject("toolStripButtonUnlabel.Image");
      toolStripButtonUnlabel.ImageTransparentColor = Color.Magenta;
      toolStripButtonUnlabel.Name = "toolStripButtonUnlabel";
      toolStripButtonUnlabel.Size = new Size(23, 20);
      toolStripButtonUnlabel.Text = "toolStripButton1";
      toolStripButtonUnlabel.ToolTipText = "Unlabel item";
      toolStripButtonUnlabel.Click += toolStripButtonUnlabel_Click_1;
      toolStripSeparator2.Name = "toolStripSeparator2";
      toolStripSeparator2.Size = new Size(6, 23);
      toolStripButtonCompareVersion.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonCompareVersion.Image = (Image) componentResourceManager.GetObject("toolStripButtonCompareVersion.Image");
      toolStripButtonCompareVersion.ImageTransparentColor = Color.Magenta;
      toolStripButtonCompareVersion.Name = "toolStripButtonCompareVersion";
      toolStripButtonCompareVersion.Size = new Size(23, 20);
      toolStripButtonCompareVersion.Text = "toolStripButton1";
      toolStripButtonCompareVersion.ToolTipText = "Compare item latest version with label version";
      toolStripButtonCompareVersion.Click += toolStripButtonCompareVersion_Click;
      toolStripButtonViewChangesetDetails.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonViewChangesetDetails.Image = (Image) componentResourceManager.GetObject("toolStripButtonViewChangesetDetails.Image");
      toolStripButtonViewChangesetDetails.ImageTransparentColor = Color.Magenta;
      toolStripButtonViewChangesetDetails.Name = "toolStripButtonViewChangesetDetails";
      toolStripButtonViewChangesetDetails.Size = new Size(23, 20);
      toolStripButtonViewChangesetDetails.Text = "View Changeset Details";
      toolStripButtonViewChangesetDetails.Click += toolStripButtonViewChangesetDetails_Click;
      toolStripSeparator3.Name = "toolStripSeparator3";
      toolStripSeparator3.Size = new Size(6, 23);
      toolStripButtonSaveToFile.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonSaveToFile.Image = (Image) componentResourceManager.GetObject("toolStripButtonSaveToFile.Image");
      toolStripButtonSaveToFile.ImageTransparentColor = Color.Black;
      toolStripButtonSaveToFile.Name = "toolStripButtonSaveToFile";
      toolStripButtonSaveToFile.Size = new Size(23, 20);
      toolStripButtonSaveToFile.Text = "toolStripButton5";
      toolStripButtonSaveToFile.ToolTipText = "Save list to file";
      toolStripButtonSaveToFile.Click += toolStripButtonSaveToFile_Click;
      groupBox2.Controls.Add(propertyGridLabel);
      groupBox2.Dock = DockStyle.Top;
      groupBox2.Location = new Point(3, 3);
      groupBox2.Name = "groupBox2";
      groupBox2.Size = new Size(358, 215);
      groupBox2.TabIndex = 2;
      groupBox2.TabStop = false;
      groupBox2.Text = " Label properties";
      propertyGridLabel.Dock = DockStyle.Fill;
      propertyGridLabel.Location = new Point(3, 16);
      propertyGridLabel.Name = "propertyGridLabel";
      propertyGridLabel.Size = new Size(352, 196);
      propertyGridLabel.TabIndex = 0;
      propertyGridLabel.ToolbarVisible = false;
      tabPageChangesets.BackColor = SystemColors.Control;
      tabPageChangesets.Controls.Add(listViewChangesets);
      tabPageChangesets.Controls.Add(toolStrip4);
      tabPageChangesets.Location = new Point(4, 22);
      tabPageChangesets.Name = "tabPageChangesets";
      tabPageChangesets.Padding = new Padding(3);
      tabPageChangesets.Size = new Size(364, 480);
      tabPageChangesets.TabIndex = 2;
      tabPageChangesets.Text = "Changesets";
      listViewChangesets.BorderStyle = BorderStyle.None;
      listViewChangesets.Columns.AddRange(new ColumnHeader[4]
      {
        columnHeader12,
        columnHeader13,
        columnHeader14,
        columnHeader15
      });
      listViewChangesets.ContextMenuStrip = contextMenuStripChangesets;
      listViewChangesets.Dock = DockStyle.Fill;
      listViewChangesets.FullRowSelect = true;
      listViewChangesets.GridLines = true;
      listViewChangesets.HideSelection = false;
      listViewChangesets.Location = new Point(3, 28);
      listViewChangesets.Name = "listViewChangesets";
      listViewChangesets.ShowGroups = false;
      listViewChangesets.Size = new Size(358, 449);
      listViewChangesets.Sorted = false;
      listViewChangesets.TabIndex = 16;
      listViewChangesets.UseCompatibleStateImageBehavior = false;
      listViewChangesets.View = View.Details;
      listViewChangesets.SelectedIndexChanged += listViewChangesets_SelectedIndexChanged;
      columnHeader12.Text = "ID";
      columnHeader13.Text = "Owner";
      columnHeader13.Width = 100;
      columnHeader14.Text = "Creation Date";
      columnHeader14.Width = 130;
      columnHeader15.Text = "Comment";
      columnHeader15.Width = 100;
      contextMenuStripChangesets.Items.AddRange(new ToolStripItem[1]
      {
        viewChangesetDetailsToolStripMenuItem
      });
      contextMenuStripChangesets.Name = "contextMenuStripChangesets";
      contextMenuStripChangesets.Size = new Size(187, 26);
      contextMenuStripChangesets.Opening += contextMenuStripChangesets_Opening;
      viewChangesetDetailsToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("viewChangesetDetailsToolStripMenuItem.Image");
      viewChangesetDetailsToolStripMenuItem.Name = "viewChangesetDetailsToolStripMenuItem";
      viewChangesetDetailsToolStripMenuItem.Size = new Size(186, 22);
      viewChangesetDetailsToolStripMenuItem.Text = "View Changeset Details";
      viewChangesetDetailsToolStripMenuItem.Click += viewChangesetDetailsToolStripMenuItem_Click;
      toolStrip4.AllowMerge = false;
      toolStrip4.CanOverflow = false;
      toolStrip4.GripMargin = new Padding(1);
      toolStrip4.GripStyle = ToolStripGripStyle.Hidden;
      toolStrip4.Items.AddRange(new ToolStripItem[2]
      {
        toolStripButtonViewChangesetDetails1,
        toolStripButtonSaveChangesets
      });
      toolStrip4.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
      toolStrip4.Location = new Point(3, 3);
      toolStrip4.Name = "toolStrip4";
      toolStrip4.Padding = new Padding(3, 1, 1, 1);
      toolStrip4.RenderMode = ToolStripRenderMode.System;
      toolStrip4.Size = new Size(358, 25);
      toolStrip4.Stretch = true;
      toolStrip4.TabIndex = 15;
      toolStripButtonViewChangesetDetails1.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonViewChangesetDetails1.Enabled = false;
      toolStripButtonViewChangesetDetails1.Image = (Image) componentResourceManager.GetObject("toolStripButtonViewChangesetDetails1.Image");
      toolStripButtonViewChangesetDetails1.ImageTransparentColor = Color.Magenta;
      toolStripButtonViewChangesetDetails1.Name = "toolStripButtonViewChangesetDetails1";
      toolStripButtonViewChangesetDetails1.Size = new Size(23, 20);
      toolStripButtonViewChangesetDetails1.Text = "View Changeset Details";
      toolStripButtonViewChangesetDetails1.Click += toolStripButtonViewChangesetDetails1_Click;
      toolStripButtonSaveChangesets.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonSaveChangesets.Image = (Image) componentResourceManager.GetObject("toolStripButtonSaveChangesets.Image");
      toolStripButtonSaveChangesets.ImageTransparentColor = Color.Black;
      toolStripButtonSaveChangesets.Name = "toolStripButtonSaveChangesets";
      toolStripButtonSaveChangesets.Size = new Size(23, 20);
      toolStripButtonSaveChangesets.Text = "toolStripButton5";
      toolStripButtonSaveChangesets.ToolTipText = "Save list to file";
      toolStripButtonSaveChangesets.Click += toolStripButtonSaveChangesets_Click;
      tabPageItems.BackColor = SystemColors.Control;
      tabPageItems.Controls.Add(listViewWorkItems);
      tabPageItems.Controls.Add(toolStrip3);
      tabPageItems.Location = new Point(4, 22);
      tabPageItems.Name = "tabPageItems";
      tabPageItems.Padding = new Padding(3);
      tabPageItems.Size = new Size(364, 480);
      tabPageItems.TabIndex = 1;
      tabPageItems.Text = "Work Items";
      listViewWorkItems.BorderStyle = BorderStyle.None;
      listViewWorkItems.Columns.AddRange(new ColumnHeader[6]
      {
        columnHeader4,
        columnHeader5,
        columnHeader6,
        columnHeader7,
        columnHeader16,
        columnHeader17
      });
      listViewWorkItems.Dock = DockStyle.Fill;
      listViewWorkItems.FullRowSelect = true;
      listViewWorkItems.GridLines = true;
      listViewWorkItems.HideSelection = false;
      listViewWorkItems.Location = new Point(3, 28);
      listViewWorkItems.Name = "listViewWorkItems";
      listViewWorkItems.ShowGroups = false;
      listViewWorkItems.Size = new Size(358, 449);
      listViewWorkItems.Sorted = false;
      listViewWorkItems.TabIndex = 1;
      listViewWorkItems.UseCompatibleStateImageBehavior = false;
      listViewWorkItems.View = View.Details;
      columnHeader4.Text = "ID";
      columnHeader5.Text = "Title";
      columnHeader5.Width = 200;
      columnHeader6.Text = "Type";
      columnHeader6.Width = 80;
      columnHeader7.Text = "State";
      columnHeader16.Text = "Modified By";
      columnHeader16.Width = 80;
      columnHeader17.Text = "Modified Date";
      columnHeader17.Width = 130;
      toolStrip3.AllowMerge = false;
      toolStrip3.CanOverflow = false;
      toolStrip3.GripMargin = new Padding(1);
      toolStrip3.GripStyle = ToolStripGripStyle.Hidden;
      toolStrip3.Items.AddRange(new ToolStripItem[1]
      {
        toolStripButtonSaveWorkItems
      });
      toolStrip3.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
      toolStrip3.Location = new Point(3, 3);
      toolStrip3.Name = "toolStrip3";
      toolStrip3.Padding = new Padding(3, 1, 1, 1);
      toolStrip3.RenderMode = ToolStripRenderMode.System;
      toolStrip3.Size = new Size(358, 25);
      toolStrip3.Stretch = true;
      toolStrip3.TabIndex = 3;
      toolStripButtonSaveWorkItems.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonSaveWorkItems.Image = (Image) componentResourceManager.GetObject("toolStripButtonSaveWorkItems.Image");
      toolStripButtonSaveWorkItems.ImageTransparentColor = Color.Black;
      toolStripButtonSaveWorkItems.Name = "toolStripButtonSaveWorkItems";
      toolStripButtonSaveWorkItems.Size = new Size(23, 20);
      toolStripButtonSaveWorkItems.Text = "toolStripButton5";
      toolStripButtonSaveWorkItems.ToolTipText = "Save list to file";
      toolStripButtonSaveWorkItems.Click += toolStripButtonSaveWorkItems_Click;
      columnHeader8.Text = "Item";
      columnHeader8.Width = 220;
      columnHeader9.Text = "Changeset";
      columnHeader9.Width = 80;
      columnHeader10.Text = "Checkin Date";
      columnHeader10.Width = 80;
      columnHeader11.Text = "Item type";
      toolTip1.AutomaticDelay = 250;
      toolTip1.ToolTipIcon = ToolTipIcon.Info;
      toolTip1.ToolTipTitle = "Search string format";
      AutoScaleDimensions = new SizeF(6f, 13f);
      AutoScaleMode = AutoScaleMode.Font;
      Controls.Add(splitContainer1);
      Name = nameof (LabelViewControl);
      Size = new Size(731, 513);
      panelSearch.ResumeLayout(false);
      groupBox1.ResumeLayout(false);
      groupBox1.PerformLayout();
      contextMenuStrip1.ResumeLayout(false);
      splitContainer1.Panel1.ResumeLayout(false);
      splitContainer1.Panel1.PerformLayout();
      splitContainer1.Panel2.ResumeLayout(false);
      splitContainer1.ResumeLayout(false);
      toolStrip2.ResumeLayout(false);
      toolStrip2.PerformLayout();
      tabControl1.ResumeLayout(false);
      tabPageProperties.ResumeLayout(false);
      groupBox3.ResumeLayout(false);
      groupBox3.PerformLayout();
      contextMenuStrip2.ResumeLayout(false);
      toolStrip1.ResumeLayout(false);
      toolStrip1.PerformLayout();
      groupBox2.ResumeLayout(false);
      tabPageChangesets.ResumeLayout(false);
      tabPageChangesets.PerformLayout();
      contextMenuStripChangesets.ResumeLayout(false);
      toolStrip4.ResumeLayout(false);
      toolStrip4.PerformLayout();
      tabPageItems.ResumeLayout(false);
      tabPageItems.PerformLayout();
      toolStrip3.ResumeLayout(false);
      toolStrip3.PerformLayout();
      ResumeLayout(false);
    }

    public override void Initialize(TfsController controller)
    {
      _controller = new LabelViewController(controller);
      _dtUsers = _controller.Users.UsersTable.Copy();
      ListTable.AddAllRow(_dtUsers);
      _dtProjects = _controller.GetProjects(false);
      ListTable.AddAllRow(_dtProjects);
      ClearLabelsList();
      ClearLabelProperties();
      LoadSearchParameters();
      DefaultSearchParameters();
    }

    public override void LoadUsers(TfsController controller)
    {
      var text = comboBoxUserName.Text;
      _dtUsers = controller.Users.UsersTable.Copy();
      ListTable.AddAllRow(_dtUsers);
      ListTable.LoadTable(comboBoxUserName, _dtUsers, _controller.UserFullName);
      if (string.IsNullOrEmpty(text))
        return;
      comboBoxUserName.SelectedValue = text;
    }

    private void LoadSearchParameters()
    {
      ListTable.LoadTable(comboBoxUserName, _dtUsers, _controller.UserFullName);
      ListTable.LoadTable(comboBoxProject, _dtProjects, "");
    }

    private void DefaultSearchParameters()
    {
      textBoxLabelName.Text = "";
      comboBoxUserName.SelectedValue = _controller.UserFullName;
      datePickerTo.Value = DateTime.Now;
      datePickerFrom.Value = DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0));
    }

    private SearchParameters GetSearchParameters()
    {
      var userName = comboBoxUserName.SelectedValue == null ? comboBoxUserName.Text : comboBoxUserName.SelectedValue.ToString();
      var str = comboBoxProject.SelectedValue == null ? comboBoxProject.Text : comboBoxProject.SelectedValue.ToString();
      if (userName == ListTable.cAllID || userName == "")
        userName = null;
      if (str == ListTable.cAllID || str == "")
        str = null;
      var dateTime1 = datePickerFrom.Value;
      var dateTime2 = datePickerTo.Value;
      return new SearchParameters(textBoxLabelName.TextLength == 0 ? null : textBoxLabelName.Text, userName, null, "$/" + str, new DateTime(dateTime1.Year, dateTime1.Month, dateTime1.Day), new DateTime(dateTime2.Year, dateTime2.Month, dateTime2.Day, 23, 59, 59));
    }

    private void ClearLabelsList()
    {
      listViewLabels.ClearItems();
      toolStripButtonDeleteLabel.Enabled = false;
    }

    private void ClearLabelProperties()
    {
      toolStripButtonUnlabel.Enabled = false;
      toolStripButtonCompareVersion.Enabled = false;
      compareWithLatestToolStripMenuItem.Enabled = false;
      viewChangesetDetailsToolStripMenuItem1.Enabled = false;
      toolStripButtonViewChangesetDetails.Enabled = false;
      toolStripButtonViewChangesetDetails1.Enabled = false;
      propertyGridLabel.SelectedObject = null;
      listViewItems.ClearItems();
      listViewChangesets.ClearItems();
      listViewWorkItems.ClearItems();
    }

    private void SearchLabels()
    {
      listViewLabels.BeginUpdate();
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        ClearLabelsList();
        ClearLabelProperties();
        var versionControlLabelArray = _controller.SearchLabels(GetSearchParameters());
        listViewChangesets.SetCapacity(versionControlLabelArray.Length);
        foreach (var versionControlLabel in versionControlLabelArray)
          listViewLabels.AddItem(new ListViewItem(versionControlLabel.Name)
          {
            SubItems = {
              Utilities.GetTableValueByID(_dtUsers, versionControlLabel.OwnerName),
              versionControlLabel.Scope,
              versionControlLabel.Comment.Length == 0 ? " " : versionControlLabel.Comment
            },
            Tag = versionControlLabel
          });
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to retrieve labels." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        listViewLabels.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
    }

    private VersionControlLabel DisplayProperties(VersionControlLabel label)
    {
      listViewItems.BeginUpdate();
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        toolStripButtonDeleteLabel.Enabled = true;
        propertyGridLabel.SelectedObject = new LabelProperties(label, Utilities.GetTableValueByID(_dtUsers, label.OwnerName));
        if (label.Items.Length == 0)
          label = _controller.GetLabelItems(label);
        listViewItems.SetCapacity(label.Items.Length);
        for (var index = 0; index < label.Items.Length; ++index)
        {
          var obj = label.Items[index];
          listViewItems.AddItem(new ListViewItem(obj.ServerItem)
          {
            SubItems = {
              obj.ChangesetId.ToString(),
              obj.CheckinDate.ToString(),
              obj.ItemType.ToString()
            },
            Tag = obj
          });
        }
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to retrieve items." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        listViewItems.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
      return label;
    }

    private VersionControlLabel DisplayChangesets(VersionControlLabel label)
    {
      if (listViewChangesets.Items.Count > 0)
        return label;
      listViewChangesets.BeginUpdate();
      listViewWorkItems.BeginUpdate();
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        if (label.Items.Length == 0)
          label = _controller.GetLabelItems(label);
        var labelChangesets = _controller.GetLabelChangesets(label);
        var labelWorkItems = _controller.GetLabelWorkItems(labelChangesets);
        listViewChangesets.SetCapacity(labelChangesets.Values.Count);
        foreach (var changeset in labelChangesets.Values)
          listViewChangesets.AddItem(new ListViewItem(changeset.ChangesetId.ToString())
          {
            SubItems = {
              Utilities.GetTableValueByID(_dtUsers, changeset.Owner),
              changeset.CreationDate.ToString(),
              string.IsNullOrEmpty(changeset.Comment) ? " " : changeset.Comment
            },
            Tag = changeset
          });
        listViewWorkItems.SetCapacity(labelWorkItems.Values.Count);
        foreach (var workItem in labelWorkItems.Values)
          listViewWorkItems.AddItem(new ListViewItem(workItem.Id.ToString())
          {
            SubItems = {
              workItem.Title,
              workItem.Type.Name,
              workItem.State,
              Utilities.GetTableValueByID(_dtUsers, workItem.ChangedBy),
              workItem.ChangedDate.ToString()
            }
          });
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to retrieve changeset data." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        listViewWorkItems.EndUpdate();
        listViewChangesets.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
      return label;
    }

    private void CompareLabels()
    {
      if (listViewLabels.SelectedIndices.Count != 2 && listViewLabels.SelectedIndices.Count != 1)
        return;
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        var listViewItem1 = listViewLabels.Items[listViewLabels.SelectedIndices[0]];
        var versionControlLabel1 = listViewItem1.Tag as VersionControlLabel;
        VersionControlLabel versionControlLabel2 = null;
        if (versionControlLabel1.Items.Length == 0)
        {
          versionControlLabel1 = _controller.GetLabelItems(versionControlLabel1);
          listViewItem1.Tag = versionControlLabel1;
        }
        if (listViewLabels.SelectedIndices.Count == 2)
        {
          var listViewItem2 = listViewLabels.Items[listViewLabels.SelectedIndices[1]];
          versionControlLabel2 = listViewItem2.Tag as VersionControlLabel;
          if (versionControlLabel2.Items.Length == 0)
          {
            versionControlLabel2 = _controller.GetLabelItems(versionControlLabel2);
            listViewItem2.Tag = versionControlLabel2;
          }
        }
        var formCompareLabels = new FormCompareLabels();
        formCompareLabels.SetLabels(_controller, versionControlLabel1, versionControlLabel2);
        Cursor.Current = Cursors.Default;
        var num = (int) formCompareLabels.ShowDialog(this);
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to compare labels." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
    }

    private void ChangeTab()
    {
      toolStripButtonDeleteLabel.Enabled = listViewLabels.SelectedIndices.Count > 0;
      if (listViewLabels.SelectedIndices.Count != 1)
      {
        ClearLabelProperties();
        listViewWorkItems.ClearItems();
        listViewChangesets.ClearItems();
      }
      else
      {
        var selectedItem = listViewLabels.SelectedItem;
        var tag = selectedItem.Tag as VersionControlLabel;
        if (tabControl1.SelectedTab == tabPageProperties)
          selectedItem.Tag = DisplayProperties(tag);
        else
          selectedItem.Tag = DisplayChangesets(tag);
      }
    }

    private void CompareVersion()
    {
      if (listViewItems.SelectedIndices.Count != 1)
        return;
      Cursor.Current = Cursors.WaitCursor;
      try
      {
        var selectedItem = listViewItems.SelectedItem;
        Difference.VisualDiffFiles(_controller.VersionControl, selectedItem.Text, new ChangesetVersionSpec(selectedItem.SubItems[1].Text), selectedItem.Text, VersionSpec.Latest);
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to compare versions." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
    }

    private void SelectLabel()
    {
      toolStripButtonDeleteLabel.Enabled = listViewLabels.SelectedIndices.Count > 0;
      if (listViewLabels.SelectedIndices.Count == 0 || listViewLabels.SelectedIndices.Count > 2)
      {
        toolStripButtonCompareLatest.Enabled = false;
        toolStripButtonCompareTwo.Enabled = false;
        ClearLabelProperties();
      }
      else if (listViewLabels.SelectedIndices.Count > 1)
      {
        toolStripButtonCompareLatest.Enabled = false;
        toolStripButtonCompareTwo.Enabled = true;
        ClearLabelProperties();
      }
      else
      {
        toolStripButtonCompareLatest.Enabled = true;
        toolStripButtonCompareTwo.Enabled = false;
        var selectedItem = listViewLabels.SelectedItem;
        var tag = selectedItem.Tag as VersionControlLabel;
        listViewChangesets.ClearItems();
        listViewWorkItems.ClearItems();
        if (tabControl1.SelectedTab == tabPageProperties)
          selectedItem.Tag = DisplayProperties(tag);
        else if (tabControl1.SelectedTab == tabPageChangesets)
        {
          selectedItem.Tag = DisplayChangesets(tag);
        }
        else
        {
          if (tabControl1.SelectedTab != tabPageItems)
            return;
          selectedItem.Tag = DisplayChangesets(tag);
        }
      }
    }

    private void SelectItem()
    {
      toolStripButtonUnlabel.Enabled = listViewItems.SelectedIndices.Count > 0;
      if (listViewItems.SelectedIndices.Count == 1)
      {
        if ((listViewItems.SelectedItem.Tag as Item).ItemType == ItemType.Folder)
          toolStripButtonCompareVersion.Enabled = false;
        else
          toolStripButtonCompareVersion.Enabled = true;
        viewChangesetDetailsToolStripMenuItem1.Enabled = true;
        toolStripButtonViewChangesetDetails.Enabled = true;
      }
      else
      {
        toolStripButtonCompareVersion.Enabled = false;
        viewChangesetDetailsToolStripMenuItem1.Enabled = false;
        toolStripButtonViewChangesetDetails.Enabled = false;
      }
      compareWithLatestToolStripMenuItem.Enabled = toolStripButtonCompareVersion.Enabled;
    }

    private bool UnlabelItem()
    {
      if (listViewItems.SelectedIndices.Count == 0 || MessageBox.Show("Do you want to unlabel selected item(s)?", "Unlabel confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return false;
      var formResults = new FormResults();
      formResults.Initialize("Unlabel item");
      var selectedItem = listViewLabels.SelectedItem;
      var tag1 = selectedItem.Tag as VersionControlLabel;
      Cursor.Current = Cursors.WaitCursor;
      try
      {
        foreach (int selectedIndex in listViewItems.SelectedIndices)
        {
          var tag2 = listViewItems.Items[selectedIndex].Tag as Item;
          try
          {
            _controller.UnlabelItem(tag1, tag2);
            formResults.AddResult(true, "Item " + tag2.ServerItem, "Item unlabeled");
          }
          catch (Exception ex)
          {
            formResults.AddResult(false, "Item " + tag2.ServerItem, ex.Message);
          }
        }
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
      if (formResults.HasErrors)
      {
        var num1 = (int) formResults.ShowDialog(this);
      }
      try
      {
        var labelItems = _controller.GetLabelItems(tag1);
        if (labelItems == null)
        {
          SearchLabels();
          return true;
        }
        selectedItem.Tag = labelItems;
      }
      catch (Exception ex)
      {
        selectedItem.Selected = false;
        var num2 = (int) MessageBox.Show("Failed to refresh label." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      SelectLabel();
      return true;
    }

    private bool DeleteLabel()
    {
      if (listViewLabels.SelectedIndices.Count == 0 || MessageBox.Show("Do you want to delete selected label(s)?", "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return false;
      var formResults = new FormResults();
      formResults.Initialize("Delete label");
      Cursor.Current = Cursors.WaitCursor;
      try
      {
        foreach (int selectedIndex in listViewLabels.SelectedIndices)
        {
          var tag = listViewLabels.Items[selectedIndex].Tag as VersionControlLabel;
          try
          {
            _controller.DeleteLabel(tag);
            formResults.AddResult(true, "Label " + tag.Name, "Label deleted");
          }
          catch (Exception ex)
          {
            formResults.AddResult(false, "Label " + tag.Name, ex.Message);
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

    private void SaveItemsToFile()
    {
      Utilities.SaveListViewToFile(saveFileDialog, listViewItems,
          $"Labeled items for '{(object)(listViewLabels.SelectedItem.Tag as VersionControlLabel).Name}' as of {(object)DateTime.Now}");
    }

    private void SaveLabelsToFile()
    {
      Utilities.SaveListViewToFile(saveFileDialog, listViewLabels,
          $"Labels list as of {(object)DateTime.Now}");
    }

    private void SaveWorkItemsToFile()
    {
      Utilities.SaveListViewToFile(saveFileDialog, listViewWorkItems,
          $"Work items list for '{(object)(listViewLabels.SelectedItem.Tag as VersionControlLabel).Name}' as of {(object)DateTime.Now}");
    }

    private void SaveChangesetsToFile()
    {
      Utilities.SaveListViewToFile(saveFileDialog, listViewChangesets,
          $"Changeset list for '{(object)(listViewLabels.SelectedItem.Tag as VersionControlLabel).Name}' as of {(object)DateTime.Now}");
    }

    private void ViewChangesetDetails(Changeset changeset)
    {
      var changeset1 = changeset;
      if (changeset.Changes.Length == 0)
        changeset1 = _controller.GetChangesetDetails(changeset.ChangesetId);
      using (var changesetDetails = new DialogChangesetDetails(_controller.VersionControl, changeset1))
        changesetDetails.ShowDialog(this);
    }

    private void ViewSelectedChangesetDetails()
    {
      if (listViewChangesets.SelectedIndices.Count != 1)
        return;
      ViewChangesetDetails(listViewChangesets.Items[listViewChangesets.SelectedIndices[0]].Tag as Changeset);
    }

    private void ViewAssociatedChangesetDetails()
    {
      int result;
      if (listViewItems.SelectedIndices.Count != 1 || !int.TryParse(listViewItems.Items[listViewItems.SelectedIndices[0]].SubItems[1].Text, out result))
        return;
      ViewChangesetDetails(_controller.VersionControl.GetChangeset(result, true, false));
    }
  }
}
