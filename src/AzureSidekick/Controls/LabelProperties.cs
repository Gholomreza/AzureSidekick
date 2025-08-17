// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.LabelProperties
// Assembly: Attrice.TeamFoundation.Controls.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=1a24c887ab53aa6e
// MVID: 393BE899-3C29-4A70-86D7-B4B965E5A8F5
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controls.12.dll

using System;
using System.ComponentModel;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace Attrice.TeamFoundation.Controls
{
  internal class LabelProperties
  {
    private VersionControlLabel _label;
    private string _ownerName;
    private string _ownerRawName;
    private string _name;
    private string _comment;
    private string _scope;

    [DisplayName("Label name")]
    [Category("General")]
    [Description("Label name")]
    public string Name => _label != null ? _label.Name : _name;

    [Category("Details")]
    [Description("Label comment")]
    public string Comment => _label != null ? _label.Comment : _comment;

    [Category("Details")]
    [Description("Label scope")]
    public string Scope => _label != null ? _label.Scope : _scope;

    [DisplayName("Last modified date")]
    [Category("Details")]
    [Description("Date of last label modification")]
    public DateTime LastModifiedDate => _label != null ? _label.LastModifiedDate : DateTime.Now;

    [DisplayName("Owner account name")]
    [Category("Details")]
    [Description("Label owner (modifier) account name")]
    public string OwnerNameRaw => _label != null ? _label.OwnerName : _ownerRawName;

    [DisplayName("Owner name")]
    [Category("General")]
    [Description("Label owner (modifier) name")]
    public string OwnerName => _ownerName;

    public LabelProperties(VersionControlLabel label, string ownerName)
    {
      _ownerName = ownerName;
      _label = label;
    }
  }
}
