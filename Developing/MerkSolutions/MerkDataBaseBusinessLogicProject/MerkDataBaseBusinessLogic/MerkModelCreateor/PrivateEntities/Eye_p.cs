﻿using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class Eye_p : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<Eye_p> _items;
		public static List<Eye_p> ItemsList = new List<Eye_p>();

		public override bool LoadFromDB
		{
			get { return true; }
		}

		public override DBCommonEntitiesType TableType
		{
			get { return DBCommonEntitiesType.PrivateInternalEntities; }
		}

		public override bool LoadItemsList()
		{
			ItemsList.Clear();
			ItemsList = DBContext_External.Eye_p.ToList();
			return true;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.Eye_p; }
		}

		public System.Collections.IList ChildrenItemsList { get; set; }

		List<string> IDBCommon.ChildrenItemsList
		{
			get { throw new NotImplementedException(); }
		}

		public string EntityName { get { return "Eye_p"; } }
		public IDBCommon GetSpecificEntity(int id)
		{
			throw new NotImplementedException();
		}
	}
}
