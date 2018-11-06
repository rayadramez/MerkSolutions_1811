using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class UnitMeasurment_cu : DBCommon, IDBCommon
	{
		public static List<UnitMeasurment_cu> ItemsList = new List<UnitMeasurment_cu>();

		#region ColumnNames

		public static String IsInventoryTracking_ColumnaName
		{
			get { return "IsInventoryTracking"; }
		}

		public static String UnitMeasurment_P_ID_ColumnaName
		{
			get { return "UnitMeasurment_P_ID"; }
		}

		public static String Description_ColumnaName
		{
			get { return "Description"; }
		}

		public static String InternalCode_ColumnaName
		{
			get { return "InternalCode"; }
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
			ItemsList = DBContext_External.UnitMeasurment_cu.Where(item => item.IsOnDuty).OrderBy(item => item.Name_P).ToList();
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
			get { return (int)DB_TableIdentity.UnitMeasurment_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "UnitMeasurment_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.UnitMeasurment_cu.FirstOrDefault(item => item.ID.Equals(id));
		}

		#endregion

		public bool HasParent
		{
			get
			{
				UnitMeasurmentTreeLink_cu unitTree =
					MerkDataBaseBusinessLogicProject.UnitMeasurmentTreeLink_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ChildUnitMeasurment_CU_ID).Equals(Convert.ToInt32(ID)));
				if (unitTree == null)
					return false;
				return true;
			}
		}

		public bool HasChild
		{
			get
			{
				UnitMeasurmentTreeLink_cu unitTree =
					MerkDataBaseBusinessLogicProject.UnitMeasurmentTreeLink_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ParentUnitMeasurment_CU_ID).Equals(Convert.ToInt32(ID)));
				if (unitTree == null)
					return false;
				return true;
			}
		}

		public UnitMeasurment_cu ParentUnitMeasurment
		{
			get
			{
				if (!HasParent)
					return null;

				UnitMeasurmentTreeLink_cu unitTree =
					MerkDataBaseBusinessLogicProject.UnitMeasurmentTreeLink_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ChildUnitMeasurment_CU_ID).Equals(Convert.ToInt32(ID)));
				if (unitTree == null)
					return null;

				UnitMeasurment_cu unitMeasurment =
					ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(unitTree.ParentUnitMeasurment_CU_ID)));
				return unitMeasurment;
			}
		}

		public UnitMeasurment_cu ChildUnitMeasurment
		{
			get
			{
				if (!HasChild)
					return null;

				UnitMeasurmentTreeLink_cu unitTree =
					MerkDataBaseBusinessLogicProject.UnitMeasurmentTreeLink_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ParentUnitMeasurment_CU_ID).Equals(Convert.ToInt32(ID)));
				if (unitTree == null)
					return null;

				UnitMeasurment_cu unitMeasurment =
					ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(unitTree.ChildUnitMeasurment_CU_ID)));
				return unitMeasurment;
			}
		}

		public int OrderIndex { get; set; }

		public double EncapsulatedQuantityToSmallestUnit { get; set; }
		public double EncapsualtedQuantityToParentUnit { get; set; }
		public bool IsInventoryTracking { get; set; }
		public bool IsSmallestUnit { get; set; }
		public bool IsLargestUnit { get; set; }
	}
}
