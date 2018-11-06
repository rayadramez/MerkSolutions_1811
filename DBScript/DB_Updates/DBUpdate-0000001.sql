USE [MerkFinance]
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
SET NOEXEC OFF

--///////////////////////////////////////////////////////
--///////////////////////////////////////////////////////
--////////////// BEGIN UPDATE 1

PRINT N'Creating [dbo].[PatientDepositeBalance]'
GO
IF OBJECT_ID(N'[dbo].[PatientDepositeBalance]', 'U') IS NULL
CREATE TABLE [dbo].[PatientDepositeBalance]
(
[ID] [int] NOT NULL IDENTITY(1, 1),
[Patient_CU_ID] [int] NOT NULL,
[Service_CU_ID] [int] NOT NULL,
[Date] [datetime] NOT NULL,
[Blance] [float] NOT NULL,
[Factor] [int] NOT NULL,
[IsOnDuty] [bit] NOT NULL,
[InsertedBy] [int] NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_PatientDepositeBalance] on [dbo].[PatientDepositeBalance]'
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'PK_PatientDepositeBalance' AND object_id = OBJECT_ID(N'[dbo].[PatientDepositeBalance]'))
ALTER TABLE [dbo].[PatientDepositeBalance] ADD CONSTRAINT [PK_PatientDepositeBalance] PRIMARY KEY CLUSTERED  ([ID])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[PatientDepositeBalance]'
GO
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PatientDepositeBalance_Patient_cu]', 'F') AND parent_object_id = OBJECT_ID(N'[dbo].[PatientDepositeBalance]', 'U'))
ALTER TABLE [dbo].[PatientDepositeBalance] ADD CONSTRAINT [FK_PatientDepositeBalance_Patient_cu] FOREIGN KEY ([Patient_CU_ID]) REFERENCES [dbo].[Patient_cu] ([Person_CU_ID])
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PatientDepositeBalance_Service_cu]', 'F') AND parent_object_id = OBJECT_ID(N'[dbo].[PatientDepositeBalance]', 'U'))
ALTER TABLE [dbo].[PatientDepositeBalance] ADD CONSTRAINT [FK_PatientDepositeBalance_Service_cu] FOREIGN KEY ([Service_CU_ID]) REFERENCES [dbo].[Service_cu] ([ID])
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PatientDepositeBalance_User_cu]', 'F') AND parent_object_id = OBJECT_ID(N'[dbo].[PatientDepositeBalance]', 'U'))
ALTER TABLE [dbo].[PatientDepositeBalance] ADD CONSTRAINT [FK_PatientDepositeBalance_User_cu] FOREIGN KEY ([InsertedBy]) REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO

--////////////// END UPDATE 1
--///////////////////////////////////////////////////////
--//////////////////////////////////////////////////////

--///////////////////////////////////////////////////////
--///////////////////////////////////////////////////////
--////////////// BEGIN UPDATE 2

PRINT N'Dropping foreign keys from [dbo].[InvoicePayment]'
GO
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_InvoicePayment_Invoice]', 'F') AND parent_object_id = OBJECT_ID(N'[dbo].[InvoicePayment]', 'U'))
ALTER TABLE [dbo].[InvoicePayment] DROP CONSTRAINT [FK_InvoicePayment_Invoice]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[InvoicePayment]'
GO
IF COL_LENGTH(N'[dbo].[InvoicePayment]', N'Patient_CU_ID') IS NULL
ALTER TABLE [dbo].[InvoicePayment] ADD[Patient_CU_ID] [int] NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[InvoicePayment] ALTER COLUMN [InvoiceID] [int] NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[InvoicePayment]'
GO
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_InvoicePayment_Invoice]', 'F') AND parent_object_id = OBJECT_ID(N'[dbo].[InvoicePayment]', 'U'))
ALTER TABLE [dbo].[InvoicePayment] ADD CONSTRAINT [FK_InvoicePayment_Invoice] FOREIGN KEY ([InvoiceID]) REFERENCES [dbo].[Invoice] ([ID])
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_InvoicePayment_Patient_cu]', 'F') AND parent_object_id = OBJECT_ID(N'[dbo].[InvoicePayment]', 'U'))
ALTER TABLE [dbo].[InvoicePayment] ADD CONSTRAINT [FK_InvoicePayment_Patient_cu] FOREIGN KEY ([Patient_CU_ID]) REFERENCES [dbo].[Patient_cu] ([Person_CU_ID])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO

--////////////// END UPDATE 2
--///////////////////////////////////////////////////////
--//////////////////////////////////////////////////////


IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN
DECLARE @Version INT = 1
PRINT '>>>>>>>>>>>>>>> The database update succeeded to version: ' + 
	CAST(@Version AS VARCHAR(5))
IF EXISTS (SELECT * FROM dbo.DBVersion WHERE Version = @Version)
	DELETE dbo.DBVersion WHERE Version = @Version
	INSERT INTO dbo.DBVersion ( Version, Comment) VALUES
		( @Version, N'
			#UPD1:
				 - New :: PatientDepositeBalance Table
				 - New :: Add Patient iD in InvoicePayment
			'
			)

COMMIT TRANSACTION
END 
ELSE PRINT '>>>>>>>>>>>>>>> The database update failed'
GO
DROP TABLE #tmpErrors
GO