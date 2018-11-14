using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class VisitTiming : DBCommon, IDBCommon, IPEMR_Element
	{
		MerkFinanceEntities _context;
		private static List<VisitTiming> _items;
		public static List<VisitTiming> ItemsList = new List<VisitTiming>();

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

			ItemsList = DBContext_External.VisitTimings.ToList();
			return true;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.VisitTiming; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get
			{
				List<string> list = new List<string>();
				//list.Add("VisitTiming_Attachement");
				//list.Add("VisitTiming_InvestigationReservation");
				//list.Add("VisitTiming_LabReservation");
				//list.Add("VisitTiming_MainDiagnosis");
				//list.Add("VisitTiming_MainSymptoms");
				//list.Add("VisitTiming_Medication");
				//list.Add("VisitTiming_SocialHistory");
				//list.Add("VisitTiming_SurgeryReservation");
				//list.Add("VisitTiming_TreatmentPlan");
				//list.Add("VisitTiming_VitalSign");
				return list;
			}
		}

		public string EntityName
		{
			get { return "VisitTiming"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.VisitTimings.FirstOrDefault(item => item.ID.Equals(id));
		}

		public List<VisitTiming_SocialHistory> List_VisitTiming_SocialHistory { get; set; }

		public List<VisitTiming_MedicalHistory> List_VisitTiming_MedicalHistory { get; set; }

		public List<VisitTiming_TreatmentPlan> List_VisitTiming_TreatmentPlan { get; set; }

		public List<VisitTiming_Attachment> List_VisitTiming_Attachment { get; set; }

		public List<VisitTiming_EOMReading> List_VisitTiming_EOMReading { get; set; }

		public List<VisitTiming_InvestigationReservation> List_VisitTiming_InvestigationReservation { get; set; }

		public List<VisitTiming_LabReservation> List_VisitTiming_LabReservation { get; set; }

		public List<VisitTiming_MainAdnexaSegmentSign> List_VisitTiming_MainAdnexaSegmentSign { get; set; }

		public List<VisitTiming_MainAnteriorSegmentSign> List_VisitTiming_MainAnteriorSegmentSign { get; set; }

		public List<VisitTiming_MainDiagnosis> List_VisitTiming_MainDiagnosis { get; set; }

		public List<VisitTiming_MainEOMSign> List_VisitTiming_MainEOMSign { get; set; }

		public List<VisitTiming_MainPosteriorSegmentSign> List_VisitTiming_MainPosteriorSegmentSign { get; set; }

		public List<VisitTiming_MainSymptoms> List_VisitTiming_MainSymptoms { get; set; }

		public List<VisitTiming_Medication> List_VisitTiming_Medication { get; set; }

		public List<VisitTiming_Pupillary> List_VisitTiming_Pupillary { get; set; }

		public List<VisitTiming_SurgeryReservation> List_VisitTiming_SurgeryReservation { get; set; }

		public List<VisitTiming_VisionRefractionReading> List_VisitTiming_VisionRefractionReading { get; set; }

		public List<VisitTiming_VitalSign> List_VisitTiming_VitalSign { get; set; }

		#region Implementation of IPEMR_Element

		public DB_PEMR_ElementType PEMR_Element { get; private set; }
		public int OrderIndex { get; private set; }
		public string ElementName { get; private set; }
		public string TranslatedItem { get; set; }
		public string TranslatedItemValue { get; set; }
		public PEMRElementStatus PEMRElementStatus { get; set; }

		#endregion
	}
}

