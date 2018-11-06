using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class GetInvoicesPaymentBriefReport_Result : DBCommon, IDBCommon
	{
		public static List<GetInvoicesPaymentBriefReport_Result> GetItemsList(object invoiceID, object invoiceTypeID,
			object paymentTypeID, object fromDate, object toDate, object userID)
		{
			return
				DBContext_External.GetInvoicesPaymentBriefReport((int?) invoiceID, (int?) invoiceTypeID, (int?) paymentTypeID,
					(DateTime?) fromDate, (DateTime?) toDate, (int?) userID).ToList();
		}
	}
}
