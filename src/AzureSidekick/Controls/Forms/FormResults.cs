// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.Forms.FormResults
// Assembly: Attrice.TeamFoundation.Controls.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=1a24c887ab53aa6e
// MVID: 393BE899-3C29-4A70-86D7-B4B965E5A8F5
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controls.12.dll

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Attrice.TeamFoundation.Common;

namespace Attrice.TeamFoundation.Controls.Forms
{
  public class FormResults : Form
  {
    private bool _hasErrors;
    private IContainer components;
    private Label labelOperation;
    private ListView listViewResults;
    private ColumnHeader columnHeader1;
    private ColumnHeader columnHeader2;
    private ColumnHeader columnHeader3;
    private Button button1;

    public bool HasErrors => _hasErrors;

    public FormResults()
    {
      InitializeComponent();
      listViewResults.ListViewItemSorter = new CustomListSorter();
    }

    public void Initialize(string operation)
    {
      _hasErrors = false;
      labelOperation.Text = operation;
      listViewResults.Items.Clear();
    }

    public void AddResult(bool success, string entity, string message)
    {
      _hasErrors |= !success;
      listViewResults.Items.Add(new ListViewItem(success ? "Success" : "Error")
      {
        SubItems = {
          entity,
          message
        }
      });
    }

    private void listViewResults_ColumnClick(object sender, ColumnClickEventArgs e)
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
      var componentResourceManager = new ComponentResourceManager(typeof (FormResults));
      labelOperation = new Label();
      listViewResults = new ListView();
      columnHeader1 = new ColumnHeader();
      columnHeader2 = new ColumnHeader();
      columnHeader3 = new ColumnHeader();
      button1 = new Button();
      SuspendLayout();
      labelOperation.AutoSize = true;
      labelOperation.Location = new Point(12, 9);
      labelOperation.Name = "labelOperation";
      labelOperation.Size = new Size(53, 13);
      labelOperation.TabIndex = 0;
      labelOperation.Text = "Operation";
      listViewResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      listViewResults.Columns.AddRange(new ColumnHeader[3]
      {
        columnHeader1,
        columnHeader2,
        columnHeader3
      });
      listViewResults.FullRowSelect = true;
      listViewResults.GridLines = true;
      listViewResults.Location = new Point(2, 25);
      listViewResults.Name = "listViewResults";
      listViewResults.Size = new Size(621, 400);
      listViewResults.TabIndex = 1;
      listViewResults.UseCompatibleStateImageBehavior = false;
      listViewResults.View = View.Details;
      listViewResults.ColumnClick += listViewResults_ColumnClick;
      columnHeader1.Text = "Result";
      columnHeader2.Text = "Entity";
      columnHeader2.Width = 150;
      columnHeader3.Text = "Message";
      columnHeader3.Width = 200;
      button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      button1.DialogResult = DialogResult.Cancel;
      button1.Location = new Point(548, 431);
      button1.Name = "button1";
      button1.Size = new Size(75, 23);
      button1.TabIndex = 2;
      button1.Text = "OK";
      button1.UseVisualStyleBackColor = true;
      AcceptButton = button1;
      AutoScaleDimensions = new SizeF(6f, 13f);
      AutoScaleMode = AutoScaleMode.Font;
      CancelButton = button1;
      ClientSize = new Size(625, 456);
      Controls.Add(button1);
      Controls.Add(listViewResults);
      Controls.Add(labelOperation);
      FormBorderStyle = FormBorderStyle.SizableToolWindow;
      Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      Name = nameof (FormResults);
      ShowIcon = false;
      SizeGripStyle = SizeGripStyle.Hide;
      StartPosition = FormStartPosition.CenterScreen;
      Text = "Results log";
      ResumeLayout(false);
      PerformLayout();
    }
  }
}
