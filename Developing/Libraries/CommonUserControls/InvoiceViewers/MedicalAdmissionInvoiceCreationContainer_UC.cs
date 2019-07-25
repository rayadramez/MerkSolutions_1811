using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using CommonUserControls.SettingsViewers.InsurancePolicyViewers;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout.Utils;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.InvoiceViewers
{
	public partial class MedicalAdmissionInvoiceCreationContainer_UC :
		//UserControl
		CommonAbstractEditorViewer<Invoice>,
		IPatientInvoiceCreation, IViewerDataRelated
	{
		private bool isRemainingEdited;
		private InsurancePolicyEditorViewer_UC _insurancePolicyEditorViewer;
		private InsurancePolicySearchViewer_UC _insurancePolicySearchViewer;
		private InvoicePayment_UC _invoicePayment;
		public Insurance InsuranceObject { get; set; }
		public InvoiceDetail SelectedInvoiceDetail { get; set; }
		private bool useCustomServicePrice { get; set; }

		public MedicalAdmissionInvoiceCreationContainer_UC()
		{
			InitializeComponent();
		}

		private void MedicalAdmissionInvoiceCreationContainer_UC_Load(object sender, EventArgs e)
		{
			BackColor = Color.White;
			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_MedicalAdmissionInvoiceCreation_UC);
			CommonViewsActions.SetupGridControl(grdServices, Resources.LocalizedRes.grd_AddmissionContainer, false);
			CommonViewsActions.SetupSyle(this);

			if (!string.IsNullOrEmpty(ApplicationStaticConfiguration.SkinName) &&
				!string.IsNullOrWhiteSpace(ApplicationStaticConfiguration.SkinName))
				if (ApplicationStaticConfiguration.SkinName.Equals("McSkin"))
				{
					lblPatientID.ForeColor = Color.DarkOrange;
					lblPatientName.ForeColor = lblInsuranceCarrierName.ForeColor = lblInsuranceLevelName.ForeColor =
						lblInsurancePercentage.ForeColor = Color.DarkBlue;
					labelControl1.ForeColor = labelControl3.ForeColor =
						labelControl5.ForeColor = labelControl7.ForeColor = Color.Black;
					layoutControlGroup7.AppearanceGroup.ForeColor = layoutControlGroup9.AppearanceGroup.ForeColor =
						layoutControlGroup5.AppearanceGroup.ForeColor = layoutControlGroup4.AppearanceGroup.ForeColor =
							layoutControlGroup3.AppearanceGroup.ForeColor =
								layoutControlGroup2.AppearanceGroup.ForeColor =
									layoutControlGroup6.AppearanceGroup.ForeColor = Color.DarkBlue;
				}
				else
				{
					lblPatientID.ForeColor = Color.OrangeRed;
					lblPatientName.ForeColor = lblInsuranceCarrierName.ForeColor = lblInsuranceLevelName.ForeColor =
						lblInsurancePercentage.ForeColor = Color.Ivory;
					labelControl1.ForeColor = labelControl3.ForeColor =
						labelControl5.ForeColor = labelControl7.ForeColor = Color.OldLace;
					layoutControlGroup7.AppearanceGroup.ForeColor = layoutControlGroup9.AppearanceGroup.ForeColor =
						layoutControlGroup5.AppearanceGroup.ForeColor = layoutControlGroup4.AppearanceGroup.ForeColor =
							layoutControlGroup3.AppearanceGroup.ForeColor =
								layoutControlGroup2.AppearanceGroup.ForeColor =
									layoutControlGroup6.AppearanceGroup.ForeColor = Color.OldLace;
				}

			BringToFront();
		}

		#region Overrides of CommonAbstractViewer<Invoice>

		public override IMVCController<Invoice> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return ViewerName.PatientInvoiceCreation; }
		}

		public override string HeaderTitle
		{
			get { return "الفواتيـــر الطبيــــة"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeDoctor, Doctor_cu.ItemsList, valueMember: "Person_CU_ID");

			if (ViewerDataRelated != null && ViewerDataRelated is Patient_cu)
			{
				lblPatientID.Text = ((Patient_cu)ViewerDataRelated).Person_CU_ID.ToString();
				lblPatientName.Text = ((Patient_cu)ViewerDataRelated).PatientFullName;
				if (((Patient_cu)ViewerDataRelated).InsuranceCarrier_InsuranceLevel_CU_ID != null)
				{
					InsuranceCarrier_InsuranceLevel_cu insuranceBridge =
						InsuranceCarrier_InsuranceLevel_cu.ItemsList.Find(
							item =>
								Convert.ToInt32(item.ID)
									.Equals(Convert.ToInt32(((Patient_cu)ViewerDataRelated).InsuranceCarrier_InsuranceLevel_CU_ID)));
					if (insuranceBridge != null)
					{
						InsuranceCarrier_cu insuranceCarrier =
							InsuranceCarrier_cu.ItemsList.Find(
								item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(insuranceBridge.InsuranceCarrier_CU_ID)));
						if (insuranceCarrier != null)
							lblInsuranceCarrierName.Text = insuranceCarrier.Name_P;
						InsuranceLevel_cu insurancelevel =
							InsuranceLevel_cu.ItemsList.Find(
								item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(insuranceBridge.InsuranceLevel_CU_ID)));
						if (insurancelevel != null)
							lblInsuranceLevelName.Text = insurancelevel.Name_P;
						lblInsurancePercentage.Text = Convert.ToString(insuranceBridge.InsurancePercentage * 100);
					}
				}
			}

			switch (ApplicationStaticConfiguration.Organization)
			{
				case DB_Organization.Cardiovascular_Clinic:
				case DB_Organization.Dental_Mostafa:
					chkInPatient.Enabled = false;
					chkOutPatient.Checked = true;
					chkNotPrivateInvoice.Enabled = false;
					chkPrivateInvoice.Checked = true;
					chkLabServiceType.Enabled = false;
					break;
			}

			switch (ApplicationStaticConfiguration.InternalReceptionApplication)
			{
				case DB_Application.AdmissionReception:
					chkInPatient.Enabled = true;
					chkOutPatient.Enabled = false;
					chkInPatient.Checked = true;
					break;
				case DB_Application.ClinicReception:
					chkInPatient.Enabled = false;
					chkOutPatient.Enabled = true;
					chkOutPatient.Checked = true;
					chkExaminationServiceType.Checked = true;
					break;
				case DB_Application.AllReception:
					chkOutPatient.Enabled = true;
					chkInPatient.Enabled = true;
					chkInPatient.Checked = true;
					break;
				case DB_Application.OneDaySurgeryReception:
					chkOutPatient.Enabled = true;
					chkInPatient.Enabled = true;
					chkInPatient.Checked = true;
					break;
			}
		}

		public override void ClearControls()
		{
			spnAccummulatedServicesCost.EditValue = 0;
			spnAccummulatedServicesCost_PatientShare.EditValue = 0;
			spnDiscountToTotalPrice.EditValue = 0;
			spnInsuranceMaxAmount.EditValue = 0;
			spnInsurancePercentage.EditValue = 0;
			spnPatientMaxAmount.EditValue = 0;
			spnPatientPercentage.EditValue = 0;
			lkeInsuranceCarrier.EditValue = null;
			lkeInsuranceCarrier.EditValue = null;
			dtInvoiceCreationDate.DateTime = DateTime.Now;
			spnAmountPaid.EditValue = 0;
			spnRemainingAmount.EditValue = 0;
			spnNet.EditValue = 0;

			lkeStationPoint.EditValue = null;
			lkeStationPointStage.EditValue = null;

			chkResetRemaining.Checked = false;
			chkIsPaymentEnough.Checked = false;
			chkIsInsuranceAppliedToService.Enabled = false;
			chkIsServiceSurchargeApplied.Enabled = false;

			chkAddStamp.Checked = false;
			chkPrivateInvoice.Checked = true;
			chkIsInsuranceAppliedToInvoice.Checked = false;

			grdServices.DataSource = null;
			Grid_InvoiceDetails = null;

			ServiceDate = DateTime.Now;
			lkeService.EditValue = null;
			lkeServiceCategory.EditValue = null;
			lkeDoctor.EditValue = null;
			ServiceDescription = null;
			ServiceDiscount = 0;
			ServicePrice = 0.00;
			IsSurchargeAppliedToService = false;
			IsInsuranceAppliedToService = false;
		}

		#endregion

		#region Implementation of IPatientService

		public object ServiceType
		{
			get
			{
				if (chkExaminationServiceType.Checked)
					return DB_ServiceType.ExaminationService;
				if (chkAccommodationServiceType.Checked)
					return DB_ServiceType.ParentAccommodationService;
				if (chkInvestigationServicetype.Checked)
					return DB_ServiceType.InvestigationServices;
				if (chkOnsDaySurgery.Checked)
					return DB_ServiceType.SurgeryService;
				if (chkLabServiceType.Checked)
					return DB_ServiceType.LabServices;

				return null;
			}
		}

		public object LabServices { get; set; }

		public object ServiceDate
		{
			get { return dtServiceDate.EditValue; }
			set { dtServiceDate.EditValue = value; }
		}

		public object ServiceCategoryID
		{
			get { return lkeServiceCategory.EditValue; }
			set { lkeServiceCategory.EditValue = value; }
		}

		public object ServiceID
		{
			get { return lkeService.EditValue; }
			set { lkeService.EditValue = value; }
		}

		public object InPatientRoomClassificationID
		{
			get { return lkeInPatientRoomClassification.EditValue; }
			set { lkeInPatientRoomClassification.EditValue = value; }
		}

		public object InPatientRoomID
		{
			get { return lkeInPatientRoom.EditValue; }
			set { lkeInPatientRoom.EditValue = value; }
		}

		public object InPatientRoomBedID
		{
			get { return lkeInPatientRoomBed.EditValue; }
			set { lkeInPatientRoomBed.EditValue = value; }
		}

		public object AdmissionDate
		{
			get { return dtAdmission_Date.EditValue; }
			set { dtAdmission_Date.EditValue = value; }
		}

		public object CompanionsNumbers
		{
			get { return spnCompanionNumbers.EditValue; }
			set { spnCompanionNumbers.EditValue = value; }
		}

		public object DoctorID
		{
			get { return lkeDoctor.EditValue; }
			set { lkeDoctor.EditValue = value; }
		}

		public object ServicePrice
		{
			get { return spnServicePrice.EditValue; }
			set { spnServicePrice.EditValue = value; }
		}

		public object ServiceDiscount
		{
			get { return spnServiceDiscount.EditValue; }
			set { spnServiceDiscount.EditValue = value; }
		}

		public object ServiceDescription
		{
			get { return txtServiceDescription.EditValue; }
			set { txtServiceDescription.EditValue = value; }
		}

		public object IsSurchargeAppliedToService
		{
			get { return chkIsServiceSurchargeApplied.Checked; }
			set { chkIsServiceSurchargeApplied.Checked = Convert.ToBoolean(value); }
		}

		public object IsInsuranceAppliedToService
		{
			get { return chkIsInsuranceAppliedToService.Checked; }
			set { chkIsInsuranceAppliedToService.Checked = Convert.ToBoolean(value); }
		}

		#endregion

		#region Implementation of IPatientInvoiceCreation

		public object InvoiceCreationDate
		{
			get { return dtInvoiceCreationDate.EditValue; }
			set { dtInvoiceCreationDate.EditValue = value; }
		}

		public object PatientID
		{
			get
			{
				if (ViewerDataRelated != null)
					return ((Patient_cu)ViewerDataRelated).Person_CU_ID;
				return null;
			}
		}

		public object InvoiceTypeID
		{
			get
			{
				if (chkInPatient.Checked && chkPrivateInvoice.Checked)
					return (int)DB_InvoiceType.InPatientPrivate;
				if (chkInPatient.Checked && chkNotPrivateInvoice.Checked)
					return (int)DB_InvoiceType.InPatientNotPrivate;
				if (chkOutPatient.Checked && chkPrivateInvoice.Checked)
					return (int)DB_InvoiceType.OutPatientPrivate;
				if (chkOutPatient.Checked && chkNotPrivateInvoice.Checked)
					return (int)DB_InvoiceType.OutPatientNotPrivate;

				return null;
			}
			set
			{
				switch ((DB_InvoiceType)value)
				{
					case DB_InvoiceType.InPatientPrivate:
						chkInPatient.Checked = true;
						chkPrivateInvoice.Checked = true;
						break;
					case DB_InvoiceType.InPatientNotPrivate:
						chkInPatient.Checked = true;
						chkNotPrivateInvoice.Checked = true;
						break;
					case DB_InvoiceType.OutPatientPrivate:
						chkOutPatient.Checked = true;
						chkPrivateInvoice.Checked = true;
						break;
					case DB_InvoiceType.OutPatientNotPrivate:
						chkOutPatient.Checked = true;
						chkNotPrivateInvoice.Checked = true;
						break;
				}
			}
		}

		public object InvoiceDiscount
		{
			get { return spnDiscountToTotalPrice.EditValue; }
			set { spnDiscountToTotalPrice.EditValue = value; }
		}

		public object IsInsuranceAppliedToInvoice
		{
			get { return chkIsInsuranceAppliedToInvoice.Checked; }
			set { chkIsInsuranceAppliedToInvoice.Checked = Convert.ToBoolean(value); }
		}

		public object IsSurchargeAppliedToInvoice
		{
			get { return chkIsSurchargesAppliedToInvoice.Checked; }
			set { chkIsSurchargesAppliedToInvoice.Checked = Convert.ToBoolean(value); }
		}

		public object InsuranceCarrierID
		{
			get { return lkeInsuranceCarrier.EditValue; }
			set { lkeInsuranceCarrier.EditValue = value; }
		}

		public object InsuranceLevelID
		{
			get { return lkeInsuaranceLevel.EditValue; }
			set { lkeInsuaranceLevel.EditValue = value; }
		}

		public object InsurancePercentage
		{
			get { return spnInsurancePercentage.EditValue; }
			set { spnInsurancePercentage.EditValue = value; }
		}

		public object InsurancePatientMaxAmount
		{
			get { return spnPatientMaxAmount.EditValue; }
			set { spnPatientMaxAmount.EditValue = value; }
		}

		public object InsuranceMaxAmount
		{
			get { return spnInsuranceMaxAmount.EditValue; }
			set { spnInsuranceMaxAmount.EditValue = value; }
		}

		public object AccummulatedServicesPrice
		{
			get { return spnAccummulatedServicesCost.EditValue; }
			set { spnAccummulatedServicesCost.EditValue = value; }
		}

		public object AccummulativeServicesPatientShare
		{
			get { return spnAccummulatedServicesCost_PatientShare.EditValue; }
			set { spnAccummulatedServicesCost_PatientShare.EditValue = value; }
		}

		public object AccummulativeServicesInsuranceShare
		{
			get
			{
				if (AccummulatedServicesPrice != null && AccummulativeServicesPatientShare != null)
					return Convert.ToDouble(AccummulatedServicesPrice) - Convert.ToDouble(AccummulativeServicesPatientShare);
				return 0;
			}
		}

		public object IsStampApplied
		{
			get { return chkAddStamp.Checked; }
			set { chkAddStamp.Checked = Convert.ToBoolean(value); }
		}

		public object StampAmount
		{
			get
			{
				return FinancialBusinessLogicLibrary.GetAccummulativeSurchargeAmount((DB_InvoiceType)InvoiceTypeID,
					DB_SurchargeType.MedicalStamp);
			}
		}

		public object SurchargeAmount
		{
			get
			{
				return FinancialBusinessLogicLibrary.GetAccummulativeSurchargeAmount((DB_InvoiceType)InvoiceTypeID,
					DB_SurchargeType.AdditionalServices);
			}
		}

		public object StationPointID
		{
			get { return lkeStationPoint.EditValue; }
			set { lkeStationPoint.EditValue = value; }
		}

		public object StationPointStageID
		{
			get { return lkeStationPointStage.EditValue; }
			set { lkeStationPointStage.EditValue = value; }
		}

		public object IsPaymentAttached
		{
			get { return chkPaymentType.IsOn; }
			set { chkPaymentType.IsOn = Convert.ToBoolean(value); }
		}

		public object IsRemainingReturned
		{
			get { return chkResetRemaining.Checked; }
			set { chkResetRemaining.Checked = Convert.ToBoolean(value); }
		}

		public object AmountPaid
		{
			get { return spnAmountPaid.EditValue; }
			set { spnAmountPaid.EditValue = value; }
		}

		public object IsPaymentEnough
		{
			get { return chkIsPaymentEnough.Checked; }
			set { chkIsPaymentEnough.Checked = Convert.ToBoolean(value); }
		}

		public List<InvoiceDetail> Grid_InvoiceDetails
		{
			get { return (List<InvoiceDetail>)grdServices.DataSource; }
			set { grdServices.DataSource = (List<InvoiceDetail>)value; }
		}

		#endregion

		#region Implementation of IViewerDataRelated

		public object ViewerDataRelated { get; set; }

		#endregion

		#region Controls Events

		#region LookupEdit Events

		private void lkeInsuranceCarrier_EditValueChanged(object sender, EventArgs e)
		{
			if (InsuranceCarrierID == null)
			{
				lkeInsuaranceLevel.Properties.DataSource = null;
				lkeInsuaranceLevel.Properties.ReadOnly = true;
				spnInsurancePercentage.Properties.ReadOnly = true;
				spnInsuranceMaxAmount.Properties.ReadOnly = true;
				spnPatientMaxAmount.Properties.ReadOnly = true;
				return;
			}

			List<InsuranceLevel_cu> levelsList = null;
			List<InsuranceCarrier_InsuranceLevel_cu> bridgesList =
				InsuranceCarrier_InsuranceLevel_cu.ItemsList.FindAll(
					item => Convert.ToInt32(item.InsuranceCarrier_CU_ID).Equals(Convert.ToInt32(InsuranceCarrierID)));
			if (bridgesList.Count == 0)
			{
				lkeInsuaranceLevel.Properties.DataSource = null;
				lkeInsuaranceLevel.Properties.ReadOnly = true;
				spnInsurancePercentage.Properties.ReadOnly = true;
				spnInsuranceMaxAmount.Properties.ReadOnly = true;
				spnPatientMaxAmount.Properties.ReadOnly = true;
				return;
			}

			foreach (InsuranceCarrier_InsuranceLevel_cu bridge in bridgesList)
			{
				InsuranceLevel_cu level =
					InsuranceLevel_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(bridge.InsuranceLevel_CU_ID)));
				if (level != null)
				{
					if (levelsList == null)
						levelsList = new List<InsuranceLevel_cu>();

					levelsList.Add(level);
				}
			}

			CommonViewsActions.FillGridlookupEdit(lkeInsuaranceLevel, levelsList);
			lkeInsuaranceLevel.Properties.ReadOnly = false;

			InsuranceObject.InsuranceCarrierID = InsuranceCarrierID;
		}

		private void lkeInsuaranceLevel_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeInsuaranceLevel == null)
				return;

			InsuranceObject.InsuranceLevelID = InsuranceLevelID;
			InsuranceObject.IsIncludedInInsurance = true;

			if (InsuranceCarrierID != null && InsuranceLevelID != null)
			{
				InsuranceCarrier_InsuranceLevel_cu bridge =
					InsuranceCarrier_InsuranceLevel_cu.ItemsList.Find(
						item =>
							Convert.ToInt32(item.InsuranceCarrier_CU_ID).Equals(Convert.ToInt32(InsuranceCarrierID)) &&
							Convert.ToInt32(item.InsuranceLevel_CU_ID).Equals(Convert.ToInt32(InsuranceLevelID)));

				if (bridge != null)
				{
					spnInsurancePercentage.EditValue = bridge.InsurancePercentage * 100;
					spnPatientPercentage.EditValue = (1 - bridge.InsurancePercentage) * 100;
					spnPatientMaxAmount.EditValue = bridge.PatientMaxAmount;
					spnInsuranceMaxAmount.EditValue = null;
				}
			}

			if (InsuranceObject != null && Convert.ToBoolean(IsInsuranceAppliedToService))
				ServicePrice = FinancialBusinessLogicLibrary.GetServicePrice(ServiceID, DoctorID, InsuranceObject.InsuranceCarrierID,
					InsuranceObject.InsuranceLevelID);
			else
				ServicePrice = FinancialBusinessLogicLibrary.GetServicePrice(ServiceID, DoctorID, null);

			if ((Grid_InvoiceDetails == null || Grid_InvoiceDetails.Count == 0))
				return;

			FinancialBusinessLogicLibrary.Recalculate_OutPatient_InvoiceDetails(Grid_InvoiceDetails, IsInsuranceAppliedToInvoice, true,
				InsuranceCarrierID, InsuranceLevelID, InsurancePercentage, InsurancePatientMaxAmount);
			grdServices.RefreshDataSource();
		}

		private void lkeServiceCategory_EditValueChanged(object sender, EventArgs e)
		{
			if (ServiceCategoryID == null)
				return;

			CommonViewsActions.FillGridlookupEdit(lkeService,
				Service_cu.ItemsList.FindAll(
					item => Convert.ToInt32(item.ServiceCategory_CU_ID).Equals(Convert.ToInt32(ServiceCategoryID))));

			if (InsuranceObject != null && Convert.ToBoolean(IsInsuranceAppliedToService))
				ServicePrice = FinancialBusinessLogicLibrary.GetServicePrice(ServiceID, DoctorID, InsuranceObject.InsuranceCarrierID,
					InsuranceObject.InsuranceLevelID);
			else
				ServicePrice = FinancialBusinessLogicLibrary.GetServicePrice(ServiceID, DoctorID, null);

			List<ServiceCategory_StationPoint_cu> bridgeList = ServiceCategory_StationPoint_cu.ItemsList.FindAll(item =>
				Convert.ToInt32(item.ServiceCategory_CU_ID).Equals(Convert.ToInt32(ServiceCategoryID)));
			if (bridgeList != null && bridgeList.Count > 0)
			{
				List<StationPoint_cu> stationPointList = new List<StationPoint_cu>();
				foreach (ServiceCategory_StationPoint_cu pointCu in bridgeList)
				{
					StationPoint_cu stationPoint = StationPoint_cu.ItemsList.Find(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(pointCu.StationPoint_CU_ID)));
					if(stationPoint != null)
						stationPointList.Add(stationPoint);
				}

				CommonViewsActions.FillGridlookupEdit(lkeStationPoint, stationPointList);
			}
		}

		private void lkeService_EditValueChanged(object sender, EventArgs e)
		{
			if (InsuranceObject != null && Convert.ToBoolean(IsInsuranceAppliedToService))
				ServicePrice = FinancialBusinessLogicLibrary.GetServicePrice(ServiceID, DoctorID, InsuranceObject.InsuranceCarrierID,
					InsuranceObject.InsuranceLevelID);
			else
				ServicePrice = FinancialBusinessLogicLibrary.GetServicePrice(ServiceID, DoctorID, null);

			List<Service_StationPoint_cu> bridgeList = Service_StationPoint_cu.ItemsList.FindAll(item =>
				Convert.ToInt32(item.Service_CU_ID).Equals(Convert.ToInt32(ServiceID)));
			if (bridgeList != null && bridgeList.Count > 0)
			{
				List<StationPoint_cu> stationPointList = new List<StationPoint_cu>();
				foreach (Service_StationPoint_cu pointCu in bridgeList)
				{
					StationPoint_cu stationPoint = StationPoint_cu.ItemsList.Find(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(pointCu.StationPoint_CU_ID)));
					if (stationPoint != null)
						stationPointList.Add(stationPoint);
				}

				CommonViewsActions.FillGridlookupEdit(lkeStationPoint, stationPointList);
			}
		}

		private void lkeDoctor_EditValueChanged(object sender, EventArgs e)
		{
			if (InsuranceObject != null && Convert.ToBoolean(IsInsuranceAppliedToService))
				ServicePrice = FinancialBusinessLogicLibrary.GetServicePrice(ServiceID, DoctorID, InsuranceObject.InsuranceCarrierID,
					InsuranceObject.InsuranceLevelID);
			else
				ServicePrice = FinancialBusinessLogicLibrary.GetServicePrice(ServiceID, DoctorID, null);

			if (Grid_InvoiceDetails != null && Grid_InvoiceDetails.Count > 0)
			{
				XtraMessageBox.Show("لا يمكنــك تغييــر الطبيـــب فى نفــس الفـاتـــورة", "تنبيـــــه", MessageBoxButtons.OK,
					MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
				lkeDoctor.EditValue = Grid_InvoiceDetails[0].Doctor_CU_ID;
			}
		}

		private void lkeStationPoint_EditValueChanged(object sender, EventArgs e)
		{
			StationPoint_cu stationPoint = StationPoint_cu.ItemsList.Find(item =>
				Convert.ToInt32(item.ID).Equals(Convert.ToInt32(lkeStationPoint.EditValue)));
			if (stationPoint != null)
				CommonViewsActions.FillGridlookupEdit(lkeStationPointStage,
					StationPointStage_cu.ItemsList
						.FindAll(item =>
							Convert.ToInt32(item.StationPoint_CU_ID).Equals(Convert.ToInt32(stationPoint.ID)))
						.OrderBy(x => x.OrderIndex).ToList());
			else
				lkeStationPointStage.Properties.DataSource = null;
		}

		private void lkeStationPointStage_EditValueChanged(object sender, EventArgs e)
		{
		
		}

		#endregion

		#region CheckEdit Events

		private void chkIsInsuranceAppliedToInvoice_CheckedChanged(object sender, EventArgs e)
		{
			CommonViewsActions.FillGridlookupEdit(lkeInsuranceCarrier, InsuranceCarrier_cu.ItemsList);

			DialogResult result = DialogResult.None;
			bool isAppliedToInsuranceExists = false;

			if (Grid_InvoiceDetails != null && Grid_InvoiceDetails.Count > 0)
			{
				foreach (InvoiceDetail invoiceDetail in Grid_InvoiceDetails)
				{
					if (invoiceDetail.IsInsuranceApplied)
					{
						isAppliedToInsuranceExists = true;
						break;
					}
				}
			}

			if (isAppliedToInsuranceExists && !chkIsInsuranceAppliedToInvoice.Checked)
				result = XtraMessageBox.Show(
					"يوجد خدمات تمت إضافتها وخاضعة للجهة." + "\r\n\r\n" + "هل ترغب فى إزالة بيانات الجهة ؟" + "\r\n\r\n" +
					"فى حالة الموافقة سوف يتم إعادة الحساب بدون جهة", "تنبيه", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

			switch (result)
			{
				case DialogResult.Yes:
					InsuranceCarrierID = null;
					InsuranceLevelID = null;
					InsurancePercentage = null;
					spnPatientPercentage.EditValue = null;
					InsurancePatientMaxAmount = null;
					FinancialBusinessLogicLibrary.Recalculate_OutPatient_InvoiceDetails(Grid_InvoiceDetails, IsInsuranceAppliedToInvoice, false,
						InsuranceCarrierID, InsuranceLevelID, InsurancePercentage, InsurancePatientMaxAmount);
					grdServices.RefreshDataSource();
					break;
			}

			//TODO :: should recalulate the invoice details if the user cancelled the insurance details or vise versa

			lkeInsuranceCarrier.Properties.ReadOnly = !chkIsInsuranceAppliedToInvoice.Checked;
			lkeInsuaranceLevel.Properties.ReadOnly = !chkIsInsuranceAppliedToInvoice.Checked;
			spnInsurancePercentage.Properties.ReadOnly = !chkIsInsuranceAppliedToInvoice.Checked;
			spnInsuranceMaxAmount.Properties.ReadOnly = !chkIsInsuranceAppliedToInvoice.Checked;
			spnPatientMaxAmount.Properties.ReadOnly = !chkIsInsuranceAppliedToInvoice.Checked;

			if (chkIsInsuranceAppliedToInvoice.Checked)
			{
				if (InsuranceObject == null)
					InsuranceObject = new Insurance
					{
						IsIncludedInInsurance = IsInsuranceAppliedToInvoice,
						InsuranceCarrierID = InsuranceCarrierID,
						InsuranceLevelID = InsuranceLevelID,
						InsurancePercentage = InsurancePercentage,
						MaxInsurancePatientAmount = InsurancePatientMaxAmount
					};
			}
			else
				InsuranceObject = null;

			InsuranceObject = InsuranceObject;

			IsInsuranceAppliedToService = IsInsuranceAppliedToInvoice;
			chkIsInsuranceAppliedToService.Enabled = Convert.ToBoolean(IsInsuranceAppliedToInvoice);

			AccummulatedServicesPrice = FinancialBusinessLogicLibrary.GetAccumulativeInvoiceDetails(Grid_InvoiceDetails,
				PriceType.TotalServicePriceBeforeSurcharges);

			spnAccummulatedServicesCost_PatientShare.EditValue =
				FinancialBusinessLogicLibrary.GetAccummulativePatientShare(Grid_InvoiceDetails);
		}

		private void chkIsInvoiceAdditionalServicesApplied_CheckedChanged(object sender, EventArgs e)
		{
			IsSurchargeAppliedToService = IsSurchargeAppliedToInvoice;
			chkIsServiceSurchargeApplied.Enabled = Convert.ToBoolean(IsSurchargeAppliedToInvoice);
		}

		private void chkAddStamp_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void chkInPatient_CheckedChanged(object sender, EventArgs e)
		{
			CommonViewsActions.LoadXMLFromString(layoutControl1,
				Resources.LocalizedRes.lyt_MedicalAdmissionInvoiceCreation_OneDaySurgery_UC);
			chkOnsDaySurgery.Checked = true;
		}

		private void chkOutPatient_CheckedChanged(object sender, EventArgs e)
		{
			CommonViewsActions.LoadXMLFromString(layoutControl1,
				Resources.LocalizedRes.lyt_MedicalAdmissionInvoiceCreation_UC);
		}

		private void chkNotPrivateInvoice_CheckedChanged(object sender, EventArgs e)
		{
			chkPrivateInvoice.Checked = !chkNotPrivateInvoice.Checked;
		}

		private void chkPrivateInvoice_CheckedChanged(object sender, EventArgs e)
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

		private void chkOnsDaySurgery_CheckedChanged(object sender, EventArgs e)
		{
			if (chkOnsDaySurgery.Checked)
				CommonViewsActions.FillGridlookupEdit(lkeServiceCategory,
					ServiceCategory_cu.ItemsList.FindAll(
						item =>
							Convert.ToBoolean(item.AllowAdmission) &&
							Convert.ToInt32(item.ServiceType_P_ID).Equals((int)DB_ServiceType.SurgeryService)));
			else
				lkeServiceCategory.Properties.DataSource = null;
		}

		private void chkIsInsuranceAppliedToService_CheckedChanged(object sender, EventArgs e)
		{
			if (InsuranceObject != null && Convert.ToBoolean(IsInsuranceAppliedToService))
				ServicePrice = FinancialBusinessLogicLibrary.GetServicePrice(ServiceID, DoctorID, InsuranceObject.InsuranceCarrierID,
					InsuranceObject.InsuranceLevelID);
			else
				ServicePrice = FinancialBusinessLogicLibrary.GetServicePrice(ServiceID, DoctorID, null);
		}

		private void chkIsServiceSurchargeApplied_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void chkUseCustomPrice_CheckedChanged(object sender, EventArgs e)
		{
			spnServicePrice.Properties.ReadOnly = !chkUseCustomPrice.Checked;
			useCustomServicePrice = chkUseCustomPrice.Checked;
		}

		private void chkResetRemaining_CheckedChanged(object sender, EventArgs e)
		{
			spnRemainingAmount.EditValue = 0;
		}

		private void chkPaymentType_Toggled(object sender, EventArgs e)
		{
			lyt_chkResetRemaining.Visibility = chkPaymentType.IsOn ? LayoutVisibility.Always : LayoutVisibility.Never;
			lyt_spnAmountPaid.Visibility = chkPaymentType.IsOn ? LayoutVisibility.Always : LayoutVisibility.Never;
			lyt_spnRemainingAmount.Visibility = chkPaymentType.IsOn ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		private void chkStationPoint_Toggled(object sender, EventArgs e)
		{
			lytStationPoint.Enabled = chkStationPoint.IsOn;
			lytStationPointStage.Enabled = chkStationPoint.IsOn;
			if (!chkStationPoint.IsOn)
			{
				StationPointID = null;
				StationPointStageID = null;
			}
		}

		#endregion

		#region SpinEdit Events

		private void spnINsurancePercentage_EditValueChanged(object sender, EventArgs e)
		{
			if (spnInsurancePercentage.EditValue == null || InsuranceObject == null)
				return;

			InsuranceObject.InsurancePercentage = InsurancePercentage;
			spnPatientPercentage.EditValue = 100 - Convert.ToDouble(spnInsurancePercentage.EditValue);
		}

		private void spnInsurancePatientMaxAmount_EditValueChanged(object sender, EventArgs e)
		{
			if (spnPatientMaxAmount.EditValue == null || InsuranceObject == null)
				return;

			InsuranceObject.MaxInsurancePatientAmount = InsurancePatientMaxAmount;
		}

		private void spnServicePrice_EditValueChanged(object sender, EventArgs e)
		{
			if (ServicePrice == null || Convert.ToDouble(ServicePrice) < 0)
				ServicePrice = 0;
		}

		private void spnServiceDiscount_EditValueChanged(object sender, EventArgs e)
		{
			if (ServiceDiscount == null || Convert.ToDouble(ServiceDiscount) < 0)
				ServiceDiscount = 0;
		}

		private void spnAccummulatedServicesCost_EditValueChanged(object sender, EventArgs e)
		{
			if (spnDiscountToTotalPrice.EditValue != null && spnAccummulatedServicesCost.EditValue != null)
				spnNet.EditValue = Convert.ToDouble(spnAccummulatedServicesCost.EditValue) -
								   Convert.ToDouble(spnDiscountToTotalPrice.EditValue);
		}

		private void spnAmountPaid_EditValueChanged(object sender, EventArgs e)
		{
			if (spnAmountPaid.EditValue == null || spnNet.EditValue == null ||
				Math.Abs(Convert.ToDouble(spnNet.EditValue)) < 0.00001)
			{
				spnRemainingAmount.EditValue = 0;
				return;
			}

			double totalNet = Convert.ToDouble(spnAccummulatedServicesCost_PatientShare.EditValue);
			double amountPaid = Convert.ToBoolean(IsPaymentAttached) ? Convert.ToDouble(spnAmountPaid.EditValue) : 0;

			if (amountPaid >= totalNet)
				chkIsPaymentEnough.Checked = true;

			spnRemainingAmount.EditValue = totalNet - amountPaid;
			if (totalNet - amountPaid > 0)
			{
				chkResetRemaining.Checked = false;
				chkIsPaymentEnough.Checked = false;
				chkResetRemaining.Enabled = false;
			}
			else
			{
				chkResetRemaining.Enabled = true;
				chkIsPaymentEnough.Checked = true;
			}

			if (Math.Abs(totalNet - amountPaid) < 0.0001)
				chkIsPaymentEnough.Checked = true;
			isRemainingEdited = false;
		}

		private void spnRemainingAmount_EditValueChanged(object sender, EventArgs e)
		{
			if (isRemainingEdited || spnRemainingAmount.EditValue == null)
				return;

			isRemainingEdited = true;
			string[] strArry = spnRemainingAmount.Text.Split('(');

			if (Math.Abs(Convert.ToDouble(strArry[0])) < 0.0001)
			{
				spnRemainingAmount.Text = spnRemainingAmount.Text + "  (=)";
				spnRemainingAmount.BackColor = Color.LawnGreen;
			}
			else if (Convert.ToDouble(strArry[0]) > 0)
			{
				spnRemainingAmount.Text = spnRemainingAmount.Text + "  (عليـه)";
				spnRemainingAmount.BackColor = Color.Red;
			}
			else if (Convert.ToDouble(strArry[0]) < 0)
			{
				spnRemainingAmount.Text = spnRemainingAmount.Text + "  (لـــه)";
				spnRemainingAmount.BackColor = Color.Yellow;
			}

			isRemainingEdited = false;
		}

		#endregion

		#region Button Events

		private void btnAddService_Click(object sender, EventArgs e)
		{
			if (ServiceID == null && (DB_ServiceType)ServiceType != DB_ServiceType.LabServices && ServiceID == null &&
				(DB_ServiceType)ServiceType != DB_ServiceType.ParentAccommodationService)
			{
				XtraMessageBox.Show("يجب إختيار الخدمة الطبية", "بيانات خاطئة",
					MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				return;
			}

			if (ServiceDate == null)
			{
				XtraMessageBox.Show("يجب إختيار تاريخ الخدمة", "بيانات خاطئة",
					MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				return;
			}

			if (DoctorID == null)
			{
				XtraMessageBox.Show("يجب إختيار الطبيب", "بيانات خاطئة",
					MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				return;
			}

			if(chkStationPoint.IsOn)
				if(StationPointID == null || StationPointStageID == null)
				{
					XtraMessageBox.Show("يجب إختيار العيــــادة والمرحلــــة", "بيانات خاطئة",
						MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
					return;
				}

			if (Grid_InvoiceDetails == null)
				Grid_InvoiceDetails = new List<InvoiceDetail>();

			InvoiceDetail serviceDetailObject = null;
			DB_ServiceType serviceType = (DB_ServiceType)ServiceType;
			switch (serviceType)
			{
				case DB_ServiceType.LabServices:
					Service_cu parentService =
						Service_cu.ItemsList.Find(
							item => Convert.ToInt32(item.ServiceType_P_ID)
								.Equals((int) DB_ServiceType.ParentLabService));
					if (parentService == null)
					{
						XtraMessageBox.Show("Error :: please refer to customer service, Parent Lab Service Not Found");
						return;
					}

					serviceDetailObject = MerkDBBusinessLogicEngine.CreateNew_InvoiceDetail(null, parentService.ID,
						ServicePrice, useCustomServicePrice, 1, ServiceDate, DoctorID, IsInsuranceAppliedToService,
						StationPointID, StationPointStageID,
						InsurancePercentage,
						IsSurchargeAppliedToService, false,
						ServiceDescription);

					foreach (Service_cu labService in (List<Service_cu>) LabServices)
						MerkDBBusinessLogicEngine.CreateNew_InvoiceDetail(serviceDetailObject, labService.ID,
							labService.DefaultPrice, useCustomServicePrice,
							1, ServiceDate, DoctorID, StationPointID, StationPointStageID, IsInsuranceAppliedToService,
							InsurancePercentage,
							IsSurchargeAppliedToService, false,
							ServiceDescription);

					break;
				case DB_ServiceType.ParentAccommodationService:
					parentService =
						Service_cu.ItemsList.Find(
							item => Convert.ToInt32(item.ServiceType_P_ID)
								.Equals((int) DB_ServiceType.ParentAccommodationService));
					if (parentService == null)
					{
						XtraMessageBox.Show("Error :: please refer to customer service, Parent Accommodation Service Not Found");
						return;
					}

					serviceDetailObject = MerkDBBusinessLogicEngine.CreateNew_InvoiceDetail(null, parentService.ID,
						ServicePrice, useCustomServicePrice, 1, ServiceDate, DoctorID, IsInsuranceAppliedToService,
						StationPointID, StationPointStageID,
						InsurancePercentage,
						IsSurchargeAppliedToService, false,
						ServiceDescription);

					MerkDBBusinessLogicEngine.CreateNew_InvoiceDetail_Accommodation(serviceDetailObject, ServicePrice,
						ServiceDate,
						InPatientRoomBedID, null, IsSurchargeAppliedToService, false, ServiceDescription,
						IsInsuranceAppliedToService,
						InsurancePercentage);

					break;
				case DB_ServiceType.ExaminationService:
				case DB_ServiceType.InvestigationServices:
					serviceDetailObject = MerkDBBusinessLogicEngine.CreateNew_InvoiceDetail(null, ServiceID,
						ServicePrice, useCustomServicePrice, 1, ServiceDate, DoctorID, StationPointID,
						StationPointStageID, IsInsuranceAppliedToService,
						InsurancePercentage,
						IsSurchargeAppliedToService, false,
						ServiceDescription);
					break;
				case DB_ServiceType.SurgeryService:
					serviceDetailObject = MerkDBBusinessLogicEngine.CreateNew_InvoiceDetail(null, ServiceID,
						ServicePrice, useCustomServicePrice, 1, ServiceDate, DoctorID, StationPointID,
						StationPointStageID, IsInsuranceAppliedToService,
						InsurancePercentage,
						IsSurchargeAppliedToService, false,
						ServiceDescription);
					break;
			}

			if (serviceDetailObject != null && Grid_InvoiceDetails.Exists(
				item => Convert.ToInt32(item.Service_CU_ID).Equals(Convert.ToInt32(serviceDetailObject.Service_CU_ID))))
			{
				DialogResult result = XtraMessageBox.Show("هذه الخدمة تمت إضافتها من قبل." + "\r\n" + "هل تريد إضافة الخدمة؟", "تنبيه",
					MessageBoxButtons.YesNo, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
				switch (result)
				{
					case DialogResult.Yes:
						Grid_InvoiceDetails.Add(serviceDetailObject);
						break;
				}
			}
			else
				Grid_InvoiceDetails.Add(serviceDetailObject);

			FinancialBusinessLogicLibrary.Recalculate_OutPatient_InvoiceDetails(Grid_InvoiceDetails, IsInsuranceAppliedToInvoice, false,
				InsuranceCarrierID, InsuranceLevelID, InsurancePercentage, InsurancePatientMaxAmount);

			AccummulatedServicesPrice = FinancialBusinessLogicLibrary.GetAccumulativeInvoiceDetails(Grid_InvoiceDetails,
				PriceType.TotalServicePriceBeforeSurcharges);

			spnAmountPaid.EditValue = spnAccummulatedServicesCost_PatientShare.EditValue =
				FinancialBusinessLogicLibrary.GetAccummulativePatientShare(Grid_InvoiceDetails);

			grdServices.DataSource = Grid_InvoiceDetails;
			grdServices.RefreshDataSource();

		}

		private void btnEditService_Click(object sender, EventArgs e)
		{

		}

		private void btnDeleteService_Click(object sender, EventArgs e)
		{
			if (Grid_InvoiceDetails == null || Grid_InvoiceDetails.Count == 0)
				return;

			if (SelectedInvoiceDetail == null)
			{
				XtraMessageBox.Show("يجــب إختيـــار الخـدمـــــة الطبيـــة قبــل الحــــذف", "تنبيــــــــه",
									MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				return;
			}

			DialogResult result = XtraMessageBox.Show("هـــل تـريــــد حــــذف الخـدمــــــة الطبيـــــة ؟",
													  "تنبيــــــــه", MessageBoxButtons.YesNo,
													  MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
			switch (result)
			{
				case DialogResult.Yes:
					Grid_InvoiceDetails.Remove(SelectedInvoiceDetail);
					break;
			}

			FinancialBusinessLogicLibrary.Recalculate_OutPatient_InvoiceDetails(Grid_InvoiceDetails, IsInsuranceAppliedToInvoice, false,
																				InsuranceCarrierID, InsuranceLevelID, InsurancePercentage, InsurancePatientMaxAmount);

			AccummulatedServicesPrice = FinancialBusinessLogicLibrary.GetAccumulativeInvoiceDetails(Grid_InvoiceDetails,
																									PriceType.TotalServicePriceBeforeSurcharges);

			spnAmountPaid.EditValue = spnAccummulatedServicesCost_PatientShare.EditValue =
				FinancialBusinessLogicLibrary.GetAccummulativePatientShare(Grid_InvoiceDetails);

			grdServices.DataSource = Grid_InvoiceDetails;
			grdServices.RefreshDataSource();
		}

		#endregion

		#region Grid Events

		private void gridView6_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			GridView view = grdServices.MainView as GridView;
			if (view == null)
				return;

			GridColumn col_IsInsuranceInvoice = view.Columns["IsInsuranceApplied"];

			if (col_IsInsuranceInvoice == e.Column)
				FinancialBusinessLogicLibrary.Recalculate_OutPatient_InvoiceDetails(Grid_InvoiceDetails, IsInsuranceAppliedToInvoice,
						false, InsuranceCarrierID, InsuranceLevelID, InsurancePercentage, InsurancePatientMaxAmount);

			GridColumn col_TotalServicePrice_BeforeAddsOn_InvoiceItem = view.Columns["TotalServicePrice_BeforeAddsOn_InvoiceItem"];
			if (col_TotalServicePrice_BeforeAddsOn_InvoiceItem == e.Column)
			{
				InvoiceDetail invoiceDetail = (InvoiceDetail)view.GetRow(e.RowHandle);
				invoiceDetail.IsCustomPriceUsed = true;
				FinancialBusinessLogicLibrary.UpdateInvoiceDetailPrice(invoiceDetail, InsurancePercentage);
				FinancialBusinessLogicLibrary.Recalculate_OutPatient_InvoiceDetails(Grid_InvoiceDetails, IsInsuranceAppliedToInvoice,
						false, InsuranceCarrierID, InsuranceLevelID, InsurancePercentage, InsurancePatientMaxAmount);
			}

			AccummulatedServicesPrice = FinancialBusinessLogicLibrary.GetAccumulativeInvoiceDetails(Grid_InvoiceDetails,
				PriceType.TotalServicePriceBeforeSurcharges);

			spnAccummulatedServicesCost_PatientShare.EditValue =
				FinancialBusinessLogicLibrary.GetAccummulativePatientShare(Grid_InvoiceDetails);

			grdServices.DataSource = Grid_InvoiceDetails;
			grdServices.RefreshDataSource();
		}

		private void grv_Services_GotFocus(object sender, EventArgs e)
		{

		}

		private void grv_Services_MouseUp(object sender, MouseEventArgs e)
		{
			SelectedInvoiceDetail = CommonViewsActions.GetSelectedRowObject<InvoiceDetail>(grv_Services);
		}

		#endregion

		#endregion

	}
}
