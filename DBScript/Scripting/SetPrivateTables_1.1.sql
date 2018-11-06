USE MerkFinance

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

-- ///////////// BEGIN :: IdentificationCardType_p
PRINT 'IdentificationCardType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].IdentificationCardType_p WHERE ID BETWEEN 1 AND 4)
INSERT INTO dbo.IdentificationCardType_p( ID, Name_P, Name_S, Description )VALUES  (1,N'بطاقة شخصية',N'بطاقة شخصية',NULL)
,(2,N'جواز سفر',N'جواز سفر',NULL)
,(3,N'رخصة قيادة سيارة خاصة',N'رخصة قيادة سيارة خاصة',NULL)
,(4,N'رخصة قيادة دراجة نارية خاصة',N'رخصة قيادة دراجة نارية خاصة',NULL)
-- ///////////// END :: IdentificationCardType_p

-- ///////////// BEGIN :: InPatientAddmissionPricingType_p
PRINT 'InPatientAddmissionPricingType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].InPatientAddmissionPricingType_p WHERE ID BETWEEN 1 AND 2)
INSERT INTO dbo.InPatientAddmissionPricingType_p( ID, Name_P, Name_S, Description )VALUES  (1,N'إقامة المريض',N'إقامة المريض',NULL)
,(2,N'إقامة المرافق',N'إقامة المرافق',NULL)
-- ///////////// END :: InPatientAddmissionPricingType_p

-- ///////////// BEGIN :: InPatientRoomBedStatus_p
PRINT 'InPatientRoomBedStatus_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].InPatientRoomBedStatus_p WHERE ID BETWEEN 1 AND 5)
INSERT INTO dbo.InPatientRoomBedStatus_p( ID, Name_P, Name_S, Description )VALUES  (1,N'فارغ',N'فارغ',NULL)
,(2,N'شاغر',N'شاغر',NULL)
,(3,N'متوقف عن العمل',N'متوقف عن العمل',NULL)
,(4,N'ملغي',N'ملغي',NULL)
,(5,N'تحت الصيانة',N'تحت الصيانة',NULL)
-- ///////////// END :: InPatientRoomBedStatus_p

-- ///////////// BEGIN :: InPatientRoomType_p
PRINT 'InPatientRoomType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].InPatientRoomType_p WHERE ID BETWEEN 1 AND 5)
INSERT INTO dbo.InPatientRoomType_p( ID, Name_P, Name_S, Description )VALUES  (1,N'غرفة مفردة',N'غرفة مفردة',NULL)
,(2,N'غرفة مزدوجة',N'غرفة مزدوجة',NULL)
,(3,N'غسيل كلى',N'غسيل كلى',NULL)
,(4,N'رعاية مركزة',N'رعاية مركزة',NULL)
,(5,N'جناح',N'جناح',NULL)
-- ///////////// END :: InPatientRoomType_p

-- ///////////// BEGIN :: InPatientRoomType_p
PRINT 'InPatientRoomType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].InPatientRoomType_p WHERE ID BETWEEN 1 AND 5)
INSERT INTO dbo.InPatientRoomType_p( ID, Name_P, Name_S, Description )VALUES  (1,N'غرفة مفردة',N'غرفة مفردة',NULL)
,(2,N'غرفة مزدوجة',N'غرفة مزدوجة',NULL)
,(3,N'غسيل كلى',N'غسيل كلى',NULL)
,(4,N'رعاية مركزة',N'رعاية مركزة',NULL)
,(5,N'جناح',N'جناح',NULL)
-- ///////////// END :: InPatientRoomType_p

-- ///////////// BEGIN :: InvoicePaymentType_p
PRINT 'InvoicePaymentType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].InvoicePaymentType_p WHERE ID BETWEEN 1 AND 2)
INSERT INTO dbo.InvoicePaymentType_p( ID, Name_P, Name_S, Description )VALUES  (1,N'فاتورة نقدية',N'فاتورة نقدية',NULL)
,(2,N'فاتورة آجلة',N'فاتورة آجلة',NULL)
-- ///////////// END :: InvoicePaymentType_p

-- ///////////// BEGIN :: InvoiceType_p
PRINT 'InvoiceType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].InvoiceType_p WHERE ID BETWEEN 1 AND 4)
INSERT INTO dbo.InvoiceType_p( ID, Name_P, Name_S, Description )VALUES  (1,N'داخلى - حالة خاصة',N'داخلى - حالة خاصة',NULL)
,(2,N'خارجي - حالة خاصة',N'خارجي - حالة خاصة',NULL)
,(3,N'داخلي - حالة مستشفى',N'داخلي - حالة مستشفى',NULL)
,(4,N'داخلي - حالة مستشفى',N'داخلي - حالة مستشفى',NULL)
-- ///////////// END :: InvoiceType_p

-- ///////////// BEGIN :: MaritalStatus_p
PRINT 'MaritalStatus_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].MaritalStatus_p WHERE ID BETWEEN 1 AND 3)
INSERT INTO dbo.MaritalStatus_p( ID, Name_P, Name_S, Description )VALUES  (1,N'أعزب',N'أعزب',NULL)
,(2,N'متزوج',N'متزوج',NULL)
,(3,N'مطلق',N'مطلق',NULL)
-- ///////////// END :: MaritalStatus_p

-- ///////////// BEGIN :: PersonRelativeType_p
PRINT 'PersonRelativeType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].PersonRelativeType_p WHERE ID BETWEEN 1 AND 6)
INSERT INTO dbo.PersonRelativeType_p( ID, Name_P, Name_S, Decsription )VALUES  (1,N'الوالد',N'الوالد',NULL)
,(2,N'الوالدة',N'الوالدة',NULL)
,(3,N'الزوج',N'الزوج',NULL)
,(4,N'الزوجة',N'الزوجة',NULL)
,(5,N'الإبن',N'الإبن',NULL)
,(6,N'الإبنة',N'الإبنة',NULL)
-- ///////////// END :: PersonRelativeType_p

-- ///////////// BEGIN :: PersonTitle_p
PRINT 'PersonTitle_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].PersonTitle_p WHERE ID BETWEEN 1 AND 3)
INSERT INTO dbo.PersonTitle_p( ID, Name_P, Name_S, Description )VALUES  (1,N'Mr.',N'Mr.',NULL)
,(2,N'Mrs.',N'Mrs.',NULL)
,(3,N'Ms.',N'Ms.',NULL)
-- ///////////// END :: PersonTitle_p

-- ///////////// BEGIN :: PhoneType_p
PRINT 'PhoneType_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].PhoneType_p WHERE ID BETWEEN 1 AND 6)
INSERT INTO dbo.PhoneType_p( ID, Name_P, Name_S, Description )VALUES  (1,N'محمول خاص',N'محمول خاص',NULL)
,(2,N'محمول عمل',N'محمول عمل',NULL)
,(3,N'محمول آخر',N'محمول آخر',NULL)
,(4,N'تليفون منزل',N'تليفون منزل',NULL)
,(5,N'تليفون عمل',N'تليفون عمل',NULL)
,(6,N'تليفون آخر',N'تليفون آخر',NULL)
-- ///////////// END :: PhoneType_p

-- ///////////// BEGIN :: DoctorSpecialization_p
PRINT 'DoctorSpecialization_p'
IF NOT EXISTS (SELECT 1 FROM [dbo].DoctorSpecialization_p WHERE ID BETWEEN 1 AND 6)
INSERT INTO dbo.DoctorSpecialization_p( ID, Name_P, Name_S, Description )VALUES  (1,N'الأشعة الباطنية',N'Abdominal Radiology',NULL)
,(2,N'إدمان الطب النفسي',N'Addiction Psychiatry',NULL)
,(3,N'طب المراهقين',N'Adolescent Medicine ',NULL)
,(4,N'التخدير',N'Anesthesiology',NULL)
,(5,N'الحساسية والمناعة',N'Allergy & Immunology',NULL)
,(6,N'الأشعة القلبية',N'Cardiothoracic Radiology',NULL)

-- ///////////// END :: DoctorSpecialization_p

-- ///////////// BEGIN :: ServiceType_p
IF NOT EXISTS (SELECT 1 FROM [dbo].ServiceType_p WHERE ID BETWEEN 1 AND 6)
INSERT INTO dbo.ServiceType_p( ID ,  Name_P ,  Name_S ,  Description) VALUES  ( 1,N'كشف',N'كشف',null)
,( 2,N'عملية',N'عملية',null)
,( 3,N'فحوصات وأشعة',N'فحوصات وأشعة',null)
,( 4,N'تحليل ومعمل',N'تحليل ومعمل',null)
,( 5,N'إقامة',N'إقامة',null)
,( 6,N'أخرى',N'أخرى',null)

-- ///////////// END :: ServiceType_p

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
