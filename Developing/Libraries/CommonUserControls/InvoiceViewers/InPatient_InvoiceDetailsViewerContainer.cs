using System;
using System.Collections.Generic;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace CommonUserControls.InvoiceViewers
{
	public partial class InPatient_InvoiceDetailsViewerContainer : DevExpress.XtraEditors.XtraUserControl
	{
		private InPatient_InvoiceDetailsGridViewer_Accommodation _invoiceDetailAccommodation;
		public List<InvoiceDetail> List_mainServices { get; set; }
		private InvoiceDetail SelectedMainService { get; set; }
		public InvoiceDetailType InvoiceDetailType { get; set; }

		public InPatient_InvoiceDetailsViewerContainer()
		{
			InitializeComponent();
			CommonViewsActions.SetupSyle(this);
			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_InvoiceDetailsViewing);
		}

		public void Initialize(List<InvoiceDetail> mainServices, InvoiceDetailType invoiceDetailType)
		{
			List_mainServices = mainServices;
			InvoiceDetailType = invoiceDetailType;

			CommonViewsActions.FillGridlookupEdit(lkeMainService, List_mainServices, "ServiceName");
			chkAccommodationServices.Checked = true;

			Service_cu accommodationService =
				Service_cu.ItemsList.Find(
					item => Convert.ToInt32(item.ServiceType_P_ID).Equals((int)DB_ServiceType.ParentAccommodationService));
			if (accommodationService != null)
			{
				InvoiceDetail accommodationMainInvoiceDetail =
					List_mainServices.Find(item => Convert.ToInt32(item.Service_CU_ID).Equals(Convert.ToInt32(accommodationService.ID)));
				if (accommodationMainInvoiceDetail != null)
					lkeMainService.EditValue = accommodationMainInvoiceDetail.ID;
			}
		}

		private void chkAccommodationServices_CheckedChanged(object sender, System.EventArgs e)
		{
			CommonViewsActions.ShowUserControl(ref _invoiceDetailAccommodation, pnlGridViewControl);
		}

		private void lkeMainService_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeMainService.EditValue == null)
				return;

			SelectedMainService =
				List_mainServices.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(lkeMainService.EditValue)));
			if (SelectedMainService == null)
				return;

			CommonViewsActions.ShowUserControl(ref _invoiceDetailAccommodation, pnlGridViewControl);
			if(_invoiceDetailAccommodation != null)
				_invoiceDetailAccommodation.Initialize(SelectedMainService.List_InvoiceDetail_Accommodation);
		}
	}
}
