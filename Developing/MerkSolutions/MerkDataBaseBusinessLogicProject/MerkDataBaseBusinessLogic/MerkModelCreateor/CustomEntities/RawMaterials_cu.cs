using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class RawMaterials_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<RawMaterials_cu> _items;
		public static List<RawMaterials_cu> ItemsList = new List<RawMaterials_cu>();

		public override IList ReGenerateList()
		{
			LoadItemsList();

			return ItemsList;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.RawMaterials_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "RawMaterials_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.RawMaterials_cu.FirstOrDefault(item => item.ID.Equals(id));
		}

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
			ItemsList = DBContext_External.RawMaterials_cu.Where(item => item.IsOnDuty).OrderBy(item => item.Name_P).ToList();
			return true;
		}

		public RawMaterialType_p RawMaterialType
		{
			get
			{
				RawMaterialType_p raw = RawMaterialType_p.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(RawTypeID)));
				return raw;
			}
		}

		public string RawMaterialTypeName
		{
			get
			{
				if (RawMaterialType == null)
					return string.Empty;
				return RawMaterialType.Name_P;
			}
		}

		public Color_cu Color
		{
			get
			{
				Color_cu color = Color_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(Color_CU_ID)));
				return color;
			}
		}

		public string ColorName
		{
			get
			{
				if (Color == null)
					return string.Empty;
				return Color.Name_P;
			}
		}

		public string RawMaterialFullName
		{
			get
			{
				string name = Name_P;
				if (Color != null)
					name += " - " + ColorName;
				name += " - " + Thickness;
				DB_DividedByType dividedByType = (DB_DividedByType) DividedByType_P_ID;
				switch (dividedByType)
				{
					case DB_DividedByType.NotDivided:
						name += " - (" + Width;
						name += " * " + Height + ")";
						break;
					case DB_DividedByType.DividedBy4:
						name += " - (" + Width;
						name += " * " + Height/4 + ")";
						break;
					case DB_DividedByType.DividedBy6:
						name += " - (" + Width/2;
						name += " * " + Height/3 + ")";
						break;
				}
				
				return name;
			}
		}
	}
}
