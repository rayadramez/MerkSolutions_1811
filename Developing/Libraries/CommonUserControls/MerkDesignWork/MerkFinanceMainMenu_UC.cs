﻿using System.Windows.Forms;
using CommonUserControls.ReportsContainer;
using CommonUserControls.SettingsViewers.Color_Viewers;
using CommonUserControls.SettingsViewers.InventoryHousingViewers;
using CommonUserControls.SettingsViewers.InventoryItemBrandViewers;
using CommonUserControls.SettingsViewers.InventoryItemCategoryViewers;
using CommonUserControls.SettingsViewers.InventoryItemPriceViewers;
using CommonUserControls.SettingsViewers.InventoryItemViewers;
using CommonUserControls.SettingsViewers.InventoryItem_Area_Viewers;
using CommonUserControls.SettingsViewers.InventoryItem_InventoryHousingViewers;
using CommonUserControls.SettingsViewers.InventoryItem_InventoryItemGroupViewers;
using CommonUserControls.SettingsViewers.InventoryItem_UnitMeasurmentViewers;
using CommonUserControls.SettingsViewers.InvetoryItemGroupViewers;
using CommonUserControls.SettingsViewers.RawMaterialTransaction;
using CommonUserControls.SettingsViewers.RawMaterial_Viewers;
using CommonUserControls.SettingsViewers.UnitMeasurmentTreeLinkViewers;
using CommonUserControls.SettingsViewers.UnitMeasurmentViewers;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;

namespace CommonUserControls.MerkDesignWork
{
	public partial class MerkFinanceMainMenu_UC : UserControl
	{
		private InventoryHousing_EditorViewer _inventoryHousingEditorViewer;
		private InventoryHousing_SearchViewer _inventoryHousingSearchViewer;

		private InventoryItemCategory_EditorViewer _inventoryItemCategoryEditorViewer;
		private InventoryItemCategory_SearchViewer _inventoryItemCategorySearchViewer;

		private InventoryItemBrand_EditorViewer _inventoryItemBrandEditorViewer;
		private InventoryItemBrand_SearchViewer _inventoryItemBrandSearchViewer;

		private InventoryItemGroup_EditorViewer _inventoryItemGroupEditorViewer;
		private InventoryItemGroup_SearchViewer _inventoryItemGroupSearchViewer;

		private UnitMeasurment_EditorViewer _unitMeasurmentEditorViewer;
		private UnitMeasurment_SearchViewer _unitMeasurmentSearchViewer;

		private UnitMeasurmentTreeLink_EditorViewer _unitMeasurmentTreeLinkEditorViewer;
		private UnitMeasurmentTreeLink_SearchViewer _unitMeasurmentTreeLinkSearchViewer;

		private InventoryItem_EditorViewer _inventoryItemEditorViewer;
		private InventoryItem_SearchViewer _inventoryItemSearchViewer;

		private InventoryItem_UnitMeasurment_EditorViewer _inventoryItemUnitMeasurmentEditorViewer;
		private InventoryItem_UnitMeasurment_SearchViewer _inventoryItemUnitMeasurmentSearchViewer;

		private InventoryItemPrice_EditorViewer _inventoryItemPriceEditorViewer;
		private InventoryItemPrice_SearchViewer _inventoryItemPriceSearchViewer;

		private InventoryItem_InventoryHousing_EditorViewer _inventoryItemInventoryHousingEditor;
		private InventoryItem_InventoryHousing_SearchViewer _inventoryItemInventoryHousingSearch;

		private InventoryItem_Area_EditorViewer _inventoryItemAreaEditorViewer;
		private InventoryItem_Area_SearchViewer _inventoryItemAreaSearchViewer;

		private InventoryItemGroup_InventoryItem_EditorViewer _inventoryItemGroupInventoryItemEditor;
		private InventoryItemGroup_InventoryItem_SearchViewer _inventoryItemGroupInventoryItemSearch;

		private RawMaterial_EditorViewer _rawMaterialEditor;
		private RawMaterial_SearchViewer _rawMaterialSearch;

		private Color_EditorViewer _colorEditor;
		private Color_SearchViewer _colorSearch;

		private RawMaterialTransaction_EditorViewer _rawMaterialTransactionEditorViewer;
		private RawMaterialTransaction_SearchViewer _rawMaterialTransactionSearchViewer;

		private GetRawMaterialCostPrices_Report _getRawMaterialCostPricesReport;

		public MerkFinanceMainMenu_UC()
		{
			InitializeComponent();
		}

		private void btnInventoryHousing_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<InventoryHousing_cu>.ShowControl(ref _inventoryHousingEditorViewer, ref _inventoryHousingSearchViewer,
				splitContainerControl2.Panel1,
				EditorContainerType.Settings,
				ViewerName.InventoryHousing_Viewer, DB_CommonTransactionType.CreateNew,
				"المخــــــــازن",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnInventoryItemCategory_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<InventoryItemCategory_cu>.ShowControl(ref _inventoryItemCategoryEditorViewer,
				ref _inventoryItemCategorySearchViewer,
				splitContainerControl2.Panel1,
				EditorContainerType.Settings,
				ViewerName.InventoryItemCategory_Viewer,
				DB_CommonTransactionType.CreateNew,
				"تصنيفــــات المنتجـــات",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnInventoryBrand_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<InventoryItemBrand_cu>.ShowControl(ref _inventoryItemBrandEditorViewer,
				ref _inventoryItemBrandSearchViewer,
				splitContainerControl2.Panel1,
				EditorContainerType.Settings,
				ViewerName.InventoryItemBrand_Viewer,
				DB_CommonTransactionType.CreateNew,
				"العـلامــــات التجـاريـــــة",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnInventoryItemGroup_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<InventoryItemGroup_cu>.ShowControl(ref _inventoryItemGroupEditorViewer,
				ref _inventoryItemGroupSearchViewer,
				splitContainerControl2.Panel1,
				EditorContainerType.Settings,
				ViewerName.InventoryItemGroup_Viewer,
				DB_CommonTransactionType.CreateNew,
				"مجمـوعــات المنتجــــات",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnInventoryItemGroup_InventoryItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<InventoryItemGroup_InventoryItem_cu>.ShowControl(ref _inventoryItemGroupInventoryItemEditor,
				ref _inventoryItemGroupInventoryItemSearch,
				splitContainerControl2.Panel1,
				EditorContainerType.Settings,
				ViewerName.InventoryItemGroup_InventoryItem_Viewer,
				DB_CommonTransactionType.CreateNew,
				"ربــــط مجمـوعــــات المنتجـــــات بالمنتجــــــات",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnInventoryItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<InventoryItem_cu>.ShowControl(ref _inventoryItemEditorViewer,
				ref _inventoryItemSearchViewer,
				splitContainerControl2.Panel1,
				EditorContainerType.Settings,
				ViewerName.InventoryItem_Viewer,
				DB_CommonTransactionType.CreateNew,
				"المنتجــــات",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnInventoryItemPrice_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<InventoryItemPrice_cu>.ShowControl(ref _inventoryItemPriceEditorViewer,
				ref _inventoryItemPriceSearchViewer,
				splitContainerControl2.Panel1,
				EditorContainerType.Settings,
				ViewerName.InventoryItemPrice_Viewer,
				DB_CommonTransactionType.CreateNew,
				"أسعـــــار المنتجـــــات",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnInventoryItem_InventoryHousing_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<InventoryItemTransaction>.ShowControl(ref _inventoryItemInventoryHousingEditor,
				ref _inventoryItemInventoryHousingSearch,
				splitContainerControl2.Panel1,
				EditorContainerType.Settings,
				ViewerName.InventoryItem_InventoryHousing_Viewer,
				DB_CommonTransactionType.CreateNew,
				"ربـــط المنتجـــــات بالمخــــازن",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnInventoryItem_Area_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<InventoryItem_Area>.ShowControl(ref _inventoryItemAreaEditorViewer,
				ref _inventoryItemAreaSearchViewer,
				splitContainerControl2.Panel1,
				EditorContainerType.Settings,
				ViewerName.InventoryItem_Area_Viewer,
				DB_CommonTransactionType.CreateNew,
				"تقسيــــم وحـــدات المنتــــج",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnRawMaterial_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<RawMaterials_cu>.ShowControl(ref _rawMaterialEditor,
				ref _rawMaterialSearch,
				splitContainerControl2.Panel1,
				EditorContainerType.Settings,
				ViewerName.RawMaterial_viewer,
				DB_CommonTransactionType.CreateNew,
				"تسجيـــــل المــــواد الخــــام",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnColor_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<Color_cu>.ShowControl(ref _colorEditor,
				ref _colorSearch,
				splitContainerControl2.Panel1,
				EditorContainerType.Settings,
				ViewerName.Color_Viewer,
				DB_CommonTransactionType.CreateNew,
				"تسجيـــــل الــــوان الأخشــــاب",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnRawMaterialTransaction_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<RawMaterialTranasction>.ShowControl(ref _rawMaterialTransactionEditorViewer,
				ref _rawMaterialTransactionSearchViewer,
				splitContainerControl2.Panel1,
				EditorContainerType.Settings,
				ViewerName.RawMaterialTransactions_viewer,
				DB_CommonTransactionType.CreateNew,
				"تسجيـــــل حـركــــة مــواد الخــــام",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnGetRawMaterialCostPrices_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<GetRawMaterialCostPrices_Result>.ShowSearchControl(ref _getRawMaterialCostPricesReport, this,
				ViewerName.GetRawMaterialCostPrices_Viewer, DB_CommonTransactionType.SearchReport,
				".... تقــريــــــر تكاليـــف المـــواد الخــــام .....", true, true);
		}
	}
}
