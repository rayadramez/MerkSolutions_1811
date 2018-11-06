namespace MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary
{
	#region DBCommon

	public enum DB_PupillaryRAPDGradingScale
	{
		None = 0,
		Grade1 = 1,
		Grade2 = 2,
		Grade3 = 3,
		Grade4 = 4,
		Grade5 = 5,
	}

	public enum DB_PupillarySize
	{
		None = 0,
		Miosis = 1,
		Medium = 2,
		Mydrisasis = 3
	}

	public enum DB_PupillaryShape
	{
		None = 0,
		Rounded = 1,
		Oval = 2,
		Sector = 3,
		Irruglar = 4,
		Peaked = 5
	}

	public enum DB_PEMRSavingMode
	{
		None = 0,
		SaveImmediately = 1,
		PostponeSaving = 2
	}

	public enum DB_SegmentSignType
	{
		None = 0,
		Anterior = 1,
		Posterior = 2,
		Adnexa = 3,
		EOM = 4
	}

	public enum DB_UncorrectedDistanceVisualAcuityUnit
	{
		None = 0,
		Snellen = 1,
		Decimal = 2,
		Logmar = 3,
		American = 4
	}

	public enum DB_VisionRefractionReadingType
	{
		None = 0,
		AutoRef = 1,
		AutoRefAfterCyclo = 2,
		OldGlasses = 3,
		OldGlassesNear = 4,
		SubjectiveRefraction = 5
	}

	public enum DB_EyeType_p
	{
		All = 0,
		OS = 1,
		OD = 2,
		OU = 3,
		
	}

	public enum DB_DiagnosisType
	{
		None = 0,
		Provisional = 1,
		Differential = 2,
		Final = 3
	}

	public enum DB_DiabetesType
	{
		Type1 = 1,
		Type2 = 2
	}

	public enum DB_DiabetedMedication
	{
		Both = 1,
		Tablets = 2,
		Insulin = 3
	}

	public enum DB_TimeDuration
	{
		None = 0,
		Hourly = 1,
		Daily = 2,
		Weekly = 3,
		Monthly = 4,
		Yearly = 5
	}

	public enum DB_WeightUnit
	{
		None = 0,
		KG = 1,
		LBs = 2
	}

	public enum DB_HeightUnit
	{
		None = 0,
		CM = 1,
		Inch = 2,
		Feet = 3,
	}

	public enum DB_TemperatureUnit
	{
		None = 0,
		Celesius = 1,
		Fahrenheit = 2
	}

	public enum DB_ServerDirectory
	{
		ScanDirectory = 1,
		OrganizationTemplates = 2,
	}

	public enum DB_ImageType
	{
		None = 0,
		IDCard = 1,
		Passport = 2,
		Investigation_Report = 3,
		Lab_Report = 4,
		Other = 5,
		PersonalImage = 6,
		Surgery_Report
	}

	public enum DB_PEMR_ElementType
	{
		MedicalHistory = 1,
		Examination = 2,
		Diagnosis = 3,
		Referral = 4,
		FollowUp = 5,
		Notes = 6,
		ScannedFiles = 7,
		VisitTiming_MedicalHistory = 8,
		VisitTiming_SocialHistory = 9,
		VisitTiming_FamilyHistory = 10,
		VisitTiming_VitalSign = 11,
		VisitTiming_Symptoms = 12,
		VisitTiming_Diagnosis = 13,
		VisitTiming_Medications = 14,
		VisitTiming_Investigations = 15,
		VisitTiming_Labs = 16,
		VisitTiming_Surgeries = 17,
		VisitTiming_TreatmentPlan = 18,
		VisitTIming_Attachment = 19,
		VisitTiming_MainDiagnosis = 20,
		VisitTiming_MainInvestigation = 21,
		VisitTiming_MainLab = 22,
		VisitTiming_MainSurgery = 23,
		SegmentSign_cu,
		SegmentSignCategory_cu,
		VisitTiming_AnteriorSegmentSign,
		VisitTiming_MainSymptoms,
		VisitTiming_AdnexaSegmentSign,
		VisitTiming_MainAdnexaSegmentSign,
		VisitTiming_PosteriorSegmentSign,
		VisitTiming_EOMSign
	}

	public enum DB_PersonType
	{
		None = 0,
		Customer = 1,
		Supplier = 2,
		User = 3,
		Patient = 4,
		Doctor = 5
	}

	public enum DB_PersonChartOtAccountType
	{
		None = 0,
		DebitAccountingCode = 1,
		ProfessionalFeesAccountingCode = 2,
		TaxAccountingCode = 3,
		CurrentAccountingCode = 4,
		CreditAccountingCode = 5
	}

	public enum DB_Application
	{
		None = 0,
		ClinicReception = 1,
		AdmissionReception = 2,
		AllReception = 3,
		PEMR = 4,
		InvoiceManager = 5,
		Settings = 6,
		QueueManager = 7
	}

	public enum DB_GeneralChartOfAccountType
	{
		None = 0,
		ExpensesWithdraw = 1,
		ReverseExpensesWithdraw = 2,
		RevenueDeposit = 3,
		ReverseRevenueDeposit = 4
	}

	public enum DB_ChartOfAccountCodeMargin
	{
		None = 0,
		FirstMargin = 1,
		SecondMargin = 2,
		ThirdMargin = 3,
		FourthMargin = 4,
		FifthMargin = 5
	}

	public enum DB_InventoryItemTransactionType
	{
		None = 0,
		StartingInventoryBalance = 1,
		EndingInventoryBalance = 2,
		InventorySettlement = 3,
		InputTransaction = 4,
		OutputTransaction = 5,
	}

	public enum DB_CommonTransactionType
	{
		None = 0,
		CreateNew = 1,
		SaveNew = 2,
		UpdateExisting = 3,
		DeleteExisting = 4,
		Load = 5,
		SearchReport = 6
	}

	public enum DB_InPatientAddmissionPricingType
	{
		MainPatientPricing = 1,
		CompanionPricing = 2
	}

	public enum DB_InvoiceType
	{
		None = 0,
		InPatientPrivate = 1,
		OutPatientPrivate = 2,
		InPatientNotPrivate = 3,
		OutPatientNotPrivate = 4,
		PurchasingInvoice = 5,
		ReturningPurchasingInvoice = 6,
		SellingInvoice = 7,
		ReturningSellingInvoice = 8
	}

	public enum DB_InvoicePaymentType
	{
		None = 0,
		CashInvoice = 1,
		PostpenedInvoice = 2
	}

	public enum DB_TableIdentity
	{
		None = 0,
		ChartOfAccountCodeMargin_p = 1,
		Doctor_cu = 2,
		DoctorCategory_cu = 3,
		AccountingJournalEntryTransaction = 4,
		AccountingJournalTransaction = 5,
		ActiveSalaryEffect_cu = 6,
		Address_cu = 7,
		Bank_cu = 8,
		BankAccount_cu = 9,
		City_cu = 10,
		Country_cu = 11,
		Employee_cu = 12,
		Floor_cu = 13,
		InPatientRoom_cu = 14,
		InPatientRoom_InPatientAdmissionPricingType_cu = 15,
		InPatientRoomBed_cu = 16,
		InPatientRoomBed_InPatientAdmissionPricingType_cu = 17,
		InPatientRoomClassification_cu = 18,
		InPatientRoomClassification_InPatientAdmissionPricingType_cu = 19,
		InsuranceCarrier_cu = 20,
		InsuranceCarrier_InsuranceLevel_cu = 21,
		InsuranceLevel_cu = 22,
		InvoiceType_Surcharge_cu = 23,
		Patient_cu = 24,
		Region_cu = 25,
		Service_cu = 26,
		Service_StationPoint_cu = 27,
		Service_Surcharge_cu = 28,
		ServiceCategory_cu = 29,
		ServiceCategory_StationPoint_cu = 30,
		ServiceCategory_Surcharge_cu = 31,
		ServicePrice_cu = 32,
		ServiceType_StationPoint_cu = 33,
		ServiceType_Surcharge_cu = 34,
		StationPoint_cu = 35,
		StationPointStage_cu = 36,
		Surcharge_cu = 37,
		User_cu = 38,
		UserGroup_cu = 39,
		User_UserGroup_cu = 40,
		UserGroup_Application_cu = 41,
		User_Application_cu = 42,
		RoleRegistration_cu = 43,
		Location_cu = 44,
		InventoryHousing_cu = 45,
		InventoryItem_cu = 46,
		InventoryItem_UnitMeasurment_cu = 47,
		InventoryItemBrand_cu = 48,
		InventoryItemCategory_cu = 49,
		InventoryItemGroup_InventoryItem_cu = 50,
		InventoryItemPrice_cu = 51,
		InvetoryItemGroup_cu = 52,
		UnitMeasurment_cu = 53,
		UnitMeasurmentTreeLink_cu = 54,
		Supplier_cu = 55,
		Customer_cu = 56,
		FinanceInvoiceShare = 57,
		FinanceInvoice = 58,
		Invoice = 60,
		QueueManager = 61,
		InvoiceShare = 62,
		InvoicePayment = 63,
		InvoiceDetail_Inventory = 64,
		InvoiceDetail_DoctorFees = 65,
		InvoiceDetail_Accommodation = 66,
		InvoiceDetail = 67,
		PriceType_p = 68,
		Application_p = 69,
		InventoryItemTransaction = 70,
		DoctorTaxType_cu = 71,
		DoctorSpecialization_p = 72,
		UnitMeasurment_p = 73,
		DoctorRank_p = 74,
		DoctorProfessionalFeesIssuingType_p = 75,
		ChartOfAccountMargin_p  = 76,
		IdentificationCardType_p = 77,
		InPatientRoomBedStatus_p = 78,
		VisitTiming = 79,
		VisitTiming_SocialHistory = 80,
		InventoryItemTransactionType_p = 81,
		FinanceInvoicePayment = 82,
		FinancialInterval_cu = 83,
		ChartOfAccount_cu = 84,
		CashBox_cu = 85,
		GeneralChartOfAccountType_cu = 86,
		GeneralChartOfAccountType_p = 87,
		CashBoxTransactionType_GeneralChartOfAccountType_cu = 88,
		CashBoxTransactionType_p = 89,
		CashBoxTransactionType_GeneralChartOfAccountType_p = 90,
		ChartOfAccount_GeneralChartOfAccountType_cu = 91,
		CashBoxInOutTransaction = 92,
		PersonType_p = 93,
		PersonChartOtAccountType_p = 94,
		PersonType_ChartOfAccount_cu = 95,
		Person_ChartOfAccount_cu = 96,
		OrganizationMachine_cu = 97,
		VisitTiming_TreatmentPlan = 98,
		PEM_ElementPrintOrder_cu = 99,
		PEMR_Elemet_p = 100,
		MedicationCategory_cu = 101,
		Medication_cu = 102,
		Medication_Dose_cu = 103,
		Dose_cu = 104,
		ServerDirectory_p = 105,
		Patient_Image_cu = 106,
		PatientAttachment_cu = 107,
		VisitTiming_Attachment = 108,
		TimeDuration_p = 109,
		VisitTiming_InvestigationReservation = 110,
		VisitTiming_Medication = 111,
		PatientDepositeBalance = 112,
		VisitTiming_InvestigationResult = 113,
		DiagnosisCategory_cu = 114,
		Diagnosis_cu = 115,
		Doctor_DiagnosisCategory_cu = 116,
		Doctor_Diagnosis_cu = 117,
		DiagnosisCategory_Diagnosis_cu = 118,
		VisitTiming_VitalSign = 119,
		VisitTiming_Diagnosis = 120,
		WeightUnit_p = 121,
		TemperatureUnit_p = 122,
		HeightUnit_p = 123,
		VisitTiming_MainDiagnosis = 124,
		VisitTiming_LabReservation = 125,
		VisitTiming_SurgeryReservation = 126,
		VisitTiming_SurgeryResult = 127,
		VisitTiming_LabResult = 128,
		VisitTiming_MainPosteriorSegmentSign = 129,
		UncorrectedDistanceVisualAcuity_cu = 130,
		NearVisiong_p = 131,
		VisionRefractionReadingType_p = 132,
		UncorrectedDistanceVisualAcuityUnit_p = 133,
		Eye_p = 134,
		DiagnosisType_p = 135,
		SegmentSign_cu = 136,
		SegmentSignCategory_cu = 137,
		SegmentSignType_p = 138,
		VisitTiming_MainAnteriorSegmentSign = 139,
		PEMRSavingMode_p = 140,
		PEMRSavingMode_User_cu = 141,
		VisitTiming_AnteriorSegmentSign = 142,
		VisitTiming_MainSymptoms = 143,
		VisitTiming_Symptoms = 144,
		VisitTiming_VisionRefractionReading = 145,
		VisitTiming_AdnexaSegmentSign = 146,
		VisitTiming_MainAdnexaSegmentSign = 147,
		VisitTiming_PosteriorSegmentSign = 148,
		VisitTiming_Pupillary = 149,
		PupillaryAbnormalities_cu = 150,
		PupillaryRAPDCauses_cu = 151,
		VisitTiming_MainEOMSign = 152,
		VisitTiming_EOMReading = 153,
		VisitTiming_EOMSign
	}

	public enum DB_Station
	{
		None = 0,
		Surgery_Reception = 1,
		Dental_StationPoint = 15,
		CardiovascularClinic = 16
	}

	public enum DB_Organization
	{
		AvvaAbraam = 1,
		Dental_Mostafa = 2,
		Cardiovascular_Clinic = 3,
		Ophthalmology = 4,
	}

	public enum DB_PaymentType
	{
		CashPayment = 1,
		CheckPayment = 2,
		VisaPayment = 3
	}

	public enum DB_ServiceType
	{
		None = 0,
		ExaminationService = 1,
		SurgeryService = 2,
		InvestigationServices = 3,
		LabServices = 4,
		AccommodationServices = 5,
		OtherServices = 6,
		ParentLabService = 7,
		ParentAccommodationService = 8,
		ParentSurgeryService = 9,
		DoctorFeesService = 10,
	}

	public enum DB_SurchargeType
	{
		None = 0,
		AdditionalServices = 1,
		Taxes = 2,
		Insurance = 3,
		MedicalStamp = 4
	}

	public enum DB_DiscountType
	{
		None = 0,
		Amount = 1,
		Percentage = 2
	}

	public enum DB_QueueManagerStatus
	{
		All = 0,
		Serving = 1,
		Paused = 2,
		Cancelled = 3,
		Waiting = 4,
		Served = 5,
		Stopped = 6
	}

	public enum DB_InPatientRoomBedStatus
	{
		None = 0,
		Available = 1,
		Occupied = 2,
		Suspended = 3,
		Cancelled = 4,
		Underconstruction = 5
	}

	public enum DB_PriceType
	{
		None = 0,
		SellingPrice = 1,
		PurchasingPrice = 2,
	}

	#endregion

	#region Other Enums

	public enum GenderType
	{
		Male = 1,
		Female = 0,
	}

	public enum GeneralInvoiceType
	{
		None = 0,
		InPatientInvoice = 1,
		OutPatientInvoice = 2
	}

	public enum GeneralPatientType
	{
		None = 0,
		PrivatePatient = 1,
		NotPrivatePatient = 2
	}

	public enum InventoryItemTransactionFactor
	{
		OneIn = 1,
		NegativeOneOut = -1,
	}

	public enum PaymentViewerType
	{
		MedicalInvoicePayment = 1,
		FinanceInvoicePayment = 2,
		PatientDepositPayment = 3
	}

	#endregion

}
