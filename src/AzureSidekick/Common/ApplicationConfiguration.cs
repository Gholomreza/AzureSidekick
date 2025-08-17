// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Common.ApplicationConfiguration
// Assembly: Attrice.TeamFoundation.Common.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=35acb2d39e045fbd
// MVID: A152C2C1-BE36-402B-9F4C-D577D02612BB
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Common.12.dll

using System.Configuration;
using System.Diagnostics;

namespace Attrice.TeamFoundation.Common
{
  public class ApplicationConfiguration
  {
    private const string cControlsAssembly = "Attrice.TeamFoundation.Controls.12.dll";
    private readonly System.Configuration.Configuration _configuration;

    public ApplicationConfiguration(System.Configuration.Configuration configuration)
    {
      _configuration = configuration;
      if (_configuration.AppSettings.Settings["General.TfsServerName"] == null)
        _configuration.AppSettings.Settings.Add("General.TfsServerName", string.Empty);
      if (_configuration.AppSettings.Settings["General.TfsUserName"] == null)
        _configuration.AppSettings.Settings.Add("General.TfsUserName", string.Empty);
      if (_configuration.AppSettings.Settings["General.TfsCustomCredentials"] == null)
        _configuration.AppSettings.Settings.Add("General.TfsCustomCredentials", "false");
      if (_configuration.AppSettings.Settings["General.DomainName"] == null)
        _configuration.AppSettings.Settings.Add("General.DomainName", string.Empty);
      if (_configuration.AppSettings.Settings["General.PerformDomainLookup"] == null)
        _configuration.AppSettings.Settings.Add("General.PerformDomainLookup", "false");
      if (_configuration.AppSettings.Settings["General.ViewStatusBar"] == null)
        _configuration.AppSettings.Settings.Add("General.ViewStatusBar", "true");
      if (_configuration.AppSettings.Settings["General.ViewMainToolbar"] == null)
        _configuration.AppSettings.Settings.Add("General.ViewMainToolbar", "true");
      if (_configuration.AppSettings.Settings["General.RetrieveUsers"] == null)
        _configuration.AppSettings.Settings.Add("General.RetrieveUsers", $"{(object)0}");
      if (!(_configuration.GetSection("sidekicksSection") is SidekicksSection))
      {
        var section = new SidekicksSection();
        _configuration.Sections.Add("sidekicksSection", section);
        section.Sidekicks.Add(new SidekickConfiguration("Workspace Sidekick", "Attrice.TeamFoundation.Controls.WorkspaceViewControl", "Attrice.TeamFoundation.Controls.12.dll"));
        section.Sidekicks.Add(new SidekickConfiguration("Status Sidekick", "Attrice.TeamFoundation.Controls.StatusViewControl", "Attrice.TeamFoundation.Controls.12.dll"));
        section.Sidekicks.Add(new SidekickConfiguration("History Sidekick", "Attrice.TeamFoundation.Controls.HistoryViewControl", "Attrice.TeamFoundation.Controls.12.dll"));
        section.Sidekicks.Add(new SidekickConfiguration("Label Sidekick", "Attrice.TeamFoundation.Controls.LabelViewControl", "Attrice.TeamFoundation.Controls.12.dll"));
        section.Sidekicks.Add(new SidekickConfiguration("Shelveset Sidekick", "Attrice.TeamFoundation.Controls.ShelvesetViewControl", "Attrice.TeamFoundation.Controls.12.dll"));
        section.Sidekicks.Add(new SidekickConfiguration("Users View Sidekick", "Attrice.TeamFoundation.Controls.UsersViewControl", "Attrice.TeamFoundation.Controls.12.dll"));
        section.Sidekicks.Add(new SidekickConfiguration("Code Review Sidekick", "Attrice.TeamFoundation.Controls.CodeReviewControl", "Attrice.TeamFoundation.Controls.12.dll", false));
      }
      _configuration.Save(ConfigurationSaveMode.Minimal);
    }

    public void UpdateSidekickSettings(
      string sidekickName,
      KeyValueConfigurationCollection settings)
    {
      if (Sidekicks == null || Sidekicks[sidekickName] == null)
      {
        Trace.TraceError("Could not update section for {0}", sidekickName);
      }
      else
      {
        _configuration.GetSection("sidekicksSection");
        Sidekicks[sidekickName].Settings = settings;
      }
    }

    public void Save() => _configuration.Save(ConfigurationSaveMode.Modified);

    public SidekicksCollection Sidekicks => !(_configuration.GetSection("sidekicksSection") is SidekicksSection section) ? null : section.Sidekicks;

    public string TfsServer
    {
      get => _configuration.AppSettings.Settings["General.TfsServerName"].Value;
      set => _configuration.AppSettings.Settings["General.TfsServerName"].Value = value;
    }

    public string TfsUserName
    {
      get => _configuration.AppSettings.Settings["General.TfsUserName"].Value;
      set => _configuration.AppSettings.Settings["General.TfsUserName"].Value = value;
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
      get => bool.Parse(_configuration.AppSettings.Settings["General.TfsCustomCredentials"].Value);
      set => _configuration.AppSettings.Settings["General.TfsCustomCredentials"].Value = value.ToString();
    }

    public bool ViewStatusBar
    {
      get => bool.Parse(_configuration.AppSettings.Settings["General.ViewStatusBar"].Value);
      set => _configuration.AppSettings.Settings["General.ViewStatusBar"].Value = value.ToString();
    }

    public bool ViewMainToolbar
    {
      get => bool.Parse(_configuration.AppSettings.Settings["General.ViewMainToolbar"].Value);
      set => _configuration.AppSettings.Settings["General.ViewMainToolbar"].Value = value.ToString();
    }

    public string DomainName
    {
      get => _configuration.AppSettings.Settings["General.DomainName"].Value;
      set => _configuration.AppSettings.Settings["General.DomainName"].Value = value;
    }

    public bool PerformDomainLookup
    {
      get => bool.Parse(_configuration.AppSettings.Settings["General.PerformDomainLookup"].Value);
      set => _configuration.AppSettings.Settings["General.PerformDomainLookup"].Value = value.ToString();
    }

    public bool Valid => TfsServer != string.Empty;
  }
}
