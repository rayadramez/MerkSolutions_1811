using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace CommonUserControls.PEMRCommonViewers
{
	public partial class PEMR_PrintOrder_UC : UserControl
	{
		private List<PEM_ElementPrintOrder_cu> List_PrintOrders { get; set; }
		public PEM_ElementPrintOrder_cu Selected_PEM_ElementPrintOrder { get; set; }

		public PEMR_PrintOrder_UC()
		{
			InitializeComponent();

			CommonViewsActions.SetupGridControl(grdElements, Resources.LocalizedRes.grd_PEMR_Element_OrderIndex_Internal, true);
			CommonViewsActions.FillGridlookupEdit(lkeHeaderElement,
			                                      PEMR_Elemet_p.ItemsList.FindAll(
				                                      item => item.IsHead != null && Convert.ToBoolean(item.IsHead)));
		}

		public void ClearControls()
		{
			lkeChildElement.EditValue = null;
			if (List_PrintOrders == null || List_PrintOrders.Count == 0)
				spnOrderIndex.EditValue = 1;
			else
				spnOrderIndex.EditValue = List_PrintOrders.Count + 1;

			grdVu_Element.IsRowSelected(-1);
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			if(ParentForm != null)
				ParentForm.Close();
		}

		private void lkeHeaderElement_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeHeaderElement == null)
				return;

			PEMR_Elemet_p element =
				PEMR_Elemet_p.ItemsList.Find(item => Convert.ToInt32(item.ID)
					                             .Equals(Convert.ToInt32(lkeHeaderElement.EditValue)));
			if (element == null)
				return;

			List<PEMR_Elemet_p> list =
				PEMR_Elemet_p.ItemsList.FindAll(item => Convert.ToInt32(item.ParentID)
					                                .Equals(Convert.ToInt32(element.ID)));
			CommonViewsActions.FillGridlookupEdit(lkeChildElement, list, "Name_S");
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (List_PrintOrders == null || List_PrintOrders.Count == 0)
			{
				XtraMessageBox.Show("The list has no Elements", "Notice",
									MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
									DefaultBoolean.Default);
				return;
			}

			if (PEM_ElementPrintOrder_cu.ItemsList.Count == 0)
				foreach (PEM_ElementPrintOrder_cu pemElementPrintOrderCu in List_PrintOrders)
					pemElementPrintOrderCu.SaveChanges();
			//else
			//{
			//	foreach (PEM_ElementPrintOrder_cu pemElementPrintOrderCu in PEM_ElementPrintOrder_cu.ItemsList)
			//	{
			//		pemElementPrintOrderCu.DBCommonTransactionType = DB_CommonTransactionType.DeleteExisting;
			//		pemElementPrintOrderCu.SaveChanges();
			//	}

			//	foreach (PEM_ElementPrintOrder_cu pemElementPrintOrderCu in List_PrintOrders)
			//	{
			//		pemElementPrintOrderCu.DBCommonTransactionType = DB_CommonTransactionType.CreateNew;
			//		pemElementPrintOrderCu.SaveChanges();
			//	}
			//}

			XtraMessageBox.Show("Saved Successfully ...", "Notice",
			                    MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
			                    DefaultBoolean.Default);
		}

		private void btnSaveAndClose_Click(object sender, EventArgs e)
		{

		}

		private void btnAddToList_Click(object sender, EventArgs e)
		{
			if (lkeChildElement.EditValue == null)
			{
				XtraMessageBox.Show("You should select Element before adding", "Notice",
									MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
									DefaultBoolean.Default);
				return;
			}

			if (spnOrderIndex.EditValue == null)
			{
				XtraMessageBox.Show("You should enter the Order Index before adding", "Notice",
									MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
									DefaultBoolean.Default);
				return;
			}

			if (List_PrintOrders == null)
				List_PrintOrders = new List<PEM_ElementPrintOrder_cu>();

			PEM_ElementPrintOrder_cu printOrder = DBCommon.CreateNewDBEntity<PEM_ElementPrintOrder_cu>();
			printOrder.PEMR_Elemet_P_ID = Convert.ToInt32(lkeChildElement.EditValue);
			printOrder.OrderIndex = Convert.ToInt32(spnOrderIndex.EditValue);
			printOrder.IsOnDuty = true;
			printOrder.InsertedBy = ApplicationStaticConfiguration.ActiveLoginUser.ID;
			if (txtDescription.EditValue != null && string.IsNullOrEmpty(txtDescription.Text)
			                                     && string.IsNullOrWhiteSpace(txtDescription.Text))
				printOrder.AddedDescription = txtDescription.Text;

			if (!List_PrintOrders.Exists(item => Convert.ToInt32(item.PEMR_Elemet_P_ID)
				                             .Equals(Convert.ToInt32(printOrder.PEMR_Elemet_P_ID))))
				List_PrintOrders.Add(printOrder);

			grdElements.DataSource = List_PrintOrders.OrderBy(item => item.OrderIndex);
			grdElements.RefreshDataSource();
			ClearControls();
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			if (Selected_PEM_ElementPrintOrder == null)
			{
				XtraMessageBox.Show("You should select the Element before Removing", "Notice",
									MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
									DefaultBoolean.Default);
				return;
			}

			if (List_PrintOrders == null || List_PrintOrders.Count == 0)
			{
				XtraMessageBox.Show("The list has no Elements", "Notice",
									MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
									DefaultBoolean.Default);
				return;
			}

			List_PrintOrders.Remove(Selected_PEM_ElementPrintOrder);
			int index = 1;
			foreach (PEM_ElementPrintOrder_cu printOrder in List_PrintOrders.OrderBy(item => item.OrderIndex))
			{
				printOrder.OrderIndex = index;
				index++;
			}

			grdElements.DataSource = List_PrintOrders.OrderBy(item => item.OrderIndex);
			grdElements.RefreshDataSource();
			ClearControls();
		}

		private void btnUp_Click(object sender, EventArgs e)
		{
			if (Selected_PEM_ElementPrintOrder == null)
			{
				XtraMessageBox.Show("You should select the Element before Removing", "Notice",
									MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
									DefaultBoolean.Default);
				return;
			}

			if (List_PrintOrders == null || List_PrintOrders.Count == 0)
			{
				XtraMessageBox.Show("The list has no Elements", "Notice",
									MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
									DefaultBoolean.Default);
				return;
			}

			PEM_ElementPrintOrder_cu printOrder = List_PrintOrders.Find(
				item => Convert.ToInt32(item.OrderIndex)
					.Equals(Convert.ToInt32(Selected_PEM_ElementPrintOrder.OrderIndex)));
			if (printOrder == null)
				return;

			PEM_ElementPrintOrder_cu previousElement = null;
			foreach (PEM_ElementPrintOrder_cu order in List_PrintOrders.OrderBy(item => item.OrderIndex))
			{
				if (order.Equals(printOrder))
					break;

				previousElement = order;
			}

			if (previousElement != null)
			{
				if (previousElement.OrderIndex <= 0)
					return;
				previousElement.OrderIndex = previousElement.OrderIndex + 1;
			}
			else
				return;

			printOrder.OrderIndex = printOrder.OrderIndex - 1;

			grdElements.DataSource = List_PrintOrders.OrderBy(item => item.OrderIndex);
			grdElements.RefreshDataSource();
			ClearControls();
		}

		private void btnDown_Click(object sender, EventArgs e)
		{
			if (Selected_PEM_ElementPrintOrder == null)
			{
				XtraMessageBox.Show("You should select the Element before Removing", "Notice",
									MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
									DefaultBoolean.Default);
				return;
			}

			if (List_PrintOrders == null || List_PrintOrders.Count == 0)
			{
				XtraMessageBox.Show("The list has no Elements", "Notice",
									MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
									DefaultBoolean.Default);
				return;
			}

			PEM_ElementPrintOrder_cu printOrder = List_PrintOrders.Find(
				item => Convert.ToInt32(item.OrderIndex)
					.Equals(Convert.ToInt32(Selected_PEM_ElementPrintOrder.OrderIndex)));
			if (printOrder == null)
				return;

			PEM_ElementPrintOrder_cu previousElement = null;
			foreach (PEM_ElementPrintOrder_cu order in List_PrintOrders.OrderByDescending(item => item.OrderIndex))
			{
				if (order.Equals(printOrder))
					break;

				previousElement = order;
			}

			if (previousElement != null)
			{
				if (previousElement.OrderIndex <= 0)
					return;
				previousElement.OrderIndex = previousElement.OrderIndex +- 1;
			}
			else
				return;

			printOrder.OrderIndex = printOrder.OrderIndex + 1;

			grdElements.DataSource = List_PrintOrders.OrderBy(item => item.OrderIndex);
			grdElements.RefreshDataSource();
			ClearControls();
		}

		private void gridView2_MouseUp(object sender, MouseEventArgs e)
		{
		}

		private void grdVu_Element_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
		{
			Selected_PEM_ElementPrintOrder =
				CommonViewsActions.GetSelectedRowObject<PEM_ElementPrintOrder_cu>(grdVu_Element);

		}
	}
}
