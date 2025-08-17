// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.ShelvesetProperties
// Assembly: Attrice.TeamFoundation.Controls.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=1a24c887ab53aa6e
// MVID: 393BE899-3C29-4A70-86D7-B4B965E5A8F5
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controls.12.dll

using System;
using System.ComponentModel;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace Attrice.TeamFoundation.Controls
{
  internal class ShelvesetProperties
  {
    private Shelveset _shelveset;
    private string _ownerName;
    private string _ownerRawName;
    private string _name;
    private string _comment;
    private string _policyComment;

    [DisplayName("Shelveset name")]
    [Category("General")]
    [Description("Shelveset name")]
    public string Name => _shelveset != null ? _shelveset.Name : _name;

    [Category("Details")]
    [Description("Shelveset comment")]
    public string Comment => _shelveset != null ? _shelveset.Comment : _comment;

    [DisplayName("Policy Comment")]
    [Category("Details")]
    [Description("Policy override comment")]
    public string PolicyComment => _shelveset != null ? _shelveset.PolicyOverrideComment : _policyComment;

    [DisplayName("Creation date")]
    [Category("Details")]
    [Description("Date of shelveset creation")]
    public DateTime CreationDate => _shelveset != null ? _shelveset.CreationDate : DateTime.Now;

    [DisplayName("Owner account name")]
    [Category("Details")]
    [Description("Shelveset owner account name")]
    public string OwnerNameRaw => _shelveset != null ? _shelveset.OwnerName : _ownerRawName;

    [DisplayName("Owner name")]
    [Category("General")]
    [Description("Shelveset owner name")]
    public string OwnerName => _ownerName;

    public ShelvesetProperties(Shelveset set, string ownerName)
    {
      _ownerName = ownerName;
      _shelveset = set;
    }
  }
}
