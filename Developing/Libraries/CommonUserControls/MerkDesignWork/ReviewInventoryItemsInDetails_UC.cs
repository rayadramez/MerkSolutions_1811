using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CommonControlLibrary;
using CommonControlLibrary.ControlsConstructors;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.HitInfo;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;

namespace CommonUserControls.MerkDesignWork
{
	public partial class ReviewInventoryItemsInDetails_UC : UserControl
	{
		public ReviewInventoryItemsInDetails_UC()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1,
				Resources.LocalizedRes.lyt_ReviewInventoryItemCostDetails_Viewer);
			CommonViewsActions.SetupSyle(this);

			CommonViewsActions.SetupGridControl(grdAreaParts,
				Resources.LocalizedRes.grd_ReviewInventoryItemCostDetails_Area_Viewer, false);
			CommonViewsActions.SetupGridControl(grdPrinting,
				Resources.LocalizedRes.grd_ReviewInventoryItemCostDetails_Printing_Viewer, false);

			CommonViewsActions.FillGridlookupEdit(lkeInventoryItems,
				InventoryItem_cu.ItemsList.OrderBy(item => item.InternalCode).ToList());
			CommonViewsActions.FillGridlookupEdit(lkeInternalCode,
				InventoryItem_cu.ItemsList.OrderBy(item => item.InternalCode).ToList(), "InternalCode");
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			if (ParentForm != null)
				ParentForm.Close();
		}

		private void chkUseProfitPerventage_CheckedChanged(object sender, EventArgs e)
		{
			if (chkUseProfitPerventage.Checked)
			{
				spnProfitPercentage.Properties.ReadOnly = false;
				spnSellingPrice.Properties.ReadOnly = true;
				spnProfit.Properties.ReadOnly = true;
			}
			else if (chkSellingPrice.Checked)
			{
				spnProfitPercentage.Properties.ReadOnly = true;
				spnSellingPrice.Properties.ReadOnly = false;
				spnProfit.Properties.ReadOnly = true;
			}
			else if (chkProfit.Checked)
			{
				spnProfitPercentage.Properties.ReadOnly = true;
				spnSellingPrice.Properties.ReadOnly = true;
				spnProfit.Properties.ReadOnly = false;
			}
		}

		private void chkSellingPrice_CheckedChanged(object sender, EventArgs e)
		{
			if (chkUseProfitPerventage.Checked)
			{
				spnProfitPercentage.Properties.ReadOnly = false;
				spnSellingPrice.Properties.ReadOnly = true;
				spnProfit.Properties.ReadOnly = true;
			}
			else if (chkSellingPrice.Checked)
			{
				spnProfitPercentage.Properties.ReadOnly = true;
				spnSellingPrice.Properties.ReadOnly = false;
				spnProfit.Properties.ReadOnly = true;
			}
			else if (chkProfit.Checked)
			{
				spnProfitPercentage.Properties.ReadOnly = true;
				spnSellingPrice.Properties.ReadOnly = true;
				spnProfit.Properties.ReadOnly = false;
			}
		}

		private void chkProfit_CheckedChanged(object sender, EventArgs e)
		{
			if (chkUseProfitPerventage.Checked)
			{
				spnProfitPercentage.Properties.ReadOnly = false;
				spnSellingPrice.Properties.ReadOnly = true;
				spnProfit.Properties.ReadOnly = true;
			}
			else if (chkSellingPrice.Checked)
			{
				spnProfitPercentage.Properties.ReadOnly = true;
				spnSellingPrice.Properties.ReadOnly = false;
				spnProfit.Properties.ReadOnly = true;
			}
			else if (chkProfit.Checked)
			{
				spnProfitPercentage.Properties.ReadOnly = true;
				spnSellingPrice.Properties.ReadOnly = true;
				spnProfit.Properties.ReadOnly = false;
			}
		}

		private void btnCalculate_Click(object sender, EventArgs e)
		{
			List<InventoryItemDetailsConstructor> list = new List<InventoryItemDetailsConstructor>();
			List<InventoryItemDetailsConstructor> summaryList = new List<InventoryItemDetailsConstructor>();

			if (lkeInventoryItems.EditValue != null && lkeInternalCode.EditValue == null)
				list = InventoryBusinessLogicEngine.GetParentConstructorDetailsList(lkeInventoryItems.EditValue);
			else if (lkeInternalCode.EditValue != null)
				list = InventoryBusinessLogicEngine.GetParentConstructorDetailsList(lkeInternalCode.EditValue);
			else if (lkeInventoryItems.EditValue == null && lkeInternalCode.EditValue == null)
				XtraMessageBox.Show("يجـــب إختيـــار المنتــج", "تنبيـــه", MessageBoxButtons.OK, MessageBoxIcon.Error,
					MessageBoxDefaultButton.Button1, DefaultBoolean.Default);

			summaryList = list.FindAll(item => item.ListType.Equals(ListType.SummaryInventoryItems));
			List<InventoryItem_AreaPartsDetailsConstructor> areaPartsList = new List<InventoryItem_AreaPartsDetailsConstructor>();
			List<InventoryItem_PrintingDetailsConstructor> printingList = new List<InventoryItem_PrintingDetailsConstructor>();

			if (summaryList != null && summaryList.Count > 0)
				areaPartsList =
					summaryList.SelectMany(item => item.List_InventoryItem_AreaPartsDetailsConstructor).ToList();
			if (summaryList != null && summaryList.Count > 0)
				printingList =
					summaryList.SelectMany(item => item.List_InventoryItem_PrintingDetailsConstructor).ToList();

			grdAreaParts.DataSource = areaPartsList;
			grdPrinting.DataSource = printingList;

			spnWidth.EditValue = list[0].ItemWidth;
			spnHeight.EditValue = list[0].ItemHeight;
			spnDepth.EditValue = list[0].ItemDepth;
			spnTotalParts.EditValue = list[0].PartsCount;
			spnTotalAreaParts.EditValue = list[0].TotalPartsArea;
			spnPrintingUnitCost.EditValue = list[0].PrintingUnitCost;
		}

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

		private void lkeInventoryItems_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeInventoryItems.EditValue == null)
				return;

			InventoryItem_cu inventoryItem = InventoryBusinessLogicEngine.GetInventoryItem(lkeInventoryItems.EditValue);
			if (inventoryItem != null)
				lkeInternalCode.EditValue = inventoryItem.ID;

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
	}
}
