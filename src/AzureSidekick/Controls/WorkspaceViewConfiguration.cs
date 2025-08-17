// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.WorkspaceViewConfiguration
// Assembly: Attrice.TeamFoundation.Controls.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=1a24c887ab53aa6e
// MVID: 393BE899-3C29-4A70-86D7-B4B965E5A8F5
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controls.12.dll


namespace Attrice.TeamFoundation.Controls
{
  public class WorkspaceViewConfiguration
  {
    public bool PerformDomainLookup;
    public string DomainName;
    public static readonly WorkspaceViewConfiguration Instance = new WorkspaceViewConfiguration();

    private WorkspaceViewConfiguration()
    {
      PerformDomainLookup = false;
      DomainName = string.Empty;
    }
  }
}
