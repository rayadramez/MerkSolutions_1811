using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.ServiceCategoryViewers
{
	public partial class ServiceCategory_EditorViewer : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<ServiceCategory_cu>,
		IServiceCategoryViewer
	{
		public ServiceCategory_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_ServiceCategory_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<ServiceCategory_cu>

		public override IMVCController<ServiceCategory_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.ServiceCategory_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "تصنيفـــات الخـدمــات"; }
		}

		public override void FillControls()
		{
			ServiceType = (int) DB_ServiceType.ExaminationService;
		}

		public override void ClearControls()
		{
			txtNameP.EditValue = null;
			txtNameS.EditValue = null;
			txtInternalCode.EditValue = null;
			txtDescription.EditValue = null;
			chkAllowAdmission.Checked = false;
			chkIsMedicalServiceCategory.Checked = false;
		}

		#endregion

		#region Implementation of IServiceCategoryViewer

		public object NameP
		{
			get { return txtNameP.EditValue; }
			set { txtNameP.EditValue = value; }
		}

		public object NameS
		{
			get { return txtNameS.EditValue; }
			set { txtNameS.EditValue = value; }
		}

		public object InternalCode
		{
			get { return txtInternalCode.EditValue; }
			set { txtInternalCode.EditValue = value; }
		}

		public object ServiceType { get; set; }

		public object IsMedical
		{
			get { return chkIsMedicalServiceCategory.Checked; }
			set { chkIsMedicalServiceCategory.Checked = (bool) value; }
		}

		public object AlloAdmission
		{
			get { return chkAllowAdmission.Checked; }
			set { chkAllowAdmission.Checked = (bool)value; }
		}

		public object Description
		{
			get { return txtDescription.EditValue; }
			set { txtDescription.EditValue = value; }
		}

		#endregion

		private void chkIsMedicalServiceCategory_CheckedChanged(object sender, System.EventArgs e)
		{
			chkAllowAdmission.Checked = chkIsMedicalServiceCategory.Checked;
		}

		private void chkExamination_CheckedChanged(object sender, System.EventArgs e)
		{
			ServiceType = (int) DB_ServiceType.ExaminationService;
		}

		private void chkInvestigation_CheckedChanged(object sender, System.EventArgs e)
		{
			ServiceType = (int)DB_ServiceType.InvestigationServices;
		}

		private void chkSurgery_CheckedChanged(object sender, System.EventArgs e)
		{
			ServiceType = (int)DB_ServiceType.SurgeryService;
		}
	}
}
