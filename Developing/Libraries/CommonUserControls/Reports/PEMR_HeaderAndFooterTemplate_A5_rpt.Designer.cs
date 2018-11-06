namespace CommonUserControls.Reports
{
	partial class PEMR_HeaderAndFooterTemplate_A5_rpt
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PEMR_HeaderAndFooterTemplate_A5_rpt));
			this.Detail = new DevExpress.XtraReports.UI.DetailBand();
			this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
			this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
			this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
			this.reportHeader = new DevExpress.XtraReports.UI.XRRichText();
			this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
			this.lblRightsAreReserved = new DevExpress.XtraReports.UI.XRLabel();
			this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
			this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
			this.xrSubreport2 = new DevExpress.XtraReports.UI.XRSubreport();
			((System.ComponentModel.ISupportInitialize)(this.reportHeader)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// Detail
			// 
			this.Detail.HeightF = 0F;
			this.Detail.Name = "Detail";
			this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
			this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
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
			// PageHeader
			// 
			this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.reportHeader});
			this.PageHeader.HeightF = 70F;
			this.PageHeader.Name = "PageHeader";
			// 
			// reportHeader
			// 
			this.reportHeader.Borders = DevExpress.XtraPrinting.BorderSide.None;
			this.reportHeader.CanShrink = true;
			this.reportHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F);
			this.reportHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
			this.reportHeader.Name = "reportHeader";
			this.reportHeader.SerializableRtfString = resources.GetString("reportHeader.SerializableRtfString");
			this.reportHeader.SizeF = new System.Drawing.SizeF(500F, 65.41666F);
			this.reportHeader.StylePriority.UseBorderColor = false;
			this.reportHeader.StylePriority.UseBorders = false;
			this.reportHeader.StylePriority.UseBorderWidth = false;
			// 
			// PageFooter
			// 
			this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblRightsAreReserved});
			this.PageFooter.HeightF = 28F;
			this.PageFooter.Name = "PageFooter";
			// 
			// lblRightsAreReserved
			// 
			this.lblRightsAreReserved.Borders = DevExpress.XtraPrinting.BorderSide.Top;
			this.lblRightsAreReserved.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRightsAreReserved.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.lblRightsAreReserved.LocationFloat = new DevExpress.Utils.PointFloat(0F, 5F);
			this.lblRightsAreReserved.Multiline = true;
			this.lblRightsAreReserved.Name = "lblRightsAreReserved";
			this.lblRightsAreReserved.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.lblRightsAreReserved.SizeF = new System.Drawing.SizeF(500F, 23F);
			this.lblRightsAreReserved.StylePriority.UseBorderColor = false;
			this.lblRightsAreReserved.StylePriority.UseBorders = false;
			this.lblRightsAreReserved.StylePriority.UseFont = false;
			this.lblRightsAreReserved.StylePriority.UseForeColor = false;
			this.lblRightsAreReserved.StylePriority.UseTextAlignment = false;
			this.lblRightsAreReserved.Text = "All rights are reserved to MERK-Solutions.com";
			this.lblRightsAreReserved.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
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
            this.xrSubreport2});
			this.Detail1.HeightF = 100F;
			this.Detail1.Name = "Detail1";
			// 
			// xrSubreport2
			// 
			this.xrSubreport2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
			this.xrSubreport2.Name = "xrSubreport2";
			this.xrSubreport2.SizeF = new System.Drawing.SizeF(500F, 99.99999F);
			// 
			// PEMR_HeaderAndFooterTemplate_A5_rpt
			// 
			this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter,
            this.DetailReport});
			this.Margins = new System.Drawing.Printing.Margins(41, 42, 32, 51);
			this.PageHeight = 827;
			this.PageWidth = 583;
			this.PaperKind = System.Drawing.Printing.PaperKind.A5;
			this.Version = "15.1";
			((System.ComponentModel.ISupportInitialize)(this.reportHeader)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.XtraReports.UI.DetailBand Detail;
		private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
		private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
		private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
		private DevExpress.XtraReports.UI.XRRichText reportHeader;
		private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
		private DevExpress.XtraReports.UI.XRLabel lblRightsAreReserved;
		private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
		private DevExpress.XtraReports.UI.DetailBand Detail1;
		private DevExpress.XtraReports.UI.XRSubreport xrSubreport2;
	}
}
