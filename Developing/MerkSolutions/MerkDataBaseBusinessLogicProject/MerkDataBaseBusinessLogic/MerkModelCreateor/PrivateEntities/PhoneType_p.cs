﻿using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class PhoneType_p : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<PhoneType_p> _items;
		public static List<PhoneType_p> ItemsList = new List<PhoneType_p>();

		#region ColumnNames

		public static String Serial_ColumnaName
		{
			get { return "Serial"; }
		}

		#endregion

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
			
			ItemsList = DBContext_External.PhoneType_p.ToList();
			return true;
		}

		public int TableIdentityID { get; set; }

		public System.Collections.IList ChildrenItemsList { get; set; }

		List<string> IDBCommon.ChildrenItemsList
		{
			get { throw new NotImplementedException(); }
		}

		public string EntityName { get; set; }
		public IDBCommon GetSpecificEntity(int id)
		{
			throw new NotImplementedException();
		}
	}
}
