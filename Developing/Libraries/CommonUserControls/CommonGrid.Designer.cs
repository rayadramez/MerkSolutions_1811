namespace CommonUserControls
{
	partial class CommonGrid
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
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.gridControl1);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(944, 492);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// gridControl1
			// 
			this.gridControl1.Location = new System.Drawing.Point(12, 12);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.gridControl1.Size = new System.Drawing.Size(920, 468);
			this.gridControl1.TabIndex = 0;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
			// 
			// gridView1
			// 
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn10,
            this.gridColumn9,
            this.gridColumn4,
            this.gridColumn6,
            this.gridColumn14,
            this.gridColumn5,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn11});
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsView.ShowAutoFilterRow = true;
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
			this.gridColumn2.Caption = "المـــــادة الخـــــام";
			this.gridColumn2.FieldName = "RawMaterialFullName";
			this.gridColumn2.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.OptionsColumn.AllowEdit = false;
			this.gridColumn2.OptionsColumn.FixedWidth = true;
			this.gridColumn2.OptionsColumn.ReadOnly = true;
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 0;
			this.gridColumn2.Width = 350;
			// 
			// gridColumn1
			// 
			this.gridColumn1.AppearanceCell.BackColor = System.Drawing.SystemColors.Info;
			this.gridColumn1.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn1.Caption = "ت. الشراء";
			this.gridColumn1.DisplayFormat.FormatString = "d";
			this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.gridColumn1.FieldName = "Date";
			this.gridColumn1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.OptionsColumn.AllowEdit = false;
			this.gridColumn1.OptionsColumn.FixedWidth = true;
			this.gridColumn1.OptionsColumn.ReadOnly = true;
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 1;
			this.gridColumn1.Width = 80;
			// 
			// gridColumn3
			// 
			this.gridColumn3.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.gridColumn3.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn3.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.gridColumn3.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn3.Caption = "العدد";
			this.gridColumn3.DisplayFormat.FormatString = "{0:#,##0.##}";
			this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.gridColumn3.FieldName = "Count";
			this.gridColumn3.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.OptionsColumn.AllowEdit = false;
			this.gridColumn3.OptionsColumn.FixedWidth = true;
			this.gridColumn3.OptionsColumn.ReadOnly = true;
			this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Count", "المجموع={0:#,##0.##}")});
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 2;
			this.gridColumn3.Width = 80;
			// 
			// gridColumn10
			// 
			this.gridColumn10.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.gridColumn10.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn10.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.gridColumn10.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn10.Caption = "تكلفة الوحدة (cm2)";
			this.gridColumn10.DisplayFormat.FormatString = "{0:#,##0.##}";
			this.gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.gridColumn10.FieldName = "UnitPriceInPounds";
			this.gridColumn10.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
			this.gridColumn10.Name = "gridColumn10";
			this.gridColumn10.OptionsColumn.FixedWidth = true;
			this.gridColumn10.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "UnitPriceInPounds", "المجموع={0:#,##0.##}")});
			this.gridColumn10.Visible = true;
			this.gridColumn10.VisibleIndex = 3;
			this.gridColumn10.Width = 150;
			// 
			// gridColumn9
			// 
			this.gridColumn9.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.gridColumn9.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn9.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.gridColumn9.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn9.Caption = "العرض";
			this.gridColumn9.DisplayFormat.FormatString = "{0:#,##0.##}";
			this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.gridColumn9.FieldName = "Width";
			this.gridColumn9.Name = "gridColumn9";
			this.gridColumn9.OptionsColumn.AllowEdit = false;
			this.gridColumn9.OptionsColumn.FixedWidth = true;
			this.gridColumn9.OptionsColumn.ReadOnly = true;
			this.gridColumn9.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Width", "المجموع={0:#,##0.##}")});
			this.gridColumn9.Visible = true;
			this.gridColumn9.VisibleIndex = 4;
			this.gridColumn9.Width = 100;
			// 
			// gridColumn4
			// 
			this.gridColumn4.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.gridColumn4.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn4.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.gridColumn4.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn4.Caption = "الطول";
			this.gridColumn4.DisplayFormat.FormatString = "{0:#,##0.##}";
			this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.gridColumn4.FieldName = "Height";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.OptionsColumn.AllowEdit = false;
			this.gridColumn4.OptionsColumn.FixedWidth = true;
			this.gridColumn4.OptionsColumn.ReadOnly = true;
			this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Height", "المجموع={0:#,##0.##}")});
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 5;
			this.gridColumn4.Width = 100;
			// 
			// gridColumn6
			// 
			this.gridColumn6.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.gridColumn6.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn6.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.gridColumn6.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn6.Caption = "التقسيم";
			this.gridColumn6.FieldName = "DividedByTypeName";
			this.gridColumn6.Name = "gridColumn6";
			this.gridColumn6.OptionsColumn.AllowEdit = false;
			this.gridColumn6.OptionsColumn.FixedWidth = true;
			this.gridColumn6.OptionsColumn.ReadOnly = true;
			this.gridColumn6.Visible = true;
			this.gridColumn6.VisibleIndex = 6;
			this.gridColumn6.Width = 150;
			// 
			// gridColumn14
			// 
			this.gridColumn14.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.gridColumn14.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn14.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn14.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.gridColumn14.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn14.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn14.Caption = "مساحة الوحدة";
			this.gridColumn14.DisplayFormat.FormatString = "{0:#,##0.##}";
			this.gridColumn14.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.gridColumn14.FieldName = "UnitArea";
			this.gridColumn14.Name = "gridColumn14";
			this.gridColumn14.OptionsColumn.AllowEdit = false;
			this.gridColumn14.OptionsColumn.FixedWidth = true;
			this.gridColumn14.OptionsColumn.ReadOnly = true;
			this.gridColumn14.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "UnitArea", "المجموع={0:#,##0.##}")});
			this.gridColumn14.Visible = true;
			this.gridColumn14.VisibleIndex = 7;
			this.gridColumn14.Width = 150;
			// 
			// gridColumn5
			// 
			this.gridColumn5.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.gridColumn5.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn5.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.gridColumn5.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn5.Caption = "إجمالي المساحة";
			this.gridColumn5.DisplayFormat.FormatString = "{0:#,##0.##}";
			this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.gridColumn5.FieldName = "TotalArea";
			this.gridColumn5.Name = "gridColumn5";
			this.gridColumn5.OptionsColumn.FixedWidth = true;
			this.gridColumn5.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalArea", "المجموع={0:#,##0.##}")});
			this.gridColumn5.Visible = true;
			this.gridColumn5.VisibleIndex = 8;
			this.gridColumn5.Width = 150;
			// 
			// gridColumn7
			// 
			this.gridColumn7.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.gridColumn7.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn7.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.gridColumn7.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn7.Caption = "سعر شراء الوحدة";
			this.gridColumn7.DisplayFormat.FormatString = "{0:#,##0.##}";
			this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.gridColumn7.FieldName = "PurchasingPriceInPounds";
			this.gridColumn7.Name = "gridColumn7";
			this.gridColumn7.OptionsColumn.FixedWidth = true;
			this.gridColumn7.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PurchasingPriceInPounds", "المجموع={0:#,##0.##}")});
			this.gridColumn7.Visible = true;
			this.gridColumn7.VisibleIndex = 9;
			this.gridColumn7.Width = 150;
			// 
			// gridColumn8
			// 
			this.gridColumn8.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.gridColumn8.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn8.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.gridColumn8.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn8.Caption = "إجمالي سعر الشراء";
			this.gridColumn8.DisplayFormat.FormatString = "{0:#,##0.##}";
			this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.gridColumn8.FieldName = "TotalPurchasingPriceInPounds";
			this.gridColumn8.Name = "gridColumn8";
			this.gridColumn8.OptionsColumn.FixedWidth = true;
			this.gridColumn8.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalPurchasingPriceInPounds", "المجموع={0:#,##0.##}")});
			this.gridColumn8.Visible = true;
			this.gridColumn8.VisibleIndex = 10;
			this.gridColumn8.Width = 150;
			// 
			// gridColumn11
			// 
			this.gridColumn11.Caption = "gridColumn11";
			this.gridColumn11.Name = "gridColumn11";
			this.gridColumn11.Visible = true;
			this.gridColumn11.VisibleIndex = 11;
			this.gridColumn11.Width = 20;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Size = new System.Drawing.Size(944, 492);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.gridControl1;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(924, 472);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// CommonGrid
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.layoutControl1);
			this.Name = "CommonGrid";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Size = new System.Drawing.Size(944, 492);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraGrid.GridControl gridControl1;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
	}
}
