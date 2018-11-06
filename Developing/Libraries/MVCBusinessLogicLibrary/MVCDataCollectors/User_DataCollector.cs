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
	public class User_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IUserViewer
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

			if (Password == null || PasswordConfirmation == null || Password.ToString() != PasswordConfirmation.ToString())
				return false;

			ID = ((IUserViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (FirstName != null)
				((Person_cu)ActiveDBItem).FirstName_P = FirstName.ToString();

			if (SecondName != null)
				((Person_cu)ActiveDBItem).SecondName_P = SecondName.ToString();

			if (ThirdName != null)
				((Person_cu)ActiveDBItem).ThirdName_P = ThirdName.ToString();

			if (FourthName != null)
				((Person_cu)ActiveDBItem).FourthName_P = FourthName.ToString();

			if (MaritalStatus != null)
				((Person_cu)ActiveDBItem).MaritalStatus_P_ID = Convert.ToInt32(MaritalStatus);

			if (Gender != null)
				((Person_cu)ActiveDBItem).Gender = Convert.ToBoolean(Gender);

			if (BirthDate != null)
				((Person_cu)ActiveDBItem).BirthDate = Convert.ToDateTime(BirthDate);

			if (Mobile1 != null)
				((Person_cu)ActiveDBItem).Mobile1 = Mobile1.ToString();

			if (Mobile2 != null)
				((Person_cu)ActiveDBItem).Mobile2 = Mobile2.ToString();

			if (Phone1 != null)
				((Person_cu)ActiveDBItem).Phone1 = Phone1.ToString();

			if (Phone2 != null)
				((Person_cu)ActiveDBItem).Phone2 = Phone2.ToString();

			if (Address != null)
				((Person_cu)ActiveDBItem).Address = Address.ToString();

			if (Email != null)
				((Person_cu)ActiveDBItem).EMail = Email.ToString();

			if (IdentificationCardType != null)
				((Person_cu)ActiveDBItem).IdentificationCardType_P_ID = Convert.ToInt32(IdentificationCardType);

			if (IdentificationCardNumber != null)
				((Person_cu)ActiveDBItem).IdentificationCardNumber = IdentificationCardNumber.ToString();

			if (IdentificationCardIssueDate != null)
				((Person_cu)ActiveDBItem).IdentificationCardIssuingDate = Convert.ToDateTime(IdentificationCardIssueDate);

			if (IdentificationCardExpirationDate != null)
				((Person_cu)ActiveDBItem).IdentificationCardExpirationDate = Convert.ToDateTime(IdentificationCardExpirationDate);

			if (((Person_cu)ActiveDBItem).User_cu == null)
				((Person_cu)ActiveDBItem).User_cu = new User_cu();

			if (InternalCode != null)
				((Person_cu)ActiveDBItem).User_cu.InternalCode = InternalCode.ToString();

			if (LoginName != null)
				((Person_cu)ActiveDBItem).User_cu.LoginName = LoginName.ToString();

			((Person_cu) ActiveDBItem).User_cu.OragnizationID = (int) ApplicationStaticConfiguration.Organization;

			((Person_cu)ActiveDBItem).User_cu.Password = Password.ToString();

			((Person_cu)ActiveDBItem).IsOnDuty = true;
			((Person_cu)ActiveDBItem).User_cu.IsOnDuty = true;
			switch (((IUserViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((Person_cu)ActiveDBItem).IsOnDuty = false;
					((Person_cu)ActiveDBItem).User_cu.IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IUserViewer)ActiveCollector.ActiveViewer).RelatedViewers;

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

		public override object[] CollectSearchCriteria()
		{
			List<User_cu> list = User_cu.ItemsList.FindAll(item => item.IsOnDuty);
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<Person_cu>();
				if (ActiveDBItem != null)
					((Person_cu)ActiveDBItem).User_cu = DBCommon.CreateNewDBEntity<User_cu>();
				((IUserViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool ValidateBeforeSave(ref string message)
		{
			if (FirstName == null)
			{
				MessageToView = "يجـــب كتـابـــة الإســم الأول";
				return false;
			}

			if (SecondName == null)
			{
				MessageToView = "يجـــب كتـابـــة الإســم الثـانــي";
				return false;
			}

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

			if (PasswordConfirmation == null)
			{
				MessageToView = "يجـــب كتـابـــة تـأكيــــد كلمـــة المــرور";
				return false;
			}

			if (!Convert.ToString(Password).Equals(PasswordConfirmation))
			{
				MessageToView = "يجـب تطـابـــق كلمـــة الســـر";
				return false;
			}

			return true;
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
			FirstName = ((Person_cu)ActiveDBItem).FirstName_P;
			SecondName = ((Person_cu)ActiveDBItem).SecondName_P;
			ThirdName = ((Person_cu)ActiveDBItem).ThirdName_P;
			FourthName = ((Person_cu)ActiveDBItem).FourthName_P;
			MaritalStatus = ((Person_cu)ActiveDBItem).MaritalStatus_P_ID;
			Gender = ((Person_cu)ActiveDBItem).Gender;
			BirthDate = ((Person_cu)ActiveDBItem).BirthDate;
			Mobile1 = ((Person_cu)ActiveDBItem).Mobile1;
			Mobile2 = ((Person_cu)ActiveDBItem).Mobile2;
			Phone1 = ((Person_cu)ActiveDBItem).Phone1;
			Phone2 = ((Person_cu)ActiveDBItem).Phone2;
			Address = ((Person_cu)ActiveDBItem).Address;
			Email = ((Person_cu)ActiveDBItem).EMail;
			IdentificationCardType = ((Person_cu)ActiveDBItem).IdentificationCardType_P_ID;
			IdentificationCardNumber = ((Person_cu)ActiveDBItem).IdentificationCardNumber;
			IdentificationCardIssueDate = ((Person_cu)ActiveDBItem).IdentificationCardIssuingDate;
			IdentificationCardExpirationDate = ((Person_cu)ActiveDBItem).IdentificationCardExpirationDate;

			if (((Person_cu)ActiveDBItem).User_cu != null)
			{
				InternalCode = ((Person_cu)ActiveDBItem).User_cu.InternalCode;
				LoginName = ((Person_cu)ActiveDBItem).User_cu.LoginName;
				Password = ((Person_cu)ActiveDBItem).User_cu.Password;
			}

			((IUserViewer)ActiveCollector.ActiveViewer).ID = ((Person_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((Person_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((Person_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IUserViewer

		public object FirstName
		{
			get { return ((IUserViewer)ActiveCollector.ActiveViewer).FirstName; }
			set { ((IUserViewer)ActiveCollector.ActiveViewer).FirstName = value; }
		}

		public object SecondName
		{
			get { return ((IUserViewer)ActiveCollector.ActiveViewer).SecondName; }
			set { ((IUserViewer)ActiveCollector.ActiveViewer).SecondName = value; }
		}

		public object ThirdName
		{
			get { return ((IUserViewer)ActiveCollector.ActiveViewer).ThirdName; }
			set { ((IUserViewer)ActiveCollector.ActiveViewer).ThirdName = value; }
		}

		public object FourthName
		{
			get { return ((IUserViewer)ActiveCollector.ActiveViewer).FourthName; }
			set { ((IUserViewer)ActiveCollector.ActiveViewer).FourthName = value; }
		}

		public object MaritalStatus
		{
			get { return ((IUserViewer)ActiveCollector.ActiveViewer).MaritalStatus; }
			set { ((IUserViewer)ActiveCollector.ActiveViewer).MaritalStatus = value; }
		}

		public object Gender
		{
			get { return ((IUserViewer)ActiveCollector.ActiveViewer).Gender; }
			set { ((IUserViewer)ActiveCollector.ActiveViewer).Gender = value; }
		}

		public object BirthDate
		{
			get { return ((IUserViewer)ActiveCollector.ActiveViewer).BirthDate; }
			set { ((IUserViewer)ActiveCollector.ActiveViewer).BirthDate = value; }
		}

		public object InternalCode
		{
			get { return ((IUserViewer)ActiveCollector.ActiveViewer).InternalCode; }
			set { ((IUserViewer)ActiveCollector.ActiveViewer).InternalCode = value; }
		}

		public object LoginName
		{
			get { return ((IUserViewer)ActiveCollector.ActiveViewer).LoginName; }
			set { ((IUserViewer)ActiveCollector.ActiveViewer).LoginName = value; }
		}

		public object Password
		{
			get { return ((IUserViewer)ActiveCollector.ActiveViewer).Password; }
			set { ((IUserViewer)ActiveCollector.ActiveViewer).Password = value; }
		}

		public object PasswordConfirmation
		{
			get { return ((IUserViewer)ActiveCollector.ActiveViewer).PasswordConfirmation; }
			set { ((IUserViewer)ActiveCollector.ActiveViewer).PasswordConfirmation = value; }
		}

		public object Mobile1
		{
			get { return ((IUserViewer)ActiveCollector.ActiveViewer).Mobile1; }
			set { ((IUserViewer)ActiveCollector.ActiveViewer).Mobile1 = value; }
		}

		public object Mobile2
		{
			get { return ((IUserViewer)ActiveCollector.ActiveViewer).Mobile2; }
			set { ((IUserViewer)ActiveCollector.ActiveViewer).Mobile2 = value; }
		}

		public object Phone1
		{
			get { return ((IUserViewer)ActiveCollector.ActiveViewer).Phone1; }
			set { ((IUserViewer)ActiveCollector.ActiveViewer).Phone1 = value; }
		}

		public object Phone2
		{
			get { return ((IUserViewer)ActiveCollector.ActiveViewer).Phone2; }
			set { ((IUserViewer)ActiveCollector.ActiveViewer).Phone2 = value; }
		}

		public object Address
		{
			get { return ((IUserViewer)ActiveCollector.ActiveViewer).Address; }
			set { ((IUserViewer)ActiveCollector.ActiveViewer).Address = value; }
		}

		public object Email
		{
			get { return ((IUserViewer)ActiveCollector.ActiveViewer).Email; }
			set { ((IUserViewer)ActiveCollector.ActiveViewer).Email = value; }
		}

		public object IdentificationCardType
		{
			get { return ((IUserViewer)ActiveCollector.ActiveViewer).IdentificationCardType; }
			set { ((IUserViewer)ActiveCollector.ActiveViewer).IdentificationCardType = value; }
		}

		public object IdentificationCardNumber
		{
			get { return ((IUserViewer)ActiveCollector.ActiveViewer).IdentificationCardNumber; }
			set { ((IUserViewer)ActiveCollector.ActiveViewer).IdentificationCardNumber = value; }
		}

		public object IdentificationCardIssueDate
		{
			get { return ((IUserViewer)ActiveCollector.ActiveViewer).IdentificationCardIssueDate; }
			set { ((IUserViewer)ActiveCollector.ActiveViewer).IdentificationCardIssueDate = value; }
		}

		public object IdentificationCardExpirationDate
		{
			get { return ((IUserViewer)ActiveCollector.ActiveViewer).IdentificationCardExpirationDate; }
			set { ((IUserViewer)ActiveCollector.ActiveViewer).IdentificationCardExpirationDate = value; }
		}

		#endregion
	}
}
