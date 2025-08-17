// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controllers.AccessPermission
// Assembly: Attrice.TeamFoundation.Controllers.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=856a3140e4bb5441
// MVID: 1A935642-B37C-4D8D-B5A3-89C4554B1701
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controllers.12.dll

using System;

namespace Attrice.TeamFoundation.Controllers
{
  [Flags]
  public enum AccessPermission
  {
    Undefined = 0,
    Inherited = 256, // 0x00000100
    Deny = 16, // 0x00000010
    Allow = 1,
    AllowInherited = Allow | Inherited, // 0x00000101
    DenyInherited = Deny | Inherited, // 0x00000110
  }
}
