using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using ApplicationConfiguration;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCDataCollectors
{
	public class RawMaterialTransaction_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IRawMaterialTransaction_Viewer
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

			ID = ((IRawMaterialTransaction_Viewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (RawMaterialID != null)
				((RawMaterialTranasction) ActiveDBItem).RawMaterial_CU_ID = Convert.ToInt32(RawMaterialID);

			if (RawTransactionTypeID != null)
				((RawMaterialTranasction)ActiveDBItem).RawMaterialTransactionType_P_ID = Convert.ToInt32(RawTransactionTypeID);

			if (Count != null)
				((RawMaterialTranasction)ActiveDBItem).Count = Convert.ToInt32(Count);

			if (PuchasingPrice != null)
				((RawMaterialTranasction)ActiveDBItem).PuchasingPrice = Convert.ToDouble(PuchasingPrice);

			if (Width != null)
				((RawMaterialTranasction)ActiveDBItem).Width = Convert.ToDouble(Width);

			if (Height != null)
				((RawMaterialTranasction)ActiveDBItem).Height = Convert.ToDouble(Height);

			if (TransactionDate != null)
				((RawMaterialTranasction)ActiveDBItem).Date = Convert.ToDateTime(TransactionDate);

			if (DividedTypeID != null)
				((RawMaterialTranasction)ActiveDBItem).DividedByType_P_ID = Convert.ToInt32(DividedTypeID);

			if (ColorID != null)
				((RawMaterialTranasction)ActiveDBItem).Color_CU_ID = Convert.ToInt32(ColorID);

			if (UserID != null)
				((RawMaterialTranasction)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((RawMaterialTranasction)ActiveDBItem).IsOnDuty = true;

			switch (((IRawMaterialTransaction_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((RawMaterialTranasction)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IRawMaterialTransaction_Viewer)ActiveCollector.ActiveViewer).RelatedViewers;

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
			List<RawMaterialTranasction> list = RawMaterialTranasction.ItemsList.FindAll(item => item.IsOnDuty);
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<RawMaterialTranasction>();

				((IRawMaterialTransaction_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((RawMaterialTranasction)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((RawMaterialTranasction)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			RawMaterialID = ((RawMaterialTranasction)ActiveDBItem).RawMaterial_CU_ID;
			RawTransactionTypeID = ((RawMaterialTranasction)ActiveDBItem).RawMaterialTransactionType_P_ID;
			Count = ((RawMaterialTranasction)ActiveDBItem).Count;
			PuchasingPrice = ((RawMaterialTranasction)ActiveDBItem).PuchasingPrice;
			Width = ((RawMaterialTranasction)ActiveDBItem).Width;
			Height = ((RawMaterialTranasction)ActiveDBItem).Height;
			TransactionDate = ((RawMaterialTranasction)ActiveDBItem).Date;

			((IRawMaterialTransaction_Viewer)ActiveCollector.ActiveViewer).ID = ((RawMaterialTranasction)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((RawMaterialTranasction)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((RawMaterialTranasction)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IRawMaterialTransaction_Viewer

		public object RawMaterialID
		{
			get { return ((IRawMaterialTransaction_Viewer)ActiveCollector.ActiveViewer).RawMaterialID; }
			set { ((IRawMaterialTransaction_Viewer)ActiveCollector.ActiveViewer).RawMaterialID = value; }
		}

		public object RawTransactionTypeID
		{
			get { return ((IRawMaterialTransaction_Viewer)ActiveCollector.ActiveViewer).RawTransactionTypeID; }
			set { ((IRawMaterialTransaction_Viewer)ActiveCollector.ActiveViewer).RawTransactionTypeID = value; }
		}

		public object Count
		{
			get { return ((IRawMaterialTransaction_Viewer)ActiveCollector.ActiveViewer).Count; }
			set { ((IRawMaterialTransaction_Viewer)ActiveCollector.ActiveViewer).Count = value; }
		}

		public object PuchasingPrice
		{
			get { return ((IRawMaterialTransaction_Viewer)ActiveCollector.ActiveViewer).PuchasingPrice; }
			set { ((IRawMaterialTransaction_Viewer)ActiveCollector.ActiveViewer).PuchasingPrice = value; }
		}

		public object Width
		{
			get { return ((IRawMaterialTransaction_Viewer)ActiveCollector.ActiveViewer).Width; }
			set { ((IRawMaterialTransaction_Viewer)ActiveCollector.ActiveViewer).Width = value; }
		}

		public object Height
		{
			get { return ((IRawMaterialTransaction_Viewer)ActiveCollector.ActiveViewer).Height; }
			set { ((IRawMaterialTransaction_Viewer)ActiveCollector.ActiveViewer).Height = value; }
		}

		public object TransactionDate
		{
			get { return ((IRawMaterialTransaction_Viewer)ActiveCollector.ActiveViewer).TransactionDate; }
			set { ((IRawMaterialTransaction_Viewer)ActiveCollector.ActiveViewer).TransactionDate = value; }
		}

		public object DividedTypeID
		{
			get { return ((IRawMaterialTransaction_Viewer)ActiveCollector.ActiveViewer).DividedTypeID; }
			set { ((IRawMaterialTransaction_Viewer)ActiveCollector.ActiveViewer).DividedTypeID = value; }
		}

		public object ColorID
		{
			get { return ((IRawMaterialTransaction_Viewer)ActiveCollector.ActiveViewer).ColorID; }
			set { ((IRawMaterialTransaction_Viewer)ActiveCollector.ActiveViewer).ColorID = value; }
		}

		#endregion
	}
}

