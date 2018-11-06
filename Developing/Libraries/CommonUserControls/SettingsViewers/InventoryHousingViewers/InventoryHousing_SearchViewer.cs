using System;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InventoryHousingViewers
{
	public partial class InventoryHousing_SearchViewer : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractSearchViewer<InventoryHousing_cu>,
		IInventoryHousingViewer
	{
		public InventoryHousing_SearchViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_InventoryHousing_SearchViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<InventoryHousing_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.InventoryHousing_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "المخــــــازن"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_InventoryHousing_SearchViewer; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeFloor, Floor_cu.ItemsList, "FloorFullName");
			CommonViewsActions.FillGridlookupEdit(lkeLocations, Location_cu.ItemsList);
		}

		public override void ClearControls()
		{
			txtNameP.EditValue = null;
			txtNameS.EditValue = null;
			txtInternalCode.EditValue = null;
			txtDescription.EditValue = null;
			chkIsMan.Checked = false;
			lkeFloor.EditValue = null;
			lkeLocations.EditValue = null;
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

		public object Location
		{
			get { return lkeLocations.EditValue; }
			set { lkeLocations.EditValue = value; }
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
	}
}
