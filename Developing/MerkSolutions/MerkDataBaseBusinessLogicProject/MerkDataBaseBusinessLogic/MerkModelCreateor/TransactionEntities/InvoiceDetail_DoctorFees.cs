using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class InvoiceDetail_DoctorFees : DBCommon, IDBCommon, IInvoiceItem
	{
		MerkFinanceEntities _context;
		private static List<InvoiceDetail_DoctorFees> _items;
		public static List<InvoiceDetail_DoctorFees> ItemsList = new List<InvoiceDetail_DoctorFees>();
		public static List<InvoiceDetail_DoctorFees> AllItemsList = new List<InvoiceDetail_DoctorFees>();

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
			
			ItemsList = AllItemsList = DBContext_External.InvoiceDetail_DoctorFees.ToList();
			return true;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.InvoiceDetail_DoctorFees; }
		}

		public IList ChildrenItemsList { get; set; }

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

		public double SurchargeAmount_PatientShare_InvoiceItem { get; private set; }
		public double SurchargeAmount_InsuranceShare_InvoiceItem { get; private set; }
		public double TotalSurchargeAmount_InvoiceItem { get; set; }
		public double StampAmount_PatientShare_InvoiceItem { get; private set; }
		public double StampAmount_InsuranceShare_InvoiceItem { get; private set; }
		public double TotalStampAmount_InvoiceItem { get; private set; }
		public double PatientShare_AfterAddsOn_InvoiceItem { get; private set; }
		public double InsuranceShare_AfterAddsOn_InvoiceItem { get; private set; }

		public double TotalServicePrice_AfterAddsOn_InvoiceItem { get; private set; }

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
			get { return InvoiceItemType.InvoiceDetail_DoctorFees; }
		}

		#endregion
	}
}
