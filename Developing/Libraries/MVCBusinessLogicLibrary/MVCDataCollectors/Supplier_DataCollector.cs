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
	public class Supplier_DataCollector<TEntity> : AbstractDataCollector<TEntity>, ISupplierViewer
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

			ID = ((ISupplierViewer)ActiveCollector.ActiveViewer).ID;

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

			if (((Person_cu) ActiveDBItem).Supplier_cu == null)
				((Person_cu)ActiveDBItem).Supplier_cu = new Supplier_cu();

			if (InternalCode != null)
				((Person_cu)ActiveDBItem).Supplier_cu.InternalCode = InternalCode.ToString();

			if (((Person_cu)ActiveDBItem).Supplier_cu != null)
				((Person_cu)ActiveDBItem).Supplier_cu.IsOnDuty = true;

			((Person_cu)ActiveDBItem).IsOnDuty = true;
			switch (((ISupplierViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((Person_cu)ActiveDBItem).IsOnDuty = false;
					if (((Person_cu)ActiveDBItem).Supplier_cu != null)
						((Person_cu)ActiveDBItem).Supplier_cu.IsOnDuty = false;
					break;
			}

			RelatedViewers = ((ISupplierViewer)ActiveCollector.ActiveViewer).RelatedViewers;

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
					((Person_cu)ActiveDBItem).Supplier_cu = DBCommon.CreateNewDBEntity<Supplier_cu>();
				((ISupplierViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
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

			if (((Person_cu)ActiveDBItem).Supplier_cu != null)
				InternalCode = ((Person_cu)ActiveDBItem).Supplier_cu.InternalCode;

			((ISupplierViewer)ActiveCollector.ActiveViewer).ID = ((Person_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((Person_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((Person_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of ISupplierViewer

		public object FirstName
		{
			get { return ((ISupplierViewer)ActiveCollector.ActiveViewer).FirstName; }
			set { ((ISupplierViewer)ActiveCollector.ActiveViewer).FirstName = value; }
		}

		public object SecondName
		{
			get { return ((ISupplierViewer)ActiveCollector.ActiveViewer).SecondName; }
			set { ((ISupplierViewer)ActiveCollector.ActiveViewer).SecondName = value; }
		}

		public object ThirdName
		{
			get { return ((ISupplierViewer)ActiveCollector.ActiveViewer).ThirdName; }
			set { ((ISupplierViewer)ActiveCollector.ActiveViewer).ThirdName = value; }
		}

		public object FourthName
		{
			get { return ((ISupplierViewer)ActiveCollector.ActiveViewer).FourthName; }
			set { ((ISupplierViewer)ActiveCollector.ActiveViewer).FourthName = value; }
		}

		public object MaritalStatus
		{
			get { return ((ISupplierViewer)ActiveCollector.ActiveViewer).MaritalStatus; }
			set { ((ISupplierViewer)ActiveCollector.ActiveViewer).MaritalStatus = value; }
		}

		public object Gender
		{
			get { return ((ISupplierViewer)ActiveCollector.ActiveViewer).Gender; }
			set { ((ISupplierViewer)ActiveCollector.ActiveViewer).Gender = value; }
		}

		public object BirthDate
		{
			get { return ((ISupplierViewer)ActiveCollector.ActiveViewer).BirthDate; }
			set { ((ISupplierViewer)ActiveCollector.ActiveViewer).BirthDate = value; }
		}

		public object InternalCode
		{
			get { return ((ISupplierViewer)ActiveCollector.ActiveViewer).InternalCode; }
			set { ((ISupplierViewer)ActiveCollector.ActiveViewer).InternalCode = value; }
		}

		public object Mobile1
		{
			get { return ((ISupplierViewer)ActiveCollector.ActiveViewer).Mobile1; }
			set { ((ISupplierViewer)ActiveCollector.ActiveViewer).Mobile1 = value; }
		}

		public object Mobile2
		{
			get { return ((ISupplierViewer)ActiveCollector.ActiveViewer).Mobile2; }
			set { ((ISupplierViewer)ActiveCollector.ActiveViewer).Mobile2 = value; }
		}

		public object Phone1
		{
			get { return ((ISupplierViewer)ActiveCollector.ActiveViewer).Phone1; }
			set { ((ISupplierViewer)ActiveCollector.ActiveViewer).Phone1 = value; }
		}

		public object Phone2
		{
			get { return ((ISupplierViewer)ActiveCollector.ActiveViewer).Phone2; }
			set { ((ISupplierViewer)ActiveCollector.ActiveViewer).Phone2 = value; }
		}

		public object Address
		{
			get { return ((ISupplierViewer)ActiveCollector.ActiveViewer).Address; }
			set { ((ISupplierViewer)ActiveCollector.ActiveViewer).Address = value; }
		}

		public object Email
		{
			get { return ((ISupplierViewer)ActiveCollector.ActiveViewer).Email; }
			set { ((ISupplierViewer)ActiveCollector.ActiveViewer).Email = value; }
		}

		public object IdentificationCardType
		{
			get { return ((ISupplierViewer)ActiveCollector.ActiveViewer).IdentificationCardType; }
			set { ((ISupplierViewer)ActiveCollector.ActiveViewer).IdentificationCardType = value; }
		}

		public object IdentificationCardNumber
		{
			get { return ((ISupplierViewer)ActiveCollector.ActiveViewer).IdentificationCardNumber; }
			set { ((ISupplierViewer)ActiveCollector.ActiveViewer).IdentificationCardNumber = value; }
		}

		public object IdentificationCardIssueDate
		{
			get { return ((ISupplierViewer)ActiveCollector.ActiveViewer).IdentificationCardIssueDate; }
			set { ((ISupplierViewer)ActiveCollector.ActiveViewer).IdentificationCardIssueDate = value; }
		}

		public object IdentificationCardExpirationDate
		{
			get { return ((ISupplierViewer)ActiveCollector.ActiveViewer).IdentificationCardExpirationDate; }
			set { ((ISupplierViewer)ActiveCollector.ActiveViewer).IdentificationCardExpirationDate = value; }
		}

		#endregion
	}
}
