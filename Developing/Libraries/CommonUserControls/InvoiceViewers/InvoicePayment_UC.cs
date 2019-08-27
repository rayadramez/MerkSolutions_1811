using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CommonControlLibrary;
using DevExpress.XtraLayout.Utils;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.InvoiceViewers
{
	public partial class InvoicePayment_UC :
		//UserControl
		CommonAbstractEditorViewer<InvoicePayment>,
		IInvoicePaymentViewer, IViewerDataRelated
	{
		public int PaymentType { get; set; }
		private Invoice ActiveMedicalInvoice { get; set; }
		private FinanceInvoice ActiveFinanceInvoice { get; set; }
		private bool _isRefund;

		public InvoicePayment_UC()
		{
			InitializeComponent();
		}

		private void InvoicePayment_UC_Load(object sender, EventArgs e)
		{
			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_InvoicePaymentEditorViewer);
			CommonViewsActions.SetupGridControl(grdPayments, Resources.LocalizedRes.grd_PaymentsHistory_Cash, true);
			CommonViewsActions.SetupSyle(this);

			if (ViewerDataRelated != null)
				if (ViewerDataRelated is Invoice)
					MedicalInitialize((Invoice)ViewerDataRelated);
				else if (ViewerDataRelated is FinanceInvoice)
					FinanceInitialize((FinanceInvoice)ViewerDataRelated);
				else if (ViewerDataRelated is Patient_cu)
					DepositInitialize();
		}

		#region Initialize

		public void MedicalInitialize(Invoice invoice)
		{
			if (invoice == null)
				return;

			ActiveMedicalInvoice = invoice;
			Invoice = ActiveMedicalInvoice;
			patientTopTitle_UC1.Initialize(invoice.PatientObject);

			grdPayments.DataSource = GetInvoicesPaymentBriefReport_Result.GetItemsList(ActiveMedicalInvoice.ID, null, null, null,
				null, null);

			LoadControls(ActiveMedicalInvoice);
		}

		public void FinanceInitialize(FinanceInvoice invoice)
		{
			if (invoice == null)
				return;

			ActiveFinanceInvoice = invoice;
			Invoice = ActiveFinanceInvoice;

			LoadControls(ActiveFinanceInvoice);
		}

		private void DepositInitialize()
		{
			PaymentViewerType = PaymentViewerType.PatientDepositPayment;
			chkPatientDeposit.Checked = true;
			chkPayment.Enabled = chkRefund.Enabled = false;

			CommonViewsActions.FillGridlookupEdit(lkeServiceCategory,
				ServiceCategory_cu.ItemsList.FindAll(
					item =>
						Convert.ToBoolean(item.AllowAdmission) &&
						Convert.ToInt32(item.ServiceType_P_ID).Equals((int)DB_ServiceType.ExaminationService)));
		}

		#endregion

		#region Load Controls

		public void LoadControls(Invoice invoice)
		{
			TotalRequired = Math.Round(invoice.PatientShare_BeforeAddsOn_InvoiceItem, 2);
			TotalPayments = Math.Round(invoice.CalculatedTotal_Payments, 2);
			Remainder = Math.Round(Convert.ToDouble(TotalRequired) - Convert.ToDouble(TotalPayments), 2);
			PaymentAmount = Math.Round(Math.Abs(Convert.ToDouble(TotalRequired) - Convert.ToDouble(TotalPayments)), 2);

			chkRefund.Checked = Convert.ToDouble(invoice.CalculatedTotal_Payments) >
								Convert.ToDouble(invoice.PatientShare_BeforeAddsOn_InvoiceItem);

			IsPaymentEnough =
				Math.Abs(Convert.ToDouble(invoice.CalculatedTotal_Payments) -
						 Convert.ToDouble(invoice.PatientShare_BeforeAddsOn_InvoiceItem)) < 0.0001;
		}

		public void LoadControls(FinanceInvoice invoice)
		{
		}

		public void LoadControls(bool isDeposite)
		{
			lytServiceChoosing.Visibility =
				isDeposite ? LayoutVisibility.Always : LayoutVisibility.Never;
			lytIsPaymentEnough.Visibility =
				!isDeposite ? LayoutVisibility.Always : LayoutVisibility.Never;
			lytRemainderStatus.Visibility =
				!isDeposite ? LayoutVisibility.Always : LayoutVisibility.Never;
			lytRemainder.Visibility =
				!isDeposite ? LayoutVisibility.Always : LayoutVisibility.Never;
			lytTotalPayments.Visibility =
				!isDeposite ? LayoutVisibility.Always : LayoutVisibility.Never;
			lytRequestedAmount.Visibility =
				!isDeposite ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		#endregion
		
		#region Controls Events

		#region CheckEdit Events

		private void chkPayment_CheckedChanged(object sender, System.EventArgs e)
		{
			_isRefund = chkPayment.Checked;
			LoadControls(false);
		}

		private void chkRefund_CheckedChanged(object sender, System.EventArgs e)
		{
			_isRefund = chkPayment.Checked;
			LoadControls(false);
		}

		private void chkPatientDeposit_CheckedChanged(object sender, EventArgs e)
		{
			_isRefund = chkPatientDeposit.Checked;
			LoadControls(true);
		}

		private void chkCashPayment_CheckedChanged(object sender, EventArgs e)
		{
			PaymentType = (int)DB_PaymentType.CashPayment;
			lytCheckDetails.Visibility = lytVisaDetails.Visibility = LayoutVisibility.Never;
		}

		private void chkCheckPayment_CheckedChanged(object sender, EventArgs e)
		{
			PaymentType = (int)DB_PaymentType.CheckPayment;
			lytCheckDetails.Visibility = LayoutVisibility.Always;
			lytVisaDetails.Visibility = LayoutVisibility.Never;
		}

		private void chkVisaPayment_CheckedChanged(object sender, EventArgs e)
		{
			PaymentType = (int)DB_PaymentType.VisaPayment;
			lytVisaDetails.Visibility = LayoutVisibility.Always;
			lytCheckDetails.Visibility = LayoutVisibility.Never;
		}

		private void chkIsPaymentEnoght_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void chkExaminationServiceType_CheckedChanged(object sender, EventArgs e)
		{
			if (chkExaminationServiceType.Checked)
			{
				CommonViewsActions.FillGridlookupEdit(lkeServiceCategory,
					ServiceCategory_cu.ItemsList.FindAll(
						item =>
							Convert.ToBoolean(item.AllowAdmission) &&
							Convert.ToInt32(item.ServiceType_P_ID).Equals((int) DB_ServiceType.ExaminationService)));
			}
			else
				lkeServiceCategory.Properties.DataSource = null;
		}

		private void chkInvestigationServicetype_CheckedChanged(object sender, EventArgs e)
		{
			if (chkInvestigationServicetype.Checked)
				CommonViewsActions.FillGridlookupEdit(lkeServiceCategory,
					ServiceCategory_cu.ItemsList.FindAll(
						item =>
							Convert.ToBoolean(item.AllowAdmission) &&
							Convert.ToInt32(item.ServiceType_P_ID).Equals((int)DB_ServiceType.InvestigationServices)));
			else
				lkeServiceCategory.Properties.DataSource = null;
		}

		private void chkLabServiceType_CheckedChanged(object sender, EventArgs e)
		{
			//LabRegistration_UC lab = new LabRegistration_UC();
			//lab.Initialize(ActivePatient);
			//DialogResult result = PopupBaseForm.ShowAsPopup(lab, this);

		}

		#endregion

		#region LookUpEdit

		private void lkeBank_CheckPayment_EditValueChanged(object sender, System.EventArgs e)
		{
			if (BankAccountID_CheckPayment != null)
				CommonViewsActions.FillGridlookupEdit(lkeBank_CheckPayment,
					BankAccount_cu.ItemsList.FindAll(
						item => Convert.ToInt32(item.Bank_CU_ID).Equals(Convert.ToInt32(BankAccountID_CheckPayment))));
			else
				CommonViewsActions.FillGridlookupEdit(lkeBankAccount_CheckPayment, BankAccount_cu.ItemsList);
		}

		private void lkeBank_VisaPayment_EditValueChanged(object sender, System.EventArgs e)
		{
			if (BankAccountID_VisaPayment != null)
				CommonViewsActions.FillGridlookupEdit(lkeBank_CheckPayment,
					BankAccount_cu.ItemsList.FindAll(
						item => Convert.ToInt32(item.Bank_CU_ID).Equals(Convert.ToInt32(BankAccountID_VisaPayment))));
			else
				CommonViewsActions.FillGridlookupEdit(lkeBankAccount_VisaPayment, BankAccount_cu.ItemsList);
		}

		private void lkeServiceCategory_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeServiceCategory.EditValue == null)
				return;

			CommonViewsActions.FillGridlookupEdit(lkeService,
				Service_cu.ItemsList.FindAll(
					item => Convert.ToInt32(item.ServiceCategory_CU_ID).Equals(Convert.ToInt32(lkeServiceCategory.EditValue))));
		}

		#endregion

		#region SpinEdit Events

		private void spnAmount_EditValueChanged(object sender, System.EventArgs e)
		{
			IsPaymentEnough = ActiveMedicalInvoice != null && ActiveMedicalInvoice.IsPaymentsEnough;

			if (spnAmount.EditValue == null)
				return;

			if (Convert.ToDouble(spnAmount.EditValue) < 0)
				spnAmount.EditValue = 0;
		}

		private void spnReaminder_EditValueChanged(object sender, EventArgs e)
		{
			if (Remainder == null)
				return;

			double value = Convert.ToDouble(Remainder);

			if (value > 0)
			{
				lblRemainderStatus.Text = " ( عليه )";
				spnReaminder.BackColor = lblRemainderStatus.BackColor = Color.Red;
				spnReaminder.ForeColor = lblRemainderStatus.ForeColor = Color.White;
				return;
			}

			if (value < 0)
			{
				lblRemainderStatus.Text = " ( له )";
				spnReaminder.BackColor = lblRemainderStatus.BackColor = Color.GreenYellow;
				spnReaminder.ForeColor = lblRemainderStatus.ForeColor = Color.Black;
				return;
			}

			if (Math.Abs(value) < 0.0001)
			{
				lblRemainderStatus.Text = "( = )";
				spnReaminder.BackColor = lblRemainderStatus.BackColor = Color.Yellow;
				spnReaminder.ForeColor = lblRemainderStatus.ForeColor = Color.Black;
				return;
			}
		}

		#endregion

		#endregion

		#region Overrides of CommonAbstractViewer<InvoicePayment>

		public override IMVCController<InvoicePayment> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return ViewerName.InvoicePayment_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "المدفوعات"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeBank_CheckPayment, Bank_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeBankAccount_CheckPayment, BankAccount_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeBank_VisaPayment, Bank_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeBankAccount_VisaPayment, BankAccount_cu.ItemsList);

			if (ServiceCategory_cu.ItemsList != null && ServiceCategory_cu.ItemsList.Count > 0)
			{
				ServiceCategory_cu firstItem = ServiceCategory_cu.ItemsList.FirstOrDefault();
				if (firstItem != null)
					lkeServiceCategory.EditValue = firstItem.ID;
			}

			CheckIssueDate_CheckPayment = DateTime.Now;
			CheckExhangeDate_CheckPayment = DateTime.Now;
			dtPaymentDate.Properties.ReadOnly = true;
			PaymentDate = DateTime.Now;
		}

		public override void ClearControls()
		{
			chkCashPayment.Checked = true;
			chkExaminationServiceType.Checked = true;
			lkeServiceCategory.EditValue = null;
			lkeService.EditValue = null;
			spnAmount.EditValue = null;
			lkeBank_CheckPayment.EditValue = null;
			lkeBankAccount_CheckPayment.EditValue = null;
			dtCheckIssueDate.EditValue = null;
			dtCheckExhcangeDate.EditValue = null;
			txtCheckNumber.EditValue = null;
			txtCheckDescription.EditValue = null;
			lkeBank_VisaPayment.EditValue = null;
			lkeBankAccount_VisaPayment.EditValue = null;
			txtCreditCardNumber.EditValue = null;
			txtVisaDescription.EditValue = null;
			txtInvoicePaymentDescription.EditValue = null;
			dtPaymentDate.EditValue = null;
			txtPaymentSerial.EditValue = null;
			spnTotalRequired.EditValue = null;
			spnTotalPayments.EditValue = null;
			spnReaminder.EditValue = null;
			chkIsPaymentEnoght.Checked = false;
			txtInvoicePaymentDescription.EditValue = null;
		}

		#endregion

		#region Implementation of IInvoicePaymentViewer

		public PaymentViewerType PaymentViewerType { get; set; }

		public object Invoice
		{
			get { return ActiveMedicalInvoice; }
			set
			{
				if (value is Invoice)
					ActiveMedicalInvoice = (Invoice)value;
				else if (value is FinanceInvoice)
					ActiveFinanceInvoice = (FinanceInvoice)value;
			}
		}

		public object Patient
		{
			get
			{
				if (ViewerDataRelated is Patient_cu)
					return (Patient_cu)ViewerDataRelated;
				return null;
			}
		}

		public object ServiceID
		{
			get { return lkeService.EditValue; }
			set { lkeService.EditValue = value; }
		}

		public object IsRefund
		{
			get { return chkRefund.Checked; }
			set { chkRefund.Checked = Convert.ToBoolean(value); }
		}

		public object IsPaymentEnough
		{
			get { return chkIsPaymentEnoght.Checked; }
			set { chkIsPaymentEnoght.Checked = Convert.ToBoolean(value); }
		}

		public object PaymentDate
		{
			get { return dtPaymentDate.EditValue; }
			set { dtPaymentDate.EditValue = value; }
		}

		public object PaymentSerial
		{
			get { return txtPaymentSerial.EditValue; }
			set { txtPaymentSerial.EditValue = value; }
		}

		public object InvoicePaymentDescription
		{
			get { return txtInvoicePaymentDescription.EditValue; }
			set { txtInvoicePaymentDescription.EditValue = value; }
		}

		public object TotalRequired
		{
			get { return spnTotalRequired.EditValue; }
			set { spnTotalRequired.EditValue = value; }
		}

		public object TotalPayments
		{
			get { return spnTotalPayments.EditValue; }
			set { spnTotalPayments.EditValue = value; }
		}

		public object Remainder
		{
			get { return spnReaminder.EditValue; }
			set { spnReaminder.EditValue = value; }
		}

		public object PaymentAmount
		{
			get
			{
				//if (spnAmount.EditValue != null)
				//	return chkRefund.Checked ? Convert.ToDouble(spnAmount.EditValue)*-1 : Convert.ToDouble(spnAmount.EditValue);
				//return null;
				return spnAmount.EditValue;
			}
			set { spnAmount.EditValue = value; }
		}

		public object PaymentTypeID
		{
			get
			{
				if (chkCashPayment.Checked)
					return (int)DB_PaymentType.CashPayment;
				if (chkCheckPayment.Checked)
					return (int)DB_PaymentType.CheckPayment;
				if (chkVisaPayment.Checked)
					return (int)DB_PaymentType.VisaPayment;
				return null;
			}
			set { PaymentAmount = value; }
		}

		public object BankID_CheckPayment
		{
			get { return lkeBank_CheckPayment.EditValue; }
			set { lkeBank_CheckPayment.EditValue = value; }
		}

		public object BankAccountID_CheckPayment
		{
			get { return lkeBankAccount_CheckPayment.EditValue; }
			set { lkeBankAccount_CheckPayment.EditValue = value; }
		}

		public object CheckIssueDate_CheckPayment
		{
			get { return dtCheckIssueDate.EditValue; }
			set { dtCheckIssueDate.EditValue = value; }
		}

		public object CheckExhangeDate_CheckPayment
		{
			get { return dtCheckExhcangeDate.EditValue; }
			set { dtCheckExhcangeDate.EditValue = value; }
		}

		public object CheckDescription
		{
			get { return txtCheckDescription.EditValue; }
			set { txtCheckDescription.EditValue = value; }
		}

		public object CheckNumber_CheckPayment
		{
			get { return txtCheckNumber.EditValue; }
			set { txtCheckNumber.EditValue = value; }
		}

		public object BankID_VisaPayment
		{
			get { return lkeBank_VisaPayment.EditValue; }
			set { lkeBank_VisaPayment.EditValue = value; }
		}

		public object BankAccountID_VisaPayment
		{
			get { return lkeBankAccount_VisaPayment.EditValue; }
			set { lkeBankAccount_VisaPayment.EditValue = value; }
		}

		public object VisaCardNumber_VisaPayment
		{
			get { return txtCreditCardNumber.EditValue; }
			set { txtCreditCardNumber.EditValue = value; }
		}

		public object VisaDescription
		{
			get { return txtVisaDescription.EditValue; }
			set { txtVisaDescription.EditValue = value; }
		}

		#endregion

		#region Implementation of IViewerDataRelated

		public object ViewerDataRelated { get; set; }

		#endregion
	}
}
