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
	public class UserGroup_Application_DataCollector<TEntity> : AbstractDataCollector<TEntity>,
		IUserGroup_Application_Viewer
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

			ID = ((IUserGroup_Application_Viewer) ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (List_UserGroup_Application == null || List_UserGroup_Application.Count == 0)
				return false;

			foreach (UserGroup_Application_cu userGroup_Application in List_UserGroup_Application)
			{
				((UserGroup_Application_cu) ActiveDBItem).UserGroup_CU_ID = userGroup_Application.UserGroup_CU_ID;
				((UserGroup_Application_cu) ActiveDBItem).Application_P_ID = userGroup_Application.Application_P_ID;
			}

			if (UserID != null)
				((UserGroup_Application_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((UserGroup_Application_cu) ActiveDBItem).IsOnDuty = true;
			switch (((IUserGroup_Application_Viewer) ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((UserGroup_Application_cu) ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IUserGroup_Application_Viewer) ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int) ViewerName.UserGroup_Application_Viewer; }
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
			List<UserGroup_Application_cu> list = UserGroup_Application_cu.ItemsList.FindAll(item => item.IsOnDuty);
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<UserGroup_Application_cu>();
				((IUserGroup_Application_Viewer) ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			foreach (UserGroup_Application_cu userGroup_Application in List_UserGroup_Application)
			{
				((UserGroup_Application_cu)ActiveDBItem).Application_P_ID = userGroup_Application.Application_P_ID;
				((UserGroup_Application_cu)ActiveDBItem).UserGroup_CU_ID = userGroup_Application.UserGroup_CU_ID;

				if (UserID != null)
					((UserGroup_Application_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

				((UserGroup_Application_cu)ActiveDBItem).IsOnDuty = true;
				switch (((IUserGroup_Application_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType)
				{
					case DB_CommonTransactionType.DeleteExisting:
						((UserGroup_Application_cu)ActiveDBItem).IsOnDuty = false;
						break;
				}

				((UserGroup_Application_cu)ActiveCollector.ActiveDBItem).SaveChanges();
			}

			((UserGroup_Application_cu)ActiveCollector.ActiveDBItem).LoadItemsList();

			return true;
		}

		public override void Edit(IDBCommon entity)
		{
			//NameP = ((UserGroup_Application_cu)ActiveDBItem).Name_P;
			//NameS = ((UserGroup_Application_cu)ActiveDBItem).Name_S;
			//InternalCode = ((UserGroup_Application_cu)ActiveDBItem).InternalCode;
			//Description = ((UserGroup_Application_cu)ActiveDBItem).Description;

			//((IUserGroup_Application_Viewer)ActiveCollector.ActiveViewer).ID = ((UserGroup_Application_cu)ActiveDBItem).ID;
			//ActiveCollector.ActiveDBItem.ID = ((UserGroup_Application_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((UserGroup_Application_cu) entity).RemoveItem();
		}

		#endregion

		#region Implementation of IUserGroup_Application_Viewer

		public List<UserGroup_Application_cu> List_UserGroup_Application
		{
			get { return ((IUserGroup_Application_Viewer) ActiveCollector.ActiveViewer).List_UserGroup_Application; }
			set { ((IUserGroup_Application_Viewer) ActiveCollector.ActiveViewer).List_UserGroup_Application = value; }
		}

		#endregion
	}
}
