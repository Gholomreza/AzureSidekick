// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Sidekicks.Properties.Settings
// Assembly: Attrice.TeamFoundation.Sidekicks.12, Version=6.0.0.0, Culture=neutral, PublicKeyToken=d1ea2fdd7e98265b
// MVID: 9FC50433-A771-4AD0-B465-1FCE84AFBC29
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Sidekicks.12.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Attrice.TeamFoundation.Sidekicks.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) Synchronized(new Settings());

    private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
    {
    }

    private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
    {
    }

    public static Settings Default => defaultInstance;

    [UserScopedSetting]
    [DebuggerNonUserCode]
    public bool ColumnChangeDate
    {
      get => (bool) this[nameof (ColumnChangeDate)];
      set => this[nameof (ColumnChangeDate)] = value;
    }
  }
}
