// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.NewWorkspaceListView
// Assembly: Attrice.TeamFoundation.Controls.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=1a24c887ab53aa6e
// MVID: 393BE899-3C29-4A70-86D7-B4B965E5A8F5
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controls.12.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Attrice.TeamFoundation.Controls
{
  public class NewWorkspaceListView : ListView
  {
    private Container components;
    private string _selectionPrompt;
    private string _defaultItemText;
    private ComboBox _comboBoxUsers;
    private ComboBox _comboBoxComputers;
    private TextBox _textBoxName;
    private ColumnHeader columnHeader1;
    private ColumnHeader columnHeader2;
    private ColumnHeader columnHeader3;
    private ListViewItem listViewItemDefault;

    public NewWorkspaceListView()
    {
      InitializeComponent();
      View = View.Details;
      FullRowSelect = true;
      MultiSelect = false;
      GridLines = true;
      OwnerDraw = true;
      HideSelection = false;
      HeaderStyle = ColumnHeaderStyle.Nonclickable;
      InitializeOwnerDraw();
      InitializeListView();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && components != null)
        components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent() => components = new Container();

    public string SelectionPrompt
    {
      get => _selectionPrompt;
      set
      {
        _selectionPrompt = value;
        if (listViewItemDefault == null)
          return;
        listViewItemDefault.SubItems[1].Text = _selectionPrompt;
      }
    }

    public string DefaultItemText
    {
      get => _defaultItemText;
      set => _defaultItemText = value;
    }

    public void DeleteCurrent()
    {
      if (SelectedIndices.Count != 1 || SelectedItems[0] == listViewItemDefault)
        return;
      Items.RemoveAt(SelectedIndices[0]);
    }

    public bool CanDelete => SelectedIndices.Count == 1 && SelectedItems[0] != listViewItemDefault;

    public ComboBox UsersComboBox => _comboBoxUsers;

    public ComboBox ComputersComboBox => _comboBoxComputers;

    public bool IsDefaultItem(ListViewItem item) => listViewItemDefault == item;

    private void InitializeListView()
    {
      columnHeader1 = new ColumnHeader();
      columnHeader2 = new ColumnHeader();
      columnHeader3 = new ColumnHeader();
      columnHeader1.Text = "Name";
      columnHeader1.Width = 99;
      columnHeader2.Text = "Owner";
      columnHeader2.Width = 187;
      columnHeader3.Text = "Computer";
      columnHeader3.Width = 129;
      Columns.AddRange(new ColumnHeader[3]
      {
        columnHeader1,
        columnHeader2,
        columnHeader3
      });
      if (DesignMode)
        return;
      listViewItemDefault = new ListViewItem();
      listViewItemDefault.UseItemStyleForSubItems = true;
      listViewItemDefault.ForeColor = SystemColors.GrayText;
      listViewItemDefault.SubItems.Add(_selectionPrompt);
      listViewItemDefault.SubItems.Add("");
      Items.Add(listViewItemDefault);
    }

    private void InitializeOwnerDraw()
    {
      _comboBoxUsers = new ComboBox();
      _comboBoxComputers = new ComboBox();
      _textBoxName = new TextBox();
      Controls.Add(_comboBoxUsers);
      Controls.Add(_comboBoxComputers);
      Controls.Add(_textBoxName);
      _comboBoxComputers.KeyPress += ControlKeyPress;
      _comboBoxUsers.KeyPress += ControlKeyPress;
      _textBoxName.KeyPress += ControlKeyPress;
      _comboBoxUsers.Visible = _comboBoxComputers.Visible = _textBoxName.Visible = false;
      _comboBoxUsers.FlatStyle = _comboBoxComputers.FlatStyle = FlatStyle.Popup;
      _textBoxName.BorderStyle = BorderStyle.FixedSingle;
      _comboBoxUsers.AutoCompleteMode = _comboBoxComputers.AutoCompleteMode = AutoCompleteMode.Suggest;
      _comboBoxUsers.AutoCompleteSource = _comboBoxComputers.AutoCompleteSource = AutoCompleteSource.ListItems;
    }

    private void ControlKeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == '\r')
      {
        HandleControls(_textBoxName.Tag as ListViewItem);
        _textBoxName.Tag = null;
        _comboBoxComputers.Visible = _comboBoxUsers.Visible = _textBoxName.Visible = false;
      }
      else if (e.KeyChar == '\u001B')
      {
        _textBoxName.Tag = null;
        _comboBoxComputers.Visible = _comboBoxUsers.Visible = _textBoxName.Visible = false;
      }
      else
      {
        if (e.KeyChar != '\t' || sender != _textBoxName)
          return;
        _comboBoxUsers.Focus();
        e.Handled = true;
      }
    }

    protected override void OnDrawItem(DrawListViewItemEventArgs e)
    {
      base.OnDrawItem(e);
      if (e.Item.Selected && (e.State & ListViewItemStates.Selected) != 0 && (e.State & ListViewItemStates.Focused) != 0)
      {
        if (_textBoxName.Tag != null && _textBoxName.Visible)
          HandleControls(_textBoxName.Tag as ListViewItem);
        _textBoxName.Tag = e.Item;
        _textBoxName.Location = e.Bounds.Location;
        _textBoxName.Width = Columns[0].Width;
        _textBoxName.Height = e.Bounds.Height - 3;
        _textBoxName.Visible = true;
        var comboBoxUsers1 = _comboBoxUsers;
        var x1 = e.Bounds.Left + Columns[0].Width;
        var bounds = e.Bounds;
        var top1 = bounds.Top;
        var point1 = new Point(x1, top1);
        comboBoxUsers1.Location = point1;
        var comboBoxUsers2 = _comboBoxUsers;
        bounds = e.Bounds;
        var num1 = bounds.Height - 5;
        comboBoxUsers2.Height = num1;
        _comboBoxUsers.Width = Columns[1].Width;
        _comboBoxUsers.Visible = true;
        var comboBoxComputers1 = _comboBoxComputers;
        var x2 = _comboBoxUsers.Location.X + Columns[1].Width;
        bounds = e.Bounds;
        var top2 = bounds.Top;
        var point2 = new Point(x2, top2);
        comboBoxComputers1.Location = point2;
        var comboBoxComputers2 = _comboBoxComputers;
        bounds = e.Bounds;
        var num2 = bounds.Height - 5;
        comboBoxComputers2.Height = num2;
        _comboBoxComputers.Width = Columns[2].Width;
        _comboBoxComputers.Visible = true;
        if (e.Item != listViewItemDefault)
        {
          _textBoxName.Text = e.Item.Text;
          _comboBoxUsers.Text = e.Item.SubItems[1].Text;
          _comboBoxComputers.Text = e.Item.SubItems[2].Text;
        }
        else
        {
          _textBoxName.Text = _defaultItemText;
          _comboBoxUsers.Text = _comboBoxComputers.Text = "";
        }
      }
      e.DrawDefault = false;
    }

    protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
    {
      base.OnDrawSubItem(e);
      e.DrawDefault = true;
    }

    protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
    {
      base.OnDrawColumnHeader(e);
      e.DrawDefault = true;
    }

    private void HandleControls(ListViewItem item)
    {
      if ((_textBoxName.Text == "" || _textBoxName.Text == _defaultItemText && !_textBoxName.Modified) && _comboBoxComputers.Text == "" && _comboBoxUsers.Text == "")
      {
        if (item == listViewItemDefault || item.Index == -1)
          return;
        Items.RemoveAt(item.Index);
      }
      else if (item != listViewItemDefault)
      {
        item.Text = _textBoxName.Text;
        item.SubItems[1].Text = _comboBoxUsers.Text;
        item.SubItems[2].Text = _comboBoxComputers.Text;
      }
      else
      {
        item = new ListViewItem(_textBoxName.Text);
        item.SubItems.Add(_comboBoxUsers.Text);
        item.SubItems.Add(_comboBoxComputers.Text);
        Items.Insert(Items.Count - 1, item);
      }
    }

    protected override void OnItemSelectionChanged(ListViewItemSelectionChangedEventArgs e)
    {
      base.OnItemSelectionChanged(e);
      if (SelectedIndices.Count != 0)
        return;
      if (_textBoxName.Visible && _textBoxName.Tag != null)
        HandleControls(_textBoxName.Tag as ListViewItem);
      _textBoxName.Tag = null;
      _comboBoxComputers.Visible = _comboBoxUsers.Visible = _textBoxName.Visible = false;
    }

    protected override void OnLeave(EventArgs e)
    {
      base.OnLeave(e);
      if (_textBoxName.Visible && _textBoxName.Tag != null)
        HandleControls(_textBoxName.Tag as ListViewItem);
      _textBoxName.Tag = null;
      _comboBoxComputers.Visible = _comboBoxUsers.Visible = _textBoxName.Visible = false;
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      base.OnKeyPress(e);
      if (e.KeyChar == '\r' && _textBoxName.Visible && _textBoxName.Tag != null)
      {
        HandleControls(_textBoxName.Tag as ListViewItem);
        _textBoxName.Tag = null;
        _comboBoxComputers.Visible = _comboBoxUsers.Visible = _textBoxName.Visible = false;
      }
      else
      {
        if (e.KeyChar != '\u001B')
          return;
        _textBoxName.Tag = null;
        _comboBoxComputers.Visible = _comboBoxUsers.Visible = _textBoxName.Visible = false;
      }
    }
  }
}
