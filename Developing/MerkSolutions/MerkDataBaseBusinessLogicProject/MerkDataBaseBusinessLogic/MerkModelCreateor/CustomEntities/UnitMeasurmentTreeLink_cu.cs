using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class UnitMeasurmentTreeLink_cu : DBCommon, IDBCommon
	{
		public static List<UnitMeasurmentTreeLink_cu> ItemsList = new List<UnitMeasurmentTreeLink_cu>();

		#region ColumnNames

		public static String ParentUnitMeasurment_CU_ID_ColumnaName
		{
			get { return "ParentUnitMeasurment_CU_ID"; }
		}

		public static String ChildUnitMeasurment_CU_ID_ColumnaName
		{
			get { return "ChildUnitMeasurment_CU_ID"; }
		}

		public static String EncapsulatedChildQantity_ColumnaName
		{
			get { return "EncapsulatedChildQantity"; }
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
			ItemsList = DBContext_External.UnitMeasurmentTreeLink_cu.Where(item => item.IsOnDuty).ToList();
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
			get { return (int)DB_TableIdentity.UnitMeasurmentTreeLink_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "UnitMeasurmentTreeLink_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.UnitMeasurmentTreeLink_cu.FirstOrDefault(item => item.ID.Equals(id));
		}

		#endregion

		public string UnitMeasurmentName_Parent
		{
			get
			{
				UnitMeasurment_cu unti =
					UnitMeasurment_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(ParentUnitMeasurment_CU_ID)));
				if (unti != null)
					return unti.Name_P;

				return string.Empty;
			}
		}

		public string UnitMeasurmentName_Child
		{
			get
			{
				UnitMeasurment_cu unti =
					UnitMeasurment_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(ChildUnitMeasurment_CU_ID)));
				if (unti != null)
					return unti.Name_P;

				return string.Empty;
			}
		}
	}
}
