using System;
using CommonControlLibrary;
using CommonUserControls.SettingsViewers.InventoryHousingViewers;
using CommonUserControls.SettingsViewers.InventoryItemBrandViewers;
using CommonUserControls.SettingsViewers.InventoryItemCategoryViewers;
using CommonUserControls.SettingsViewers.UnitMeasurmentViewers;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InventoryItemViewers
{
	public partial class InventoryItem_EditorViewer :
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<InventoryItem_cu>,
		IInventoryItemViewer
	{
		private InventoryHousing_EditorViewer _inventoryHousingEditorViewer;
		private InventoryItemCategory_EditorViewer _inventoryItemCategoryEditorViewer;
		private UnitMeasurment_EditorViewer _unitMeasurmentEditorViewer;
		private InventoryItemBrand_EditorViewer _inventoryItemBrandEditorViewer;

		public InventoryItem_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_InventoryItem_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<InventoryItem_cu>

		public override IMVCController<InventoryItem_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int) ViewerName.InventoryItem_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "المنتجــــات"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeInventoryHousing, InventoryHousing_cu.ItemsList, "InventoryHousingFullName");
			CommonViewsActions.FillGridlookupEdit(lkeInventoryItemCategory, InventoryItemCategory_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeInventoryItemBrand, InventoryItemBrand_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeInventoryItemType, InventoryItemType_p.ItemsList);
		}

		public override void ClearControls()
		{
			txtNameP.EditValue = null;
			txtNameS.EditValue = null;
			lkeInventoryHousing.EditValue = null;
			lkeDefaultUnitMeasurment.EditValue = null;
			lkeInventoryItemCategory.EditValue = null;
			lkeInventoryItemBrand.EditValue = null;
			txtInternalCode.EditValue = null;
			spnDefaultSellingPrice.EditValue = null;
			spnDefaultPurchasingPrice.EditValue = null;
			spnMinStockLevel.EditValue = null;
			spnMaxStockLevel.EditValue = null;
			spnReorderPoint.EditValue = null;
			dtSellingStartDate.EditValue = null;
			dtSellingEndDate.EditValue = null;
			dtExpirationDate.EditValue = null;
			txtDefaultBarcode.EditValue = null;
			txtDescription.EditValue = null;
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

		public object InventoryItemType_P_ID
		{
			get { return lkeInventoryItemType.EditValue; }
			set { lkeInventoryItemType.EditValue = value; }
		}

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

		public object Width
		{
			get { return spnWidth.EditValue; }
			set { spnWidth.EditValue = value; }
		}

		public object Height
		{
			get { return spnHeight.EditValue; }
			set { spnHeight.EditValue = value; }
		}

		public object Depth
		{
			get { return spnDepth.EditValue; }
			set { spnDepth.EditValue = value; }
		}

		#endregion

		private void btnInventoryHousing_Click(object sender, EventArgs e)
		{
			BaseController<InventoryHousing_cu>.ShowEditorControl(ref _inventoryHousingEditorViewer, this, null, null,
					EditorContainerType.Regular, ViewerName.InventoryHousing_Viewer, DB_CommonTransactionType.CreateNew,
					"المخـــــازن", true);
			CommonViewsActions.FillGridlookupEdit(lkeInventoryHousing, InventoryHousing_cu.ItemsList, "InventoryHousingFullName");
		}

		private void btnInventoryItemCategory_Click(object sender, EventArgs e)
		{
			BaseController<InventoryItemCategory_cu>.ShowEditorControl(ref _inventoryItemCategoryEditorViewer, this, null, null,
						EditorContainerType.Regular, ViewerName.InventoryItemCategory_Viewer, DB_CommonTransactionType.CreateNew,
						"تصنيفــــات المنتجـــــات", true);
			CommonViewsActions.FillGridlookupEdit(lkeInventoryItemCategory, InventoryItemCategory_cu.ItemsList);
		}

		private void btnUnitMeasurment_Click(object sender, EventArgs e)
		{
			BaseController<UnitMeasurment_cu>.ShowEditorControl(ref _unitMeasurmentEditorViewer, this, null, null,
							EditorContainerType.Regular, ViewerName.UnitMeasurment_Viewer, DB_CommonTransactionType.CreateNew,
							"وحــــدات القيــــاس", true);
			CommonViewsActions.FillGridlookupEdit(lkeDefaultUnitMeasurment, UnitMeasurment_cu.ItemsList);
		}

		private void btnInventoryItemBrand_Click(object sender, EventArgs e)
		{
			BaseController<InventoryItemBrand_cu>.ShowEditorControl(ref _inventoryItemBrandEditorViewer, this, null, null,
								EditorContainerType.Regular, ViewerName.InventoryItemBrand_Viewer, DB_CommonTransactionType.CreateNew,
								"العـلامـــات التجـاريـــــة", true);
			CommonViewsActions.FillGridlookupEdit(lkeInventoryItemBrand, InventoryItemBrand_cu.ItemsList);
		}
	}
}
