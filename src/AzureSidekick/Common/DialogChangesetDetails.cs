// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Common.DialogChangesetDetails
// Assembly: Attrice.TeamFoundation.Common.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=35acb2d39e045fbd
// MVID: A152C2C1-BE36-402B-9F4C-D577D02612BB
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Common.12.dll

using System;
using System.Windows.Forms;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace Attrice.TeamFoundation.Common
{
  public class DialogChangesetDetails : IDisposable
  {
    private readonly Form _dialog;

    public DialogChangesetDetails(VersionControlServer server, Changeset changeset)
    {
        //TODO
      //   _dialog = Assembly.GetAssembly(typeof (WorkItemPolicy)).CreateInstance("Microsoft.TeamFoundation.VersionControl.Controls.DialogChangesetDetails", false, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new object[]
      // {
      //   server,
      //   changeset,
      //   false
      // }, CultureInfo.CurrentCulture, null) as Form;
      //   if (_dialog != null) _dialog.StartPosition = FormStartPosition.CenterScreen;
    }

    public bool ShowDialog(IWin32Window window)
    {
      return _dialog.ShowDialog(window) == DialogResult.OK;
    }

    public void Dispose()
    {
      if (_dialog == null)
        return;
      _dialog.Dispose();
    }
  }
}
