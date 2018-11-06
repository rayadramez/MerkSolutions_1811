using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class InvoiceDetail_Accommodation : DBCommon, IDBCommon, IInvoiceItem
	{
		MerkFinanceEntities _context;
		private static List<InvoiceDetail_Accommodation> _items;
		public static List<InvoiceDetail_Accommodation> ItemsList = new List<InvoiceDetail_Accommodation>();
		public static List<InvoiceDetail_Accommodation> AllItemsList = new List<InvoiceDetail_Accommodation>();

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
			
			ItemsList = AllItemsList = DBContext_External.InvoiceDetail_Accommodation.ToList();
			return true;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.InvoiceDetail_Accommodation; }
		}

		public System.Collections.IList ChildrenItemsList { get; set; }

		List<string> IDBCommon.ChildrenItemsList
		{
			get { throw new NotImplementedException(); }
		}

		public string EntityName { get; set; }
		public IDBCommon GetSpecificEntity(int id)
		{
			throw new NotImplementedException();
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
				if (PatientShareDiscount != null)
					return Convert.ToDouble(PatientShareDiscount);
				return 0;
			}
			set { PatientShareDiscount = value; }
		}

		public double SurchargeAmount_PatientShare_InvoiceItem
		{
			get
			{
				return Math.Round(FinancialBusinessLogicLibrary.CalculateInvoiceDetail_Item(ParentInvoiceObject, this,
					PriceType.SurchargeAmount_PatientShare), 2);
			}
		}

		public double SurchargeAmount_InsuranceShare_InvoiceItem
		{
			get
			{
				return Math.Round(FinancialBusinessLogicLibrary.CalculateInvoiceDetail_Item(ParentInvoiceObject, this,
					PriceType.SurchargeAmount_InsuranceShare), 2);
			}
		}

		public double TotalSurchargeAmount_InvoiceItem
		{
			get
			{
				return
					FinancialBusinessLogicLibrary.CalculateInvoiceDetail_Item(ParentInvoiceObject, this,
						PriceType.SurchargeAmount_PatientShare) +
					FinancialBusinessLogicLibrary.CalculateInvoiceDetail_Item(ParentInvoiceObject, this,
						PriceType.SurchargeAmount_InsuranceShare);
			}
			set { SurchargeAmount = value; }
		}

		public double StampAmount_PatientShare_InvoiceItem { get; private set; }
		public double StampAmount_InsuranceShare_InvoiceItem { get; private set; }
		public double TotalStampAmount_InvoiceItem { get; private set; }
		public double PatientShare_AfterAddsOn_InvoiceItem { get; private set; }
		public double InsuranceShare_AfterAddsOn_InvoiceItem { get; private set; }

		public double TotalServicePrice_AfterAddsOn_InvoiceItem
		{
			get { return TotalServicePrice_BeforeAddsOn_InvoiceItem + TotalSurchargeAmount_InvoiceItem; }
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

		public List<IInvoiceItem> List_InvoiceItems { get; private set; }

		public InvoiceItemType InvoiceItemType
		{
			get { return InvoiceItemType.InvoiceDetail_Accommodation; }
		}

		#endregion

		private InvoiceDetail _parentInvoiceDetailObject;
		public InvoiceDetail ParentInvoiceDetailObject
		{
			get
			{
				if (_parentInvoiceDetailObject == null)
					_parentInvoiceDetailObject = InvoiceDetail;

				return _parentInvoiceDetailObject;
			}
		}

		private Invoice _parentInvoiceObject;
		public Invoice ParentInvoiceObject
		{
			get
			{
				if (_parentInvoiceObject == null)
					_parentInvoiceObject = ParentInvoiceDetailObject.Invoice;

				return _parentInvoiceObject;
			}
		}

		public string InPatientAccommodationFullName
		{
			get
			{
				InPatientRoomBed_cu roomBed =
					MerkDataBaseBusinessLogicProject.InPatientRoomBed_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(InPatientRoomBed_CU_ID)));
				if (roomBed == null)
					return "";

				InPatientRoom_cu room =
					InPatientRoom_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(InPatientRoom_CU_ID)));
				if (room == null)
					return "";
				InPatientRoomClassification_cu roomClass =
					InPatientRoomClassification_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(room.InPatientRoomClassification_CU_ID)));
				if (roomClass == null)
					return "";
				string fullName = "";
				fullName += roomClass.ShortName ?? roomClass.Name_P;
				fullName += "-";
				fullName += room.ShortName ?? room.Name_P;

				if (string.IsNullOrEmpty(fullName) || string.IsNullOrWhiteSpace(fullName))
					return roomBed.ShortName ?? roomBed.Name_P;

				return roomBed.ShortName != null ? fullName + "-" + roomBed.ShortName : fullName + "-" + roomBed.Name_P;
			}
		}
	}
}
