﻿using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class DoctorProfessionalFeesIssuingType_p : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<DoctorProfessionalFeesIssuingType_p> _items;
		public static List<DoctorProfessionalFeesIssuingType_p> ItemsList = new List<DoctorProfessionalFeesIssuingType_p>();

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

			ItemsList = DBContext_External.DoctorProfessionalFeesIssuingType_p.ToList();
			return true;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.DoctorProfessionalFeesIssuingType_p; }
		}

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
