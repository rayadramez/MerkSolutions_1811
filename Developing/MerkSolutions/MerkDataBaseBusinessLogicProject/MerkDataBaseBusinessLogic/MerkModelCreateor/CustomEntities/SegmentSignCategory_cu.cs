using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class SegmentSignCategory_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<SegmentSignCategory_cu> _items;
		public static List<SegmentSignCategory_cu> ItemsList = new List<SegmentSignCategory_cu>();

		#region ColumnNames

		public static String VisitTimingID_ColumnaName
		{
			get { return "VisitTimingID"; }
		}

		public static String Treatment_ColumnaName
		{
			get { return "Treatment"; }
		}

		public static String StepOrderIndex_ColumnaName
		{
			get { return "StepOrderIndex"; }
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

			ItemsList = DBContext_External.SegmentSignCategory_cu.ToList();
			return true;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.SegmentSignCategory_cu; }
		}

		public List<string> ChildrenItemsList
		{
			get
			{
				List<string> list = new List<string>();
				return list;
			}
		}

		public string EntityName
		{
			get { return "SegmentSignCategory_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.SegmentSignCategory_cu.FirstOrDefault(item => item.ID.Equals(id));
		}
	}
}
