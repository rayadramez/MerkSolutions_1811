using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class GetRawMaterialCostPrices_Result : DBCommon, IDBCommon
	{
		public static List<GetRawMaterialCostPrices_Result> GetItemsList(object rawMaterialID, object fromDate, object toDate, object userID)
		{
			return
				DBContext_External.GetRawMaterialCostPrices((int?) rawMaterialID, (DateTime?) fromDate,
					(DateTime?) toDate, (int?) userID).ToList();
		}

		public RawMaterials_cu RawMaterial
		{
			get
			{
				RawMaterials_cu rawMaterial =
					RawMaterials_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(RawID)));
				return rawMaterial;
			}
		}

		public string RawMaterialFullName
		{
			get { return RawMaterial.RawMaterialFullName; }
		}
	}
}
