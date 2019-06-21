using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class VisitTiming_Medication : DBCommon, IDBCommon, IPEMR_Element
	{
		MerkFinanceEntities _context;
		private static List<VisitTiming_Medication> _items;
		public static List<VisitTiming_Medication> ItemsList = new List<VisitTiming_Medication>();

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

			ItemsList = DBContext_External.VisitTiming_Medication.ToList();
			return true;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.VisitTiming_Medication; }
		}

		public List<string> ChildrenItemsList
		{
			get
			{
				List<string> list = new List<string>();
				return list;
			}
		}

		public string EntityName
		{
			get { return "VisitTiming_Medication"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.VisitTiming_Medication.FirstOrDefault(item => item.ID.Equals(id));
		}

		#region Implementation of IPEMR_Element

		public DB_PEMR_ElementType PEMR_Element
		{
			get { return DB_PEMR_ElementType.VisitTiming_Medications; }
		}

		public int OrderIndex
		{
			get
			{
				PEMR_Elemet_p element = null;
				if (PEM_ElementPrintOrder_cu.ItemsList.Count == 0)
				{
					element = PEMR_Elemet_p.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals((int)DB_PEMR_ElementType.VisitTiming_Medications));
					if (element != null)
						return Convert.ToInt32(element.DefaultOrderIndex);
					return 0;
				}

				PEM_ElementPrintOrder_cu elementPrintOrder = PEM_ElementPrintOrder_cu.ItemsList.Find(
					item => Convert.ToInt32(item.PEMR_Elemet_P_ID)
						.Equals((int)DB_PEMR_ElementType.VisitTiming_Medications));
				if (elementPrintOrder != null)
					return elementPrintOrder.OrderIndex;

				element = PEMR_Elemet_p.ItemsList.Find(
					item => Convert.ToInt32(item.ID).Equals((int)DB_PEMR_ElementType.VisitTiming_Medications));
				if (element != null)
					return Convert.ToInt32(element.DefaultOrderIndex);
				return 0;
			}
		}

		public string ElementName
		{
			get
			{
				PEMR_Elemet_p element = PEMR_Elemet_p.ItemsList.Find(
					item => Convert.ToInt32(item.ID).Equals((int)DB_PEMR_ElementType.VisitTiming_Medications));
				if (element == null)
					return String.Empty;

				return element.Name_S;
			}
		}

		public string TranslatedItem { get; set; }

		public string TranslatedItemValue { get; set; }

		public PEMRElementStatus PEMRElementStatus { get; set; }

		#endregion

		public Medication_cu Medication
		{
			get
			{
				Medication_cu medication = Medication_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(Medication_CU_ID)));
				return medication;
			}
		}

		public string MedicationName_English
		{
			get
			{
				if(Medication == null)
					return String.Empty;
				return Medication.Name_P;
			}
		}

		public string MedicationName_Arabic
		{
			get
			{
				if (Medication == null)
					return String.Empty;
				return Medication.Name_S;
			}
		}

		public Dose_cu Dosage
		{
			get
			{
				Dose_cu dosage = Dose_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(Dose_CU_ID)));
				return dosage;
			}
		}

		public string DosageName_English
		{
			get
			{
				if (Dosage == null)
					return String.Empty;
				return Dosage.Name_P;
			}
		}

		public string DosageName_Arabic
		{
			get
			{
				if (Dosage == null)
					return String.Empty;
				return Dosage.Name_S;
			}
		}

		public TimeDuration_p TimeDuration
		{
			get
			{
				TimeDuration_p timeDuration = TimeDuration_p.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(TimeDuration_P_ID)));
				return timeDuration;
			}
		}

		public string TimeDurationName_English
		{
			get
			{
				if (TimeDuration == null)
					return String.Empty;
				return TimeDuration.Name_P;
			}
		}

		public string TimeDurationName_Arabic
		{
			get
			{
				if (TimeDuration == null)
					return String.Empty;
				return TimeDuration.Name_S;
			}
		}

		public string StartDateString
		{
			get
			{
				if (StartDate == null)
					return String.Empty;
				return Convert.ToDateTime(StartDate).ConvertDateTimeToString(false, false);
			}
		}

		public string EndDateString
		{
			get
			{
				if (EndDate == null)
					return String.Empty;
				return Convert.ToDateTime(EndDate).ConvertDateTimeToString(false, false);
			}
		}
	}
}
