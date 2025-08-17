// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.Properties.Resources
// Assembly: Attrice.TeamFoundation.Controls.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=1a24c887ab53aa6e
// MVID: 393BE899-3C29-4A70-86D7-B4B965E5A8F5
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controls.12.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Attrice.TeamFoundation.Controls.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (resourceMan == null)
          resourceMan = new ResourceManager("Attrice.TeamFoundation.Controls.Properties.Resources", typeof (Resources).Assembly);
        return resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => resourceCulture;
      set => resourceCulture = value;
    }

    internal static Bitmap CodeReviewImage => (Bitmap) ResourceManager.GetObject(nameof (CodeReviewImage), resourceCulture);

    internal static Bitmap HistoryImage => (Bitmap) ResourceManager.GetObject(nameof (HistoryImage), resourceCulture);

    internal static Bitmap LabelsImage => (Bitmap) ResourceManager.GetObject(nameof (LabelsImage), resourceCulture);

    internal static Bitmap PermissionsImage => (Bitmap) ResourceManager.GetObject(nameof (PermissionsImage), resourceCulture);

    internal static Bitmap ShelvesetsImage => (Bitmap) ResourceManager.GetObject(nameof (ShelvesetsImage), resourceCulture);

    internal static Bitmap StatusImage => (Bitmap) ResourceManager.GetObject(nameof (StatusImage), resourceCulture);

    internal static Bitmap UsersViewImage => (Bitmap) ResourceManager.GetObject(nameof (UsersViewImage), resourceCulture);

    internal static Bitmap WorkspaceImage => (Bitmap) ResourceManager.GetObject(nameof (WorkspaceImage), resourceCulture);
  }
}
