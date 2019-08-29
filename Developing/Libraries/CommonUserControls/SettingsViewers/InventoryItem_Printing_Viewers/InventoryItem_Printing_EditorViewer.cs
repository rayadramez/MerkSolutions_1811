using System;
using System.Drawing;
using System.Windows.Forms;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InventoryItem_Printing_Viewers
{
	public partial class InventoryItem_Printing_EditorViewer : 
		//UserControl
		CommonAbstractEditorViewer<InventoryItem_Printing_cu>,
		IInventoryItem_Printing_Viewer
	{
		public InventoryItem_Printing_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1,
				Resources.LocalizedRes.lyt_InventoryItem_Printing_EditorViewer);
			CommonViewsActions.SetupSyle(this);

			labelControl1.ForeColor = labelControl2.ForeColor = Color.Black;
		}

		#region Overrides of CommonAbstractViewer<InventoryItem_Printing_cu>

		public override IMVCController<InventoryItem_Printing_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.InventoryItem_Printing_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "المنتجــــات"; }
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
			spnCalculatedCost.EditValue = null;
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

		public object Date
		{
			get { return dtDate.EditValue; }
			set { dtDate.EditValue = value; }
		}

		public object PrintingMaxTimeInMinutes
		{
			get { return spnPrintingMaxTimeInMinutes.EditValue; }
			set { spnPrintingMaxTimeInMinutes.EditValue = value; }
		}

		public object AddedMinutes
		{
			get { return spnAddedMinutes.EditValue; }
			set { spnAddedMinutes.EditValue = value; }
		}

		public object PrintingAverageUnitCostPrice
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

		public object Description
		{
			get { return txtDescription.EditValue; }
			set { txtDescription.EditValue = value; }
		}

		#endregion

		private void spnPrintingMaxTimeInMinutes_EditValueChanged(object sender, EventArgs e)
		{
			spnCalculatedCost.EditValue = Convert.ToDouble(spnPrintingMaxTimeInMinutes.EditValue) *
			                              Convert.ToDouble(spnPrintingAverageUnitCostPrice.EditValue);
			spnTotalCalculatedCost.EditValue =
				(Convert.ToDouble(spnPrintingMaxTimeInMinutes.EditValue) +
				 Convert.ToDouble(spnAddedMinutes.EditValue)) *
				Convert.ToDouble(spnPrintingAverageUnitCostPrice.EditValue);
		}

		private void spnPrintingAverageUnitCostPrice_EditValueChanged(object sender, EventArgs e)
		{
			spnCalculatedCost.EditValue = Convert.ToDouble(spnPrintingMaxTimeInMinutes.EditValue) *
			                              Convert.ToDouble(spnPrintingAverageUnitCostPrice.EditValue);
			spnTotalCalculatedCost.EditValue =
				(Convert.ToDouble(spnPrintingMaxTimeInMinutes.EditValue) +
				 Convert.ToDouble(spnAddedMinutes.EditValue)) *
				Convert.ToDouble(spnPrintingAverageUnitCostPrice.EditValue);
		}

		private void spnAddedMinutes_EditValueChanged(object sender, EventArgs e)
		{
			spnCalculatedCost.EditValue = Convert.ToDouble(spnPrintingMaxTimeInMinutes.EditValue) *
			                              Convert.ToDouble(spnPrintingAverageUnitCostPrice.EditValue);
			spnTotalCalculatedCost.EditValue =
				(Convert.ToDouble(spnPrintingMaxTimeInMinutes.EditValue) +
				 Convert.ToDouble(spnAddedMinutes.EditValue)) *
				Convert.ToDouble(spnPrintingAverageUnitCostPrice.EditValue);
		}
	}
}
