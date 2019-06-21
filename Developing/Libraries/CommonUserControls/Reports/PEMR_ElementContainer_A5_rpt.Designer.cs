namespace CommonUserControls.Reports
{
	partial class PEMR_ElementContainer_A5_rpt
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
			this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
			this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
			this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
			this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
			this.lblElementHeaderTitle = new DevExpress.XtraReports.UI.XRLabel();
			this.elementSubReport = new DevExpress.XtraReports.UI.XRSubreport();
			this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
			this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
			this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
			this.lblDate = new DevExpress.XtraReports.UI.XRLabel();
			this.lblPatientID = new DevExpress.XtraReports.UI.XRLabel();
			this.lblPatientName = new DevExpress.XtraReports.UI.XRLabel();
			this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
			this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
			this.lblDoctorName = new DevExpress.XtraReports.UI.XRLabel();
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
			// TopMargin
			// 
			this.TopMargin.HeightF = 32F;
			this.TopMargin.Name = "TopMargin";
			this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
			this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
			// 
			// BottomMargin
			// 
			this.BottomMargin.HeightF = 53F;
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
            this.elementSubReport});
			this.Detail1.HeightF = 71.79168F;
			this.Detail1.Name = "Detail1";
			// 
			// lblElementHeaderTitle
			// 
			this.lblElementHeaderTitle.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
			this.lblElementHeaderTitle.ForeColor = System.Drawing.Color.MidnightBlue;
			this.lblElementHeaderTitle.LocationFloat = new DevExpress.Utils.PointFloat(9.999993F, 0F);
			this.lblElementHeaderTitle.Name = "lblElementHeaderTitle";
			this.lblElementHeaderTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.lblElementHeaderTitle.SizeF = new System.Drawing.SizeF(476F, 23.00001F);
			this.lblElementHeaderTitle.StylePriority.UseBorders = false;
			this.lblElementHeaderTitle.StylePriority.UseFont = false;
			this.lblElementHeaderTitle.StylePriority.UseForeColor = false;
			this.lblElementHeaderTitle.StylePriority.UseTextAlignment = false;
			this.lblElementHeaderTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
			// 
			// elementSubReport
			// 
			this.elementSubReport.LocationFloat = new DevExpress.Utils.PointFloat(9.999993F, 23.00001F);
			this.elementSubReport.Name = "elementSubReport";
			this.elementSubReport.SizeF = new System.Drawing.SizeF(476F, 48.79166F);
			// 
			// ReportHeader
			// 
			this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine1,
            this.xrLabel6,
            this.lblDate,
            this.lblPatientID,
            this.lblPatientName,
            this.xrLabel2,
            this.xrLabel3,
            this.lblDoctorName,
            this.xrShape1});
			this.ReportHeader.HeightF = 72F;
			this.ReportHeader.Name = "ReportHeader";
			// 
			// xrLine1
			// 
			this.xrLine1.LineWidth = 2;
			this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(18F, 31.00001F);
			this.xrLine1.Name = "xrLine1";
			this.xrLine1.SizeF = new System.Drawing.SizeF(462.0001F, 6.333326F);
			// 
			// xrLabel6
			// 
			this.xrLabel6.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
			this.xrLabel6.ForeColor = System.Drawing.Color.Navy;
			this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(328.9999F, 37.33333F);
			this.xrLabel6.Name = "xrLabel6";
			this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel6.SizeF = new System.Drawing.SizeF(40.2916F, 23.00001F);
			this.xrLabel6.StylePriority.UseBorders = false;
			this.xrLabel6.StylePriority.UseFont = false;
			this.xrLabel6.StylePriority.UseForeColor = false;
			this.xrLabel6.StylePriority.UseTextAlignment = false;
			this.xrLabel6.Text = "Date :";
			this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
			// 
			// lblDate
			// 
			this.lblDate.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
			this.lblDate.ForeColor = System.Drawing.Color.Navy;
			this.lblDate.LocationFloat = new DevExpress.Utils.PointFloat(369.2915F, 37.33333F);
			this.lblDate.Name = "lblDate";
			this.lblDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.lblDate.SizeF = new System.Drawing.SizeF(110.7086F, 23.00002F);
			this.lblDate.StylePriority.UseBorders = false;
			this.lblDate.StylePriority.UseFont = false;
			this.lblDate.StylePriority.UseForeColor = false;
			this.lblDate.StylePriority.UseTextAlignment = false;
			this.lblDate.Text = "24-29-2018";
			this.lblDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
			// 
			// lblPatientID
			// 
			this.lblPatientID.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
			this.lblPatientID.ForeColor = System.Drawing.Color.Maroon;
			this.lblPatientID.LocationFloat = new DevExpress.Utils.PointFloat(18F, 8F);
			this.lblPatientID.Name = "lblPatientID";
			this.lblPatientID.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.lblPatientID.SizeF = new System.Drawing.SizeF(81.25F, 23.00001F);
			this.lblPatientID.StylePriority.UseBorders = false;
			this.lblPatientID.StylePriority.UseFont = false;
			this.lblPatientID.StylePriority.UseForeColor = false;
			this.lblPatientID.StylePriority.UseTextAlignment = false;
			this.lblPatientID.Text = "1922330";
			this.lblPatientID.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
			// 
			// lblPatientName
			// 
			this.lblPatientName.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
			this.lblPatientName.ForeColor = System.Drawing.Color.Maroon;
			this.lblPatientName.LocationFloat = new DevExpress.Utils.PointFloat(122.1667F, 7.999992F);
			this.lblPatientName.Name = "lblPatientName";
			this.lblPatientName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.lblPatientName.SizeF = new System.Drawing.SizeF(357.8334F, 23.00001F);
			this.lblPatientName.StylePriority.UseBorders = false;
			this.lblPatientName.StylePriority.UseFont = false;
			this.lblPatientName.StylePriority.UseForeColor = false;
			this.lblPatientName.StylePriority.UseTextAlignment = false;
			this.lblPatientName.Text = "Mohsen Abd El Hameed Hafez";
			this.lblPatientName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
			// 
			// xrLabel2
			// 
			this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
			this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(99.25F, 7.999992F);
			this.xrLabel2.Name = "xrLabel2";
			this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel2.SizeF = new System.Drawing.SizeF(22.91666F, 23.00002F);
			this.xrLabel2.StylePriority.UseBorders = false;
			this.xrLabel2.StylePriority.UseFont = false;
			this.xrLabel2.StylePriority.UseTextAlignment = false;
			this.xrLabel2.Text = "/";
			this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
			// 
			// xrLabel3
			// 
			this.xrLabel3.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
			this.xrLabel3.ForeColor = System.Drawing.Color.Navy;
			this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(18F, 37.33333F);
			this.xrLabel3.Name = "xrLabel3";
			this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel3.SizeF = new System.Drawing.SizeF(59.37331F, 23.00002F);
			this.xrLabel3.StylePriority.UseBorders = false;
			this.xrLabel3.StylePriority.UseFont = false;
			this.xrLabel3.StylePriority.UseForeColor = false;
			this.xrLabel3.StylePriority.UseTextAlignment = false;
			this.xrLabel3.Text = "Doctor :";
			this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
			// 
			// lblDoctorName
			// 
			this.lblDoctorName.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
			this.lblDoctorName.ForeColor = System.Drawing.Color.Navy;
			this.lblDoctorName.LocationFloat = new DevExpress.Utils.PointFloat(77.37331F, 37.33333F);
			this.lblDoctorName.Name = "lblDoctorName";
			this.lblDoctorName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.lblDoctorName.SizeF = new System.Drawing.SizeF(227.7917F, 23.00001F);
			this.lblDoctorName.StylePriority.UseBorders = false;
			this.lblDoctorName.StylePriority.UseFont = false;
			this.lblDoctorName.StylePriority.UseForeColor = false;
			this.lblDoctorName.StylePriority.UseTextAlignment = false;
			this.lblDoctorName.Text = "Wael Shaker Abd El Hamed Sayed";
			this.lblDoctorName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
			// 
			// xrShape1
			// 
			this.xrShape1.LocationFloat = new DevExpress.Utils.PointFloat(9.999998F, 0F);
			this.xrShape1.Name = "xrShape1";
			this.xrShape1.Shape = shapeRectangle1;
			this.xrShape1.SizeF = new System.Drawing.SizeF(476F, 70.83F);
			// 
			// PEMR_ElementContainer_A5_rpt
			// 
			this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.DetailReport,
            this.ReportHeader});
			this.Margins = new System.Drawing.Printing.Margins(30, 38, 32, 53);
			this.PageHeight = 827;
			this.PageWidth = 583;
			this.PaperKind = System.Drawing.Printing.PaperKind.A5;
			this.Version = "15.1";
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.XtraReports.UI.DetailBand Detail;
		private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
		private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
		private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
		private DevExpress.XtraReports.UI.DetailBand Detail1;
		private DevExpress.XtraReports.UI.XRSubreport elementSubReport;
		private DevExpress.XtraReports.UI.XRLabel lblElementHeaderTitle;
		private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
		private DevExpress.XtraReports.UI.XRLabel lblPatientID;
		private DevExpress.XtraReports.UI.XRLabel lblPatientName;
		private DevExpress.XtraReports.UI.XRLabel xrLabel2;
		private DevExpress.XtraReports.UI.XRLabel xrLabel6;
		private DevExpress.XtraReports.UI.XRLabel lblDate;
		private DevExpress.XtraReports.UI.XRShape xrShape1;
		private DevExpress.XtraReports.UI.XRLabel xrLabel3;
		private DevExpress.XtraReports.UI.XRLine xrLine1;
		private DevExpress.XtraReports.UI.XRLabel lblDoctorName;
	}
}
