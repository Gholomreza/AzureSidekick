// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controllers.PermissionsViewController
// Assembly: Attrice.TeamFoundation.Controllers.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=856a3140e4bb5441
// MVID: 1A935642-B37C-4D8D-B5A3-89C4554B1701
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controllers.12.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Xml;
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.VisualStudio.Services.Common;

namespace Attrice.TeamFoundation.Controllers
{
  public class PermissionsViewController: TfsController
  {
    public const string TeamFoundationAdministratorsGroup = "[SERVER]\\Team Foundation Administrators";
    public const string ProjectAdministratorsGroup = "[{0}]\\Project Administrators";

    public PermissionsViewController(TfsController controller) : base(controller)
    {
    }

    public Item GetItem(string serverItem) => VersionControl.GetItem(serverItem);

    public Item[] GetSubItems(string serverItem)
    {
      return VersionControl.GetItems(serverItem, VersionSpec.Latest, (RecursionType) 1, 0, 0, false).Items;
    }

    public Item[] GetFirstLevelItems(string projectName)
    {
      return VersionControl.GetItems("$/" + projectName, VersionSpec.Latest, (RecursionType) 1, 0, 0, false).Items;
    }

    public Dictionary<string, string> GetProjectAreas(string projectName)
    {
      var projectFromName = CommonStructure.GetProjectFromName(projectName);
      var results = new Dictionary<string, string>();
      foreach (var listStructure in CommonStructure.ListStructures(projectFromName.Uri))
      {
        if (!(listStructure.StructureType != "ProjectModelHierarchy"))
        {
          var nodesXml = CommonStructure.GetNodesXml(new string[1]
          {
            listStructure.Uri
          }, true);
          AddChildNodes(listStructure.Name, nodesXml.ChildNodes[0], results);
        }
      }
      return results;
    }

    private void AddChildNodes(
      string parentPath,
      XmlNode parentNode,
      Dictionary<string, string> results)
    {
      results.Add(parentPath, parentNode.Attributes["NodeID"].Value);
      if (parentNode.ChildNodes[0] == null)
        return;
      foreach (XmlNode childNode in parentNode.ChildNodes[0].ChildNodes)
        AddChildNodes(parentPath + "/" + childNode.Attributes["Name"].Value, childNode, results);
    }

    public Dictionary<string, AccessEntry> GetAreaPermissions(
      string projectName,
      Dictionary<string, TeamFoundationIdentity> groups,
      string areaUri)
    {
      var emptyPermissions = CreateEmptyPermissions(groups);
      return GetAcePermissions(projectName, groups, areaUri, emptyPermissions);
    }

    private Dictionary<string, AccessEntry> GetAcePermissions(
      string projectName,
      Dictionary<string, TeamFoundationIdentity> groups,
      string uri,
      Dictionary<string, AccessEntry> permissions)
    {
      try
      {
        foreach (var readAccessControl in AuthorizationService.ReadAccessControlList(uri))
        {
          var identity = IdentityManagement.ReadIdentity(IdentityHelper.CreateDescriptorFromSid(readAccessControl.Sid), 0, (ReadIdentityOptions) 1);
          identity.DisplayName = ExpandIdentityDisplayName(identity);
          if (permissions.ContainsKey(identity.DisplayName))
          {
            var accessEntry = permissions[identity.DisplayName];
            if (accessEntry == null)
            {
              accessEntry = typeof (AccessEntry).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[0], null).Invoke(new object[0]) as AccessEntry;
              permissions[identity.DisplayName] = accessEntry;
            }
            accessEntry.IdentityName = identity.DisplayName;
            if (readAccessControl.Deny)
            {
              var strArray = new string[accessEntry.Deny.Length + 1];
              accessEntry.Deny.CopyTo(strArray, 1);
              strArray[0] = readAccessControl.ActionId;
              accessEntry.Deny = strArray;
            }
            else
            {
              var strArray = new string[accessEntry.Allow.Length + 1];
              accessEntry.Allow.CopyTo(strArray, 1);
              strArray[0] = readAccessControl.ActionId;
              accessEntry.Allow = strArray;
            }
          }
        }
      }
      catch (Exception ex)
      {
        Trace.TraceError("In VC GetAcePermissions {0} {1}", ex.Message, ex.StackTrace);
      }
      return permissions;
    }

    private static Dictionary<string, AccessEntry> CreateEmptyPermissions(
      Dictionary<string, TeamFoundationIdentity> groups)
    {
      var emptyPermissions = new Dictionary<string, AccessEntry>();
      if (groups == null)
        return emptyPermissions;
      var array = new string[groups.Keys.Count];
      groups.Keys.CopyTo(array, 0);
      foreach (var key in groups.Keys)
        emptyPermissions[key] = null;
      return emptyPermissions;
    }

    public Dictionary<string, AccessEntry> GetProjectPermissions(
      string projectName,
      Dictionary<string, TeamFoundationIdentity> groups)
    {
      var emptyPermissions = CreateEmptyPermissions(groups);
      var uri = PermissionNamespaces.Project + CommonStructure.GetProjectFromName(projectName).Uri;
      return GetAcePermissions(projectName, groups, uri, emptyPermissions);
    }

    public Dictionary<string, AccessEntry> GetGlobalPermissions(
      Dictionary<string, TeamFoundationIdentity> groups)
    {
      var emptyPermissions = CreateEmptyPermissions(groups);
      var array = new string[groups.Keys.Count];
      try
      {
        groups.Keys.CopyTo(array, 0);
        foreach (var entry in VersionControl.GetGlobalPermissions(array).Entries)
        {
          if (emptyPermissions.ContainsKey(entry.IdentityName))
            emptyPermissions[entry.IdentityName] = entry;
        }
      }
      catch (Exception ex)
      {
        Trace.TraceError("In VC GetGlobalPermissions {0} {1}", ex.Message, ex.StackTrace);
      }
      return GetAcePermissions(null, groups, PermissionNamespaces.Global, emptyPermissions);
    }

    public Dictionary<string, AccessEntry> GetItemPermissions(
      Dictionary<string, TeamFoundationIdentity> groups,
      Item item)
    {
      var permissions = VersionControl.GetPermissions(null, new string[1]
      {
        item.ServerItem
      }, 0);
      var array = new string[groups.Keys.Count];
      groups.Keys.CopyTo(array, 0);
      var itemPermissions = new Dictionary<string, AccessEntry>();
      foreach (var key in groups.Keys)
        itemPermissions[key] = null;
      foreach (var itemSecurity in permissions)
      {
        foreach (var entry in itemSecurity.Entries)
        {
          if (itemPermissions.ContainsKey(entry.IdentityName))
            itemPermissions[entry.IdentityName] = entry;
        }
      }
      return itemPermissions;
    }

    public Dictionary<string, TeamFoundationIdentity> GetUserGroups(string accountName)
    {
      var userGroups = new Dictionary<string, TeamFoundationIdentity>();
      var identity = IdentityManagement.ReadIdentity(0, accountName, (MembershipQuery) 2, 0);
      if (identity == null)
        throw new Exception("User not found");
      foreach (var readIdentity in IdentityManagement.ReadIdentities(identity.MemberOf, 0, 0))
      {
        if (readIdentity.Descriptor.IdentityType == IdentityConstants.TeamFoundationType)
        {
          readIdentity.DisplayName = ExpandIdentityDisplayName(readIdentity);
          userGroups.Add(readIdentity.DisplayName, readIdentity);
        }
      }
      identity.DisplayName = ExpandIdentityDisplayName(identity);
      userGroups.Add(identity.DisplayName, identity);
      return userGroups;
    }

    private string ExpandIdentityDisplayName(TeamFoundationIdentity identity)
    {
      return identity.Descriptor.IdentityType == IdentityConstants.WindowsType ? identity.GetAttribute("Domain", "") + "\\" + identity.GetAttribute("Account", "") : identity.DisplayName;
    }
  }
}
