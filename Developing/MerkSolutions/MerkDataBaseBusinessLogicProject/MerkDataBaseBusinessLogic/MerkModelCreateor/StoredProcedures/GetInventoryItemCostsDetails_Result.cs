using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class GetInventoryItemCostsDetails_Result : DBCommon, IDBCommon
	{
		public static List<GetInventoryItemCostsDetails_Result> GetItemsList(object rawMaterialID,
			 object colorID, object itemID, object additionalCostToBeAdded)
		{
			return null;
			//return
			//	DBContext_External.GetInventoryItemCostsDetails((int?) rawMaterialID,
			//		(int?) colorID, (int?)itemID,
			//		(int?)additionalCostToBeAdded).ToList();
		}

	}
}
