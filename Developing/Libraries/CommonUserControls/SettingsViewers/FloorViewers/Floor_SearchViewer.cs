using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.FloorViewers
{
	public partial class Floor_SearchViewer : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractSearchViewer<Floor_cu>,
		IFloorViewer
	{
		public Floor_SearchViewer()
		{
			InitializeComponent();
			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_FloorEditor);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<Floor_cu>

		public override object ViewerID
		{
			get { return ViewerName.FloorViewer; }
		}

		public override string HeaderTitle
		{
			get { return "الأدوار"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_FloorSearch; }
		}

		#endregion

		#region Implementation of IFloorViewer

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

		public object Location_CU_ID
		{
			get { return lkeLocation.EditValue; }
			set { lkeLocation.EditValue = value; }
		}

		public object ShortName
		{
			get { return txtShortName.EditValue; }
			set { txtShortName.EditValue = value; }
		}
		public object Description { get; set; }

		public object InternalCode
		{
			get { return txtInternalCode.EditValue; }
			set { txtInternalCode.EditValue = value; }
		}

		#endregion
	}
}
