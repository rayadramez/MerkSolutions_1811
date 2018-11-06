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
	public class Medication_Dose_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IMedication_Dose_Viewer
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

			ID = ((IMedication_Dose_Viewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (List_Medication_Dose == null || List_Medication_Dose.Count == 0)
				return false;

			RelatedViewers = ((IMedication_Dose_Viewer)ActiveCollector.ActiveViewer).RelatedViewers;

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
			List<Medication_Dose_cu> list = Medication_Dose_cu.ItemsList.FindAll(item => item.IsOnDuty);
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<Medication_Dose_cu>();
				((IMedication_Dose_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			foreach (Medication_Dose_cu user_UserGroup in List_Medication_Dose)
			{
				((Medication_Dose_cu)ActiveDBItem).Medication_CU_ID = user_UserGroup.Medication_CU_ID;
				((Medication_Dose_cu)ActiveDBItem).Dose_CU_ID = user_UserGroup.Dose_CU_ID;

				if (UserID != null)
					((Medication_Dose_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

				((Medication_Dose_cu)ActiveDBItem).IsOnDuty = true;
				switch (((IMedication_Dose_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType)
				{
					case DB_CommonTransactionType.DeleteExisting:
						((Medication_Dose_cu)ActiveDBItem).IsOnDuty = false;
						break;
				}

				((Medication_Dose_cu)ActiveCollector.ActiveDBItem).SaveChanges();
			}

			((Medication_Dose_cu)ActiveCollector.ActiveDBItem).LoadItemsList();

			return true;
		}

		public override void Edit(IDBCommon entity)
		{
			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((Medication_Dose_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IMedication_Dose_Viewer

		public List<Medication_Dose_cu> List_Medication_Dose
		{
			get { return ((IMedication_Dose_Viewer)ActiveCollector.ActiveViewer).List_Medication_Dose; }
			set { ((IMedication_Dose_Viewer)ActiveCollector.ActiveViewer).List_Medication_Dose = value; }
		}

		public object Medication_CU_ID
		{
			get { return ((IMedication_Dose_Viewer)ActiveCollector.ActiveViewer).Medication_CU_ID; }
			set { ((IMedication_Dose_Viewer)ActiveCollector.ActiveViewer).Medication_CU_ID = value; }
		}

		public object Dose_CU_ID
		{
			get { return ((IMedication_Dose_Viewer)ActiveCollector.ActiveViewer).Dose_CU_ID; }
			set { ((IMedication_Dose_Viewer)ActiveCollector.ActiveViewer).Dose_CU_ID = value; }
		}

		#endregion
	}
}
