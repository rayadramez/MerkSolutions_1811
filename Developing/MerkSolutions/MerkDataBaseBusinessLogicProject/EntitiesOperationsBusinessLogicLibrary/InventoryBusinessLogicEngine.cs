using System;
using System.Collections.Generic;
using System.Linq;

namespace MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary
{
	public enum ComparingUnitMeasurment
	{
		None = 0,
		FromUnit_LargerThan_ToUnit = 1,
		ToUnit_LargerThan_FromUnit = 2,
		FromUnit_EqualTo_ToUnit = 3,
	}

	public class InventoryBusinessLogicEngine
	{
		public static DB_InvoiceType ActiveInvoiceType { get; set; }

		public static List<FinanceInvoiceDetail> List_ActiveFinanceInvoiceDetails { get; set; }

		public static bool IsFloorOfLocationHasMainInventory(int floorID)
		{
			Floor_cu floor = Floor_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(floorID)));
			if (floor == null)
				return false;

			if (floor.Location_CU_ID != null)
				return IsLocationHasMainInventory(Convert.ToInt32(floor.Location_CU_ID));

			return false;
		}

		public static bool IsLocationHasMainInventory(int locationID)
		{
			Location_cu location = Location_cu.ItemsList.Find(item => item.ID.Equals(locationID));
			if (location == null)
				return false;

			return IsLocationHasMainInventory(location);
		}

		public static bool IsLocationHasMainInventory(Location_cu location)
		{
			if (location == null)
				return false;

			List<Floor_cu> floorsList =
				Floor_cu.ItemsList.FindAll(item => Convert.ToInt32(item.Location_CU_ID).Equals(Convert.ToInt32(location.ID)));
			List<InventoryHousing_cu> inventoryHousingList = new List<InventoryHousing_cu>();

			foreach (Floor_cu floorCu in floorsList)
			{
				if (floorCu.Location_CU_ID == null)
					continue;

				InventoryHousing_cu internalLocation =
					InventoryHousing_cu.ItemsList.Find(item => Convert.ToInt32(item.Floor_CU_ID).Equals(Convert.ToInt32(floorCu.ID)));
				if (internalLocation == null)
					continue;

				inventoryHousingList.Add(internalLocation);
			}

			if (inventoryHousingList.Count == 0)
				return false;

			foreach (InventoryHousing_cu inventoryHousingCu in inventoryHousingList)
			{
				if (inventoryHousingCu.IsMain)
					return true;
			}

			return false;
		}

		public static double GetInventoryItemSellingPrice(object inventoryItemID, object unitMeasurmentID, object date,
			object personID, DB_PriceType priceType)
		{
			double price = 0;
			if (inventoryItemID == null || unitMeasurmentID == null || date == null)
				return price;

			InventoryItem_cu inventoryItem =
				InventoryItem_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(inventoryItemID)));
			if (inventoryItem == null)
				return price;

			switch (priceType)
			{
				case DB_PriceType.PurchasingPrice:
					price = inventoryItem.DefaultCost != null ? Convert.ToDouble(inventoryItem.DefaultCost) : 0;
					break;
				case DB_PriceType.SellingPrice:
					price = inventoryItem.DefaultSellingPrice != null ? Convert.ToDouble(inventoryItem.DefaultSellingPrice) : 0;
					break;
			}

			List<InventoryItemPrice_cu> inventoryItemPricesList;
				//InventoryItemPrice_cu.ItemsList.OrderBy(item => item.Date).ToList();

			inventoryItemPricesList = GetInventoryItemPricesList(inventoryItemID, unitMeasurmentID, date, priceType, personID);
			if (inventoryItemPricesList == null || inventoryItemPricesList.Count == 0)
				inventoryItemPricesList = GetInventoryItemPricesList(inventoryItemID, unitMeasurmentID, date, priceType, null);

			if (inventoryItemPricesList != null && inventoryItemPricesList.Count > 0)
			{
				InventoryItemPrice_cu inventoryItemPrice = null;
				DateTime previousDate;
				foreach (InventoryItemPrice_cu inventoryItemPriceCu in inventoryItemPricesList)
				{
					previousDate = Convert.ToDateTime(inventoryItemPriceCu.Date);

					if (Convert.ToDateTime(date).Date >= previousDate.Date)
					{
						inventoryItemPrice = inventoryItemPriceCu;
						continue;
					}

					break;
				}
				if (personID != null)
					foreach (InventoryItemPrice_cu inventoryItemPriceCu in
							inventoryItemPricesList.FindAll(item =>
								Convert.ToInt32(item.Customer_CU_ID).Equals(Convert.ToInt32(personID))))
					{
						inventoryItemPrice = inventoryItemPriceCu;
						previousDate = Convert.ToDateTime(inventoryItemPriceCu.Date);

						if (Convert.ToDateTime(date) <= previousDate)
							continue;

						break;
					}

				if (inventoryItemPrice != null)
					price = Convert.ToDouble(inventoryItemPrice.Price);
			}

			return price;
		}

		public static List<InventoryItemPrice_cu> GetInventoryItemPricesList(object inventoryItemID, object unitMeasurmentID, object date,
			DB_PriceType priceType, object personID)
		{
			if (inventoryItemID == null || unitMeasurmentID == null)
				return null;

			if (date == null)
				date = DateTime.Now;

			List<InventoryItemPrice_cu> inventoryItemPricesList =
				InventoryItemPrice_cu.ItemsList.OrderBy(item => item.Date).ToList();

			if (inventoryItemPricesList.Count == 0)
				return null;

			InventoryItem_UnitMeasurment_cu bridge =
				InventoryItem_UnitMeasurment_cu.ItemsList.Find(
					item =>
						Convert.ToInt32(item.InventoryItem_CU_ID).Equals(Convert.ToInt32(inventoryItemID)) &&
						Convert.ToInt32(item.UnitMeasurment_CU_ID).Equals(Convert.ToInt32(unitMeasurmentID)));
			if (bridge == null)
				return null;

			switch (priceType)
			{
				case DB_PriceType.SellingPrice:
					inventoryItemPricesList =
						InventoryItemPrice_cu.ItemsList.OrderBy(item => item.Date)
							.ToList()
							.FindAll(
								item =>
									Convert.ToInt32(item.InventoryItem_CU_ID).Equals(Convert.ToInt32(inventoryItemID)) &&
									Convert.ToInt32(item.InventoryItem_UnitMeasurment_CU_ID).Equals(Convert.ToInt32(bridge.ID)) &&
									Convert.ToInt32(item.PriceType_P_ID).Equals(Convert.ToInt32(priceType)) && item.Customer_CU_ID == null);
					if (personID != null)
						return InventoryItemPrice_cu.ItemsList.OrderBy(item => item.Date)
							.ToList()
							.FindAll(
								item =>
									Convert.ToInt32(item.InventoryItem_CU_ID).Equals(Convert.ToInt32(inventoryItemID)) &&
									Convert.ToInt32(item.InventoryItem_UnitMeasurment_CU_ID).Equals(Convert.ToInt32(bridge.ID)) &&
									Convert.ToInt32(item.PriceType_P_ID).Equals(Convert.ToInt32(priceType)) &&
									Convert.ToInt32(item.Customer_CU_ID).Equals(Convert.ToInt32(personID)));
					break;
				case DB_PriceType.PurchasingPrice:
					inventoryItemPricesList =
						InventoryItemPrice_cu.ItemsList.OrderBy(item => item.Date)
							.ToList()
							.FindAll(
								item =>
									Convert.ToInt32(item.InventoryItem_CU_ID).Equals(Convert.ToInt32(inventoryItemID)) &&
									Convert.ToInt32(item.InventoryItem_UnitMeasurment_CU_ID).Equals(Convert.ToInt32(bridge.ID)) &&
									Convert.ToInt32(item.PriceType_P_ID).Equals(Convert.ToInt32(priceType)) && item.Supplier_CU_ID == null);
					if (personID != null)
						return InventoryItemPrice_cu.ItemsList.OrderBy(item => item.Date)
							.ToList()
							.FindAll(
								item =>
									Convert.ToInt32(item.InventoryItem_CU_ID).Equals(Convert.ToInt32(inventoryItemID)) &&
									Convert.ToInt32(item.InventoryItem_UnitMeasurment_CU_ID).Equals(Convert.ToInt32(bridge.ID)) &&
									Convert.ToInt32(item.PriceType_P_ID).Equals(Convert.ToInt32(priceType)) &&
									Convert.ToInt32(item.Supplier_CU_ID).Equals(Convert.ToInt32(personID)));
					break;
			}

			return inventoryItemPricesList;
		}

		public static double GetInventoryItemAvailableQuantity(object inventoryItemID, object changeToUnitID,
			object inventoryHousingID = null)
		{
			if (inventoryItemID == null)
				return 0;

			double accummuldativeQuanity = 0;
			double quantity = 0;

			InventoryItem_cu inventoryItem =
				InventoryItem_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(inventoryItemID)));
			if (inventoryItem == null || inventoryItem.InventoryTrackingUnitMeasurment == null)
				return 0;
			UnitMeasurment_cu inventoryTrackingUnitMeasurment = inventoryItem.InventoryTrackingUnitMeasurment;
			UnitMeasurment_cu changeToUnit = null;
			if (changeToUnitID == null)
				changeToUnit = inventoryTrackingUnitMeasurment;
			else
				changeToUnit =
					UnitMeasurment_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(changeToUnitID)));
			if (changeToUnit == null)
				return 0;

			List<InventoryItemTransaction> inventoryItemTransactionsList;
			if (inventoryHousingID != null)
				inventoryItemTransactionsList =
					inventoryItem.InventoryItemTransactions.ToList()
						.FindAll(
							item => Convert.ToBoolean(item.IsOnDuty) &&
									Convert.ToInt32(item.InventoryHousing_CU_ID).Equals(Convert.ToInt32(inventoryHousingID)));
			else
				inventoryItemTransactionsList =
					inventoryItem.InventoryItemTransactions.ToList().FindAll(item => Convert.ToBoolean(item.IsOnDuty));

			foreach (InventoryItemTransaction inventoryItemInventoryHousing in inventoryItemTransactionsList)
			{
				UnitMeasurment_cu transactionUnitMeasurment =
					UnitMeasurment_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(inventoryItemInventoryHousing.UnitMeasurment_CU_ID)));
				if(transactionUnitMeasurment == null)
					continue;
				ComparingUnitMeasurment comparingUnitMeasurment = CompareUnitMeasurment(transactionUnitMeasurment,
					changeToUnit);
				double encapsulatedQuantity;
				switch (comparingUnitMeasurment)
				{
					case ComparingUnitMeasurment.FromUnit_EqualTo_ToUnit:
						accummuldativeQuanity = accummuldativeQuanity +
												inventoryItemInventoryHousing.TransactionFactor *
												Convert.ToDouble(inventoryItemInventoryHousing.Quantity);
						break;
					case ComparingUnitMeasurment.ToUnit_LargerThan_FromUnit:
						encapsulatedQuantity = GetEncapsulatedQuantity(transactionUnitMeasurment, changeToUnit);
						accummuldativeQuanity = accummuldativeQuanity +
												inventoryItemInventoryHousing.TransactionFactor *
												(Convert.ToDouble(inventoryItemInventoryHousing.Quantity) / encapsulatedQuantity);
						break;
					case ComparingUnitMeasurment.FromUnit_LargerThan_ToUnit:
						encapsulatedQuantity = GetEncapsulatedQuantity(transactionUnitMeasurment, changeToUnit);
						accummuldativeQuanity = accummuldativeQuanity +
												inventoryItemInventoryHousing.TransactionFactor *
												(Convert.ToDouble(inventoryItemInventoryHousing.Quantity) * encapsulatedQuantity);
						break;
				}
			}

			if (List_ActiveFinanceInvoiceDetails == null || List_ActiveFinanceInvoiceDetails.Count == 0)
				return accummuldativeQuanity;

			int factor = 0;
			switch (ActiveInvoiceType)
			{
				case DB_InvoiceType.PurchasingInvoice:
				case DB_InvoiceType.ReturningSellingInvoice:
					factor = (int)InventoryItemTransactionFactor.OneIn;
					break;
				case DB_InvoiceType.SellingInvoice:
				case DB_InvoiceType.ReturningPurchasingInvoice:
					factor = (int)InventoryItemTransactionFactor.NegativeOneOut;
					break;
			}

			foreach (FinanceInvoiceDetail financeInvoiceDetail in List_ActiveFinanceInvoiceDetails)
			{
				ComparingUnitMeasurment comparingUnitMeasurment = CompareUnitMeasurment(financeInvoiceDetail.UnitMeasurment_CU_ID,
					changeToUnit.ID);
				double encapsulatedQuantity;

				switch (comparingUnitMeasurment)
				{
					case ComparingUnitMeasurment.FromUnit_EqualTo_ToUnit:
						accummuldativeQuanity = accummuldativeQuanity + factor * Convert.ToDouble(financeInvoiceDetail.Quantity);
						break;
					case ComparingUnitMeasurment.ToUnit_LargerThan_FromUnit:
						encapsulatedQuantity = GetEncapsulatedQuantity(financeInvoiceDetail.UnitMeasurment_CU_ID, changeToUnit.ID);
						accummuldativeQuanity = accummuldativeQuanity +
												factor * (Convert.ToDouble(financeInvoiceDetail.Quantity) / encapsulatedQuantity);
						break;
					case ComparingUnitMeasurment.FromUnit_LargerThan_ToUnit:
						encapsulatedQuantity = GetEncapsulatedQuantity(financeInvoiceDetail.UnitMeasurment_CU_ID, changeToUnit.ID);
						accummuldativeQuanity = accummuldativeQuanity +
												factor * (Convert.ToDouble(financeInvoiceDetail.Quantity) * encapsulatedQuantity);
						break;
				}
			}

			return accummuldativeQuanity;
		}

		public static List<UnitMeasurment_cu> GetInventoryItemRegisteredUnitMeasurments(int inventoryItemID)
		{
			InventoryItem_cu inventoryItem =
				InventoryItem_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(inventoryItemID));
			if (inventoryItem == null)
				return null;

			List<UnitMeasurment_cu> UnitMeasurmentsList = new List<UnitMeasurment_cu>();
			List<InventoryItem_UnitMeasurment_cu> registeredUnitMeasurmentsList =
				InventoryItem_UnitMeasurment_cu.ItemsList.FindAll(
					item => Convert.ToInt32(item.InventoryItem_CU_ID).Equals(Convert.ToInt32(inventoryItem.ID)));
			if (registeredUnitMeasurmentsList.Count == 0)
				return null;

			foreach (InventoryItem_UnitMeasurment_cu unitMeasurmentCu in registeredUnitMeasurmentsList)
			{
				UnitMeasurment_cu unitMesurment =
					UnitMeasurment_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(unitMeasurmentCu.UnitMeasurment_CU_ID)));
				if (unitMesurment == null)
					continue;

				if (UnitMeasurmentsList.Count > 0 &&
					!UnitMeasurmentsList.Exists(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(unitMesurment.ID))))
					UnitMeasurmentsList.Add(unitMesurment);
				else
					UnitMeasurmentsList.Add(unitMesurment);
			}

			return ReOrderUnitMeasurments(UnitMeasurmentsList);
		}

		public static UnitMeasurment_cu GetInventoryTrackingUnitMeasurment(object inventoryItemID)
		{
			if (inventoryItemID == null)
				return null;

			InventoryItem_cu inventoryItem =
				InventoryItem_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(inventoryItemID)));
			if (inventoryItem == null)
				return null;

			return GetInventoryTrackingUnitMeasurment(inventoryItem);
		}

		public static UnitMeasurment_cu GetInventoryTrackingUnitMeasurment(InventoryItem_cu inventoryItem)
		{
			if (inventoryItem == null)
				return null;

			return inventoryItem.InventoryTrackingUnitMeasurment;
		}

		public static List<UnitMeasurment_cu> GetUnitMeasurmentTree(object inventoryItemID)
		{
			if (inventoryItemID == null)
				return null;

			InventoryItem_cu inventoryItem =
				InventoryItem_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(inventoryItemID)));

			return GetUnitMeasurmentTreeLinkObjectsList(inventoryItem);
		}

		public static double GetEncapsulatedQuantity(object changeFromUnitID, object changeToUnitID)
		{
			if (changeFromUnitID == null || changeToUnitID == null)
				return 0;

			UnitMeasurment_cu changeFromUnit =
				UnitMeasurment_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(changeFromUnitID)));
			UnitMeasurment_cu changeToUnit =
				UnitMeasurment_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(changeToUnitID)));

			if (changeFromUnit == null || changeToUnit == null)
				return 0;

			return GetEncapsulatedQuantity(changeFromUnit, changeToUnit);
		}

		public static double GetEncapsulatedQuantity(UnitMeasurment_cu changeFromUnit, UnitMeasurment_cu changeToUnit)
		{
			if (changeFromUnit == null)
				return 0;

			double changeFromEncapsulatedQuantity = changeFromUnit.EncapsualtedQuantityToParentUnit;
			if (changeToUnit == null)
				return changeFromEncapsulatedQuantity;

			double accummulativeEncapsulatedQuantity = 1;
			double encapsulatedQuantity = 1;

			ComparingUnitMeasurment comparingUnitMeasurment = CompareUnitMeasurment(changeFromUnit, changeToUnit);

			switch (comparingUnitMeasurment)
			{
				case ComparingUnitMeasurment.FromUnit_EqualTo_ToUnit:
					return accummulativeEncapsulatedQuantity;
				case ComparingUnitMeasurment.FromUnit_LargerThan_ToUnit:
					if (changeToUnit.HasParent)
					{
						UnitMeasurment_cu parentUnitMeasurment = changeToUnit.ParentUnitMeasurment;
						if (parentUnitMeasurment == null)
							return accummulativeEncapsulatedQuantity;

						UnitMeasurmentTreeLink_cu unitTreeLink =
							UnitMeasurmentTreeLink_cu.ItemsList.Find(
								item =>
									Convert.ToInt32(item.ParentUnitMeasurment_CU_ID).Equals(Convert.ToInt32(parentUnitMeasurment.ID)) &&
									Convert.ToInt32(item.ChildUnitMeasurment_CU_ID).Equals(Convert.ToInt32(changeToUnit.ID)));
						if (unitTreeLink == null)
							return accummulativeEncapsulatedQuantity;

						return unitTreeLink.EncapsulatedChildQantity * GetEncapsulatedQuantity(changeFromUnit, parentUnitMeasurment);
					}
					break;
				case ComparingUnitMeasurment.ToUnit_LargerThan_FromUnit:
					if (changeToUnit.HasChild)
					{
						UnitMeasurment_cu childUnitMeasurment = changeToUnit.ChildUnitMeasurment;
						if (childUnitMeasurment == null)
							return accummulativeEncapsulatedQuantity;

						UnitMeasurmentTreeLink_cu unitTreeLink =
							UnitMeasurmentTreeLink_cu.ItemsList.Find(
								item =>
									Convert.ToInt32(item.ParentUnitMeasurment_CU_ID).Equals(Convert.ToInt32(changeToUnit.ID)) &&
									Convert.ToInt32(item.ChildUnitMeasurment_CU_ID).Equals(Convert.ToInt32(childUnitMeasurment.ID)));
						if (unitTreeLink == null)
							return accummulativeEncapsulatedQuantity;

						return unitTreeLink.EncapsulatedChildQantity * GetEncapsulatedQuantity(changeFromUnit, childUnitMeasurment);
					}
					break;
			}

			return accummulativeEncapsulatedQuantity;
		}

		public static double GetInventoryTrackingTransactionQuantity(InventoryItem_cu inventoryItem, object inventoryHousingId,
			UnitMeasurment_cu transactionUnitMeasurment, double transactionQuantity)
		{
			if (inventoryItem == null || transactionQuantity == null)
				return 0;

			double quantity = transactionQuantity;
			UnitMeasurment_cu inventoryTrackingUnitMeasurment = inventoryItem.InventoryTrackingUnitMeasurment;
			double encapsulatedAmount = GetEncapsulatedQuantity(transactionUnitMeasurment,
				inventoryTrackingUnitMeasurment);

			ComparingUnitMeasurment comparingUnitMeasurment =
				CompareUnitMeasurment(transactionUnitMeasurment, inventoryTrackingUnitMeasurment);
			switch (comparingUnitMeasurment)
			{
				/*case ComparingUnitMeasurment.FromUnit_EqualTo_ToUnit:

					break;*/
				case ComparingUnitMeasurment.ToUnit_LargerThan_FromUnit:
					quantity = quantity / encapsulatedAmount;
					break;
				case ComparingUnitMeasurment.FromUnit_LargerThan_ToUnit:
					quantity = quantity * encapsulatedAmount;
					break;
			}

			return quantity;
		}

		public static bool CanBeAdded(DB_InvoiceType invoiceType, InventoryItem_cu inventoryItem, object inventoryHousingId,
			UnitMeasurment_cu transactionUnitMeasurment, double transactionQuantity)
		{
			if (inventoryItem == null || transactionUnitMeasurment == null)
				return false;

			UnitMeasurment_cu inventoryTrackingUnitMeasurment = inventoryItem.InventoryTrackingUnitMeasurment;
			double availableQuantityInStock = GetInventoryItemAvailableQuantity(inventoryItem.ID,
				inventoryTrackingUnitMeasurment.ID, inventoryHousingId);
			double quantity = GetInventoryTrackingTransactionQuantity(inventoryItem, inventoryHousingId,
						transactionUnitMeasurment, transactionQuantity);
			switch (invoiceType)
			{
				case DB_InvoiceType.SellingInvoice:
					double minInventoryItemStock;
					if (inventoryItem.StockMinLevel != null)
					{
						minInventoryItemStock = Convert.ToDouble(inventoryItem.StockMinLevel);
						return (minInventoryItemStock <= availableQuantityInStock - quantity) ||
							   Convert.ToBoolean(inventoryItem.AcceptOverrideMinAmount);
					}
					return true;
				case DB_InvoiceType.PurchasingInvoice:
					double maxInventoryItemStock;
					if (inventoryItem.StockMaxLevel != null)
					{
						maxInventoryItemStock = Convert.ToDouble(inventoryItem.StockMaxLevel);
						return maxInventoryItemStock >= availableQuantityInStock + quantity;
					}
					return true;
			}

			return false;
		}

		public static ComparingUnitMeasurment CompareUnitMeasurment(object unitFromID, object unitToID)
		{
			if (unitFromID == null || unitToID == null)
				return ComparingUnitMeasurment.None;

			UnitMeasurment_cu changeFromUnit =
				UnitMeasurment_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(unitFromID)));
			UnitMeasurment_cu changeToUnit =
				UnitMeasurment_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(unitToID)));

			if (changeFromUnit == null || changeToUnit == null)
				return ComparingUnitMeasurment.None;

			return CompareUnitMeasurment(changeFromUnit, changeToUnit);
		}

		public static ComparingUnitMeasurment CompareUnitMeasurment(UnitMeasurment_cu unitFrom, UnitMeasurment_cu unitTo)
		{
			ComparingUnitMeasurment comparingUnitMeasurment = ComparingUnitMeasurment.None;

			if (unitFrom == null || unitTo == null)
				return comparingUnitMeasurment;

			if (Convert.ToInt32(unitFrom.ID).Equals(Convert.ToInt32(unitTo.ID)))
				return ComparingUnitMeasurment.FromUnit_EqualTo_ToUnit;

			if (unitFrom.HasParent)
			{
				if (unitFrom.ParentUnitMeasurment != null)
					if (Convert.ToInt32(unitFrom.ParentUnitMeasurment.ID).Equals(Convert.ToInt32(unitTo.ID)))
						return ComparingUnitMeasurment.ToUnit_LargerThan_FromUnit;

				return CompareUnitMeasurment(unitFrom.ParentUnitMeasurment, unitTo);
			}

			return ComparingUnitMeasurment.FromUnit_LargerThan_ToUnit;
		}

		public static List<UnitMeasurment_cu> GetUnitMeasurmentTreeLinkObjectsList(InventoryItem_cu inventoryItem)
		{
			if (inventoryItem == null)
				return null;

			List<UnitMeasurmentTreeLinkObject> treeLinksList = new List<UnitMeasurmentTreeLinkObject>();

			List<UnitMeasurment_cu> registeredUnitMeasurments = GetInventoryItemRegisteredUnitMeasurments(inventoryItem.ID);
			if (registeredUnitMeasurments == null || registeredUnitMeasurments.Count == 0)
				return null;
			registeredUnitMeasurments = SetEncapsulatedAmountToSmallestUnit(registeredUnitMeasurments, inventoryItem);
			registeredUnitMeasurments = SetEncapsulatedAmountToParentUnit(registeredUnitMeasurments);

			foreach (UnitMeasurment_cu unitMeasurment in registeredUnitMeasurments)
				treeLinksList.Add(CreateUnitMeasurmentTreeLinkObject(inventoryItem, unitMeasurment));

			return registeredUnitMeasurments;
		}

		public static UnitMeasurmentTreeLinkObject CreateUnitMeasurmentTreeLinkObject(InventoryItem_cu inventoryItem,
			UnitMeasurment_cu unitMeasurment)
		{
			UnitMeasurmentTreeLinkObject unitMeasurmentTreeLink = new UnitMeasurmentTreeLinkObject();
			unitMeasurmentTreeLink.EncapsulatedQuantityRelativeToSmallestUnit = unitMeasurment.EncapsulatedQuantityToSmallestUnit;
			unitMeasurmentTreeLink.InventoryItem = inventoryItem;
			unitMeasurmentTreeLink.UnitMeasurment = unitMeasurment;
			unitMeasurmentTreeLink.OrderIndex = unitMeasurment.OrderIndex;
			unitMeasurmentTreeLink.EncapsulatedQuantityRelativeToInventoryTrackingUnit = unitMeasurment.EncapsulatedQuantityToSmallestUnit;
			UnitMeasurment_cu inventoryTrackingUnitMeasurment = inventoryItem.InventoryTrackingUnitMeasurment;
			if (inventoryTrackingUnitMeasurment != null)
				unitMeasurmentTreeLink.IsInventoryTracking =
					Convert.ToInt32(inventoryTrackingUnitMeasurment.ID).Equals(Convert.ToInt32(unitMeasurment.ID));
			return unitMeasurmentTreeLink;
		}

		public static List<UnitMeasurment_cu> ReOrderUnitMeasurments(List<UnitMeasurment_cu> unitMeasurments)
		{
			if (unitMeasurments == null || unitMeasurments.Count == 0)
				return null;

			List<UnitMeasurment_cu> list = new List<UnitMeasurment_cu>();
			List<UnitMeasurment_cu> reorderedList = new List<UnitMeasurment_cu>();
			int orderIndex = 1000;
			foreach (UnitMeasurment_cu unitMeasurment in unitMeasurments)
			{
				unitMeasurment.OrderIndex = orderIndex;
				orderIndex = orderIndex - 1;
				if (!list.Exists(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(unitMeasurment.ID))))
					list.Add(unitMeasurment);
			}

			if (list.Count > 0)
			{
				orderIndex = 0;
				foreach (UnitMeasurment_cu unitMeasurment in list.OrderBy(item => item.OrderIndex))
				{
					unitMeasurment.OrderIndex = orderIndex;
					orderIndex = orderIndex + 1;
					if (!reorderedList.Exists(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(unitMeasurment.ID))))
						reorderedList.Add(unitMeasurment);
				}
			}

			return reorderedList;
		}

		private static List<UnitMeasurment_cu> SetEncapsulatedAmountToSmallestUnit(List<UnitMeasurment_cu> unitMeasurments,
			InventoryItem_cu inventoryItem)
		{
			if (unitMeasurments == null || unitMeasurments.Count == 0 || inventoryItem == null ||
				inventoryItem.InventoryTrackingUnitMeasurment == null)
				return null;

			UnitMeasurment_cu inventoryTrackingUnit = inventoryItem.InventoryTrackingUnitMeasurment;

			List<UnitMeasurment_cu> list = new List<UnitMeasurment_cu>();
			double encapsulatedQuantity = 1;
			for (int i = unitMeasurments.Count - 1; i >= 0; i--)
			{
				UnitMeasurment_cu unitMeasurment = unitMeasurments[i];
				if (unitMeasurment == null)
					return null;

				if (Convert.ToInt32(inventoryTrackingUnit.ID).Equals(Convert.ToInt32(unitMeasurment.ID)))
					unitMeasurment.IsInventoryTracking = true;

				if (i == unitMeasurments.Count - 1)
				{
					unitMeasurment.EncapsulatedQuantityToSmallestUnit = encapsulatedQuantity;
					if (list.Count == 0 || !list.Exists(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(unitMeasurment.ID))))
						list.Add(unitMeasurment);
					unitMeasurment.IsSmallestUnit = true;
					continue;
				}

				if (unitMeasurment.ChildUnitMeasurment == null)
					continue;

				UnitMeasurmentTreeLink_cu unitTreeLink =
					UnitMeasurmentTreeLink_cu.ItemsList.Find(
						item =>
							Convert.ToInt32(item.ParentUnitMeasurment_CU_ID).Equals(Convert.ToInt32(unitMeasurment.ID)) &&
							Convert.ToInt32(item.ChildUnitMeasurment_CU_ID).Equals(Convert.ToInt32(unitMeasurment.ChildUnitMeasurment.ID)));
				if (unitTreeLink == null)
					continue;

				unitMeasurment.EncapsulatedQuantityToSmallestUnit = encapsulatedQuantity * unitTreeLink.EncapsulatedChildQantity;
				encapsulatedQuantity = unitMeasurment.EncapsulatedQuantityToSmallestUnit;

				if (i == unitMeasurments.Count)
					unitMeasurment.IsLargestUnit = true;

				if (list.Count == 0 || !list.Exists(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(unitMeasurment.ID))))
					list.Add(unitMeasurment);
			}

			if (list.Find(item => item.IsInventoryTracking) == null)
			{
				UnitMeasurment_cu smallestUnit = list.Find(item => item.IsSmallestUnit);
				if (smallestUnit != null)
					smallestUnit.IsInventoryTracking = true;
			}

			return list;
		}

		private static List<UnitMeasurment_cu> SetEncapsulatedAmountToParentUnit(List<UnitMeasurment_cu> unitMeasurments)
		{
			if (unitMeasurments == null || unitMeasurments.Count == 0)
				return null;

			List<UnitMeasurment_cu> list = new List<UnitMeasurment_cu>();
			double encapsulatedQuantity = 1;

			UnitMeasurment_cu inventoryTrackingUnit = unitMeasurments.Find(item => item.IsInventoryTracking);
			if (inventoryTrackingUnit == null)
				return null;

			inventoryTrackingUnit.EncapsualtedQuantityToParentUnit = 1;
			list.Add(inventoryTrackingUnit);
			if (!unitMeasurments.Remove(inventoryTrackingUnit))
				return null;

			foreach (UnitMeasurment_cu unitMeasurment in unitMeasurments)
			{
				if (unitMeasurment.HasParent)
				{
					UnitMeasurment_cu parentUnitMeasurment = unitMeasurment.ParentUnitMeasurment;
					if (parentUnitMeasurment == null)
						continue;

					UnitMeasurmentTreeLink_cu unitTreeLink =
						UnitMeasurmentTreeLink_cu.ItemsList.Find(
							item =>
								Convert.ToInt32(item.ParentUnitMeasurment_CU_ID).Equals(Convert.ToInt32(parentUnitMeasurment.ID)) &&
								Convert.ToInt32(item.ChildUnitMeasurment_CU_ID).Equals(Convert.ToInt32(unitMeasurment.ID)));
					if (unitTreeLink == null)
						continue;

					double unitTreeEncapsulatedQuantity = unitTreeLink.EncapsulatedChildQantity;
					double inventoryTrackingEncapsulatedQuantity = inventoryTrackingUnit.EncapsualtedQuantityToParentUnit;
					unitMeasurment.EncapsualtedQuantityToParentUnit =
						Math.Round(inventoryTrackingEncapsulatedQuantity / unitTreeEncapsulatedQuantity, 2);
				}
			}

			return list;
		}
	}
}
