using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using CommonUserControls.SettingsViewers.CustomersViewers;
using CommonUserControls.SettingsViewers.SupplierViewers;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.InvoiceViewers.MarketInvoice
{
	public partial class MarketCreationFinanceInvoice_UC :
		//UserControl
		CommonAbstractEditorViewer<FinanceInvoice>,
		IFinanceInvoiceCreation, IViewerDataRelated
	{
		private Customer_EditorViewer _customerEditorViewer;
		private Supplier_EditorViewer _supplierEditorViewer;
		private bool isRemainingEdited;
		private FinanceInvoiceDetail SelectedFinanceInvoiceDetail { get; set; }

		public MarketCreationFinanceInvoice_UC()
		{
			InitializeComponent();
		}

		private void MarketCreationFinanceInvoice_UC_Load(object sender, System.EventArgs e)
		{
			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_MarketInvoiceCreation);
			CommonViewsActions.SetupGridControl(grdInventoryItems, Resources.LocalizedRes.grd_MarketInvoiceCreation, true);
			CommonViewsActions.SetupSyle(this);

			if (ViewerDataRelated != null)
				Initialize((DB_InvoiceType)ViewerDataRelated);
		}

		public void Initialize(DB_InvoiceType invoiceType)
		{
			InvoiceTypeID = invoiceType;
			InventoryBusinessLogicEngine.ActiveInvoiceType = invoiceType;
			switch ((DB_InvoiceType)InvoiceTypeID)
			{
				case DB_InvoiceType.PurchasingInvoice:
				case DB_InvoiceType.ReturningPurchasingInvoice:
					CommonViewsActions.FillGridlookupEdit(lkeDedicatedPerson, Supplier_cu.ItemsList, "FullName", "Person_CU_ID");
					lytDedicatedPerson.Text = "المــــورد";
					break;
				case DB_InvoiceType.SellingInvoice:
				case DB_InvoiceType.ReturningSellingInvoice:
					CommonViewsActions.FillGridlookupEdit(lkeDedicatedPerson, Customer_cu.ItemsList, "FullName", "Person_CU_ID");
					lytDedicatedPerson.Text = "العميـــل";
					break;
			}

			if (CommonActions.CommonActions.GetMaxFinancialIntervalDate(2018) != null)
				dtInvoiceCreation.Properties.MaxValue =
					Convert.ToDateTime(CommonActions.CommonActions.GetMaxFinancialIntervalDate(2018)).StartOfDayDateTime();
			if (CommonActions.CommonActions.GetMinFinancialIntervalDate(2018) != null)
				dtInvoiceCreation.Properties.MinValue =
					Convert.ToDateTime(CommonActions.CommonActions.GetMinFinancialIntervalDate(2018)).StartOfDayDateTime();
		}

		#region Overrides of CommonAbstractViewer<FinanceInvoice>

		public override IMVCController<FinanceInvoice> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { throw new NotImplementedException(); }
		}

		public override string HeaderTitle
		{
			get { throw new NotImplementedException(); }
		}

		public override void FillControls()
		{
			chkIsPaymentEnough.Checked = false;
			chkResetRemaining.Checked = false;
			txtInvoiceSerial.EditValue = "0000";
			dtInvoiceCreation.DateTime = DateTime.Now;

			if (InvoiceTypeID != null)
				switch ((DB_InvoiceType)InvoiceTypeID)
				{
					case DB_InvoiceType.PurchasingInvoice:
					case DB_InvoiceType.ReturningPurchasingInvoice:
						CommonViewsActions.FillGridlookupEdit(lkeDedicatedPerson, Supplier_cu.ItemsList, "FullName", "Person_CU_ID");
						lytDedicatedPerson.Text = "المــــورد";
						break;
					case DB_InvoiceType.SellingInvoice:
					case DB_InvoiceType.ReturningSellingInvoice:
						CommonViewsActions.FillGridlookupEdit(lkeDedicatedPerson, Customer_cu.ItemsList, "FullName", "Person_CU_ID");
						lytDedicatedPerson.Text = "العميـــل";
						break;
				}

			CommonViewsActions.FillGridlookupEdit(lkeLocations, Location_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeFloors, Floor_cu.ItemsList, "FloorFullName");
			CommonViewsActions.FillGridlookupEdit(lkeInventoryHousing, InventoryHousing_cu.ItemsList, "InventoryHousingFullName");
			lkeInventoryHousing.EditValue = ApplicationStaticConfiguration.ActiveInventoryHousingID;

			spnLineProductPrice.EditValue = 0;
			spnLineDiscount.EditValue = 0;
			spnLineQuantity.EditValue = 1;
			txtLineNet.EditValue = 0;
			spnAccummulativeAmount.EditValue = 0;
			spnAmountPaid.EditValue = 0;
			spnExtraDiscount.EditValue = 0;
			spnRemainingAmount.EditValue = 0;
			spnNet.EditValue = 0;
			grdInventoryItems.DataSource = null;
			FinanceInvoiceDetailsList = null;
			txtEncapsulatedQuantity.EditValue = null;
			InvoicePaymentTypeID = (int) DB_InvoicePaymentType.CashInvoice;
		}

		public override void ClearControls()
		{
			chkIsPaymentEnough.Checked = false;
			chkResetRemaining.Checked = false;

			spnLineProductPrice.EditValue = 0;
			spnLineDiscount.EditValue = 0;
			spnLineQuantity.EditValue = 1;

			txtLineNet.EditValue = 0;
			txtInventoryItemAvailableQuantity.EditValue = null;
			txtInventoryItemInternalCode.EditValue = null;
			txtInventoryItemBarcode.EditValue = null;

			lkeLocations.EditValue = null;
			lkeFloors.EditValue = null;
			lkeInventoryItems.EditValue = null;
			lkeUnitMeasurments.EditValue = null;

			txtEncapsulatedQuantity.EditValue = null;
		}

		#endregion

		#region Controls Events

		#region GridLookupEdit Events

		private void lkeDedicatedPerson_EditValueChanged(object sender, EventArgs e)
		{
			DB_PriceType priceType = DB_PriceType.None;
			DB_InvoiceType invoiceType = (DB_InvoiceType) InvoiceTypeID;
			switch (invoiceType)
			{
				case DB_InvoiceType.SellingInvoice:
				case DB_InvoiceType.ReturningSellingInvoice:
					priceType = DB_PriceType.SellingPrice;
					break;
				case DB_InvoiceType.PurchasingInvoice:
				case DB_InvoiceType.ReturningPurchasingInvoice:
					priceType = DB_PriceType.PurchasingPrice;
					break;
			}
			Line_PricePerUnit = InventoryBusinessLogicEngine.GetInventoryItemSellingPrice(Line_InventoryItem_CU_ID,
				Line_UnitMeasurment_CU_ID, InvoiceCreationDate, Person_CU_ID, priceType);
			if (InvoiceTypeID != null)
			switch ((DB_InvoiceType)InvoiceTypeID)
			{
				case DB_InvoiceType.SellingInvoice:
				case DB_InvoiceType.ReturningSellingInvoice:
					txtDedicatedPersonBalance.EditValue =
						FinancialBusinessLogicLibrary.GetCustomerBalance(FinancialBusinessLogicLibrary.CustomerBalanceType.NetBalance,
							InvoiceTypeID, lkeDedicatedPerson.EditValue);
					break;
				case DB_InvoiceType.PurchasingInvoice:
				case DB_InvoiceType.ReturningPurchasingInvoice:
					txtDedicatedPersonBalance.EditValue = FinancialBusinessLogicLibrary.GetSupplierBalance(InvoiceTypeID,
							lkeDedicatedPerson.EditValue);
					break;
			}
		}

		private void lkeLocations_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeLocations.EditValue == null)
			{
				CommonViewsActions.FillGridlookupEdit(lkeFloors, Floor_cu.ItemsList, "FloorFullName");
				return;
			}

			List<Floor_cu> floorList =
				Floor_cu.ItemsList.FindAll(
					item => Convert.ToInt32(item.Location_CU_ID).Equals(Convert.ToInt32(lkeLocations.EditValue)));
			CommonViewsActions.FillGridlookupEdit(lkeFloors, floorList, "FloorFullName");
		}

		private void lkeFloors_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeFloors.EditValue == null)
			{
				CommonViewsActions.FillGridlookupEdit(lkeInventoryHousing, InventoryHousing_cu.ItemsList, "InventoryHousingFullName");
				return;
			}

			List<InventoryHousing_cu> inventoryHousingList =
				InventoryHousing_cu.ItemsList.FindAll(
					item => Convert.ToInt32(item.Floor_CU_ID).Equals(Convert.ToInt32(lkeFloors.EditValue)));
			CommonViewsActions.FillGridlookupEdit(lkeInventoryHousing, inventoryHousingList, "InventoryHousingFullName");
		}

		private void lkeInventoryHousing_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeInventoryHousing.EditValue == null)
			{
				lkeInventoryItems.Properties.DataSource = null;
				return;
			}

			CommonViewsActions.FillGridlookupEdit(lkeInventoryItems,
				InventoryItem_cu.ItemsList.FindAll(
					item => Convert.ToInt32(item.InventoryHousing_CU_ID).Equals(Convert.ToInt32(lkeInventoryHousing.EditValue))));
		}

		private void lkeInventoryItems_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeInventoryItems.EditValue == null)
			{
				lkeUnitMeasurments.Properties.DataSource = null;
				return;
			}

			List<UnitMeasurment_cu> unitMeasurmentList =
				InventoryBusinessLogicEngine.GetInventoryItemRegisteredUnitMeasurments(Convert.ToInt32(lkeInventoryItems.EditValue));
			if (unitMeasurmentList == null || unitMeasurmentList.Count == 0)
				XtraMessageBox.Show("لا يـوجـــد وحـدات قيــــاس مـربـوطــــة مـع هــذا المنتــــج", "تنبيـــــه",
					MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, DefaultBoolean.Default);

			CommonViewsActions.FillGridlookupEdit(lkeUnitMeasurments, unitMeasurmentList);

			DB_PriceType priceType = DB_PriceType.None;
			DB_InvoiceType invoiceType = (DB_InvoiceType)InvoiceTypeID;
			switch (invoiceType)
			{
				case DB_InvoiceType.SellingInvoice:
				case DB_InvoiceType.ReturningSellingInvoice:
					priceType = DB_PriceType.SellingPrice;
					break;
				case DB_InvoiceType.PurchasingInvoice:
				case DB_InvoiceType.ReturningPurchasingInvoice:
					priceType = DB_PriceType.PurchasingPrice;
					break;
			}

			Line_PricePerUnit = InventoryBusinessLogicEngine.GetInventoryItemSellingPrice(Line_InventoryItem_CU_ID,
				Line_UnitMeasurment_CU_ID, InvoiceCreationDate, Person_CU_ID, priceType);
			Line_InventoryItemAvaliableQuantity =
				Math.Floor(InventoryBusinessLogicEngine.GetInventoryItemAvailableQuantity(Line_InventoryItem_CU_ID,
					Line_UnitMeasurment_CU_ID, InventoryHousing_CU_ID));

			UnitMeasurment_cu inventoryTrackingUnitMeasurment =
				InventoryBusinessLogicEngine.GetInventoryTrackingUnitMeasurment(Line_InventoryItem_CU_ID);
			if (inventoryTrackingUnitMeasurment != null)
				txtInventoryTrackingUnitMeasurment.Text = inventoryTrackingUnitMeasurment.Name_P;
			else
				txtInventoryTrackingUnitMeasurment.Text = "لـم يحــدد";

			if (inventoryTrackingUnitMeasurment == null)
				txtEncapsulatedQuantity.EditValue = 1;
			else if (Line_UnitMeasurment_CU_ID != null)
				txtEncapsulatedQuantity.EditValue = InventoryBusinessLogicEngine.GetEncapsulatedQuantity(Line_UnitMeasurment_CU_ID,
					inventoryTrackingUnitMeasurment.ID);
			else
				txtEncapsulatedQuantity.EditValue = 1;
		}

		private void lkeUnitMeasurments_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeInventoryItems.EditValue == null)
				return;

			DB_PriceType priceType = DB_PriceType.None;
			DB_InvoiceType invoiceType = (DB_InvoiceType)InvoiceTypeID;
			switch (invoiceType)
			{
				case DB_InvoiceType.SellingInvoice:
				case DB_InvoiceType.ReturningSellingInvoice:
					priceType = DB_PriceType.SellingPrice;
					break;
				case DB_InvoiceType.PurchasingInvoice:
				case DB_InvoiceType.ReturningPurchasingInvoice:
					priceType = DB_PriceType.PurchasingPrice;
					break;
			}

			Line_PricePerUnit = InventoryBusinessLogicEngine.GetInventoryItemSellingPrice(Line_InventoryItem_CU_ID,
				Line_UnitMeasurment_CU_ID, InvoiceCreationDate, Person_CU_ID, priceType);

			Line_InventoryItemAvaliableQuantity =
				Math.Floor(InventoryBusinessLogicEngine.GetInventoryItemAvailableQuantity(Line_InventoryItem_CU_ID,
					Line_UnitMeasurment_CU_ID, InventoryHousing_CU_ID));

			UnitMeasurment_cu inventoryTrackingUnitMeasurment =
				InventoryBusinessLogicEngine.GetInventoryTrackingUnitMeasurment(Line_InventoryItem_CU_ID);
			if (inventoryTrackingUnitMeasurment == null)
				txtEncapsulatedQuantity.EditValue = 1;
			else
				txtEncapsulatedQuantity.EditValue = InventoryBusinessLogicEngine.GetEncapsulatedQuantity(Line_UnitMeasurment_CU_ID,
					inventoryTrackingUnitMeasurment.ID);
		}

		#endregion

		#region CheckButtonEdit Events

		private void chkLineDiscountType_Toggled(object sender, EventArgs e)
		{
			//if (chkLineDiscountType.IsOn)
			//	Line_DiscountTypeID = (int)DB_DiscountType.Amount;
			//else
			//	Line_DiscountTypeID = (int)DB_DiscountType.Percentage;

			Line_Net = FinancialBusinessLogicLibrary.GetInvoiceCreationLineTotal(Line_PricePerUnit, Line_Quantity,
				Line_DiscountAmount, Line_DiscountTypeID);
		}

		private void chkResetRemaining_CheckedChanged(object sender, EventArgs e)
		{
			spnRemainingAmount.EditValue = 0;
		}

		private void chkPaymentType_Toggled(object sender, EventArgs e)
		{
			if (chkPaymentType.IsOn)
				PaymentTypeID = (int) DB_PaymentType.CashPayment;
		}

		#endregion

		#region SpinEdit Events

		private void spnLineQuantity_EditValueChanged(object sender, EventArgs e)
		{
			Line_Net = FinancialBusinessLogicLibrary.GetInvoiceCreationLineTotal(Line_PricePerUnit, Line_Quantity,
				Line_DiscountAmount, Line_DiscountTypeID);
		}

		private void spnLineProductPrice_EditValueChanged(object sender, EventArgs e)
		{
			Line_Net = FinancialBusinessLogicLibrary.GetInvoiceCreationLineTotal(Line_PricePerUnit, Line_Quantity,
				Line_DiscountAmount, Line_DiscountTypeID);
		}

		private void spnLineDiscount_EditValueChanged(object sender, EventArgs e)
		{
			Line_Net = FinancialBusinessLogicLibrary.GetInvoiceCreationLineTotal(Line_PricePerUnit, Line_Quantity,
				Line_DiscountAmount, Line_DiscountTypeID);
		}

		private void spnExtraDiscount_EditValueChanged(object sender, EventArgs e)
		{
			if (spnExtraDiscount.EditValue != null && spnAccummulativeAmount.EditValue != null)
				spnNet.EditValue = Convert.ToDouble(spnAccummulativeAmount.EditValue) - Convert.ToDouble(spnExtraDiscount.EditValue);
		}

		private void spnAccummulativeAmount_EditValueChanged(object sender, EventArgs e)
		{
			if (spnExtraDiscount.EditValue != null && spnAccummulativeAmount.EditValue != null)
				spnNet.EditValue = Convert.ToDouble(spnAccummulativeAmount.EditValue) - Convert.ToDouble(spnExtraDiscount.EditValue);
		}

		private void spnAmountPaid_EditValueChanged(object sender, EventArgs e)
		{
			if (spnAmountPaid.EditValue == null || spnNet.EditValue == null ||
				Math.Abs(Convert.ToDouble(spnNet.EditValue)) < 0.00001)
			{
				spnRemainingAmount.EditValue = 0;
				return;
			}

			double totalNet = Convert.ToDouble(spnNet.EditValue);
			double amountPaid = Convert.ToDouble(spnAmountPaid.EditValue);

			if (amountPaid >= totalNet)
				chkIsPaymentEnough.Checked = true;

			spnRemainingAmount.EditValue = totalNet - amountPaid;
			isRemainingEdited = false;
		}

		private void spnRemainingAmount_EditValueChanged(object sender, EventArgs e)
		{
			if (isRemainingEdited || spnRemainingAmount.EditValue == null)
				return;

			isRemainingEdited = true;
			string[] strArry = spnRemainingAmount.Text.Split('(');

			if (Math.Abs(Convert.ToDouble(strArry[0])) < 0.0001)
			{
				spnRemainingAmount.Text = spnRemainingAmount.Text + "  (=)";
				spnRemainingAmount.BackColor = Color.LawnGreen;
			}
			else if (Convert.ToDouble(strArry[0]) > 0)
			{
				spnRemainingAmount.Text = spnRemainingAmount.Text + "  (عليـه)";
				spnRemainingAmount.BackColor = Color.Red;
			}
			else if (Convert.ToDouble(strArry[0]) < 0)
			{
				spnRemainingAmount.Text = spnRemainingAmount.Text + "  (لـــه)";
				spnRemainingAmount.BackColor = Color.Yellow;
			}

			isRemainingEdited = false;
		}

		#endregion

		#region Button Events

		private void btnAddToList_Click(object sender, EventArgs e)
		{
			if (Line_InventoryItemAvaliableQuantity == null || Line_InventoryItem_CU_ID == null ||
				Line_UnitMeasurment_CU_ID == null || Line_Quantity == null || Line_PricePerUnit == null)
				return;

			InventoryItem_cu inventoryItem =
				InventoryItem_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(Line_InventoryItem_CU_ID)));
			if (inventoryItem == null || inventoryItem.InventoryTrackingUnitMeasurment == null)
				return;

			UnitMeasurment_cu transactionUnitMeasurment =
				UnitMeasurment_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(Line_UnitMeasurment_CU_ID)));
			double transactionQuantity = Convert.ToDouble(Line_Quantity);

			if (InventoryBusinessLogicEngine.CanBeAdded((DB_InvoiceType)InvoiceTypeID, inventoryItem, InventoryHousing_CU_ID,
				transactionUnitMeasurment, transactionQuantity))
			{
				FinanceInvoiceDetail financeInvoiceDetail = MerkDBBusinessLogicEngine.CreateNew_FinanceInvoiceDetail(null,
					Line_InventoryItem_CU_ID, Line_PricePerUnit, Line_UnitMeasurment_CU_ID, Line_Quantity, InvoiceCreationDate,
					Line_DiscountAmount, Line_DiscountTypeID, Line_Description, Line_IsSurchargeApplied, 0);
				if (financeInvoiceDetail == null)
					return;

				if (FinanceInvoiceDetailsList == null)
					FinanceInvoiceDetailsList = new List<FinanceInvoiceDetail>();

				if (InventoryBusinessLogicEngine.List_ActiveFinanceInvoiceDetails == null)
					InventoryBusinessLogicEngine.List_ActiveFinanceInvoiceDetails = new List<FinanceInvoiceDetail>();
				if (FinanceInvoiceDetailsList.Count == 0)
					FinanceInvoiceDetailsList.Add(financeInvoiceDetail);
				else
				{
					if (FinanceInvoiceDetailsList.Exists(
						item =>
							Convert.ToInt32(item.InventoryItem_CU_ID).Equals(Convert.ToInt32(financeInvoiceDetail.InventoryItem_CU_ID)) &&
							Convert.ToInt32(item.UnitMeasurment_CU_ID).Equals(Convert.ToInt32(financeInvoiceDetail.UnitMeasurment_CU_ID))))
					{
						DialogResult result =
							XtraMessageBox.Show("قـد تمـت إضـافتــــه مـن قبــل." + "\r\n\r\n" + "هـل تـريــد إضـافتـــه ؟", "تنبيـــه",
								MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
						switch (result)
						{
							case DialogResult.Yes:
								FinanceInvoiceDetailsList.Add(financeInvoiceDetail);
								break;
						}
					}
					else
						FinanceInvoiceDetailsList.Add(financeInvoiceDetail);
				}
			}
			else
			{
				switch ((DB_InvoiceType)InvoiceTypeID)
				{
					case DB_InvoiceType.SellingInvoice:
						XtraMessageBox.Show(
							"لا يمكنـك الإضـافـــة، حيـث أن العــدد أكبــر مـن الحــد الأدنـى للمخــزون" + "\r\n\r\n" + "الحـــد الأدنـــــى : " +
							inventoryItem.StockMinLevel + " " + inventoryItem.InventoryTrackingUnitMeasurment.Name_P, "تنبيـــه",
							MessageBoxButtons.OK, MessageBoxIcon.Stop,
							MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
						break;
					case DB_InvoiceType.PurchasingInvoice:
						XtraMessageBox.Show(
							"لا يمكنـك الإضـافـــة، حيـث أن الكميـــة المضـافــــــة تخطــت الحـــد الأقصـــى للمخـــــزون" + "\r\n\r\n" +
							"الحـــد الأقصـــى : " + inventoryItem.StockMaxLevel + " " + inventoryItem.InventoryTrackingUnitMeasurment.Name_P,
							"تنبيـــه", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
						break;
				}
			}

			grdInventoryItems.DataSource = FinanceInvoiceDetailsList;
			InventoryBusinessLogicEngine.List_ActiveFinanceInvoiceDetails = FinanceInvoiceDetailsList;
			spnAccummulativeAmount.EditValue =
				spnAmountPaid.EditValue = FinancialBusinessLogicLibrary.GetTotalNet(FinanceInvoiceDetailsList);

			grdInventoryItems.RefreshDataSource();
			ClearControls();
		}

		private void simpleButton2_Click(object sender, EventArgs e)
		{
			if (SelectedFinanceInvoiceDetail == null)
				return;

			DialogResult result =
				XtraMessageBox.Show("هـل تـريـــد حــذف الصنـــف ? " + "\r\n" + SelectedFinanceInvoiceDetail.InventoryItemName,
					"تنبيــــه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
					DefaultBoolean.Default);
			switch (result)
			{
				case DialogResult.Yes:
					FinanceInvoiceDetailsList.Remove(SelectedFinanceInvoiceDetail);
					break;
			}

			grdInventoryItems.DataSource = FinanceInvoiceDetailsList;
			InventoryBusinessLogicEngine.List_ActiveFinanceInvoiceDetails = FinanceInvoiceDetailsList;
			spnAccummulativeAmount.EditValue = FinancialBusinessLogicLibrary.GetTotalNet(FinanceInvoiceDetailsList);
			grdInventoryItems.RefreshDataSource();
			ClearControls();
		}

		private void btnNewDedicatedPerson_Click(object sender, EventArgs e)
		{
			switch ((DB_InvoiceType)InvoiceTypeID)
			{
				case DB_InvoiceType.PurchasingInvoice:
				case DB_InvoiceType.ReturningPurchasingInvoice:
					BaseController<Supplier_cu>.ShowEditorControl(ref _supplierEditorViewer, this, null, null,
						EditorContainerType.Regular, ViewerName.Supplier_Viewer, DB_CommonTransactionType.CreateNew,
						"المـورديــــن", true);
					CommonViewsActions.FillGridlookupEdit(lkeDedicatedPerson, Supplier_cu.ItemsList, "FullName");
					break;
				case DB_InvoiceType.SellingInvoice:
				case DB_InvoiceType.ReturningSellingInvoice:
					BaseController<Customer_cu>.ShowEditorControl(ref _customerEditorViewer, this, null, null,
						EditorContainerType.Regular, ViewerName.Customer_Viewer, DB_CommonTransactionType.CreateNew,
						"العمـــلاء", true);
					CommonViewsActions.FillGridlookupEdit(lkeDedicatedPerson, Customer_cu.ItemsList, "FullName");
					break;
			}
		}

		private void btnNewLocation_Click(object sender, EventArgs e)
		{

		}

		private void btnNewFloor_Click(object sender, EventArgs e)
		{

		}

		private void btnNewInventoryHousing_Click(object sender, EventArgs e)
		{

		}

		#endregion

		#region TextEdit Events

		private void txtInventoryItemInternalCode_EditValueChanged(object sender, EventArgs e)
		{

		}

		private void txtInventoryItemBarcode_EditValueChanged(object sender, EventArgs e)
		{

		}

		#endregion

		private void gridView7_GotFocus(object sender, EventArgs e)
		{
			SelectedFinanceInvoiceDetail = CommonViewsActions.GetSelectedRowObject<FinanceInvoiceDetail>(gridView7);
		}

		#endregion

		#region Implementation of IFinanceInvoiceCreation

		public object InvoiceTypeID { get; set; }

		public object InvoicePaymentTypeID { get; set; }

		public object InvoiceSerialNumber
		{
			get { return txtInvoiceSerial.EditValue; }
			set { txtInvoiceSerial.EditValue = value; }
		}

		public object InvoiceCreationDate
		{
			get { return dtInvoiceCreation.EditValue; }
			set { dtInvoiceCreation.EditValue = value; }
		}

		public object InvoiceDescription
		{
			get { return txtInvoice_Description.EditValue; }
			set { txtInvoice_Description.EditValue = value; }
		}

		public object InvoiceIsSurchargeApplied { get; set; }

		public object Person_CU_ID
		{
			get { return lkeDedicatedPerson.EditValue; }
			set { lkeDedicatedPerson.EditValue = value; }
		}

		public object Location_CU_ID
		{
			get { return lkeLocations.EditValue; }
			set { lkeLocations.EditValue = value; }
		}

		public object Floor_CU_ID
		{
			get { return lkeFloors.EditValue; }
			set { lkeFloors.EditValue = value; }
		}

		public object InventoryHousing_CU_ID
		{
			get { return lkeInventoryHousing.EditValue; }
			set { lkeInventoryHousing.EditValue = value; }
		}

		public object Line_InventoryItem_Barcode
		{
			get { return txtInventoryItemBarcode.EditValue; }
			set { txtInventoryItemBarcode.EditValue = value; }
		}

		public object Line_InventoryItem_InternalCode
		{
			get { return txtInventoryItemInternalCode.EditValue; }
			set { txtInventoryItemInternalCode.EditValue = value; }
		}

		public object Line_InventoryItem_CU_ID
		{
			get { return lkeInventoryItems.EditValue; }
			set { lkeInventoryItems.EditValue = value; }
		}

		public object Line_UnitMeasurment_CU_ID
		{
			get { return lkeUnitMeasurments.EditValue; }
			set { lkeUnitMeasurments.EditValue = value; }
		}

		public object Line_InventoryItemAvaliableQuantity
		{
			get { return txtInventoryItemAvailableQuantity.EditValue; }
			set { txtInventoryItemAvailableQuantity.EditValue = value; }
		}

		public object Line_Quantity
		{
			get { return spnLineQuantity.EditValue; }
			set { spnLineQuantity.EditValue = value; }
		}

		public object Line_PricePerUnit
		{
			get { return spnLineProductPrice.EditValue; }
			set { spnLineProductPrice.EditValue = value; }
		}

		public object Line_DiscountAmount
		{
			get { return spnLineDiscount.EditValue; }
			set { spnLineDiscount.EditValue = value; }
		}

		public object Line_DiscountTypeID
		{
			get
			{
				if (chkLineDiscountType.IsOn)
					return (int) DB_DiscountType.Amount;
				return (int) DB_DiscountType.Percentage;
			}
			set
			{
				chkLineDiscountType.IsOn = Convert.ToBoolean(value);
			}
		}

		public object Line_Net
		{
			get { return txtLineNet.EditValue; }
			set { txtLineNet.EditValue = value; }
		}

		public object Line_Description
		{
			get { return txtLine_Description.EditValue; }
			set { txtLine_Description.EditValue = value; }
		}

		public object Line_IsSurchargeApplied { get; set; }

		public object PaymentTypeID
		{
			get { return chkPaymentType.EditValue; }
			set { chkPaymentType.EditValue = value; }
		}

		public object IsRemainingReturned
		{
			get { return chkResetRemaining.Checked; }
			set { chkResetRemaining.Checked = Convert.ToBoolean(value); }
		}

		public object AmountPaid
		{
			get { return spnAmountPaid.EditValue; }
			set { spnAmountPaid.EditValue = value; }
		}

		public object IsPaymentEnough
		{
			get { return chkIsPaymentEnough.Checked; }
			set { chkIsPaymentEnough.Checked = Convert.ToBoolean(value); }
		}

		public List<FinanceInvoiceDetail> FinanceInvoiceDetailsList { get; set; }

		#endregion

		#region Implementation of IViewerDataRelated

		public object ViewerDataRelated { get; set; }

		#endregion

		private void chkIsPaymentEnough_CheckedChanged(object sender, EventArgs e)
		{
		
		}
	}
}
