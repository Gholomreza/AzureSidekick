// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Common.VirtualListView
// Assembly: Attrice.TeamFoundation.Common.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=35acb2d39e045fbd
// MVID: A152C2C1-BE36-402B-9F4C-D577D02612BB
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Common.12.dll

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Attrice.TeamFoundation.Common
{
  public class VirtualListView : ListView
  {
    private readonly List<ListViewItem> _virtualItems;
    private int _itemsCount;
    private readonly CustomListSorter _sorter;
    private bool _sorted;

    public VirtualListView()
    {
      _virtualItems = new List<ListViewItem>();
      _virtualItems.Capacity = 50;
      _itemsCount = 0;
      RetrieveVirtualItem += DoRetrieveVirtualItem;
      ColumnClick += DoColumnClick;
      _sorter = new CustomListSorter();
      _sorted = false;
    }

    private void DoColumnClick(object sender, ColumnClickEventArgs e)
    {
      if (!_sorted)
        return;
      _sorter.SetColumn(Columns[e.Column]);
      _virtualItems.Sort(0, _itemsCount, _sorter);
      Refresh();
    }

    private void DoRetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
    {
      if (_itemsCount == 0)
        return;
      e.Item = _virtualItems[e.ItemIndex];
    }

    public ListViewItem SelectedItem => SelectedIndices.Count == 0 ? null : Items[SelectedIndices[0]];

    public void SetCapacity(int capacity)
    {
      if (_virtualItems.Capacity >= capacity)
        return;
      _virtualItems.Capacity = capacity;
    }

    public void ClearItems()
    {
      Items.Clear();
      if (_sorted)
        _sorter.SetColumn(Columns[0]);
      VirtualListSize = 0;
      _itemsCount = 0;
    }

    public void AddItem(ListViewItem item)
    {
      ++_itemsCount;
      if (_virtualItems.Count < _itemsCount)
        _virtualItems.Add(item);
      else
        _virtualItems[_itemsCount - 1] = item;
    }

    public int ItemsCount => _itemsCount;

    public void InsertItem(ListViewItem item, int index)
    {
      ++_itemsCount;
      _virtualItems.Insert(index, item);
    }

    public new void BeginUpdate()
    {
      base.BeginUpdate();
      ClearItems();
      VirtualMode = false;
    }

    public CustomListSorter Sorter => _sorter;

    public bool Sorted
    {
      get => _sorted;
      set => _sorted = value;
    }

    public new void EndUpdate()
    {
      if (_sorted)
      {
        _sorter.SetColumn(Columns[0]);
        _virtualItems.Sort(0, _itemsCount, _sorter);
      }
      VirtualListSize = _itemsCount;
      VirtualMode = true;
      base.EndUpdate();
    }

    public ListViewItem Find(string key)
    {
      var num = 0;
      foreach (var virtualItem in _virtualItems)
      {
        if (string.Compare(virtualItem.Text, key, StringComparison.OrdinalIgnoreCase) == 0)
        {
          virtualItem.Tag = num;
          return virtualItem;
        }
        ++num;
      }
      return null;
    }
  }
}
