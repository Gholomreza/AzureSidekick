// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.BaseSidekickControl
// Assembly: Attrice.TeamFoundation.Controls.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=1a24c887ab53aa6e
// MVID: 393BE899-3C29-4A70-86D7-B4B965E5A8F5
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controls.12.dll

using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;
using Attrice.TeamFoundation.Controllers;

namespace Attrice.TeamFoundation.Controls
{
  public class BaseSidekickControl : UserControl
  {
    protected SaveFileDialog saveFileDialog;
    protected StringDictionary _settings;

    public BaseSidekickControl() => InitializeComponent();

    public virtual Image Image => throw new NotImplementedException();

    public virtual void Initialize(TfsController controller) => throw new NotImplementedException();

    public virtual void LoadUsers(TfsController controller) => throw new NotImplementedException();

    private void InitializeComponent()
    {
      saveFileDialog = new SaveFileDialog();
      SuspendLayout();
      saveFileDialog.DefaultExt = "csv";
      saveFileDialog.Filter = "CSV  files (*.csv)|*.csv|All files (*.*)|*.*";
      saveFileDialog.Title = "Save to file";
      Name = "BaseUserControl";
      ResumeLayout(false);
    }

    public virtual StringDictionary Settings
    {
      get => _settings;
      set => _settings = value;
    }
  }
}
