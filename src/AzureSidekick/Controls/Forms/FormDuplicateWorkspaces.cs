// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.Forms.FormDuplicateWorkspaces
// Assembly: Attrice.TeamFoundation.Controls.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=1a24c887ab53aa6e
// MVID: 393BE899-3C29-4A70-86D7-B4B965E5A8F5
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controls.12.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Attrice.TeamFoundation.Common;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace Attrice.TeamFoundation.Controls.Forms
{
  public class FormDuplicateWorkspaces : Form
  {
    private Workspace _workspace;
    private DataTable _dtUsers;
    private DataTable _dtComputers;
    private IContainer components;
    private Panel panel1;
    private Button buttonCancel;
    private Button buttonDuplicate;
    private GroupBox groupBox2;
    private Label label6;
    private TextBox textBoxSourceUserName;
    private GroupBox groupBox1;
    private TextBox textBoxSourceComputer;
    private Label label2;
    private TextBox textBoxSourceName;
    private Label label1;
    private NewWorkspaceListView workspaceListView1;
    private ToolStrip toolStrip1;
    private ToolStripButton toolStripButtonDelete;

    public FormDuplicateWorkspaces()
    {
      InitializeComponent();
      toolStripButtonDelete.Enabled = false;
      workspaceListView1.SelectionPrompt = "Click here";
    }

    public Workspace Workspace
    {
      get => _workspace;
      set => _workspace = value;
    }

    public DataTable Users
    {
      get => _dtUsers;
      set
      {
        _dtUsers = value;
        workspaceListView1.UsersComboBox.DataSource = new DataView(_dtUsers)
        {
            Sort = ListTable.ColumnValue
        };
        workspaceListView1.UsersComboBox.ValueMember = ListTable.ColumnID;
        workspaceListView1.UsersComboBox.DisplayMember = ListTable.ColumnValue;
      }
    }

    internal WorkspaceProperties[] NewWorkspaces()
    {
      var workspacePropertiesList = new List<WorkspaceProperties>();
      foreach (ListViewItem listViewItem in workspaceListView1.Items)
      {
        if (!workspaceListView1.IsDefaultItem(listViewItem))
        {
          var text1 = listViewItem.SubItems[0].Text;
          var text2 = listViewItem.SubItems[1].Text;
          var text3 = listViewItem.SubItems[2].Text;
          var tableIdByValue = Utilities.GetTableIDByValue(_dtUsers, text2);
          workspacePropertiesList.Add(new WorkspaceProperties(text1, "", text2, tableIdByValue, text3));
        }
      }
      return workspacePropertiesList.ToArray();
    }

    public DataTable Computers
    {
      get => _dtComputers;
      set
      {
        _dtComputers = value;
        workspaceListView1.ComputersComboBox.DataSource = new DataView(_dtComputers)
        {
            Sort = ListTable.ColumnValue
        };
        workspaceListView1.ComputersComboBox.ValueMember = ListTable.ColumnID;
        workspaceListView1.ComputersComboBox.DisplayMember = ListTable.ColumnValue;
      }
    }

    private void workspaceListView1_SelectedIndexChanged(object sender, EventArgs e)
    {
      toolStripButtonDelete.Enabled = workspaceListView1.CanDelete;
    }

    private void buttonDuplicate_Click(object sender, EventArgs e)
    {
      if (workspaceListView1.Items.Count == 1)
      {
        var num1 = (int) MessageBox.Show("Please specify at least one new workspace", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        foreach (ListViewItem listViewItem in workspaceListView1.Items)
        {
          if (!workspaceListView1.IsDefaultItem(listViewItem))
          {
            if (listViewItem.SubItems[0].Text == "")
            {
              var num2 = (int) MessageBox.Show("Please specify name for each new workspace", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
              return;
            }
            if (listViewItem.SubItems[1].Text == "")
            {
              var num3 = (int) MessageBox.Show("Please specify user for each new workspace", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
              return;
            }
            if (listViewItem.SubItems[2].Text == "")
            {
              var num4 = (int) MessageBox.Show("Please specify computer for each new workspace", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
              return;
            }
          }
        }
        DialogResult = DialogResult.OK;
        Close();
      }
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      Close();
    }

    private void toolStripButtonDelete_Click(object sender, EventArgs e)
    {
      workspaceListView1.DeleteCurrent();
    }

    private void FormDuplicateWorkspaces_Shown(object sender, EventArgs e)
    {
      textBoxSourceName.Text = _workspace.Name;
      textBoxSourceComputer.Text = _workspace.Computer;
      textBoxSourceUserName.Text = Utilities.GetTableValueByID(_dtUsers, _workspace.OwnerName);
      workspaceListView1.DefaultItemText = _workspace.Name;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && components != null)
        components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      var componentResourceManager = new ComponentResourceManager(typeof (FormDuplicateWorkspaces));
      panel1 = new Panel();
      buttonCancel = new Button();
      buttonDuplicate = new Button();
      groupBox2 = new GroupBox();
      workspaceListView1 = new NewWorkspaceListView();
      toolStrip1 = new ToolStrip();
      toolStripButtonDelete = new ToolStripButton();
      label6 = new Label();
      textBoxSourceUserName = new TextBox();
      groupBox1 = new GroupBox();
      textBoxSourceComputer = new TextBox();
      label2 = new Label();
      textBoxSourceName = new TextBox();
      label1 = new Label();
      panel1.SuspendLayout();
      groupBox2.SuspendLayout();
      toolStrip1.SuspendLayout();
      groupBox1.SuspendLayout();
      SuspendLayout();
      panel1.Controls.Add(buttonCancel);
      panel1.Controls.Add(buttonDuplicate);
      panel1.Dock = DockStyle.Bottom;
      panel1.Location = new Point(0, 392);
      panel1.Name = "panel1";
      panel1.Size = new Size(539, 49);
      panel1.TabIndex = 5;
      buttonCancel.DialogResult = DialogResult.Cancel;
      buttonCancel.FlatStyle = FlatStyle.System;
      buttonCancel.Location = new Point(452, 14);
      buttonCancel.Name = "buttonCancel";
      buttonCancel.Size = new Size(75, 23);
      buttonCancel.TabIndex = 5;
      buttonCancel.Text = "Cancel";
      buttonCancel.UseVisualStyleBackColor = true;
      buttonCancel.Click += buttonCancel_Click;
      buttonDuplicate.FlatStyle = FlatStyle.System;
      buttonDuplicate.Location = new Point(371, 14);
      buttonDuplicate.Name = "buttonDuplicate";
      buttonDuplicate.Size = new Size(75, 23);
      buttonDuplicate.TabIndex = 4;
      buttonDuplicate.Text = "OK";
      buttonDuplicate.UseVisualStyleBackColor = true;
      buttonDuplicate.Click += buttonDuplicate_Click;
      groupBox2.Controls.Add(workspaceListView1);
      groupBox2.Controls.Add(toolStrip1);
      groupBox2.Dock = DockStyle.Fill;
      groupBox2.Location = new Point(0, 118);
      groupBox2.Name = "groupBox2";
      groupBox2.Size = new Size(539, 274);
      groupBox2.TabIndex = 6;
      groupBox2.TabStop = false;
      groupBox2.Text = " New (duplicated) workspaces";
      workspaceListView1.DefaultItemText = null;
      workspaceListView1.Dock = DockStyle.Fill;
      workspaceListView1.FullRowSelect = true;
      workspaceListView1.GridLines = true;
      workspaceListView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
      workspaceListView1.HideSelection = false;
      workspaceListView1.Location = new Point(3, 41);
      workspaceListView1.MultiSelect = false;
      workspaceListView1.Name = "workspaceListView1";
      workspaceListView1.OwnerDraw = true;
      workspaceListView1.SelectionPrompt = null;
      workspaceListView1.Size = new Size(533, 230);
      workspaceListView1.TabIndex = 0;
      workspaceListView1.UseCompatibleStateImageBehavior = false;
      workspaceListView1.View = View.Details;
      workspaceListView1.SelectedIndexChanged += workspaceListView1_SelectedIndexChanged;
      toolStrip1.AllowMerge = false;
      toolStrip1.CanOverflow = false;
      toolStrip1.GripMargin = new Padding(1);
      toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
      toolStrip1.Items.AddRange(new ToolStripItem[1]
      {
        toolStripButtonDelete
      });
      toolStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
      toolStrip1.Location = new Point(3, 16);
      toolStrip1.Name = "toolStrip1";
      toolStrip1.Padding = new Padding(3, 1, 1, 1);
      toolStrip1.RenderMode = ToolStripRenderMode.System;
      toolStrip1.Size = new Size(533, 25);
      toolStrip1.Stretch = true;
      toolStrip1.TabIndex = 3;
      toolStripButtonDelete.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonDelete.Image = (Image) componentResourceManager.GetObject("toolStripButtonDelete.Image");
      toolStripButtonDelete.ImageTransparentColor = Color.Magenta;
      toolStripButtonDelete.Name = "toolStripButtonDelete";
      toolStripButtonDelete.Size = new Size(23, 20);
      toolStripButtonDelete.Text = "toolStripButton1";
      toolStripButtonDelete.ToolTipText = "Delete working folder";
      toolStripButtonDelete.Click += toolStripButtonDelete_Click;
      label6.AutoSize = true;
      label6.Location = new Point(12, 81);
      label6.Name = "label6";
      label6.Size = new Size(55, 13);
      label6.TabIndex = 4;
      label6.Text = "Computer:";
      textBoxSourceUserName.Location = new Point(256, 52);
      textBoxSourceUserName.Name = "textBoxSourceUserName";
      textBoxSourceUserName.ReadOnly = true;
      textBoxSourceUserName.Size = new Size(271, 20);
      textBoxSourceUserName.TabIndex = 1000;
      textBoxSourceUserName.TabStop = false;
      groupBox1.Controls.Add(textBoxSourceComputer);
      groupBox1.Controls.Add(label6);
      groupBox1.Controls.Add(textBoxSourceUserName);
      groupBox1.Controls.Add(label2);
      groupBox1.Controls.Add(textBoxSourceName);
      groupBox1.Controls.Add(label1);
      groupBox1.Dock = DockStyle.Top;
      groupBox1.Location = new Point(0, 0);
      groupBox1.Name = "groupBox1";
      groupBox1.Size = new Size(539, 118);
      groupBox1.TabIndex = 4;
      groupBox1.TabStop = false;
      groupBox1.Text = " Source workspace ";
      textBoxSourceComputer.Location = new Point(256, 78);
      textBoxSourceComputer.Name = "textBoxSourceComputer";
      textBoxSourceComputer.ReadOnly = true;
      textBoxSourceComputer.Size = new Size(271, 20);
      textBoxSourceComputer.TabIndex = 1000;
      textBoxSourceComputer.TabStop = false;
      label2.AutoSize = true;
      label2.Location = new Point(12, 55);
      label2.Name = "label2";
      label2.Size = new Size(32, 13);
      label2.TabIndex = 2;
      label2.Text = "User:";
      textBoxSourceName.Location = new Point(256, 26);
      textBoxSourceName.Name = "textBoxSourceName";
      textBoxSourceName.ReadOnly = true;
      textBoxSourceName.Size = new Size(271, 20);
      textBoxSourceName.TabIndex = 1000;
      textBoxSourceName.TabStop = false;
      label1.AutoSize = true;
      label1.Location = new Point(12, 28);
      label1.Name = "label1";
      label1.Size = new Size(94, 13);
      label1.TabIndex = 0;
      label1.Text = "Workspace name:";
      AutoScaleDimensions = new SizeF(6f, 13f);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(539, 441);
      Controls.Add(groupBox2);
      Controls.Add(groupBox1);
      Controls.Add(panel1);
      FormBorderStyle = FormBorderStyle.FixedDialog;
      Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      Name = nameof (FormDuplicateWorkspaces);
      StartPosition = FormStartPosition.CenterScreen;
      Text = "Duplicate workspaces";
      Shown += FormDuplicateWorkspaces_Shown;
      panel1.ResumeLayout(false);
      groupBox2.ResumeLayout(false);
      groupBox2.PerformLayout();
      toolStrip1.ResumeLayout(false);
      toolStrip1.PerformLayout();
      groupBox1.ResumeLayout(false);
      groupBox1.PerformLayout();
      ResumeLayout(false);
    }
  }
}
