// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.ShelvesetViewControl
// Assembly: Attrice.TeamFoundation.Controls.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=1a24c887ab53aa6e
// MVID: 393BE899-3C29-4A70-86D7-B4B965E5A8F5
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controls.12.dll

using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Attrice.TeamFoundation.Common;
using Attrice.TeamFoundation.Controllers;
using Attrice.TeamFoundation.Controls.Forms;
using Attrice.TeamFoundation.Controls.Properties;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace Attrice.TeamFoundation.Controls
{
  public class ShelvesetViewControl : BaseSidekickControl
  {
    private IContainer components;
    private SplitContainer splitContainer1;
    private VirtualListView listViewSets;
    private ColumnHeader columnHeader1;
    private ColumnHeader columnHeader2;
    private ColumnHeader columnHeader3;
    private ToolStrip toolStrip2;
    private ToolStripButton toolStripButtonDeleteSet;
    private Panel panelSearch;
    private Button buttonClear;
    private Button buttonSearch;
    private GroupBox groupBox1;
    private TextBox textBoxSetName;
    private Label label5;
    private Label label4;
    private Label label3;
    private Label label1;
    private ComboBox comboBoxUserName;
    private TeamFoundationDateTimePicker datePickerTo;
    private TeamFoundationDateTimePicker datePickerFrom;
    private TabControl tabControl1;
    private TabPage tabPageProperties;
    private GroupBox groupBox3;
    private VirtualListView listViewItems;
    private ColumnHeader columnItem;
    private ColumnHeader columnChangeset;
    private ColumnHeader columnChangeType;
    private ColumnHeader columnType;
    private ToolStrip toolStrip1;
    private ToolStripButton toolStripButtonViewItem;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripButton toolStripButtonCompareLatest;
    private GroupBox groupBox2;
    private PropertyGrid propertyGridSet;
    private TabPage tabPageChangesets;
    private SaveFileDialog saveFileDialog1;
    private ToolStripButton toolStripButtonSaveItem;
    private ToolStripButton toolStripButtonCompareWithOriginal;
    private SplitContainer splitContainer2;
    private GroupBox groupBox4;
    private ListView listViewNotes;
    private ColumnHeader columnHeaderNoteName;
    private ColumnHeader columnHeaderNoteValue;
    private GroupBox groupBox5;
    private ListView listViewWorkItems;
    private ColumnHeader columnHeader4;
    private ColumnHeader columnHeader5;
    private ColumnHeader columnHeader6;
    private ColumnHeader columnHeader7;
    private ColumnHeader columnHeader16;
    private ColumnHeader columnHeader17;
    private ColumnHeader columnHeader8;
    private ContextMenuStrip contextMenuStrip2;
    private ToolStripMenuItem toolStripMenuItemCompareLatest;
    private ToolStripMenuItem toolStripMenuItemCompareOriginal;
    private ToolStripMenuItem toolStripMenuItemSave;
    private ToolStripMenuItem toolStripMenuItemView;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripButton toolStripButtonSaveAll;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem toolStripMenuItem1;
    private ShelvesetViewController _controller;
    private DataTable _dtUsers;
    private DataTable _dtProjects;

    public ShelvesetViewControl()
    {
      InitializeComponent();
      listViewItems.Sorted = true;
      listViewSets.Sorted = true;
      listViewNotes.ListViewItemSorter = new CustomListSorter(ListSorterType.Date | ListSorterType.Integer);
      (listViewNotes.ListViewItemSorter as CustomListSorter).Ascending = true;
      listViewWorkItems.ListViewItemSorter = new CustomListSorter(ListSorterType.Date | ListSorterType.Integer);
      (listViewWorkItems.ListViewItemSorter as CustomListSorter).Ascending = true;
      splitContainer1.Panel1MinSize = 350;
      splitContainer1.Panel2MinSize = 350;
      panelSearch.BackColor = SystemColors.Control;
      splitContainer1.BackColor = SystemColors.Control;
      datePickerTo.CustomFormat = datePickerFrom.CustomFormat = "dd-MMM-yyyy";
      datePickerTo.Format = datePickerFrom.Format = DateTimePickerFormat.Custom;
      Name = "Shelveset Sidekick";
    }

    public override Image Image => Resources.ShelvesetsImage;

    private void buttonSearch_Click(object sender, EventArgs e) => SearchShelvesets();

    private void listViewSets_SelectedIndexChanged(object sender, EventArgs e) => SelectSet();

    private void toolStripButtonDeleteSet_Click(object sender, EventArgs e)
    {
      if (!DeleteShelveset())
        return;
      SearchShelvesets();
    }

    private void toolStripButtonViewItem_Click(object sender, EventArgs e)
    {
      DownloadItem(false);
    }

    private void listViewItems_SelectedIndexChanged(object sender, EventArgs e)
    {
      SelectItem();
    }

    private void toolStripButtonSaveItem_Click(object sender, EventArgs e)
    {
      DownloadItem(true);
    }

    private void toolStripButtonCompareLatest_Click(object sender, EventArgs e)
    {
      CompareVersion(true);
    }

    private void toolStripButtonCompareWithOriginal_Click(object sender, EventArgs e)
    {
      CompareVersion(false);
    }

    private void listViewSets_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      var listView = sender as ListView;
      (listView.ListViewItemSorter as CustomListSorter).SetColumn(listView.Columns[e.Column]);
      listView.Sort();
    }

    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) => ChangeTab();

    private void toolStripButtonSaveAll_Click(object sender, EventArgs e) => DownloadAll();

    private void toolStripMenuItem1_Click(object sender, EventArgs e) => DownloadAll();

    private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
    {
      e.Cancel = listViewSets.SelectedIndices.Count != 1;
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
      var componentResourceManager = new ComponentResourceManager(typeof (ShelvesetViewControl));
      splitContainer1 = new SplitContainer();
      listViewSets = new VirtualListView();
      columnHeader1 = new ColumnHeader();
      columnHeader2 = new ColumnHeader();
      columnHeader3 = new ColumnHeader();
      contextMenuStrip1 = new ContextMenuStrip(components);
      toolStripMenuItem1 = new ToolStripMenuItem();
      toolStrip2 = new ToolStrip();
      toolStripButtonDeleteSet = new ToolStripButton();
      toolStripSeparator3 = new ToolStripSeparator();
      toolStripButtonSaveAll = new ToolStripButton();
      panelSearch = new Panel();
      buttonClear = new Button();
      buttonSearch = new Button();
      groupBox1 = new GroupBox();
      textBoxSetName = new TextBox();
      label5 = new Label();
      label4 = new Label();
      label3 = new Label();
      label1 = new Label();
      comboBoxUserName = new ComboBox();
      datePickerTo = new TeamFoundationDateTimePicker();
      datePickerFrom = new TeamFoundationDateTimePicker();
      tabControl1 = new TabControl();
      tabPageProperties = new TabPage();
      groupBox3 = new GroupBox();
      listViewItems = new VirtualListView();
      columnItem = new ColumnHeader();
      columnChangeset = new ColumnHeader();
      columnChangeType = new ColumnHeader();
      columnType = new ColumnHeader();
      contextMenuStrip2 = new ContextMenuStrip(components);
      toolStripMenuItemSave = new ToolStripMenuItem();
      toolStripMenuItemView = new ToolStripMenuItem();
      toolStripSeparator1 = new ToolStripSeparator();
      toolStripMenuItemCompareLatest = new ToolStripMenuItem();
      toolStripMenuItemCompareOriginal = new ToolStripMenuItem();
      toolStrip1 = new ToolStrip();
      toolStripButtonSaveItem = new ToolStripButton();
      toolStripButtonViewItem = new ToolStripButton();
      toolStripSeparator2 = new ToolStripSeparator();
      toolStripButtonCompareLatest = new ToolStripButton();
      toolStripButtonCompareWithOriginal = new ToolStripButton();
      groupBox2 = new GroupBox();
      propertyGridSet = new PropertyGrid();
      tabPageChangesets = new TabPage();
      splitContainer2 = new SplitContainer();
      groupBox4 = new GroupBox();
      listViewNotes = new ListView();
      columnHeaderNoteName = new ColumnHeader();
      columnHeaderNoteValue = new ColumnHeader();
      groupBox5 = new GroupBox();
      listViewWorkItems = new ListView();
      columnHeader4 = new ColumnHeader();
      columnHeader5 = new ColumnHeader();
      columnHeader6 = new ColumnHeader();
      columnHeader7 = new ColumnHeader();
      columnHeader16 = new ColumnHeader();
      columnHeader17 = new ColumnHeader();
      columnHeader8 = new ColumnHeader();
      saveFileDialog1 = new SaveFileDialog();
      splitContainer1.Panel1.SuspendLayout();
      splitContainer1.Panel2.SuspendLayout();
      splitContainer1.SuspendLayout();
      contextMenuStrip1.SuspendLayout();
      toolStrip2.SuspendLayout();
      panelSearch.SuspendLayout();
      groupBox1.SuspendLayout();
      tabControl1.SuspendLayout();
      tabPageProperties.SuspendLayout();
      groupBox3.SuspendLayout();
      contextMenuStrip2.SuspendLayout();
      toolStrip1.SuspendLayout();
      groupBox2.SuspendLayout();
      tabPageChangesets.SuspendLayout();
      splitContainer2.Panel1.SuspendLayout();
      splitContainer2.Panel2.SuspendLayout();
      splitContainer2.SuspendLayout();
      groupBox4.SuspendLayout();
      groupBox5.SuspendLayout();
      SuspendLayout();
      splitContainer1.BorderStyle = BorderStyle.Fixed3D;
      splitContainer1.Dock = DockStyle.Fill;
      splitContainer1.FixedPanel = FixedPanel.Panel1;
      splitContainer1.Location = new Point(0, 0);
      splitContainer1.Name = "splitContainer1";
      splitContainer1.Panel1.Controls.Add(listViewSets);
      splitContainer1.Panel1.Controls.Add(toolStrip2);
      splitContainer1.Panel1.Controls.Add(panelSearch);
      splitContainer1.Panel2.Controls.Add(tabControl1);
      splitContainer1.Panel2.Padding = new Padding(3, 3, 0, 0);
      splitContainer1.Size = new Size(725, 522);
      splitContainer1.SplitterDistance = 350;
      splitContainer1.SplitterWidth = 2;
      splitContainer1.TabIndex = 3;
      listViewSets.Columns.AddRange(new ColumnHeader[3]
      {
        columnHeader1,
        columnHeader2,
        columnHeader3
      });
      listViewSets.ContextMenuStrip = contextMenuStrip1;
      listViewSets.Dock = DockStyle.Fill;
      listViewSets.FullRowSelect = true;
      listViewSets.GridLines = true;
      listViewSets.HideSelection = false;
      listViewSets.Location = new Point(0, 190);
      listViewSets.Name = "listViewSets";
      listViewSets.Size = new Size(346, 328);
      listViewSets.Sorted = false;
      listViewSets.TabIndex = 3;
      listViewSets.UseCompatibleStateImageBehavior = false;
      listViewSets.View = View.Details;
      listViewSets.SelectedIndexChanged += listViewSets_SelectedIndexChanged;
      columnHeader1.Text = "Name";
      columnHeader1.Width = 140;
      columnHeader2.Text = "Owner";
      columnHeader2.Width = 100;
      columnHeader3.Text = "Date";
      columnHeader3.Width = 100;
      contextMenuStrip1.Items.AddRange(new ToolStripItem[1]
      {
        toolStripMenuItem1
      });
      contextMenuStrip1.Name = "contextMenuStrip2";
      contextMenuStrip1.Size = new Size(157, 26);
      contextMenuStrip1.Opening += contextMenuStrip1_Opening;
      toolStripMenuItem1.Image = (Image) componentResourceManager.GetObject("toolStripMenuItem1.Image");
      toolStripMenuItem1.Name = "toolStripMenuItem1";
      toolStripMenuItem1.Size = new Size(156, 22);
      toolStripMenuItem1.Text = "Export Shelveset";
      toolStripMenuItem1.Click += toolStripMenuItem1_Click;
      toolStrip2.AllowMerge = false;
      toolStrip2.CanOverflow = false;
      toolStrip2.GripMargin = new Padding(1);
      toolStrip2.GripStyle = ToolStripGripStyle.Hidden;
      toolStrip2.Items.AddRange(new ToolStripItem[3]
      {
        toolStripButtonDeleteSet,
        toolStripSeparator3,
        toolStripButtonSaveAll
      });
      toolStrip2.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
      toolStrip2.Location = new Point(0, 165);
      toolStrip2.Name = "toolStrip2";
      toolStrip2.Padding = new Padding(3, 1, 1, 1);
      toolStrip2.RenderMode = ToolStripRenderMode.System;
      toolStrip2.Size = new Size(346, 25);
      toolStrip2.Stretch = true;
      toolStrip2.TabIndex = 4;
      toolStripButtonDeleteSet.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonDeleteSet.Image = (Image) componentResourceManager.GetObject("toolStripButtonDeleteSet.Image");
      toolStripButtonDeleteSet.ImageTransparentColor = Color.Magenta;
      toolStripButtonDeleteSet.Name = "toolStripButtonDeleteSet";
      toolStripButtonDeleteSet.Size = new Size(23, 20);
      toolStripButtonDeleteSet.Text = "toolStripButton1";
      toolStripButtonDeleteSet.ToolTipText = "Delete Shelveset";
      toolStripButtonDeleteSet.Click += toolStripButtonDeleteSet_Click;
      toolStripSeparator3.Name = "toolStripSeparator3";
      toolStripSeparator3.Size = new Size(6, 23);
      toolStripButtonSaveAll.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonSaveAll.Image = (Image) componentResourceManager.GetObject("toolStripButtonSaveAll.Image");
      toolStripButtonSaveAll.ImageTransparentColor = Color.Magenta;
      toolStripButtonSaveAll.Name = "toolStripButtonSaveAll";
      toolStripButtonSaveAll.Size = new Size(23, 20);
      toolStripButtonSaveAll.Text = "toolStripButton1";
      toolStripButtonSaveAll.ToolTipText = "Export Shelveset";
      toolStripButtonSaveAll.Click += toolStripButtonSaveAll_Click;
      panelSearch.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      panelSearch.Controls.Add(buttonClear);
      panelSearch.Controls.Add(buttonSearch);
      panelSearch.Controls.Add(groupBox1);
      panelSearch.Dock = DockStyle.Top;
      panelSearch.Location = new Point(0, 0);
      panelSearch.Margin = new Padding(0);
      panelSearch.Name = "panelSearch";
      panelSearch.Size = new Size(346, 165);
      panelSearch.TabIndex = 2;
      buttonClear.Location = new Point(85, 132);
      buttonClear.Name = "buttonClear";
      buttonClear.Size = new Size(75, 23);
      buttonClear.TabIndex = 2;
      buttonClear.Text = "Clear";
      buttonClear.UseVisualStyleBackColor = true;
      buttonSearch.Location = new Point(4, 132);
      buttonSearch.Name = "buttonSearch";
      buttonSearch.Size = new Size(75, 23);
      buttonSearch.TabIndex = 1;
      buttonSearch.Text = "Search";
      buttonSearch.UseVisualStyleBackColor = true;
      buttonSearch.Click += buttonSearch_Click;
      groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      groupBox1.Controls.Add(textBoxSetName);
      groupBox1.Controls.Add(label5);
      groupBox1.Controls.Add(label4);
      groupBox1.Controls.Add(label3);
      groupBox1.Controls.Add(label1);
      groupBox1.Controls.Add(comboBoxUserName);
      groupBox1.Controls.Add(datePickerTo);
      groupBox1.Controls.Add(datePickerFrom);
      groupBox1.Location = new Point(3, 3);
      groupBox1.Name = "groupBox1";
      groupBox1.Size = new Size(340, 123);
      groupBox1.TabIndex = 0;
      groupBox1.TabStop = false;
      groupBox1.Text = " Search criteria";
      textBoxSetName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      textBoxSetName.Location = new Point(139, 16);
      textBoxSetName.Name = "textBoxSetName";
      textBoxSetName.Size = new Size(195, 20);
      textBoxSetName.TabIndex = 17;
      label5.AutoSize = true;
      label5.Location = new Point(9, 21);
      label5.Name = "label5";
      label5.Size = new Size(83, 13);
      label5.TabIndex = 16;
      label5.Text = "Shelveset name";
      label4.AutoSize = true;
      label4.Location = new Point(8, 101);
      label4.Name = "label4";
      label4.Size = new Size(86, 13);
      label4.TabIndex = 15;
      label4.Text = "Change date (to)";
      label3.AutoSize = true;
      label3.Location = new Point(8, 74);
      label3.Name = "label3";
      label3.Size = new Size(97, 13);
      label3.TabIndex = 14;
      label3.Text = "Change date (from)";
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
      datePickerTo.AlternateBackgroundColor = SystemColors.Control;
      datePickerTo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      datePickerTo.CustomFormat = "";
      datePickerTo.Location = new Point(139, 95);
      datePickerTo.Name = "datePickerTo";
      datePickerTo.Size = new Size(195, 20);
      datePickerTo.TabIndex = 4;
      datePickerTo.UseAlternateBackgroundColor = false;
      datePickerFrom.AlternateBackgroundColor = SystemColors.Control;
      datePickerFrom.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      datePickerFrom.CustomFormat = "";
      datePickerFrom.Location = new Point(139, 69);
      datePickerFrom.Name = "datePickerFrom";
      datePickerFrom.Size = new Size(195, 20);
      datePickerFrom.TabIndex = 3;
      datePickerFrom.UseAlternateBackgroundColor = false;
      tabControl1.Controls.Add(tabPageProperties);
      tabControl1.Controls.Add(tabPageChangesets);
      tabControl1.Dock = DockStyle.Fill;
      tabControl1.Location = new Point(3, 3);
      tabControl1.Name = "tabControl1";
      tabControl1.SelectedIndex = 0;
      tabControl1.Size = new Size(366, 515);
      tabControl1.TabIndex = 3;
      tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
      tabPageProperties.BackColor = SystemColors.Control;
      tabPageProperties.Controls.Add(groupBox3);
      tabPageProperties.Controls.Add(groupBox2);
      tabPageProperties.Location = new Point(4, 22);
      tabPageProperties.Name = "tabPageProperties";
      tabPageProperties.Padding = new Padding(3);
      tabPageProperties.Size = new Size(358, 489);
      tabPageProperties.TabIndex = 0;
      tabPageProperties.Text = "Properties";
      groupBox3.Controls.Add(listViewItems);
      groupBox3.Controls.Add(toolStrip1);
      groupBox3.Dock = DockStyle.Fill;
      groupBox3.Location = new Point(3, 218);
      groupBox3.Name = "groupBox3";
      groupBox3.Size = new Size(352, 268);
      groupBox3.TabIndex = 3;
      groupBox3.TabStop = false;
      groupBox3.Text = " Shelved Items";
      listViewItems.BorderStyle = BorderStyle.None;
      listViewItems.Columns.AddRange(new ColumnHeader[4]
      {
        columnItem,
        columnChangeset,
        columnChangeType,
        columnType
      });
      listViewItems.ContextMenuStrip = contextMenuStrip2;
      listViewItems.Dock = DockStyle.Fill;
      listViewItems.FullRowSelect = true;
      listViewItems.GridLines = true;
      listViewItems.HideSelection = false;
      listViewItems.Location = new Point(3, 41);
      listViewItems.MultiSelect = false;
      listViewItems.Name = "listViewItems";
      listViewItems.ShowGroups = false;
      listViewItems.Size = new Size(346, 224);
      listViewItems.Sorted = false;
      listViewItems.TabIndex = 0;
      listViewItems.UseCompatibleStateImageBehavior = false;
      listViewItems.View = View.Details;
      listViewItems.SelectedIndexChanged += listViewItems_SelectedIndexChanged;
      columnItem.Text = "Server item";
      columnItem.Width = 230;
      columnChangeset.Text = "From version";
      columnChangeset.Width = 80;
      columnChangeType.Text = "Change type";
      columnChangeType.Width = 130;
      columnType.Text = "Item type";
      contextMenuStrip2.Items.AddRange(new ToolStripItem[5]
      {
        toolStripMenuItemSave,
        toolStripMenuItemView,
        toolStripSeparator1,
        toolStripMenuItemCompareLatest,
        toolStripMenuItemCompareOriginal
      });
      contextMenuStrip2.Name = "contextMenuStrip2";
      contextMenuStrip2.Size = new Size(194, 98);
      toolStripMenuItemSave.Image = (Image) componentResourceManager.GetObject("toolStripMenuItemSave.Image");
      toolStripMenuItemSave.Name = "toolStripMenuItemSave";
      toolStripMenuItemSave.Size = new Size(193, 22);
      toolStripMenuItemSave.Text = "Save";
      toolStripMenuItemSave.Click += toolStripButtonSaveItem_Click;
      toolStripMenuItemView.Image = (Image) componentResourceManager.GetObject("toolStripMenuItemView.Image");
      toolStripMenuItemView.Name = "toolStripMenuItemView";
      toolStripMenuItemView.Size = new Size(193, 22);
      toolStripMenuItemView.Text = "View";
      toolStripMenuItemView.Click += toolStripButtonViewItem_Click;
      toolStripSeparator1.Name = "toolStripSeparator1";
      toolStripSeparator1.Size = new Size(190, 6);
      toolStripMenuItemCompareLatest.Image = (Image) componentResourceManager.GetObject("toolStripMenuItemCompareLatest.Image");
      toolStripMenuItemCompareLatest.Name = "toolStripMenuItemCompareLatest";
      toolStripMenuItemCompareLatest.Size = new Size(193, 22);
      toolStripMenuItemCompareLatest.Text = "Compare With Latest...";
      toolStripMenuItemCompareLatest.Click += toolStripButtonCompareLatest_Click;
      toolStripMenuItemCompareOriginal.Image = (Image) componentResourceManager.GetObject("toolStripMenuItemCompareOriginal.Image");
      toolStripMenuItemCompareOriginal.Name = "toolStripMenuItemCompareOriginal";
      toolStripMenuItemCompareOriginal.Size = new Size(193, 22);
      toolStripMenuItemCompareOriginal.Text = "Compare With Original...";
      toolStripMenuItemCompareOriginal.Click += toolStripButtonCompareWithOriginal_Click;
      toolStrip1.AllowMerge = false;
      toolStrip1.CanOverflow = false;
      toolStrip1.GripMargin = new Padding(1);
      toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
      toolStrip1.Items.AddRange(new ToolStripItem[5]
      {
        toolStripButtonSaveItem,
        toolStripButtonViewItem,
        toolStripSeparator2,
        toolStripButtonCompareLatest,
        toolStripButtonCompareWithOriginal
      });
      toolStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
      toolStrip1.Location = new Point(3, 16);
      toolStrip1.Name = "toolStrip1";
      toolStrip1.Padding = new Padding(3, 1, 1, 1);
      toolStrip1.RenderMode = ToolStripRenderMode.System;
      toolStrip1.Size = new Size(346, 25);
      toolStrip1.Stretch = true;
      toolStrip1.TabIndex = 2;
      toolStripButtonSaveItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonSaveItem.Image = (Image) componentResourceManager.GetObject("toolStripButtonSaveItem.Image");
      toolStripButtonSaveItem.ImageTransparentColor = Color.Magenta;
      toolStripButtonSaveItem.Name = "toolStripButtonSaveItem";
      toolStripButtonSaveItem.Size = new Size(23, 20);
      toolStripButtonSaveItem.Text = "toolStripButton1";
      toolStripButtonSaveItem.ToolTipText = "Save Item";
      toolStripButtonSaveItem.Click += toolStripButtonSaveItem_Click;
      toolStripButtonViewItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonViewItem.Image = (Image) componentResourceManager.GetObject("toolStripButtonViewItem.Image");
      toolStripButtonViewItem.ImageTransparentColor = Color.Magenta;
      toolStripButtonViewItem.Name = "toolStripButtonViewItem";
      toolStripButtonViewItem.Size = new Size(23, 20);
      toolStripButtonViewItem.Text = "toolStripButton1";
      toolStripButtonViewItem.ToolTipText = "View Item";
      toolStripButtonViewItem.Click += toolStripButtonViewItem_Click;
      toolStripSeparator2.Name = "toolStripSeparator2";
      toolStripSeparator2.Size = new Size(6, 23);
      toolStripButtonCompareLatest.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonCompareLatest.Image = (Image) componentResourceManager.GetObject("toolStripButtonCompareLatest.Image");
      toolStripButtonCompareLatest.ImageTransparentColor = Color.Magenta;
      toolStripButtonCompareLatest.Name = "toolStripButtonCompareLatest";
      toolStripButtonCompareLatest.Size = new Size(23, 20);
      toolStripButtonCompareLatest.Text = "toolStripButton1";
      toolStripButtonCompareLatest.ToolTipText = "Compare with latest version";
      toolStripButtonCompareLatest.Click += toolStripButtonCompareLatest_Click;
      toolStripButtonCompareWithOriginal.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonCompareWithOriginal.Image = (Image) componentResourceManager.GetObject("toolStripButtonCompareWithOriginal.Image");
      toolStripButtonCompareWithOriginal.ImageTransparentColor = Color.Magenta;
      toolStripButtonCompareWithOriginal.Name = "toolStripButtonCompareWithOriginal";
      toolStripButtonCompareWithOriginal.Size = new Size(23, 20);
      toolStripButtonCompareWithOriginal.Text = "toolStripButton2";
      toolStripButtonCompareWithOriginal.ToolTipText = "Compare with original version";
      toolStripButtonCompareWithOriginal.Click += toolStripButtonCompareWithOriginal_Click;
      groupBox2.Controls.Add(propertyGridSet);
      groupBox2.Dock = DockStyle.Top;
      groupBox2.Location = new Point(3, 3);
      groupBox2.Name = "groupBox2";
      groupBox2.Size = new Size(352, 215);
      groupBox2.TabIndex = 2;
      groupBox2.TabStop = false;
      groupBox2.Text = " Shelveset properties";
      propertyGridSet.Dock = DockStyle.Fill;
      propertyGridSet.Location = new Point(3, 16);
      propertyGridSet.Name = "propertyGridSet";
      propertyGridSet.Size = new Size(346, 196);
      propertyGridSet.TabIndex = 0;
      propertyGridSet.ToolbarVisible = false;
      tabPageChangesets.BackColor = SystemColors.Control;
      tabPageChangesets.Controls.Add(splitContainer2);
      tabPageChangesets.Location = new Point(4, 22);
      tabPageChangesets.Name = "tabPageChangesets";
      tabPageChangesets.Padding = new Padding(3);
      tabPageChangesets.Size = new Size(358, 489);
      tabPageChangesets.TabIndex = 2;
      tabPageChangesets.Text = "Checkin Properties";
      splitContainer2.Dock = DockStyle.Fill;
      splitContainer2.Location = new Point(3, 3);
      splitContainer2.Name = "splitContainer2";
      splitContainer2.Orientation = Orientation.Horizontal;
      splitContainer2.Panel1.Controls.Add(groupBox4);
      splitContainer2.Panel2.Controls.Add(groupBox5);
      splitContainer2.Size = new Size(352, 483);
      splitContainer2.SplitterDistance = 241;
      splitContainer2.TabIndex = 4;
      groupBox4.Controls.Add(listViewNotes);
      groupBox4.Dock = DockStyle.Fill;
      groupBox4.Location = new Point(0, 0);
      groupBox4.Name = "groupBox4";
      groupBox4.Size = new Size(352, 241);
      groupBox4.TabIndex = 4;
      groupBox4.TabStop = false;
      groupBox4.Text = "Checkin Notes ";
      listViewNotes.BorderStyle = BorderStyle.None;
      listViewNotes.Columns.AddRange(new ColumnHeader[2]
      {
        columnHeaderNoteName,
        columnHeaderNoteValue
      });
      listViewNotes.Dock = DockStyle.Fill;
      listViewNotes.FullRowSelect = true;
      listViewNotes.GridLines = true;
      listViewNotes.HideSelection = false;
      listViewNotes.Location = new Point(3, 16);
      listViewNotes.Name = "listViewNotes";
      listViewNotes.ShowGroups = false;
      listViewNotes.Size = new Size(346, 222);
      listViewNotes.TabIndex = 4;
      listViewNotes.UseCompatibleStateImageBehavior = false;
      listViewNotes.View = View.Details;
      listViewNotes.ColumnClick += listViewSets_ColumnClick;
      columnHeaderNoteName.Text = "Note name";
      columnHeaderNoteName.Width = 120;
      columnHeaderNoteValue.Text = "Value";
      columnHeaderNoteValue.Width = 250;
      groupBox5.Controls.Add(listViewWorkItems);
      groupBox5.Dock = DockStyle.Fill;
      groupBox5.Location = new Point(0, 0);
      groupBox5.Name = "groupBox5";
      groupBox5.Size = new Size(352, 238);
      groupBox5.TabIndex = 5;
      groupBox5.TabStop = false;
      groupBox5.Text = " Work Items ";
      listViewWorkItems.BorderStyle = BorderStyle.None;
      listViewWorkItems.Columns.AddRange(new ColumnHeader[7]
      {
        columnHeader4,
        columnHeader5,
        columnHeader6,
        columnHeader7,
        columnHeader16,
        columnHeader17,
        columnHeader8
      });
      listViewWorkItems.Dock = DockStyle.Fill;
      listViewWorkItems.FullRowSelect = true;
      listViewWorkItems.GridLines = true;
      listViewWorkItems.HideSelection = false;
      listViewWorkItems.Location = new Point(3, 16);
      listViewWorkItems.Name = "listViewWorkItems";
      listViewWorkItems.ShowGroups = false;
      listViewWorkItems.Size = new Size(346, 219);
      listViewWorkItems.TabIndex = 5;
      listViewWorkItems.UseCompatibleStateImageBehavior = false;
      listViewWorkItems.View = View.Details;
      listViewWorkItems.ColumnClick += listViewSets_ColumnClick;
      columnHeader4.Text = "ID";
      columnHeader5.Text = "Title";
      columnHeader5.Width = 200;
      columnHeader6.Text = "Type";
      columnHeader6.Width = 80;
      columnHeader7.Text = "State";
      columnHeader16.Text = "Modified by";
      columnHeader16.Width = 80;
      columnHeader17.Text = "Modified date";
      columnHeader17.Width = 130;
      columnHeader8.Text = "Checkin action";
      columnHeader8.Width = 80;
      saveFileDialog1.Filter = "All files (*.*)|*.*";
      saveFileDialog1.Title = "Save File";
      AutoScaleDimensions = new SizeF(6f, 13f);
      AutoScaleMode = AutoScaleMode.Font;
      Controls.Add(splitContainer1);
      Name = nameof (ShelvesetViewControl);
      Size = new Size(725, 522);
      splitContainer1.Panel1.ResumeLayout(false);
      splitContainer1.Panel1.PerformLayout();
      splitContainer1.Panel2.ResumeLayout(false);
      splitContainer1.ResumeLayout(false);
      contextMenuStrip1.ResumeLayout(false);
      toolStrip2.ResumeLayout(false);
      toolStrip2.PerformLayout();
      panelSearch.ResumeLayout(false);
      groupBox1.ResumeLayout(false);
      groupBox1.PerformLayout();
      tabControl1.ResumeLayout(false);
      tabPageProperties.ResumeLayout(false);
      groupBox3.ResumeLayout(false);
      groupBox3.PerformLayout();
      contextMenuStrip2.ResumeLayout(false);
      toolStrip1.ResumeLayout(false);
      toolStrip1.PerformLayout();
      groupBox2.ResumeLayout(false);
      tabPageChangesets.ResumeLayout(false);
      splitContainer2.Panel1.ResumeLayout(false);
      splitContainer2.Panel2.ResumeLayout(false);
      splitContainer2.ResumeLayout(false);
      groupBox4.ResumeLayout(false);
      groupBox5.ResumeLayout(false);
      ResumeLayout(false);
    }

    public override void Initialize(TfsController controller)
    {
      _controller = new ShelvesetViewController(controller);
      _dtUsers = _controller.Users.UsersTable.Copy();
      ListTable.AddAllRow(_dtUsers);
      _dtProjects = _controller.GetProjects(false);
      ListTable.AddAllRow(_dtProjects);
      ClearSetsList();
      ClearSetProperties();
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
    }

    private void DefaultSearchParameters()
    {
      textBoxSetName.Text = "";
      comboBoxUserName.SelectedValue = _controller.UserFullName;
      datePickerTo.Value = DateTime.Now;
      datePickerFrom.Value = DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0));
    }

    private SearchParameters GetSearchParameters()
    {
      var userName = comboBoxUserName.SelectedValue == null ? comboBoxUserName.Text : comboBoxUserName.SelectedValue.ToString();
      if (userName == ListTable.cAllID || userName == "")
        userName = null;
      var dateTime1 = datePickerFrom.Value;
      var dateTime2 = datePickerTo.Value;
      return new SearchParameters(textBoxSetName.TextLength == 0 ? null : textBoxSetName.Text, userName, null, "$/", new DateTime(dateTime1.Year, dateTime1.Month, dateTime1.Day), new DateTime(dateTime2.Year, dateTime2.Month, dateTime2.Day, 23, 59, 59));
    }

    private void ClearSetsList()
    {
      listViewSets.ClearItems();
      toolStripButtonDeleteSet.Enabled = false;
      toolStripButtonSaveAll.Enabled = false;
    }

    private void ClearSetProperties()
    {
      propertyGridSet.SelectedObject = null;
      listViewItems.ClearItems();
      toolStripButtonCompareLatest.Enabled = false;
      toolStripButtonCompareWithOriginal.Enabled = false;
      toolStripButtonSaveItem.Enabled = false;
      toolStripButtonViewItem.Enabled = false;
    }

    private void SearchShelvesets()
    {
      listViewSets.BeginUpdate();
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        ClearSetsList();
        ClearSetProperties();
        var shelvesetArray = _controller.SearchShelvesets(GetSearchParameters());
        listViewSets.SetCapacity(shelvesetArray.Length);
        foreach (var shelveset in shelvesetArray)
          listViewSets.AddItem(new ListViewItem(shelveset.Name)
          {
            SubItems = {
              Utilities.GetTableValueByID(_dtUsers, shelveset.OwnerName),
              Utilities.FormatDateTimeInvariant(shelveset.CreationDate)
            },
            Tag = shelveset
          });
        toolStripButtonDeleteSet.Enabled = listViewSets.Items.Count > 0;
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to retrieve labels." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        listViewSets.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
    }

    private void DisplayProperties(Shelveset set)
    {
      listViewItems.BeginUpdate();
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        toolStripButtonDeleteSet.Enabled = true;
        propertyGridSet.SelectedObject = new ShelvesetProperties(set, Utilities.GetTableValueByID(_dtUsers, set.OwnerName));
        var shelvedChanges = _controller.GetShelvedChanges(set);
        if (shelvedChanges.Length != 1)
          return;
        foreach (var pendingChange in shelvedChanges[0].PendingChanges)
          listViewItems.AddItem(new ListViewItem(pendingChange.ServerItem)
          {
            SubItems = {
              pendingChange.Version.ToString(),
              pendingChange.ChangeTypeName,
              pendingChange.ItemType.ToString()
            },
            Tag = pendingChange
          });
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
    }

    private void DisplayChangesets(Shelveset set)
    {
      listViewNotes.BeginUpdate();
      listViewWorkItems.BeginUpdate();
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        listViewNotes.Items.Clear();
        listViewWorkItems.Items.Clear();
        foreach (var checkinNoteFieldValue in set.CheckinNote.Values)
          listViewNotes.Items.Add(new ListViewItem(checkinNoteFieldValue.Name)
          {
            SubItems = {
              checkinNoteFieldValue.Value
            }
          });
        foreach (var workItemCheckinInfo in set.WorkItemInfo)
          listViewWorkItems.Items.Add(new ListViewItem(workItemCheckinInfo.WorkItem.Id.ToString())
          {
            SubItems = {
              workItemCheckinInfo.WorkItem.Title,
              workItemCheckinInfo.WorkItem.Type.Name,
              workItemCheckinInfo.WorkItem.State,
              Utilities.GetTableValueByID(_dtUsers, workItemCheckinInfo.WorkItem.ChangedBy),
              workItemCheckinInfo.WorkItem.ChangedDate.ToString(),
              workItemCheckinInfo.CheckinAction.ToString()
            }
          });
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to retrieve shelveset data." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        listViewWorkItems.EndUpdate();
        listViewNotes.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
    }

    private void ChangeTab()
    {
      if (listViewSets.SelectedIndices.Count != 1)
      {
        toolStripButtonDeleteSet.Enabled = false;
        ClearSetProperties();
        listViewWorkItems.Items.Clear();
        listViewNotes.Items.Clear();
      }
      else
      {
        var tag = listViewSets.SelectedItem.Tag as Shelveset;
        if (tabControl1.SelectedTab == tabPageProperties)
          DisplayProperties(tag);
        else
          DisplayChangesets(tag);
      }
    }

    private void CompareVersion(bool latest)
    {
      if (listViewItems.SelectedIndices.Count != 1)
        return;
      var tag1 = listViewSets.Items[listViewSets.SelectedIndices[0]].Tag as Shelveset;
      var tag2 = listViewItems.Items[listViewItems.SelectedIndices[0]].Tag as PendingChange;
      Cursor.Current = Cursors.WaitCursor;
      try
      {
        var shelvedChange = _controller.GetShelvedChange(tag1, tag2);
        IDiffItem idiffItem = new DiffItemShelvedChange(tag1.Name, shelvedChange);
        IDiffItem targetDiffItem;
        if (latest)
        {
          targetDiffItem = Difference.CreateTargetDiffItem(_controller.VersionControl, shelvedChange, VersionSpec.Latest);
        }
        else
        {
          var versionControl = _controller.VersionControl;
          var pendingChange = shelvedChange;
          var changesetVersionSpec = new ChangesetVersionSpec(pendingChange.Version);
          targetDiffItem = Difference.CreateTargetDiffItem(versionControl, pendingChange, changesetVersionSpec);
        }
        Difference.VisualDiffItems(_controller.VersionControl, idiffItem, targetDiffItem);
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to compare files." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
    }

    private void SelectSet()
    {
      toolStripButtonSaveAll.Enabled = listViewSets.SelectedIndices.Count == 1;
      if (listViewSets.SelectedIndices.Count == 0)
      {
        toolStripButtonDeleteSet.Enabled = false;
        ClearSetProperties();
      }
      else if (listViewSets.SelectedIndices.Count > 1)
      {
        toolStripButtonDeleteSet.Enabled = true;
        ClearSetProperties();
      }
      else
      {
        var tag = listViewSets.SelectedItem.Tag as Shelveset;
        if (tabControl1.SelectedTab == tabPageProperties)
        {
          DisplayProperties(tag);
        }
        else
        {
          if (tabControl1.SelectedTab != tabPageChangesets)
            return;
          DisplayChangesets(tag);
        }
      }
    }

    private void SelectItem()
    {
      if (listViewItems.SelectedIndices.Count == 1)
      {
        var tag = listViewItems.SelectedItem.Tag as PendingChange;
        if (tag.ItemType == ItemType.Folder)
        {
          toolStripMenuItemSave.Enabled = toolStripMenuItemView.Enabled = toolStripMenuItemCompareLatest.Enabled = toolStripMenuItemCompareOriginal.Enabled = toolStripButtonCompareLatest.Enabled = toolStripButtonCompareWithOriginal.Enabled = toolStripButtonViewItem.Enabled = toolStripButtonSaveItem.Enabled = false;
        }
        else
        {
          var itemCompareLatest = toolStripMenuItemCompareLatest;
          var itemCompareOriginal = toolStripMenuItemCompareOriginal;
          var compareWithOriginal = toolStripButtonCompareWithOriginal;
          bool flag1;
          toolStripButtonCompareLatest.Enabled = flag1 = tag.IsEdit && !tag.IsAdd;
          int num1;
          var flag2 = (num1 = flag1 ? 1 : 0) != 0;
          compareWithOriginal.Enabled = num1 != 0;
          int num2;
          var flag3 = (num2 = flag2 ? 1 : 0) != 0;
          itemCompareOriginal.Enabled = num2 != 0;
          var num3 = flag3 ? 1 : 0;
          itemCompareLatest.Enabled = num3 != 0;
          toolStripMenuItemSave.Enabled = toolStripMenuItemView.Enabled = toolStripButtonViewItem.Enabled = toolStripButtonSaveItem.Enabled = true;
        }
      }
      else
        toolStripMenuItemSave.Enabled = toolStripMenuItemView.Enabled = toolStripMenuItemCompareLatest.Enabled = toolStripMenuItemCompareOriginal.Enabled = toolStripButtonCompareLatest.Enabled = toolStripButtonCompareWithOriginal.Enabled = toolStripButtonViewItem.Enabled = toolStripButtonSaveItem.Enabled = false;
    }

    private void DownloadAll()
    {
      var folderBrowserDialog = new FolderBrowserDialog();
      if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
        return;
      Refresh();
      var tag1 = listViewSets.SelectedItem.Tag as Shelveset;
      Cursor.Current = Cursors.WaitCursor;
      try
      {
        for (var index = 0; index < listViewItems.ItemsCount; ++index)
        {
          var tag2 = listViewItems.Items[index].Tag as PendingChange;
          var shelvedChange = _controller.GetShelvedChange(tag1, tag2);
          var path2 = shelvedChange.ServerItem.Substring(2);
          var str = Path.Combine(folderBrowserDialog.SelectedPath, path2);
          shelvedChange.DownloadShelvedFile(str);
        }
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to retrieve shelved file." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
    }

    private void DownloadItem(bool saveOnly)
    {
      var tag1 = listViewItems.SelectedItem.Tag as PendingChange;
      var tag2 = listViewSets.SelectedItem.Tag as Shelveset;
      Cursor.Current = Cursors.WaitCursor;
      try
      {
        var shelvedChange = _controller.GetShelvedChange(tag2, tag1);
        string fileName;
        if (saveOnly)
        {
          saveFileDialog1.FileName = shelvedChange.FileName;
          if (saveFileDialog1.ShowDialog() != DialogResult.OK)
            return;
          fileName = saveFileDialog1.FileName;
        }
        else
        {
          var tempFileName = Path.GetTempFileName();
          File.Delete(tempFileName);
          fileName = Path.Combine(Path.GetDirectoryName(tempFileName), Path.GetFileNameWithoutExtension(tempFileName)) + "\\" + shelvedChange.FileName;
        }
        shelvedChange.DownloadShelvedFile(fileName);
        if (saveOnly)
          return;
        Process.Start(fileName);
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to retrieve shelved file." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
    }

    private bool DeleteShelveset()
    {
      if (listViewSets.SelectedIndices.Count == 0 || MessageBox.Show("Do you want to delete selected shelveset(s)?", "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return false;
      var formResults = new FormResults();
      formResults.Initialize("Delete shelveset");
      Cursor.Current = Cursors.WaitCursor;
      try
      {
        foreach (int selectedIndex in listViewSets.SelectedIndices)
        {
          var tag = listViewSets.Items[selectedIndex].Tag as Shelveset;
          try
          {
            _controller.DeleteShelveset(tag);
            formResults.AddResult(true, "Shelveset " + tag.DisplayName, "Shelveset deleted");
          }
          catch (Exception ex)
          {
            formResults.AddResult(false, "Shelveset " + tag.DisplayName, ex.Message);
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
  }
}
