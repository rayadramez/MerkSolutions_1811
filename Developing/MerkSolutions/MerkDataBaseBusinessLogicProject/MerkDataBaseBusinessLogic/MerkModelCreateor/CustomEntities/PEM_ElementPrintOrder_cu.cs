using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class PEM_ElementPrintOrder_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<PEM_ElementPrintOrder_cu> _items;
		public static List<PEM_ElementPrintOrder_cu> ItemsList = new List<PEM_ElementPrintOrder_cu>();

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
			get { return (int)DB_TableIdentity.PEM_ElementPrintOrder_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "PEM_ElementPrintOrder_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.PEM_ElementPrintOrder_cu.FirstOrDefault(item => item.ID.Equals(id));
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
			ItemsList = DBContext_External.PEM_ElementPrintOrder_cu.Where(item => item.IsOnDuty).OrderBy(item => item.ID).ToList();
			return true;
		}

		public PEMR_Elemet_p PEMR_Element
		{
			get
			{
				PEMR_Elemet_p element =
					PEMR_Elemet_p.ItemsList.Find(item => Convert.ToInt32(item.ID)
						                             .Equals(Convert.ToInt32(PEMR_Elemet_P_ID)));
				return element;
			}
		}

		public string PEMR_ElementName
		{
			get
			{
				if (PEMR_Element == null)
					return string.Empty;
				return PEMR_Element.Name_S;
			}
		}
	}
}
