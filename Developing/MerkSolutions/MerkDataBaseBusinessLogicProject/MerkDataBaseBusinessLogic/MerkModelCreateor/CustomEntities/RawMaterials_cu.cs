using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class RawMaterials_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<RawMaterials_cu> _items;
		public static List<RawMaterials_cu> ItemsList = new List<RawMaterials_cu>();

		public override IList ReGenerateList()
		{
			LoadItemsList();

			return ItemsList;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.RawMaterials_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "RawMaterials_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.RawMaterials_cu.FirstOrDefault(item => item.ID.Equals(id));
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
			ItemsList = DBContext_External.RawMaterials_cu.Where(item => item.IsOnDuty).OrderBy(item => item.Name_P).ToList();
			return true;
		}
	}
}
