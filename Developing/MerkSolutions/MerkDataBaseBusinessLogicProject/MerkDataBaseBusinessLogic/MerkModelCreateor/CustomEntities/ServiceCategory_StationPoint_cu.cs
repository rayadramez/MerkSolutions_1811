﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class ServiceCategory_StationPoint_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<ServiceCategory_StationPoint_cu> _items;
		public static List<ServiceCategory_StationPoint_cu> ItemsList = new List<ServiceCategory_StationPoint_cu>();

		#region ColumnNames

		public static String ServiceCategory_CU_ID_ColumnaName
		{
			get { return "ServiceCategory_CU_ID"; }
		}

		public static String StationPoint_CU_ID_ColumnaName
		{
			get { return "StationPoint_CU_ID"; }
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
			ItemsList = DBContext_External.ServiceCategory_StationPoint_cu.Where(item => item.IsOnDuty).ToList();
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
			get { return (int)DB_TableIdentity.ServiceCategory_StationPoint_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "ServiceCategory_StationPoint_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.ServiceCategory_StationPoint_cu.FirstOrDefault(item => item.ID.Equals(id));
		}

		#endregion

		public string ServiceCategoryName
		{
			get
			{
				ServiceCategory_cu serviceCategory =
					ServiceCategory_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(ServiceCategory_CU_ID)));
				if (serviceCategory != null)
					return serviceCategory.Name_P;

				return string.Empty;
			}
		}

		public string StationPointName
		{
			get
			{
				StationPoint_cu stationPoint =
					StationPoint_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(StationPoint_CU_ID)));
				if (stationPoint != null)
					return stationPoint.Name_P;

				return string.Empty;
			}
		}
	}
}
