using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraLayout.Utils;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace CommonUserControls.PEMRCommonViewers.PEMR_InternalViewers.Ophthalmology
{
	public enum ReadingsType
	{
		VisionAndRefractionRading = 1,
		EOMReading = 2
	}

	public partial class PEMR_VisionRefractionGraph_UC : UserControl
	{
		private object PatientID { get; set; }
		private object DateFrom { get; set; }
		private object DateTo { get; set; }
		private ReadingsType ReadingsType { get; set; }

		public PEMR_VisionRefractionGraph_UC()
		{
			InitializeComponent();
			SetChartControl(true, true, true);
			SetChartControl(true, true, true, true, true, true);
		}

		public void Initialize(ReadingsType readingsType, object patientID, object dateFrom, object dateTo)
		{
			ReadingsType = readingsType;
			PatientID = patientID;
			DateFrom = dateFrom;
			DateTo = dateTo;

			SetChartControl(ReadingsType);
			SetControls(ReadingsType);

			if (dateFrom != null)
				DateFrom = Convert.ToDateTime(DateFrom).Date;
			if(dateTo != null)
				DateTo = Convert.ToDateTime(DateTo).Date;

			switch (readingsType)
			{
				case ReadingsType.VisionAndRefractionRading:
					List<GetPreviousVisitTiming_VisionRefractionReading_Result> list_VisionRefraction =
						PEMRBusinessLogic.GetPrevious_VisitTiming_VisionRefractionReading(patientID, DateFrom, DateTo);
					if (list_VisionRefraction != null)
						list_VisionRefraction = list_VisionRefraction.OrderByDescending(item => item.TakenDateTime).ToList();
					chartControl1.DataSource = chartControl2.DataSource = list_VisionRefraction;
					break;
				case ReadingsType.EOMReading:
					List<GetPreviousVisitTiming_EOMReading_Result> list_EOMReading =
						PEMRBusinessLogic.GetPrevious_VisitTiming_EOMReading_Result(patientID, DateFrom, DateTo);
					if (list_EOMReading != null)
						list_EOMReading = list_EOMReading.OrderByDescending(item => item.TakenDateTime).ToList();
					chartControl1.DataSource = chartControl2.DataSource = list_EOMReading;
					break;
			}
		}

		public void SetChartControl(ReadingsType readingMode)
		{
			switch (readingMode)
			{
				case ReadingsType.VisionAndRefractionRading:

					#region OD

					Series sph_Series_OD = new Series();
					sph_Series_OD.View = new LineSeriesView();
					sph_Series_OD.Name = "Sphere";
					sph_Series_OD.ArgumentDataMember = "TakenDateTime";
					sph_Series_OD.ArgumentScaleType = ScaleType.DateTime;
					sph_Series_OD.ValueDataMembers.AddRange("Sph_OD");
					sph_Series_OD.ValueScaleType = ScaleType.Numerical;
					chartControl1.Series.Add(sph_Series_OD);

					Series cyl_Series_OD = new Series();
					cyl_Series_OD.View = new LineSeriesView();
					cyl_Series_OD.Name = "Cylinder";
					cyl_Series_OD.ArgumentDataMember = "TakenDateTime";
					cyl_Series_OD.ArgumentScaleType = ScaleType.DateTime;
					cyl_Series_OD.ValueDataMembers.AddRange("Cyl_OD");
					cyl_Series_OD.ValueScaleType = ScaleType.Numerical;
					chartControl1.Series.Add(cyl_Series_OD);

					Series axis_Series_OD = new Series();
					axis_Series_OD.View = new LineSeriesView();
					axis_Series_OD.Name = "Axis";
					axis_Series_OD.ArgumentDataMember = "TakenDateTime";
					axis_Series_OD.ArgumentScaleType = ScaleType.DateTime;
					axis_Series_OD.ValueDataMembers.AddRange("Axis_OD");
					axis_Series_OD.ValueScaleType = ScaleType.Numerical;
					chartControl1.Series.Add(axis_Series_OD);

					Series add_Series_OD = new Series();
					add_Series_OD.View = new LineSeriesView();
					add_Series_OD.Name = "Add";
					add_Series_OD.ArgumentDataMember = "TakenDateTime";
					add_Series_OD.ArgumentScaleType = ScaleType.DateTime;
					add_Series_OD.ValueDataMembers.AddRange("Add_OD");
					add_Series_OD.ValueScaleType = ScaleType.Numerical;
					chartControl1.Series.Add(add_Series_OD);

					#endregion

					#region OS

					Series sph_Series_OS = new Series();
					sph_Series_OS.View = new LineSeriesView();
					sph_Series_OS.Name = "Sphere";
					sph_Series_OS.ArgumentDataMember = "TakenDateTime";
					sph_Series_OS.ArgumentScaleType = ScaleType.DateTime;
					sph_Series_OS.ValueDataMembers.AddRange("Sph_OS");
					sph_Series_OS.ValueScaleType = ScaleType.Numerical;
					chartControl2.Series.Add(sph_Series_OS);

					Series cyl_Series_OS = new Series();
					cyl_Series_OS.View = new LineSeriesView();
					cyl_Series_OS.Name = "Cylinder";
					cyl_Series_OS.ArgumentDataMember = "TakenDateTime";
					cyl_Series_OS.ArgumentScaleType = ScaleType.DateTime;
					cyl_Series_OS.ValueDataMembers.AddRange("Cyl_OS");
					cyl_Series_OS.ValueScaleType = ScaleType.Numerical;
					chartControl2.Series.Add(cyl_Series_OS);

					Series axis_Series_OS = new Series();
					axis_Series_OS.View = new LineSeriesView();
					axis_Series_OS.Name = "Axis";
					axis_Series_OS.ArgumentDataMember = "TakenDateTime";
					axis_Series_OS.ArgumentScaleType = ScaleType.DateTime;
					axis_Series_OS.ValueDataMembers.AddRange("Axis_OS");
					axis_Series_OS.ValueScaleType = ScaleType.Numerical;
					chartControl2.Series.Add(axis_Series_OS);

					Series add_Series_OS = new Series();
					add_Series_OS.View = new LineSeriesView();
					add_Series_OS.Name = "Add";
					add_Series_OS.ArgumentDataMember = "TakenDateTime";
					add_Series_OS.ArgumentScaleType = ScaleType.DateTime;
					add_Series_OS.ValueDataMembers.AddRange("Add_OS");
					add_Series_OS.ValueScaleType = ScaleType.Numerical;
					chartControl2.Series.Add(add_Series_OS);

					#endregion

					break;
				case ReadingsType.EOMReading:

					#region OD

					Series SR_Series_OD = new Series();
					SR_Series_OD.View = new LineSeriesView();
					SR_Series_OD.Name = "SR";
					SR_Series_OD.ArgumentDataMember = "TakenDateTime";
					SR_Series_OD.ArgumentScaleType = ScaleType.DateTime;
					SR_Series_OD.ValueDataMembers.AddRange("SR_OD");
					SR_Series_OD.ValueScaleType = ScaleType.Numerical;
					chartControl1.Series.Add(SR_Series_OD);

					Series LR_Series_OD = new Series();
					LR_Series_OD.View = new LineSeriesView();
					LR_Series_OD.Name = "LR";
					LR_Series_OD.ArgumentDataMember = "TakenDateTime";
					LR_Series_OD.ArgumentScaleType = ScaleType.DateTime;
					LR_Series_OD.ValueDataMembers.AddRange("LR_OD");
					LR_Series_OD.ValueScaleType = ScaleType.Numerical;
					chartControl1.Series.Add(LR_Series_OD);

					Series IR_Series_OD = new Series();
					IR_Series_OD.View = new LineSeriesView();
					IR_Series_OD.Name = "IR";
					IR_Series_OD.ArgumentDataMember = "TakenDateTime";
					IR_Series_OD.ArgumentScaleType = ScaleType.DateTime;
					IR_Series_OD.ValueDataMembers.AddRange("IR_OD");
					IR_Series_OD.ValueScaleType = ScaleType.Numerical;
					chartControl1.Series.Add(IR_Series_OD);

					Series IO_Series_OD = new Series();
					IO_Series_OD.View = new LineSeriesView();
					IO_Series_OD.Name = "IO";
					IO_Series_OD.ArgumentDataMember = "TakenDateTime";
					IO_Series_OD.ArgumentScaleType = ScaleType.DateTime;
					IO_Series_OD.ValueDataMembers.AddRange("IO_OD");
					IO_Series_OD.ValueScaleType = ScaleType.Numerical;
					chartControl1.Series.Add(IO_Series_OD);

					Series MR_Series_OD = new Series();
					MR_Series_OD.View = new LineSeriesView();
					MR_Series_OD.Name = "MR";
					MR_Series_OD.ArgumentDataMember = "TakenDateTime";
					MR_Series_OD.ArgumentScaleType = ScaleType.DateTime;
					MR_Series_OD.ValueDataMembers.AddRange("MR_OD");
					MR_Series_OD.ValueScaleType = ScaleType.Numerical;
					chartControl1.Series.Add(MR_Series_OD);

					Series SO_Series_OD = new Series();
					SO_Series_OD.View = new LineSeriesView();
					SO_Series_OD.Name = "SO";
					SO_Series_OD.ArgumentDataMember = "TakenDateTime";
					SO_Series_OD.ArgumentScaleType = ScaleType.DateTime;
					SO_Series_OD.ValueDataMembers.AddRange("SO_OD");
					SO_Series_OD.ValueScaleType = ScaleType.Numerical;
					chartControl1.Series.Add(SO_Series_OD);

					#endregion

					#region OS

					Series SR_Series_OS = new Series();
					SR_Series_OS.View = new LineSeriesView();
					SR_Series_OS.Name = "SR";
					SR_Series_OS.ArgumentDataMember = "TakenDateTime";
					SR_Series_OS.ArgumentScaleType = ScaleType.DateTime;
					SR_Series_OS.ValueDataMembers.AddRange("SR_OS");
					SR_Series_OS.ValueScaleType = ScaleType.Numerical;
					chartControl2.Series.Add(SR_Series_OS);

					Series LR_Series_OS = new Series();
					LR_Series_OS.View = new LineSeriesView();
					LR_Series_OS.Name = "LR";
					LR_Series_OS.ArgumentDataMember = "TakenDateTime";
					LR_Series_OS.ArgumentScaleType = ScaleType.DateTime;
					LR_Series_OS.ValueDataMembers.AddRange("LR_OS");
					LR_Series_OS.ValueScaleType = ScaleType.Numerical;
					chartControl2.Series.Add(LR_Series_OS);

					Series IR_Series_OS = new Series();
					IR_Series_OS.View = new LineSeriesView();
					IR_Series_OS.Name = "IR";
					IR_Series_OS.ArgumentDataMember = "TakenDateTime";
					IR_Series_OS.ArgumentScaleType = ScaleType.DateTime;
					IR_Series_OS.ValueDataMembers.AddRange("IR_OS");
					IR_Series_OS.ValueScaleType = ScaleType.Numerical;
					chartControl2.Series.Add(IR_Series_OS);

					Series IO_Series_OS = new Series();
					IO_Series_OS.View = new LineSeriesView();
					IO_Series_OS.Name = "IO";
					IO_Series_OS.ArgumentDataMember = "TakenDateTime";
					IO_Series_OS.ArgumentScaleType = ScaleType.DateTime;
					IO_Series_OS.ValueDataMembers.AddRange("IO_OS");
					IO_Series_OS.ValueScaleType = ScaleType.Numerical;
					chartControl2.Series.Add(IO_Series_OS);

					Series MR_Series_OS = new Series();
					MR_Series_OS.Name = "MR";
					MR_Series_OS.ArgumentDataMember = "TakenDateTime";
					MR_Series_OS.ArgumentScaleType = ScaleType.DateTime;
					MR_Series_OS.ValueDataMembers.AddRange("MR_OS");
					MR_Series_OS.ValueScaleType = ScaleType.Numerical;
					chartControl2.Series.Add(MR_Series_OS);

					Series SO_Series_OS = new Series();
					SO_Series_OS.View = new LineSeriesView();
					SO_Series_OS.Name = "SO";
					SO_Series_OS.ArgumentDataMember = "TakenDateTime";
					SO_Series_OS.ArgumentScaleType = ScaleType.DateTime;
					SO_Series_OS.ValueDataMembers.AddRange("SO_OS");
					SO_Series_OS.ValueScaleType = ScaleType.Numerical;
					chartControl2.Series.Add(SO_Series_OS);

					#endregion

					break;
			}
		}

		public void SetControls(ReadingsType readingsType)
		{
			switch (readingsType)
			{
				case ReadingsType.VisionAndRefractionRading:
					lyt_Axis.Visibility = lyt_Sphere.Visibility = lyt_Cylinder.Visibility = LayoutVisibility.Always;
					break;
				case ReadingsType.EOMReading:
					lyt_SR.Visibility = lyt_LR.Visibility = lyt_IR.Visibility = lyt_IO.Visibility =
						lyt_MR.Visibility = lyt_SO.Visibility = LayoutVisibility.Always;
					break;
			}
		}

		public void SetChartControl(bool showSphere, bool showCylinder, bool showAxis)
		{
			foreach (Series series in chartControl1.Series)
			{
				if (series.SeriesID == 0)
					series.Visible = showSphere;
				if (series.SeriesID == 1)
					series.Visible = showCylinder;
				if (series.SeriesID == 2)
					series.Visible = showAxis;
			}
			foreach (Series series in chartControl2.Series)
			{
				if (series.SeriesID == 0)
					series.Visible = showSphere;
				if (series.SeriesID == 1)
					series.Visible = showCylinder;
				if (series.SeriesID == 2)
					series.Visible = showAxis;
			}

			chartControl1.RefreshData();
			chartControl1.Refresh();
			chartControl2.RefreshData();
			chartControl2.Refresh();
		}

		public void SetChartControl(bool show_SR, bool show_LR, bool show_IR, bool show_IO, bool show_MR, bool show_SO)
		{
			foreach (Series series in chartControl1.Series)
			{
				if (series.SeriesID == 0)
					series.Visible = show_SR;
				if (series.SeriesID == 1)
					series.Visible = show_LR;
				if (series.SeriesID == 2)
					series.Visible = show_IR;
				if (series.SeriesID == 3)
					series.Visible = show_IO;
				if (series.SeriesID == 4)
					series.Visible = show_MR;
				if (series.SeriesID == 5)
					series.Visible = show_SO;
			}

			foreach (Series series in chartControl2.Series)
			{
				if (series.SeriesID == 0)
					series.Visible = show_SR;
				if (series.SeriesID == 1)
					series.Visible = show_LR;
				if (series.SeriesID == 2)
					series.Visible = show_IR;
				if (series.SeriesID == 3)
					series.Visible = show_IO;
				if (series.SeriesID == 4)
					series.Visible = show_MR;
				if (series.SeriesID == 5)
					series.Visible = show_SO;
			}

			chartControl1.RefreshData();
			chartControl1.Refresh();
			chartControl2.RefreshData();
			chartControl2.Refresh();
		}

		private void chkAll_CheckedChanged(object sender, System.EventArgs e)
		{
			SetChartControl(true, true, true);
			SetChartControl(true, true, true, true, true, true);
		}

		private void chkSphere_CheckedChanged(object sender, System.EventArgs e)
		{
			SetChartControl(true, false, false);
		}

		private void chkCylinder_CheckedChanged(object sender, System.EventArgs e)
		{
			SetChartControl(false, true, false);
		}

		private void chkAxis_CheckedChanged(object sender, System.EventArgs e)
		{
			SetChartControl(false, false, true);
		}

		private void chk_SR_CheckedChanged(object sender, EventArgs e)
		{
			SetChartControl(true, false, false, false, false, false);
		}

		private void chk_LR_CheckedChanged(object sender, EventArgs e)
		{
			SetChartControl(false, true, false, false, false, false);
		}

		private void chk_IR_CheckedChanged(object sender, EventArgs e)
		{
			SetChartControl(false, false, true, false, false, false);
		}

		private void chk_IO_CheckedChanged(object sender, EventArgs e)
		{
			SetChartControl(false, false, false, true, false, false);
		}

		private void chk_MR_CheckedChanged(object sender, EventArgs e)
		{
			SetChartControl(false, false, false, false, true, false);
		}

		private void chk_SO_CheckedChanged(object sender, EventArgs e)
		{
			SetChartControl(false, false, false, false, false, true);
		}

		private void btnFullScreen_Click(object sender, System.EventArgs e)
		{
			if(ParentForm != null)
				ParentForm.Close();
		}

	}
}
