// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.Forms.FormRelativeHistory
// Assembly: Attrice.TeamFoundation.Controls.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=1a24c887ab53aa6e
// MVID: 393BE899-3C29-4A70-86D7-B4B965E5A8F5
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controls.12.dll

using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Attrice.TeamFoundation.Common;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace Attrice.TeamFoundation.Controls.Forms
{
  public class FormRelativeHistory : Form
  {
    private IContainer components;
    private ListView listView1;
    private ColumnHeader columnHeader1;
    private ColumnHeader columnHeader2;
    private ColumnHeader columnHeader3;
    private ColumnHeader columnHeader4;
    private Button buttonCancel;

    public FormRelativeHistory()
    {
      InitializeComponent();
      listView1.ListViewItemSorter = new CustomListSorter(ListSorterType.Date | ListSorterType.Integer);
    }

    public void Initialize(
      string serverItem,
      string labelFrom,
      string labelTo,
      IEnumerable changesets)
    {
      Text += serverItem;
      foreach (Changeset changeset in changesets)
        listView1.Items.Add(new ListViewItem(changeset.ChangesetId.ToString())
        {
          SubItems = {
            " ",
            changeset.CreationDate.ToString(),
            changeset.Comment
          }
        });
      listView1.Items[0].SubItems[1].Text = labelTo;
      listView1.Items[listView1.Items.Count - 1].SubItems[1].Text = labelFrom;
    }

    private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      var listView = sender as ListView;
      (listView.ListViewItemSorter as CustomListSorter).SetColumn(listView.Columns[e.Column]);
      listView.Sort();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && components != null)
        components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      var componentResourceManager = new ComponentResourceManager(typeof (FormRelativeHistory));
      listView1 = new ListView();
      columnHeader1 = new ColumnHeader();
      columnHeader2 = new ColumnHeader();
      columnHeader3 = new ColumnHeader();
      columnHeader4 = new ColumnHeader();
      buttonCancel = new Button();
      SuspendLayout();
      listView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      listView1.Columns.AddRange(new ColumnHeader[4]
      {
        columnHeader1,
        columnHeader2,
        columnHeader3,
        columnHeader4
      });
      listView1.FullRowSelect = true;
      listView1.GridLines = true;
      listView1.Location = new Point(0, 1);
      listView1.MultiSelect = false;
      listView1.Name = "listView1";
      listView1.Size = new Size(414, 237);
      listView1.TabIndex = 0;
      listView1.UseCompatibleStateImageBehavior = false;
      listView1.View = View.Details;
      listView1.ColumnClick += listView1_ColumnClick;
      columnHeader1.Text = "Changeset";
      columnHeader1.Width = 80;
      columnHeader2.Text = "Label";
      columnHeader2.Width = 80;
      columnHeader3.Text = "Checkin Date";
      columnHeader3.Width = 130;
      columnHeader4.Text = "Comment";
      columnHeader4.Width = 120;
      buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      buttonCancel.DialogResult = DialogResult.Cancel;
      buttonCancel.Location = new Point(332, 248);
      buttonCancel.Name = "buttonCancel";
      buttonCancel.Size = new Size(75, 23);
      buttonCancel.TabIndex = 1;
      buttonCancel.Text = "Cancel";
      buttonCancel.UseVisualStyleBackColor = true;
      AutoScaleDimensions = new SizeF(6f, 13f);
      AutoScaleMode = AutoScaleMode.Font;
      CancelButton = buttonCancel;
      ClientSize = new Size(413, 280);
      Controls.Add(buttonCancel);
      Controls.Add(listView1);
      FormBorderStyle = FormBorderStyle.SizableToolWindow;
      Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      Name = nameof (FormRelativeHistory);
      ShowIcon = false;
      SizeGripStyle = SizeGripStyle.Hide;
      StartPosition = FormStartPosition.CenterScreen;
      Text = "Relative History for ";
      ResumeLayout(false);
    }
  }
}
