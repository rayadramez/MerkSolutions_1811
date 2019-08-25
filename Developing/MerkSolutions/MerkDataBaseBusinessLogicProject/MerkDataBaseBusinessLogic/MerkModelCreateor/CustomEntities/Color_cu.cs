using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class Color_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<Color_cu> _items;
		public static List<Color_cu> ItemsList = new List<Color_cu>();

		public override IList ReGenerateList()
		{
			LoadItemsList();

			return ItemsList;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.Color_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "Color_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.Color_cu.FirstOrDefault(item => item.ID.Equals(id));
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
			ItemsList = DBContext_External.Color_cu.Where(item => item.IsOnDuty).OrderBy(item => item.Name_P).ToList();
			return true;
		}
	}
}
