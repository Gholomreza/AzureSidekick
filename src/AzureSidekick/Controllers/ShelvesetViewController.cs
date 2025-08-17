// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controllers.ShelvesetViewController
// Assembly: Attrice.TeamFoundation.Controllers.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=856a3140e4bb5441
// MVID: 1A935642-B37C-4D8D-B5A3-89C4554B1701
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controllers.12.dll

using System;
using System.Collections.Generic;
using Attrice.TeamFoundation.Common;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace Attrice.TeamFoundation.Controllers
{
  public class ShelvesetViewController: TfsController
  {
      public ShelvesetViewController(TfsController controller) : base(controller)
      {
      }

      public Shelveset[] SearchShelvesets(SearchParameters parameters)
    {
      var shelvesetArray = VersionControl.QueryShelvesets(parameters.FreeText, parameters.UserName);
      var shelvesetList = new List<Shelveset>();
      foreach (var shelveset in shelvesetArray)
      {
        if (parameters.FromDate <= shelveset.CreationDate && parameters.ToDate >= shelveset.CreationDate)
          shelvesetList.Add(shelveset);
      }
      return shelvesetList.ToArray();
    }

    public PendingSet[] GetShelvedChanges(Shelveset set)
    {
      return VersionControl.QueryShelvedChanges(set.Name, set.OwnerName, null, false);
    }

    public PendingChange GetShelvedChange(Shelveset set, PendingChange change)
    {
      var pendingSetArray = VersionControl.QueryShelvedChanges(set.Name, set.OwnerName, new ItemSpec[1]
      {
        new ItemSpec(change)
      }, true);
      if (pendingSetArray.Length != 1 || pendingSetArray[0].PendingChanges.Length != 1)
        throw new InvalidOperationException(nameof (GetShelvedChange));
      return pendingSetArray[0].PendingChanges[0];
    }

    public void DeleteShelveset(Shelveset set) => VersionControl.DeleteShelveset(set);
  }
}
