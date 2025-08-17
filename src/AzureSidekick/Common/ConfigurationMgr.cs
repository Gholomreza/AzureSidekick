// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Common.ConfigurationMgr
// Assembly: Attrice.TeamFoundation.Common.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=35acb2d39e045fbd
// MVID: A152C2C1-BE36-402B-9F4C-D577D02612BB
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Common.12.dll

using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Attrice.TeamFoundation.Common
{
  public class ConfigurationMgr
  {
    private const string cUserConfig = "user.9.config";

    private static string GetConfigurationFilePath()
    {
      var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
      var executingAssembly = Assembly.GetExecutingAssembly();
      var company = (executingAssembly.GetCustomAttributes(typeof (AssemblyCompanyAttribute), false)[0] as AssemblyCompanyAttribute).Company;
      var product = (executingAssembly.GetCustomAttributes(typeof (AssemblyProductAttribute), false)[0] as AssemblyProductAttribute).Product;
      var str = executingAssembly.GetName().Version.ToString(4);
      return $"{(object)folderPath}\\{(object)company}\\{(object)product}\\{(object)str}\\{(object)"user.9.config"}";
    }

    private static string GetApplicationPath()
    {
      return Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
    }

    public static System.Configuration.Configuration GetConfiguration()
    {
      var fileMap = new ExeConfigurationFileMap();
      fileMap.ExeConfigFilename = GetConfigurationFilePath();
      Trace.WriteLine(GetApplicationPath());
      Environment.CurrentDirectory = GetApplicationPath();
      return ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
    }
  }
}
