using System;
using System.Collections.Generic;
using CommonControlLibrary;
using CommonUserControls.SettingsViewers.InsurancePolicyViewers;
using DevExpress.XtraEditors.DXErrorProvider;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.PatientViewers
{
	public partial class PatientEditorViewer_UC : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<Person_cu>,
		IPatientViewer
	{
		public bool HasRelativeDetails { get; set; }
		private bool IsAgeChanged { get; set; }
		private InsurancePolicyEditorViewer_UC _insurancePolicyEditorViewer;

		public PatientEditorViewer_UC()
		{
			InitializeComponent();

			CommonViewsActions.SetupSyle(this);
			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_PatientEditorViewer_UC);
		}

		private void dtDateOfBirth_EditValueChanged(object sender, System.EventArgs e)
		{
			if (IsAgeChanged || dtDateOfBirth.EditValue == null)
				return;

			IsAgeChanged = true;

			spnAge.EditValue = CommonActions.CommonActions.CalculateYears(dtDateOfBirth.EditValue);
			IsAgeChanged = false;
		}

		private void spnAge_EditValueChanged(object sender, System.EventArgs e)
		{
			if (IsAgeChanged || spnAge.EditValue == null)
				return;

			IsAgeChanged = true;

			if (spnAge.EditValue != null)
			{
				if (Convert.ToInt32(spnAge.EditValue) > 0)
					dtDateOfBirth.EditValue = CommonActions.CommonActions.ConvertYearsToDate(Convert.ToInt32(spnAge.EditValue));
				else
				{
					spnAge.EditValue = null;
					IsAgeChanged = false;
					return;
				}
			}
			else
				dtDateOfBirth.EditValue = null;
		}

		private void lkeCountryOfResidence_EditValueChanged(object sender, System.EventArgs e)
		{
			if (lkeCountryOfResidence.EditValue == null)
			{
				lkeCity.EditValue = null;
				lkeCity.Properties.ReadOnly = true;
				lkeRegion.Properties.ReadOnly = true;
				return;
			}

			lkeCity.Properties.ReadOnly = false;
			CommonViewsActions.FillGridlookupEdit(lkeCity,
				City_cu.ItemsList.FindAll(
					item => Convert.ToInt32(item.Country_CU_ID).Equals(Convert.ToInt32(lkeCountryOfResidence.EditValue))));
		}

		private void lkeCity_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeCity.EditValue == null)
			{
				lkeRegion.EditValue = null;
				lkeRegion.Properties.ReadOnly = true;
				return;
			}

			lkeRegion.Properties.ReadOnly = false;
			CommonViewsActions.FillGridlookupEdit(lkeRegion,
				Region_cu.ItemsList.FindAll(
					item => Convert.ToInt32(item.City_CU_ID).Equals(Convert.ToInt32(lkeCity.EditValue))));
		}

		private void lkeInsuranceCarrier_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeInsuranceCarrier.EditValue == null)
			{
				lkeInsuranceLevel.Properties.ReadOnly = true;
				return;
			}

			InsuranceCarrier_cu carrier =
				InsuranceCarrier_cu.ItemsList.Find(
					item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(lkeInsuranceCarrier.EditValue)));
			if(carrier == null)
			{
				lkeInsuranceLevel.Properties.ReadOnly = true;
				return;
			}

			List<InsuranceCarrier_InsuranceLevel_cu> bridgeList =
				InsuranceCarrier_InsuranceLevel_cu.ItemsList.FindAll(
					item => Convert.ToInt32(item.InsuranceCarrier_CU_ID).Equals(Convert.ToInt32(carrier.ID)));
			if (bridgeList.Count == 0)
			{
				lkeInsuranceLevel.Properties.ReadOnly = true;
				return;
			}

			List<InsuranceLevel_cu> levelsList = new List<InsuranceLevel_cu>();
			foreach (InsuranceCarrier_InsuranceLevel_cu bridge in bridgeList)
			{
				InsuranceLevel_cu level =
					InsuranceLevel_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(bridge.InsuranceLevel_CU_ID)));
				if (level == null)
				{
					lkeInsuranceLevel.Properties.ReadOnly = true;
					continue;
				}

				levelsList.Add(level);
			}

			CommonViewsActions.FillGridlookupEdit(lkeInsuranceLevel, levelsList);
			lkeInsuranceLevel.Properties.ReadOnly = false;
		}

		private void btnAddNewInsurancePolicy_Click(object sender, EventArgs e)
		{
			BaseController<InsuranceCarrier_InsuranceLevel_cu>.ShowEditorControl(ref _insurancePolicyEditorViewer, this, null, null,
				EditorContainerType.Regular, ViewerName.InsurancePolicy_Viewer, DB_CommonTransactionType.CreateNew,
				"سياســات جهــات التأميـــن", true);
		}

		public void ValidateBeforeSave()
		{
			ConditionValidationRule firstName = new ConditionValidationRule();
			firstName.ConditionOperator = ConditionOperator.IsNotBlank;
			firstName.ErrorText = "برجاء إدخال الإسم الأول";

			ConditionValidationRule secondName = new ConditionValidationRule();
			secondName.ConditionOperator = ConditionOperator.IsNotBlank;
			secondName.ErrorText = "برجاء إدخال الإسم الثاني";

			ConditionValidationRule thirdName = new ConditionValidationRule();
			thirdName.ConditionOperator = ConditionOperator.IsNotBlank;
			thirdName.ErrorText = "برجاء إدخال الإسم الثالث";

			ConditionValidationRule gender = new ConditionValidationRule();
			gender.ConditionOperator = ConditionOperator.IsNotBlank;
			gender.ErrorText = "برجاء إختيار الجنس";

			dxValidationProvider1.SetValidationRule(txtFirstNameP, firstName);
			dxValidationProvider1.SetValidationRule(txtSecondNameP, secondName);
			dxValidationProvider1.SetValidationRule(txtThirdNameP, thirdName);
			dxValidationProvider1.SetValidationRule(rdGender, gender);

			dxValidationProvider1.ValidationMode = ValidationMode.Auto;

			dxValidationProvider1.Validate(txtFirstNameP);
			dxValidationProvider1.Validate(txtSecondNameP);
			dxValidationProvider1.Validate(txtThirdNameP);
			dxValidationProvider1.Validate(rdGender);
		}

		#region Overrides of CommonAbstractViewer<Person_cu>

		public override IMVCController<Person_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return ViewerName.PatientViewer; }
		}

		public override string HeaderTitle
		{
			get { return "بيانــات المرضــى"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkePersonTitle, PersonTitle_p.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeNationality, Country_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeMaritalStatus, MaritalStatus_p.ItemsList);

			List<InsuranceCarrier_InsuranceLevel_cu> bridgeList = InsuranceCarrier_InsuranceLevel_cu.ItemsList;
			List<InsuranceCarrier_cu> carriersList = new List<InsuranceCarrier_cu>();
			foreach (InsuranceCarrier_InsuranceLevel_cu bridge in bridgeList)
			{
				InsuranceCarrier_cu carrier =
					InsuranceCarrier_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(bridge.InsuranceCarrier_CU_ID)));
				if(carrier != null && !carriersList.Contains(carrier))
					carriersList.Add(carrier);
			}

			CommonViewsActions.FillGridlookupEdit(lkeInsuranceCarrier, carriersList);
			CommonViewsActions.FillGridlookupEdit(lkeCountryOfResidence, Country_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeRelationshipType, PersonRelativeType_p.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeRegion, Region_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeFirstIdentificationCardType, IdentificationCardType_p.ItemsList);

			lkeCity.Properties.ReadOnly = true;
			lkeRegion.Properties.ReadOnly = true;
			txtFirstNameP.Focus();
			IsAgeChanged = false;
		}

		#endregion

		#region Implementation of IPatientViewer

		public object PersonTitle
		{
			get { return lkePersonTitle.EditValue; }
			set { lkePersonTitle.EditValue = value; }
		}

		public object PersonGender
		{
			get { return rdGender.EditValue; }
			set { rdGender.EditValue = value; }
		}

		public object FirstNameP
		{
			get { return txtFirstNameP.EditValue; }
			set { txtFirstNameP.EditValue = value; }
		}

		public object SecondNameP
		{
			get { return txtSecondNameP.EditValue; }
			set { txtSecondNameP.EditValue = value; }
		}

		public object ThirdNameP
		{
			get { return txtThirdNameP.EditValue; }
			set { txtThirdNameP.EditValue = value; }
		}

		public object FourthNameP
		{
			get { return txtFourthNameP.EditValue; }
			set { txtFourthNameP.EditValue = value; }
		}

		public object FirstNameS
		{
			get { return txtFIrstNameS.EditValue; }
			set { txtFIrstNameS.EditValue = value; }
		}

		public object SecondNameS
		{
			get { return txtSecondNameS.EditValue; }
			set { txtSecondNameS.EditValue = value; }
		}

		public object ThirdNameS
		{
			get { return txtThirdNameS.EditValue; }
			set { txtThirdNameS.EditValue = value; }
		}

		public object FourthNameS
		{
			get { return txtFourthNameS.EditValue; }
			set { txtFourthNameS.EditValue = value; }
		}

		public object Nationality
		{
			get { return lkeNationality.EditValue; }
			set { lkeNationality.EditValue = value; }
		}

		public object MaritalStatus
		{
			get { return lkeMaritalStatus.EditValue; }
			set { lkeMaritalStatus.EditValue = value; }
		}

		public object DateOfBirth
		{
			get { return dtDateOfBirth.EditValue; }
			set { dtDateOfBirth.EditValue = value; }
		}

		public object InsuranceCarrier
		{
			get { return lkeInsuranceCarrier.EditValue; }
			set { lkeInsuranceCarrier.EditValue = value; }
		}

		public object InsuranceLevel
		{
			get { return lkeInsuranceLevel.EditValue; }
			set { lkeInsuranceLevel.EditValue = value; }
		}

		public object CountryOfResidence
		{
			get { return lkeCountryOfResidence.EditValue; }
			set { lkeCountryOfResidence.EditValue = value; }
		}

		public object City
		{
			get { return lkeCity.EditValue; }
			set { lkeCity.EditValue = value; }
		}

		public object Region
		{
			get { return lkeRegion.EditValue; }
			set { lkeRegion.EditValue = value; }
		}

		public object Address
		{
			get { return txtAddress.EditValue; }
			set { txtAddress.EditValue = value; }
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

		public object Email
		{
			get { return txtEmail.EditValue; }
			set { txtEmail.EditValue = value; }
		}

		public object RelativeName
		{
			get { return txtRelationName.EditValue; }
			set { txtRelationName.EditValue = value; }
		}

		public object RelativeType
		{
			get { return lkeRelationshipType.EditValue; }
			set { lkeRelationshipType.EditValue = value; }
		}

		public object RelativePhone
		{
			get { return txtRelationPhone.EditValue; }
			set { txtRelationPhone.EditValue = value; }
		}

		public object RelativeAddress {
			get { return txtRelationAddress.EditValue; }
			set { txtRelationAddress.EditValue = value; }
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

		public object IdentificationCardIssuingDate
		{
			get { return dtFirstIdentificationCardIssueDate.EditValue; }
			set { dtFirstIdentificationCardIssueDate.EditValue = value; }
		}

		public object IdentificationCardEpirationDate
		{
			get { return dtFirstIdentificationCardExpirationDate.EditValue; }
			set { dtFirstIdentificationCardExpirationDate.EditValue = value; }
		}

		#endregion

	}
}

