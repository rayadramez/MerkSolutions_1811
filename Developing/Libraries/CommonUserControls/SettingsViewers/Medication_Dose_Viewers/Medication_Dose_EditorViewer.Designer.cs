namespace CommonUserControls.SettingsViewers.Medication_Dose_Viewers
{
	partial class Medication_Dose_EditorViewer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Medication_Dose_EditorViewer));
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.lst_DiagnosisCategory_Diagnosis = new DevExpress.XtraEditors.ListBoxControl();
			this.btnRemoveFromList = new DevExpress.XtraEditors.SimpleButton();
			this.btnAddList = new DevExpress.XtraEditors.SimpleButton();
			this.grdDoses = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.lkeMedication = new DevExpress.XtraEditors.GridLookUpEdit();
			this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lst_DiagnosisCategory_Diagnosis)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grdDoses)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lkeMedication.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.lst_DiagnosisCategory_Diagnosis);
			this.layoutControl1.Controls.Add(this.btnRemoveFromList);
			this.layoutControl1.Controls.Add(this.btnAddList);
			this.layoutControl1.Controls.Add(this.grdDoses);
			this.layoutControl1.Controls.Add(this.lkeMedication);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(354, 267, 289, 444);
			this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(1117, 600);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// lst_DiagnosisCategory_Diagnosis
			// 
			this.lst_DiagnosisCategory_Diagnosis.Appearance.BackColor = System.Drawing.Color.LightGoldenrodYellow;
			this.lst_DiagnosisCategory_Diagnosis.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
			this.lst_DiagnosisCategory_Diagnosis.Appearance.ForeColor = System.Drawing.Color.Navy;
			this.lst_DiagnosisCategory_Diagnosis.Appearance.Options.UseBackColor = true;
			this.lst_DiagnosisCategory_Diagnosis.Appearance.Options.UseFont = true;
			this.lst_DiagnosisCategory_Diagnosis.Appearance.Options.UseForeColor = true;
			this.lst_DiagnosisCategory_Diagnosis.ItemHeight = 30;
			this.lst_DiagnosisCategory_Diagnosis.Location = new System.Drawing.Point(3, 3);
			this.lst_DiagnosisCategory_Diagnosis.Name = "lst_DiagnosisCategory_Diagnosis";
			this.lst_DiagnosisCategory_Diagnosis.Size = new System.Drawing.Size(338, 594);
			this.lst_DiagnosisCategory_Diagnosis.StyleController = this.layoutControl1;
			this.lst_DiagnosisCategory_Diagnosis.TabIndex = 25;
			// 
			// btnRemoveFromList
			// 
			this.btnRemoveFromList.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveFromList.Image")));
			this.btnRemoveFromList.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			this.btnRemoveFromList.Location = new System.Drawing.Point(347, 109);
			this.btnRemoveFromList.MaximumSize = new System.Drawing.Size(70, 100);
			this.btnRemoveFromList.MinimumSize = new System.Drawing.Size(70, 100);
			this.btnRemoveFromList.Name = "btnRemoveFromList";
			this.btnRemoveFromList.Size = new System.Drawing.Size(70, 100);
			this.btnRemoveFromList.StyleController = this.layoutControl1;
			this.btnRemoveFromList.TabIndex = 11;
			this.btnRemoveFromList.Text = "إزالـــــة";
			this.btnRemoveFromList.Click += new System.EventHandler(this.btnRemoveFromList_Click);
			// 
			// btnAddList
			// 
			this.btnAddList.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.btnAddList.Appearance.Options.UseFont = true;
			this.btnAddList.Appearance.Options.UseTextOptions = true;
			this.btnAddList.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.btnAddList.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.btnAddList.Image = ((System.Drawing.Image)(resources.GetObject("btnAddList.Image")));
			this.btnAddList.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			this.btnAddList.Location = new System.Drawing.Point(347, 3);
			this.btnAddList.MaximumSize = new System.Drawing.Size(70, 100);
			this.btnAddList.MinimumSize = new System.Drawing.Size(70, 100);
			this.btnAddList.Name = "btnAddList";
			this.btnAddList.Size = new System.Drawing.Size(70, 100);
			this.btnAddList.StyleController = this.layoutControl1;
			this.btnAddList.TabIndex = 12;
			this.btnAddList.Text = "تنفيــذ الربــط";
			this.btnAddList.Click += new System.EventHandler(this.btnAddList_Click);
			// 
			// grdDoses
			// 
			this.grdDoses.Location = new System.Drawing.Point(423, 45);
			this.grdDoses.MainView = this.gridView1;
			this.grdDoses.Name = "grdDoses";
			this.grdDoses.Size = new System.Drawing.Size(691, 552);
			this.grdDoses.TabIndex = 30;
			this.grdDoses.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
			// 
			// gridView1
			// 
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn2,
            this.gridColumn14});
			this.gridView1.GridControl = this.grdDoses;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
			this.gridView1.OptionsView.ShowAutoFilterRow = true;
			this.gridView1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
			this.gridView1.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView1_RowClick);
			// 
			// gridColumn3
			// 
			this.gridColumn3.AppearanceCell.BackColor = System.Drawing.SystemColors.Info;
			this.gridColumn3.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn3.AppearanceHeader.BackColor = System.Drawing.SystemColors.Info;
			this.gridColumn3.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn3.Caption = "الإســــم الأول";
			this.gridColumn3.FieldName = "Name_P";
			this.gridColumn3.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.OptionsColumn.AllowEdit = false;
			this.gridColumn3.OptionsColumn.FixedWidth = true;
			this.gridColumn3.OptionsColumn.ReadOnly = true;
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 0;
			this.gridColumn3.Width = 300;
			// 
			// gridColumn2
			// 
			this.gridColumn2.AppearanceCell.BackColor = System.Drawing.SystemColors.Info;
			this.gridColumn2.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn2.AppearanceHeader.BackColor = System.Drawing.SystemColors.Info;
			this.gridColumn2.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn2.Caption = "الإســــم الثـانــــي";
			this.gridColumn2.FieldName = "Name_S";
			this.gridColumn2.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.OptionsColumn.AllowEdit = false;
			this.gridColumn2.OptionsColumn.FixedWidth = true;
			this.gridColumn2.OptionsColumn.ReadOnly = true;
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 1;
			this.gridColumn2.Width = 250;
			// 
			// gridColumn14
			// 
			this.gridColumn14.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn14.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn14.Caption = "ملاحظــــات";
			this.gridColumn14.FieldName = "Address";
			this.gridColumn14.Name = "gridColumn14";
			this.gridColumn14.OptionsColumn.AllowEdit = false;
			this.gridColumn14.OptionsColumn.ReadOnly = true;
			this.gridColumn14.Visible = true;
			this.gridColumn14.VisibleIndex = 2;
			this.gridColumn14.Width = 20;
			// 
			// lkeMedication
			// 
			this.lkeMedication.Location = new System.Drawing.Point(732, 3);
			this.lkeMedication.MaximumSize = new System.Drawing.Size(300, 0);
			this.lkeMedication.MinimumSize = new System.Drawing.Size(300, 0);
			this.lkeMedication.Name = "lkeMedication";
			this.lkeMedication.Properties.Appearance.BackColor = System.Drawing.Color.Gold;
			this.lkeMedication.Properties.Appearance.Options.UseBackColor = true;
			this.lkeMedication.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lkeMedication.Properties.View = this.gridView2;
			this.lkeMedication.Size = new System.Drawing.Size(300, 20);
			this.lkeMedication.StyleController = this.layoutControl1;
			this.lkeMedication.TabIndex = 17;
			// 
			// gridView2
			// 
			this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView2.Name = "gridView2";
			this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView2.OptionsView.ShowGroupPanel = false;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.CustomizationFormText = "Root";
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.layoutControlItem5,
            this.emptySpaceItem3,
            this.layoutControlItem4,
            this.layoutControlItem3,
            this.emptySpaceItem2});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(1117, 600);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.grdDoses;
			this.layoutControlItem2.CustomizationFormText = "Doses";
			this.layoutControlItem2.Location = new System.Drawing.Point(420, 26);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem2.Size = new System.Drawing.Size(697, 574);
			this.layoutControlItem2.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem2.Text = "الجـرعــــــات";
			this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
			this.layoutControlItem2.TextSize = new System.Drawing.Size(79, 13);
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.lkeMedication;
			this.layoutControlItem1.CustomizationFormText = "Medication";
			this.layoutControlItem1.Location = new System.Drawing.Point(729, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem1.Size = new System.Drawing.Size(388, 26);
			this.layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem1.Text = "الــدواء / العــــلاج";
			this.layoutControlItem1.TextSize = new System.Drawing.Size(79, 13);
			// 
			// layoutControlItem5
			// 
			this.layoutControlItem5.Control = this.lst_DiagnosisCategory_Diagnosis;
			this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
			this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem5.Name = "layoutControlItem5";
			this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem5.Size = new System.Drawing.Size(344, 600);
			this.layoutControlItem5.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem5.TextVisible = false;
			// 
			// emptySpaceItem3
			// 
			this.emptySpaceItem3.AllowHotTrack = false;
			this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
			this.emptySpaceItem3.Location = new System.Drawing.Point(344, 212);
			this.emptySpaceItem3.Name = "emptySpaceItem3";
			this.emptySpaceItem3.Size = new System.Drawing.Size(76, 388);
			this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItem4
			// 
			this.layoutControlItem4.Control = this.btnRemoveFromList;
			this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
			this.layoutControlItem4.Location = new System.Drawing.Point(344, 106);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem4.Size = new System.Drawing.Size(76, 106);
			this.layoutControlItem4.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem4.TextVisible = false;
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.Control = this.btnAddList;
			this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
			this.layoutControlItem3.Location = new System.Drawing.Point(344, 0);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem3.Size = new System.Drawing.Size(76, 106);
			this.layoutControlItem3.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			// 
			// emptySpaceItem2
			// 
			this.emptySpaceItem2.AllowHotTrack = false;
			this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
			this.emptySpaceItem2.Location = new System.Drawing.Point(420, 0);
			this.emptySpaceItem2.Name = "emptySpaceItem2";
			this.emptySpaceItem2.Size = new System.Drawing.Size(309, 26);
			this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
			// 
			// Medication_Dose_EditorViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.layoutControl1);
			this.MinimumSize = new System.Drawing.Size(0, 600);
			this.Name = "Medication_Dose_EditorViewer";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Size = new System.Drawing.Size(1117, 600);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.lst_DiagnosisCategory_Diagnosis)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grdDoses)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lkeMedication.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraEditors.GridLookUpEdit lkeMedication;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
		private DevExpress.XtraGrid.GridControl grdDoses;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
		private DevExpress.XtraEditors.SimpleButton btnAddList;
		private DevExpress.XtraEditors.SimpleButton btnRemoveFromList;
		private DevExpress.XtraEditors.ListBoxControl lst_DiagnosisCategory_Diagnosis;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
	}
}
