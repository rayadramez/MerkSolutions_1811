using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class VisitTiming_MedicalHistory : DBCommon, IDBCommon, IPEMR_Element
	{
		MerkFinanceEntities _context;
		private static List<VisitTiming_MedicalHistory> _items;
		public static List<VisitTiming_MedicalHistory> ItemsList = new List<VisitTiming_MedicalHistory>();

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

			ItemsList = DBContext_External.VisitTiming_MedicalHistory.ToList();
			return true;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.VisitTiming_MedicalHistory; }
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
			get { return "VisitTiming_MedicalHistory"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.VisitTiming_MedicalHistory.FirstOrDefault(item => item.ID.Equals(id));
		}

		#region Implementation of IPEMR_Element

		public DB_PEMR_ElementType PEMR_Element
		{
			get { return DB_PEMR_ElementType.VisitTiming_MedicalHistory; }
		}

		public int OrderIndex
		{
			get
			{
				PEMR_Elemet_p element = null;
				if (PEM_ElementPrintOrder_cu.ItemsList.Count == 0)
				{
					element = PEMR_Elemet_p.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals((int)DB_PEMR_ElementType.VisitTiming_MedicalHistory));
					if (element != null)
						return Convert.ToInt32(element.DefaultOrderIndex);
					return 0;
				}

				PEM_ElementPrintOrder_cu elementPrintOrder = PEM_ElementPrintOrder_cu.ItemsList.Find(
					item => Convert.ToInt32(item.PEMR_Elemet_P_ID)
						.Equals((int)DB_PEMR_ElementType.VisitTiming_MedicalHistory));
				if (elementPrintOrder != null)
					return elementPrintOrder.OrderIndex;

				element = PEMR_Elemet_p.ItemsList.Find(
					item => Convert.ToInt32(item.ID).Equals((int)DB_PEMR_ElementType.VisitTiming_MedicalHistory));
				if (element != null)
					return Convert.ToInt32(element.DefaultOrderIndex);
				return 0;
			}
		}

		public string ElementName
		{
			get
			{
				PEMR_Elemet_p element = element = PEMR_Elemet_p.ItemsList.Find(
					item => Convert.ToInt32(item.ID).Equals((int)DB_PEMR_ElementType.VisitTiming_MedicalHistory));
				if (element == null)
					return String.Empty;

				return element.Name_S;
			}
		}

		public string TranslatedItem { get; set; }
		public string TranslatedItemValue { get; set; }
		public PEMRElementStatus PEMRElementStatus { get; set; }

		#endregion

		public DiabetedMedicationType_p DiabetedMedicationType
		{
			get
			{
				DiabetedMedicationType_p diabetedMedicationType = DiabetedMedicationType_p.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(DiabetesMedicationType_P_ID)));
				return diabetedMedicationType;
			}
		}

		public string DiabetedMedicationTypeName
		{
			get
			{
				if (DiabetedMedicationType != null)
					return DiabetedMedicationType.Name_P;
				return string.Empty;
			}
		}

		public Medication_cu DiabetesMedication
		{
			get
			{
				if (DiabetesMedication_CU_ID == null)
					return null;
				Medication_cu medication = Medication_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(DiabetesMedication_CU_ID)));
				return medication;
			}
		}

		public string DiabetesMedicationName
		{
			get
			{
				if (DiabetesMedication != null)
					return DiabetesMedication.Name_P;
				return string.Empty;
			}
		}

		public Dose_cu DiabetesDose
		{
			get
			{
				if (DiabetesDose_CU_ID == null)
					return null;
				Dose_cu dose = Dose_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(DiabetesDose_CU_ID)));
				return dose;
			}
		}

		public string DiabetesDoseName
		{
			get
			{
				if (DiabetesDose != null)
					return DiabetesDose.Name_P;
				return string.Empty;
			}
		}

		public TimeDuration_p DiabetesTimeDurationType
		{
			get
			{
				if (DiabetesTimeDurationType_P_ID == null)
					return null;
				TimeDuration_p timeDuration = TimeDuration_p.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(DiabetesTimeDurationType_P_ID)));
				return timeDuration;
			}
		}

		public string DiabetesTimeDurationTypeName
		{
			get
			{
				if (DiabetesTimeDurationType != null)
					return DiabetesTimeDurationType.Name_P;
				return string.Empty;
			}
		}

		public Medication_cu HypertensionMedication
		{
			get
			{
				if (HypertensionMedication_CU_ID == null)
					return null;
				Medication_cu medication = Medication_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(HypertensionMedication_CU_ID)));
				return medication;
			}
		}

		public string HypertensionMedicationName
		{
			get
			{
				if (DiabetesMedication != null)
					return DiabetesMedication.Name_P;
				return string.Empty;
			}
		}

		public Dose_cu HypertensionDose
		{
			get
			{
				if (HypertensionDose_CU_ID == null)
					return null;
				Dose_cu dose = Dose_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(HypertensionDose_CU_ID)));
				return dose;
			}
		}

		public string HypertensionDoseName
		{
			get
			{
				if (HypertensionDose != null)
					return HypertensionDose.Name_P;
				return string.Empty;
			}
		}

		public TimeDuration_p HypertensionTimeDurationType
		{
			get
			{
				if (HypertensionTimeDurationType_P_ID == null)
					return null;
				TimeDuration_p timeDuration = TimeDuration_p.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(HypertensionTimeDurationType_P_ID)));
				return timeDuration;
			}
		}

		public string HypertensionTimeDurationTypeName
		{
			get
			{
				if (HypertensionTimeDurationType != null)
					return HypertensionTimeDurationType.Name_P;
				return string.Empty;
			}
		}

		public DiabetesType_p DiabetesType
		{
			get
			{
				if (DiabetesType_P_ID == null)
					return null;
				DiabetesType_p diabetesType = DiabetesType_p.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(DiabetesType_P_ID)));
				return diabetesType;
			}
		}

		public string DiabetesTypeName
		{
			get
			{
				if (DiabetesType != null)
					return DiabetesType.Name_P;
				return string.Empty;
			}
		}


	}
}
