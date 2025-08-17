// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controllers.StatusViewController
// Assembly: Attrice.TeamFoundation.Controllers.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=856a3140e4bb5441
// MVID: 1A935642-B37C-4D8D-B5A3-89C4554B1701
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controllers.12.dll

using System.Collections.Generic;
using Attrice.TeamFoundation.Common;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace Attrice.TeamFoundation.Controllers
{
  public class StatusViewController : TfsController
  {
    private Workspace _workspace;

    public StatusViewController(TfsController controller) : base(controller)
    {
    }

    public PendingChangeProperties[] SearchChanges(SearchParameters parameters)
    {
        var path = parameters.SourcePath != null ? parameters.SourcePath + "/*" : "$/";
        var strArray = new string[1]
      {
        path
      };
      var changePropertiesList = new List<PendingChangeProperties>();
      var queryPendingSets = VersionControl.QueryPendingSets(strArray, RecursionType.Full, null, parameters.UserName);
      // var queryPendingSets = VersionControl.QueryPendingSets(
      //     new ItemSpec[]{new ItemSpec(path, RecursionType.Full)},
      //     null,
      //     parameters.UserName
      // );
      foreach (var queryPendingSet in queryPendingSets)
      {
        foreach (var pendingChange in queryPendingSet.PendingChanges)
          changePropertiesList.Add(new PendingChangeProperties(queryPendingSet.Computer, queryPendingSet.OwnerName, queryPendingSet.Name, pendingChange));
      }
      return changePropertiesList.ToArray();
    }

    public void UnlockChange(PendingChangeProperties properties)
    {
      if (_workspace == null || _workspace.Name != properties.Name || _workspace.OwnerName != properties.OwnerName)
        _workspace = VersionControl.GetWorkspace(properties.Name, properties.OwnerName);
      _workspace.SetLock(properties.Change.ServerItem, 0);
    }

    public void UndoChange(PendingChangeProperties properties)
    {
      if (_workspace == null || _workspace.Name != properties.Name || _workspace.OwnerName != properties.OwnerName)
        _workspace = VersionControl.GetWorkspace(properties.Name, properties.OwnerName);
      _workspace.Undo(new PendingChange[1]
      {
        properties.Change
      });
    }
  }
}
