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
	public class Medication_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IMedication_Viewer
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

			ID = ((IMedication_Viewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Name_P != null)
				((Medication_cu)ActiveDBItem).Name_P = Name_P.ToString();

			if (Name_S != null)
				((Medication_cu)ActiveDBItem).Name_S = Name_S.ToString();

			if (Description != null)
				((Medication_cu)ActiveDBItem).Description = Description.ToString();

			if (MedicationCategory_CU_ID != null)
				((Medication_cu)ActiveDBItem).MedicationCategory_CU_ID = Convert.ToInt32(MedicationCategory_CU_ID);

			if (UserID != null)
				((Medication_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((Medication_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IMedication_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((Medication_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IMedication_Viewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.CashBoxViewer; }
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
			List<Medication_cu> list = Medication_cu.ItemsList;
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<Medication_cu>();

				((IMedication_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool ValidateBeforeSave(ref string message)
		{
			if (Name_P == null)
			{
				MessageToView = "يجـــب كتـابـــة الإســـم الأول";
				return false;
			}

			if (MedicationCategory_CU_ID == null)
			{
				MessageToView = "يجـــب إختيــــــار تصنيــــــف الأدويـــــــــة";
				return false;
			}

			return true;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((Medication_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((Medication_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			Name_P = ((Medication_cu)ActiveDBItem).Name_P;
			Name_S = ((Medication_cu)ActiveDBItem).Name_S;
			MedicationCategory_CU_ID = ((Medication_cu)ActiveDBItem).MedicationCategory_CU_ID;
			Description = ((Medication_cu)ActiveDBItem).Description;

			((IMedication_Viewer)ActiveCollector.ActiveViewer).ID = ((Medication_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((Medication_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((Medication_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IMedicationViewer

		public object MedicationCategory_CU_ID
		{
			get { return ((IMedication_Viewer)ActiveCollector.ActiveViewer).MedicationCategory_CU_ID; }
			set { ((IMedication_Viewer)ActiveCollector.ActiveViewer).MedicationCategory_CU_ID = value; }
		}

		public object Name_P
		{
			get { return ((IMedication_Viewer)ActiveCollector.ActiveViewer).Name_P; }
			set { ((IMedication_Viewer)ActiveCollector.ActiveViewer).Name_P = value; }
		}

		public object Name_S
		{
			get { return ((IMedication_Viewer)ActiveCollector.ActiveViewer).Name_S; }
			set { ((IMedication_Viewer)ActiveCollector.ActiveViewer).Name_S = value; }
		}

		public object Description
		{
			get { return ((IMedication_Viewer)ActiveCollector.ActiveViewer).Description; }
			set { ((IMedication_Viewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		#endregion
	}
}
