using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class Country_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<Country_cu> _items;
		public static List<Country_cu> ItemsList = new List<Country_cu>();

		#region ColumnNames

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
			ItemsList = DBContext_External.Country_cu.Where(item => item.IsOnDuty).OrderBy(item => item.Name_P).ToList();
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
			get { return (int)DB_TableIdentity.Country_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "Country_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.Country_cu.FirstOrDefault(item => item.ID.Equals(id));
		}

		#endregion
	}
}
