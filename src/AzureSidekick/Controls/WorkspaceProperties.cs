// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.WorkspaceProperties
// Assembly: Attrice.TeamFoundation.Controls.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=1a24c887ab53aa6e
// MVID: 393BE899-3C29-4A70-86D7-B4B965E5A8F5
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controls.12.dll

using System;
using System.ComponentModel;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace Attrice.TeamFoundation.Controls
{
  internal class WorkspaceProperties
  {
    private Workspace _workspace;
    private string _ownerName;
    private string _ownerRawName;
    private string _name;
    private string _comment;
    private string _computer;
    private string _permissionsProfile;

    [DisplayName("Workspace name")]
    [Category("General")]
    [Description("Workspace name")]
    public string Name => _workspace != null ? _workspace.Name : _name;

    [Category("Details")]
    [Description("Workspace comment")]
    public string Comment => _workspace != null ? _workspace.Comment : _comment;

    [DisplayName("Last access date")]
    [Category("Details")]
    [Description("Date of last workspace access")]
    public DateTime LastAccessDate => _workspace != null ? _workspace.LastAccessDate : DateTime.Now;

    [Category("General")]
    [Description("Workspace computer name")]
    public string Computer => _workspace != null ? _workspace.Computer : _computer;

    [DisplayName("Owner account name")]
    [Category("Details")]
    [Description("Workspace owner account name")]
    public string OwnerNameRaw => _workspace != null ? _workspace.OwnerName : _ownerRawName;

    [DisplayName("Is local")]
    [Category("Details")]
    [Description("Specifies whether workspace is located at current computer")]
    public string IsLocal
    {
      get
      {
        if (_workspace == null)
          return "";
        return !_workspace.IsLocal ? "No" : "Yes";
      }
    }

    [DisplayName("Owner name")]
    [Category("General")]
    [Description("Workspace owner name")]
    public string OwnerName => _ownerName;

    [DisplayName("Workspace Permissions")]
    [Category("General")]
    [Description("Workspace permissions profile: Private, Public or Public with limited access")]
    public string PermissionsProfile => _workspace != null ? _workspace.PermissionsProfile.Name : _permissionsProfile;

    public WorkspaceProperties(Workspace workspace, string ownerName)
    {
      _ownerName = ownerName;
      _workspace = workspace;
    }

    public WorkspaceProperties(
      string name,
      string comment,
      string ownerName,
      string ownerRawName,
      string computer)
    {
      _ownerName = ownerName;
      _ownerRawName = ownerRawName;
      _name = name;
      _comment = comment;
      _computer = computer;
      _workspace = null;
      _permissionsProfile = "Private";
    }
  }
}
