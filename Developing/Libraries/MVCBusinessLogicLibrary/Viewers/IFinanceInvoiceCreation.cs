using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;

namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IFinanceInvoiceCreation : IViewer
	{
		object InvoiceTypeID { get; set; }
		object InvoicePaymentTypeID { get; set; }
		object InvoiceSerialNumber { get; set; }
		object InvoiceCreationDate { get; set; }
		object InvoiceDescription { get; set;  }
		object InvoiceIsSurchargeApplied { get; set; }
		object Person_CU_ID { get; set; }
		object Location_CU_ID { get; set; }
		object Floor_CU_ID { get; set; }
		object InventoryHousing_CU_ID { get; set; }
		object Line_InventoryItem_Barcode { get; set; }
		object Line_InventoryItem_InternalCode { get; set; }
		object Line_InventoryItem_CU_ID { get; set; }
		object Line_UnitMeasurment_CU_ID { get; set; }
		object Line_InventoryItemAvaliableQuantity { get; set; }
		object Line_Quantity { get; set; }
		object Line_PricePerUnit { get; set; }
		object Line_DiscountAmount { get; set; }
		object Line_DiscountTypeID { get; set; }
		object Line_Net { get; set; }
		object Line_Description { get; set; }
		object Line_IsSurchargeApplied { get; set; }
		object PaymentTypeID { get; set; }
		object IsRemainingReturned { get; set; }
		object AmountPaid { get; set; }
		object IsPaymentEnough { get; set; }
		List<FinanceInvoiceDetail> FinanceInvoiceDetailsList { get; set; }
	}
}
