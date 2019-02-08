using System.Windows.Forms;
using CommonControlLibrary;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using MerkDataBaseBusinessLogicProject;

namespace CommonUserControls.InvoiceViewers
{
	public partial class OutPatient_InvoiceDetailsViewerContainer : UserControl
	{
		public Invoice ActiveInvoice { get; set; }

		public OutPatient_InvoiceDetailsViewerContainer()
		{
			InitializeComponent();

			CommonViewsActions.SetupGridControl(grd_InvoiceDetail_OutPatient,
				Resources.LocalizedRes.grd_OutPatientInvoiceDetailItem, false);
			CommonViewsActions.SetupSyle(this);
		}

		public void Initialize(Invoice invoice)
		{
			ActiveInvoice = invoice;
			grd_InvoiceDetail_OutPatient.DataSource = ActiveInvoice.List_InvoiceDetails;

			SetGrid();
		}

		public void SetGrid()
		{
			GridView view = grd_InvoiceDetail_OutPatient.MainView as GridView;
			if (view == null)
				return;

			GridColumn col_IsInsuranceInvoice = view.Columns["IsInsuranceApplied_InvoiceItem"];
			if (col_IsInsuranceInvoice != null)
			{
				col_IsInsuranceInvoice.OptionsColumn.AllowEdit = ActiveInvoice.HasInsuranceDetailsSetted;
				col_IsInsuranceInvoice.OptionsColumn.ReadOnly = ActiveInvoice.HasInsuranceDetailsSetted;
			}
		}

		private void grdView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			GridView view = grd_InvoiceDetail_OutPatient.MainView as GridView;
			if (view == null)
				return;

			GridColumn col_IsInsuranceInvoice = view.Columns["IsInsuranceApplied_InvoiceItem"];
			if (col_IsInsuranceInvoice == e.Column)
			{
				//InvoiceDetail invoiceDetail = (InvoiceDetail)view.GetRow(e.RowHandle);
				//FinancialBusinessLogicLibrary.Recalculate_OutPatient_InvoiceDetails(ActiveInvoice.List_InvoiceDetails, ActiveInvoice.,
				//						false, InsuranceCarrierID, InsuranceLevelID, InsurancePercentage, InsurancePatientMaxAmount);
			}
		}
	}
}
