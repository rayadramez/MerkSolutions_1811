using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InventoryItemPriceViewers
{
	public partial class InventoryItemPrice_SearchViewer : 
		//UserControl
		CommonAbstractSearchViewer<InventoryItemPrice_cu>,
		IInventoryItemPriceViewer
	{
		public InventoryItemPrice_SearchViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_InventoryItemPrice_SearchViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<InventoryItemPrice_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.InventoryItemPrice_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "تحـديـــد أسعـــار المنتجـــــات"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_InventoryItemPrice_SearchViewer; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeInventoryItem, InventoryItem_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeUnitMeasurment, UnitMeasurment_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeSuppliers, Supplier_cu.ItemsList, "FullName");
			CommonViewsActions.FillGridlookupEdit(lkeCustomers, Customer_cu.ItemsList, "FullName");
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
	}
}
