// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Configuration.SidekicksSection
// Assembly: Attrice.TeamFoundation.Configuration, Version=6.0.0.0, Culture=neutral, PublicKeyToken=ed0806f44a1db3da
// MVID: 55E92D01-E3B2-44E8-8B88-32DB102B31F8
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Configuration.dll

using System.Configuration;

namespace Attrice.TeamFoundation.Configuration
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
