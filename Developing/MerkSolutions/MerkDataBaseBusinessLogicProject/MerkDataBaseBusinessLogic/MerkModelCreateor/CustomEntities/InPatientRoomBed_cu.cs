using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class InPatientRoomBed_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<InPatientRoomBed_cu> _items;
		public static List<InPatientRoomBed_cu> ItemsList = new List<InPatientRoomBed_cu>();

		#region ColumnNames

		public static String InPatientRoom_CU_ID_ColumnaName
		{
			get { return "InPatientRoom_CU_ID"; }
		}

		public static String InPatientRoomBedStatus_P_ID_ColumnaName
		{
			get { return "InPatientRoomBedStatus_P_ID"; }
		}

		public static String InternalCode_ColumnaName
		{
			get { return "InternalCode"; }
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
			ItemsList = DBContext_External.InPatientRoomBed_cu.Where(item => item.IsOnDuty).OrderBy(item => item.Name_P).ToList();
			return true;
		}

		public string BedFullName
		{
			get
			{
				InPatientRoom_cu room =
					InPatientRoom_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(InPatientRoom_CU_ID)));
				if (room == null)
					return "";
				InPatientRoomClassification_cu roomClass =
					InPatientRoomClassification_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(room.InPatientRoomClassification_CU_ID)));
				if (roomClass == null)
					return "";
				string fullName = "";
				fullName += roomClass.ShortName ?? roomClass.Name_P;
				fullName += "-";
				fullName += room.ShortName ?? room.Name_P;
				
				if(string.IsNullOrEmpty(fullName) || string.IsNullOrWhiteSpace(fullName))
					return ShortName ?? Name_P;

				return ShortName != null ? fullName + "-" + ShortName : fullName + "-" + Name_P;
			}
		}

		#region Implementation of IDBCommon

		public override IList ReGenerateList()
		{
			LoadItemsList();

			return ItemsList;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.InPatientRoomBed_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "InPatientRoomBed_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.InPatientRoomBed_cu.FirstOrDefault(item => item.ID.Equals(id));
		}

		#endregion
	}
}
