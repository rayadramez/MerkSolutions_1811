using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationConfiguration;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCDataCollectors
{
	public class MedicalAdmissionInvoiceCreation_DataCollector<TEntity> :
		AbstractDataCollector<TEntity>, IPatientInvoiceCreation
		where TEntity : DBCommon, IDBCommon, new()
	{
		#region Overrides of AbstractDataCollector<TEntity>

		public override AbstractDataCollector<TEntity> ActiveCollector { get; set; }
		public override AbstractDataCollector<TEntity> ParentActiveCollector { get; set; }
		public override bool Collect(AbstractDataCollector<TEntity> collector)
		{
			if (collector == null)
				return false;

			ActiveCollector = collector;

			ID = ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			((Invoice)ActiveDBItem).DBCommonTransactionType = CommonTransactionType;
			((Invoice)ActiveDBItem).InvoiceType_P_ID = (int)InvoiceTypeID;
			((Invoice)ActiveDBItem).InvoicePaymentType_P_ID = (int)DB_InvoicePaymentType.CashInvoice;
			((Invoice)ActiveDBItem).InvoiceCreationDate = Convert.ToDateTime(InvoiceCreationDate);
			((Invoice)ActiveDBItem).Patient_CU_ID = Convert.ToInt32(PatientID);
			((Invoice)ActiveDBItem).IsPrinted = false;
			((Invoice)ActiveDBItem).InvoiceSerial = null;
			((Invoice)ActiveDBItem).PrintingDate = null;
			((Invoice)ActiveDBItem).IsPaymentsEnough = Convert.ToBoolean(IsPaymentEnough);
			((Invoice)ActiveDBItem).Description = null;
			((Invoice)ActiveDBItem).IsOnDuty = true;
			switch ((DB_InvoiceType)InvoiceTypeID)
			{
				case DB_InvoiceType.OutPatientNotPrivate:
				case DB_InvoiceType.OutPatientPrivate:
					((Invoice)ActiveDBItem).IsFinanciallyReviewed = true;
					break;
				case DB_InvoiceType.InPatientPrivate:
				case DB_InvoiceType.InPatientNotPrivate:
					((Invoice)ActiveDBItem).IsFinanciallyReviewed = false;
					break;
			}
			((Invoice)ActiveDBItem).IsMedicallyDone = false;
			((Invoice)ActiveDBItem).IsFinanciallyCompleted = false;
			((Invoice)ActiveDBItem).IsCancelled = false;
			((Invoice)ActiveDBItem).CancelledBy = null;
			((Invoice)ActiveDBItem).CancellationDate = null;

			((Invoice)ActiveDBItem).IsSuspended = false;

			((Invoice)ActiveDBItem).InvoiceShare = new InvoiceShare();
			if (AccummulativeServicesPatientShare != null)
				((Invoice)ActiveDBItem).InvoiceShare.TotalPatientShare = Convert.ToDouble(AccummulativeServicesPatientShare);
			if (AccummulativeServicesInsuranceShare != null)
				((Invoice)ActiveDBItem).InvoiceShare.TotalInsuranceShare = Convert.ToDouble(AccummulativeServicesInsuranceShare);
			((Invoice)ActiveDBItem).InvoiceShare.TotalPayment = 0;

			((Invoice)ActiveDBItem).InvoiceShare.IsInsuranceApplied = Convert.ToBoolean(IsInsuranceAppliedToInvoice);
			if (InsuranceCarrierID != null)
				((Invoice)ActiveDBItem).InvoiceShare.InsuranceCarrier_CU_ID = Convert.ToInt32(InsuranceCarrierID);
			if (InsuranceLevelID != null)
				((Invoice)ActiveDBItem).InvoiceShare.InsuanceLevel_CU_ID = Convert.ToInt32(InsuranceLevelID);
			if (InsurancePercentage != null)
				((Invoice)ActiveDBItem).InvoiceShare.InsurancePercentageApplied = Convert.ToDouble(InsurancePercentage) / 100;
			if (InsurancePatientMaxAmount != null)
				((Invoice)ActiveDBItem).InvoiceShare.InsurancePatientMaxAmount = Convert.ToDouble(InsurancePatientMaxAmount);
			((Invoice)ActiveDBItem).InvoiceShare.IsSurchargeApplied = Convert.ToBoolean(IsSurchargeAppliedToInvoice);
			if (SurchargeAmount != null)
				((Invoice)ActiveDBItem).InvoiceShare.TotalSurchargeAccummulativePercentage = Convert.ToDouble(SurchargeAmount);

			((Invoice)ActiveDBItem).InvoiceShare.IsStampApplied = Convert.ToBoolean(IsStampApplied);
			if (StampAmount != null)
				((Invoice)ActiveDBItem).InvoiceShare.TotalStampAmount = Convert.ToDouble(StampAmount);

			((Invoice)ActiveDBItem).InvoiceShare.IsSurchargeDistributedToInsurancePercentage = true;
			((Invoice)ActiveDBItem).InvoiceShare.IsSurchargeAppliedToPatientOnly = false;

			if (UserID != null)
				((Invoice)ActiveDBItem).InvoiceShare.InsertedBy = Convert.ToInt32(UserID);

			((Invoice)ActiveDBItem).InvoiceShare.IsStampDistributedToInsurancePercentage = true;
			((Invoice)ActiveDBItem).InvoiceShare.IsStampAppliedToPatientOnly = false;

			if (Convert.ToBoolean(IsPaymentAttached) && AmountPaid != null &&
				Convert.ToDouble(AmountPaid) >= 0)
			{
				InvoicePayment invoicePayment = DBCommon.CreateNewDBEntity<InvoicePayment>();
				invoicePayment.Amount = ((Invoice)ActiveDBItem).InvoiceShare.TotalPayment = Convert.ToDouble(AmountPaid);
				if (InvoiceCreationDate != null)
					invoicePayment.Date = Convert.ToDateTime(InvoiceCreationDate);
				invoicePayment.PaymentType_P_ID = (int)DB_PaymentType.CashPayment;
				invoicePayment.PaymentSerial = "123";
				invoicePayment.IsOnDuty = true;
				invoicePayment.Description = ((Invoice)ActiveDBItem).Description;
				invoicePayment.IsRemainingReturned = Convert.ToBoolean(IsRemainingReturned);
				if (UserID != null)
					invoicePayment.InsertedBy = Convert.ToInt32(UserID);
				((Invoice)ActiveDBItem).InvoicePayments.Add(invoicePayment);
			}

			if (UserID != null)
				((Invoice)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			if (Grid_InvoiceDetails != null && Grid_InvoiceDetails.Count > 0)
				switch (CommonTransactionType)
				{
					case DB_CommonTransactionType.CreateNew:
						foreach (InvoiceDetail invoiceDetail in Grid_InvoiceDetails)
						{
							if (UserID != null)
								invoiceDetail.InsertedBy = Convert.ToInt32(UserID);
							((Invoice)ActiveDBItem).InvoiceDetails.Add(invoiceDetail);
						}

						break;
					case DB_CommonTransactionType.UpdateExisting:
						if (Grid_InvoiceDetails.Count >= ((Invoice)ActiveDBItem).InvoiceDetails.Count)
							foreach (InvoiceDetail invoiceDetail in Grid_InvoiceDetails)
							{
								if (UserID != null)
									invoiceDetail.InsertedBy = Convert.ToInt32(UserID);
								if (((Invoice)ActiveDBItem).InvoiceDetails.ToList()
									.Exists(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(invoiceDetail.ID))))
									continue;
								if (UserID != null)
									invoiceDetail.InsertedBy = Convert.ToInt32(UserID);
								invoiceDetail.InvoiceID = ((Invoice)ActiveDBItem).ID;
								invoiceDetail.SaveChanges();
							}
						else
							foreach (InvoiceDetail invoiceDetail in ((Invoice)ActiveDBItem).InvoiceDetails)
							{
								if (Grid_InvoiceDetails.ToList()
									.Exists(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(invoiceDetail.ID))))
									continue;
								invoiceDetail.IsOnDuty = false;
								invoiceDetail.InvoiceID = ((Invoice)ActiveDBItem).ID;
								invoiceDetail.DBCommonTransactionType = DB_CommonTransactionType.UpdateExisting;
								invoiceDetail.SaveChanges();
							}

						break;
				}
			else
				return false;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).ViewerID; }
		}

		public override object UserID
		{
			get
			{
				if (ApplicationStaticConfiguration.ActiveLoginUser != null)
					return ApplicationStaticConfiguration.ActiveLoginUser.Person_CU_ID;
				return null;
			}
		}

		public override object EditingDate
		{
			get { return DateTime.Now; }
		}

		public override object IsOnDUty { get; set; }

		public override DB_CommonTransactionType CommonTransactionType
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).CommonTransactionType; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).CommonTransactionType = value; }
		}

		public override string HeaderTitle
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).HeaderTitle; }
		}

		public override string GridXML
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).GridXML; }
		}

		public override List<IViewer> RelatedViewers { get; set; }
		public override void ClearControls()
		{

		}

		public override void FillControls()
		{

		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<Invoice>();
				((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;
			switch (CommonTransactionType)
			{
				case DB_CommonTransactionType.CreateNew:
					return ((Invoice)ActiveCollector.ActiveDBItem).SaveChanges();
				case DB_CommonTransactionType.UpdateExisting:
					InvoiceShare invoiceShare = ((Invoice)ActiveDBItem).InvoiceShare;
					invoiceShare.DBCommonTransactionType = commonTransactionType;
					invoiceShare.InvoiceID = ((Invoice)ActiveDBItem).ID;
					invoiceShare.SaveChanges();
					if (((Invoice)ActiveDBItem).InvoicePayments != null
						&& ((Invoice)ActiveDBItem).InvoicePayments.Count > 0)
					{
						List<InvoicePayment> invoicePayments = ((Invoice)ActiveDBItem).InvoicePayments.ToList();

					}

					return true;
			}

			return false;
		}

		public override bool AfterSave()
		{
			if ((Invoice)ActiveDBItem == null)
				return false;

			if (Grid_InvoiceDetails != null && (Grid_InvoiceDetails).Count > 0)
				switch (CommonTransactionType)
				{
					case DB_CommonTransactionType.CreateNew:
						foreach (InvoiceDetail invoiceDetail in Grid_InvoiceDetails)
						{
							QueueManager manager =
								MerkDBBusinessLogicEngine.CreateNewQueueManager((Invoice) ActiveDBItem, invoiceDetail);
							if (manager != null)
								manager.SaveChanges();
						}
						break;
					case DB_CommonTransactionType.UpdateExisting:
						if (Grid_InvoiceDetails.Count >= ((Invoice)ActiveDBItem).InvoiceDetails.Count)
							foreach (InvoiceDetail invoiceDetail in Grid_InvoiceDetails)
							{
								if (((Invoice)ActiveDBItem).List_InvoiceDetails != null && ((Invoice)ActiveDBItem).InvoiceDetails.Count > 0)
									if (((Invoice) ActiveDBItem).List_InvoiceDetails.ToList()
										.Exists(item => Convert.ToInt32(item.ID)
											        .Equals(Convert.ToInt32(invoiceDetail.ID))))
										continue;
								QueueManager manager = MerkDBBusinessLogicEngine.CreateNewQueueManager((Invoice)ActiveDBItem, invoiceDetail);
								if (manager != null)
									manager.SaveChanges();
							}
						else
							foreach (InvoiceDetail invoiceDetail in ((Invoice)ActiveDBItem).InvoiceDetails)
							{
								if (Grid_InvoiceDetails.ToList()
									.Exists(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(invoiceDetail.ID))))
									continue;
								QueueManager queueManager = invoiceDetail.QueueManagers.ToList()
									.Find(item => Convert.ToInt32(item.InvoiceDetailID)
										      .Equals(Convert.ToInt32(invoiceDetail.ID)));
								if(queueManager == null)
									continue;
								queueManager.DBCommonTransactionType = DB_CommonTransactionType.UpdateExisting;
								queueManager.IsOnDuty = false;
								queueManager.SaveChanges();
							}

						break;
				}

			return true;
		}

		public override void Edit(IDBCommon entity)
		{
			if (entity == null)
				return;
			ActiveDBItem = entity;

			InvoiceTypeID = ((Invoice)ActiveDBItem).InvoiceType_P_ID;
			//InvoiceDiscount = ((Invoice)ActiveDBItem).Discou;
			IsInsuranceAppliedToInvoice = ((Invoice)ActiveDBItem).IsInsuranceApplied_InvoiceItem;
			IsSurchargeAppliedToInvoice = ((Invoice)ActiveDBItem).IsSurchargeApplied_InvoiceItem;
			if (((Invoice)ActiveDBItem).InsuranceCarrierObject != null)
				InsuranceCarrierID = ((Invoice)ActiveDBItem).InsuranceCarrierObject.ID;
			if (((Invoice)ActiveDBItem).InsuranceLevelObject != null)
				InsuranceLevelID = ((Invoice)ActiveDBItem).InsuranceLevelObject.ID;
			InsurancePercentage = ((Invoice)ActiveDBItem).InsurancePercentage;
			if (((Invoice)ActiveDBItem).InvoiceShareObject.InsurancePatientMaxAmount != null)
				InsurancePatientMaxAmount = ((Invoice)ActiveDBItem).InvoiceShareObject.InsurancePatientMaxAmount;
			if (((Invoice)ActiveDBItem).InvoiceShareObject.InsuranceMaxAmount != null)
				InsuranceMaxAmount = ((Invoice)ActiveDBItem).InvoiceShareObject.InsuranceMaxAmount;
			if (((Invoice)ActiveDBItem).InvoiceShareObject.InsuranceMaxAmount != null)
				InsuranceMaxAmount = ((Invoice)ActiveDBItem).InvoiceShareObject.InsuranceMaxAmount;
			InvoiceTypeID = ((Invoice)ActiveDBItem).InvoiceType_P_ID;

			AccummulatedServicesPrice = ((Invoice)ActiveDBItem).TotalServicePrice_AfterAddsOn_InvoiceItem;
			AccummulativeServicesPatientShare = ((Invoice)ActiveDBItem).PatientShare_AfterAddsOn_InvoiceItem;
			IsPaymentEnough = ((Invoice)ActiveDBItem).IsPaymentsEnough;
			if (((Invoice)ActiveDBItem).List_InvoiceDetails != null
				&& ((Invoice)ActiveDBItem).List_InvoiceDetails.Count > 0)
			{
				DoctorID = ((Invoice)ActiveDBItem).List_InvoiceDetails[0].Doctor_CU_ID;
				Grid_InvoiceDetails = ((Invoice)ActiveDBItem).List_InvoiceDetails;
			}
		}

		#endregion

		#region Implementation of IPatientService

		public object InvoiceTypeID
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).InvoiceTypeID; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).InvoiceTypeID = value; }
		}

		public object InvoiceDiscount
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).InvoiceDiscount; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).InvoiceDiscount = value; }
		}

		public object IsInsuranceAppliedToInvoice
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).IsInsuranceAppliedToInvoice; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).IsInsuranceAppliedToInvoice = value; }
		}

		public object IsSurchargeAppliedToInvoice
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).IsSurchargeAppliedToInvoice; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).IsSurchargeAppliedToInvoice = value; }
		}

		public object InsuranceCarrierID
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).InsuranceCarrierID; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).InsuranceCarrierID = value; }
		}

		public object InsuranceLevelID
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).InsuranceLevelID; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).InsuranceLevelID = value; }
		}

		public object InsurancePercentage
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).InsurancePercentage; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).InsurancePercentage = value; }
		}

		public object InsurancePatientMaxAmount
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).InsurancePatientMaxAmount; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).InsurancePatientMaxAmount = value; }
		}

		public object InsuranceMaxAmount
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).InsuranceMaxAmount; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).InsuranceMaxAmount = value; }
		}

		public object ServiceDate
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).ServiceDate; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).ServiceDate = value; }
		}

		public object AccummulatedServicesPrice
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).AccummulatedServicesPrice; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).AccummulatedServicesPrice = value; }
		}

		public object AccummulativeServicesPatientShare
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).AccummulativeServicesPatientShare; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).AccummulativeServicesPatientShare = value; }
		}

		public object AccummulativeServicesInsuranceShare
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).AccummulativeServicesInsuranceShare; }
		}

		public object AmountPaid
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).AmountPaid; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).AmountPaid = value; }
		}

		public object IsPaymentAttached
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).IsPaymentAttached; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).IsPaymentAttached = value; }
		}

		public object IsRemainingReturned
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).IsRemainingReturned; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).IsRemainingReturned = value; }
		}

		public object IsPaymentEnough
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).IsPaymentEnough; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).IsPaymentEnough = value; }
		}

		public object IsStampApplied
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).IsStampApplied; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).IsStampApplied = value; }
		}

		public object StampAmount
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).StampAmount; }
		}

		public object SurchargeAmount
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).SurchargeAmount; }
		}

		public object StationPointID
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).StationPointID; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).StationPointID = value; }
		}

		public object StationPointStageID
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).StationPointStageID; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).StationPointStageID = value; }
		}


		public List<InvoiceDetail> Grid_InvoiceDetails
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).Grid_InvoiceDetails; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).Grid_InvoiceDetails = value; }
		}

		public object ServiceCategoryID
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).ServiceCategoryID; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).ServiceCategoryID = value; }
		}

		public object ServiceID
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).ServiceID; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).ServiceID = value; }
		}

		public object InPatientRoomClassificationID
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).InPatientRoomClassificationID; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).InPatientRoomClassificationID = value; }
		}

		public object InPatientRoomID
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).InPatientRoomID; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).InPatientRoomID = value; }
		}

		public object InPatientRoomBedID
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).InPatientRoomBedID; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).InPatientRoomBedID = value; }
		}

		public object AdmissionDate
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).AdmissionDate; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).AdmissionDate = value; }
		}

		public object CompanionsNumbers
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).CompanionsNumbers; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).CompanionsNumbers = value; }
		}

		public object DoctorID
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).DoctorID; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).DoctorID = value; }
		}

		public object ServicePrice
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).ServicePrice; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).ServicePrice = value; }
		}

		public object ServiceDiscount
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).ServiceDiscount; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).ServiceDiscount = value; }
		}

		public object ServiceDescription
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).ServiceDescription; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).ServiceDescription = value; }
		}

		public object IsSurchargeAppliedToService
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).IsSurchargeAppliedToService; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).IsSurchargeAppliedToService = value; }
		}

		public object IsInsuranceAppliedToService
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).IsInsuranceAppliedToInvoice; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).IsInsuranceAppliedToInvoice = value; }
		}

		public object ServiceType
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).ServiceType; }
		}

		public object LabServices
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).LabServices; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).LabServices = value; }
		}

		#endregion

		#region Implementation of IPatientInvoiceCreation

		public object InvoiceCreationDate
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).InvoiceCreationDate; }
			set { ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).InvoiceCreationDate = value; }
		}

		public object PatientID
		{
			get { return ((IPatientInvoiceCreation)ActiveCollector.ActiveViewer).PatientID; }
		}

		#endregion
	}
}
