using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class StationPoint_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<StationPoint_cu> _items;
		public static List<StationPoint_cu> ItemsList = new List<StationPoint_cu>();

		#region ColumnNames

		public static String Station_P_ID_ColumnaName
		{
			get { return "Station_P_ID"; }
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
			ItemsList = DBContext_External.StationPoint_cu.Where(item => item.IsOnDuty).ToList();
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
			get { return (int)DB_TableIdentity.StationPoint_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "StationPoint_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.StationPoint_cu.FirstOrDefault(item => item.ID.Equals(id));
		}

		#endregion

		public string StationName
		{
			get
			{
				Station_p station = Station_p.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(Station_P_ID)));
				if (station != null)
					return station.Name_P;

				return string.Empty;
			}
		}
	}
}
