// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Configuration.ApplicationConfiguration
// Assembly: Attrice.TeamFoundation.Configuration, Version=6.0.0.0, Culture=neutral, PublicKeyToken=ed0806f44a1db3da
// MVID: 55E92D01-E3B2-44E8-8B88-32DB102B31F8
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Configuration.dll

using System.Configuration;
using System.Diagnostics;

namespace Attrice.TeamFoundation.Configuration
{
  public class ApplicationConfiguration
  {
    private const string ControlsAssemblyPrefix = "Attrice.TeamFoundation.Controls";
    private const string PermissionsSidekickAssemblyPrefix = "Attrice.TeamFoundation.PermissionsSidekick";
    private System.Configuration.Configuration _configuration;

    public ApplicationConfiguration(VisualStudioVersion visualStudioVersion)
    {
      _configuration = ConfigurationManager.GetConfiguration(visualStudioVersion);
      var settings = _configuration.AppSettings.Settings;
      var count1 = settings.Count;
      if (settings["General.TfsServerName"] == null)
        settings.Add("General.TfsServerName", string.Empty);
      if (settings["General.TfsUserName"] == null)
        settings.Add("General.TfsUserName", string.Empty);
      if (settings["General.TfsCustomCredentials"] == null)
        settings.Add("General.TfsCustomCredentials", "false");
      if (settings["General.ViewStatusBar"] == null)
        settings.Add("General.ViewStatusBar", "true");
      if (settings["General.ViewMainToolbar"] == null)
        settings.Add("General.ViewMainToolbar", "true");
      if (settings["General.RetrieveUsers"] == null)
        settings.Add("General.RetrieveUsers", $"{(object)0}");
      var count2 = settings.Count;
      var flag = count1 != count2;
      if (Sidekicks == null)
      {
        flag = true;
        _configuration.Sections.Add("sidekicksSection", new SidekicksSection());
        var sidekicks = Sidekicks;
        var assembly = "Attrice.TeamFoundation.Controls" + (visualStudioVersion == VisualStudioVersion.Twelve ? ".12.dll" : ".dll");
        sidekicks.Add(new SidekickConfiguration("Workspace Sidekick", "Attrice.TeamFoundation.Controls.WorkspaceViewControl", assembly));
        sidekicks.Add(new SidekickConfiguration("Status Sidekick", "Attrice.TeamFoundation.Controls.StatusViewControl", assembly));
        sidekicks.Add(new SidekickConfiguration("History Sidekick", "Attrice.TeamFoundation.Controls.HistoryViewControl", assembly));
        sidekicks.Add(new SidekickConfiguration("Label Sidekick", "Attrice.TeamFoundation.Controls.LabelViewControl", assembly));
        sidekicks.Add(new SidekickConfiguration("Shelveset Sidekick", "Attrice.TeamFoundation.Controls.ShelvesetViewControl", assembly));
        sidekicks.Add(new SidekickConfiguration("Permissions Sidekick", "Attrice.TeamFoundation.Controls.PermissionsViewControl", assembly));
        sidekicks.Add(new SidekickConfiguration("Users View Sidekick", "Attrice.TeamFoundation.Controls.UsersViewControl", assembly));
        sidekicks.Add(new SidekickConfiguration("Code Review Sidekick", "Attrice.TeamFoundation.Controls.CodeReviewControl", assembly, false));
      }
      if (!flag)
        return;
      Trace.TraceInformation("Configuration was changed to set defaults");
      Save();
    }

    public void UpdateSidekickSettings(
      string sidekickName,
      KeyValueConfigurationCollection settings)
    {
      if (Sidekicks == null || Sidekicks[sidekickName] == null)
        Trace.TraceError("Could not update section for {0}", sidekickName);
      else
        Sidekicks[sidekickName].Settings = settings;
    }

    public void Save() => _configuration.Save(ConfigurationSaveMode.Minimal);

    public SidekicksCollection Sidekicks => !(_configuration.GetSection("sidekicksSection") is SidekicksSection section) ? null : section.Sidekicks;

    private void SetString(string name, string value)
    {
      _configuration.AppSettings.Settings[name].Value = value;
    }

    private string GetString(string name) => _configuration.AppSettings.Settings[name].Value;

    private void SetBoolean(string name, bool value)
    {
      _configuration.AppSettings.Settings[name].Value = value.ToString();
    }

    private bool GetBoolean(string name)
    {
      return bool.Parse(_configuration.AppSettings.Settings[name].Value);
    }

    public string TfsServer
    {
      get => GetString("General.TfsServerName");
      set => SetString("General.TfsServerName", value);
    }

    public string TfsUserName
    {
      get => GetString("General.TfsUserName");
      set => SetString("General.TfsUserName", value);
    }

    public RetrieveUsersOption RetrieveUsers
    {
      get
      {
        int result;
        return !int.TryParse(_configuration.AppSettings.Settings["General.RetrieveUsers"].Value, out result) ? RetrieveUsersOption.Background : (RetrieveUsersOption) result;
      }
      set => _configuration.AppSettings.Settings["General.RetrieveUsers"].Value = $"{(object)(int)value}";
    }

    public bool CustomCredentials
    {
      get => GetBoolean("General.TfsCustomCredentials");
      set => SetBoolean("General.TfsCustomCredentials", value);
    }

    public bool ViewStatusBar
    {
      get => GetBoolean("General.ViewStatusBar");
      set => SetBoolean("General.ViewStatusBar", value);
    }

    public bool ViewMainToolbar
    {
      get => GetBoolean("General.ViewMainToolbar");
      set => SetBoolean("General.ViewMainToolbar", value);
    }
  }
}
