using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCFactories
{
	public enum ViewerName
	{
		None = 0,
		ChartOfAccountViewer = 1,
		CashBoxViewer = 2,
		EmployeeViewer = 3,
		FloorViewer = 4,
		InPatientRoomViewer = 5,
		InPatientRoomBedViewer = 6,
		InPatientRoomClassificationViewer = 7,
		PatientViewer = 8,
		PatientInvoice = 9,
		PatientInvoiceCreation = 10,
		InvoicePayment_Viewer = 11,
		InsurancePolicy_Viewer = 12,
		User_Viewer = 13,
		UserGroup_Viewer = 14,
		User_UserGroup_Viewer = 15,
		UserGroup_Application_Viewer = 16,
		RoleRegistration_Viewer = 17,
		User_Application_Viewer = 18,
		ServiceCategory_Viewer = 19,
		Service_Viewer = 20,
		ServicePrice_Viewer = 21,
		StationPoint_Viewer = 22,
		StationPointStage_Viewer = 23,
		Service_StationPoint_Viewer = 24,
		ServiceCategory_StationPoint_Viewer = 25,
		ServiceType_StationPoint_Viewer = 26,
		InventoryHousing_Viewer = 27,
		Location_Viewer = 28,
		InventoryItemCategory_Viewer = 29,
		InventoryItemBrand_Viewer = 30,
		InventoryItemGroup_Viewer = 31,
		UnitMeasurment_Viewer = 32,
		UnitMeasurmentTreeLink_Viewer = 33,
		InventoryItem_Viewer = 34,
		InventoryItem_UnitMeasurment_Viewer = 35,
		FinanceInvoiceCreation_Viewer = 36,
		Customer_Viewer = 37,
		Supplier_Viewer = 38,
		InventoryItemPrice_Viewer = 39,
		InventoryItem_InventoryHousing_Viewer = 40,
		Doctor_Viewer = 41,
		CustomerFinanceInvoicesReport_Viewer = 42,
		GeneralChartOfAccountType_Viewer = 43,
		CashBoxTransactionType_GeneralChartOfAccountType_Viewer = 44,
		InvoicePaymentBrief_Report_Viewer = 45,
		TotalServiceAndDoctorRevenues_Report_Viewer = 46,
		Bank_Viewer = 47,
		BankAccount_Viewer = 48,
		PersonType_ChartOfAccount_Viewer = 49,
		Person_ChartOfAccount_Viewer = 50,
		MedicationCategory_Viewer = 51,
		DoctorCategory_Viewer = 52,
		Medication_Viewer = 53,
		OrganizationMachine_viewer = 54,
		Dose_Viewer = 55,
		Medication_Dose_Viewer = 56,
		PatientDepositBalance_Report_Viewer = 57,
		DiagnosisCategory_Viewer = 58,
		Diagnosis_Viewer = 59,
		DiagnosisCategory_Diagnosis_Viewer = 60,
		InventoryItem_Area_Viewer = 61,
		InventoryItemGroup_InventoryItem_Viewer,
		RawMaterial_viewer,
		Color_Viewer,
		RawMaterialTransactions_viewer,
		GetRawMaterialCostPrices_Viewer,
		GetInventoryItemAreaParts_Viewer,
		InventoryItem_Printing_Viewer,
		InventoryItem_RawMaterial_Viewer,
		GetInventoryItemCostsDetails_Viewer
	}

	public enum MVCType
	{
		None = 0,
		Single = 1,
		Collection = 2
	}

	public enum AbstractViewerType
	{
		None = 0,
		EditorViewer = 1,
		SearchViewer = 2
	}

	public enum EditorContainerType
	{
		None = 0,
		Regular = 1,
		Settings = 2
	}

	public class MVCControllerFactory
	{
		public static IMVCControllerFactory GetControllerFactory<TEntity>(ViewerName viewerName, IViewer viewer)
			where TEntity : DBCommon, IDBCommon, new()
		{
			return new MVCController<TEntity>(viewer, viewerName);
		}
	}
}
