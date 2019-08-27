using System.Windows.Forms;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.ReportsContainer
{
	public partial class CustomerFinanceInvoices_Report : 
		//UserControl
		CommonAbstractSearchViewer<GetCustomerInvoices_Result>,
		ICustomerFinanceInvoices_Report_Viewer
	{
		public CustomerFinanceInvoices_Report()
		{
			InitializeComponent();
		}

		#region Overrides of CommonAbstractViewer<GetCustomerInvoices_Result>

		public override object ViewerID
		{
			get { return (int) ViewerName.CustomerFinanceInvoicesReport_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "تقـريــــر فـواتيـــر العمـــلاء"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_CustomerFinanceInvoices_ReportViewer; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeDedicatedPerson, Customer_cu.ItemsList, "FullName", "Person_CU_ID");
		}

		public override void ClearControls()
		{
			lkeDedicatedPerson.EditValue = null;
		}

		public override object[] CollectSearchCriteria()
		{
			object[] parameters = new object[6];
			parameters[0] = CustomerID;
			parameters[1] = IsOnDuty;
			parameters[2] = InvoiceTypeID;
			parameters[3] = IsPaymentEnough;
			parameters[4] = IsFinanciallyReviewed;
			parameters[5] = IsFinanciallyCompleted;
			return parameters;
		}

		#endregion

		#region Implementation of ICustomerFinanceInvoices_Report_Viewer

		public object CustomerID
		{
			get { return lkeDedicatedPerson.EditValue; }
		}

		public object IsOnDuty
		{
			get
			{
				if (chkIsOnDuty_All.Checked)
					return null;
				if (chkIsOnDuty_Deleted.Checked)
					return false;
				if (chkIsOnDuty_NotDeleted.Checked)
					return true;
				return null;
			}
		}

		public object InvoiceTypeID
		{
			get
			{
				if (chkInvoiceType_All.Checked)
					return null;
				if (chkInvoiceType_SellingInvoicesOnly.Checked)
					return (int)DB_InvoiceType.SellingInvoice;
				if (chkInvoiceType_ReturningSellingInvoicesOnly.Checked)
					return (int)DB_InvoiceType.ReturningSellingInvoice;
				return null;
			}
		}

		public object IsPaymentEnough
		{
			get { return null; }
		}

		public object IsFinanciallyReviewed
		{
			get
			{
				if (chkIsReviewed_All.Checked)
					return null;
				if (chkIsReviewed_NotReviewed.Checked)
					return false;
				if (chkIsReviewed_Reviewed.Checked)
					return true;
				return null;
			}
		}

		public object IsFinanciallyCompleted
		{
			get
			{
				if (chkIsCompleted_All.Checked)
					return null;
				if (chkIsCompleted_NotCompleted.Checked)
					return false;
				if (chkIsCompleted_Completed.Checked)
					return true;
				return null;
			}
		}

		#endregion

	}
}
