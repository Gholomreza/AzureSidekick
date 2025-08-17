// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controllers.SourceControlPermission
// Assembly: Attrice.TeamFoundation.Controllers.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=856a3140e4bb5441
// MVID: 1A935642-B37C-4D8D-B5A3-89C4554B1701
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controllers.12.dll

using System.ComponentModel;

namespace Attrice.TeamFoundation.Controllers
{
  public enum SourceControlPermission
  {
    [Description("Read (Read)")] Read = 1,
    [Description("Check out (PendChange)")] PendChange = 2,
    [Description("Checkin (Checkin)")] Checkin = 3,
    [Description("Label (Label)")] Label = 4,
    [Description("Lock (Lock)")] Lock = 5,
    [Description("Revise other user's changes (ReviseOther)")] ReviseOther = 6,
    [Description("Unlock other user's changes (UnlockOther)")] UnlockOther = 7,
    [Description("Undo other user's changes (UndoOther)")] UndoOther = 8,
    [Description("Administer labels (LabelOther)")] LabelOther = 9,
    [Description("Manipulate security settings (AdminProjectRights)")] AdminProjectRights = 10, // 0x0000000A
    [Description("Check in other user's changes (CheckinOther)")] CheckinOther = 11, // 0x0000000B
  }
}
