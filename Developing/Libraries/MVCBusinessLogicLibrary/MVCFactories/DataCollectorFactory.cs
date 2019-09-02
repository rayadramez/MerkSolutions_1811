using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.MVCDataCollectors;

namespace MVCBusinessLogicLibrary.MVCFactories
{
	public class DataCollectorFactory
	{
		public static IMVCDataCollector<TEntity> GetDataCollectorFactory<TEntity>(ViewerName viewerName)
			where TEntity : DBCommon, IDBCommon, new()
		{
			switch (viewerName)
			{
				case ViewerName.FloorViewer:
					return new Floor_DataCollector<TEntity>();
				case ViewerName.InPatientRoomViewer:
					return new InPatientRoomDataCollector<TEntity>();
				case ViewerName.InPatientRoomBedViewer:
					return new InPatientRoomBedDataCollector<TEntity>();
				case ViewerName.InPatientRoomClassificationViewer:
					return new InPatientRoomClassificationDataCollector<TEntity>();
				case ViewerName.PatientViewer:
					return new PatientDataCollector<TEntity>();
				case ViewerName.PatientInvoiceCreation:
					return new MedicalAdmissionInvoiceCreation_DataCollector<TEntity>();
				case ViewerName.InvoicePayment_Viewer:
					return new InvoicePaymentDataCollector<TEntity>();
				case ViewerName.InsurancePolicy_Viewer:
					return new InsurancePolicyDataCollector<TEntity>();
				case ViewerName.User_Viewer:
					return new User_DataCollector<TEntity>();
				case ViewerName.UserGroup_Viewer:
					return new UserGroup_DataCollector<TEntity>();
				case ViewerName.User_UserGroup_Viewer:
					return new User_UserGroup_DataCollector<TEntity>();
				case ViewerName.UserGroup_Application_Viewer:
					return new UserGroup_Application_DataCollector<TEntity>();
				case ViewerName.User_Application_Viewer:
					return new User_Application_DataCollector<TEntity>();
				case ViewerName.RoleRegistration_Viewer:
					return new RoleRegistration_DataCollector<TEntity>();
				case ViewerName.ServiceCategory_Viewer:
					return new ServiceCategory_DataCollector<TEntity>();
				case ViewerName.Service_Viewer:
					return new Service_DataCollector<TEntity>();
				case ViewerName.ServicePrice_Viewer:
					return new ServicePrice_DataCollector<TEntity>();
				case ViewerName.StationPoint_Viewer:
					return new StationPoint_DataCollector<TEntity>();
				case ViewerName.StationPointStage_Viewer:
					return new StationPointStage_DataCollector<TEntity>();
				case ViewerName.ServiceType_StationPoint_Viewer:
					return new ServiceType_StationPoint_DataCollector<TEntity>();
				case ViewerName.ServiceCategory_StationPoint_Viewer:
					return new ServiceCategory_StationPoint_DataCollector<TEntity>();
				case ViewerName.Service_StationPoint_Viewer:
					return new Service_StationPointData_Collector<TEntity>();
				case ViewerName.InventoryHousing_Viewer:
					return new InventoryHousing_DataCollector<TEntity>();
				case ViewerName.Location_Viewer:
					return new Location_DataCollector<TEntity>();
				case ViewerName.InventoryItemCategory_Viewer:
					return new InventoryItemCategory_DataCollector<TEntity>();
				case ViewerName.InventoryItemBrand_Viewer:
					return new InventoryItemBrand_DataCollector<TEntity>();
				case ViewerName.InventoryItemGroup_Viewer:
					return new InventoryItemGroup_DataCollector<TEntity>();
				case ViewerName.UnitMeasurment_Viewer:
					return new UnitMeasurment_DataCollector<TEntity>();
				case ViewerName.UnitMeasurmentTreeLink_Viewer:
					return new UnitMeasurmentTreeLink_DataCollector<TEntity>();
				case ViewerName.InventoryItem_Viewer:
					return new InventoryItem_DataCollector<TEntity>();
				case ViewerName.InventoryItem_UnitMeasurment_Viewer:
					return new InventoryItem_UnitMeasurment_DataCollector<TEntity>();
				case ViewerName.Customer_Viewer:
					return new Customer_DataCollector<TEntity>();
				case ViewerName.Supplier_Viewer:
					return new Supplier_DataCollector<TEntity>();
				case ViewerName.InventoryItemPrice_Viewer:
					return new InventoryItemPrice_DataCollector<TEntity>();
				case ViewerName.InventoryItem_InventoryHousing_Viewer:
					return new InventoryItem_InventoryHousing_DataCollector<TEntity>();
				case ViewerName.Doctor_Viewer:
					return new Doctor_DataCollector<TEntity>();
				case ViewerName.FinanceInvoiceCreation_Viewer:
					return new FinanceInvoiceCreationDataCollector<TEntity>();
				case ViewerName.CustomerFinanceInvoicesReport_Viewer:
					return new CustomerFinanceInvoice_Report_DataCollector<TEntity>();
				case ViewerName.ChartOfAccountViewer:
					return new ChartOfAccount_DataCollector<TEntity>();
				case ViewerName.CashBoxViewer:
					return new CashBox_DataCollector<TEntity>();
				case ViewerName.GeneralChartOfAccountType_Viewer:
					return new GeneralChartOfAccountType_DataCollector<TEntity>();
				case ViewerName.CashBoxTransactionType_GeneralChartOfAccountType_Viewer:
					return new CashBoxTransactionType_GeneralChartOfAccountType_DataCollector<TEntity>();
				case ViewerName.InvoicePaymentBrief_Report_Viewer:
					return new InvoicePaymentBrief_Report_DataCollector<TEntity>();
				case ViewerName.TotalServiceAndDoctorRevenues_Report_Viewer:
					return new TotalServiceAndDoctorRevenue_Report_DataCollector<TEntity>();
				case ViewerName.PatientDepositBalance_Report_Viewer:
					return new PatientDepositBalance_Report_DataCollector<TEntity>();
				case ViewerName.Bank_Viewer:
					return new Bank_DataCollector<TEntity>();
				case ViewerName.BankAccount_Viewer:
					return new BankAccount_DataCollector<TEntity>();
				case ViewerName.PersonType_ChartOfAccount_Viewer:
					return new PersonType_ChartOfAccount_DataCollector<TEntity>();
				case ViewerName.Person_ChartOfAccount_Viewer:
					return new Person_ChartOfAccount_DataCollector<TEntity>();
				case ViewerName.MedicationCategory_Viewer:
					return new MedicationCategory_DataCollector<TEntity>();
				case ViewerName.DoctorCategory_Viewer:
					return new DoctorCategory_DataCollector<TEntity>();
				case ViewerName.Medication_Viewer:
					return new Medication_DataCollector<TEntity>();
				case ViewerName.OrganizationMachine_viewer:
					return new OrganizationMachine_DataCollector<TEntity>();
				case ViewerName.Dose_Viewer:
					return new Dose_DataCollector<TEntity>();
				case ViewerName.Medication_Dose_Viewer:
					return new Medication_Dose_DataCollector<TEntity>();
				case ViewerName.DiagnosisCategory_Viewer:
					return new DiagnosisCategory_DataCollector<TEntity>();
				case ViewerName.Diagnosis_Viewer:
					return new Diagnosis_DataCollector<TEntity>();
				case ViewerName.DiagnosisCategory_Diagnosis_Viewer:
					return new DiagnosisCategory_Diagnosis_DataCollector<TEntity>();
				case ViewerName.InventoryItem_Area_Viewer:
					return new InventoryItem_Area_DataCollector<TEntity>();
				case ViewerName.InventoryItemGroup_InventoryItem_Viewer:
					return new InventoryItemGroup_InventoryItem_DataCollector<TEntity>();
				case ViewerName.RawMaterial_viewer:
					return new RawMaterial_DataCollector<TEntity>();
				case ViewerName.Color_Viewer:
					return new Color_DataCollector<TEntity>();
				case ViewerName.RawMaterialTransactions_viewer:
					return new RawMaterialTransaction_DataCollector<TEntity>();
				case ViewerName.GetRawMaterialCostPrices_Viewer:
					return new GetRawMaterialCostPrices_Report_DataCollecor<TEntity>();
				case ViewerName.GetInventoryItemAreaParts_Viewer:
					return new GetInventoryItemAreaParts_DataCollector<TEntity>();
				case ViewerName.InventoryItem_Printing_Viewer:
					return new InventoryItem_Printing_DataCollector<TEntity>();
				case ViewerName.InventoryItem_RawMaterial_Viewer:
					return new InventoryItem_RawMaterial_DataCollector<TEntity>();
				case ViewerName.GetInventoryItemCostsDetails_Viewer:
					return new GetInventoryItemCostsDetails_Report_DataCollector<TEntity>();

			}

			return null;
		}
	}
}
