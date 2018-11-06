using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class Patient_cu : DBCommon, IDBCommon
	{
		public static List<Patient_cu> ItemsList = new List<Patient_cu>();

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
				DBContext_External.Patient_cu.OrderByDescending(item => item.Person_CU_ID).Take(500).ToList();
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
			get { return (int)DB_TableIdentity.Patient_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "Patient_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.Patient_cu.FirstOrDefault(item => item.Person_CU_ID.Equals(id));
		}

		#endregion

		private string _fullName;
		public string PatientFullName
		{
			get
			{
				if (PersonObject == null)
					return string.Empty;

				if (_fullName == null || string.IsNullOrEmpty(_fullName) || string.IsNullOrWhiteSpace(_fullName))
					_fullName = PersonObject.FirstName_P + " " + PersonObject.SecondName_P + " " + PersonObject.ThirdName_P + " " +
					            PersonObject.FourthName_P;

				return _fullName;
			}
		}

		private Person_cu _person;

		public Person_cu PersonObject
		{
			get
			{
				if (_person == null)
					_person = Person_cu;
				return _person;
			}
			set { _person = value; }
		}

		public bool IsMale
		{
			get
			{
				if (PersonObject != null)
					return PersonObject.Gender;

				return false;
			}
		}

		public string Phone1
		{
			get
			{
				if (PersonObject == null)
					return string.Empty;

				return PersonObject.Phone1;
			}
		}

		public string Phone2
		{
			get
			{
				if (PersonObject == null)
					return string.Empty;

				return PersonObject.Phone2;
			}
		}

		public string Mobile1
		{
			get
			{
				if (PersonObject == null)
					return string.Empty;

				return PersonObject.Mobile1;
			}
		}

		public string Mobile2
		{
			get
			{
				if (PersonObject == null)
					return string.Empty;

				return PersonObject.Mobile2;
			}
		}

		public string Address
		{
			get
			{
				if (PersonObject == null)
					return string.Empty;

				return PersonObject.Address;
			}
		}

		public DateTime? BirthDate
		{
			get
			{
				if (PersonObject.BirthDate != null) 
					return (DateTime) PersonObject.BirthDate;
				return null;
			}
		}

		public static List<Patient_cu> GetPatientsList(int count)
		{
			ItemsList.Clear();
			ItemsList = DBContext_External.Patient_cu.OrderByDescending(item => item.Person_CU_ID).Take(count).ToList();
			return ItemsList.OrderBy(item => item.Person_CU_ID).ToList();
		}
	}
}
