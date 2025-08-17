// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.Forms.FormSelectWorkItems
// Assembly: Attrice.TeamFoundation.Controls.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=1a24c887ab53aa6e
// MVID: 393BE899-3C29-4A70-86D7-B4B965E5A8F5
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controls.12.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Attrice.TeamFoundation.Controllers;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace Attrice.TeamFoundation.Controls.Forms
{
  public class FormSelectWorkItems : Form
  {
    private TfsController _controller;
    private WorkItem[] _workItems;
    private IContainer components;
    private GroupBox groupBoxSelectionType;
    private Button buttonSearch;
    private Panel panel1;
    private Button buttonOK;
    private TabControl tabControl;
    private TabPage tabPageQuery;
    private Panel panel2;
    private ComboBox comboBoxQueryName;
    private ComboBox comboBoxProject;
    private Label labelQueryName;
    private Label labelProject;
    private GroupBox groupBoxResults;
    private ListView listView;
    private Button buttonClose;
    private TabPage tabPageCustom;
    private CustomWIQLControl customWIQLControl1;
    private Button buttonClear;
    private Button buttonSelectAll;

    public WorkItem[] WorkItems => _workItems;

    public FormSelectWorkItems(TfsController controller)
    {
      InitializeComponent();
      _controller = controller;
      _workItems = new WorkItem[0];
      comboBoxProject.BeginUpdate();
      foreach (Project project in _controller.WorkItemStore.Projects)
        comboBoxProject.Items.Add(project);
      comboBoxProject.EndUpdate();
      customWIQLControl1.Initialize(_controller);
    }

    public void LoadUsers() => customWIQLControl1.LoadUsers();

    private void comboBoxProject_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (!(comboBoxProject.SelectedItem is Project selectedItem))
        return;
      comboBoxQueryName.BeginUpdate();
      comboBoxQueryName.Items.Clear();
      foreach (StoredQuery storedQuery in selectedItem.StoredQueries)
        comboBoxQueryName.Items.Add(storedQuery);
      comboBoxQueryName.EndUpdate();
    }

    private void ButtonSearch_Click(object sender, EventArgs e) => Search();

    private void Search()
    {
      Cursor = Cursors.WaitCursor;
      listView.Clear();
      listView.BeginUpdate();
      if (tabControl.SelectedTab == tabPageQuery)
      {
        if (comboBoxQueryName.SelectedItem is StoredQuery selectedItem)
          FillByQuery(selectedItem);
      }
      else if (tabControl.SelectedTab == tabPageCustom)
      {
        var wiql = customWIQLControl1.WIQL;
        if (!string.IsNullOrEmpty(wiql))
          FillByWIQL(wiql);
      }
      listView.EndUpdate();
      Cursor = Cursors.Default;
    }

    private void FillByQuery(StoredQuery query)
    {
      FillByWIQL(query.QueryText.Replace("@project", $"'{(object)query.Project.Name}'"));
    }

    private void FillByWorkItemID(int id)
    {
      WorkItem workItem;
      try
      {
        workItem = _controller.WorkItemStore.GetWorkItem(id);
      }
      catch
      {
        return;
      }
      if (workItem == null)
        return;
      listView.Columns.AddRange(new ColumnHeader[4]
      {
        new ColumnHeader { Text = "ID" },
        new ColumnHeader { Text = "Work Item Type" },
        new ColumnHeader { Text = "State" },
        new ColumnHeader { Text = "Title" }
      });
      listView.Items.Add(new ListViewItem(new string[4]
      {
        workItem.Id.ToString(),
        workItem.Type.Name,
        workItem.State,
        workItem.Title
      })
      {
        Tag = workItem
      });
    }

    private void FillByWIQL(string wiql)
    {
      try
      {
        listView.BeginUpdate();
        var query1 = new Query(_controller.WorkItemStore, wiql);
        var query2 = query1;
        var workItemCollection = query2.EndQuery(query2.BeginQuery());
        foreach (FieldDefinition displayField in query1.DisplayFieldList)
          listView.Columns.Add(new ColumnHeader
          {
            Text = displayField.Name
          });
        if (workItemCollection.Count == 0)
          return;
        var items = new string[query1.DisplayFieldList.Count];
        foreach (WorkItem workItem in workItemCollection)
        {
          var num = 0;
          foreach (FieldDefinition displayField in query1.DisplayFieldList)
            items[num++] = workItem.Fields[displayField.Name].Value.ToString();
          listView.Items.Add(new ListViewItem(items)
          {
            Tag = workItem
          });
        }
        foreach (ColumnHeader column in listView.Columns)
        {
          listView.ColumnWidthChanged += listView_ColumnWidthChanged;
          column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
          ResizeColumn(column);
        }
      }
      catch (Exception ex)
      {
      }
      finally
      {
        listView.EndUpdate();
      }
    }

    private void listView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
    {
      ResizeColumn(listView.Columns[e.ColumnIndex]);
    }

    private void ResizeColumn(ColumnHeader column)
    {
      listView.ColumnWidthChanged -= listView_ColumnWidthChanged;
      var num = (int) Math.Ceiling(listView.CreateGraphics().MeasureString(column.Text, listView.Font).Width) + 8;
      if (num > column.Width)
        column.Width = num;
      listView.ColumnWidthChanged += listView_ColumnWidthChanged;
    }

    private void comboBoxProject_Format(object sender, ListControlConvertEventArgs e)
    {
      var convertEventArgs = e;
      convertEventArgs.Value = (convertEventArgs.ListItem as Project).Name;
    }

    private void comboBoxQueryName_Format(object sender, ListControlConvertEventArgs e)
    {
      var convertEventArgs = e;
      convertEventArgs.Value = (convertEventArgs.ListItem as StoredQuery).Name;
    }

    private WorkItem[] FillWorkItems()
    {
      var workItemArray = new WorkItem[listView.SelectedItems.Count];
      var num = 0;
      foreach (ListViewItem selectedItem in listView.SelectedItems)
        workItemArray[num++] = selectedItem.Tag as WorkItem;
      return workItemArray;
    }

    private void listView_ItemSelectionChanged(
      object sender,
      ListViewItemSelectionChangedEventArgs e)
    {
      buttonOK.Enabled = listView.SelectedIndices.Count != 0;
    }

    private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (listView.SelectedIndices.Count == 0)
        return;
      buttonOK_Click(sender, e);
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      _workItems = FillWorkItems();
      ClearAll();
      DialogResult = DialogResult.OK;
      Close();
    }

    private void ClearAll()
    {
      listView.Clear();
      buttonOK.Enabled = false;
    }

    private void buttonClear_Click(object sender, EventArgs e) => customWIQLControl1.Clear();

    private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
    {
      buttonClear.Visible = tabControl.SelectedTab == tabPageCustom;
    }

    private void buttonSelectAll_Click(object sender, EventArgs e)
    {
      listView.Focus();
      foreach (ListViewItem listViewItem in listView.Items)
        listViewItem.Selected = true;
    }

    private void listView_SelectedIndexChanged(object sender, EventArgs e)
    {
      buttonOK.Enabled = listView.SelectedIndices.Count != 0;
    }

    private void buttonClose_Click(object sender, EventArgs e) => ClearAll();

    protected override void Dispose(bool disposing)
    {
      if (disposing && components != null)
        components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      groupBoxSelectionType = new GroupBox();
      tabControl = new TabControl();
      tabPageQuery = new TabPage();
      comboBoxQueryName = new ComboBox();
      comboBoxProject = new ComboBox();
      labelQueryName = new Label();
      labelProject = new Label();
      tabPageCustom = new TabPage();
      panel2 = new Panel();
      buttonSelectAll = new Button();
      buttonClear = new Button();
      buttonSearch = new Button();
      panel1 = new Panel();
      buttonClose = new Button();
      buttonOK = new Button();
      groupBoxResults = new GroupBox();
      listView = new ListView();
      customWIQLControl1 = new CustomWIQLControl();
      groupBoxSelectionType.SuspendLayout();
      tabControl.SuspendLayout();
      tabPageQuery.SuspendLayout();
      tabPageCustom.SuspendLayout();
      panel2.SuspendLayout();
      panel1.SuspendLayout();
      groupBoxResults.SuspendLayout();
      SuspendLayout();
      groupBoxSelectionType.Controls.Add(tabControl);
      groupBoxSelectionType.Controls.Add(panel2);
      groupBoxSelectionType.Dock = DockStyle.Top;
      groupBoxSelectionType.Location = new Point(0, 0);
      groupBoxSelectionType.Name = "groupBoxSelectionType";
      groupBoxSelectionType.Size = new Size(719, 384);
      groupBoxSelectionType.TabIndex = 0;
      groupBoxSelectionType.TabStop = false;
      groupBoxSelectionType.Text = "Select by";
      tabControl.Controls.Add(tabPageQuery);
      tabControl.Controls.Add(tabPageCustom);
      tabControl.Dock = DockStyle.Fill;
      tabControl.Location = new Point(3, 16);
      tabControl.Name = "tabControl";
      tabControl.SelectedIndex = 0;
      tabControl.Size = new Size(713, 333);
      tabControl.TabIndex = 13;
      tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
      tabPageQuery.Controls.Add(comboBoxQueryName);
      tabPageQuery.Controls.Add(comboBoxProject);
      tabPageQuery.Controls.Add(labelQueryName);
      tabPageQuery.Controls.Add(labelProject);
      tabPageQuery.Location = new Point(4, 22);
      tabPageQuery.Name = "tabPageQuery";
      tabPageQuery.Padding = new Padding(3);
      tabPageQuery.Size = new Size(705, 307);
      tabPageQuery.TabIndex = 0;
      tabPageQuery.Text = "Query";
      tabPageQuery.UseVisualStyleBackColor = true;
      comboBoxQueryName.DropDownStyle = ComboBoxStyle.DropDownList;
      comboBoxQueryName.FormattingEnabled = true;
      comboBoxQueryName.Location = new Point(78, 44);
      comboBoxQueryName.Name = "comboBoxQueryName";
      comboBoxQueryName.Size = new Size(259, 21);
      comboBoxQueryName.TabIndex = 3;
      comboBoxQueryName.Format += comboBoxQueryName_Format;
      comboBoxProject.DropDownStyle = ComboBoxStyle.DropDownList;
      comboBoxProject.FormattingEnabled = true;
      comboBoxProject.Location = new Point(78, 17);
      comboBoxProject.Name = "comboBoxProject";
      comboBoxProject.Size = new Size(259, 21);
      comboBoxProject.TabIndex = 2;
      comboBoxProject.SelectedIndexChanged += comboBoxProject_SelectedIndexChanged;
      comboBoxProject.Format += comboBoxProject_Format;
      labelQueryName.AutoSize = true;
      labelQueryName.Location = new Point(6, 49);
      labelQueryName.Name = "labelQueryName";
      labelQueryName.Size = new Size(66, 13);
      labelQueryName.TabIndex = 1;
      labelQueryName.Text = "Query Name";
      labelProject.AutoSize = true;
      labelProject.Location = new Point(6, 20);
      labelProject.Name = "labelProject";
      labelProject.Size = new Size(40, 13);
      labelProject.TabIndex = 0;
      labelProject.Text = "Project";
      tabPageCustom.Controls.Add(customWIQLControl1);
      tabPageCustom.Location = new Point(4, 22);
      tabPageCustom.Name = "tabPageCustom";
      tabPageCustom.Size = new Size(705, 307);
      tabPageCustom.TabIndex = 3;
      tabPageCustom.Text = "Custom";
      tabPageCustom.UseVisualStyleBackColor = true;
      panel2.Controls.Add(buttonClear);
      panel2.Controls.Add(buttonSearch);
      panel2.Dock = DockStyle.Bottom;
      panel2.Location = new Point(3, 349);
      panel2.Name = "panel2";
      panel2.Size = new Size(713, 32);
      panel2.TabIndex = 13;
      buttonSelectAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      buttonSelectAll.Location = new Point(12, 10);
      buttonSelectAll.Name = "buttonSelectAll";
      buttonSelectAll.Size = new Size(75, 23);
      buttonSelectAll.TabIndex = 8;
      buttonSelectAll.Text = "Select All";
      buttonSelectAll.UseVisualStyleBackColor = true;
      buttonSelectAll.Click += buttonSelectAll_Click;
      buttonClear.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      buttonClear.Location = new Point(547, 6);
      buttonClear.Name = "buttonClear";
      buttonClear.Size = new Size(75, 23);
      buttonClear.TabIndex = 7;
      buttonClear.Text = "Clear";
      buttonClear.UseVisualStyleBackColor = true;
      buttonClear.Visible = false;
      buttonClear.Click += buttonClear_Click;
      buttonSearch.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      buttonSearch.Location = new Point(628, 6);
      buttonSearch.Name = "buttonSearch";
      buttonSearch.Size = new Size(75, 23);
      buttonSearch.TabIndex = 6;
      buttonSearch.Text = "Search";
      buttonSearch.UseVisualStyleBackColor = true;
      buttonSearch.Click += ButtonSearch_Click;
      panel1.Controls.Add(buttonSelectAll);
      panel1.Controls.Add(buttonClose);
      panel1.Controls.Add(buttonOK);
      panel1.Dock = DockStyle.Bottom;
      panel1.Location = new Point(0, 548);
      panel1.Name = "panel1";
      panel1.Size = new Size(719, 45);
      panel1.TabIndex = 1;
      buttonClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      buttonClose.DialogResult = DialogResult.Cancel;
      buttonClose.Location = new Point(631, 10);
      buttonClose.Name = "buttonClose";
      buttonClose.Size = new Size(75, 23);
      buttonClose.TabIndex = 8;
      buttonClose.Text = "Close";
      buttonClose.UseVisualStyleBackColor = true;
      buttonClose.Click += buttonClose_Click;
      buttonOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      buttonOK.Enabled = false;
      buttonOK.Location = new Point(550, 10);
      buttonOK.Name = "buttonOK";
      buttonOK.Size = new Size(75, 23);
      buttonOK.TabIndex = 7;
      buttonOK.Text = "OK";
      buttonOK.UseVisualStyleBackColor = true;
      buttonOK.Click += buttonOK_Click;
      groupBoxResults.Controls.Add(listView);
      groupBoxResults.Dock = DockStyle.Fill;
      groupBoxResults.Location = new Point(0, 384);
      groupBoxResults.Name = "groupBoxResults";
      groupBoxResults.Size = new Size(719, 164);
      groupBoxResults.TabIndex = 2;
      groupBoxResults.TabStop = false;
      groupBoxResults.Text = "Results";
      listView.Dock = DockStyle.Fill;
      listView.FullRowSelect = true;
      listView.GridLines = true;
      listView.Location = new Point(3, 16);
      listView.Name = "listView";
      listView.ShowItemToolTips = true;
      listView.Size = new Size(713, 145);
      listView.TabIndex = 3;
      listView.UseCompatibleStateImageBehavior = false;
      listView.View = View.Details;
      listView.MouseDoubleClick += listView_MouseDoubleClick;
      listView.SelectedIndexChanged += listView_SelectedIndexChanged;
      customWIQLControl1.Dock = DockStyle.Fill;
      customWIQLControl1.Location = new Point(0, 0);
      customWIQLControl1.Name = "customWIQLControl1";
      customWIQLControl1.Size = new Size(705, 307);
      customWIQLControl1.TabIndex = 0;
      AutoScaleDimensions = new SizeF(6f, 13f);
      AutoScaleMode = AutoScaleMode.Font;
      CancelButton = buttonClose;
      ClientSize = new Size(719, 593);
      Controls.Add(groupBoxResults);
      Controls.Add(panel1);
      Controls.Add(groupBoxSelectionType);
      Name = nameof (FormSelectWorkItems);
      ShowIcon = false;
      ShowInTaskbar = false;
      StartPosition = FormStartPosition.CenterScreen;
      Text = "Select Work Items";
      groupBoxSelectionType.ResumeLayout(false);
      tabControl.ResumeLayout(false);
      tabPageQuery.ResumeLayout(false);
      tabPageQuery.PerformLayout();
      tabPageCustom.ResumeLayout(false);
      panel2.ResumeLayout(false);
      panel1.ResumeLayout(false);
      groupBoxResults.ResumeLayout(false);
      ResumeLayout(false);
    }
  }
}
