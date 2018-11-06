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
	public class User_Application_DataCollector<TEntity> : AbstractDataCollector<TEntity>,
		IUser_Application_Viewer
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

			ID = ((IUser_Application_Viewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (List_User_Application == null || List_User_Application.Count == 0)
				return false;

			foreach (User_Application_cu user_Application in List_User_Application)
			{
				((User_Application_cu)ActiveDBItem).User_CU_ID = user_Application.User_CU_ID;
				((User_Application_cu)ActiveDBItem).Application_P_ID = user_Application.Application_P_ID;
			}

			if (UserID != null)
				((User_Application_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((User_Application_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IUser_Application_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((User_Application_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IUser_Application_Viewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.User_Application_Viewer; }
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
			List<User_Application_cu> list = User_Application_cu.ItemsList.FindAll(item => item.IsOnDuty);
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<User_Application_cu>();
				((IUser_Application_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((User_Application_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((User_Application_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			//NameP = ((User_Application_cu)ActiveDBItem).Name_P;
			//NameS = ((User_Application_cu)ActiveDBItem).Name_S;
			//InternalCode = ((User_Application_cu)ActiveDBItem).InternalCode;
			//Description = ((User_Application_cu)ActiveDBItem).Description;

			//((IUser_Application_Viewer)ActiveCollector.ActiveViewer).ID = ((User_Application_cu)ActiveDBItem).ID;
			//ActiveCollector.ActiveDBItem.ID = ((User_Application_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((User_Application_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IUser_Application_Viewer

		public List<User_Application_cu> List_User_Application
		{
			get { return ((IUser_Application_Viewer)ActiveCollector.ActiveViewer).List_User_Application; }
			set { ((IUser_Application_Viewer)ActiveCollector.ActiveViewer).List_User_Application = value; }
		}

		#endregion
	}
}
