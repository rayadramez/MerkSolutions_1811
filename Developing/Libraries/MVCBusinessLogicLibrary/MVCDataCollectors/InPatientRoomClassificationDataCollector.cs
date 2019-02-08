using System;
using System.Collections.Generic;
using ApplicationConfiguration;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCDataCollectors
{
	public class InPatientRoomClassificationDataCollector<TEntity> : AbstractDataCollector<TEntity>, IInPatientRoomClassificationViewer
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

			ID = ((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			((InPatientRoomClassification_cu)ActiveDBItem).DBCommonTransactionType =
				((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).CommonTransactionType;
			((InPatientRoomClassification_cu) ActiveDBItem).Name_P =
				((IInPatientRoomClassificationViewer) ActiveCollector.ActiveViewer).NameP.ToString();
			((InPatientRoomClassification_cu) ActiveDBItem).Name_S =
				((IInPatientRoomClassificationViewer) ActiveCollector.ActiveViewer).NameS.ToString();
			((InPatientRoomClassification_cu)ActiveDBItem).InPatientRoomType_P_ID =
				Convert.ToInt32(((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).InPatientRoomType);

			if (((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).Description != null)
				((InPatientRoomClassification_cu)ActiveDBItem).Description =
					((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).Description.ToString();
			else
				((InPatientRoomClassification_cu)ActiveDBItem).Description = null;

			if (((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).InternalCode != null)
				((InPatientRoomClassification_cu)ActiveDBItem).InternalCode =
					((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).InternalCode.ToString();
			else
				((InPatientRoomClassification_cu)ActiveDBItem).InternalCode = null;

			if (((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).ShortName != null)
				((InPatientRoomClassification_cu)ActiveDBItem).ShortName =
					((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).ShortName.ToString();
			else
				((InPatientRoomClassification_cu)ActiveDBItem).ShortName = null;

			if (((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).HasMainPatientPricing)
			{
				InPatientRoomClassification_InPatientAdmissionPricingType_cu pricingObject =
					DBCommon.CreateNewDBEntity<InPatientRoomClassification_InPatientAdmissionPricingType_cu>();
				if (pricingObject != null)
				{
					if (((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).PricePerDay_MainPatient != null &&
						Convert.ToDouble(((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).PricePerDay_MainPatient) > 0)
					{
						pricingObject.PricePerDay =
							Convert.ToDouble(((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).PricePerDay_MainPatient);
					}
					if (((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).MinimumAddmissionAmount_MainPatient != null &&
						Convert.ToDouble(((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).MinimumAddmissionAmount_MainPatient) > 0)
					{
						pricingObject.MinimumAddmissionAmount =
							Convert.ToDouble(((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).MinimumAddmissionAmount_MainPatient);
					}

					pricingObject.InPatientAddmissionPricingType_P_ID = (int)DB_InPatientAddmissionPricingType.MainPatientPricing;
				}

				((InPatientRoomClassification_cu) ActiveDBItem).InPatientRoomClassification_InPatientAdmissionPricingType_cu.Add(
					pricingObject);
			}

			if (((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).HasCompanionPricing)
			{
				InPatientRoomClassification_InPatientAdmissionPricingType_cu pricingObject =
					DBCommon.CreateNewDBEntity<InPatientRoomClassification_InPatientAdmissionPricingType_cu>();
				if (pricingObject != null)
				{
					if (((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).PricePerDay_CompanionPatient != null &&
						Convert.ToDouble(((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).PricePerDay_CompanionPatient) > 0)
					{
						pricingObject.PricePerDay =
							Convert.ToDouble(((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).PricePerDay_CompanionPatient);
					}
					if (((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).MinimumAddmissionAmount_CompanionPatient != null &&
						Convert.ToDouble(((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).MinimumAddmissionAmount_CompanionPatient) > 0)
					{
						pricingObject.MinimumAddmissionAmount =
							Convert.ToDouble(((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).MinimumAddmissionAmount_CompanionPatient);
					}

					pricingObject.InPatientAddmissionPricingType_P_ID = (int)DB_InPatientAddmissionPricingType.CompanionPricing;
				}

				((InPatientRoomClassification_cu) ActiveDBItem).InPatientRoomClassification_InPatientAdmissionPricingType_cu.Add(
					pricingObject);
			}

			if (UserID != null)
				((InPatientRoomClassification_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((InPatientRoomClassification_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IStationPointStageViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((InPatientRoomClassification_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { throw new System.NotImplementedException(); }
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
			((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).NameP = null;
			((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).NameS = null;
			((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).InPatientRoomType = null;
			((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).Description = null;
			((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).InternalCode = null;
			((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).ShortName = null;
		}

		public override void FillControls()
		{
			throw new System.NotImplementedException();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<InPatientRoomClassification_cu>();
				((IInPatientRoomClassificationViewer) ActiveCollector.ActiveViewer).CommonTransactionType =
					DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveDBItem == null)
				return false;

			return ((InPatientRoomClassification_cu)ActiveCollector.ActiveDBItem).SaveChanges();
		}

		public override void Edit(IDBCommon entity)
		{
			((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.UpdateExisting;
			((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).NameP = ((InPatientRoomClassification_cu)entity).Name_P;
			((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).NameS = ((InPatientRoomClassification_cu)entity).Name_S;
			((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).ShortName = ((InPatientRoomClassification_cu)entity).ShortName;
			((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).Description = ((InPatientRoomClassification_cu)entity).Description;
			((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).InternalCode = ((InPatientRoomClassification_cu)entity).InternalCode;
			((IInPatientRoomClassificationViewer) ActiveCollector.ActiveViewer).InPatientRoomType =
				((InPatientRoomClassification_cu) entity).InPatientRoomType_P_ID;

			((IInPatientRoomClassificationViewer)ActiveCollector.ActiveViewer).ID = ((InPatientRoomClassification_cu)entity).ID;
			ActiveCollector.ActiveDBItem.ID = ((InPatientRoomClassification_cu)entity).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((InPatientRoomClassification_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IInPatientRoomClassificationViewer

		public object NameP { get; set; }
		public object NameS { get; set; }
		public object Description { get; set; }
		public object ShortName { get; set; }
		public object InternalCode { get; set; }
		public object InPatientRoomType { get; set; }
		public bool HasMainPatientPricing { get; private set; }
		public object PricePerDay_MainPatient { get; set; }
		public object MinimumAddmissionAmount_MainPatient { get; set; }
		public bool HasCompanionPricing { get; private set; }
		public object PricePerDay_CompanionPatient { get; set; }
		public object MinimumAddmissionAmount_CompanionPatient { get; set; }

		#endregion
	}
}
