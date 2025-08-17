// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controllers.ProjectPermission
// Assembly: Attrice.TeamFoundation.Controllers.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=856a3140e4bb5441
// MVID: 1A935642-B37C-4D8D-B5A3-89C4554B1701
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controllers.12.dll

using System.ComponentModel;

namespace Attrice.TeamFoundation.Controllers
{
  public enum ProjectPermission
  {
    [Description("Administer a build")] ADMINISTER_BUILD,
    [Description("Delete this project (DELETE)")] DELETE,
    [Description("Edit project-level information (GENERIC_WRITE)")] GENERIC_WRITE,
    [Description("Publish test results (PUBLISH_TEST_RESULTS)")] PUBLISH_TEST_RESULTS,
    [Description("View project-level information (GENERIC_READ)")] GENERIC_READ,
    [Description("Start a build")] START_BUILD,
    [Description("Write to build operational store")] UPDATE_BUILD,
    [Description("Edit build quality")] EDIT_BUILD_STATUS,
  }
}
