using System.Drawing;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.ReportsContainer
{
	public partial class InvoicePaymentBrief_Report : 
		//UserControl
		CommonAbstractSearchViewer<GetInvoicesPaymentBriefReport_Result>,
		IInvoicePaymentBrief_Report_Viewer
	{
		public InvoicePaymentBrief_Report()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_InvoicePaymentBrief_Report);
			CommonViewsActions.SetupSyle(this);

			switch (ApplicationStaticConfiguration.Organization)
			{
				case DB_Organization.Cardiovascular_Clinic:
					chkInAndNotPrivate.Enabled = false;
					chkOutAndNotPrivate.Enabled = false;
					chkInAndPrivate.Enabled = false;
					break;
			}

			if (!string.IsNullOrEmpty(ApplicationStaticConfiguration.SkinName) &&
			    !string.IsNullOrWhiteSpace(ApplicationStaticConfiguration.SkinName))
				if (!ApplicationStaticConfiguration.SkinName.Equals("McSkin"))
				{
					layoutControlGroup3.AppearanceGroup.ForeColor = Color.OldLace;
					layoutControlGroup2.AppearanceGroup.ForeColor = Color.OldLace;
					layoutControlGroup5.AppearanceGroup.ForeColor = Color.OldLace;
				}
				else
				{
					layoutControlGroup3.AppearanceGroup.ForeColor = Color.Navy;
					layoutControlGroup2.AppearanceGroup.ForeColor = Color.Navy;
					layoutControlGroup5.AppearanceGroup.ForeColor = Color.Navy;
				}
		}

		#region Overrides of CommonAbstractViewer<GetInvoicesPaymentBriefReport_Result>

		public override object ViewerID
		{
			get { return (int) ViewerName.InvoicePaymentBrief_Report_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return string.Empty; }
		}

		public override string GridXML
		{
			get
			{
				if (chkCashPayment.Checked)
					return Resources.LocalizedRes.grd_InvoicePaymentBrief_Report_CashOnly;
				if (chkCheckPayment.Checked)
					return Resources.LocalizedRes.grd_InvoicePaymentBrief_Report_CheckOnly;
				if (chkVisaPayment.Checked)
					return Resources.LocalizedRes.grd_InvoicePaymentBrief_Report_VisaOnly;
				if (chkPaymentType_All.Checked)
					return Resources.LocalizedRes.grd_InvoicePaymentBrief_Report;

				return null;
			}
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeUsers, User_cu.ItemsList, "FullName", "Person_CU_ID");
		}

		#endregion

		#region Implementation of IInvoicePaymentBrief_Report_Viewer

		public object InvoiceTypeID
		{
			get
			{
				if (chkInOut_All.Checked)
					return null;
				if(chkInAndPrivate.Checked)
						return (int) DB_InvoiceType.InPatientPrivate;
				if(chkInAndNotPrivate.Checked)
					return (int) DB_InvoiceType.InPatientNotPrivate;
				if(chkOutAndPrivate.Checked)
						return (int) DB_InvoiceType.OutPatientPrivate;
				if(chkOutAndNotPrivate.Checked )
					return (int) DB_InvoiceType.OutPatientNotPrivate;

				return null;
			}
		}

		public object PaymentTypeID
		{
			get
			{
				if (chkPaymentType_All.Checked)
					return null;
				if (chkCashPayment.Checked)
					return (int) DB_PaymentType.CashPayment;
				if (chkCheckPayment.Checked)
					return (int) DB_PaymentType.CheckPayment;
				if (chkVisaPayment.Checked)
					return (int)DB_PaymentType.VisaPayment;

				return null;
			}
		}

		public object FromDate
		{
			get { return dtDateFrom.EditValue; }
		}

		public object ToDate {
			get { return dtDateTo.EditValue; }
		}

		#endregion

		private void chkDate_CheckedChanged(object sender, System.EventArgs e)
		{
			dtDateFrom.Properties.ReadOnly = dtDateTo.Properties.ReadOnly = chkDate.Checked;
			if (chkDate.Checked)
			{
				dtDateFrom.EditValue = null;
				dtDateTo.EditValue = null;
			}
		}

		private void chkCashPayment_CheckedChanged(object sender, System.EventArgs e)
		{
			
		}

		private void chkVisaPayment_CheckedChanged(object sender, System.EventArgs e)
		{
		
		}

		private void chkCheckPayment_CheckedChanged(object sender, System.EventArgs e)
		{
		
		}

		private void chkPaymentType_All_CheckedChanged(object sender, System.EventArgs e)
		{
		
		}

	}
}
