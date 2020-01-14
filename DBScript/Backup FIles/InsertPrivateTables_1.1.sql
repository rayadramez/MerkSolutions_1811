--USE MerkFinance

SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
IF EXISTS (SELECT * FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#tmpErrors')) DROP TABLE #tmpErrors
GO
CREATE TABLE #tmpErrors (Error int)
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO
BEGIN TRANSACTION

-- ///////////// BEGIN :: Application_p
PRINT 'Application_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].Application_p WHERE ID BETWEEN 1 AND 7)
INSERT INTO dbo.Application_p( ID ,Name_P ,Name_S ,IsOnDuty ,Description)VALUES  ( 1,N'برنـامــــج إستقبـال خـارجــــي',N'ClinicReception',1,NULL)
,( 2,N'برنـامــــج إستقبـال داخلــــي',N'AdmissionReception',1,NULL)
,( 3,N'برنـامــــج إستقبــال خـارجـــي وداخلــــي',N'AllReception',1,NULL)
,( 4,N'برنـامــــج الملفــات الطبيـــة للمرضــى',N'PEMR',1,NULL)
,( 5,N'برنـامــــج إدارة الفـواتيــــر',N'InvoiceManager',1,NULL)
,( 6,N'برنـامــــج الإعـــدادات العـامـــــة',N'Settings',1,NULL)
,( 7,N'برنـامــــج إدارة الطـوابيـــر',N'Queue Manager',1,NULL)

PRINT 'Application_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].Application_p WHERE ID BETWEEN 8 AND 10)
INSERT INTO dbo.Application_p( ID ,Name_P ,Name_S ,IsOnDuty ,Description)VALUES  ( 8,N'FinanceInvoiceCreation',N'FinanceInvoiceCreation',1,NULL),
( 9,N'برنامج العمليات',N'برنامج العمليات',1,NULL),
( 10,N'OphalmologySurgeryApplication',N'OphalmologySurgeryApplication',1,NULL)

PRINT 'Application_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].Application_p WHERE ID = 11)
INSERT INTO dbo.Application_p( ID ,Name_P ,Name_S ,IsOnDuty ,Description)VALUES  ( 11,N'MerkFinance',N'MerkFinance',1,NULL)

IF NOT EXISTS (SELECT 1 FROM [dbo].Application_p WHERE ID = 8)
INSERT INTO dbo.Application_p( ID ,Name_P ,Name_S ,IsOnDuty ,Description)VALUES  ( 8,N'FinanceInvoiceCreation',N'FinanceInvoiceCreation',1,NULL)
IF NOT EXISTS (SELECT 1 FROM [dbo].Application_p WHERE ID = 9)
INSERT INTO dbo.Application_p( ID ,Name_P ,Name_S ,IsOnDuty ,Description)VALUES  ( 9,N'برنامج إستقبال عمليات اليوم الواحد',N'One Day Surgery Reception',1,NULL)
IF NOT EXISTS (SELECT 1 FROM [dbo].Application_p WHERE ID = 10)
INSERT INTO dbo.Application_p( ID ,Name_P ,Name_S ,IsOnDuty ,Description)VALUES  ( 10,N'OphalmologySurgeryApplication',N'OphalmologySurgeryApplication',1,NULL)
-- ///////////// END :: Application_p

-- ///////////// BEGIN :: CashBoxTransactionType_p
PRINT 'CashBoxTransactionType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].CashBoxTransactionType_p WHERE ID BETWEEN 1 AND 5)
INSERT INTO dbo.CashBoxTransactionType_p( ID ,Name_P ,Name_S ,Description ,MultiplierEffect)VALUES( 1,N'مسحوبات من الخزينة',N'ExpenseWithdraw',NULL,-1)
,( 2,N'مردودات مسحوبات من الخزينة',N'ReverseExpenseWithdraw',NULL,1)
,( 3,N'إيداع إيرادات إلى الخزينة',N'RevenueDeposit',NULL,1)
,( 4,N'مردودات إيداع إيرادات إلى الخزينة',N'ReverseRevenueDeposit',NULL,-1)
,( 5,N'تحويل إلى خزينة',N'TransferToFundsCashBox',NULL,-1)
-- ///////////// END :: CashBoxTransactionType_p

-- ///////////// BEGIN :: ChartOfAccountCodeMargin_p
PRINT 'ChartOfAccountCodeMargin_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].ChartOfAccountCodeMargin_p WHERE ID BETWEEN 1 AND 4)
INSERT INTO dbo.ChartOfAccountCodeMargin_p( ID, Name_P, Name_S, NumberOfDigits )VALUES  ( 1,N'المستوى الأول',N'NULL',1)
,( 2,N'المستوى الثاني',N'NULL',2)
,( 3,N'المستوى الثالث',N'NULL',2)
,( 4,N'المستوى الرابع',N'NULL',2)
-- ///////////// END :: ChartOfAccountCodeMargin_p

-- ///////////// BEGIN :: ChartOfAccountCodeMargin_p
IF NOT EXISTS (SELECT 1 FROM [dbo].ChartOfAccountCodeMargin_p WHERE ID = 5)
INSERT INTO dbo.ChartOfAccountCodeMargin_p( ID, Name_P, Name_S, NumberOfDigits )VALUES  ( 5,N'المستوى الخامس',N'NULL',1)
-- ///////////// END :: ChartOfAccountCodeMargin_p

-- ///////////// BEGIN :: CommonTransactionType_p
PRINT 'CommonTransactionType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].CommonTransactionType_p WHERE ID BETWEEN 1 AND 5)
INSERT INTO dbo.CommonTransactionType_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'CreateNew',N'CreateNew',NULL)
,( 2,N'SaveNew',N'SaveNew',NULL)
,( 3,N'UpdateExisting',N'UpdateExisting',NULL)
,( 4,N'DeleteExisting',N'DeleteExisting',NULL)
,( 5,N'Load',N'Load',NULL)
-- ///////////// END :: CommonTransactionType_p

-- ///////////// BEGIN :: DiscountType_p
PRINT 'DiscountType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].DiscountType_p WHERE ID BETWEEN 1 AND 2)
INSERT INTO dbo.DiscountType_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'قيمــــة',N'Amount',NULL)
,( 2,N'نسبـــة',N'Percentage',NULL)
-- ///////////// END :: DiscountType_p

-- ///////////// BEGIN :: PEMRSavingMode_p
PRINT 'PEMRSavingMode_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].PEMRSavingMode_p WHERE ID BETWEEN 1 AND 2)
INSERT INTO dbo.PEMRSavingMode_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'SaveImmediately',N'SaveImmediately',NULL)
,( 2,N'PostponeSaving',N'PostponeSaving',NULL)
-- ///////////// END :: PEMRSavingMode_p

-- ///////////// BEGIN :: DoctorProfessionalFeesIssuingType_p
PRINT 'DoctorProfessionalFeesIssuingType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].DoctorProfessionalFeesIssuingType_p WHERE ID BETWEEN 1 AND 3)
INSERT INTO dbo.DoctorProfessionalFeesIssuingType_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'لا يوجد',N'None',NULL)
,( 2,N'أتعاب الأطباء خاصه بالكامل',N'AllDoctorProfessionalFeesIsPrivate',NULL)
,( 3,N'معالجة أتعاب الأطباء بقسم الحسابات',N'NotPrivateDoctorProfessionalFees',NULL)
-- ///////////// END :: DoctorProfessionalFeesIssuingType_p

-- ///////////// BEGIN :: DoctorRank_p
PRINT 'DoctorRank_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].DoctorRank_p WHERE ID BETWEEN 1 AND 3)
INSERT INTO dbo.DoctorRank_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'طبيب أخصائي',N'طبيب إمتيـــاز',NULL)
,( 2,N'طبيب إستشاري',N'Consultant Doctor',NULL)
,( 3,N'طبيب مساعد',N'Assistant Doctor',NULL)
-- ///////////// END :: DoctorRank_p

-- ///////////// BEGIN :: DoctorSpecialization_p
PRINT 'DoctorSpecialization_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].DoctorSpecialization_p WHERE ID BETWEEN 1 AND 8)
INSERT INTO dbo.DoctorSpecialization_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'الأشعة الباطنية',N'Abdominal Radiology',NULL)
,( 2,N'إدمان الطب النفسي',N'Addiction Psychiatry',NULL)
,( 3,N'طب المراهقين',N'Adolescent Medicine ',NULL)
,( 4,N'التخدير',N'Anesthesiology',NULL)
,( 5,N'الحساسية والمناعة',N'Allergy & Immunology',NULL)
,( 6,N'الأشعة القلبية',N'Cardiothoracic Radiology',NULL)
,( 7,N'طبيب ممارس عام',N'طبيب ممارس عام',NULL)
,( 8,N'طبيب أسنان',N'طبيب أسنان',NULL)
-- ///////////// END :: DoctorSpecialization_p

-- ///////////// BEGIN :: DoctorSpecialization_p
PRINT 'DoctorSpecialization_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].DoctorSpecialization_p WHERE ID BETWEEN 9 AND 16)
INSERT INTO dbo.DoctorSpecialization_p( ID, Name_P, Name_S, Description )VALUES  ( 9,N'اختصاص طبي',N'اختصاص طبي',NULL)
,( 10,N'أمراض نفسية جسمية',N'أمراض نفسية جسمية',NULL)
,( 11,N'إحصاء طبي',N'إحصاء طبي ',NULL)
,( 12,N'إدارة الخدمات الطبية',N'إدارة الخدمات الطبية',NULL)
,( 13,N'إدراك بصري',N'إدراك بصري',NULL)
,( 14,N'إصابة البطن',N'إصابة البطن',NULL)
,( 15,N'طب القلب‏',N'طب القلب‏',NULL)
,( 16,N'الغدد الصم',N'الغدد الصم',NULL)
-- ///////////// END :: DoctorSpecialization_p

-- ///////////// BEGIN :: GeneralChartOfAccountType_p
PRINT 'GeneralChartOfAccountType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].GeneralChartOfAccountType_p WHERE ID BETWEEN 1 AND 33)
INSERT INTO dbo.GeneralChartOfAccountType_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'عهد',N'NULL',NULL)
,( 2,N'عهد مستديمه',N'NULL',NULL)
,( 3,N'عهد مؤقته',N'NULL',NULL)
,( 4,N'إكراميات',N'NULL',NULL)
,( 5,N'إعتمادت مستندية',N'NULL',NULL)
,( 6,N'سفر وإنتقال',N'NULL',NULL)
,( 7,N'إيرادات عامة',N'NULL',NULL)
,( 8,N'مرتبات',N'NULL',NULL)
,( 9,N'حوافز',N'NULL',NULL)
,( 10,N'مرتبات وحوافز',N'NULL',NULL)
,( 11,N'رسوم',N'NULL',NULL)
,( 12,N'إشتراكات',N'NULL',NULL)
,( 13,N'رسوم وإشتراكات',N'NULL',NULL)
,( 14,N'بيع أصول',N'NULL',NULL)
,( 15,N'بيع أصول ثابتة',N'NULL',NULL)
,( 16,N'بيع أصول متداولة',N'NULL',NULL)
,( 17,N'مدينون متنوعون',N'NULL',NULL)
,( 18,N'أوراق قبض',N'NULL',NULL)
,( 19,N'إيجار',N'NULL',NULL)
,( 20,N'مصروفات',N'NULL',NULL)
,( 21,N'مصروفات عامة',N'NULL',NULL)
,( 22,N'مصروفات إدارية',N'NULL',NULL)
,( 23,N'تحويلات الخزائن',N'NULL',NULL)
,( 24,N'مشتريات عامة',N'NULL',NULL)
,( 25,N'مصاريف المشتريات',N'NULL',NULL)
,( 26,N'مردودات ومسموحات المشتريات',N'NULL',NULL)
,( 27,N'خصم عام',N'NULL',NULL)
,( 28,N'خصم الكمية المكتسب',N'NULL',NULL)
,( 29,N'مردودات المصروفات الإدارية',N'NULL',NULL)
,( 30,N'مردودات الإيرادات العامة',N'NULL',NULL)
,( 31,N'إيرادات المبيعات',N'NULL',NULL)
,( 32,N'مرودوات المصروفات العامة',N'NULL',NULL)
,( 33,N'مردودات إيرادات المبيعات',N'NULL',NULL)
-- ///////////// END :: GeneralChartOfAccountType_p

-- ///////////// BEGIN :: IdentificationCardType_p
PRINT 'IdentificationCardType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].IdentificationCardType_p WHERE ID BETWEEN 1 AND 4)
INSERT INTO dbo.IdentificationCardType_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'بطاقة شخصية',N'بطاقة شخصية',NULL)
,( 2,N'جواز سفر',N'جواز سفر',NULL)
,( 3,N'رخصة قيادة سيارة خاصة',N'رخصة قيادة سيارة خاصة',NULL)
,( 4,N'رخصة قيادة دراجة نارية خاصة',N'رخصة قيادة دراجة نارية خاصة',NULL)
-- ///////////// END :: IdentificationCardType_p

-- ///////////// BEGIN :: InPatientAdmissionPricingType_p
PRINT 'InPatientAdmissionPricingType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].InPatientAdmissionPricingType_p WHERE ID BETWEEN 1 AND 2)
INSERT INTO dbo.InPatientAdmissionPricingType_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'إقامة المريض',N'إقامة المريض',NULL)
,( 2,N'إقامة المرافق',N'إقامة المرافق',NULL)
-- ///////////// END :: InPatientAdmissionPricingType_p

-- ///////////// BEGIN :: InPatientRoomBedStatus_p
PRINT 'InPatientRoomBedStatus_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].InPatientRoomBedStatus_p WHERE ID BETWEEN 1 AND 5)
INSERT INTO dbo.InPatientRoomBedStatus_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'فارغ',N'فارغ',NULL)
,( 2,N'شاغر',N'شاغر',NULL)
,( 3,N'متوقف عن العمل',N'متوقف عن العمل',NULL)
,( 4,N'ملغي',N'ملغي',NULL)
,( 5,N'تحت الصيانة',N'تحت الصيانة',NULL)
-- ///////////// END :: InPatientRoomBedStatus_p

-- ///////////// BEGIN :: InPatientRoomType_p
PRINT 'InPatientRoomType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].InPatientRoomType_p WHERE ID BETWEEN 1 AND 5)
INSERT INTO dbo.InPatientRoomType_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'غرفة مفردة',N'غرفة مفردة',NULL)
,( 2,N'غرفة مزدوجة',N'غرفة مزدوجة',NULL)
,( 3,N'غسيل كلى',N'غسيل كلى',NULL)
,( 4,N'رعاية مركزة',N'رعاية مركزة',NULL)
,( 5,N'جناح',N'جناح',NULL)
-- ///////////// END :: InPatientRoomType_p

-- ///////////// BEGIN :: InventoryItemTransactionType_p
PRINT 'InventoryItemTransactionType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].InventoryItemTransactionType_p WHERE ID BETWEEN 1 AND 5)
INSERT INTO dbo.InventoryItemTransactionType_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'رصيد بداية المدة',N'StartingInventoryBalance',NULL)
,( 2,N'رصيد نهاية المدة',N'EndingInventoryBalance',NULL)
,( 3,N'تسوية المخزون',N'InventorySettlement',NULL)
,( 4,N'إدخال',N'Input Transaction',NULL)
,( 5,N'إخراج',N'Output Transaction',NULL)
-- ///////////// END :: InventoryItemTransactionType_p

-- ///////////// BEGIN :: InventoryTrackingType_p
PRINT 'InventoryTrackingType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].InventoryTrackingType_p WHERE ID BETWEEN 1 AND 2)
INSERT INTO dbo.InventoryTrackingType_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'الجـرد المستمـر',N'الجـرد المستمـر',NULL)
,( 2,N'الجـرد الـدوري',N'الجـرد الـدوري',NULL)
-- ///////////// END :: InventoryTrackingType_p

-- ///////////// BEGIN :: InvoicePaymentType_p
PRINT 'InvoicePaymentType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].InvoicePaymentType_p WHERE ID BETWEEN 1 AND 2)
INSERT INTO dbo.InvoicePaymentType_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'فاتورة نقدية',N'فاتورة نقدية',NULL)
,( 2,N'فاتورة آجلة',N'فاتورة آجلة',NULL)
-- ///////////// END :: InvoicePaymentType_p

-- ///////////// BEGIN :: InvoiceType_p
PRINT 'InvoiceType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].InvoiceType_p WHERE ID BETWEEN 1 AND 8)
INSERT INTO dbo.InvoiceType_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'داخلى - حالة خاصة',N'داخلى - حالة خاصة',NULL)
,( 2,N'خارجي - حالة خاصة',N'خارجي - حالة خاصة',NULL)
,( 3,N'داخلي - حالة مستشفى',N'داخلي - حالة مستشفى',NULL)
,( 4,N'خارجي - حالة مستشفى',N'خارجي - حالة مستشفى',NULL)
,( 5,N'فاتورة المشتريات',N'PurchasingInvoice',NULL)
,( 6,N'مرتجع فاتورة المشتريات',N'ReturningPurchasingInvoice',NULL)
,( 7,N'فاتورة المبيعات',N'SellingInvoice',NULL)
,( 8,N'مرتجع فاتورة المبيعات',N'ReturningSellingInvoice',NULL)
-- ///////////// END :: InvoiceType_p

-- ///////////// BEGIN :: MaritalStatus_p
PRINT 'MaritalStatus_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].MaritalStatus_p WHERE ID BETWEEN 1 AND 3)
INSERT INTO dbo.MaritalStatus_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'أعزب',N'أعزب',NULL)
,( 2,N'متزوج',N'متزوج',NULL)
,( 3,N'مطلق',N'مطلق',NULL)
-- ///////////// END :: MaritalStatus_p

-- ///////////// BEGIN :: MedicalFlow_p
PRINT 'MedicalFlow_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].MedicalFlow_p WHERE ID BETWEEN 1 AND 2)
INSERT INTO dbo.MedicalFlow_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'خارجي',N'OutPatientFlow',NULL)
,( 2,N'داخلي',N'InPatientFlow',NULL)
-- ///////////// END :: MedicalFlow_p

-- ///////////// BEGIN :: Organization_p
PRINT 'Organization_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].Organization_p WHERE ID BETWEEN 1 AND 3)
INSERT INTO dbo.Organization_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'الأنبا إبرآم',N'Ava Abraam',NULL)
,( 2,N'Dental_Clinic',N'Dental_Clinic',NULL)
,( 3,N'Cardiovascular_Clinic',N'Cardiovascular_Clinic',NULL)
-- ///////////// END :: Organization_p

-- ///////////// BEGIN :: PaymentType_p
PRINT 'PaymentType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].PaymentType_p WHERE ID BETWEEN 1 AND 2)
INSERT INTO dbo.PaymentType_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'كاش',N'Cash Payment',NULL)
,( 2,N'شيك',N'Check Payment',NULL)
,( 3,N'فيزا',N'Visa Payment',NULL)
-- ///////////// END :: PaymentType_p

-- ///////////// BEGIN :: PersonChartOtAccountType_p
PRINT 'PersonChartOtAccountType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].PersonChartOtAccountType_p WHERE ID BETWEEN 1 AND 4)
INSERT INTO dbo.PersonChartOtAccountType_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'حساب المدينون',N'DebitAccountingCode',NULL)
,( 2,N'حساب الإتعاب المهنية',N'ProfessionalFeesAccountingCode',NULL)
,( 3,N'حساب الضرائب',N'TaxAccountingCode',NULL)
,( 4,N'حساب الجارى',N'CurrentAccountingCode',NULL)
-- ///////////// END :: PersonChartOtAccountType_p

-- ///////////// BEGIN :: PersonRelativeType_p
PRINT 'PersonRelativeType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].PersonRelativeType_p WHERE ID BETWEEN 1 AND 6)
INSERT INTO dbo.PersonRelativeType_p( ID, Name_P, Name_S, Decsription )VALUES  ( 1,N'الوالد',N'الوالد',NULL)
,( 2,N'الوالدة',N'الوالدة',NULL)
,( 3,N'الزوج',N'الزوج',NULL)
,( 4,N'الزوجة',N'الزوجة',NULL)
,( 5,N'الإبن',N'الإبن',NULL)
,( 6,N'الإبنة',N'الإبنة',NULL)
-- ///////////// END :: PersonRelativeType_p

-- ///////////// BEGIN :: PersonTitle_p
PRINT 'PersonTitle_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].PersonTitle_p WHERE ID BETWEEN 1 AND 5)
INSERT INTO dbo.PersonTitle_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'السيد',N'Mr.',NULL)
,( 2,N'السيدة',N'Mrs.',NULL)
,( 3,N'الآنسة',N'Ms.',NULL)
,( 4,N'الدكتور',N'Dr.',NULL)
,( 5,N'الأستاذ الدكتور',N'Prof.',NULL)
-- ///////////// END :: PersonTitle_p

-- ///////////// BEGIN :: PhoneType_p
PRINT 'PhoneType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].PhoneType_p WHERE ID BETWEEN 1 AND 6)
INSERT INTO dbo.PhoneType_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'محمول خاص',N'محمول خاص',NULL)
,( 2,N'محمول عمل',N'محمول عمل',NULL)
,( 3,N'محمول آخر',N'محمول آخر',NULL)
,( 4,N'تليفون منزل',N'تليفون منزل',NULL)
,( 5,N'تليفون عمل',N'تليفون عمل',NULL)
,( 6,N'تليفون آخر',N'تليفون آخر',NULL)
-- ///////////// END :: PhoneType_p

-- ///////////// BEGIN :: PriceType_p
PRINT 'PriceType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].PriceType_p WHERE ID BETWEEN 1 AND 2)
INSERT INTO dbo.PriceType_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'سعر البيع',N'Selling Price',NULL)
,( 2,N'سعر الشراء',N'Purchasing Price',NULL)
-- ///////////// END :: PriceType_p

-- ///////////// BEGIN :: QueueManagerStatus_p
PRINT 'QueueManagerStatus_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].QueueManagerStatus_p WHERE ID BETWEEN 1 AND 6)
INSERT INTO dbo.QueueManagerStatus_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'Serving',N'Serving',NULL)
,( 2,N'Paused',N'Paused',NULL)
,( 3,N'Cancelled',N'Cancelled',NULL)
,( 4,N'Waiting',N'Waiting',NULL)
,( 5,N'Served',N'Served',NULL)
,( 6,N'Stopped',N'Stopped',NULL)
-- ///////////// END :: QueueManagerStatus_p

-- ///////////// BEGIN :: PriceType_p
PRINT 'PriceType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].PriceType_p WHERE ID BETWEEN 1 AND 2)
INSERT INTO dbo.PriceType_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'سعر البيع',N'Selling Price',NULL)
,( 2,N'سعر الشراء',N'Purchasing Price',NULL)
-- ///////////// END :: PriceType_p

-- ///////////// BEGIN :: Role_p
PRINT 'Role_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].Role_p WHERE ID BETWEEN 1 AND 3)
INSERT INTO dbo.Role_p( ID ,Name_P ,Name_S ,Application_P_ID ,Description)VALUES  ( 1,N'حجز العيادات الخارجية فقط',N'Reserve Clinic Services Only',3,NULL)
,( 2,N'حجز الخدمات الداخلي فقط',N'Reserve In Patient Services Only',3,NULL)
,( 3,N'السماح بإضافة سياسة جهات التأمين من الفاتورة',N'Can Reserve Insurance Policy from Invoice Creation',NULL,NULL)
-- ///////////// END :: Role_p

-- ///////////// BEGIN :: ServiceType_p
PRINT 'ServiceType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].ServiceType_p WHERE ID BETWEEN 1 AND 10)
INSERT INTO dbo.ServiceType_p( ID ,Name_P ,Name_S ,Description ,IsMedical ,AllowAdmission)VALUES  (1,N'كشف',N'كشف',NULL,1,1)
,(2,N'عملية',N'عملية',NULL,1,1)
,(3,N'فحوصات وأشعة',N'فحوصات وأشعة',NULL,1,1)
,(4,N'معامل وتحاليل',N'معامل وتحاليل',NULL,1,1)
,(5,N'إقامة',N'إقامة',NULL,1,1)
,(6,N'أخرى',N'أخرى',NULL,0,1)
,(7,N'ParentLabService',N'ParentLabService',NULL,1,NULL)
,(8,N'ParentAccommodationService',N'ParentAccommodationService',NULL,1,NULL)
,(9,N'ParentSurgeryService',N'ParentSurgeryService',NULL,1,NULL)
,(10,N'DoctorFeesService',N'DoctorFeesService',NULL,0,NULL)
-- ///////////// END :: ServiceType_p

-- ///////////// BEGIN :: Station_p
PRINT 'Station_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].Station_p WHERE ID BETWEEN 1 AND 16)
INSERT INTO dbo.Station_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'إستقبال العمليات',N'Surgery Reception',NULL)
,( 2,N'غرفة التحضير قبل العمليات',N'Suergery General Preperation',NULL)
,( 3,N'غرفة الإفاقة بعد العمليات',N'Surgery General Discharge',NULL)
,( 4,N'غرفة عمليات النساء والولادة',N'Katina4',NULL)
,( 5,N'غرفة عمليات عامة',N'General Surgery Room',NULL)
,( 6,N'عيادة كشف العظام',N'',NULL)
,( 7,N'عيادة كشف النساء والتوليد',N'Clinic for women and obstetrics Room',NULL)
,( 8,N'عيادة كشف الأذن والحنجرة',N'Ear and Throat Examination Room',NULL)
,( 9,N'غرفة عمليات القلب المفتوح',N'Open Heart Surgery Room',NULL)
,( 10,N'غرفة عمليات قسطرة القلب',N'Cardiac Cathererisation Surgery Room',NULL)
,( 11,N'عيادة الصدر',N'Breast Clinic Room',NULL)
,( 12,N'عيادة الجلدية',N'Dermatology Clinic Room',NULL)
,( 13,N'المعمل',N'Laboratory Room',NULL)
,( 14,N'عيادة الأنف والأذن والحنجرة',N'عيادة الأنف والأذن والحنجرة',NULL)
,( 15,N'عيادة الأسنان',N'عيادة الأسنان',NULL)
,( 16,N'عيادة القلب والأوعية الدموية',N'Cardiovascular Clinic',NULL)

IF NOT EXISTS (SELECT 1 FROM [dbo].Station_p WHERE ID = 18)
INSERT INTO dbo.Station_p( ID, Name_P, Name_S, Description )VALUES  ( 18,N'غرفة عمليات العيون',N'OphthalmologySurgeryRoom',NULL)
-- ///////////// END :: Station_p

-- ///////////// BEGIN :: SurchargeType_p
PRINT 'SurchargeType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].SurchargeType_p WHERE ID BETWEEN 1 AND 4)
INSERT INTO dbo.SurchargeType_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'خدمات إضافية',N'AdditionalServices',NULL)
,( 2,N'ضرائب',N'Taxes',NULL)
,( 3,N'تأمين',N'Insurance',NULL)
,( 4,N'دمغة',N'Stamp',NULL)
-- ///////////// END :: SurchargeType_p

-- ///////////// BEGIN :: ImageType_p
PRINT 'ImageType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].ImageType_p WHERE ID BETWEEN 1 AND 6)
INSERT INTO dbo.ImageType_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'بطاقة شخصية',N'بطاقة شخصية',NULL)
,( 2,N'جواز سفر',N'جواز سفر',NULL)
,( 3,N'نتيجــة فحوصات',N'نتيجــة فحوصات',NULL)
,( 4,N'نتيجــة التحاليل',N'نتيجــة التحاليل',NULL)
,( 5,N'أخرى',N'أخرى',NULL)
,( 6,N'صـــورة شخصيـــة',N'صـــورة شخصيـــة',NULL)
-- ///////////// END :: ImageType_p

-- ///////////// BEGIN :: ImageType_p
PRINT 'ImageType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].ImageType_p WHERE ID = 7)
INSERT INTO dbo.ImageType_p( ID, Name_P, Name_S, Description )VALUES  ( 7,N'تقرير عملية',N'Surgery Report',NULL)

-- ///////////// BEGIN :: UnitMeasurment_p
PRINT 'UnitMeasurment_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].UnitMeasurment_p WHERE ID BETWEEN 1 AND 17)
INSERT INTO dbo.UnitMeasurment_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'جرام',N'Gram',NULL)
,( 2,N'كيلوجرام',N'Kilogram',NULL)
,( 3,N'وحده',N'Item',NULL)
,( 4,N'مخزن البيانات',N'Data Storage',NULL)
,( 5,N'الطول',N'Length',NULL)
,( 6,N'درجة حرارة',N'Temperature',NULL)
,( 7,N'زمن',N'Time',NULL)
,( 8,N'كتلة',N'Mass',NULL)
,( 9,N'سرعة',N'Speed',NULL)
,( 10,N'تسارع',N'Acceleration',NULL)
,( 11,N'ضغط',N'Pressure',NULL)
,( 12,N'دوران',N'Rotation',NULL)
,( 13,N'نسبة مئوية',N'Percentage',NULL)
,( 14,N'مؤشر الأشعة فوق البنفسجية',N'UV Index',NULL)
,( 15,N'حجم',N'Volume',NULL)
,( 16,N'جاذبية',N'Gravity',NULL)
,( 17,N'Military rating',N'Military rating',NULL)
-- ///////////// END :: UnitMeasurment_p

-- ///////////// BEGIN :: VisitAssessmentTopic_p
PRINT 'VisitAssessmentTopic_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].VisitAssessmentTopic_p WHERE ID BETWEEN 1 AND 4)
INSERT INTO dbo.VisitAssessmentTopic_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'Complains',N'Complains',NULL)
,( 2,N'Medical History',N'Medical History',NULL)
,( 3,N'Dental History',N'Dental History',NULL)
,( 4,N'Examination',N'Examination',NULL)
-- ///////////// END :: VisitAssessmentTopic_p

-- ///////////// BEGIN :: VisitAssessmentTopic_p
PRINT 'VisitAssessmentTopic_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].VisitAssessmentTopic_p WHERE ID BETWEEN 5 AND 6)
INSERT INTO dbo.VisitAssessmentTopic_p( ID, Name_P, Name_S, Description )VALUES  ( 5,N'Medications',N'Medications',NULL)
,( 6,N'Dose',N'Dose',NULL)
-- ///////////// END :: VisitAssessmentTopic_p

-- ///////////// BEGIN :: TimeDuration_p
PRINT 'TimeDuration_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].TimeDuration_p WHERE ID BETWEEN 1 AND 5)
INSERT INTO dbo.TimeDuration_p( ID, Name_P, Name_S, Description )VALUES  ( 1,N'Hourly',N'Hourly',NULL)
,( 2,N'Daily',N'Daily',NULL)
,( 3,N'Weekly',N'Weekly',NULL)
,( 4,N'Monthly',N'Monthly',NULL)
,( 5,N'Yearly',N'Yearly',NULL)
-- ///////////// END :: TimeDuration_p

-- ///////////// BEGIN :: PersonType_p
PRINT 'PersonType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].PersonType_p WHERE ID BETWEEN 1 AND 5)
INSERT INTO dbo.PersonType_p( ID, Name_P, Name_S, Description, IsOnDuty )VALUES  ( 1,N'عميـل',N'Customer',NULL, 1)
,( 2,N'مــورد',N'Supplier',NULL, 1)
,( 3,N'مستخــدم',N'User',NULL, 1)
,( 4,N'مريــض',N'Patient',NULL, 1)
,( 5,N'طبيــب',N'Doctor',NULL, 1)
-- ///////////// END :: PersonType_p

-- ///////////// BEGIN :: DiagnosisType_p
PRINT 'DiagnosisType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].DiagnosisType_p WHERE ID BETWEEN 1 AND 3)
INSERT INTO dbo.DiagnosisType_p( ID, Name_P, Name_S, Description)VALUES  ( 1,N'Provisional',N'Provisional',NULL)
,(2, N'Differential',N'Differential',NULL)
,(3, N'Final',N'Final',NULL)
-- ///////////// END :: DiagnosisType_p

-- ///////////// BEGIN :: Eye_p
PRINT 'Eye_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].Eye_p WHERE ID BETWEEN 1 AND 3)
INSERT INTO dbo.Eye_p( ID, Name_P, Name_S, Description)VALUES  ( 1,N'OS',N'OS',NULL)
,(2, N'OD',N'OD',NULL)
,(3, N'OU',N'OU',NULL)
-- ///////////// END :: Eye_p

-- ///////////// BEGIN :: DiabetesType_p
PRINT 'DiabetesType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].DiabetesType_p WHERE ID BETWEEN 1 AND 2)
INSERT INTO dbo.DiabetesType_p( ID, Name_P, Name_S, Description)VALUES  ( 1,N'Type 1',N'Type 1',NULL)
,(2, N'Type 2',N'Type 2',NULL)
-- ///////////// END :: DiabetesType_p

-- ///////////// BEGIN :: DiabetedMedicationType_p
PRINT 'DiabetedMedicationType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].DiabetedMedicationType_p WHERE ID BETWEEN 1 AND 3)
INSERT INTO dbo.DiabetedMedicationType_p( ID, Name_P, Name_S, Description)VALUES  ( 1,N'Both',N'Both',NULL)
,(2, N'Tablets',N'Tablets',NULL)
,(3, N'Insulin',N'Insulin',NULL)
-- ///////////// END :: DiabetedMedicationType_p

-- ///////////// BEGIN :: PupillarySize_p
PRINT 'PupillarySize_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].PupillarySize_p WHERE ID BETWEEN 1 AND 3)
INSERT INTO dbo.PupillarySize_p( ID, Name_P, Name_S, Description)VALUES  ( 1,N'Miosis',N'Miosis',NULL)
,(2, N'Medium',N'Medium',NULL)
,(3, N'Mydrisasis',N'Mydrisasis',NULL)
-- ///////////// END :: PupillarySize_p

-- ///////////// BEGIN :: PupillaryShape_p
PRINT 'PupillaryShape_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].PupillaryShape_p WHERE ID BETWEEN 1 AND 5)
INSERT INTO dbo.PupillaryShape_p( ID, Name_P, Name_S, Description)VALUES  ( 1,N'Rounded',N'Rounded',NULL)
,(2, N'Oval',N'Oval',NULL)
,(3, N'Sector',N'Sector',NULL)
,(4, N'Irruglar',N'Irruglar',NULL)
,(5, N'Peaked',N'Peaked',NULL)
-- ///////////// END :: PupillaryShape_p

-- ///////////// BEGIN :: PupillaryRAPDGradingScale_p
PRINT 'PupillaryRAPDGradingScale_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].PupillaryRAPDGradingScale_p WHERE ID BETWEEN 1 AND 5)
INSERT INTO dbo.PupillaryRAPDGradingScale_p( ID, Name_P, Name_S, Description)VALUES  ( 1,N'Grade +1',N'Grade +1','A week initial pupillary followed by greater redilation')
,(2, N'Grade +2',N'Grade +2','An initial pupillary stall followed by greater redilation')
,(3, N'Grade +3',N'Grade +3','An immediate pupillary dillation')
,(4, N'Grade +4',N'Grade +4','Immediate pupillary dilation following 6 sec. illumination')
,(5, N'Grade +5',N'Grade +5','Immediate pupillary dilation with no construction at all')
-- ///////////// END :: PupillaryRAPDGradingScale_p

-- ///////////// BEGIN :: SegmentSignType_p
PRINT 'SegmentType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].SegmentSignType_p WHERE ID BETWEEN 1 AND 3)
INSERT INTO dbo.SegmentSignType_p( ID, Name_P, Name_S, Description)VALUES  ( 1,N'Anterior',N'Anterior',NULL)
,(2, N'Posterior',N'Posterior',NULL)
,(3, N'Adnexa',N'Adnexa',NULL)
-- ///////////// END :: SegmentSignType_p

-- ///////////// BEGIN :: SegmentSignType_p
PRINT 'SegmentType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].SegmentSignType_p WHERE ID = 4)
INSERT INTO dbo.SegmentSignType_p( ID, Name_P, Name_S, Description)VALUES  ( 4,N'EOM',N'EOM',NULL)
-- ///////////// END :: SegmentSignType_p

-- ///////////// BEGIN :: UncorrectedDistanceVisualAcuityUnit_p
PRINT 'UncorrectedDistanceVisualAcuityUnit_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].UncorrectedDistanceVisualAcuityUnit_p WHERE ID BETWEEN 1 AND 4)
INSERT INTO dbo.UncorrectedDistanceVisualAcuityUnit_p( ID, Name_P, Name_S, Description)VALUES  ( 1,N'Snellen',N'Snellen',NULL)
,(2, N'Decimal',N'Decimal',NULL)
,(3, N'Logmar',N'Logmar',NULL)
,(4, N'American',N'American',NULL)
-- ///////////// END :: UncorrectedDistanceVisualAcuityUnit_p

-- ///////////// BEGIN :: NearVisiong_p
PRINT 'NearVisiong_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].NearVisiong_p WHERE ID BETWEEN 1 AND 12)
INSERT INTO dbo.NearVisiong_p( ID, Name_P, Name_S, Description)VALUES  
( 1,N'N3',N'N3',NULL)
,(2, N'N4',N'N4',NULL)
,(3, N'N5',N'N5',NULL)
,(4, N'N6',N'N6',NULL)
,( 5,N'N8',N'N8',NULL)
,(6, N'N10',N'N10',NULL)
,(7, N'N12',N'N12',NULL)
,(8, N'N14',N'N14',NULL)
,(9,N'N18',N'N18',NULL)
,(10, N'N24',N'N24',NULL)
,(11, N'N36',N'N36',NULL)
,(12, N'N48',N'N48',NULL)
-- ///////////// END :: NearVisiong_p

-- ///////////// BEGIN :: VisionRefractionReadingType_p
PRINT 'VisionRefractionReadingType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].VisionRefractionReadingType_p WHERE ID BETWEEN 1 AND 5)
INSERT INTO dbo.VisionRefractionReadingType_p( ID, Name_P, Name_S, Description)VALUES  ( 1,N'AutoRef',N'AutoRef',NULL)
,(2, N'AutoRef After Cyclo',N'AutoRef After Cyclo',NULL)
,(3, N'Old Glasses',N'Old Glasses',NULL)
,(4, N'Old Glasses Near',N'Old Glasses Near',NULL)
,(5, N'Subjective Refraction',N'Subjective Refraction',NULL)
-- ///////////// END :: VisionRefractionReadingType_p

-- ///////////// BEGIN :: TemperatureUnit_p
PRINT 'TemperatureUnit_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].TemperatureUnit_p WHERE ID BETWEEN 1 AND 2)
INSERT INTO dbo.TemperatureUnit_p( ID, Name_P, Name_S, Description)VALUES  ( 1,N'Celesius',N'Celesius',NULL)
,(2, N'Fahrenheit',N'Fahrenheit',NULL)
-- ///////////// END :: TemperatureUnit_p

-- ///////////// BEGIN :: WeightUnit_p
PRINT 'WeightUnit_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].WeightUnit_p WHERE ID BETWEEN 1 AND 2)
INSERT INTO dbo.WeightUnit_p( ID, Name_P, Name_S, Description)VALUES  ( 1,N'KG',N'KG',NULL)
,(2, N'KG',N'KG',NULL)
-- ///////////// END :: WeightUnit_p

-- ///////////// BEGIN :: HeightUnit_p
PRINT 'HeightUnit_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].HeightUnit_p WHERE ID BETWEEN 1 AND 3)
INSERT INTO dbo.HeightUnit_p( ID, Name_P, Name_S, Description)VALUES  ( 1,N'CM',N'CM',NULL)
,(2, N'Inch',N'Inch',NULL)
,(3, N'Feet',N'Feet',NULL)
-- ///////////// END :: HeightUnit_p

-- ///////////// BEGIN :: PainLevel_p
PRINT 'PainLevel_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].PainLevel_p WHERE ID BETWEEN 1 AND 6)
INSERT INTO dbo.PainLevel_p( ID, Name_P, Name_S, Description)VALUES  ( 1,N'No Pain 0',N'No Pain 0',NULL)
,(2, N'Mild 1-3',N'Mild 1-3',NULL)
,(3, N'Moderate 4-5',N'Moderate 4-5',NULL)
,(4, N'Sever 6-7',N'Sever 6-7',NULL)
,(5, N'Very Sever 8-9',N'Very Sever 8-9',NULL)
,(6, N'Worst Pain Possible 10',N'Worst Pain Possible 10',NULL)
-- ///////////// END :: PainLevel_p

-- ///////////// BEGIN :: PEMR_Elemet_p
PRINT 'PEMR_Elemet_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].PEMR_Elemet_p WHERE ID BETWEEN 1 AND 18)
INSERT INTO dbo.PEMR_Elemet_p( ID ,ParentID ,Name_P ,Name_S ,IsHead ,DefaultOrderIndex ,Description)VALUES  
 ( 1 ,NULL ,N'MedicalHistory' , N'MedicalHistory' , 1 ,0 , NULL )
,( 2 ,NULL ,N'Examination' , N'Examination' , 1 ,1 , NULL )
,( 3 ,NULL ,N'Diagnosis' , N'Diagnosis' , 1 ,2 , NULL )
,( 4 ,NULL ,N'Referral' , N'Referral' , 1 ,3 , NULL )
,( 5 ,NULL ,N'Follow Up' , N'Follow Up' , 1 ,4 , NULL )
,( 6 ,NULL ,N'Notes' , N'Notes' , 1 ,5 , NULL )
,( 7 ,NULL ,N'Scanned Files' , N'Scanned Files' , 1 ,6 , NULL )
,( 8 ,1 ,N'Medical History' , N'Medical History' , 0 ,0 , NULL )
,( 9 ,1 ,N'Social History' , N'Social History' , 0 ,1 , NULL )
,( 10 ,1 ,N'Family History' , N'Family History' , 0 ,2 , NULL )
,( 11 ,2 ,N'Vital Signs' , N'Vital Signs' , 0 ,0 , NULL )
,( 12 ,2 ,N'Symptoms and Complaints' , N'Symptoms and Complaints' , 0 ,1 , NULL )
,( 13 ,3 ,N'Diagnosis' , N'Diagnosis' , 0 ,0 , NULL )
,( 14 ,3 ,N'Medications' , N'Medications' , 0 ,1 , NULL )
,( 15 ,3 ,N'Investigations' , N'Investigations' , 0 ,2 , NULL )
,( 16 ,3 ,N'Labs' , N'Labs' , 0 ,3 , NULL )
,( 17 ,3 ,N'Surgeries' , N'Surgeries' , 0 ,4 , NULL )
,( 18 ,3 ,N'Treatment Plan' , N'Treatment Plan' , 0 ,5 , NULL )
-- ///////////// END :: PEMR_Elemet_p

-- ///////////// BEGIN :: PEMR_Elemet_p
PRINT 'PEMR_Elemet_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].PEMR_Elemet_p WHERE ID = 19)
INSERT INTO dbo.PEMR_Elemet_p( ID ,ParentID ,Name_P ,Name_S ,IsHead ,DefaultOrderIndex ,Description)VALUES  
 ( 19 ,7 ,N'Attachment' , N'Attachment' , 0 ,0 , NULL )
-- ///////////// END :: PEMR_Elemet_p

-- ///////////// BEGIN :: Country_cu
PRINT 'Country_cu'
IF NOT EXISTS (SELECT 1 FROM [dbo].Country_cu WHERE ID = 1)
INSERT INTO dbo.Country_cu( Name_P, Name_S, Description, IsOnDuty )VALUES  ( N'مصر',N'Egypt',NULL, 1)
-- ///////////// END :: Country_cu

-- ///////////// BEGIN :: City_cu
PRINT 'City_cu'
IF NOT EXISTS (SELECT 1 FROM [dbo].City_cu WHERE ID BETWEEN 1 AND 27)
INSERT INTO dbo.City_cu( Name_P, Name_S, Country_CU_ID, Description, IsOnDuty )VALUES  ( N'الإسكندرية',N'الإسكندرية', 1, NULL, 1),
( N'الإسماعيلية',N'الإسماعيلية', 1, NULL, 1),
( N'أسوان',N'أسوان', 1, NULL, 1),
( N'أسيوط',N'أسيوط', 1, NULL, 1),
( N'الأقصر',N'الأقصر', 1, NULL, 1),
( N'البحر الأحمر',N'البحر الأحمر', 1, NULL, 1),
( N'البحيرة',N'البحيرة', 1, NULL, 1),
( N'بني سويف',N'بني سويف', 1, NULL, 1),
( N'بورسعيد',N'بورسعيد', 1, NULL, 1),
( N'جنوب سيناء',N'جنوب سيناء', 1, NULL, 1),
( N'الجيزة',N'الجيزة', 1, NULL, 1),
( N'الدقهلية',N'الدقهلية', 1, NULL, 1),
( N'دمياط',N'دمياط', 1, NULL, 1),
( N'سوهاج',N'سوهاج', 1, NULL, 1),
( N'السويس',N'السويس', 1, NULL, 1),
( N'الشرقية',N'الشرقية', 1, NULL, 1),
( N'شمال سيناء',N'شمال سيناء', 1, NULL, 1),
( N'الغربية',N'الغربية', 1, NULL, 1),
( N'الفيوم',N'الفيوم', 1, NULL, 1),
( N'القاهرة',N'القاهرة', 1, NULL, 1),
( N'القليوبية',N'القليوبية', 1, NULL, 1),
( N'قنا',N'قنا', 1, NULL, 1),
( N'كفر الشيخ',N'كفر الشيخ', 1, NULL, 1),
( N'مطروح',N'مطروح', 1, NULL, 1),
( N'المنوفية',N'المنوفية', 1, NULL, 1),
( N'المنيا',N'المنيا', 1, NULL, 1),
( N'الوادي الجديد',N'الوادي الجديد', 1, NULL, 1)
-- ///////////// END :: City_cu

-- ///////////// BEGIN :: Region_cu
PRINT 'Region_cu'
IF NOT EXISTS (SELECT 1 FROM [dbo].Region_cu WHERE ID BETWEEN 1 AND 35)
INSERT INTO dbo.Region_cu( Name_P, Name_S, City_CU_ID, Description, IsOnDuty )VALUES  ( N'المرج',N'المرج', 39, NULL, 1),
( N'المطرية',N'المطرية', 39, NULL, 1),
( N'عين شمس',N'عين شمس', 39, NULL, 1),
( N'حي السلام أول',N'حي السلام أول', 39, NULL, 1),
( N'حي السلام ثان',N'حي السلام ثان', 39, NULL, 1),
( N'النزهة',N'النزهة', 39, NULL, 1),
( N'مصر الجديدة',N'مصر الجديدة', 39, NULL, 1),
( N'حي شرق مدينة نصر',N'حي شرق مدينة نصر', 39, NULL, 1),
( N'حي غرب مدينة نصر',N'حي غرب مدينة نصر', 39, NULL, 1),
( N'منشأة ناصر',N'منشأة ناصر', 39, NULL, 1),
( N'الوايلى',N'الوايلى', 39, NULL, 1),
( N'باب الشعرية',N'باب الشعرية', 39, NULL, 1),
( N'حى وسط',N'حى وسط', 39, NULL, 1),
( N'الموسكى',N'الموسكى', 39, NULL, 1),
( N'الأزبكية',N'الأزبكية', 39, NULL, 1),
( N'عابدين',N'عابدين', 39, NULL, 1),
( N'بولاق',N'بولاق', 39, NULL, 1),
( N'حى غرب',N'حى غرب', 39, NULL, 1),
( N'الزيتون',N'الزيتون', 39, NULL, 1),
( N'حدائق القبة',N'حدائق القبة', 39, NULL, 1),
( N'الزاوية الحمراء',N'الزاوية الحمراء', 39, NULL, 1),
( N'الشرابية',N'الشرابية', 39, NULL, 1),
( N'حى الساحل',N'حى الساحل', 39, NULL, 1),
( N'شبرا',N'شبرا', 39, NULL, 1),
( N'روض الفرج',N'روض الفرج', 39, NULL, 1),
( N'السيدة زينب',N'السيدة زينب', 39, NULL, 1),
( N'مصر القديمة',N'مصر القديمة', 39, NULL, 1),
( N'الخليفة',N'الخليفة', 39, NULL, 1),
( N'المقطم',N'المقطم', 39, NULL, 1),
( N'البساتين',N'البساتين', 39, NULL, 1),
( N'دار السلام',N'دار السلام', 39, NULL, 1),
( N'المعادي وطرة',N'المعادي وطرة', 39, NULL, 1),
( N'حلوان',N'حلوان', 39, NULL, 1),
( N'التبين',N'التبين', 39, NULL, 1),
( N'15 مايو',N'15 مايو', 39, NULL, 1)
-- ///////////// END :: Region_cu

-- ///////////// BEGIN :: RawMaterialTranasctionType_p
PRINT 'RawMaterialTranasctionType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].RawMaterialTranasctionType_p WHERE ID BETWEEN 1 AND 2)
INSERT INTO dbo.RawMaterialTranasctionType_p ( ID, Name_P, Name_S, Description ) VALUES  ( 1, N'شراء',N'Purchasing',N'' ),
( 2, N'بيع',N'Selling',N'' )

PRINT 'RawMaterialTranasctionType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].RawMaterialTranasctionType_p WHERE ID = 3)
INSERT INTO dbo.RawMaterialTranasctionType_p ( ID, Name_P, Name_S, Description ) VALUES  ( 3, N'إستهلاك',N'Consuming',N'' )
-- ///////////// END :: RawMaterialTranasctionType_p

-- ///////////// BEGIN :: RawMaterialType_p
PRINT 'RawMaterialType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].RawMaterialType_p WHERE ID BETWEEN 1 AND 2)
INSERT INTO dbo.RawMaterialType_p ( ID, Name_P, Name_S, Description ) VALUES  ( 1, N'أخشاب إم دي أف',N'MDF Wood',N'' ),
( 2, N'لزق أمير',N'Amir Adhesive',N'' )

IF NOT EXISTS (SELECT 1 FROM [dbo].RawMaterialType_p WHERE ID = 3)
INSERT INTO dbo.RawMaterialType_p ( ID, Name_P, Name_S, Description ) VALUES ( 3, N'حلقان للمفاتيح',N'Keychain Ring',N'' )
-- ///////////// END :: RawMaterialType_p

-- ///////////// BEGIN :: DividedByType_p
PRINT 'DividedByType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].DividedByType_p WHERE ID BETWEEN 1 AND 3)
INSERT INTO dbo.DividedByType_p ( ID, Name_P, Name_S, Description ) VALUES  ( 1, N'غير مقسم',N'Not Divided',N'120*240' ),
( 2, N'مقسم إلى 4 بالطول',N'Divided By 4 Height',N'120*60' ),
( 3, N'مقسم إلى 6 بالعرض والطول',N'Divided By 6 Width and Height',N'60*80' )
-- ///////////// END :: DividedByType_p

-- ///////////// BEGIN :: DividedByType_p
PRINT 'DividedByType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].SizeUnitMeasure_p WHERE ID BETWEEN 1 AND 2)
INSERT INTO dbo.SizeUnitMeasure_p ( ID, Name_P, Name_S, Description ) VALUES  ( 1, N'سنتى متر',N'CM',N'120*240' ),
( 2, N'مللي متر',N'MM',N'120*60' )
-- ///////////// END :: DividedByType_p

IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END

IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN

COMMIT TRANSACTION
PRINT 'SMD Private Data Updated Successfully'
END 
ELSE PRINT '>>>>>>>>>>>>>>> The database update failed'
GO
DROP TABLE #tmpErrors
GO
