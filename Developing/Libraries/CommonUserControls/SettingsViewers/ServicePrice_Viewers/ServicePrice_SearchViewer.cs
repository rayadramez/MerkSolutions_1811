using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.ServicePrice_Viewers
{
	public partial class ServicePrice_SearchViewer : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractSearchViewer<ServicePrice_cu>,
		IServicePrice_EditorViewer
	{
		public ServicePrice_SearchViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_ServicePrice_SearchViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<ServicePrice_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.ServicePrice_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "أسعـــــار الخــدمـــــات"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_ServicePrice_SearchViewer; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeServiceCategory, ServiceCategory_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeService, Service_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeDoctor, Doctor_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeDoctorCategory, DoctorCategory_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeDoctorCategory, InsuranceCarrier_cu.ItemsList);
		}

		#endregion

		#region Implementation of IServicePrice_EditorViewer

		public object Service_CU_ID
		{
			get { return lkeService.EditValue; }
			set { lkeService.EditValue = value; }
		}

		public object ServiceCategory_CU_ID
		{
			get { return lkeServiceCategory.EditValue; }
			set { lkeServiceCategory.EditValue = value; }
		}

		public object Doctor_CU_ID
		{
			get { return lkeDoctor.EditValue; }
			set { lkeDoctor.EditValue = value; }
		}

		public object DoctorCategory_CU_ID
		{
			get { return lkeDoctorCategory.EditValue; }
			set { lkeDoctorCategory.EditValue = value; }
		}

		public object Price { get; set; }

		public object InsuranceCarrierID { get; set; }

		public object InsuranceLevelID { get; set; }

		public object InsurancePrice { get; set; }

		#endregion
	}
}
