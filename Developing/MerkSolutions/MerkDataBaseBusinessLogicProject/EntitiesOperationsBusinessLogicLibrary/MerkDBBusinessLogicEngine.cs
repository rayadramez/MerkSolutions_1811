using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary
{
	public enum AdmissionType
	{
		ClinicAdmission = 1,
		InPatientAdmission = 2,
		All = 3
	}

	public class MerkDBBusinessLogicEngine
	{
		public static DB_Station Private_StationPoint { get; set; }

		public static StationPoint_cu ActiveStationPoint { get; set; }

		public static StationPointStage_cu ActiveStationPointStage { get; set; }

		public static System.Timers.Timer ActiveTimer { get; set; }

		public static QueueManager CreateNewQueueManager(Invoice invoice, InvoiceDetail invoiceDetail)
		{
			Service_cu service;
			if (invoiceDetail.Service_CU_ID != null)
				service = GetService(Convert.ToInt32(invoiceDetail.Service_CU_ID));
			else
				return null;

			List<StationPointStage_cu> serviceType_StationPointStagesList = null;
			List<StationPointStage_cu> ServiceCategory_StationPointStagesList = null;
			List<StationPointStage_cu> service_StationPointStagesList = null;

			ServiceCategory_cu serviceCategory = GetServiceCategory(Convert.ToInt32(service.ServiceCategory_CU_ID));
			ServiceCategory_StationPoint_cu serviceCategoryStation = null;
			if (serviceCategory != null)
				serviceCategoryStation = GetServiceCategoryStationPoint(serviceCategory.ID);
			if (serviceCategoryStation != null)
				ServiceCategory_StationPointStagesList = GetStationPointStagesList(serviceCategoryStation.StationPoint_CU_ID, true);

			DB_ServiceType serviceType = (DB_ServiceType)service.ServiceType_P_ID;
			ServiceType_StationPoint_cu serviceTypeStation =
				ServiceType_StationPoint_cu.ItemsList.Find(
					item => Convert.ToInt32(item.ServiceType_P_ID).Equals((int)serviceType));
			if (serviceTypeStation != null)
				serviceType_StationPointStagesList = GetStationPointStagesList(serviceTypeStation.StationPoint_CU_ID, true);

			Service_StationPoint_cu serviceStation = GetServiceStationPoint(service.ID);
			if (serviceStation != null)
				service_StationPointStagesList = GetStationPointStagesList(serviceStation.StationPoint_CU_ID, true);

			if (serviceStation != null)
				return CreateNewQueueManager(invoice, invoiceDetail, serviceStation, service_StationPointStagesList);
			if (serviceCategoryStation != null)
				return CreateNewQueueManager(invoice, invoiceDetail, serviceCategoryStation, ServiceCategory_StationPointStagesList);
			if (serviceTypeStation != null)
				return CreateNewQueueManager(invoice, invoiceDetail, serviceTypeStation, serviceType_StationPointStagesList);

			return null;
		}

		public static QueueManager CreateNewQueueManager(Invoice invoice, InvoiceDetail invoiceDetail,
			int stationPointID, int stationPointStageID)
		{
			QueueManager manager = DBCommon.CreateNewDBEntity<QueueManager>();
			if (invoiceDetail.Doctor_CU_ID != null)
				manager.Doctor_CU_ID = Convert.ToInt32(invoiceDetail.Doctor_CU_ID);
			manager.InvoiceDetailID = invoiceDetail.ID;
			manager.Service_CU_ID = invoiceDetail.Service_CU_ID != null
				? Convert.ToInt32(invoiceDetail.Service_CU_ID)
				: (int?)null;
			manager.StationPoint_CU_ID = stationPointID;
			manager.StationPointStage_CU_ID = stationPointStageID;
			manager.Patient_CU_ID = invoice.Patient_CU_ID;
			manager.QueueManagerStatus_P_ID = (int)DB_QueueManagerStatus.Waiting;
			manager.IsOnDuty = true;
			return manager;
		}

		public static QueueManager CreateNewQueueManager(Invoice invoice, InvoiceDetail invoiceDetail,
			ServiceCategory_StationPoint_cu serviceCategoryStationPoint,
			List<StationPointStage_cu> stationPointStagesList)
		{
			if (invoice == null || invoiceDetail == null || serviceCategoryStationPoint == null)
				return null;

			QueueManager manager = DBCommon.CreateNewDBEntity<QueueManager>();
			if (invoiceDetail.Doctor_CU_ID != null)
				manager.Doctor_CU_ID = Convert.ToInt32(invoiceDetail.Doctor_CU_ID);
			manager.InvoiceDetailID = invoiceDetail.ID;
			manager.Service_CU_ID = invoiceDetail.Service_CU_ID != null
				? Convert.ToInt32(invoiceDetail.Service_CU_ID)
				: (int?)null;

			if (invoiceDetail.StationPointID != null)
				manager.StationPoint_CU_ID = Convert.ToInt32(invoiceDetail.StationPointID);
			else
				manager.StationPoint_CU_ID = serviceCategoryStationPoint.StationPoint_CU_ID;
			if (invoiceDetail.StationPointStagesID != null)
				manager.StationPointStage_CU_ID = Convert.ToInt32(invoiceDetail.StationPointStagesID);
			else if (stationPointStagesList.Count > 0)
				manager.StationPointStage_CU_ID = stationPointStagesList.First().ID;
			manager.Patient_CU_ID = invoice.Patient_CU_ID;
			manager.QueueManagerStatus_P_ID = (int)DB_QueueManagerStatus.Waiting;
			manager.IsOnDuty = true;
			return manager;
		}

		public static QueueManager CreateNewQueueManager(Invoice invoice, InvoiceDetail invoiceDetail,
			Service_StationPoint_cu serviceStation,
			List<StationPointStage_cu> stationPointStagesList)
		{
			QueueManager manager = DBCommon.CreateNewDBEntity<QueueManager>();
			if (invoiceDetail.Doctor_CU_ID != null)
				manager.Doctor_CU_ID = Convert.ToInt32(invoiceDetail.Doctor_CU_ID);
			manager.InvoiceDetailID = invoiceDetail.ID;
			manager.Service_CU_ID = invoiceDetail.Service_CU_ID != null
				? Convert.ToInt32(invoiceDetail.Service_CU_ID)
				: (int?) null;
			if (invoiceDetail.StationPointID != null)
				manager.StationPoint_CU_ID = Convert.ToInt32(invoiceDetail.StationPointID);
			else
				manager.StationPoint_CU_ID = serviceStation.StationPoint_CU_ID;
			if (invoiceDetail.StationPointStagesID != null)
				manager.StationPointStage_CU_ID = Convert.ToInt32(invoiceDetail.StationPointStagesID);
			else if (stationPointStagesList.Count > 0)
				manager.StationPointStage_CU_ID = stationPointStagesList.First().ID;
			manager.Patient_CU_ID = invoice.Patient_CU_ID;
			manager.QueueManagerStatus_P_ID = (int)DB_QueueManagerStatus.Waiting;
			manager.IsOnDuty = true;
			return manager;
		}

		public static QueueManager CreateNewQueueManager(Invoice invoice, InvoiceDetail invoiceDetail,
			ServiceType_StationPoint_cu serviceTypeStation,
			List<StationPointStage_cu> stationPointStagesList)
		{
			QueueManager manager = DBCommon.CreateNewDBEntity<QueueManager>();
			if (invoiceDetail.Doctor_CU_ID != null)
				manager.Doctor_CU_ID = Convert.ToInt32(invoiceDetail.Doctor_CU_ID);
			manager.InvoiceDetailID = invoiceDetail.ID;
			manager.Service_CU_ID = invoiceDetail.Service_CU_ID != null ? Convert.ToInt32(invoiceDetail.Service_CU_ID) : (int?)null;
			manager.StationPoint_CU_ID = serviceTypeStation.StationPoint_CU_ID;
			if (stationPointStagesList.Count > 0)
				manager.StationPointStage_CU_ID = stationPointStagesList.First().ID;
			manager.Patient_CU_ID = invoice.Patient_CU_ID;
			manager.QueueManagerStatus_P_ID = (int)DB_QueueManagerStatus.Waiting;
			manager.IsOnDuty = true;
			return manager;
		}

		public static Service_cu GetService(int id)
		{
			return Service_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(id)));
		}

		public static ServiceCategory_cu GetServiceCategory(int serviceCategoryID)
		{
			return ServiceCategory_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(serviceCategoryID)));
		}

		public static Service_StationPoint_cu GetServiceStationPoint(int serviceID)
		{
			return Service_StationPoint_cu.ItemsList.Find(
						item => Convert.ToInt32(item.Service_CU_ID).Equals(Convert.ToInt32(serviceID)));
		}

		public static ServiceCategory_StationPoint_cu GetServiceCategoryStationPoint(int serviceCategoryID)
		{
			return
				ServiceCategory_StationPoint_cu.ItemsList.Find(
					item => Convert.ToInt32(item.ServiceCategory_CU_ID).Equals(Convert.ToInt32(serviceCategoryID)));

		}

		public static List<StationPointStage_cu> GetStationPointStagesList(int stationPointID, bool orderByIndex)
		{
			List<StationPointStage_cu> list = StationPointStage_cu.ItemsList.FindAll(
				item => Convert.ToInt32(item.StationPoint_CU_ID).Equals(Convert.ToInt32(stationPointID)));

			return orderByIndex ? list.OrderBy(item => item.OrderIndex).ToList() : list;
		}

		public static List<ReadyInvoicesForAction> ReadyInvoicesForAction(AdmissionType admissionType,
			DateTime? InvoiceCreationDateStart,
			DateTime? InvoiceCreationDateEnd, bool? InvoiceIsOnDuty, bool? InvoiceIsFinanciallyReviewed,
			bool? InvoiceIsPrinted, bool? InvoiceIsPaymentEnough, int? DoctorID, Patient_cu Patient)
		{
			List<ReadyInvoicesForAction> readyInvoiceFormPaymentsList = new List<ReadyInvoicesForAction>();
			List<GetInvoiceForAddmission_Result> result = new List<GetInvoiceForAddmission_Result>();

			using (DBCommon.DBContext_External)
			{
				switch (admissionType)
				{
					case AdmissionType.ClinicAdmission:
						result =
							DBCommon.DBContext_External.GetInvoiceForAddmission(InvoiceCreationDateStart, InvoiceCreationDateEnd,
								(int)DB_InvoiceType.OutPatientPrivate, InvoiceIsOnDuty, InvoiceIsFinanciallyReviewed, InvoiceIsPrinted,
								InvoiceIsPaymentEnough, DoctorID, Patient != null ? Patient.Person_CU_ID : (int?)null)
								.OrderByDescending(item => item.InvoiceCreationDate)
								.ToList();
						result.AddRange(
							DBCommon.DBContext_External.GetInvoiceForAddmission(InvoiceCreationDateStart, InvoiceCreationDateEnd,
								(int)DB_InvoiceType.OutPatientNotPrivate, InvoiceIsOnDuty, InvoiceIsFinanciallyReviewed, InvoiceIsPrinted,
								InvoiceIsPaymentEnough, DoctorID, Patient != null ? Patient.Person_CU_ID : (int?)null)
								.OrderByDescending(item => item.InvoiceCreationDate)
								.ToList());
						break;
					case AdmissionType.InPatientAdmission:
						result =
							DBCommon.DBContext_External.GetInvoiceForAddmission(InvoiceCreationDateStart, InvoiceCreationDateEnd,
								(int)DB_InvoiceType.InPatientPrivate, InvoiceIsOnDuty, InvoiceIsFinanciallyReviewed, InvoiceIsPrinted,
								InvoiceIsPaymentEnough, DoctorID, Patient != null ? Patient.Person_CU_ID : (int?)null)
								.OrderByDescending(item => item.InvoiceCreationDate)
								.ToList();
						result.AddRange(
							DBCommon.DBContext_External.GetInvoiceForAddmission(InvoiceCreationDateStart, InvoiceCreationDateEnd,
								(int)DB_InvoiceType.InPatientNotPrivate, InvoiceIsOnDuty, InvoiceIsFinanciallyReviewed, InvoiceIsPrinted,
								InvoiceIsPaymentEnough, DoctorID, Patient != null ? Patient.Person_CU_ID : (int?)null)
								.OrderByDescending(item => item.InvoiceCreationDate)
								.ToList());
						break;
				}

				List<Invoice> invoicesList = new List<Invoice>();
				foreach (GetInvoiceForAddmission_Result getInvoiceForAddmissionResult in result)
				{
					Invoice invoice = DBCommon.GetEntity<Invoice>(getInvoiceForAddmissionResult.InvoiceID);
					if (invoice == null)
						continue;
					invoicesList.Add(invoice);
					ReadyInvoicesForAction readyInvoicesForPayments = new ReadyInvoicesForAction()
					{
						PatientID = getInvoiceForAddmissionResult.PatientID,
						PatientFullName = getInvoiceForAddmissionResult.PatientFullName,
						InvoiceID = getInvoiceForAddmissionResult.InvoiceID,
						InvoiceType = (DB_InvoiceType)invoice.InvoiceType_P_ID,
						InvoiceCreationDate = getInvoiceForAddmissionResult.InvoiceCreationDate,
						InvoiceSerial = getInvoiceForAddmissionResult.InvoiceSerial,
						DoctorID = getInvoiceForAddmissionResult.DoctorID,
						DoctorName = getInvoiceForAddmissionResult.DoctorName,
						IsPaymentEnough = getInvoiceForAddmissionResult.IsPaymentEnough,
						TotalRequired = invoice.PatientShare_BeforeAddsOn_InvoiceItem,
						TotalPayments = invoice.CalculatedTotal_Payments,
						ActiveInvoice = invoice,
						ActivePatient = Patient
					};

					readyInvoiceFormPaymentsList.Add(readyInvoicesForPayments);
				}

			}

			return readyInvoiceFormPaymentsList;
		}

		public static List<ReadyInvoicesForAction> GetReadyInvoicesForActionList(List<GetBriefQueue_Result> queueBrieflist)
		{
			if (queueBrieflist == null || queueBrieflist.Count == 0)
				return null;
			List<ReadyInvoicesForAction> list = new List<ReadyInvoicesForAction>();
			foreach (GetBriefQueue_Result queueResult in queueBrieflist)
			{
				Invoice invoice = DBCommon.GetEntity<Invoice>(queueResult.InvoiceID);
				if (invoice == null)
					continue;
				Patient_cu patient = DBCommon.GetEntity<Patient_cu>(invoice.Patient_CU_ID);
				if (patient == null)
					continue;
				ReadyInvoicesForAction readyInvoicesForPayments = new ReadyInvoicesForAction()
				{
					PatientID = queueResult.PatientID,
					PatientFullName = queueResult.PatientFullName,
					InvoiceID = queueResult.InvoiceID,
					InvoiceType = (DB_InvoiceType)invoice.InvoiceType_P_ID,
					InvoiceCreationDate = queueResult.ReservationTime,
					InvoiceSerial = invoice.InvoiceSerial,
					DoctorID = queueResult.DoctorID,
					DoctorName = queueResult.DoctorFullName,
					IsPaymentEnough = invoice.IsPaymentsEnough,
					TotalRequired = invoice.PatientShare_BeforeAddsOn_InvoiceItem,
					TotalPayments = invoice.CalculatedTotal_Payments,
					ActiveInvoice = invoice,
					ActivePatient = patient
				};

				list.Add(readyInvoicesForPayments);
			}

			return list;
		}

		public static List<ReadyInvoicesForAction> GetSpecificReadyInvoices(List<ReadyInvoicesForAction> readyInvoicesList,
			bool isInsurance, bool isInPatient)
		{
			if (readyInvoicesList == null || readyInvoicesList.Count == 0)
				return null;

			List<ReadyInvoicesForAction> listFirstStep = new List<ReadyInvoicesForAction>();
			List<ReadyInvoicesForAction> listSecondStep = new List<ReadyInvoicesForAction>();

			foreach (ReadyInvoicesForAction readyInvoicesForAction in
					readyInvoicesList.FindAll(
						item =>
							Convert.ToBoolean(item.ActiveInvoice.InvoiceShareObject.IsInsuranceApplied).Equals(isInsurance)))
				listFirstStep.Add(readyInvoicesForAction);

			if (isInPatient)
				foreach (ReadyInvoicesForAction readyInvoicesForAction in
						listFirstStep.FindAll(
							item => Convert.ToInt32(item.ActiveInvoice.InvoiceType_P_ID).Equals((int)DB_InvoiceType.InPatientPrivate) ||
									Convert.ToInt32(item.ActiveInvoice.InvoiceType_P_ID).Equals((int)DB_InvoiceType.InPatientNotPrivate)))
					listSecondStep.Add(readyInvoicesForAction);
			else
				foreach (ReadyInvoicesForAction readyInvoicesForAction in
						listFirstStep.FindAll(
							item => Convert.ToInt32(item.ActiveInvoice.InvoiceType_P_ID).Equals((int)DB_InvoiceType.OutPatientPrivate) ||
									Convert.ToInt32(item.ActiveInvoice.InvoiceType_P_ID).Equals((int)DB_InvoiceType.OutPatientNotPrivate)))
					listSecondStep.Add(readyInvoicesForAction);

			return listSecondStep;
		}

		public static List<GetBriefQueue_Result> GetBriefQueue(object doctorID, object stationPointStageID, object date,
			object queueManagerStatus)
		{
			return DBCommon.DBContext_External.GetBriefQueue((int?)stationPointStageID, (int?)queueManagerStatus,
				(int?)doctorID, (DateTime?)date).ToList();
		}

		public static List<GetPreviousMedicalVisits_Result> GetPreviousMedicalVisitsList(object patientID,
			object isOnDUty,
			object serviceID, object visitTimingDateFrom, object visitTimingDateTo, object userID)
		{
			return GetPreviousMedicalVisits_Result.GetItemsList(patientID, isOnDUty, serviceID, visitTimingDateFrom,
				visitTimingDateTo, userID);
		}

		public static InvoiceDetail CreateNew_InvoiceDetail(InvoiceDetail parentInvoiceDetail, object serviceId,
			object servicePrice, bool useCustomPrice, object serviceCount, object serviceDate, object doctorId,
			object stationPointID, object stationPointStageID,
			object isServiceIncludedInInsurance, object insurancePercetnage, object isSurchargeApplied,
			object isTaxApplied, object serviceDescription)
		{
			InvoiceDetail serviceDetailObject = new InvoiceDetail();
			if (parentInvoiceDetail != null)
			{
				if (parentInvoiceDetail.InvoiceDetail1 == null)
					parentInvoiceDetail.InvoiceDetail1 = new List<InvoiceDetail>();

				parentInvoiceDetail.InvoiceDetail1.Add(serviceDetailObject);
			}

			serviceDetailObject.Service_CU_ID = Convert.ToInt32(serviceId);

			if (Convert.ToBoolean(isServiceIncludedInInsurance) && insurancePercetnage != null)
			{
				serviceDetailObject.PatientShare =
					(100 - Convert.ToDouble(insurancePercetnage)) / 100 * Convert.ToDouble(servicePrice);
				serviceDetailObject.InsuranceShare =
					Convert.ToDouble(insurancePercetnage) / 100 * Convert.ToDouble(servicePrice);
				serviceDetailObject.IsInsuranceApplied = true;
			}
			else
			{
				serviceDetailObject.PatientShare = Convert.ToDouble(servicePrice);
				serviceDetailObject.InsuranceShare = 0;
				serviceDetailObject.IsInsuranceApplied = false;
			}

			serviceDetailObject.IsOnDuty = true;
			serviceDetailObject.IsCustomPriceUsed = useCustomPrice;
			serviceDetailObject.IsSurchargeApplied = Convert.ToBoolean(isSurchargeApplied);
			serviceDetailObject.Count = Convert.ToInt32(serviceCount);
			serviceDetailObject.Doctor_CU_ID = Convert.ToInt32(doctorId);
			serviceDetailObject.Date = Convert.ToDateTime(serviceDate);
			if (serviceDescription != null)
				serviceDetailObject.Description = serviceDescription.ToString();

			if (stationPointID != null)
				serviceDetailObject.StationPointID = Convert.ToInt32(stationPointID);
			if (stationPointStageID != null)
				serviceDetailObject.StationPointStagesID = Convert.ToInt32(stationPointStageID);

			return serviceDetailObject;
		}

		public static InvoiceDetail_Accommodation CreateNew_InvoiceDetail_Accommodation(InvoiceDetail parentInvoiceDetail,
			object servicePrice, object startDate, object bedId, object overridenroomClassId, object isSurchargeApplied,
			object isTaxApplied, object serviceDescription,
			object isServiceIncludedInInsurance, object insurancePercetnage)
		{
			if (bedId == null || parentInvoiceDetail == null)
				return null;

			InvoiceDetail_Accommodation accommodation = DBCommon.CreateNewDBEntity<InvoiceDetail_Accommodation>();

			if (parentInvoiceDetail.InvoiceDetail_Accommodation == null)
				parentInvoiceDetail.InvoiceDetail_Accommodation = new List<InvoiceDetail_Accommodation>();
			parentInvoiceDetail.InvoiceDetail_Accommodation.Add(accommodation);

			if (Convert.ToBoolean(isServiceIncludedInInsurance) && insurancePercetnage != null)
			{
				accommodation.PatientShare = (1 - Convert.ToDouble(insurancePercetnage)) * Convert.ToDouble(servicePrice);
				accommodation.InsuranceShare = Convert.ToDouble(insurancePercetnage) * Convert.ToDouble(servicePrice);
				accommodation.IsInsuranceApplied = true;
			}
			else
			{
				accommodation.PatientShare = Convert.ToDouble(servicePrice);
				accommodation.InsuranceShare = 0;
				accommodation.IsInsuranceApplied = false;
			}

			InPatientRoomBed_cu roomBed =
				InPatientRoomBed_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(bedId)));
			if (roomBed == null)
				return null;
			InPatientRoom_cu room =
				InPatientRoom_cu.ItemsList.Find(
					item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(roomBed.InPatientRoom_CU_ID)));
			if (room == null)
				return null;
			InPatientRoomClassification_cu roomClassification =
				InPatientRoomClassification_cu.ItemsList.Find(
					item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(room.InPatientRoomClassification_CU_ID)));
			if (roomClassification == null)
				return null;

			accommodation.InPatientRoomBed_CU_ID = roomBed.ID;
			accommodation.InPatientRoom_CU_ID = room.ID;
			accommodation.InPatientRoomClassification_CU_ID = roomClassification.ID;

			accommodation.IsOnDuty = true;
			accommodation.IsSurchargeApplied = Convert.ToBoolean(isSurchargeApplied);
			accommodation.StartDate = Convert.ToDateTime(startDate);
			if (serviceDescription != null)
				accommodation.Description = serviceDescription.ToString();

			return accommodation;
		}

		public static InvoiceDetail CreateNew_InvoiceDetail_Surgery(InvoiceDetail parentInvoiceDetail, object serviceId,
			object servicePrice, bool useCustomPrice, object serviceCount, object serviceDate, object doctorId,
			object stationPointID, object stationPointStageID,
			object isServiceIncludedInInsurance, object insurancePercetnage, object isSurchargeApplied,
			object isTaxApplied, object serviceDescription)
		{
			return CreateNew_InvoiceDetail(null, serviceId, servicePrice, useCustomPrice, serviceCount, serviceDate,
				doctorId, stationPointID, stationPointStageID, isServiceIncludedInInsurance, insurancePercetnage,
				isSurchargeApplied, isTaxApplied,
				serviceDescription);
		}

		public static FinanceInvoiceDetail CreateNew_FinanceInvoiceDetail(FinanceInvoiceDetail parentFinanceInvoiceDetail,
			object inventoryItemID, object pricePerUnit, object unitMeasurmentID, object quantity, object date,
			object discountAmount, object discountTypeID, object description, object isSurchargeApplied, object surchargeAmount)
		{
			if (inventoryItemID == null || pricePerUnit == null || unitMeasurmentID == null || quantity == null || date == null)
				return null;

			FinanceInvoiceDetail financeInvoiceDetail = new FinanceInvoiceDetail();

			if (parentFinanceInvoiceDetail != null)
			{
				if (parentFinanceInvoiceDetail.FinanceInvoiceDetail2 == null)
					parentFinanceInvoiceDetail.FinanceInvoiceDetail2 = new FinanceInvoiceDetail();
				parentFinanceInvoiceDetail.FinanceInvoiceDetail1.Add(financeInvoiceDetail);
			}

			InventoryItem_cu inventoryItem =
				InventoryItem_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(inventoryItemID)));
			if (inventoryItem == null)
				return null;

			financeInvoiceDetail.InventoryItem_CU_ID = inventoryItem.ID;
			financeInvoiceDetail.InventoryHousing_CU_ID = inventoryItem.InventoryHousing_CU_ID;
			financeInvoiceDetail.PricePerUnit = Convert.ToDouble(pricePerUnit);
			financeInvoiceDetail.UnitMeasurment_CU_ID = Convert.ToInt32(unitMeasurmentID);
			financeInvoiceDetail.Quantity = Convert.ToDouble(quantity);
			financeInvoiceDetail.Date = Convert.ToDateTime(date);
			if (discountAmount != null)
				financeInvoiceDetail.DiscountAmount = Convert.ToDouble(discountAmount);
			else
				financeInvoiceDetail.DiscountAmount = 0;
			financeInvoiceDetail.IsSurchargeApplied = Convert.ToBoolean(isSurchargeApplied);
			if (surchargeAmount != null)
				financeInvoiceDetail.SurchargeAmount = Convert.ToDouble(surchargeAmount);
			if (description != null)
				financeInvoiceDetail.Description = description.ToString();
			financeInvoiceDetail.IsOnDuty = true;

			return financeInvoiceDetail;
		}

		public static InvoiceObject GetInvoiceFullTree(int invoiceID)
		{
			InvoiceObject obj = new InvoiceObject();
			obj.ActiveInvoice = DBCommon.GetEntity<Invoice>(invoiceID);
			if (obj.ActiveInvoice != null)
				obj.ActivePatient = obj.ActiveInvoice.PatientObject;

			return obj;
		}

		public static List<User_cu> GetUsers(List<User_UserGroup_cu> list)
		{
			if (list.Count == 0)
				return null;

			List<User_cu> usersList = new List<User_cu>();
			foreach (User_UserGroup_cu userBridge in list)
			{
				User_cu user =
					User_cu.ItemsList.Find(item => Convert.ToInt32(item.Person_CU_ID).Equals(Convert.ToInt32(userBridge.User_CU_ID)));
				if (user != null)
					usersList.Add(user);
			}

			return usersList;
		}

		public static bool RemoveUser(List<User_UserGroup_cu> list, User_cu user)
		{
			if (list.Count == 0 || user == null)
				return false;

			User_UserGroup_cu userBridge =
				list.Find(item => Convert.ToInt32(item.User_CU_ID).Equals(Convert.ToInt32(user.Person_CU_ID)));
			if (userBridge == null)
				return false;

			list.Remove(userBridge);

			return true;
		}

		public static bool UpdateAndSave_QueueManagerStatus(object queueManagerID, DB_QueueManagerStatus queueManagerStatus)
		{
			if (queueManagerID == null)
				return false;

			QueueManager queueManager = DBCommon.GetEntity<QueueManager>(Convert.ToInt32(queueManagerID));
			if (queueManager == null)
				return false;

			queueManager.QueueManagerStatus_P_ID = (int)queueManagerStatus;
			queueManager.DBCommonTransactionType = DB_CommonTransactionType.UpdateExisting;
			queueManager.SaveChanges();

			return true;
		}

		public static List<StationPointStage_cu> GetAllStationPointStages()
		{
			StationPoint_cu stationPoint =
				StationPoint_cu.ItemsList.Find(
					item => Convert.ToInt32(item.Station_P_ID).Equals(Convert.ToInt32(Private_StationPoint)));
			if (stationPoint == null)
				return null;

			return StationPointStage_cu.ItemsList.FindAll(
					item => Convert.ToInt32(item.StationPoint_CU_ID).Equals(Convert.ToInt32(stationPoint.ID)))
					.OrderBy(item => Convert.ToInt32(item.OrderIndex))
					.ToList();
		}

		public static List<StationPointStage_cu> GetOrganizationMachineStationPointStages(
			OrganizationMachine_cu organizationMachine, DB_Application application)
		{
			List<StationPoint_cu> list_StationPoint_cu = GetOrganizationMachineStationPoint(organizationMachine);

			if (list_StationPoint_cu == null || list_StationPoint_cu.Count == 0)
				return null;

			List<StationPointStage_cu> list_StationPointStage_cu = new List<StationPointStage_cu>();
			foreach (StationPoint_cu stationPointCu in list_StationPoint_cu)
			{
				List<StationPointStage_cu> tempStagesList = new List<StationPointStage_cu>();
				if (application.Equals(DB_Application.All))
					tempStagesList = StationPointStage_cu.ItemsList.FindAll(item =>
						Convert.ToInt32(item.StationPoint_CU_ID).Equals(Convert.ToInt32(stationPointCu.ID)));
				else
					tempStagesList = StationPointStage_cu.ItemsList.FindAll(item =>
						Convert.ToInt32(item.StationPoint_CU_ID).Equals(Convert.ToInt32(stationPointCu.ID)) &&
						Convert.ToInt32(item.ServingApplication_P_ID).Equals(Convert.ToInt32(application)));
				if (tempStagesList.Count > 0)
					list_StationPointStage_cu.AddRange(tempStagesList);
			}

			return list_StationPointStage_cu;
		}

		public static List<StationPoint_cu> GetOrganizationMachineStationPoint(OrganizationMachine_cu organizationMachine)
		{
			if (organizationMachine == null)
				return null;

			List<OrganizationMachine_StationPoint_cu> list_OrganizationMachine_StationPoint_cu =
				OrganizationMachine_StationPoint_cu.ItemsList.FindAll(item =>
					Convert.ToInt32(item.OrganizationMachine_CU_ID).Equals(Convert.ToInt32(organizationMachine.ID)));

			List<StationPoint_cu> list_StationPoint_cu = GetStationPointsList(list_OrganizationMachine_StationPoint_cu);
			return list_StationPoint_cu;
		}

		public static List<StationPointStage_cu> GetStationPointStagesList(StationPoint_cu stationPoint)
		{
			if (stationPoint == null)
				return null;

			List<StationPointStage_cu> list = new List<StationPointStage_cu>();

			foreach (StationPointStage_cu stage in StationPointStage_cu.ItemsList)
			{
				if(Convert.ToInt32(stage.StationPoint_CU_ID).Equals(Convert.ToInt32(stationPoint.ID)))
					list.Add(stage);
			}

			return list;
		}

		public static List<StationPoint_cu> GetStationPointsList(List<OrganizationMachine_StationPoint_cu> list)
		{
			List<StationPoint_cu> list_StationPoint_cu = new List<StationPoint_cu>();

			foreach (OrganizationMachine_StationPoint_cu organizationMachineStationPointCu in list)
			{
				StationPoint_cu stationPoint = StationPoint_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID)
						.Equals(Convert.ToInt32(organizationMachineStationPointCu.StationPoint_CU_ID)));
				if (stationPoint != null)
					list_StationPoint_cu.Add(stationPoint);
			}

			return list_StationPoint_cu;
		}

		public static List<Doctor_StationPointStage_cu> GetDoctor_StationPointStages(
			StationPointStage_cu stationPointStage)
		{
			List<Doctor_StationPointStage_cu> list = new List<Doctor_StationPointStage_cu>();

			if (Doctor_StationPointStage_cu.ItemsList == null || Doctor_StationPointStage_cu.ItemsList.Count == 0)
				return null;

			list = Doctor_StationPointStage_cu.ItemsList.FindAll(item =>
				Convert.ToInt32(item.StationPointStage_CU_ID).Equals(Convert.ToInt32(stationPointStage.ID)));

			return list;
		}

		public static PatientAttachment_cu CreateNewPatientAttachement(int patientID, string imageName,
			string imagePath, DB_ImageType imageType, object description, int userID)
		{
			PatientAttachment_cu patientAttachment = DBCommon.CreateNewDBEntity<PatientAttachment_cu>();
			patientAttachment.Patient_CU_ID = patientID;
			patientAttachment.ImageName = imageName;
			patientAttachment.ImagePath = imagePath;
			patientAttachment.ImageType_P_ID = Convert.ToInt32(imageType);
			patientAttachment.IsOnDuty = true;
			if (description != null)
				patientAttachment.Description = description.ToString();
			patientAttachment.InsertedBy = userID;

			return patientAttachment;
		}

		public static bool SavePatientAttachement(PatientAttachment_cu patientAttachement)
		{
			if (patientAttachement == null)
				return false;
			if (patientAttachement.SaveChanges())
			{
				PatientAttachment_cu.ItemsList.Add(patientAttachement);
				return true;
			}

			return false;
		}
	}
}
