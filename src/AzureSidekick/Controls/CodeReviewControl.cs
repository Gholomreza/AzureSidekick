// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.CodeReviewControl
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
using Attrice.TeamFoundation.Controllers;
using Attrice.TeamFoundation.Controls.Forms;
using Attrice.TeamFoundation.Controls.Properties;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.VersionControl.Common;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace Attrice.TeamFoundation.Controls
{
  public class CodeReviewControl : BaseSidekickControl
  {
    private IContainer components;
    private SplitContainer splitContainer1;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem compareWithLatestToolStripMenuItem;
    private ToolStripMenuItem compareVersionsToolStripMenuItem;
    private ToolStrip toolStrip2;
    private ToolStripButton toolStripButtonSelectChangesets;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripButton toolStripButtonReviewMode;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripButton toolStripButtonSaveChangesetsAndWIs;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripButton toolStripButtonFromWorkItems;
    private ToolStripButton toolStripButtonRemove;
    private TabControl tabControl;
    private TabPage tabPageFiles;
    private TabPage tabPageChangesetsAndWIs;
    private ContextMenuStrip contextMenuStripChangeset;
    private ToolStripMenuItem viewChangesetDetailsToolStripMenuItem;
    private ToolStripMenuItem viewChangesetDetailsToolStripMenuItem1;
    private ToolStrip toolStrip1;
    private ToolStripButton toolStripButtonComparePrevious;
    private ToolStripButton toolStripButtonCompareVersions;
    private ToolStripButton toolStripButtonViewChangesetDetails;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripButton toolStripButtonSaveToFile;
    private ListView listViewFiles;
    private ColumnHeader columnHeader1;
    private ColumnHeader columnHeader2;
    private ColumnHeader columnHeader3;
    private ColumnHeader columnHeader4;
    private ColumnHeader columnHeader5;
    private GroupBox groupBoxFilter;
    private Button buttonFilter;
    private ComboBox comboBoxFiles;
    private ComboBox comboBoxUser;
    private Label labelUser;
    private Label labeFileName;
    private ListView listViewWIsByChangesets;
    private ColumnHeader columnHeaderWorkItemId;
    private ColumnHeader columnHeaderWorkItemType;
    private ColumnHeader columnHeaderWorkItemState;
    private ColumnHeader columnHeaderAssignedTo;
    private ColumnHeader columnHeaderChangedBy;
    private ColumnHeader columnHeaderWorkItemTitle;
    private ListView listViewChangesetsByWI;
    private ColumnHeader columnHeader12;
    private ColumnHeader columnHeader13;
    private ColumnHeader columnHeader14;
    private ColumnHeader columnHeader15;
    private ToolStrip toolStrip3;
    private ToolStripButton toolStripButtonSaveAssociatedWIsAndChangesets;
    private ToolStripButton toolStripButton1;
    private ToolStripButton toolStripButton2;
    private ToolStripButton toolStripButton3;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripButton toolStripButton4;
    private GroupBox groupBoxSelectedChangesetsAndWIs;
    private ListView listViewChangesets;
    private ColumnHeader columnHeaderID;
    private ColumnHeader columnHeaderDate;
    private ColumnHeader columnHeaderUser;
    private ColumnHeader columnHeaderComment;
    private ListView listViewWorkItems;
    private ColumnHeader columnHeader6;
    private ColumnHeader columnHeader7;
    private ColumnHeader columnHeader8;
    private ColumnHeader columnHeader9;
    private ColumnHeader columnHeader10;
    private ColumnHeader columnHeader11;
    private ToolStripButton toolStripButtonCompareWithNewest;
    private ToolStripButton toolStripButtonCompareWithOldest;
    private ToolStripMenuItem compareWithNewestToolStripMenuItem;
    private ToolStripMenuItem compareWithOldestToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator7;
    private ToolStripSeparator toolStripSeparator8;
    private ToolStripButton toolStripButtonChangesetDetails;
    private FlowLayoutPanel flowLayoutPanel1;
    private ColumnHeader columnHeaderWI;
    private ToolStripMenuItem compareWithBeforeViewToolStripMenuItem;
    private ToolStripSeparator toolStripSeparatorChangeset;
    private ColumnHeader columnHeader16;
    private ColumnHeader columnHeaderPolicyViolation;
    private CodeReviewController _controller;
    private DataTable _dtUsers;
    private FormSelectWorkItems _formWorkItems;
    private DialogSettings _changesetSettings;

    public CodeReviewControl()
    {
      InitializeComponent();
      toolStripButtonComparePrevious.Enabled = toolStripButtonCompareVersions.Enabled = false;
      toolStripButtonCompareWithOldest.Enabled = toolStripButtonCompareWithNewest.Enabled = false;
      toolStripButtonSaveToFile.Enabled = false;
      toolStripButtonReviewMode.Enabled = false;
      toolStripButtonSaveChangesetsAndWIs.Enabled = false;
      splitContainer1.BackColor = SystemColors.Control;
      listViewWIsByChangesets.Visible = true;
      listViewChangesetsByWI.Visible = false;
      listViewWorkItems.Visible = false;
      toolStripButtonRemove.Enabled = false;
      toolStripButtonChangesetDetails.Visible = false;
      toolStripButtonChangesetDetails.Enabled = false;
      toolStripButtonViewChangesetDetails.Enabled = false;
      Name = "Code Review Sidekick";
      comboBoxUser.Tag = comboBoxFiles.Tag = string.Empty;
    }

    public override Image Image => Resources.CodeReviewImage;

    private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
    {
      e.Cancel = !RefreshVersionCompare();
    }

    private void buttonSelectChangesets_Click(object sender, EventArgs e)
    {
      SelectChangesetsForReview();
    }

    private void listViewChangesets_SelectedIndexChanged(object sender, EventArgs e)
    {
      ChangesetSelected();
    }

    private void compareWithLatestToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CompareWithPreviousVersion();
    }

    private void compareVersionsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CompareTwoVersions();
    }

    private void toolStripButtonCompareVersions_Click(object sender, EventArgs e)
    {
      CompareTwoVersions();
    }

    private void listViewFiles_SelectedIndexChanged(object sender, EventArgs e)
    {
      RefreshVersionCompare();
    }

    private void toolStripButtonSaveToFile_Click(object sender, EventArgs e)
    {
      Utilities.SaveListViewToFile(saveFileDialog, listViewFiles, "List of items for code review");
    }

    private void toolStripButtonReviewMode_Click(object sender, EventArgs e)
    {
      SwitchReviewMode();
    }

    private void toolStripButtonSaveChangesetsAndWIs_Click(object sender, EventArgs e)
    {
      Utilities.SaveListViewToFile(saveFileDialog, listViewWorkItems.Visible ? listViewWorkItems : listViewChangesets, listViewWorkItems.Visible ? "List of Work Items for code review" : "List of changesets for code review");
    }

    private void toolStripButtonComparePrevious_Click(object sender, EventArgs e)
    {
      CompareWithPreviousVersion();
    }

    private void toolStripButtonFromWorkItems_Click(object sender, EventArgs e)
    {
      SelectChangesetFromWorkItems();
    }

    private void comboBoxUser_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (string.Compare(comboBoxUser.Tag as string, comboBoxUser.Text) == 0)
        return;
      comboBoxUser.Tag = comboBoxUser.Text;
      ReviewChangesets(!(listViewChangesets.Visible ? listViewChangesets : (Control) listViewWorkItems).Enabled);
    }

    private void comboBoxFiles_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyData != Keys.Return)
        return;
      ReviewChangesets(!(listViewChangesets.Visible ? listViewChangesets : (Control) listViewWorkItems).Enabled);
    }

    private void comboBoxUser_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyData != Keys.Return)
        return;
      ReviewChangesets(!(listViewChangesets.Visible ? listViewChangesets : (Control) listViewWorkItems).Enabled);
    }

    private void listViewWorkItems_SelectedIndexChanged(object sender, EventArgs e)
    {
      WorkItemSelected();
    }

    private void toolStripButtonRemove_Click(object sender, EventArgs e)
    {
      RemoveSelected(listViewChangesets.Visible ? listViewChangesets : listViewWorkItems);
    }

    private void comboBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (string.Compare(comboBoxFiles.Tag as string, comboBoxFiles.Text) == 0)
        return;
      comboBoxFiles.Tag = comboBoxFiles.Text;
      ReviewChangesets(!(listViewChangesets.Visible ? listViewChangesets : (Control) listViewWorkItems).Enabled);
    }

    private void viewChangesetDetailsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ViewSelectedChangesetDetails();
    }

    private void contextMenuStripChangeset_Opening(object sender, CancelEventArgs e)
    {
      e.Cancel = CancelViewChangesets();
    }

    private void viewChangesetDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      ViewAssociatedChangesetDetails();
    }

    private void toolStripButtonViewChangesetDetails_Click(object sender, EventArgs e)
    {
      ViewAssociatedChangesetDetails();
    }

    private void buttonFilter_Click(object sender, EventArgs e)
    {
      ReviewChangesets(!(listViewChangesets.Visible ? listViewChangesets : (Control) listViewWorkItems).Enabled);
    }

    private void toolStripButtonSaveAssociatedWIsAndChangesets_Click(object sender, EventArgs e)
    {
      Utilities.SaveListViewToFile(saveFileDialog, listViewChangesetsByWI.Visible ? listViewChangesetsByWI : listViewWIsByChangesets, listViewChangesetsByWI.Visible ? "List of associated changesets for code review" : "List of associated Work Items for code review");
    }

    private void compareWithNewestToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CompareWithNewestVersion();
    }

    private void compareWithOldestToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CompareWithOldestVersion();
    }

    private void toolStripButtonCompareWithNewest_Click(object sender, EventArgs e)
    {
      CompareWithNewestVersion();
    }

    private void toolStripButtonCompareWithOldest_Click(object sender, EventArgs e)
    {
      CompareWithOldestVersion();
    }

    private void toolStripButtonChangesetDetails_Click(object sender, EventArgs e)
    {
      ViewSelectedChangesetDetails();
    }

    private void listViewChangesetsByWI_SelectedIndexChanged(object sender, EventArgs e)
    {
      toolStripButtonChangesetDetails.Enabled = listViewChangesetsByWI.SelectedIndices.Count == 1;
    }

    private void compareWithBeforeViewToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CompareWithBeforeViewVersion();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && components != null)
        components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      components = new Container();
      var componentResourceManager = new ComponentResourceManager(typeof (CodeReviewControl));
      splitContainer1 = new SplitContainer();
      groupBoxSelectedChangesetsAndWIs = new GroupBox();
      listViewChangesets = new ListView();
      columnHeaderWI = new ColumnHeader();
      columnHeaderID = new ColumnHeader();
      columnHeaderDate = new ColumnHeader();
      columnHeaderUser = new ColumnHeader();
      columnHeaderComment = new ColumnHeader();
      columnHeaderPolicyViolation = new ColumnHeader();
      contextMenuStripChangeset = new ContextMenuStrip(components);
      viewChangesetDetailsToolStripMenuItem = new ToolStripMenuItem();
      listViewWorkItems = new ListView();
      columnHeader6 = new ColumnHeader();
      columnHeader7 = new ColumnHeader();
      columnHeader8 = new ColumnHeader();
      columnHeader9 = new ColumnHeader();
      columnHeader10 = new ColumnHeader();
      columnHeader11 = new ColumnHeader();
      toolStrip2 = new ToolStrip();
      toolStripButtonSelectChangesets = new ToolStripButton();
      toolStripSeparator4 = new ToolStripSeparator();
      toolStripButtonFromWorkItems = new ToolStripButton();
      toolStripSeparator1 = new ToolStripSeparator();
      toolStripButtonReviewMode = new ToolStripButton();
      toolStripButtonRemove = new ToolStripButton();
      toolStripSeparator2 = new ToolStripSeparator();
      toolStripButtonSaveChangesetsAndWIs = new ToolStripButton();
      tabControl = new TabControl();
      tabPageFiles = new TabPage();
      listViewFiles = new ListView();
      columnHeader1 = new ColumnHeader();
      columnHeader2 = new ColumnHeader();
      columnHeader3 = new ColumnHeader();
      columnHeader4 = new ColumnHeader();
      columnHeader5 = new ColumnHeader();
      contextMenuStrip1 = new ContextMenuStrip(components);
      compareWithBeforeViewToolStripMenuItem = new ToolStripMenuItem();
      compareWithLatestToolStripMenuItem = new ToolStripMenuItem();
      compareVersionsToolStripMenuItem = new ToolStripMenuItem();
      compareWithNewestToolStripMenuItem = new ToolStripMenuItem();
      compareWithOldestToolStripMenuItem = new ToolStripMenuItem();
      toolStripSeparatorChangeset = new ToolStripSeparator();
      viewChangesetDetailsToolStripMenuItem1 = new ToolStripMenuItem();
      groupBoxFilter = new GroupBox();
      flowLayoutPanel1 = new FlowLayoutPanel();
      labelUser = new Label();
      comboBoxUser = new ComboBox();
      labeFileName = new Label();
      comboBoxFiles = new ComboBox();
      buttonFilter = new Button();
      toolStrip1 = new ToolStrip();
      toolStripButtonComparePrevious = new ToolStripButton();
      toolStripButtonCompareVersions = new ToolStripButton();
      toolStripSeparator7 = new ToolStripSeparator();
      toolStripButtonCompareWithNewest = new ToolStripButton();
      toolStripButtonCompareWithOldest = new ToolStripButton();
      toolStripSeparator8 = new ToolStripSeparator();
      toolStripButtonViewChangesetDetails = new ToolStripButton();
      toolStripSeparator3 = new ToolStripSeparator();
      toolStripButtonSaveToFile = new ToolStripButton();
      tabPageChangesetsAndWIs = new TabPage();
      listViewWIsByChangesets = new ListView();
      columnHeaderWorkItemId = new ColumnHeader();
      columnHeaderWorkItemType = new ColumnHeader();
      columnHeaderWorkItemState = new ColumnHeader();
      columnHeaderAssignedTo = new ColumnHeader();
      columnHeaderChangedBy = new ColumnHeader();
      columnHeaderWorkItemTitle = new ColumnHeader();
      listViewChangesetsByWI = new ListView();
      columnHeader12 = new ColumnHeader();
      columnHeader13 = new ColumnHeader();
      columnHeader14 = new ColumnHeader();
      columnHeader15 = new ColumnHeader();
      columnHeader16 = new ColumnHeader();
      toolStrip3 = new ToolStrip();
      toolStripButtonChangesetDetails = new ToolStripButton();
      toolStripButtonSaveAssociatedWIsAndChangesets = new ToolStripButton();
      toolStripButton1 = new ToolStripButton();
      toolStripButton2 = new ToolStripButton();
      toolStripButton3 = new ToolStripButton();
      toolStripSeparator5 = new ToolStripSeparator();
      toolStripButton4 = new ToolStripButton();
      splitContainer1.Panel1.SuspendLayout();
      splitContainer1.Panel2.SuspendLayout();
      splitContainer1.SuspendLayout();
      groupBoxSelectedChangesetsAndWIs.SuspendLayout();
      contextMenuStripChangeset.SuspendLayout();
      toolStrip2.SuspendLayout();
      tabControl.SuspendLayout();
      tabPageFiles.SuspendLayout();
      contextMenuStrip1.SuspendLayout();
      groupBoxFilter.SuspendLayout();
      flowLayoutPanel1.SuspendLayout();
      toolStrip1.SuspendLayout();
      tabPageChangesetsAndWIs.SuspendLayout();
      toolStrip3.SuspendLayout();
      SuspendLayout();
      splitContainer1.BorderStyle = BorderStyle.Fixed3D;
      splitContainer1.Dock = DockStyle.Fill;
      splitContainer1.Location = new Point(0, 0);
      splitContainer1.Name = "splitContainer1";
      splitContainer1.Panel1.Controls.Add(groupBoxSelectedChangesetsAndWIs);
      splitContainer1.Panel1.Controls.Add(toolStrip2);
      splitContainer1.Panel2.Controls.Add(tabControl);
      splitContainer1.Size = new Size(751, 486);
      splitContainer1.SplitterDistance = 271;
      splitContainer1.SplitterWidth = 2;
      splitContainer1.TabIndex = 0;
      groupBoxSelectedChangesetsAndWIs.Controls.Add(listViewChangesets);
      groupBoxSelectedChangesetsAndWIs.Controls.Add(listViewWorkItems);
      groupBoxSelectedChangesetsAndWIs.Dock = DockStyle.Fill;
      groupBoxSelectedChangesetsAndWIs.Location = new Point(0, 25);
      groupBoxSelectedChangesetsAndWIs.Name = "groupBoxSelectedChangesetsAndWIs";
      groupBoxSelectedChangesetsAndWIs.Size = new Size(267, 457);
      groupBoxSelectedChangesetsAndWIs.TabIndex = 7;
      groupBoxSelectedChangesetsAndWIs.TabStop = false;
      groupBoxSelectedChangesetsAndWIs.Text = "Changesets";
      listViewChangesets.BorderStyle = BorderStyle.None;
      listViewChangesets.Columns.AddRange(new ColumnHeader[6]
      {
        columnHeaderWI,
        columnHeaderID,
        columnHeaderDate,
        columnHeaderUser,
        columnHeaderComment,
        columnHeaderPolicyViolation
      });
      listViewChangesets.ContextMenuStrip = contextMenuStripChangeset;
      listViewChangesets.Dock = DockStyle.Fill;
      listViewChangesets.FullRowSelect = true;
      listViewChangesets.HeaderStyle = ColumnHeaderStyle.Nonclickable;
      listViewChangesets.HideSelection = false;
      listViewChangesets.Location = new Point(3, 16);
      listViewChangesets.Name = "listViewChangesets";
      listViewChangesets.Size = new Size(261, 438);
      listViewChangesets.TabIndex = 2;
      listViewChangesets.UseCompatibleStateImageBehavior = false;
      listViewChangesets.View = View.Details;
      listViewChangesets.SelectedIndexChanged += listViewChangesets_SelectedIndexChanged;
      columnHeaderWI.Text = "WI";
      columnHeaderWI.Width = 30;
      columnHeaderID.Text = "Changeset";
      columnHeaderID.Width = 70;
      columnHeaderDate.Text = "Date";
      columnHeaderDate.Width = 104;
      columnHeaderUser.Text = "User";
      columnHeaderUser.Width = 90;
      columnHeaderComment.Text = "Comment";
      columnHeaderComment.Width = 178;
      columnHeaderPolicyViolation.Text = "Policy Violation";
      contextMenuStripChangeset.Items.AddRange(new ToolStripItem[1]
      {
        viewChangesetDetailsToolStripMenuItem
      });
      contextMenuStripChangeset.Name = "contextMenuStripChangeset";
      contextMenuStripChangeset.Size = new Size(187, 26);
      contextMenuStripChangeset.Opening += contextMenuStripChangeset_Opening;
      viewChangesetDetailsToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("viewChangesetDetailsToolStripMenuItem.Image");
      viewChangesetDetailsToolStripMenuItem.Name = "viewChangesetDetailsToolStripMenuItem";
      viewChangesetDetailsToolStripMenuItem.Size = new Size(186, 22);
      viewChangesetDetailsToolStripMenuItem.Text = "View Changeset Details";
      viewChangesetDetailsToolStripMenuItem.Click += viewChangesetDetailsToolStripMenuItem_Click;
      listViewWorkItems.BorderStyle = BorderStyle.None;
      listViewWorkItems.Columns.AddRange(new ColumnHeader[6]
      {
        columnHeader6,
        columnHeader7,
        columnHeader8,
        columnHeader9,
        columnHeader10,
        columnHeader11
      });
      listViewWorkItems.Dock = DockStyle.Fill;
      listViewWorkItems.FullRowSelect = true;
      listViewWorkItems.HeaderStyle = ColumnHeaderStyle.Nonclickable;
      listViewWorkItems.HideSelection = false;
      listViewWorkItems.Location = new Point(3, 16);
      listViewWorkItems.Name = "listViewWorkItems";
      listViewWorkItems.Size = new Size(261, 438);
      listViewWorkItems.TabIndex = 7;
      listViewWorkItems.UseCompatibleStateImageBehavior = false;
      listViewWorkItems.View = View.Details;
      listViewWorkItems.SelectedIndexChanged += listViewWorkItems_SelectedIndexChanged;
      columnHeader6.Text = "ID";
      columnHeader6.Width = 50;
      columnHeader7.Text = "Work Item Type";
      columnHeader7.Width = 88;
      columnHeader8.Text = "State";
      columnHeader8.Width = 45;
      columnHeader9.Text = "Assigned To";
      columnHeader9.Width = 71;
      columnHeader10.Text = "Changed By";
      columnHeader10.Width = 70;
      columnHeader11.Text = "Title";
      columnHeader11.Width = 112;
      toolStrip2.AllowMerge = false;
      toolStrip2.CanOverflow = false;
      toolStrip2.GripMargin = new Padding(1);
      toolStrip2.GripStyle = ToolStripGripStyle.Hidden;
      toolStrip2.Items.AddRange(new ToolStripItem[8]
      {
        toolStripButtonSelectChangesets,
        toolStripSeparator4,
        toolStripButtonFromWorkItems,
        toolStripSeparator1,
        toolStripButtonReviewMode,
        toolStripButtonRemove,
        toolStripSeparator2,
        toolStripButtonSaveChangesetsAndWIs
      });
      toolStrip2.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
      toolStrip2.Location = new Point(0, 0);
      toolStrip2.Name = "toolStrip2";
      toolStrip2.Padding = new Padding(3, 1, 1, 1);
      toolStrip2.RenderMode = ToolStripRenderMode.System;
      toolStrip2.Size = new Size(267, 25);
      toolStrip2.Stretch = true;
      toolStrip2.TabIndex = 5;
      toolStripButtonSelectChangesets.Image = (Image) componentResourceManager.GetObject("toolStripButtonSelectChangesets.Image");
      toolStripButtonSelectChangesets.ImageTransparentColor = Color.Magenta;
      toolStripButtonSelectChangesets.Name = "toolStripButtonSelectChangesets";
      toolStripButtonSelectChangesets.Size = new Size(114, 20);
      toolStripButtonSelectChangesets.Text = "By Changesets ...";
      toolStripButtonSelectChangesets.ToolTipText = "Select Changesets for Review";
      toolStripButtonSelectChangesets.Click += buttonSelectChangesets_Click;
      toolStripSeparator4.Name = "toolStripSeparator4";
      toolStripSeparator4.Size = new Size(6, 23);
      toolStripButtonFromWorkItems.Image = (Image) componentResourceManager.GetObject("toolStripButtonFromWorkItems.Image");
      toolStripButtonFromWorkItems.ImageTransparentColor = Color.Magenta;
      toolStripButtonFromWorkItems.Name = "toolStripButtonFromWorkItems";
      toolStripButtonFromWorkItems.Size = new Size(109, 20);
      toolStripButtonFromWorkItems.Text = "By Work Items...";
      toolStripButtonFromWorkItems.ToolTipText = "Select Work Items for Review";
      toolStripButtonFromWorkItems.Click += toolStripButtonFromWorkItems_Click;
      toolStripSeparator1.Name = "toolStripSeparator1";
      toolStripSeparator1.Size = new Size(6, 23);
      toolStripButtonReviewMode.Checked = true;
      toolStripButtonReviewMode.CheckOnClick = true;
      toolStripButtonReviewMode.CheckState = CheckState.Checked;
      toolStripButtonReviewMode.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonReviewMode.Image = (Image) componentResourceManager.GetObject("toolStripButtonReviewMode.Image");
      toolStripButtonReviewMode.ImageTransparentColor = Color.Transparent;
      toolStripButtonReviewMode.Name = "toolStripButtonReviewMode";
      toolStripButtonReviewMode.Size = new Size(23, 20);
      toolStripButtonReviewMode.Text = "toolStripButton1";
      toolStripButtonReviewMode.ToolTipText = "Review all/selected in view";
      toolStripButtonReviewMode.Click += toolStripButtonReviewMode_Click;
      toolStripButtonRemove.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonRemove.Image = (Image) componentResourceManager.GetObject("toolStripButtonRemove.Image");
      toolStripButtonRemove.ImageTransparentColor = Color.Magenta;
      toolStripButtonRemove.Name = "toolStripButtonRemove";
      toolStripButtonRemove.Size = new Size(23, 20);
      toolStripButtonRemove.Text = "Remove selected from view";
      toolStripButtonRemove.Visible = false;
      toolStripButtonRemove.Click += toolStripButtonRemove_Click;
      toolStripSeparator2.Name = "toolStripSeparator2";
      toolStripSeparator2.Size = new Size(6, 23);
      toolStripButtonSaveChangesetsAndWIs.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonSaveChangesetsAndWIs.Image = (Image) componentResourceManager.GetObject("toolStripButtonSaveChangesetsAndWIs.Image");
      toolStripButtonSaveChangesetsAndWIs.ImageTransparentColor = Color.Black;
      toolStripButtonSaveChangesetsAndWIs.Name = "toolStripButtonSaveChangesetsAndWIs";
      toolStripButtonSaveChangesetsAndWIs.Size = new Size(23, 20);
      toolStripButtonSaveChangesetsAndWIs.Text = "toolStripButton5";
      toolStripButtonSaveChangesetsAndWIs.ToolTipText = "Save list to file";
      toolStripButtonSaveChangesetsAndWIs.Click += toolStripButtonSaveChangesetsAndWIs_Click;
      tabControl.Controls.Add(tabPageFiles);
      tabControl.Controls.Add(tabPageChangesetsAndWIs);
      tabControl.Dock = DockStyle.Fill;
      tabControl.Location = new Point(0, 0);
      tabControl.Name = "tabControl";
      tabControl.SelectedIndex = 0;
      tabControl.Size = new Size(474, 482);
      tabControl.TabIndex = 7;
      tabPageFiles.BackColor = SystemColors.Control;
      tabPageFiles.Controls.Add(listViewFiles);
      tabPageFiles.Controls.Add(groupBoxFilter);
      tabPageFiles.Controls.Add(toolStrip1);
      tabPageFiles.Location = new Point(4, 22);
      tabPageFiles.Margin = new Padding(0);
      tabPageFiles.Name = "tabPageFiles";
      tabPageFiles.Size = new Size(466, 456);
      tabPageFiles.TabIndex = 0;
      tabPageFiles.Text = "Files";
      listViewFiles.BorderStyle = BorderStyle.None;
      listViewFiles.Columns.AddRange(new ColumnHeader[5]
      {
        columnHeader1,
        columnHeader2,
        columnHeader3,
        columnHeader4,
        columnHeader5
      });
      listViewFiles.ContextMenuStrip = contextMenuStrip1;
      listViewFiles.Dock = DockStyle.Fill;
      listViewFiles.FullRowSelect = true;
      listViewFiles.HeaderStyle = ColumnHeaderStyle.Nonclickable;
      listViewFiles.HideSelection = false;
      listViewFiles.Location = new Point(0, 81);
      listViewFiles.Name = "listViewFiles";
      listViewFiles.Size = new Size(466, 375);
      listViewFiles.TabIndex = 11;
      listViewFiles.UseCompatibleStateImageBehavior = false;
      listViewFiles.View = View.Details;
      listViewFiles.SelectedIndexChanged += listViewFiles_SelectedIndexChanged;
      columnHeader1.Text = "Item";
      columnHeader1.Width = 300;
      columnHeader2.Text = "Change";
      columnHeader3.Text = "Changeset";
      columnHeader3.Width = 70;
      columnHeader4.Text = "User";
      columnHeader4.Width = 90;
      columnHeader5.Text = "Comment";
      columnHeader5.Width = 100;
      contextMenuStrip1.Items.AddRange(new ToolStripItem[7]
      {
        compareWithBeforeViewToolStripMenuItem,
        compareWithLatestToolStripMenuItem,
        compareVersionsToolStripMenuItem,
        compareWithNewestToolStripMenuItem,
        compareWithOldestToolStripMenuItem,
        toolStripSeparatorChangeset,
        viewChangesetDetailsToolStripMenuItem1
      });
      contextMenuStrip1.Name = "contextMenuStrip1";
      contextMenuStrip1.Size = new Size(268, 142);
      contextMenuStrip1.Opening += contextMenuStrip1_Opening;
      compareWithBeforeViewToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("compareWithBeforeViewToolStripMenuItem.Image");
      compareWithBeforeViewToolStripMenuItem.Name = "compareWithBeforeViewToolStripMenuItem";
      compareWithBeforeViewToolStripMenuItem.Size = new Size(267, 22);
      compareWithBeforeViewToolStripMenuItem.Text = "Compare With Previous (Not In View) ...";
      compareWithBeforeViewToolStripMenuItem.Click += compareWithBeforeViewToolStripMenuItem_Click;
      compareWithLatestToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("compareWithLatestToolStripMenuItem.Image");
      compareWithLatestToolStripMenuItem.Name = "compareWithLatestToolStripMenuItem";
      compareWithLatestToolStripMenuItem.Size = new Size(267, 22);
      compareWithLatestToolStripMenuItem.Text = "Compare With Previous In View...";
      compareWithLatestToolStripMenuItem.Click += compareWithLatestToolStripMenuItem_Click;
      compareVersionsToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("compareVersionsToolStripMenuItem.Image");
      compareVersionsToolStripMenuItem.Name = "compareVersionsToolStripMenuItem";
      compareVersionsToolStripMenuItem.Size = new Size(267, 22);
      compareVersionsToolStripMenuItem.Text = "Compare Versions ...";
      compareVersionsToolStripMenuItem.Click += compareVersionsToolStripMenuItem_Click;
      compareWithNewestToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("compareWithNewestToolStripMenuItem.Image");
      compareWithNewestToolStripMenuItem.Name = "compareWithNewestToolStripMenuItem";
      compareWithNewestToolStripMenuItem.Size = new Size(267, 22);
      compareWithNewestToolStripMenuItem.Text = "Compare With Newest in View ...";
      compareWithNewestToolStripMenuItem.Click += compareWithNewestToolStripMenuItem_Click;
      compareWithOldestToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("compareWithOldestToolStripMenuItem.Image");
      compareWithOldestToolStripMenuItem.Name = "compareWithOldestToolStripMenuItem";
      compareWithOldestToolStripMenuItem.Size = new Size(267, 22);
      compareWithOldestToolStripMenuItem.Text = "Compare With Oldest in View...";
      compareWithOldestToolStripMenuItem.Click += compareWithOldestToolStripMenuItem_Click;
      toolStripSeparatorChangeset.Name = "toolStripSeparatorChangeset";
      toolStripSeparatorChangeset.Size = new Size(264, 6);
      viewChangesetDetailsToolStripMenuItem1.Image = (Image) componentResourceManager.GetObject("viewChangesetDetailsToolStripMenuItem1.Image");
      viewChangesetDetailsToolStripMenuItem1.Name = "viewChangesetDetailsToolStripMenuItem1";
      viewChangesetDetailsToolStripMenuItem1.Size = new Size(267, 22);
      viewChangesetDetailsToolStripMenuItem1.Text = "View Changeset Details";
      viewChangesetDetailsToolStripMenuItem1.Click += viewChangesetDetailsToolStripMenuItem1_Click;
      groupBoxFilter.BackColor = SystemColors.Control;
      groupBoxFilter.Controls.Add(flowLayoutPanel1);
      groupBoxFilter.Dock = DockStyle.Top;
      groupBoxFilter.Location = new Point(0, 25);
      groupBoxFilter.Name = "groupBoxFilter";
      groupBoxFilter.Size = new Size(466, 56);
      groupBoxFilter.TabIndex = 10;
      groupBoxFilter.TabStop = false;
      groupBoxFilter.Text = "Filter";
      flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      flowLayoutPanel1.Controls.Add(labelUser);
      flowLayoutPanel1.Controls.Add(comboBoxUser);
      flowLayoutPanel1.Controls.Add(labeFileName);
      flowLayoutPanel1.Controls.Add(comboBoxFiles);
      flowLayoutPanel1.Controls.Add(buttonFilter);
      flowLayoutPanel1.Dock = DockStyle.Fill;
      flowLayoutPanel1.Location = new Point(3, 16);
      flowLayoutPanel1.MinimumSize = new Size(465, 37);
      flowLayoutPanel1.Name = "flowLayoutPanel1";
      flowLayoutPanel1.Size = new Size(465, 37);
      flowLayoutPanel1.TabIndex = 6;
      labelUser.AutoSize = true;
      labelUser.Dock = DockStyle.Fill;
      labelUser.Location = new Point(3, 0);
      labelUser.Name = "labelUser";
      labelUser.Size = new Size(29, 29);
      labelUser.TabIndex = 1;
      labelUser.Text = "User";
      labelUser.TextAlign = ContentAlignment.MiddleLeft;
      comboBoxUser.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      comboBoxUser.AutoCompleteMode = AutoCompleteMode.Suggest;
      comboBoxUser.AutoCompleteSource = AutoCompleteSource.ListItems;
      comboBoxUser.FormattingEnabled = true;
      comboBoxUser.Location = new Point(38, 3);
      comboBoxUser.MaxDropDownItems = 20;
      comboBoxUser.Name = "comboBoxUser";
      comboBoxUser.Size = new Size(131, 21);
      comboBoxUser.TabIndex = 2;
      comboBoxUser.SelectedIndexChanged += comboBoxUser_SelectedIndexChanged;
      comboBoxUser.KeyDown += comboBoxUser_KeyDown;
      labeFileName.AutoSize = true;
      labeFileName.Dock = DockStyle.Fill;
      labeFileName.Location = new Point(175, 0);
      labeFileName.Name = "labeFileName";
      labeFileName.Size = new Size(54, 29);
      labeFileName.TabIndex = 0;
      labeFileName.Text = "File Name";
      labeFileName.TextAlign = ContentAlignment.MiddleLeft;
      comboBoxFiles.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      comboBoxFiles.AutoCompleteMode = AutoCompleteMode.Suggest;
      comboBoxFiles.AutoCompleteSource = AutoCompleteSource.ListItems;
      comboBoxFiles.FormattingEnabled = true;
      comboBoxFiles.Location = new Point(235, 3);
      comboBoxFiles.MaxDropDownItems = 20;
      comboBoxFiles.Name = "comboBoxFiles";
      comboBoxFiles.Size = new Size(131, 21);
      comboBoxFiles.TabIndex = 4;
      comboBoxFiles.SelectedIndexChanged += comboBoxFiles_SelectedIndexChanged;
      comboBoxFiles.KeyDown += comboBoxFiles_KeyDown;
      buttonFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      buttonFilter.Location = new Point(379, 3);
      buttonFilter.Margin = new Padding(10, 3, 3, 3);
      buttonFilter.Name = "buttonFilter";
      buttonFilter.Size = new Size(75, 23);
      buttonFilter.TabIndex = 5;
      buttonFilter.Text = "Filter";
      buttonFilter.UseVisualStyleBackColor = true;
      buttonFilter.Click += buttonFilter_Click;
      toolStrip1.AllowMerge = false;
      toolStrip1.CanOverflow = false;
      toolStrip1.GripMargin = new Padding(1);
      toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
      toolStrip1.Items.AddRange(new ToolStripItem[9]
      {
        toolStripButtonComparePrevious,
        toolStripButtonCompareVersions,
        toolStripSeparator7,
        toolStripButtonCompareWithNewest,
        toolStripButtonCompareWithOldest,
        toolStripSeparator8,
        toolStripButtonViewChangesetDetails,
        toolStripSeparator3,
        toolStripButtonSaveToFile
      });
      toolStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
      toolStrip1.Location = new Point(0, 0);
      toolStrip1.Name = "toolStrip1";
      toolStrip1.Padding = new Padding(3, 1, 1, 1);
      toolStrip1.RenderMode = ToolStripRenderMode.System;
      toolStrip1.Size = new Size(466, 25);
      toolStrip1.Stretch = true;
      toolStrip1.TabIndex = 9;
      toolStripButtonComparePrevious.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonComparePrevious.Image = (Image) componentResourceManager.GetObject("toolStripButtonComparePrevious.Image");
      toolStripButtonComparePrevious.ImageTransparentColor = Color.Magenta;
      toolStripButtonComparePrevious.Name = "toolStripButtonComparePrevious";
      toolStripButtonComparePrevious.Size = new Size(23, 20);
      toolStripButtonComparePrevious.Text = "toolStripButton1";
      toolStripButtonComparePrevious.ToolTipText = "Compare with previous version";
      toolStripButtonComparePrevious.Click += toolStripButtonComparePrevious_Click;
      toolStripButtonCompareVersions.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonCompareVersions.Image = (Image) componentResourceManager.GetObject("toolStripButtonCompareVersions.Image");
      toolStripButtonCompareVersions.ImageTransparentColor = Color.Magenta;
      toolStripButtonCompareVersions.Name = "toolStripButtonCompareVersions";
      toolStripButtonCompareVersions.Size = new Size(23, 20);
      toolStripButtonCompareVersions.Text = "toolStripButton1";
      toolStripButtonCompareVersions.ToolTipText = "Compare two versions";
      toolStripButtonCompareVersions.Click += toolStripButtonCompareVersions_Click;
      toolStripSeparator7.Name = "toolStripSeparator7";
      toolStripSeparator7.Size = new Size(6, 23);
      toolStripButtonCompareWithNewest.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonCompareWithNewest.Image = (Image) componentResourceManager.GetObject("toolStripButtonCompareWithNewest.Image");
      toolStripButtonCompareWithNewest.ImageTransparentColor = Color.Magenta;
      toolStripButtonCompareWithNewest.Name = "toolStripButtonCompareWithNewest";
      toolStripButtonCompareWithNewest.Size = new Size(23, 20);
      toolStripButtonCompareWithNewest.Text = "toolStripButton1";
      toolStripButtonCompareWithNewest.ToolTipText = "Compare with newest in view";
      toolStripButtonCompareWithNewest.Click += toolStripButtonCompareWithNewest_Click;
      toolStripButtonCompareWithOldest.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonCompareWithOldest.Image = (Image) componentResourceManager.GetObject("toolStripButtonCompareWithOldest.Image");
      toolStripButtonCompareWithOldest.ImageTransparentColor = Color.Magenta;
      toolStripButtonCompareWithOldest.Name = "toolStripButtonCompareWithOldest";
      toolStripButtonCompareWithOldest.Size = new Size(23, 20);
      toolStripButtonCompareWithOldest.Text = "toolStripButton1";
      toolStripButtonCompareWithOldest.ToolTipText = "Compare with oldest in view";
      toolStripButtonCompareWithOldest.Click += toolStripButtonCompareWithOldest_Click;
      toolStripSeparator8.Name = "toolStripSeparator8";
      toolStripSeparator8.Size = new Size(6, 23);
      toolStripButtonViewChangesetDetails.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonViewChangesetDetails.Image = (Image) componentResourceManager.GetObject("toolStripButtonViewChangesetDetails.Image");
      toolStripButtonViewChangesetDetails.ImageTransparentColor = Color.Magenta;
      toolStripButtonViewChangesetDetails.Name = "toolStripButtonViewChangesetDetails";
      toolStripButtonViewChangesetDetails.Size = new Size(23, 20);
      toolStripButtonViewChangesetDetails.Text = "View Changeset Details";
      toolStripButtonViewChangesetDetails.Click += toolStripButtonViewChangesetDetails_Click;
      toolStripSeparator3.Name = "toolStripSeparator3";
      toolStripSeparator3.Size = new Size(6, 23);
      toolStripButtonSaveToFile.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonSaveToFile.Image = (Image) componentResourceManager.GetObject("toolStripButtonSaveToFile.Image");
      toolStripButtonSaveToFile.ImageTransparentColor = Color.Black;
      toolStripButtonSaveToFile.Name = "toolStripButtonSaveToFile";
      toolStripButtonSaveToFile.Size = new Size(23, 20);
      toolStripButtonSaveToFile.Text = "toolStripButton5";
      toolStripButtonSaveToFile.ToolTipText = "Save list to file";
      toolStripButtonSaveToFile.Click += toolStripButtonSaveToFile_Click;
      tabPageChangesetsAndWIs.BackColor = SystemColors.Control;
      tabPageChangesetsAndWIs.Controls.Add(listViewWIsByChangesets);
      tabPageChangesetsAndWIs.Controls.Add(listViewChangesetsByWI);
      tabPageChangesetsAndWIs.Controls.Add(toolStrip3);
      tabPageChangesetsAndWIs.Location = new Point(4, 22);
      tabPageChangesetsAndWIs.Name = "tabPageChangesetsAndWIs";
      tabPageChangesetsAndWIs.Padding = new Padding(3);
      tabPageChangesetsAndWIs.Size = new Size(466, 456);
      tabPageChangesetsAndWIs.TabIndex = 1;
      tabPageChangesetsAndWIs.Text = "Work Items";
      listViewWIsByChangesets.BorderStyle = BorderStyle.None;
      listViewWIsByChangesets.Columns.AddRange(new ColumnHeader[6]
      {
        columnHeaderWorkItemId,
        columnHeaderWorkItemType,
        columnHeaderWorkItemState,
        columnHeaderAssignedTo,
        columnHeaderChangedBy,
        columnHeaderWorkItemTitle
      });
      listViewWIsByChangesets.Dock = DockStyle.Fill;
      listViewWIsByChangesets.FullRowSelect = true;
      listViewWIsByChangesets.HeaderStyle = ColumnHeaderStyle.Nonclickable;
      listViewWIsByChangesets.Location = new Point(3, 28);
      listViewWIsByChangesets.Name = "listViewWIsByChangesets";
      listViewWIsByChangesets.Size = new Size(460, 425);
      listViewWIsByChangesets.TabIndex = 12;
      listViewWIsByChangesets.UseCompatibleStateImageBehavior = false;
      listViewWIsByChangesets.View = View.Details;
      columnHeaderWorkItemId.Text = "ID";
      columnHeaderWorkItemId.Width = 67;
      columnHeaderWorkItemType.Text = "Work Item Type";
      columnHeaderWorkItemType.Width = 97;
      columnHeaderWorkItemState.Text = "State";
      columnHeaderWorkItemState.Width = 108;
      columnHeaderAssignedTo.Text = "Assigned To";
      columnHeaderAssignedTo.Width = 80;
      columnHeaderChangedBy.Text = "Changed By";
      columnHeaderChangedBy.Width = 75;
      columnHeaderWorkItemTitle.Text = "Title";
      columnHeaderWorkItemTitle.Width = 112;
      listViewChangesetsByWI.BorderStyle = BorderStyle.None;
      listViewChangesetsByWI.Columns.AddRange(new ColumnHeader[5]
      {
        columnHeader12,
        columnHeader13,
        columnHeader14,
        columnHeader15,
        columnHeader16
      });
      listViewChangesetsByWI.ContextMenuStrip = contextMenuStripChangeset;
      listViewChangesetsByWI.Dock = DockStyle.Fill;
      listViewChangesetsByWI.FullRowSelect = true;
      listViewChangesetsByWI.HeaderStyle = ColumnHeaderStyle.Nonclickable;
      listViewChangesetsByWI.HideSelection = false;
      listViewChangesetsByWI.Location = new Point(3, 28);
      listViewChangesetsByWI.Name = "listViewChangesetsByWI";
      listViewChangesetsByWI.Size = new Size(460, 425);
      listViewChangesetsByWI.TabIndex = 13;
      listViewChangesetsByWI.UseCompatibleStateImageBehavior = false;
      listViewChangesetsByWI.View = View.Details;
      listViewChangesetsByWI.SelectedIndexChanged += listViewChangesetsByWI_SelectedIndexChanged;
      columnHeader12.Text = "Changeset";
      columnHeader12.Width = 70;
      columnHeader13.Text = "Date";
      columnHeader13.Width = 104;
      columnHeader14.Text = "User";
      columnHeader14.Width = 90;
      columnHeader15.Text = "Comment";
      columnHeader16.Text = "Policy Violation";
      toolStrip3.AllowMerge = false;
      toolStrip3.CanOverflow = false;
      toolStrip3.GripMargin = new Padding(1);
      toolStrip3.GripStyle = ToolStripGripStyle.Hidden;
      toolStrip3.Items.AddRange(new ToolStripItem[2]
      {
        toolStripButtonChangesetDetails,
        toolStripButtonSaveAssociatedWIsAndChangesets
      });
      toolStrip3.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
      toolStrip3.Location = new Point(3, 3);
      toolStrip3.Name = "toolStrip3";
      toolStrip3.Padding = new Padding(3, 1, 1, 1);
      toolStrip3.RenderMode = ToolStripRenderMode.System;
      toolStrip3.Size = new Size(460, 25);
      toolStrip3.Stretch = true;
      toolStrip3.TabIndex = 11;
      toolStripButtonChangesetDetails.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonChangesetDetails.Image = (Image) componentResourceManager.GetObject("toolStripButtonChangesetDetails.Image");
      toolStripButtonChangesetDetails.ImageTransparentColor = Color.Magenta;
      toolStripButtonChangesetDetails.Name = "toolStripButtonChangesetDetails";
      toolStripButtonChangesetDetails.Size = new Size(23, 20);
      toolStripButtonChangesetDetails.Text = "View Changeset Details";
      toolStripButtonChangesetDetails.Click += toolStripButtonChangesetDetails_Click;
      toolStripButtonSaveAssociatedWIsAndChangesets.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButtonSaveAssociatedWIsAndChangesets.Image = (Image) componentResourceManager.GetObject("toolStripButtonSaveAssociatedWIsAndChangesets.Image");
      toolStripButtonSaveAssociatedWIsAndChangesets.ImageTransparentColor = Color.Black;
      toolStripButtonSaveAssociatedWIsAndChangesets.Name = "toolStripButtonSaveAssociatedWIsAndChangesets";
      toolStripButtonSaveAssociatedWIsAndChangesets.Size = new Size(23, 20);
      toolStripButtonSaveAssociatedWIsAndChangesets.Text = "Save list to file";
      toolStripButtonSaveAssociatedWIsAndChangesets.ToolTipText = "Save list to file";
      toolStripButtonSaveAssociatedWIsAndChangesets.Click += toolStripButtonSaveAssociatedWIsAndChangesets_Click;
      toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButton1.Image = (Image) componentResourceManager.GetObject("toolStripButton1.Image");
      toolStripButton1.ImageTransparentColor = Color.Magenta;
      toolStripButton1.Name = "toolStripButton1";
      toolStripButton1.Size = new Size(23, 20);
      toolStripButton1.Text = "toolStripButton1";
      toolStripButton1.ToolTipText = "Compare with previous version";
      toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButton2.Image = (Image) componentResourceManager.GetObject("toolStripButton2.Image");
      toolStripButton2.ImageTransparentColor = Color.Magenta;
      toolStripButton2.Name = "toolStripButton2";
      toolStripButton2.Size = new Size(23, 20);
      toolStripButton2.Text = "toolStripButton1";
      toolStripButton2.ToolTipText = "Compare two versions";
      toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButton3.Image = (Image) componentResourceManager.GetObject("toolStripButton3.Image");
      toolStripButton3.ImageTransparentColor = Color.Magenta;
      toolStripButton3.Name = "toolStripButton3";
      toolStripButton3.Size = new Size(23, 20);
      toolStripButton3.Text = "View Changeset Details";
      toolStripSeparator5.Name = "toolStripSeparator5";
      toolStripSeparator5.Size = new Size(6, 23);
      toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripButton4.Image = (Image) componentResourceManager.GetObject("toolStripButton4.Image");
      toolStripButton4.ImageTransparentColor = Color.Black;
      toolStripButton4.Name = "toolStripButton4";
      toolStripButton4.Size = new Size(23, 20);
      toolStripButton4.Text = "toolStripButton5";
      toolStripButton4.ToolTipText = "Save list to file";
      AutoScaleDimensions = new SizeF(6f, 13f);
      AutoScaleMode = AutoScaleMode.Font;
      Controls.Add(splitContainer1);
      Name = nameof (CodeReviewControl);
      Size = new Size(751, 486);
      splitContainer1.Panel1.ResumeLayout(false);
      splitContainer1.Panel1.PerformLayout();
      splitContainer1.Panel2.ResumeLayout(false);
      splitContainer1.ResumeLayout(false);
      groupBoxSelectedChangesetsAndWIs.ResumeLayout(false);
      contextMenuStripChangeset.ResumeLayout(false);
      toolStrip2.ResumeLayout(false);
      toolStrip2.PerformLayout();
      tabControl.ResumeLayout(false);
      tabPageFiles.ResumeLayout(false);
      tabPageFiles.PerformLayout();
      contextMenuStrip1.ResumeLayout(false);
      groupBoxFilter.ResumeLayout(false);
      flowLayoutPanel1.ResumeLayout(false);
      flowLayoutPanel1.PerformLayout();
      toolStrip1.ResumeLayout(false);
      toolStrip1.PerformLayout();
      tabPageChangesetsAndWIs.ResumeLayout(false);
      tabPageChangesetsAndWIs.PerformLayout();
      toolStrip3.ResumeLayout(false);
      toolStrip3.PerformLayout();
      ResumeLayout(false);
    }

    public override void Initialize(TfsController controller)
    {
      _controller = new CodeReviewController(controller);
      _formWorkItems = new FormSelectWorkItems(_controller);
      _dtUsers = _controller.Users.UsersTable.Copy();
      ListTable.AddAllRow(_dtUsers);
      LoadSearchParameters();
    }

    public override void LoadUsers(TfsController controller)
    {
      var text = comboBoxUser.Text;
      _dtUsers = controller.Users.UsersTable.Copy();
      ListTable.AddAllRow(_dtUsers);
      comboBoxUser.SelectedIndexChanged -= comboBoxUser_SelectedIndexChanged;
      var stringList = new List<string>();
      foreach (string id in comboBoxUser.Items)
        stringList.Add(Utilities.GetTableValueByID(_dtUsers, id));
      comboBoxUser.Items.Clear();
      comboBoxUser.Items.AddRange(stringList.ToArray());
      comboBoxUser.SelectedValue = text;
      comboBoxUser.SelectedIndexChanged += comboBoxUser_SelectedIndexChanged;
      _formWorkItems.LoadUsers();
    }

    private void LoadSearchParameters() => _formWorkItems.LoadUsers();

    private void LoadChangesets(int[] changesetIds)
    {
      groupBoxSelectedChangesetsAndWIs.Text = "Changesets";
      tabPageChangesetsAndWIs.Text = "Work Items";
      listViewWorkItems.Visible = false;
      listViewWIsByChangesets.Visible = true;
      listViewChangesetsByWI.Visible = false;
      listViewChangesets.Visible = true;
      toolStripButtonChangesetDetails.Visible = false;
      toolStripButtonRemove.Enabled = false;
      listViewChangesets.BeginUpdate();
      listViewChangesets.Items.Clear();
      comboBoxFiles.Items.Clear();
      comboBoxFiles.Text = string.Empty;
      comboBoxUser.Items.Clear();
      comboBoxUser.Text = string.Empty;
      try
      {
        listViewChangesets.SelectedIndexChanged -= listViewChangesets_SelectedIndexChanged;
        foreach (var changesetsFromId in _controller.GetChangesetsFromIds(changesetIds))
        {
          var listViewItem = NewChangeset(changesetsFromId, true);
          listViewChangesets.Items.Add(listViewItem);
          listViewItem.Selected = true;
        }
        listViewChangesets.SelectedIndexChanged += listViewChangesets_SelectedIndexChanged;
        foreach (ColumnHeader column in listViewChangesets.Columns)
        {
          column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
          listViewChangesets.ColumnWidthChanged -= listViewChangesets_ColumnWidthChanged;
          ResizeColumn(listViewChangesets, column);
          listViewChangesets.ColumnWidthChanged += listViewChangesets_ColumnWidthChanged;
        }
        ReviewChangesets(true, true);
      }
      finally
      {
        listViewChangesets.EndUpdate();
      }
      toolStripButtonReviewMode.Enabled = listViewChangesets.Items.Count > 0;
      if (toolStripButtonReviewMode.Enabled)
        toolStripButtonReviewMode.Checked = true;
      toolStripButtonSaveChangesetsAndWIs.Enabled = listViewChangesets.Items.Count > 0;
    }

    private void listViewChangesets_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
    {
      listViewChangesets.ColumnWidthChanged -= listViewChangesets_ColumnWidthChanged;
      ResizeColumn(listViewChangesets, listViewChangesets.Columns[e.ColumnIndex]);
      listViewChangesets.ColumnWidthChanged += listViewChangesets_ColumnWidthChanged;
    }

    private ListViewItem NewChangeset(Changeset changeset, bool addIndication)
    {
      ListViewItem listViewItem;
      if (addIndication)
      {
        listViewItem = new ListViewItem(changeset.WorkItems.Length != 0 ? "*" : "");
        listViewItem.SubItems.Add(changeset.ChangesetId.ToString());
      }
      else
        listViewItem = new ListViewItem(changeset.ChangesetId.ToString());
      listViewItem.SubItems.Add(changeset.CreationDate.ToString("dd-MMM-yyyy HH:mm"));
      listViewItem.SubItems.Add(Utilities.GetTableValueByID(_dtUsers, changeset.Owner));
      listViewItem.SubItems.Add(changeset.Comment);
      listViewItem.SubItems.Add(changeset.PolicyOverride.PolicyFailures.Length != 0 ? "Yes" : "No");
      listViewItem.Tag = changeset;
      return listViewItem;
    }

    private Changeset GetChangeset(ListViewItem lvi)
    {
      var changeset = lvi.Tag as Changeset;
      if (changeset.Changes.Length == 0)
      {
        changeset = _controller.VersionControl.GetChangeset(changeset.ChangesetId, true, false);
        lvi.Tag = changeset;
      }
      return changeset;
    }

    private Changeset[] GetChangesets(bool getAll, bool fillList)
    {
      return !listViewChangesets.Visible ? GetChangesetsFromWorkItems(getAll, fillList) : GetChangesetsDirectly(getAll, fillList);
    }

    private Changeset[] GetChangesetsDirectly(bool getAll, bool fillList)
    {
      var changesetList = new List<Changeset>();
      if (getAll)
      {
        foreach (ListViewItem lvi in listViewChangesets.Items)
        {
          var changeset = GetChangeset(lvi);
          if (fillList)
            AddWorkItemsFromChangeset(changeset);
          changesetList.Add(changeset);
        }
      }
      else
      {
        for (var index = 0; index < listViewChangesets.SelectedIndices.Count; ++index)
        {
          var changeset = GetChangeset(listViewChangesets.Items[listViewChangesets.SelectedIndices[index]]);
          if (fillList)
            AddWorkItemsFromChangeset(changeset);
          changesetList.Add(changeset);
        }
      }
      if (fillList)
      {
        foreach (ColumnHeader column in listViewWIsByChangesets.Columns)
        {
          column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
          listViewWIsByChangesets.ColumnWidthChanged -= listViewWIsByChangesets_ColumnWidthChanged;
          ResizeColumn(listViewWIsByChangesets, column);
          listViewWIsByChangesets.ColumnWidthChanged += listViewWIsByChangesets_ColumnWidthChanged;
        }
      }
      return changesetList.ToArray();
    }

    private void ResizeColumn(ListView listView, ColumnHeader column)
    {
      var num = (int) Math.Ceiling(listView.CreateGraphics().MeasureString(column.Text, listView.Font).Width) + 8;
      if (num <= column.Width)
        return;
      column.Width = num;
    }

    private void listViewWIsByChangesets_ColumnWidthChanged(
      object sender,
      ColumnWidthChangedEventArgs e)
    {
      listViewWIsByChangesets.ColumnWidthChanged -= listViewWIsByChangesets_ColumnWidthChanged;
      ResizeColumn(listViewWIsByChangesets, listViewWIsByChangesets.Columns[e.ColumnIndex]);
      listViewWIsByChangesets.ColumnWidthChanged += listViewWIsByChangesets_ColumnWidthChanged;
    }

    private void AddWorkItemsFromChangeset(Changeset changeset)
    {
      foreach (var workItem in changeset.WorkItems)
      {
        var key = $"Changeset {(object)changeset.ChangesetId}";
        var group = listViewWIsByChangesets.Groups[key];
        if (group == null)
        {
          var str = key;
          group = new ListViewGroup(str, str);
          listViewWIsByChangesets.Groups.Add(group);
        }
        var listViewItem = NewWorkItem(workItem);
        group.Items.Add(listViewItem);
        listViewWIsByChangesets.Items.Add(listViewItem);
      }
    }

    private Changeset[] GetChangesetsFromWorkItems(bool getAll, bool fillList)
    {
      var dictionary = new Dictionary<int, Changeset>();
      if (getAll)
      {
        foreach (ListViewItem listViewItem in listViewWorkItems.Items)
        {
          var tag = listViewItem.Tag as WorkItem;
          var changesetsFromWorkItem = _controller.GetChangesetsFromWorkItem(tag);
          if (fillList)
            AddChangesetsFromWI(tag, changesetsFromWorkItem);
          foreach (var changeset in changesetsFromWorkItem)
          {
            if (!dictionary.ContainsKey(changeset.ChangesetId))
              dictionary.Add(changeset.ChangesetId, changeset);
          }
        }
      }
      else
      {
        for (var index = 0; index < listViewWorkItems.SelectedIndices.Count; ++index)
        {
          var tag = listViewWorkItems.Items[listViewWorkItems.SelectedIndices[index]].Tag as WorkItem;
          var changesetsFromWorkItem = _controller.GetChangesetsFromWorkItem(tag);
          if (fillList)
            AddChangesetsFromWI(tag, changesetsFromWorkItem);
          foreach (var changeset in changesetsFromWorkItem)
          {
            if (!dictionary.ContainsKey(changeset.ChangesetId))
              dictionary.Add(changeset.ChangesetId, changeset);
          }
        }
      }
      if (fillList)
      {
        foreach (ColumnHeader column in listViewChangesetsByWI.Columns)
        {
          column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
          listViewChangesetsByWI.ColumnWidthChanged -= listViewChangesetsByWI_ColumnWidthChanged;
          ResizeColumn(listViewChangesetsByWI, column);
          listViewChangesetsByWI.ColumnWidthChanged += listViewChangesetsByWI_ColumnWidthChanged;
        }
      }
      var array = new Changeset[dictionary.Values.Count];
      dictionary.Values.CopyTo(array, 0);
      return array;
    }

    private void listViewChangesetsByWI_ColumnWidthChanged(
      object sender,
      ColumnWidthChangedEventArgs e)
    {
      listViewChangesetsByWI.ColumnWidthChanged -= listViewChangesetsByWI_ColumnWidthChanged;
      ResizeColumn(listViewChangesetsByWI, listViewChangesetsByWI.Columns[e.ColumnIndex]);
      listViewChangesetsByWI.ColumnWidthChanged += listViewChangesetsByWI_ColumnWidthChanged;
    }

    private void AddChangesetsFromWI(WorkItem workItem, Changeset[] changesets)
    {
      foreach (var changeset in changesets)
      {
        var key = $"{(object)workItem.Type.Name} {(object)workItem.Id}: {(object)workItem.Title}";
        var group = listViewChangesetsByWI.Groups[key];
        if (group == null)
        {
          var str = key;
          group = new ListViewGroup(str, str);
          listViewChangesetsByWI.Groups.Add(group);
        }
        var listViewItem = NewChangeset(changeset, false);
        group.Items.Add(listViewItem);
        listViewChangesetsByWI.Items.Add(listViewItem);
      }
    }

    private void ReviewChangesets(bool getAll) => ReviewChangesets(getAll, false);

    private void ReviewChangesets(bool getAll, bool getUsers)
    {
      listViewFiles.BeginUpdate();
      listViewFiles.Items.Clear();
      listViewFiles.Groups.Clear();
      listViewWIsByChangesets.BeginUpdate();
      listViewWIsByChangesets.Items.Clear();
      listViewChangesetsByWI.BeginUpdate();
      listViewChangesetsByWI.Items.Clear();
      comboBoxFiles.BeginUpdate();
      var text1 = comboBoxFiles.Text;
      comboBoxFiles.SelectedIndexChanged -= comboBoxFiles_SelectedIndexChanged;
      comboBoxFiles.Items.Clear();
      comboBoxFiles.Items.Add(string.Empty);
      comboBoxUser.BeginUpdate();
      comboBoxUser.SelectedIndexChanged -= comboBoxUser_SelectedIndexChanged;
      var dictionary = new Dictionary<string, List<ListViewItem>>();
      Cursor.Current = Cursors.WaitCursor;
      try
      {
        var lower = comboBoxFiles.Text != null ? comboBoxFiles.Text.ToLower() : null;
        var text2 = comboBoxUser.Text;
        var stringList = new List<string>();
        foreach (var changeset in GetChangesets(getAll, true))
        {
          foreach (var change in changeset.Changes)
          {
            var serverItem = change.Item.ServerItem;
            var fileName = VersionControlPath.GetFileName(serverItem);
            if (!comboBoxFiles.Items.Contains(fileName))
              comboBoxFiles.Items.Add(fileName);
            var tableValueById = Utilities.GetTableValueByID(_dtUsers, changeset.Owner);
            if (getUsers && !stringList.Contains(tableValueById))
              stringList.Add(tableValueById);
            if ((string.IsNullOrEmpty(lower) || fileName.ToLower().Contains(lower)) && (string.IsNullOrEmpty(text2) || string.Compare(tableValueById, text2, true) == 0))
            {
              List<ListViewItem> listViewItemList;
              if (!dictionary.TryGetValue(serverItem, out listViewItemList))
              {
                listViewItemList = new List<ListViewItem>();
                dictionary.Add(serverItem, listViewItemList);
              }
              listViewItemList.Add(new ListViewItem(new string[5]
              {
                fileName,
                change.ChangeType.ToString(),
                change.Item.ChangesetId.ToString(),
                tableValueById,
                changeset.Comment
              })
              {
                Tag = change
              });
            }
          }
          if (getUsers)
          {
            comboBoxUser.Items.Clear();
            comboBoxUser.Items.Add(string.Empty);
            comboBoxUser.Items.AddRange(stringList.ToArray());
            comboBoxUser.Text = text2;
          }
        }
        foreach (var key in dictionary.Keys)
        {
          var str = key;
          var group = new ListViewGroup(str, str);
          listViewFiles.Groups.Add(group);
          var array = dictionary[key].ToArray();
          Array.Sort(array, (lvi1, lvi2) => int.Parse(lvi2.SubItems[2].Text).CompareTo(int.Parse(lvi1.SubItems[2].Text)));
          group.Items.AddRange(array);
          listViewFiles.Items.AddRange(array);
        }
        foreach (ColumnHeader column in listViewFiles.Columns)
        {
          column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
          listViewFiles.ColumnWidthChanged -= listViewFiles_ColumnWidthChanged;
          ResizeColumn(listViewFiles, column);
          listViewFiles.ColumnWidthChanged += listViewFiles_ColumnWidthChanged;
        }
      }
      finally
      {
        comboBoxUser.EndUpdate();
        comboBoxUser.SelectedIndexChanged += comboBoxUser_SelectedIndexChanged;
        comboBoxFiles.Sorted = true;
        comboBoxFiles.EndUpdate();
        comboBoxFiles.Text = text1;
        comboBoxFiles.SelectedIndexChanged += comboBoxFiles_SelectedIndexChanged;
        listViewFiles.EndUpdate();
        listViewWIsByChangesets.EndUpdate();
        listViewChangesetsByWI.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
      toolStripButtonComparePrevious.Enabled = false;
      toolStripButtonCompareVersions.Enabled = false;
      toolStripButtonSaveToFile.Enabled = listViewFiles.Items.Count > 0;
    }

    private void listViewFiles_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
    {
      listViewFiles.ColumnWidthChanged -= listViewFiles_ColumnWidthChanged;
      ResizeColumn(listViewFiles, listViewFiles.Columns[e.ColumnIndex]);
      listViewFiles.ColumnWidthChanged += listViewFiles_ColumnWidthChanged;
    }

    public bool SelectChangesetsForReview(string filterPath)
    {
      using (var dialogFindChangeset = new DialogFindChangeset(_controller.VersionControl, _changesetSettings))
      {
        dialogFindChangeset.SetFilterPath(filterPath);
        if (!dialogFindChangeset.ShowDialog(this))
          return false;
        _changesetSettings = dialogFindChangeset.Settings;
        LoadChangesets(dialogFindChangeset.SelectedChangesets);
        return true;
      }
    }

    private void SelectChangesetsForReview()
    {
      using (var dialogFindChangeset = new DialogFindChangeset(_controller.VersionControl, _changesetSettings))
      {
        if (!dialogFindChangeset.ShowDialog(this))
          return;
        _changesetSettings = dialogFindChangeset.Settings;
        LoadChangesets(dialogFindChangeset.SelectedChangesets);
      }
    }

    private void ChangesetSelected()
    {
      toolStripButtonViewChangesetDetails.Enabled = false;
      if (toolStripButtonReviewMode.Checked)
      {
        listViewChangesets.SelectedIndexChanged -= listViewChangesets_SelectedIndexChanged;
        listViewChangesets.SelectedIndices.Clear();
        toolStripButtonReviewMode.Checked = false;
        listViewChangesets.SelectedIndexChanged += listViewChangesets_SelectedIndexChanged;
      }
      else
        ReviewChangesets(false, true);
    }

    private void WorkItemSelected()
    {
      if (toolStripButtonReviewMode.Checked)
      {
        listViewWorkItems.SelectedIndexChanged -= listViewWorkItems_SelectedIndexChanged;
        listViewWorkItems.SelectedIndices.Clear();
        toolStripButtonReviewMode.Checked = false;
        listViewWorkItems.SelectedIndexChanged += listViewWorkItems_SelectedIndexChanged;
      }
      toolStripButtonChangesetDetails.Enabled = false;
      ReviewChangesets(false, true);
    }

    private bool RefreshVersionCompare()
    {
      var flag1 = listViewFiles.SelectedIndices.Count == 1;
      var flag2 = listViewFiles.SelectedIndices.Count == 1;
      var flag3 = listViewFiles.SelectedIndices.Count == 2;
      var flag4 = false;
      var flag5 = false;
      var flag6 = false;
      viewChangesetDetailsToolStripMenuItem1.Visible = toolStripSeparatorChangeset.Visible = toolStripButtonViewChangesetDetails.Enabled = flag1;
      if (flag2)
      {
        var listViewItem = listViewFiles.Items[listViewFiles.SelectedIndices[0]];
        var tag = listViewItem.Tag as Change;
        flag2 = flag2 && tag.Item.ItemType != ItemType.Folder && tag.ChangeType != ChangeType.Delete;
        var num = listViewItem.Group.Items.IndexOf(listViewItem);
        flag5 = num > 0;
        flag4 = num < listViewItem.Group.Items.Count - 1;
        flag6 = listViewItem.Group.Items.Count > 1 && num != listViewItem.Group.Items.Count - 1;
      }
      else if (flag3)
      {
        var tag1 = listViewFiles.Items[listViewFiles.SelectedIndices[0]].Tag as Change;
        flag3 = flag3 && tag1.Item.ItemType != ItemType.Folder && tag1.ChangeType != ChangeType.Delete;
        if (flag3)
        {
          var tag2 = listViewFiles.Items[listViewFiles.SelectedIndices[1]].Tag as Change;
          flag3 = flag3 && tag2.ChangeType != ChangeType.Delete;
        }
      }
      compareWithBeforeViewToolStripMenuItem.Visible = flag2;
      compareWithLatestToolStripMenuItem.Visible = flag6;
      compareVersionsToolStripMenuItem.Visible = flag3;
      toolStripButtonComparePrevious.Enabled = flag2;
      toolStripButtonCompareVersions.Enabled = flag3;
      compareWithOldestToolStripMenuItem.Visible = flag4;
      compareWithNewestToolStripMenuItem.Visible = flag5;
      toolStripButtonCompareWithOldest.Enabled = flag4;
      toolStripButtonCompareWithNewest.Enabled = flag5;
      return flag2 | flag3 | flag1;
    }

    private void CompareWithNewestVersion()
    {
      var listViewItem = listViewFiles.Items[listViewFiles.SelectedIndices[0]];
      var tag1 = listViewItem.Tag as Change;
      var tag2 = listViewItem.Group.Items[0].Tag as Change;
      Cursor.Current = Cursors.WaitCursor;
      try
      {
        _controller.CompareChanges(tag1, tag2);
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to compare with newest version." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
    }

    private void CompareWithBeforeViewVersion()
    {
      var listViewItem = listViewFiles.Items[listViewFiles.SelectedIndices[0]];
      var tag1 = listViewItem.Tag as Change;
      var tag2 = listViewItem.Group.Items[listViewItem.Group.Items.Count - 1].Tag as Change;
      Cursor.Current = Cursors.WaitCursor;
      try
      {
        _controller.CompareWithVersion(tag1, tag2.Item.ChangesetId - 1);
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to compare with version." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
    }

    private void CompareWithOldestVersion()
    {
      var listViewItem = listViewFiles.Items[listViewFiles.SelectedIndices[0]];
      var tag1 = listViewItem.Tag as Change;
      var tag2 = listViewItem.Group.Items[listViewItem.Group.Items.Count - 1].Tag as Change;
      Cursor.Current = Cursors.WaitCursor;
      try
      {
        _controller.CompareChanges(tag1, tag2);
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to compare with oldest version." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
    }

    private void CompareWithPreviousVersion()
    {
      var tag = listViewFiles.Items[listViewFiles.SelectedIndices[0]].Tag as Change;
      Cursor.Current = Cursors.WaitCursor;
      try
      {
        _controller.CompareWithPreviousVersion(tag);
      }
      catch (Exception ex)
      {
        var num = (int) MessageBox.Show("Failed to compare with previous version." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
    }

    private void CompareTwoVersions()
    {
      var tag1 = listViewFiles.Items[listViewFiles.SelectedIndices[0]].Tag as Change;
      var tag2 = listViewFiles.Items[listViewFiles.SelectedIndices[1]].Tag as Change;
      Cursor.Current = Cursors.WaitCursor;
      try
      {
        _controller.CompareChanges(tag1, tag2);
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
    }

    private void SwitchReviewMode()
    {
      listViewChangesets.SelectedIndexChanged -= listViewChangesets_SelectedIndexChanged;
      listViewWorkItems.SelectedIndexChanged -= listViewWorkItems_SelectedIndexChanged;
      SwitchReviewMode(listViewChangesets.Visible ? listViewChangesets : listViewWorkItems);
      RefreshVersionCompare();
      listViewChangesets.SelectedIndexChanged += listViewChangesets_SelectedIndexChanged;
      listViewWorkItems.SelectedIndexChanged += listViewWorkItems_SelectedIndexChanged;
    }

    private void SwitchReviewMode(ListView listView)
    {
      var num = toolStripButtonReviewMode.Checked ? 1 : 0;
      listViewFiles.BeginUpdate();
      listView.BeginUpdate();
      if (num == 0)
      {
        listView.SelectedIndices.Clear();
        listViewFiles.Items.Clear();
        listViewChangesetsByWI.Items.Clear();
      }
      else
      {
        foreach (ListViewItem listViewItem in listView.Items)
          listViewItem.Selected = true;
        ReviewChangesets(true, true);
      }
      listView.EndUpdate();
      listViewFiles.EndUpdate();
    }

    private void SelectChangesetFromWorkItems()
    {
      if (_formWorkItems.ShowDialog(this) != DialogResult.OK)
        return;
      LoadWorkItems(_formWorkItems.WorkItems);
    }

    private void LoadWorkItems(WorkItem[] workItems)
    {
      groupBoxSelectedChangesetsAndWIs.Text = "Work Items";
      tabPageChangesetsAndWIs.Text = "Changesets";
      listViewWorkItems.Visible = true;
      listViewWIsByChangesets.Visible = false;
      listViewChangesetsByWI.Visible = true;
      listViewChangesets.Visible = false;
      toolStripButtonViewChangesetDetails.Enabled = false;
      toolStripButtonChangesetDetails.Visible = true;
      toolStripButtonRemove.Enabled = false;
      listViewWorkItems.BeginUpdate();
      listViewWorkItems.Items.Clear();
      try
      {
        listViewWorkItems.SelectedIndexChanged -= listViewWorkItems_SelectedIndexChanged;
        foreach (var workItem in workItems)
        {
          var listViewItem = NewWorkItem(workItem);
          listViewItem.Selected = true;
          listViewWorkItems.Items.Add(listViewItem);
        }
        foreach (ColumnHeader column in listViewWorkItems.Columns)
        {
          column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
          ResizeColumn(listViewWorkItems, column);
        }
        listViewWorkItems.SelectedIndexChanged += listViewWorkItems_SelectedIndexChanged;
        ReviewChangesets(true, true);
      }
      finally
      {
        listViewWorkItems.EndUpdate();
      }
      toolStripButtonReviewMode.Enabled = listViewWorkItems.Items.Count > 0;
      if (toolStripButtonReviewMode.Enabled)
        toolStripButtonReviewMode.Checked = true;
      toolStripButtonSaveChangesetsAndWIs.Enabled = listViewWorkItems.Items.Count > 0;
    }

    private void listViewWorkItems_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
    {
      listViewWorkItems.ColumnWidthChanged -= listViewWorkItems_ColumnWidthChanged;
      ResizeColumn(listViewWorkItems, listViewWorkItems.Columns[e.ColumnIndex]);
      listViewWorkItems.ColumnWidthChanged += listViewWorkItems_ColumnWidthChanged;
    }

    private ListViewItem NewWorkItem(WorkItem workItem)
    {
      var tableValueById1 = Utilities.GetTableValueByID(_dtUsers, workItem.Fields["System.AssignedTo"].Value.ToString());
      var tableValueById2 = Utilities.GetTableValueByID(_dtUsers, workItem.ChangedBy);
      return new ListViewItem(new string[6]
      {
        workItem.Id.ToString(),
        workItem.Type.Name,
        workItem.State,
        tableValueById1,
        tableValueById2,
        workItem.Title
      })
      {
        Tag = workItem
      };
    }

    private void RemoveSelected(ListView listView)
    {
      if (!listView.Enabled)
        return;
      listView.BeginUpdate();
      for (var index = listView.SelectedIndices.Count - 1; index >= 0; --index)
        listView.Items.RemoveAt(listView.SelectedIndices[index]);
      listView.EndUpdate();
    }

    private void ViewChangesetDetails(Changeset changeset)
    {
      if (changeset == null)
        return;
      using (var changesetDetails = new DialogChangesetDetails(_controller.VersionControl, changeset))
        changesetDetails.ShowDialog(this);
    }

    private bool CancelViewChangesets()
    {
      ListView listView = null;
      if (listViewChangesets.Visible)
        listView = listViewChangesets;
      else if (listViewChangesetsByWI.Visible)
        listView = listViewChangesetsByWI;
      return listView != null && listView.SelectedItems.Count != 1;
    }

    private void ViewAssociatedChangesetDetails()
    {
      int result;
      if (listViewFiles.SelectedIndices.Count != 1 || !int.TryParse(listViewFiles.Items[listViewFiles.SelectedIndices[0]].SubItems[2].Text, out result))
        return;
      ViewChangesetDetails(_controller.VersionControl.GetChangeset(result, true, false));
    }

    private void ViewSelectedChangesetDetails()
    {
      Changeset changeset = null;
      if (listViewChangesets.Visible)
        changeset = listViewChangesets.SelectedItems[0].Tag as Changeset;
      else if (listViewChangesetsByWI.Visible)
        changeset = listViewChangesetsByWI.SelectedItems[0].Tag as Changeset;
      ViewChangesetDetails(changeset);
    }

    public void EnableCodeReview(bool enable)
    {
      toolStripButtonSelectChangesets.Enabled = toolStripButtonFromWorkItems.Enabled = enable;
    }
  }
}
