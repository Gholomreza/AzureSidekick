// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Configuration.ConfigurationManager
// Assembly: Attrice.TeamFoundation.Configuration, Version=6.0.0.0, Culture=neutral, PublicKeyToken=ed0806f44a1db3da
// MVID: 55E92D01-E3B2-44E8-8B88-32DB102B31F8
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Configuration.dll

using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace Attrice.TeamFoundation.Configuration
{
  internal class ConfigurationManager
  {
    private const string UserConfigurationPrefix = "user";

    private static string GetConfigurationFilePath(VisualStudioVersion visualStudioVersion)
    {
      var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
      var executingAssembly = Assembly.GetExecutingAssembly();
      var company = (executingAssembly.GetCustomAttributes(typeof (AssemblyCompanyAttribute), false)[0] as AssemblyCompanyAttribute).Company;
      var product = (executingAssembly.GetCustomAttributes(typeof (AssemblyProductAttribute), false)[0] as AssemblyProductAttribute).Product;
      var str1 = executingAssembly.GetName().Version.ToString(4);
      var str2 = visualStudioVersion == VisualStudioVersion.Nine ? ".9.config" : ".config";
      return $"{(object)folderPath}\\{(object)company}\\{(object)product}\\{(object)str1}\\{(object)("user" + str2)}";
    }

    private static string GetApplicationPath()
    {
      return Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
    }

    internal static System.Configuration.Configuration GetConfiguration(
      VisualStudioVersion visualStudioVersion)
    {
      var fileMap = new ExeConfigurationFileMap();
      fileMap.ExeConfigFilename = GetConfigurationFilePath(visualStudioVersion);
      Environment.CurrentDirectory = GetApplicationPath();
      return System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
    }
  }
}
