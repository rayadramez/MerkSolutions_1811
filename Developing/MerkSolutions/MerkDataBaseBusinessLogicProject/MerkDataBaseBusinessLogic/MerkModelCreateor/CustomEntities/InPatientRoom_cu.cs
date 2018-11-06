using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class InPatientRoom_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<InPatientRoom_cu> _items;
		public static List<InPatientRoom_cu> ItemsList = new List<InPatientRoom_cu>();

		#region ColumnNames

		public static String InternalCode_ColumnaName
		{
			get { return "InternalCode"; }
		}

		public static String Floor_CU_ID_ColumnaName
		{
			get { return "Floor_CU_ID"; }
		}

		public static String InPatientRoomClassification_CU_ID_ColumnaName
		{
			get { return "InPatientRoomClassification_CU_ID"; }
		}

		public static String ShortName_ColumnaName
		{
			get { return "ShortName"; }
		}

		public static String Description_ColumnaName
		{
			get { return "Description"; }
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
			ItemsList = DBContext_External.InPatientRoom_cu.Where(item => item.IsOnDuty).OrderBy(item => item.Name_P).ToList();
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
			get { return (int)DB_TableIdentity.InPatientRoom_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "InPatientRoom_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.InPatientRoom_cu.FirstOrDefault(item => item.ID.Equals(id));
		}

		#endregion
	}
}
