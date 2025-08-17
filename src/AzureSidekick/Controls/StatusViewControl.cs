// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.StatusViewControl
// Assembly: Attrice.TeamFoundation.Controls.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=1a24c887ab53aa6e
// MVID: 393BE899-3C29-4A70-86D7-B4B965E5A8F5
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controls.12.dll

using System;
using System.Collections.Generic;
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
  public class StatusViewControl : BaseSidekickControl
  {
    private IContainer components;
    private ComboBox comboBoxUserName;
    private ComboBox comboBoxProject;
    private TeamFoundationDateTimePicker datePickerTo;
    private TeamFoundationDateTimePicker datePickerFrom;
    private Label label4;
    private Label label3;
    private GroupBox groupBox1;
    private Label label2;
    private Label label1;
    private Button buttonClear;
    private SplitContainer splitContainer1;
    private Panel panelSearch;
    private Button buttonSearch;
    private VirtualListView listViewHistory;
    private ColumnHeader columnHeader1;
    private ColumnHeader columnHeader2;
    private ColumnHeader columnHeader8;
    private ColumnHeader columnHeader3;
    private ColumnHeader columnHeader4;
    private ColumnHeader columnHeader5;
    private ColumnHeader columnHeader6;
    private ColumnHeader columnHeader9;
    private ColumnHeader columnHeader7;
    private ToolStrip toolStrip1;
    private ToolStripButton toolStripButtonUndo;
    private ToolStripButton toolStripButtonUnlock;
    private ToolStripButton toolStripButtonSaveToFile;
    private ToolStripButton toolStripButtonChangeLayout;
    private ImageList imageList;
    private TreeView treeViewStatus;
    private ToolStripSeparator toolStripSeparator1;
    private StatusViewController _controller;
    private DataTable _dtUsers;
    private DataTable _dtProjects;

    public StatusViewControl()
    {
      InitializeComponent();
      listViewHistory.Sorted = true;
      listViewHistory.ListViewItemSorter = new CustomListSorter(ListSorterType.Date | ListSorterType.Integer);
      (listViewHistory.ListViewItemSorter as CustomListSorter).Ascending = true;
      splitContainer1.Panel1MinSize = 350;
      splitContainer1.Panel2MinSize = 350;
      panelSearch.BackColor = SystemColors.Control;
      splitContainer1.BackColor = SystemColors.Control;
      datePickerTo.CustomFormat = datePickerFrom.CustomFormat = "dd-MMM-yyyy";
      datePickerTo.Format = datePickerFrom.Format = DateTimePickerFormat.Custom;
      Name = "Status Sidekick";
    }

    public override Image Image => Resources.StatusImage;

    private void buttonSearch_Click(object sender, EventArgs e)
    {
      ClearChangesTree();
      ClearChangesList();
      SearchChanges();
    }

    private void buttonClear_Click(object sender, EventArgs e) => DefaultSearchParameters();

    private void toolStripButtonSaveToFile_Click(object sender, EventArgs e) => SaveToFile();

    private void toolStripButtonChangeLayout_Click(object sender, EventArgs e)
    {
      ConfigureColumns();
    }

    private void treeViewStatus_AfterSelect(object sender, TreeViewEventArgs e)
    {
      SelectChangesNode(e.Node);
    }

    private void listViewHistory_SelectedIndexChanged(object sender, EventArgs e)
    {
      SelectChangesList();
    }

    private void toolStripButtonUndo_Click(object sender, EventArgs e)
    {
      UndoSelectedChanges();
    }

    private void toolStripButtonUnlock_Click(object sender, EventArgs e)
    {
      UnlockSelectedChanges();
    }

    private void listViewHistory_VirtualItemsSelectionRangeChanged(
      object sender,
      ListViewVirtualItemsSelectionRangeChangedEventArgs e)
    {
      SelectChangesList();
    }

    public override StringDictionary Settings
    {
      get
      {
        _settings = new StringDictionary();
        _settings.Add("ColumnChangeType", StatusViewConfiguration.Instance.ColumnChangeType.ToString());
        _settings.Add("ColumnLockType", StatusViewConfiguration.Instance.ColumnLockType.ToString());
        _settings.Add("ColumnItemName", StatusViewConfiguration.Instance.ColumnItemName.ToString());
        _settings.Add("ColumnItemType", StatusViewConfiguration.Instance.ColumnItemType.ToString());
        _settings.Add("ColumnOwnerName", StatusViewConfiguration.Instance.ColumnOwnerName.ToString());
        _settings.Add("ColumnWorkspace", StatusViewConfiguration.Instance.ColumnWorkspace.ToString());
        _settings.Add("ColumnComputerName", StatusViewConfiguration.Instance.ColumnComputerName.ToString());
        _settings.Add("ColumnVersion", StatusViewConfiguration.Instance.ColumnVersion.ToString());
        _settings.Add("ColumnLocalPath", StatusViewConfiguration.Instance.ColumnLocalPath.ToString());
        _settings.Add("ColumnServerPath", StatusViewConfiguration.Instance.ColumnServerPath.ToString());
        _settings.Add("ColumnChangeDate", StatusViewConfiguration.Instance.ColumnChangeDate.ToString());
        return _settings;
      }
      set
      {
        base.Settings = value;
        if (_settings == null)
          return;
        var result1 = true;
        if (_settings["ColumnChangeType"] != null)
          bool.TryParse(_settings["ColumnChangeType"], out result1);
        StatusViewConfiguration.Instance.ColumnChangeType = result1;
        var result2 = true;
        if (_settings["ColumnLockType"] != null)
          bool.TryParse(_settings["ColumnLockType"], out result2);
        StatusViewConfiguration.Instance.ColumnLockType = result2;
        var result3 = true;
        if (_settings["ColumnItemName"] != null)
          bool.TryParse(_settings["ColumnItemName"], out result3);
        StatusViewConfiguration.Instance.ColumnItemName = result3;
        var result4 = true;
        if (_settings["ColumnItemType"] != null)
          bool.TryParse(_settings["ColumnItemType"], out result4);
        StatusViewConfiguration.Instance.ColumnItemType = result4;
        var result5 = true;
        if (_settings["ColumnOwnerName"] != null)
          bool.TryParse(_settings["ColumnOwnerName"], out result5);
        StatusViewConfiguration.Instance.ColumnOwnerName = result5;
        var result6 = true;
        if (_settings["ColumnWorkspace"] != null)
          bool.TryParse(_settings["ColumnWorkspace"], out result6);
        StatusViewConfiguration.Instance.ColumnWorkspace = result6;
        var result7 = true;
        if (_settings["ColumnComputerName"] != null)
          bool.TryParse(_settings["ColumnComputerName"], out result7);
        StatusViewConfiguration.Instance.ColumnComputerName = result7;
        var result8 = true;
        if (_settings["ColumnVersion"] != null)
          bool.TryParse(_settings["ColumnVersion"], out result8);
        StatusViewConfiguration.Instance.ColumnVersion = result8;
        var result9 = true;
        if (_settings["ColumnLocalPath"] != null)
          bool.TryParse(_settings["ColumnLocalPath"], out result9);
        StatusViewConfiguration.Instance.ColumnLocalPath = result9;
        var result10 = true;
        if (_settings["ColumnServerPath"] != null)
          bool.TryParse(_settings["ColumnServerPath"], out result10);
        StatusViewConfiguration.Instance.ColumnServerPath = result10;
        var result11 = true;
        if (_settings["ColumnChangeDate"] != null)
          bool.TryParse(_settings["ColumnChangeDate"], out result11);
        StatusViewConfiguration.Instance.ColumnChangeDate = result11;
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
      var componentResourceManager = new ComponentResourceManager(typeof (StatusViewControl));
      comboBoxUserName = new ComboBox();
      comboBoxProject = new ComboBox();
      datePickerTo = new TeamFoundationDateTimePicker();
      datePickerFrom = new TeamFoundationDateTimePicker();
      label4 = new Label();
      label3 = new Label();
      groupBox1 = new GroupBox();
      label2 = new Label();
      label1 = new Label();
      buttonClear = new Button();
      splitContainer1 = new SplitContainer();
      treeViewStatus = new TreeView();
      imageList = new ImageList(components);
      panelSearch = new Panel();
      buttonSearch = new Button();
      listViewHistory = new VirtualListView();
      columnHeader1 = new ColumnHeader();
      columnHeader2 = new ColumnHeader();
      columnHeader8 = new ColumnHeader();
      columnHeader3 = new ColumnHeader();
      columnHeader4 = new ColumnHeader();
      columnHeader5 = new ColumnHeader();
      columnHeader6 = new ColumnHeader();
      columnHeader9 = new ColumnHeader();
      columnHeader7 = new ColumnHeader();
      toolStrip1 = new ToolStrip();
      toolStripButtonUndo = new ToolStripButton();
      toolStripButtonUnlock = new ToolStripButton();
      toolStripSeparator1 = new ToolStripSeparator();
      toolStripButtonSaveToFile = new ToolStripButton();
      toolStripButtonChangeLayout = new ToolStripButton();
      groupBox1.SuspendLayout();
      splitContainer1.Panel1.SuspendLayout();
      splitContainer1.Panel2.SuspendLayout();
      splitContainer1.SuspendLayout();
      panelSearch.SuspendLayout();
      toolStrip1.SuspendLayout();
      SuspendLayout();
      comboBoxUserName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      comboBoxUserName.AutoCompleteMode = AutoCompleteMode.Suggest;
      comboBoxUserName.AutoCompleteSource = AutoCompleteSource.ListItems;
      comboBoxUserName.FormattingEnabled = true;
      comboBoxUserName.IntegralHeight = false;
      comboBoxUserName.ItemHeight = 13;
      comboBoxUserName.Location = new Point(139, 18);
      comboBoxUserName.MaxDropDownItems = 32;
      comboBoxUserName.Name = "comboBoxUserName";
      comboBoxUserName.Size = new Size(195, 21);
      comboBoxUserName.TabIndex = 1;
      comboBoxProject.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      comboBoxProject.AutoCompleteMode = AutoCompleteMode.Suggest;
      comboBoxProject.AutoCompleteSource = AutoCompleteSource.ListItems;
      comboBoxProject.FormattingEnabled = true;
      comboBoxProject.IntegralHeight = false;
      comboBoxProject.Location = new Point(139, 45);
      comboBoxProject.MaxDropDownItems = 32;
      comboBoxProject.Name = "comboBoxProject";
      comboBoxProject.Size = new Size(195, 21);
      comboBoxProject.TabIndex = 2;
      datePickerTo.AlternateBackgroundColor = SystemColors.Control;
      datePickerTo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      datePickerTo.CustomFormat = "";
      datePickerTo.Location = new Point(139, 98);
      datePickerTo.Name = "datePickerTo";
      datePickerTo.Size = new Size(195, 20);
      datePickerTo.TabIndex = 4;
      datePickerTo.UseAlternateBackgroundColor = false;
      datePickerFrom.AlternateBackgroundColor = SystemColors.Control;
      datePickerFrom.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      datePickerFrom.CustomFormat = "";
      datePickerFrom.Location = new Point(139, 72);
      datePickerFrom.Name = "datePickerFrom";
      datePickerFrom.Size = new Size(195, 20);
      datePickerFrom.TabIndex = 3;
      datePickerFrom.UseAlternateBackgroundColor = false;
      label4.AutoSize = true;
      label4.Location = new Point(8, 104);
      label4.Name = "label4";
      label4.Size = new Size(86, 13);
      label4.TabIndex = 15;
      label4.Text = "Change date (to)";
      label3.AutoSize = true;
      label3.Location = new Point(8, 77);
      label3.Name = "label3";
      label3.Size = new Size(97, 13);
      label3.TabIndex = 14;
      label3.Text = "Change date (from)";
      groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
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
      groupBox1.Size = new Size(340, 134);
      groupBox1.TabIndex = 0;
      groupBox1.TabStop = false;
      groupBox1.Text = " Search criteria";
      label2.AutoSize = true;
      label2.Location = new Point(8, 50);
      label2.Name = "label2";
      label2.Size = new Size(69, 13);
      label2.TabIndex = 13;
      label2.Text = "Project name";
      label1.AutoSize = true;
      label1.Location = new Point(8, 23);
      label1.Name = "label1";
      label1.Size = new Size(58, 13);
      label1.TabIndex = 12;
      label1.Text = "User name";
      buttonClear.Location = new Point(85, 143);
      buttonClear.Name = "buttonClear";
      buttonClear.Size = new Size(75, 23);
      buttonClear.TabIndex = 2;
      buttonClear.Text = "Clear";
      buttonClear.UseVisualStyleBackColor = true;
      buttonClear.Click += buttonClear_Click;
      splitContainer1.BorderStyle = BorderStyle.Fixed3D;
      splitContainer1.Dock = DockStyle.Fill;
      splitContainer1.FixedPanel = FixedPanel.Panel1;
      splitContainer1.Location = new Point(0, 0);
      splitContainer1.Name = "splitContainer1";
      splitContainer1.Panel1.Controls.Add(treeViewStatus);
      splitContainer1.Panel1.Controls.Add(panelSearch);
      splitContainer1.Panel2.Controls.Add(listViewHistory);
      splitContainer1.Panel2.Controls.Add(toolStrip1);
      splitContainer1.Panel2.Padding = new Padding(3, 3, 0, 0);
      splitContainer1.Size = new Size(772, 479);
      splitContainer1.SplitterDistance = 350;
      splitContainer1.SplitterWidth = 2;
      splitContainer1.TabIndex = 1;
      treeViewStatus.Dock = DockStyle.Fill;
      treeViewStatus.HideSelection = false;
      treeViewStatus.ImageIndex = 0;
      treeViewStatus.ImageList = imageList;
      treeViewStatus.Location = new Point(0, 177);
      treeViewStatus.Name = "treeViewStatus";
      treeViewStatus.SelectedImageIndex = 1;
      treeViewStatus.Size = new Size(346, 298);
      treeViewStatus.StateImageList = imageList;
      treeViewStatus.TabIndex = 3;
      treeViewStatus.AfterSelect += treeViewStatus_AfterSelect;
      imageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList.ImageStream");
      imageList.TransparentColor = Color.Transparent;
      imageList.Images.SetKeyName(0, "server.bmp");
      imageList.Images.SetKeyName(1, "project.bmp");
      imageList.Images.SetKeyName(2, "folder.bmp");
      imageList.Images.SetKeyName(3, "folder_open.bmp");
      imageList.Images.SetKeyName(4, "item.gif");
      imageList.Images.SetKeyName(5, "check.gif");
      imageList.Images.SetKeyName(6, "lock.gif");
      panelSearch.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      panelSearch.Controls.Add(buttonClear);
      panelSearch.Controls.Add(buttonSearch);
      panelSearch.Controls.Add(groupBox1);
      panelSearch.Dock = DockStyle.Top;
      panelSearch.Location = new Point(0, 0);
      panelSearch.Margin = new Padding(0);
      panelSearch.Name = "panelSearch";
      panelSearch.Size = new Size(346, 177);
      panelSearch.TabIndex = 2;
      buttonSearch.Location = new Point(4, 143);
      buttonSearch.Name = "buttonSearch";
      buttonSearch.Size = new Size(75, 23);
      buttonSearch.TabIndex = 1;
      buttonSearch.Text = "Search";
      buttonSearch.UseVisualStyleBackColor = true;
      buttonSearch.Click += buttonSearch_Click;
      listViewHistory.AllowColumnReorder = true;
      listViewHistory.Columns.AddRange(new ColumnHeader[9]
      {
        columnHeader1,
        columnHeader2,
        columnHeader8,
        columnHeader3,
        columnHeader4,
        columnHeader5,
        columnHeader6,
        columnHeader9,
        columnHeader7
      });
      listViewHistory.Dock = DockStyle.Fill;
      listViewHistory.FullRowSelect = true;
      listViewHistory.GridLines = true;
      listViewHistory.HideSelection = false;
      listViewHistory.Location = new Point(3, 28);
      listViewHistory.Name = "listViewHistory";
      listViewHistory.Size = new Size(413, 447);
      listViewHistory.Sorted = false;
      listViewHistory.Sorting = SortOrder.Descending;
      listViewHistory.TabIndex = 4;
      listViewHistory.UseCompatibleStateImageBehavior = false;
      listViewHistory.View = View.Details;
      listViewHistory.VirtualItemsSelectionRangeChanged += listViewHistory_VirtualItemsSelectionRangeChanged;
      listViewHistory.SelectedIndexChanged += listViewHistory_SelectedIndexChanged;
      columnHeader1.Text = "Change";
      columnHeader2.Text = "Item name";
      columnHeader2.Width = 100;
      columnHeader8.DisplayIndex = 8;
      columnHeader8.Text = "Item type";
      columnHeader3.DisplayIndex = 2;
      columnHeader3.Text = "Owner";
      columnHeader3.Width = 100;
      columnHeader4.DisplayIndex = 3;
      columnHeader4.Text = "Workspace";
      columnHeader4.Width = 100;
      columnHeader5.DisplayIndex = 4;
      columnHeader5.Text = "Computer";
      columnHeader5.Width = 100;
      columnHeader6.DisplayIndex = 5;
      columnHeader6.Text = "Version";
      columnHeader9.DisplayIndex = 6;
      columnHeader9.Text = "Local path";
      columnHeader9.Width = 200;
      columnHeader7.DisplayIndex = 7;
      columnHeader7.Text = "Change date";
      toolStrip1.AllowMerge = false;
      toolStrip1.CanOverflow = false;
      toolStrip1.GripMargin = new Padding(1);
      toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
      toolStrip1.Items.AddRange(new ToolStripItem[5]
      {
        toolStripButtonUndo,
        toolStripButtonUnlock,
        toolStripSeparator1,
        toolStripButtonSaveToFile,
        toolStripButtonChangeLayout
      });
      toolStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
      toolStrip1.Location = new Point(3, 3);
      toolStrip1.Name = "toolStrip1";
      toolStrip1.Padding = new Padding(3, 1, 1, 1);
      toolStrip1.RenderMode = ToolStripRenderMode.System;
      toolStrip1.Size = new Size(413, 25);
      toolStrip1.Stretch = true;
      toolStrip1.TabIndex = 3;
      toolStripButtonUndo.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonUndo.Image = (Image) componentResourceManager.GetObject("toolStripButtonUndo.Image");
      toolStripButtonUndo.ImageTransparentColor = Color.Magenta;
      toolStripButtonUndo.Name = "toolStripButtonUndo";
      toolStripButtonUndo.Size = new Size(23, 20);
      toolStripButtonUndo.ToolTipText = "Undo pending change";
      toolStripButtonUndo.Click += toolStripButtonUndo_Click;
      toolStripButtonUnlock.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonUnlock.Image = (Image) componentResourceManager.GetObject("toolStripButtonUnlock.Image");
      toolStripButtonUnlock.ImageTransparentColor = Color.Magenta;
      toolStripButtonUnlock.Name = "toolStripButtonUnlock";
      toolStripButtonUnlock.Size = new Size(23, 20);
      toolStripButtonUnlock.Text = "toolStripButton1";
      toolStripButtonUnlock.ToolTipText = "Unlock lock";
      toolStripButtonUnlock.Click += toolStripButtonUnlock_Click;
      toolStripSeparator1.Name = "toolStripSeparator1";
      toolStripSeparator1.Size = new Size(6, 23);
      toolStripButtonSaveToFile.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonSaveToFile.Image = (Image) componentResourceManager.GetObject("toolStripButtonSaveToFile.Image");
      toolStripButtonSaveToFile.ImageTransparentColor = Color.Black;
      toolStripButtonSaveToFile.Name = "toolStripButtonSaveToFile";
      toolStripButtonSaveToFile.Size = new Size(23, 20);
      toolStripButtonSaveToFile.Text = "toolStripButton5";
      toolStripButtonSaveToFile.ToolTipText = "Save list to file";
      toolStripButtonSaveToFile.Click += toolStripButtonSaveToFile_Click;
      toolStripButtonChangeLayout.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonChangeLayout.Image = (Image) componentResourceManager.GetObject("toolStripButtonChangeLayout.Image");
      toolStripButtonChangeLayout.ImageTransparentColor = Color.Magenta;
      toolStripButtonChangeLayout.Name = "toolStripButtonChangeLayout";
      toolStripButtonChangeLayout.Size = new Size(23, 20);
      toolStripButtonChangeLayout.Text = "toolStripButton1";
      toolStripButtonChangeLayout.ToolTipText = "Configure list columns";
      toolStripButtonChangeLayout.Click += toolStripButtonChangeLayout_Click;
      AutoScaleDimensions = new SizeF(6f, 13f);
      AutoScaleMode = AutoScaleMode.Font;
      Controls.Add(splitContainer1);
      Name = nameof (StatusViewControl);
      Size = new Size(772, 479);
      groupBox1.ResumeLayout(false);
      groupBox1.PerformLayout();
      splitContainer1.Panel1.ResumeLayout(false);
      splitContainer1.Panel2.ResumeLayout(false);
      splitContainer1.Panel2.PerformLayout();
      splitContainer1.ResumeLayout(false);
      panelSearch.ResumeLayout(false);
      toolStrip1.ResumeLayout(false);
      toolStrip1.PerformLayout();
      ResumeLayout(false);
    }

    public override void Initialize(TfsController controller)
    {
      _controller = new StatusViewController(controller);
      _dtUsers = _controller.Users.UsersTable.Copy();
      ListTable.AddAllRow(_dtUsers);
      _dtProjects = _controller.GetProjects(false);
      ListTable.AddAllRow(_dtProjects);
      LoadSearchParameters();
      DefaultSearchParameters();
      ClearChangesTree();
      ClearChangesList();
      LoadColumns();
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

    private void DefaultSearchParameters()
    {
      comboBoxUserName.SelectedValue = _controller.UserFullName;
      datePickerTo.Value = DateTime.Now;
      datePickerFrom.Value = DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0));
    }

    private void ClearChangesTree() => treeViewStatus.Nodes.Clear();

    private void ClearChangesList()
    {
      listViewHistory.ClearItems();
      toolStripButtonUndo.Enabled = toolStripButtonUnlock.Enabled = toolStripButtonSaveToFile.Enabled = false;
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
      return new SearchParameters(userName, null, "$/" + str, new DateTime(dateTime1.Year, dateTime1.Month, dateTime1.Day), new DateTime(dateTime2.Year, dateTime2.Month, dateTime2.Day, 23, 59, 59));
    }

    private void SearchChanges()
    {
      ClearChangesTree();
      ClearChangesList();
      treeViewStatus.BeginUpdate();
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        var searchParameters = GetSearchParameters();
        var changes = _controller.SearchChanges(searchParameters);
        FillTree(searchParameters, changes);
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to retrieve pending changes." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        treeViewStatus.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
    }

    private TreeNode InsertSortNode(TreeNodeCollection nodesParent, string text, bool sortFolders)
    {
      if (sortFolders)
      {
        var num = 0;
        foreach (TreeNode treeNode in nodesParent)
        {
          if (treeNode.ImageIndex == 2 && string.Compare(treeNode.Text, text, StringComparison.OrdinalIgnoreCase) < 0)
            num = treeNode.Index;
          else
            return treeNode.ImageIndex == 2 ? nodesParent.Insert(treeNode.Index + 1, text) : nodesParent.Insert(treeNode.Index, text);
        }
        return nodesParent.Insert(num + 1, text);
      }
      foreach (TreeNode treeNode in nodesParent)
      {
        if (treeNode.ImageIndex != 2 && (treeNode.ImageIndex != 4 || string.Compare(treeNode.Text, text) >= 0))
          return nodesParent.Insert(treeNode.Index, text);
      }
      return nodesParent.Add(text);
    }

    private void FillTree(SearchParameters parameters, PendingChangeProperties[] changes)
    {
      string key1 = null;
      if (treeViewStatus.SelectedNode != null)
        key1 = treeViewStatus.SelectedNode.Name;
      foreach (var change in changes)
      {
        if (!(change.Change.CreationDate < parameters.FromDate) && !(change.Change.CreationDate > parameters.ToDate))
        {
          var treeNodeArray1 = treeViewStatus.Nodes.Find(change.Change.ServerItem, true);
          if (treeNodeArray1 == null || treeNodeArray1.Length == 0)
          {
            var key2 = string.Empty;
            TreeNode treeNode1 = null;
            var serverItem = change.Change.ServerItem;
            var chArray = new char[1]{ '/' };
            foreach (var text in serverItem.Split(chArray))
            {
              key2 = key2 + text + "/";
              var treeNodeArray2 = treeViewStatus.Nodes.Find(key2, true);
              if (treeNodeArray2 == null || treeNodeArray2.Length == 0)
              {
                var nodesParent = treeNode1 != null ? treeNode1.Nodes : treeViewStatus.Nodes;
                var treeNode2 = !(text == change.Change.FileName) || change.Change.ItemType == ItemType.Folder ? InsertSortNode(nodesParent, text, true) : InsertSortNode(nodesParent, text, false);
                treeNode2.Name = key2;
                treeNode1 = treeNode2;
              }
              else
                treeNode1 = treeNodeArray2[0];
              if (text == change.Change.FileName)
              {
                if (change.Change.ItemType == ItemType.File)
                {
                  var treeNode3 = treeNode1;
                  int num1;
                  var num2 = num1 = 4;
                  treeNode3.SelectedImageIndex = num1;
                  treeNode3.ImageIndex = num2;
                }
                else
                {
                  treeNode1.ImageIndex = 2;
                  treeNode1.SelectedImageIndex = 3;
                }
                if (change.Change.IsLock)
                  treeNode1.StateImageIndex = 6;
              }
              else
              {
                treeNode1.ImageIndex = 2;
                treeNode1.SelectedImageIndex = 3;
              }
            }
            if (!(treeNode1.Tag is List<PendingChangeProperties> changePropertiesList))
            {
              changePropertiesList = new List<PendingChangeProperties>();
              treeNode1.Tag = changePropertiesList;
            }
            changePropertiesList.Add(change);
          }
        }
      }
      if (treeViewStatus.Nodes.Count > 0 && key1 != null)
      {
        var treeNodeArray = treeViewStatus.Nodes.Find(key1, true);
        if (treeNodeArray.Length == 1)
          treeViewStatus.SelectedNode = treeNodeArray[0];
      }
      if (treeViewStatus.Nodes.Count <= 0)
        return;
      treeViewStatus.Nodes[0].ImageIndex = treeViewStatus.Nodes[0].SelectedImageIndex = 0;
      foreach (TreeNode node in treeViewStatus.Nodes[0].Nodes)
      {
        int num3;
        var num4 = num3 = 1;
        node.SelectedImageIndex = num3;
        node.ImageIndex = num4;
      }
      if (treeViewStatus.SelectedNode != null)
        return;
      treeViewStatus.Nodes[0].Expand();
    }

    private void SelectNode() => ClearChangesList();

    private void LoadSearchParameters()
    {
      ListTable.LoadTable(comboBoxUserName, _dtUsers, _controller.UserFullName);
      ListTable.LoadTable(comboBoxProject, _dtProjects, "");
    }

    private void ConfigureColumns()
    {
      if (new FormConfigureStatus().ShowDialog() != DialogResult.OK)
        return;
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        listViewHistory.BeginUpdate();
        LoadColumns();
        FillList(treeViewStatus.SelectedNode);
        DefaultChangesList();
      }
      finally
      {
        listViewHistory.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
    }

    private void LoadColumns()
    {
      listViewHistory.ClearItems();
      listViewHistory.Columns.Clear();
      if (StatusViewConfiguration.Instance.ColumnChangeDate)
        listViewHistory.Columns.Add("Change date").Width = 120;
      if (StatusViewConfiguration.Instance.ColumnChangeType)
        listViewHistory.Columns.Add("Change type").Width = 90;
      if (StatusViewConfiguration.Instance.ColumnLockType)
        listViewHistory.Columns.Add("Lock type").Width = 60;
      if (StatusViewConfiguration.Instance.ColumnItemName)
        listViewHistory.Columns.Add("Item name").Width = 150;
      if (StatusViewConfiguration.Instance.ColumnItemType)
        listViewHistory.Columns.Add("Item type").Width = 60;
      if (StatusViewConfiguration.Instance.ColumnOwnerName)
        listViewHistory.Columns.Add("Owner").Width = 120;
      if (StatusViewConfiguration.Instance.ColumnVersion)
        listViewHistory.Columns.Add("Version").Width = 60;
      if (StatusViewConfiguration.Instance.ColumnWorkspace)
        listViewHistory.Columns.Add("Workspace").Width = 120;
      if (StatusViewConfiguration.Instance.ColumnComputerName)
        listViewHistory.Columns.Add("Computer").Width = 100;
      if (StatusViewConfiguration.Instance.ColumnLocalPath)
        listViewHistory.Columns.Add("Local path").Width = 200;
      if (!StatusViewConfiguration.Instance.ColumnServerPath)
        return;
      listViewHistory.Columns.Add("Server path").Width = 200;
    }

    private void FillList(TreeNode node)
    {
      if (node == null)
        return;
      if (!(node.Tag is List<PendingChangeProperties> tag))
      {
        foreach (TreeNode node1 in node.Nodes)
          FillList(node1);
      }
      else
      {
        if (node.Nodes.Count > 0)
        {
          foreach (TreeNode node2 in node.Nodes)
            FillList(node2);
        }
        var stringList = new List<string>();
        foreach (var changeProperties in tag)
        {
          stringList.Clear();
          var listViewItem = new ListViewItem();
          foreach (ColumnHeader column in listViewHistory.Columns)
          {
            if (column.Text == "Change type")
            {
              var changeType = changeProperties.Change.ChangeType;
              if (changeProperties.Change.IsLock)
                changeType = changeType ^ ChangeType.Lock;
              if (changeType == null)
                changeType = (ChangeType) 1;
              stringList.Add(changeType.ToString());
            }
            if (column.Text == "Lock type")
              stringList.Add(changeProperties.Change.LockLevel.ToString());
            if (column.Text == "Item name")
              stringList.Add(Utilities.ParseServerItem(changeProperties.Change.ServerItem));
            if (column.Text == "Item type")
              stringList.Add(changeProperties.Change.ItemType.ToString());
            if (column.Text == "Owner")
              stringList.Add(Utilities.GetTableValueByID(_dtUsers, changeProperties.OwnerName));
            if (column.Text == "Workspace")
              stringList.Add(changeProperties.Name);
            if (column.Text == "Computer")
              stringList.Add(changeProperties.Computer);
            if (column.Text == "Version")
              stringList.Add(changeProperties.Change.Version.ToString());
            if (column.Text == "Local path")
              stringList.Add(changeProperties.Change.LocalItem ?? " ");
            if (column.Text == "Server path")
              stringList.Add(changeProperties.Change.ServerItem);
            if (column.Text == "Change date")
              stringList.Add(Utilities.FormatDateTimeInvariant(changeProperties.Change.CreationDate));
            if (column.Index == 0)
            {
              listViewItem.Text = stringList[0];
              stringList.Clear();
            }
          }
          listViewItem.Tag = changeProperties;
          listViewItem.SubItems.AddRange(stringList.ToArray());
          listViewHistory.AddItem(listViewItem);
        }
      }
    }

    private void DefaultChangesList()
    {
      toolStripButtonSaveToFile.Enabled = listViewHistory.ItemsCount > 0;
      if (listViewHistory.SelectedItem == null)
        return;
      listViewHistory.Select();
      listViewHistory.SelectedIndices.Add(0);
    }

    private void SelectChangesNode(TreeNode node)
    {
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        listViewHistory.BeginUpdate();
        ClearChangesList();
        FillList(node);
        DefaultChangesList();
      }
      finally
      {
        listViewHistory.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
    }

    private void SelectChangesList()
    {
      toolStripButtonUndo.Enabled = toolStripButtonUnlock.Enabled = false;
      foreach (int selectedIndex in listViewHistory.SelectedIndices)
      {
        var tag = listViewHistory.Items[selectedIndex].Tag as PendingChangeProperties;
        toolStripButtonUnlock.Enabled = toolStripButtonUnlock.Enabled || tag.Change.IsLock;
        toolStripButtonUndo.Enabled = true;
      }
    }

    private void UndoSelectedChanges()
    {
      if (listViewHistory.SelectedIndices.Count == 0)
        return;
      var str = string.Empty;
      Cursor.Current = Cursors.WaitCursor;
      try
      {
        foreach (int selectedIndex in listViewHistory.SelectedIndices)
        {
          var tag = listViewHistory.Items[selectedIndex].Tag as PendingChangeProperties;
          str = tag.Change.ServerItem;
          _controller.UndoChange(tag);
        }
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show($"Failed to unlock {(object)str} ({(object)ex.Message})", "Unlock error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        UpdateTree();
        Cursor.Current = Cursors.Default;
      }
    }

    private void UnlockSelectedChanges()
    {
      if (listViewHistory.SelectedIndices.Count == 0)
        return;
      var str = string.Empty;
      Cursor.Current = Cursors.WaitCursor;
      try
      {
        foreach (int selectedIndex in listViewHistory.SelectedIndices)
        {
          var tag = listViewHistory.Items[selectedIndex].Tag as PendingChangeProperties;
          str = tag.Change.ServerItem;
          _controller.UnlockChange(tag);
        }
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show($"Failed to unlock {(object)str} ({(object)ex.Message})", "Unlock error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        UpdateTree();
        Cursor.Current = Cursors.Default;
      }
    }

    private void UpdateTree()
    {
      var name1 = treeViewStatus.SelectedNode.Name;
      var isExpanded = treeViewStatus.SelectedNode.IsExpanded;
      TreeNode node;
      if (treeViewStatus.SelectedNode.Parent == null)
      {
        var name2 = treeViewStatus.SelectedNode.Name;
        node = treeViewStatus.SelectedNode;
      }
      else
      {
        var name3 = treeViewStatus.SelectedNode.Parent.Name;
        node = treeViewStatus.SelectedNode.Parent;
      }
      node.Nodes.Clear();
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        treeViewStatus.BeginUpdate();
        var searchParameters = GetSearchParameters();
        searchParameters.ChangeSourcePath(node.Name);
        var changes = _controller.SearchChanges(searchParameters);
        if (changes.Length == 0)
        {
          if (node == treeViewStatus.SelectedNode)
          {
            ClearChangesTree();
            ClearChangesList();
            return;
          }
          RemoveNodes(node);
        }
        else
          FillTree(searchParameters, changes);
      }
      finally
      {
        treeViewStatus.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
      if (treeViewStatus.Nodes.Find(node.Name, true).Length == 0)
        return;
      var treeNodeArray = node.Nodes.Find(name1, false);
      if (treeNodeArray.Length == 1)
      {
        treeViewStatus.SelectedNode = treeNodeArray[0];
        if (isExpanded)
          treeViewStatus.SelectedNode.Expand();
      }
      else
        treeViewStatus.SelectedNode = node;
      SelectChangesNode(treeViewStatus.SelectedNode);
    }

    private void RemoveNodes(TreeNode node)
    {
      if (node.Tag != null)
        return;
      var parent = node.Parent;
      node.Remove();
      if (parent == null || parent.Nodes.Count > 0)
        return;
      RemoveNodes(parent);
    }

    private void SaveToFile()
    {
      Utilities.SaveListViewToFile(saveFileDialog, listViewHistory,
          $"Pending changes list as of {(object)DateTime.Now}");
    }
  }
}
