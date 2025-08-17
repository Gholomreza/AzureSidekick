// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controllers.HistoryViewController
// Assembly: Attrice.TeamFoundation.Controllers.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=856a3140e4bb5441
// MVID: 1A935642-B37C-4D8D-B5A3-89C4554B1701
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controllers.12.dll

using System.Collections;
using System.Collections.Generic;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.VersionControl.Common;

namespace Attrice.TeamFoundation.Controllers
{
  public class HistoryViewController : TfsController
  {
      public HistoryViewController(TfsController controller) : base(controller)
      {
      }

      public BranchObject[] GetItemBranchObjects(Item item)
    {
      VersionControl.QueryRootBranchObjects((RecursionType) 2);
      return VersionControl.QueryBranchObjects(new ItemIdentifier(item.ServerItem, VersionSpec.Latest), (RecursionType) 2);
    }

    public BranchHistoryTreeItem[][] GetItemBranches(Item item)
    {
      return VersionControl.GetBranchHistory(new ItemSpec[1]
      {
        new ItemSpec(item.ServerItem, 0)
      }, VersionSpec.Latest);
    }

    public VersionControlLabel[] GetItemLabels(Item item, string owner)
    {
      return VersionControl.QueryLabels(null, "$/" + VersionControlPath.GetTeamProjectName(item.ServerItem), owner, true, item.ServerItem, VersionSpec.Latest, false);
    }

    public Changeset GetChangeset(int id) => VersionControl.GetChangeset(id, true, false);

    public ChangesetMerge[] GetItemMerges(Item item)
    {
      return item.ItemType == ItemType.Folder ? VersionControl.QueryMerges(null, null, item.ServerItem, VersionSpec.Latest, null, null, (RecursionType) 2) : VersionControl.QueryMerges(null, null, item.ServerItem, VersionSpec.Latest, null, null, 0);
    }

    public ChangesetMerge[] GetItemMerges(
      Item targetItem,
      Item sourceItem,
      VersionSpec sourceVersion)
    {
      if (targetItem.ItemType == ItemType.Folder)
        return VersionControl.QueryMerges(sourceItem.ServerItem, sourceVersion, targetItem.ServerItem, VersionSpec.Latest, null, null, (RecursionType) 2);
      return targetItem.DeletionId != 0 ? VersionControl.QueryMerges(sourceItem.ServerItem, sourceVersion, targetItem.ServerItem, new ChangesetVersionSpec(targetItem.ChangesetId - 1), null, null, 0) : VersionControl.QueryMerges(sourceItem.ServerItem, sourceVersion, targetItem.ServerItem, VersionSpec.Latest, null, null, 0);
    }

    public PendingChangeProperties[] GetItemChanges(Item item)
    {
      var changePropertiesList = new List<PendingChangeProperties>();
      var versionControl = VersionControl;
      var itemSpecArray = new ItemSpec[1]
      {
        new ItemSpec(item.ServerItem, 0)
      };
      foreach (var queryPendingSet in versionControl.QueryPendingSets(itemSpecArray, null, null, false))
      {
        foreach (var pendingChange in queryPendingSet.PendingChanges)
          changePropertiesList.Add(new PendingChangeProperties(queryPendingSet.Computer, queryPendingSet.OwnerName, queryPendingSet.Name, pendingChange));
      }
      return changePropertiesList.ToArray();
    }

    public Changeset[] SearchHistory(Item item, string userName)
    {
      var dictionary = new Dictionary<int, Changeset>();
      IEnumerable enumerable;
      if (item.ItemType == ItemType.Folder)
      {
        foreach (Changeset changeset in VersionControl.QueryHistory(item.ServerItem, new ChangesetVersionSpec(item.ChangesetId), 0, 0, userName, new ChangesetVersionSpec(1), VersionSpec.Latest, int.MaxValue, true, false))
          dictionary.Add(changeset.ChangesetId, changeset);
        enumerable = VersionControl.QueryHistory(item.ServerItem, new ChangesetVersionSpec(item.ChangesetId), 0, (RecursionType) 2, userName, new ChangesetVersionSpec(1), VersionSpec.Latest, int.MaxValue, false, false);
      }
      else
        enumerable = VersionControl.QueryHistory(item.ServerItem, new ChangesetVersionSpec(item.ChangesetId), 0, 0, userName, new ChangesetVersionSpec(1), VersionSpec.Latest, int.MaxValue, true, false);
      var changesetList = new List<Changeset>();
      foreach (Changeset changeset in enumerable)
      {
        if (dictionary.ContainsKey(changeset.ChangesetId))
          changesetList.Add(dictionary[changeset.ChangesetId]);
        else
          changesetList.Add(changeset);
      }
      return changesetList.ToArray();
    }

    public ItemSet SearchItems(
      string searchPath,
      bool fullRecursion,
      ItemType itemType,
      bool showDeleted)
    {
      return VersionControl.GetItems(searchPath, VersionSpec.Latest, fullRecursion ? (RecursionType) 2 : (RecursionType) 1, showDeleted ? (DeletedState) 2 : 0, itemType, false);
    }

    public Item[] GetFirstLevelItems()
    {
      return VersionControl.GetItems("$/", VersionSpec.Latest, (RecursionType) 1, 0, 0, false).Items;
    }

    public Item[] GetSubItems(string serverItem)
    {
      return VersionControl.GetItems(new ItemSpec(serverItem, (RecursionType) 1), VersionSpec.Latest, 0, 0, (GetItemsOptions) 4).Items;
    }

    public Item[] GetBranchRelatives(Item item)
    {
      var objList = new List<Item>();
      var itemBranches = GetItemBranches(item);
      BranchHistoryTreeItem branchHistoryTreeItem = null;
      if (itemBranches.Length != 0 && itemBranches[0].Length != 0)
        branchHistoryTreeItem = itemBranches[0][0];
      if (branchHistoryTreeItem == null)
        return objList.ToArray();
      var requestedItem = branchHistoryTreeItem.GetRequestedItem();
      if (requestedItem.Parent != null && requestedItem.Parent.Relative.BranchToItem != null && requestedItem.Parent.Relative.BranchToItem.DeletionId == 0)
        objList.Add(requestedItem.Parent.Relative.BranchToItem);
      foreach (BranchHistoryTreeItem child in requestedItem.Children)
      {
        if (child.Relative.BranchToItem != null && child.Relative.BranchToItem.DeletionId == 0)
          objList.Add(child.Relative.BranchToItem);
      }
      return objList.ToArray();
    }

    public void CompareFiles(
      string fromFile,
      VersionSpec fromVersion,
      string toFile,
      VersionSpec toVersion)
    {
      Difference.VisualDiffFiles(VersionControl, fromFile, fromVersion, toFile, toVersion);
    }
  }
}
