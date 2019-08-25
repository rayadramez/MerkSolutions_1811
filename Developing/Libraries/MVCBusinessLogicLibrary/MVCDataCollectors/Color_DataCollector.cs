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
	public class Color_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IColorViewer
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

			ID = ((IColorViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Name_P != null)
				((Color_cu)ActiveDBItem).Name_P = Name_P.ToString();

			if (Name_S != null)
				((Color_cu)ActiveDBItem).Name_S = Name_S.ToString();

			if (InternalCode != null)
				((Color_cu)ActiveDBItem).InternalCode = InternalCode.ToString();

			if (Description != null)
				((Color_cu)ActiveDBItem).Description = Description.ToString();

			((Color_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IColorViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((Color_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IColorViewer)ActiveCollector.ActiveViewer).RelatedViewers;

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
			List<Color_cu> list = Color_cu.ItemsList.FindAll(item => item.IsOnDuty);
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<Color_cu>();

				((IColorViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((Color_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((Color_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			Name_P = ((Color_cu)ActiveDBItem).Name_P;
			Name_S = ((Color_cu)ActiveDBItem).Name_S;
			InternalCode = ((Color_cu)ActiveDBItem).InternalCode;
			Description = ((Color_cu)ActiveDBItem).Description;

			((IColorViewer)ActiveCollector.ActiveViewer).ID = ((Color_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((Color_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((Color_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IColorViewer

		public object Name_P
		{
			get { return ((IColorViewer)ActiveCollector.ActiveViewer).Name_P; }
			set { ((IColorViewer)ActiveCollector.ActiveViewer).Name_P = value; }
		}

		public object Name_S
		{
			get { return ((IColorViewer)ActiveCollector.ActiveViewer).Name_S; }
			set { ((IColorViewer)ActiveCollector.ActiveViewer).Name_S = value; }
		}

		public object InternalCode
		{
			get { return ((IColorViewer)ActiveCollector.ActiveViewer).InternalCode; }
			set { ((IColorViewer)ActiveCollector.ActiveViewer).InternalCode = value; }
		}

		public object Description
		{
			get { return ((IColorViewer)ActiveCollector.ActiveViewer).Description; }
			set { ((IColorViewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		#endregion

	}
}

