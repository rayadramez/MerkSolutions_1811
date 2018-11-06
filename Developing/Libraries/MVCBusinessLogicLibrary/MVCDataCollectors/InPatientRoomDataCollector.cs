using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationConfiguration;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCDataCollectors
{
	public class InPatientRoomDataCollector<TEntity> : AbstractDataCollector<TEntity>, IInPatientRoomViewer
		where TEntity : DBCommon, IDBCommon, new()
	{
		public override AbstractDataCollector<TEntity> ActiveCollector { get; set; }
		public override AbstractDataCollector<TEntity> ParentActiveCollector { get; set; }

		public override bool Collect(AbstractDataCollector<TEntity> collector)
		{
			if (collector == null)
				return false;

			ActiveCollector = collector;

			ID = ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			((InPatientRoom_cu)ActiveDBItem).DBCommonTransactionType =
				((IInPatientRoomViewer)ActiveCollector.ActiveViewer).CommonTransactionType;
			((InPatientRoom_cu)ActiveDBItem).Name_P = ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).NameP.ToString();
			((InPatientRoom_cu)ActiveDBItem).Name_S = ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).NameS.ToString();
			((InPatientRoom_cu)ActiveDBItem).Floor_CU_ID =
				Convert.ToInt32(((IInPatientRoomViewer)ActiveCollector.ActiveViewer).Floor);
			((InPatientRoom_cu)ActiveDBItem).InPatientRoomClassification_CU_ID =
				Convert.ToInt32(((IInPatientRoomViewer)ActiveCollector.ActiveViewer).InPatientRoomClassification);
			
			if (((IInPatientRoomViewer)ActiveCollector.ActiveViewer).Description != null)
				((InPatientRoom_cu)ActiveDBItem).Description =
					((IInPatientRoomViewer)ActiveCollector.ActiveViewer).Description.ToString();
			else
				((InPatientRoom_cu)ActiveDBItem).Description = null;

			if (((IInPatientRoomViewer)ActiveCollector.ActiveViewer).InternalCode != null)
				((InPatientRoom_cu)ActiveDBItem).InternalCode =
					((IInPatientRoomViewer)ActiveCollector.ActiveViewer).InternalCode.ToString();
			else
				((InPatientRoom_cu)ActiveDBItem).InternalCode = null;

			if (((IInPatientRoomViewer)ActiveCollector.ActiveViewer).ShortName != null)
				((InPatientRoom_cu)ActiveDBItem).ShortName =
					((IInPatientRoomViewer)ActiveCollector.ActiveViewer).ShortName.ToString();
			else
				((InPatientRoom_cu)ActiveDBItem).ShortName = null;

			if (((IInPatientRoomViewer)ActiveCollector.ActiveViewer).HasMainPatientPricing)
			{
				InPatientRoom_InPatientAdmissionPricingType_cu pricingObject = 
					DBCommon.CreateNewDBEntity<InPatientRoom_InPatientAdmissionPricingType_cu>();
				if (pricingObject != null)
				{
					if (((IInPatientRoomViewer) ActiveCollector.ActiveViewer).PricePerDay_MainPatient != null &&
					    Convert.ToDouble(((IInPatientRoomViewer) ActiveCollector.ActiveViewer).PricePerDay_MainPatient) > 0)
					{
						pricingObject.PricePerDay =
							Convert.ToDouble(((IInPatientRoomViewer) ActiveCollector.ActiveViewer).PricePerDay_MainPatient);
					}
					if (((IInPatientRoomViewer)ActiveCollector.ActiveViewer).MinimumAddmissionAmount_MainPatient != null &&
						Convert.ToDouble(((IInPatientRoomViewer)ActiveCollector.ActiveViewer).MinimumAddmissionAmount_MainPatient) > 0)
					{
						pricingObject.MinimumAddmissionAmount =
							Convert.ToDouble(((IInPatientRoomViewer)ActiveCollector.ActiveViewer).MinimumAddmissionAmount_MainPatient);
					}

					pricingObject.InPatientAddmissionPricingType_P_ID = (int) DB_InPatientAddmissionPricingType.MainPatientPricing;
				}

				((InPatientRoom_cu)ActiveDBItem).InPatientRoom_InPatientAdmissionPricingType_cu.Add(pricingObject);
			}

			if (((IInPatientRoomViewer)ActiveCollector.ActiveViewer).HasCompanionPricing)
			{
				InPatientRoom_InPatientAdmissionPricingType_cu pricingObject =
					DBCommon.CreateNewDBEntity<InPatientRoom_InPatientAdmissionPricingType_cu>();
				if (pricingObject != null)
				{
					if (((IInPatientRoomViewer)ActiveCollector.ActiveViewer).PricePerDay_CompanionPatient != null &&
						Convert.ToDouble(((IInPatientRoomViewer)ActiveCollector.ActiveViewer).PricePerDay_CompanionPatient) > 0)
					{
						pricingObject.PricePerDay =
							Convert.ToDouble(((IInPatientRoomViewer)ActiveCollector.ActiveViewer).PricePerDay_CompanionPatient);
					}
					if (((IInPatientRoomViewer)ActiveCollector.ActiveViewer).MinimumAddmissionAmount_CompanionPatient != null &&
						Convert.ToDouble(((IInPatientRoomViewer)ActiveCollector.ActiveViewer).MinimumAddmissionAmount_CompanionPatient) > 0)
					{
						pricingObject.MinimumAddmissionAmount =
							Convert.ToDouble(((IInPatientRoomViewer)ActiveCollector.ActiveViewer).MinimumAddmissionAmount_CompanionPatient);
					}

					pricingObject.InPatientAddmissionPricingType_P_ID = (int)DB_InPatientAddmissionPricingType.CompanionPricing;
				}

				((InPatientRoom_cu)ActiveDBItem).InPatientRoom_InPatientAdmissionPricingType_cu.Add(pricingObject);
			}

			if (UserID != null)
				((InPatientRoom_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((InPatientRoom_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IStationPointStageViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((InPatientRoom_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return ActiveViewer.ViewerID; }
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
			get { return ActiveViewer.HeaderTitle; }
		}

		public override string GridXML
		{
			get { return ActiveViewer.GridXML; }
		}

		public override List<IViewer> RelatedViewers { get; set; }
		public override void ClearControls()
		{
			((IInPatientRoomViewer)ActiveCollector.ActiveViewer).NameP = null;
			((IInPatientRoomViewer)ActiveCollector.ActiveViewer).NameS = null;
			((IInPatientRoomViewer)ActiveCollector.ActiveViewer).Floor = null;
			((IInPatientRoomViewer)ActiveCollector.ActiveViewer).InPatientRoomClassification = null;
			((IInPatientRoomViewer)ActiveCollector.ActiveViewer).Description = null;
			((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).InternalCode = null;
			((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).ShortName = null;
		}

		public override void FillControls()
		{

		}

		public override bool AddToParent()
		{
			if (ActiveDBItem == null)
				return false;

			((InPatientRoom_cu)ActiveDBItem).Name_P = ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).NameP.ToString();
			((InPatientRoom_cu)ActiveDBItem).Name_S = ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).NameS.ToString();
			((InPatientRoom_cu)ActiveDBItem).IsOnDuty = true;
			((InPatientRoom_cu)ActiveDBItem).DBCommonTransactionType = DB_CommonTransactionType.SaveNew;
			((InPatientRoom_cu)ActiveDBItem).InPatientRoomClassification_CU_ID =
				Convert.ToInt32(((IInPatientRoomViewer)ActiveCollector.ActiveViewer).InPatientRoomClassification);

			if (((IInPatientRoomViewer)ActiveCollector.ActiveViewer).Description != null)
				((InPatientRoom_cu)ActiveDBItem).Description =
					((IInPatientRoomViewer)ActiveCollector.ActiveViewer).Description.ToString();

			if (((IInPatientRoomViewer)ActiveCollector.ActiveViewer).ShortName != null)
				((InPatientRoom_cu)ActiveDBItem).ShortName =
					((IInPatientRoomViewer)ActiveCollector.ActiveViewer).ShortName.ToString();

			if (((IInPatientRoomViewer)ActiveCollector.ActiveViewer).InternalCode != null)
				((InPatientRoom_cu)ActiveDBItem).InternalCode =
					((IInPatientRoomViewer)ActiveCollector.ActiveViewer).InternalCode.ToString();

			if (ParentActiveCollector != null)
				((Floor_cu)ParentActiveCollector.ActiveDBItem).InPatientRoom_cu.Add((InPatientRoom_cu)ActiveDBItem);
			else
				((InPatientRoom_cu)ActiveDBItem).Floor_CU_ID =
					Convert.ToInt32(((IInPatientRoomViewer)ActiveCollector.ActiveViewer).Floor);

			return base.AddToParent();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<InPatientRoom_cu>();
				((IInPatientRoomViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			return ((InPatientRoom_cu)ActiveCollector.ActiveDBItem).SaveChanges();
		}

		public override void Edit(IDBCommon entity)
		{
			((IInPatientRoomViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.UpdateExisting;
			((IInPatientRoomViewer)ActiveCollector.ActiveViewer).NameP = ((InPatientRoom_cu)entity).Name_P;
			((IInPatientRoomViewer)ActiveCollector.ActiveViewer).NameS = ((InPatientRoom_cu)entity).Name_S;
			((IInPatientRoomViewer)ActiveCollector.ActiveViewer).ShortName = ((InPatientRoom_cu)entity).ShortName;
			((IInPatientRoomViewer)ActiveCollector.ActiveViewer).Description = ((InPatientRoom_cu)entity).Description;
			((IInPatientRoomViewer)ActiveCollector.ActiveViewer).InternalCode = ((InPatientRoom_cu)entity).InternalCode;
			((IInPatientRoomViewer)ActiveCollector.ActiveViewer).Floor = ((InPatientRoom_cu)entity).Floor_CU_ID;
			((IInPatientRoomViewer)ActiveCollector.ActiveViewer).InPatientRoomClassification =
				((InPatientRoom_cu)entity).InPatientRoomClassification_CU_ID;


			((IInPatientRoomViewer)ActiveCollector.ActiveViewer).ID = ((InPatientRoom_cu)entity).ID;
			ActiveCollector.ActiveDBItem.ID = ((InPatientRoom_cu)entity).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((InPatientRoom_cu)entity).RemoveItem();
		}

		public override IEnumerable<TEntity> GetItemsList()
		{
			List<InPatientRoom_cu> list = DBCommon.GetItemsList<InPatientRoom_cu>().ToList().ToList();

			if (((IInPatientRoomViewer)ActiveCollector.ActiveViewer).NameP != null)
			{
				string name = ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).NameP.ToString();
				list = DBCommon.GetItemsList<InPatientRoom_cu>(item => item.Name_P.Contains(name)).ToList();
			}

			if (((IInPatientRoomViewer)ActiveCollector.ActiveViewer).NameS != null)
			{
				string name = ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).NameS.ToString();
				list = DBCommon.GetItemsList<InPatientRoom_cu>(item => item.Name_S.Contains(name)).ToList();
			}

			if (((IInPatientRoomViewer)ActiveCollector.ActiveViewer).Floor != null)
				list = DBCommon.GetItemsList<InPatientRoom_cu>(item => item.Floor_CU_ID.Equals(Floor)).ToList();

			if (((IInPatientRoomViewer)ActiveCollector.ActiveViewer).InPatientRoomClassification != null)
				list =
					DBCommon.GetItemsList<InPatientRoom_cu>(
						item => item.InPatientRoomClassification_CU_ID.Equals(InPatientRoomClassification)).ToList();

			if (((IInPatientRoomViewer)ActiveCollector.ActiveViewer).InternalCode != null)
			{
				string name = ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).InternalCode.ToString();
				list = DBCommon.GetItemsList<InPatientRoom_cu>(item => item.InternalCode.Contains(name)).ToList();
			}

			if (((IInPatientRoomViewer)ActiveCollector.ActiveViewer).ShortName != null)
			{
				string name = ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).ShortName.ToString();
				list = DBCommon.GetItemsList<InPatientRoom_cu>(item => item.ShortName.Contains(name)).ToList();
			}

			return (IEnumerable<TEntity>)list;
		}

		#region Implementation of IInPatientRoomViewer

		public object NameP
		{
			get { return ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).NameP; }
			set { ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).NameP = value; }
		}

		public object NameS
		{
			get { return ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).NameP; }
			set { ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).NameP = value; }
		}

		public object Floor
		{
			get { return ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).Floor; }
			set { ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).Floor = value; }
		}

		public object InPatientRoomClassification
		{
			get { return ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).InPatientRoomClassification; }
			set { ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).InPatientRoomClassification = value; }
		}

		public object Description
		{
			get { return ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).Description; }
			set { ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		public object ShortName
		{
			get { return ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).ShortName; }
			set { ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).ShortName = value; }
		}

		public object InternalCode
		{
			get { return ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).InternalCode; }
			set { ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).InternalCode = value; }
		}

		public bool HasMainPatientPricing
		{
			get { return ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).HasMainPatientPricing; }
		}

		public object PricePerDay_MainPatient
		{
			get { return ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).PricePerDay_MainPatient; }
			set { ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).PricePerDay_MainPatient = value; }
		}

		public object MinimumAddmissionAmount_MainPatient
		{
			get { return ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).MinimumAddmissionAmount_MainPatient; }
			set { ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).MinimumAddmissionAmount_MainPatient = value; }
		}

		public bool HasCompanionPricing
		{
			get { return ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).HasCompanionPricing; }
		}

		public object PricePerDay_CompanionPatient
		{
			get { return ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).PricePerDay_CompanionPatient; }
			set { ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).PricePerDay_CompanionPatient = value; }
		}

		public object MinimumAddmissionAmount_CompanionPatient
		{
			get { return ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).MinimumAddmissionAmount_CompanionPatient; }
			set { ((IInPatientRoomViewer)ActiveCollector.ActiveViewer).MinimumAddmissionAmount_CompanionPatient = value; }
		}

		#endregion
	}
}
