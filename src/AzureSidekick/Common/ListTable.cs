// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Common.ListTable
// Assembly: Attrice.TeamFoundation.Common.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=35acb2d39e045fbd
// MVID: A152C2C1-BE36-402B-9F4C-D577D02612BB
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Common.12.dll

using System.Data;
using System.Windows.Forms;

namespace Attrice.TeamFoundation.Common
{
  public static class ListTable
  {
    public static readonly string ColumnID = "ID";
    public static readonly string ColumnValue = "Value";
    public static readonly string cAllID = "All";

    public static void LoadTable(ComboBox comboBox, DataTable table, object selectedValue)
    {
      comboBox.DataSource = new DataView(table)
      {
          Sort = ColumnValue
      };
      comboBox.DisplayMember = ColumnValue;
      comboBox.ValueMember = ColumnID;
      if (selectedValue == null)
        return;
      comboBox.SelectedValue = selectedValue;
    }

    public static void AddAllRow(DataTable table)
    {
      var row = table.NewRow();
      row[ColumnID] = cAllID;
      row[ColumnID] = "";
      table.Rows.Add(row);
    }

    public static DataTable CreateListTable()
    {
      var listTable = new DataTable
      {
        Columns = {
          ColumnID,
          ColumnValue
        }
      };
      listTable.Constraints.Add("pk", listTable.Columns[ColumnID], true);
      return listTable;
    }
  }
}
