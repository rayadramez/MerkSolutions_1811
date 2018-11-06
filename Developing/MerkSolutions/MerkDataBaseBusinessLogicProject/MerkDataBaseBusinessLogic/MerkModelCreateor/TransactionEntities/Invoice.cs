using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class Invoice : DBCommon, IDBCommon, IInvoiceItem
	{
		MerkFinanceEntities _context;
		private static List<Invoice> _items;
		public static List<Invoice> ItemsList = new List<Invoice>();

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
			
			ItemsList = DBContext_External.Invoices.ToList();
			return true;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.Invoice; }
		}

		public List<string> ChildrenItemsList
		{
			get
			{
				List<string> list = new List<string>();
				list.Add("InvoiceShare");
				list.Add("InvoiceDetail");
				list.Add("InvoicePayment");
				return list;
			}
		}

		public string EntityName
		{
			get { return "Invoice"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context,int id)
		{
			return context.Invoices.FirstOrDefault(item => item.ID.Equals(id));
		}

		public override IDBCommon RegenerateEntityObject(IDBCommon entity)
		{
			if (entity == null)
				return null;

			Invoice regeneratedInvoice = DBCommon.GetEntity<Invoice>(((Invoice)entity).ID);
			//regeneratedInvoice.List_InvoiceDetails = ((Invoice)entity).InvoiceDetails.ToList();
			//regeneratedInvoice.InvoiceShareObject = ((Invoice)entity).InvoiceShare;
			//regeneratedInvoice.List_InvoicePayments = ((Invoice)entity).InvoicePayments.ToList();

			return regeneratedInvoice;
		}

		public override IList ReGenerateList()
		{
			LoadItemsList();

			return ItemsList;
		}

		private Patient_cu _patientObject;
		public Patient_cu PatientObject
		{
			get { return _patientObject ?? (_patientObject = GetEntity<Patient_cu>(Convert.ToInt32(Patient_CU_ID))); }
			set { _patientObject = value; }
		}

		private List<InvoiceDetail> _invoiceDetailsList;
		public List<InvoiceDetail> List_InvoiceDetails
		{
			get { return _invoiceDetailsList ?? (_invoiceDetailsList = InvoiceDetails.ToList().FindAll(item => item.IsOnDuty)); }
			set { _invoiceDetailsList = value; }
		}

		private List<InvoicePayment> _invoicePaymentsList;
		public List<InvoicePayment> List_InvoicePayments
		{
			get { return _invoicePaymentsList ?? (_invoicePaymentsList = InvoicePayments.ToList()); }
			set { _invoicePaymentsList = value; }
		}

		private InvoiceShare _invoiceShareObject;
		public InvoiceShare InvoiceShareObject
		{
			get
			{
				if (_invoiceShareObject == null)
					_invoiceShareObject = InvoiceShare;

				return _invoiceShareObject;
			}
			set { _invoiceShareObject = value; }
		}

		private List<InvoiceDiscount> _list_invoiceDiscount;
		public List<InvoiceDiscount> List_InvoiceDiscounts
		{
			get { return _list_invoiceDiscount ?? (_list_invoiceDiscount = InvoiceDiscounts.ToList()); }
			set { _list_invoiceDiscount = value; }
		}

		public double CalculatedTotal_Payments
		{
			get { return List_InvoicePayments.Sum(item => Convert.ToDouble(item.Amount)); }
		}

		public bool HasInsuranceDetailsSetted
		{
			get
			{
				if (InvoiceShareObject == null)
					return false;

				return InvoiceShareObject.IsInsuranceApplied && InvoiceShareObject.InsuranceCarrier_CU_ID != null &&
				       InvoiceShareObject.InsuanceLevel_CU_ID != null && InvoiceShareObject.InsurancePercentageApplied != null;
			}
		}

		public Doctor_cu DoctorObject
		{
			get
			{
				Doctor_cu doctor = null;
				if (List_InvoiceDetails != null && List_InvoiceDetails.Count > 0)
					doctor =
						Doctor_cu.ItemsList.Find(
							item => Convert.ToInt32(item.Person_CU_ID).Equals(Convert.ToInt32(List_InvoiceDetails[0].Doctor_CU_ID)));
				return doctor;
			}
		}

		public string DoctorName
		{
			get
			{
				if (DoctorObject == null)
					return string.Empty;

				return DoctorObject.Name_P;
			}
		}

		public InsuranceCarrier_cu InsuranceCarrierObject
		{
			get
			{
				if (InvoiceShareObject == null || InvoiceShareObject.InsuranceCarrier_CU_ID == null)
					return null;

				InsuranceCarrier_cu insuranceCarrier =
					InsuranceCarrier_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(InvoiceShareObject.InsuranceCarrier_CU_ID)));
				return insuranceCarrier;
			}
		}

		public string InsuranceCarrierName
		{
			get
			{
				if (InsuranceCarrierObject == null)
					return string.Empty;

				return InsuranceCarrierObject.Name_P;
			}
		}

		public InsuranceLevel_cu InsuranceLevelObject
		{
			get
			{
				if (InvoiceShareObject == null || InvoiceShareObject.InsuanceLevel_CU_ID == null)
					return null;

				InsuranceLevel_cu insuranceLevel =
					InsuranceLevel_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(InvoiceShareObject.InsuanceLevel_CU_ID)));
				return insuranceLevel;
			}
		}

		public string InsuranceLevelName
		{
			get
			{
				if (InsuranceLevelObject == null)
					return string.Empty;

				return InsuranceLevelObject.Name_P;
			}
		}

		public double InsurancePercentage
		{
			get
			{
				double percentage = 0;
				if (InvoiceShareObject == null || InvoiceShareObject.InsurancePercentageApplied == null)
					return percentage;
				percentage = Convert.ToDouble(InvoiceShareObject.InsurancePercentageApplied);
				return percentage;
			}
		}

		#region Implementation of IInvoiceItem

		public double PatientShare_BeforeAddsOn_InvoiceItem
		{
			get { return Math.Round(FinancialBusinessLogicLibrary.CalculateInvoice_Totals(this, PriceType.PatientShare), 2); }
			set
			{
				if (InvoiceShareObject != null) 
					InvoiceShareObject.TotalPatientShare = value;
			}
		}

		public double InsuranceShare_BeforeAddsOn_InvoiceItem
		{
			get { return Math.Round(FinancialBusinessLogicLibrary.CalculateInvoice_Totals(this, PriceType.InsuranceShare), 2); }
			set
			{
				if (InvoiceShareObject != null)
					InvoiceShareObject.TotalInsuranceShare = value;
			}
		}

		public double TotalServicePrice_BeforeAddsOn_InvoiceItem
		{
			get
			{
				return
					Math.Round(
						FinancialBusinessLogicLibrary.CalculateInvoice_Totals(this, PriceType.TotalServicePriceBeforeSurcharges), 2);
			}
			set
			{
				if (InvoiceShareObject != null)
				{
					InvoiceShareObject.TotalPatientShare = Convert.ToDouble(value) - Convert.ToDouble(InsuranceShare_BeforeAddsOn_InvoiceItem);
					InvoiceShareObject.TotalInsuranceShare = Convert.ToDouble(value) - Convert.ToDouble(PatientShare_BeforeAddsOn_InvoiceItem);
				}
			}
		}

		public double PatientShareDiscount_InvoiceItem { get; private set; }

		public double SurchargeAmount_PatientShare_InvoiceItem
		{
			get
			{
				return
					Math.Round(FinancialBusinessLogicLibrary.CalculateInvoice_Totals(this, PriceType.SurchargeAmount_PatientShare), 2);
			}
		}

		public double SurchargeAmount_InsuranceShare_InvoiceItem
		{
			get
			{
				return
					Math.Round(FinancialBusinessLogicLibrary.CalculateInvoice_Totals(this, PriceType.SurchargeAmount_InsuranceShare), 2);
			}
		}

		public double TotalSurchargeAmount_InvoiceItem
		{
			get
			{
				return Math.Round(FinancialBusinessLogicLibrary.CalculateInvoice_Totals(this, PriceType.TotalSurchargeAmount), 2);
			}
			set
			{
				if (InvoiceShareObject != null) 
					InvoiceShareObject.TotalSurchargeAccummulativePercentage = value;
			}
		}

		public double StampAmount_PatientShare_InvoiceItem
		{
			get { return Math.Round(FinancialBusinessLogicLibrary.GetStampAmount(this, true)); }
		}

		public double StampAmount_InsuranceShare_InvoiceItem
		{
			get { return Math.Round(FinancialBusinessLogicLibrary.GetStampAmount(this, false)); }
		}

		public double TotalStampAmount_InvoiceItem
		{
			get { return StampAmount_PatientShare_InvoiceItem + StampAmount_InsuranceShare_InvoiceItem; }
		}

		public double PatientShare_AfterAddsOn_InvoiceItem
		{
			get
			{
				return PatientShare_BeforeAddsOn_InvoiceItem + SurchargeAmount_PatientShare_InvoiceItem +
				       StampAmount_PatientShare_InvoiceItem;
			}
		}

		public double InsuranceShare_AfterAddsOn_InvoiceItem
		{
			get
			{
				return InsuranceShare_BeforeAddsOn_InvoiceItem + SurchargeAmount_InsuranceShare_InvoiceItem +
				       StampAmount_InsuranceShare_InvoiceItem;
			}
		}

		public double TotalServicePrice_AfterAddsOn_InvoiceItem
		{
			get
			{
				return TotalServicePrice_BeforeAddsOn_InvoiceItem + TotalSurchargeAmount_InvoiceItem + TotalStampAmount_InvoiceItem;
			}
		}

		public bool IsInsuranceApplied_InvoiceItem
		{
			get
			{
				if (InvoiceShareObject != null)
					return InvoiceShareObject.IsInsuranceApplied;
				return false;
			}
			set
			{
				if (InvoiceShareObject != null) 
					InvoiceShareObject.IsInsuranceApplied = value;
			}
		}

		public bool IsSurchargeApplied_InvoiceItem
		{
			get
			{
				if (InvoiceShareObject != null) 
					return InvoiceShareObject.IsSurchargeApplied;
				return false;
			}
			set
			{
				if (InvoiceShareObject != null)
					InvoiceShareObject.IsSurchargeApplied = value;
			}
		}

		public bool IsStampApplied_InvoiceItem
		{
			get
			{
				if (InvoiceShareObject != null)
					return InvoiceShareObject.IsStampApplied;
				return false;
			}
			set
			{
				if (InvoiceShareObject != null)
					InvoiceShareObject.IsStampApplied = value;
			}
		}

		public List<IInvoiceItem> List_InvoiceItems
		{
			get
			{
				List<IInvoiceItem> list = new List<IInvoiceItem>();
				DB_InvoiceType invoiceType = (DB_InvoiceType) InvoiceType_P_ID;
				switch (invoiceType)
				{
					case DB_InvoiceType.OutPatientNotPrivate:
					case DB_InvoiceType.OutPatientPrivate:
						list.AddRange(
							List_InvoiceDetails.FindAll(
								item =>
									Convert.ToInt32(item.InvoiceDetailType).Equals((int) DB_ServiceType.ExaminationService) ||
									Convert.ToInt32(item.InvoiceDetailType).Equals((int) DB_ServiceType.ParentLabService) ||
									Convert.ToInt32(item.InvoiceDetailType).Equals((int) DB_ServiceType.InvestigationServices)));
						break;
					case DB_InvoiceType.InPatientNotPrivate:
					case DB_InvoiceType.InPatientPrivate:
						list.AddRange(
							List_InvoiceDetails.FindAll(
								item =>
									Convert.ToInt32(item.InvoiceDetailType).Equals((int) DB_ServiceType.ParentSurgeryService) ||
									Convert.ToInt32(item.InvoiceDetailType).Equals((int) DB_ServiceType.ParentSurgeryService)));
						break;
				}

				return list;
			}
		}

		public InvoiceItemType InvoiceItemType
		{
			get { return InvoiceItemType.Invoice; }
		}

		#endregion
	}
}
