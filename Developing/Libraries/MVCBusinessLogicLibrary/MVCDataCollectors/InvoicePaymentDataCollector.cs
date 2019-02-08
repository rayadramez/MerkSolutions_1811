using System;
using System.Collections.Generic;
using ApplicationConfiguration;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCDataCollectors
{
	public class InvoicePaymentDataCollector<TEntity> : AbstractDataCollector<TEntity>, IInvoicePaymentViewer
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

			ID = ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			((InvoicePayment)ActiveDBItem).DBCommonTransactionType = CommonTransactionType;

			switch (PaymentViewerType)
			{
				case PaymentViewerType.MedicalInvoicePayment:
					if (((Invoice)Invoice) != null)
						((InvoicePayment)ActiveDBItem).InvoiceID = ((Invoice)Invoice).ID;

					break;
				case PaymentViewerType.PatientDepositPayment:
					if (((Patient_cu)Patient) != null)
						((InvoicePayment)ActiveDBItem).Patient_CU_ID = ((Patient_cu)Patient).ID;
					break;
			}

			((InvoicePayment)ActiveDBItem).Date = Convert.ToDateTime(PaymentDate);
			((InvoicePayment)ActiveDBItem).Amount = Convert.ToBoolean(IsRefund)
				? Convert.ToDouble(PaymentAmount) * -1
				: Convert.ToDouble(PaymentAmount);
			if (Math.Abs(((InvoicePayment)ActiveDBItem).Amount) < 0.0001)
				return false;

			((InvoicePayment)ActiveDBItem).PaymentType_P_ID = Convert.ToInt32(PaymentTypeID);
			if (PaymentSerial != null)
				((InvoicePayment)ActiveDBItem).PaymentSerial = PaymentSerial.ToString();
			((InvoicePayment)ActiveDBItem).IsOnDuty = true;
			if (InvoicePaymentDescription != null)
				((InvoicePayment)ActiveDBItem).Description = InvoicePaymentDescription.ToString();

			if (UserID != null)
				((InvoicePayment)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			DB_PaymentType paymentType = (DB_PaymentType)PaymentTypeID;
			switch (paymentType)
			{
				case DB_PaymentType.CheckPayment:
					InvoicePayment_Check checkPayment = new InvoicePayment_Check();
					checkPayment.Bank_CU_ID = Convert.ToInt32(BankID_CheckPayment);
					if (BankAccountID_CheckPayment != null)
						checkPayment.BankAccoumt_CU_ID = Convert.ToInt32(BankAccountID_CheckPayment);
					if (CheckIssueDate_CheckPayment != null)
						checkPayment.IssueDate = Convert.ToDateTime(CheckIssueDate_CheckPayment);
					if (CheckExhangeDate_CheckPayment != null)
						checkPayment.ExchangeDate = Convert.ToDateTime(CheckExhangeDate_CheckPayment);
					if (CheckNumber_CheckPayment != null)
						checkPayment.CheckNumber = CheckNumber_CheckPayment.ToString();
					if (CheckDescription != null)
						checkPayment.Description = CheckDescription.ToString();
					((InvoicePayment)ActiveDBItem).InvoicePayment_Check = checkPayment;

					if (UserID != null)
						(((InvoicePayment)ActiveDBItem).InvoicePayment_Check).InsertedBy = Convert.ToInt32(UserID);

					break;
				case DB_PaymentType.VisaPayment:
					InvoicePayment_Visa visaPayment = new InvoicePayment_Visa();
					if (BankID_VisaPayment != null)
						visaPayment.Bank_CU_ID = Convert.ToInt32(BankID_VisaPayment);
					if (BankAccountID_VisaPayment != null)
						visaPayment.BankAccount_CU_ID = Convert.ToInt32(BankAccountID_VisaPayment);
					if (VisaCardNumber_VisaPayment != null)
						visaPayment.CreditCardNumber = VisaCardNumber_VisaPayment.ToString();
					if (VisaDescription != null)
						visaPayment.Description = VisaDescription.ToString();
					((InvoicePayment)ActiveDBItem).InvoicePayment_Visa = visaPayment;
					if (UserID != null)
						(((InvoicePayment)ActiveDBItem).InvoicePayment_Visa).InsertedBy = Convert.ToInt32(UserID);

					break;
			}

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { throw new System.NotImplementedException(); }
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
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).CommonTransactionType; }
			set { ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).CommonTransactionType = value; }
		}

		public override string HeaderTitle
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).HeaderTitle; }
		}

		public override string GridXML
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).GridXML; }
		}

		public override List<IViewer> RelatedViewers { get; set; }
		public override void ClearControls()
		{

		}

		public override void FillControls()
		{

		}

		public override object[] CollectSearchCriteria()
		{
			List<InvoicePayment> list = InvoicePayment.ItemsList;
			return list.ToArray();
		}

		public override bool ValidateBeforeSave(ref string message)
		{
			switch (PaymentViewerType)
			{
				case PaymentViewerType.MedicalInvoicePayment:
					if (Invoice == null)
					{
						MessageToView = "يجــب إختيـــار الفـاتــــورة";
						return false;
					}

					break;
				case PaymentViewerType.PatientDepositPayment:
					if (Patient == null)
					{
						MessageToView = "يجــب إختيـــار المريـــض";
						return false;
					}

					if (ServiceID == null)
					{
						MessageToView = "يجــب إختيـــار الخدمـــة";
						return false;
					}
					break;
			}

			if (PaymentDate == null)
			{
				MessageToView = "يجــب إختيــار تــاريـــخ الـدفــــع";
				return false;
			}

			if (PaymentAmount == null)
			{
				MessageToView = "يجــب كتـابـــة القيمـــــة";
				return false;
			}

			return true;
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<InvoicePayment>();
				((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			return ((InvoicePayment)ActiveCollector.ActiveDBItem).SaveChanges();
		}

		public override bool AfterSave()
		{
			if ((InvoicePayment)ActiveDBItem == null)
				return false;

			switch (PaymentViewerType)
			{
				case PaymentViewerType.MedicalInvoicePayment:
					Invoice invoice = DBCommon.GetEntity<Invoice>(Convert.ToInt32(((InvoicePayment)ActiveDBItem).InvoiceID));
					if (invoice != null)
					{
						invoice.IsPaymentsEnough = Convert.ToBoolean(IsPaymentEnough);

						InvoiceShare invoiceShare = DBCommon.GetEntity<InvoiceShare>(Convert.ToInt32(((InvoicePayment)ActiveDBItem).InvoiceID));
						invoiceShare.TotalPayment = Convert.ToBoolean(IsRefund)
							? Convert.ToDouble(invoiceShare.TotalPayment) + Convert.ToDouble(PaymentAmount) * -1
							: Convert.ToDouble(invoiceShare.TotalPayment) + Convert.ToDouble(PaymentAmount);
						if (Math.Abs(Convert.ToDouble(invoiceShare.TotalPayment) - Convert.ToDouble(invoiceShare.TotalRequestedAmount)) <
						    0.0001)
							invoice.IsPaymentsEnough = true;

						invoice.DBCommonTransactionType = DB_CommonTransactionType.UpdateExisting;
						invoiceShare.DBCommonTransactionType = DB_CommonTransactionType.UpdateExisting;
						invoice.InvoiceShare = invoiceShare;
						return invoice.SaveChanges();
					}
					break;
				case PaymentViewerType.PatientDepositPayment:
					PatientDepositeBalance patientBalance = DBCommon.CreateNewDBEntity<PatientDepositeBalance>();
					patientBalance.Service_CU_ID = Convert.ToInt32(ServiceID);
					patientBalance.Patient_CU_ID = Convert.ToInt32(((Patient_cu)Patient).ID);
					patientBalance.Date = Convert.ToDateTime(PaymentDate);
					patientBalance.Blance = Convert.ToDouble(PaymentAmount);
					patientBalance.Factor = 1;
					patientBalance.IsOnDuty = true;
					if (UserID != null)
						patientBalance.InsertedBy = Convert.ToInt32(UserID);
					return patientBalance.SaveChanges();
					break;
			}

			return false;
		}

		#endregion

		#region Implementation of IInvoicePaymentViewer

		public PaymentViewerType PaymentViewerType
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).PaymentViewerType; }
			set { ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).PaymentViewerType = value; }
		}

		public object Invoice
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).Invoice; }
			set { ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).Invoice = value; }
		}

		public object Patient
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).Patient; }
		}

		public object ServiceID
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).ServiceID; }
			set { ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).ServiceID = value; }
		}

		public object IsRefund
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).IsRefund; }
			set { ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).IsRefund = value; }
		}

		public object IsPaymentEnough
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).IsPaymentEnough; }
			set { ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).IsPaymentEnough = value; }
		}

		public object PaymentDate
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).PaymentDate; }
			set { ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).PaymentDate = value; }
		}

		public object PaymentSerial
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).PaymentSerial; }
			set { ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).PaymentSerial = value; }
		}

		public object InvoicePaymentDescription
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).InvoicePaymentDescription; }
			set { ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).InvoicePaymentDescription = value; }
		}

		public object TotalRequired
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).TotalRequired; }
			set { ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).TotalRequired = value; }
		}

		public object TotalPayments
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).TotalPayments; }
			set { ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).TotalPayments = value; }
		}

		public object Remainder
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).Remainder; }
			set { ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).Remainder = value; }
		}

		public object PaymentAmount
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).PaymentAmount; }
			set { ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).PaymentAmount = value; }
		}

		public object PaymentTypeID
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).PaymentTypeID; }
			set { ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).PaymentTypeID = value; }
		}

		public object BankID_CheckPayment
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).BankID_CheckPayment; }
			set { ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).BankID_CheckPayment = value; }
		}

		public object BankAccountID_CheckPayment
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).BankAccountID_CheckPayment; }
			set { ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).BankAccountID_CheckPayment = value; }
		}

		public object CheckIssueDate_CheckPayment
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).CheckIssueDate_CheckPayment; }
			set { ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).CheckIssueDate_CheckPayment = value; }
		}

		public object CheckExhangeDate_CheckPayment
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).CheckExhangeDate_CheckPayment; }
			set { ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).CheckExhangeDate_CheckPayment = value; }
		}

		public object CheckDescription
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).CheckDescription; }
			set { ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).CheckDescription = value; }
		}

		public object CheckNumber_CheckPayment
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).CheckNumber_CheckPayment; }
			set { ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).CheckNumber_CheckPayment = value; }
		}

		public object BankID_VisaPayment
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).BankID_VisaPayment; }
			set { ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).BankID_VisaPayment = value; }
		}

		public object BankAccountID_VisaPayment
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).BankAccountID_VisaPayment; }
			set { ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).BankAccountID_VisaPayment = value; }
		}

		public object VisaCardNumber_VisaPayment
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).VisaCardNumber_VisaPayment; }
			set { ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).VisaCardNumber_VisaPayment = value; }
		}

		public object VisaDescription
		{
			get { return ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).VisaDescription; }
			set { ((IInvoicePaymentViewer)ActiveCollector.ActiveViewer).VisaDescription = value; }
		}

		#endregion
	}
}
