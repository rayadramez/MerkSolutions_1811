using System;
using System.Drawing;
using ApplicationConfiguration;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.ReportsContainer
{
	public partial class TotalServiceAndDoctorRevenues_Report : 
		//UserControl
		CommonAbstractSearchViewer<GetTotalServiceAndDoctorRevenues_Result>,
		ITotalServiceAndDoctorRevenues_Report_Viewer
	{
		public TotalServiceAndDoctorRevenues_Report()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_TotalServiceAndDoctorRevenues_Report);
			CommonViewsActions.SetupSyle(this);

			switch (ApplicationStaticConfiguration.Organization)
			{
				case DB_Organization.Cardiovascular_Clinic:
					chkInAndNotPrivate.Enabled = false;
					chkOutAndNotPrivate.Enabled = false;
					chkInAndPrivate.Enabled = false;
					break;
			}

			if (!string.IsNullOrEmpty(ApplicationStaticConfiguration.SkinName) &&
				!string.IsNullOrWhiteSpace(ApplicationStaticConfiguration.SkinName))
				if (!ApplicationStaticConfiguration.SkinName.Equals("McSkin"))
				{
					layoutControlGroup3.AppearanceGroup.ForeColor = Color.OldLace;
					layoutControlGroup4.AppearanceGroup.ForeColor = Color.OldLace;
					layoutControlGroup2.AppearanceGroup.ForeColor = Color.OldLace;
					layoutControlGroup5.AppearanceGroup.ForeColor = Color.OldLace;
				}
				else
				{
					layoutControlGroup3.AppearanceGroup.ForeColor = Color.Navy;
					layoutControlGroup4.AppearanceGroup.ForeColor = Color.Navy;
					layoutControlGroup2.AppearanceGroup.ForeColor = Color.Navy;
					layoutControlGroup5.AppearanceGroup.ForeColor = Color.OldLace;
				}
		}

		#region Overrides of CommonAbstractViewer<GetTotalServiceAndDoctorRevenues_Result>

		public override object ViewerID
		{
			get { return (int)ViewerName.TotalServiceAndDoctorRevenues_Report_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return string.Empty; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_TotalServicesAndDoctorsRevenues; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeDoctors, Doctor_cu.ItemsList, "FullName", "Person_CU_ID");
			CommonViewsActions.FillGridlookupEdit(lkeServiceCategories, ServiceCategory_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeSerivces, Service_cu.ItemsList);
		}

		#endregion

		#region Implementation of ITotalServiceAndDoctorRevenues_Report_Viewer

		public object InvoiceTypeID
		{
			get
			{
				if (chkInOut_All.Checked)
					return null;
				if (chkInAndPrivate.Checked)
					return (int)DB_InvoiceType.InPatientPrivate;
				if (chkInAndNotPrivate.Checked)
					return (int)DB_InvoiceType.InPatientNotPrivate;
				if (chkOutAndPrivate.Checked)
					return (int)DB_InvoiceType.OutPatientPrivate;
				if (chkOutAndNotPrivate.Checked)
					return (int)DB_InvoiceType.OutPatientNotPrivate;

				return null;
			}
		}

		public object ServiceID
		{
			get { return lkeSerivces.EditValue; }
		}

		public object ServiceTypeID
		{
			get
			{
				if (chkExamination.Checked)
					return (int) DB_ServiceType.ExaminationService;
				if(chkInvestigation.Checked)
					return (int)DB_ServiceType.InvestigationServices;
				if (chkServiceTypeALl.Checked)
					return null;
				return null;
			}
		}

		public object ServiceCategoryID
		{
			get { return lkeServiceCategories.EditValue; }
		}

		public object DoctorID
		{
			get { return lkeDoctors.EditValue; }
		}

		public object IsOnDuty
		{
			get
			{
				if (chkIsOnDuty.Checked)
					return true;
				if (chkIsNotOnDuty.Checked)
					return false;
				if (chkIsOnDuty_All.Checked)
					return null;
				return null;
			}
		}

		public object FromDate
		{
			get { return dtDateFrom.EditValue; }
		}

		public object ToDate
		{
			get { return dtDateTo.EditValue; }
		}

		#endregion

		private void lkeServiceCategories_EditValueChanged(object sender, System.EventArgs e)
		{
			if (lkeServiceCategories.EditValue == null)
			{
				CommonViewsActions.FillGridlookupEdit(lkeSerivces, Service_cu.ItemsList);
				return;
			}

			CommonViewsActions.FillGridlookupEdit(lkeSerivces,
				Service_cu.ItemsList.FindAll(
					item => Convert.ToInt32(item.ServiceCategory_CU_ID).Equals(Convert.ToInt32(lkeServiceCategories.EditValue))));
		}

		private void chkDate_CheckedChanged(object sender, System.EventArgs e)
		{
			dtDateFrom.Properties.ReadOnly = dtDateTo.Properties.ReadOnly = chkDate.Checked;
			if (chkDate.Checked)
			{
				dtDateFrom.EditValue = null;
				dtDateTo.EditValue = null;
			}
		}

	}
}
