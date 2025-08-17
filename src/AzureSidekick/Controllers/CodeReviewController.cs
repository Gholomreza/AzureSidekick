// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controllers.CodeReviewController
// Assembly: Attrice.TeamFoundation.Controllers.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=856a3140e4bb5441
// MVID: 1A935642-B37C-4D8D-B5A3-89C4554B1701
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controllers.12.dll

using System.Collections.Generic;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.VisualStudio.Services.Common;

namespace Attrice.TeamFoundation.Controllers
{
  public class CodeReviewController:TfsController
  {
      public CodeReviewController(TfsController controller) : base(controller)
      {
      }

      public Changeset[] GetChangesetsFromIds(int[] changesets)
    {
      var changesetList = new List<Changeset>();
      foreach (var changeset in changesets)
        changesetList.Add(VersionControl.GetChangeset(changeset, true, false));
      return changesetList.ToArray();
    }

    public Changeset[] GetChangesetsFromWorkItem(WorkItem workItem)
    {
      var changesetList = new List<Changeset>();
      foreach (Link link in workItem.Links)
      {
        if (link is ExternalLink externalLink)
        {
          var artifactId = LinkingUtilities.DecodeUri(externalLink.LinkedArtifactUri);
          if (!(artifactId.ArtifactType != "Changeset"))
          {
            var changeset = VersionControl.GetChangeset(int.Parse(artifactId.ToolSpecificId));
            if (changeset != null)
              changesetList.Add(changeset);
          }
        }
      }
      return changesetList.ToArray();
    }

    public void CompareChanges(Change change1, Change change2)
    {
      Difference.VisualDiffFiles(VersionControl, change1.Item.ServerItem, new ChangesetVersionSpec(change1.Item.ChangesetId), change2.Item.ServerItem, new ChangesetVersionSpec(change2.Item.ChangesetId));
    }

    public void CompareWithPreviousVersion(Change change)
    {
      VersionSpec versionSpec = new ChangesetVersionSpec(change.Item.ChangesetId - 1);
      Difference.VisualDiffFiles(VersionControl, change.Item.ServerItem, versionSpec, change.Item.ServerItem, new ChangesetVersionSpec(change.Item.ChangesetId));
    }

    public void CompareWithVersion(Change change, int changesetId)
    {
      VersionSpec versionSpec = new ChangesetVersionSpec(changesetId);
      Difference.VisualDiffFiles(VersionControl, change.Item.ServerItem, versionSpec, change.Item.ServerItem, new ChangesetVersionSpec(change.Item.ChangesetId));
    }
  }
}
