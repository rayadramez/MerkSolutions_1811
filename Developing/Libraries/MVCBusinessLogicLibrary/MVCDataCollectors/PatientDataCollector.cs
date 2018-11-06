using System;
using System.Collections.Generic;
using ApplicationConfiguration;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCDataCollectors
{
	public class PatientDataCollector<TEntity> : AbstractDataCollector<TEntity>, IPatientViewer
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

			ID = ((IPatientViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			((Person_cu)ActiveDBItem).DBCommonTransactionType =
				((IPatientViewer)ActiveCollector.ActiveViewer).CommonTransactionType;

			if (FirstNameP != null)
				((Person_cu)ActiveDBItem).FirstName_P = FirstNameP.ToString();

			if (SecondNameP != null)
				((Person_cu)ActiveDBItem).SecondName_P = SecondNameP.ToString();

			if (ThirdNameP != null)
				((Person_cu)ActiveDBItem).ThirdName_P = ThirdNameP.ToString();

			if (FourthNameP != null)
				((Person_cu)ActiveDBItem).FourthName_P = FourthNameP.ToString();

			if (FirstNameS != null)
				((Person_cu)ActiveDBItem).FirstName_S = FirstNameS.ToString();

			if (SecondNameS != null)
				((Person_cu)ActiveDBItem).SecondName_S = SecondNameS.ToString();

			if (ThirdNameS != null)
				((Person_cu)ActiveDBItem).ThirdName_S = ThirdNameS.ToString();

			if (FourthNameS != null)
				((Person_cu)ActiveDBItem).FourthName_S = FourthNameS.ToString();

			if (PersonGender != null)
				((Person_cu)ActiveDBItem).Gender = Convert.ToBoolean(PersonGender);

			if (PersonTitle != null)
				((Person_cu)ActiveDBItem).PersonTitle_P_ID =
					Convert.ToInt32(PersonTitle);

			if (Nationality != null)
				((Person_cu)ActiveDBItem).Nationality_CU_ID = Convert.ToInt32(Nationality);

			if (MaritalStatus != null)
				((Person_cu)ActiveDBItem).MaritalStatus_P_ID = Convert.ToInt32(MaritalStatus);

			if (DateOfBirth != null)
				((Person_cu)ActiveDBItem).BirthDate = Convert.ToDateTime(DateOfBirth);

			if (((Person_cu)ActiveDBItem).Patient_cu == null)
				((Person_cu)ActiveDBItem).Patient_cu = new Patient_cu();

			if (UserID != null)
				((Person_cu)ActiveDBItem).Patient_cu.InsertedBy = Convert.ToInt32(UserID);

			if (InsuranceCarrier != null && InsuranceLevel != null)
			{
				InsuranceCarrier_InsuranceLevel_cu insuaCarrierInsuranceLevelCu =
					InsuranceCarrier_InsuranceLevel_cu.ItemsList.Find(
						item =>
							Convert.ToInt32(item.InsuranceCarrier_CU_ID).Equals(Convert.ToInt32(InsuranceCarrier)) &&
							Convert.ToInt32(item.InsuranceLevel_CU_ID).Equals(Convert.ToInt32(InsuranceLevel)));
				if (insuaCarrierInsuranceLevelCu != null)
				{
					((Person_cu)ActiveDBItem).Patient_cu.InsuranceCarrier_InsuranceLevel_CU_ID =
						Convert.ToInt32(insuaCarrierInsuranceLevelCu.ID);
				}

				if (RelativeName != null)
					((Person_cu)ActiveDBItem).Patient_cu.RelativeName = RelativeName.ToString();

				if (RelativeAddress != null)
					((Person_cu)ActiveDBItem).Patient_cu.RelativeAddress = RelativeAddress.ToString();

				if (RelativePhone != null)
					((Person_cu)ActiveDBItem).Patient_cu.RelativePhone = RelativePhone.ToString();

				if (RelativeType != null)
					((Person_cu)ActiveDBItem).Patient_cu.PersonRelativeType_P_ID = Convert.ToInt32(RelativeType);
			}

			if (CountryOfResidence != null)
				((Person_cu)ActiveDBItem).CountryOfResidence_CU_ID = Convert.ToInt32(CountryOfResidence);

			if (City != null)
				((Person_cu)ActiveDBItem).CityOfResidence_CU_ID = Convert.ToInt32(City);

			if (Region != null)
				((Person_cu)ActiveDBItem).Region_CU_ID = Convert.ToInt32(Region);

			if (Address != null)
				((Person_cu)ActiveDBItem).Address = Address.ToString();

			if (Phone1 != null)
				((Person_cu)ActiveDBItem).Phone1 = Phone1.ToString();

			if (Phone2 != null)
				((Person_cu)ActiveDBItem).Phone2 = Phone2.ToString();

			if (Mobile1 != null)
				((Person_cu)ActiveDBItem).Mobile1 = Mobile1.ToString();

			if (Mobile2 != null)
				((Person_cu)ActiveDBItem).Mobile2 = Mobile2.ToString();

			if (Email != null)
				((Person_cu)ActiveDBItem).Mobile2 = Email.ToString();

			if (Email != null)
				((Person_cu)ActiveDBItem).Mobile2 = Email.ToString();

			if (IdentificationCardType != null)
				((Person_cu)ActiveDBItem).IdentificationCardType_P_ID = Convert.ToInt32(IdentificationCardType);

			if (IdentificationCardNumber != null)
				((Person_cu)ActiveDBItem).IdentificationCardNumber = IdentificationCardNumber.ToString();

			if (IdentificationCardIssuingDate != null)
				((Person_cu)ActiveDBItem).IdentificationCardIssuingDate = Convert.ToDateTime(IdentificationCardIssuingDate);

			if (IdentificationCardEpirationDate != null)
				((Person_cu)ActiveDBItem).IdentificationCardExpirationDate = Convert.ToDateTime(IdentificationCardEpirationDate);

			((Person_cu)ActiveDBItem).IsOnDuty = true;
			switch (CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((Person_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IPatientViewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { throw new System.NotImplementedException(); }
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
					((Person_cu)ActiveDBItem).Patient_cu = DBCommon.CreateNewDBEntity<Patient_cu>();
				((IPatientViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
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
			((IPatientViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.UpdateExisting;

			FirstNameP = ((Person_cu)ActiveDBItem).FirstName_P;
			SecondNameP = ((Person_cu)ActiveDBItem).SecondName_P;
			ThirdNameP = ((Person_cu)ActiveDBItem).ThirdName_P;
			FourthNameP = ((Person_cu)ActiveDBItem).FourthName_P;
			FirstNameS = ((Person_cu)ActiveDBItem).FirstName_S;
			SecondNameS = ((Person_cu)ActiveDBItem).SecondName_S;
			ThirdNameS = ((Person_cu)ActiveDBItem).ThirdName_S;
			FourthNameS = ((Person_cu)ActiveDBItem).FourthName_S;
			PersonGender = ((Person_cu)ActiveDBItem).Gender;
			PersonTitle = ((Person_cu)ActiveDBItem).PersonTitle_P_ID;
			Nationality = ((Person_cu)ActiveDBItem).Nationality_CU_ID;
			CountryOfResidence = ((Person_cu)ActiveDBItem).CountryOfResidence_CU_ID;
			City = ((Person_cu)ActiveDBItem).CityOfResidence_CU_ID;
			Region = ((Person_cu)ActiveDBItem).Region_CU_ID;
			MaritalStatus = ((Person_cu)ActiveDBItem).MaritalStatus_P_ID;
			DateOfBirth = ((Person_cu)ActiveDBItem).BirthDate;
			Address = ((Person_cu)ActiveDBItem).Address;
			Mobile1 = ((Person_cu)ActiveDBItem).Mobile1;
			Mobile2 = ((Person_cu)ActiveDBItem).Mobile2;
			Phone1 = ((Person_cu)ActiveDBItem).Phone1;
			Phone2 = ((Person_cu)ActiveDBItem).Phone2;
			Email = ((Person_cu)ActiveDBItem).EMail;
			IdentificationCardType = ((Person_cu)ActiveDBItem).IdentificationCardType_P_ID;
			IdentificationCardNumber = ((Person_cu)ActiveDBItem).IdentificationCardNumber;
			IdentificationCardIssuingDate = ((Person_cu)ActiveDBItem).IdentificationCardIssuingDate;
			IdentificationCardEpirationDate = ((Person_cu)ActiveDBItem).IdentificationCardExpirationDate;

			if (((Person_cu)ActiveDBItem).Patient_cu != null)
			{
				InsuranceCarrier_InsuranceLevel_cu bridge =
					InsuranceCarrier_InsuranceLevel_cu.ItemsList.Find(
						item =>
							Convert.ToInt32(item.ID)
								.Equals(Convert.ToInt32(((Person_cu)ActiveDBItem).Patient_cu.InsuranceCarrier_InsuranceLevel_CU_ID)));
				if (bridge != null)
				{
					InsuranceCarrier_cu carrier =
						InsuranceCarrier_cu.ItemsList.Find(
							item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(bridge.InsuranceCarrier_CU_ID)));
					InsuranceLevel_cu level =
						InsuranceLevel_cu.ItemsList.Find(
							item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(bridge.InsuranceLevel_CU_ID)));
					if (carrier != null)
						((IPatientViewer)ActiveCollector.ActiveViewer).InsuranceCarrier = carrier.ID;
					if (level != null)
						((IPatientViewer)ActiveCollector.ActiveViewer).InsuranceLevel = level.ID;
				}

				RelativeName = ((Person_cu)ActiveDBItem).Patient_cu.RelativeName;
				RelativeAddress = ((Person_cu)ActiveDBItem).Patient_cu.RelativeAddress;
				RelativeType = ((Person_cu)ActiveDBItem).Patient_cu.PersonRelativeType_P_ID;
				RelativePhone = ((Person_cu)ActiveDBItem).Patient_cu.RelativePhone;
			}

			((IPatientViewer)ActiveCollector.ActiveViewer).ID = ((Person_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((Person_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((Person_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IPatientViewer

		public object PersonTitle
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).PersonTitle; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).PersonTitle = value; }
		}

		public object PersonGender
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).PersonGender; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).PersonGender = value; }
		}

		public object FirstNameP
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).FirstNameP; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).FirstNameP = value; }
		}

		public object SecondNameP
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).SecondNameP; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).SecondNameP = value; }
		}

		public object ThirdNameP
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).ThirdNameP; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).ThirdNameP = value; }
		}

		public object FourthNameP
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).FourthNameP; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).FourthNameP = value; }
		}

		public object FirstNameS
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).FirstNameS; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).FirstNameS = value; }
		}

		public object SecondNameS
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).SecondNameS; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).SecondNameS = value; }
		}

		public object ThirdNameS
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).ThirdNameS; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).ThirdNameS = value; }
		}

		public object FourthNameS
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).FourthNameS; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).FourthNameS = value; }
		}

		public object Nationality
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).Nationality; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).Nationality = value; }
		}

		public object MaritalStatus
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).MaritalStatus; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).MaritalStatus = value; }
		}

		public object DateOfBirth
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).DateOfBirth; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).DateOfBirth = value; }
		}

		public object InsuranceCarrier
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).InsuranceCarrier; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).InsuranceCarrier = value; }
		}

		public object InsuranceLevel
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).InsuranceLevel; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).InsuranceLevel = value; }
		}

		public object CountryOfResidence
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).CountryOfResidence; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).CountryOfResidence = value; }
		}

		public object City
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).City; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).City = value; }
		}

		public object Region
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).Region; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).Region = value; }
		}

		public object Address
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).Address; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).Address = value; }
		}

		public object Mobile1
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).Mobile1; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).Mobile1 = value; }
		}

		public object Mobile2
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).Mobile2; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).Mobile2 = value; }
		}

		public object Phone1
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).Phone1; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).Phone1 = value; }
		}

		public object Phone2
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).Phone2; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).Phone2 = value; }
		}

		public object Email
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).Email; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).Email = value; }
		}

		public object RelativeName
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).RelativeName; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).RelativeName = value; }
		}

		public object RelativeType
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).RelativeName; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).RelativeName = value; }
		}

		public object RelativePhone
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).RelativePhone; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).RelativePhone = value; }
		}

		public object RelativeAddress
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).RelativeAddress; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).RelativeAddress = value; }
		}

		public object IdentificationCardType
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).IdentificationCardType; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).IdentificationCardType = value; }
		}

		public object IdentificationCardNumber
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).IdentificationCardNumber; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).IdentificationCardNumber = value; }
		}

		public object IdentificationCardIssuingDate
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).IdentificationCardIssuingDate; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).IdentificationCardIssuingDate = value; }
		}

		public object IdentificationCardEpirationDate
		{
			get { return ((IPatientViewer)ActiveCollector.ActiveViewer).IdentificationCardEpirationDate; }
			set { ((IPatientViewer)ActiveCollector.ActiveViewer).IdentificationCardEpirationDate = value; }
		}

		public List<IPersonRelativeViewer> List_PersonRelativeViewers { get; set; }

		#endregion
	}
}
