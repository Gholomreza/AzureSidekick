// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.CustomWIQLControl
// Assembly: Attrice.TeamFoundation.Controls.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=1a24c887ab53aa6e
// MVID: 393BE899-3C29-4A70-86D7-B4B965E5A8F5
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controls.12.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Attrice.TeamFoundation.Common;
using Attrice.TeamFoundation.Controllers;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace Attrice.TeamFoundation.Controls
{
  public class CustomWIQLControl : UserControl
  {
    private TfsController _controller;
    private IContainer components;
    private GroupBox groupBox3;
    private DateTimePicker dateTimePickerActivatedDate;
    private Label label11;
    private DateTimePicker dateTimePickerChangedDate;
    private Label label10;
    private DateTimePicker dateTimePickerClosedDate;
    private Label label9;
    private Label label8;
    private DateTimePicker dateTimePickerResolvedDate;
    private GroupBox groupBox2;
    private ComboBox comboBoxChangedBy;
    private ComboBox comboBoxAssignedTo;
    private Label label4;
    private Label label6;
    private ComboBox comboBoxResolvedBy;
    private ComboBox comboBoxClosedBy;
    private Label label5;
    private Label label7;
    private GroupBox groupBox1;
    private ComboBox comboBoxProject;
    private Label label1;
    private Label label2;
    private Label label3;
    private ComboBox comboBoxIteration;
    private ComboBox comboBoxArea;
    private GroupBox groupBox4;
    private TextBox textBoxIdTo;
    private TextBox textBoxIdFrom;
    private Label label13;
    private Label label12;

    public string WIQL
    {
      get
      {
        var wiql = string.Empty;
        if (!string.IsNullOrEmpty(comboBoxProject.Text))
          wiql += $"System.TeamProject = '{(object)comboBoxProject.Text}'";
        if (!string.IsNullOrEmpty(comboBoxArea.Text))
        {
          AddAndOperator(ref wiql);
          wiql += $"System.AreaPath = '{(object)comboBoxArea.Text}'";
        }
        if (!string.IsNullOrEmpty(comboBoxIteration.Text))
        {
          AddAndOperator(ref wiql);
          wiql += $"System.IterationPath = '{(object)comboBoxIteration.Text}'";
        }
        if (!string.IsNullOrEmpty(comboBoxAssignedTo.Text))
        {
          AddAndOperator(ref wiql);
          wiql += $"System.AssignedTo = '{(object)comboBoxAssignedTo.Text}'";
        }
        if (!string.IsNullOrEmpty(comboBoxChangedBy.Text))
        {
          AddAndOperator(ref wiql);
          wiql += $"System.ChangedBy = '{(object)comboBoxChangedBy.Text}'";
        }
        if (!string.IsNullOrEmpty(comboBoxClosedBy.Text))
        {
          AddAndOperator(ref wiql);
          wiql += $"Microsoft.VSTS.Common.ClosedBy = '{(object)comboBoxClosedBy.Text}'";
        }
        if (!string.IsNullOrEmpty(comboBoxResolvedBy.Text))
        {
          AddAndOperator(ref wiql);
          wiql += $"Microsoft.VSTS.Common.ResolvedBy = '{(object)comboBoxResolvedBy.Text}'";
        }
        if (dateTimePickerActivatedDate.Checked)
        {
          AddAndOperator(ref wiql);
          wiql += $"Microsoft.VSTS.Common.ActivatedDate = '{(object)dateTimePickerActivatedDate.Value.Date}'";
        }
        if (dateTimePickerChangedDate.Checked)
        {
          AddAndOperator(ref wiql);
          wiql += $"System.ChangedDate = '{(object)dateTimePickerChangedDate.Value.Date}'";
        }
        if (dateTimePickerClosedDate.Checked)
        {
          AddAndOperator(ref wiql);
          wiql += $"Microsoft.VSTS.Common.ClosedDate = '{(object)dateTimePickerClosedDate.Value.Date}'";
        }
        if (dateTimePickerResolvedDate.Checked)
        {
          AddAndOperator(ref wiql);
          wiql += $"Microsoft.VSTS.Common.ResolvedDate = '{(object)dateTimePickerResolvedDate.Value.Date}'";
        }
        if (!string.IsNullOrEmpty(textBoxIdFrom.Text))
        {
          AddAndOperator(ref wiql);
          wiql += $"Id >= {(object)textBoxIdFrom.Text}";
        }
        if (!string.IsNullOrEmpty(textBoxIdTo.Text))
        {
          AddAndOperator(ref wiql);
          wiql += $"Id <= {(object)textBoxIdTo.Text}";
        }
        if (!string.IsNullOrEmpty(wiql))
          wiql = $" where({(object)wiql})";
        wiql = $"Select Id,System.WorkItemType,State,System.AssignedTo,Title from Issue{(object)wiql} order by Id";
        return wiql;
      }
    }

    public void AddAndOperator(ref string wiql)
    {
      if (string.IsNullOrEmpty(wiql))
        return;
      wiql += " And ";
    }

    public CustomWIQLControl() => InitializeComponent();

    public void Initialize(TfsController controller)
    {
      _controller = controller;
      FillProjects();
    }

    public void LoadUsers()
    {
      var text1 = comboBoxAssignedTo.Text;
      var text2 = comboBoxChangedBy.Text;
      var text3 = comboBoxClosedBy.Text;
      var text4 = comboBoxResolvedBy.Text;
      var table = _controller.Users.UsersTable.Copy();
      ListTable.AddAllRow(table);
      ListTable.LoadTable(comboBoxAssignedTo, table, "");
      ListTable.LoadTable(comboBoxChangedBy, table, "");
      ListTable.LoadTable(comboBoxClosedBy, table, "");
      ListTable.LoadTable(comboBoxResolvedBy, table, "");
      if (!string.IsNullOrEmpty(text1))
        comboBoxAssignedTo.Text = text1;
      if (!string.IsNullOrEmpty(text2))
        comboBoxChangedBy.Text = text2;
      if (!string.IsNullOrEmpty(text3))
        comboBoxClosedBy.Text = text3;
      if (string.IsNullOrEmpty(text4))
        return;
      comboBoxResolvedBy.Text = text4;
    }

    private void FillProjects()
    {
      comboBoxProject.BeginUpdate();
      comboBoxProject.Items.Clear();
      foreach (Project project in _controller.WorkItemStore.Projects)
        comboBoxProject.Items.Add(project);
      comboBoxProject.EndUpdate();
    }

    private void comboBoxProject_Format(object sender, ListControlConvertEventArgs e)
    {
      var convertEventArgs = e;
      convertEventArgs.Value = (convertEventArgs.ListItem as Project).Name;
    }

    private void comboBoxProject_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (!(comboBoxProject.SelectedItem is Project selectedItem))
        return;
      FillAreas(selectedItem);
      FillIterations(selectedItem);
    }

    private void FillIterations(Project project)
    {
      comboBoxIteration.BeginUpdate();
      comboBoxIteration.Items.Clear();
      foreach (Node iterationRootNode in project.IterationRootNodes)
        comboBoxIteration.Items.Add(iterationRootNode.Path);
      comboBoxIteration.EndUpdate();
    }

    private void FillAreas(Project project)
    {
      comboBoxArea.BeginUpdate();
      comboBoxArea.Items.Clear();
      foreach (Node areaRootNode in project.AreaRootNodes)
        comboBoxArea.Items.Add(areaRootNode.Path);
      comboBoxArea.EndUpdate();
    }

    public void Clear()
    {
      comboBoxProject.SelectedItem = null;
      comboBoxArea.SelectedItem = null;
      comboBoxIteration.SelectedItem = null;
      comboBoxAssignedTo.SelectedItem = null;
      comboBoxChangedBy.SelectedItem = null;
      comboBoxClosedBy.SelectedItem = null;
      comboBoxResolvedBy.SelectedItem = null;
      var now = DateTime.Now;
      dateTimePickerActivatedDate.Checked = false;
      dateTimePickerActivatedDate.Value = now;
      dateTimePickerChangedDate.Checked = false;
      dateTimePickerChangedDate.Value = now;
      dateTimePickerClosedDate.Checked = false;
      dateTimePickerClosedDate.Value = now;
      dateTimePickerResolvedDate.Checked = false;
      dateTimePickerResolvedDate.Value = now;
      textBoxIdFrom.Text = string.Empty;
      textBoxIdTo.Text = string.Empty;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && components != null)
        components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      groupBox3 = new GroupBox();
      dateTimePickerActivatedDate = new DateTimePicker();
      label11 = new Label();
      dateTimePickerChangedDate = new DateTimePicker();
      label10 = new Label();
      dateTimePickerClosedDate = new DateTimePicker();
      label9 = new Label();
      label8 = new Label();
      dateTimePickerResolvedDate = new DateTimePicker();
      groupBox2 = new GroupBox();
      comboBoxChangedBy = new ComboBox();
      comboBoxAssignedTo = new ComboBox();
      label4 = new Label();
      label6 = new Label();
      comboBoxResolvedBy = new ComboBox();
      comboBoxClosedBy = new ComboBox();
      label5 = new Label();
      label7 = new Label();
      groupBox1 = new GroupBox();
      comboBoxIteration = new ComboBox();
      comboBoxArea = new ComboBox();
      comboBoxProject = new ComboBox();
      label1 = new Label();
      label2 = new Label();
      label3 = new Label();
      groupBox4 = new GroupBox();
      label12 = new Label();
      label13 = new Label();
      textBoxIdFrom = new TextBox();
      textBoxIdTo = new TextBox();
      groupBox3.SuspendLayout();
      groupBox2.SuspendLayout();
      groupBox1.SuspendLayout();
      groupBox4.SuspendLayout();
      SuspendLayout();
      groupBox3.Controls.Add(dateTimePickerActivatedDate);
      groupBox3.Controls.Add(label11);
      groupBox3.Controls.Add(dateTimePickerChangedDate);
      groupBox3.Controls.Add(label10);
      groupBox3.Controls.Add(dateTimePickerClosedDate);
      groupBox3.Controls.Add(label9);
      groupBox3.Controls.Add(label8);
      groupBox3.Controls.Add(dateTimePickerResolvedDate);
      groupBox3.Location = new Point(3, 177);
      groupBox3.Name = "groupBox3";
      groupBox3.Size = new Size(694, 74);
      groupBox3.TabIndex = 28;
      groupBox3.TabStop = false;
      groupBox3.Text = "Date";
      dateTimePickerActivatedDate.Checked = false;
      dateTimePickerActivatedDate.Location = new Point(80, 19);
      dateTimePickerActivatedDate.Name = "dateTimePickerActivatedDate";
      dateTimePickerActivatedDate.ShowCheckBox = true;
      dateTimePickerActivatedDate.Size = new Size(259, 20);
      dateTimePickerActivatedDate.TabIndex = 23;
      dateTimePickerActivatedDate.Value = new DateTime(2008, 2, 26, 0, 0, 0, 0);
      label11.AutoSize = true;
      label11.Location = new Point(11, 23);
      label11.Name = "label11";
      label11.Size = new Size(52, 13);
      label11.TabIndex = 24;
      label11.Text = "Activated";
      dateTimePickerChangedDate.Checked = false;
      dateTimePickerChangedDate.Location = new Point(80, 45);
      dateTimePickerChangedDate.Name = "dateTimePickerChangedDate";
      dateTimePickerChangedDate.ShowCheckBox = true;
      dateTimePickerChangedDate.Size = new Size(259, 20);
      dateTimePickerChangedDate.TabIndex = 21;
      label10.AutoSize = true;
      label10.Location = new Point(11, 49);
      label10.Name = "label10";
      label10.Size = new Size(50, 13);
      label10.TabIndex = 22;
      label10.Text = "Changed";
      dateTimePickerClosedDate.Checked = false;
      dateTimePickerClosedDate.Location = new Point(418, 19);
      dateTimePickerClosedDate.Name = "dateTimePickerClosedDate";
      dateTimePickerClosedDate.ShowCheckBox = true;
      dateTimePickerClosedDate.Size = new Size(259, 20);
      dateTimePickerClosedDate.TabIndex = 17;
      label9.AutoSize = true;
      label9.Location = new Point(346, 49);
      label9.Name = "label9";
      label9.Size = new Size(52, 13);
      label9.TabIndex = 20;
      label9.Text = "Resolved";
      label8.AutoSize = true;
      label8.Location = new Point(346, 23);
      label8.Name = "label8";
      label8.Size = new Size(39, 13);
      label8.TabIndex = 18;
      label8.Text = "Closed";
      dateTimePickerResolvedDate.Checked = false;
      dateTimePickerResolvedDate.Location = new Point(418, 45);
      dateTimePickerResolvedDate.Name = "dateTimePickerResolvedDate";
      dateTimePickerResolvedDate.ShowCheckBox = true;
      dateTimePickerResolvedDate.Size = new Size(259, 20);
      dateTimePickerResolvedDate.TabIndex = 19;
      groupBox2.Controls.Add(comboBoxChangedBy);
      groupBox2.Controls.Add(comboBoxAssignedTo);
      groupBox2.Controls.Add(label4);
      groupBox2.Controls.Add(label6);
      groupBox2.Controls.Add(comboBoxResolvedBy);
      groupBox2.Controls.Add(comboBoxClosedBy);
      groupBox2.Controls.Add(label5);
      groupBox2.Controls.Add(label7);
      groupBox2.Location = new Point(3, 88);
      groupBox2.Name = "groupBox2";
      groupBox2.Size = new Size(694, 83);
      groupBox2.TabIndex = 27;
      groupBox2.TabStop = false;
      groupBox2.Text = "User";
      comboBoxChangedBy.FormattingEnabled = true;
      comboBoxChangedBy.Location = new Point(80, 49);
      comboBoxChangedBy.Name = "comboBoxChangedBy";
      comboBoxChangedBy.Size = new Size(259, 21);
      comboBoxChangedBy.TabIndex = 13;
      comboBoxAssignedTo.FormattingEnabled = true;
      comboBoxAssignedTo.Location = new Point(80, 22);
      comboBoxAssignedTo.Name = "comboBoxAssignedTo";
      comboBoxAssignedTo.Size = new Size(259, 21);
      comboBoxAssignedTo.TabIndex = 9;
      label4.AutoSize = true;
      label4.Location = new Point(8, 25);
      label4.Name = "label4";
      label4.Size = new Size(66, 13);
      label4.TabIndex = 10;
      label4.Text = "Assigned To";
      label6.AutoSize = true;
      label6.Location = new Point(8, 52);
      label6.Name = "label6";
      label6.Size = new Size(65, 13);
      label6.TabIndex = 14;
      label6.Text = "Changed By";
      comboBoxResolvedBy.FormattingEnabled = true;
      comboBoxResolvedBy.Location = new Point(418, 49);
      comboBoxResolvedBy.Name = "comboBoxResolvedBy";
      comboBoxResolvedBy.Size = new Size(259, 21);
      comboBoxResolvedBy.TabIndex = 15;
      comboBoxClosedBy.FormattingEnabled = true;
      comboBoxClosedBy.Location = new Point(418, 22);
      comboBoxClosedBy.Name = "comboBoxClosedBy";
      comboBoxClosedBy.Size = new Size(259, 21);
      comboBoxClosedBy.TabIndex = 11;
      label5.AutoSize = true;
      label5.Location = new Point(346, 25);
      label5.Name = "label5";
      label5.Size = new Size(54, 13);
      label5.TabIndex = 12;
      label5.Text = "Closed By";
      label7.AutoSize = true;
      label7.Location = new Point(346, 52);
      label7.Name = "label7";
      label7.Size = new Size(67, 13);
      label7.TabIndex = 16;
      label7.Text = "Resolved By";
      groupBox1.Controls.Add(comboBoxIteration);
      groupBox1.Controls.Add(comboBoxArea);
      groupBox1.Controls.Add(comboBoxProject);
      groupBox1.Controls.Add(label1);
      groupBox1.Controls.Add(label2);
      groupBox1.Controls.Add(label3);
      groupBox1.Location = new Point(3, 3);
      groupBox1.Name = "groupBox1";
      groupBox1.Size = new Size(694, 79);
      groupBox1.TabIndex = 26;
      groupBox1.TabStop = false;
      groupBox1.Text = "Project";
      comboBoxIteration.FormattingEnabled = true;
      comboBoxIteration.Location = new Point(418, 46);
      comboBoxIteration.Name = "comboBoxIteration";
      comboBoxIteration.Size = new Size(259, 21);
      comboBoxIteration.TabIndex = 10;
      comboBoxArea.FormattingEnabled = true;
      comboBoxArea.Location = new Point(80, 46);
      comboBoxArea.Name = "comboBoxArea";
      comboBoxArea.Size = new Size(259, 21);
      comboBoxArea.TabIndex = 9;
      comboBoxProject.FormattingEnabled = true;
      comboBoxProject.Location = new Point(80, 19);
      comboBoxProject.Name = "comboBoxProject";
      comboBoxProject.Size = new Size(259, 21);
      comboBoxProject.TabIndex = 3;
      comboBoxProject.SelectedIndexChanged += comboBoxProject_SelectedIndexChanged;
      comboBoxProject.Format += comboBoxProject_Format;
      label1.AutoSize = true;
      label1.Location = new Point(8, 22);
      label1.Name = "label1";
      label1.Size = new Size(40, 13);
      label1.TabIndex = 5;
      label1.Text = "Project";
      label2.AutoSize = true;
      label2.Location = new Point(8, 49);
      label2.Name = "label2";
      label2.Size = new Size(29, 13);
      label2.TabIndex = 6;
      label2.Text = "Area";
      label3.AutoSize = true;
      label3.Location = new Point(346, 49);
      label3.Name = "label3";
      label3.Size = new Size(45, 13);
      label3.TabIndex = 8;
      label3.Text = "Iteration";
      groupBox4.Controls.Add(textBoxIdTo);
      groupBox4.Controls.Add(textBoxIdFrom);
      groupBox4.Controls.Add(label13);
      groupBox4.Controls.Add(label12);
      groupBox4.Location = new Point(3, 257);
      groupBox4.Name = "groupBox4";
      groupBox4.Size = new Size(694, 50);
      groupBox4.TabIndex = 29;
      groupBox4.TabStop = false;
      groupBox4.Text = "ID Range";
      label12.AutoSize = true;
      label12.Location = new Point(346, 22);
      label12.Name = "label12";
      label12.Size = new Size(20, 13);
      label12.TabIndex = 6;
      label12.Text = "To";
      label13.AutoSize = true;
      label13.Location = new Point(11, 22);
      label13.Name = "label13";
      label13.Size = new Size(30, 13);
      label13.TabIndex = 7;
      label13.Text = "From";
      textBoxIdFrom.Location = new Point(80, 19);
      textBoxIdFrom.Name = "textBoxIdFrom";
      textBoxIdFrom.Size = new Size(259, 20);
      textBoxIdFrom.TabIndex = 8;
      textBoxIdTo.Location = new Point(418, 19);
      textBoxIdTo.Name = "textBoxIdTo";
      textBoxIdTo.Size = new Size(259, 20);
      textBoxIdTo.TabIndex = 9;
      AutoScaleDimensions = new SizeF(6f, 13f);
      AutoScaleMode = AutoScaleMode.Font;
      Controls.Add(groupBox4);
      Controls.Add(groupBox3);
      Controls.Add(groupBox2);
      Controls.Add(groupBox1);
      Name = nameof (CustomWIQLControl);
      Size = new Size(701, 312);
      groupBox3.ResumeLayout(false);
      groupBox3.PerformLayout();
      groupBox2.ResumeLayout(false);
      groupBox2.PerformLayout();
      groupBox1.ResumeLayout(false);
      groupBox1.PerformLayout();
      groupBox4.ResumeLayout(false);
      groupBox4.PerformLayout();
      ResumeLayout(false);
    }
  }
}
