using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class Medication_Dose_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<Medication_Dose_cu> _items;
		public static List<Medication_Dose_cu> ItemsList = new List<Medication_Dose_cu>();

		#region ColumnNames

		public static String Floor_CU_ID_ColumnaName
		{
			get { return "Floor_CU_ID"; }
		}

		public static String IsMain_ColumnaName
		{
			get { return "IsMain"; }
		}

		public static String ChartOfAccount_CU_ID_ColumnaName
		{
			get { return "ChartOfAccount_CU_ID"; }
		}

		public static String InternalCode_ColumnaName
		{
			get { return "InternalCode"; }
		}

		public static String Description_ColumnaName
		{
			get { return "Description"; }
		}

		#endregion

		public override IList ReGenerateList()
		{
			LoadItemsList();

			return ItemsList;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.Medication_Dose_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "Medication_Dose_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.Medication_Dose_cu.FirstOrDefault(item => item.ID.Equals(id));
		}

		public override bool LoadFromDB
		{
			get { return true; }
		}

		public override DBCommonEntitiesType TableType
		{
			get { return DBCommonEntitiesType.CustomUserEntities; }
		}

		public override bool LoadItemsList()
		{
			ItemsList.Clear();
			ItemsList = DBContext_External.Medication_Dose_cu.Where(item => item.IsOnDuty).ToList();
			return true;
		}

		public Medication_cu Medication
		{
			get
			{
				Medication_cu medication = Medication_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(Medication_CU_ID)));
				return medication;
			}
		}

		public string MedicationName
		{
			get
			{
				if(Medication == null)
					return String.Empty;
				return Medication.Name_P;
			}
		}

		public Dose_cu Dose
		{
			get
			{
				Dose_cu dose = Dose_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(Dose_CU_ID)));
				return dose;
			}
		}

		public string DoseName
		{
			get
			{
				if (Dose == null)
					return String.Empty;
				return Dose.Name_P;
			}
		}
	}
}
