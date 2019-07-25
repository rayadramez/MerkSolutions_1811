using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class InvoiceDetail : DBCommon, IDBCommon, IInvoiceItem
	{
		MerkFinanceEntities _context;
		private static List<InvoiceDetail> _items;
		public static List<InvoiceDetail> ItemsList = new List<InvoiceDetail>();
		public static List<InvoiceDetail> AllItemsList = new List<InvoiceDetail>();

		#region ColumnNames

		public static string InvoiceID_ColumnaName
		{
			get { return "InvoiceID"; }
		}

		public static string ParentInvoiceDetailID_ColumnaName
		{
			get { return "ParentInvoiceDetailID"; }
		}

		public static string Service_CU_ID_ColumnaName
		{
			get { return "Service_CU_ID"; }
		}

		public static string Date_ColumnaName
		{
			get { return "Date"; }
		}

		public static string Doctor_CU_ID_ColumnaName
		{
			get { return "Doctor_CU_ID"; }
		}

		public static string Count_ColumnaName
		{
			get { return "Count"; }
		}

		public static string PatientShare_ColumnaName
		{
			get { return "PatientShare"; }
		}

		public static string InsuranceShare_ColumnaName
		{
			get { return "InsuranceShare"; }
		}

		public static string DiscountType_P_ID_ColumnaName
		{
			get { return "DiscountType_P_ID"; }
		}

		public static string PatientShareDiscount_ColumnaName
		{
			get { return "PatientShareDiscount"; }
		}

		public static string IsInsuranceApplied_ColumnaName
		{
			get { return "IsInsuranceApplied"; }
		}

		public static string IsOnDuty_ColumnaName
		{
			get { return "IsOnDuty"; }
		}

		public static string IsSurchargeApplied_ColumnaName
		{
			get { return "IsSurchargeApplied"; }
		}

		public static string SurchargeAmount_ColumnaName
		{
			get { return "SurchargeAmount"; }
		}

		public static string Description_ColumnaName
		{
			get { return "Description"; }
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
			
			ItemsList = AllItemsList = DBContext_External.InvoiceDetails.ToList();
			return true;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.InvoiceDetail; }
		}

		public IList ChildrenItemsList { get; set; }

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName { get; set; }
		public IDBCommon GetSpecificEntity(int id)
		{
			throw new NotImplementedException();
		}

		public double ServicePrice
		{
			get
			{
				return Convert.ToDouble(PatientShare) + Convert.ToDouble(InsuranceShare);
			}
		}

		public Service_cu Service
		{
			get
			{
				Service_cu service = null;
				if (Service_CU_ID != null)
					service = Service_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(Service_CU_ID)));

				return service;
			}
		}

		public string ServiceName
		{
			get { return Service != null ? Service.Name_P : string.Empty; }
		}

		public ServiceCategory_cu ServiceCategory
		{
			get
			{
				if (Service == null)
					return null;
				ServiceCategory_cu serviceCategory =
					ServiceCategory_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(Service.ServiceCategory_CU_ID)));
				return serviceCategory;
			}
		}

		public string ServiceCategoryName
		{
			get { return ServiceCategory != null ? ServiceCategory.Name_P : string.Empty; }
		}

		public ServiceType_p ServiceType
		{
			get
			{
				if (Service == null)
					return null;
				ServiceType_p serviceType =
					ServiceType_p.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(Service.ServiceType_P_ID)));
				return serviceType;
			}
		}

		public string ServiceTypeName
		{
			get { return ServiceType != null ? ServiceType.Name_P : string.Empty; }
		}

		public Doctor_cu GetDoctor()
		{
			Doctor_cu doctor = null;
			if (Doctor_CU_ID != null)
				return Doctor_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(Doctor_CU_ID)));
			return doctor;
		}

		public string DoctorName
		{
			get
			{
				if (GetDoctor() != null)
					return GetDoctor().Name_P;

				return string.Empty;
			}
		}

		private InvoiceDetail _parentInvoiceDetail;

		public InvoiceDetail ParentInvoiceDetail
		{
			get
			{
				if (ParentInvoiceDetailID != null)
					if (_parentInvoiceDetail == null)
						_parentInvoiceDetail = GetEntity<InvoiceDetail>(Convert.ToInt32(ParentInvoiceDetailID));
				return _parentInvoiceDetail;
			}
		}

		public DB_ServiceType InvoiceDetailType
		{
			get
			{
				if (Service != null)
				{
					int? serviceTypePId = Service.ServiceType_P_ID;
					if (serviceTypePId != null)
						return (DB_ServiceType) serviceTypePId;
				}
				return DB_ServiceType.None;
			}
		}

		public bool IsInPatientParentService
		{
			get
			{
				return InvoiceDetailType == DB_ServiceType.ParentAccommodationService ||
				       InvoiceDetailType == DB_ServiceType.SurgeryService;
			}
		}

		public bool IsParentLabService()
		{
			return InvoiceDetailType == DB_ServiceType.ParentLabService;
		}

		private List<InvoiceDetail> _list_InvoiceDetail_Children;
		public List<InvoiceDetail> List_InvoiceDetail_Children
		{
			get
			{
				return _list_InvoiceDetail_Children ??
					   (_list_InvoiceDetail_Children = InvoiceDetail1.ToList());
			}
		}

		private List<InvoiceDetail_Accommodation> _list_InvoiceDetail_Accommodation;
		public List<InvoiceDetail_Accommodation> List_InvoiceDetail_Accommodation
		{
			get
			{
				return _list_InvoiceDetail_Accommodation ??
				       (_list_InvoiceDetail_Accommodation = InvoiceDetail_Accommodation.ToList());
			}
		}

		private List<InvoiceDetail_DoctorFees> _list_InvoiceDetail_DoctorFees;
		public List<InvoiceDetail_DoctorFees> List_InvoiceDetail_DoctorFees
		{
			get
			{
				return _list_InvoiceDetail_DoctorFees ??
					   (_list_InvoiceDetail_DoctorFees = InvoiceDetail_DoctorFees.ToList());
			}
		}

		private List<InvoiceDetail_Inventory> _list_InvoiceDetail_Inventory;
		public List<InvoiceDetail_Inventory> List_InvoiceDetail_Inventory
		{
			get
			{
				return _list_InvoiceDetail_Inventory ??
					   (_list_InvoiceDetail_Inventory = InvoiceDetail_Inventory.ToList());
			}
		}

		private List<VisitTiming> _list_Visit_Timing;

		public List<VisitTiming> List_Visit_Timing
		{
			get { return _list_Visit_Timing ?? (_list_Visit_Timing = VisitTimings.ToList()); }
		}

		#region Implementation of IInvoiceItem

		public double PatientShare_BeforeAddsOn_InvoiceItem
		{
			get { return PatientShare; }
			set { PatientShare = value; }
		}
		public double InsuranceShare_BeforeAddsOn_InvoiceItem
		{
			get { return InsuranceShare; }
			set { InsuranceShare = value; }
		}
		public double TotalServicePrice_BeforeAddsOn_InvoiceItem
		{
			get { return PatientShare + InsuranceShare; }
			set
			{
				PatientShare = Convert.ToDouble(value) - Convert.ToDouble(InsuranceShare);
				InsuranceShare = Convert.ToDouble(value) - Convert.ToDouble(PatientShare);
			}
		}

		public double PatientShareDiscount_InvoiceItem
		{
			get
			{
				if(PatientShareDiscount != null)
					return Convert.ToDouble(PatientShareDiscount);
				return 0;
			}
			set { PatientShareDiscount = value; }
		}

		public double SurchargeAmount_PatientShare_InvoiceItem { get; private set; }
		public double SurchargeAmount_InsuranceShare_InvoiceItem { get; private set; }
		public double TotalSurchargeAmount_InvoiceItem { get; set; }

		public double StampAmount_PatientShare_InvoiceItem { get; private set; }
		public double StampAmount_InsuranceShare_InvoiceItem { get; private set; }
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
			get { return IsInsuranceApplied; }
			set { IsInsuranceApplied = value; }
		}

		public bool IsSurchargeApplied_InvoiceItem
		{
			get { return IsSurchargeApplied; }
			set { IsSurchargeApplied = value; }
		}

		public bool IsStampApplied_InvoiceItem { get; set; }

		public double surchargeAmount_InvoiceItem { get; set; }

		public List<IInvoiceItem> List_InvoiceItems
		{
			get
			{
				List<IInvoiceItem> list = new List<IInvoiceItem>();
				list.AddRange(List_InvoiceDetail_Accommodation);
				list.AddRange(List_InvoiceDetail_DoctorFees);
				list.AddRange(List_InvoiceDetail_Inventory);
				return list;
			}
		}

		public InvoiceItemType InvoiceItemType
		{
			get
			{
				switch (InvoiceDetailType)
				{
					case DB_ServiceType.ParentAccommodationService:
						return InvoiceItemType.InPatient_Parent_AccommodationService;
					case DB_ServiceType.ParentSurgeryService:
						return InvoiceItemType.InPatient_Parent_SurgeryService;
					case DB_ServiceType.ExaminationService:
						return InvoiceItemType.OutPatient_InvoiceDetail_ExaminationService;
					case DB_ServiceType.ParentLabService:
						return InvoiceItemType.OutPatient_InvoiceDetail_ParentLabService;
					case DB_ServiceType.LabServices:
						return InvoiceItemType.OutPatient_InvoiceDetail_ChildLabService;
					case DB_ServiceType.InvestigationServices:
						return InvoiceItemType.OutPatient_InvoiceDetail_InvestigationService;
				}

				return InvoiceItemType.None;
			}
		}

		#endregion

		public bool IsCustomPriceUsed { get; set; }

		public object StationPointID { get; set; }
		public object StationPointStagesID { get; set; }
	}
}
