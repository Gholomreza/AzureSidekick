// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.BranchProperties
// Assembly: Attrice.TeamFoundation.Controls.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=1a24c887ab53aa6e
// MVID: 393BE899-3C29-4A70-86D7-B4B965E5A8F5
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controls.12.dll

using System.ComponentModel;
using Attrice.TeamFoundation.Common;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace Attrice.TeamFoundation.Controls
{
  internal class BranchProperties
  {
    private BranchHistoryTreeItem _item;

    [DisplayName("Server Path")]
    [Category("Parent Properties")]
    [Description("Branch parent''s server path")]
    public string ParentServerItem => _item.Relative.BranchFromItem != null ? _item.Relative.BranchFromItem.ServerItem : "";

    [DisplayName("Server Path")]
    [Category("Branch Properties")]
    [Description("Branch server path")]
    public string ServerItem => _item.Relative.BranchToItem == null ? "No permissions" : _item.Relative.BranchToItem.ServerItem;

    [DisplayName("Item Type")]
    [Category("Branch Properties")]
    [Description("Branch item type (either file or folder)")]
    public string ItemType => _item.Relative.BranchToItem == null ? "No permissions" : _item.Relative.BranchToItem.ItemType.ToString();

    [DisplayName("Creation Date")]
    [Category("Branch Properties")]
    [Description("Branch creation date")]
    public string BranchDate => _item.Relative.BranchToItem == null ? "No permissions" : Utilities.FormatDateTimeInvariant(_item.Relative.BranchToItem.CheckinDate);

    [DisplayName("Changeset")]
    [Category("Parent Properties")]
    [Description("ID of the changeset from which current branch originated")]
    public string ParentChangesetID => _item.Relative.BranchFromItem != null ? _item.Relative.BranchFromItem.ChangesetId.ToString() : "Not applicable";

    [DisplayName("Changeset")]
    [Category("Branch Properties")]
    [Description("ID of the changeset in which current branch was checked in")]
    public string ChangesetID => _item.Relative.BranchToItem == null ? "No permissions" : _item.Relative.BranchToItem.ChangesetId.ToString();

    [DisplayName("Status")]
    [Category("Branch Properties")]
    [Description("Status of branch item (Active or Deleted)")]
    public string Deleted
    {
      get
      {
        if (_item.Relative.BranchToItem == null)
          return "No permissions";
        return _item.Relative.BranchToItem.DeletionId != 0 ? nameof (Deleted) : "Active";
      }
    }

    public BranchProperties(BranchHistoryTreeItem item) => _item = item;
  }
}
