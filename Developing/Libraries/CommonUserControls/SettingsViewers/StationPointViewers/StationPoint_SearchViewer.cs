using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.StationPointViewers
{
	public partial class StationPoint_SearchViewer : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractSearchViewer<StationPoint_cu>,
		IStationPointViewer
	{
		public StationPoint_SearchViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_StationPoint_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<ServiceCategory_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.StationPoint_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "العيـــــادت"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_StationPoint_SearchViewer; }
		}

		#endregion

		#region Implementation of IStationPointViewer

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

		public object Station_P_ID
		{
			get { return lkeStation.EditValue; }
			set { lkeStation.EditValue = value; }
		}

		public object InternalCode
		{
			get { return txtInternalCode.EditValue; }
			set { txtInternalCode.EditValue = value; }
		}

		public object Description
		{
			get { return txtDescription.EditValue; }
			set { txtDescription.EditValue = value; }
		}

		#endregion
	}
}
