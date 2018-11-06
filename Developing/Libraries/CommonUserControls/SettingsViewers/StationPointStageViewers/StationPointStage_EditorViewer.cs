using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.StationPointStageViewers
{
	public partial class StationPointStage_EditorViewer : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<StationPointStage_cu>,
		IStationPointStageViewer
	{
		public StationPointStage_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_StationPointStage_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<StationPointStage_cu>

		public override IMVCController<StationPointStage_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.StationPointStage_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "مـراحـــل العيــــادات"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeStationPoint, StationPoint_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeFloor, Floor_cu.ItemsList, "FloorFullName");
		}

		public override void ClearControls()
		{
			txtNameP.EditValue = null;
			txtNameS.EditValue = null;
			txtInternalCode.EditValue = null;
			txtDescription.EditValue = null;
			lkeFloor.EditValue = null;
			lkeStationPoint.EditValue = null;
			spnOrderIndex.EditValue = null;
		}

		#endregion

		#region Implementation of IStationPointStageViewer

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

		public object StationPoint_CU_ID
		{
			get { return lkeStationPoint.EditValue; }
			set { lkeStationPoint.EditValue = value; }
		}

		public object Floor_CU_ID
		{
			get { return lkeFloor.EditValue; }
			set { lkeFloor.EditValue = value; }
		}

		public object OrderIndex
		{
			get { return spnOrderIndex.EditValue; }
			set { spnOrderIndex.EditValue = value; }
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
