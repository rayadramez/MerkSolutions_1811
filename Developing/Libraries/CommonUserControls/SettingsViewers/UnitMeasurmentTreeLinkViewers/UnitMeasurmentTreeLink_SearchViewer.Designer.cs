namespace CommonUserControls.SettingsViewers.UnitMeasurmentTreeLinkViewers
{
	partial class UnitMeasurmentTreeLink_SearchViewer
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
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.lkeParentUnitMeasurment = new DevExpress.XtraEditors.GridLookUpEdit();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.lkeChildUnitMeasurment = new DevExpress.XtraEditors.GridLookUpEdit();
			this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.spnEncapsulatedUnits = new DevExpress.XtraEditors.SpinEdit();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lkeParentUnitMeasurment.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lkeChildUnitMeasurment.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnEncapsulatedUnits.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.spnEncapsulatedUnits);
			this.layoutControl1.Controls.Add(this.lkeChildUnitMeasurment);
			this.layoutControl1.Controls.Add(this.lkeParentUnitMeasurment);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(46, 165, 250, 350);
			this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(715, 100);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.CustomizationFormText = "Root";
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.emptySpaceItem2});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(715, 100);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// lkeParentUnitMeasurment
			// 
			this.lkeParentUnitMeasurment.Location = new System.Drawing.Point(282, 3);
			this.lkeParentUnitMeasurment.MaximumSize = new System.Drawing.Size(300, 0);
			this.lkeParentUnitMeasurment.MinimumSize = new System.Drawing.Size(300, 0);
			this.lkeParentUnitMeasurment.Name = "lkeParentUnitMeasurment";
			this.lkeParentUnitMeasurment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lkeParentUnitMeasurment.Properties.View = this.gridView1;
			this.lkeParentUnitMeasurment.Size = new System.Drawing.Size(300, 20);
			this.lkeParentUnitMeasurment.StyleController = this.layoutControl1;
			this.lkeParentUnitMeasurment.TabIndex = 26;
			// 
			// gridView1
			// 
			this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			// 
			// lkeChildUnitMeasurment
			// 
			this.lkeChildUnitMeasurment.Location = new System.Drawing.Point(282, 29);
			this.lkeChildUnitMeasurment.MaximumSize = new System.Drawing.Size(300, 0);
			this.lkeChildUnitMeasurment.MinimumSize = new System.Drawing.Size(300, 0);
			this.lkeChildUnitMeasurment.Name = "lkeChildUnitMeasurment";
			this.lkeChildUnitMeasurment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lkeChildUnitMeasurment.Properties.View = this.gridView2;
			this.lkeChildUnitMeasurment.Size = new System.Drawing.Size(300, 20);
			this.lkeChildUnitMeasurment.StyleController = this.layoutControl1;
			this.lkeChildUnitMeasurment.TabIndex = 27;
			// 
			// gridView2
			// 
			this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView2.Name = "gridView2";
			this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView2.OptionsView.ShowGroupPanel = false;
			// 
			// spnEncapsulatedUnits
			// 
			this.spnEncapsulatedUnits.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.spnEncapsulatedUnits.Location = new System.Drawing.Point(432, 55);
			this.spnEncapsulatedUnits.MaximumSize = new System.Drawing.Size(150, 30);
			this.spnEncapsulatedUnits.MinimumSize = new System.Drawing.Size(150, 30);
			this.spnEncapsulatedUnits.Name = "spnEncapsulatedUnits";
			this.spnEncapsulatedUnits.Properties.Appearance.BackColor = System.Drawing.Color.Yellow;
			this.spnEncapsulatedUnits.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
			this.spnEncapsulatedUnits.Properties.Appearance.Options.UseBackColor = true;
			this.spnEncapsulatedUnits.Properties.Appearance.Options.UseFont = true;
			this.spnEncapsulatedUnits.Properties.Appearance.Options.UseTextOptions = true;
			this.spnEncapsulatedUnits.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.spnEncapsulatedUnits.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.spnEncapsulatedUnits.Size = new System.Drawing.Size(150, 30);
			this.spnEncapsulatedUnits.StyleController = this.layoutControl1;
			this.spnEncapsulatedUnits.TabIndex = 36;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.lkeParentUnitMeasurment;
			this.layoutControlItem1.CustomizationFormText = "وحــــدة القيـــــاس الأكبـــــر";
			this.layoutControlItem1.Location = new System.Drawing.Point(279, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem1.Size = new System.Drawing.Size(436, 26);
			this.layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem1.Text = "وحــــدة القيـــــاس الأكبـــــر";
			this.layoutControlItem1.TextSize = new System.Drawing.Size(127, 13);
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.lkeChildUnitMeasurment;
			this.layoutControlItem2.CustomizationFormText = "وحــــدة القيـــــاس الأصغـــر";
			this.layoutControlItem2.Location = new System.Drawing.Point(279, 26);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem2.Size = new System.Drawing.Size(436, 26);
			this.layoutControlItem2.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem2.Text = "وحــــدة القيـــــاس الأصغـــر";
			this.layoutControlItem2.TextSize = new System.Drawing.Size(127, 13);
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.Control = this.spnEncapsulatedUnits;
			this.layoutControlItem3.CustomizationFormText = "عـدد الوحــدات";
			this.layoutControlItem3.Location = new System.Drawing.Point(429, 52);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem3.Size = new System.Drawing.Size(286, 48);
			this.layoutControlItem3.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem3.Text = "عـدد الوحــدات";
			this.layoutControlItem3.TextSize = new System.Drawing.Size(127, 13);
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 52);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(429, 48);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// emptySpaceItem2
			// 
			this.emptySpaceItem2.AllowHotTrack = false;
			this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
			this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
			this.emptySpaceItem2.Name = "emptySpaceItem2";
			this.emptySpaceItem2.Size = new System.Drawing.Size(279, 52);
			this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
			// 
			// UnitMeasurmentTreeLink_SearchViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.layoutControl1);
			this.MinimumSize = new System.Drawing.Size(0, 100);
			this.Name = "UnitMeasurmentTreeLink_SearchViewer";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Size = new System.Drawing.Size(715, 100);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lkeParentUnitMeasurment.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lkeChildUnitMeasurment.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnEncapsulatedUnits.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraEditors.GridLookUpEdit lkeParentUnitMeasurment;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraEditors.GridLookUpEdit lkeChildUnitMeasurment;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
		private DevExpress.XtraEditors.SpinEdit spnEncapsulatedUnits;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
	}
}
