using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class VisitTiming_MainDiagnosis : DBCommon, IDBCommon, IPEMR_Element
	{
		MerkFinanceEntities _context;
		private static List<VisitTiming_MainDiagnosis> _items;
		public static List<VisitTiming_MainDiagnosis> ItemsList = new List<VisitTiming_MainDiagnosis>();

		#region ColumnNames

		public static String VisitTimingID_ColumnaName
		{
			get { return "VisitTimingID"; }
		}

		public static String Treatment_ColumnaName
		{
			get { return "Treatment"; }
		}

		public static String StepOrderIndex_ColumnaName
		{
			get { return "StepOrderIndex"; }
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

			ItemsList = DBContext_External.VisitTiming_MainDiagnosis.ToList();
			return true;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.VisitTiming_MainDiagnosis; }
		}

		public List<string> ChildrenItemsList
		{
			get
			{
				List<string> list = new List<string>();
				//list.Add("VisitTiming_Diagnosis");
				return list;
			}
		}

		public string EntityName
		{
			get { return "VisitTiming_MainDiagnosis"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.VisitTiming_MainDiagnosis.FirstOrDefault(item => item.ID.Equals(id));
		}

		#region Implementation of IPEMR_Element

		public DB_PEMR_ElementType PEMR_Element
		{
			get { return DB_PEMR_ElementType.VisitTiming_MainDiagnosis; }
		}

		public int OrderIndex
		{
			get
			{
				PEMR_Elemet_p element = null;
				if (PEM_ElementPrintOrder_cu.ItemsList.Count == 0)
				{
					element = PEMR_Elemet_p.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals((int)DB_PEMR_ElementType.VisitTiming_Diagnosis));
					if (element != null)
						return Convert.ToInt32(element.DefaultOrderIndex);
					return 0;
				}
				PEM_ElementPrintOrder_cu elementPrintOrder = PEM_ElementPrintOrder_cu.ItemsList.Find(
					item => Convert.ToInt32(item.PEMR_Elemet_P_ID)
						.Equals((int)DB_PEMR_ElementType.VisitTiming_Diagnosis));
				if (elementPrintOrder != null)
					return elementPrintOrder.OrderIndex;
				element = PEMR_Elemet_p.ItemsList.Find(
					item => Convert.ToInt32(item.ID).Equals((int)DB_PEMR_ElementType.VisitTiming_Diagnosis));
				if (element != null)
					return Convert.ToInt32(element.DefaultOrderIndex);
				return 0;
			}
		}

		public string ElementName
		{
			get
			{
				return "Diagnosis";
			}
		}

		public string TranslatedItem { get; set; }

		public string TranslatedItemValue { get; set; }
		public PEMRElementStatus PEMRElementStatus { get; set; }

		#endregion

		public DiagnosisType_p DiagnosisType
		{
			get
			{
				DiagnosisType_p type = DiagnosisType_p.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(DiagnosisType_P_ID)));
				return type;
			}
		}

		public string DiagnosisTypeName
		{
			get
			{
				if (DiagnosisType == null)
					return string.Empty;
				return DiagnosisType.Name_P;
			}
		}
	}
}
