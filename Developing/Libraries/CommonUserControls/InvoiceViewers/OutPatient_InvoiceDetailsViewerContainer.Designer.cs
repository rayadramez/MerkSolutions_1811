namespace CommonUserControls.InvoiceViewers
{
	partial class OutPatient_InvoiceDetailsViewerContainer
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
			this.grd_InvoiceDetail_OutPatient = new DevExpress.XtraGrid.GridControl();
			this.grdView = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd_InvoiceDetail_OutPatient)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grdView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.grd_InvoiceDetail_OutPatient);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(1104, 524);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// grd_InvoiceDetail_OutPatient
			// 
			this.grd_InvoiceDetail_OutPatient.Location = new System.Drawing.Point(3, 3);
			this.grd_InvoiceDetail_OutPatient.MainView = this.grdView;
			this.grd_InvoiceDetail_OutPatient.Name = "grd_InvoiceDetail_OutPatient";
			this.grd_InvoiceDetail_OutPatient.Size = new System.Drawing.Size(1098, 518);
			this.grd_InvoiceDetail_OutPatient.TabIndex = 0;
			this.grd_InvoiceDetail_OutPatient.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdView});
			// 
			// grdView
			// 
			this.grdView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn5,
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn12,
            this.gridColumn4,
            this.gridColumn6,
            this.gridColumn9,
            this.gridColumn11,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn10,
            this.gridColumn17,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn16});
			this.grdView.GridControl = this.grd_InvoiceDetail_OutPatient;
			this.grdView.Name = "grdView";
			this.grdView.OptionsView.ShowAutoFilterRow = true;
			this.grdView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grdView_CellValueChanged);
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
			this.gridColumn2.Caption = "الخـدمـــــة";
			this.gridColumn2.FieldName = "ServiceName";
			this.gridColumn2.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.OptionsColumn.AllowEdit = false;
			this.gridColumn2.OptionsColumn.FixedWidth = true;
			this.gridColumn2.OptionsColumn.ReadOnly = true;
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 0;
			this.gridColumn2.Width = 200;
			// 
			// gridColumn5
			// 
			this.gridColumn5.AppearanceCell.BackColor = System.Drawing.SystemColors.Info;
			this.gridColumn5.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn5.AppearanceHeader.BackColor = System.Drawing.SystemColors.Info;
			this.gridColumn5.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn5.Caption = "تصنيـــف الخـدمـــات";
			this.gridColumn5.FieldName = "ServiceCategoryName";
			this.gridColumn5.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
			this.gridColumn5.Name = "gridColumn5";
			this.gridColumn5.OptionsColumn.AllowEdit = false;
			this.gridColumn5.OptionsColumn.FixedWidth = true;
			this.gridColumn5.OptionsColumn.ReadOnly = true;
			this.gridColumn5.Visible = true;
			this.gridColumn5.VisibleIndex = 1;
			this.gridColumn5.Width = 150;
			// 
			// gridColumn1
			// 
			this.gridColumn1.AppearanceCell.BackColor = System.Drawing.SystemColors.Info;
			this.gridColumn1.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn1.AppearanceHeader.BackColor = System.Drawing.SystemColors.Info;
			this.gridColumn1.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn1.Caption = "نـــوع الخـدمـــــة";
			this.gridColumn1.FieldName = "ServiceTypeName";
			this.gridColumn1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.OptionsColumn.AllowEdit = false;
			this.gridColumn1.OptionsColumn.FixedWidth = true;
			this.gridColumn1.OptionsColumn.ReadOnly = true;
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 2;
			this.gridColumn1.Width = 150;
			// 
			// gridColumn3
			// 
			this.gridColumn3.AppearanceCell.BackColor = System.Drawing.Color.GreenYellow;
			this.gridColumn3.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn3.AppearanceHeader.BackColor = System.Drawing.Color.GreenYellow;
			this.gridColumn3.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn3.Caption = "حصــة المـريــض";
			this.gridColumn3.FieldName = "PatientShare_BeforeAddsOn_InvoiceItem";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.OptionsColumn.AllowEdit = false;
			this.gridColumn3.OptionsColumn.FixedWidth = true;
			this.gridColumn3.OptionsColumn.ReadOnly = true;
			this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PatientShare_BeforeAddsOn_InvoiceItem", "SUM={0:#,##0.##}")});
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 3;
			this.gridColumn3.Width = 100;
			// 
			// gridColumn12
			// 
			this.gridColumn12.AppearanceCell.BackColor = System.Drawing.Color.GreenYellow;
			this.gridColumn12.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn12.AppearanceHeader.BackColor = System.Drawing.Color.GreenYellow;
			this.gridColumn12.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn12.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn12.Caption = "جهة ؟";
			this.gridColumn12.FieldName = "IsInsuranceApplied_InvoiceItem";
			this.gridColumn12.Name = "gridColumn12";
			this.gridColumn12.OptionsColumn.FixedWidth = true;
			this.gridColumn12.Visible = true;
			this.gridColumn12.VisibleIndex = 4;
			this.gridColumn12.Width = 60;
			// 
			// gridColumn4
			// 
			this.gridColumn4.AppearanceCell.BackColor = System.Drawing.Color.GreenYellow;
			this.gridColumn4.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn4.AppearanceHeader.BackColor = System.Drawing.Color.GreenYellow;
			this.gridColumn4.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn4.Caption = "حصـة الجهـــة";
			this.gridColumn4.FieldName = "InsuranceShare_BeforeAddsOn_InvoiceItem";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.OptionsColumn.AllowEdit = false;
			this.gridColumn4.OptionsColumn.FixedWidth = true;
			this.gridColumn4.OptionsColumn.ReadOnly = true;
			this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalServiceShare", "SUM={0:#,##0.##}")});
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 5;
			this.gridColumn4.Width = 100;
			// 
			// gridColumn6
			// 
			this.gridColumn6.AppearanceCell.BackColor = System.Drawing.Color.GreenYellow;
			this.gridColumn6.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn6.AppearanceHeader.BackColor = System.Drawing.Color.GreenYellow;
			this.gridColumn6.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn6.Caption = "خصم حصة المريض";
			this.gridColumn6.FieldName = "PatientShareDiscount_InvoiceItem";
			this.gridColumn6.Name = "gridColumn6";
			this.gridColumn6.OptionsColumn.AllowEdit = false;
			this.gridColumn6.OptionsColumn.FixedWidth = true;
			this.gridColumn6.OptionsColumn.ReadOnly = true;
			this.gridColumn6.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PatientShareDiscount_InvoiceItem", "SUM={0:#,##0.##}")});
			this.gridColumn6.Visible = true;
			this.gridColumn6.VisibleIndex = 6;
			this.gridColumn6.Width = 100;
			// 
			// gridColumn9
			// 
			this.gridColumn9.AppearanceCell.BackColor = System.Drawing.Color.GreenYellow;
			this.gridColumn9.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn9.AppearanceHeader.BackColor = System.Drawing.Color.GreenYellow;
			this.gridColumn9.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn9.Caption = "إجمـالــي الخـدمـــة";
			this.gridColumn9.FieldName = "TotalServicePrice_BeforeAddsOn_InvoiceItem";
			this.gridColumn9.Name = "gridColumn9";
			this.gridColumn9.OptionsColumn.AllowEdit = false;
			this.gridColumn9.OptionsColumn.FixedWidth = true;
			this.gridColumn9.OptionsColumn.ReadOnly = true;
			this.gridColumn9.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalServicePrice_BeforeAddsOn_InvoiceItem", "SUM={0:#,##0.##}")});
			this.gridColumn9.Visible = true;
			this.gridColumn9.VisibleIndex = 7;
			this.gridColumn9.Width = 100;
			// 
			// gridColumn11
			// 
			this.gridColumn11.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.gridColumn11.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn11.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn11.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.gridColumn11.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn11.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn11.Caption = "12% ؟";
			this.gridColumn11.FieldName = "IsSurchargeApplied_InvoiceItem";
			this.gridColumn11.Name = "gridColumn11";
			this.gridColumn11.OptionsColumn.FixedWidth = true;
			this.gridColumn11.Visible = true;
			this.gridColumn11.VisibleIndex = 8;
			this.gridColumn11.Width = 60;
			// 
			// gridColumn7
			// 
			this.gridColumn7.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.gridColumn7.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn7.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.gridColumn7.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn7.Caption = "12% حصة المريض";
			this.gridColumn7.FieldName = "SurchargeAmount_PatientShare_InvoiceItem";
			this.gridColumn7.Name = "gridColumn7";
			this.gridColumn7.OptionsColumn.AllowEdit = false;
			this.gridColumn7.OptionsColumn.FixedWidth = true;
			this.gridColumn7.OptionsColumn.ReadOnly = true;
			this.gridColumn7.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SurchargeAmount_PatientShare_InvoiceItem", "SUM={0:#,##0.##}")});
			this.gridColumn7.Visible = true;
			this.gridColumn7.VisibleIndex = 9;
			this.gridColumn7.Width = 100;
			// 
			// gridColumn8
			// 
			this.gridColumn8.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.gridColumn8.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn8.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.gridColumn8.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn8.Caption = "12% حصة الجهة";
			this.gridColumn8.Name = "gridColumn8";
			this.gridColumn8.OptionsColumn.AllowEdit = false;
			this.gridColumn8.OptionsColumn.FixedWidth = true;
			this.gridColumn8.OptionsColumn.ReadOnly = true;
			this.gridColumn8.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "", "SUM={0:#,##0.##}")});
			this.gridColumn8.Visible = true;
			this.gridColumn8.VisibleIndex = 10;
			this.gridColumn8.Width = 100;
			// 
			// gridColumn10
			// 
			this.gridColumn10.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.gridColumn10.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn10.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn10.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.gridColumn10.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn10.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn10.Caption = "إجمالى 12%";
			this.gridColumn10.FieldName = "TotalSurchargeAmount_InvoiceItem";
			this.gridColumn10.Name = "gridColumn10";
			this.gridColumn10.OptionsColumn.AllowEdit = false;
			this.gridColumn10.OptionsColumn.FixedWidth = true;
			this.gridColumn10.OptionsColumn.ReadOnly = true;
			this.gridColumn10.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalSurchargeAmount_InvoiceItem", "SUM={0:#,##0.##}")});
			this.gridColumn10.Visible = true;
			this.gridColumn10.VisibleIndex = 11;
			this.gridColumn10.Width = 100;
			// 
			// gridColumn17
			// 
			this.gridColumn17.Caption = "ملاحظـــات";
			this.gridColumn17.FieldName = "Description";
			this.gridColumn17.Name = "gridColumn17";
			this.gridColumn17.Visible = true;
			this.gridColumn17.VisibleIndex = 12;
			this.gridColumn17.Width = 20;
			// 
			// gridColumn13
			// 
			this.gridColumn13.AppearanceCell.BackColor = System.Drawing.Color.Azure;
			this.gridColumn13.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn13.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn13.AppearanceHeader.BackColor = System.Drawing.Color.Azure;
			this.gridColumn13.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn13.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn13.Caption = "حصــة المـريــض";
			this.gridColumn13.FieldName = "PatientShare_AfterAddsOn_InvoiceItem";
			this.gridColumn13.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
			this.gridColumn13.Name = "gridColumn13";
			this.gridColumn13.OptionsColumn.AllowEdit = false;
			this.gridColumn13.OptionsColumn.FixedWidth = true;
			this.gridColumn13.OptionsColumn.ReadOnly = true;
			this.gridColumn13.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PatientShare_AfterAddsOn_InvoiceItem", "SUM={0:#,##0.##}")});
			this.gridColumn13.Visible = true;
			this.gridColumn13.VisibleIndex = 13;
			this.gridColumn13.Width = 100;
			// 
			// gridColumn14
			// 
			this.gridColumn14.AppearanceCell.BackColor = System.Drawing.Color.Azure;
			this.gridColumn14.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn14.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn14.AppearanceHeader.BackColor = System.Drawing.Color.Azure;
			this.gridColumn14.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn14.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn14.Caption = "حصة الجهة";
			this.gridColumn14.FieldName = "InsuranceShare_AfterAddsOn_InvoiceItem";
			this.gridColumn14.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
			this.gridColumn14.Name = "gridColumn14";
			this.gridColumn14.OptionsColumn.AllowEdit = false;
			this.gridColumn14.OptionsColumn.FixedWidth = true;
			this.gridColumn14.OptionsColumn.ReadOnly = true;
			this.gridColumn14.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "InsuranceShare_AfterAddsOn_InvoiceItem", "SUM={0:#,##0.##}")});
			this.gridColumn14.Visible = true;
			this.gridColumn14.VisibleIndex = 14;
			this.gridColumn14.Width = 100;
			// 
			// gridColumn16
			// 
			this.gridColumn16.AppearanceCell.BackColor = System.Drawing.Color.Azure;
			this.gridColumn16.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn16.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn16.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn16.AppearanceHeader.BackColor = System.Drawing.Color.Azure;
			this.gridColumn16.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn16.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn16.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn16.Caption = "صافي الخدمة";
			this.gridColumn16.FieldName = "TotalServicePrice_AfterAddsOn_InvoiceItem";
			this.gridColumn16.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
			this.gridColumn16.Name = "gridColumn16";
			this.gridColumn16.OptionsColumn.AllowEdit = false;
			this.gridColumn16.OptionsColumn.FixedWidth = true;
			this.gridColumn16.OptionsColumn.ReadOnly = true;
			this.gridColumn16.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalServicePrice_AfterAddsOn_InvoiceItem", "SUM={0:#,##0.##}")});
			this.gridColumn16.Visible = true;
			this.gridColumn16.VisibleIndex = 15;
			this.gridColumn16.Width = 100;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(1104, 524);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.grd_InvoiceDetail_OutPatient;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem1.Size = new System.Drawing.Size(1104, 524);
			this.layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// OutPatient_InvoiceDetailsViewerContainer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.layoutControl1);
			this.Name = "OutPatient_InvoiceDetailsViewerContainer";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Size = new System.Drawing.Size(1104, 524);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd_InvoiceDetail_OutPatient)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grdView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraGrid.GridControl grd_InvoiceDetail_OutPatient;
		private DevExpress.XtraGrid.Views.Grid.GridView grdView;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
	}
}
