using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class DiagnosisCategory_Diagnosis_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<DiagnosisCategory_Diagnosis_cu> _items;
		public static List<DiagnosisCategory_Diagnosis_cu> ItemsList = new List<DiagnosisCategory_Diagnosis_cu>();

		#region ColumnNames

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
			get { return (int)DB_TableIdentity.DiagnosisCategory_Diagnosis_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "DiagnosisCategory_Diagnosis_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.DiagnosisCategory_Diagnosis_cu.FirstOrDefault(item => item.ID.Equals(id));
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
			ItemsList = DBContext_External.DiagnosisCategory_Diagnosis_cu.Where(item => item.IsOnDuty).OrderBy(item => item.ID).ToList();
			return true;
		}

		public DiagnosisCategory_cu DiagnosisCategory
		{
			get
			{
				DiagnosisCategory_cu diagnosisCategory = DiagnosisCategory_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(DiagnosisCategory_CU_ID)));
				return diagnosisCategory;
			}
		}

		public string DiagnosisCategoryName
		{
			get
			{
				if (DiagnosisCategory == null)
					return string.Empty;

				return DiagnosisCategory.Name_P;
			}
		}

		public Diagnosis_cu Diagnosis
		{
			get
			{
				Diagnosis_cu diagnosis = Diagnosis_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(Diagnosis_CU_ID)));
				return diagnosis;
			}
		}

		public string DiagnosisName
		{
			get
			{
				if (Diagnosis == null)
					return string.Empty;

				return Diagnosis.Name_P;
			}
		}
	}
}
