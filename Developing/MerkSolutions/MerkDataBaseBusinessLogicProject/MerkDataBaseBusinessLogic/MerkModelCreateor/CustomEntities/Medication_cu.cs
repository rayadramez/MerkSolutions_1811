using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class Medication_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<Medication_cu> _items;
		public static List<Medication_cu> ItemsList = new List<Medication_cu>();

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
			get { return (int)DB_TableIdentity.Medication_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "Medication_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.Medication_cu.FirstOrDefault(item => item.ID.Equals(id));
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
			ItemsList = DBContext_External.Medication_cu.Where(item => item.IsOnDuty).OrderBy(item => item.Name_P).ToList();
			return true;
		}

		public MedicationCategory_cu MedicationCategory
		{
			get
			{
				MedicationCategory_cu medicationCategory = MedicationCategory_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(MedicationCategory_CU_ID)));
				return medicationCategory;
			}
		}

		public string MedicationCategoryName
		{
			get
			{
				if (MedicationCategory == null)
					return string.Empty;

				return MedicationCategory.Name_P;
			}
		}
	}
}
