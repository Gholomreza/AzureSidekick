// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controllers.AreaPermission
// Assembly: Attrice.TeamFoundation.Controllers.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=856a3140e4bb5441
// MVID: 1A935642-B37C-4D8D-B5A3-89C4554B1701
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controllers.12.dll

using System.ComponentModel;

namespace Attrice.TeamFoundation.Controllers
{
  public enum AreaPermission
  {
    [Description("Create and order child nodes (CREATE_CHILDREN)")] CREATE_CHILDREN = 1,
    [Description("Delete this node (DELETE)")] DELETE = 2,
    [Description("Edit this node (GENERIC_WRITE)")] GENERIC_WRITE = 3,
    [Description("Edit work items in this node (WORK_ITEM_WRITE)")] WORK_ITEM_WRITE = 4,
    [Description("View this node (GENERIC_READ)")] GENERIC_READ = 5,
    [Description("View work items in this node (WORK_ITEM_READ)")] WORK_ITEM_READ = 6,
  }
}
