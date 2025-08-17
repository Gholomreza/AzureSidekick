// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Common.Utilities
// Assembly: Attrice.TeamFoundation.Common.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=35acb2d39e045fbd
// MVID: A152C2C1-BE36-402B-9F4C-D577D02612BB
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Common.12.dll

using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Attrice.TeamFoundation.Common
{
  public class Utilities
  {
    private static readonly string cDateTimeFormat = "dd-MMM-yyyy HH:mm:ss";

    public static void SaveListViewToFile(
      SaveFileDialog saveFileDialog,
      ListView listView,
      string fileTitle)
    {
      if (saveFileDialog.ShowDialog() != DialogResult.OK)
        return;
      Cursor.Current = Cursors.WaitCursor;
      StreamWriter streamWriter = null;
      try
      {
        streamWriter = File.CreateText(saveFileDialog.FileName);
        var stringBuilder = new StringBuilder(fileTitle + "\r\n");
        if (listView.Groups.Count > 0)
          stringBuilder.AppendFormat("{0},", "Grouped by");
        foreach (ColumnHeader column in listView.Columns)
        {
          if (column.Width != 0)
          {
            if (column.Index != listView.Columns.Count - 1)
              stringBuilder.AppendFormat("{0},", column.Text);
            else
              stringBuilder.AppendFormat("{0}\r\n", column.Text);
          }
        }
        for (var index1 = 0; index1 < listView.Items.Count; ++index1)
        {
          var listViewItem = listView.Items[index1];
          if (listViewItem.Group != null)
            stringBuilder.AppendFormat("{0},", listViewItem.Group.Name.Replace(',', ';'));
          for (var index2 = 0; index2 < listViewItem.SubItems.Count; ++index2)
          {
            if (listView.Columns[index2].Width != 0)
            {
              var text = listViewItem.SubItems[index2].Text;
              if (index2 != listViewItem.SubItems.Count - 1)
                stringBuilder.AppendFormat("{0},", text.Replace(',', ';'));
              else
                stringBuilder.AppendFormat("{0}\r\n", text.Replace(',', ';'));
            }
          }
        }
        streamWriter.Write(stringBuilder.ToString());
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show($"Failed to save list ({(object)ex.Message}).", "Data retreival error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        streamWriter?.Close();
        Cursor.Current = Cursors.Default;
      }
    }

    public static bool ParseDateTimeInvariant(string value, out DateTime date)
    {
      return DateTime.TryParseExact(value, cDateTimeFormat, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AssumeLocal, out date);
    }

    public static string FormatDateTimeInvariant(DateTime date)
    {
      return date.ToString(cDateTimeFormat, DateTimeFormatInfo.InvariantInfo);
    }

    public static string GetRelativeServerPath(string parentServerItem, string serverItem)
    {
      return serverItem.Substring(parentServerItem.Length);
    }

    public static string GetTableValueByID(DataTable table, string id)
    {
      if (table == null)
        return id;
      var dataRow = table.Rows.Find(id);
      return dataRow == null ? id : dataRow[ListTable.ColumnValue].ToString();
    }

    public static string GetTableIDByValue(DataTable table, string value)
    {
      if (table == null)
        return value;
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
      {
        if (row[ListTable.ColumnValue].ToString() == value)
          return row[ListTable.ColumnID].ToString();
      }
      return value;
    }

    public static string ParseServerItem(string serverItem)
    {
      if (serverItem == "$/")
        return serverItem;
      var num = serverItem.LastIndexOf('/');
      return num == -1 ? string.Empty : serverItem.Substring(num + 1);
    }
  }
}
