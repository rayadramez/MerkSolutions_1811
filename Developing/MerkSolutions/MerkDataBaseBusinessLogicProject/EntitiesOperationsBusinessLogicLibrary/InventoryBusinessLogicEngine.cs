using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

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

		public static double GetInventoryItemTotalAreaParts(int inventoryItemID)
		{
			InventoryItem_cu inventoryItem =
				InventoryItem_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(inventoryItemID));
			if (inventoryItem == null)
				return 0;
			return GetInventoryItemTotalAreaParts(inventoryItem);
		}

		public static double GetInventoryItemTotalAreaParts(InventoryItem_cu inventoryItem)
		{
			if (inventoryItem == null)
				return 0;
			List<InventoryItem_Area> areasList = InventoryItem_Area.ItemsList.FindAll(item =>
				Convert.ToInt32(item.InventoryItemID).Equals(Convert.ToInt32(inventoryItem.ID)));
			double totalArea = 0;
			foreach (InventoryItem_Area inventoryItemArea in areasList)
				totalArea += inventoryItemArea.Width * inventoryItemArea.Height * inventoryItemArea.Count;

			return totalArea;
		}

		public static double GetInventoryItemTotalCountParts(int inventoryItemID)
		{
			InventoryItem_cu inventoryItem =
				InventoryItem_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(inventoryItemID));
			if (inventoryItem == null)
				return 0;
			return GetInventoryItemTotalAreaParts(inventoryItem);
		}

		public static double GetInventoryItemTotalCountParts(object inventoryItemID)
		{
			InventoryItem_cu inventoryItem = InventoryItem_cu.ItemsList.Find(item =>
				Convert.ToInt32(item.ID).Equals(Convert.ToInt32(inventoryItemID)));
			return GetInventoryItemTotalCountParts(inventoryItemID);
		}

		public static double GetInventoryItemTotalCountParts(InventoryItem_cu inventoryItem)
		{
			if (inventoryItem == null)
				return 0;
			List<InventoryItem_Area> areasList = InventoryItem_Area.ItemsList.FindAll(item =>
				Convert.ToInt32(item.InventoryItemID).Equals(Convert.ToInt32(inventoryItem.ID)));
			return areasList.Sum(item => item.Count);
		}

		public static double CalculateRawMaterialArea(int rawMaterialID)
		{
			RawMaterials_cu rawMaterial =
				RawMaterials_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(rawMaterialID));
			return CalculateRawMaterialTotalArea(rawMaterial);
		}

		public static double CalculateRawMaterialTotalArea(RawMaterials_cu rawMaterial)
		{
			if (rawMaterial == null)
				return 0;

			double unitArea = 0;
			unitArea = Convert.ToDouble(rawMaterial.Width) * Convert.ToDouble(rawMaterial.Height);
			//DB_DividedByType rawDividedType = (DB_DividedByType)rawMaterial.DividedByType_P_ID;
			//switch (rawDividedType)
			//{
			//	case DB_DividedByType.NotDivided:
			//		unitArea = Convert.ToDouble(rawMaterial.Width) * Convert.ToDouble(rawMaterial.Height);
			//		break;
			//	case DB_DividedByType.DividedBy4:
			//		unitArea = Convert.ToDouble(rawMaterial.Width) * Convert.ToDouble(rawMaterial.Height / 4);
			//		break;
			//	case DB_DividedByType.DividedBy6:
			//		unitArea = Convert.ToDouble(rawMaterial.Width / 2) * Convert.ToDouble(rawMaterial.Height / 3);
			//		break;
			//}

			return unitArea;
		}

		public static double CalculateRawMaterialUnitCost(RawMaterials_cu rawMaterial, RawMaterialUnitCostCalculation rawMaterialUnitCostCalculation)
		{
			if (rawMaterial == null)
				return 0;

			double unitCost = 0;
			double rawUnitArea = CalculateRawMaterialTotalArea(rawMaterial);
			List<RawMaterialTranasction> rawMaterialTransactionsList = DBCommon
				.GetItemsList<RawMaterialTranasction>(item =>
					item.RawMaterial_CU_ID.Equals(rawMaterial.ID)).ToList();

			switch (rawMaterialUnitCostCalculation)
			{
				case RawMaterialUnitCostCalculation.LastPurchasingCost:
					RawMaterialTranasction lastTransacion =
						rawMaterialTransactionsList.OrderByDescending(item => item.Date).First();
					if (lastTransacion == null)
						return 0;
					unitCost = Convert.ToDouble(lastTransacion.PuchasingPrice) / Convert.ToInt32(lastTransacion.Count);
					unitCost = unitCost / rawUnitArea;
					break;
			}
			return unitCost;
		}

		public static RawMaterials_cu GetRawMaterial(object rawMaterialID)
		{
			if (rawMaterialID == null)
				return null;

			return RawMaterials_cu.ItemsList.Find(item =>
				Convert.ToInt32(item.ID).Equals(Convert.ToInt32(rawMaterialID)));
		}

		public static List<InventoryItem_RawMaterial_cu> GetInventoryItem_RawMaterials_List(InventoryItem_cu inventoryitem)
		{
			if (inventoryitem == null)
				return null;

			List<InventoryItem_RawMaterial_cu> list = new List<InventoryItem_RawMaterial_cu>();

			list = InventoryItem_RawMaterial_cu.ItemsList.FindAll(item =>
				Convert.ToInt32(item.InventoryItem_CU_ID).Equals(Convert.ToInt32(inventoryitem.ID)));

			return list;
		}

		public static RawMaterials_cu GetRawMaterial(InventoryItem_RawMaterial_cu ivnentoryItem_RawMaterial)
		{
			if (ivnentoryItem_RawMaterial == null)
				return null;

			RawMaterials_cu rawMaterial = RawMaterials_cu.ItemsList.Find(item =>
				Convert.ToInt32(item.ID).Equals(Convert.ToInt32(ivnentoryItem_RawMaterial.RawMaterial_CU_ID)));

			return rawMaterial;
		}

		public static List<RawMaterials_cu> GetRawMaterials_List(InventoryItem_cu inventoryItem)
		{
			if (inventoryItem == null)
				return null;

			List<RawMaterials_cu> list = new List<RawMaterials_cu>();

			List<InventoryItem_RawMaterial_cu> inventoryItem_RawMaterials_List =
				GetInventoryItem_RawMaterials_List(inventoryItem);
			foreach (InventoryItem_RawMaterial_cu inventoryItemRawMaterialCu in inventoryItem_RawMaterials_List)
			{
				RawMaterials_cu rawMaterial = GetRawMaterial(inventoryItemRawMaterialCu);
				if (rawMaterial != null)
					list.Add(rawMaterial);
			}

			return list;
		}

		public static InventoryItem_cu GetInventoryItem(object inventoryItemID)
		{
			if (inventoryItemID == null)
				return null;

			InventoryItem_cu inventoryItem = InventoryItem_cu.ItemsList.Find(item =>
				Convert.ToInt32(item.ID).Equals(Convert.ToInt32(inventoryItemID)));
			return inventoryItem;
		}

		public static List<InventoryItem_Printing_cu> GetInventoryItem_Printing_List(object inventoryItemID)
		{
			if (inventoryItemID == null)
				return null;

			InventoryItem_cu inventoryItem = GetInventoryItem(inventoryItemID);
			return GetInventoryItem_Printing_List(inventoryItem);
		}

		public static List<InventoryItem_Printing_cu> GetInventoryItem_Printing_List(InventoryItem_cu inventoryItem)
		{
			if (inventoryItem == null)
				return null;

			List<InventoryItem_Printing_cu> list = InventoryItem_Printing_cu.ItemsList
				.FindAll(item => Convert.ToInt32(item.InventoryItem_CU_ID).Equals(Convert.ToInt32(inventoryItem.ID)))
				.OrderByDescending(item => item.Date).ThenByDescending(item => item.RawMaterial_CU_ID).ToList();

			List<InventoryItem_Printing_cu> newList = new List<InventoryItem_Printing_cu>();

			List<RawMaterials_cu> rawMaterialsList = GetRawMaterials_List(inventoryItem);

			foreach (RawMaterials_cu rawMaterialsCu in rawMaterialsList)
			{
				InventoryItem_Printing_cu ivnentoryItem_Printing =
					GetInventoryItem_Printing(inventoryItem, rawMaterialsCu);
				if(ivnentoryItem_Printing != null)
					newList.Add(ivnentoryItem_Printing);

			}

			return newList;
		}

		public static InventoryItem_Printing_cu GetInventoryItem_Printing(InventoryItem_cu inventoryItem,
			RawMaterials_cu rawMaterial)
		{
			if (rawMaterial == null)
				return null;

			InventoryItem_Printing_cu ivnentoryItem_Printing = InventoryItem_Printing_cu.ItemsList
				.FindAll(item =>
					Convert.ToInt32(item.InventoryItem_CU_ID).Equals(Convert.ToInt32(inventoryItem.ID)) &&
					Convert.ToInt32(item.RawMaterial_CU_ID).Equals(Convert.ToInt32(rawMaterial.ID)))
				.OrderByDescending(item => item.Date).FirstOrDefault();

			return ivnentoryItem_Printing;
		}

		public static List<InventoryItemDetailsConstructor> GetParentConstructorDetailsList(object inventoryItemID)
		{
			List<InventoryItemDetailsConstructor> ParentConstructor = new List<InventoryItemDetailsConstructor>();

			if (inventoryItemID != null)
			{
				InventoryItemDetailsConstructor inventoryItemConstructor = new InventoryItemDetailsConstructor();
				InventoryItem_cu inventoryItem = InventoryItem_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(inventoryItemID)));

				if (inventoryItem == null)
					return null;

				inventoryItemConstructor.ItemID = inventoryItem.ID;
				inventoryItemConstructor.ItemInternalCode = inventoryItem.InternalCode;
				inventoryItemConstructor.ItemName = inventoryItem.Name_P;
				inventoryItemConstructor.ItemWidth = inventoryItem.Width;
				inventoryItemConstructor.ItemHeight = inventoryItem.Height;
				inventoryItemConstructor.ItemDepth = inventoryItem.Depth;
				inventoryItemConstructor.PartsCount = GetInventoryItemTotalCountParts(inventoryItem);

				SetInventoryItem_AreaParts_ConstructorDetailsList(ref inventoryItemConstructor, inventoryItem);
				SetInventoryItem_Printing_ConstructorDetailsList(ref inventoryItemConstructor, inventoryItem,
					PrintingCalculationType.LastDate);

				inventoryItemConstructor.ListType = ListType.SummaryInventoryItems;
				ParentConstructor.Add(inventoryItemConstructor);
			}
			else
			{
				foreach (InventoryItem_cu inventoryItem in InventoryItem_cu.ItemsList)
				{
					InventoryItemDetailsConstructor inventoryItemConstructor = new InventoryItemDetailsConstructor();

					if (inventoryItem == null)
						return null;

					inventoryItemConstructor.ItemID = inventoryItem.ID;
					inventoryItemConstructor.ItemInternalCode = inventoryItem.InternalCode;
					inventoryItemConstructor.ItemName = inventoryItem.Name_P;
					inventoryItemConstructor.ItemWidth = inventoryItem.Width;
					inventoryItemConstructor.ItemHeight = inventoryItem.Height;
					inventoryItemConstructor.ItemDepth = inventoryItem.Depth;
					inventoryItemConstructor.PartsCount = GetInventoryItemTotalCountParts(inventoryItem);

					SetInventoryItem_AreaParts_ConstructorDetailsList(ref inventoryItemConstructor, inventoryItem);
					SetInventoryItem_Printing_ConstructorDetailsList(ref inventoryItemConstructor, inventoryItem,
						PrintingCalculationType.LastDate);

					inventoryItemConstructor.ListType = ListType.SummaryInventoryItems;
					ParentConstructor.Add(inventoryItemConstructor);
				}
			}

			return ParentConstructor;
		}

		public static bool SetInventoryItem_AreaParts_ConstructorDetailsList(
			ref InventoryItemDetailsConstructor inventoryItemConstructor, InventoryItem_cu inventoryItem)
		{
			if (inventoryItemConstructor == null)
				return false;

			List<InventoryItem_Area> inventoryItem_AreasParts = InventoryItem_Area.ItemsList.FindAll(item =>
				Convert.ToInt32(item.InventoryItemID).Equals(Convert.ToInt32(inventoryItem.ID)));

			inventoryItemConstructor.List_InventoryItem_AreaPartsDetailsConstructor =
				new List<InventoryItem_AreaPartsDetailsConstructor>();

			if (inventoryItem_AreasParts.Count == 0)
			{
				inventoryItemConstructor.TotalPartsArea = Convert.ToDouble(inventoryItemConstructor.ItemWidth) *
				                                          Convert.ToDouble(inventoryItemConstructor.ItemHeight);
				return true;
			}

			foreach (InventoryItem_Area inventoryItemAreasPart in inventoryItem_AreasParts)
			{
				InventoryItem_AreaPartsDetailsConstructor itemAreaPart = new InventoryItem_AreaPartsDetailsConstructor();
				itemAreaPart.ItemID = inventoryItemConstructor.ItemID;
				itemAreaPart.ItemName = inventoryItem.Name_P;
				itemAreaPart.ItemInternalCode = inventoryItem.InternalCode;
				itemAreaPart.PartInternalCode = inventoryItemAreasPart.InternalCode;
				itemAreaPart.PartWidth = inventoryItemAreasPart.Width;
				itemAreaPart.PartHeight = inventoryItemAreasPart.Height;
				itemAreaPart.PartCount = inventoryItemAreasPart.Count;
				itemAreaPart.PartArea = inventoryItemAreasPart.Count * inventoryItemAreasPart.Width *
				                        inventoryItemAreasPart.Height;
				inventoryItemConstructor.ListType = ListType.AreaParts;
				inventoryItemConstructor.List_InventoryItem_AreaPartsDetailsConstructor.Add(itemAreaPart);
			}

			double totalArea =
				inventoryItemConstructor.List_InventoryItem_AreaPartsDetailsConstructor.Sum(item =>
					Convert.ToDouble(item.PartArea));
			inventoryItemConstructor.TotalPartsArea = totalArea;

			return true;
		}

		public static bool SetInventoryItem_Printing_ConstructorDetailsList(
			ref InventoryItemDetailsConstructor inventoryItemConstructor, InventoryItem_cu inventoryItem,
			PrintingCalculationType printingCalculationType)
		{
			if (inventoryItemConstructor == null)
				return false;

			InventoryItem_Printing_cu inventoryItemsPrinting = null;
			List<InventoryItem_Printing_cu> inventoryItemsPrintingList = null;

			switch (printingCalculationType)
			{
				case PrintingCalculationType.LastDate:
					inventoryItemsPrintingList = GetInventoryItem_Printing_List(inventoryItem);
					break;
			}

			if (inventoryItemsPrintingList == null || inventoryItemsPrintingList.Count == 0)
				return false;

			inventoryItemConstructor.List_InventoryItem_PrintingDetailsConstructor =
				new List<InventoryItem_PrintingDetailsConstructor>();

			foreach (InventoryItem_Printing_cu inventoryItemPrintingCu in inventoryItemsPrintingList)
			{
				InventoryItem_PrintingDetailsConstructor printingDetailsConstructor =
					new InventoryItem_PrintingDetailsConstructor();
				RawMaterials_cu rawMaterial = RawMaterials_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID)
						.Equals(Convert.ToInt32(inventoryItemPrintingCu.RawMaterial_CU_ID)));

				if (rawMaterial != null)
					printingDetailsConstructor.RawName = rawMaterial.Name_P;

				printingDetailsConstructor.ItemID = inventoryItem.ID;
				printingDetailsConstructor.ItemName = inventoryItem.Name_P;
				printingDetailsConstructor.PrintingMinutes = inventoryItemPrintingCu.PrintingMaxTimeInMinutes;
				printingDetailsConstructor.PrintingUnitCostFactor =
					inventoryItemPrintingCu.PrintingAverageUnitCostPrice;
				printingDetailsConstructor.PrintingCalculatedCost =
					Convert.ToDouble(inventoryItemPrintingCu.PrintingAverageUnitCostPrice) *
					Convert.ToDouble(inventoryItemPrintingCu.PrintingMaxTimeInMinutes);
				printingDetailsConstructor.PrintingAddedMinutes = inventoryItemPrintingCu.AddedMinutes;
				printingDetailsConstructor.PrintingTotalCalculatedCost =
					(Convert.ToDouble(inventoryItemPrintingCu.AddedMinutes) +
					 Convert.ToDouble(inventoryItemPrintingCu.PrintingMaxTimeInMinutes)) *
					Convert.ToDouble(inventoryItemPrintingCu.PrintingAverageUnitCostPrice);
				printingDetailsConstructor.PrintingUseRealCost = inventoryItemPrintingCu.UseRealCost;
				printingDetailsConstructor.PrintingRealCost = inventoryItemPrintingCu.PrintingRealCostPrice;

				if(inventoryItemConstructor.TotalPartsArea != null)
					if (!Convert.ToBoolean(printingDetailsConstructor.PrintingUseRealCost))
						printingDetailsConstructor.PrintingUnitCost =
							Convert.ToDouble(printingDetailsConstructor.PrintingTotalCalculatedCost) /
							Convert.ToDouble(inventoryItemConstructor.TotalPartsArea);
					else
						printingDetailsConstructor.PrintingUnitCost = printingDetailsConstructor.PrintingRealCost;

				printingDetailsConstructor.TotalPartsArea = inventoryItemConstructor.TotalPartsArea;

				inventoryItemConstructor.List_InventoryItem_PrintingDetailsConstructor.Add(
					printingDetailsConstructor);
			}

			inventoryItemConstructor.PrintingUnitCost =
				Convert.ToDouble(
					inventoryItemConstructor.List_InventoryItem_PrintingDetailsConstructor.Sum(item =>
						Convert.ToDouble(item.PrintingUnitCost)));
			inventoryItemConstructor.ListType = ListType.Printing;

			return true;
		}
	}
}

