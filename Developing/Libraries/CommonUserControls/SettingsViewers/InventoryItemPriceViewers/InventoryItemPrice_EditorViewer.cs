using System;
using System.Collections.Generic;
using CommonControlLibrary;
using CommonUserControls.SettingsViewers.CustomersViewers;
using CommonUserControls.SettingsViewers.InventoryItem_UnitMeasurmentViewers;
using CommonUserControls.SettingsViewers.SupplierViewers;
using DevExpress.XtraLayout.Utils;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InventoryItemPriceViewers
{
	public partial class InventoryItemPrice_EditorViewer :
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<InventoryItemPrice_cu>,
		IInventoryItemPriceViewer
	{
		private Customer_EditorViewer _customerEditorViewer;
		private Supplier_EditorViewer _supplierEditorViewer;
		private InventoryItem_UnitMeasurment_EditorViewer _intInventoryItemUnitMeasurmentEditorViewer;

		public InventoryItemPrice_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_InventoryItemPrice_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		private void chkSellingInvoice_CheckedChanged(object sender, EventArgs e)
		{
			lytSellingPrice.Visibility = chkSellingInvoice.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			lytPurchasingPrice.Visibility = chkPurchasingPrice.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		private void chkPurchasingPrice_CheckedChanged(object sender, EventArgs e)
		{
			lytPurchasingPrice.Visibility = chkPurchasingPrice.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			lytSellingPrice.Visibility = chkSellingInvoice.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		#region Overrides of CommonAbstractViewer<InventoryItemPrice_cu>

		public override IMVCController<InventoryItemPrice_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.InventoryItemPrice_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "تحـديـــد أسعـــار المنتجـــــات"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeInventoryItem, InventoryItem_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeSuppliers, Supplier_cu.ItemsList, "FullName", "Person_CU_ID");
			CommonViewsActions.FillGridlookupEdit(lkeCustomers, Customer_cu.ItemsList, "FullName", "Person_CU_ID");
			Date = DateTime.Now.Date.StartOfDayDateTime();
		}

		public override void ClearControls()
		{
			lkeCustomers.EditValue = null;
			lkeSuppliers.EditValue = null;
			lkeInventoryItem.EditValue = null;
			lkeUnitMeasurment.EditValue = null;
			spnSellingPrice.EditValue = 0;
			spnPurchasingPrice.EditValue = 0;
			chkSellingInvoice.Checked = true;
		}

		#endregion

		#region Implementation of IInventoryItemPriceViewer

		public object InventoryItem_CU_ID
		{
			get { return lkeInventoryItem.EditValue; }
			set { lkeInventoryItem.EditValue = value; }
		}

		public object UnitMeasurment_CU_ID
		{
			get { return lkeUnitMeasurment.EditValue; }
			set { lkeUnitMeasurment.EditValue = value; }
		}

		public object Date
		{
			get { return dtDate.EditValue; }
			set { dtDate.EditValue = value; }
		}

		public object Price
		{
			get
			{
				if (chkSellingInvoice.Checked)
					return spnSellingPrice.EditValue;
				if (chkPurchasingPrice.Checked)
					return spnPurchasingPrice.EditValue;

				return null;
			}
			set
			{
				if (chkSellingInvoice.Checked)
					spnSellingPrice.EditValue = value;
				else if (chkPurchasingPrice.Checked)
					spnPurchasingPrice.EditValue = value;
			}
		}

		public object Customer_CU_ID
		{
			get { return lkeCustomers.EditValue; }
			set { lkeCustomers.EditValue = value; }
		}

		public object PriceType_P_ID
		{
			get
			{
				if (chkSellingInvoice.Checked)
					return (int)DB_PriceType.SellingPrice;
				if (chkPurchasingPrice.Checked)
					return (int)DB_PriceType.PurchasingPrice;

				return (int)DB_PriceType.None;
			}
			set
			{
				switch ((DB_PriceType)value)
				{
					case DB_PriceType.PurchasingPrice:
						chkSellingInvoice.Checked = true;
						break;
					case DB_PriceType.SellingPrice:
						chkPurchasingPrice.Checked = true;
						break;
				}
			}
		}

		public object Supplier_CU_ID
		{
			get { return lkeSuppliers.EditValue; }
			set { lkeSuppliers.EditValue = value; }
		}

		#endregion

		private void btnnewCustomer_Click(object sender, EventArgs e)
		{
			BaseController<Customer_cu>.ShowEditorControl(ref _customerEditorViewer, this, null, null,
						EditorContainerType.Regular, ViewerName.Customer_Viewer, DB_CommonTransactionType.CreateNew,
						"العمـــلاء", true);
			CommonViewsActions.FillGridlookupEdit(lkeCustomers, Customer_cu.ItemsList, "FullName", "Person_CU_ID");
		}

		private void btnNewSupplier_Click(object sender, EventArgs e)
		{
			BaseController<Supplier_cu>.ShowEditorControl(ref _supplierEditorViewer, this, null, null,
							EditorContainerType.Regular, ViewerName.Supplier_Viewer, DB_CommonTransactionType.CreateNew,
							"المـورديــــن", true);
			CommonViewsActions.FillGridlookupEdit(lkeSuppliers, Supplier_cu.ItemsList, "FullName", "Person_CU_ID");
		}

		private void btnInventoryItem_UnitMeasurment_Click(object sender, EventArgs e)
		{
			BaseController<InventoryItem_UnitMeasurment_cu>.ShowEditorControl(ref _intInventoryItemUnitMeasurmentEditorViewer, this, null, null,
				EditorContainerType.Regular, ViewerName.InventoryItem_UnitMeasurment_Viewer, DB_CommonTransactionType.CreateNew,
				"ربـط المنتجــــات بـوحـــدات القيــــاس", true);
			if (lkeInventoryItem.EditValue == null)
			{
				lkeUnitMeasurment.Properties.DataSource = null;
				return;
			}

			List<UnitMeasurment_cu> unitsList =
				InventoryBusinessLogicEngine.GetInventoryItemRegisteredUnitMeasurments(Convert.ToInt32(lkeInventoryItem.EditValue));
			CommonViewsActions.FillGridlookupEdit(lkeUnitMeasurment, unitsList);
		}

		private void lkeInventoryItem_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeInventoryItem.EditValue == null)
			{
				lkeUnitMeasurment.Properties.DataSource = null;
				return;
			}

			List<UnitMeasurment_cu> unitsList =
				InventoryBusinessLogicEngine.GetInventoryItemRegisteredUnitMeasurments(Convert.ToInt32(lkeInventoryItem.EditValue));
			CommonViewsActions.FillGridlookupEdit(lkeUnitMeasurment, unitsList);
		}

		private void lkeUnitMeasurment_EditValueChanged(object sender, EventArgs e)
		{
			//if (lkeUnitMeasurment.EditValue == null || lkeInventoryItem.EditValue == null)
			//	return;

			//InventoryItem_cu inventoryitem =
			//	InventoryItem_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(lkeInventoryItem.EditValue)));
			//if (inventoryitem == null)
			//	return;

			//UnitMeasurment_cu inventorytrackingUnitMeasurment = inventoryitem.InventoryTrackingUnitMeasurment;
			//if (inventorytrackingUnitMeasurment == null)
			//	return;
			//UnitMeasurment_cu unitMeasurment =
			//	UnitMeasurment_cu.ItemsList.Find(
			//		item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(lkeUnitMeasurment.EditValue)));
			//if (unitMeasurment == null)
			//	return;

			//double encapsulatedAmount = InventoryBusinessLogicEngine.GetEncapsulatedQuantity(inventorytrackingUnitMeasurment.ID,
			//	unitMeasurment.ID);

			//double itemPrice = 0;
			//DB_PriceType priceType = DB_PriceType.None;
			//if (chkSellingInvoice.Checked)
			//	priceType = DB_PriceType.SellingPrice;
			//else
			//	priceType = DB_PriceType.PurchasingPrice;
			//if (Date != null)
			//	itemPrice = InventoryBusinessLogicEngine.GetInventoryItemSellingPrice(inventoryitem.ID, inventorytrackingUnitMeasurment.ID, Date,
			//		null, priceType);
			//itemPrice = itemPrice*encapsulatedAmount;
		}

		private void chkRegisterPriceToCustomer_CheckedChanged(object sender, EventArgs e)
		{
			lytCustomers.Visibility = chkRegisterPriceToCustomer.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			if (!chkRegisterPriceToCustomer.Checked)
				lkeCustomers.EditValue = null;
		}

		private void chkRegisterPriceToSupplier_CheckedChanged(object sender, EventArgs e)
		{
			lytSuppliers.Visibility = chkRegisterPriceToSupplier.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			if (!chkRegisterPriceToSupplier.Checked)
				lkeSuppliers.EditValue = null;
		}
	}
}
