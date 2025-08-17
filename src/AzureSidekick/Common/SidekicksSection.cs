// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Common.SidekicksSection
// Assembly: Attrice.TeamFoundation.Common.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=35acb2d39e045fbd
// MVID: A152C2C1-BE36-402B-9F4C-D577D02612BB
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Common.12.dll

using System.Configuration;

namespace Attrice.TeamFoundation.Common
{
  public class SidekicksSection : ConfigurationSection
  {
    [ConfigurationProperty("sidekicks")]
    public SidekicksCollection Sidekicks
    {
      get => (SidekicksCollection) this["sidekicks"];
      set => this["sidekicks"] = value;
    }
  }
}
