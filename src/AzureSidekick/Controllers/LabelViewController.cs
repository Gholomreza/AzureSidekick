// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controllers.LabelViewController
// Assembly: Attrice.TeamFoundation.Controllers.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=856a3140e4bb5441
// MVID: 1A935642-B37C-4D8D-B5A3-89C4554B1701
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controllers.12.dll

using System;
using System.Collections.Generic;
using System.Text;
using Attrice.TeamFoundation.Common;
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.VisualStudio.Services.Common;

namespace Attrice.TeamFoundation.Controllers
{
  public class LabelViewController : TfsController
  {
      public LabelViewController(TfsController controller) : base(controller)
      {
      }

      public VersionControlLabel GetLabelItems(VersionControlLabel label)
    {
      var now = DateTime.Now;
      var versionControlLabelArray = VersionControl.QueryLabels(label.Name, label.Scope, label.OwnerName, true);
      if (versionControlLabelArray.Length > 1)
        throw new InvalidOperationException("Failed to refresh label with QueryLabels");
      return versionControlLabelArray.Length == 0 ? null : versionControlLabelArray[0];
    }

    public VersionControlLabel[] SearchLabels(SearchParameters parameters)
    {
      var versionControlLabelArray = VersionControl.QueryLabels(parameters.FreeText, parameters.SourcePath, parameters.UserName, false);
      var versionControlLabelList = new List<VersionControlLabel>();
      foreach (var versionControlLabel in versionControlLabelArray)
      {
        if (parameters.FromDate <= versionControlLabel.LastModifiedDate && parameters.ToDate >= versionControlLabel.LastModifiedDate)
          versionControlLabelList.Add(versionControlLabel);
      }
      return versionControlLabelList.ToArray();
    }

    public void DeleteLabel(VersionControlLabel label)
    {
      VersionControl.DeleteLabel(label.Name, label.Scope);
    }

    public void UnlabelItem(VersionControlLabel label, Item item)
    {
      VersionControl.UnlabelItem(label.Name, label.Scope, new ItemSpec[1]
      {
        new ItemSpec(item.ServerItem, 0)
      }, new ChangesetVersionSpec(item.ChangesetId));
    }

    public Changeset GetChangesetDetails(int id)
    {
      return VersionControl.GetChangeset(id, true, false);
    }

    public SortedDictionary<int, Changeset> GetLabelChangesets(VersionControlLabel label)
    {
      var labelChangesets = new SortedDictionary<int, Changeset>();
      foreach (var obj in label.Items)
      {
        if (!labelChangesets.ContainsKey(obj.ChangesetId))
          labelChangesets[obj.ChangesetId] = VersionControl.GetChangeset(obj.ChangesetId, false, false);
      }
      return labelChangesets;
    }

    public SortedDictionary<string, Item> GetLabelLatest(VersionControlLabel label)
    {
      var labelLatest = new SortedDictionary<string, Item>(StringComparer.OrdinalIgnoreCase);
      var latestChangesetId = VersionControl.GetLatestChangesetId();
      var intList = new List<int>();
      foreach (var obj in label.Items)
        intList.Add(obj.ItemId);
      try
      {
        foreach (var obj in VersionControl.GetItems(intList.ToArray(), latestChangesetId, false))
          labelLatest[obj.ServerItem] = obj;
      }
      catch (Exception ex)
      {
      }
      return labelLatest;
    }

    public SortedDictionary<int, WorkItem> GetLabelWorkItems(
      SortedDictionary<int, Changeset> changesets)
    {
      var labelWorkItems = new SortedDictionary<int, WorkItem>();
      var service1 = (WorkItemStore) Server.Server.GetService(typeof (WorkItemStore));
      var service2 = (ILinking) Server.Server.GetService(typeof (ILinking));
      var stringList = new List<string>();
      foreach (var changeset in changesets.Values)
        stringList.Add(changeset.ArtifactUri.ToString());
      var referencingArtifacts = service2.GetReferencingArtifacts(stringList.ToArray(), new LinkFilter[1]
      {
        new LinkFilter
        {
          FilterType = 0,
          FilterValues = new string[1]{ "WorkItemTracking" }
        }
      });
      var stringBuilder = new StringBuilder();
      foreach (var artifact in referencingArtifacts)
      {
        var artifactId = LinkingUtilities.DecodeUri(artifact.Uri);
        stringBuilder.AppendFormat("{0},", artifactId.ToolSpecificId);
      }
      if (stringBuilder.Length == 0)
        return labelWorkItems;
      foreach (WorkItem workItem in service1.Query(
                   $"SELECT System.Id, System.Title, System.WorkItemType, System.State, System.ChangedBy, System.ChangedDate  FROM WorkItems WHERE System.Id IN ({(object)stringBuilder.ToString(0, stringBuilder.Length - 1)})"))
        labelWorkItems.Add(workItem.Id, workItem);
      return labelWorkItems;
    }
  }
}
