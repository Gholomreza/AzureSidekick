// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Common.DialogFindChangeset
// Assembly: Attrice.TeamFoundation.Common.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=35acb2d39e045fbd
// MVID: A152C2C1-BE36-402B-9F4C-D577D02612BB
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Common.12.dll

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace Attrice.TeamFoundation.Common
{
  public class DialogFindChangeset : IDisposable
  {
    private readonly Form _dialog;
    private Button _detailsButton;
    private Button _okButton;
    private Control _changesetsControl;
    private readonly List<int> _selectedChangesets;

    public DialogFindChangeset(VersionControlServer server, DialogSettings settings)
    {
        //TODO
      // _dialog = Assembly.GetAssembly(typeof (WorkItemPolicy)).CreateInstance("Microsoft.TeamFoundation.VersionControl.Controls.DialogFindChangeset", false, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new object[3]
      // {
      //   server,
      //   true,
      //   null
      // }, CultureInfo.CurrentCulture, null) as Form;
      // _dialog.StartPosition = FormStartPosition.CenterScreen;
      // _selectedChangesets = new List<int>();
      // Customize(settings);
    }

    public void SetFilterPath(string path)
    {
      var str = _dialog.GetType().InvokeMember("FilteredPath", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetProperty, null, _dialog, null) as string;
      if (string.IsNullOrEmpty(path) || path.Equals(str))
        return;
      _dialog.GetType().InvokeMember("FilteredPath", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetProperty, null, _dialog, new object[1]
      {
        path
      });
    }

    public int[] SelectedChangesets => _selectedChangesets.ToArray();

    private void Changesets_SelectedItemsChanged(object sender, EventArgs e)
    {
      var objArray = _changesetsControl.GetType().GetProperty("SelectedItems", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(_changesetsControl, null) as object[];
      _detailsButton.Enabled = objArray.Length == 1;
      _okButton.Enabled = objArray.Length != 0;
      _selectedChangesets.Clear();
      PropertyInfo propertyInfo = null;
      foreach (var obj in objArray)
      {
        if (propertyInfo == null)
          propertyInfo = obj.GetType().GetProperty("Microsoft.TeamFoundation.VersionControl.Controls.IHistoryChangesetItem.ChangesetId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        _selectedChangesets.Add((int) propertyInfo.GetValue(obj, null));
      }
    }

    private void Customize(DialogSettings settings)
    {
      _dialog.Text += " For Code Review";
      var type = _dialog.GetType();
      _changesetsControl = type.GetField("m_controlChangesets", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(_dialog) as Control;
      var eventInfo = _changesetsControl.GetType().GetEvent("SelectedItemsChanged");
      var @delegate = Delegate.CreateDelegate(eventInfo.EventHandlerType, this, GetType().GetMethod("Changesets_SelectedItemsChanged", BindingFlags.Instance | BindingFlags.NonPublic));
      eventInfo.GetAddMethod().Invoke(_changesetsControl, new object[1]
      {
        @delegate
      });
      _changesetsControl.GetType().GetProperty("SelectedItems", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(_changesetsControl, null);
      var control = _changesetsControl.GetType().GetField("changesetTreeGrid", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(_changesetsControl) as Control;
      control.GetType().GetProperty("SelectionMode", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).SetValue(control, SelectionMode.MultiSimple, null);
      _detailsButton = type.GetField("detailsButton", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(_dialog) as Button;
      _okButton = type.GetField("okButton", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(_dialog) as Button;
      _okButton.Visible = true;
      (type.GetField("cancelButton", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(_dialog) as Button).Text = "Cancel";
      var controlFindChangeset = type.GetField("controlFindChangeset", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(_dialog);
      settings?.Apply(controlFindChangeset);
    }

    public DialogSettings Settings => new DialogSettings(_dialog.GetType().GetField("controlFindChangeset", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(_dialog));

    private void OnListViewInvalidated(object sender, InvalidateEventArgs e)
    {
    }

    private void OnListViewItemSelectionChanged(
      object sender,
      ListViewItemSelectionChangedEventArgs e)
    {
    }

    public bool ShowDialog(IWin32Window window)
    {
        if (_selectedChangesets != null) _selectedChangesets.Clear();
        return _dialog.ShowDialog(window) == DialogResult.OK;
    }

    public void Dispose()
    {
      if (_dialog == null)
        return;
      _dialog.Dispose();
    }
  }
}
