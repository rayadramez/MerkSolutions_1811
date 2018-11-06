using System;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.ServiceViewers
{
	public partial class Service_EditorViewer :
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<Service_cu>,
		IServiceViewer
	{
		public Service_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_Service_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<Service_cu>

		public override IMVCController<Service_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.Service_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "الخـدمــات"; }
		}

		public override void FillControls()
		{
			spnDefaultPrice.EditValue = null;
			ServiceType_P_ID = (int)DB_ServiceType.ExaminationService;
			CommonViewsActions.FillGridlookupEdit(lkeServiceCategory, ServiceCategory_cu.ItemsList);
		}

		public override void ClearControls()
		{
			txtNameP.EditValue = null;
			txtNameS.EditValue = null;
			txtInternalCode.EditValue = null;
			txtDescription.EditValue = null;
			lkeServiceCategory.EditValue = null;
			chkAllowAdmission.Checked = false;
			chkEnforceCategorization.Checked = false;
			chkIsDailyCharge.Checked = false;
			spnDefaultPrice.EditValue = null;
		}

		#endregion

		#region Implementation of IServiceViewer

		public object Name_P
		{
			get { return txtNameP.EditValue; }
			set { txtNameP.EditValue = value; }
		}

		public object Name_S
		{
			get { return txtNameS.EditValue; }
			set { txtNameS.EditValue = value; }
		}

		public object ServiceCategory_CU_ID
		{
			get { return lkeServiceCategory.EditValue; }
			set { lkeServiceCategory.EditValue = value; }
		}

		public object ServiceType_P_ID { get; set; }

		public object ParentService_CU_ID { get; set; }

		public object InternalCode
		{
			get { return txtInternalCode.EditValue; }
			set { txtInternalCode.EditValue = value; }
		}

		public object EnforceCategorization
		{
			get { return chkEnforceCategorization.Checked; }
			set { chkEnforceCategorization.Checked = Convert.ToBoolean(value); }
		}

		public object IsDailyCharged
		{
			get { return chkIsDailyCharge.Checked; }
			set { chkIsDailyCharge.Checked = Convert.ToBoolean(value); }
		}

		public object DefaultPriceFrom
		{
			get { return spnDefaultPrice.EditValue; }
			set { spnDefaultPrice.EditValue = value; }
		}

		public object DefaultPriceTo
		{
			get { return spnDefaultPrice.EditValue; }
			set { spnDefaultPrice.EditValue = value; }
		}

		public object AllowAddmission
		{
			get { return chkAllowAdmission.Checked; }
			set { chkAllowAdmission.Checked = Convert.ToBoolean(value); }
		}

		public object Description
		{
			get { return txtDescription.EditValue; }
			set { txtDescription.EditValue = value; }
		}

		#endregion

		private void chkExamination_CheckedChanged(object sender, EventArgs e)
		{
			ServiceType_P_ID = (int)DB_ServiceType.ExaminationService;
		}

		private void chkInvestigation_CheckedChanged(object sender, EventArgs e)
		{
			ServiceType_P_ID = (int)DB_ServiceType.InvestigationServices;
		}

		private void chkSurgery_CheckedChanged(object sender, EventArgs e)
		{
			ServiceType_P_ID = (int)DB_ServiceType.SurgeryService;
		}
	}
}
