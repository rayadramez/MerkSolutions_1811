using System;
using System.Drawing;
using System.Windows.Forms;
using CommonControlLibrary;
using DevExpress.XtraLayout.Utils;
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
			CommonViewsActions.FillGridlookupEdit(lkeRawMaterials, RawMaterials_cu.ItemsList);

			chkAverageTime.Checked = true;
		}

		public override void ClearControls()
		{
			lkeInventoryItems.EditValue = null;
			dtDate.EditValue = DateTime.Now;
			spnTotalTime.EditValue = null;
			spnAddedMinutes.EditValue = null;
			spnMinuteCost.EditValue = null;
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

		public object RawMaterialsID
		{
			get { return lkeRawMaterials.EditValue; }
			set { lkeRawMaterials.EditValue = value; }
		}

		public object Date
		{
			get { return dtDate.EditValue; }
			set { dtDate.EditValue = value; }
		}

		public object TotalMinutes
		{
			get { return spnTotalTime.EditValue; }
			set { spnTotalTime.EditValue = value; }
		}

		public object LightMinutes
		{
			get { return spnLightTime.EditValue; }
			set { spnLightTime.EditValue = value; }
		}

		public object AddedMinutes
		{
			get { return spnAddedMinutes.EditValue; }
			set { spnAddedMinutes.EditValue = value; }
		}

		public object UseAverageCostPrice
		{
			get { return chkAverageTime.Checked; }
			set
			{
				if (Convert.ToBoolean(value))
					chkAverageTime.Checked = true;
				else
					chkAverageTime.Checked = true;
			}
		}

		public object MinuteUnitCost
		{
			get { return spnMinuteCost.EditValue; }
			set { spnMinuteCost.EditValue = value; }
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

		private void spnPrintingMaxTimeInMinutes_EditValueChanged(object sender, EventArgs e)
		{
			if (TotalMinutes == null || LightMinutes == null || MinuteUnitCost == null)
				return;

			double totalMinutes = Convert.ToDouble(TotalMinutes);
			double lightMinutes = Convert.ToDouble(LightMinutes);
			double unitCost = Convert.ToDouble(MinuteUnitCost);
			double cost = 0;
			double totalCost = 0;

			if (chkAverageTime.Checked)
				if (AddedMinutes != null)
				{
					cost = (((totalMinutes + lightMinutes) / 2)) * unitCost;
					totalCost = (((totalMinutes + lightMinutes) / 2) + Convert.ToDouble(AddedMinutes)) * unitCost;
				}
				else
					cost = totalCost = ((totalMinutes + lightMinutes) / 2) * unitCost;
			else if (chkTotalTime.Checked)
				if (AddedMinutes != null)
				{
					cost = totalMinutes * unitCost;
					totalCost = (totalMinutes + Convert.ToDouble(AddedMinutes)) * unitCost;
				}
				else
					cost = totalCost = totalMinutes * unitCost;

			spnCalculatedCost.EditValue = cost;
			spnTotalCalculatedCost.EditValue = totalCost;
		}

		private void spnPrintingAverageUnitCostPrice_EditValueChanged(object sender, EventArgs e)
		{
			if (TotalMinutes == null || LightMinutes == null || MinuteUnitCost == null)
				return;

			double totalMinutes = Convert.ToDouble(TotalMinutes);
			double lightMinutes = Convert.ToDouble(LightMinutes);
			double unitCost = Convert.ToDouble(MinuteUnitCost);
			double cost = 0;
			double totalCost = 0;

			if (chkAverageTime.Checked)
				if (AddedMinutes != null)
				{
					cost = (((totalMinutes + lightMinutes) / 2)) * unitCost;
					totalCost = (((totalMinutes + lightMinutes) / 2) + Convert.ToDouble(AddedMinutes)) * unitCost;
				}
				else
					cost = totalCost = ((totalMinutes + lightMinutes) / 2) * unitCost;
			else if (chkTotalTime.Checked)
				if (AddedMinutes != null)
				{
					cost = totalMinutes * unitCost;
					totalCost = (totalMinutes + Convert.ToDouble(AddedMinutes)) * unitCost;
				}
				else
					cost = totalCost = totalMinutes * unitCost;

			spnCalculatedCost.EditValue = cost;
			spnTotalCalculatedCost.EditValue = totalCost;
		}

		private void spnAddedMinutes_EditValueChanged(object sender, EventArgs e)
		{
			if (TotalMinutes == null || LightMinutes == null || MinuteUnitCost == null)
				return;

			double totalMinutes = Convert.ToDouble(TotalMinutes);
			double lightMinutes = Convert.ToDouble(LightMinutes);
			double unitCost = Convert.ToDouble(MinuteUnitCost);
			double cost = 0;
			double totalCost = 0;

			if (chkAverageTime.Checked)
				if (AddedMinutes != null)
				{
					cost = (((totalMinutes + lightMinutes) / 2)) * unitCost;
					totalCost = (((totalMinutes + lightMinutes) / 2) + Convert.ToDouble(AddedMinutes)) * unitCost;
				}
				else
					cost = totalCost = ((totalMinutes + lightMinutes) / 2) * unitCost;
			else if (chkTotalTime.Checked)
				if (AddedMinutes != null)
				{
					cost = totalMinutes * unitCost;
					totalCost = (totalMinutes + Convert.ToDouble(AddedMinutes)) * unitCost;
				}
				else
					cost = totalCost = totalMinutes * unitCost;

			spnCalculatedCost.EditValue = cost;
			spnTotalCalculatedCost.EditValue = totalCost;
		}

		private void chkUseExpectedCost_CheckedChanged(object sender, EventArgs e)
		{
			lytEstimatedCost.Visibility = chkUseExpectedCost.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			lytRealCost.Visibility = chkUseRealCost.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		private void chkUseRealCost_CheckedChanged(object sender, EventArgs e)
		{
			lytEstimatedCost.Visibility = chkUseExpectedCost.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			lytRealCost.Visibility = chkUseRealCost.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		private void chkAverageTime_CheckedChanged(object sender, EventArgs e)
		{
			if(TotalMinutes == null || LightMinutes == null || MinuteUnitCost == null)
				return;

			double totalMinutes = Convert.ToDouble(TotalMinutes);
			double lightMinutes = Convert.ToDouble(LightMinutes);
			double unitCost = Convert.ToDouble(MinuteUnitCost);
			double cost = 0;
			double totalCost = 0;

			if (chkAverageTime.Checked)
				if (AddedMinutes != null)
				{
					cost = (((totalMinutes + lightMinutes) / 2)) * unitCost;
					totalCost = (((totalMinutes + lightMinutes) / 2) + Convert.ToDouble(AddedMinutes)) * unitCost;
				}
				else
					cost = totalCost = ((totalMinutes + lightMinutes) / 2) * unitCost;
			else if(chkTotalTime.Checked)
				if (AddedMinutes != null)
				{
					cost = totalMinutes * unitCost;
					totalCost = (totalMinutes + Convert.ToDouble(AddedMinutes)) * unitCost;
				}
				else
					cost = totalCost = totalMinutes * unitCost;

			spnCalculatedCost.EditValue = cost;
			spnTotalCalculatedCost.EditValue = totalCost;
		}

		private void chkTotalTime_CheckedChanged(object sender, EventArgs e)
		{
			if(TotalMinutes == null || LightMinutes == null || MinuteUnitCost == null)
				return;

			double totalMinutes = Convert.ToDouble(TotalMinutes);
			double lightMinutes = Convert.ToDouble(LightMinutes);
			double unitCost = Convert.ToDouble(MinuteUnitCost);
			double cost = 0;
			double totalCost = 0;

			if (chkAverageTime.Checked)
				if (AddedMinutes != null)
				{
					cost = (((totalMinutes + lightMinutes) / 2)) * unitCost;
					totalCost = (((totalMinutes + lightMinutes) / 2) + Convert.ToDouble(AddedMinutes)) * unitCost;
				}
				else
					cost = totalCost = ((totalMinutes + lightMinutes) / 2) * unitCost;
			else if(chkTotalTime.Checked)
				if (AddedMinutes != null)
				{
					cost = totalMinutes * unitCost;
					totalCost = (totalMinutes + Convert.ToDouble(AddedMinutes)) * unitCost;
				}
				else
					cost = totalCost = totalMinutes * unitCost;

			spnCalculatedCost.EditValue = cost;
			spnTotalCalculatedCost.EditValue = totalCost;
		}
	}
}
