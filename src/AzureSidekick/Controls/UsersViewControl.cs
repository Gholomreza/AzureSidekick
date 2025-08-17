// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.UsersViewControl
// Assembly: Attrice.TeamFoundation.Controls.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=1a24c887ab53aa6e
// MVID: 393BE899-3C29-4A70-86D7-B4B965E5A8F5
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controls.12.dll

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Attrice.TeamFoundation.Common;
using Attrice.TeamFoundation.Controllers;
using Attrice.TeamFoundation.Controls.Properties;

namespace Attrice.TeamFoundation.Controls
{
  public class UsersViewControl : BaseSidekickControl
  {
    private IContainer components;
    private TextBox textBoxFilter;
    private VirtualListView listViewUsers;
    private ColumnHeader columnHeader9;
    private ColumnHeader columnHeader10;
    private GroupBox groupBoxFilter;
    private ColumnHeader columnHeader1;
    private DataTable _dtUsers;

    public UsersViewControl()
    {
      InitializeComponent();
      listViewUsers.Sorted = true;
      groupBoxFilter.BackColor = SystemColors.Control;
      Name = "Users View Sidekick";
    }

    public override Image Image => Resources.UsersViewImage;

    private void buttonSearch_Click(object sender, EventArgs e)
    {
    }

    private void toolStripMenuItem1_Click(object sender, EventArgs e)
    {
    }

    private void toolStripButtonSaveItem_Click(object sender, EventArgs e)
    {
    }

    private void toolStripButtonViewItem_Click(object sender, EventArgs e)
    {
    }

    private void toolStripButtonCompareLatest_Click(object sender, EventArgs e)
    {
    }

    private void textBoxFilter_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar != '\r')
        return;
      SearchList();
    }

    private void textBoxFilter_TextChanged(object sender, EventArgs e) => SearchList();

    protected override void Dispose(bool disposing)
    {
      if (disposing && components != null)
        components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      groupBoxFilter = new GroupBox();
      textBoxFilter = new TextBox();
      listViewUsers = new VirtualListView();
      columnHeader9 = new ColumnHeader();
      columnHeader10 = new ColumnHeader();
      columnHeader1 = new ColumnHeader();
      groupBoxFilter.SuspendLayout();
      SuspendLayout();
      groupBoxFilter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      groupBoxFilter.Controls.Add(textBoxFilter);
      groupBoxFilter.Dock = DockStyle.Top;
      groupBoxFilter.Location = new Point(0, 0);
      groupBoxFilter.Name = "groupBoxFilter";
      groupBoxFilter.Size = new Size(725, 54);
      groupBoxFilter.TabIndex = 4;
      groupBoxFilter.TabStop = false;
      groupBoxFilter.Text = " Filter criteria";
      textBoxFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      textBoxFilter.Location = new Point(16, 18);
      textBoxFilter.Name = "textBoxFilter";
      textBoxFilter.Size = new Size(686, 20);
      textBoxFilter.TabIndex = 17;
      textBoxFilter.TextChanged += textBoxFilter_TextChanged;
      textBoxFilter.KeyPress += textBoxFilter_KeyPress;
      listViewUsers.Columns.AddRange(new ColumnHeader[3]
      {
        columnHeader9,
        columnHeader1,
        columnHeader10
      });
      listViewUsers.Dock = DockStyle.Fill;
      listViewUsers.FullRowSelect = true;
      listViewUsers.GridLines = true;
      listViewUsers.HideSelection = false;
      listViewUsers.Location = new Point(0, 54);
      listViewUsers.Name = "listViewUsers";
      listViewUsers.Size = new Size(725, 468);
      listViewUsers.Sorted = false;
      listViewUsers.TabIndex = 5;
      listViewUsers.UseCompatibleStateImageBehavior = false;
      listViewUsers.View = View.Details;
      columnHeader9.Text = "User Name";
      columnHeader9.Width = 140;
      columnHeader10.Text = "Display Name";
      columnHeader10.Width = 300;
      columnHeader1.Text = "Domain";
      columnHeader1.Width = 100;
      AutoScaleDimensions = new SizeF(6f, 13f);
      AutoScaleMode = AutoScaleMode.Font;
      Controls.Add(listViewUsers);
      Controls.Add(groupBoxFilter);
      Name = nameof (UsersViewControl);
      Size = new Size(725, 522);
      groupBoxFilter.ResumeLayout(false);
      groupBoxFilter.PerformLayout();
      ResumeLayout(false);
    }

    private void AddUsers(string searchString)
    {
      listViewUsers.BeginUpdate();
      listViewUsers.ClearItems();
      foreach (DataRow row in (InternalDataCollectionBase) _dtUsers.Rows)
      {
        var text = row[ListTable.ColumnID] as string;
        var str1 = row[ListTable.ColumnValue] as string;
        var str2 = string.Empty;
        var length = text.IndexOf('\\');
        if (length != -1)
        {
          str2 = text.Substring(0, length);
          text = text.Substring(length + 1);
        }
        if (string.IsNullOrEmpty(searchString) || text.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1 || str1.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1)
          listViewUsers.AddItem(new ListViewItem(text)
          {
            SubItems = {
              str2,
              str1
            }
          });
      }
      listViewUsers.EndUpdate();
    }

    public override void Initialize(TfsController controller)
    {
      _dtUsers = controller.Users.UsersTable.Copy();
      AddUsers(string.Empty);
    }

    public override void LoadUsers(TfsController controller)
    {
      _dtUsers = controller.Users.UsersTable.Copy();
      AddUsers(string.Empty);
    }

    private void SearchList() => AddUsers(textBoxFilter.Text);
  }
}
