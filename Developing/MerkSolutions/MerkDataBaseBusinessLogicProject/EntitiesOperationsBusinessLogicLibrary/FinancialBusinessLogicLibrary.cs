using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary
{
	public enum PriceType
	{
		PatientShare = 1,
		InsuranceShare = 2,
		TotalServicePriceBeforeSurcharges = 3,
		SurchargeAmount_PatientShare = 4,
		SurchargeAmount_InsuranceShare = 5,
		TotalSurchargeAmount = 6,
 		TotalServicePriceAfterSurcharges = 7,
		PatientShareDiscountAmount = 8
	}

	public class FinancialBusinessLogicLibrary
	{
		public static double CalculateInvoice_Totals(Invoice invoice, PriceType priceType)
		{
			if (invoice == null)
				return 0;
			double price = 0;
			DB_InvoiceType invoiceType = (DB_InvoiceType) invoice.InvoiceType_P_ID;
			switch (invoiceType)
			{
				case DB_InvoiceType.InPatientPrivate:
				case DB_InvoiceType.InPatientNotPrivate:
					foreach (InvoiceDetail invoiceDetail in invoice.List_InvoiceDetails.FindAll(item =>item.IsInPatientParentService))
						price += CalculateInvoiceDetail_Totals(invoice, invoiceDetail, priceType);
					break;
				case DB_InvoiceType.OutPatientPrivate:
				case DB_InvoiceType.OutPatientNotPrivate:
					foreach (InvoiceDetail invoiceDetail in invoice.List_InvoiceDetails.FindAll(item => !item.IsInPatientParentService))
						price += CalculateInvoiceDetail_Totals(invoice, invoiceDetail, priceType);
					break;
			}

			return price;
		}

		public static double CalculateInvoiceDetail_Totals(Invoice invoice, IInvoiceItem invoiceItem, PriceType priceType)
		{
			InvoiceItemType invoiceItemType = invoiceItem.InvoiceItemType;
			double price = 0;
			if (invoiceItem.List_InvoiceItems != null && invoiceItem.List_InvoiceItems.Count > 0)
				switch (invoiceItemType)
				{
					case InvoiceItemType.InPatient_Parent_AccommodationService:
					case InvoiceItemType.InPatient_Parent_SurgeryService:
						foreach (IInvoiceItem childInvoiceItem in invoiceItem.List_InvoiceItems)
							price += CalculateInvoiceDetail_Item(invoice, childInvoiceItem, priceType);
						break;
					case InvoiceItemType.OutPatient_InvoiceDetail_ExaminationService:
						foreach (IInvoiceItem childInvoiceItem in invoiceItem.List_InvoiceItems)
							price += CalculateInvoiceDetail_Item(invoice, childInvoiceItem, priceType);
						break;
				}
			else
				return CalculateInvoiceDetail_Item(invoice, invoiceItem, priceType);

			return price;
		}

		public static double CalculateInvoiceDetail_Item(Invoice invoice, IInvoiceItem invoiceItem, PriceType priceType)
		{
			double price = 0;
			double surchargeTotalPercentage = 0;
			switch (priceType)
			{
				case PriceType.PatientShare:
					price = invoiceItem.PatientShare_BeforeAddsOn_InvoiceItem;
					break;
				case PriceType.InsuranceShare:
					price = invoiceItem.InsuranceShare_BeforeAddsOn_InvoiceItem;
					break;
				case PriceType.TotalServicePriceBeforeSurcharges:
					price = invoiceItem.PatientShare_BeforeAddsOn_InvoiceItem + invoiceItem.InsuranceShare_BeforeAddsOn_InvoiceItem;
					break;
				case PriceType.SurchargeAmount_PatientShare:
					surchargeTotalPercentage = GetSurchargeAmount(invoice);

					if (invoice.InvoiceShareObject.IsSurchargeApplied &&
					    Convert.ToBoolean(invoice.InvoiceShareObject.IsSurchargeDistributedToInsurancePercentage))
						price = CalculateInvoiceDetail_Item(invoice, invoiceItem, PriceType.PatientShare);
					else if (invoice.InvoiceShareObject.IsSurchargeApplied &&
					         Convert.ToBoolean(invoice.InvoiceShareObject.IsSurchargeAppliedToPatientOnly))
						price = CalculateInvoiceDetail_Item(invoice, invoiceItem, PriceType.TotalServicePriceBeforeSurcharges);
					else if (invoice.InvoiceShareObject.IsSurchargeApplied &&
					         Convert.ToBoolean(invoice.InvoiceShareObject.IsSurchargeAppliedToInsuranceOnly))
						price = 0;

					if (invoice.InvoiceShareObject.TotalSurchargeAccummulativePercentage > 0)
						price = price*surchargeTotalPercentage;
					else
						price = 0;
					break;
				case PriceType.SurchargeAmount_InsuranceShare:
					surchargeTotalPercentage = GetSurchargeAmount(invoice);

					if (invoice.InvoiceShareObject.IsSurchargeApplied &&
					    Convert.ToBoolean(invoice.InvoiceShareObject.IsSurchargeDistributedToInsurancePercentage))
						price = CalculateInvoiceDetail_Item(invoice, invoiceItem, PriceType.InsuranceShare);
					else if (invoice.InvoiceShareObject.IsSurchargeApplied &&
					         Convert.ToBoolean(invoice.InvoiceShareObject.IsSurchargeAppliedToPatientOnly))
						price = 0;
					else if (invoice.InvoiceShareObject.IsSurchargeApplied &&
					         Convert.ToBoolean(invoice.InvoiceShareObject.IsSurchargeAppliedToInsuranceOnly))
						price = CalculateInvoiceDetail_Item(invoice, invoiceItem, PriceType.TotalServicePriceBeforeSurcharges);

					if (invoice.InvoiceShareObject.TotalSurchargeAccummulativePercentage > 0)
						price = price*surchargeTotalPercentage;
					else
						price = 0;
					break;
				case PriceType.TotalSurchargeAmount:
					surchargeTotalPercentage = GetSurchargeAmount(invoice);
					price = CalculateInvoiceDetail_Item(invoice, invoiceItem, PriceType.TotalServicePriceBeforeSurcharges);
					if (invoice.InvoiceShareObject.IsSurchargeApplied &&
					    invoice.InvoiceShareObject.TotalSurchargeAccummulativePercentage > 0)
						price = price*surchargeTotalPercentage;
					else
						price = 0;
					break;
			}

			return price;
		}

		public static void Recalculate_OutPatient_InvoiceDetails(List<InvoiceDetail> list_InvoiceDetails,
			object isInsuranceAppliedToInvoice, bool forceApplyInsurance, object insuranceCarrierID, object insuranceLevelID,
			object insurancePercentage,
			object patientMaxAmount)
		{
			if (list_InvoiceDetails == null || list_InvoiceDetails.Count == 0)
				return;

			if (Convert.ToBoolean(isInsuranceAppliedToInvoice))
			{
				if (insuranceCarrierID == null || insuranceLevelID == null)
					return;

				InsuranceCarrier_InsuranceLevel_cu insuranceBridge =
					InsuranceCarrier_InsuranceLevel_cu.ItemsList.Find(
						item =>
							Convert.ToInt32(item.InsuranceCarrier_CU_ID).Equals(Convert.ToInt32(insuranceCarrierID)) &&
							Convert.ToInt32(item.InsuranceLevel_CU_ID).Equals(Convert.ToInt32(insuranceLevelID)));
				if (insuranceBridge == null)
					return;

				double totalAccummulativePrice = 0;
				double totalAccummulativePatientShare = 0;
				double totalAccummulativeInsuranceShare = 0;
				object patientMaxAmountSettings = insuranceBridge.PatientMaxAmount != null ? insuranceBridge.PatientMaxAmount : null;
				foreach (InvoiceDetail invoiceDetail in list_InvoiceDetails)
				{
					Service_cu service = null;
					Doctor_cu doctor = null;
					if (invoiceDetail.Service_CU_ID != null)
						service =
							Service_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(invoiceDetail.Service_CU_ID)));
					if (invoiceDetail.Doctor_CU_ID != null)
						doctor =
							Doctor_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(invoiceDetail.Doctor_CU_ID)));

					if (service == null || doctor == null)
						continue;

					totalAccummulativePatientShare = totalAccummulativePatientShare + invoiceDetail.PatientShare;
					totalAccummulativeInsuranceShare = totalAccummulativeInsuranceShare + invoiceDetail.InsuranceShare;
					totalAccummulativePrice = totalAccummulativePrice + invoiceDetail.ServicePrice;

					if (Convert.ToBoolean(invoiceDetail.IsInsuranceApplied) || forceApplyInsurance)
					{
						double serviceInsurancePrice = GetServicePrice(service.ID, doctor.Person_CU_ID, insuranceCarrierID,
							insuranceLevelID);
						if (invoiceDetail.IsCustomPriceUsed)
							serviceInsurancePrice = invoiceDetail.ServicePrice;
						invoiceDetail.PatientShare = serviceInsurancePrice * (100 - Convert.ToDouble(insurancePercentage)) / 100;
						invoiceDetail.InsuranceShare = serviceInsurancePrice * Convert.ToDouble(insurancePercentage) / 100;
						if (forceApplyInsurance)
							invoiceDetail.IsInsuranceApplied = true;
					}
					else
					{
						double serviceNotInsurancePrice = GetServicePrice(service.ID, doctor.Person_CU_ID, null);
						if (invoiceDetail.IsCustomPriceUsed)
							serviceNotInsurancePrice = invoiceDetail.ServicePrice;
						invoiceDetail.PatientShare = serviceNotInsurancePrice;
						invoiceDetail.InsuranceShare = 0;
					}
				}
			}
			else
			{
				foreach (InvoiceDetail invoiceDetail in list_InvoiceDetails)
				{
					Service_cu service = null;
					Doctor_cu doctor = null;
					if (invoiceDetail.Service_CU_ID != null)
						service =
							Service_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(invoiceDetail.Service_CU_ID)));
					if (invoiceDetail.Doctor_CU_ID != null)
						doctor =
							Doctor_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(invoiceDetail.Doctor_CU_ID)));

					if (service == null || doctor == null)
						continue;

					double serviceNotInsurancePrice = GetServicePrice(service.ID, doctor.Person_CU_ID, null);
					invoiceDetail.PatientShare = serviceNotInsurancePrice;
					invoiceDetail.InsuranceShare = 0;
					invoiceDetail.IsInsuranceApplied = false;
				}
			}
		}

		public static void UpdateInvoiceDetailPrice(InvoiceDetail invoiceDetail, object insurancePercentage)
		{
			if (invoiceDetail == null)
				return;

			double servicePrice = invoiceDetail.TotalServicePrice_BeforeAddsOn_InvoiceItem;
			if (invoiceDetail.IsInsuranceApplied_InvoiceItem && insurancePercentage != null)
			{
				invoiceDetail.PatientShare_BeforeAddsOn_InvoiceItem = servicePrice*(100 - Convert.ToDouble(insurancePercentage))/
				                                                      100;
				invoiceDetail.InsuranceShare = servicePrice*Convert.ToDouble(insurancePercentage)/100;
				invoiceDetail.IsInsuranceApplied_InvoiceItem = true;
			}
			else
			{
				invoiceDetail.PatientShare_BeforeAddsOn_InvoiceItem = servicePrice;
				invoiceDetail.InsuranceShare_BeforeAddsOn_InvoiceItem = 0;
				invoiceDetail.IsInsuranceApplied_InvoiceItem = false;
			}
		}

		public static void HandleInvoieItemPrice(MedicalInvoiceItemConstructor invoiceItem)
		{
			if (invoiceItem.List_MMedicalInvoiceItemConstructors != null &&
			    invoiceItem.List_MMedicalInvoiceItemConstructors.Count > 0)
				foreach (MedicalInvoiceItemConstructor invoiceItemConstructor in invoiceItem.List_MMedicalInvoiceItemConstructors)
					HandleInvoieItemPrice(invoiceItemConstructor);
			else
			{
				foreach (IInvoiceItem serviceInvoiceItem in invoiceItem.List_InvoiceItems)
				{
					if (invoiceItem.IsInsuranceAppliedToInvoice)
					{
						if (serviceInvoiceItem.IsInsuranceApplied_InvoiceItem)
						{
							double insurancePercentage = invoiceItem.InsurancePercentageApplied;
							double patientPercentage = Math.Abs(Convert.ToDouble(invoiceItem.InsurancePercentageApplied)*100 - 100)/100;
							serviceInvoiceItem.InsuranceShare_BeforeAddsOn_InvoiceItem = invoiceItem.ItemPrice * insurancePercentage;
							serviceInvoiceItem.PatientShare_BeforeAddsOn_InvoiceItem = invoiceItem.ItemPrice * patientPercentage;
							serviceInvoiceItem.IsInsuranceApplied_InvoiceItem = true;
							break;
						}
						serviceInvoiceItem.InsuranceShare_BeforeAddsOn_InvoiceItem = 0;

						if(invoiceItem.InsuranceCarrierID == null && invoiceItem.InsuranceLevelID == null)
							continue;

						int insuranceProviderId = Convert.ToInt32(invoiceItem.InsuranceCarrierID);
						int insuranceLevelId = Convert.ToInt32(invoiceItem.InsuranceLevelID);

						switch (invoiceItem.InvoiceItemType)
						{
							case InvoiceItemType.OutPatient_InvoiceDetail_ExaminationService:

								break;
						}
					}
				}
			}
		}

		public static bool HandleInvoiceItemPrice(IInvoiceItem invoiceItem, object serviceID, object doctorID,
			object itemPrice, object insuranceCarrierID, object insuranceLevelID)
		{
			if (serviceID == null)
				return false;

			double serviceProperPrice = GetServicePrice(serviceID, doctorID, insuranceCarrierID, insuranceLevelID);

			//ServicePrice_cu servicePrice = 

			return true;
		}

		public static double GetAccummulativePatientShare(List<InvoiceDetail> list_InvoiceDetails)
		{
			if (list_InvoiceDetails == null || list_InvoiceDetails.Count == 0)
				return 0;

			return list_InvoiceDetails.Sum(item => item.PatientShare);
		}

		public static double GetTotalPricePerUnit(List<FinanceInvoiceDetail> financeInvoiceDetails)
		{
			if (financeInvoiceDetails == null || financeInvoiceDetails.Count == 0)
				return 0;

			return financeInvoiceDetails.Sum(item => Convert.ToDouble(item.PricePerUnit));
		}

		public static double GetTotalDiscount(List<FinanceInvoiceDetail> financeInvoiceDetails)
		{
			if (financeInvoiceDetails == null || financeInvoiceDetails.Count == 0)
				return 0;

			return financeInvoiceDetails.Sum(item => Convert.ToDouble(item.DiscountAmount));
		}

		public static double GetTotalNet(List<FinanceInvoiceDetail> financeInvoiceDetails)
		{
			if (financeInvoiceDetails == null || financeInvoiceDetails.Count == 0)
				return 0;

			return financeInvoiceDetails.Sum(item => item.NetPrice);
		}

		public static double GetSurchargeAmount(Invoice invoice)
		{
			if (invoice == null)
				return 0;

			if (invoice.InvoiceShareObject == null)
				return 0;

			if (invoice.InvoiceShareObject.IsSurchargeApplied)
				return invoice.InvoiceShareObject.TotalSurchargeAccummulativePercentage != null
					? Convert.ToDouble(invoice.InvoiceShareObject.TotalSurchargeAccummulativePercentage)
					: 0;

			return 0;
		}

		public static double GetStampAmount(Invoice invoice, bool isPatientShare)
		{
			if (invoice == null)
				return 0;

			double stampValue = invoice.InvoiceShareObject.TotalStampAmount != null
				? Convert.ToDouble(invoice.InvoiceShareObject.TotalStampAmount)
				: GetAccummulativeSurchargeAmount((DB_InvoiceType)invoice.InvoiceType_P_ID, DB_SurchargeType.MedicalStamp);

			if (!invoice.InvoiceShareObject.IsStampApplied)
				return 0;

			if (Convert.ToBoolean(invoice.InvoiceShareObject.IsStampAppliedToPatientOnly) &&
			    !Convert.ToBoolean(invoice.InvoiceShareObject.IsStampDistributedToInsurancePercentage) && isPatientShare)
				return stampValue;

			if (Convert.ToBoolean(invoice.InvoiceShareObject.IsStampAppliedToPatientOnly) &&
			    !Convert.ToBoolean(invoice.InvoiceShareObject.IsStampDistributedToInsurancePercentage) && !isPatientShare)
				return 0;

			if (Convert.ToBoolean(invoice.InvoiceShareObject.IsStampAppliedToInsuranceOnly) &&
				!Convert.ToBoolean(invoice.InvoiceShareObject.IsStampDistributedToInsurancePercentage) && !isPatientShare)
				return stampValue;

			if (Convert.ToBoolean(invoice.InvoiceShareObject.IsStampAppliedToInsuranceOnly) &&
				!Convert.ToBoolean(invoice.InvoiceShareObject.IsStampDistributedToInsurancePercentage) && isPatientShare)
				return 0;

			if (Convert.ToBoolean(invoice.InvoiceShareObject.IsStampDistributedToInsurancePercentage))
			{
				if (invoice.InvoiceShareObject.InsurancePercentageApplied != null)
					return isPatientShare
						? stampValue*(1 - Convert.ToDouble(invoice.InvoiceShareObject.InsurancePercentageApplied))
						: stampValue*Convert.ToDouble(invoice.InvoiceShareObject.InsurancePercentageApplied);
					return isPatientShare ? stampValue : 0;
			}

			return stampValue;
		}

		public static double GetServicePrice(object serviceId, object doctorId, object insuranceCarrierId, object insuranceLevelId)
		{
			if (insuranceCarrierId != null && insuranceLevelId != null)
			{
				InsuranceCarrier_InsuranceLevel_cu bridge =
					InsuranceCarrier_InsuranceLevel_cu.ItemsList.Find(
						item =>
							Convert.ToInt32(item.InsuranceCarrier_CU_ID).Equals(Convert.ToInt32(insuranceCarrierId)) &&
							Convert.ToInt32(item.InsuranceLevel_CU_ID).Equals(Convert.ToInt32(insuranceLevelId)));
				return GetServicePrice(serviceId, doctorId, bridge.ID);
			}

			return GetServicePrice(serviceId, doctorId, null);
		}

		public static double GetServicePrice(object serviceId, object doctorId, object insuranceBridgeId)
		{
			Doctor_cu doctor = null;
			if (doctorId != null)
				doctor = Doctor_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(doctorId)));

			DoctorCategory_cu doctorCategory = null;
			if (doctor != null && doctor.DoctorCategory_CU_ID != null)
				doctorCategory =
					DoctorCategory_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(doctor.DoctorCategory_CU_ID)));

			Service_cu service = null;
			if (serviceId != null)
				service = Service_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(serviceId)));

			ServiceCategory_cu serviceCategory = null;
			if (service != null && service.ServiceCategory_CU_ID != null)
				serviceCategory =
					ServiceCategory_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(service.ServiceCategory_CU_ID)));

			ServicePrice_cu servicePrice = null;

			#region Doctor && Service
			//if doctor has a record pricing with that service and in insurance
			servicePrice = GetServicePriceObject_ByDoctorAndService(serviceId, doctorId, insuranceBridgeId);

			if (servicePrice != null && servicePrice.InsurancePrice != null && insuranceBridgeId != null)
				return Convert.ToDouble(servicePrice.InsurancePrice);

			if (servicePrice != null)
				return Convert.ToDouble(servicePrice.Price);
			#endregion

			#region Doctor and ServiceCategory
			//if that doctor has a record with the Service Category of that service and in insurance
			if (serviceCategory != null)
				servicePrice = GetServicePriceObject_ByDoctorAndServiceCategory(serviceCategory.ID, doctorId, insuranceBridgeId);

			if (servicePrice != null && servicePrice.InsurancePrice != null && insuranceBridgeId != null)
				return Convert.ToDouble(servicePrice.InsurancePrice);

			if (servicePrice != null)
				return Convert.ToDouble(servicePrice.Price);
			#endregion

			#region DoctorCategory and Service
			//if the doctor has not any records but the doctor category has a record with that service
			if (doctorCategory != null)
				servicePrice = GetServicePriceObject_ByDoctorCategoryAndService(serviceId, doctorCategory.ID, insuranceBridgeId);

			if (servicePrice != null && servicePrice.InsurancePrice != null && insuranceBridgeId != null)
				return Convert.ToDouble(servicePrice.InsurancePrice);

			if (servicePrice != null)
				return Convert.ToDouble(servicePrice.Price);
			#endregion

			#region DoctorCategory and ServiceCategory

			if (serviceCategory != null && doctorCategory != null)
				servicePrice = GetServicePriceObject_ByDoctorCategoryAndServiceCategory(serviceCategory.ID, doctorCategory.ID,
					insuranceBridgeId);

			if (servicePrice != null && servicePrice.InsurancePrice != null)
				return Convert.ToDouble(servicePrice.InsurancePrice);

			if (servicePrice != null)
				return Convert.ToDouble(servicePrice.Price);

			#endregion

			#region Doctor only and Not (Service, ServiceCategory)

			if (doctor != null && insuranceBridgeId != null)
				servicePrice =
					ServicePrice_cu.ItemsList.Find(
						item =>
							Convert.ToInt32(item.Doctor_CU_ID).Equals(Convert.ToInt32(doctor.ID)) && item.Service_CU_ID == null &&
							item.ServiceCategory_CU_ID == null &&
							Convert.ToInt32(item.InsuranceCarrier_InsuranceLevel_CU_ID).Equals(Convert.ToInt32(insuranceBridgeId)));
			else if (doctor != null)
				servicePrice =
					ServicePrice_cu.ItemsList.Find(
						item =>
							Convert.ToInt32(item.Doctor_CU_ID).Equals(Convert.ToInt32(doctor.ID)) && item.Service_CU_ID == null &&
							item.ServiceCategory_CU_ID == null);

			if (servicePrice != null && servicePrice.InsurancePrice != null)
				return Convert.ToDouble(servicePrice.InsurancePrice);

			if (servicePrice != null)
				return Convert.ToDouble(servicePrice.Price);

			#endregion

			#region Service

			servicePrice = GetServicePriceObject_ByService(serviceId, insuranceBridgeId);

			if (servicePrice != null && servicePrice.InsurancePrice != null && insuranceBridgeId != null)
				return Convert.ToDouble(servicePrice.InsurancePrice);

			if (servicePrice != null)
				return Convert.ToDouble(servicePrice.Price);

			#endregion

			#region ServiceCategory

			if (serviceCategory != null)
				servicePrice = GetServicePriceObject_ByServiceCategory(serviceCategory.ID, insuranceBridgeId);
			
			if (servicePrice != null && servicePrice.InsurancePrice != null && insuranceBridgeId != null)
				return Convert.ToDouble(servicePrice.InsurancePrice);

			if (servicePrice != null)
				return Convert.ToDouble(servicePrice.Price);

			#endregion

			//if there is no record get the default price of exists
			if (service != null && service.DefaultPrice != null)
				return Convert.ToDouble(service.DefaultPrice);

			return 0;
		}

		public static ServicePrice_cu GetServicePriceObject_ByDoctorAndService(object serviceID, object doctorID,
			object insuranceBridgeId)
		{
			if (serviceID == null || doctorID == null)
				return null;

			ServicePrice_cu servicePriceObject = null;

			servicePriceObject =
				ServicePrice_cu.ItemsList.Find(
					item =>
						Convert.ToInt32(item.Doctor_CU_ID).Equals(Convert.ToInt32(doctorID)) &&
						Convert.ToInt32(item.Service_CU_ID).Equals(Convert.ToInt32(serviceID)));
			if(insuranceBridgeId != null)
				servicePriceObject =
					ServicePrice_cu.ItemsList.Find(
						item =>
							Convert.ToInt32(item.Doctor_CU_ID).Equals(Convert.ToInt32(doctorID)) &&
							Convert.ToInt32(item.Service_CU_ID).Equals(Convert.ToInt32(serviceID)) &&
							Convert.ToInt32(item.InsuranceCarrier_InsuranceLevel_CU_ID).Equals(Convert.ToInt32(insuranceBridgeId)));

			return servicePriceObject;
		}

		public static ServicePrice_cu GetServicePriceObject_ByDoctorAndServiceCategory(object serviceCategoryID, object doctorID,
			object insuranceBridgeId)
		{
			if (serviceCategoryID == null || doctorID == null)
				return null;

			ServicePrice_cu servicePriceObject = null;

			servicePriceObject =
				ServicePrice_cu.ItemsList.Find(
					item =>
						Convert.ToInt32(item.Doctor_CU_ID).Equals(Convert.ToInt32(doctorID)) &&
						Convert.ToInt32(item.ServiceCategory_CU_ID).Equals(Convert.ToInt32(serviceCategoryID)));
			if (insuranceBridgeId != null)
				servicePriceObject =
					ServicePrice_cu.ItemsList.Find(
						item =>
							Convert.ToInt32(item.Doctor_CU_ID).Equals(Convert.ToInt32(doctorID)) &&
							Convert.ToInt32(item.ServiceCategory_CU_ID).Equals(Convert.ToInt32(serviceCategoryID)) &&
							Convert.ToInt32(item.InsuranceCarrier_InsuranceLevel_CU_ID).Equals(Convert.ToInt32(insuranceBridgeId)));

			return servicePriceObject;
		}

		public static ServicePrice_cu GetServicePriceObject_ByDoctorCategoryAndService(object serviceID, object doctorCaregoryID,
			object insuranceBridgeId)
		{
			if (serviceID == null || doctorCaregoryID == null)
				return null;

			ServicePrice_cu servicePriceObject = null;

			servicePriceObject =
				ServicePrice_cu.ItemsList.Find(
					item =>
						Convert.ToInt32(item.DoctorCategory_CU_ID).Equals(Convert.ToInt32(doctorCaregoryID)) &&
						Convert.ToInt32(item.Service_CU_ID).Equals(Convert.ToInt32(serviceID)));
			if (insuranceBridgeId != null)
				servicePriceObject =
					ServicePrice_cu.ItemsList.Find(
						item =>
							Convert.ToInt32(item.DoctorCategory_CU_ID).Equals(Convert.ToInt32(doctorCaregoryID)) &&
							Convert.ToInt32(item.Service_CU_ID).Equals(Convert.ToInt32(serviceID)) &&
							Convert.ToInt32(item.InsuranceCarrier_InsuranceLevel_CU_ID).Equals(Convert.ToInt32(insuranceBridgeId)));

			return servicePriceObject;
		}

		public static ServicePrice_cu GetServicePriceObject_ByDoctorCategoryAndServiceCategory(object serviceCategoryID,
			object doctorCaregoryID,
			object insuranceBridgeId)
		{
			if (serviceCategoryID == null || doctorCaregoryID == null)
				return null;

			ServicePrice_cu servicePriceObject = null;

			servicePriceObject =
				ServicePrice_cu.ItemsList.Find(
					item =>
						Convert.ToInt32(item.DoctorCategory_CU_ID).Equals(Convert.ToInt32(doctorCaregoryID)) &&
						Convert.ToInt32(item.ServiceCategory_CU_ID).Equals(Convert.ToInt32(serviceCategoryID)));
			if (insuranceBridgeId != null)
				servicePriceObject =
					ServicePrice_cu.ItemsList.Find(
						item =>
							Convert.ToInt32(item.DoctorCategory_CU_ID).Equals(Convert.ToInt32(doctorCaregoryID)) &&
							Convert.ToInt32(item.ServiceCategory_CU_ID).Equals(Convert.ToInt32(serviceCategoryID)) &&
							Convert.ToInt32(item.InsuranceCarrier_InsuranceLevel_CU_ID).Equals(Convert.ToInt32(insuranceBridgeId)));

			return servicePriceObject;
		}

		public static ServicePrice_cu GetServicePriceObject_ByService(object serviceID, object insuranceBridgeId)
		{
			if (serviceID == null)
				return null;

			ServicePrice_cu servicePriceObject = null;

			servicePriceObject =
				ServicePrice_cu.ItemsList.Find(
					item =>
						Convert.ToInt32(item.Service_CU_ID).Equals(Convert.ToInt32(serviceID)));
			if (insuranceBridgeId != null)
				servicePriceObject =
					ServicePrice_cu.ItemsList.Find(
						item =>
							Convert.ToInt32(item.Service_CU_ID).Equals(Convert.ToInt32(serviceID)) &&
							Convert.ToInt32(item.InsuranceCarrier_InsuranceLevel_CU_ID).Equals(Convert.ToInt32(insuranceBridgeId)));

			return servicePriceObject;
		}

		public static ServicePrice_cu GetServicePriceObject_ByServiceCategory(object serviceCategoryID, object insuranceBridgeId)
		{
			if (serviceCategoryID == null)
				return null;

			ServicePrice_cu servicePriceObject = null;

			servicePriceObject =
					ServicePrice_cu.ItemsList.Find(
						item =>
							Convert.ToInt32(item.ServiceCategory_CU_ID).Equals(Convert.ToInt32(serviceCategoryID)));
			if(insuranceBridgeId != null)
				servicePriceObject =
					ServicePrice_cu.ItemsList.Find(
						item =>
							Convert.ToInt32(item.ServiceCategory_CU_ID).Equals(Convert.ToInt32(serviceCategoryID)) &&
							Convert.ToInt32(item.InsuranceCarrier_InsuranceLevel_CU_ID).Equals(Convert.ToInt32(insuranceBridgeId)));

			return servicePriceObject;
		}

		public static double GetAccommodationUnitPricePerDay(object roomBedId, bool forceGettingTheRoomPrice,
			object insuranceCarrierId, object insuranceLevelId)
		{
			double price = 0;

			InPatientRoomClassification_cu roomClass = null;
			InPatientRoom_cu room = null;
			InPatientRoomBed_cu bed = null;
			InsuranceCarrier_InsuranceLevel_cu insuranceBridge = null;

			if (insuranceCarrierId != null && insuranceLevelId != null)
				insuranceBridge =
					InsuranceCarrier_InsuranceLevel_cu.ItemsList.Find(
						item =>
							Convert.ToInt32(item.InsuranceCarrier_CU_ID).Equals(Convert.ToInt32(insuranceCarrierId)) &&
							Convert.ToInt32(item.InsuranceLevel_CU_ID).Equals(Convert.ToInt32(insuranceLevelId)));

			if (roomBedId != null)
				bed = InPatientRoomBed_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(roomBedId)));

			if (bed != null)
				room = InPatientRoom_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(bed.InPatientRoom_CU_ID)));

			if (room != null)
				roomClass =
					InPatientRoomClassification_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(room.InPatientRoomClassification_CU_ID)));

			InPatientRoomBed_InPatientAdmissionPricingType_cu roomBedPricing = null;
			InPatientRoom_InPatientAdmissionPricingType_cu roomPricing = null;
			InPatientRoomClassification_InPatientAdmissionPricingType_cu roomClassPricing = null;

			if (bed != null && !forceGettingTheRoomPrice)
			{
				if (insuranceBridge != null)
					roomBedPricing =
						InPatientRoomBed_InPatientAdmissionPricingType_cu.ItemsList.Find(
							item =>
								Convert.ToInt32(item.InPatientRoomBed_CU_ID).Equals(Convert.ToInt32(bed.ID)) &&
								Convert.ToInt32(item.InsuranceCarrier_InsuranceLevel_CU_ID).Equals(Convert.ToInt32(insuranceBridge.ID)));
				if (roomBedPricing == null)
					roomBedPricing =
						InPatientRoomBed_InPatientAdmissionPricingType_cu.ItemsList.Find(
							item =>
								Convert.ToInt32(item.InPatientRoomBed_CU_ID).Equals(Convert.ToInt32(bed.ID)));
				if (roomBedPricing != null)
					return roomBedPricing.PricePerDay;
			}
			else if (forceGettingTheRoomPrice && room != null)
			{
				if (insuranceBridge != null)
					roomPricing =
						InPatientRoom_InPatientAdmissionPricingType_cu.ItemsList.Find(
							item =>
								Convert.ToInt32(item.InPatientRoom_CU_ID).Equals(Convert.ToInt32(room.ID)) &&
								Convert.ToInt32(item.InsuranceCarrier_InsuranceLevel_CU_ID).Equals(Convert.ToInt32(insuranceBridge.ID)));
				if (roomPricing == null)
					roomPricing =
						InPatientRoom_InPatientAdmissionPricingType_cu.ItemsList.Find(
							item =>
								Convert.ToInt32(item.InPatientRoom_CU_ID).Equals(Convert.ToInt32(room.ID)));
				if (roomPricing != null)
					return roomPricing.PricePerDay;
			}

			if (room != null)
			{
				if (insuranceBridge != null)
					roomPricing =
						InPatientRoom_InPatientAdmissionPricingType_cu.ItemsList.Find(
							item =>
								Convert.ToInt32(item.InPatientRoom_CU_ID).Equals(Convert.ToInt32(room.ID)) &&
								Convert.ToInt32(item.InsuranceCarrier_InsuranceLevel_CU_ID).Equals(Convert.ToInt32(insuranceBridge.ID)));
				if (roomPricing == null)
					roomPricing =
						InPatientRoom_InPatientAdmissionPricingType_cu.ItemsList.Find(
							item =>
								Convert.ToInt32(item.InPatientRoom_CU_ID).Equals(Convert.ToInt32(room.ID)));
				if (roomPricing != null)
					return roomPricing.PricePerDay;
			}

			if (roomClass != null)
			{
				if (insuranceBridge != null)
					roomClassPricing =
						InPatientRoomClassification_InPatientAdmissionPricingType_cu.ItemsList.Find(
							item =>
								Convert.ToInt32(item.InPatientRoomClassification_CU_ID)
									.Equals(Convert.ToInt32(room.InPatientRoomClassification_CU_ID)) &&
								Convert.ToInt32(item.InsuranceCarrier_InsuranceLevel_CU_ID).Equals(Convert.ToInt32(insuranceBridge.ID)));
				if (roomClassPricing == null)
					roomClassPricing =
						InPatientRoomClassification_InPatientAdmissionPricingType_cu.ItemsList.Find(
							item =>
								Convert.ToInt32(item.InPatientRoomClassification_CU_ID)
									.Equals(Convert.ToInt32(room.InPatientRoomClassification_CU_ID)));
				if (roomClassPricing != null)
					return roomClassPricing.PricePerDay;
			}

			return price;
		}

		public static double GetAccumulativeInvoiceDetails(List<InvoiceDetail> invoiceDetailsList, PriceType requiredPriceType)
		{
			if (invoiceDetailsList == null || invoiceDetailsList.Count == 0)
				return 0;

			switch (requiredPriceType)
			{
				case PriceType.PatientShare:
					return invoiceDetailsList.Sum(item => Convert.ToDouble(item.PatientShare));
				case PriceType.InsuranceShare:
					return invoiceDetailsList.Sum(item => Convert.ToDouble(item.InsuranceShare));
				case PriceType.TotalServicePriceBeforeSurcharges:
					return invoiceDetailsList.Sum(item => Convert.ToDouble(item.PatientShare)) +
						   invoiceDetailsList.Sum(item => Convert.ToDouble(item.InsuranceShare));
			}

			return 0;
		}

		public static bool IsInvoiceReadyForPayments(Invoice invoice)
		{
			if (invoice == null)
				return false;

			if (invoice.InvoiceDetails == null || invoice.InvoiceDetails.Count == 0)
				return false;

			return true;
		}

		public static bool HasSurchargeRecord_ServiceType(DB_ServiceType serviceType, Surcharge_cu surcharge,
			DB_InvoiceType invoiceType)
		{
			ServiceType_Surcharge_cu serviceTypeSurcharge =
				ServiceType_Surcharge_cu.ItemsList.Find(
					item =>
						Convert.ToInt32(item.ServiceType_P_ID).Equals(Convert.ToInt32(serviceType)) &&
						Convert.ToInt32(item.Surcharge_CU_ID).Equals(Convert.ToInt32(surcharge.ID)) &&
						Convert.ToInt32(item.InvoiceType_P_ID).Equals((int)invoiceType));
			if (serviceTypeSurcharge != null)
				return serviceTypeSurcharge.IsApplied;

			return false;
		}

		public static bool HasSurchargeRecord_ServiceCategory(ServiceCategory_cu serviceCategory,
			Surcharge_cu surcharge, DB_InvoiceType invoiceType)
		{
			if (serviceCategory == null)
				return false;

			ServiceCategory_Surcharge_cu serviceCategorySurcharge =
				ServiceCategory_Surcharge_cu.ItemsList.Find(
					item =>
						item.ServiceCategory_CU_ID.Equals(serviceCategory.ID) &&
						Convert.ToInt32(item.Surcharge_CU_ID).Equals(Convert.ToInt32(surcharge.ID)) &&
						Convert.ToInt32(item.InvoiceType_P_ID).Equals((int)invoiceType));
			if (serviceCategorySurcharge != null)
				return serviceCategorySurcharge.IsApplied;

			return false;
		}

		public static bool HasSurchargeRecord_Service(Service_cu service, Surcharge_cu surcharge,
			DB_InvoiceType invoiceType)
		{
			if (service == null)
				return false;

			Service_Surcharge_cu serviceSurcharge =
				Service_Surcharge_cu.ItemsList.Find(
					item =>
						item.Service_CU_ID.Equals(service.ID) &&
						Convert.ToInt32(item.Surcharge_CU_ID).Equals(Convert.ToInt32(surcharge.ID)) &&
						Convert.ToInt32(item.InvoiceType_P_ID).Equals((int)invoiceType));
			if (serviceSurcharge != null)
				return serviceSurcharge.IsApplied;

			return false;
		}

		public static bool HasSurchargeRecord_InvoiceType(DB_InvoiceType invoiceType, Surcharge_cu surcharge)
		{
			InvoiceType_Surcharge_cu invoiceTypeSurcharge =
				InvoiceType_Surcharge_cu.ItemsList.Find(
					item =>
						Convert.ToInt32(item.InvoiceType_P_ID).Equals((int)invoiceType) &&
						Convert.ToInt32(item.Surcharge_CU_ID).Equals(Convert.ToInt32(surcharge.ID)));
			if (invoiceTypeSurcharge != null)
				return invoiceTypeSurcharge.IsApplied;

			return false;
		}

		public static double GetAccummulativeSurchargeAmount(DB_InvoiceType invoiceType, DB_SurchargeType surchargeType)
		{
			double amount = 0;
			List<Surcharge_cu> surchargeList =
				Surcharge_cu.ItemsList.FindAll(
					item => Convert.ToInt32(item.SurchargeType_P_ID).Equals(Convert.ToInt32(surchargeType)));
			if (surchargeList.Count == 0)
				return amount;

			foreach (Surcharge_cu surchargeCu in surchargeList)
			{
				if (HasSurchargeRecord_InvoiceType(invoiceType, surchargeCu))
				{
					InvoiceType_Surcharge_cu invoiceTypeSurcharge =
						InvoiceType_Surcharge_cu.ItemsList.Find(
							item =>
								Convert.ToInt32(item.InvoiceType_P_ID).Equals(Convert.ToInt32(invoiceType)) &&
								Convert.ToInt32(item.Surcharge_CU_ID).Equals(Convert.ToInt32(surchargeCu.ID)));
					if (invoiceTypeSurcharge != null)
						amount += surchargeCu.Amount;
				}
			}

			return amount;
		}

		public static double GetInvoiceCreationLineTotal(object sellingPrice, object quantity, object discount,
			object discountTypeID)
		{
			double total = Convert.ToDouble(quantity)*Convert.ToDouble(sellingPrice);

			if(discount != null && discountTypeID != null)
				switch ((DB_DiscountType)discountTypeID)
				{
					case DB_DiscountType.Amount:
						total = total - Convert.ToDouble(discount);
						break;
					case DB_DiscountType.Percentage:
						
						break;
				}

				if (Convert.ToBoolean(discountTypeID))
					total = total - Convert.ToDouble(discount);
				else
				{
					double discountPercentage = Convert.ToDouble(discount) / 100;
					total = total - (total * discountPercentage);
				}

			return total;
		}

		public enum CustomerBalanceType
		{
			None = 0,
			NetBalance = 1,
			TotalBalance = 2
		}

		public static double GetCustomerBalance(CustomerBalanceType customerBalanceType, object invoiceTypeID,
			object customerID)
		{
			double balance = 0;

			List<GetCustomerBalance_Result> result;
			using (DBCommon.DBContext_External)
				if (customerID != null && invoiceTypeID != null)
					result =
						DBCommon.DBContext_External.GetCustomerBalance(Convert.ToInt32(customerID), Convert.ToInt32(invoiceTypeID))
							.ToList();
				else if (customerID != null)
					result = DBCommon.DBContext_External.GetCustomerBalance(Convert.ToInt32(customerID), null).ToList();
				else if (invoiceTypeID != null)
					result = DBCommon.DBContext_External.GetCustomerBalance(null, Convert.ToInt32(invoiceTypeID)).ToList();
				else
					result = DBCommon.DBContext_External.GetCustomerBalance(null, null).ToList();

			if (result.Count > 0)
				foreach (GetCustomerBalance_Result balanceResult in result)
					switch (customerBalanceType)
					{
						case CustomerBalanceType.TotalBalance:
							balance = balance + Convert.ToDouble(balanceResult.TotalPayments) -
							          Convert.ToDouble(balanceResult.ReturningAmount);
							break;
						case CustomerBalanceType.NetBalance:
							balance = balance + Convert.ToDouble(balanceResult.NetBalance);
							break;
					}

			return balance;
		}

		public static double GetSupplierBalance(object invoiceTypeID, object customerID)
		{
			double balance = 0;

			List<GetSupplierBalance_Result> result;
			using (DBCommon.DBContext_External)
				if (customerID != null && invoiceTypeID != null)
					result =
						DBCommon.DBContext_External.GetSupplierBalance(Convert.ToInt32(customerID), Convert.ToInt32(invoiceTypeID))
							.ToList();
				else if (customerID != null)
					result = DBCommon.DBContext_External.GetSupplierBalance(Convert.ToInt32(customerID), null).ToList();
				else if (invoiceTypeID != null)
					result = DBCommon.DBContext_External.GetSupplierBalance(null, Convert.ToInt32(invoiceTypeID)).ToList();
				else
					result = DBCommon.DBContext_External.GetSupplierBalance(null, null).ToList();

			if (result.Count > 0)
				foreach (GetSupplierBalance_Result balanceResult in result)
					balance = balance + Convert.ToDouble(balanceResult.TotalPayments) - Convert.ToDouble(balanceResult.ReturningAmount);

			return balance;
		}

		public static bool IsFloorOfLocationHasMainCashBox(int floorID)
		{
			Floor_cu floor = Floor_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(floorID)));
			if (floor == null)
				return false;

			if (floor.Location_CU_ID != null)
				return IsLocationHasMainCashBox(Convert.ToInt32(floor.Location_CU_ID));

			return false;
		}

		public static bool IsLocationHasMainCashBox(int locationID)
		{
			Location_cu location = Location_cu.ItemsList.Find(item => item.ID.Equals(locationID));
			if (location == null)
				return false;

			return IsLocationHasMainCashBox(location);
		}

		public static bool IsLocationHasMainCashBox(Location_cu location)
		{
			if (location == null)
				return false;

			List<Floor_cu> floorsList =
				Floor_cu.ItemsList.FindAll(item => Convert.ToInt32(item.Location_CU_ID).Equals(Convert.ToInt32(location.ID)));
			List<CashBox_cu> cashBoxesList = new List<CashBox_cu>();

			foreach (Floor_cu floorCu in floorsList)
			{
				if (floorCu.Location_CU_ID == null)
					continue;

				CashBox_cu internalLocation =
					CashBox_cu.ItemsList.Find(item => Convert.ToInt32(item.Floor_CU_ID).Equals(Convert.ToInt32(floorCu.ID)));
				if (internalLocation == null)
					continue;

				cashBoxesList.Add(internalLocation);
			}

			if (cashBoxesList.Count == 0)
				return false;

			foreach (CashBox_cu cashBox in cashBoxesList)
			{
				if (cashBox.IsMain)
					return true;
			}

			return false;
		}

		public static string GetNextMedicalInvoiceSerial()
		{
			string serial = "";

			string[] strArry = DBCommon.DBContext_External.GetInvoicePaymentSerial().ToArray();
			if (strArry.Length > 0)
				serial = strArry[0];

			return serial;
		}
	}
}
