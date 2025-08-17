// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.Forms.FormCompareLabels
// Assembly: Attrice.TeamFoundation.Controls.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=1a24c887ab53aa6e
// MVID: 393BE899-3C29-4A70-86D7-B4B965E5A8F5
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controls.12.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Attrice.TeamFoundation.Common;
using Attrice.TeamFoundation.Controllers;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace Attrice.TeamFoundation.Controls.Forms
{
  public class FormCompareLabels : Form
  {
    private LabelViewController _controller;
    private VersionControlLabel _label1;
    private VersionControlLabel _label2;
    private SortedDictionary<int, Changeset> _sets1;
    private SortedDictionary<int, Changeset> _sets2;
    private SortedDictionary<int, WorkItem> _items1;
    private SortedDictionary<int, WorkItem> _items2;
    private SortedDictionary<string, Item> _labelItems1 = new SortedDictionary<string, Item>(StringComparer.OrdinalIgnoreCase);
    private SortedDictionary<string, Item> _labelItems2;
    private bool _changing;
    private IContainer components;
    private TabControl tabControl1;
    private TabPage tabPage1;
    private TabPage tabPage2;
    private SplitContainer splitContainerItems;
    private VirtualListView listViewItems1;
    private ColumnHeader columnHeader1;
    private ColumnHeader columnHeader2;
    private VirtualListView listViewItems2;
    private ColumnHeader columnHeader3;
    private ColumnHeader columnHeader4;
    private SplitContainer splitContainerSets;
    private VirtualListView listViewSets1;
    private ColumnHeader columnHeader6;
    private VirtualListView listViewSets2;
    private ColumnHeader columnHeader8;
    private TabPage tabPage3;
    private SplitContainer splitContainerWorkItems;
    private VirtualListView listViewWorkItems1;
    private ColumnHeader columnHeader5;
    private VirtualListView listViewWorkItems2;
    private ColumnHeader columnHeader7;
    private ColumnHeader columnHeader12;
    private ColumnHeader columnHeader13;
    private ColumnHeader columnHeader14;
    private ColumnHeader columnHeader9;
    private ColumnHeader columnHeader10;
    private ColumnHeader columnHeader11;
    private ColumnHeader columnHeader15;
    private ColumnHeader columnHeader16;
    private ColumnHeader columnHeader17;
    private ColumnHeader columnHeader18;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem compareVersionsToolStripMenuItem;
    private Panel panel1;
    private Button buttonCancel;
    private ToolStripMenuItem viewRelativeHistoryToolStripMenuItem;
    private ToolStrip toolStrip1;
    private ToolStripLabel toolStripLabel2;
    private ToolStripLabel toolStripLabel23;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripButton toolStripButtonSave23;
    private ToolStrip toolStrip11;
    private ToolStripLabel toolStripLabel4;
    private ToolStripLabel toolStripLabel11;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripButton toolStripButtonSave11;
    private ToolStrip toolStrip21;
    private ToolStripLabel toolStripLabel7;
    private ToolStripLabel toolStripLabel21;
    private ToolStripSeparator toolStripSeparator6;
    private ToolStripButton toolStripButtonSave21;
    private ToolStrip toolStrip2;
    private ToolStripLabel toolStripLabel3;
    private ToolStripLabel toolStripLabel12;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripButton toolStripButtonSave12;
    private ToolStrip toolStrip3;
    private ToolStripLabel toolStripLabel5;
    private ToolStripLabel toolStripLabel22;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripButton toolStripButtonSave22;
    protected SaveFileDialog saveFileDialog;
    private CheckBox checkBoxDifference;
    private ToolStrip toolStrip31;
    private ToolStripLabel toolStripLabel1;
    private ToolStripLabel toolStripLabel13;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripButton toolStripButtonSave13;
    private ComboBox comboBoxFileFilter;
    private Label labelFilterByFile;

    public FormCompareLabels()
    {
      InitializeComponent();
      splitContainerItems.Panel2MinSize = 250;
      splitContainerItems.Panel1MinSize = 250;
      splitContainerItems.SplitterDistance = 330;
      splitContainerSets.Panel2MinSize = 250;
      splitContainerSets.Panel1MinSize = 250;
      splitContainerSets.SplitterDistance = 330;
      splitContainerWorkItems.Panel2MinSize = 250;
      splitContainerWorkItems.Panel1MinSize = 250;
      splitContainerWorkItems.SplitterDistance = 330;
      listViewSets1.Sorted = listViewSets2.Sorted = false;
      listViewWorkItems1.Sorted = listViewWorkItems2.Sorted = false;
    }

    public void SetLabels(
      LabelViewController controller,
      VersionControlLabel label1,
      VersionControlLabel label2)
    {
      comboBoxFileFilter.Items.Clear();
      comboBoxFileFilter.Items.Add(".*");
      comboBoxFileFilter.SelectedIndex = 0;
      comboBoxFileFilter.SelectedIndexChanged += comboBoxFileFilter_SelectedIndexChanged;
      _controller = controller;
      _label1 = label1;
      _label2 = label2;
      toolStripLabel11.Text = toolStripLabel12.Text = toolStripLabel13.Text = _label1.Name;
      if (_label2 != null)
      {
        toolStripLabel21.Text = toolStripLabel22.Text = toolStripLabel23.Text = _label2.Name;
      }
      else
      {
        tabControl1.TabPages.RemoveAt(1);
        tabControl1.TabPages.RemoveAt(1);
        toolStripLabel21.Text = toolStripLabel22.Text = toolStripLabel23.Text = "No label (latest version)";
      }
      _changing = true;
      CompareItems();
      _changing = false;
    }

    private void CompareChangesets()
    {
      listViewSets1.BeginUpdate();
      listViewSets2.BeginUpdate();
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        if (_sets1 == null)
        {
          _sets1 = _controller.GetLabelChangesets(_label1);
          _sets2 = _controller.GetLabelChangesets(_label2);
        }
        FillItemsList(_sets1, _sets2, listViewSets1, listViewSets2, checkBoxDifference.Checked);
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to retrieve changeset data." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        listViewSets1.EndUpdate();
        listViewSets2.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
    }

    private void CompareWorkItems()
    {
      listViewWorkItems1.BeginUpdate();
      listViewWorkItems2.BeginUpdate();
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        if (_items1 == null)
        {
          CompareChangesets();
          _items1 = _controller.GetLabelWorkItems(_sets1);
          _items2 = _controller.GetLabelWorkItems(_sets2);
        }
        FillItemsList(_items1, _items2, listViewWorkItems1, listViewWorkItems2, checkBoxDifference.Checked);
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to retrieve changeset/work items data." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        listViewWorkItems1.EndUpdate();
        listViewWorkItems2.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
    }

    private void CompareItems()
    {
      listViewItems1.BeginUpdate();
      listViewItems2.BeginUpdate();
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        listViewItems1.ClearItems();
        listViewItems2.ClearItems();
        if (_labelItems1.Count == 0)
        {
          foreach (var obj in _label1.Items)
            _labelItems1[obj.ServerItem] = obj;
        }
        if (_labelItems2 == null)
        {
          if (_label2 == null)
          {
            _labelItems2 = _controller.GetLabelLatest(_label1);
          }
          else
          {
            _labelItems2 = new SortedDictionary<string, Item>(StringComparer.OrdinalIgnoreCase);
            foreach (var obj in _label2.Items)
              _labelItems2[obj.ServerItem] = obj;
          }
        }
        FillItemsList(_labelItems1, _labelItems2, listViewItems1, checkBoxDifference.Checked);
        FillItemsList(_labelItems2, _labelItems1, listViewItems2, checkBoxDifference.Checked);
        FillDifferentItemsList(_labelItems1, _labelItems2, listViewItems1, listViewItems2);
        FillDifferentItemsList(_labelItems2, _labelItems1, listViewItems2, listViewItems1);
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to retrieve items data." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        listViewItems1.EndUpdate();
        listViewItems2.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
    }

    private void LabelAsDifferent(ListViewItem item1, ListViewItem item2)
    {
      item1.BackColor = Color.MistyRose;
      item2.BackColor = Color.MistyRose;
    }

    private void LabelAsAbsent(ListViewItem item1, ListViewItem item2)
    {
      item1.BackColor = Color.Azure;
      item2.BackColor = Color.LightBlue;
    }

    private void LabelAsFolder(ListViewItem item1, ListViewItem item2)
    {
      item1.ForeColor = Color.DarkBlue;
      item2.ForeColor = Color.DarkBlue;
    }

    private void SyncSelection(VirtualListView listView1, VirtualListView listView2)
    {
      if (_changing)
        return;
      _changing = true;
      if (listView1.SelectedIndices.Count == 0)
      {
        listView2.SelectedIndices.Clear();
      }
      else
      {
        listView2.SelectedIndices.Clear();
        listView2.SelectedIndices.Add(listView1.SelectedIndices[0]);
        listView2.SelectedItem.EnsureVisible();
      }
      _changing = false;
    }

    private void listViewItems1_SelectedIndexChanged(object sender, EventArgs e)
    {
      SyncSelection(listViewItems1, listViewItems2);
      SetContextMenu();
    }

    private void listViewItems2_SelectedIndexChanged(object sender, EventArgs e)
    {
      SyncSelection(listViewItems2, listViewItems1);
      SetContextMenu();
    }

    private void listViewSets1_SelectedIndexChanged(object sender, EventArgs e)
    {
      SyncSelection(listViewSets1, listViewSets2);
    }

    private void listViewSets2_SelectedIndexChanged(object sender, EventArgs e)
    {
      SyncSelection(listViewSets2, listViewSets1);
    }

    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
      _changing = true;
      labelFilterByFile.Visible = comboBoxFileFilter.Visible = tabControl1.SelectedIndex == 0;
      if (tabControl1.SelectedIndex == 1)
        CompareChangesets();
      else if (tabControl1.SelectedIndex == 2)
        CompareWorkItems();
      _changing = false;
    }

    private void listViewWorkItems1_SelectedIndexChanged(object sender, EventArgs e)
    {
      SyncSelection(listViewWorkItems1, listViewWorkItems2);
    }

    private void listViewWorkItems2_SelectedIndexChanged(object sender, EventArgs e)
    {
      SyncSelection(listViewWorkItems2, listViewWorkItems1);
    }

    private void SetContextMenu()
    {
      compareVersionsToolStripMenuItem.Enabled = listViewItems1.SelectedIndices.Count == 1 && listViewItems2.SelectedIndices.Count == 1;
      if (!compareVersionsToolStripMenuItem.Enabled)
        return;
      var selectedItem1 = listViewItems1.SelectedItem;
      var selectedItem2 = listViewItems2.SelectedItem;
      var text1 = selectedItem1.Text;
      var text2 = selectedItem1.SubItems[1].Text;
      var text3 = selectedItem2.SubItems[1].Text;
      if (text2 == "None" || text3 == "None")
        viewRelativeHistoryToolStripMenuItem.Enabled = compareVersionsToolStripMenuItem.Enabled = false;
      else if (selectedItem1.ForeColor == Color.DarkBlue)
      {
        viewRelativeHistoryToolStripMenuItem.Enabled = selectedItem1.BackColor == Color.MistyRose;
        compareVersionsToolStripMenuItem.Enabled = false;
      }
      else if (selectedItem1.BackColor == Color.MistyRose)
        viewRelativeHistoryToolStripMenuItem.Enabled = compareVersionsToolStripMenuItem.Enabled = true;
      else
        viewRelativeHistoryToolStripMenuItem.Enabled = compareVersionsToolStripMenuItem.Enabled = false;
    }

    private void compareVersionsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      var selectedItem1 = listViewItems1.SelectedItem;
      var selectedItem2 = listViewItems2.SelectedItem;
      var text1 = selectedItem1.Text;
      var text2 = selectedItem1.SubItems[1].Text;
      var text3 = selectedItem2.SubItems[1].Text;
      Cursor.Current = Cursors.WaitCursor;
      try
      {
        Difference.VisualDiffFiles(_controller.VersionControl, text1, new ChangesetVersionSpec(text2), text1, new ChangesetVersionSpec(text3));
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to compare items" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
    }

    private void viewRelativeHistoryToolStripMenuItem_Click(object sender, EventArgs e)
    {
      var selectedItem1 = listViewItems1.SelectedItem;
      var selectedItem2 = listViewItems2.SelectedItem;
      var text = selectedItem1.Text;
      var changesetVersionSpec1 = new ChangesetVersionSpec(selectedItem1.SubItems[1].Text);
      var changesetVersionSpec2 = new ChangesetVersionSpec(selectedItem2.SubItems[1].Text);
      var changesets = changesetVersionSpec1.ChangesetId >= changesetVersionSpec2.ChangesetId ? _controller.VersionControl.QueryHistory(text, VersionSpec.Latest, 0, 0, null, changesetVersionSpec2, changesetVersionSpec1, int.MaxValue, false, true) : _controller.VersionControl.QueryHistory(text, VersionSpec.Latest, 0, 0, null, changesetVersionSpec1, changesetVersionSpec2, int.MaxValue, false, true);
      var formRelativeHistory = new FormRelativeHistory();
      if (changesetVersionSpec1.ChangesetId < changesetVersionSpec2.ChangesetId)
        formRelativeHistory.Initialize(text, _label1.Name, _label2 == null ? "Latest" : _label2.Name, changesets);
      else
        formRelativeHistory.Initialize(text, _label2 == null ? "Latest" : _label2.Name, _label1.Name, changesets);
      var num = (int) formRelativeHistory.ShowDialog(this);
    }

    private void toolStripButtonSaveClick(object sender, EventArgs e)
    {
      ListView listView = null;
      var flag = checkBoxDifference.Checked;
      var str1 = "";
      var str2 = "";
      var str3 = "";
      if (sender == toolStripButtonSave11)
      {
        listView = listViewItems1;
        str2 = $"Label \"{(object)_label1.Name}\"";
        str3 = _label2 == null ? "Latest version compared to label" : $"Label \"{(object)_label2.Name}\"";
        str1 = "items";
      }
      else if (sender == toolStripButtonSave21)
      {
        listView = listViewItems2;
        str2 = _label2 == null ? "Latest version compared to label" : $"Label \"{(object)_label2.Name}\"";
        str3 = $"Label \"{(object)_label1.Name}\"";
        str1 = "items";
      }
      else if (sender == toolStripButtonSave13)
      {
        listView = listViewWorkItems1;
        str2 = $"Label \"{(object)_label1.Name}\"";
        str3 = _label2 == null ? "Latest version compared to label" : $"Label \"{(object)_label2.Name}\"";
        str1 = "work items";
      }
      else if (sender == toolStripButtonSave23)
      {
        listView = listViewWorkItems2;
        str2 = _label2 == null ? "Latest version compared to label" : $"Label \"{(object)_label2.Name}\"";
        str3 = $"Label \"{(object)_label1.Name}\"";
        str1 = "work items";
      }
      else if (sender == toolStripButtonSave12)
      {
        listView = listViewSets1;
        str2 = $"Label \"{(object)_label1.Name}\"";
        str3 = _label2 == null ? "Latest version compared to label" : $"Label \"{(object)_label2.Name}\"";
        str1 = "changesets";
      }
      else if (sender == toolStripButtonSave22)
      {
        listView = listViewSets2;
        str2 = _label2 == null ? "Latest version compared to label" : $"Label \"{(object)_label2.Name}\"";
        str3 = $"Label \"{(object)_label1.Name}\"";
        str1 = "changesets";
      }
      var fileTitle =
          $"{(object)str2} {(object)str1} {(flag ? $"(only different from {(object)str3} shown)" : (object)"")}; valid as of {(object)DateTime.Now}";
      Utilities.SaveListViewToFile(saveFileDialog, listView, fileTitle);
    }

    private void FillItemsList(
      SortedDictionary<int, WorkItem> items1,
      SortedDictionary<int, WorkItem> items2,
      VirtualListView listView1,
      VirtualListView listView2,
      bool showDifferentOnly)
    {
      foreach (var key in items1.Keys)
      {
        var itemCompared = ItemCompared.Same;
        var workItem = items1[key];
        if (!items2.ContainsKey(key))
          itemCompared = ItemCompared.Absent;
        if (!(itemCompared == ItemCompared.Same & showDifferentOnly))
        {
          var id = workItem.Id;
          var listViewItem1 = new ListViewItem(id.ToString());
          listViewItem1.SubItems.Add(workItem.Title);
          listViewItem1.SubItems.Add(workItem.Type.Name);
          listViewItem1.SubItems.Add(workItem.State);
          listView1.AddItem(listViewItem1);
          ListViewItem listViewItem2;
          if (itemCompared == ItemCompared.Same)
          {
            id = workItem.Id;
            listViewItem2 = new ListViewItem(id.ToString());
            listViewItem2.SubItems.Add(workItem.Title);
            listViewItem2.SubItems.Add(workItem.Type.Name);
            listViewItem2.SubItems.Add(workItem.State);
          }
          else
          {
            listViewItem2 = new ListViewItem("None");
            listViewItem2.SubItems.Add(" ");
            listViewItem2.SubItems.Add(" ");
            listViewItem2.SubItems.Add(" ");
            LabelAsAbsent(listViewItem1, listViewItem2);
          }
          listView2.AddItem(listViewItem2);
        }
      }
      foreach (var key in items2.Keys)
      {
        if (!items1.ContainsKey(key))
        {
          var workItem = items2[key];
          var listViewItem3 = new ListViewItem(workItem.Id.ToString());
          listViewItem3.SubItems.Add(workItem.Title);
          listViewItem3.SubItems.Add(workItem.Type.Name);
          listViewItem3.SubItems.Add(workItem.State);
          listView2.AddItem(listViewItem3);
          var listViewItem4 = new ListViewItem("None");
          listViewItem4.SubItems.Add(" ");
          listViewItem4.SubItems.Add(" ");
          listViewItem4.SubItems.Add(" ");
          listView1.AddItem(listViewItem4);
          LabelAsAbsent(listViewItem3, listViewItem4);
        }
      }
    }

    private void FillItemsList(
      SortedDictionary<int, Changeset> items1,
      SortedDictionary<int, Changeset> items2,
      VirtualListView listView1,
      VirtualListView listView2,
      bool showDifferentOnly)
    {
      foreach (var key in items1.Keys)
      {
        var itemCompared = ItemCompared.Same;
        var changeset = items1[key];
        if (!items2.ContainsKey(key))
          itemCompared = ItemCompared.Absent;
        if (!(itemCompared == ItemCompared.Same & showDifferentOnly))
        {
          var changesetId = changeset.ChangesetId;
          var listViewItem1 = new ListViewItem(changesetId.ToString());
          listViewItem1.SubItems.Add(changeset.CreationDate.ToString());
          listViewItem1.SubItems.Add(string.IsNullOrEmpty(changeset.Comment) ? " " : changeset.Comment);
          listView1.AddItem(listViewItem1);
          ListViewItem listViewItem2;
          if (itemCompared == ItemCompared.Same)
          {
            changesetId = changeset.ChangesetId;
            listViewItem2 = new ListViewItem(changesetId.ToString());
            listViewItem2.SubItems.Add(changeset.CreationDate.ToString());
            listViewItem2.SubItems.Add(string.IsNullOrEmpty(changeset.Comment) ? " " : changeset.Comment);
          }
          else
          {
            listViewItem2 = new ListViewItem("None");
            listViewItem2.SubItems.Add(" ");
            listViewItem2.SubItems.Add(" ");
            LabelAsAbsent(listViewItem1, listViewItem2);
          }
          listView2.AddItem(listViewItem2);
        }
      }
      foreach (var key in items2.Keys)
      {
        if (!items1.ContainsKey(key))
        {
          var changeset = items2[key];
          var listViewItem3 = new ListViewItem(changeset.ChangesetId.ToString());
          listViewItem3.SubItems.Add(changeset.CreationDate.ToString());
          listViewItem3.SubItems.Add(string.IsNullOrEmpty(changeset.Comment) ? " " : changeset.Comment);
          listView2.AddItem(listViewItem3);
          var listViewItem4 = new ListViewItem("None");
          listViewItem4.SubItems.Add(" ");
          listViewItem4.SubItems.Add(" ");
          listView1.AddItem(listViewItem4);
          LabelAsAbsent(listViewItem3, listViewItem4);
        }
      }
    }

    private void FillItemsList(
      SortedDictionary<string, Item> items1,
      SortedDictionary<string, Item> items2,
      VirtualListView listView,
      bool showDifferentOnly)
    {
      var flag = comboBoxFileFilter.SelectedIndex > 0;
      var str = flag ? comboBoxFileFilter.SelectedItem.ToString() : string.Empty;
      foreach (var key in items1.Keys)
      {
        if (flag)
        {
          var num = key.Length - str.Length;
          if (key.IndexOf(str, num - 1, StringComparison.OrdinalIgnoreCase) != num)
            continue;
        }
        var itemCompared = ItemCompared.Same;
        var obj1 = items1[key];
        if (!items2.ContainsKey(key))
        {
          itemCompared = ItemCompared.Absent;
        }
        else
        {
          var obj2 = items2[key];
          if (obj1.ChangesetId != obj2.ChangesetId)
            itemCompared = ItemCompared.Different;
        }
        if (!(itemCompared == ItemCompared.Same & showDifferentOnly))
        {
          var listViewItem = new ListViewItem(obj1.ServerItem, obj1.ServerItem);
          listViewItem.SubItems.Add(obj1.ChangesetId.ToString());
          listView.AddItem(listViewItem);
          if (itemCompared == ItemCompared.Different)
            listViewItem.BackColor = Color.MistyRose;
          if (obj1.ItemType == ItemType.Folder)
            listViewItem.ForeColor = Color.Navy;
          if (!flag)
          {
            var extension = Path.GetExtension(key);
            if (!string.IsNullOrEmpty(extension))
            {
              var lowerInvariant = extension.ToLowerInvariant();
              if (comboBoxFileFilter.Items.IndexOf(lowerInvariant) == -1)
                comboBoxFileFilter.Items.Add(lowerInvariant);
            }
          }
        }
      }
    }

    private void FillDifferentItemsList(
      SortedDictionary<string, Item> items1,
      SortedDictionary<string, Item> items2,
      VirtualListView listViewToModify,
      VirtualListView listView)
    {
      var flag = comboBoxFileFilter.SelectedIndex > 0;
      var str1 = flag ? comboBoxFileFilter.SelectedItem.ToString() : string.Empty;
      foreach (var key in items2.Keys)
      {
        if (!items1.ContainsKey(key))
        {
          if (flag)
          {
            var num = key.Length - str1.Length;
            if (key.IndexOf(str1, num - 1, StringComparison.OrdinalIgnoreCase) != num)
              continue;
          }
          var str2 = key;
          var listViewItem1 = new ListViewItem(str2, str2);
          listViewItem1.SubItems.Add("None");
          var listViewItem2 = listView.Find(key);
          listViewToModify.InsertItem(listViewItem1, (int) listViewItem2.Tag);
          LabelAsAbsent(listViewItem1, listViewItem2);
        }
      }
    }

    private void checkBoxDifference_CheckedChanged(object sender, EventArgs e)
    {
      if (tabControl1.SelectedIndex == 0)
        CompareItems();
      else if (tabControl1.SelectedIndex == 2)
      {
        CompareItems();
        CompareWorkItems();
      }
      else
      {
        if (tabControl1.SelectedIndex != 1)
          return;
        CompareItems();
        CompareChangesets();
      }
    }

    private void comboBoxFileFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
      CompareItems();
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
      var componentResourceManager = new ComponentResourceManager(typeof (FormCompareLabels));
      tabControl1 = new TabControl();
      tabPage1 = new TabPage();
      splitContainerItems = new SplitContainer();
      listViewItems1 = new VirtualListView();
      columnHeader1 = new ColumnHeader();
      columnHeader2 = new ColumnHeader();
      contextMenuStrip1 = new ContextMenuStrip(components);
      compareVersionsToolStripMenuItem = new ToolStripMenuItem();
      viewRelativeHistoryToolStripMenuItem = new ToolStripMenuItem();
      toolStrip11 = new ToolStrip();
      toolStripLabel4 = new ToolStripLabel();
      toolStripLabel11 = new ToolStripLabel();
      toolStripSeparator5 = new ToolStripSeparator();
      toolStripButtonSave11 = new ToolStripButton();
      listViewItems2 = new VirtualListView();
      columnHeader3 = new ColumnHeader();
      columnHeader4 = new ColumnHeader();
      toolStrip21 = new ToolStrip();
      toolStripLabel7 = new ToolStripLabel();
      toolStripLabel21 = new ToolStripLabel();
      toolStripSeparator6 = new ToolStripSeparator();
      toolStripButtonSave21 = new ToolStripButton();
      tabPage2 = new TabPage();
      splitContainerSets = new SplitContainer();
      listViewSets1 = new VirtualListView();
      columnHeader6 = new ColumnHeader();
      columnHeader15 = new ColumnHeader();
      columnHeader16 = new ColumnHeader();
      toolStrip2 = new ToolStrip();
      toolStripLabel3 = new ToolStripLabel();
      toolStripLabel12 = new ToolStripLabel();
      toolStripSeparator3 = new ToolStripSeparator();
      toolStripButtonSave12 = new ToolStripButton();
      listViewSets2 = new VirtualListView();
      columnHeader8 = new ColumnHeader();
      columnHeader17 = new ColumnHeader();
      columnHeader18 = new ColumnHeader();
      toolStrip3 = new ToolStrip();
      toolStripLabel5 = new ToolStripLabel();
      toolStripLabel22 = new ToolStripLabel();
      toolStripSeparator4 = new ToolStripSeparator();
      toolStripButtonSave22 = new ToolStripButton();
      tabPage3 = new TabPage();
      splitContainerWorkItems = new SplitContainer();
      listViewWorkItems1 = new VirtualListView();
      columnHeader5 = new ColumnHeader();
      columnHeader12 = new ColumnHeader();
      columnHeader13 = new ColumnHeader();
      columnHeader14 = new ColumnHeader();
      toolStrip31 = new ToolStrip();
      toolStripLabel1 = new ToolStripLabel();
      toolStripLabel13 = new ToolStripLabel();
      toolStripSeparator1 = new ToolStripSeparator();
      toolStripButtonSave13 = new ToolStripButton();
      listViewWorkItems2 = new VirtualListView();
      columnHeader7 = new ColumnHeader();
      columnHeader9 = new ColumnHeader();
      columnHeader10 = new ColumnHeader();
      columnHeader11 = new ColumnHeader();
      toolStrip1 = new ToolStrip();
      toolStripLabel2 = new ToolStripLabel();
      toolStripLabel23 = new ToolStripLabel();
      toolStripSeparator2 = new ToolStripSeparator();
      toolStripButtonSave23 = new ToolStripButton();
      panel1 = new Panel();
      labelFilterByFile = new Label();
      comboBoxFileFilter = new ComboBox();
      checkBoxDifference = new CheckBox();
      buttonCancel = new Button();
      saveFileDialog = new SaveFileDialog();
      tabControl1.SuspendLayout();
      tabPage1.SuspendLayout();
      splitContainerItems.Panel1.SuspendLayout();
      splitContainerItems.Panel2.SuspendLayout();
      splitContainerItems.SuspendLayout();
      contextMenuStrip1.SuspendLayout();
      toolStrip11.SuspendLayout();
      toolStrip21.SuspendLayout();
      tabPage2.SuspendLayout();
      splitContainerSets.Panel1.SuspendLayout();
      splitContainerSets.Panel2.SuspendLayout();
      splitContainerSets.SuspendLayout();
      toolStrip2.SuspendLayout();
      toolStrip3.SuspendLayout();
      tabPage3.SuspendLayout();
      splitContainerWorkItems.Panel1.SuspendLayout();
      splitContainerWorkItems.Panel2.SuspendLayout();
      splitContainerWorkItems.SuspendLayout();
      toolStrip31.SuspendLayout();
      toolStrip1.SuspendLayout();
      panel1.SuspendLayout();
      SuspendLayout();
      tabControl1.Controls.Add(tabPage1);
      tabControl1.Controls.Add(tabPage2);
      tabControl1.Controls.Add(tabPage3);
      tabControl1.Dock = DockStyle.Fill;
      tabControl1.Location = new Point(0, 0);
      tabControl1.Name = "tabControl1";
      tabControl1.SelectedIndex = 0;
      tabControl1.Size = new Size(687, 460);
      tabControl1.TabIndex = 0;
      tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
      tabPage1.BackColor = SystemColors.Control;
      tabPage1.Controls.Add(splitContainerItems);
      tabPage1.Location = new Point(4, 22);
      tabPage1.Name = "tabPage1";
      tabPage1.Padding = new Padding(3);
      tabPage1.Size = new Size(679, 434);
      tabPage1.TabIndex = 0;
      tabPage1.Text = "Items";
      splitContainerItems.Dock = DockStyle.Fill;
      splitContainerItems.Location = new Point(3, 3);
      splitContainerItems.Name = "splitContainerItems";
      splitContainerItems.Panel1.Controls.Add(listViewItems1);
      splitContainerItems.Panel1.Controls.Add(toolStrip11);
      splitContainerItems.Panel2.Controls.Add(listViewItems2);
      splitContainerItems.Panel2.Controls.Add(toolStrip21);
      splitContainerItems.Size = new Size(673, 428);
      splitContainerItems.SplitterDistance = 224;
      splitContainerItems.TabIndex = 0;
      listViewItems1.Columns.AddRange(new ColumnHeader[2]
      {
        columnHeader1,
        columnHeader2
      });
      listViewItems1.ContextMenuStrip = contextMenuStrip1;
      listViewItems1.Dock = DockStyle.Fill;
      listViewItems1.FullRowSelect = true;
      listViewItems1.GridLines = true;
      listViewItems1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
      listViewItems1.HideSelection = false;
      listViewItems1.Location = new Point(0, 25);
      listViewItems1.MultiSelect = false;
      listViewItems1.Name = "listViewItems1";
      listViewItems1.Size = new Size(224, 403);
      listViewItems1.Sorted = false;
      listViewItems1.TabIndex = 3;
      listViewItems1.UseCompatibleStateImageBehavior = false;
      listViewItems1.View = View.Details;
      listViewItems1.SelectedIndexChanged += listViewItems1_SelectedIndexChanged;
      columnHeader1.Text = "Item";
      columnHeader1.Width = 220;
      columnHeader2.Text = "Changeset";
      columnHeader2.Width = 80;
      contextMenuStrip1.Items.AddRange(new ToolStripItem[2]
      {
        compareVersionsToolStripMenuItem,
        viewRelativeHistoryToolStripMenuItem
      });
      contextMenuStrip1.Name = "contextMenuStrip1";
      contextMenuStrip1.Size = new Size(188, 48);
      compareVersionsToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("compareVersionsToolStripMenuItem.Image");
      compareVersionsToolStripMenuItem.Name = "compareVersionsToolStripMenuItem";
      compareVersionsToolStripMenuItem.Size = new Size(187, 22);
      compareVersionsToolStripMenuItem.Text = "Compare Versions...";
      compareVersionsToolStripMenuItem.Click += compareVersionsToolStripMenuItem_Click;
      viewRelativeHistoryToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("viewRelativeHistoryToolStripMenuItem.Image");
      viewRelativeHistoryToolStripMenuItem.Name = "viewRelativeHistoryToolStripMenuItem";
      viewRelativeHistoryToolStripMenuItem.Size = new Size(187, 22);
      viewRelativeHistoryToolStripMenuItem.Text = "View Relative History...";
      viewRelativeHistoryToolStripMenuItem.Click += viewRelativeHistoryToolStripMenuItem_Click;
      toolStrip11.GripStyle = ToolStripGripStyle.Hidden;
      toolStrip11.Items.AddRange(new ToolStripItem[4]
      {
        toolStripLabel4,
        toolStripLabel11,
        toolStripSeparator5,
        toolStripButtonSave11
      });
      toolStrip11.Location = new Point(0, 0);
      toolStrip11.Name = "toolStrip11";
      toolStrip11.RenderMode = ToolStripRenderMode.System;
      toolStrip11.Size = new Size(224, 25);
      toolStrip11.TabIndex = 6;
      toolStrip11.Text = "toolStrip12";
      toolStripLabel4.Name = "toolStripLabel4";
      toolStripLabel4.Size = new Size(39, 22);
      toolStripLabel4.Text = " Label:";
      toolStripLabel11.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
      toolStripLabel11.Name = "toolStripLabel11";
      toolStripLabel11.Size = new Size(47, 22);
      toolStripLabel11.Text = " Label1";
      toolStripSeparator5.Name = "toolStripSeparator5";
      toolStripSeparator5.Size = new Size(6, 25);
      toolStripButtonSave11.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonSave11.Image = (Image) componentResourceManager.GetObject("toolStripButtonSave11.Image");
      toolStripButtonSave11.ImageTransparentColor = Color.Magenta;
      toolStripButtonSave11.Name = "toolStripButtonSave11";
      toolStripButtonSave11.Size = new Size(23, 22);
      toolStripButtonSave11.Text = "Save labeled items list";
      toolStripButtonSave11.Click += toolStripButtonSaveClick;
      listViewItems2.Columns.AddRange(new ColumnHeader[2]
      {
        columnHeader3,
        columnHeader4
      });
      listViewItems2.ContextMenuStrip = contextMenuStrip1;
      listViewItems2.Dock = DockStyle.Fill;
      listViewItems2.FullRowSelect = true;
      listViewItems2.GridLines = true;
      listViewItems2.HeaderStyle = ColumnHeaderStyle.Nonclickable;
      listViewItems2.HideSelection = false;
      listViewItems2.Location = new Point(0, 25);
      listViewItems2.MultiSelect = false;
      listViewItems2.Name = "listViewItems2";
      listViewItems2.Size = new Size(445, 403);
      listViewItems2.Sorted = false;
      listViewItems2.TabIndex = 4;
      listViewItems2.UseCompatibleStateImageBehavior = false;
      listViewItems2.View = View.Details;
      listViewItems2.SelectedIndexChanged += listViewItems2_SelectedIndexChanged;
      columnHeader3.Text = "Item";
      columnHeader3.Width = 220;
      columnHeader4.Text = "Changeset";
      columnHeader4.Width = 80;
      toolStrip21.GripStyle = ToolStripGripStyle.Hidden;
      toolStrip21.Items.AddRange(new ToolStripItem[4]
      {
        toolStripLabel7,
        toolStripLabel21,
        toolStripSeparator6,
        toolStripButtonSave21
      });
      toolStrip21.Location = new Point(0, 0);
      toolStrip21.Name = "toolStrip21";
      toolStrip21.RenderMode = ToolStripRenderMode.System;
      toolStrip21.Size = new Size(445, 25);
      toolStrip21.TabIndex = 6;
      toolStrip21.Text = "toolStrip12";
      toolStripLabel7.Name = "toolStripLabel7";
      toolStripLabel7.Size = new Size(39, 22);
      toolStripLabel7.Text = " Label:";
      toolStripLabel21.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
      toolStripLabel21.Name = "toolStripLabel21";
      toolStripLabel21.Size = new Size(47, 22);
      toolStripLabel21.Text = " Label2";
      toolStripSeparator6.Name = "toolStripSeparator6";
      toolStripSeparator6.Size = new Size(6, 25);
      toolStripButtonSave21.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonSave21.Image = (Image) componentResourceManager.GetObject("toolStripButtonSave21.Image");
      toolStripButtonSave21.ImageTransparentColor = Color.Magenta;
      toolStripButtonSave21.Name = "toolStripButtonSave21";
      toolStripButtonSave21.Size = new Size(23, 22);
      toolStripButtonSave21.Text = "Save labeled items list";
      toolStripButtonSave21.Click += toolStripButtonSaveClick;
      tabPage2.BackColor = SystemColors.Control;
      tabPage2.Controls.Add(splitContainerSets);
      tabPage2.Location = new Point(4, 22);
      tabPage2.Name = "tabPage2";
      tabPage2.Padding = new Padding(3);
      tabPage2.Size = new Size(679, 434);
      tabPage2.TabIndex = 1;
      tabPage2.Text = "Changesets";
      splitContainerSets.Dock = DockStyle.Fill;
      splitContainerSets.Location = new Point(3, 3);
      splitContainerSets.Name = "splitContainerSets";
      splitContainerSets.Panel1.Controls.Add(listViewSets1);
      splitContainerSets.Panel1.Controls.Add(toolStrip2);
      splitContainerSets.Panel2.Controls.Add(listViewSets2);
      splitContainerSets.Panel2.Controls.Add(toolStrip3);
      splitContainerSets.Size = new Size(673, 428);
      splitContainerSets.SplitterDistance = 224;
      splitContainerSets.TabIndex = 1;
      listViewSets1.Columns.AddRange(new ColumnHeader[3]
      {
        columnHeader6,
        columnHeader15,
        columnHeader16
      });
      listViewSets1.Dock = DockStyle.Fill;
      listViewSets1.FullRowSelect = true;
      listViewSets1.GridLines = true;
      listViewSets1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
      listViewSets1.HideSelection = false;
      listViewSets1.Location = new Point(0, 25);
      listViewSets1.MultiSelect = false;
      listViewSets1.Name = "listViewSets1";
      listViewSets1.Size = new Size(224, 403);
      listViewSets1.Sorted = false;
      listViewSets1.TabIndex = 3;
      listViewSets1.UseCompatibleStateImageBehavior = false;
      listViewSets1.View = View.Details;
      listViewSets1.SelectedIndexChanged += listViewSets1_SelectedIndexChanged;
      columnHeader6.Text = "Changeset";
      columnHeader6.Width = 80;
      columnHeader15.Text = "Creation Date";
      columnHeader15.Width = 130;
      columnHeader16.Text = "Comment";
      columnHeader16.Width = 130;
      toolStrip2.GripStyle = ToolStripGripStyle.Hidden;
      toolStrip2.Items.AddRange(new ToolStripItem[4]
      {
        toolStripLabel3,
        toolStripLabel12,
        toolStripSeparator3,
        toolStripButtonSave12
      });
      toolStrip2.Location = new Point(0, 0);
      toolStrip2.Name = "toolStrip2";
      toolStrip2.RenderMode = ToolStripRenderMode.System;
      toolStrip2.Size = new Size(224, 25);
      toolStrip2.TabIndex = 5;
      toolStrip2.Text = "toolStrip12";
      toolStripLabel3.Name = "toolStripLabel3";
      toolStripLabel3.Size = new Size(39, 22);
      toolStripLabel3.Text = " Label:";
      toolStripLabel12.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
      toolStripLabel12.Name = "toolStripLabel12";
      toolStripLabel12.Size = new Size(47, 22);
      toolStripLabel12.Text = " Label1";
      toolStripSeparator3.Name = "toolStripSeparator3";
      toolStripSeparator3.Size = new Size(6, 25);
      toolStripButtonSave12.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonSave12.Image = (Image) componentResourceManager.GetObject("toolStripButtonSave12.Image");
      toolStripButtonSave12.ImageTransparentColor = Color.Magenta;
      toolStripButtonSave12.Name = "toolStripButtonSave12";
      toolStripButtonSave12.Size = new Size(23, 22);
      toolStripButtonSave12.Text = "Save labeled items list";
      toolStripButtonSave12.Click += toolStripButtonSaveClick;
      listViewSets2.Columns.AddRange(new ColumnHeader[3]
      {
        columnHeader8,
        columnHeader17,
        columnHeader18
      });
      listViewSets2.Dock = DockStyle.Fill;
      listViewSets2.FullRowSelect = true;
      listViewSets2.GridLines = true;
      listViewSets2.HeaderStyle = ColumnHeaderStyle.Nonclickable;
      listViewSets2.HideSelection = false;
      listViewSets2.Location = new Point(0, 25);
      listViewSets2.MultiSelect = false;
      listViewSets2.Name = "listViewSets2";
      listViewSets2.Size = new Size(445, 403);
      listViewSets2.Sorted = false;
      listViewSets2.TabIndex = 4;
      listViewSets2.UseCompatibleStateImageBehavior = false;
      listViewSets2.View = View.Details;
      listViewSets2.SelectedIndexChanged += listViewSets2_SelectedIndexChanged;
      columnHeader8.Text = "Changeset";
      columnHeader8.Width = 80;
      columnHeader17.Text = "Creation Date";
      columnHeader17.Width = 130;
      columnHeader18.Text = "Comment";
      columnHeader18.Width = 130;
      toolStrip3.GripStyle = ToolStripGripStyle.Hidden;
      toolStrip3.Items.AddRange(new ToolStripItem[4]
      {
        toolStripLabel5,
        toolStripLabel22,
        toolStripSeparator4,
        toolStripButtonSave22
      });
      toolStrip3.Location = new Point(0, 0);
      toolStrip3.Name = "toolStrip3";
      toolStrip3.RenderMode = ToolStripRenderMode.System;
      toolStrip3.Size = new Size(445, 25);
      toolStrip3.TabIndex = 5;
      toolStrip3.Text = "toolStrip22";
      toolStripLabel5.Name = "toolStripLabel5";
      toolStripLabel5.Size = new Size(39, 22);
      toolStripLabel5.Text = " Label:";
      toolStripLabel22.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
      toolStripLabel22.Name = "toolStripLabel22";
      toolStripLabel22.Size = new Size(47, 22);
      toolStripLabel22.Text = " Label2";
      toolStripSeparator4.Name = "toolStripSeparator4";
      toolStripSeparator4.Size = new Size(6, 25);
      toolStripButtonSave22.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonSave22.Image = (Image) componentResourceManager.GetObject("toolStripButtonSave22.Image");
      toolStripButtonSave22.ImageTransparentColor = Color.Magenta;
      toolStripButtonSave22.Name = "toolStripButtonSave22";
      toolStripButtonSave22.Size = new Size(23, 22);
      toolStripButtonSave22.Text = "Save labeled items list";
      toolStripButtonSave22.Click += toolStripButtonSaveClick;
      tabPage3.BackColor = SystemColors.Control;
      tabPage3.Controls.Add(splitContainerWorkItems);
      tabPage3.Location = new Point(4, 22);
      tabPage3.Name = "tabPage3";
      tabPage3.Padding = new Padding(3);
      tabPage3.Size = new Size(679, 434);
      tabPage3.TabIndex = 2;
      tabPage3.Text = "Work Items";
      splitContainerWorkItems.Dock = DockStyle.Fill;
      splitContainerWorkItems.Location = new Point(3, 3);
      splitContainerWorkItems.Name = "splitContainerWorkItems";
      splitContainerWorkItems.Panel1.Controls.Add(listViewWorkItems1);
      splitContainerWorkItems.Panel1.Controls.Add(toolStrip31);
      splitContainerWorkItems.Panel2.Controls.Add(listViewWorkItems2);
      splitContainerWorkItems.Panel2.Controls.Add(toolStrip1);
      splitContainerWorkItems.Size = new Size(673, 428);
      splitContainerWorkItems.SplitterDistance = 224;
      splitContainerWorkItems.TabIndex = 2;
      listViewWorkItems1.Columns.AddRange(new ColumnHeader[4]
      {
        columnHeader5,
        columnHeader12,
        columnHeader13,
        columnHeader14
      });
      listViewWorkItems1.Dock = DockStyle.Fill;
      listViewWorkItems1.FullRowSelect = true;
      listViewWorkItems1.GridLines = true;
      listViewWorkItems1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
      listViewWorkItems1.HideSelection = false;
      listViewWorkItems1.Location = new Point(0, 25);
      listViewWorkItems1.MultiSelect = false;
      listViewWorkItems1.Name = "listViewWorkItems1";
      listViewWorkItems1.Size = new Size(224, 403);
      listViewWorkItems1.Sorted = false;
      listViewWorkItems1.TabIndex = 3;
      listViewWorkItems1.UseCompatibleStateImageBehavior = false;
      listViewWorkItems1.View = View.Details;
      listViewWorkItems1.SelectedIndexChanged += listViewWorkItems1_SelectedIndexChanged;
      columnHeader5.Text = "ID";
      columnHeader12.Text = "Title";
      columnHeader12.Width = 130;
      columnHeader13.Text = "Type";
      columnHeader13.Width = 80;
      columnHeader14.Text = "State";
      columnHeader14.Width = 80;
      toolStrip31.GripStyle = ToolStripGripStyle.Hidden;
      toolStrip31.Items.AddRange(new ToolStripItem[4]
      {
        toolStripLabel1,
        toolStripLabel13,
        toolStripSeparator1,
        toolStripButtonSave13
      });
      toolStrip31.Location = new Point(0, 0);
      toolStrip31.Name = "toolStrip31";
      toolStrip31.RenderMode = ToolStripRenderMode.System;
      toolStrip31.Size = new Size(224, 25);
      toolStrip31.TabIndex = 5;
      toolStrip31.Text = "toolStrip1";
      toolStripLabel1.Name = "toolStripLabel1";
      toolStripLabel1.Size = new Size(39, 22);
      toolStripLabel1.Text = " Label:";
      toolStripLabel13.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
      toolStripLabel13.Name = "toolStripLabel13";
      toolStripLabel13.Size = new Size(47, 22);
      toolStripLabel13.Text = " Label1";
      toolStripSeparator1.Name = "toolStripSeparator1";
      toolStripSeparator1.Size = new Size(6, 25);
      toolStripButtonSave13.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonSave13.Image = (Image) componentResourceManager.GetObject("toolStripButtonSave13.Image");
      toolStripButtonSave13.ImageTransparentColor = Color.Magenta;
      toolStripButtonSave13.Name = "toolStripButtonSave13";
      toolStripButtonSave13.Size = new Size(23, 22);
      toolStripButtonSave13.Text = "Save labeled items list";
      toolStripButtonSave13.Click += toolStripButtonSaveClick;
      listViewWorkItems2.Columns.AddRange(new ColumnHeader[4]
      {
        columnHeader7,
        columnHeader9,
        columnHeader10,
        columnHeader11
      });
      listViewWorkItems2.Dock = DockStyle.Fill;
      listViewWorkItems2.FullRowSelect = true;
      listViewWorkItems2.GridLines = true;
      listViewWorkItems2.HeaderStyle = ColumnHeaderStyle.Nonclickable;
      listViewWorkItems2.HideSelection = false;
      listViewWorkItems2.Location = new Point(0, 25);
      listViewWorkItems2.MultiSelect = false;
      listViewWorkItems2.Name = "listViewWorkItems2";
      listViewWorkItems2.Size = new Size(445, 403);
      listViewWorkItems2.Sorted = false;
      listViewWorkItems2.TabIndex = 4;
      listViewWorkItems2.UseCompatibleStateImageBehavior = false;
      listViewWorkItems2.View = View.Details;
      listViewWorkItems2.SelectedIndexChanged += listViewWorkItems2_SelectedIndexChanged;
      columnHeader7.Text = "ID";
      columnHeader9.Text = "Title";
      columnHeader9.Width = 130;
      columnHeader10.Text = "Type";
      columnHeader10.Width = 80;
      columnHeader11.Text = "State";
      columnHeader11.Width = 80;
      toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
      toolStrip1.Items.AddRange(new ToolStripItem[4]
      {
        toolStripLabel2,
        toolStripLabel23,
        toolStripSeparator2,
        toolStripButtonSave23
      });
      toolStrip1.Location = new Point(0, 0);
      toolStrip1.Name = "toolStrip1";
      toolStrip1.RenderMode = ToolStripRenderMode.System;
      toolStrip1.Size = new Size(445, 25);
      toolStrip1.TabIndex = 5;
      toolStrip1.Text = "toolStrip1";
      toolStripLabel2.Name = "toolStripLabel2";
      toolStripLabel2.Size = new Size(39, 22);
      toolStripLabel2.Text = " Label:";
      toolStripLabel23.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
      toolStripLabel23.Name = "toolStripLabel23";
      toolStripLabel23.Size = new Size(47, 22);
      toolStripLabel23.Text = " Label2";
      toolStripSeparator2.Name = "toolStripSeparator2";
      toolStripSeparator2.Size = new Size(6, 25);
      toolStripButtonSave23.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonSave23.Image = (Image) componentResourceManager.GetObject("toolStripButtonSave23.Image");
      toolStripButtonSave23.ImageTransparentColor = Color.Magenta;
      toolStripButtonSave23.Name = "toolStripButtonSave23";
      toolStripButtonSave23.Size = new Size(23, 22);
      toolStripButtonSave23.Text = "Save labeled items list";
      toolStripButtonSave23.Click += toolStripButtonSaveClick;
      panel1.Controls.Add(labelFilterByFile);
      panel1.Controls.Add(comboBoxFileFilter);
      panel1.Controls.Add(checkBoxDifference);
      panel1.Controls.Add(buttonCancel);
      panel1.Dock = DockStyle.Bottom;
      panel1.Location = new Point(0, 460);
      panel1.Name = "panel1";
      panel1.Size = new Size(687, 41);
      panel1.TabIndex = 2;
      labelFilterByFile.AutoSize = true;
      labelFilterByFile.Location = new Point(171, 14);
      labelFilterByFile.Name = "labelFilterByFile";
      labelFilterByFile.Size = new Size(59, 13);
      labelFilterByFile.TabIndex = 13;
      labelFilterByFile.Text = "Filter by file";
      comboBoxFileFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      comboBoxFileFilter.AutoCompleteMode = AutoCompleteMode.Suggest;
      comboBoxFileFilter.AutoCompleteSource = AutoCompleteSource.ListItems;
      comboBoxFileFilter.DropDownStyle = ComboBoxStyle.DropDownList;
      comboBoxFileFilter.FormattingEnabled = true;
      comboBoxFileFilter.IntegralHeight = false;
      comboBoxFileFilter.ItemHeight = 13;
      comboBoxFileFilter.Location = new Point(235, 11);
      comboBoxFileFilter.MaxDropDownItems = 32;
      comboBoxFileFilter.Name = "comboBoxFileFilter";
      comboBoxFileFilter.Size = new Size(195, 21);
      comboBoxFileFilter.Sorted = true;
      comboBoxFileFilter.TabIndex = 2;
      checkBoxDifference.AutoSize = true;
      checkBoxDifference.Location = new Point(12, 13);
      checkBoxDifference.Name = "checkBoxDifference";
      checkBoxDifference.Size = new Size(125, 17);
      checkBoxDifference.TabIndex = 1;
      checkBoxDifference.Text = "Show only difference";
      checkBoxDifference.UseVisualStyleBackColor = true;
      checkBoxDifference.CheckedChanged += checkBoxDifference_CheckedChanged;
      buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      buttonCancel.DialogResult = DialogResult.Cancel;
      buttonCancel.Location = new Point(605, 9);
      buttonCancel.Name = "buttonCancel";
      buttonCancel.Size = new Size(75, 23);
      buttonCancel.TabIndex = 0;
      buttonCancel.Text = "Cancel";
      buttonCancel.UseVisualStyleBackColor = true;
      saveFileDialog.DefaultExt = "csv";
      saveFileDialog.Filter = "CSV  files (*.csv)|*.csv|All files (*.*)|*.*";
      saveFileDialog.Title = "Save to file";
      AutoScaleDimensions = new SizeF(6f, 13f);
      AutoScaleMode = AutoScaleMode.Font;
      CancelButton = buttonCancel;
      ClientSize = new Size(687, 501);
      Controls.Add(tabControl1);
      Controls.Add(panel1);
      Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      Name = nameof (FormCompareLabels);
      ShowIcon = false;
      StartPosition = FormStartPosition.CenterScreen;
      Text = "Compare Labels";
      tabControl1.ResumeLayout(false);
      tabPage1.ResumeLayout(false);
      splitContainerItems.Panel1.ResumeLayout(false);
      splitContainerItems.Panel1.PerformLayout();
      splitContainerItems.Panel2.ResumeLayout(false);
      splitContainerItems.Panel2.PerformLayout();
      splitContainerItems.ResumeLayout(false);
      contextMenuStrip1.ResumeLayout(false);
      toolStrip11.ResumeLayout(false);
      toolStrip11.PerformLayout();
      toolStrip21.ResumeLayout(false);
      toolStrip21.PerformLayout();
      tabPage2.ResumeLayout(false);
      splitContainerSets.Panel1.ResumeLayout(false);
      splitContainerSets.Panel1.PerformLayout();
      splitContainerSets.Panel2.ResumeLayout(false);
      splitContainerSets.Panel2.PerformLayout();
      splitContainerSets.ResumeLayout(false);
      toolStrip2.ResumeLayout(false);
      toolStrip2.PerformLayout();
      toolStrip3.ResumeLayout(false);
      toolStrip3.PerformLayout();
      tabPage3.ResumeLayout(false);
      splitContainerWorkItems.Panel1.ResumeLayout(false);
      splitContainerWorkItems.Panel1.PerformLayout();
      splitContainerWorkItems.Panel2.ResumeLayout(false);
      splitContainerWorkItems.Panel2.PerformLayout();
      splitContainerWorkItems.ResumeLayout(false);
      toolStrip31.ResumeLayout(false);
      toolStrip31.PerformLayout();
      toolStrip1.ResumeLayout(false);
      toolStrip1.PerformLayout();
      panel1.ResumeLayout(false);
      panel1.PerformLayout();
      ResumeLayout(false);
    }

    private enum ItemCompared
    {
      Same,
      Different,
      Absent,
    }
  }
}
