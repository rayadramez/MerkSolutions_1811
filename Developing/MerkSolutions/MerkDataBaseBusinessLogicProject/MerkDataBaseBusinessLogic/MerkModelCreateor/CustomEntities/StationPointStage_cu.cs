using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class StationPointStage_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<StationPointStage_cu> _items;
		public static List<StationPointStage_cu> ItemsList = new List<StationPointStage_cu>();

		#region ColumnNames

		public static String StationPoint_CU_ID_ColumnaName
		{
			get { return "StationPoint_CU_ID"; }
		}

		public static String Floor_CU_ID_ColumnaName
		{
			get { return "Floor_CU_ID"; }
		}

		public static String OrderIndex_ColumnaName
		{
			get { return "OrderIndex"; }
		}

		public static String Description_ColumnaName
		{
			get { return "Description"; }
		}

		#endregion

		public override bool LoadFromDB
		{
			get { return true; }
		}

		public override DBCommonEntitiesType TableType
		{
			get { return DBCommonEntitiesType.CustomUserEntities; }
		}

		public override bool LoadItemsList()
		{
			ItemsList.Clear();
			ItemsList = DBContext_External.StationPointStage_cu.Where(item => item.IsOnDuty).ToList();
			return true;
		}

		#region Implementation of IDBCommon

		public override IList ReGenerateList()
		{
			LoadItemsList();

			return ItemsList;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.StationPointStage_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "StationPointStage_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.StationPointStage_cu.FirstOrDefault(item => item.ID.Equals(id));
		}

		#endregion

		public StationPoint_cu StationPoint
		{
			get
			{
				StationPoint_cu stationPoint =
					StationPoint_cu.ItemsList.Find(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(StationPoint_CU_ID)));
				if (stationPoint != null)
					return stationPoint;
				return null;
			}
		}

		public string StationPointName
		{
			get
			{
				if (StationPoint != null)
					return StationPoint.Name_P;

				return string.Empty;
			}
		}

		public string FloorName
		{
			get
			{
				if (Floor_CU_ID != null)
				{
					Floor_cu floor =
						Floor_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(Floor_CU_ID)));
					if (floor != null)
						return floor.Name_P;
				}

				return string.Empty;
			}
		}

		public string StationPointStageFullName
		{
			get
			{
				if (StationPoint != null)
					return Name_P + " - " + StationPoint.Name_P;
				return string.Empty;
			}
		}
	}
}
