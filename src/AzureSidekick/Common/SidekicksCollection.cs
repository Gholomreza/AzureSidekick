// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Common.SidekicksCollection
// Assembly: Attrice.TeamFoundation.Common.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=35acb2d39e045fbd
// MVID: A152C2C1-BE36-402B-9F4C-D577D02612BB
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Common.12.dll

using System.Configuration;

namespace Attrice.TeamFoundation.Common
{
  public class SidekicksCollection : ConfigurationElementCollection
  {
    protected override ConfigurationElement CreateNewElement()
    {
      return new SidekickConfiguration();
    }

    protected override object GetElementKey(ConfigurationElement element)
    {
      return ((SidekickConfiguration) element).Name;
    }

    public void Add(SidekickConfiguration element) => BaseAdd(element);

    public void Remove(SidekickConfiguration element) => BaseRemove(element);

    public SidekickConfiguration this[string name] => BaseGet(name) as SidekickConfiguration;
  }
}
