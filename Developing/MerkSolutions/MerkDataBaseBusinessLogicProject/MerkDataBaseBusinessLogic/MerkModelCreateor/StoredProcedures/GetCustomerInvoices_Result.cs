using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class GetCustomerInvoices_Result : DBCommon, IDBCommon
	{
		public static List<GetCustomerInvoices_Result> GetItemsList(object customerID, object isOnDuty, object invoiceTypeID,
			object isPaymentEnough, object isFinanciallyReviewed, object isFinanciallyCompleted)
		{
			return DBContext_External.GetCustomerInvoices((int?) customerID, (bool?) isOnDuty, (int?) invoiceTypeID,
				(bool?) isPaymentEnough, (bool?) isFinanciallyReviewed, (bool?) isFinanciallyCompleted).ToList();
		}

		public string RemainingAmount
		{
			get
			{
				double remainingAmount = Convert.ToDouble(Requested_VS_Payment)*-1;
				if (remainingAmount < 0)
					return "( " + remainingAmount + " )";
				return remainingAmount.ToString();
			}
		}
	}
}
