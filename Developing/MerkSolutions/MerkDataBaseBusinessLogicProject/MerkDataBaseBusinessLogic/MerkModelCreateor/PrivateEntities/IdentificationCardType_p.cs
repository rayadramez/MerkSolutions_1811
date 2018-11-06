using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class IdentificationCardType_p : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<IdentificationCardType_p> _items;
		public static List<IdentificationCardType_p> ItemsList = new List<IdentificationCardType_p>();

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

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.IdentificationCardType_p; }
		}

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
			ItemsList = DBContext_External.IdentificationCardType_p.ToList();
			return true;
		}

		#region Implementation of IDBCommon

		public IList ChildrenItemsList { get; set; }

		List<string> IDBCommon.ChildrenItemsList
		{
			get { throw new NotImplementedException(); }
		}

		public string EntityName
		{
			get { return "IdentificationCardType_p"; }
		}

		public IDBCommon GetSpecificEntity(int id)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
