using System;
using System.Windows.Forms;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InventoryItem_Printing_Viewers
{
	public partial class InventoryItem_Printing_SearchViewer : 
		//UserControl
		CommonAbstractSearchViewer<InventoryItem_Printing_cu>,
		IInventoryItem_Printing_Viewer
	{
		public InventoryItem_Printing_SearchViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1,
				Resources.LocalizedRes.lyt_InventoryItem_Printing_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<InventoryItem_Printing_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.InventoryItem_Area_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "المنتجــــات"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_InventoryItem_Printing_SearchViewer; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeInventoryItems, InventoryItem_cu.ItemsList);
		}

		public override void ClearControls()
		{
			lkeInventoryItems.EditValue = null;
			dtDate.EditValue = DateTime.Now;
			spnPrintingMaxTimeInMinutes.EditValue = null;
			spnAddedMinutes.EditValue = null;
			spnPrintingAverageUnitCostPrice.EditValue = null;
			spnExpectedCost.EditValue = null;
			spnPrintingRealCostPrice.EditValue = null;
			txtDescription.EditValue = null;
			chkUseExpectedCost.Checked = true;
		}

		#endregion

		#region Implementation of IInventoryItem_Printing_Viewer

		public object InventoryItemID
		{
			get { return lkeInventoryItems.EditValue; }
			set { lkeInventoryItems.EditValue = value; }
		}

		public object RawMaterialsID { get; set; }

		public object Date
		{
			get { return dtDate.EditValue; }
			set { dtDate.EditValue = value; }
		}

		public object TotalMinutes
		{
			get { return spnPrintingMaxTimeInMinutes.EditValue; }
			set { spnPrintingMaxTimeInMinutes.EditValue = value; }
		}

		public object LightMinutes { get; set; }

		public object AddedMinutes
		{
			get { return spnAddedMinutes.EditValue; }
			set { spnAddedMinutes.EditValue = value; }
		}

		public object UseAverageCostPrice { get; set; }

		public object MinuteUnitCost
		{
			get { return spnPrintingAverageUnitCostPrice.EditValue; }
			set { spnPrintingAverageUnitCostPrice.EditValue = value; }
		}

		public object UseRealCost
		{
			get { return chkUseRealCost.Checked; }
			set
			{
				if (Convert.ToBoolean(value))
					chkUseRealCost.Checked = true;
				else
					chkUseExpectedCost.Checked = true;
			}
		}

		public object PrintingRealCostPrice
		{
			get { return spnPrintingRealCostPrice.EditValue; }
			set { spnPrintingRealCostPrice.EditValue = value; }
		}

		public object RealMinutes { get; set; }

		public object Description
		{
			get { return txtDescription.EditValue; }
			set { txtDescription.EditValue = value; }
		}

		#endregion
	}
}
