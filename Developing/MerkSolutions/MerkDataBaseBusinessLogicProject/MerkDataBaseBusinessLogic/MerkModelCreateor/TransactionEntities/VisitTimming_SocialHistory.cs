using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class VisitTiming_SocialHistory : DBCommon, IDBCommon, IPEMR_Element
	{
		MerkFinanceEntities _context;
		private static List<VisitTiming_SocialHistory> _items;
		public static List<VisitTiming_SocialHistory> ItemsList = new List<VisitTiming_SocialHistory>();

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

			ItemsList = DBContext_External.VisitTiming_SocialHistory.ToList();
			return true;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.VisitTiming_SocialHistory; }
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
			get { return "VisitTiming_SocialHistory"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.VisitTiming_SocialHistory.FirstOrDefault(item => item.ID.Equals(id));
		}

		#region Implementation of IPEMR_Element

		public DB_PEMR_ElementType PEMR_Element
		{
			get { return DB_PEMR_ElementType.VisitTiming_SocialHistory; }
		}

		public int OrderIndex
		{
			get
			{
				PEMR_Elemet_p element = null;
				if (PEM_ElementPrintOrder_cu.ItemsList.Count == 0)
				{
					element = PEMR_Elemet_p.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals((int) DB_PEMR_ElementType.VisitTiming_SocialHistory));
					if (element != null)
						return Convert.ToInt32(element.DefaultOrderIndex);
					return 0;
				}

				PEM_ElementPrintOrder_cu elementPrintOrder = PEM_ElementPrintOrder_cu.ItemsList.Find(
					item => Convert.ToInt32(item.PEMR_Elemet_P_ID)
						.Equals((int) DB_PEMR_ElementType.VisitTiming_SocialHistory));
				if (elementPrintOrder != null)
					return elementPrintOrder.OrderIndex;

				element = PEMR_Elemet_p.ItemsList.Find(
					item => Convert.ToInt32(item.ID).Equals((int)DB_PEMR_ElementType.VisitTiming_SocialHistory));
				if (element != null)
					return Convert.ToInt32(element.DefaultOrderIndex);
				return 0;
			}
		}

		public string ElementName
		{
			get
			{
				PEMR_Elemet_p element = element = PEMR_Elemet_p.ItemsList.Find(
					item => Convert.ToInt32(item.ID).Equals((int)DB_PEMR_ElementType.VisitTiming_SocialHistory));
				if(element == null)
					return String.Empty;

				return element.Name_S;
			}
		}

		public string TranslatedItem { get; set; }
		public string TranslatedItemValue { get; set; }
		public PEMRElementStatus PEMRElementStatus { get; set; }

		#endregion
	}
}
