// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controllers.TfsController
// Assembly: Attrice.TeamFoundation.Controllers.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=856a3140e4bb5441
// MVID: 1A935642-B37C-4D8D-B5A3-89C4554B1701
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controllers.12.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using Attrice.TeamFoundation.Common;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.Server;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using TeamProject = Microsoft.TeamFoundation.VersionControl.Client.TeamProject;


namespace Attrice.TeamFoundation.Controllers
{
    public class TfsController
    {
        protected readonly string _organizationUrl;

        public static string GetBaseUrl(string collectionUrl)
        {
            if (string.IsNullOrEmpty(collectionUrl))
                return null;

            try
            {
                var uri = new Uri(collectionUrl);

                // حذف path
                var baseUri = $"{uri.Scheme}://{uri.Host}";

                // اضافه کردن پورت اگر وجود دارد
                if (!uri.IsDefaultPort)
                    baseUri += $":{uri.Port}";

                return baseUri;
            }
            catch
            {
                return null;
            }
        }



        public UsersList Users { get; }


        private ServerWrapper _serverTfs;
        private VersionControlServer _serverVc;
        private IIdentityManagementService _identityManagement;
        private ICommonStructureService _commonStructure;
        private WorkItemStore _workItemStore;
        private IAuthorizationService _authorizationService;
        private string _userFullName;


        public TfsController(string teamFoundationServer)
            : this(new ServerWrapper(teamFoundationServer))
        {
            _organizationUrl = teamFoundationServer;

        }

        public string UserDisplayName => _serverTfs.Server.AuthorizedIdentity.DisplayName;

        public string UserFullName
        {
            get
            {
                if (_userFullName == null)
                {
                    var foundationIdentity = _identityManagement.ReadIdentity(_serverTfs.Server.AuthorizedIdentity.Descriptor, (MembershipQuery)1, (ReadIdentityOptions)1);
                    _userFullName = foundationIdentity.GetAttribute("Domain", "") + "\\" + foundationIdentity.GetAttribute("Account", "");
                }
                return _userFullName;
            }
        }

        public TfsController(ServerWrapper server)
        {
            _serverTfs = server;
            _serverVc = _serverTfs.GetVersionControl();
            _workItemStore = _serverTfs.GetWorkItemStore();
            _identityManagement = _serverTfs.GetIdentityManagement();
            _commonStructure = _serverTfs.GetCommonStructure();
            _authorizationService = _serverTfs.GetAuthorizationService();
            Users = new UsersList(_identityManagement, UserFullName);
            Users.RetrieveUsers();
        }

        public TfsController(TfsController controller)
        {
          _serverTfs = controller._serverTfs;
          _serverVc = controller._serverVc;
          _workItemStore = controller._workItemStore;
          _identityManagement = controller._identityManagement;
          _commonStructure = controller._commonStructure;
          _authorizationService = controller._authorizationService;
          Users = controller.Users;
        }

        public static string[] GetTfsServers()
        {
            var projectCollections = RegisteredTfsConnections.GetProjectCollections();
            var stringList = new List<string>();
            foreach (var projectCollection in projectCollections)
                stringList.Add(projectCollection.Uri.ToString());
            return stringList.ToArray();
        }

        public VersionControlServer VersionControl => _serverVc;

        public WorkItemStore WorkItemStore => _workItemStore;

        public IIdentityManagementService IdentityManagement => _identityManagement;

        public ICommonStructureService CommonStructure => _commonStructure;

        public IAuthorizationService AuthorizationService => _authorizationService;

        public ServerWrapper Server => _serverTfs;



        public DataTable GetComputers(string domainName)
        {
            var listTable = ListTable.CreateListTable();
            var workstationName = _serverTfs.WorkstationName;
            var row1 = listTable.NewRow();
            row1[ListTable.ColumnID] = row1[ListTable.ColumnValue] = workstationName;
            listTable.Rows.Add(row1);
            if (string.IsNullOrEmpty(domainName))
                return listTable;
            foreach (SearchResult searchResult in new DirectorySearcher($"LDAP://dc={(object)domainName},dc=com")
                     {
                         SearchScope = SearchScope.Subtree,
                         Filter = "(objectClass=computer)"
                     }.FindAll())
            {
                var property = searchResult.Properties["name"];
                if (property.Count == 1 && String.Compare(property[0].ToString(), workstationName, StringComparison.OrdinalIgnoreCase) != 0)
                {
                    var row2 = listTable.NewRow();
                    row2[ListTable.ColumnID] = row2[ListTable.ColumnValue] = property[0].ToString();
                    listTable.Rows.Add(row2);
                }
            }

            return listTable;
        }

        public DataTable GetProjects(bool useServerPathAsID)
        {
            var allTeamProjects = _serverVc.GetAllTeamProjects(false);
            var listTable = ListTable.CreateListTable();
            foreach (var teamProject in allTeamProjects)
            {
                var row = listTable.NewRow();
                row[ListTable.ColumnID] = !useServerPathAsID ? teamProject.Name : (object)teamProject.ServerItem;
                row[ListTable.ColumnValue] = teamProject.Name;
                listTable.Rows.Add(row);
            }
            return listTable;
        }

    }



}
