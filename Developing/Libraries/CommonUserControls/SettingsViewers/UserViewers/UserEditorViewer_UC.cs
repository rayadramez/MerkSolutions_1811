using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.UserViewers
{
	public partial class UserEditorViewer_UC : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<User_cu>,
		IUserViewer
	{
		public UserEditorViewer_UC()
		{
			InitializeComponent();
			
			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_UserEditorViewer_UC);
			CommonViewsActions.SetupSyle(this);

			txtFirstNameP.Focus();
		}

		#region Overrides of CommonAbstractViewer<Person_cu>

		public override IMVCController<User_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return ViewerName.User_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "بيانــات المستخدمين"; }
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

			lkeMaritalStatus.EditValue = null;
			dtDateOfBirth.EditValue = null;
			txtInternalCode.EditValue = null;
			txtLoginName.EditValue = null;
			txtLoginPassword.EditValue = null;
			txtLoginPasswordConfirmation.EditValue = null;
			txtMobile1.EditValue = null;
			txtMobile2.EditValue = null;
			txtPhone1.EditValue = null;
			txtPhone2.EditValue = null;
			txtAddress.EditValue = null;
			txtEmail.EditValue = null;
			lkeFirstIdentificationCardType.EditValue = null;
			txtFirstIdentifiactionCardNumber.EditValue = null;
			dtFirstIdentificationCardIssueDate.EditValue = null;
			dtFirstIdentificationCardExpirationDate.EditValue = null;
		}

		#endregion

		#region Implementation of IUserViewer

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

		public object LoginName
		{
			get { return txtLoginName.EditValue; }
			set { txtLoginName.EditValue = value; }
		}

		public object Password
		{
			get { return txtLoginPassword.EditValue; }
			set { txtLoginPassword.EditValue = value; }
		}

		public object PasswordConfirmation
		{
			get { return txtLoginPasswordConfirmation.EditValue; }
			set { txtLoginPasswordConfirmation.EditValue = value; }
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

		#endregion
	}
}
