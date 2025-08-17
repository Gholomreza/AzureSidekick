// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Sidekicks.ApplicationMain
// Assembly: Attrice.TeamFoundation.Sidekicks.12, Version=6.0.0.0, Culture=neutral, PublicKeyToken=d1ea2fdd7e98265b
// MVID: 9FC50433-A771-4AD0-B465-1FCE84AFBC29
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Sidekicks.12.exe

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Attrice.TeamFoundation.Configuration;
using ApplicationConfiguration = Attrice.TeamFoundation.Configuration.ApplicationConfiguration;

namespace Attrice.TeamFoundation.Sidekicks
{
  internal static class ApplicationMain
  {
    private static ApplicationConfiguration _configuration;
    private static string _teamFoundationPath;
    private static string _visualStudioPath;

    public static ApplicationConfiguration Configuration => _configuration;

    public static Version GetVersion() => Assembly.GetExecutingAssembly().GetName().Version;

    private static string GetTeamFoundationPath()
    {
      var path1 = GetVisualStudioPath();
      if (!string.IsNullOrEmpty(path1))
        path1 = Path.Combine(path1, "CommonExtensions\\Microsoft\\TeamFoundation\\Team Explorer");
      return path1;
    }
    
    public static string GetVisualStudioPath()
        {
            try
            {
                // Get environment variables for Program Files
                var programFiles = Environment.GetEnvironmentVariable("ProgramFiles")?.Replace(" (x86)", "");
                var programFilesX86 = Environment.GetEnvironmentVariable("ProgramFiles(x86)");

                // Common installation paths for Visual Studio using %ProgramFiles% and %ProgramFiles(x86)%
                var possiblePaths = new[]
                {
                    Path.Combine(programFiles, @"Microsoft Visual Studio\2022\Community"),
                    Path.Combine(programFiles, @"Microsoft Visual Studio\2022\Professional"),
                    Path.Combine(programFiles, @"Microsoft Visual Studio\2022\Enterprise"),
                    Path.Combine(programFilesX86, @"Microsoft Visual Studio\2019\Community"),
                    Path.Combine(programFilesX86, @"Microsoft Visual Studio\2019\Professional"),
                    Path.Combine(programFilesX86, @"Microsoft Visual Studio\2019\Enterprise"),
                    Path.Combine(programFilesX86, @"Microsoft Visual Studio\2017\Community"),
                    Path.Combine(programFilesX86, @"Microsoft Visual Studio\2017\Professional"),
                    Path.Combine(programFilesX86, @"Microsoft Visual Studio\2017\Enterprise")
                };

                foreach (var path in possiblePaths)
                {
                    if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
                    {
                        return path;
                    }
                }

                return null; // Return null if no valid path is found
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking paths: {ex.Message}");
                return null;
            }
        }
    
    
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      var formSplash = new FormSplash(false);
      formSplash.Show();
      formSplash.Update();
      _teamFoundationPath = GetTeamFoundationPath();
      _visualStudioPath = GetVisualStudioPath();
      Trace.TraceInformation("TFS path {0}", _teamFoundationPath);
      if (string.IsNullOrEmpty(_teamFoundationPath))
      {
        formSplash.Hide();
        var num = (int) MessageBox.Show("Application cannot read registry. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        AppDomain.CurrentDomain.AssemblyResolve += DomainAssemblyResolve;
        try
        {
          _configuration = new ApplicationConfiguration(VisualStudioVersion.Twelve);
          var mainForm = new FormMain();
          mainForm.Initialize();
          formSplash.Hide();
          Application.Run(mainForm);
          _configuration.Save();
        }
        catch (Exception ex)
        {
          Trace.TraceError("Configuration.Load failure {0}\r\n{1}", ex.Message, ex.StackTrace);
          formSplash.Hide();
          var num = (int) MessageBox.Show($"Application cannot be started. {(object)ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
    }

    private static Assembly DomainAssemblyResolve(object sender, ResolveEventArgs args)
    {
      Trace.WriteLine("Assembly resolve:" + args.Name);
      var str = args.Name.Substring(0, args.Name.IndexOf(','));
      var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, str + ".dll");
      Trace.WriteLine("Trying to resolve:" + path);
      if (File.Exists(path))
        return null;
      Trace.WriteLine("Trying to resolve:" + Path.Combine(_teamFoundationPath, str + ".dll"));
      string assemblyFile;
      if (!File.Exists(Path.Combine(_teamFoundationPath, str + ".dll")))
      {
        if (!File.Exists(Path.Combine(_visualStudioPath, str + ".dll")))
          return null;
        assemblyFile = Path.Combine(_visualStudioPath, str + ".dll");
      }
      else
        assemblyFile = Path.Combine(_teamFoundationPath, str + ".dll");
      try
      {
        Trace.WriteLine("Loading:" + assemblyFile);
        return Assembly.LoadFrom(assemblyFile);
      }
      catch (Exception ex)
      {
        Trace.TraceError("DomainAssemblyResolve {0}\r\n{1}", ex.Message, ex.StackTrace);
      }
      return null;
    }
  }
}
