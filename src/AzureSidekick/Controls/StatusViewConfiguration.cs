// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.StatusViewConfiguration
// Assembly: Attrice.TeamFoundation.Controls.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=1a24c887ab53aa6e
// MVID: 393BE899-3C29-4A70-86D7-B4B965E5A8F5
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controls.12.dll


namespace Attrice.TeamFoundation.Controls
{
  public class StatusViewConfiguration
  {
    public bool ColumnChangeDate;
    public bool ColumnChangeType;
    public bool ColumnLockType;
    public bool ColumnItemName;
    public bool ColumnItemType;
    public bool ColumnOwnerName;
    public bool ColumnVersion;
    public bool ColumnWorkspace;
    public bool ColumnComputerName;
    public bool ColumnLocalPath;
    public bool ColumnServerPath;
    public static readonly StatusViewConfiguration Instance = new StatusViewConfiguration();

    private StatusViewConfiguration()
    {
      ColumnChangeDate = true;
      ColumnChangeType = true;
      ColumnLockType = true;
      ColumnItemName = true;
      ColumnItemType = true;
      ColumnOwnerName = true;
      ColumnVersion = true;
      ColumnWorkspace = true;
      ColumnComputerName = true;
      ColumnLocalPath = true;
      ColumnServerPath = true;
    }
  }
}
