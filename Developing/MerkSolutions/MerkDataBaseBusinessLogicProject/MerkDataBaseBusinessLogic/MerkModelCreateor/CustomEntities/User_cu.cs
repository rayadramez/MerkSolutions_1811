using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class User_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<User_cu> _items;
		public static List<User_cu> ItemsList = new List<User_cu>();

		#region ColumnNames

		public static String Description_ColumnaName
		{
			get { return "Description"; }
		}

		#endregion

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
				DBContext_External.User_cu.OrderByDescending(item => item.Person_CU_ID).ToList();

			foreach (User_cu user in ItemsList)
			{
				user.FullName = user.Person_cu.GetFullName();
				user.Address = user.Person_cu.Address;
				user.Gender = user.Person_cu.Gender;
				user.MaritalStatus = user.Person_cu.MaritalStatus;
				user.Mobile1 = user.Person_cu.Mobile1;
				user.Mobile2 = user.Person_cu.Mobile2;
				user.Phone1 = user.Person_cu.Phone1;
				user.Phone2 = user.Person_cu.Phone2;
				user.EMail = user.Person_cu.EMail;
				user.BirthDate = user.Person_cu.BirthDate;
				user.IdentificationCardType = user.Person_cu.IdentificationCardType;
				user.IdentificationCardNumber = user.Person_cu.IdentificationCardNumber;
				user.IdentificationCardIssuingDate = user.Person_cu.IdentificationCardIssuingDate;
				user.IdentificationCardExpirationDate = user.Person_cu.IdentificationCardExpirationDate;
				user.IsOnDuty = user.Person_cu.IsOnDuty;
			}

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
			get { return (int)DB_TableIdentity.User_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "User_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.User_cu.FirstOrDefault(item => item.Person_CU_ID.Equals(id));
		}

		#endregion
		//{
		//	get
		//	{
		//		if (_person == null)
		//			_person = DBCommon.GetEntity<Person_cu>(Person_CU_ID);
		//		return _person;
		//	}
		//}

		public object FullName { get; set; }
		public object Address { get; set; }
		public object Gender { get; set; }
		public object MaritalStatus { get; set; }
		public object Mobile1 { get; set; }
		public object Mobile2 { get; set; }
		public object Phone1 { get; set; }
		public object Phone2 { get; set; }
		public object EMail { get; set; }
		public object BirthDate { get; set; }
		public object IdentificationCardType { get; set; }
		public object IdentificationCardNumber { get; set; }
		public object IdentificationCardIssuingDate { get; set; }
		public object IdentificationCardExpirationDate { get; set; }
	}
}
