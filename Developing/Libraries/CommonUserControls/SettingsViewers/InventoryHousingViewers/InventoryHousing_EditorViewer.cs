using System;
using CommonControlLibrary;
using CommonUserControls.SettingsViewers.FloorViewers;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InventoryHousingViewers
{
	public partial class InventoryHousing_EditorViewer :
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<InventoryHousing_cu>,
		IInventoryHousingViewer
	{
		private Floor_EditorViewer _floorEditorViewer;

		public InventoryHousing_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_InventoryHousing_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<InventoryHousing_cu>

		public override IMVCController<InventoryHousing_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.InventoryHousing_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "المخــــــازن"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeFloor, Floor_cu.ItemsList, "FloorFullName");
		}

		public override void ClearControls()
		{
			txtNameP.EditValue = null;
			txtNameS.EditValue = null;
			txtInternalCode.EditValue = null;
			txtDescription.EditValue = null;
			chkIsMan.Checked = false;
			lkeFloor.EditValue = null;
		}

		#endregion

		#region Implementation of IInventoryHousingViewer

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

		public object Floor_CU_ID
		{
			get { return lkeFloor.EditValue; }
			set { lkeFloor.EditValue = value; }
		}

		public object IsMain
		{
			get { return chkIsMan.Checked; }
			set { chkIsMan.Checked = Convert.ToBoolean(value); }
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

		private void btnAddFloor_Click(object sender, EventArgs e)
		{
			BaseController<Location_cu>.ShowEditorControl(ref _floorEditorViewer, this, null, null,
				EditorContainerType.Regular, ViewerName.FloorViewer, DB_CommonTransactionType.CreateNew,
				"أدوار المنظمـــــة", true);
			CommonViewsActions.FillGridlookupEdit(lkeFloor, Floor_cu.ItemsList, "FloorFullName");
		}

		private void lkeFloor_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeFloor.EditValue == null)
				return;

			chkIsMan.Enabled = !InventoryBusinessLogicEngine.IsFloorOfLocationHasMainInventory(Convert.ToInt32(lkeFloor.EditValue));
		}
	}
}
