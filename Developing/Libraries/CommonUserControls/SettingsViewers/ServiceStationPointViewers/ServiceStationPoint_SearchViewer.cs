using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.ServiceStationPointViewers
{
	public partial class ServiceStationPoint_SearchViewer : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractSearchViewer<Service_StationPoint_cu>,
		IService_StationPointViewer
	{
		public ServiceStationPoint_SearchViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_ServiceStationPoint_SearchViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<Service_StationPoint_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.Service_StationPoint_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "ربــط الخـدمــات بالعيـــادات"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeStationPoint, StationPoint_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeService, Service_cu.ItemsList);
		}

		public override void ClearControls()
		{
			lkeStationPoint.EditValue = null;
			lkeService.EditValue = null;
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_Service_StationPoint_SearchViewer; }
		}

		#endregion

		#region Implementation of IService_StationPointViewer

		public object Service_CU_ID
		{
			get { return lkeService.EditValue; }
			set { lkeService.EditValue = value; }
		}

		public object StationPoint_CU_ID
		{
			get { return lkeStationPoint.EditValue; }
			set { lkeStationPoint.EditValue = value; }
		}

		#endregion
	}
}
