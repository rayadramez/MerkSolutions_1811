using System;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.DoctorViewers
{
	public partial class Doctor_EditorViewer :
		//UserControl
		CommonAbstractEditorViewer<Doctor_cu>,
		IDoctorViewer
	{
		public Doctor_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_Doctor_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<Doctor_cu>

		public override IMVCController<Doctor_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.Doctor_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return Resources.LocalizedRes.grd_UserSearchViewer_UC; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeMaritalStatus, MaritalStatus_p.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeFirstIdentificationCardType, IdentificationCardType_p.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeDoctorCategory, DoctorCategory_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeDoctorProfessionalFees, DoctorProfessionalFeesIssuingType_p.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeDoctorRank, DoctorRank_p.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeDoctorSpecialization, DoctorSpecialization_p.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeDoctorTaxType, DoctorTaxType_cu.ItemsList);

			dtDateOfBirth.EditValue = DateTime.Now;
			chkInternalDoctor.Checked = true;
		}

		public override void ClearControls()
		{
			txtFirstNameP.EditValue = null;
			txtSecondNameP.EditValue = null;
			txtThirdNameP.EditValue = null;
			txtFourthNameP.EditValue = null;
			rdGender.EditValue = true;
			txtInternalCode.EditValue = null;
			lkeMaritalStatus.EditValue = null;
			dtDateOfBirth.EditValue = DateTime.Now;
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
			txtLoginName.EditValue = null;
			txtLoginPassword.EditValue = null;
			txtLoginPasswordConfirmation.EditValue = null;
			lkeDoctorRank.EditValue = null;
			lkeDoctorSpecialization.EditValue = null;
			lkeDoctorCategory.EditValue = null;
			lkeDoctorProfessionalFees.EditValue = null;
			lkeDoctorTaxType.EditValue = null;
			chkInternalDoctor.Checked = true;
			txtDoctorPrivateMobile.EditValue = null;
			txtDescription.EditValue = null;
		}

		#endregion

		#region Implementation of IDoctorViewer

		public object FirstName_P
		{
			get { return txtFirstNameP.EditValue; }
			set { txtFirstNameP.EditValue = value; }
		}

		public object SecondName_P
		{
			get { return txtSecondNameP.EditValue; }
			set { txtSecondNameP.EditValue = value; }
		}

		public object ThirdName_P
		{
			get { return txtThirdNameP.EditValue; }
			set { txtThirdNameP.EditValue = value; }
		}

		public object FourthName_P
		{
			get { return txtFourthNameP.EditValue; }
			set { txtFourthNameP.EditValue = value; }
		}

		public object Gender
		{
			get { return rdGender.EditValue; }
			set { rdGender.EditValue = value; }
		}

		public object MaritalStatusID
		{
			get { return lkeMaritalStatus.EditValue; }
			set { lkeMaritalStatus.EditValue = value; }
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

		public object EMail
		{
			get { return txtEmail.EditValue; }
			set { txtEmail.EditValue = value; }
		}

		public object IdentificationCardTypeID
		{
			get { return lkeFirstIdentificationCardType.EditValue; }
			set { lkeFirstIdentificationCardType.EditValue = value; }
		}

		public object IdentificationCardNumber
		{
			get { return txtFirstIdentifiactionCardNumber.EditValue; }
			set { txtFirstIdentifiactionCardNumber.EditValue = value; }
		}

		public object IdentificationCardIssuingDate
		{
			get { return dtFirstIdentificationCardIssueDate.EditValue; }
			set { dtFirstIdentificationCardIssueDate.EditValue = value; }
		}

		public object IdentificationCardExpirationDate
		{
			get { return dtFirstIdentificationCardExpirationDate.EditValue; }
			set { dtFirstIdentificationCardExpirationDate.EditValue = value; }
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

		public object ConfirmationPassword
		{
			get { return txtLoginPasswordConfirmation.EditValue; }
			set { txtLoginPasswordConfirmation.EditValue = value; }
		}

		public object DoctorRankID
		{
			get { return lkeDoctorRank.EditValue; }
			set { lkeDoctorRank.EditValue = value; }
		}

		public object DoctorSpecializationID
		{
			get { return lkeDoctorSpecialization.EditValue; }
			set { lkeDoctorSpecialization.EditValue = value; }
		}

		public object DoctorCategoryID
		{
			get { return lkeDoctorCategory.EditValue; }
			set { lkeDoctorCategory.EditValue = value; }
		}

		public object DoctorTaxTypeID
		{
			get { return lkeDoctorTaxType.EditValue; }
			set { lkeDoctorTaxType.EditValue = value; }
		}

		public object DoctorProfessionalFees
		{
			get { return lkeDoctorProfessionalFees.EditValue; }
			set { lkeDoctorProfessionalFees.EditValue = value; }
		}

		public object IsInternal
		{
			get { return chkInternalDoctor.Checked; }
			set
			{
				if (Convert.ToBoolean(value))
					chkInternalDoctor.Checked = true;
				else
					chkOutDoctor.Checked = true;
			}
		}

		public object PrivateMobile
		{
			get { return txtDoctorPrivateMobile.EditValue; }
			set { txtFirstNameP.EditValue = value; }
		}

		public object Description
		{
			get { return txtDescription.EditValue; }
			set { txtDescription.EditValue = value; }
		}

		#endregion

		private void lkeDoctorTaxType_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeDoctorTaxType.EditValue == null)
			{
				txtDoctorTaxPercentage.EditValue = 0;
				return;
			}

			DoctorTaxType_cu doctorTaxType =
				DoctorTaxType_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(lkeDoctorTaxType.EditValue)));
			if(doctorTaxType == null)
			{
				txtDoctorTaxPercentage.EditValue = 0;
				return;
			}

			txtDoctorTaxPercentage.Text = doctorTaxType.TaxPercent*100 + "%";
		}
	}
}

