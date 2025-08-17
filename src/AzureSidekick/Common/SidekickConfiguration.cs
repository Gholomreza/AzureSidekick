// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Common.SidekickConfiguration
// Assembly: Attrice.TeamFoundation.Common.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=35acb2d39e045fbd
// MVID: A152C2C1-BE36-402B-9F4C-D577D02612BB
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Common.12.dll

using System.Configuration;

namespace Attrice.TeamFoundation.Common
{
  public class SidekickConfiguration : ConfigurationElement
  {
    public SidekickConfiguration()
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
