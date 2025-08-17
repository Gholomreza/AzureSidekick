// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Configuration.SidekicksCollection
// Assembly: Attrice.TeamFoundation.Configuration, Version=6.0.0.0, Culture=neutral, PublicKeyToken=ed0806f44a1db3da
// MVID: 55E92D01-E3B2-44E8-8B88-32DB102B31F8
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Configuration.dll

using System.Configuration;

namespace Attrice.TeamFoundation.Configuration
{
  public class SidekicksCollection : ConfigurationElementCollection
  {
    protected override ConfigurationElement CreateNewElement()
    {
      return new SidekickConfiguration();
    }

    protected override object GetElementKey(ConfigurationElement element)
    {
      return (element as SidekickConfiguration).Name;
    }

    public void Add(SidekickConfiguration element) => BaseAdd(element);

    public void Remove(SidekickConfiguration element) => BaseRemove(element);

    public SidekickConfiguration this[string name] => BaseGet(name) as SidekickConfiguration;
  }
}
