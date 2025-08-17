// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Configuration.SidekickConfiguration
// Assembly: Attrice.TeamFoundation.Configuration, Version=6.0.0.0, Culture=neutral, PublicKeyToken=ed0806f44a1db3da
// MVID: 55E92D01-E3B2-44E8-8B88-32DB102B31F8
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Configuration.dll

using System.Configuration;

namespace Attrice.TeamFoundation.Configuration
{
  public class SidekickConfiguration : ConfigurationElement
  {
    internal SidekickConfiguration()
    {
    }

    public SidekickConfiguration(string name, string type, string assembly, bool addToVSMenu)
    {
      Name = name;
      Type = type;
      Assembly = assembly;
      AddToVSMenu = addToVSMenu;
    }

    public SidekickConfiguration(string name, string type, string assembly)
      : this(name, type, assembly, true)
    {
    }

    public SidekickConfiguration(
      string name,
      string type,
      string assembly,
      KeyValueConfigurationCollection settings)
      : this(name, type, assembly)
    {
      Settings = settings;
    }

    [ConfigurationProperty("name")]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = value;
    }

    [ConfigurationProperty("type")]
    public string Type
    {
      get => (string) this["type"];
      set => this["type"] = value;
    }

    [ConfigurationProperty("assembly")]
    public string Assembly
    {
      get => (string) this["assembly"];
      set => this["assembly"] = value;
    }

    [ConfigurationProperty("addToVSMenu", DefaultValue = true, IsRequired = false)]
    public bool AddToVSMenu
    {
      get => (bool) this["addToVSMenu"];
      set => this["addToVSMenu"] = value;
    }

    [ConfigurationProperty("settings")]
    public KeyValueConfigurationCollection Settings
    {
      get => (KeyValueConfigurationCollection) this["settings"];
      set => this["settings"] = value;
    }
  }
}
