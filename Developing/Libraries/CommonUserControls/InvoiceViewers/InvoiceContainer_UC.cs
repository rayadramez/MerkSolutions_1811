using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CommonControlLibrary;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;

namespace CommonUserControls.InvoiceViewers
{
	public enum InvoiceDetailType
	{
		Accommodation = 1,
	}

	public partial class InvoiceContainer_UC : DevExpress.XtraEditors.XtraUserControl
	{
		private InvoicePayment_UC _invoicePayment;
		private InPatient_InvoiceDetailsViewerContainer _invoiceDetailsViewing;
		private OutPatient_InvoiceDetailsViewerContainer _outPatientInvoiceDetails;
		private InvoiceManagerQueueContainerWithHeaderIcons_UC ParentContainer { get; set; }
		private Invoice ActiveInvoice { get; set; }
		private Control ParentControl { get; set; }

		public InvoiceContainer_UC()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_InvoiceContainer);
			CommonViewsActions.SetupSyle(this);
		}

		public void Initialize(InvoiceManagerQueueContainerWithHeaderIcons_UC parentContainer, Invoice invoiceToLoad)
		{
			ParentContainer = parentContainer;
			ActiveInvoice = invoiceToLoad;

			Initialize(invoiceToLoad);
		}

		public void Initialize(Control parentControl, Invoice invoiceToLoad)
		{
			ParentControl = parentControl;
			ActiveInvoice = invoiceToLoad;
			Initialize(invoiceToLoad);
		}

		public void Initialize(Invoice invoiceToLoad)
		{
			LoadControls(invoiceToLoad);

			if (ActiveInvoice != null)
			{
				DB_InvoiceType invoiceType = (DB_InvoiceType)ActiveInvoice.InvoiceType_P_ID;
				switch (invoiceType)
				{
					case DB_InvoiceType.InPatientPrivate:
					case DB_InvoiceType.InPatientNotPrivate:
						List<InvoiceDetail> List_mainServices = ActiveInvoice.List_InvoiceDetails.FindAll(item => item.IsInPatientParentService);

						CommonViewsActions.ShowUserControl(ref _invoiceDetailsViewing, pnlMain);
						if (_invoiceDetailsViewing != null)
							_invoiceDetailsViewing.Initialize(List_mainServices, InvoiceDetailType.Accommodation);
						lytInPatientExit.Visibility = LayoutVisibility.Always;
						break;
					case DB_InvoiceType.OutPatientPrivate:
					case DB_InvoiceType.OutPatientNotPrivate:
						CommonViewsActions.ShowUserControl(ref _outPatientInvoiceDetails, pnlMain);
						if (_outPatientInvoiceDetails != null)
							_outPatientInvoiceDetails.Initialize(ActiveInvoice);
						lytInPatientExit.Visibility = LayoutVisibility.Never;
						break;
				}
			}
		}

		#region Controls Loading

		public void LoadControls(Invoice invoiceToLoad)
		{
			if (ActiveInvoice == null)
				return;

			dtInvoiceCreationDate.EditValue = ActiveInvoice.InvoiceCreationDate;
			dtInvoicePrintingDate.EditValue = ActiveInvoice.PrintingDate != null
				? ActiveInvoice.PrintingDate
				: null;
			txtInvoiceSerial.EditValue = ActiveInvoice.InvoiceSerial;
			SetPatientTitle(invoiceToLoad);
			UpdateAllControls(invoiceToLoad);
		}

		public void SetPatientTitle(Invoice invoice)
		{
			if (invoice == null || invoice.PatientObject == null)
				return;

			lblTitlePatientID.Text = invoice.PatientObject.Person_CU_ID.ToString();
			lblTitlePatientName.Text = invoice.PatientObject.PatientFullName;
			lblTitleDoctorName.Text = invoice.DoctorName;
			lblTitleInsuranceCarrier.Text = invoice.InsuranceCarrierName;
			lblTitleInsuranceLevel.Text = invoice.InsuranceLevelName;
			lblTitleInsurancePercentage.Text = invoice.InsurancePercentage.ToString();
		}

		public void UpdateAllControls(Invoice invoiceToLoad)
		{
			if (invoiceToLoad.InvoiceShareObject != null)
			{
				SetTopBriefControls(invoiceToLoad);
				SetInsuranceDetailsControls(invoiceToLoad);
				SetTotalsBeforeAddsOnControls(invoiceToLoad);
				SetSurchargeDetailsControls(invoiceToLoad);
				SetStampDetailsControls(invoiceToLoad);
				SetTotalsAfterAddsOnControls(invoiceToLoad);
			}
			else
				SetLeftPanelToDefaultValues();
		}

		private void SetTopBriefControls(Invoice invoiceToLoad)
		{
			txtBrief_TotalPatientShare_Before.EditValue = Math.Round(invoiceToLoad.PatientShare_BeforeAddsOn_InvoiceItem, 2);
			txtBrief_TotalSurchargePatientShare.EditValue = Math.Round(invoiceToLoad.SurchargeAmount_PatientShare_InvoiceItem, 2);
			txtBrief_TotalStampPatientShare.EditValue = Math.Round(invoiceToLoad.StampAmount_PatientShare_InvoiceItem, 2);
			txtBrief_TotalDiscount.EditValue = Math.Round(invoiceToLoad.PatientShareDiscount_InvoiceItem, 2);
			txtBrief_TotalPatientShare_After.EditValue = Math.Round(invoiceToLoad.PatientShare_AfterAddsOn_InvoiceItem, 2);
			txtBrief_TotalPayments.EditValue = Math.Round(invoiceToLoad.CalculatedTotal_Payments, 2);
			txtBrief_Remainder.EditValue = Math.Round(invoiceToLoad.PatientShare_AfterAddsOn_InvoiceItem -
										   invoiceToLoad.PatientShareDiscount_InvoiceItem -
										   invoiceToLoad.CalculatedTotal_Payments, 2);
		}

		private void SetInsuranceDetailsControls(Invoice invoiceToLoad)
		{
			lyt_InsuranceDetails.Visibility = invoiceToLoad.InvoiceShareObject.IsInsuranceApplied
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			chkIsInsuranceAppliedToInvoice.Checked = invoiceToLoad.InvoiceShareObject.IsInsuranceApplied;

			if (invoiceToLoad.InvoiceShareObject.IsInsuranceApplied)
			{
				CommonViewsActions.FillGridlookupEdit(lkeInsuranceCarrier, InsuranceCarrier_cu.ItemsList);
				CommonViewsActions.FillGridlookupEdit(lkeInsuranceLevel, InsuranceLevel_cu.ItemsList);

				lkeInsuranceCarrier.EditValue = invoiceToLoad.InvoiceShareObject.InsuranceCarrier_CU_ID;
				lkeInsuranceLevel.EditValue = invoiceToLoad.InvoiceShareObject.InsuanceLevel_CU_ID;

				txtInsurance_PatientPercentage.EditValue = invoiceToLoad.InvoiceShareObject.InsurancePercentageApplied;
				txtInsurance_InsurancePercentage.EditValue = 1 - invoiceToLoad.InvoiceShareObject.InsurancePercentageApplied;
				txtInsurance_PatientMaxAmount.EditValue = invoiceToLoad.InvoiceShareObject.InsurancePatientMaxAmount != null &&
														  Convert.ToDouble(
															  invoiceToLoad.InvoiceShareObject.InsurancePatientMaxAmount) > 0
					? invoiceToLoad.InvoiceShareObject.InsurancePatientMaxAmount
					: 0;
				txtInsurance_InsuranceMaxAmount.EditValue = invoiceToLoad.InvoiceShareObject.InsuranceMaxAmount != null &&
															Convert.ToDouble(invoiceToLoad.InvoiceShareObject.InsuranceMaxAmount) >
															0
					? invoiceToLoad.InvoiceShareObject.InsuranceMaxAmount
					: 0;
				txtInsurance_InsuranceMaxAmount.Properties.ReadOnly = true;
			}
		}

		private void SetTotalsBeforeAddsOnControls(Invoice invoiceToLoad)
		{
			txtTotalServices_Before.EditValue = Math.Round(invoiceToLoad.TotalServicePrice_BeforeAddsOn_InvoiceItem, 2);
			txtPatientShare_Before.EditValue = Math.Round(invoiceToLoad.PatientShare_BeforeAddsOn_InvoiceItem, 2);
			txtInsuranceShare_Before.EditValue = Math.Round(invoiceToLoad.InsuranceShare_BeforeAddsOn_InvoiceItem, 2);
		}

		private void SetSurchargeDetailsControls(Invoice invoiceToLoad)
		{
			chkIsSurchargeAppliedToInvoice.Checked = invoiceToLoad.IsSurchargeApplied_InvoiceItem;

			chkIsSurchargeAppliedToPatientOnly.Checked =
				Convert.ToBoolean(invoiceToLoad.InvoiceShareObject.IsSurchargeAppliedToPatientOnly);
			chkIsSurchargeAppliedToInsuranceOnly.Checked =
				Convert.ToBoolean(invoiceToLoad.InvoiceShareObject.IsSurchargeAppliedToInsuranceOnly);
			chkDistributeSurcharge.Checked =
				Convert.ToBoolean(invoiceToLoad.InvoiceShareObject.IsSurchargeDistributedToInsurancePercentage);

			txtSurchargeAmount.EditValue = Math.Round(invoiceToLoad.TotalSurchargeAmount_InvoiceItem, 2);
			txtSurchargeAmount_PatientShare.EditValue = Math.Round(invoiceToLoad.SurchargeAmount_PatientShare_InvoiceItem, 2);
			txtSurchargeAmount_InsuranceShare.EditValue = Math.Round(invoiceToLoad.SurchargeAmount_InsuranceShare_InvoiceItem, 2);

			lyt_chkIsSurchargeAppliedToPatientOnly.Visibility =
				lyt_chkDistributeSurcharge.Visibility =
					lyt_chkIsSurchargeAppliedToInsuranceOnly.Visibility =
						lyt_txtSurchargeAmount.Visibility =
							lyt_txtSurchargeAmount_PatientShare.Visibility =
								emptySpaceItem9.Visibility =
									lyt_txtSurchargeAmount_InsuranceShare.Visibility = ActiveInvoice.IsSurchargeApplied_InvoiceItem
										? LayoutVisibility.Always
										: LayoutVisibility.Never;
		}

		private void SetStampDetailsControls(Invoice invoiceToLoad)
		{
			chkIsStampAppliedToInvoice.Checked = invoiceToLoad.IsStampApplied_InvoiceItem;

			chkIsStampAppliedToPatientOnly.Checked =
				Convert.ToBoolean(invoiceToLoad.InvoiceShareObject.IsStampAppliedToPatientOnly);
			chkIsStampAppliedToInsuranceOnly.Checked =
				Convert.ToBoolean(invoiceToLoad.InvoiceShareObject.IsStampAppliedToInsuranceOnly);
			chkDistributeStamp.Checked =
				Convert.ToBoolean(invoiceToLoad.InvoiceShareObject.IsStampDistributedToInsurancePercentage);

			txtStampAmount.EditValue = Math.Round(invoiceToLoad.TotalStampAmount_InvoiceItem, 2);
			txtStampAmount_PatientShare.EditValue = Math.Round(invoiceToLoad.StampAmount_PatientShare_InvoiceItem, 2);
			txtStampAmount_InsuranceShare.EditValue = Math.Round(invoiceToLoad.StampAmount_InsuranceShare_InvoiceItem, 2);

			lyt_chkIsStampAppliedToPatientOnly.Visibility =
				lyt_chkIsStampAppliedToInsuranceOnly.Visibility =
					lyt_chkDistributeStamp.Visibility =
						lyt_txtStampAmount.Visibility =
							lyt_txtStampAmount_PatientShare.Visibility =
								lyt_txtStampAmount_InsuranceShare.Visibility =
									emptySpaceItem10.Visibility = ActiveInvoice.IsStampApplied_InvoiceItem
										? LayoutVisibility.Always
										: LayoutVisibility.Never;
		}

		private void SetTotalsAfterAddsOnControls(Invoice invoiceToLoad)
		{
			txtTotalServices_After.EditValue = Math.Round(invoiceToLoad.TotalServicePrice_AfterAddsOn_InvoiceItem, 2);
			txtPatientShare_After.EditValue = Math.Round(invoiceToLoad.PatientShare_AfterAddsOn_InvoiceItem, 2);
			txtInsuranceShare_After.EditValue = Math.Round(invoiceToLoad.InsuranceShare_AfterAddsOn_InvoiceItem, 2);
		}

		private void SetLeftPanelToDefaultValues()
		{
			txtBrief_TotalPatientShare_Before.EditValue = 0;
			txtBrief_TotalSurchargePatientShare.EditValue = 0;
			txtBrief_TotalStampPatientShare.EditValue = 0;
			txtBrief_TotalDiscount.EditValue = 0;
			txtBrief_TotalPatientShare_After.EditValue = 0;
			txtBrief_TotalPayments.EditValue = 0;
			txtBrief_Remainder.EditValue = 0;
			txtInsurance_PatientPercentage.EditValue = 0;
			txtInsurance_InsurancePercentage.EditValue = 0;
			txtInsurance_PatientMaxAmount.EditValue = 0;
			txtInsurance_InsuranceMaxAmount.EditValue = 0;
			txtTotalServices_Before.EditValue = 0;
			txtPatientShare_Before.EditValue = 0;
			txtInsuranceShare_Before.EditValue = 0;
			txtSurchargeAmount.EditValue = 0;
			txtSurchargeAmount_PatientShare.EditValue = 0;
			txtSurchargeAmount_InsuranceShare.EditValue = 0;
			txtStampAmount.EditValue = 0;
			txtStampAmount_PatientShare.EditValue = 0;
			txtStampAmount_InsuranceShare.EditValue = 0;
			txtTotalServices_After.EditValue = 0;
			txtPatientShare_After.EditValue = 0;
			txtInsuranceShare_After.EditValue = 0;
		}

		#endregion

		#region Controls Events

		#region Button Events

		private void btnBack_Click(object sender, System.EventArgs e)
		{
			DialogResult result = XtraMessageBox.Show("هل تريد الحفظ قبل الخروج؟", "تنبيه", MessageBoxButtons.YesNoCancel,
				MessageBoxIcon.Question);
			switch (result)
			{
				case DialogResult.Yes:
					btnSave_Click(null, null);
					if (ParentContainer != null)
					{
						ParentContainer.CollapseLeftPanel(false);
						ParentContainer.ShowInvoiceContainer(false);
					}
					this.Visible = false;
					break;
				case DialogResult.No:
					if (ParentContainer != null)
					{
						ParentContainer.CollapseLeftPanel(false);
						ParentContainer.ShowInvoiceContainer(false);
					}
					this.Visible = false;
					break;
				case DialogResult.Cancel:
					return;
			}
		}

		private void btnDischargePatient_Click(object sender, System.EventArgs e)
		{

		}

		private void btnPrintInvoice_Click(object sender, System.EventArgs e)
		{

		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			ActiveInvoice.SaveChanges<Invoice>(DB_CommonTransactionType.UpdateExisting);
		}

		private void btnInsuranceDetails_Click(object sender, EventArgs e)
		{

		}

		private void btnPayment_Click(object sender, EventArgs e)
		{
			BaseController<InvoicePayment>.ShowEditorControl(ref _invoicePayment, this, ActiveInvoice, null,
				EditorContainerType.Regular, ViewerName.InvoicePayment_Viewer, DB_CommonTransactionType.CreateNew,
				"مـدفـوعــــات المـريـــض", true);
			ActiveInvoice = (Invoice)ActiveInvoice.RegenerateEntityObject(ActiveInvoice);
			LoadControls(ActiveInvoice);
			lytMenu.Visibility = LayoutVisibility.Never;
			chkShowHideNavigationMenu.Checked = false;
		}

		#endregion

		#region CheckEdit Controls

		#region Surcharges Controls

		private void chkIsSurchargeAppliedToInvoice_CheckedChanged(object sender, EventArgs e)
		{
			if (ActiveInvoice == null)
				return;

			ActiveInvoice.IsSurchargeApplied_InvoiceItem = chkIsSurchargeAppliedToInvoice.Checked;

			lyt_chkIsSurchargeAppliedToPatientOnly.Visibility =
				lyt_chkDistributeSurcharge.Visibility =
					lyt_chkIsSurchargeAppliedToInsuranceOnly.Visibility =
						lyt_txtSurchargeAmount.Visibility =
							lyt_txtSurchargeAmount_PatientShare.Visibility =
								emptySpaceItem9.Visibility =
									lyt_txtSurchargeAmount_InsuranceShare.Visibility = ActiveInvoice.IsSurchargeApplied_InvoiceItem
										? LayoutVisibility.Always
										: LayoutVisibility.Never;
			UpdateAllControls(ActiveInvoice);
		}

		private void chkDistributeSurcharge_CheckedChanged(object sender, EventArgs e)
		{
			if (ActiveInvoice == null)
				return;

			if (chkDistributeSurcharge.Checked)
			{
				ActiveInvoice.InvoiceShareObject.IsSurchargeDistributedToInsurancePercentage = true;
				ActiveInvoice.InvoiceShareObject.IsSurchargeAppliedToPatientOnly = false;
				ActiveInvoice.InvoiceShareObject.IsSurchargeAppliedToInsuranceOnly = false;
			}

			UpdateAllControls(ActiveInvoice);
		}

		private void chkIsSurchargeAppliedToPatientOnly_CheckedChanged(object sender, EventArgs e)
		{
			if (ActiveInvoice == null)
				return;

			if (chkIsSurchargeAppliedToPatientOnly.Checked)
			{
				ActiveInvoice.InvoiceShareObject.IsSurchargeDistributedToInsurancePercentage = false;
				ActiveInvoice.InvoiceShareObject.IsSurchargeAppliedToPatientOnly = true;
				ActiveInvoice.InvoiceShareObject.IsSurchargeAppliedToInsuranceOnly = false;
			}

			UpdateAllControls(ActiveInvoice);
		}

		private void chkIsSurchargeAppliedToInsuranceOnly_CheckedChanged(object sender, EventArgs e)
		{
			if (ActiveInvoice == null)
				return;

			if (chkIsSurchargeAppliedToInsuranceOnly.Checked)
			{
				ActiveInvoice.InvoiceShareObject.IsSurchargeDistributedToInsurancePercentage = false;
				ActiveInvoice.InvoiceShareObject.IsSurchargeAppliedToPatientOnly = false;
				ActiveInvoice.InvoiceShareObject.IsSurchargeAppliedToInsuranceOnly = true;
			}

			UpdateAllControls(ActiveInvoice);
		}

		#endregion

		#region Stamp Controls

		private void chkIsStampAppliedToInvoice_CheckedChanged(object sender, EventArgs e)
		{
			if (ActiveInvoice == null)
				return;

			ActiveInvoice.IsStampApplied_InvoiceItem = chkIsStampAppliedToInvoice.Checked;

			lyt_chkIsStampAppliedToPatientOnly.Visibility =
				lyt_chkIsStampAppliedToInsuranceOnly.Visibility =
					lyt_chkDistributeStamp.Visibility =
						lyt_txtStampAmount.Visibility =
							lyt_txtStampAmount_PatientShare.Visibility =
								lyt_txtStampAmount_InsuranceShare.Visibility =
									emptySpaceItem10.Visibility = ActiveInvoice.IsStampApplied_InvoiceItem
										? LayoutVisibility.Always
										: LayoutVisibility.Never;
			UpdateAllControls(ActiveInvoice);
		}

		private void chkDistributeStamp_CheckedChanged(object sender, EventArgs e)
		{
			if (ActiveInvoice == null)
				return;

			if (chkDistributeStamp.Checked)
			{
				ActiveInvoice.InvoiceShareObject.IsStampDistributedToInsurancePercentage = true;
				ActiveInvoice.InvoiceShareObject.IsStampAppliedToPatientOnly = false;
				ActiveInvoice.InvoiceShareObject.IsStampAppliedToInsuranceOnly = false;
			}

			UpdateAllControls(ActiveInvoice);
		}

		private void chkIsStampAppliedToPatientOnly_CheckedChanged(object sender, EventArgs e)
		{
			if (ActiveInvoice == null)
				return;

			if (chkIsStampAppliedToPatientOnly.Checked)
			{
				ActiveInvoice.InvoiceShareObject.IsStampDistributedToInsurancePercentage = false;
				ActiveInvoice.InvoiceShareObject.IsStampAppliedToPatientOnly = true;
				ActiveInvoice.InvoiceShareObject.IsStampAppliedToInsuranceOnly = false;
			}

			UpdateAllControls(ActiveInvoice);
		}

		private void chkIsStampAppliedToInsuranceOnly_CheckedChanged(object sender, EventArgs e)
		{
			if (ActiveInvoice == null)
				return;

			if (chkIsStampAppliedToInsuranceOnly.Checked)
			{
				ActiveInvoice.InvoiceShareObject.IsStampDistributedToInsurancePercentage = false;
				ActiveInvoice.InvoiceShareObject.IsStampAppliedToPatientOnly = false;
				ActiveInvoice.InvoiceShareObject.IsStampAppliedToInsuranceOnly = true;
			}

			UpdateAllControls(ActiveInvoice);
		}

		private void txtStampAmount_EditValueChanged_1(object sender, EventArgs e)
		{
			if (ActiveInvoice == null)
				return;

			if (txtStampAmount.EditValue == null)
			{
				DialogResult result = XtraMessageBox.Show("هل تريد إلغاء الدمغة ؟", "تنبيه", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
				switch (result)
				{
					case DialogResult.Yes:
						chkIsStampAppliedToInvoice.Checked = false;
						return;
					case DialogResult.No:
						result = XtraMessageBox.Show("هل تريد إرجاع الدمغة لقيمة الإعدادت ؟", "تنبيه", MessageBoxButtons.YesNo,
							MessageBoxIcon.Error);
						switch (result)
						{
							case DialogResult.Yes:
								txtStampAmount.EditValue = ActiveInvoice.InvoiceShareObject.TotalStampAmount =
									FinancialBusinessLogicLibrary.GetAccummulativeSurchargeAmount((DB_InvoiceType)ActiveInvoice.InvoiceType_P_ID,
										DB_SurchargeType.MedicalStamp);
								return;
							case DialogResult.No:
								return;
						}
						break;
					case DialogResult.Cancel:
						return;
				}
			}

			if (ActiveInvoice.IsStampApplied_InvoiceItem)
				ActiveInvoice.InvoiceShareObject.TotalStampAmount = Convert.ToDouble(txtStampAmount.EditValue);
			else
				ActiveInvoice.InvoiceShareObject.TotalStampAmount = null;

			UpdateAllControls(ActiveInvoice);
		}

		#endregion

		private void chkShowHideNavigationMenu_CheckedChanged(object sender, EventArgs e)
		{
			lytMenu.Visibility = chkShowHideNavigationMenu.Checked
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
		}

		#endregion

		#region Tiles Controls

		private void tleMainAdd_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
		{

		}

		private void tileNavCategory2_Tile_ItemClick(object sender, TileItemEventArgs e)
		{

		}

		private void tleMainAdd_Tile_ItemPress(object sender, TileItemEventArgs e)
		{

		}

		#endregion

		#endregion

	}
}
