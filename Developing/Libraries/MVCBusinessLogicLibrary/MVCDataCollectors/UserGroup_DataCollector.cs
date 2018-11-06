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
	public class UserGroup_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IUserGroupViewer
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

			ID = ((IUserGroupViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (NameP != null)
				((UserGroup_cu) ActiveDBItem).Name_P = NameP.ToString();

			if (NameS != null)
				((UserGroup_cu)ActiveDBItem).Name_S = NameS.ToString();

			if (InternalCode != null)
				((UserGroup_cu)ActiveDBItem).InternalCode = InternalCode.ToString();

			if (Description != null)
				((UserGroup_cu)ActiveDBItem).Description = Description.ToString();

			if (UserID != null)
				((UserGroup_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((UserGroup_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IUserGroupViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((UserGroup_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IUserGroupViewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.UserGroup_Viewer; }
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
			List<UserGroup_cu> list = UserGroup_cu.ItemsList.FindAll(item => item.IsOnDuty);
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<UserGroup_cu>();
				((IUserGroupViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool ValidateBeforeSave(ref string message)
		{
			if (NameP == null)
			{
				MessageToView = "يجـــب كتـابـــة الإســـم الأول";
				return false;
			}

			return true;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((UserGroup_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((UserGroup_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			NameP = ((UserGroup_cu)ActiveDBItem).Name_P;
			NameS = ((UserGroup_cu)ActiveDBItem).Name_S;
			InternalCode = ((UserGroup_cu)ActiveDBItem).InternalCode;
			Description = ((UserGroup_cu)ActiveDBItem).Description;

			((IUserGroupViewer)ActiveCollector.ActiveViewer).ID = ((UserGroup_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((UserGroup_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((UserGroup_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IUserGroupViewer

		public object NameP
		{
			get { return ((IUserGroupViewer)ActiveCollector.ActiveViewer).NameP; }
			set { ((IUserGroupViewer)ActiveCollector.ActiveViewer).NameP = value; }
		}

		public object NameS
		{
			get { return ((IUserGroupViewer)ActiveCollector.ActiveViewer).NameS; }
			set { ((IUserGroupViewer)ActiveCollector.ActiveViewer).NameS = value; }
		}

		public object InternalCode
		{
			get { return ((IUserGroupViewer)ActiveCollector.ActiveViewer).InternalCode; }
			set { ((IUserGroupViewer)ActiveCollector.ActiveViewer).InternalCode = value; }
		}

		public object Description
		{
			get { return ((IUserGroupViewer)ActiveCollector.ActiveViewer).Description; }
			set { ((IUserGroupViewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		#endregion
	}
}
