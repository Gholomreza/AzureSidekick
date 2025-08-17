// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Common.DialogSettings
// Assembly: Attrice.TeamFoundation.Common.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=35acb2d39e045fbd
// MVID: A152C2C1-BE36-402B-9F4C-D577D02612BB
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Common.12.dll

using System;
using System.Reflection;
using System.Windows.Forms;

namespace Attrice.TeamFoundation.Common
{
  public class DialogSettings
  {
    public string FileEditBox;
    public string FromEditBox;
    public string ToEditBox;
    public bool DateRadioButton;
    public bool NumberRadioButton;
    public bool AllChanges;
    public DateTime StartDatePicker;
    public DateTime EndDatePicker;
    public string UserEditBox;

    public void Apply(object controlFindChangeset)
    {
      (controlFindChangeset.GetType().GetField("allChanges", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(controlFindChangeset) as RadioButton).Checked = AllChanges;
      (controlFindChangeset.GetType().GetField("dateRadioButton", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(controlFindChangeset) as RadioButton).Checked = DateRadioButton;
      (controlFindChangeset.GetType().GetField("numberRadioButton", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(controlFindChangeset) as RadioButton).Checked = NumberRadioButton;
      (controlFindChangeset.GetType().GetField("userComboBox", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(controlFindChangeset) as ComboBox).Text = UserEditBox;
      (controlFindChangeset.GetType().GetField("fileEditBox", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(controlFindChangeset) as TextBox).Text = FileEditBox;
      (controlFindChangeset.GetType().GetField("fromEditBox", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(controlFindChangeset) as TextBox).Text = FromEditBox;
      (controlFindChangeset.GetType().GetField("toEditBox", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(controlFindChangeset) as TextBox).Text = ToEditBox;
      (controlFindChangeset.GetType().GetField("startDatePicker", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(controlFindChangeset) as DateTimePicker).Value = StartDatePicker;
      (controlFindChangeset.GetType().GetField("endDatePicker", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(controlFindChangeset) as DateTimePicker).Value = EndDatePicker;
    }

    public DialogSettings(object controlFindChangeset)
    {
      AllChanges = (controlFindChangeset.GetType().GetField("allChanges", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(controlFindChangeset) as RadioButton).Checked;
      DateRadioButton = (controlFindChangeset.GetType().GetField("dateRadioButton", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(controlFindChangeset) as RadioButton).Checked;
      NumberRadioButton = (controlFindChangeset.GetType().GetField("numberRadioButton", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(controlFindChangeset) as RadioButton).Checked;
      UserEditBox = (controlFindChangeset.GetType().GetField("userComboBox", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(controlFindChangeset) as ComboBox).Text;
      FileEditBox = (controlFindChangeset.GetType().GetField("fileEditBox", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(controlFindChangeset) as TextBox).Text;
      FromEditBox = (controlFindChangeset.GetType().GetField("fromEditBox", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(controlFindChangeset) as TextBox).Text;
      ToEditBox = (controlFindChangeset.GetType().GetField("toEditBox", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(controlFindChangeset) as TextBox).Text;
      StartDatePicker = (controlFindChangeset.GetType().GetField("startDatePicker", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(controlFindChangeset) as DateTimePicker).Value;
      EndDatePicker = (controlFindChangeset.GetType().GetField("endDatePicker", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(controlFindChangeset) as DateTimePicker).Value;
    }
  }
}
