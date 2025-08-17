// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controllers.PendingChangeProperties
// Assembly: Attrice.TeamFoundation.Controllers.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=856a3140e4bb5441
// MVID: 1A935642-B37C-4D8D-B5A3-89C4554B1701
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controllers.12.dll

using Microsoft.TeamFoundation.VersionControl.Client;

namespace Attrice.TeamFoundation.Controllers
{
  public class PendingChangeProperties
  {
    private string _computer;
    private string _name;
    private string _ownerName;
    private PendingChange _change;

    public string Computer => _computer;

    public string Name => _name;

    public string OwnerName => _ownerName;

    public PendingChange Change => _change;

    public PendingChangeProperties(
      string computer,
      string ownerName,
      string name,
      PendingChange change)
    {
      _computer = computer;
      _ownerName = ownerName;
      _name = name;
      _change = change;
    }
  }
}
