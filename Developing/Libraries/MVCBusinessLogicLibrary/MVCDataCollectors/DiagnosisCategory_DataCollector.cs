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
	public class DiagnosisCategory_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IDiagnosisCategory_Viewer
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

			ID = ((IDiagnosisCategory_Viewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Name_P != null)
				((DiagnosisCategory_cu)ActiveDBItem).Name_P = Name_P.ToString();

			if (Name_S != null)
				((DiagnosisCategory_cu)ActiveDBItem).Name_S = Name_S.ToString();

			if (Abbreviation != null)
				((DiagnosisCategory_cu)ActiveDBItem).Abbreviation = Abbreviation.ToString();

			if(IsDoctorRelated != null && Convert.ToBoolean(IsDoctorRelated))
			{
				if (DoctorID == null)
				{
					MessageToView = "You should choose the Doctor";
					return false;
				}

				((DiagnosisCategory_cu) ActiveDBItem).IsDoctorRelated = true;
			}

			if (UserID != null)
				((DiagnosisCategory_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((DiagnosisCategory_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IDiagnosisCategory_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((DiagnosisCategory_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IDiagnosisCategory_Viewer)ActiveCollector.ActiveViewer).RelatedViewers;

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
			List<DiagnosisCategory_cu> list = DiagnosisCategory_cu.ItemsList;
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<DiagnosisCategory_cu>();

				((IDiagnosisCategory_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
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

			return true;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((DiagnosisCategory_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((DiagnosisCategory_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override bool AfterSave()
		{
			if (IsDoctorRelated == null || !Convert.ToBoolean(IsDoctorRelated))
				return true;

			if (DoctorID == null)
			{
				MessageToView = "You should choose the Doctor";
				return false;
			}

			Doctor_DiagnosisCategory_cu bridge = DBCommon.CreateNewDBEntity<Doctor_DiagnosisCategory_cu>();
			bridge.DiagnosisCategory_CU_ID = ((DiagnosisCategory_cu) ActiveCollector.ActiveDBItem).ID;
			bridge.Doctor_CU_ID = Convert.ToInt32(DoctorID);
			if (UserID != null)
				bridge.InsertedBy = Convert.ToInt32(UserID);

			bridge.IsOnDuty = true;
			switch (((IDiagnosisCategory_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					bridge.IsOnDuty = false;
					break;
			}

			bridge.IsMerkInsertion = false;
			return bridge.SaveChanges();
		}

		public override void Edit(IDBCommon entity)
		{
			Name_P = ((DiagnosisCategory_cu)ActiveDBItem).Name_P;
			Name_S = ((DiagnosisCategory_cu)ActiveDBItem).Name_S;

			((IDiagnosisCategory_Viewer)ActiveCollector.ActiveViewer).ID = ((DiagnosisCategory_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((DiagnosisCategory_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((DiagnosisCategory_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IDiagnosisCategory_Viewer

		public object Name_P
		{
			get { return ((IDiagnosisCategory_Viewer)ActiveCollector.ActiveViewer).Name_P; }
			set { ((IDiagnosisCategory_Viewer)ActiveCollector.ActiveViewer).Name_P = value; }
		}

		public object Name_S
		{
			get { return ((IDiagnosisCategory_Viewer)ActiveCollector.ActiveViewer).Name_S; }
			set { ((IDiagnosisCategory_Viewer)ActiveCollector.ActiveViewer).Name_S = value; }
		}

		public object Abbreviation
		{
			get { return ((IDiagnosisCategory_Viewer)ActiveCollector.ActiveViewer).Abbreviation; }
			set { ((IDiagnosisCategory_Viewer)ActiveCollector.ActiveViewer).Abbreviation = value; }
		}

		public object IsDoctorRelated
		{
			get { return ((IDiagnosisCategory_Viewer)ActiveCollector.ActiveViewer).IsDoctorRelated; }
			set { ((IDiagnosisCategory_Viewer)ActiveCollector.ActiveViewer).IsDoctorRelated = value; }
		}

		public object DoctorID
		{
			get { return ((IDiagnosisCategory_Viewer)ActiveCollector.ActiveViewer).DoctorID; }
			set { ((IDiagnosisCategory_Viewer)ActiveCollector.ActiveViewer).DoctorID = value; }
		}

		#endregion
	}
}
