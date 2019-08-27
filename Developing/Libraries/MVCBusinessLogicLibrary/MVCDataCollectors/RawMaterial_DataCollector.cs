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
	public class RawMaterial_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IRawMaterial_Viewer
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

			ID = ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Name_P != null)
				((RawMaterials_cu)ActiveDBItem).Name_P = Name_P.ToString();

			if (Name_S != null)
				((RawMaterials_cu)ActiveDBItem).Name_S = Name_S.ToString();

			if (RawTypeID != null)
				((RawMaterials_cu)ActiveDBItem).RawTypeID = Convert.ToInt32(RawTypeID);

			if (InternalCode != null)
				((RawMaterials_cu)ActiveDBItem).InternalCode = InternalCode.ToString();

			if (Width != null)
				((RawMaterials_cu)ActiveDBItem).Width = Convert.ToDouble(Width);

			if (Thickness != null)
				((RawMaterials_cu)ActiveDBItem).Thickness = Convert.ToDouble(Thickness);

			if (Height != null)
				((RawMaterials_cu)ActiveDBItem).Height = Convert.ToDouble(Height);

			if (Weight != null)
				((RawMaterials_cu)ActiveDBItem).Weight = Convert.ToDouble(Weight);

			if (ExpirationDate != null)
				((RawMaterials_cu)ActiveDBItem).ExpirationDate = Convert.ToDateTime(ExpirationDate);

			if (IsStockAvailable != null)
				((RawMaterials_cu)ActiveDBItem).IsStockAvailable = Convert.ToBoolean(IsStockAvailable);

			if (IsCountable != null)
				((RawMaterials_cu)ActiveDBItem).IsCountable = Convert.ToBoolean(IsCountable);

			if (ColorID != null)
				((RawMaterials_cu)ActiveDBItem).Color_CU_ID = Convert.ToInt32(ColorID);

			if (DividedTypeID != null)
				((RawMaterials_cu)ActiveDBItem).DividedByType_P_ID = Convert.ToInt32(DividedTypeID);

			if (Description != null)
				((RawMaterials_cu)ActiveDBItem).Description = Description.ToString();

			if (UserID != null)
				((RawMaterials_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((RawMaterials_cu)ActiveDBItem).IsOnDuty = true;

			switch (((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((RawMaterials_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.InventoryItem_Viewer; }
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
			List<RawMaterials_cu> list = RawMaterials_cu.ItemsList.FindAll(item => item.IsOnDuty);
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<RawMaterials_cu>();

				((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((RawMaterials_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((RawMaterials_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			Name_P = ((RawMaterials_cu)ActiveDBItem).Name_P;
			Name_S = ((RawMaterials_cu)ActiveDBItem).Name_S;
			RawTypeID = ((RawMaterials_cu)ActiveDBItem).RawTypeID;
			InternalCode = ((RawMaterials_cu)ActiveDBItem).InternalCode;
			Width = ((RawMaterials_cu)ActiveDBItem).Width;
			Thickness = ((RawMaterials_cu)ActiveDBItem).Thickness;
			Height = ((RawMaterials_cu)ActiveDBItem).Height;
			Weight = ((RawMaterials_cu)ActiveDBItem).Weight;
			ExpirationDate = ((RawMaterials_cu)ActiveDBItem).ExpirationDate;
			IsStockAvailable = ((RawMaterials_cu)ActiveDBItem).IsStockAvailable;
			IsCountable = ((RawMaterials_cu)ActiveDBItem).IsCountable;
			ColorID = ((RawMaterials_cu)ActiveDBItem).Color_CU_ID;
			Description = ((RawMaterials_cu)ActiveDBItem).Description;

			((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).ID = ((RawMaterials_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((RawMaterials_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((RawMaterials_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IRawMaterial_Viewer

		public object Name_P
		{
			get { return ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).Name_P; }
			set { ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).Name_P = value; }
		}

		public object Name_S
		{
			get { return ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).Name_S; }
			set { ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).Name_S = value; }
		}

		public object RawTypeID
		{
			get { return ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).RawTypeID; }
			set { ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).RawTypeID = value; }
		}

		public object InternalCode
		{
			get { return ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).InternalCode; }
			set { ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).InternalCode = value; }
		}

		public object Thickness
		{
			get { return ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).Thickness; }
			set { ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).Thickness = value; }
		}

		public object Width
		{
			get { return ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).Width; }
			set { ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).Width = value; }
		}

		public object Height
		{
			get { return ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).Height; }
			set { ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).Height = value; }
		}

		public object Weight
		{
			get { return ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).Weight; }
			set { ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).Weight = value; }
		}

		public object ExpirationDate
		{
			get { return ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).ExpirationDate; }
			set { ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).ExpirationDate = value; }
		}

		public object IsStockAvailable
		{
			get { return ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).IsStockAvailable; }
			set { ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).IsStockAvailable = value; }
		}

		public object IsCountable
		{
			get { return ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).IsCountable; }
			set { ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).IsCountable = value; }
		}

		public object Description
		{
			get { return ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).Description; }
			set { ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		public object ColorID
		{
			get { return ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).ColorID; }
			set { ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).ColorID = value; }
		}

		public object DividedTypeID
		{
			get { return ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).DividedTypeID; }
			set { ((IRawMaterial_Viewer)ActiveCollector.ActiveViewer).DividedTypeID = value; }
		}

		#endregion

	}
}

