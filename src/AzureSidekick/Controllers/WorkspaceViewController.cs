// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controllers.WorkspaceViewController
// Assembly: Attrice.TeamFoundation.Controllers.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=856a3140e4bb5441
// MVID: 1A935642-B37C-4D8D-B5A3-89C4554B1701
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controllers.12.dll

using System.Collections.Generic;
using Attrice.TeamFoundation.Common;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace Attrice.TeamFoundation.Controllers
{
  public class WorkspaceViewController: TfsController
  {
      public WorkspaceViewController(TfsController baseController) : base(baseController){}
  
    public Workspace[] SearchWorkspaces(SearchParameters parameters)
    {
      var workspaceArray = VersionControl.QueryWorkspaces(null, parameters.UserName, parameters.ComputerName);
      var workspaceList = new List<Workspace>();
      foreach (var workspace in workspaceArray)
      {
        if (parameters.FromDate <= workspace.LastAccessDate && parameters.ToDate >= workspace.LastAccessDate)
          workspaceList.Add(workspace);
      }
      return workspaceList.ToArray();
    }

    public void DeleteWorkspace(Workspace workspace)
    {
      VersionControl.DeleteWorkspace(workspace.Name, workspace.OwnerName);
    }

    public void UpdateWorkspaceComputer(Workspace workspace) => workspace.UpdateComputerName();

    public void DuplicateWorkspace(
      Workspace workspace,
      string workspaceName,
      string userName,
      string computerName,
      string comment)
    {
      VersionControl.CreateWorkspace(workspaceName, userName, comment, workspace.Folders, computerName, new WorkspacePermissionProfile("Private", null), true);
    }

    public void DeleteWorkspaceMapping(Workspace workspace, string serverPath)
    {
      foreach (var folder in workspace.Folders)
      {
        if (folder.ServerItem == serverPath)
        {
          workspace.DeleteMapping(folder);
          break;
        }
      }
    }

    public void AddWorkspaceMapping(Workspace workspace, string serverPath, string localPath)
    {
      workspace.CreateMapping(new WorkingFolder(serverPath, localPath, 0));
    }

    public void CloakWorkspaceMapping(Workspace workspace, string serverPath, bool cloak)
    {
      foreach (var folder in workspace.Folders)
      {
        if (folder.ServerItem == serverPath)
        {
          if (cloak && !folder.IsCloaked)
          {
            workspace.Cloak(serverPath);
            break;
          }
          if (cloak || !folder.IsCloaked)
            break;
          var workingFolder = new WorkingFolder(folder.ServerItem, workspace.GetLocalItemForServerItem(folder.ServerItem), 0);
          workspace.DeleteMapping(folder);
          workspace.CreateMapping(workingFolder);
          break;
        }
      }
    }
  }
}
