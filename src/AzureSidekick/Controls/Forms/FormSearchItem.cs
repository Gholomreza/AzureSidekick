// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.Forms.FormSearchItem
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
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.VersionControl.Common;

namespace Attrice.TeamFoundation.Controls.Forms
{
  public class FormSearchItem : Form
  {
    private HistoryViewController _controller;
    private DataTable _dtProjects;
    private bool _showDeleted;
    private string _initialServerItem;
    private bool _isInitialFolder;
    private string _serverItem;
    private IContainer components;
    private Button buttonSearch;
    private GroupBox groupBox1;
    private ComboBox comboBoxSearchType;
    private Label label3;
    private TextBox textBoxSearchString;
    private ComboBox comboBoxPath;
    private Label label2;
    private Label label1;
    private VirtualListView listViewItems;
    private ColumnHeader columnHeaderItemName;
    private ColumnHeader columnHeaderItemPath;
    private ColumnHeader columnHeaderItemType;
    private Button buttonJump;
    private Button buttonCancel;
    private ToolTip toolTip1;
    private TextBox textBoxSearchPath;
    private Label label4;
    private Label label5;
    private RadioButton radioButtonFull;
    private RadioButton radioButtonPath;

    public bool ShowDeleted
    {
      get => _showDeleted;
      set => _showDeleted = value;
    }

    public string InitialServerItem
    {
      get => _initialServerItem;
      set => _initialServerItem = value;
    }

    public bool IsInitialFolder
    {
      get => _isInitialFolder;
      set => _isInitialFolder = value;
    }

    public string SelectedServerItem => _serverItem;

    public FormSearchItem()
    {
      InitializeComponent();
      listViewItems.Sorted = true;
      buttonJump.Enabled = false;
    }

    public void Initialize(HistoryViewController controller)
    {
      _controller = controller;
      _dtProjects = controller.GetProjects(false);
      comboBoxSearchType.Items.Add("Files and folders");
      comboBoxSearchType.Items.Add("Files only");
      comboBoxSearchType.Items.Add("Folders only");
      comboBoxSearchType.SelectedIndex = 0;
      var row = _dtProjects.NewRow();
      row[ListTable.ColumnValue] = "All projects";
      row[ListTable.ColumnID] = "$/";
      _dtProjects.Rows.Add(row);
      ListTable.LoadTable(comboBoxPath, _dtProjects, "$/");
      if (_showDeleted)
        listViewItems.Columns.Add("Deleted status", 60);
      if (!string.IsNullOrEmpty(_initialServerItem) && _initialServerItem != "$/")
      {
        if ("$/" != _initialServerItem)
          comboBoxPath.SelectedValue = VersionControlPath.GetTeamProjectName(_initialServerItem);
        else
          comboBoxPath.SelectedValue = _initialServerItem;
        if (_isInitialFolder)
        {
          textBoxSearchPath.Text = _initialServerItem;
          textBoxSearchString.Text = "*";
        }
        else
        {
          textBoxSearchPath.Text = VersionControlPath.GetFolderName(_initialServerItem);
          textBoxSearchString.Text = VersionControlPath.GetFileName(_initialServerItem);
        }
        comboBoxPath.Enabled = false;
      }
      else
      {
        comboBoxPath.SelectedIndexChanged += comboBoxPath_SelectedIndexChanged;
        comboBoxPath.Enabled = true;
        textBoxSearchPath.Text = "$/";
      }
    }

    private void buttonSearch_Click(object sender, EventArgs e)
    {
      ItemType itemType = 0;
      if (comboBoxSearchType.SelectedIndex == 1)
        itemType = (ItemType) 2;
      if (comboBoxSearchType.SelectedIndex == 2)
        itemType = (ItemType) 1;
      var text = textBoxSearchPath.Text;
      var str1 = "*";
      if (textBoxSearchString.TextLength > 0)
        str1 = textBoxSearchString.Text;
      var str2 = text;
      if (str2[str2.Length - 1] != '/')
        text += "/";
      var searchPath = text + str1;
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        listViewItems.BeginUpdate();
        foreach (var obj in _controller.SearchItems(searchPath, radioButtonFull.Checked, itemType, _showDeleted).Items)
        {
          var listViewItem = new ListViewItem(Utilities.ParseServerItem(obj.ServerItem));
          listViewItem.SubItems.Add(obj.ServerItem.Substring(0, obj.ServerItem.Length - listViewItem.Text.Length));
          listViewItem.SubItems.Add(obj.ItemType.ToString());
          if (_showDeleted)
            listViewItem.SubItems.Add(obj.DeletionId > 0 ? "Deleted" : "Not deleted");
          listViewItem.Tag = obj;
          listViewItems.AddItem(listViewItem);
        }
      }
      finally
      {
        listViewItems.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      Close();
    }

    private void buttonJump_Click(object sender, EventArgs e)
    {
      if (listViewItems.SelectedIndices.Count != 1)
        return;
      DialogResult = DialogResult.OK;
      _serverItem = (listViewItems.SelectedItem.Tag as Item).ServerItem;
      Close();
    }

    private void FormSearchItem_Activated(object sender, EventArgs e)
    {
      textBoxSearchString.Focus();
    }

    private void listViewItems_SelectedIndexChanged(object sender, EventArgs e)
    {
      buttonJump.Enabled = listViewItems.SelectedIndices.Count == 1;
    }

    private void radioButtonFull_CheckedChanged(object sender, EventArgs e)
    {
      radioButtonPath.Checked = !radioButtonFull.Checked;
    }

    private void radioButtonPath_CheckedChanged(object sender, EventArgs e)
    {
      radioButtonFull.Checked = !radioButtonPath.Checked;
    }

    private void comboBoxPath_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (comboBoxPath.SelectedValue == null)
        return;
      if (comboBoxPath.SelectedValue.ToString() == "$/")
        textBoxSearchPath.Text = "$/";
      else
        textBoxSearchPath.Text = "$/" + comboBoxPath.SelectedValue;
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
      var componentResourceManager = new ComponentResourceManager(typeof (FormSearchItem));
      buttonSearch = new Button();
      groupBox1 = new GroupBox();
      label5 = new Label();
      radioButtonFull = new RadioButton();
      radioButtonPath = new RadioButton();
      textBoxSearchPath = new TextBox();
      label4 = new Label();
      comboBoxSearchType = new ComboBox();
      label3 = new Label();
      textBoxSearchString = new TextBox();
      comboBoxPath = new ComboBox();
      label2 = new Label();
      label1 = new Label();
      buttonJump = new Button();
      buttonCancel = new Button();
      toolTip1 = new ToolTip(components);
      listViewItems = new VirtualListView();
      columnHeaderItemName = new ColumnHeader();
      columnHeaderItemPath = new ColumnHeader();
      columnHeaderItemType = new ColumnHeader();
      groupBox1.SuspendLayout();
      SuspendLayout();
      buttonSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      buttonSearch.Location = new Point(464, 193);
      buttonSearch.Name = "buttonSearch";
      buttonSearch.Size = new Size(75, 23);
      buttonSearch.TabIndex = 6;
      buttonSearch.Text = "Find";
      buttonSearch.UseVisualStyleBackColor = true;
      buttonSearch.Click += buttonSearch_Click;
      groupBox1.Controls.Add(label5);
      groupBox1.Controls.Add(radioButtonFull);
      groupBox1.Controls.Add(radioButtonPath);
      groupBox1.Controls.Add(textBoxSearchPath);
      groupBox1.Controls.Add(label4);
      groupBox1.Controls.Add(comboBoxSearchType);
      groupBox1.Controls.Add(label3);
      groupBox1.Controls.Add(buttonSearch);
      groupBox1.Controls.Add(textBoxSearchString);
      groupBox1.Controls.Add(comboBoxPath);
      groupBox1.Controls.Add(label2);
      groupBox1.Controls.Add(label1);
      groupBox1.Dock = DockStyle.Top;
      groupBox1.Location = new Point(0, 0);
      groupBox1.Name = "groupBox1";
      groupBox1.Size = new Size(552, 225);
      groupBox1.TabIndex = 0;
      groupBox1.TabStop = false;
      groupBox1.Text = " Search criteria";
      label5.AutoSize = true;
      label5.Location = new Point(16, 128);
      label5.Name = "label5";
      label5.Size = new Size(71, 13);
      label5.TabIndex = 23;
      label5.Text = "Search depth";
      radioButtonFull.AutoSize = true;
      radioButtonFull.Checked = true;
      radioButtonFull.Location = new Point(123, 126);
      radioButtonFull.Name = "radioButtonFull";
      radioButtonFull.Size = new Size(87, 17);
      radioButtonFull.TabIndex = 3;
      radioButtonFull.TabStop = true;
      radioButtonFull.Text = "Full recursion";
      radioButtonFull.UseVisualStyleBackColor = true;
      radioButtonFull.CheckedChanged += radioButtonFull_CheckedChanged;
      radioButtonPath.AutoSize = true;
      radioButtonPath.Location = new Point(215, sbyte.MaxValue);
      radioButtonPath.Name = "radioButtonPath";
      radioButtonPath.Size = new Size(80, 17);
      radioButtonPath.TabIndex = 4;
      radioButtonPath.Text = "In path only";
      radioButtonPath.UseVisualStyleBackColor = true;
      radioButtonPath.CheckedChanged += radioButtonPath_CheckedChanged;
      textBoxSearchPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      textBoxSearchPath.Location = new Point(122, 92);
      textBoxSearchPath.Name = "textBoxSearchPath";
      textBoxSearchPath.ReadOnly = true;
      textBoxSearchPath.Size = new Size(417, 20);
      textBoxSearchPath.TabIndex = 2;
      toolTip1.SetToolTip(textBoxSearchPath, "Search string may contain both exact item names and names with wildcards * or ?");
      label4.AutoSize = true;
      label4.Location = new Point(16, 94);
      label4.Name = "label4";
      label4.Size = new Size(95, 13);
      label4.TabIndex = 19;
      label4.Text = "Search under path";
      comboBoxSearchType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      comboBoxSearchType.DropDownStyle = ComboBoxStyle.DropDownList;
      comboBoxSearchType.FormattingEnabled = true;
      comboBoxSearchType.Location = new Point(122, 157);
      comboBoxSearchType.Name = "comboBoxSearchType";
      comboBoxSearchType.Size = new Size(417, 21);
      comboBoxSearchType.TabIndex = 5;
      label3.AutoSize = true;
      label3.Location = new Point(16, 159);
      label3.Name = "label3";
      label3.Size = new Size(86, 13);
      label3.TabIndex = 17;
      label3.Text = "Search item type";
      textBoxSearchString.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      textBoxSearchString.Location = new Point(122, 27);
      textBoxSearchString.Name = "textBoxSearchString";
      textBoxSearchString.Size = new Size(417, 20);
      textBoxSearchString.TabIndex = 0;
      toolTip1.SetToolTip(textBoxSearchString, "Search string may contain both exact item names and names with wildcards * or ?");
      comboBoxPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      comboBoxPath.DropDownStyle = ComboBoxStyle.DropDownList;
      comboBoxPath.FormattingEnabled = true;
      comboBoxPath.Location = new Point(122, 59);
      comboBoxPath.Name = "comboBoxPath";
      comboBoxPath.Size = new Size(417, 21);
      comboBoxPath.TabIndex = 1;
      label2.AutoSize = true;
      label2.Location = new Point(16, 62);
      label2.Name = "label2";
      label2.Size = new Size(87, 13);
      label2.TabIndex = 11;
      label2.Text = "Search in project";
      label1.AutoSize = true;
      label1.Location = new Point(16, 30);
      label1.Name = "label1";
      label1.Size = new Size(69, 13);
      label1.TabIndex = 10;
      label1.Text = "Search string";
      buttonJump.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      buttonJump.Location = new Point(5, 381);
      buttonJump.Name = "buttonJump";
      buttonJump.Size = new Size(92, 23);
      buttonJump.TabIndex = 2;
      buttonJump.Text = "Jump To Item";
      buttonJump.UseVisualStyleBackColor = true;
      buttonJump.Click += buttonJump_Click;
      buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      buttonCancel.DialogResult = DialogResult.Cancel;
      buttonCancel.Location = new Point(473, 381);
      buttonCancel.Name = "buttonCancel";
      buttonCancel.Size = new Size(75, 23);
      buttonCancel.TabIndex = 3;
      buttonCancel.Text = "Cancel";
      buttonCancel.UseVisualStyleBackColor = true;
      buttonCancel.Click += buttonCancel_Click;
      toolTip1.AutomaticDelay = 250;
      toolTip1.ToolTipIcon = ToolTipIcon.Info;
      toolTip1.ToolTipTitle = "Search string format";
      listViewItems.AllowColumnReorder = true;
      listViewItems.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      listViewItems.Columns.AddRange(new ColumnHeader[3]
      {
        columnHeaderItemName,
        columnHeaderItemPath,
        columnHeaderItemType
      });
      listViewItems.FullRowSelect = true;
      listViewItems.GridLines = true;
      listViewItems.HideSelection = false;
      listViewItems.Location = new Point(0, 231);
      listViewItems.MultiSelect = false;
      listViewItems.Name = "listViewItems";
      listViewItems.Size = new Size(552, 140);
      listViewItems.Sorted = false;
      listViewItems.Sorting = SortOrder.Descending;
      listViewItems.TabIndex = 1;
      listViewItems.UseCompatibleStateImageBehavior = false;
      listViewItems.View = View.Details;
      listViewItems.SelectedIndexChanged += listViewItems_SelectedIndexChanged;
      columnHeaderItemName.Text = "Item Name";
      columnHeaderItemName.Width = 130;
      columnHeaderItemPath.Text = "Item Path";
      columnHeaderItemPath.Width = 210;
      columnHeaderItemType.Text = "Item Type";
      AutoScaleDimensions = new SizeF(6f, 13f);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(552, 409);
      Controls.Add(buttonCancel);
      Controls.Add(buttonJump);
      Controls.Add(listViewItems);
      Controls.Add(groupBox1);
      Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      MaximizeBox = false;
      MinimizeBox = false;
      MinimumSize = new Size(400, 400);
      Name = nameof (FormSearchItem);
      ShowInTaskbar = false;
      SizeGripStyle = SizeGripStyle.Hide;
      StartPosition = FormStartPosition.CenterScreen;
      Text = "Find item";
      Activated += FormSearchItem_Activated;
      groupBox1.ResumeLayout(false);
      groupBox1.PerformLayout();
      ResumeLayout(false);
    }
  }
}
