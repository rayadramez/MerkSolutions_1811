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
	public class DiagnosisCategory_Diagnosis_DataCollector<TEntity> : AbstractDataCollector<TEntity>,
		IDiagnosisCategory_Diagnosis_Viewer
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

			ID = ((IDiagnosisCategory_Diagnosis_Viewer) ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (List_DiagnosisCategory_Diagnosis == null || List_DiagnosisCategory_Diagnosis.Count == 0)
				return false;

			RelatedViewers = ((IDiagnosisCategory_Diagnosis_Viewer) ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int) ViewerName.UserGroup_Viewer; }
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
			List<DiagnosisCategory_Diagnosis_cu> list =
				DiagnosisCategory_Diagnosis_cu.ItemsList.FindAll(item => item.IsOnDuty);
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<DiagnosisCategory_Diagnosis_cu>();
				((IDiagnosisCategory_Diagnosis_Viewer) ActiveCollector.ActiveViewer).CommonTransactionType =
					DB_CommonTransactionType.SaveNew;
				return true;
			}

			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			foreach (DiagnosisCategory_Diagnosis_cu bridge in List_DiagnosisCategory_Diagnosis)
			{
				((DiagnosisCategory_Diagnosis_cu) ActiveDBItem).DiagnosisCategory_CU_ID =
					bridge.DiagnosisCategory_CU_ID;
				((DiagnosisCategory_Diagnosis_cu) ActiveDBItem).Diagnosis_CU_ID = bridge.Diagnosis_CU_ID;

				if (UserID != null)
					((DiagnosisCategory_Diagnosis_cu) ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

				((DiagnosisCategory_Diagnosis_cu) ActiveDBItem).IsOnDuty = true;
				switch (((IDiagnosisCategory_Diagnosis_Viewer) ActiveCollector.ActiveViewer).CommonTransactionType)
				{
					case DB_CommonTransactionType.DeleteExisting:
						((DiagnosisCategory_Diagnosis_cu) ActiveDBItem).IsOnDuty = false;
						break;
				}

				((DiagnosisCategory_Diagnosis_cu) ActiveCollector.ActiveDBItem).SaveChanges();
			}

			((DiagnosisCategory_Diagnosis_cu) ActiveCollector.ActiveDBItem).LoadItemsList();

			return true;
		}

		public override void Edit(IDBCommon entity)
		{
			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((DiagnosisCategory_Diagnosis_cu) entity).RemoveItem();
		}

		#endregion

		#region Implementation of IDiagnosisCategory_Diagnosis_Viewer

		public List<DiagnosisCategory_Diagnosis_cu> List_DiagnosisCategory_Diagnosis
		{
			get
			{
				return ((IDiagnosisCategory_Diagnosis_Viewer) ActiveCollector.ActiveViewer)
					.List_DiagnosisCategory_Diagnosis;
			}
			set
			{
				((IDiagnosisCategory_Diagnosis_Viewer) ActiveCollector.ActiveViewer).List_DiagnosisCategory_Diagnosis =
					value;
			}
		}

		public object DiagnosisCategory_ID
		{
			get { return ((IDiagnosisCategory_Diagnosis_Viewer) ActiveCollector.ActiveViewer).DiagnosisCategory_ID; }
			set { ((IDiagnosisCategory_Diagnosis_Viewer) ActiveCollector.ActiveViewer).DiagnosisCategory_ID = value; }
		}

		public object Diagnosis_ID
		{
			get { return ((IDiagnosisCategory_Diagnosis_Viewer) ActiveCollector.ActiveViewer).Diagnosis_ID; }
			set { ((IDiagnosisCategory_Diagnosis_Viewer) ActiveCollector.ActiveViewer).Diagnosis_ID = value; }
		}

		#endregion
	}
}
