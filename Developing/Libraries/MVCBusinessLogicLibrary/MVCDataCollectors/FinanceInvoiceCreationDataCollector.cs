using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationConfiguration;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCDataCollectors
{
	public class FinanceInvoiceCreationDataCollector<TEntity> :
		AbstractDataCollector<TEntity>, IFinanceInvoiceCreation
		where TEntity : DBCommon, IDBCommon, new()
	{
		#region Overrides of AbstractDataCollector<TEntity>

		public override AbstractDataCollector<TEntity> ActiveCollector { get; set; }
		public override AbstractDataCollector<TEntity> ParentActiveCollector { get; set; }
		public override bool Collect(AbstractDataCollector<TEntity> collector)
		{
			if (collector == null)
				return false;

			ActiveCollector = collector;

			ID = ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			((FinanceInvoice)ActiveDBItem).DBCommonTransactionType = CommonTransactionType;
			if (InvoiceTypeID != null)
				((FinanceInvoice)ActiveDBItem).InvoiceType_P_ID = (int)InvoiceTypeID;
			if (InvoicePaymentTypeID != null)
				((FinanceInvoice)ActiveDBItem).InvoicePaymentType_P_ID = (int)InvoicePaymentTypeID;
			if (InvoiceCreationDate != null)
				((FinanceInvoice)ActiveDBItem).InvoiceCreationDate = Convert.ToDateTime(InvoiceCreationDate);

			DB_InvoiceType invoiceType = (DB_InvoiceType)Convert.ToInt32(((FinanceInvoice)ActiveDBItem).InvoiceType_P_ID);
			switch (invoiceType)
			{
				case DB_InvoiceType.SellingInvoice:
				case DB_InvoiceType.ReturningSellingInvoice:
					if (Person_CU_ID != null)
						((FinanceInvoice)ActiveDBItem).Customer_CU_ID = Convert.ToInt32(Person_CU_ID);
					break;
				case DB_InvoiceType.PurchasingInvoice:
				case DB_InvoiceType.ReturningPurchasingInvoice:
					if (Person_CU_ID != null)
						((FinanceInvoice)ActiveDBItem).Supplier_CU_ID = Convert.ToInt32(Person_CU_ID);
					break;
			}

			((FinanceInvoice)ActiveDBItem).IsPrinted = false;
			if (InvoiceSerialNumber != null)
				((FinanceInvoice)ActiveDBItem).InvoiceSerial = InvoiceSerialNumber.ToString();
			((FinanceInvoice)ActiveDBItem).PrintingDate = null;
			((FinanceInvoice)ActiveDBItem).IsPaymentsEnough = Convert.ToBoolean(IsPaymentEnough);
			if (InvoiceDescription != null)
				((FinanceInvoice)ActiveDBItem).Description = InvoiceDescription.ToString();
			((FinanceInvoice)ActiveDBItem).Description = null;
			((FinanceInvoice)ActiveDBItem).IsOnDuty = true;
			((FinanceInvoice)ActiveDBItem).IsFinanciallyReviewed = false;
			((FinanceInvoice)ActiveDBItem).IsFinanciallyCompleted = false;
			((FinanceInvoice)ActiveDBItem).IsCancelled = false;
			((FinanceInvoice)ActiveDBItem).CancelledBy = null;
			((FinanceInvoice)ActiveDBItem).CancellationDate = null;
			((FinanceInvoice)ActiveDBItem).IsSuspended = false;
			if (UserID != null)
				((FinanceInvoice)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			double accummulativeRequiredAmount = 0;
			if (FinanceInvoiceDetailsList != null && FinanceInvoiceDetailsList.Count > 0)
				foreach (FinanceInvoiceDetail invoiceDetail in FinanceInvoiceDetailsList)
				{
					if (UserID != null)
						invoiceDetail.InsertedBy = Convert.ToInt32(UserID);
					((FinanceInvoice)ActiveDBItem).FinanceInvoiceDetails.Add(invoiceDetail);
					accummulativeRequiredAmount = accummulativeRequiredAmount +
												  Convert.ToDouble(invoiceDetail.PricePerUnit) * Convert.ToDouble(invoiceDetail.Quantity);
				}
			else
				return false;

			((FinanceInvoice)ActiveDBItem).FinanceInvoiceShare = new FinanceInvoiceShare();
			((FinanceInvoice)ActiveDBItem).FinanceInvoiceShare.TotalRequestedAmount = accummulativeRequiredAmount;
			if (UserID != null)
				((FinanceInvoice)ActiveDBItem).FinanceInvoiceShare.InsertedBy = Convert.ToInt32(UserID);
			if (AmountPaid != null)
				((FinanceInvoice) ActiveDBItem).FinanceInvoiceShare.TotalPayment = Convert.ToDouble(AmountPaid);

			if (Convert.ToBoolean(PaymentTypeID) && AmountPaid != null &&
				Convert.ToDouble(AmountPaid) >= 0)
			{
				FinanceInvoicePayment invoicePayment = DBCommon.CreateNewDBEntity<FinanceInvoicePayment>();
				invoicePayment.Amount = Convert.ToDouble(AmountPaid);
				if (InvoiceCreationDate != null)
					invoicePayment.Date = Convert.ToDateTime(InvoiceCreationDate);
				invoicePayment.PaymentType_P_ID = (int)DB_PaymentType.CashPayment;
				invoicePayment.PaymentSerial = "123";
				invoicePayment.IsOnDuty = true;
				invoicePayment.Description = "sdfsdfsdf";
				invoicePayment.IsRemainingReturned = Convert.ToBoolean(IsRemainingReturned);
				if (UserID != null)
					invoicePayment.InsertedBy = Convert.ToInt32(UserID);
				((FinanceInvoice) ActiveDBItem).FinanceInvoicePayments.Add(invoicePayment);
			}

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).ViewerID; }
		}

		public override object UserID
		{
			get
			{
				if (ApplicationStaticConfiguration.ActiveLoginUser != null)
					return ApplicationStaticConfiguration.ActiveLoginUser.Person_CU_ID;
				return null;
			}
		}

		public override object EditingDate
		{
			get { return DateTime.Now; }
		}

		public override object IsOnDUty { get; set; }

		public override DB_CommonTransactionType CommonTransactionType
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).CommonTransactionType; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).CommonTransactionType = value; }
		}

		public override string HeaderTitle
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).HeaderTitle; }
		}

		public override string GridXML
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).GridXML; }
		}

		public override List<IViewer> RelatedViewers { get; set; }
		public override void ClearControls()
		{
			//((IFinanceInvoiceCreation) ActiveCollector.ActiveViewer).FinanceInvoiceDetailsList = null;
		}

		public override void FillControls()
		{

		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<FinanceInvoice>();
				((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			return ((FinanceInvoice)ActiveCollector.ActiveDBItem).SaveChanges();
		}

		public override bool AfterSave()
		{
			if ((FinanceInvoice)ActiveDBItem == null)
				return false;

			if (FinanceInvoiceDetailsList != null && FinanceInvoiceDetailsList.Count > 0)
			{
				foreach (FinanceInvoiceDetail invoiceDetail in FinanceInvoiceDetailsList.OrderBy(item => item.UnitMeasurment_CU_ID))
				{
					InventoryItemTransaction inventoryItemTransaction = DBCommon.CreateNewDBEntity<InventoryItemTransaction>();
					inventoryItemTransaction.UnitMeasurment_CU_ID = Convert.ToInt32(invoiceDetail.UnitMeasurment_CU_ID);
					inventoryItemTransaction.InventoryItem_CU_ID = Convert.ToInt32(invoiceDetail.InventoryItem_CU_ID);
					inventoryItemTransaction.InventoryHousing_CU_ID = Convert.ToInt32(invoiceDetail.InventoryHousing_CU_ID);
					inventoryItemTransaction.Quantity = Convert.ToDouble(invoiceDetail.Quantity);
					inventoryItemTransaction.Date = Convert.ToDateTime(invoiceDetail.Date);
					inventoryItemTransaction.IsOnDuty = true;
					if (UserID != null)
						inventoryItemTransaction.InsertedBy = Convert.ToInt32(UserID);
					DB_InvoiceType invoiceType = (DB_InvoiceType)Convert.ToInt32(((FinanceInvoice)ActiveDBItem).InvoiceType_P_ID);
					switch (invoiceType)
					{
						case DB_InvoiceType.SellingInvoice:
						case DB_InvoiceType.ReturningPurchasingInvoice:
							inventoryItemTransaction.TransactionFactor = -1;
							inventoryItemTransaction.InventoryItemTransactionType_P_ID =
								Convert.ToInt32(DB_InventoryItemTransactionType.OutputTransaction);
							break;
						case DB_InvoiceType.PurchasingInvoice:
						case DB_InvoiceType.ReturningSellingInvoice:
							inventoryItemTransaction.TransactionFactor = 1;
							inventoryItemTransaction.InventoryItemTransactionType_P_ID =
								Convert.ToInt32(DB_InventoryItemTransactionType.InputTransaction);
							break;
					}
					inventoryItemTransaction.SaveChanges();
				}
			}

			FinanceInvoiceDetailsList = null;

			return true;
		}

		#endregion

		#region Implementation of IFinanceInvoiceCreation

		public object InvoiceTypeID
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).InvoiceTypeID; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).InvoiceTypeID = value; }
		}

		public object InvoicePaymentTypeID
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).InvoicePaymentTypeID; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).InvoicePaymentTypeID = value; }
		}

		public object InvoiceSerialNumber
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).InvoiceSerialNumber; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).InvoiceSerialNumber = value; }
		}

		public object InvoiceCreationDate
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).InvoiceCreationDate; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).InvoiceCreationDate = value; }
		}

		public object InvoiceDescription
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).InvoiceDescription; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).InvoiceDescription = value; }
		}

		public object InvoiceIsSurchargeApplied
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).InvoiceIsSurchargeApplied; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).InvoiceIsSurchargeApplied = value; }
		}

		public object Person_CU_ID
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Person_CU_ID; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Person_CU_ID = value; }
		}

		public object Location_CU_ID
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Location_CU_ID; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Location_CU_ID = value; }
		}

		public object Floor_CU_ID
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Floor_CU_ID; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Floor_CU_ID = value; }
		}

		public object InventoryHousing_CU_ID
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).InventoryHousing_CU_ID; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).InventoryHousing_CU_ID = value; }
		}

		public object Line_InventoryItem_Barcode
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Line_InventoryItem_Barcode; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Line_InventoryItem_Barcode = value; }
		}

		public object Line_InventoryItem_InternalCode
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Line_InventoryItem_InternalCode; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Line_InventoryItem_InternalCode = value; }
		}

		public object Line_InventoryItem_CU_ID
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Line_InventoryItem_CU_ID; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Line_InventoryItem_CU_ID = value; }
		}

		public object Line_UnitMeasurment_CU_ID
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Line_UnitMeasurment_CU_ID; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Line_UnitMeasurment_CU_ID = value; }
		}

		public object Line_InventoryItemAvaliableQuantity
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Line_InventoryItemAvaliableQuantity; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Line_InventoryItemAvaliableQuantity = value; }
		}

		public object Line_Quantity
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Line_Quantity; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Line_Quantity = value; }
		}

		public object Line_PricePerUnit
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Line_PricePerUnit; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Line_PricePerUnit = value; }
		}

		public object Line_DiscountAmount
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Line_DiscountAmount; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Line_DiscountAmount = value; }
		}

		public object Line_DiscountTypeID
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Line_DiscountTypeID; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Line_DiscountTypeID = value; }
		}

		public object Line_Net
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Line_Net; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Line_Net = value; }
		}

		public object Line_Description
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Line_Description; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Line_Description = value; }
		}

		public object Line_IsSurchargeApplied
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Line_IsSurchargeApplied; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).Line_IsSurchargeApplied = value; }
		}

		public object PaymentTypeID
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).PaymentTypeID; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).PaymentTypeID = value; }
		}

		public object IsRemainingReturned
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).IsRemainingReturned; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).IsRemainingReturned = value; }
		}

		public object AmountPaid
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).AmountPaid; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).AmountPaid = value; }
		}

		public object IsPaymentEnough
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).IsPaymentEnough; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).IsPaymentEnough = value; }
		}

		public List<FinanceInvoiceDetail> FinanceInvoiceDetailsList
		{
			get { return ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).FinanceInvoiceDetailsList; }
			set { ((IFinanceInvoiceCreation)ActiveCollector.ActiveViewer).FinanceInvoiceDetailsList = value; }
		}

		#endregion
	}
}
