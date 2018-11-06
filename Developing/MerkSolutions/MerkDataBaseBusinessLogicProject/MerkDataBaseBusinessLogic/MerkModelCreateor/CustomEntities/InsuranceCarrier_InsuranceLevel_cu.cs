using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class InsuranceCarrier_InsuranceLevel_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<InsuranceCarrier_InsuranceLevel_cu> _items;
		public static List<InsuranceCarrier_InsuranceLevel_cu> ItemsList = new List<InsuranceCarrier_InsuranceLevel_cu>();

		#region ColumnNames

		public static String InsuranceCarrier_CU_ID_ColumnaName
		{
			get { return "InsuranceCarrier_CU_ID"; }
		}

		public static String InsuranceLevel_CU_ID_ColumnaName
		{
			get { return "InsuranceLevel_CU_ID"; }
		}

		public static String InsurancePercentage_ColumnaName
		{
			get { return "InsurancePercentage"; }
		}

		public static String PatientMaxAmount_ColumnaName
		{
			get { return "PatientMaxAmount"; }
		}

		#endregion

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
			ItemsList = DBContext_External.InsuranceCarrier_InsuranceLevel_cu.Where(item => item.IsOnDuty).OrderBy(item => item.InsuranceCarrier_CU_ID).ToList();
			return true;
		}

		#region Implementation of IDBCommon

		public override IList ReGenerateList()
		{
			LoadItemsList();

			return ItemsList;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.InsuranceCarrier_InsuranceLevel_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "InsuranceCarrier_InsuranceLevel_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.InsuranceCarrier_InsuranceLevel_cu.FirstOrDefault(item => item.ID.Equals(id));
		}

		#endregion
	}
}
