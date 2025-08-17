// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controllers.ServerPermission
// Assembly: Attrice.TeamFoundation.Controllers.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=856a3140e4bb5441
// MVID: 1A935642-B37C-4D8D-B5A3-89C4554B1701
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controllers.12.dll

using System.ComponentModel;

namespace Attrice.TeamFoundation.Controllers
{
  public enum ServerPermission
  {
    [Description("Administer shelved changes (AdminShelvesets)")] AdminShelvesets = 1,
    [Description("Administer warehouse (ADMINISTER_WAREHOUSE)")] ADMINISTER_WAREHOUSE = 2,
    [Description("Administer workspaces (AdminWorkspaces)")] AdminWorkspaces = 3,
    [Description("Create a workspace (CreateWorkspace)")] CreateWorkspace = 4,
    [Description("Create new projects (CREATE_PROJECTS)")] CREATE_PROJECTS = 5,
    [Description("Edit server-level information (GENERIC_WRITE)")] GENERIC_WRITE = 6,
    [Description("Edit server-level information (AdminConfiguration)")] AdminConfiguration = 7,
    [Description("Edit server-level information (AdminConnections)")] AdminConnections = 8,
    [Description("Alter trace settings (DIAGNOSTIC_TRACE)")] DIAGNOSTIC_TRACE = 9,
    [Description("Trigger Events (TRIGGER_EVENT)")] TRIGGER_EVENT = 10, // 0x0000000A
    [Description("Manage process template (MANAGE_TEMPLATE)")] MANAGE_TEMPLATE = 11, // 0x0000000B
    [Description("View server-level information (GENERIC_READ)")] GENERIC_READ = 12, // 0x0000000C
    [Description("View system synchronization information (SYNCHRONIZE_READ)")] SYNCHRONIZE_READ = 13, // 0x0000000D
  }
}
