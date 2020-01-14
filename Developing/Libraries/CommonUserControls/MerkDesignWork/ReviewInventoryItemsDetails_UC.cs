using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonControlLibrary;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.HitInfo;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace CommonUserControls.MerkDesignWork
{
	public enum SellingPriceStrategey
	{
		Percentage = 1,
		SellingPrice = 2,
		Profit = 3
	}

	public partial class ReviewInventoryItemsDetails_UC : UserControl
	{
		public SellingPriceStrategey SellingPriceStrategey { get; set; }

		public ReviewInventoryItemsDetails_UC()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1,
				Resources.LocalizedRes.lyt_ReviewInventoryItemsDetails_UC);
			CommonViewsActions.SetupSyle(this);

			CommonViewsActions.SetupGridControl(grd_AreaParts,
				Resources.LocalizedRes.grd_InventoryItem_Area_SearchViewer, false);
			CommonViewsActions.SetupGridControl(grd_Printing,
				Resources.LocalizedRes.grd_InventoryItem_Printing_SearchViewer, false);
			CommonViewsActions.SetupGridControl(grd_RawMaterials,
				Resources.LocalizedRes.grd_InventoryItem_RawMaterial_SearchViewer, false);
			//CommonViewsActions.SetupGridControl(grd_Color,
			//	Resources.LocalizedRes.grd_InventoryItem_Area_SearchViewer, false);

			CommonViewsActions.FillGridlookupEdit(lkeInventoryItems,
				InventoryItem_cu.ItemsList.OrderBy(item => item.InternalCode).ToList());
			CommonViewsActions.FillGridlookupEdit(lkeInternalCode,
				InventoryItem_cu.ItemsList.OrderBy(item => item.InternalCode).ToList(), "InternalCode");

			SellingPriceStrategey = SellingPriceStrategey.Percentage;
		}

		#region Controls Events

		#region LayoutGroup

		private void lytGroup_AreaParts_MouseUp(object sender, MouseEventArgs e)
		{
			BaseLayoutItemHitInfo hitInfo = layoutControl1.CalcHitInfo(e.Location);
			if (hitInfo.HitType != LayoutItemHitTest.Item || e.Clicks != 1)
				return;
			LayoutGroup group = hitInfo.Item as LayoutGroup;
			if (group != null)
			{
				if (group.ViewInfo.BorderInfo.CaptionBounds.Contains(e.Location))
				{
					group.Expanded = !group.Expanded;
					if (lytGroup_Printing.Expanded)
						lytGroup_Printing.Expanded = false;
					if (lytGroup_RawMaterials.Expanded)
						lytGroup_RawMaterials.Expanded = false;
					if (lytGroup_Color.Expanded)
						lytGroup_Color.Expanded = false;
				}
			}
		}

		private void lytGroup_Printing_MouseUp(object sender, MouseEventArgs e)
		{
			BaseLayoutItemHitInfo hitInfo = layoutControl1.CalcHitInfo(e.Location);
			if (hitInfo.HitType != LayoutItemHitTest.Item || e.Clicks != 1)
				return;
			LayoutGroup group = hitInfo.Item as LayoutGroup;
			if (group != null)
			{
				if (group.ViewInfo.BorderInfo.CaptionBounds.Contains(e.Location))
				{
					group.Expanded = !group.Expanded;
					if (lytGroup_AreaParts.Expanded)
						lytGroup_AreaParts.Expanded = false;
					if (lytGroup_RawMaterials.Expanded)
						lytGroup_RawMaterials.Expanded = false;
					if (lytGroup_Color.Expanded)
						lytGroup_Color.Expanded = false;
				}
			}
		}

		private void lytGroup_RawMaterials_MouseUp(object sender, MouseEventArgs e)
		{
			BaseLayoutItemHitInfo hitInfo = layoutControl1.CalcHitInfo(e.Location);
			if (hitInfo.HitType != LayoutItemHitTest.Item || e.Clicks != 1)
				return;
			LayoutGroup group = hitInfo.Item as LayoutGroup;
			if (group != null)
			{
				if (group.ViewInfo.BorderInfo.CaptionBounds.Contains(e.Location))
				{
					group.Expanded = !group.Expanded;
					if (lytGroup_AreaParts.Expanded)
						lytGroup_AreaParts.Expanded = false;
					if (lytGroup_Printing.Expanded)
						lytGroup_Printing.Expanded = false;
					if (lytGroup_Color.Expanded)
						lytGroup_Color.Expanded = false;
				}
			}
		}

		private void lytGroup_Color_MouseUp(object sender, MouseEventArgs e)
		{
			BaseLayoutItemHitInfo hitInfo = layoutControl1.CalcHitInfo(e.Location);
			if (hitInfo.HitType != LayoutItemHitTest.Item || e.Clicks != 1)
				return;
			LayoutGroup group = hitInfo.Item as LayoutGroup;
			if (group != null)
			{
				if (group.ViewInfo.BorderInfo.CaptionBounds.Contains(e.Location))
				{
					group.Expanded = !group.Expanded;
					if (lytGroup_AreaParts.Expanded)
						lytGroup_AreaParts.Expanded = false;
					if (lytGroup_Printing.Expanded)
						lytGroup_Printing.Expanded = false;
					if (lytGroup_RawMaterials.Expanded)
						lytGroup_RawMaterials.Expanded = false;
				}
			}
		}

		#endregion

		#region Buttons

		private void btnCalculate_Click(object sender, EventArgs e)
		{
			if (lkeInventoryItems.EditValue == null && lkeInternalCode.EditValue == null)
			{
				XtraMessageBox.Show("يجـــب إختيـــار المنتــج", "تنبيـــه", MessageBoxButtons.OK, MessageBoxIcon.Error,
					MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
				return;
			}

			InventoryItem_cu selectedItem = null;
			if (lkeInventoryItems.EditValue != null || lkeInternalCode.EditValue == null)
				selectedItem = InventoryBusinessLogicEngine.GetInventoryItem(lkeInventoryItems.EditValue);
			else if (lkeInventoryItems.EditValue == null || lkeInternalCode.EditValue != null)
				selectedItem = InventoryBusinessLogicEngine.GetInventoryItem(lkeInternalCode.EditValue);

			if (selectedItem == null)
			{
				XtraMessageBox.Show("هـــذا المنـــج غيـــر مـوجــــــود", "تنبيـــه", MessageBoxButtons.OK, MessageBoxIcon.Error,
					MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
				return;
			}

			List<InventoryItem_Area> areaList = InventoryItem_Area.ItemsList.FindAll(item =>
				Convert.ToInt32(item.InventoryItemID).Equals(Convert.ToInt32(selectedItem.ID)));
			List<InventoryItem_RawMaterial_cu> rawMaterialsList = InventoryItem_RawMaterial_cu.ItemsList.FindAll(item =>
				Convert.ToInt32(item.InventoryItem_CU_ID).Equals(Convert.ToInt32(selectedItem.ID)));
			List<InventoryItem_Printing_cu> printingList;
			if (rawMaterialsList.Count != 0)
				printingList = InventoryBusinessLogicEngine.GetInventoryItem_Printing_List(selectedItem);
			else
			{
				printingList = InventoryItem_Printing_cu.ItemsList.FindAll(item =>
					Convert.ToInt32(item.InventoryItem_CU_ID).Equals(Convert.ToInt32(selectedItem.ID)));
				XtraMessageBox.Show("المنتـــج غيـــر مـربــوط بمــــواد الخــــام", "تنبيـــه", MessageBoxButtons.OK, MessageBoxIcon.Error,
					MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
			}
			List<InventoryItem_Color_cu> colorsList = InventoryItem_Color_cu.ItemsList.FindAll(item =>
				Convert.ToInt32(item.InventoryItem_CU_ID).Equals(Convert.ToInt32(selectedItem.ID)));

			grd_AreaParts.DataSource = areaList;
			grd_Printing.DataSource = printingList.OrderByDescending(item => item.Date);
			grd_RawMaterials.DataSource = rawMaterialsList;
			grd_Color.DataSource = colorsList;

			spnTotalParts.EditValue = InventoryBusinessLogicEngine.GetInventoryItemTotalCountParts(selectedItem);
			spnTotalAreaParts.EditValue = InventoryBusinessLogicEngine.GetInventoryItemTotalAreaParts(selectedItem);
			spnPrintingUnitCost.EditValue = InventoryBusinessLogicEngine.GetPrintingCost(printingList);
			spnRawMaterialsCost.EditValue = InventoryBusinessLogicEngine.GetRawMaterialsCost(rawMaterialsList);
			spnRawMaterialsArea.EditValue = InventoryBusinessLogicEngine.GetRawMaterialsTotalArea(rawMaterialsList);

			spnTotalCost.EditValue = Convert.ToDouble(spnRawMaterialsCost.EditValue) +
			                         Convert.ToDouble(spnPrintingUnitCost.EditValue);
		}

		#endregion

		#region CheckEdit

		private void chkUseProfitPerventage_CheckedChanged(object sender, EventArgs e)
		{
			if (chkUseProfitPerventage.Checked)
			{
				spnProfitPercentage.Properties.ReadOnly = false;
				spnSellingPrice.Properties.ReadOnly = true;
				spnProfit.Properties.ReadOnly = true;

				spnProfitPercentage.EditValue = 0;
				spnSellingPrice.EditValue = 0;
				spnProfit.EditValue = 0;

				SellingPriceStrategey = SellingPriceStrategey.Percentage;
			}
			else if (chkSellingPrice.Checked)
			{
				spnProfitPercentage.Properties.ReadOnly = true;
				spnSellingPrice.Properties.ReadOnly = false;
				spnProfit.Properties.ReadOnly = true;

				spnProfitPercentage.EditValue = 0;
				spnSellingPrice.EditValue = 0;
				spnProfit.EditValue = 0;

				SellingPriceStrategey = SellingPriceStrategey.SellingPrice;
			}
			else if (chkProfit.Checked)
			{
				spnProfitPercentage.Properties.ReadOnly = true;
				spnSellingPrice.Properties.ReadOnly = true;
				spnProfit.Properties.ReadOnly = false;

				spnProfitPercentage.EditValue = 0;
				spnSellingPrice.EditValue = 0;
				spnProfit.EditValue = 0;

				SellingPriceStrategey = SellingPriceStrategey.Profit;
			}
		}

		private void chkSellingPrice_CheckedChanged(object sender, EventArgs e)
		{
			if (chkUseProfitPerventage.Checked)
			{
				spnProfitPercentage.Properties.ReadOnly = false;
				spnSellingPrice.Properties.ReadOnly = true;
				spnProfit.Properties.ReadOnly = true;

				spnProfitPercentage.EditValue = 0;
				spnSellingPrice.EditValue = 0;
				spnProfit.EditValue = 0;

				SellingPriceStrategey = SellingPriceStrategey.Percentage;
			}
			else if (chkSellingPrice.Checked)
			{
				spnProfitPercentage.Properties.ReadOnly = true;
				spnSellingPrice.Properties.ReadOnly = false;
				spnProfit.Properties.ReadOnly = true;

				spnProfitPercentage.EditValue = 0;
				spnSellingPrice.EditValue = 0;
				spnProfit.EditValue = 0;

				SellingPriceStrategey = SellingPriceStrategey.SellingPrice;
			}
			else if (chkProfit.Checked)
			{
				spnProfitPercentage.Properties.ReadOnly = true;
				spnSellingPrice.Properties.ReadOnly = true;
				spnProfit.Properties.ReadOnly = false;

				spnProfitPercentage.EditValue = 0;
				spnSellingPrice.EditValue = 0;
				spnProfit.EditValue = 0;

				SellingPriceStrategey = SellingPriceStrategey.Profit;
			}
		}

		private void chkProfit_CheckedChanged(object sender, EventArgs e)
		{
			if (chkUseProfitPerventage.Checked)
			{
				spnProfitPercentage.Properties.ReadOnly = false;
				spnSellingPrice.Properties.ReadOnly = true;
				spnProfit.Properties.ReadOnly = true;

				spnProfitPercentage.EditValue = 0;
				spnSellingPrice.EditValue = 0;
				spnProfit.EditValue = 0;

				SellingPriceStrategey = SellingPriceStrategey.Percentage;
			}
			else if (chkSellingPrice.Checked)
			{
				spnProfitPercentage.Properties.ReadOnly = true;
				spnSellingPrice.Properties.ReadOnly = false;
				spnProfit.Properties.ReadOnly = true;

				spnProfitPercentage.EditValue = 0;
				spnSellingPrice.EditValue = 0;
				spnProfit.EditValue = 0;

				SellingPriceStrategey = SellingPriceStrategey.SellingPrice;
			}
			else if (chkProfit.Checked)
			{
				spnProfitPercentage.Properties.ReadOnly = true;
				spnSellingPrice.Properties.ReadOnly = true;
				spnProfit.Properties.ReadOnly = false;

				spnProfitPercentage.EditValue = 0;
				spnSellingPrice.EditValue = 0;
				spnProfit.EditValue = 0;

				SellingPriceStrategey = SellingPriceStrategey.Profit;
			}
		}

		private void chkCM_CheckedChanged(object sender, EventArgs e)
		{
			InventoryItem_Area._SizeUnitMeasure = DB_SizeUnitMeasure.CM;
			btnCalculate_Click(null, null);
		}

		private void chkMM_CheckedChanged(object sender, EventArgs e)
		{
			InventoryItem_Area._SizeUnitMeasure = DB_SizeUnitMeasure.MM;
			btnCalculate_Click(null, null);
		}

		#endregion

		#region SpinEdit

		private void spnProfitPercentage_EditValueChanged(object sender, EventArgs e)
		{
			if (SellingPriceStrategey == SellingPriceStrategey.Percentage)
			{
				double cost = Convert.ToDouble(spnTotalCost.EditValue);
				if (spnAdditionalCostAmount.EditValue != null)
					cost += Convert.ToDouble(spnAdditionalCostAmount.EditValue);
				double sellingPrice = cost * (100 + Convert.ToDouble(spnProfitPercentage.EditValue))/100;
				
				spnSellingPrice.EditValue = sellingPrice;
				spnProfit.EditValue = sellingPrice - cost;
			}
		}

		private void spnSellingPrice_EditValueChanged(object sender, EventArgs e)
		{
			if (SellingPriceStrategey == SellingPriceStrategey.SellingPrice)
			{
				double cost = Convert.ToDouble(spnTotalCost.EditValue);
				if (spnAdditionalCostAmount.EditValue != null)
					cost += Convert.ToDouble(spnAdditionalCostAmount.EditValue);
				double sellingPrice = Convert.ToDouble(spnSellingPrice.EditValue);
				double percentage = ((sellingPrice * 100) / cost) - 100;

				spnProfitPercentage.EditValue = percentage;
				spnProfit.EditValue = sellingPrice - cost;
			}
		}

		private void spnProfit_EditValueChanged(object sender, EventArgs e)
		{

		}

		private void spnAdditionalCostAmount_EditValueChanged(object sender, EventArgs e)
		{
			if (SellingPriceStrategey == SellingPriceStrategey.Percentage)
			{
				double cost = Convert.ToDouble(spnTotalCost.EditValue);
				if (spnAdditionalCostAmount.EditValue != null)
					cost += Convert.ToDouble(spnAdditionalCostAmount.EditValue);
				double sellingPrice = cost * (100 + Convert.ToDouble(spnProfitPercentage.EditValue)) / 100;

				spnSellingPrice.EditValue = sellingPrice;
				spnProfit.EditValue = sellingPrice - cost;
			}
			else if (SellingPriceStrategey == SellingPriceStrategey.SellingPrice)
			{
				double cost = Convert.ToDouble(spnTotalCost.EditValue);
				if (spnAdditionalCostAmount.EditValue != null)
					cost += Convert.ToDouble(spnAdditionalCostAmount.EditValue);
				double sellingPrice = Convert.ToDouble(spnSellingPrice.EditValue);
				double percentage = ((sellingPrice * 100) / cost) - 100;

				spnProfitPercentage.EditValue = percentage;
				spnProfit.EditValue = sellingPrice - cost;
			}
		}

		#endregion

		#region LookupEdit

		private void lkeInventoryItems_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeInventoryItems.EditValue == null)
				return;

			InventoryItem_cu inventoryItem = InventoryBusinessLogicEngine.GetInventoryItem(lkeInventoryItems.EditValue);
			if (inventoryItem != null)
			{
				lkeInternalCode.EditValue = inventoryItem.ID;
				spnWidth.EditValue = inventoryItem.Width;
				spnHeight.EditValue = inventoryItem.Height;
				spnDepth.EditValue = inventoryItem.Depth;
			}

			btnCalculate_Click(null, null);
		}

		private void lkeInternalCode_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeInternalCode.EditValue == null)
				return;

			InventoryItem_cu inventoryItem = InventoryBusinessLogicEngine.GetInventoryItem(lkeInternalCode.EditValue);
			if (inventoryItem != null)
				lkeInventoryItems.EditValue = inventoryItem.ID;

			btnCalculate_Click(null, null);
		}

		#endregion

		#endregion
	}
}
