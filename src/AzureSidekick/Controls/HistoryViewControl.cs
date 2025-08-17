// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.HistoryViewControl
// Assembly: Attrice.TeamFoundation.Controls.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=1a24c887ab53aa6e
// MVID: 393BE899-3C29-4A70-86D7-B4B965E5A8F5
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controls.12.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Attrice.TeamFoundation.Common;
using Attrice.TeamFoundation.Controllers;
using Attrice.TeamFoundation.Controls.Forms;
using Attrice.TeamFoundation.Controls.Properties;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace Attrice.TeamFoundation.Controls
{
  public class HistoryViewControl : BaseSidekickControl
  {
    private IContainer components;
    private TreeView treeViewSourceCode;
    private SplitContainer splitContainer1;
    private TabControl tabControl1;
    private TabPage tabPageHistory;
    private TabPage tabPageProperties;
    private TabPage tabPageBranches;
    private TabPage tabPageMerges;
    private TabPage tabPageLabels;
    private VirtualListView listViewHistory;
    private ColumnHeader columnHeaderChangeset;
    private ColumnHeader columnHeaderOwner;
    private ColumnHeader columnHeaderDate;
    private ColumnHeader columnHeaderComment;
    private GroupBox groupBox2;
    private Label label3;
    private ComboBox comboBoxUserName;
    private ImageList imageList;
    private ColumnHeader columnHeaderChange;
    private GroupBox groupBoxProperties;
    private PropertyGrid propertyGridItem;
    private GroupBox groupBoxPendingChanges;
    private ListView listViewChanges;
    private ColumnHeader columnHeader1;
    private ColumnHeader columnHeader3;
    private ColumnHeader columnHeader4;
    private ColumnHeader columnHeader5;
    private ColumnHeader columnHeader6;
    private ColumnHeader columnHeader7;
    private GroupBox groupBoxBranches;
    private TreeView treeViewBranches;
    private GroupBox groupBoxBranchProperties;
    private PropertyGrid propertyGridBranch;
    private TableLayoutPanel tableLayoutPanelMerge;
    private GroupBox groupBoxMergeFrom;
    private ListView listViewMergesFrom;
    private ColumnHeader columnHeader2;
    private ColumnHeader columnHeader8;
    private ColumnHeader columnHeader13;
    private ColumnHeader columnHeader14;
    private GroupBox groupBoxMergeTo;
    private ListView listViewMergesTo;
    private ColumnHeader columnHeader15;
    private ColumnHeader columnHeader11;
    private ColumnHeader columnHeader12;
    private ColumnHeader columnHeader9;
    private ColumnHeader columnHeader10;
    private ColumnHeader columnHeader16;
    private ContextMenuStrip contextMenuStripMergeTo;
    private ToolStripMenuItem compareToolStripMenuItem;
    private ContextMenuStrip contextMenuStripMergeFrom;
    private ToolStripMenuItem toolStripMenuItem1;
    private TabPage tabPageMergeCandidates;
    private GroupBox groupBoxCandidates;
    private Label label4;
    private Label label1;
    private TextBox textBoxMergeTo;
    private ComboBox comboBoxMergeFrom;
    private ColumnHeader columnHeader17;
    private ColumnHeader columnHeader19;
    private ColumnHeader columnHeader18;
    private ContextMenuStrip contextMenuStripMergeCandidate;
    private ToolStripMenuItem toolStripMenuItem2;
    private GroupBox groupBoxLabel;
    private Label label5;
    private ComboBox comboBoxUserNameLabel;
    private ToolStrip toolStrip1;
    private ToolStripButton toolStripButtonRefresh;
    private ToolStripButton toolStripButtonFindItem;
    private ContextMenuStrip contextMenuStripLabels;
    private ToolStripMenuItem toolStripMenuItemCompareLabels;
    private ContextMenuStrip contextMenuStripHistory;
    private ToolStripMenuItem toolStripMenuItemCompareTwoVersions;
    private SplitContainer splitContainerMergeCandidates;
    private TreeView treeViewMergeTargets;
    private ListView listViewMergeReport;
    private ColumnHeader columnHeader20;
    private ColumnHeader columnHeader21;
    private ColumnHeader columnHeader23;
    private ColumnHeader columnHeader24;
    private ToolStrip toolStrip2;
    private ToolStripButton toolStripButtonSaveToFile;
    private ToolStripMenuItem toolStripMenuItemCompareLatest;
    private ToolStripMenuItem toolStripMenuItemCompareLabelLatest;
    private ToolStrip toolStrip3;
    private ToolStripButton toolStripButtonToggleTreeMode;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripButton toolStripButtonSaveList;
    private ColumnHeader columnHeader29;
    private ToolStripButton toolStripButtonViewChangesetDetails;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripButton toolStripButtonChangesetDetails;
    private VirtualListView listViewLabels;
    private ColumnHeader columnHeader28;
    private ColumnHeader columnHeader22;
    private ColumnHeader columnHeader25;
    private ColumnHeader columnHeader26;
    private ColumnHeader columnHeader27;
    private ToolStrip toolStrip4;
    private ToolStripButton toolStripButtonChangesetDetails3;
    private ToolStripButton toolStripButton1;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripButton toolStripButton2;
    private ToolStripMenuItem viewChangesetDetailsToolStripMenuItem;
    private ToolStripMenuItem viewChangesetDetailsToolStripMenuItem1;
    private ToolStripMenuItem viewSourceChangesetDetailsToolStripMenuItem;
    private ToolStripMenuItem viewTargetChangesetDetailsToolStripMenuItem;
    private ToolStripMenuItem viewSourceChangesetDetailsToolStripMenuItem1;
    private ToolStripMenuItem viewTargetChangesetDetailsToolStripMenuItem1;
    private ToolStripMenuItem viewChangesetDetailsToolStripMenuItem2;
    private HistoryViewController _controller;
    private DataTable _dtUsers;
    private DataTable _dtProjects;
    private const string cAllID = "All";

    private void ShowBranchesTab()
    {
      if (!SetCurrentPage(tabPageBranches))
        return;
      var selectedItem = GetSelectedItem();
      if (selectedItem == null)
        return;
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        treeViewBranches.BeginUpdate();
        treeViewBranches.Nodes.Clear();
        propertyGridBranch.SelectedObject = null;
        var itemBranches = _controller.GetItemBranches(selectedItem);
        if (itemBranches.Length == 0)
          return;
        var branchHistoryTreeItemArray = itemBranches[0];
        if (branchHistoryTreeItemArray.Length == 0)
          return;
        var branchToItem = branchHistoryTreeItemArray[0].Relative.BranchToItem;
        TreeNode node;
        if (branchToItem == null)
        {
          node = treeViewBranches.Nodes.Add("[no permissions to read]");
        }
        else
        {
          node = treeViewBranches.Nodes.Add(branchToItem.ServerItem);
          node.Tag = branchHistoryTreeItemArray[0];
        }
        SetBranchNodeSelected(node, branchHistoryTreeItemArray[0]);
        SetNodeImages(node, branchToItem, selectedItem);
        AddBranches(node, selectedItem, branchHistoryTreeItemArray[0].Children);
        treeViewBranches.Nodes[0].ExpandAll();
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to retrieve branches" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        treeViewBranches.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
    }

    private void SetBranchNodeSelected(TreeNode node, BranchHistoryTreeItem item)
    {
      if (!item.Relative.IsRequestedItem)
        return;
      node.TreeView.SelectedNode = node;
      var treeNode = node;
      treeNode.NodeFont = new Font(treeNode.TreeView.Font.FontFamily, node.TreeView.Font.Size, FontStyle.Bold);
    }

    private void SetNodeImages(TreeNode node, Item itemBranch, Item itemParent)
    {
      if (itemParent.ItemType == ItemType.Folder)
      {
        node.ImageIndex = 2;
        node.SelectedImageIndex = 3;
      }
      else
      {
        var treeNode = node;
        int num1;
        var num2 = num1 = 4;
        treeNode.SelectedImageIndex = num1;
        treeNode.ImageIndex = num2;
      }
      if (itemBranch == null || itemBranch.DeletionId == 0)
        return;
      node.StateImageIndex = 5;
    }

    private void AddBranches(TreeNode node, Item parentItem, ICollection items)
    {
      if (items == null)
        return;
      foreach (BranchHistoryTreeItem branchHistoryTreeItem in items)
      {
        var branchToItem = branchHistoryTreeItem.Relative.BranchToItem;
        var node1 = branchToItem != null ? node.Nodes.Add(branchToItem.ServerItem) : node.Nodes.Add("[no permissions to read]");
        node1.Tag = branchHistoryTreeItem;
        SetNodeImages(node1, branchToItem, parentItem);
        SetBranchNodeSelected(node1, branchHistoryTreeItem);
        AddBranches(node1, parentItem, branchHistoryTreeItem.Children);
      }
    }

    private void ShowBranchProperties(TreeNode node)
    {
      propertyGridBranch.SelectedObject = new BranchProperties(node.Tag as BranchHistoryTreeItem);
    }

    private void ShowHistoryTab(bool forceRefresh)
    {
      if (!SetCurrentPage(tabPageHistory) && !forceRefresh)
        return;
      var selectedItem = GetSelectedItem();
      if (selectedItem == null)
      {
        listViewHistory.ClearItems();
      }
      else
      {
        listViewHistory.ContextMenuStrip = contextMenuStripHistory;
        try
        {
          Cursor.Current = Cursors.WaitCursor;
          listViewHistory.BeginUpdate();
          var userName = comboBoxUserName.SelectedValue == null ? comboBoxUserName.Text : comboBoxUserName.SelectedValue.ToString();
          if (userName == "All" || userName == "")
            userName = null;
          var changesetArray = _controller.SearchHistory(selectedItem, userName);
          listViewHistory.SetCapacity(changesetArray.Length);
          foreach (var changeset in changesetArray)
          {
            var index = changeset.ChangesetId;
            var listViewItem = new ListViewItem(index.ToString());
            ChangeType changeType;
            if (selectedItem.ItemType == ItemType.File)
            {
              var subItems = listViewItem.SubItems;
              changeType = changeset.Changes[0].ChangeType;
              var text = changeType.ToString();
              subItems.Add(text);
            }
            else
            {
              var flag = false;
              var changes = changeset.Changes;
              for (index = 0; index < changes.Length; ++index)
              {
                var change = changes[index];
                if (change.Item.ServerItem == selectedItem.ServerItem)
                {
                  flag = true;
                  var subItems = listViewItem.SubItems;
                  changeType = change.ChangeType;
                  var text = changeType.ToString();
                  subItems.Add(text);
                }
              }
              if (!flag)
                listViewItem.SubItems.Add("Contained change");
            }
            listViewItem.SubItems.Add(Utilities.GetTableValueByID(_dtUsers, changeset.Owner));
            listViewItem.SubItems.Add(Utilities.FormatDateTimeInvariant(changeset.CreationDate));
            listViewItem.SubItems.Add(changeset.Comment);
            listViewHistory.AddItem(listViewItem);
          }
        }
        catch (Exception ex)
        {
          var num = (int) MessageBox.Show("Failed to retrieve history." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        finally
        {
          try
          {
            listViewHistory.EndUpdate();
          }
          catch
          {
          }
          Cursor.Current = Cursors.Default;
        }
      }
    }

    private void CompareHistory()
    {
      var selectedItem = GetSelectedItem();
      var text = listViewHistory.Items[listViewHistory.SelectedIndices[0]].SubItems[0].Text;
      var toVersion = listViewHistory.SelectedIndices.Count != 2 ? VersionSpec.Latest : new ChangesetVersionSpec(listViewHistory.Items[listViewHistory.SelectedIndices[1]].SubItems[0].Text);
      Cursor.Current = Cursors.WaitCursor;
      try
      {
        _controller.CompareFiles(selectedItem.ServerItem, new ChangesetVersionSpec(text), selectedItem.ServerItem, toVersion);
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Compare operation failed." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
    }

    private void ShowLabelsTab(bool forceRefresh)
    {
      if (!SetCurrentPage(tabPageLabels) && !forceRefresh)
        return;
      var selectedItem = GetSelectedItem();
      if (selectedItem == null)
        return;
      listViewLabels.ContextMenuStrip = contextMenuStripLabels;
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        listViewLabels.BeginUpdate();
        var owner = comboBoxUserNameLabel.SelectedValue == null ? comboBoxUserNameLabel.Text : comboBoxUserNameLabel.SelectedValue.ToString();
        if (owner == "All" || owner == "")
          owner = null;
        var itemLabels = _controller.GetItemLabels(selectedItem, owner);
        listViewLabels.SetCapacity(itemLabels.Length);
        foreach (var versionControlLabel in itemLabels)
        {
          var text = "Unknown";
          foreach (var obj in versionControlLabel.Items)
          {
            if (obj.ServerItem == selectedItem.ServerItem)
            {
              text = obj.ChangesetId.ToString();
              break;
            }
          }
          listViewLabels.AddItem(new ListViewItem(text)
          {
            SubItems = {
              versionControlLabel.Name,
              Utilities.GetTableValueByID(_dtUsers, versionControlLabel.OwnerName),
              Utilities.FormatDateTimeInvariant(versionControlLabel.LastModifiedDate),
              versionControlLabel.Comment
            },
            Tag = versionControlLabel
          });
        }
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

    public void CompareLabels()
    {
      var selectedItem = GetSelectedItem();
      var text = listViewLabels.Items[listViewLabels.SelectedIndices[0]].SubItems[1].Text;
      var toVersion = listViewLabels.SelectedIndices.Count != 1 ? new LabelVersionSpec(listViewLabels.Items[listViewLabels.SelectedIndices[1]].SubItems[1].Text) : VersionSpec.Latest;
      Cursor.Current = Cursors.WaitCursor;
      try
      {
        _controller.CompareFiles(selectedItem.ServerItem, new LabelVersionSpec(text), selectedItem.ServerItem, toVersion);
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Compare operation failed." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
    }

    private void ShowMergesTab()
    {
      if (!SetCurrentPage(tabPageMerges))
        return;
      var selectedItem = GetSelectedItem();
      if (selectedItem == null)
        return;
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        listViewMergesTo.BeginUpdate();
        listViewMergesFrom.BeginUpdate();
        listViewMergesTo.Items.Clear();
        listViewMergesFrom.Items.Clear();
        var branchRelatives = _controller.GetBranchRelatives(selectedItem);
        FillMergesToItem(selectedItem, branchRelatives);
        FillMergesFromItem(selectedItem, branchRelatives);
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to retrieve merge history." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        listViewMergesTo.EndUpdate();
        listViewMergesFrom.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
    }

    private void FillMergesFromItem(Item item, Item[] relatedItems)
    {
      foreach (var relatedItem in relatedItems)
      {
        foreach (var itemMerge in _controller.GetItemMerges(relatedItem, item, VersionSpec.Latest))
        {
          var flag1 = false;
          var changes1 = _controller.GetChangeset(itemMerge.SourceVersion).Changes;
          int index;
          for (index = 0; index < changes1.Length; ++index)
          {
            if (changes1[index].Item.ServerItem == item.ServerItem)
            {
              flag1 = true;
              break;
            }
          }
          if (flag1)
          {
            var flag2 = false;
            var changes2 = _controller.GetChangeset(itemMerge.TargetVersion).Changes;
            for (index = 0; index < changes2.Length; ++index)
            {
              var change = changes2[index];
              if ((change.ChangeType & ChangeType.Merge) > 0 && change.Item.ServerItem == relatedItem.ServerItem)
              {
                flag2 = true;
                break;
              }
            }
            if (flag2)
            {
              var listViewItem = new ListViewItem(relatedItem.ServerItem);
              var subItems1 = listViewItem.SubItems;
              index = itemMerge.SourceVersion;
              var text1 = index.ToString();
              subItems1.Add(text1);
              var subItems2 = listViewItem.SubItems;
              index = itemMerge.TargetVersion;
              var text2 = index.ToString();
              subItems2.Add(text2);
              listViewItem.SubItems.Add(Utilities.GetTableValueByID(_dtUsers, itemMerge.TargetChangeset.Owner));
              listViewItem.SubItems.Add(Utilities.FormatDateTimeInvariant(itemMerge.TargetChangeset.CreationDate));
              listViewMergesFrom.Items.Add(listViewItem);
            }
          }
        }
      }
    }

    private void FillMergesToItem(Item item, Item[] relatedItems)
    {
      Changeset changeset = null;
      foreach (var itemMerge in _controller.GetItemMerges(item))
      {
        var flag = false;
        if (changeset == null || changeset.ChangesetId != itemMerge.TargetVersion)
          changeset = _controller.GetChangeset(itemMerge.TargetVersion);
        var changes1 = changeset.Changes;
        int index;
        for (index = 0; index < changes1.Length; ++index)
        {
          var change = changes1[index];
          if ((change.ChangeType & ChangeType.Merge) > 0 && change.Item.ServerItem == item.ServerItem)
          {
            flag = true;
            break;
          }
        }
        if (flag)
        {
          var objList = new List<Item>();
          var changes2 = _controller.GetChangeset(itemMerge.SourceVersion).Changes;
          for (index = 0; index < changes2.Length; ++index)
          {
            var change = changes2[index];
            foreach (var relatedItem in relatedItems)
            {
              if (change.Item.ServerItem == relatedItem.ServerItem)
                objList.Add(change.Item);
            }
          }
          string text1 = null;
          if (objList.Count == 1)
          {
            text1 = objList[0].ServerItem;
          }
          else
          {
            foreach (var sourceItem in objList)
            {
              try
              {
                var itemMerges = _controller.GetItemMerges(item, sourceItem, new ChangesetVersionSpec(itemMerge.SourceVersion));
                for (index = 0; index < itemMerges.Length; ++index)
                {
                  if (itemMerges[index].TargetVersion == itemMerge.TargetVersion)
                  {
                    text1 = sourceItem.ServerItem;
                    break;
                  }
                }
                if (text1 != null)
                  break;
              }
              catch
              {
                break;
              }
            }
          }
          if (text1 == null)
          {
            if (item.ItemType != ItemType.Folder)
              text1 = "Unknown contributor";
            else
              continue;
          }
          var listViewItem = new ListViewItem(text1);
          var subItems1 = listViewItem.SubItems;
          index = itemMerge.SourceVersion;
          var text2 = index.ToString();
          subItems1.Add(text2);
          var subItems2 = listViewItem.SubItems;
          index = itemMerge.TargetVersion;
          var text3 = index.ToString();
          subItems2.Add(text3);
          listViewItem.SubItems.Add(Utilities.GetTableValueByID(_dtUsers, itemMerge.TargetChangeset.Owner));
          listViewItem.SubItems.Add(Utilities.FormatDateTimeInvariant(itemMerge.TargetChangeset.CreationDate));
          listViewMergesTo.Items.Add(listViewItem);
        }
      }
    }

    private void CompareVersions(ListView listView)
    {
      if (listView.SelectedIndices.Count != 1)
        return;
      var selectedItem1 = GetSelectedItem();
      if (selectedItem1.ItemType == ItemType.Folder)
        return;
      var selectedItem2 = listView.SelectedItems[0];
      var text1 = selectedItem2.SubItems[0].Text;
      var text2 = selectedItem2.SubItems[1].Text;
      var text3 = selectedItem2.SubItems[2].Text;
      if (text1.IndexOf("$") != 0)
        return;
      try
      {
        if (listView == listViewMergesTo)
          _controller.CompareFiles(text1, new ChangesetVersionSpec(text2), selectedItem1.ServerItem, new ChangesetVersionSpec(text3));
        else
          _controller.CompareFiles(selectedItem1.ServerItem, new ChangesetVersionSpec(text2), text1, new ChangesetVersionSpec(text3));
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Compare operation failed." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void FillFileMergeCandidates(
      MergeCandidate[] candidates,
      string targetPath,
      string sourcePath)
    {
      var dictionary1 = new Dictionary<int, Changeset>();
      var dictionary2 = new Dictionary<Changeset, Change>();
      foreach (var candidate in candidates)
      {
        var changeset = _controller.GetChangeset(candidate.Changeset.ChangesetId);
        dictionary1[changeset.ChangesetId] = changeset;
        foreach (var change in changeset.Changes)
        {
          if (change.Item.ServerItem == sourcePath)
            dictionary2[changeset] = change;
        }
      }
      if (dictionary2.Count > 0)
      {
        var node = AddTreeNode(treeViewMergeTargets, targetPath, false);
        node.Tag = dictionary2;
        var treeNode = node;
        int num1;
        var num2 = num1 = 4;
        treeNode.SelectedImageIndex = num1;
        treeNode.ImageIndex = num2;
        SetParentImages(node);
        InitUpperNodes();
        treeViewMergeTargets.SelectedNode = node;
      }
      listViewMergeReport.Tag = dictionary1;
    }

    private void FillFolderMergeCandidates(
      MergeCandidate[] candidates,
      string targetPath,
      string sourcePath)
    {
      var dictionary1 = new Dictionary<int, Changeset>();
      foreach (var candidate in candidates)
      {
        Changeset changeset;
        if (dictionary1.ContainsKey(candidate.Changeset.ChangesetId))
        {
          changeset = dictionary1[candidate.Changeset.ChangesetId];
        }
        else
        {
          changeset = _controller.GetChangeset(candidate.Changeset.ChangesetId);
          dictionary1[candidate.Changeset.ChangesetId] = changeset;
        }
        foreach (var change in changeset.Changes)
        {
          var relativeServerPath = Utilities.GetRelativeServerPath(sourcePath, change.Item.ServerItem);
          if (!(sourcePath + relativeServerPath != change.Item.ServerItem) && change.Item.ServerItem.IndexOf(sourcePath + "/") == 0)
          {
            var node = AddTreeNode(treeViewMergeTargets, targetPath + relativeServerPath, false);
            Dictionary<Changeset, Change> dictionary2;
            if (node.Tag != null)
            {
              dictionary2 = node.Tag as Dictionary<Changeset, Change>;
            }
            else
            {
              dictionary2 = new Dictionary<Changeset, Change>();
              node.Tag = dictionary2;
              var treeNode = node;
              int num1;
              var num2 = num1 = 4;
              treeNode.SelectedImageIndex = num1;
              treeNode.ImageIndex = num2;
              SetParentImages(node);
            }
            dictionary2[changeset] = change;
          }
        }
      }
      InitUpperNodes();
      listViewMergeReport.Tag = dictionary1;
    }

    private void SetParentImages(TreeNode node)
    {
      var parent = node.Parent;
      do
      {
        parent.SelectedImageIndex = 3;
        parent.ImageIndex = 2;
        parent = parent.Parent;
      }
      while (parent != null);
    }

    private void InitUpperNodes()
    {
      if (treeViewMergeTargets.Nodes.Count <= 0)
        return;
      treeViewMergeTargets.Nodes[0].Expand();
      treeViewMergeTargets.Nodes[0].SelectedImageIndex = treeViewMergeTargets.Nodes[0].ImageIndex = 0;
      foreach (TreeNode node in treeViewMergeTargets.Nodes[0].Nodes)
      {
        int num1;
        var num2 = num1 = 1;
        node.ImageIndex = num1;
        node.SelectedImageIndex = num2;
      }
    }

    private void ShowSelectedMergeCandidates(TreeNode node)
    {
      listViewMergeReport.Items.Clear();
      if (!(node.Tag is Dictionary<Changeset, Change> tag))
        return;
      foreach (var key in tag.Keys)
      {
        var change = tag[key];
        listViewMergeReport.Items.Add(new ListViewItem(key.ChangesetId.ToString())
        {
          SubItems = {
            change.ChangeType.ToString(),
            Utilities.FormatDateTimeInvariant(key.CreationDate),
            Utilities.GetTableValueByID(_dtUsers, key.Owner),
            key.Comment
          },
          Tag = change
        });
      }
    }

    private void ClearMergeCandidates()
    {
      treeViewMergeTargets.Nodes.Clear();
      listViewMergeReport.Items.Clear();
    }

    private void DefaultMergeCandidateFilter()
    {
      toolStripButtonToggleTreeMode.Checked = !splitContainerMergeCandidates.Panel1Collapsed;
      var selectedItem = GetSelectedItem();
      if (selectedItem == null)
        return;
      textBoxMergeTo.Text = selectedItem.ServerItem;
      var branchRelatives = _controller.GetBranchRelatives(selectedItem);
      comboBoxMergeFrom.Items.Clear();
      comboBoxMergeFrom.Items.Add("");
      foreach (var obj in branchRelatives)
        comboBoxMergeFrom.Items.Add(obj.ServerItem);
      ClearMergeCandidates();
      treeViewMergeTargets.Tag = null;
      listViewMergeReport.Tag = null;
    }

    private void ShowMergeCandidates() => DefaultMergeCandidateFilter();

    private void UpdateCandidatesList()
    {
      ClearMergeCandidates();
      var selectedItem = GetSelectedItem();
      var sourcePath = comboBoxMergeFrom.SelectedItem.ToString();
      if (sourcePath == "")
        return;
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        treeViewMergeTargets.BeginUpdate();
        treeViewMergeTargets.Nodes.Clear();
        treeViewMergeTargets.Tag = new Dictionary<string, Dictionary<Changeset, Change>>();
        if (selectedItem.ItemType == ItemType.File)
          FillFileMergeCandidates(_controller.VersionControl.GetMergeCandidates(sourcePath, selectedItem.ServerItem, 0), selectedItem.ServerItem, sourcePath);
        else
          FillFolderMergeCandidates(_controller.VersionControl.GetMergeCandidates(sourcePath, selectedItem.ServerItem, (RecursionType) 2), selectedItem.ServerItem, sourcePath);
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to retrieve merge candidates." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        treeViewMergeTargets.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
    }

    private void CompareMergeCandidate()
    {
      if (listViewMergeReport.SelectedIndices.Count != 1)
        return;
      GetSelectedItem();
      var selectedItem = listViewMergeReport.SelectedItems[0];
      var tag = selectedItem.Tag as Change;
      var toFile = treeViewMergeTargets.SelectedNode.Name.TrimEnd('/');
      var serverItem = tag.Item.ServerItem;
      var text = selectedItem.SubItems[0].Text;
      try
      {
        _controller.CompareFiles(serverItem, new ChangesetVersionSpec(text), toFile, VersionSpec.Latest);
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Compare operation failed." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void ToggleMergeTreeMode()
    {
      toolStripButtonToggleTreeMode.Checked = !toolStripButtonToggleTreeMode.Checked;
      splitContainerMergeCandidates.Panel1Collapsed = !toolStripButtonToggleTreeMode.Checked;
      if (toolStripButtonToggleTreeMode.Checked)
      {
        listViewMergeReport.Columns[1].Width = 100;
        treeViewMergeTargets.SelectedNode = null;
        listViewMergeReport.Items.Clear();
      }
      else
      {
        listViewMergeReport.Items.Clear();
        listViewMergeReport.Columns[1].Width = 0;
        if (!(listViewMergeReport.Tag is Dictionary<int, Changeset> tag))
          return;
        foreach (var changeset in tag.Values)
          listViewMergeReport.Items.Add(new ListViewItem(changeset.ChangesetId.ToString())
          {
            SubItems = {
              " ",
              Utilities.FormatDateTimeInvariant(changeset.CreationDate),
              Utilities.GetTableValueByID(_dtUsers, changeset.Owner),
              changeset.Comment
            },
            Tag = null
          });
      }
    }

    private void SaveCandidatesList()
    {
      Utilities.SaveListViewToFile(saveFileDialog, listViewMergeReport, "Merge candidates changesets");
    }

    private bool ShowMergeMenu()
    {
      return toolStripButtonToggleTreeMode.Checked && listViewMergeReport.SelectedIndices.Count == 1;
    }

    private void ShowPropertiesTab()
    {
      if (!SetCurrentPage(tabPageProperties))
        return;
      var selectedItem = GetSelectedItem();
      if (selectedItem == null)
        return;
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        listViewChanges.BeginUpdate();
        listViewChanges.Items.Clear();
        propertyGridItem.SelectedObject = new ItemProperties(selectedItem);
        foreach (var itemChange in _controller.GetItemChanges(selectedItem))
          listViewChanges.Items.Add(new ListViewItem(itemChange.Change.ChangeType.ToString())
          {
            SubItems = {
              Utilities.GetTableValueByID(_dtUsers, itemChange.OwnerName),
              itemChange.Name,
              itemChange.Computer,
              itemChange.Change.Version.ToString(),
              Utilities.FormatDateTimeInvariant(itemChange.Change.CreationDate)
            }
          });
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to retrieve item pending changes." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        listViewChanges.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
    }

    public HistoryViewControl()
    {
      InitializeComponent();
      listViewHistory.Sorted = true;
      listViewChanges.ListViewItemSorter = new CustomListSorter(ListSorterType.Date | ListSorterType.Integer);
      listViewMergesTo.ListViewItemSorter = new CustomListSorter(ListSorterType.Date | ListSorterType.Integer);
      listViewMergesFrom.ListViewItemSorter = new CustomListSorter(ListSorterType.Date | ListSorterType.Integer);
      listViewMergeReport.ListViewItemSorter = new CustomListSorter(ListSorterType.Date | ListSorterType.Integer);
      listViewLabels.Sorted = true;
      splitContainer1.Panel1MinSize = 350;
      splitContainer1.Panel2MinSize = 350;
      splitContainer1.BackColor = SystemColors.Control;
      Name = "History Sidekick";
    }

    public override Image Image => Resources.HistoryImage;

    private void treeSourceCodeTree_AfterSelect(object sender, TreeViewEventArgs e)
    {
      SelectNode(e.Node);
    }

    private void comboBoxUserName_SelectedValueChanged(object sender, EventArgs e)
    {
      if (GetSelectedItem() == null)
        return;
      ShowHistoryTab(true);
    }

    private void listViewHistory_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      var listView = sender as ListView;
      (listView.ListViewItemSorter as CustomListSorter).SetColumn(listView.Columns[e.Column]);
      listView.Sort();
    }

    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) => ShowTab();

    private void listViewHistory_SelectedIndexChanged(object sender, EventArgs e)
    {
      toolStripButtonViewChangesetDetails.Enabled = listViewHistory.SelectedIndices.Count == 1;
    }

    private void treeViewBranches_AfterSelect(object sender, TreeViewEventArgs e)
    {
      ShowBranchProperties(e.Node);
    }

    private void MergeToCompareToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CompareVersions(listViewMergesTo);
    }

    private void MergeFromCompareToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CompareVersions(listViewMergesFrom);
    }

    private void comboBoxMergeFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateCandidatesList();
    }

    private void treeViewMergeTargets_AfterSelect(object sender, TreeViewEventArgs e)
    {
      ShowSelectedMergeCandidates(e.Node);
    }

    private void comboBoxUserNameLabel_SelectedValueChanged(object sender, EventArgs e)
    {
      if (GetSelectedItem() == null)
        return;
      ShowLabelsTab(true);
    }

    private void treeViewSourceCode_BeforeExpand(object sender, TreeViewCancelEventArgs e)
    {
      if (e.Action != TreeViewAction.Expand)
        return;
      if (e.Node.Nodes.ContainsKey("placeholder"))
      {
        e.Node.Nodes.RemoveByKey("placeholder");
        FillSubTree(e.Node);
      }
      var viewCancelEventArgs = e;
      viewCancelEventArgs.Cancel = viewCancelEventArgs.Node.Nodes.Count == 0;
    }

    private void toolStripButtonRefresh_Click(object sender, EventArgs e)
    {
      var selectedNode = treeViewSourceCode.SelectedNode;
      if (selectedNode == null)
        return;
      RefreshSubTree(selectedNode.Name);
    }

    private void toolStripButtonFindItem_Click(object sender, EventArgs e)
    {
      using (var formSearchItem = new FormSearchItem())
      {
        formSearchItem.Initialize(_controller);
        if (formSearchItem.ShowDialog() != DialogResult.OK)
          return;
        var treeNodeArray = treeViewSourceCode.Nodes.Find(formSearchItem.SelectedServerItem + "/", true);
        if (treeNodeArray.Length == 0)
          RefreshSubTree(formSearchItem.SelectedServerItem);
        else
          treeViewSourceCode.SelectedNode = treeNodeArray[0];
      }
    }

    private void toolStripMenuItemCompareMergeCandidate_Click(object sender, EventArgs e)
    {
      CompareMergeCandidate();
    }

    private void toolStripMenuItemCompareLabel_Click(object sender, EventArgs e)
    {
      CompareLabels();
    }

    private void toolStripMenuItemCompareHistory_Click(object sender, EventArgs e)
    {
      CompareHistory();
    }

    private void contextMenuStripHistory_Opening(object sender, CancelEventArgs e)
    {
      var selectedItem = GetSelectedItem();
      e.Cancel = listViewHistory.SelectedIndices.Count != 2 && listViewHistory.SelectedIndices.Count != 1;
      viewChangesetDetailsToolStripMenuItem.Visible = listViewHistory.SelectedIndices.Count == 1;
      if (selectedItem.ItemType == ItemType.Folder)
      {
        toolStripMenuItemCompareLatest.Visible = false;
        toolStripMenuItemCompareTwoVersions.Visible = false;
      }
      else
      {
        toolStripMenuItemCompareLatest.Visible = listViewHistory.SelectedIndices.Count == 1;
        toolStripMenuItemCompareTwoVersions.Visible = listViewHistory.SelectedIndices.Count == 2;
      }
    }

    private void contextMenuStripLabels_Opening(object sender, CancelEventArgs e)
    {
      var selectedItem = GetSelectedItem();
      e.Cancel = listViewLabels.SelectedIndices.Count != 2 && listViewLabels.SelectedIndices.Count != 1;
      viewChangesetDetailsToolStripMenuItem1.Visible = listViewLabels.SelectedIndices.Count == 1;
      if (selectedItem.ItemType == ItemType.Folder)
      {
        toolStripMenuItemCompareLabelLatest.Visible = false;
        toolStripMenuItemCompareLabels.Visible = false;
      }
      else
      {
        toolStripMenuItemCompareLabelLatest.Visible = listViewLabels.SelectedIndices.Count == 1;
        toolStripMenuItemCompareLabels.Visible = listViewLabels.SelectedIndices.Count == 2;
      }
    }

    private void toolStripButtonSaveToFile_Click(object sender, EventArgs e) => SaveToFile();

    private void comboBoxUserNameLabel_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Tab && e.KeyCode != Keys.Return)
        return;
      e.Handled = true;
      ShowLabelsTab(true);
    }

    private void comboBoxUserName_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Tab && e.KeyCode != Keys.Return)
        return;
      e.Handled = true;
      ShowHistoryTab(true);
    }

    private void toolStripButtonSaveList_Click(object sender, EventArgs e)
    {
      SaveCandidatesList();
    }

    private void toolStripButtonToggleTreeMode_Click(object sender, EventArgs e)
    {
      ToggleMergeTreeMode();
    }

    private void contextMenuStripMergeCandidate_Opening(object sender, CancelEventArgs e)
    {
      e.Cancel = !ShowMergeMenu();
    }

    private void toolStripButtonViewChangesetDetails_Click(object sender, EventArgs e)
    {
      ViewChangesetDetailsForHistoryWindow();
    }

    private void listViewMergeReport_SelectedIndexChanged(object sender, EventArgs e)
    {
      toolStripButtonChangesetDetails.Enabled = listViewMergeReport.SelectedIndices.Count == 1;
    }

    private void toolStripButtonChangesetDetails_Click(object sender, EventArgs e)
    {
      ViewChangesetDetailsMergeCandidates();
    }

    private void toolStripButtonChangesetDetails3_Click(object sender, EventArgs e)
    {
      ViewChangesetDetailsForLabelsWindow();
    }

    private void listViewLabels_SelectedIndexChanged(object sender, EventArgs e)
    {
      toolStripButtonChangesetDetails3.Enabled = listViewLabels.SelectedIndices.Count == 1;
    }

    private void viewChangesetDetailsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ViewChangesetDetailsForHistoryWindow();
    }

    private void viewChangesetDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      ViewChangesetDetailsForLabelsWindow();
    }

    private void viewSourceChangesetDetailsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ViewSourceChangesetDetails(listViewMergesTo);
    }

    private void viewTargetChangesetDetailsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ViewTargetChangesetDetails(listViewMergesTo);
    }

    private void viewSourceChangesetDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      ViewSourceChangesetDetails(listViewMergesFrom);
    }

    private void viewTargetChangesetDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      ViewTargetChangesetDetails(listViewMergesFrom);
    }

    private void contextMenuStripMergeTo_Opening(object sender, CancelEventArgs e)
    {
      compareToolStripMenuItem.Visible = GetSelectedItem().ItemType != ItemType.Folder;
      e.Cancel = listViewMergesTo.SelectedIndices.Count != 1;
    }

    private void contextMenuStripMergeFrom_Opening(object sender, CancelEventArgs e)
    {
      toolStripMenuItem1.Visible = GetSelectedItem().ItemType != ItemType.Folder;
      e.Cancel = listViewMergesFrom.SelectedIndices.Count != 1;
    }

    private void viewChangesetDetailsToolStripMenuItem2_Click(object sender, EventArgs e)
    {
      ViewChangesetDetailsMergeCandidates();
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
      var componentResourceManager = new ComponentResourceManager(typeof (HistoryViewControl));
      treeViewSourceCode = new TreeView();
      imageList = new ImageList(components);
      splitContainer1 = new SplitContainer();
      toolStrip1 = new ToolStrip();
      toolStripButtonRefresh = new ToolStripButton();
      toolStripButtonFindItem = new ToolStripButton();
      tabControl1 = new TabControl();
      tabPageHistory = new TabPage();
      listViewHistory = new VirtualListView();
      columnHeaderChangeset = new ColumnHeader();
      columnHeaderChange = new ColumnHeader();
      columnHeaderOwner = new ColumnHeader();
      columnHeaderDate = new ColumnHeader();
      columnHeaderComment = new ColumnHeader();
      contextMenuStripHistory = new ContextMenuStrip(components);
      toolStripMenuItemCompareTwoVersions = new ToolStripMenuItem();
      toolStripMenuItemCompareLatest = new ToolStripMenuItem();
      viewChangesetDetailsToolStripMenuItem = new ToolStripMenuItem();
      toolStrip2 = new ToolStrip();
      toolStripButtonViewChangesetDetails = new ToolStripButton();
      toolStripSeparator2 = new ToolStripSeparator();
      toolStripButtonSaveToFile = new ToolStripButton();
      groupBox2 = new GroupBox();
      label3 = new Label();
      comboBoxUserName = new ComboBox();
      tabPageProperties = new TabPage();
      groupBoxPendingChanges = new GroupBox();
      listViewChanges = new ListView();
      columnHeader1 = new ColumnHeader();
      columnHeader3 = new ColumnHeader();
      columnHeader4 = new ColumnHeader();
      columnHeader5 = new ColumnHeader();
      columnHeader6 = new ColumnHeader();
      columnHeader7 = new ColumnHeader();
      groupBoxProperties = new GroupBox();
      propertyGridItem = new PropertyGrid();
      tabPageBranches = new TabPage();
      groupBoxBranches = new GroupBox();
      treeViewBranches = new TreeView();
      groupBoxBranchProperties = new GroupBox();
      propertyGridBranch = new PropertyGrid();
      tabPageMerges = new TabPage();
      tableLayoutPanelMerge = new TableLayoutPanel();
      groupBoxMergeFrom = new GroupBox();
      listViewMergesFrom = new ListView();
      columnHeader16 = new ColumnHeader();
      columnHeader2 = new ColumnHeader();
      columnHeader8 = new ColumnHeader();
      columnHeader13 = new ColumnHeader();
      columnHeader14 = new ColumnHeader();
      contextMenuStripMergeFrom = new ContextMenuStrip(components);
      toolStripMenuItem1 = new ToolStripMenuItem();
      viewSourceChangesetDetailsToolStripMenuItem1 = new ToolStripMenuItem();
      viewTargetChangesetDetailsToolStripMenuItem1 = new ToolStripMenuItem();
      groupBoxMergeTo = new GroupBox();
      listViewMergesTo = new ListView();
      columnHeader15 = new ColumnHeader();
      columnHeader11 = new ColumnHeader();
      columnHeader12 = new ColumnHeader();
      columnHeader9 = new ColumnHeader();
      columnHeader10 = new ColumnHeader();
      contextMenuStripMergeTo = new ContextMenuStrip(components);
      compareToolStripMenuItem = new ToolStripMenuItem();
      viewSourceChangesetDetailsToolStripMenuItem = new ToolStripMenuItem();
      viewTargetChangesetDetailsToolStripMenuItem = new ToolStripMenuItem();
      tabPageMergeCandidates = new TabPage();
      splitContainerMergeCandidates = new SplitContainer();
      treeViewMergeTargets = new TreeView();
      listViewMergeReport = new ListView();
      columnHeader20 = new ColumnHeader();
      columnHeader21 = new ColumnHeader();
      columnHeader23 = new ColumnHeader();
      columnHeader24 = new ColumnHeader();
      columnHeader29 = new ColumnHeader();
      contextMenuStripMergeCandidate = new ContextMenuStrip(components);
      toolStripMenuItem2 = new ToolStripMenuItem();
      viewChangesetDetailsToolStripMenuItem2 = new ToolStripMenuItem();
      toolStrip3 = new ToolStrip();
      toolStripButtonToggleTreeMode = new ToolStripButton();
      toolStripSeparator1 = new ToolStripSeparator();
      toolStripButtonChangesetDetails = new ToolStripButton();
      toolStripSeparator3 = new ToolStripSeparator();
      toolStripButtonSaveList = new ToolStripButton();
      groupBoxCandidates = new GroupBox();
      label4 = new Label();
      label1 = new Label();
      textBoxMergeTo = new TextBox();
      comboBoxMergeFrom = new ComboBox();
      tabPageLabels = new TabPage();
      listViewLabels = new VirtualListView();
      columnHeader28 = new ColumnHeader();
      columnHeader22 = new ColumnHeader();
      columnHeader25 = new ColumnHeader();
      columnHeader26 = new ColumnHeader();
      columnHeader27 = new ColumnHeader();
      contextMenuStripLabels = new ContextMenuStrip(components);
      toolStripMenuItemCompareLabels = new ToolStripMenuItem();
      toolStripMenuItemCompareLabelLatest = new ToolStripMenuItem();
      viewChangesetDetailsToolStripMenuItem1 = new ToolStripMenuItem();
      toolStrip4 = new ToolStrip();
      toolStripButtonChangesetDetails3 = new ToolStripButton();
      groupBoxLabel = new GroupBox();
      label5 = new Label();
      comboBoxUserNameLabel = new ComboBox();
      columnHeader17 = new ColumnHeader();
      columnHeader19 = new ColumnHeader();
      columnHeader18 = new ColumnHeader();
      toolStripButton1 = new ToolStripButton();
      toolStripSeparator4 = new ToolStripSeparator();
      toolStripButton2 = new ToolStripButton();
      splitContainer1.BeginInit();
      splitContainer1.Panel1.SuspendLayout();
      splitContainer1.Panel2.SuspendLayout();
      splitContainer1.SuspendLayout();
      toolStrip1.SuspendLayout();
      tabControl1.SuspendLayout();
      tabPageHistory.SuspendLayout();
      contextMenuStripHistory.SuspendLayout();
      toolStrip2.SuspendLayout();
      groupBox2.SuspendLayout();
      tabPageProperties.SuspendLayout();
      groupBoxPendingChanges.SuspendLayout();
      groupBoxProperties.SuspendLayout();
      tabPageBranches.SuspendLayout();
      groupBoxBranches.SuspendLayout();
      groupBoxBranchProperties.SuspendLayout();
      tabPageMerges.SuspendLayout();
      tableLayoutPanelMerge.SuspendLayout();
      groupBoxMergeFrom.SuspendLayout();
      contextMenuStripMergeFrom.SuspendLayout();
      groupBoxMergeTo.SuspendLayout();
      contextMenuStripMergeTo.SuspendLayout();
      tabPageMergeCandidates.SuspendLayout();
      splitContainerMergeCandidates.BeginInit();
      splitContainerMergeCandidates.Panel1.SuspendLayout();
      splitContainerMergeCandidates.Panel2.SuspendLayout();
      splitContainerMergeCandidates.SuspendLayout();
      contextMenuStripMergeCandidate.SuspendLayout();
      toolStrip3.SuspendLayout();
      groupBoxCandidates.SuspendLayout();
      tabPageLabels.SuspendLayout();
      contextMenuStripLabels.SuspendLayout();
      toolStrip4.SuspendLayout();
      groupBoxLabel.SuspendLayout();
      SuspendLayout();
      treeViewSourceCode.BorderStyle = BorderStyle.None;
      treeViewSourceCode.Dock = DockStyle.Fill;
      treeViewSourceCode.HideSelection = false;
      treeViewSourceCode.ImageIndex = 0;
      treeViewSourceCode.ImageList = imageList;
      treeViewSourceCode.Location = new Point(0, 25);
      treeViewSourceCode.Name = "treeViewSourceCode";
      treeViewSourceCode.SelectedImageIndex = 0;
      treeViewSourceCode.Size = new Size(346, 496);
      treeViewSourceCode.TabIndex = 3;
      treeViewSourceCode.BeforeExpand += treeViewSourceCode_BeforeExpand;
      treeViewSourceCode.AfterSelect += treeSourceCodeTree_AfterSelect;
      imageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList.ImageStream");
      imageList.TransparentColor = Color.Transparent;
      imageList.Images.SetKeyName(0, "server.bmp");
      imageList.Images.SetKeyName(1, "project.bmp");
      imageList.Images.SetKeyName(2, "folder.bmp");
      imageList.Images.SetKeyName(3, "folder_open.bmp");
      imageList.Images.SetKeyName(4, "item.gif");
      imageList.Images.SetKeyName(5, "deleted.gif");
      imageList.Images.SetKeyName(6, "status_new.gif");
      imageList.Images.SetKeyName(7, "Branch.bmp");
      splitContainer1.BorderStyle = BorderStyle.Fixed3D;
      splitContainer1.Dock = DockStyle.Fill;
      splitContainer1.FixedPanel = FixedPanel.Panel1;
      splitContainer1.Location = new Point(0, 0);
      splitContainer1.Name = "splitContainer1";
      splitContainer1.Panel1.Controls.Add(treeViewSourceCode);
      splitContainer1.Panel1.Controls.Add(toolStrip1);
      splitContainer1.Panel2.Controls.Add(tabControl1);
      splitContainer1.Panel2.Padding = new Padding(3, 3, 0, 0);
      splitContainer1.Size = new Size(931, 525);
      splitContainer1.SplitterDistance = 350;
      splitContainer1.SplitterWidth = 2;
      splitContainer1.TabIndex = 3;
      toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
      toolStrip1.Items.AddRange(new ToolStripItem[2]
      {
        toolStripButtonRefresh,
        toolStripButtonFindItem
      });
      toolStrip1.Location = new Point(0, 0);
      toolStrip1.Name = "toolStrip1";
      toolStrip1.Padding = new Padding(3, 1, 1, 1);
      toolStrip1.RenderMode = ToolStripRenderMode.System;
      toolStrip1.Size = new Size(346, 25);
      toolStrip1.TabIndex = 4;
      toolStrip1.Text = "toolStrip1";
      toolStripButtonRefresh.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonRefresh.Image = (Image) componentResourceManager.GetObject("toolStripButtonRefresh.Image");
      toolStripButtonRefresh.ImageTransparentColor = Color.Magenta;
      toolStripButtonRefresh.Name = "toolStripButtonRefresh";
      toolStripButtonRefresh.Size = new Size(23, 20);
      toolStripButtonRefresh.Text = "toolStripButton1";
      toolStripButtonRefresh.ToolTipText = "Refresh tree";
      toolStripButtonRefresh.Click += toolStripButtonRefresh_Click;
      toolStripButtonFindItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonFindItem.Image = (Image) componentResourceManager.GetObject("toolStripButtonFindItem.Image");
      toolStripButtonFindItem.ImageTransparentColor = Color.Yellow;
      toolStripButtonFindItem.Name = "toolStripButtonFindItem";
      toolStripButtonFindItem.Size = new Size(23, 20);
      toolStripButtonFindItem.Text = "toolStripButton1";
      toolStripButtonFindItem.ToolTipText = "Find file or folder";
      toolStripButtonFindItem.Click += toolStripButtonFindItem_Click;
      tabControl1.Controls.Add(tabPageHistory);
      tabControl1.Controls.Add(tabPageProperties);
      tabControl1.Controls.Add(tabPageBranches);
      tabControl1.Controls.Add(tabPageMerges);
      tabControl1.Controls.Add(tabPageMergeCandidates);
      tabControl1.Controls.Add(tabPageLabels);
      tabControl1.Dock = DockStyle.Fill;
      tabControl1.Location = new Point(3, 3);
      tabControl1.Name = "tabControl1";
      tabControl1.SelectedIndex = 0;
      tabControl1.Size = new Size(572, 518);
      tabControl1.TabIndex = 0;
      tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
      tabPageHistory.BackColor = SystemColors.Control;
      tabPageHistory.Controls.Add(listViewHistory);
      tabPageHistory.Controls.Add(toolStrip2);
      tabPageHistory.Controls.Add(groupBox2);
      tabPageHistory.Location = new Point(4, 22);
      tabPageHistory.Name = "tabPageHistory";
      tabPageHistory.Padding = new Padding(3);
      tabPageHistory.Size = new Size(564, 492);
      tabPageHistory.TabIndex = 0;
      tabPageHistory.Text = "History";
      listViewHistory.AllowColumnReorder = true;
      listViewHistory.Columns.AddRange(new ColumnHeader[5]
      {
        columnHeaderChangeset,
        columnHeaderChange,
        columnHeaderOwner,
        columnHeaderDate,
        columnHeaderComment
      });
      listViewHistory.ContextMenuStrip = contextMenuStripHistory;
      listViewHistory.Dock = DockStyle.Fill;
      listViewHistory.FullRowSelect = true;
      listViewHistory.GridLines = true;
      listViewHistory.HideSelection = false;
      listViewHistory.Location = new Point(3, 77);
      listViewHistory.Name = "listViewHistory";
      listViewHistory.Size = new Size(558, 412);
      listViewHistory.Sorted = false;
      listViewHistory.Sorting = SortOrder.Descending;
      listViewHistory.TabIndex = 5;
      listViewHistory.UseCompatibleStateImageBehavior = false;
      listViewHistory.View = View.Details;
      listViewHistory.SelectedIndexChanged += listViewHistory_SelectedIndexChanged;
      columnHeaderChangeset.Text = "Changeset";
      columnHeaderChangeset.Width = 100;
      columnHeaderChange.Text = "Change";
      columnHeaderChange.Width = 120;
      columnHeaderOwner.Text = "Owner";
      columnHeaderOwner.Width = 100;
      columnHeaderDate.Text = "Date";
      columnHeaderDate.Width = 120;
      columnHeaderComment.Text = "Comment";
      columnHeaderComment.Width = 130;
      contextMenuStripHistory.Items.AddRange(new ToolStripItem[3]
      {
        toolStripMenuItemCompareTwoVersions,
        toolStripMenuItemCompareLatest,
        viewChangesetDetailsToolStripMenuItem
      });
      contextMenuStripHistory.Name = "contextMenuStripMerge";
      contextMenuStripHistory.Size = new Size(214, 70);
      contextMenuStripHistory.Opening += contextMenuStripHistory_Opening;
      toolStripMenuItemCompareTwoVersions.Image = (Image) componentResourceManager.GetObject("toolStripMenuItemCompareTwoVersions.Image");
      toolStripMenuItemCompareTwoVersions.Name = "toolStripMenuItemCompareTwoVersions";
      toolStripMenuItemCompareTwoVersions.Size = new Size(213, 22);
      toolStripMenuItemCompareTwoVersions.Text = "Compare History Versions";
      toolStripMenuItemCompareTwoVersions.Click += toolStripMenuItemCompareHistory_Click;
      toolStripMenuItemCompareLatest.Image = (Image) componentResourceManager.GetObject("toolStripMenuItemCompareLatest.Image");
      toolStripMenuItemCompareLatest.Name = "toolStripMenuItemCompareLatest";
      toolStripMenuItemCompareLatest.Size = new Size(213, 22);
      toolStripMenuItemCompareLatest.Text = "Compare With Latest Version";
      toolStripMenuItemCompareLatest.Click += toolStripMenuItemCompareHistory_Click;
      viewChangesetDetailsToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("viewChangesetDetailsToolStripMenuItem.Image");
      viewChangesetDetailsToolStripMenuItem.Name = "viewChangesetDetailsToolStripMenuItem";
      viewChangesetDetailsToolStripMenuItem.Size = new Size(213, 22);
      viewChangesetDetailsToolStripMenuItem.Text = "View Changeset Details";
      viewChangesetDetailsToolStripMenuItem.Click += viewChangesetDetailsToolStripMenuItem_Click;
      toolStrip2.AllowMerge = false;
      toolStrip2.CanOverflow = false;
      toolStrip2.GripMargin = new Padding(1);
      toolStrip2.GripStyle = ToolStripGripStyle.Hidden;
      toolStrip2.Items.AddRange(new ToolStripItem[3]
      {
        toolStripButtonViewChangesetDetails,
        toolStripSeparator2,
        toolStripButtonSaveToFile
      });
      toolStrip2.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
      toolStrip2.Location = new Point(3, 52);
      toolStrip2.Name = "toolStrip2";
      toolStrip2.Padding = new Padding(3, 1, 1, 1);
      toolStrip2.RenderMode = ToolStripRenderMode.System;
      toolStrip2.Size = new Size(558, 25);
      toolStrip2.Stretch = true;
      toolStrip2.TabIndex = 13;
      toolStripButtonViewChangesetDetails.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonViewChangesetDetails.Enabled = false;
      toolStripButtonViewChangesetDetails.Image = (Image) componentResourceManager.GetObject("toolStripButtonViewChangesetDetails.Image");
      toolStripButtonViewChangesetDetails.ImageTransparentColor = Color.Magenta;
      toolStripButtonViewChangesetDetails.Name = "toolStripButtonViewChangesetDetails";
      toolStripButtonViewChangesetDetails.Size = new Size(23, 20);
      toolStripButtonViewChangesetDetails.Text = "View Changeset Details";
      toolStripButtonViewChangesetDetails.Click += toolStripButtonViewChangesetDetails_Click;
      toolStripSeparator2.Name = "toolStripSeparator2";
      toolStripSeparator2.Size = new Size(6, 23);
      toolStripButtonSaveToFile.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonSaveToFile.Image = (Image) componentResourceManager.GetObject("toolStripButtonSaveToFile.Image");
      toolStripButtonSaveToFile.ImageTransparentColor = Color.Black;
      toolStripButtonSaveToFile.Name = "toolStripButtonSaveToFile";
      toolStripButtonSaveToFile.Size = new Size(23, 20);
      toolStripButtonSaveToFile.Text = "toolStripButton5";
      toolStripButtonSaveToFile.ToolTipText = "Save list to file";
      toolStripButtonSaveToFile.Click += toolStripButtonSaveToFile_Click;
      groupBox2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      groupBox2.Controls.Add(label3);
      groupBox2.Controls.Add(comboBoxUserName);
      groupBox2.Dock = DockStyle.Top;
      groupBox2.Location = new Point(3, 3);
      groupBox2.Name = "groupBox2";
      groupBox2.Size = new Size(558, 49);
      groupBox2.TabIndex = 6;
      groupBox2.TabStop = false;
      groupBox2.Text = " History filter criteria";
      label3.AutoSize = true;
      label3.Location = new Point(8, 23);
      label3.Name = "label3";
      label3.Size = new Size(58, 13);
      label3.TabIndex = 12;
      label3.Text = "User name";
      comboBoxUserName.AutoCompleteMode = AutoCompleteMode.Suggest;
      comboBoxUserName.AutoCompleteSource = AutoCompleteSource.ListItems;
      comboBoxUserName.FormattingEnabled = true;
      comboBoxUserName.IntegralHeight = false;
      comboBoxUserName.ItemHeight = 13;
      comboBoxUserName.Location = new Point(100, 19);
      comboBoxUserName.MaxDropDownItems = 32;
      comboBoxUserName.Name = "comboBoxUserName";
      comboBoxUserName.Size = new Size(202, 21);
      comboBoxUserName.TabIndex = 1;
      comboBoxUserName.SelectedValueChanged += comboBoxUserName_SelectedValueChanged;
      comboBoxUserName.KeyUp += comboBoxUserName_KeyUp;
      tabPageProperties.BackColor = SystemColors.Control;
      tabPageProperties.Controls.Add(groupBoxPendingChanges);
      tabPageProperties.Controls.Add(groupBoxProperties);
      tabPageProperties.Location = new Point(4, 22);
      tabPageProperties.Name = "tabPageProperties";
      tabPageProperties.Padding = new Padding(3);
      tabPageProperties.Size = new Size(564, 492);
      tabPageProperties.TabIndex = 1;
      tabPageProperties.Text = "Properties";
      groupBoxPendingChanges.Controls.Add(listViewChanges);
      groupBoxPendingChanges.Dock = DockStyle.Fill;
      groupBoxPendingChanges.Location = new Point(3, 225);
      groupBoxPendingChanges.Name = "groupBoxPendingChanges";
      groupBoxPendingChanges.Size = new Size(558, 264);
      groupBoxPendingChanges.TabIndex = 7;
      groupBoxPendingChanges.TabStop = false;
      groupBoxPendingChanges.Text = " Item pending changes";
      listViewChanges.AllowColumnReorder = true;
      listViewChanges.BorderStyle = BorderStyle.None;
      listViewChanges.Columns.AddRange(new ColumnHeader[6]
      {
        columnHeader1,
        columnHeader3,
        columnHeader4,
        columnHeader5,
        columnHeader6,
        columnHeader7
      });
      listViewChanges.Dock = DockStyle.Fill;
      listViewChanges.FullRowSelect = true;
      listViewChanges.GridLines = true;
      listViewChanges.HideSelection = false;
      listViewChanges.Location = new Point(3, 16);
      listViewChanges.Name = "listViewChanges";
      listViewChanges.Size = new Size(552, 245);
      listViewChanges.Sorting = SortOrder.Descending;
      listViewChanges.TabIndex = 6;
      listViewChanges.UseCompatibleStateImageBehavior = false;
      listViewChanges.View = View.Details;
      columnHeader1.Text = "Change";
      columnHeader3.Text = "Owner";
      columnHeader3.Width = 100;
      columnHeader4.Text = "Workspace";
      columnHeader4.Width = 100;
      columnHeader5.Text = "Computer";
      columnHeader5.Width = 100;
      columnHeader6.Text = "Version";
      columnHeader7.Text = "Change date";
      columnHeader7.Width = 110;
      groupBoxProperties.Controls.Add(propertyGridItem);
      groupBoxProperties.Dock = DockStyle.Top;
      groupBoxProperties.Location = new Point(3, 3);
      groupBoxProperties.Name = "groupBoxProperties";
      groupBoxProperties.Size = new Size(558, 222);
      groupBoxProperties.TabIndex = 6;
      groupBoxProperties.TabStop = false;
      groupBoxProperties.Text = " Item properties";
      propertyGridItem.CommandsBackColor = SystemColors.ControlText;
      propertyGridItem.Dock = DockStyle.Fill;
      propertyGridItem.Location = new Point(3, 16);
      propertyGridItem.Name = "propertyGridItem";
      propertyGridItem.Size = new Size(552, 203);
      propertyGridItem.TabIndex = 1;
      propertyGridItem.ToolbarVisible = false;
      tabPageBranches.BackColor = SystemColors.Control;
      tabPageBranches.Controls.Add(groupBoxBranches);
      tabPageBranches.Controls.Add(groupBoxBranchProperties);
      tabPageBranches.Location = new Point(4, 22);
      tabPageBranches.Name = "tabPageBranches";
      tabPageBranches.Size = new Size(564, 492);
      tabPageBranches.TabIndex = 2;
      tabPageBranches.Text = "Branches";
      groupBoxBranches.Controls.Add(treeViewBranches);
      groupBoxBranches.Dock = DockStyle.Fill;
      groupBoxBranches.Location = new Point(0, 0);
      groupBoxBranches.Name = "groupBoxBranches";
      groupBoxBranches.Size = new Size(564, 257);
      groupBoxBranches.TabIndex = 3;
      groupBoxBranches.TabStop = false;
      groupBoxBranches.Text = " Branches tree";
      treeViewBranches.BorderStyle = BorderStyle.None;
      treeViewBranches.Dock = DockStyle.Fill;
      treeViewBranches.FullRowSelect = true;
      treeViewBranches.HideSelection = false;
      treeViewBranches.ImageIndex = 0;
      treeViewBranches.ImageList = imageList;
      treeViewBranches.Location = new Point(3, 16);
      treeViewBranches.Name = "treeViewBranches";
      treeViewBranches.SelectedImageIndex = 0;
      treeViewBranches.Size = new Size(558, 238);
      treeViewBranches.StateImageList = imageList;
      treeViewBranches.TabIndex = 1;
      treeViewBranches.AfterSelect += treeViewBranches_AfterSelect;
      groupBoxBranchProperties.Controls.Add(propertyGridBranch);
      groupBoxBranchProperties.Dock = DockStyle.Bottom;
      groupBoxBranchProperties.Location = new Point(0, 257);
      groupBoxBranchProperties.Name = "groupBoxBranchProperties";
      groupBoxBranchProperties.Size = new Size(564, 235);
      groupBoxBranchProperties.TabIndex = 2;
      groupBoxBranchProperties.TabStop = false;
      groupBoxBranchProperties.Text = " Selected branch properties";
      propertyGridBranch.Dock = DockStyle.Fill;
      propertyGridBranch.Location = new Point(3, 16);
      propertyGridBranch.Name = "propertyGridBranch";
      propertyGridBranch.Size = new Size(558, 216);
      propertyGridBranch.TabIndex = 2;
      propertyGridBranch.ToolbarVisible = false;
      tabPageMerges.BackColor = SystemColors.Control;
      tabPageMerges.Controls.Add(tableLayoutPanelMerge);
      tabPageMerges.Location = new Point(4, 22);
      tabPageMerges.Name = "tabPageMerges";
      tabPageMerges.Size = new Size(564, 492);
      tabPageMerges.TabIndex = 3;
      tabPageMerges.Text = "Merges";
      tableLayoutPanelMerge.ColumnCount = 1;
      tableLayoutPanelMerge.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanelMerge.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
      tableLayoutPanelMerge.Controls.Add(groupBoxMergeFrom, 0, 1);
      tableLayoutPanelMerge.Controls.Add(groupBoxMergeTo, 0, 0);
      tableLayoutPanelMerge.Dock = DockStyle.Fill;
      tableLayoutPanelMerge.Location = new Point(0, 0);
      tableLayoutPanelMerge.Name = "tableLayoutPanelMerge";
      tableLayoutPanelMerge.RowCount = 2;
      tableLayoutPanelMerge.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
      tableLayoutPanelMerge.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
      tableLayoutPanelMerge.Size = new Size(564, 492);
      tableLayoutPanelMerge.TabIndex = 9;
      groupBoxMergeFrom.Controls.Add(listViewMergesFrom);
      groupBoxMergeFrom.Dock = DockStyle.Fill;
      groupBoxMergeFrom.Location = new Point(3, 249);
      groupBoxMergeFrom.Name = "groupBoxMergeFrom";
      groupBoxMergeFrom.Size = new Size(558, 240);
      groupBoxMergeFrom.TabIndex = 9;
      groupBoxMergeFrom.TabStop = false;
      groupBoxMergeFrom.Text = " Merges From Item";
      listViewMergesFrom.AllowColumnReorder = true;
      listViewMergesFrom.BorderStyle = BorderStyle.None;
      listViewMergesFrom.Columns.AddRange(new ColumnHeader[5]
      {
        columnHeader16,
        columnHeader2,
        columnHeader8,
        columnHeader13,
        columnHeader14
      });
      listViewMergesFrom.ContextMenuStrip = contextMenuStripMergeFrom;
      listViewMergesFrom.Dock = DockStyle.Fill;
      listViewMergesFrom.FullRowSelect = true;
      listViewMergesFrom.GridLines = true;
      listViewMergesFrom.HideSelection = false;
      listViewMergesFrom.Location = new Point(3, 16);
      listViewMergesFrom.MultiSelect = false;
      listViewMergesFrom.Name = "listViewMergesFrom";
      listViewMergesFrom.Size = new Size(552, 221);
      listViewMergesFrom.Sorting = SortOrder.Descending;
      listViewMergesFrom.TabIndex = 8;
      listViewMergesFrom.UseCompatibleStateImageBehavior = false;
      listViewMergesFrom.View = View.Details;
      listViewMergesFrom.ColumnClick += listViewHistory_ColumnClick;
      columnHeader16.Text = "To Item";
      columnHeader16.Width = 200;
      columnHeader2.Text = "Source";
      columnHeader2.Width = 50;
      columnHeader8.Text = "Target";
      columnHeader8.Width = 50;
      columnHeader13.Text = "Author";
      columnHeader13.Width = 100;
      columnHeader14.Text = "Merge Date";
      columnHeader14.Width = 120;
      contextMenuStripMergeFrom.Items.AddRange(new ToolStripItem[3]
      {
        toolStripMenuItem1,
        viewSourceChangesetDetailsToolStripMenuItem1,
        viewTargetChangesetDetailsToolStripMenuItem1
      });
      contextMenuStripMergeFrom.Name = "contextMenuStripMerge";
      contextMenuStripMergeFrom.Size = new Size(223, 70);
      contextMenuStripMergeFrom.Opening += contextMenuStripMergeFrom_Opening;
      toolStripMenuItem1.Image = (Image) componentResourceManager.GetObject("toolStripMenuItem1.Image");
      toolStripMenuItem1.Name = "toolStripMenuItem1";
      toolStripMenuItem1.Size = new Size(222, 22);
      toolStripMenuItem1.Text = "Compare Source To Target";
      toolStripMenuItem1.Click += MergeFromCompareToolStripMenuItem_Click;
      viewSourceChangesetDetailsToolStripMenuItem1.Image = (Image) componentResourceManager.GetObject("viewSourceChangesetDetailsToolStripMenuItem1.Image");
      viewSourceChangesetDetailsToolStripMenuItem1.Name = "viewSourceChangesetDetailsToolStripMenuItem1";
      viewSourceChangesetDetailsToolStripMenuItem1.Size = new Size(222, 22);
      viewSourceChangesetDetailsToolStripMenuItem1.Text = "View Source Changeset Details";
      viewSourceChangesetDetailsToolStripMenuItem1.Click += viewSourceChangesetDetailsToolStripMenuItem1_Click;
      viewTargetChangesetDetailsToolStripMenuItem1.Image = (Image) componentResourceManager.GetObject("viewTargetChangesetDetailsToolStripMenuItem1.Image");
      viewTargetChangesetDetailsToolStripMenuItem1.Name = "viewTargetChangesetDetailsToolStripMenuItem1";
      viewTargetChangesetDetailsToolStripMenuItem1.Size = new Size(222, 22);
      viewTargetChangesetDetailsToolStripMenuItem1.Text = "View Target Changeset Details";
      viewTargetChangesetDetailsToolStripMenuItem1.Click += viewTargetChangesetDetailsToolStripMenuItem1_Click;
      groupBoxMergeTo.Controls.Add(listViewMergesTo);
      groupBoxMergeTo.Dock = DockStyle.Fill;
      groupBoxMergeTo.Location = new Point(3, 3);
      groupBoxMergeTo.Name = "groupBoxMergeTo";
      groupBoxMergeTo.Size = new Size(558, 240);
      groupBoxMergeTo.TabIndex = 8;
      groupBoxMergeTo.TabStop = false;
      groupBoxMergeTo.Text = " Merges To Item";
      listViewMergesTo.AllowColumnReorder = true;
      listViewMergesTo.BorderStyle = BorderStyle.None;
      listViewMergesTo.Columns.AddRange(new ColumnHeader[5]
      {
        columnHeader15,
        columnHeader11,
        columnHeader12,
        columnHeader9,
        columnHeader10
      });
      listViewMergesTo.ContextMenuStrip = contextMenuStripMergeTo;
      listViewMergesTo.Dock = DockStyle.Fill;
      listViewMergesTo.FullRowSelect = true;
      listViewMergesTo.GridLines = true;
      listViewMergesTo.HideSelection = false;
      listViewMergesTo.Location = new Point(3, 16);
      listViewMergesTo.MultiSelect = false;
      listViewMergesTo.Name = "listViewMergesTo";
      listViewMergesTo.Size = new Size(552, 221);
      listViewMergesTo.Sorting = SortOrder.Descending;
      listViewMergesTo.TabIndex = 7;
      listViewMergesTo.UseCompatibleStateImageBehavior = false;
      listViewMergesTo.View = View.Details;
      listViewMergesTo.ColumnClick += listViewHistory_ColumnClick;
      columnHeader15.Text = "From Item";
      columnHeader15.Width = 200;
      columnHeader11.Text = "Source";
      columnHeader11.Width = 50;
      columnHeader12.Text = "Target";
      columnHeader12.Width = 50;
      columnHeader9.Text = "Author";
      columnHeader9.Width = 100;
      columnHeader10.Text = "Merge date";
      columnHeader10.Width = 120;
      contextMenuStripMergeTo.Items.AddRange(new ToolStripItem[3]
      {
        compareToolStripMenuItem,
        viewSourceChangesetDetailsToolStripMenuItem,
        viewTargetChangesetDetailsToolStripMenuItem
      });
      contextMenuStripMergeTo.Name = "contextMenuStripMergeTo";
      contextMenuStripMergeTo.Size = new Size(223, 70);
      contextMenuStripMergeTo.Opening += contextMenuStripMergeTo_Opening;
      compareToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("compareToolStripMenuItem.Image");
      compareToolStripMenuItem.Name = "compareToolStripMenuItem";
      compareToolStripMenuItem.Size = new Size(222, 22);
      compareToolStripMenuItem.Text = "Compare Source To Target";
      compareToolStripMenuItem.Click += MergeToCompareToolStripMenuItem_Click;
      viewSourceChangesetDetailsToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("viewSourceChangesetDetailsToolStripMenuItem.Image");
      viewSourceChangesetDetailsToolStripMenuItem.Name = "viewSourceChangesetDetailsToolStripMenuItem";
      viewSourceChangesetDetailsToolStripMenuItem.Size = new Size(222, 22);
      viewSourceChangesetDetailsToolStripMenuItem.Text = "View Source Changeset Details";
      viewSourceChangesetDetailsToolStripMenuItem.Click += viewSourceChangesetDetailsToolStripMenuItem_Click;
      viewTargetChangesetDetailsToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("viewTargetChangesetDetailsToolStripMenuItem.Image");
      viewTargetChangesetDetailsToolStripMenuItem.Name = "viewTargetChangesetDetailsToolStripMenuItem";
      viewTargetChangesetDetailsToolStripMenuItem.Size = new Size(222, 22);
      viewTargetChangesetDetailsToolStripMenuItem.Text = "View Target Changeset Details";
      viewTargetChangesetDetailsToolStripMenuItem.Click += viewTargetChangesetDetailsToolStripMenuItem_Click;
      tabPageMergeCandidates.BackColor = SystemColors.Control;
      tabPageMergeCandidates.Controls.Add(splitContainerMergeCandidates);
      tabPageMergeCandidates.Controls.Add(toolStrip3);
      tabPageMergeCandidates.Controls.Add(groupBoxCandidates);
      tabPageMergeCandidates.Location = new Point(4, 22);
      tabPageMergeCandidates.Name = "tabPageMergeCandidates";
      tabPageMergeCandidates.Padding = new Padding(3);
      tabPageMergeCandidates.Size = new Size(564, 492);
      tabPageMergeCandidates.TabIndex = 5;
      tabPageMergeCandidates.Text = "Merge candidates";
      splitContainerMergeCandidates.Dock = DockStyle.Fill;
      splitContainerMergeCandidates.Location = new Point(3, 119);
      splitContainerMergeCandidates.Name = "splitContainerMergeCandidates";
      splitContainerMergeCandidates.Panel1.Controls.Add(treeViewMergeTargets);
      splitContainerMergeCandidates.Panel2.Controls.Add(listViewMergeReport);
      splitContainerMergeCandidates.Size = new Size(558, 370);
      splitContainerMergeCandidates.SplitterDistance = 186;
      splitContainerMergeCandidates.TabIndex = 11;
      treeViewMergeTargets.Dock = DockStyle.Fill;
      treeViewMergeTargets.FullRowSelect = true;
      treeViewMergeTargets.HideSelection = false;
      treeViewMergeTargets.ImageIndex = 0;
      treeViewMergeTargets.ImageList = imageList;
      treeViewMergeTargets.Location = new Point(0, 0);
      treeViewMergeTargets.Name = "treeViewMergeTargets";
      treeViewMergeTargets.SelectedImageIndex = 0;
      treeViewMergeTargets.Size = new Size(186, 370);
      treeViewMergeTargets.TabIndex = 12;
      treeViewMergeTargets.AfterSelect += treeViewMergeTargets_AfterSelect;
      listViewMergeReport.Columns.AddRange(new ColumnHeader[5]
      {
        columnHeader20,
        columnHeader21,
        columnHeader23,
        columnHeader24,
        columnHeader29
      });
      listViewMergeReport.ContextMenuStrip = contextMenuStripMergeCandidate;
      listViewMergeReport.Dock = DockStyle.Fill;
      listViewMergeReport.FullRowSelect = true;
      listViewMergeReport.GridLines = true;
      listViewMergeReport.HideSelection = false;
      listViewMergeReport.Location = new Point(0, 0);
      listViewMergeReport.MultiSelect = false;
      listViewMergeReport.Name = "listViewMergeReport";
      listViewMergeReport.Size = new Size(368, 370);
      listViewMergeReport.TabIndex = 11;
      listViewMergeReport.UseCompatibleStateImageBehavior = false;
      listViewMergeReport.View = View.Details;
      listViewMergeReport.SelectedIndexChanged += listViewMergeReport_SelectedIndexChanged;
      columnHeader20.Text = "Changeset";
      columnHeader20.Width = 70;
      columnHeader21.Text = "Change";
      columnHeader21.Width = 100;
      columnHeader23.Text = "Change Date";
      columnHeader23.Width = 120;
      columnHeader24.Text = "Owner";
      columnHeader24.Width = 100;
      columnHeader29.Text = "Comment";
      columnHeader29.Width = 100;
      contextMenuStripMergeCandidate.Items.AddRange(new ToolStripItem[2]
      {
        toolStripMenuItem2,
        viewChangesetDetailsToolStripMenuItem2
      });
      contextMenuStripMergeCandidate.Name = "contextMenuStripMerge";
      contextMenuStripMergeCandidate.Size = new Size(218, 48);
      contextMenuStripMergeCandidate.Opening += contextMenuStripMergeCandidate_Opening;
      toolStripMenuItem2.Image = (Image) componentResourceManager.GetObject("toolStripMenuItem2.Image");
      toolStripMenuItem2.Name = "toolStripMenuItem2";
      toolStripMenuItem2.Size = new Size(217, 22);
      toolStripMenuItem2.Text = "Compare Candidate To Latest";
      toolStripMenuItem2.Click += toolStripMenuItemCompareMergeCandidate_Click;
      viewChangesetDetailsToolStripMenuItem2.Image = (Image) componentResourceManager.GetObject("viewChangesetDetailsToolStripMenuItem2.Image");
      viewChangesetDetailsToolStripMenuItem2.Name = "viewChangesetDetailsToolStripMenuItem2";
      viewChangesetDetailsToolStripMenuItem2.Size = new Size(217, 22);
      viewChangesetDetailsToolStripMenuItem2.Text = "View Changeset Details";
      viewChangesetDetailsToolStripMenuItem2.Click += viewChangesetDetailsToolStripMenuItem2_Click;
      toolStrip3.GripStyle = ToolStripGripStyle.Hidden;
      toolStrip3.Items.AddRange(new ToolStripItem[5]
      {
        toolStripButtonToggleTreeMode,
        toolStripSeparator1,
        toolStripButtonChangesetDetails,
        toolStripSeparator3,
        toolStripButtonSaveList
      });
      toolStrip3.Location = new Point(3, 94);
      toolStrip3.Name = "toolStrip3";
      toolStrip3.RenderMode = ToolStripRenderMode.System;
      toolStrip3.Size = new Size(558, 25);
      toolStrip3.TabIndex = 12;
      toolStrip3.Text = "toolStrip3";
      toolStripButtonToggleTreeMode.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonToggleTreeMode.Image = (Image) componentResourceManager.GetObject("toolStripButtonToggleTreeMode.Image");
      toolStripButtonToggleTreeMode.ImageTransparentColor = Color.Magenta;
      toolStripButtonToggleTreeMode.Name = "toolStripButtonToggleTreeMode";
      toolStripButtonToggleTreeMode.Size = new Size(23, 22);
      toolStripButtonToggleTreeMode.Text = "Toggle tree mode";
      toolStripButtonToggleTreeMode.Click += toolStripButtonToggleTreeMode_Click;
      toolStripSeparator1.Name = "toolStripSeparator1";
      toolStripSeparator1.Size = new Size(6, 25);
      toolStripButtonChangesetDetails.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonChangesetDetails.Enabled = false;
      toolStripButtonChangesetDetails.Image = (Image) componentResourceManager.GetObject("toolStripButtonChangesetDetails.Image");
      toolStripButtonChangesetDetails.ImageTransparentColor = Color.Magenta;
      toolStripButtonChangesetDetails.Name = "toolStripButtonChangesetDetails";
      toolStripButtonChangesetDetails.Size = new Size(23, 22);
      toolStripButtonChangesetDetails.Text = "View Changeset Details";
      toolStripButtonChangesetDetails.Click += toolStripButtonChangesetDetails_Click;
      toolStripSeparator3.Name = "toolStripSeparator3";
      toolStripSeparator3.Size = new Size(6, 25);
      toolStripButtonSaveList.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonSaveList.Image = (Image) componentResourceManager.GetObject("toolStripButtonSaveList.Image");
      toolStripButtonSaveList.ImageTransparentColor = Color.Black;
      toolStripButtonSaveList.Name = "toolStripButtonSaveList";
      toolStripButtonSaveList.Size = new Size(23, 22);
      toolStripButtonSaveList.Text = "Save list of changesets";
      toolStripButtonSaveList.Click += toolStripButtonSaveList_Click;
      groupBoxCandidates.Controls.Add(label4);
      groupBoxCandidates.Controls.Add(label1);
      groupBoxCandidates.Controls.Add(textBoxMergeTo);
      groupBoxCandidates.Controls.Add(comboBoxMergeFrom);
      groupBoxCandidates.Dock = DockStyle.Top;
      groupBoxCandidates.Location = new Point(3, 3);
      groupBoxCandidates.Name = "groupBoxCandidates";
      groupBoxCandidates.Size = new Size(558, 91);
      groupBoxCandidates.TabIndex = 7;
      groupBoxCandidates.TabStop = false;
      groupBoxCandidates.Text = " Candidate filter criteria";
      label4.AutoSize = true;
      label4.Location = new Point(15, 64);
      label4.Name = "label4";
      label4.Size = new Size(60, 13);
      label4.TabIndex = 14;
      label4.Text = "Merge from";
      label1.AutoSize = true;
      label1.Location = new Point(15, 27);
      label1.Name = "label1";
      label1.Size = new Size(49, 13);
      label1.TabIndex = 13;
      label1.Text = "Merge to";
      textBoxMergeTo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      textBoxMergeTo.Location = new Point(sbyte.MaxValue, 24);
      textBoxMergeTo.Name = "textBoxMergeTo";
      textBoxMergeTo.ReadOnly = true;
      textBoxMergeTo.Size = new Size(425, 20);
      textBoxMergeTo.TabIndex = 12;
      comboBoxMergeFrom.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      comboBoxMergeFrom.DropDownStyle = ComboBoxStyle.DropDownList;
      comboBoxMergeFrom.FormattingEnabled = true;
      comboBoxMergeFrom.Location = new Point(sbyte.MaxValue, 61);
      comboBoxMergeFrom.MaxDropDownItems = 16;
      comboBoxMergeFrom.Name = "comboBoxMergeFrom";
      comboBoxMergeFrom.Size = new Size(425, 21);
      comboBoxMergeFrom.TabIndex = 11;
      comboBoxMergeFrom.SelectedIndexChanged += comboBoxMergeFrom_SelectedIndexChanged;
      tabPageLabels.BackColor = SystemColors.Control;
      tabPageLabels.Controls.Add(listViewLabels);
      tabPageLabels.Controls.Add(toolStrip4);
      tabPageLabels.Controls.Add(groupBoxLabel);
      tabPageLabels.Location = new Point(4, 22);
      tabPageLabels.Name = "tabPageLabels";
      tabPageLabels.Size = new Size(564, 492);
      tabPageLabels.TabIndex = 4;
      tabPageLabels.Text = "Labels";
      listViewLabels.AllowColumnReorder = true;
      listViewLabels.BorderStyle = BorderStyle.None;
      listViewLabels.Columns.AddRange(new ColumnHeader[5]
      {
        columnHeader28,
        columnHeader22,
        columnHeader25,
        columnHeader26,
        columnHeader27
      });
      listViewLabels.ContextMenuStrip = contextMenuStripLabels;
      listViewLabels.Dock = DockStyle.Fill;
      listViewLabels.FullRowSelect = true;
      listViewLabels.GridLines = true;
      listViewLabels.HideSelection = false;
      listViewLabels.Location = new Point(0, 74);
      listViewLabels.Name = "listViewLabels";
      listViewLabels.Size = new Size(564, 418);
      listViewLabels.Sorted = false;
      listViewLabels.Sorting = SortOrder.Descending;
      listViewLabels.TabIndex = 15;
      listViewLabels.UseCompatibleStateImageBehavior = false;
      listViewLabels.View = View.Details;
      listViewLabels.SelectedIndexChanged += listViewLabels_SelectedIndexChanged;
      columnHeader28.Text = "Changeset";
      columnHeader28.Width = 65;
      columnHeader22.Text = "Name";
      columnHeader22.Width = 120;
      columnHeader25.Text = "Owner name";
      columnHeader25.Width = 100;
      columnHeader26.Text = "Last Modified Date";
      columnHeader26.Width = 120;
      columnHeader27.Text = "Comment";
      columnHeader27.Width = 200;
      contextMenuStripLabels.Items.AddRange(new ToolStripItem[3]
      {
        toolStripMenuItemCompareLabels,
        toolStripMenuItemCompareLabelLatest,
        viewChangesetDetailsToolStripMenuItem1
      });
      contextMenuStripLabels.Name = "contextMenuStripMerge";
      contextMenuStripLabels.Size = new Size(214, 70);
      contextMenuStripLabels.Opening += contextMenuStripLabels_Opening;
      toolStripMenuItemCompareLabels.Image = (Image) componentResourceManager.GetObject("toolStripMenuItemCompareLabels.Image");
      toolStripMenuItemCompareLabels.Name = "toolStripMenuItemCompareLabels";
      toolStripMenuItemCompareLabels.Size = new Size(213, 22);
      toolStripMenuItemCompareLabels.Text = "Compare Label Versions";
      toolStripMenuItemCompareLabels.Click += toolStripMenuItemCompareLabel_Click;
      toolStripMenuItemCompareLabelLatest.Image = (Image) componentResourceManager.GetObject("toolStripMenuItemCompareLabelLatest.Image");
      toolStripMenuItemCompareLabelLatest.Name = "toolStripMenuItemCompareLabelLatest";
      toolStripMenuItemCompareLabelLatest.Size = new Size(213, 22);
      toolStripMenuItemCompareLabelLatest.Text = "Compare With Latest Version";
      toolStripMenuItemCompareLabelLatest.Click += toolStripMenuItemCompareLabel_Click;
      viewChangesetDetailsToolStripMenuItem1.Image = (Image) componentResourceManager.GetObject("viewChangesetDetailsToolStripMenuItem1.Image");
      viewChangesetDetailsToolStripMenuItem1.Name = "viewChangesetDetailsToolStripMenuItem1";
      viewChangesetDetailsToolStripMenuItem1.Size = new Size(213, 22);
      viewChangesetDetailsToolStripMenuItem1.Text = "View Changeset Details";
      viewChangesetDetailsToolStripMenuItem1.Click += viewChangesetDetailsToolStripMenuItem1_Click;
      toolStrip4.AllowMerge = false;
      toolStrip4.CanOverflow = false;
      toolStrip4.GripMargin = new Padding(1);
      toolStrip4.GripStyle = ToolStripGripStyle.Hidden;
      toolStrip4.Items.AddRange(new ToolStripItem[1]
      {
        toolStripButtonChangesetDetails3
      });
      toolStrip4.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
      toolStrip4.Location = new Point(0, 49);
      toolStrip4.Name = "toolStrip4";
      toolStrip4.Padding = new Padding(3, 1, 1, 1);
      toolStrip4.RenderMode = ToolStripRenderMode.System;
      toolStrip4.Size = new Size(564, 25);
      toolStrip4.Stretch = true;
      toolStrip4.TabIndex = 14;
      toolStripButtonChangesetDetails3.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonChangesetDetails3.Enabled = false;
      toolStripButtonChangesetDetails3.Image = (Image) componentResourceManager.GetObject("toolStripButtonChangesetDetails3.Image");
      toolStripButtonChangesetDetails3.ImageTransparentColor = Color.Magenta;
      toolStripButtonChangesetDetails3.Name = "toolStripButtonChangesetDetails3";
      toolStripButtonChangesetDetails3.Size = new Size(23, 20);
      toolStripButtonChangesetDetails3.Text = "View Changeset Details";
      toolStripButtonChangesetDetails3.Click += toolStripButtonChangesetDetails3_Click;
      groupBoxLabel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      groupBoxLabel.Controls.Add(label5);
      groupBoxLabel.Controls.Add(comboBoxUserNameLabel);
      groupBoxLabel.Dock = DockStyle.Top;
      groupBoxLabel.Location = new Point(0, 0);
      groupBoxLabel.Name = "groupBoxLabel";
      groupBoxLabel.Size = new Size(564, 49);
      groupBoxLabel.TabIndex = 7;
      groupBoxLabel.TabStop = false;
      groupBoxLabel.Text = " Label filter criteria";
      label5.AutoSize = true;
      label5.Location = new Point(8, 23);
      label5.Name = "label5";
      label5.Size = new Size(58, 13);
      label5.TabIndex = 12;
      label5.Text = "User name";
      comboBoxUserNameLabel.AutoCompleteMode = AutoCompleteMode.Suggest;
      comboBoxUserNameLabel.AutoCompleteSource = AutoCompleteSource.ListItems;
      comboBoxUserNameLabel.FormattingEnabled = true;
      comboBoxUserNameLabel.IntegralHeight = false;
      comboBoxUserNameLabel.ItemHeight = 13;
      comboBoxUserNameLabel.Location = new Point(100, 19);
      comboBoxUserNameLabel.MaxDropDownItems = 32;
      comboBoxUserNameLabel.Name = "comboBoxUserNameLabel";
      comboBoxUserNameLabel.Size = new Size(202, 21);
      comboBoxUserNameLabel.TabIndex = 1;
      comboBoxUserNameLabel.SelectedValueChanged += comboBoxUserNameLabel_SelectedValueChanged;
      comboBoxUserNameLabel.KeyUp += comboBoxUserNameLabel_KeyUp;
      columnHeader19.DisplayIndex = 2;
      columnHeader19.Width = 100;
      columnHeader18.DisplayIndex = 1;
      columnHeader18.Width = 200;
      toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButton1.Image = (Image) componentResourceManager.GetObject("toolStripButton1.Image");
      toolStripButton1.ImageTransparentColor = Color.Black;
      toolStripButton1.Name = "toolStripButton1";
      toolStripButton1.Size = new Size(23, 20);
      toolStripButton1.Text = "toolStripButton5";
      toolStripButton1.ToolTipText = "Save list to file";
      toolStripSeparator4.Name = "toolStripSeparator4";
      toolStripSeparator4.Size = new Size(6, 23);
      toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButton2.Enabled = false;
      toolStripButton2.Image = (Image) componentResourceManager.GetObject("toolStripButton2.Image");
      toolStripButton2.ImageTransparentColor = Color.Magenta;
      toolStripButton2.Name = "toolStripButton2";
      toolStripButton2.Size = new Size(23, 20);
      toolStripButton2.Text = "View Changeset Details";
      AutoScaleDimensions = new SizeF(6f, 13f);
      AutoScaleMode = AutoScaleMode.Font;
      Controls.Add(splitContainer1);
      Name = nameof (HistoryViewControl);
      Size = new Size(931, 525);
      splitContainer1.Panel1.ResumeLayout(false);
      splitContainer1.Panel1.PerformLayout();
      splitContainer1.Panel2.ResumeLayout(false);
      splitContainer1.EndInit();
      splitContainer1.ResumeLayout(false);
      toolStrip1.ResumeLayout(false);
      toolStrip1.PerformLayout();
      tabControl1.ResumeLayout(false);
      tabPageHistory.ResumeLayout(false);
      tabPageHistory.PerformLayout();
      contextMenuStripHistory.ResumeLayout(false);
      toolStrip2.ResumeLayout(false);
      toolStrip2.PerformLayout();
      groupBox2.ResumeLayout(false);
      groupBox2.PerformLayout();
      tabPageProperties.ResumeLayout(false);
      groupBoxPendingChanges.ResumeLayout(false);
      groupBoxProperties.ResumeLayout(false);
      tabPageBranches.ResumeLayout(false);
      groupBoxBranches.ResumeLayout(false);
      groupBoxBranchProperties.ResumeLayout(false);
      tabPageMerges.ResumeLayout(false);
      tableLayoutPanelMerge.ResumeLayout(false);
      groupBoxMergeFrom.ResumeLayout(false);
      contextMenuStripMergeFrom.ResumeLayout(false);
      groupBoxMergeTo.ResumeLayout(false);
      contextMenuStripMergeTo.ResumeLayout(false);
      tabPageMergeCandidates.ResumeLayout(false);
      tabPageMergeCandidates.PerformLayout();
      splitContainerMergeCandidates.Panel1.ResumeLayout(false);
      splitContainerMergeCandidates.Panel2.ResumeLayout(false);
      splitContainerMergeCandidates.EndInit();
      splitContainerMergeCandidates.ResumeLayout(false);
      contextMenuStripMergeCandidate.ResumeLayout(false);
      toolStrip3.ResumeLayout(false);
      toolStrip3.PerformLayout();
      groupBoxCandidates.ResumeLayout(false);
      groupBoxCandidates.PerformLayout();
      tabPageLabels.ResumeLayout(false);
      tabPageLabels.PerformLayout();
      contextMenuStripLabels.ResumeLayout(false);
      toolStrip4.ResumeLayout(false);
      toolStrip4.PerformLayout();
      groupBoxLabel.ResumeLayout(false);
      groupBoxLabel.PerformLayout();
      ResumeLayout(false);
    }

    public override void Initialize(TfsController controller)
    {
      _controller = new HistoryViewController(controller);
      _dtUsers = _controller.Users.UsersTable.Copy();
      ListTable.AddAllRow(_dtUsers);
      _dtProjects = _controller.GetProjects(false);
      ListTable.AddAllRow(_dtProjects);
      LoadSearchParameters();
      DefaultSearchParameters();
      FillInitialTree();
    }

    public override void LoadUsers(TfsController controller)
    {
      var text1 = comboBoxUserName.Text;
      var text2 = comboBoxUserNameLabel.Text;
      _dtUsers = controller.Users.UsersTable.Copy();
      ListTable.AddAllRow(_dtUsers);
      ListTable.LoadTable(comboBoxUserName, _dtUsers, "");
      if (!string.IsNullOrEmpty(text1))
        comboBoxUserName.SelectedValue = text1;
      ListTable.LoadTable(comboBoxUserNameLabel, _dtUsers, "");
      if (string.IsNullOrEmpty(text2))
        return;
      comboBoxUserNameLabel.SelectedValue = text2;
    }

    private void DefaultSearchParameters()
    {
    }

    private void LoadSearchParameters()
    {
      ListTable.LoadTable(comboBoxUserName, _dtUsers, "");
      ListTable.LoadTable(comboBoxUserNameLabel, _dtUsers, "");
    }

    private void FillInitialTree()
    {
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        treeViewSourceCode.BeginUpdate();
        FillTree(_controller.GetFirstLevelItems(), true);
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to retrieve items." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        treeViewSourceCode.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
    }

    private void FillSubTree(TreeNode node)
    {
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        treeViewSourceCode.BeginUpdate();
        FillTree(_controller.GetSubItems((node.Tag as Item).ServerItem), false);
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to retrieve items." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        treeViewSourceCode.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
    }

    private void RefreshSubTree(string serverItem)
    {
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        treeViewSourceCode.BeginUpdate();
        treeViewSourceCode.Nodes.Clear();
        if (serverItem == "$/")
        {
          FillInitialTree();
        }
        else
        {
          var stringBuilder = new StringBuilder();
          var str1 = serverItem;
          var separator = new char[1]{ '/' };
          foreach (var str2 in str1.Split(separator, StringSplitOptions.RemoveEmptyEntries))
          {
            stringBuilder.Append(str2);
            stringBuilder.Append('/');
            FillTree(_controller.GetSubItems(stringBuilder.ToString()), false);
          }
          SelectInitialTreeNodes(stringBuilder.ToString());
        }
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to retrieve items." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        treeViewSourceCode.EndUpdate();
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
          if (treeNode.Tag is Item tag)
          {
            if (tag.ItemType == ItemType.Folder && string.Compare(treeNode.Text, text, true) < 0)
              num = treeNode.Index;
            else
              return tag.ItemType == ItemType.Folder ? nodesParent.Insert(treeNode.Index + 1, text) : nodesParent.Insert(treeNode.Index, text);
          }
        }
        return nodesParent.Insert(num + 1, text);
      }
      foreach (TreeNode treeNode in nodesParent)
      {
        if (treeNode.Tag is Item tag && tag.ItemType != ItemType.Folder && (tag.ItemType != ItemType.File || string.Compare(treeNode.Text, text) >= 0))
          return nodesParent.Insert(treeNode.Index, text);
      }
      return nodesParent.Add(text);
    }

    private void FillTree(Item[] items, bool refreshSelected)
    {
      string selectedNodeName = null;
      if (treeViewSourceCode.SelectedNode != null)
        selectedNodeName = treeViewSourceCode.SelectedNode.Name;
      foreach (var obj in items)
      {
        var treeNode1 = AddTreeNode(treeViewSourceCode, obj.ServerItem, obj.ItemType == ItemType.Folder);
        if (!(treeNode1.Tag is Item))
        {
          treeNode1.Tag = obj;
          if (obj.ItemType == ItemType.Folder)
          {
            if (treeNode1.Parent == null)
            {
              var treeNode2 = treeNode1;
              int num1;
              var num2 = num1 = 0;
              treeNode2.SelectedImageIndex = num1;
              treeNode2.ImageIndex = num2;
            }
            else if (treeNode1.Level == 1)
            {
              var treeNode3 = treeNode1;
              int num3;
              var num4 = num3 = 1;
              treeNode3.SelectedImageIndex = num3;
              treeNode3.ImageIndex = num4;
            }
            else if (obj.IsBranch)
            {
              var treeNode4 = treeNode1;
              int num5;
              var num6 = num5 = 7;
              treeNode4.SelectedImageIndex = num5;
              treeNode4.ImageIndex = num6;
            }
            else
            {
              treeNode1.SelectedImageIndex = 3;
              treeNode1.ImageIndex = 2;
            }
            treeNode1.Nodes.Add(new TreeNode("placeholder")
            {
              Name = "placeholder"
            });
          }
          else
          {
            var treeNode5 = treeNode1;
            int num7;
            var num8 = num7 = 4;
            treeNode5.SelectedImageIndex = num7;
            treeNode5.ImageIndex = num8;
          }
        }
      }
      if (!refreshSelected)
        return;
      SelectInitialTreeNodes(selectedNodeName);
    }

    private void SelectInitialTreeNodes(string selectedNodeName)
    {
      if (treeViewSourceCode.Nodes.Count > 0 && selectedNodeName != null)
      {
        var treeNodeArray = treeViewSourceCode.Nodes.Find(selectedNodeName, true);
        if (treeNodeArray.Length == 1)
          treeViewSourceCode.SelectedNode = treeNodeArray[0];
      }
      if (treeViewSourceCode.Nodes.Count <= 0)
        return;
      treeViewSourceCode.Nodes[0].Expand();
    }

    private void ShowTab()
    {
      if (tabControl1.SelectedTab == tabPageHistory)
        ShowHistoryTab(false);
      else if (tabControl1.SelectedTab == tabPageProperties)
        ShowPropertiesTab();
      else if (tabControl1.SelectedTab == tabPageBranches)
        ShowBranchesTab();
      else if (tabControl1.SelectedTab == tabPageMerges)
        ShowMergesTab();
      else if (tabControl1.SelectedTab == tabPageMergeCandidates)
      {
        ShowMergeCandidates();
      }
      else
      {
        if (tabControl1.SelectedTab != tabPageLabels)
          return;
        ShowLabelsTab(false);
      }
    }

    private void SelectNode(TreeNode node)
    {
      foreach (Control tabPage in tabControl1.TabPages)
        tabPage.Tag = null;
      if (node == null || node.Text == "$")
        return;
      tabControl1.Tag = node.Tag as Item;
      ShowTab();
    }

    private bool SetCurrentPage(TabPage page)
    {
      if (page.Tag is Item)
        return false;
      var tag = tabControl1.Tag as Item;
      page.Tag = tag;
      return true;
    }

    private Item GetSelectedItem() => tabControl1.Tag as Item;

    private void FillBranches()
    {
      var selectedItem = GetSelectedItem();
      textBoxMergeTo.Text = selectedItem.ServerItem;
      var branchRelatives = _controller.GetBranchRelatives(selectedItem);
      comboBoxMergeFrom.Items.Clear();
      foreach (var obj in branchRelatives)
        comboBoxMergeFrom.Items.Add(obj.ServerItem);
    }

    private TreeNode AddTreeNode(TreeView treeView, string serverItem, bool folder)
    {
      var stringBuilder = new StringBuilder();
      TreeNode treeNode1 = null;
      var treeNodeArray1 = treeView.Nodes.Find(serverItem, true);
      if (treeNodeArray1 != null && treeNodeArray1.Length == 1)
        return treeNodeArray1[0];
      var str = serverItem;
      var separator = new char[1]{ '/' };
      foreach (var text in str.Split(separator, StringSplitOptions.RemoveEmptyEntries))
      {
        stringBuilder.Append(text);
        stringBuilder.Append('/');
        var treeNodeArray2 = treeView.Nodes.Find(stringBuilder.ToString(), true);
        if (treeNodeArray2 == null || treeNodeArray2.Length == 0)
        {
          var nodesParent = treeNode1 != null ? treeNode1.Nodes : treeView.Nodes;
          var treeNode2 = !(text == Utilities.ParseServerItem(serverItem)) || folder ? InsertSortNode(nodesParent, text, true) : InsertSortNode(nodesParent, text, false);
          treeNode2.Name = stringBuilder.ToString();
          treeNode1 = treeNode2;
        }
        else
          treeNode1 = treeNodeArray2[0];
      }
      return treeNode1;
    }

    private void SaveToFile()
    {
      Utilities.SaveListViewToFile(saveFileDialog, listViewHistory,
          $"File history for '{(object)(tabControl1.Tag as Item).ServerItem}' as of {(object)DateTime.Now}");
    }

    private void ViewChangesetDetails(Changeset changeset)
    {
      using (var changesetDetails = new DialogChangesetDetails(_controller.VersionControl, changeset))
        changesetDetails.ShowDialog(this);
    }

    private void ViewChangesetDetailsForHistoryWindow()
    {
      int result;
      if (listViewHistory.SelectedIndices.Count != 1 || !int.TryParse(listViewHistory.Items[listViewHistory.SelectedIndices[0]].Text, out result))
        return;
      ViewChangesetDetails(_controller.GetChangeset(result));
    }

    private void ViewChangesetDetailsForLabelsWindow()
    {
      int result;
      if (listViewLabels.SelectedIndices.Count != 1 || !int.TryParse(listViewLabels.Items[listViewLabels.SelectedIndices[0]].Text, out result))
        return;
      ViewChangesetDetails(_controller.GetChangeset(result));
    }

    private void ViewSourceChangesetDetails(ListView lv)
    {
      int result;
      if (lv.SelectedIndices.Count != 1 || !int.TryParse(lv.Items[lv.SelectedIndices[0]].SubItems[1].Text, out result))
        return;
      ViewChangesetDetails(_controller.GetChangeset(result));
    }

    private void ViewTargetChangesetDetails(ListView lv)
    {
      int result;
      if (lv.SelectedIndices.Count != 1 || !int.TryParse(lv.Items[lv.SelectedIndices[0]].SubItems[2].Text, out result))
        return;
      ViewChangesetDetails(_controller.GetChangeset(result));
    }

    private void ViewChangesetDetailsMergeCandidates()
    {
      int result;
      if (listViewMergeReport.SelectedIndices.Count != 1 || !int.TryParse(listViewMergeReport.Items[listViewMergeReport.SelectedIndices[0]].Text, out result))
        return;
      ViewChangesetDetails(_controller.GetChangeset(result));
    }
  }
}
