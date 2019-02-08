using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.OrganizationMachineViewers
{
	public partial class OrganizationMachine_SearchViewer :
		//UserControl
		CommonAbstractSearchViewer<OrganizationMachine_cu>,
		IOrganizationMachine_Viewer
	{
		public OrganizationMachine_SearchViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_OrganizationMachine_SearchViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<OrganizationMachine_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.OrganizationMachine_viewer; }
		}

		public override string HeaderTitle
		{
			get { return ""; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeStationPoints, StationPoint_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeStationPointStages, StationPointStage_cu.ItemsList);
		}

		public override void ClearControls()
		{
			txtNameP.EditValue = null;
			lkeStationPoints.EditValue = null;
			lkeStationPointStages.EditValue = null;
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_OrganizationMachine_SearchViewer; }
		}

		#endregion

		#region Implementation of IOrganizationMachine_Viewer

		public object Name_P
		{
			get { return txtNameP.EditValue; }
			set { txtNameP.EditValue = value; }
		}

		public object StationPoint_CU_ID
		{
			get { return lkeStationPoints.EditValue; }
			set { lkeStationPoints.EditValue = value; }
		}

		public object StationPointStage_CU_ID
		{
			get { return lkeStationPointStages.EditValue; }
			set { lkeStationPointStages.EditValue = value; }
		}

		public object OrganizationID { get; set; }

		public object SkinName { get; set; }

		public object Color { get; set; }

		#endregion
	}
}
