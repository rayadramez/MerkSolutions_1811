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
	public class RoleRegistration_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IRoleRegistrationViewer
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

			ID = ((IRoleRegistrationViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (List_RoleRegistration == null || List_RoleRegistration.Count == 0)
				return false;

			foreach (RoleRegistration_cu roleRegistration in List_RoleRegistration)
			{
				((RoleRegistration_cu)ActiveDBItem).User_CU_ID = roleRegistration.User_CU_ID;
				((RoleRegistration_cu)ActiveDBItem).UserGroup_CU_ID = roleRegistration.UserGroup_CU_ID;
				((RoleRegistration_cu)ActiveDBItem).Role_P_ID = roleRegistration.Role_P_ID;
			}

			if (UserID != null)
				((RoleRegistration_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((RoleRegistration_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IRoleRegistrationViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((RoleRegistration_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IRoleRegistrationViewer)ActiveCollector.ActiveViewer).RelatedViewers;

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

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<RoleRegistration_cu>();
				((IRoleRegistrationViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((RoleRegistration_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((RoleRegistration_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			//NameP = ((UserGroup_cu)ActiveDBItem).Name_P;
			//NameS = ((UserGroup_cu)ActiveDBItem).Name_S;
			//InternalCode = ((UserGroup_cu)ActiveDBItem).InternalCode;
			//Description = ((UserGroup_cu)ActiveDBItem).Description;

			//((IRoleRegistrationViewer)ActiveCollector.ActiveViewer).ID = ((UserGroup_cu)ActiveDBItem).ID;
			//ActiveCollector.ActiveDBItem.ID = ((UserGroup_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((RoleRegistration_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IRoleRegistrationViewer

		public List<RoleRegistration_cu> List_RoleRegistration
		{
			get { return ((IRoleRegistrationViewer)ActiveCollector.ActiveViewer).List_RoleRegistration; }
			set { ((IRoleRegistrationViewer)ActiveCollector.ActiveViewer).List_RoleRegistration = value; }
		}

		#endregion
	}
}
