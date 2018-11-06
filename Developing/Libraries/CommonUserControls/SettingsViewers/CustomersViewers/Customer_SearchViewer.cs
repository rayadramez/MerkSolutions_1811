using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.CustomersViewers
{
	public partial class Customer_SearchViewer : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractSearchViewer<Customer_cu>,
		ICustomerViewer
	{
		public Customer_SearchViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_Customer_SearchViewer);
			CommonViewsActions.SetupSyle(this);

			txtFirstNameP.Focus();
		}

		#region Overrides of CommonAbstractViewer<Customer_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.Customer_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "العمــــلاء"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_Customer_SearchViewer; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeMaritalStatus, MaritalStatus_p.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeFirstIdentificationCardType, IdentificationCardType_p.ItemsList);
		}

		public override void ClearControls()
		{
			txtFirstNameP.EditValue = null;
			txtSecondNameP.EditValue = null;
			txtThirdNameP.EditValue = null;
			txtFourthNameP.EditValue = null;
			rdGender.EditValue = true;

			dtDateOfBirth.EditValue = null;
			dtFirstIdentificationCardIssueDate.EditValue = null;
			dtFirstIdentificationCardExpirationDate.EditValue = null;
		}

		#endregion

		#region Implementation of ICustomerViewer

		public object FirstName
		{
			get { return txtFirstNameP.EditValue; }
			set { txtFirstNameP.EditValue = value; }
		}

		public object SecondName
		{
			get { return txtSecondNameP.EditValue; }
			set { txtSecondNameP.EditValue = value; }
		}

		public object ThirdName
		{
			get { return txtThirdNameP.EditValue; }
			set { txtThirdNameP.EditValue = value; }
		}

		public object FourthName
		{
			get { return txtFourthNameP.EditValue; }
			set { txtFourthNameP.EditValue = value; }
		}

		public object MaritalStatus
		{
			get { return lkeMaritalStatus.EditValue; }
			set { lkeMaritalStatus.EditValue = value; }
		}

		public object Gender
		{
			get { return rdGender.EditValue; }
			set { rdGender.EditValue = value; }
		}

		public object BirthDate
		{
			get { return dtDateOfBirth.EditValue; }
			set { dtDateOfBirth.EditValue = value; }
		}

		public object InternalCode
		{
			get { return txtInternalCode.EditValue; }
			set { txtInternalCode.EditValue = value; }
		}

		public object Mobile1
		{
			get { return txtMobile1.EditValue; }
			set { txtMobile1.EditValue = value; }
		}

		public object Mobile2
		{
			get { return txtMobile2.EditValue; }
			set { txtMobile2.EditValue = value; }
		}

		public object Phone1
		{
			get { return txtPhone1.EditValue; }
			set { txtPhone1.EditValue = value; }
		}

		public object Phone2
		{
			get { return txtPhone2.EditValue; }
			set { txtPhone2.EditValue = value; }
		}

		public object Address
		{
			get { return txtAddress.EditValue; }
			set { txtAddress.EditValue = value; }
		}

		public object Email
		{
			get { return txtEmail.EditValue; }
			set { txtEmail.EditValue = value; }
		}

		public object IdentificationCardType
		{
			get { return lkeFirstIdentificationCardType.EditValue; }
			set { lkeFirstIdentificationCardType.EditValue = value; }
		}

		public object IdentificationCardNumber
		{
			get { return txtFirstIdentifiactionCardNumber.EditValue; }
			set { txtFirstIdentifiactionCardNumber.EditValue = value; }
		}

		public object IdentificationCardIssueDate
		{
			get { return dtFirstIdentificationCardIssueDate.EditValue; }
			set { dtFirstIdentificationCardIssueDate.EditValue = value; }
		}

		public object IdentificationCardExpirationDate
		{
			get { return dtFirstIdentificationCardExpirationDate.EditValue; }
			set { dtFirstIdentificationCardExpirationDate.EditValue = value; }
		}

		public object IsDebitChartOfAccount { get; set; }
		public object Debit_ChartOfAccount { get; set; }
		public object IsTaxChartOfAccount { get; set; }
		public object Tax_ChartOfAccount { get; set; }
		public object IsCreditChartOfAccount { get; set; }
		public object Credit_ChartOfAccount { get; set; }
		public object IsCurrentChartOfAccount { get; set; }
		public object Current_ChartOfAccount { get; set; }

		#endregion
	}
}
