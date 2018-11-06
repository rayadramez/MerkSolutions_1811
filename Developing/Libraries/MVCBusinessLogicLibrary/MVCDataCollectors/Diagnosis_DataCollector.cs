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
	public class Diagnosis_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IDiagnosis_Viewer
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

			ID = ((IDiagnosis_Viewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Name_P != null)
				((Diagnosis_cu)ActiveDBItem).Name_P = Name_P.ToString();

			if (Name_S != null)
				((Diagnosis_cu)ActiveDBItem).Name_S = Name_S.ToString();

			if (Abbreviation != null)
				((Diagnosis_cu)ActiveDBItem).Abbreviation = Abbreviation.ToString();

			if (Description != null)
				((Diagnosis_cu)ActiveDBItem).Description = Description.ToString();

			if (IsDoctorRelated != null && Convert.ToBoolean(IsDoctorRelated))
			{
				if (DoctorID == null)
				{
					MessageToView = "You should choose the Doctor";
					return false;
				}

				((Diagnosis_cu)ActiveDBItem).IsDoctorRelated = true;
			}

			if (UserID != null)
				((Diagnosis_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((Diagnosis_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IDiagnosis_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((Diagnosis_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IDiagnosis_Viewer)ActiveCollector.ActiveViewer).RelatedViewers;

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
			List<Diagnosis_cu> list = Diagnosis_cu.ItemsList;
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<Diagnosis_cu>();

				((IDiagnosis_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
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

			if (((Diagnosis_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((Diagnosis_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
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

			Doctor_Diagnosis_cu bridge = DBCommon.CreateNewDBEntity<Doctor_Diagnosis_cu>();
			bridge.Diagnosis_CU_ID = ((Diagnosis_cu)ActiveCollector.ActiveDBItem).ID;
			bridge.Doctor_CU_ID = Convert.ToInt32(DoctorID);
			if (UserID != null)
				bridge.InsertedBy = Convert.ToInt32(UserID);

			bridge.IsOnDuty = true;
			switch (((IDiagnosis_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType)
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
			Name_P = ((Diagnosis_cu)ActiveDBItem).Name_P;
			Name_S = ((Diagnosis_cu)ActiveDBItem).Name_S;

			((IDiagnosis_Viewer)ActiveCollector.ActiveViewer).ID = ((Diagnosis_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((Diagnosis_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((Diagnosis_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IDiagnosis_Viewer

		public object Name_P
		{
			get { return ((IDiagnosis_Viewer)ActiveCollector.ActiveViewer).Name_P; }
			set { ((IDiagnosis_Viewer)ActiveCollector.ActiveViewer).Name_P = value; }
		}

		public object Name_S
		{
			get { return ((IDiagnosis_Viewer)ActiveCollector.ActiveViewer).Name_S; }
			set { ((IDiagnosis_Viewer)ActiveCollector.ActiveViewer).Name_S = value; }
		}

		public object Abbreviation
		{
			get { return ((IDiagnosis_Viewer)ActiveCollector.ActiveViewer).Abbreviation; }
			set { ((IDiagnosis_Viewer)ActiveCollector.ActiveViewer).Abbreviation = value; }
		}

		public object Description
		{
			get { return ((IDiagnosis_Viewer)ActiveCollector.ActiveViewer).Description; }
			set { ((IDiagnosis_Viewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		public object IsDoctorRelated
		{
			get { return ((IDiagnosis_Viewer)ActiveCollector.ActiveViewer).IsDoctorRelated; }
			set { ((IDiagnosis_Viewer)ActiveCollector.ActiveViewer).IsDoctorRelated = value; }
		}

		public object DoctorID
		{
			get { return ((IDiagnosis_Viewer)ActiveCollector.ActiveViewer).DoctorID; }
			set { ((IDiagnosis_Viewer)ActiveCollector.ActiveViewer).DoctorID = value; }
		}

		#endregion
	}
}
