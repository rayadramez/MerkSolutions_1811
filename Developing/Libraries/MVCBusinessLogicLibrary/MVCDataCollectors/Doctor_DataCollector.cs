using System;
using System.Collections.Generic;
using ApplicationConfiguration;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCDataCollectors
{
	public class Doctor_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IDoctorViewer
		where TEntity : DBCommon, IDBCommon, new()
	{
		#region Overrides of AbstractDataCollector<TEntity>

		public override AbstractDataCollector<TEntity> ActiveCollector { get; set; }
		public override AbstractDataCollector<TEntity> ParentActiveCollector { get; set; }
		public override bool Collect(AbstractDataCollector<TEntity> collector)
		{
			if (collector == null)
				return false;

			ActiveCollector = collector;

			ID = ((IDoctorViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (((Person_cu)ActiveDBItem).Doctor_cu == null)
				((Person_cu)ActiveDBItem).Doctor_cu = new Doctor_cu();

			if (((Person_cu)ActiveDBItem).User_cu == null)
				((Person_cu)ActiveDBItem).User_cu = new User_cu();

			#region General Person Details

			if (FirstName_P != null)
				((Person_cu)ActiveDBItem).FirstName_P = FirstName_P.ToString();

			if (SecondName_P != null)
				((Person_cu)ActiveDBItem).SecondName_P = SecondName_P.ToString();

			if (ThirdName_P != null)
				((Person_cu)ActiveDBItem).ThirdName_P = ThirdName_P.ToString();

			if (FourthName_P != null)
				((Person_cu)ActiveDBItem).FourthName_P = FourthName_P.ToString();

			if (Gender != null)
				((Person_cu)ActiveDBItem).Gender = Convert.ToBoolean(Gender);

			if (MaritalStatusID != null)
				((Person_cu)ActiveDBItem).MaritalStatus_P_ID = Convert.ToInt32(MaritalStatusID);

			if (BirthDate != null)
				((Person_cu)ActiveDBItem).BirthDate = Convert.ToDateTime(BirthDate);

			if (Phone1 != null)
				((Person_cu)ActiveDBItem).Phone1 = Phone1.ToString();

			if (Phone2 != null)
				((Person_cu)ActiveDBItem).Phone2 = Phone2.ToString();

			if (Address != null)
				((Person_cu)ActiveDBItem).Address = Address.ToString();

			if (EMail != null)
				((Person_cu)ActiveDBItem).EMail = EMail.ToString();

			if (IdentificationCardTypeID != null)
				((Person_cu)ActiveDBItem).IdentificationCardType_P_ID = Convert.ToInt32(IdentificationCardTypeID);

			if (IdentificationCardNumber != null)
				((Person_cu)ActiveDBItem).IdentificationCardNumber = IdentificationCardNumber.ToString();

			if (IdentificationCardIssuingDate != null)
				((Person_cu)ActiveDBItem).IdentificationCardIssuingDate = Convert.ToDateTime(IdentificationCardIssuingDate);

			if (IdentificationCardExpirationDate != null)
				((Person_cu)ActiveDBItem).IdentificationCardExpirationDate = Convert.ToDateTime(IdentificationCardExpirationDate);

			if (Mobile1 != null)
				((Person_cu)ActiveDBItem).Mobile1 = Mobile1.ToString();

			if (Mobile2 != null)
				((Person_cu)ActiveDBItem).Mobile2 = Mobile2.ToString();

			((Person_cu)ActiveDBItem).IsOnDuty = true;

			#endregion

			#region User Details

			if (InternalCode != null)
				((Person_cu)ActiveDBItem).User_cu.InternalCode = InternalCode.ToString();

			if (LoginName != null)
				((Person_cu)ActiveDBItem).User_cu.LoginName = LoginName.ToString();

			if (Password != null)
				((Person_cu)ActiveDBItem).User_cu.Password = Password.ToString();

			((Person_cu)ActiveDBItem).User_cu.OragnizationID = (int)ApplicationStaticConfiguration.Organization;

			((Person_cu)ActiveDBItem).User_cu.IsOnDuty = true;

			#endregion

			#region Doctor Details

			if (InternalCode != null)
				((Person_cu)ActiveDBItem).Doctor_cu.InternalCode = InternalCode.ToString();

			if (DoctorRankID != null)
				((Person_cu)ActiveDBItem).Doctor_cu.DoctorRank_P_ID = Convert.ToInt32(DoctorRankID);

			if (DoctorSpecializationID != null)
				((Person_cu)ActiveDBItem).Doctor_cu.DoctorSpecialization_P_ID = Convert.ToInt32(DoctorSpecializationID);

			if (DoctorCategoryID != null)
				((Person_cu)ActiveDBItem).Doctor_cu.DoctorCategory_CU_ID = Convert.ToInt32(DoctorCategoryID);

			if (DoctorProfessionalFees != null)
				((Person_cu)ActiveDBItem).Doctor_cu.DoctorProfessionalFeesIssuingType_P_ID = Convert.ToInt32(DoctorProfessionalFees);

			if (DoctorTaxTypeID != null)
				((Person_cu)ActiveDBItem).Doctor_cu.DoctorTaxType_CU_ID = Convert.ToInt32(DoctorTaxTypeID);

			if (PrivateMobile != null)
				((Person_cu)ActiveDBItem).Doctor_cu.PrivateMobile = PrivateMobile.ToString();

			if (Description != null)
				((Person_cu)ActiveDBItem).Doctor_cu.Description = Description.ToString();

			((Person_cu)ActiveDBItem).Doctor_cu.IsOnDuty = true;

			#endregion

			switch (((IDoctorViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((Person_cu)ActiveDBItem).IsOnDuty = false;
					((Person_cu)ActiveDBItem).User_cu.IsOnDuty = false;
					((Person_cu)ActiveDBItem).Doctor_cu.IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IDoctorViewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.User_Viewer; }
		}

		public override object UserID
		{
			get
			{
				if (ApplicationStaticConfiguration.ActiveLoginUser != null)
					return ApplicationStaticConfiguration.ActiveLoginUser.Person_CU_ID;
				return null;
			}
		}

		public override object EditingDate
		{
			get { return DateTime.Now; }
		}

		public override object IsOnDUty { get; set; }
		public override DB_CommonTransactionType CommonTransactionType { get; set; }

		public override string HeaderTitle
		{
			get { throw new System.NotImplementedException(); }
		}

		public override string GridXML
		{
			get { throw new System.NotImplementedException(); }
		}

		public override List<IViewer> RelatedViewers { get; set; }
		public override void ClearControls()
		{
			throw new System.NotImplementedException();
		}

		public override void FillControls()
		{
			throw new System.NotImplementedException();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<Person_cu>();
				if (ActiveDBItem != null)
				{
					((Person_cu)ActiveDBItem).User_cu = DBCommon.CreateNewDBEntity<User_cu>();
					((Person_cu)ActiveDBItem).Doctor_cu = DBCommon.CreateNewDBEntity<Doctor_cu>();
				}

				((IDoctorViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool ValidateBeforeSave(ref string message)
		{
			if (LoginName == null)
			{
				MessageToView = "يجـــب كتـابـــة إســم المستخــــدم";
				return false;
			}

			if (Password == null)
			{
				MessageToView = "يجـــب كتـابـــة كلمـــة المــرور";
				return false;
			}

			if (ConfirmationPassword == null)
			{
				MessageToView = "يجـــب كتـابـــة تـأكيــــد كلمـــة المــرور";
				return false;
			}

			if (!Convert.ToString(Password).Equals(ConfirmationPassword))
			{
				MessageToView = "يجـب تطـابـــق كلمـــة الســـر";
				return false;
			}

			return true;
		}

		public override bool CheckIfActiveDBItemExists()
		{
			if (ActiveCollector.ActiveDBItem == null)
				return true;

			foreach (Doctor_cu doctorCu in Doctor_cu.ItemsList)
				if (doctorCu.Person_cu.FirstName_P.Equals(FirstName_P) &&
				    doctorCu.Person_cu.SecondName_P.Equals(SecondName_P) &&
				    doctorCu.Person_cu.ThirdName_P.Equals(ThirdName_P) &&
				    doctorCu.Person_cu.FourthName_P.Equals(FourthName_P))
				{
					MessageToView = "هـذا الطبيـــب موجـود  بالفعـــل" + "\r\n" + "لا يمكــن الإضـافــــة";
					return true;
				}

			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((Person_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((Person_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((Person_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IDoctorViewer

		public object FirstName_P
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).FirstName_P; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).FirstName_P = value; }
		}

		public object SecondName_P
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).SecondName_P; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).SecondName_P = value; }
		}

		public object ThirdName_P
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).ThirdName_P; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).ThirdName_P = value; }
		}

		public object FourthName_P
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).FourthName_P; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).FourthName_P = value; }
		}

		public object Gender
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).Gender; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).Gender = value; }
		}

		public object MaritalStatusID
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).MaritalStatusID; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).MaritalStatusID = value; }
		}

		public object BirthDate
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).BirthDate; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).BirthDate = value; }
		}

		public object InternalCode
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).InternalCode; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).InternalCode = value; }
		}

		public object Mobile1
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).Mobile1; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).Mobile1 = value; }
		}

		public object Mobile2
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).Mobile2; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).Mobile2 = value; }
		}

		public object Phone1
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).Phone1; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).Phone1 = value; }
		}

		public object Phone2
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).Phone2; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).Phone2 = value; }
		}

		public object Address
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).Address; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).Address = value; }
		}

		public object EMail
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).EMail; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).EMail = value; }
		}

		public object IdentificationCardTypeID
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).IdentificationCardTypeID; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).IdentificationCardTypeID = value; }
		}

		public object IdentificationCardNumber
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).IdentificationCardNumber; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).IdentificationCardNumber = value; }
		}

		public object IdentificationCardIssuingDate
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).IdentificationCardIssuingDate; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).IdentificationCardIssuingDate = value; }
		}

		public object IdentificationCardExpirationDate
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).IdentificationCardExpirationDate; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).IdentificationCardExpirationDate = value; }
		}

		public object LoginName
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).LoginName; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).LoginName = value; }
		}

		public object Password
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).Password; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).Password = value; }
		}

		public object ConfirmationPassword
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).ConfirmationPassword; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).ConfirmationPassword = value; }
		}

		public object DoctorRankID
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).DoctorRankID; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).DoctorRankID = value; }
		}

		public object DoctorSpecializationID
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).DoctorSpecializationID; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).DoctorSpecializationID = value; }
		}

		public object DoctorCategoryID
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).DoctorCategoryID; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).DoctorCategoryID = value; }
		}

		public object DoctorTaxTypeID
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).DoctorTaxTypeID; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).DoctorTaxTypeID = value; }
		}

		public object DoctorProfessionalFees
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).DoctorProfessionalFees; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).DoctorProfessionalFees = value; }
		}

		public object IsInternal
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).IsInternal; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).IsInternal = value; }
		}

		public object PrivateMobile
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).PrivateMobile; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).PrivateMobile = value; }
		}

		public object Description
		{
			get { return ((IDoctorViewer)ActiveCollector.ActiveViewer).Description; }
			set { ((IDoctorViewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		#endregion
	}
}
