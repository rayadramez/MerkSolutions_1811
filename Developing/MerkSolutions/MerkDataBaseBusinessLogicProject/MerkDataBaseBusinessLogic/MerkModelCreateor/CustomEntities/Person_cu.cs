using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class Person_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<Person_cu> _items;
		public static List<Person_cu> ItemsList = new List<Person_cu>();

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

		public override IList ReGenerateList()
		{
			LoadItemsList();

			return ItemsList;
		}

		public int TableIdentityID { get; set; }
		public IList ChildrenItemsList { get; set; }

		List<string> IDBCommon.ChildrenItemsList
		{
			get
			{
				List<string> list = new List<string>();
				list.Add("Patient_cu");
				list.Add("User_cu");
				list.Add("Doctor_cu");
				list.Add("Customer_cu");
				list.Add("Supplier_cu");
				return list;
			}
		}

		public string EntityName
		{
			get { return "Person_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.Person_cu.FirstOrDefault(item => item.ID.Equals(id));
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
				DBContext_External.Person_cu.Where(item => item.IsOnDuty)
					.OrderByDescending(item => item.ID)
					.ToList();

			return true;
		}

		public string GetFullName()
		{
			return FirstName_P + " " + SecondName_P + " " + ThirdName_P + " " + FourthName_P;
		}

		public string MaritalStatus
		{
			get
			{
				if (MaritalStatus_P_ID != null)
				{
					MaritalStatus_p maritalStatus =
						MerkDataBaseBusinessLogicProject.MaritalStatus_p.ItemsList.Find(
							item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(MaritalStatus_P_ID)));
					if (maritalStatus != null)
						return maritalStatus.Name_P;
				}

				return "";
			}
		}

		public string IdentificationCardType
		{
			get
			{
				if (IdentificationCardType_P_ID != null)
				{
					MerkDataBaseBusinessLogicProject.IdentificationCardType_p identificationCardType =
						MerkDataBaseBusinessLogicProject.IdentificationCardType_p.ItemsList.Find(
							item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(IdentificationCardType_P_ID)));
					if (identificationCardType != null)
						return identificationCardType.Name_P;
				}

				return "";
			}
		}
	}
}
