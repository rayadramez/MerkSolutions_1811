using System;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InventoryItemViewers
{
	public partial class InventoryItem_SearchViewer : 
		//UserControl
		CommonAbstractSearchViewer<InventoryItem_cu>,
		IInventoryItemViewer
	{
		public InventoryItem_SearchViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_InventoryItem_SearchViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<InventoryItem_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.InventoryItem_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "المنتجــــات"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_InventoryItem_SearchViewer; }
		}

		#endregion

		#region Implementation of IInventoryItemViewer

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

		public object InventoryHousing_CU_ID
		{
			get { return lkeInventoryHousing.EditValue; }
			set { lkeInventoryHousing.EditValue = value; }
		}

		public object InventoryItemCategory_CU_ID
		{
			get { return lkeInventoryItemCategory.EditValue; }
			set { lkeInventoryItemCategory.EditValue = value; }
		}

		public object InventoryItemBrand_CU_ID
		{
			get { return lkeInventoryItemBrand.EditValue; }
			set { lkeInventoryItemBrand.EditValue = value; }
		}

		public object DefaultUnitMeasurment_CU_ID
		{
			get { return lkeDefaultUnitMeasurment.EditValue; }
			set { lkeDefaultUnitMeasurment.EditValue = value; }
		}

		public object InventoryItemType_P_ID { get; set; }

		public object InternalCode
		{
			get { return txtInternalCode.EditValue; }
			set { txtInternalCode.EditValue = value; }
		}

		public object DefaultBarcode
		{
			get { return txtDefaultBarcode.EditValue; }
			set { txtDefaultBarcode.EditValue = value; }
		}

		public object DefaultSellingPrice
		{
			get { return spnDefaultSellingPrice.EditValue; }
			set { spnDefaultSellingPrice.EditValue = value; }
		}

		public object DefaultCost
		{
			get { return spnDefaultPurchasingPrice.EditValue; }
			set { spnDefaultPurchasingPrice.EditValue = value; }
		}

		public object RorderedPoint
		{
			get { return spnReorderPoint.EditValue; }
			set { spnReorderPoint.EditValue = value; }
		}

		public object StockMinLevel
		{
			get { return spnMinStockLevel.EditValue; }
			set { spnMinStockLevel.EditValue = value; }
		}

		public object StockMaxLevel
		{
			get { return spnMaxStockLevel.EditValue; }
			set { spnMaxStockLevel.EditValue = value; }
		}

		public object AcceptOverrideMinAmount
		{
			get { return chkAcceptOverrideMinAmount.Checked; }
			set { chkAcceptOverrideMinAmount.Checked = Convert.ToBoolean(value); }
		}

		public object CanBeSold
		{
			get { return chkCanBeSold.Checked; }
			set { chkCanBeSold.Checked = Convert.ToBoolean(value); }
		}

		public object IsAvailable
		{
			get { return chkIsAvailable.Checked; }
			set { chkIsAvailable.Checked = Convert.ToBoolean(value); }
		}

		public object AcceptPartingSelling
		{
			get { return chkAcceptPartingSelling.Checked; }
			set { chkAcceptPartingSelling.Checked = Convert.ToBoolean(value); }
		}

		public object IsCountable
		{
			get { return chkIsCountable.Checked; }
			set { chkIsCountable.Checked = Convert.ToBoolean(value); }
		}

		public object SellingStartDate
		{
			get { return dtSellingStartDate.EditValue; }
			set { dtSellingStartDate.EditValue = value; }
		}

		public object SellingEndDate
		{
			get { return dtSellingEndDate.EditValue; }
			set { spnDefaultSellingPrice.EditValue = value; }
		}

		public object ExpirationDate
		{
			get { return dtExpirationDate.EditValue; }
			set { dtExpirationDate.EditValue = value; }
		}

		public object Description
		{
			get { return txtDescription.EditValue; }
			set { txtDescription.EditValue = value; }
		}

		#endregion


		public new object Width { get; set; }

		public new object Height { get; set; }

		public object Depth { get; set; }
	}
}
