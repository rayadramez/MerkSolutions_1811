using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationConfiguration;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCDataCollectors
{
	public class InsurancePolicyDataCollector<TEntity> : AbstractDataCollector<TEntity>, IInsurancePolicyViewer
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

			ID = ((IInsurancePolicyViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			((InsuranceCarrier_InsuranceLevel_cu) ActiveDBItem).DBCommonTransactionType =
				((IInsurancePolicyViewer) ActiveCollector.ActiveViewer).CommonTransactionType;
			if (((IInsurancePolicyViewer) ActiveCollector.ActiveViewer).InsuranceCarrierID != null)
				((InsuranceCarrier_InsuranceLevel_cu) ActiveDBItem).InsuranceCarrier_CU_ID =
					Convert.ToInt32(((IInsurancePolicyViewer) ActiveCollector.ActiveViewer).InsuranceCarrierID);
			if (((IInsurancePolicyViewer)ActiveCollector.ActiveViewer).InsuranceLevelID != null)
				((InsuranceCarrier_InsuranceLevel_cu)ActiveDBItem).InsuranceLevel_CU_ID =
					Convert.ToInt32(((IInsurancePolicyViewer)ActiveCollector.ActiveViewer).InsuranceLevelID);
			if (((IInsurancePolicyViewer)ActiveCollector.ActiveViewer).PatientMaxAmount != null)
				((InsuranceCarrier_InsuranceLevel_cu)ActiveDBItem).PatientMaxAmount =
					Convert.ToInt32(((IInsurancePolicyViewer)ActiveCollector.ActiveViewer).PatientMaxAmount);

			if (UserID != null)
				((InsuranceCarrier_InsuranceLevel_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((InsuranceCarrier_InsuranceLevel_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IStationPointStageViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((InsuranceCarrier_InsuranceLevel_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return ViewerName.InsurancePolicy_Viewer; }
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
			get { return ActiveViewer.GridXML; }
		}

		public override List<IViewer> RelatedViewers { get; set; }
		public override void ClearControls()
		{
			((IInsurancePolicyViewer) ActiveCollector.ActiveViewer).InsuranceCarrierID = null;
			((IInsurancePolicyViewer) ActiveCollector.ActiveViewer).InsuranceLevelID = null;
			((IInsurancePolicyViewer) ActiveCollector.ActiveViewer).InsurancePercetnage = 0;
			((IInsurancePolicyViewer) ActiveCollector.ActiveViewer).PatientMaxAmount = null;
		}

		public override void FillControls()
		{
			throw new System.NotImplementedException();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<InsuranceCarrier_InsuranceLevel_cu>();
				((IInsurancePolicyViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveDBItem == null)
				return false;

			if (((InsuranceCarrier_InsuranceLevel_cu) ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((InsuranceCarrier_InsuranceLevel_cu) ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			((IInsurancePolicyViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.UpdateExisting;
			((IInsurancePolicyViewer) ActiveCollector.ActiveViewer).InsuranceCarrierID =
				((InsuranceCarrier_InsuranceLevel_cu) entity).InsuranceCarrier_CU_ID;
			((IInsurancePolicyViewer) ActiveCollector.ActiveViewer).InsuranceLevelID =
				((InsuranceCarrier_InsuranceLevel_cu) entity).InsuranceLevel_CU_ID;
			((IInsurancePolicyViewer) ActiveCollector.ActiveViewer).InsurancePercetnage =
				((InsuranceCarrier_InsuranceLevel_cu) entity).InsurancePercentage;
			((IInsurancePolicyViewer) ActiveCollector.ActiveViewer).PatientMaxAmount =
				((InsuranceCarrier_InsuranceLevel_cu) entity).PatientMaxAmount;

			((IInsurancePolicyViewer)ActiveCollector.ActiveViewer).ID = ((InsuranceCarrier_InsuranceLevel_cu)entity).ID;
			ActiveCollector.ActiveDBItem.ID = ((InsuranceCarrier_InsuranceLevel_cu)entity).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((InsuranceCarrier_InsuranceLevel_cu)entity).RemoveItem();
		}

		public override IEnumerable<TEntity> GetItemsList()
		{
			List<InsuranceCarrier_InsuranceLevel_cu> list =
				DBCommon.GetItemsList<InsuranceCarrier_InsuranceLevel_cu>().ToList().ToList();

			return (IEnumerable<TEntity>)list;
		}

		#endregion

		#region Implementation of IInsurancePolicyViewer

		public object InsuranceCarrierID { get; set; }
		public object InsuranceLevelID { get; set; }
		public object InsurancePercetnage { get; set; }
		public object PatientMaxAmount { get; set; }

		#endregion
	}
}
