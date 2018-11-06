namespace CommonUserControls.SettingsViewers.ServiceTypeStationPointViewers
{
	partial class ServiceTypeStationPoint_EditorViewer
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.chkLab = new DevExpress.XtraEditors.CheckButton();
			this.chkSurgery = new DevExpress.XtraEditors.CheckButton();
			this.chkInvestigation = new DevExpress.XtraEditors.CheckButton();
			this.chkExamination = new DevExpress.XtraEditors.CheckButton();
			this.lkeStationPoint = new DevExpress.XtraEditors.GridLookUpEdit();
			this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lkeStationPoint.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.chkLab);
			this.layoutControl1.Controls.Add(this.chkSurgery);
			this.layoutControl1.Controls.Add(this.chkInvestigation);
			this.layoutControl1.Controls.Add(this.chkExamination);
			this.layoutControl1.Controls.Add(this.lkeStationPoint);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(740, 75);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// chkLab
			// 
			this.chkLab.GroupIndex = 1;
			this.chkLab.Location = new System.Drawing.Point(365, 29);
			this.chkLab.MaximumSize = new System.Drawing.Size(120, 30);
			this.chkLab.MinimumSize = new System.Drawing.Size(120, 30);
			this.chkLab.Name = "chkLab";
			this.chkLab.Size = new System.Drawing.Size(120, 30);
			this.chkLab.StyleController = this.layoutControl1;
			this.chkLab.TabIndex = 25;
			this.chkLab.TabStop = false;
			this.chkLab.Text = "معمـــل";
			this.chkLab.CheckedChanged += new System.EventHandler(this.chkLab_CheckedChanged);
			// 
			// chkSurgery
			// 
			this.chkSurgery.GroupIndex = 1;
			this.chkSurgery.Location = new System.Drawing.Point(239, 29);
			this.chkSurgery.MaximumSize = new System.Drawing.Size(120, 30);
			this.chkSurgery.MinimumSize = new System.Drawing.Size(120, 30);
			this.chkSurgery.Name = "chkSurgery";
			this.chkSurgery.Size = new System.Drawing.Size(120, 30);
			this.chkSurgery.StyleController = this.layoutControl1;
			this.chkSurgery.TabIndex = 24;
			this.chkSurgery.TabStop = false;
			this.chkSurgery.Text = "عمليـــة";
			this.chkSurgery.CheckedChanged += new System.EventHandler(this.chkSurgery_CheckedChanged);
			// 
			// chkInvestigation
			// 
			this.chkInvestigation.GroupIndex = 1;
			this.chkInvestigation.Location = new System.Drawing.Point(491, 29);
			this.chkInvestigation.MaximumSize = new System.Drawing.Size(120, 30);
			this.chkInvestigation.MinimumSize = new System.Drawing.Size(120, 30);
			this.chkInvestigation.Name = "chkInvestigation";
			this.chkInvestigation.Size = new System.Drawing.Size(120, 30);
			this.chkInvestigation.StyleController = this.layoutControl1;
			this.chkInvestigation.TabIndex = 24;
			this.chkInvestigation.TabStop = false;
			this.chkInvestigation.Text = "فحـــص";
			this.chkInvestigation.CheckedChanged += new System.EventHandler(this.chkInvestigation_CheckedChanged);
			// 
			// chkExamination
			// 
			this.chkExamination.GroupIndex = 1;
			this.chkExamination.Location = new System.Drawing.Point(617, 29);
			this.chkExamination.MaximumSize = new System.Drawing.Size(120, 30);
			this.chkExamination.MinimumSize = new System.Drawing.Size(120, 30);
			this.chkExamination.Name = "chkExamination";
			this.chkExamination.Size = new System.Drawing.Size(120, 30);
			this.chkExamination.StyleController = this.layoutControl1;
			this.chkExamination.TabIndex = 23;
			this.chkExamination.TabStop = false;
			this.chkExamination.Text = "كشـــف";
			this.chkExamination.CheckedChanged += new System.EventHandler(this.chkExamination_CheckedChanged);
			// 
			// lkeStationPoint
			// 
			this.lkeStationPoint.Location = new System.Drawing.Point(393, 3);
			this.lkeStationPoint.MaximumSize = new System.Drawing.Size(300, 0);
			this.lkeStationPoint.MinimumSize = new System.Drawing.Size(300, 0);
			this.lkeStationPoint.Name = "lkeStationPoint";
			this.lkeStationPoint.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lkeStationPoint.Properties.View = this.gridView3;
			this.lkeStationPoint.Size = new System.Drawing.Size(300, 20);
			this.lkeStationPoint.StyleController = this.layoutControl1;
			this.lkeStationPoint.TabIndex = 22;
			// 
			// gridView3
			// 
			this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView3.Name = "gridView3";
			this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView3.OptionsView.ShowGroupPanel = false;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.layoutControlItem5});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(740, 75);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.lkeStationPoint;
			this.layoutControlItem1.Location = new System.Drawing.Point(390, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem1.Size = new System.Drawing.Size(350, 26);
			this.layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem1.Text = "العيــــادة";
			this.layoutControlItem1.TextSize = new System.Drawing.Size(41, 13);
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.chkExamination;
			this.layoutControlItem2.Location = new System.Drawing.Point(614, 26);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem2.Size = new System.Drawing.Size(126, 49);
			this.layoutControlItem2.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.Control = this.chkInvestigation;
			this.layoutControlItem3.Location = new System.Drawing.Point(488, 26);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem3.Size = new System.Drawing.Size(126, 49);
			this.layoutControlItem3.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			// 
			// layoutControlItem4
			// 
			this.layoutControlItem4.Control = this.chkSurgery;
			this.layoutControlItem4.Location = new System.Drawing.Point(236, 26);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem4.Size = new System.Drawing.Size(126, 49);
			this.layoutControlItem4.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem4.TextVisible = false;
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 26);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(236, 49);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// emptySpaceItem2
			// 
			this.emptySpaceItem2.AllowHotTrack = false;
			this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
			this.emptySpaceItem2.Name = "emptySpaceItem2";
			this.emptySpaceItem2.Size = new System.Drawing.Size(390, 26);
			this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItem5
			// 
			this.layoutControlItem5.Control = this.chkLab;
			this.layoutControlItem5.Location = new System.Drawing.Point(362, 26);
			this.layoutControlItem5.Name = "layoutControlItem5";
			this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem5.Size = new System.Drawing.Size(126, 49);
			this.layoutControlItem5.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem5.TextVisible = false;
			// 
			// ServiceTypeStationPoint_EditorViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.layoutControl1);
			this.MinimumSize = new System.Drawing.Size(0, 75);
			this.Name = "ServiceTypeStationPoint_EditorViewer";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Size = new System.Drawing.Size(740, 75);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.lkeStationPoint.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraEditors.GridLookUpEdit lkeStationPoint;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraEditors.CheckButton chkSurgery;
		private DevExpress.XtraEditors.CheckButton chkInvestigation;
		private DevExpress.XtraEditors.CheckButton chkExamination;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
		private DevExpress.XtraEditors.CheckButton chkLab;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
	}
}
