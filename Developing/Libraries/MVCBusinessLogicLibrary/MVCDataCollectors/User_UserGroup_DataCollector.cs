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
	public class User_UserGroup_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IUser_UserGroup_Viewer
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

			ID = ((IUser_UserGroup_Viewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (List_User_UserGroup == null || List_User_UserGroup.Count == 0)
				return false;

			RelatedViewers = ((IUser_UserGroup_Viewer)ActiveCollector.ActiveViewer).RelatedViewers;

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
			List<User_UserGroup_cu> list = User_UserGroup_cu.ItemsList.FindAll(item => item.IsOnDuty);
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<User_UserGroup_cu>();
				((IUser_UserGroup_Viewer) ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			foreach (User_UserGroup_cu user_UserGroup in List_User_UserGroup)
			{
				((User_UserGroup_cu)ActiveDBItem).User_CU_ID = user_UserGroup.User_CU_ID;
				((User_UserGroup_cu)ActiveDBItem).UserGroup_CU_ID = user_UserGroup.UserGroup_CU_ID;

				if (UserID != null)
					((User_UserGroup_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

				((User_UserGroup_cu)ActiveDBItem).IsOnDuty = true;
				switch (((IUser_UserGroup_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType)
				{
					case DB_CommonTransactionType.DeleteExisting:
						((User_UserGroup_cu)ActiveDBItem).IsOnDuty = false;
						break;
				}

				((User_UserGroup_cu)ActiveCollector.ActiveDBItem).SaveChanges();
			}

			((User_UserGroup_cu)ActiveCollector.ActiveDBItem).LoadItemsList();

			return true;
		}

		public override void Edit(IDBCommon entity)
		{
			//NameP = ((UserGroup_cu)ActiveDBItem).Name_P;
			//NameS = ((UserGroup_cu)ActiveDBItem).Name_S;
			//InternalCode = ((UserGroup_cu)ActiveDBItem).InternalCode;
			//Description = ((UserGroup_cu)ActiveDBItem).Description;

			//((IUser_UserGroup_Viewer)ActiveCollector.ActiveViewer).ID = ((UserGroup_cu)ActiveDBItem).ID;
			//ActiveCollector.ActiveDBItem.ID = ((UserGroup_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((User_UserGroup_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IUser_UserGroup_Viewer

		public List<User_UserGroup_cu> List_User_UserGroup
		{
			get { return ((IUser_UserGroup_Viewer)ActiveCollector.ActiveViewer).List_User_UserGroup; }
			set { ((IUser_UserGroup_Viewer)ActiveCollector.ActiveViewer).List_User_UserGroup = value; }
		}

		#endregion
	}
}
