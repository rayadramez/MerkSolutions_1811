using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.StationPointViewers
{
	public partial class StationPoint_EditorViewer : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<StationPoint_cu>,
		IStationPointViewer
	{
		public StationPoint_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_StationPoint_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<StationPoint_cu>

		public override IMVCController<StationPoint_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.StationPoint_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "العيـــــادت"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeStation, Station_p.ItemsList);
		}

		public override void ClearControls()
		{
			txtNameP.EditValue = null;
			txtNameS.EditValue = null;
			txtInternalCode.EditValue = null;
			txtDescription.EditValue = null;
			lkeStation.EditValue = null;
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
