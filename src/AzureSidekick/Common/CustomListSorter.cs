// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Common.CustomListSorter
// Assembly: Attrice.TeamFoundation.Common.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=35acb2d39e045fbd
// MVID: A152C2C1-BE36-402B-9F4C-D577D02612BB
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Common.12.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Attrice.TeamFoundation.Common
{
  public class CustomListSorter : IComparer, IComparer<ListViewItem>
  {
    private ColumnHeader _columnHeader;
    private readonly ListSorterType _type;
    private bool _ascending;

    public bool Ascending
    {
      get => _ascending;
      set => _ascending = value;
    }

    public CustomListSorter()
    {
      _ascending = false;
      _columnHeader = null;
      _type = ListSorterType.Date | ListSorterType.Integer;
    }

    public CustomListSorter(ListSorterType type)
    {
      _ascending = false;
      _columnHeader = null;
      _type = type;
    }

    public void SetColumn(ColumnHeader columnHeader)
    {
      _columnHeader = columnHeader;
      if (_columnHeader.Tag == null)
        _columnHeader.Tag = _ascending;
      else
        _columnHeader.Tag = !(bool) _columnHeader.Tag;
    }

    public int Compare(ListViewItem lv1, ListViewItem lv2)
    {
      var flag = !_ascending;
      if (_columnHeader != null && _columnHeader.Tag != null)
        flag = (bool) _columnHeader.Tag;
      var index = _columnHeader == null ? 0 : _columnHeader.Index;
      var text1 = lv1.SubItems[index].Text;
      var text2 = lv2.SubItems[index].Text;
      DateTime date1;
      DateTime date2;
      int result1;
      int result2;
      return (_type & ListSorterType.Date) > ListSorterType.Undefined && Utilities.ParseDateTimeInvariant(text1, out date1) && Utilities.ParseDateTimeInvariant(text2, out date2) ? (!flag ? DateTime.Compare(date1, date2) : DateTime.Compare(date2, date1)) : ((_type & ListSorterType.Integer) > ListSorterType.Undefined && int.TryParse(text1, out result1) && int.TryParse(text2, out result2) ? (!flag ? result1 - result2 : result2 - result1) : (!flag ? string.Compare(text1, text2) : string.Compare(text2, text1)));
    }

    public int Compare(object x, object y) => Compare(x as ListViewItem, y as ListViewItem);
  }
}
