namespace CommonUserControls.Reports
{
	partial class PEMR_PatientMedicalRecordReportSub_rpt
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

		#region Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			DevExpress.XtraPrinting.Shape.ShapeRectangle shapeRectangle1 = new DevExpress.XtraPrinting.Shape.ShapeRectangle();
			this.Detail = new DevExpress.XtraReports.UI.DetailBand();
			this.elementSubReport = new DevExpress.XtraReports.UI.XRSubreport();
			this.lblElementHeaderTitle = new DevExpress.XtraReports.UI.XRLabel();
			this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
			this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
			this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
			this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
			this.xrShape1 = new DevExpress.XtraReports.UI.XRShape();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// Detail
			// 
			this.Detail.HeightF = 0F;
			this.Detail.Name = "Detail";
			this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
			this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
			// 
			// elementSubReport
			// 
			this.elementSubReport.LocationFloat = new DevExpress.Utils.PointFloat(18F, 23.00002F);
			this.elementSubReport.Name = "elementSubReport";
			this.elementSubReport.SizeF = new System.Drawing.SizeF(716F, 48.79166F);
			// 
			// lblElementHeaderTitle
			// 
			this.lblElementHeaderTitle.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
			this.lblElementHeaderTitle.ForeColor = System.Drawing.Color.LightSlateGray;
			this.lblElementHeaderTitle.LocationFloat = new DevExpress.Utils.PointFloat(18F, 0F);
			this.lblElementHeaderTitle.Name = "lblElementHeaderTitle";
			this.lblElementHeaderTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.lblElementHeaderTitle.SizeF = new System.Drawing.SizeF(716F, 23.00001F);
			this.lblElementHeaderTitle.StylePriority.UseBorders = false;
			this.lblElementHeaderTitle.StylePriority.UseFont = false;
			this.lblElementHeaderTitle.StylePriority.UseForeColor = false;
			this.lblElementHeaderTitle.StylePriority.UseTextAlignment = false;
			this.lblElementHeaderTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
			// 
			// TopMargin
			// 
			this.TopMargin.HeightF = 32F;
			this.TopMargin.Name = "TopMargin";
			this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
			this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
			// 
			// BottomMargin
			// 
			this.BottomMargin.HeightF = 51F;
			this.BottomMargin.Name = "BottomMargin";
			this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
			this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
			// 
			// DetailReport
			// 
			this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1});
			this.DetailReport.Level = 0;
			this.DetailReport.Name = "DetailReport";
			// 
			// Detail1
			// 
			this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblElementHeaderTitle,
            this.elementSubReport,
            this.xrShape1});
			this.Detail1.HeightF = 71.79168F;
			this.Detail1.Name = "Detail1";
			// 
			// xrShape1
			// 
			this.xrShape1.BackColor = System.Drawing.Color.DimGray;
			this.xrShape1.LocationFloat = new DevExpress.Utils.PointFloat(5F, 7F);
			this.xrShape1.Name = "xrShape1";
			this.xrShape1.Shape = shapeRectangle1;
			this.xrShape1.SizeF = new System.Drawing.SizeF(10F, 10F);
			this.xrShape1.StylePriority.UseBackColor = false;
			// 
			// PEMR_PatientMedicalRecordReportSub_rpt
			// 
			this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.DetailReport});
			this.Margins = new System.Drawing.Printing.Margins(41, 42, 32, 51);
			this.PageHeight = 1169;
			this.PageWidth = 827;
			this.PaperKind = System.Drawing.Printing.PaperKind.A4;
			this.Version = "15.1";
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.XtraReports.UI.DetailBand Detail;
		private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
		private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
		private DevExpress.XtraReports.UI.XRLabel lblElementHeaderTitle;
		private DevExpress.XtraReports.UI.XRSubreport elementSubReport;
		private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
		private DevExpress.XtraReports.UI.DetailBand Detail1;
		private DevExpress.XtraReports.UI.XRShape xrShape1;
	}
}
