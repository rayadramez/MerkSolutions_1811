using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class RawMaterialTranasction : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<RawMaterialTranasction> _items;
		public static List<RawMaterialTranasction> ItemsList = new List<RawMaterialTranasction>();
		public static List<RawMaterialTranasction> AllItemsList = new List<RawMaterialTranasction>();

		#region ColumnNames

		public static String Serial_ColumnaName
		{
			get { return "Serial"; }
		}

		#endregion

		public override bool LoadFromDB
		{
			get { return false; }
		}

		public override DBCommonEntitiesType TableType
		{
			get { return DBCommonEntitiesType.TransactionsEntities; }
		}

		public override bool LoadItemsList()
		{
			ItemsList.Clear();

			ItemsList = AllItemsList = DBContext_External.RawMaterialTranasctions.ToList();
			return true;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.RawMaterialTranasction; }
		}

		public System.Collections.IList ChildrenItemsList { get; set; }

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName { get; set; }
		public IDBCommon GetSpecificEntity(int id)
		{
			throw new NotImplementedException();
		}

		public RawMaterials_cu RawMaterial
		{
			get
			{
				RawMaterials_cu raw = RawMaterials_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(RawMaterial_CU_ID)));
				return raw;
			}
		}

		public string RawMaterialFullName
		{
			get
			{
				if (RawMaterial == null)
					return string.Empty;
				return RawMaterial.RawMaterialFullName;
			}
		}

		public RawMaterialTranasctionType_p RawMaterialTranasctionType
		{
			get
			{
				RawMaterialTranasctionType_p raw = RawMaterialTranasctionType_p.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(RawMaterialTransactionType_P_ID)));
				return raw;
			}
		}

		public string RawMaterialTranasctionTypeName
		{
			get
			{
				if (RawMaterialTranasctionType == null)
					return string.Empty;
				return RawMaterialTranasctionType.Name_P;
			}
		}
	}
}
