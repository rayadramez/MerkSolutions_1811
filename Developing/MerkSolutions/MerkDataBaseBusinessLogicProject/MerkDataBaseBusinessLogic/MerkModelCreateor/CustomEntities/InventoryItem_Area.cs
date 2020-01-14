using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class InventoryItem_Area : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<InventoryItem_Area> _items;
		public static List<InventoryItem_Area> ItemsList = new List<InventoryItem_Area>();
		public static double _Area = 0;
		public double totalArea = 0;
		public static DB_SizeUnitMeasure _SizeUnitMeasure;
		public static double _Width = 0;
		public static double _Height = 0;
		public static double _Count = 0;

		public InventoryItem_Area()
		{
			_SizeUnitMeasure = (DB_SizeUnitMeasure)this.SizeUnitMeasure_P_ID;
			_Width = this.Width;
			_Height = this.Height;
			_Count = this.Count;
		}

		#region ColumnNames

		public static String Description_ColumnaName
		{
			get { return "Description"; }
		}

		#endregion

		public override IList ReGenerateList()
		{
			LoadItemsList();

			return ItemsList;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.InventoryItem_Area; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "InventoryItem_Area"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.InventoryItem_Area.FirstOrDefault(item => item.ID.Equals(id));
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
			ItemsList = DBContext_External.InventoryItem_Area.Where(item => item.IsOnDuty).OrderBy(item => item.ID).ToList();
			return true;
		}

		public string InventoryItemName
		{
			get
			{
				InventoryItem_cu inventoryItem = InventoryItem_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(InventoryItemID)));
				if (inventoryItem == null) 
					return String.Empty;
				return inventoryItem.Name_P;
			}
		}

		public double Area
		{
			get
			{
				return CalculatedArea();
			}
		}

		public double TotalArea
		{
			get { return CalculatedTotalArea(); }
		}

		public SizeUnitMeasure_p SizeUnitMeasure
		{
			get
			{
				SizeUnitMeasure_p sizeMeasure = SizeUnitMeasure_p.ItemsList.Find(item => Convert.ToInt32(item.ID)
					.Equals(
						Convert.ToInt32(SizeUnitMeasure_P_ID)));
				return sizeMeasure;
			}
		}

		public string SizeUnitMeasureName
		{
			get
			{
				if (SizeUnitMeasure == null)
					return string.Empty;

				return SizeUnitMeasure.Name_P;
			}
		}

		public double CalculatedWidth
		{
			get
			{
				return CalculateWidth();
			}
		}

		public double CalculatedHeight
		{
			get
			{
				return CalculateHeight();
			}
		}

		public double CalculateWidth()
		{
			if (_SizeUnitMeasure == DB_SizeUnitMeasure.None)
				_SizeUnitMeasure = (DB_SizeUnitMeasure)SizeUnitMeasure_P_ID;

			switch (_SizeUnitMeasure)
			{
				case DB_SizeUnitMeasure.CM:
					if (Convert.ToInt32(_SizeUnitMeasure).Equals(Convert.ToInt32(SizeUnitMeasure_P_ID)))
						return Width;
					return Width / 10;
				case DB_SizeUnitMeasure.MM:
					if (Convert.ToInt32(_SizeUnitMeasure).Equals(Convert.ToInt32(SizeUnitMeasure_P_ID)))
						return Width;
					return Width * 10;
			}

			return 0;
		}

		public double CalculateHeight()
		{
			if (_SizeUnitMeasure == DB_SizeUnitMeasure.None)
				_SizeUnitMeasure = (DB_SizeUnitMeasure)SizeUnitMeasure_P_ID;

			switch (_SizeUnitMeasure)
			{
				case DB_SizeUnitMeasure.CM:
					if (Convert.ToInt32(_SizeUnitMeasure).Equals(Convert.ToInt32(SizeUnitMeasure_P_ID)))
						return Height;
					return Height / 10;
				case DB_SizeUnitMeasure.MM:
					if (Convert.ToInt32(_SizeUnitMeasure).Equals(Convert.ToInt32(SizeUnitMeasure_P_ID)))
						return Height;
					return Height * 10;
			}

			return 0;
		}

		public double CalculatedArea()
		{
			if (_SizeUnitMeasure == DB_SizeUnitMeasure.None)
				_SizeUnitMeasure = (DB_SizeUnitMeasure)this.SizeUnitMeasure_P_ID;
			_Width = Width;
			_Height = Height;
			_Count = Count;

			switch (_SizeUnitMeasure)
			{
				case DB_SizeUnitMeasure.CM:
					if (Convert.ToInt32(_SizeUnitMeasure).Equals(Convert.ToInt32(SizeUnitMeasure_P_ID)))
						return _Width * _Height;
					return _Area = (_Width / 10) * (_Height / 10);
				case DB_SizeUnitMeasure.MM:
					if (Convert.ToInt32(_SizeUnitMeasure).Equals(Convert.ToInt32(SizeUnitMeasure_P_ID)))
						return _Width  * _Height;
					return _Area = (_Width  * 10) * (_Height * 10);
			}

			return 0;
		}

		public double CalculatedTotalArea()
		{
			if (_SizeUnitMeasure == DB_SizeUnitMeasure.None)
				_SizeUnitMeasure = (DB_SizeUnitMeasure)SizeUnitMeasure_P_ID;
			_Width = this.Width;
			_Height = Height;
			_Count = Count;

			switch (_SizeUnitMeasure)
			{
				case DB_SizeUnitMeasure.CM:
					if (Convert.ToInt32(_SizeUnitMeasure).Equals(Convert.ToInt32(SizeUnitMeasure_P_ID)))
						return _Width * _Height * _Count;
					return _Area = (_Width / 10) * (_Height / 10) * _Count;
				case DB_SizeUnitMeasure.MM:
					if (Convert.ToInt32(_SizeUnitMeasure).Equals(Convert.ToInt32(SizeUnitMeasure_P_ID)))
						return _Width * _Height * _Count;
					return _Area = (_Width * 10) * (_Height * 10) * _Count;
			}

			return 0;
		}
	}
}
