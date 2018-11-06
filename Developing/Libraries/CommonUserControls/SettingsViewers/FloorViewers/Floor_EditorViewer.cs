using System.Collections.Generic;
using CommonControlLibrary;
using CommonUserControls.SettingsViewers.InPatientRoomViewers;
using CommonUserControls.SettingsViewers.LocationViewers;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.FloorViewers
{
	public partial class Floor_EditorViewer : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<Floor_cu>,
		IFloorViewer
	{
		private Location_EditorViewer_UC _locationEditorViewer;

		public Floor_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_FloorEditor);
			CommonViewsActions.SetupSyle(this);

			txtNameP.Focus();
		}

		#region Overrides of CommonAbstractEditorViewer

		public override IMVCController<Floor_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.FloorViewer; }
		}

		public override void SetRelatedViewer(IMVCController<Floor_cu> relateDataCollector)
		{
			RelatedViewers = new List<IViewer>();
			RelatedViewers.Add(new InPatientRoomEditorViewer_UC());
			base.SetRelatedViewer(relateDataCollector);
		}

		public override void CancelEditing()
		{
			
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_ChartOfAccountMargin; }
		}

		public override string HeaderTitle
		{
			get { return "الأدوار"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeLocation, Location_cu.ItemsList);
		}

		public override void ClearControls()
		{
			txtNameP.EditValue = null;
			txtNameS.EditValue = null;
			txtInternalCode.EditValue = null;
			txtDescription.EditValue = null;
			lkeLocation.EditValue = null;
			txtShortName.EditValue = null;
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

		private void btnAddLocation_Click(object sender, System.EventArgs e)
		{
			BaseController<Location_cu>.ShowEditorControl(ref _locationEditorViewer, this, null, null,
				EditorContainerType.Regular, ViewerName.Location_Viewer, DB_CommonTransactionType.CreateNew,
				"مـواقــــع المنظمـــــة", true);
			CommonViewsActions.FillGridlookupEdit(lkeLocation, Location_cu.ItemsList);
		}
	}
}
