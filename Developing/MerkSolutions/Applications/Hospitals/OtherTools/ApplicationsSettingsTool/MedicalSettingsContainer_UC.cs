using CommonUserControls.SettingsViewers.DiagnosisCategoriesViewers;
using CommonUserControls.SettingsViewers.DiagnosisCategory_Diagnosis_Viewers;
using CommonUserControls.SettingsViewers.DiagnosisViewers;
using CommonUserControls.SettingsViewers.DoctorCategoriesViewers;
using CommonUserControls.SettingsViewers.DoctorViewers;
using CommonUserControls.SettingsViewers.DoseViewers;
using CommonUserControls.SettingsViewers.MedicationCategoriesViewers;
using CommonUserControls.SettingsViewers.MedicationViewers;
using CommonUserControls.SettingsViewers.Medication_Dose_Viewers;
using CommonUserControls.SettingsViewers.OrganizationMachineViewers;
using CommonUserControls.SettingsViewers.ServiceCategoryStationPointViewers;
using CommonUserControls.SettingsViewers.ServiceCategoryViewers;
using CommonUserControls.SettingsViewers.ServicePrice_Viewers;
using CommonUserControls.SettingsViewers.ServiceStationPointViewers;
using CommonUserControls.SettingsViewers.ServiceTypeStationPointViewers;
using CommonUserControls.SettingsViewers.ServiceViewers;
using CommonUserControls.SettingsViewers.StationPointStageViewers;
using CommonUserControls.SettingsViewers.StationPointViewers;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;

namespace ApplicationsSettingsTool
{
	public partial class MedicalSettingsContainer_UC : DevExpress.XtraEditors.XtraUserControl
	{
		private ServiceCategory_EditorViewer _serviceCategoryEditorViewer;
		private ServiceCategory_SearchViewer _serviceCategorySearchViewer;

		private Service_EditorViewer _serviceEditorViewer;
		private Service_SearchViewer _serviceSearchViewer;

		private ServicePrice_EditorViewer _servicePriceEditorViewer;
		private ServicePrice_SearchViewer _servicePriceSearchViewer;

		private StationPoint_EditorViewer _stationPointEditorViewer;
		private StationPoint_SearchViewer _stationPointSearchViewer;

		private StationPointStage_EditorViewer _stationPointStageEditorViewer;
		private StationPointStage_SearchViewer _stationPointStageSearchViewer;

		private ServiceStationPoint_EditorViewer _serviceStationPointEditorViewer;
		private ServiceStationPoint_SearchViewer _serviceStationPointSearchViewer;

		private ServiceCategoryStationPoint_EditorViewer _serviceCategoryStationPointEditorViewer;
		private ServiceCategoryStationPoint_SearchViewer _serviceCategoryStationPointSearchViewer;

		private ServiceTypeStationPoint_EditorViewer _serviceTypeStationPointEditorViewer;
		private ServiceTypeStationPoint_SearchViewer _serviceTypeStationPointSearchViewer;

		private Doctor_EditorViewer _doctorEditorViewer;
		private Doctor_SearchViewer _doctorSearchViewer;

		private DoctorCategory_EditorViewer _doctorCategoryEditorViewer;
		private DoctorCategory_SearchViewer _doctorCategorySearchViewer;

		private MedicationCategory_EditorViewer _medicationCategoryEditorViewer;
		private MedicationCategory_SearchViewer _medicationCategorySearchViewer;

		private Medication_EditorViewer _medicationEditorViewer;
		private Medication_SearchViewer _medicationSearchViewer;

		private OrganizationMachine_EditorViewer _organizationMachineEditorViewer;
		private OrganizationMachine_SearchViewer _organizationMachineSearchViewer;

		private Dose_EditorViewer _doseEditorViewer;
		private Dose_SearchViewer _doseSearchViewer;

		private Medication_Dose_EditorViewer _medicationDoseEditorViewer;
		private Medication_Dose_SearchViewer _medicationDoseSearchViewer;

		private DiagnosisCategories_EditorViewer _diagnosisCategoriesEditorViewer;
		private DiagnosisCategories_SearchViewer _diagnosisCategoriesSearchViewer;

		private Diagnosis_EditorViewer _diagnosisEditorViewer;
		private Diagnosis_SearchViewer _diagnosisSearchViewer;

		private DiagnosisCategory_Diagnosis_EditorViewer _diagnosisCategoryDiagnosisEditorViewer;
		private DiagnosisCategory_Diagnosis_SearchViewer _diagnosisCategoryDiagnosisSearchViewer;

		public MedicalSettingsContainer_UC()
		{
			InitializeComponent();
		}

		private void btnServiceCategory_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<ServiceCategory_cu>.ShowControl(ref _serviceCategoryEditorViewer, ref _serviceCategorySearchViewer,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.ServiceCategory_Viewer, DB_CommonTransactionType.CreateNew,
				"تصنيفـــات الخـدمــات",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnService_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<Service_cu>.ShowControl(ref _serviceEditorViewer, ref _serviceSearchViewer,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.Service_Viewer, DB_CommonTransactionType.CreateNew,
				"الخـدمــات الطبيــة",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnServicePrice_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<ServicePrice_cu>.ShowControl(ref _servicePriceEditorViewer, ref _servicePriceSearchViewer,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.ServicePrice_Viewer, DB_CommonTransactionType.CreateNew,
				"أسعـــــار الخـدمـــــات",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnStationPoint_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<StationPoint_cu>.ShowControl(ref _stationPointEditorViewer, ref _stationPointSearchViewer,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.StationPoint_Viewer, DB_CommonTransactionType.CreateNew,
				"العيـــــادات",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnStationPointStage_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<StationPointStage_cu>.ShowControl(ref _stationPointStageEditorViewer, ref _stationPointStageSearchViewer,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.StationPointStage_Viewer, DB_CommonTransactionType.CreateNew,
				"مـراحــــل العيـــــادات",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnServiceType_StationPoint_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<ServiceType_StationPoint_cu>.ShowControl(ref _serviceTypeStationPointEditorViewer,
				ref _serviceTypeStationPointSearchViewer,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.ServiceType_StationPoint_Viewer, DB_CommonTransactionType.CreateNew,
				"ربــط نـوع الخـدمــات بالعيـــادات",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnServiceCategory_StationPoint_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<ServiceCategory_StationPoint_cu>.ShowControl(ref _serviceCategoryStationPointEditorViewer,
				ref _serviceCategoryStationPointSearchViewer,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.ServiceCategory_StationPoint_Viewer, DB_CommonTransactionType.CreateNew,
				"ربــط تصنيفــــات الخـدمــات بالعيـــادات",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnService_StationPoint_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<Service_StationPoint_cu>.ShowControl(ref _serviceStationPointEditorViewer,
				ref _serviceStationPointSearchViewer,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.Service_StationPoint_Viewer, DB_CommonTransactionType.CreateNew,
				"ربــط الخـدمــات بالعيـــادات",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnDoctorCategories_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<DoctorCategory_cu>.ShowControl(ref _doctorCategoryEditorViewer,
				ref _doctorCategorySearchViewer,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.DoctorCategory_Viewer, DB_CommonTransactionType.CreateNew,
				"بيـانــــات تصنيفـــــــات الأطبــــــاء",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnDoctors_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<Doctor_cu>.ShowControl(ref _doctorEditorViewer,
				ref _doctorSearchViewer,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.Doctor_Viewer, DB_CommonTransactionType.CreateNew,
				".... بيـانــــات الأطبــــاء ....",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnMedicationCategory_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<MedicationCategory_cu>.ShowControl(ref _medicationCategoryEditorViewer,
				ref _medicationCategorySearchViewer,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.MedicationCategory_Viewer, DB_CommonTransactionType.CreateNew,
				".... بيـانــــات تصنيفـــــــات الأدويـــــــــة ....",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnMedication_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<Medication_cu>.ShowControl(ref _medicationEditorViewer,
				ref _medicationSearchViewer,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.Medication_Viewer, DB_CommonTransactionType.CreateNew,
				".... بيـانــــات الأدويـــــــــة ....",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnDose_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<Dose_cu>.ShowControl(ref _doseEditorViewer,
				ref _doseSearchViewer,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.Dose_Viewer, DB_CommonTransactionType.CreateNew,
				".... بيـانــــات الجـرعـــــات ....",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnMedicationDose_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<Medication_Dose_cu>.ShowControl(ref _medicationDoseEditorViewer,
				ref _medicationDoseSearchViewer,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.Medication_Dose_Viewer, DB_CommonTransactionType.CreateNew,
				".... ربـــط الأدويــــة بالجـرعـــــات ....",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnOrganizationMachine_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<OrganizationMachine_cu>.ShowControl(ref _organizationMachineEditorViewer,
				ref _organizationMachineSearchViewer,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.OrganizationMachine_viewer, DB_CommonTransactionType.CreateNew,
				".... أجهـــــزة العيــــادات ....",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnDiagnosisCategories_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<DiagnosisCategory_cu>.ShowControl(ref _diagnosisCategoriesEditorViewer,
				ref _diagnosisCategoriesSearchViewer,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.DiagnosisCategory_Viewer, DB_CommonTransactionType.CreateNew,
				".... تصنيفــــات التشخيصــــات ....",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnDiagnosis_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<Diagnosis_cu>.ShowControl(ref _diagnosisEditorViewer,
				ref _diagnosisSearchViewer,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.Diagnosis_Viewer, DB_CommonTransactionType.CreateNew,
				".... التشخيصــــات ....",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnDiagnosisCategoiresDiagnosis_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<DiagnosisCategory_Diagnosis_cu>.ShowControl(ref _diagnosisCategoryDiagnosisEditorViewer,
				ref _diagnosisCategoryDiagnosisSearchViewer,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.DiagnosisCategory_Diagnosis_Viewer, 
				DB_CommonTransactionType.CreateNew,
				".... ربــــط التصنيفـــــات بالتشخصيـــــات ....",
				AbstractViewerType.SearchViewer,
				true);
		}

	}
}
