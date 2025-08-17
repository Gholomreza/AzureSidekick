// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controllers.ServerWrapper
// Assembly: Attrice.TeamFoundation.Controllers.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=856a3140e4bb5441
// MVID: 1A935642-B37C-4D8D-B5A3-89C4554B1701
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controllers.12.dll

using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Server;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace Attrice.TeamFoundation.Controllers
{
  public class ServerWrapper
  {
    private TfsTeamProjectCollection _server;
    private string _serverName;

    public static string[] GetRegisteredServers()
    {
      var projectCollections = RegisteredTfsConnections.GetProjectCollections();
      var stringList = new List<string>();
      foreach (var projectCollection in projectCollections)
      {
        if (!projectCollection.Offline)
          stringList.Add(projectCollection.Uri.ToString());
      }
      return stringList.ToArray();
    }

    public TfsTeamProjectCollection Server => _server;

    public ServerWrapper(string tfsServerName)
    {
      _server = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(tfsServerName));
      _server.EnsureAuthenticated();
      ServerName = _server.Name;
      ServerUri = _server.Uri.ToString();
    }

    public ServerWrapper(string tfsServerName, string userName, string password)
    {
      _server = new TfsTeamProjectCollection(new Uri(tfsServerName), new NetworkCredential(userName, password));
      _server.EnsureAuthenticated();
      ServerName = _server.Name;
      ServerUri = _server.Uri.ToString();
    }

    public VersionControlServer GetVersionControl()
    {
      return _server.GetService(typeof (VersionControlServer)) as VersionControlServer;
    }

    public WorkItemStore GetWorkItemStore()
    {
      return _server.GetService(typeof (WorkItemStore)) as WorkItemStore;
    }

    public ICommonStructureService GetCommonStructure()
    {
      return _server.GetService(typeof (ICommonStructureService)) as ICommonStructureService;
    }

    public IAuthorizationService GetAuthorizationService()
    {
      return _server.GetService(typeof (IAuthorizationService)) as IAuthorizationService;
    }

    public IIdentityManagementService GetIdentityManagement()
    {
      return _server.GetService<IIdentityManagementService>();
    }

    public string ServerUri { get; private set; }

    public string ServerName { get; private set; }

    public string WorkstationName => Workstation.Current.Name;
  }
}
