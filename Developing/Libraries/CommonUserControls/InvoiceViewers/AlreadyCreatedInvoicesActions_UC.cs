using System;
using System.Collections.Generic;
using System.Linq;
using CommonControlLibrary;
using CommonUserControls.CommonViewers;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using GridView = DevExpress.XtraGrid.Views.Grid.GridView;

namespace CommonUserControls.InvoiceViewers
{
	public partial class AlreadyCreatedInvoicesActions_UC : 
		DevExpress.XtraEditors.XtraUserControl
	{
		private List<GetInvoiceForAddmission_Result> result = new List<GetInvoiceForAddmission_Result>();

		public object InvoiceCreationDateStart
		{
			get { return dtnvoiceCreationDateFrom.EditValue; }
			set { dtnvoiceCreationDateFrom.EditValue = value; }
		}

		public object InvoiceCreationDateEnd
		{
			get { return dtInvoiceCreationDateTo.EditValue; }
			set { dtInvoiceCreationDateTo.EditValue = value; }
		}

		public object PatientID { get; set; }

		public object InvoiceIsPaymentEnough { get; set; }

		public object InvoiceType { get; set; }

		public object DoctorID { get; set; }

		public AlreadyCreatedInvoicesActions_UC()
		{
			InitializeComponent();

			CommonViewsActions.SetupSyle(this);
			CommonViewsActions.SetupGridControl(gridControl1, Resources.LocalizedRes.grd_ReadyInvoicesForActions, false);

			CommonViewsActions.FillGridlookupEdit(lkeDoctor, Doctor_cu.ItemsList);
			InvoiceCreationDateStart = DateTime.Now;
			InvoiceCreationDateEnd = DateTime.Now;
			using (MerkFinanceEntities context = new MerkFinanceEntities())
				result =
					context.GetInvoiceForAddmission(Convert.ToDateTime(InvoiceCreationDateStart),
						Convert.ToDateTime(InvoiceCreationDateEnd), (int?) InvoiceType, true, true, false, (bool?) InvoiceIsPaymentEnough,
						(int?) DoctorID, (int?) PatientID).ToList();

			gridControl1.DataSource = result;
		}

		private void rdInvoicePaymemtStatus_SelectedIndexChanged(object sender, EventArgs e)
		{
			InvoiceIsPaymentEnough = rdInvoicePaymemtStatus.EditValue;
		}

		private void rdInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
		{
			InvoiceType = rdInvoiceType.EditValue;
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			using (MerkFinanceEntities context = new MerkFinanceEntities())
				result =
					context.GetInvoiceForAddmission(Convert.ToDateTime(InvoiceCreationDateStart).Date,
						Convert.ToDateTime(InvoiceCreationDateEnd).Date, (int?)InvoiceType, true, true, false, (bool?)InvoiceIsPaymentEnough,
						(int?)DoctorID, PatientID != null ? Convert.ToInt32(PatientID) : (int?) null).ToList();

			gridControl1.DataSource = result;
			gridControl1.RefreshDataSource();
		}

		private void dtnvoiceCreationDateFrom_EditValueChanged(object sender, EventArgs e)
		{
			if(InvoiceCreationDateStart == null)
				InvoiceCreationDateStart = DateTime.Now.Date;
		}

		private void dtInvoiceCreationDateTo_EditValueChanged(object sender, EventArgs e)
		{
			if (InvoiceCreationDateEnd == null)
				InvoiceCreationDateEnd = DateTime.Now.Date;
		}

		private void lkeDoctor_EditValueChanged(object sender, EventArgs e)
		{
			DoctorID = lkeDoctor.EditValue;
		}

		private void txtPatientID_EditValueChanged(object sender, EventArgs e)
		{
			PatientID = txtPatientID.EditValue;
		}

		private void gridControl1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			var rowObject = CommonViewsActions.GetSelectedRowObject(gridView1);
			if (rowObject is GetInvoiceForAddmission_Result)
			{
				InvoiceObject invoiceObject =
					MerkDBBusinessLogicEngine.GetInvoiceFullTree(((GetInvoiceForAddmission_Result)rowObject).InvoiceID);
				if (invoiceObject == null)
					return;

				MainPatientInvoiceActions patienActions = new MainPatientInvoiceActions();
				patienActions.Initialize(invoiceObject);
				PopupBaseForm.ShowAsPopup(patienActions, this);
			}
		}

		private TEntity GetSelectedRow<TEntity>(GridView gridView)
		{
			return (TEntity)gridView.GetRow(gridView.FocusedRowHandle);
		}
	}
}
