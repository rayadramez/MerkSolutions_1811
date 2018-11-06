using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class Doctor_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<Doctor_cu> _items;
		public static List<Doctor_cu> ItemsList = new List<Doctor_cu>();

		#region ColumnNames

		public static String DoctorSpecialization_P_ID_ColumnaName
		{
			get { return "DoctorSpecialization_P_ID"; }
		}

		public static String DoctorCategory_CU_ID_ColumnaName
		{
			get { return "DoctorCategory_CU_ID"; }
		}

		public static String Description_ColumnaName
		{
			get { return "Description"; }
		}

		#endregion

		public string Name_P { get; set; }
		public string Name_S { get; set; }

		public override int ID
		{
			get { return Person_CU_ID; }
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
			ItemsList =
				DBContext_External.Doctor_cu.OrderByDescending(item => item.Person_CU_ID).Take(1000).ToList();

			foreach (Doctor_cu doctor in ItemsList)
				doctor.Name_P = doctor.Name_S = doctor.Person_cu.GetFullName();

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
			get { return (int)DB_TableIdentity.Doctor_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "Doctor_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.Doctor_cu.FirstOrDefault(item => item.Person_CU_ID.Equals(id));
		}

		#endregion
	}
}
