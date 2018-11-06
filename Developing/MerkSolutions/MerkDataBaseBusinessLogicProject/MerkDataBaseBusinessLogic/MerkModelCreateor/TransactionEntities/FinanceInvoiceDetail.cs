using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class FinanceInvoiceDetail : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<FinanceInvoiceDetail> _items;
		public static List<FinanceInvoiceDetail> ItemsList = new List<FinanceInvoiceDetail>();

		#region ColumnNames

		public static String Serial_ColumnaName
		{
			get { return "Serial"; }
		}

		#endregion

		public override bool LoadFromDB
		{
			get { return false; }
		}

		public override DBCommonEntitiesType TableType
		{
			get { return DBCommonEntitiesType.TransactionsEntities; }
		}

		public override bool LoadItemsList()
		{
			ItemsList.Clear();

			ItemsList = DBContext_External.FinanceInvoiceDetails.ToList();
			return true;
		}

		public int TableIdentityID { get; set; }

		public List<string> ChildrenItemsList { get; private set; }

		public string EntityName
		{
			get { return "FinanceInvoiceDetail"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.FinanceInvoiceDetails.FirstOrDefault(item => item.FinanceInvoiceID.Equals(id));
		}

		public string InventoryHousingName
		{
			get
			{
				if (InventoryHousing_CU_ID != null)
				{
					InventoryHousing_cu inventoryHousing =
						InventoryHousing_cu.ItemsList.Find(
							item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(InventoryHousing_CU_ID)));
					if (inventoryHousing != null)
						return inventoryHousing.InventoryHousingFullName;
				}

				return string.Empty;
			}
		}

		public string InventoryItemName
		{
			get
			{
				if (InventoryItem_CU_ID != null)
				{
					InventoryItem_cu inventoryItem =
						InventoryItem_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(InventoryItem_CU_ID)));
					if (inventoryItem != null)
						return inventoryItem.Name_P;
				}

				return string.Empty;
			}
		}

		public string UnitMeasurmentName
		{
			get
			{
				if (InventoryItem_CU_ID != null)
				{
					UnitMeasurment_cu unitMeasurment =
						UnitMeasurment_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(UnitMeasurment_CU_ID)));
					if (unitMeasurment != null)
						return unitMeasurment.Name_P;
				}

				return string.Empty;
			}
		}

		public double NetPrice { get
		{
			double priceperUnit = Convert.ToDouble(PricePerUnit);
			double quantity = Convert.ToDouble(Quantity);
			double discount = 0;
			if (DiscountAmount != null)
				discount = Convert.ToDouble(DiscountAmount);
			return (priceperUnit*quantity) - discount;
		}}
	}
}
