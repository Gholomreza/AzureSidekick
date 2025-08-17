// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Common.SearchParameters
// Assembly: Attrice.TeamFoundation.Common.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=35acb2d39e045fbd
// MVID: A152C2C1-BE36-402B-9F4C-D577D02612BB
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Common.12.dll

using System;

namespace Attrice.TeamFoundation.Common
{
  public class SearchParameters
  {
    private readonly string _userName;
    private readonly string _computerName;
    private string _sourcePath;
    private readonly DateTime _fromDate;
    private readonly DateTime _toDate;
    private readonly string _freeText;

    public string FreeText => _freeText;

    public string UserName => _userName;

    public string ComputerName => _computerName;

    public string SourcePath => _sourcePath;

    public void ChangeSourcePath(string value) => _sourcePath = value;

    public DateTime FromDate => _fromDate;

    public DateTime ToDate => _toDate;

    public SearchParameters(
      string freeText,
      string userName,
      string computerName,
      string sourcePath,
      DateTime fromDate,
      DateTime toDate)
    {
      _freeText = freeText;
      _userName = userName;
      _computerName = computerName;
      _sourcePath = sourcePath;
      _fromDate = fromDate;
      _toDate = toDate;
    }

    public SearchParameters(
      string userName,
      string computerName,
      string sourcePath,
      DateTime fromDate,
      DateTime toDate)
    {
      _freeText = null;
      _userName = userName;
      _computerName = computerName;
      _sourcePath = sourcePath;
      _fromDate = fromDate;
      _toDate = toDate;
    }
  }
}
