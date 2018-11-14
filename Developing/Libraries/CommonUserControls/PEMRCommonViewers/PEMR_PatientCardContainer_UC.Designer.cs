namespace CommonUserControls.PEMRCommonViewers
{
	partial class PEMR_PatientCardContainer_UC
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PEMR_PatientCardContainer_UC));
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
			this.label1 = new System.Windows.Forms.Label();
			this.lkeStationPointStages = new DevExpress.XtraEditors.GridLookUpEdit();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.label2 = new System.Windows.Forms.Label();
			this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
			this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
			this.tabWaiting = new DevExpress.XtraTab.XtraTabPage();
			this.tabServed = new DevExpress.XtraTab.XtraTabPage();
			this.tabPaused = new DevExpress.XtraTab.XtraTabPage();
			this.txtPatientName = new DevExpress.XtraEditors.TextEdit();
			this.txtPatientID = new DevExpress.XtraEditors.TextEdit();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			this.lytName = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lkeStationPointStages.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
			this.xtraTabControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtPatientName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtPatientID.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lytName)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.btnRefresh);
			this.layoutControl1.Controls.Add(this.label1);
			this.layoutControl1.Controls.Add(this.lkeStationPointStages);
			this.layoutControl1.Controls.Add(this.label2);
			this.layoutControl1.Controls.Add(this.btnSearch);
			this.layoutControl1.Controls.Add(this.xtraTabControl1);
			this.layoutControl1.Controls.Add(this.txtPatientName);
			this.layoutControl1.Controls.Add(this.txtPatientID);
			resources.ApplyResources(this.layoutControl1, "layoutControl1");
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(524, 288, 250, 350);
			this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
			this.layoutControl1.Root = this.layoutControlGroup1;
			// 
			// btnRefresh
			// 
			this.btnRefresh.Image = global::CommonUserControls.Properties.Resources.Refresh_16_01;
			this.btnRefresh.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			resources.ApplyResources(this.btnRefresh, "btnRefresh");
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.StyleController = this.layoutControl1;
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(182)))), ((int)(((byte)(42)))));
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// lkeStationPointStages
			// 
			resources.ApplyResources(this.lkeStationPointStages, "lkeStationPointStages");
			this.lkeStationPointStages.Name = "lkeStationPointStages";
			this.lkeStationPointStages.Properties.Appearance.BackColor = ((System.Drawing.Color)(resources.GetObject("lkeStationPointStages.Properties.Appearance.BackColor")));
			this.lkeStationPointStages.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lkeStationPointStages.Properties.Appearance.Font")));
			this.lkeStationPointStages.Properties.Appearance.ForeColor = ((System.Drawing.Color)(resources.GetObject("lkeStationPointStages.Properties.Appearance.ForeColor")));
			this.lkeStationPointStages.Properties.Appearance.Options.UseBackColor = true;
			this.lkeStationPointStages.Properties.Appearance.Options.UseFont = true;
			this.lkeStationPointStages.Properties.Appearance.Options.UseForeColor = true;
			this.lkeStationPointStages.Properties.Appearance.Options.UseTextOptions = true;
			this.lkeStationPointStages.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.lkeStationPointStages.Properties.AppearanceDisabled.Font = ((System.Drawing.Font)(resources.GetObject("lkeStationPointStages.Properties.AppearanceDisabled.Font")));
			this.lkeStationPointStages.Properties.AppearanceDisabled.Options.UseFont = true;
			this.lkeStationPointStages.Properties.AppearanceDisabled.Options.UseTextOptions = true;
			this.lkeStationPointStages.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.lkeStationPointStages.Properties.AppearanceDropDown.Font = ((System.Drawing.Font)(resources.GetObject("lkeStationPointStages.Properties.AppearanceDropDown.Font")));
			this.lkeStationPointStages.Properties.AppearanceDropDown.Options.UseFont = true;
			this.lkeStationPointStages.Properties.AppearanceDropDown.Options.UseTextOptions = true;
			this.lkeStationPointStages.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.lkeStationPointStages.Properties.AppearanceFocused.Font = ((System.Drawing.Font)(resources.GetObject("lkeStationPointStages.Properties.AppearanceFocused.Font")));
			this.lkeStationPointStages.Properties.AppearanceFocused.Options.UseFont = true;
			this.lkeStationPointStages.Properties.AppearanceFocused.Options.UseTextOptions = true;
			this.lkeStationPointStages.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.lkeStationPointStages.Properties.AppearanceReadOnly.Font = ((System.Drawing.Font)(resources.GetObject("lkeStationPointStages.Properties.AppearanceReadOnly.Font")));
			this.lkeStationPointStages.Properties.AppearanceReadOnly.Options.UseFont = true;
			this.lkeStationPointStages.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
			this.lkeStationPointStages.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.lkeStationPointStages.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("lkeStationPointStages.Properties.Buttons"))))});
			this.lkeStationPointStages.Properties.ReadOnly = true;
			this.lkeStationPointStages.Properties.View = this.gridView1;
			this.lkeStationPointStages.StyleController = this.layoutControl1;
			this.lkeStationPointStages.EditValueChanged += new System.EventHandler(this.lkeStationPointStages_EditValueChanged);
			// 
			// gridView1
			// 
			this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(182)))), ((int)(((byte)(42)))));
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// btnSearch
			// 
			this.btnSearch.Image = global::CommonUserControls.Properties.Resources.Search_1_16;
			this.btnSearch.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			resources.ApplyResources(this.btnSearch, "btnSearch");
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.StyleController = this.layoutControl1;
			// 
			// xtraTabControl1
			// 
			this.xtraTabControl1.Appearance.BackColor = ((System.Drawing.Color)(resources.GetObject("xtraTabControl1.Appearance.BackColor")));
			this.xtraTabControl1.Appearance.Options.UseBackColor = true;
			this.xtraTabControl1.AppearancePage.HeaderActive.Font = ((System.Drawing.Font)(resources.GetObject("xtraTabControl1.AppearancePage.HeaderActive.Font")));
			this.xtraTabControl1.AppearancePage.HeaderActive.ForeColor = ((System.Drawing.Color)(resources.GetObject("xtraTabControl1.AppearancePage.HeaderActive.ForeColor")));
			this.xtraTabControl1.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControl1.AppearancePage.HeaderActive.Options.UseForeColor = true;
			resources.ApplyResources(this.xtraTabControl1, "xtraTabControl1");
			this.xtraTabControl1.Name = "xtraTabControl1";
			this.xtraTabControl1.SelectedTabPage = this.tabWaiting;
			this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabServed,
            this.tabPaused,
            this.tabWaiting});
			// 
			// tabWaiting
			// 
			this.tabWaiting.Appearance.Header.BackColor = ((System.Drawing.Color)(resources.GetObject("tabWaiting.Appearance.Header.BackColor")));
			this.tabWaiting.Appearance.Header.Font = ((System.Drawing.Font)(resources.GetObject("tabWaiting.Appearance.Header.Font")));
			this.tabWaiting.Appearance.Header.Options.UseBackColor = true;
			this.tabWaiting.Appearance.Header.Options.UseFont = true;
			this.tabWaiting.Appearance.HeaderActive.BackColor = ((System.Drawing.Color)(resources.GetObject("tabWaiting.Appearance.HeaderActive.BackColor")));
			this.tabWaiting.Appearance.HeaderActive.Font = ((System.Drawing.Font)(resources.GetObject("tabWaiting.Appearance.HeaderActive.Font")));
			this.tabWaiting.Appearance.HeaderActive.Options.UseBackColor = true;
			this.tabWaiting.Appearance.HeaderActive.Options.UseFont = true;
			this.tabWaiting.Appearance.PageClient.BackColor = ((System.Drawing.Color)(resources.GetObject("tabWaiting.Appearance.PageClient.BackColor")));
			this.tabWaiting.Appearance.PageClient.Options.UseBackColor = true;
			resources.ApplyResources(this.tabWaiting, "tabWaiting");
			this.tabWaiting.Name = "tabWaiting";
			// 
			// tabServed
			// 
			this.tabServed.Appearance.Header.BackColor = ((System.Drawing.Color)(resources.GetObject("tabServed.Appearance.Header.BackColor")));
			this.tabServed.Appearance.Header.Font = ((System.Drawing.Font)(resources.GetObject("tabServed.Appearance.Header.Font")));
			this.tabServed.Appearance.Header.Options.UseBackColor = true;
			this.tabServed.Appearance.Header.Options.UseFont = true;
			this.tabServed.Appearance.HeaderActive.BackColor = ((System.Drawing.Color)(resources.GetObject("tabServed.Appearance.HeaderActive.BackColor")));
			this.tabServed.Appearance.HeaderActive.Font = ((System.Drawing.Font)(resources.GetObject("tabServed.Appearance.HeaderActive.Font")));
			this.tabServed.Appearance.HeaderActive.Options.UseBackColor = true;
			this.tabServed.Appearance.HeaderActive.Options.UseFont = true;
			resources.ApplyResources(this.tabServed, "tabServed");
			this.tabServed.Name = "tabServed";
			// 
			// tabPaused
			// 
			this.tabPaused.Appearance.Header.BackColor = ((System.Drawing.Color)(resources.GetObject("tabPaused.Appearance.Header.BackColor")));
			this.tabPaused.Appearance.Header.Font = ((System.Drawing.Font)(resources.GetObject("tabPaused.Appearance.Header.Font")));
			this.tabPaused.Appearance.Header.Options.UseBackColor = true;
			this.tabPaused.Appearance.Header.Options.UseFont = true;
			this.tabPaused.Appearance.HeaderActive.BackColor = ((System.Drawing.Color)(resources.GetObject("tabPaused.Appearance.HeaderActive.BackColor")));
			this.tabPaused.Appearance.HeaderActive.Font = ((System.Drawing.Font)(resources.GetObject("tabPaused.Appearance.HeaderActive.Font")));
			this.tabPaused.Appearance.HeaderActive.Options.UseBackColor = true;
			this.tabPaused.Appearance.HeaderActive.Options.UseFont = true;
			resources.ApplyResources(this.tabPaused, "tabPaused");
			this.tabPaused.Name = "tabPaused";
			// 
			// txtPatientName
			// 
			resources.ApplyResources(this.txtPatientName, "txtPatientName");
			this.txtPatientName.Name = "txtPatientName";
			this.txtPatientName.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("txtPatientName.Properties.Appearance.Font")));
			this.txtPatientName.Properties.Appearance.Options.UseFont = true;
			this.txtPatientName.Properties.Appearance.Options.UseTextOptions = true;
			this.txtPatientName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.txtPatientName.StyleController = this.layoutControl1;
			// 
			// txtPatientID
			// 
			resources.ApplyResources(this.txtPatientID, "txtPatientID");
			this.txtPatientID.Name = "txtPatientID";
			this.txtPatientID.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("txtPatientID.Properties.Appearance.Font")));
			this.txtPatientID.Properties.Appearance.Options.UseFont = true;
			this.txtPatientID.Properties.Appearance.Options.UseTextOptions = true;
			this.txtPatientID.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.txtPatientID.StyleController = this.layoutControl1;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem5,
            this.layoutControlItem4,
            this.lytName,
            this.layoutControlItem2,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.emptySpaceItem1,
            this.emptySpaceItem2});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(511, 470);
			this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem1.AppearanceItemCaption.Font")));
			this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem1.Control = this.txtPatientID;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem1.Size = new System.Drawing.Size(155, 28);
			this.layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
			this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left;
			this.layoutControlItem1.TextSize = new System.Drawing.Size(50, 13);
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.Control = this.xtraTabControl1;
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 92);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem3.Size = new System.Drawing.Size(505, 372);
			this.layoutControlItem3.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			// 
			// layoutControlItem5
			// 
			this.layoutControlItem5.Control = this.label2;
			this.layoutControlItem5.Location = new System.Drawing.Point(0, 28);
			this.layoutControlItem5.Name = "layoutControlItem5";
			this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem5.Size = new System.Drawing.Size(505, 3);
			this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem5.TextVisible = false;
			// 
			// layoutControlItem4
			// 
			this.layoutControlItem4.Control = this.btnSearch;
			this.layoutControlItem4.Location = new System.Drawing.Point(425, 0);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem4.Size = new System.Drawing.Size(80, 28);
			this.layoutControlItem4.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem4.TextVisible = false;
			// 
			// lytName
			// 
			this.lytName.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lytName.AppearanceItemCaption.Font")));
			this.lytName.AppearanceItemCaption.Options.UseFont = true;
			this.lytName.Control = this.txtPatientName;
			this.lytName.Location = new System.Drawing.Point(155, 0);
			this.lytName.Name = "lytName";
			this.lytName.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytName.Size = new System.Drawing.Size(270, 28);
			this.lytName.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			resources.ApplyResources(this.lytName, "lytName");
			this.lytName.TextLocation = DevExpress.Utils.Locations.Left;
			this.lytName.TextSize = new System.Drawing.Size(50, 13);
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem2.AppearanceItemCaption.Font")));
			this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.layoutControlItem2.Control = this.lkeStationPointStages;
			this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 31);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem2.Size = new System.Drawing.Size(432, 58);
			this.layoutControlItem2.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
			this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
			this.layoutControlItem2.TextSize = new System.Drawing.Size(50, 17);
			// 
			// layoutControlItem6
			// 
			this.layoutControlItem6.Control = this.label1;
			this.layoutControlItem6.Location = new System.Drawing.Point(0, 89);
			this.layoutControlItem6.Name = "layoutControlItem6";
			this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem6.Size = new System.Drawing.Size(505, 3);
			this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem6.TextVisible = false;
			// 
			// layoutControlItem7
			// 
			this.layoutControlItem7.Control = this.btnRefresh;
			this.layoutControlItem7.Location = new System.Drawing.Point(453, 41);
			this.layoutControlItem7.Name = "layoutControlItem7";
			this.layoutControlItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem7.Size = new System.Drawing.Size(52, 48);
			this.layoutControlItem7.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem7.TextVisible = false;
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(432, 31);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(21, 58);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// emptySpaceItem2
			// 
			this.emptySpaceItem2.AllowHotTrack = false;
			this.emptySpaceItem2.Location = new System.Drawing.Point(453, 31);
			this.emptySpaceItem2.Name = "emptySpaceItem2";
			this.emptySpaceItem2.Size = new System.Drawing.Size(52, 10);
			this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
			// 
			// PEMR_PatientCardContainer_UC
			// 
			this.Appearance.BackColor = ((System.Drawing.Color)(resources.GetObject("PEMR_PatientCardContainer_UC.Appearance.BackColor")));
			this.Appearance.Options.UseBackColor = true;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.layoutControl1);
			this.Name = "PEMR_PatientCardContainer_UC";
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.lkeStationPointStages.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
			this.xtraTabControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.txtPatientName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtPatientID.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lytName)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraEditors.TextEdit txtPatientID;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraEditors.TextEdit txtPatientName;
		private DevExpress.XtraLayout.LayoutControlItem lytName;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
		private DevExpress.XtraTab.XtraTabPage tabWaiting;
		private DevExpress.XtraTab.XtraTabPage tabPaused;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
		private DevExpress.XtraEditors.SimpleButton btnSearch;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
		private System.Windows.Forms.Label label2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
		private DevExpress.XtraTab.XtraTabPage tabServed;
		private System.Windows.Forms.Label label1;
		private DevExpress.XtraEditors.GridLookUpEdit lkeStationPointStages;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
		private DevExpress.XtraEditors.SimpleButton btnRefresh;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;


	}
}
