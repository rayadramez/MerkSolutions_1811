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
	public class ServicePrice_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IServicePrice_EditorViewer
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

			ID = ((IServicePrice_EditorViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Service_CU_ID != null)
				((ServicePrice_cu) ActiveDBItem).Service_CU_ID = Convert.ToInt32(Service_CU_ID);

			if (ServiceCategory_CU_ID != null)
				((ServicePrice_cu)ActiveDBItem).ServiceCategory_CU_ID = Convert.ToInt32(ServiceCategory_CU_ID);

			if (Doctor_CU_ID != null)
				((ServicePrice_cu)ActiveDBItem).Service_CU_ID = Convert.ToInt32(Doctor_CU_ID);

			if (DoctorCategory_CU_ID != null)
				((ServicePrice_cu)ActiveDBItem).DoctorCategory_CU_ID = Convert.ToInt32(DoctorCategory_CU_ID);

			if (InsuranceCarrierID != null && InsuranceLevelID != null)
			{
				InsuranceCarrier_InsuranceLevel_cu insuranceBridge =
					InsuranceCarrier_InsuranceLevel_cu.ItemsList.Find(
						item =>
							Convert.ToInt32(item.InsuranceCarrier_CU_ID).Equals(Convert.ToInt32(InsuranceCarrierID)) &&
							Convert.ToInt32(item.InsuranceLevel_CU_ID).Equals(Convert.ToInt32(InsuranceLevelID)));
				if (insuranceBridge != null)
					((ServicePrice_cu) ActiveDBItem).InsuranceCarrier_InsuranceLevel_CU_ID = Convert.ToInt32(insuranceBridge.ID);
			}

			if (InsurancePrice != null)
				((ServicePrice_cu)ActiveDBItem).InsurancePrice = Convert.ToDouble(InsurancePrice);

			if (Price != null)
				((ServicePrice_cu)ActiveDBItem).Price = Convert.ToDouble(Price);

			if (UserID != null)
				((ServicePrice_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((ServicePrice_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IServicePrice_EditorViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((ServicePrice_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IServicePrice_EditorViewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.User_Viewer; }
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
			List<ServicePrice_cu> list = ServicePrice_cu.ItemsList;
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<ServicePrice_cu>();

				((IServicePrice_EditorViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((ServicePrice_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((ServicePrice_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			Service_CU_ID = ((ServicePrice_cu)ActiveDBItem).Service_CU_ID;
			ServiceCategory_CU_ID = ((ServicePrice_cu)ActiveDBItem).ServiceCategory_CU_ID;
			Doctor_CU_ID = ((ServicePrice_cu)ActiveDBItem).Doctor_CU_ID;
			Doctor_CU_ID = ((ServicePrice_cu)ActiveDBItem).Doctor_CU_ID;
			DoctorCategory_CU_ID = ((ServicePrice_cu)ActiveDBItem).DoctorCategory_CU_ID;
			Price = ((ServicePrice_cu)ActiveDBItem).Price;
			InsurancePrice = ((ServicePrice_cu)ActiveDBItem).InsurancePrice;
			if (((ServicePrice_cu) ActiveDBItem).InsuranceCarrier_InsuranceLevel_CU_ID != null)
			{
				InsuranceCarrier_InsuranceLevel_cu insuranceBridge =
					InsuranceCarrier_InsuranceLevel_cu.ItemsList.Find(
						item =>
							Convert.ToInt32(item.ID)
								.Equals(Convert.ToInt32(((ServicePrice_cu) ActiveDBItem).InsuranceCarrier_InsuranceLevel_CU_ID)));
				if (insuranceBridge != null)
				{
					InsuranceCarrierID = insuranceBridge.InsuranceLevel_CU_ID;
					InsuranceLevelID = insuranceBridge.InsuranceLevel_CU_ID;
				}
			}

			((IServicePrice_EditorViewer)ActiveCollector.ActiveViewer).ID = ((ServicePrice_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((ServicePrice_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((ServicePrice_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IServicePrice_EditorViewer

		public object Service_CU_ID
		{
			get { return ((IServicePrice_EditorViewer)ActiveCollector.ActiveViewer).Service_CU_ID; }
			set { ((IServicePrice_EditorViewer)ActiveCollector.ActiveViewer).Service_CU_ID = value; }
		}

		public object ServiceCategory_CU_ID
		{
			get { return ((IServicePrice_EditorViewer)ActiveCollector.ActiveViewer).ServiceCategory_CU_ID; }
			set { ((IServicePrice_EditorViewer)ActiveCollector.ActiveViewer).ServiceCategory_CU_ID = value; }
		}

		public object Doctor_CU_ID
		{
			get { return ((IServicePrice_EditorViewer)ActiveCollector.ActiveViewer).Doctor_CU_ID; }
			set { ((IServicePrice_EditorViewer)ActiveCollector.ActiveViewer).Doctor_CU_ID = value; }
		}

		public object DoctorCategory_CU_ID
		{
			get { return ((IServicePrice_EditorViewer)ActiveCollector.ActiveViewer).DoctorCategory_CU_ID; }
			set { ((IServicePrice_EditorViewer)ActiveCollector.ActiveViewer).DoctorCategory_CU_ID = value; }
		}

		public object Price
		{
			get { return ((IServicePrice_EditorViewer)ActiveCollector.ActiveViewer).Price; }
			set { ((IServicePrice_EditorViewer)ActiveCollector.ActiveViewer).Price = value; }
		}

		public object InsuranceCarrierID
		{
			get { return ((IServicePrice_EditorViewer)ActiveCollector.ActiveViewer).InsuranceCarrierID; }
			set { ((IServicePrice_EditorViewer)ActiveCollector.ActiveViewer).InsuranceCarrierID = value; }
		}

		public object InsuranceLevelID
		{
			get { return ((IServicePrice_EditorViewer)ActiveCollector.ActiveViewer).InsuranceLevelID; }
			set { ((IServicePrice_EditorViewer)ActiveCollector.ActiveViewer).InsuranceLevelID = value; }
		}

		public object InsurancePrice
		{
			get { return ((IServicePrice_EditorViewer)ActiveCollector.ActiveViewer).InsurancePrice; }
			set { ((IServicePrice_EditorViewer)ActiveCollector.ActiveViewer).InsurancePrice = value; }
		}

		#endregion
	}
}
