USE [master]
GO
/****** Object:  Database [MerkFinance]    Script Date: 31-Aug-18 20:16:16 ******/
CREATE DATABASE [MerkFinance]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MerkFinance', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.RAYADLAPSQLSRV\MSSQL\DATA\MerkFinance.mdf' , SIZE = 12288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MerkFinance_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.RAYADLAPSQLSRV\MSSQL\DATA\MerkFinance_log.ldf' , SIZE = 32448KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MerkFinance] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MerkFinance].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MerkFinance] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MerkFinance] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MerkFinance] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MerkFinance] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MerkFinance] SET ARITHABORT OFF 
GO
ALTER DATABASE [MerkFinance] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MerkFinance] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [MerkFinance] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MerkFinance] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MerkFinance] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MerkFinance] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MerkFinance] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MerkFinance] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MerkFinance] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MerkFinance] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MerkFinance] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MerkFinance] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MerkFinance] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MerkFinance] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MerkFinance] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MerkFinance] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MerkFinance] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MerkFinance] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MerkFinance] SET RECOVERY FULL 
GO
ALTER DATABASE [MerkFinance] SET  MULTI_USER 
GO
ALTER DATABASE [MerkFinance] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MerkFinance] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MerkFinance] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MerkFinance] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MerkFinance', N'ON'
GO
USE [MerkFinance]
GO
/****** Object:  User [merkuser]    Script Date: 31-Aug-18 20:16:16 ******/
CREATE USER [merkuser] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[db_accessadmin]
GO
ALTER ROLE [db_owner] ADD MEMBER [merkuser]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [merkuser]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [merkuser]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [merkuser]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [merkuser]
GO
ALTER ROLE [db_datareader] ADD MEMBER [merkuser]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [merkuser]
GO
ALTER ROLE [db_denydatareader] ADD MEMBER [merkuser]
GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [merkuser]
GO
/****** Object:  StoredProcedure [dbo].[GetBriefQueue]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetBriefQueue]
	@StationPointStage INT,
	@QueueManagerStatusID INT,
	@DoctorID INT,
	@InvoiceCreationDate DATETIME
AS
BEGIN
	SELECT
		QueueManager.ID AS QueueManagerID,  
		dbo.QueueManager.Patient_CU_ID AS PatientID,
		dbo.GetPatientFullName(dbo.Patient_cu.Person_CU_ID) AS PatientFullName,
		dbo.QueueManager.Doctor_CU_ID AS DoctorID,
		dbo.GetDoctorFullName(dbo.QueueManager.Doctor_CU_ID) AS DoctorFullName,
		Service_cu.Name_P AS ServiceName,
		QueueManager.InvoiceDetailID AS InvoiceDetailID,
		CAST(InvoiceDetail.Date AS DATETIME) AS ReservationTime,
		QueueManagerStatus_P_ID AS QueueStatusID
	FROM 
		dbo.QueueManager
		JOIN dbo.Patient_cu ON dbo.QueueManager.Patient_CU_ID = dbo.Patient_cu.Person_CU_ID
		JOIN dbo.Service_cu ON dbo.QueueManager.Service_CU_ID = dbo.Service_cu.ID
		JOIN dbo.Doctor_cu ON dbo.QueueManager.Doctor_CU_ID = dbo.Doctor_cu.Person_CU_ID
		JOIN dbo.InvoiceDetail ON dbo.QueueManager.InvoiceDetailID = dbo.InvoiceDetail.ID
		JOIN dbo.Invoice ON dbo.InvoiceDetail.InvoiceID = dbo.Invoice.ID
	WHERE 
		(@StationPointStage IS NULL OR StationPointStage_CU_ID = @StationPointStage)
		AND
		(@DoctorID IS NULL OR dbo.Doctor_cu.Person_CU_ID = @DoctorID) 
		AND
		(@InvoiceCreationDate IS NULL OR CAST(dbo.Invoice.InvoiceCreationDate AS DATE) = @InvoiceCreationDate)       
		AND
		dbo.InvoiceDetail.IsOnDuty = 1
		AND
		Invoice.IsOnDuty = 1
	ORDER BY
		CAST(InvoiceDetail.Date AS TIME)
END

GO
/****** Object:  StoredProcedure [dbo].[GetCustomerBalance]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCustomerBalance]
	@CustomerID INT,
	@InvoiceTypeID INT
AS
BEGIN
	SELECT 
	FinanceInvoice.Customer_CU_ID AS CustomerID,
	SUM(FinanceInvoiceShare.TotalRequestedAmount) AS TotalRequired,
	SUM(FinanceInvoiceShare.TotalPayment) AS TotalPayments,
	ReturningAmount = 
		CASE
			WHEN dbo.FinanceInvoicePayment.IsRemainingReturned = 1
			THEN SUM(FinanceInvoiceShare.TotalPayment) - SUM(FinanceInvoiceShare.TotalRequestedAmount)
		END,
	NetBalance = 
		CASE
			WHEN dbo.FinanceInvoicePayment.IsRemainingReturned = 1
			THEN SUM(FinanceInvoiceShare.TotalPayment) 
				- SUM(FinanceInvoiceShare.TotalRequestedAmount) 
				- (SUM(FinanceInvoiceShare.TotalPayment) 
				- SUM(FinanceInvoiceShare.TotalRequestedAmount))
		END
FROM 
	dbo.FinanceInvoice
	JOIN FinanceInvoiceShare ON dbo.FinanceInvoice.ID = dbo.FinanceInvoiceShare.FinanceInvoiceID
	JOIN FinanceInvoicePayment ON FinanceInvoicePayment.FinanceInvoiceID = dbo.FinanceInvoice.ID
WHERE
	(@InvoiceTypeID is NULL OR FinanceInvoice.InvoiceType_P_ID = @InvoiceTypeID)
	AND
	dbo.FinanceInvoice.IsOnDuty = 1
	AND
	(@CustomerID IS NULL OR FinanceInvoice.Customer_CU_ID = @CustomerID)

GROUP BY
	FinanceInvoice.Customer_CU_ID,
	dbo.FinanceInvoicePayment.IsRemainingReturned
END

GO
/****** Object:  StoredProcedure [dbo].[GetCustomerInvoices]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCustomerInvoices]
	@CustomerID INT,
	@IsOnDuty BIT,
	@InvoiceTypeID INT,
	@IsPaymentEnough BIT,
	@IsFinanciallyReviewed BIT,
	@IsFinanciallyCompleted BIT
AS
BEGIN
SELECT 
	dbo.FinanceInvoice.ID AS FinanceInvoiceID,
	FinanceInvoice.InvoiceType_P_ID AS InvoiceTypeID,
	FinanceInvoice.Customer_CU_ID AS CustomerID,
	FinanceInvoice.InvoiceSerial AS Serial,
	FinanceInvoice.IsPaymentsEnough AS IsPaymentEnough,
	FinanceInvoice.Description AS InvoiceDescription,
	FinanceInvoice.IsFinanciallyReviewed AS IsFinanciallyReviewed,
	CAST(FinanceInvoice.InvoiceCreationDate AS DATE) AS InvoiceCreationDate,
	SUM(dbo.FinanceInvoiceShare.TotalRequestedAmount) AS TotalRequestedAmount,
	SUM(dbo.FinanceInvoiceShare.TotalPayment) AS TotalPaymentsAmount,
	Requested_VS_Payment = 
		CASE 
			WHEN (dbo.FinanceInvoicePayment.IsRemainingReturned = 1 OR dbo.FinanceInvoice.IsPaymentsEnough = 1)
			THEN 0
			ELSE 
				CASE
					WHEN SUM(dbo.FinanceInvoiceShare.TotalRequestedAmount) - SUM(dbo.FinanceInvoiceShare.TotalPayment) = 0
					THEN 0
					ELSE SUM(dbo.FinanceInvoiceShare.TotalRequestedAmount) - SUM(dbo.FinanceInvoiceShare.TotalPayment)
				END
		END              
FROM
	FinanceInvoice
	JOIN dbo.FinanceInvoicePayment ON dbo.FinanceInvoice.ID = dbo.FinanceInvoicePayment.FinanceInvoiceID
	JOIN dbo.FinanceInvoiceShare ON dbo.FinanceInvoice.ID = dbo.FinanceInvoiceShare.FinanceInvoiceID
WHERE
	dbo.FinanceInvoice.IsOnDuty = 1
	AND
	(dbo.FinanceInvoice.IsCancelled IS NULL OR dbo.FinanceInvoice.IsCancelled = 0)  
	AND
	(@CustomerID IS NULL OR FinanceInvoice.Customer_CU_ID = @CustomerID)
	AND
	(@IsOnDuty IS NULL OR FinanceInvoice.IsOnDuty = @IsOnDuty)  
	AND
	(@InvoiceTypeID IS NULL OR FinanceInvoice.InvoiceType_P_ID = @InvoiceTypeID)
	AND
	(@IsPaymentEnough IS NULL OR FinanceInvoice.IsPaymentsEnough = @IsPaymentEnough)  
	AND
	(@IsFinanciallyReviewed IS NULL OR FinanceInvoice.IsFinanciallyReviewed = @IsFinanciallyReviewed)  
	AND
	(@IsFinanciallyCompleted IS NULL OR FinanceInvoice.IsFinanciallyCompleted = @IsFinanciallyCompleted)  
GROUP BY
	dbo.FinanceInvoice.ID,
	FinanceInvoice.InvoiceType_P_ID,
	FinanceInvoice.Customer_CU_ID,
	FinanceInvoice.InvoiceSerial,
	FinanceInvoice.IsPaymentsEnough,
	FinanceInvoice.Description,
	FinanceInvoice.IsFinanciallyReviewed,
	CAST(FinanceInvoice.InvoiceCreationDate AS DATE),
	dbo.FinanceInvoicePayment.IsRemainingReturned,
	dbo.FinanceInvoice.IsPaymentsEnough
END

GO
/****** Object:  StoredProcedure [dbo].[GetInvoiceForAddmission]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetInvoiceForAddmission] 
	@InvoiceCreationDateStart DATETIME,
	@InvoiceCreationDateEnd DATETIME,
	@InvoiceTypeID INT,
	@InvoiceIsOnDuty BIT,
	@InvoiceIsFinanciallyReviewed BIT,
	@InvoiceIsPrinted BIT,
	@InvoiceIsPaymentEnough BIT,
	@DoctorID INT,
	@PatientID INT
AS
BEGIN
	SELECT 
		Distinct(Person_cu.ID) AS PatientID,
		Person_cu.FirstName_P + ' ' + Person_cu.SecondName_P + ' ' + Person_cu.ThirdName_P + ' ' + Person_cu.FourthName_P AS PatientFullName,
		Invoice.ID AS InvoiceID, 
		Invoice.InvoiceCreationDate AS InvoiceCreationDate,
		Invoice.InvoiceSerial AS InvoiceSerial, 
		Invoice.PrintingDate AS InvoicePrintingDate,
		Doctor_cu.Person_CU_ID AS DoctorID, 
		dbo.GetDoctorFullName(Doctor_cu.Person_CU_ID) AS DoctorName,
		Invoice.IsPaymentsEnough AS IsPaymentEnough
	FROM 
		dbo.Invoice
		JOIN dbo.Person_cu ON dbo.Invoice.Patient_CU_ID = dbo.Person_cu.ID
		JOIN dbo.InvoiceType_p ON dbo.Invoice.InvoiceType_P_ID = dbo.InvoiceType_p.ID
		JOIN dbo.InvoiceDetail ON dbo.Invoice.ID = dbo.InvoiceDetail.InvoiceID
		JOIN dbo.Doctor_cu ON dbo.InvoiceDetail.Doctor_CU_ID = dbo.Doctor_cu.Person_CU_ID
	WHERE 
		(@InvoiceTypeID IS NULL OR Invoice.InvoiceType_P_ID = @InvoiceTypeID )
		AND Invoice.IsOnDuty = @InvoiceIsOnDuty
		AND Invoice.IsFinanciallyReviewed = @InvoiceIsFinanciallyReviewed
		AND Invoice.IsPrinted = @InvoiceIsPrinted
		AND (Invoice.IsFinanciallyCompleted IS NULL OR Invoice.IsFinanciallyCompleted = 0)
		AND (@InvoiceCreationDateStart IS NULL OR CAST(Invoice.InvoiceCreationDate AS DATE) BETWEEN @InvoiceCreationDateStart AND @InvoiceCreationDateEnd)
		AND (@PatientID IS NULL OR Invoice.Patient_CU_ID = @PatientID)
		AND (@InvoiceIsPaymentEnough IS NULL OR Invoice.IsPaymentsEnough = @InvoiceIsPaymentEnough)
		AND (@DoctorID IS NULL OR InvoiceDetail.Doctor_CU_ID = @DoctorID)
END

GO
/****** Object:  StoredProcedure [dbo].[GetSupplierBalance]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetSupplierBalance]
	@SupplierID INT,
	@InvoiceTypeID INT
AS
BEGIN
	SELECT 
	FinanceInvoice.Customer_CU_ID AS CustomerID,
	SUM(FinanceInvoiceShare.TotalRequestedAmount) AS TotalRequired,
	SUM(FinanceInvoiceShare.TotalPayment) AS TotalPayments,
	ReturningAmount = 
		CASE
			WHEN dbo.FinanceInvoicePayment.IsRemainingReturned = 1
			THEN SUM(FinanceInvoiceShare.TotalPayment) - SUM(FinanceInvoiceShare.TotalRequestedAmount)
		END
FROM 
	dbo.FinanceInvoice
	JOIN FinanceInvoiceShare ON dbo.FinanceInvoice.ID = dbo.FinanceInvoiceShare.FinanceInvoiceID
	JOIN FinanceInvoicePayment ON FinanceInvoicePayment.FinanceInvoiceID = dbo.FinanceInvoice.ID
WHERE
	(@InvoiceTypeID is NULL OR FinanceInvoice.InvoiceType_P_ID = @InvoiceTypeID)
	AND
	dbo.FinanceInvoice.IsOnDuty = 1
	AND
	(@SupplierID IS NULL OR FinanceInvoice.Supplier_CU_ID = @SupplierID)

GROUP BY
	FinanceInvoice.Customer_CU_ID,
	dbo.FinanceInvoicePayment.IsRemainingReturned
END

GO
/****** Object:  UserDefinedFunction [dbo].[GetCustomerFullName]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetCustomerFullName]
(
	@CustomerID INT 
)
RETURNS NVARCHAR(200)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Fullname NVARCHAR(200) = (SELECT Person_cu.FirstName_P + ' ' + Person_cu.SecondName_P + ' ' + Person_cu.ThirdName_P + ' ' + Person_cu.FourthName_P
	 FROM dbo.Customer_cu
	JOIN dbo.Person_cu ON dbo.Customer_cu.Person_CU_ID = dbo.Person_cu.ID
	WHERE Customer_cu.Person_CU_ID = @CustomerID)
	
	RETURN @Fullname

END

GO
/****** Object:  UserDefinedFunction [dbo].[GetDoctorFullName]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetDoctorFullName]
(
	@DoctorID int
)
RETURNS NVARCHAR(200)
AS
BEGIN

	DECLARE @FullName NVARCHAR(200) = (SELECT Person_cu.FirstName_P + ' ' + Person_cu.SecondName_P + ' ' + Person_cu.ThirdName_P + ' ' + Person_cu.FourthName_P
	FROM dbo.Doctor_cu
	JOIN dbo.Person_cu ON dbo.Doctor_cu.Person_CU_ID = dbo.Person_cu.ID
	WHERE dbo.Doctor_cu.Person_CU_ID = @DoctorID)
	RETURN @FullName
END

GO
/****** Object:  UserDefinedFunction [dbo].[GetPatientFullName]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetPatientFullName]
(
	@PatientID INT 
)
RETURNS NVARCHAR(200)
AS
BEGIN
	DECLARE @Fullname NVARCHAR(200) = (SELECT Person_cu.FirstName_P + ' ' + Person_cu.SecondName_P + ' ' + Person_cu.ThirdName_P + ' ' + Person_cu.FourthName_P
	 FROM Patient_cu
	JOIN dbo.Person_cu ON dbo.Patient_cu.Person_CU_ID = dbo.Person_cu.ID
	WHERE Patient_cu.Person_CU_ID = @PatientID)
	
	RETURN @Fullname

END

GO
/****** Object:  UserDefinedFunction [dbo].[GetSupplierFullName]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetSupplierFullName]
(
	@SupplierID INT 
)
RETURNS NVARCHAR(200)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Fullname NVARCHAR(200) = (SELECT Person_cu.FirstName_P + ' ' + Person_cu.SecondName_P + ' ' + Person_cu.ThirdName_P + ' ' + Person_cu.FourthName_P
	 FROM Supplier_cu
	JOIN dbo.Person_cu ON dbo.Supplier_cu.Person_CU_ID = dbo.Person_cu.ID
	WHERE Supplier_cu.Person_CU_ID = @SupplierID)
	
	RETURN @Fullname

END

GO
/****** Object:  UserDefinedFunction [dbo].[GetUserFullName]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetUserFullName]
(
	@PatientID INT
)
RETURNS NVARCHAR(200)
AS
BEGIN
	DECLARE @Fullname NVARCHAR(200) = (SELECT Person_cu.FirstName_P + ' ' + Person_cu.SecondName_P + ' ' + Person_cu.ThirdName_P + ' ' + Person_cu.FourthName_P
	 FROM dbo.User_cu
	JOIN dbo.Person_cu ON dbo.User_cu.Person_CU_ID = dbo.Person_cu.ID
	WHERE User_cu.Person_CU_ID = @PatientID)
	
	RETURN @Fullname

END

GO
/****** Object:  Table [dbo].[AccountingJournalEntryTransaction]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountingJournalEntryTransaction](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[Serial] [nvarchar](200) NOT NULL,
	[AccountingJournalTransaction_ID] [int] NOT NULL,
	[ChartOfAccount_CU_ID] [bigint] NOT NULL,
	[IsDebit] [bit] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_AccountingJournalEntryTransaction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AccountingJournalTransaction]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountingJournalTransaction](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[JounralSerial] [nvarchar](200) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[TransactionAmount] [float] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[FinancialTransactionType_P_ID] [int] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_AccountingJournalTransaction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ActiveSalaryEffect_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActiveSalaryEffect_cu](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[Name_EN] [nvarchar](150) NOT NULL,
	[Name_AR] [nvarchar](150) NULL,
	[EffectiveFactor] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [ntext] NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_ActiveSalaryEffect_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Address_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address_cu](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[Country_CU_ID] [int] NULL,
	[City_CU_ID] [int] NULL,
	[Region_CU_ID] [int] NULL,
	[Territory_CU_ID] [int] NULL,
	[BuildingNumber] [int] NULL,
	[FloorNumber] [int] NULL,
	[ZipCode] [nvarchar](50) NULL,
	[AddressLine1] [nvarchar](200) NULL,
	[AddressLine2] [nvarchar](200) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_Address_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AddressRefrenceType_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddressRefrenceType_cu](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[Name_P] [nvarchar](50) NOT NULL,
	[Name_S] [nvarchar](50) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [ntext] NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_AddressRefrenceType_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Application_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Application_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](50) NOT NULL,
	[Name_S] [nvarchar](50) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_Application_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Bank_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bank_cu](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](50) NOT NULL,
	[Name_S] [nvarchar](50) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](50) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_Bank_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BankAccount_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankAccount_cu](
	[ID] [int] NOT NULL,
	[Bank_CU_ID] [int] NOT NULL,
	[Name_P] [nvarchar](50) NOT NULL,
	[Name_S] [nvarchar](50) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](50) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_BankAccount_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CashBox_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CashBox_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[InternalCode] [nvarchar](50) NULL,
	[IsMain] [bit] NOT NULL,
	[Floor_CU_ID] [int] NULL,
	[ChartOfAccount_CU_ID] [bigint] NULL,
	[Description] [nvarchar](150) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NOT NULL,
 CONSTRAINT [PK_CashBox_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CashBoxInOutTransaction]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CashBoxInOutTransaction](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TranscationDate] [datetime] NOT NULL,
	[CashBoxTransactionType_P_ID] [int] NOT NULL,
	[ChartOfAccount_CU_ID] [bigint] NOT NULL,
	[GeneralChartOfAccountType_CU_ID] [int] NOT NULL,
	[TransactionAmount] [float] NOT NULL,
	[PaymentType_P_ID] [int] NOT NULL,
	[CashBox_CU_ID] [int] NULL,
	[Bank_CU_ID] [int] NULL,
	[BankAccount_CU_ID] [int] NULL,
	[Currency_CU_ID] [int] NOT NULL,
	[CurrencyExchangeRate] [float] NULL,
	[TranscationSerial] [nvarchar](150) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](150) NULL,
	[InsertedBy] [int] NOT NULL,
	[IsCancelled] [bit] NOT NULL,
	[CancelledBy] [int] NULL,
	[CancellationDate] [datetime] NULL,
	[CancellationReason] [nvarchar](150) NULL,
 CONSTRAINT [PK_CashBoxInOutTransaction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CashBoxInOutTransaction_AccountingJournalTransaction]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CashBoxInOutTransaction_AccountingJournalTransaction](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CashBoxInOutTransactionID] [int] NOT NULL,
	[AccountingJournalTransactionID] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NOT NULL,
 CONSTRAINT [PK_CashBoxInOutTransaction_AccountingJournalTransaction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CashBoxTransactionType_GeneralChartOfAccountType_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CashBoxTransactionType_GeneralChartOfAccountType_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CashBoxTransactionType_P_ID] [int] NOT NULL,
	[GeneralChartOfAccountType_CU_ID] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NOT NULL,
 CONSTRAINT [PK_CashBoxTransactionType_GeneralChartOfAccountType_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CashBoxTransactionType_GeneralChartOfAccountType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CashBoxTransactionType_GeneralChartOfAccountType_p](
	[ID] [int] NOT NULL,
	[CashBoxTransactionType_P_ID] [int] NOT NULL,
	[GeneralChartOfAccountType_P_ID] [int] NOT NULL,
	[Description] [nvarchar](150) NULL,
 CONSTRAINT [PK_CashBoxTransactionType_GeneralChartOfAccountType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CashBoxTransactionType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CashBoxTransactionType_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[Description] [nvarchar](150) NULL,
	[MultiplierEffect] [int] NOT NULL,
 CONSTRAINT [PK_CashBoxTransactionType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChartOfAccount_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChartOfAccount_cu](
	[ID] [bigint] IDENTITY(1,2) NOT NULL,
	[ChartOfAccount_CU_ID] [bigint] NULL,
	[Serial] [bigint] NOT NULL,
	[ChartOfAccountingType_CU_ID] [bigint] NOT NULL,
	[Name_P] [nvarchar](200) NOT NULL,
	[Name_S] [nvarchar](200) NULL,
	[IsDebit] [bit] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_ChartOfAccounting_cu_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChartOfAccount_GeneralChartOfAccountType_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChartOfAccount_GeneralChartOfAccountType_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ChartOfAccount_CU_ID] [bigint] NOT NULL,
	[GeneralChartOfAccountType_CU_ID] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NOT NULL,
 CONSTRAINT [PK_ChartOfAccount_GeneralChartOfAccountType_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChartOfAccountMargin_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChartOfAccountMargin_cu](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[ChartOfAccountingMargin_P_ID] [int] NOT NULL,
	[NumberOfDigits] [int] NOT NULL,
	[Name_P] [nvarchar](200) NOT NULL,
	[Name_S] [nvarchar](200) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_ChartOfAccountDigitLevel_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChartOfAccountMargin_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChartOfAccountMargin_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](50) NOT NULL,
	[Name_S] [nvarchar](50) NULL,
 CONSTRAINT [PK_ChartOfAccountMargin_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChartOfAccountType_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChartOfAccountType_cu](
	[ID] [bigint] IDENTITY(1,2) NOT NULL,
	[ChartOfAccounting_P_ID] [int] NULL,
	[Serial] [nvarchar](100) NULL,
	[Name_P] [nvarchar](200) NOT NULL,
	[Name_S] [nvarchar](200) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_ChartOfAccounting_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChartOfAccountType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChartOfAccountType_p](
	[ID] [int] NOT NULL,
	[Serial] [nvarchar](200) NULL,
	[Name_EN] [nvarchar](200) NOT NULL,
	[Name_AR] [nvarchar](200) NULL,
	[Description] [nvarchar](200) NULL,
 CONSTRAINT [PK_ChartOfAccountingType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[City_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City_cu](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[Country_CU_ID] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [ntext] NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_City_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CommonTransactionType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommonTransactionType_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](50) NOT NULL,
	[Name_S] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_CommonTransactionType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Country_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country_cu](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [ntext] NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_Country_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Currency_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currency_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[InternalCode] [nvarchar](150) NULL,
	[Country_CU_ID] [int] NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_Currency_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customer_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer_cu](
	[Person_CU_ID] [int] NOT NULL,
	[CustomerType_P_ID] [int] NULL,
	[Description] [nvarchar](150) NULL,
	[InsertedBy] [int] NULL,
	[InternalCode] [nvarchar](50) NULL,
 CONSTRAINT [PK_Customer_cu] PRIMARY KEY CLUSTERED 
(
	[Person_CU_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CustomerPaymentTransaction]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerPaymentTransaction](
	[ID] [int] NOT NULL,
	[Customer_CU_ID] [int] NULL,
	[FinanceInvoiceID] [int] NULL,
	[TotalRequired] [float] NULL,
	[TotalPayment] [float] NULL,
	[Date] [datetime] NULL,
	[IsPaymentEnough] [bit] NULL,
	[AccountingJournalTransactionID] [int] NULL,
	[InsertedBy] [int] NULL,
	[IsOnDuty] [bit] NULL,
 CONSTRAINT [PK_CustomerPaymentTransaction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CustomerType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerType_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[Description] [ntext] NULL,
 CONSTRAINT [PK_CustomerType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DBVersion]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DBVersion](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Comment] [ntext] NOT NULL,
 CONSTRAINT [PK_DBVersion] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Department_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department_cu](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[ParentDepartment_CU_ID] [int] NULL,
	[Department_P_ID] [int] NULL,
	[Manager_CU_ID] [int] NULL,
	[Location_CU_ID] [int] NULL,
	[Description] [ntext] NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_Department_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Department_JobTitle_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department_JobTitle_cu](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[Department_CU_ID] [int] NOT NULL,
	[JobTitle_CU_ID] [int] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_Department_JobTitle_CU_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Department_JobTitle_WorkingShiftTitle_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department_JobTitle_WorkingShiftTitle_cu](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[Department_CU_ID] [int] NOT NULL,
	[JobTitle_CU_ID] [int] NOT NULL,
	[WorkingShiftTitle_CU_ID] [int] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_Department_WorkingShiftTitle_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DepartmentType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DepartmentType_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[Description] [ntext] NULL,
 CONSTRAINT [PK_DepartmentType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DiscountType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiscountType_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[Description] [ntext] NULL,
 CONSTRAINT [PK_DiscountType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Doctor_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctor_cu](
	[Person_CU_ID] [int] NOT NULL,
	[DoctorSpecialization_P_ID] [int] NULL,
	[DoctorCategory_CU_ID] [int] NULL,
	[Description] [nvarchar](150) NULL,
	[IsInternal] [bit] NULL,
	[DoctorTaxType_CU_ID] [int] NULL,
	[DoctorProfessionalFeesIssuingType_P_ID] [int] NULL,
	[PrivateMobile] [nvarchar](50) NULL,
	[InsertedBy] [int] NULL,
	[DoctorRank_P_ID] [int] NULL,
	[InternalCode] [nvarchar](50) NULL,
 CONSTRAINT [PK_Doctor_cu] PRIMARY KEY CLUSTERED 
(
	[Person_CU_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Doctor_Service_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctor_Service_cu](
	[ID] [int] NOT NULL,
	[Doctor_CU_ID] [int] NOT NULL,
	[DoctorCategory_CU_ID] [int] NULL,
	[Service_CU_ID] [int] NULL,
	[ServiceCartegory_CU_ID] [int] NULL,
	[ServiceType_P_ID] [int] NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_Doctor_Service_CU] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DoctorCategory_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoctorCategory_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name_P] [nvarchar](150) NULL,
	[Name_S] [nvarchar](150) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](150) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_DoctorCategory_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DoctorFeesService_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoctorFeesService_cu](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[Price] [float] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nchar](10) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_DoctorFeesService_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DoctorProfessionalFeesIssuingType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoctorProfessionalFeesIssuingType_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](50) NOT NULL,
	[Name_S] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_DoctorProfessionalFeesIssuingType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DoctorRank_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoctorRank_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](50) NOT NULL,
	[Name_S] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_DoctorRank_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DoctorSpecialization_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoctorSpecialization_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](150) NULL,
	[Name_S] [nvarchar](150) NULL,
	[Description] [nvarchar](150) NULL,
 CONSTRAINT [PK_DoctorSpecialization_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DoctorTaxType_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoctorTaxType_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[TaxPercent] [float] NOT NULL,
	[Description] [nvarchar](150) NULL,
	[InsertedBy] [int] NULL,
	[IsOnDuty] [bit] NOT NULL,
 CONSTRAINT [PK_DoctorTaxType_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employee_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee_cu](
	[Person_CU_ID] [int] NOT NULL,
	[EmployeeType_CU_ID] [int] NULL,
	[AbbreviationName] [nvarchar](50) NULL,
	[MilitaryStatus_P_ID] [int] NOT NULL,
	[Email] [nvarchar](150) NULL,
	[EmploymentDate_CU_ID] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[RetirementDate] [datetime] NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_Employee_cu] PRIMARY KEY CLUSTERED 
(
	[Person_CU_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employee_Department_JobTitle_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee_Department_JobTitle_cu](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[Employee_CU_ID] [int] NOT NULL,
	[Department_CU_ID] [int] NOT NULL,
	[JobTitle_CU_ID] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](150) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_Employee_Department_JobTitle_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employee_WorkingShift_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee_WorkingShift_cu](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[Employee_CU_ID] [int] NOT NULL,
	[WorkingShift_CU_ID] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [ntext] NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_Employee_WorkingShift_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employee_WorkingShiftTitle_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee_WorkingShiftTitle_cu](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[Employee_CU_ID] [int] NOT NULL,
	[WorkingShiftTitle_CU_ID] [int] NOT NULL,
	[JobTitle_CU_ID] [int] NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [ntext] NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_Employee_WorkingShiftTitle_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmployeeType_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeType_cu](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[Name_P] [nvarchar](50) NOT NULL,
	[Name_S] [nvarchar](50) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [ntext] NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_EmployeeType_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmploymentDate_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmploymentDate_cu](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[Date] [datetime] NOT NULL,
	[EmploymentDateType_P_ID] [int] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_EmploymentDate_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmploymentDateType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmploymentDateType_p](
	[ID] [int] NOT NULL,
	[Name_EN] [nvarchar](50) NOT NULL,
	[Name_AR] [nvarchar](50) NULL,
	[Description] [ntext] NULL,
 CONSTRAINT [PK_EmploymentDateType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FinanceInvoice]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FinanceInvoice](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceType_P_ID] [int] NOT NULL,
	[InvoicePaymentType_P_ID] [int] NOT NULL,
	[InvoiceCreationDate] [datetime] NOT NULL,
	[Customer_CU_ID] [int] NULL,
	[Supplier_CU_ID] [int] NULL,
	[IsPrinted] [bit] NOT NULL,
	[InvoiceSerial] [nvarchar](200) NULL,
	[PrintingDate] [datetime] NULL,
	[IsPaymentsEnough] [bit] NOT NULL,
	[Description] [nvarchar](150) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[IsFinanciallyReviewed] [bit] NULL,
	[IsFinanciallyCompleted] [bit] NULL,
	[IsCancelled] [bit] NOT NULL,
	[CancelledBy] [int] NULL,
	[CancellationDate] [datetime] NULL,
	[IsSuspended] [bit] NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_FinanceInvoice] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FinanceInvoiceDetail]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FinanceInvoiceDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FinanceInvoiceID] [int] NOT NULL,
	[ParentInvoiceDetailID] [int] NULL,
	[InventoryHousing_CU_ID] [int] NULL,
	[InventoryItem_CU_ID] [int] NULL,
	[PricePerUnit] [float] NULL,
	[UnitMeasurment_CU_ID] [int] NULL,
	[Quantity] [float] NULL,
	[Date] [datetime] NOT NULL,
	[DiscountAmount] [float] NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[IsSurchargeApplied] [bit] NOT NULL,
	[SurchargeAmount] [float] NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_FinanceInvoiceDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FinanceInvoicePayment]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FinanceInvoicePayment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FinanceInvoiceID] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Amount] [float] NOT NULL,
	[PaymentType_P_ID] [int] NOT NULL,
	[IsRemainingReturned] [bit] NULL,
	[PaymentSerial] [nvarchar](150) NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](150) NULL,
	[InsertedBy] [int] NOT NULL,
 CONSTRAINT [PK_FinanceInvoicePayment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FinanceInvoiceShare]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FinanceInvoiceShare](
	[FinanceInvoiceID] [int] NOT NULL,
	[TotalRequestedAmount] [float] NULL,
	[TotalPayment] [float] NOT NULL,
	[IsSurchargeApplied] [bit] NOT NULL,
	[TotalSurchargeAccummulativePercentage] [float] NULL,
	[IsStampApplied] [bit] NOT NULL,
	[TotalStampAmount] [float] NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_FinanceInvoiceShare] PRIMARY KEY CLUSTERED 
(
	[FinanceInvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FinancialInterval_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FinancialInterval_cu](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[Name_P] [nvarchar](200) NOT NULL,
	[Name_S] [nvarchar](200) NULL,
	[StartDate] [datetime] NOT NULL,
	[EdnDate] [datetime] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_FinancialInterval_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FinancialInterval_Month_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FinancialInterval_Month_cu](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[FinancialInterval_CU_ID] [int] NOT NULL,
	[Month_P_ID] [int] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_FinancialInterval_Month_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FinancialTransactionType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FinancialTransactionType_p](
	[ID] [int] NOT NULL,
	[Name_EN] [nvarchar](200) NOT NULL,
	[Name_AR] [nvarchar](200) NULL,
	[Description] [nvarchar](200) NULL,
 CONSTRAINT [PK_FinancialTransactionType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Floor_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Floor_cu](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[Name_P] [nvarchar](100) NOT NULL,
	[Name_S] [nvarchar](100) NULL,
	[InternalCode] [nvarchar](50) NULL,
	[ShortName] [nvarchar](50) NULL,
	[Location_CU_ID] [int] NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [ntext] NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_Floor_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GeneralChartOfAccountType_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GeneralChartOfAccountType_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[GeneralChartOfAccountType_P_ID] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](150) NOT NULL,
	[InsertedBy] [int] NOT NULL,
 CONSTRAINT [PK_GeneralChartOfAccountType_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GeneralChartOfAccountType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GeneralChartOfAccountType_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[Description] [nvarchar](150) NULL,
 CONSTRAINT [PK_GeneralChartOfAccountType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentificationCardType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentificationCardType_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](50) NOT NULL,
	[Name_S] [nvarchar](50) NULL,
	[Description] [ntext] NULL,
 CONSTRAINT [PK_IdentificationCardsType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ImageType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImageType_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](50) NOT NULL,
	[Name_s] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_ImageType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InPatientAdmissionPricingType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InPatientAdmissionPricingType_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](50) NULL,
	[Name_S] [nvarchar](50) NULL,
	[Description] [ntext] NULL,
 CONSTRAINT [PK_InPatientAddmissionPricingType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InPatientRoom_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InPatientRoom_cu](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[InternalCode] [nvarchar](50) NULL,
	[Floor_CU_ID] [int] NOT NULL,
	[InPatientRoomClassification_CU_ID] [int] NOT NULL,
	[ShortName] [nvarchar](50) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [ntext] NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InPatientRoom_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InPatientRoom_InPatientAdmissionPricingType_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InPatientRoom_InPatientAdmissionPricingType_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InPatientRoom_CU_ID] [int] NOT NULL,
	[InPatientAddmissionPricingType_P_ID] [int] NOT NULL,
	[PricePerDay] [float] NOT NULL,
	[MinimumAddmissionAmount] [float] NULL,
	[InsuranceCarrier_InsuranceLevel_CU_ID] [int] NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InPatientRoom_InPatientAddmissionPricingType_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InPatientRoomBed_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InPatientRoomBed_cu](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[Name_P] [nvarchar](150) NULL,
	[Name_S] [nvarchar](150) NULL,
	[InPatientRoom_CU_ID] [int] NOT NULL,
	[InPatientRoomBedStatus_P_ID] [int] NOT NULL,
	[InternalCode] [nvarchar](50) NULL,
	[ShortName] [nvarchar](50) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [ntext] NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InPatientRoomBed_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InPatientRoomBed_InPatientAdmissionPricingType_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InPatientRoomBed_InPatientAdmissionPricingType_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InPatientRoomBed_CU_ID] [int] NOT NULL,
	[InPatientAddmissionPricingType_P_ID] [int] NOT NULL,
	[PricePerDay] [float] NOT NULL,
	[MinimumAddmissionAmount] [float] NULL,
	[InsuranceCarrier_InsuranceLevel_CU_ID] [int] NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InPatientRoomBed_InPatientAddmissionPricingType_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InPatientRoomBedStatus_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InPatientRoomBedStatus_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](50) NOT NULL,
	[Name_S] [nvarchar](50) NULL,
	[Description] [ntext] NULL,
 CONSTRAINT [PK_InPatientRoomBedStatus_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InPatientRoomClassification_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InPatientRoomClassification_cu](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[InPatientRoomType_P_ID] [int] NOT NULL,
	[InternalCode] [nvarchar](50) NULL,
	[ShortName] [nvarchar](50) NULL,
	[DefaultPricePerDay] [float] NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [ntext] NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InPatientRoomClassification_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InPatientRoomClassification_InPatientAdmissionPricingType_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InPatientRoomClassification_InPatientAdmissionPricingType_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InPatientRoomClassification_CU_ID] [int] NOT NULL,
	[InPatientAddmissionPricingType_P_ID] [int] NOT NULL,
	[PricePerDay] [float] NOT NULL,
	[MinimumAddmissionAmount] [float] NULL,
	[InsuranceCarrier_InsuranceLevel_CU_ID] [int] NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InPatientRoomClassification_InPatientAddmissionPricingType_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InPatientRoomType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InPatientRoomType_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[Description] [ntext] NULL,
 CONSTRAINT [PK_RoomType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InsuranceCarrier_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InsuranceCarrier_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name_P] [nvarchar](200) NOT NULL,
	[Name_S] [nvarchar](200) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InsuranceCarrier_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InsuranceCarrier_InsuranceLevel_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InsuranceCarrier_InsuranceLevel_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InsuranceCarrier_CU_ID] [int] NOT NULL,
	[InsuranceLevel_CU_ID] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsurancePercentage] [float] NOT NULL,
	[PatientMaxAmount] [float] NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InsuranceCarrier_InsuranceLevel_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InsuranceLevel_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InsuranceLevel_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name_P] [nvarchar](200) NOT NULL,
	[Name_S] [nvarchar](200) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InsuranceLevel_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InventoryHousing_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryHousing_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[Floor_CU_ID] [int] NULL,
	[IsMain] [bit] NOT NULL,
	[InternalCode] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](150) NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InventoryHousing_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InventoryItem_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryItem_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[InventoryHousing_CU_ID] [int] NULL,
	[InventoryItemCategory_CU_ID] [int] NULL,
	[InventoryItemBrand_CU_ID] [int] NULL,
	[InventoryTrackingUnitMeasurment_CU_ID] [int] NULL,
	[InventoryItemType_P_ID] [int] NULL,
	[InternalCode] [nvarchar](150) NULL,
	[DefaultBarcode] [nvarchar](200) NULL,
	[DefaultSellingPrice] [float] NULL,
	[DefaultCost] [float] NULL,
	[RorderedPoint] [float] NULL,
	[StockMinLevel] [float] NULL,
	[StockMaxLevel] [float] NULL,
	[AcceptOverrideMinAmount] [bit] NULL,
	[CanBeSold] [bit] NULL,
	[IsAvailable] [bit] NULL,
	[AcceptPartingSelling] [bit] NULL,
	[IsCountable] [bit] NULL,
	[SellingStartDate] [datetime] NULL,
	[SellingEndDate] [datetime] NULL,
	[ExpirationDate] [datetime] NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](150) NULL,
	[InsertedBy] [int] NULL,
	[IsTaxable] [bit] NULL,
	[IsSurcharge] [bit] NULL,
 CONSTRAINT [PK_InventoryItem_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InventoryItem_UnitMeasurment_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryItem_UnitMeasurment_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InventoryItem_CU_ID] [int] NOT NULL,
	[UnitMeasurment_CU_ID] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NULL,
	[IsInventoryTracking] [bit] NULL,
 CONSTRAINT [PK_InventoryItem_UnitMeasurment_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InventoryItemBrand_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryItemBrand_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](150) NULL,
	[InternalCode] [nvarchar](50) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InventoryItemBrand_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InventoryItemCategory_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryItemCategory_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](150) NULL,
	[InsertedBy] [int] NULL,
	[InternalCode] [nvarchar](50) NULL,
 CONSTRAINT [PK_InventoryItemCategory_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InventoryItemGroup_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryItemGroup_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name_P] [nvarchar](150) NULL,
	[Name_S] [nvarchar](150) NULL,
	[InternalCode] [nvarchar](150) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](150) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InvetoryItemGroup_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InventoryItemGroup_InventoryItem_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryItemGroup_InventoryItem_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InvetoryItemGroup_CU_ID] [int] NOT NULL,
	[InventoryItem_CU_ID] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
 CONSTRAINT [PK_InventoryItemGroup_InventoryItem_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InventoryItemPrice_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryItemPrice_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InventoryItem_CU_ID] [int] NOT NULL,
	[InventoryItem_UnitMeasurment_CU_ID] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Price] [float] NOT NULL,
	[Customer_CU_ID] [int] NULL,
	[PriceType_P_ID] [int] NOT NULL,
	[Supplier_CU_ID] [int] NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InventoryItemPrice_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InventoryItemTransaction]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryItemTransaction](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InventoryItem_CU_ID] [int] NOT NULL,
	[InventoryHousing_CU_ID] [int] NOT NULL,
	[Quantity] [float] NOT NULL,
	[TransactionFactor] [int] NOT NULL,
	[UnitMeasurment_CU_ID] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[InventoryItemTransactionType_P_ID] [int] NULL,
	[ExpirationDate] [datetime] NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InventoryItem_InventoryHousing_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InventoryItemTransactionType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryItemTransactionType_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](50) NOT NULL,
	[Name_S] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_InventoryTransactionType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InventoryItemType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryItemType_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[Organization_P_ID] [int] NULL,
	[Description] [nvarchar](150) NULL,
 CONSTRAINT [PK_InventoryItemType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceType_P_ID] [int] NOT NULL,
	[InvoicePaymentType_P_ID] [int] NOT NULL,
	[InvoiceCreationDate] [datetime] NOT NULL,
	[Patient_CU_ID] [int] NOT NULL,
	[IsPrinted] [bit] NOT NULL,
	[InvoiceSerial] [nvarchar](200) NULL,
	[PrintingDate] [datetime] NULL,
	[IsPaymentsEnough] [bit] NOT NULL,
	[Description] [nvarchar](150) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[IsFinanciallyReviewed] [bit] NULL,
	[IsMedicallyDone] [bit] NULL,
	[IsFinanciallyCompleted] [bit] NULL,
	[IsCancelled] [bit] NOT NULL,
	[CancelledBy] [int] NULL,
	[CancellationDate] [datetime] NULL,
	[IsSuspended] [bit] NULL,
	[InvoiceCostingStrategy_P_ID] [int] NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InvoiceHeader_tr] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InvoiceCostingStrategy_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceCostingStrategy_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[Description] [nvarchar](150) NULL,
 CONSTRAINT [PK_InvoiceCostingStrategy_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InvoiceDetail]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceID] [int] NOT NULL,
	[ParentInvoiceDetailID] [int] NULL,
	[Service_CU_ID] [int] NULL,
	[Date] [datetime] NOT NULL,
	[Doctor_CU_ID] [int] NULL,
	[Count] [float] NOT NULL,
	[PatientShare] [float] NOT NULL,
	[InsuranceShare] [float] NOT NULL,
	[DiscountType_P_ID] [int] NULL,
	[PatientShareDiscount] [float] NULL,
	[IsInsuranceApplied] [bit] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[IsSurchargeApplied] [bit] NOT NULL,
	[SurchargeAmount] [float] NULL,
	[Description] [nvarchar](200) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InvoiceDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InvoiceDetail_Accommodation]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceDetail_Accommodation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceDetailID] [int] NOT NULL,
	[PatientShare] [float] NOT NULL,
	[InsuranceShare] [float] NOT NULL,
	[PatientShareDiscount] [float] NULL,
	[IsInsuranceApplied] [bit] NOT NULL,
	[DiscountType_P_ID] [int] NULL,
	[StartDate] [datetime] NOT NULL,
	[ExitDate] [datetime] NULL,
	[InPatientRoom_CU_ID] [int] NOT NULL,
	[InPatientRoomBed_CU_ID] [int] NOT NULL,
	[InPatientRoomClassification_CU_ID] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[IsSurchargeApplied] [bit] NOT NULL,
	[SurchargeAmount] [float] NULL,
	[Description] [nvarchar](200) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InvoiceDetail_Accommodation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InvoiceDetail_DoctorFees]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceDetail_DoctorFees](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceDetailID] [int] NOT NULL,
	[Doctor_CU_ID] [int] NOT NULL,
	[DoctorFeesService_CU_ID] [int] NOT NULL,
	[PatientShare] [float] NOT NULL,
	[InsuranceShare] [float] NOT NULL,
	[PatientShareDiscount] [float] NULL,
	[IsInsuranceApplied] [bit] NOT NULL,
	[DiscountType_P_ID] [int] NULL,
	[IsOnDuty] [bit] NOT NULL,
	[IsSurchargeApplied] [bit] NOT NULL,
	[SurchargeAmount] [float] NULL,
	[Description] [nvarchar](200) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InvoiceDetail_DoctorFees] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InvoiceDetail_Inventory]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceDetail_Inventory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceDetailID] [int] NOT NULL,
	[InventoryHousing_CU_ID] [int] NOT NULL,
	[InventoryItem_CU_ID] [int] NOT NULL,
	[Count] [float] NULL,
	[PatientShare] [float] NOT NULL,
	[InsuranceShare] [float] NOT NULL,
	[PatientShareDiscount] [float] NOT NULL,
	[IsInsuranceApplied] [bit] NOT NULL,
	[DiscountType_P_ID] [int] NULL,
	[IsOnDuty] [bit] NOT NULL,
	[IsSurchargeApplied] [bit] NOT NULL,
	[SurchargeAmount] [float] NULL,
	[Description] [nvarchar](200) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InvoiceDetail_Inventory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InvoiceDiscount]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceDiscount](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceID] [int] NOT NULL,
	[DisountType_P_ID] [int] NOT NULL,
	[DiscountAmount] [float] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](150) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InvoiceDiscount] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InvoicePayment]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoicePayment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceID] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Amount] [float] NOT NULL,
	[PaymentType_P_ID] [int] NOT NULL,
	[PaymentSerial] [nvarchar](100) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InvoicePayment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InvoicePayment_Check]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoicePayment_Check](
	[InvoicePaymentID] [int] IDENTITY(1,1) NOT NULL,
	[Bank_CU_ID] [int] NOT NULL,
	[BankAccoumt_CU_ID] [int] NULL,
	[IssueDate] [datetime] NULL,
	[ExchangeDate] [datetime] NULL,
	[CheckNumber] [nvarchar](200) NULL,
	[Description] [nchar](10) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InvoicePayment_Check_1] PRIMARY KEY CLUSTERED 
(
	[InvoicePaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InvoicePayment_Visa]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoicePayment_Visa](
	[InvoicePaymentID] [int] IDENTITY(1,1) NOT NULL,
	[Bank_CU_ID] [int] NULL,
	[BankAccount_CU_ID] [int] NULL,
	[CreditCardNumber] [nvarchar](150) NULL,
	[Description] [nvarchar](200) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InvoicePyament_Visa_1] PRIMARY KEY CLUSTERED 
(
	[InvoicePaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InvoicePaymentType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoicePaymentType_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[Description] [ntext] NULL,
 CONSTRAINT [PK_InvoicePaymentType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InvoiceRequestedAmount]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceRequestedAmount](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceID] [int] NULL,
	[Date] [datetime] NULL,
	[RequestedAmount] [float] NULL,
	[IsOnDuty] [bit] NULL,
	[Description] [nvarchar](50) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InvoiceRequestedAmount] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InvoiceShare]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceShare](
	[InvoiceID] [int] NOT NULL,
	[TotalRequestedAmount] [float] NULL,
	[TotalPatientShare] [float] NOT NULL,
	[TotalInsuranceShare] [float] NOT NULL,
	[TotalPayment] [float] NOT NULL,
	[IsInsuranceApplied] [bit] NOT NULL,
	[InsuranceCarrier_CU_ID] [int] NULL,
	[InsuanceLevel_CU_ID] [int] NULL,
	[InsurancePercentageApplied] [float] NULL,
	[InsurancePatientMaxAmount] [float] NULL,
	[InsuranceMaxAmount] [float] NULL,
	[IsSurchargeApplied] [bit] NOT NULL,
	[IsSurchargeDistributedToInsurancePercentage] [bit] NULL,
	[IsSurchargeAppliedToInsuranceOnly] [bit] NULL,
	[IsSurchargeAppliedToPatientOnly] [bit] NULL,
	[TotalSurchargeAccummulativePercentage] [float] NULL,
	[IsStampApplied] [bit] NOT NULL,
	[IsStampDistributedToInsurancePercentage] [bit] NULL,
	[IsStampAppliedToPatientOnly] [bit] NULL,
	[IsStampAppliedToInsuranceOnly] [bit] NULL,
	[TotalStampAmount] [float] NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InvoiceShare] PRIMARY KEY CLUSTERED 
(
	[InvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InvoiceType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceType_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[Description] [ntext] NULL,
 CONSTRAINT [PK_InvoiceType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InvoiceType_Surcharge_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceType_Surcharge_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceType_P_ID] [int] NOT NULL,
	[Surcharge_CU_ID] [int] NOT NULL,
	[IsApplied] [bit] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](50) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InvoiceType_Surcharge_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[JobTitle_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobTitle_cu](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[Name_EN] [nvarchar](50) NOT NULL,
	[Name_AR] [nvarchar](50) NULL,
	[Description] [nchar](10) NULL,
	[nvarchar(150)] [int] NULL,
 CONSTRAINT [PK_JobTitle_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Location_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location_cu](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[Organization_P_ID] [int] NOT NULL,
	[Country_CU_ID] [int] NULL,
	[City_CU_ID] [int] NULL,
	[Region_CU_ID] [int] NULL,
	[Territory_CU_ID] [int] NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](150) NULL,
	[InsertedBy] [int] NULL,
	[Address] [nvarchar](200) NULL,
	[InternalCode] [nvarchar](50) NULL,
 CONSTRAINT [PK_Location_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Manager_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manager_cu](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[Name_EN] [nvarchar](150) NOT NULL,
	[Name_AR] [nvarchar](150) NULL,
	[Description] [ntext] NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_Manager_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MaritalStatus_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaritalStatus_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](50) NOT NULL,
	[Name_S] [nvarchar](50) NULL,
	[Description] [ntext] NULL,
 CONSTRAINT [PK_MaritalStatus_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MedicalFlow_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalFlow_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[Description] [nvarchar](150) NULL,
 CONSTRAINT [PK_MedicalFlow_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MedicalStage_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalStage_cu](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](150) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_MedicalStage_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MilitaryStatus_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MilitaryStatus_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[Description] [ntext] NULL,
 CONSTRAINT [PK_MilitaryStatus_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Month_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Month_p](
	[ID] [int] NOT NULL,
	[Name_EN] [nvarchar](50) NOT NULL,
	[Name_AR] [nvarchar](50) NULL,
	[Description] [nvarchar](200) NULL,
 CONSTRAINT [PK_Month_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Organization_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organization_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](150) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_Organization_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Patient_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient_cu](
	[Person_CU_ID] [int] NOT NULL,
	[InsuranceCarrier_InsuranceLevel_CU_ID] [int] NULL,
	[PersonRelativeType_P_ID] [int] NULL,
	[RelativeName] [nvarchar](150) NULL,
	[RelativePhone] [nvarchar](50) NULL,
	[RelativeAddress] [nvarchar](200) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_Patient_cu] PRIMARY KEY CLUSTERED 
(
	[Person_CU_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PatientAttachment]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientAttachment](
	[ID] [int] NOT NULL,
	[Patient_CU_ID] [int] NOT NULL,
	[ImagePath] [nvarchar](400) NOT NULL,
	[ImageType_P_ID] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](50) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_PatientAttachment_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PaymentType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentType_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](50) NOT NULL,
	[Name_S] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_PaymentType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Person_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person_cu](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[FirstName_P] [nvarchar](50) NOT NULL,
	[SecondName_P] [nvarchar](50) NOT NULL,
	[ThirdName_P] [nvarchar](50) NULL,
	[FourthName_P] [nvarchar](50) NULL,
	[FirstName_S] [nvarchar](50) NULL,
	[SecondName_S] [nvarchar](50) NULL,
	[ThirdName_S] [nvarchar](50) NULL,
	[FourthName_S] [nvarchar](50) NULL,
	[PersonTitle_P_ID] [int] NULL,
	[Nationality_CU_ID] [int] NULL,
	[Gender] [bit] NOT NULL,
	[MaritalStatus_P_ID] [int] NULL,
	[Religion_P_ID] [int] NULL,
	[CountryOfResidence_CU_ID] [int] NULL,
	[CityOfResidence_CU_ID] [int] NULL,
	[Region_CU_ID] [int] NULL,
	[Address] [nvarchar](200) NULL,
	[Mobile1] [nvarchar](50) NULL,
	[Mobile2] [nvarchar](50) NULL,
	[Phone1] [nvarchar](50) NULL,
	[Phone2] [nvarchar](50) NULL,
	[EMail] [nvarchar](50) NULL,
	[BirthDate] [datetime] NULL,
	[IsOnDuty] [bit] NOT NULL,
	[AdditionalContactInfo] [nvarchar](200) NULL,
	[IdentificationCardType_CU_ID] [int] NULL,
	[IdentificationCardNumber] [nvarchar](200) NULL,
	[IdentificationCardIssuingDate] [datetime] NULL,
	[IdentificationCardExpirationDate] [datetime] NULL,
 CONSTRAINT [PK_Person_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Person_IdentificationCardType_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person_IdentificationCardType_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Person_CU_ID] [int] NOT NULL,
	[IdentificationCardType_CU_ID] [int] NOT NULL,
	[CardNumber] [nvarchar](200) NOT NULL,
	[IssuingDate] [datetime] NOT NULL,
	[EpirationDate] [datetime] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_Person_IdentificationCardsType_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Person_Phone_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person_Phone_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Person_CU_ID] [int] NULL,
	[PhoneType_P_ID] [int] NULL,
	[Phone] [nvarchar](50) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_Person_Phone_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PersonRelativeType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonRelativeType_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](50) NULL,
	[Name_S] [nvarchar](50) NULL,
	[Decsription] [nvarchar](50) NULL,
 CONSTRAINT [PK_PatientRelative_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PersonTitle_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonTitle_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](10) NOT NULL,
	[Name_S] [nvarchar](10) NULL,
	[Description] [ntext] NULL,
 CONSTRAINT [PK_PersonTitle_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhoneType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhoneType_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](50) NOT NULL,
	[Name_S] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_PhoneType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PriceType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PriceType_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[Description] [nvarchar](150) NULL,
 CONSTRAINT [PK_PriceType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QueueManager]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QueueManager](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StationPoint_CU_ID] [int] NOT NULL,
	[StationPointStage_CU_ID] [int] NOT NULL,
	[Service_CU_ID] [int] NULL,
	[Patient_CU_ID] [int] NULL,
	[Doctor_CU_ID] [int] NOT NULL,
	[AssignedName] [datetime] NULL,
	[InvoiceDetailID] [int] NOT NULL,
	[QueueManagerStatus_P_ID] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_QueueManager] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QueueManagerStatus_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QueueManagerStatus_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](50) NOT NULL,
	[Name_S] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_QueueManagerStatus_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Region_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Region_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[City_CU_ID] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [ntext] NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_Region_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Religion_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Religion_p](
	[ID] [int] NOT NULL,
	[Name_EN] [nvarchar](50) NOT NULL,
	[Name_AR] [nvarchar](50) NULL,
	[Description] [ntext] NULL,
 CONSTRAINT [PK_Religion_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](200) NOT NULL,
	[Name_S] [nvarchar](200) NULL,
	[Application_P_ID] [int] NULL,
	[Description] [nvarchar](200) NULL,
 CONSTRAINT [PK_Role_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RoleRegistration_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleRegistration_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Role_P_ID] [int] NOT NULL,
	[User_CU_ID] [int] NULL,
	[UserGroup_CU_ID] [int] NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_RoleRegistration_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Service_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name_P] [nvarchar](200) NOT NULL,
	[Name_S] [nvarchar](200) NULL,
	[ServiceCategory_CU_ID] [int] NULL,
	[ServiceType_P_ID] [int] NULL,
	[ParentService_CU_ID] [int] NULL,
	[InternalCode] [nvarchar](20) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[EnforceCategorization] [bit] NULL,
	[IsDailyCharged] [bit] NULL,
	[DefaultPrice] [float] NULL,
	[AllowAddmission] [bit] NULL,
	[Description] [nvarchar](200) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_InvoiceFinancialService_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Service_StationPoint_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service_StationPoint_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Service_CU_ID] [int] NOT NULL,
	[StationPoint_CU_ID] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_Service_StationPoint_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Service_Surcharge_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service_Surcharge_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Service_CU_ID] [int] NOT NULL,
	[Surcharge_CU_ID] [int] NOT NULL,
	[InvoiceType_P_ID] [int] NOT NULL,
	[IsApplied] [bit] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](100) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_Service_SurchargeType_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ServiceCategory_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceCategory_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[ServiceType_P_ID] [int] NOT NULL,
	[AllowAdmission] [bit] NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](150) NULL,
	[IsMedical] [bit] NULL,
	[InternalCode] [nvarchar](50) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_ServiceCategory_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ServiceCategory_StationPoint_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceCategory_StationPoint_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceCategory_CU_ID] [int] NOT NULL,
	[StationPoint_CU_ID] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_ServiceCategory_StationPoint_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ServiceCategory_Surcharge_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceCategory_Surcharge_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceCategory_CU_ID] [int] NOT NULL,
	[Surcharge_CU_ID] [int] NOT NULL,
	[InvoiceType_P_ID] [int] NOT NULL,
	[IsApplied] [bit] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](100) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_ServiceCategory_Surcharge_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ServicePrice_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServicePrice_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Service_CU_ID] [int] NULL,
	[ServiceCategory_CU_ID] [int] NULL,
	[Doctor_CU_ID] [int] NULL,
	[DoctorSpecialization_P_ID] [int] NULL,
	[DoctorRank_P_ID] [int] NULL,
	[DoctorCategory_CU_ID] [int] NULL,
	[Price] [float] NOT NULL,
	[InsuranceCarrier_InsuranceLevel_CU_ID] [int] NULL,
	[InsurancePrice] [float] NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_ServicePrice_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ServiceType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceType_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[Description] [nvarchar](150) NULL,
	[IsMedical] [bit] NULL,
	[AllowAdmission] [bit] NULL,
 CONSTRAINT [PK_InvoiceServiceType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ServiceType_StationPoint_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceType_StationPoint_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceType_P_ID] [int] NOT NULL,
	[StationPoint_CU_ID] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_ServiceType_StationPoint_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ServiceType_Surcharge_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceType_Surcharge_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceType_P_ID] [int] NOT NULL,
	[Surcharge_CU_ID] [int] NOT NULL,
	[InvoiceType_P_ID] [int] NOT NULL,
	[IsApplied] [bit] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](100) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_ServiceType_Surcharge_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Station_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Station_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](150) NULL,
	[Name_S] [nvarchar](150) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](150) NULL,
 CONSTRAINT [PK_Station_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StationPoint_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StationPoint_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[Station_P_ID] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InternalCode] [nvarchar](50) NULL,
	[Description] [nvarchar](150) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_StationPoint_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StationPointStage_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StationPointStage_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[StationPoint_CU_ID] [int] NOT NULL,
	[Floor_CU_ID] [int] NULL,
	[IsOnDuty] [bit] NOT NULL,
	[OrderIndex] [int] NULL,
	[InternalCode] [nvarchar](50) NULL,
	[Description] [nvarchar](150) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_StationPointStage_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Supplier_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier_cu](
	[Person_CU_ID] [int] NOT NULL,
	[SupplierType_P_ID] [int] NULL,
	[InsertedBy] [int] NULL,
	[InternalCode] [nvarchar](50) NULL,
 CONSTRAINT [PK_Supplier_cu] PRIMARY KEY CLUSTERED 
(
	[Person_CU_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SupplierType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupplierType_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[Description] [nvarchar](150) NULL,
 CONSTRAINT [PK_SupplierType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Surcharge_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Surcharge_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SurchargeType_P_ID] [int] NOT NULL,
	[Name_P] [nvarchar](100) NOT NULL,
	[Name_S] [nvarchar](100) NULL,
	[IsPercentage] [bit] NOT NULL,
	[Amount] [float] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](100) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_SurchargeType_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SurchargeType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SurchargeType_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](50) NOT NULL,
	[Name_S] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_SuchargeType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TableIdentity]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TableIdentity](
	[ID] [int] NOT NULL,
	[TableName] [nvarchar](200) NOT NULL,
	[NextAvailableID] [int] NULL,
	[CommonEnityTypeID] [int] NULL,
 CONSTRAINT [PK_TableIdentity] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Terriotry_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Terriotry_cu](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[Region_CU_ID] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [ntext] NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_Terriotry_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TrialBalanceTransaction]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrialBalanceTransaction](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[ChartOfAccount_CU_ID] [bigint] NOT NULL,
	[FinancialInterval_CU_ID] [int] NOT NULL,
	[StartingBalance] [float] NOT NULL,
	[EndingBalance] [float] NOT NULL,
	[IsDebit] [bit] NOT NULL,
	[TrialBalanceTransactionType_P_ID] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_ChartOfAccountBalance] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TrialBalanceTransactionType_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrialBalanceTransactionType_p](
	[ID] [int] NOT NULL,
	[Name_EN] [nvarchar](200) NOT NULL,
	[Name_AR] [nvarchar](200) NULL,
	[Description] [nvarchar](200) NULL,
 CONSTRAINT [PK_BalanceTransactionType_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UnitMeasurment_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnitMeasurment_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name_P] [nvarchar](100) NOT NULL,
	[Name_S] [nvarchar](100) NULL,
	[UnitMeasurment_P_ID] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](100) NULL,
	[InsertedBy] [int] NULL,
	[InternalCode] [nvarchar](50) NULL,
 CONSTRAINT [PK_UnitMeasurment_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UnitMeasurment_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnitMeasurment_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](50) NOT NULL,
	[Name_S] [nvarchar](50) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_UnitMeasurment_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UnitMeasurmentTreeLink_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnitMeasurmentTreeLink_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentUnitMeasurment_CU_ID] [int] NOT NULL,
	[ChildUnitMeasurment_CU_ID] [int] NOT NULL,
	[EncapsulatedChildQantity] [float] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_UnitMeasurmentTreeLink_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User_Application_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Application_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[User_CU_ID] [int] NOT NULL,
	[Application_P_ID] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](50) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_User_Application_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_cu](
	[Person_CU_ID] [int] NOT NULL,
	[LoginName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[OragnizationID] [int] NULL,
	[InternalCode] [nvarchar](50) NULL,
	[Description] [nvarchar](150) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_User_cu] PRIMARY KEY CLUSTERED 
(
	[Person_CU_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User_UserGroup_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_UserGroup_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[User_CU_ID] [int] NOT NULL,
	[UserGroup_CU_ID] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_User_UserGroup_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserGroup_Application_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroup_Application_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserGroup_CU_ID] [int] NOT NULL,
	[Application_P_ID] [int] NOT NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](50) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_UserGroup_Application] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserGroup_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroup_cu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[InternalCode] [nvarchar](150) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Description] [nvarchar](150) NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_UserGroup_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VisitAssessmentTopic_p]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisitAssessmentTopic_p](
	[ID] [int] NOT NULL,
	[Name_P] [nvarchar](150) NOT NULL,
	[Name_S] [nvarchar](150) NULL,
	[Description] [nvarchar](150) NULL,
 CONSTRAINT [PK_VisitAssessmentTopic_p] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VisitTiming]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisitTiming](
	[ID] [int] NOT NULL,
	[InvoiceDetailID] [int] NOT NULL,
	[StationPoint_CU_ID] [int] NOT NULL,
	[StationPointStage_CU_ID] [int] NULL,
	[Doctor_CU_ID] [int] NOT NULL,
	[SignInDateTime] [datetime] NOT NULL,
	[SignOutDateTime] [datetime] NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_PatientVisitTiming] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VisitTimming_SocialHistory]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisitTimming_SocialHistory](
	[VisitTimingID] [int] NOT NULL,
	[NegativeSocialHistory] [bit] NULL,
	[DidYouEverSmoke] [bit] NULL,
	[NumberOfPacks] [float] NULL,
	[NumberOfYears] [float] NULL,
	[SmokeFurtherDetails] [nvarchar](200) NULL,
	[QuitingSmokeLessThan] [bit] NULL,
	[QuitingSmokeBetween] [bit] NULL,
	[QuitingSmokeMoreThan] [bit] NULL,
	[QuitingSmokeFurtherDetails] [nvarchar](200) NULL,
	[DrinkAlcohol] [bit] NULL,
	[HowMuchAlcohol] [float] NULL,
	[AlcoholFurtherDetails] [nvarchar](200) NULL,
	[HadProblemWithAlcohol] [bit] NULL,
	[WhenHadProblemWIthAlcohol] [float] NULL,
	[HadProblemWithAlcoholFurtherDetails] [nvarchar](200) NULL,
	[Addicted] [bit] NULL,
	[AddictionFurtherDetails] [nvarchar](200) NULL,
	[HadProblemWithAddiction] [bit] NULL,
	[HadProblemWithAddictionFurtherDetails] [nvarchar](200) NULL,
	[UseRecreationalDrugs] [bit] NULL,
	[UseRecreationalDrugsFurtherDetails] [nvarchar](200) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_VisitTimming_SocialHistory] PRIMARY KEY CLUSTERED 
(
	[VisitTimingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WorkingShiftTitle_cu]    Script Date: 31-Aug-18 20:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkingShiftTitle_cu](
	[ID] [int] IDENTITY(1,2) NOT NULL,
	[Name_EN] [nvarchar](50) NOT NULL,
	[Name_AR] [nvarchar](50) NULL,
	[InternalCode] [nvarchar](50) NULL,
	[StartDateTime] [datetime] NOT NULL,
	[EndDateTime] [datetime] NOT NULL,
	[Description] [ntext] NULL,
	[IsOnDuty] [bit] NOT NULL,
	[InsertedBy] [int] NULL,
 CONSTRAINT [PK_WorkingShiftTitle_cu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[AccountingJournalEntryTransaction]  WITH CHECK ADD  CONSTRAINT [FK_AccountingJournalEntryTransaction_AccountingJournalTransaction] FOREIGN KEY([AccountingJournalTransaction_ID])
REFERENCES [dbo].[AccountingJournalTransaction] ([ID])
GO
ALTER TABLE [dbo].[AccountingJournalEntryTransaction] CHECK CONSTRAINT [FK_AccountingJournalEntryTransaction_AccountingJournalTransaction]
GO
ALTER TABLE [dbo].[AccountingJournalEntryTransaction]  WITH CHECK ADD  CONSTRAINT [FK_AccountingJournalEntryTransaction_ChartOfAccount_cu] FOREIGN KEY([ChartOfAccount_CU_ID])
REFERENCES [dbo].[ChartOfAccount_cu] ([ID])
GO
ALTER TABLE [dbo].[AccountingJournalEntryTransaction] CHECK CONSTRAINT [FK_AccountingJournalEntryTransaction_ChartOfAccount_cu]
GO
ALTER TABLE [dbo].[AccountingJournalEntryTransaction]  WITH CHECK ADD  CONSTRAINT [FK_AccountingJournalEntryTransaction_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[AccountingJournalEntryTransaction] CHECK CONSTRAINT [FK_AccountingJournalEntryTransaction_User_cu]
GO
ALTER TABLE [dbo].[AccountingJournalTransaction]  WITH CHECK ADD  CONSTRAINT [FK_AccountingJournalTransaction_FinancialTransactionType_p] FOREIGN KEY([FinancialTransactionType_P_ID])
REFERENCES [dbo].[FinancialTransactionType_p] ([ID])
GO
ALTER TABLE [dbo].[AccountingJournalTransaction] CHECK CONSTRAINT [FK_AccountingJournalTransaction_FinancialTransactionType_p]
GO
ALTER TABLE [dbo].[AccountingJournalTransaction]  WITH CHECK ADD  CONSTRAINT [FK_AccountingJournalTransaction_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[AccountingJournalTransaction] CHECK CONSTRAINT [FK_AccountingJournalTransaction_User_cu]
GO
ALTER TABLE [dbo].[ActiveSalaryEffect_cu]  WITH CHECK ADD  CONSTRAINT [FK_ActiveSalaryEffect_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[ActiveSalaryEffect_cu] CHECK CONSTRAINT [FK_ActiveSalaryEffect_cu_User_cu]
GO
ALTER TABLE [dbo].[Address_cu]  WITH CHECK ADD  CONSTRAINT [FK_Address_cu_City_cu] FOREIGN KEY([City_CU_ID])
REFERENCES [dbo].[City_cu] ([ID])
GO
ALTER TABLE [dbo].[Address_cu] CHECK CONSTRAINT [FK_Address_cu_City_cu]
GO
ALTER TABLE [dbo].[Address_cu]  WITH CHECK ADD  CONSTRAINT [FK_Address_cu_Country_cu] FOREIGN KEY([Country_CU_ID])
REFERENCES [dbo].[Country_cu] ([ID])
GO
ALTER TABLE [dbo].[Address_cu] CHECK CONSTRAINT [FK_Address_cu_Country_cu]
GO
ALTER TABLE [dbo].[Address_cu]  WITH CHECK ADD  CONSTRAINT [FK_Address_cu_Terriotry_cu] FOREIGN KEY([Territory_CU_ID])
REFERENCES [dbo].[Terriotry_cu] ([ID])
GO
ALTER TABLE [dbo].[Address_cu] CHECK CONSTRAINT [FK_Address_cu_Terriotry_cu]
GO
ALTER TABLE [dbo].[Address_cu]  WITH CHECK ADD  CONSTRAINT [FK_Address_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Address_cu] CHECK CONSTRAINT [FK_Address_cu_User_cu]
GO
ALTER TABLE [dbo].[AddressRefrenceType_cu]  WITH CHECK ADD  CONSTRAINT [FK_AddressRefrenceType_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[AddressRefrenceType_cu] CHECK CONSTRAINT [FK_AddressRefrenceType_cu_User_cu]
GO
ALTER TABLE [dbo].[Bank_cu]  WITH CHECK ADD  CONSTRAINT [FK_Bank_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Bank_cu] CHECK CONSTRAINT [FK_Bank_cu_User_cu]
GO
ALTER TABLE [dbo].[BankAccount_cu]  WITH CHECK ADD  CONSTRAINT [FK_BankAccount_cu_Bank_cu] FOREIGN KEY([Bank_CU_ID])
REFERENCES [dbo].[Bank_cu] ([ID])
GO
ALTER TABLE [dbo].[BankAccount_cu] CHECK CONSTRAINT [FK_BankAccount_cu_Bank_cu]
GO
ALTER TABLE [dbo].[BankAccount_cu]  WITH CHECK ADD  CONSTRAINT [FK_BankAccount_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[BankAccount_cu] CHECK CONSTRAINT [FK_BankAccount_cu_User_cu]
GO
ALTER TABLE [dbo].[CashBox_cu]  WITH CHECK ADD  CONSTRAINT [FK_CashBox_cu_ChartOfAccount_cu] FOREIGN KEY([ChartOfAccount_CU_ID])
REFERENCES [dbo].[ChartOfAccount_cu] ([ID])
GO
ALTER TABLE [dbo].[CashBox_cu] CHECK CONSTRAINT [FK_CashBox_cu_ChartOfAccount_cu]
GO
ALTER TABLE [dbo].[CashBox_cu]  WITH CHECK ADD  CONSTRAINT [FK_CashBox_cu_Floor_cu] FOREIGN KEY([Floor_CU_ID])
REFERENCES [dbo].[Floor_cu] ([ID])
GO
ALTER TABLE [dbo].[CashBox_cu] CHECK CONSTRAINT [FK_CashBox_cu_Floor_cu]
GO
ALTER TABLE [dbo].[CashBox_cu]  WITH CHECK ADD  CONSTRAINT [FK_CashBox_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[CashBox_cu] CHECK CONSTRAINT [FK_CashBox_cu_User_cu]
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CashBoxInOutTransaction_Bank_cu] FOREIGN KEY([Bank_CU_ID])
REFERENCES [dbo].[Bank_cu] ([ID])
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction] CHECK CONSTRAINT [FK_CashBoxInOutTransaction_Bank_cu]
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CashBoxInOutTransaction_BankAccount_cu] FOREIGN KEY([BankAccount_CU_ID])
REFERENCES [dbo].[BankAccount_cu] ([ID])
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction] CHECK CONSTRAINT [FK_CashBoxInOutTransaction_BankAccount_cu]
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CashBoxInOutTransaction_CashBox_cu] FOREIGN KEY([CashBox_CU_ID])
REFERENCES [dbo].[CashBox_cu] ([ID])
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction] CHECK CONSTRAINT [FK_CashBoxInOutTransaction_CashBox_cu]
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CashBoxInOutTransaction_CashBoxTransactionType_p] FOREIGN KEY([CashBoxTransactionType_P_ID])
REFERENCES [dbo].[CashBoxTransactionType_p] ([ID])
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction] CHECK CONSTRAINT [FK_CashBoxInOutTransaction_CashBoxTransactionType_p]
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CashBoxInOutTransaction_ChartOfAccount_cu] FOREIGN KEY([ChartOfAccount_CU_ID])
REFERENCES [dbo].[ChartOfAccount_cu] ([ID])
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction] CHECK CONSTRAINT [FK_CashBoxInOutTransaction_ChartOfAccount_cu]
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CashBoxInOutTransaction_Currency_cu] FOREIGN KEY([Currency_CU_ID])
REFERENCES [dbo].[Currency_cu] ([ID])
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction] CHECK CONSTRAINT [FK_CashBoxInOutTransaction_Currency_cu]
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CashBoxInOutTransaction_GeneralChartOfAccountType_cu] FOREIGN KEY([GeneralChartOfAccountType_CU_ID])
REFERENCES [dbo].[GeneralChartOfAccountType_cu] ([ID])
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction] CHECK CONSTRAINT [FK_CashBoxInOutTransaction_GeneralChartOfAccountType_cu]
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CashBoxInOutTransaction_PaymentType_p] FOREIGN KEY([PaymentType_P_ID])
REFERENCES [dbo].[PaymentType_p] ([ID])
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction] CHECK CONSTRAINT [FK_CashBoxInOutTransaction_PaymentType_p]
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CashBoxInOutTransaction_User_cu] FOREIGN KEY([CancelledBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction] CHECK CONSTRAINT [FK_CashBoxInOutTransaction_User_cu]
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CashBoxInOutTransaction_User_cu1] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction] CHECK CONSTRAINT [FK_CashBoxInOutTransaction_User_cu1]
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction_AccountingJournalTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CashBoxInOutTransaction_AccountingJournalTransaction_AccountingJournalTransaction] FOREIGN KEY([AccountingJournalTransactionID])
REFERENCES [dbo].[AccountingJournalTransaction] ([ID])
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction_AccountingJournalTransaction] CHECK CONSTRAINT [FK_CashBoxInOutTransaction_AccountingJournalTransaction_AccountingJournalTransaction]
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction_AccountingJournalTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CashBoxInOutTransaction_AccountingJournalTransaction_CashBoxInOutTransaction] FOREIGN KEY([CashBoxInOutTransactionID])
REFERENCES [dbo].[CashBoxInOutTransaction] ([ID])
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction_AccountingJournalTransaction] CHECK CONSTRAINT [FK_CashBoxInOutTransaction_AccountingJournalTransaction_CashBoxInOutTransaction]
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction_AccountingJournalTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CashBoxInOutTransaction_AccountingJournalTransaction_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[CashBoxInOutTransaction_AccountingJournalTransaction] CHECK CONSTRAINT [FK_CashBoxInOutTransaction_AccountingJournalTransaction_User_cu]
GO
ALTER TABLE [dbo].[CashBoxTransactionType_GeneralChartOfAccountType_cu]  WITH CHECK ADD  CONSTRAINT [FK_CashBoxTransactionType_GeneralChartOfAccountType_cu_CashBoxTransactionType_p] FOREIGN KEY([CashBoxTransactionType_P_ID])
REFERENCES [dbo].[CashBoxTransactionType_p] ([ID])
GO
ALTER TABLE [dbo].[CashBoxTransactionType_GeneralChartOfAccountType_cu] CHECK CONSTRAINT [FK_CashBoxTransactionType_GeneralChartOfAccountType_cu_CashBoxTransactionType_p]
GO
ALTER TABLE [dbo].[CashBoxTransactionType_GeneralChartOfAccountType_cu]  WITH CHECK ADD  CONSTRAINT [FK_CashBoxTransactionType_GeneralChartOfAccountType_cu_GeneralChartOfAccountType_cu] FOREIGN KEY([GeneralChartOfAccountType_CU_ID])
REFERENCES [dbo].[GeneralChartOfAccountType_cu] ([ID])
GO
ALTER TABLE [dbo].[CashBoxTransactionType_GeneralChartOfAccountType_cu] CHECK CONSTRAINT [FK_CashBoxTransactionType_GeneralChartOfAccountType_cu_GeneralChartOfAccountType_cu]
GO
ALTER TABLE [dbo].[CashBoxTransactionType_GeneralChartOfAccountType_cu]  WITH CHECK ADD  CONSTRAINT [FK_CashBoxTransactionType_GeneralChartOfAccountType_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[CashBoxTransactionType_GeneralChartOfAccountType_cu] CHECK CONSTRAINT [FK_CashBoxTransactionType_GeneralChartOfAccountType_cu_User_cu]
GO
ALTER TABLE [dbo].[CashBoxTransactionType_GeneralChartOfAccountType_p]  WITH CHECK ADD  CONSTRAINT [FK_CashBoxTransactionType_GeneralChartOfAccountType_p_CashBoxTransactionType_p] FOREIGN KEY([CashBoxTransactionType_P_ID])
REFERENCES [dbo].[CashBoxTransactionType_p] ([ID])
GO
ALTER TABLE [dbo].[CashBoxTransactionType_GeneralChartOfAccountType_p] CHECK CONSTRAINT [FK_CashBoxTransactionType_GeneralChartOfAccountType_p_CashBoxTransactionType_p]
GO
ALTER TABLE [dbo].[CashBoxTransactionType_GeneralChartOfAccountType_p]  WITH CHECK ADD  CONSTRAINT [FK_CashBoxTransactionType_GeneralChartOfAccountType_p_GeneralChartOfAccountType_p] FOREIGN KEY([GeneralChartOfAccountType_P_ID])
REFERENCES [dbo].[GeneralChartOfAccountType_p] ([ID])
GO
ALTER TABLE [dbo].[CashBoxTransactionType_GeneralChartOfAccountType_p] CHECK CONSTRAINT [FK_CashBoxTransactionType_GeneralChartOfAccountType_p_GeneralChartOfAccountType_p]
GO
ALTER TABLE [dbo].[ChartOfAccount_cu]  WITH CHECK ADD  CONSTRAINT [FK_ChartOfAccount_cu_ChartOfAccount_cu] FOREIGN KEY([ChartOfAccountingType_CU_ID])
REFERENCES [dbo].[ChartOfAccount_cu] ([ID])
GO
ALTER TABLE [dbo].[ChartOfAccount_cu] CHECK CONSTRAINT [FK_ChartOfAccount_cu_ChartOfAccount_cu]
GO
ALTER TABLE [dbo].[ChartOfAccount_cu]  WITH CHECK ADD  CONSTRAINT [FK_ChartOfAccount_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[ChartOfAccount_cu] CHECK CONSTRAINT [FK_ChartOfAccount_cu_User_cu]
GO
ALTER TABLE [dbo].[ChartOfAccount_cu]  WITH CHECK ADD  CONSTRAINT [FK_ChartOfAccounting_cu_ChartOfAccountingType_cu] FOREIGN KEY([ChartOfAccountingType_CU_ID])
REFERENCES [dbo].[ChartOfAccountType_cu] ([ID])
GO
ALTER TABLE [dbo].[ChartOfAccount_cu] CHECK CONSTRAINT [FK_ChartOfAccounting_cu_ChartOfAccountingType_cu]
GO
ALTER TABLE [dbo].[ChartOfAccount_GeneralChartOfAccountType_cu]  WITH CHECK ADD  CONSTRAINT [FK_ChartOfAccount_GeneralChartOfAccountType_cu_ChartOfAccount_cu] FOREIGN KEY([ChartOfAccount_CU_ID])
REFERENCES [dbo].[ChartOfAccount_cu] ([ID])
GO
ALTER TABLE [dbo].[ChartOfAccount_GeneralChartOfAccountType_cu] CHECK CONSTRAINT [FK_ChartOfAccount_GeneralChartOfAccountType_cu_ChartOfAccount_cu]
GO
ALTER TABLE [dbo].[ChartOfAccount_GeneralChartOfAccountType_cu]  WITH CHECK ADD  CONSTRAINT [FK_ChartOfAccount_GeneralChartOfAccountType_cu_GeneralChartOfAccountType_cu] FOREIGN KEY([GeneralChartOfAccountType_CU_ID])
REFERENCES [dbo].[GeneralChartOfAccountType_cu] ([ID])
GO
ALTER TABLE [dbo].[ChartOfAccount_GeneralChartOfAccountType_cu] CHECK CONSTRAINT [FK_ChartOfAccount_GeneralChartOfAccountType_cu_GeneralChartOfAccountType_cu]
GO
ALTER TABLE [dbo].[ChartOfAccount_GeneralChartOfAccountType_cu]  WITH CHECK ADD  CONSTRAINT [FK_ChartOfAccount_GeneralChartOfAccountType_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[ChartOfAccount_GeneralChartOfAccountType_cu] CHECK CONSTRAINT [FK_ChartOfAccount_GeneralChartOfAccountType_cu_User_cu]
GO
ALTER TABLE [dbo].[ChartOfAccountMargin_cu]  WITH CHECK ADD  CONSTRAINT [FK_ChartOfAccountMargin_cu_ChartOfAccountMargin_p] FOREIGN KEY([ChartOfAccountingMargin_P_ID])
REFERENCES [dbo].[ChartOfAccountMargin_p] ([ID])
GO
ALTER TABLE [dbo].[ChartOfAccountMargin_cu] CHECK CONSTRAINT [FK_ChartOfAccountMargin_cu_ChartOfAccountMargin_p]
GO
ALTER TABLE [dbo].[ChartOfAccountMargin_cu]  WITH CHECK ADD  CONSTRAINT [FK_ChartOfAccountMargin_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[ChartOfAccountMargin_cu] CHECK CONSTRAINT [FK_ChartOfAccountMargin_cu_User_cu]
GO
ALTER TABLE [dbo].[ChartOfAccountType_cu]  WITH CHECK ADD  CONSTRAINT [FK_ChartOfAccounting_cu_ChartOfAccountingType_p] FOREIGN KEY([ChartOfAccounting_P_ID])
REFERENCES [dbo].[ChartOfAccountType_p] ([ID])
GO
ALTER TABLE [dbo].[ChartOfAccountType_cu] CHECK CONSTRAINT [FK_ChartOfAccounting_cu_ChartOfAccountingType_p]
GO
ALTER TABLE [dbo].[ChartOfAccountType_cu]  WITH CHECK ADD  CONSTRAINT [FK_ChartOfAccountType_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[ChartOfAccountType_cu] CHECK CONSTRAINT [FK_ChartOfAccountType_cu_User_cu]
GO
ALTER TABLE [dbo].[City_cu]  WITH CHECK ADD  CONSTRAINT [FK_City_cu_Country_cu] FOREIGN KEY([Country_CU_ID])
REFERENCES [dbo].[Country_cu] ([ID])
GO
ALTER TABLE [dbo].[City_cu] CHECK CONSTRAINT [FK_City_cu_Country_cu]
GO
ALTER TABLE [dbo].[City_cu]  WITH CHECK ADD  CONSTRAINT [FK_City_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[City_cu] CHECK CONSTRAINT [FK_City_cu_User_cu]
GO
ALTER TABLE [dbo].[Country_cu]  WITH CHECK ADD  CONSTRAINT [FK_Country_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Country_cu] CHECK CONSTRAINT [FK_Country_cu_User_cu]
GO
ALTER TABLE [dbo].[Currency_cu]  WITH CHECK ADD  CONSTRAINT [FK_Currency_cu_Country_cu] FOREIGN KEY([Country_CU_ID])
REFERENCES [dbo].[Country_cu] ([ID])
GO
ALTER TABLE [dbo].[Currency_cu] CHECK CONSTRAINT [FK_Currency_cu_Country_cu]
GO
ALTER TABLE [dbo].[Currency_cu]  WITH CHECK ADD  CONSTRAINT [FK_Currency_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Currency_cu] CHECK CONSTRAINT [FK_Currency_cu_User_cu]
GO
ALTER TABLE [dbo].[Customer_cu]  WITH CHECK ADD  CONSTRAINT [FK_Customer_cu_CustomerType_p] FOREIGN KEY([CustomerType_P_ID])
REFERENCES [dbo].[CustomerType_p] ([ID])
GO
ALTER TABLE [dbo].[Customer_cu] CHECK CONSTRAINT [FK_Customer_cu_CustomerType_p]
GO
ALTER TABLE [dbo].[Customer_cu]  WITH CHECK ADD  CONSTRAINT [FK_Customer_cu_Person_cu] FOREIGN KEY([Person_CU_ID])
REFERENCES [dbo].[Person_cu] ([ID])
GO
ALTER TABLE [dbo].[Customer_cu] CHECK CONSTRAINT [FK_Customer_cu_Person_cu]
GO
ALTER TABLE [dbo].[Customer_cu]  WITH CHECK ADD  CONSTRAINT [FK_Customer_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Customer_cu] CHECK CONSTRAINT [FK_Customer_cu_User_cu]
GO
ALTER TABLE [dbo].[CustomerPaymentTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPaymentTransaction_AccountingJournalTransaction] FOREIGN KEY([AccountingJournalTransactionID])
REFERENCES [dbo].[AccountingJournalTransaction] ([ID])
GO
ALTER TABLE [dbo].[CustomerPaymentTransaction] CHECK CONSTRAINT [FK_CustomerPaymentTransaction_AccountingJournalTransaction]
GO
ALTER TABLE [dbo].[CustomerPaymentTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPaymentTransaction_Customer_cu] FOREIGN KEY([Customer_CU_ID])
REFERENCES [dbo].[Customer_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[CustomerPaymentTransaction] CHECK CONSTRAINT [FK_CustomerPaymentTransaction_Customer_cu]
GO
ALTER TABLE [dbo].[CustomerPaymentTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPaymentTransaction_FinanceInvoice] FOREIGN KEY([FinanceInvoiceID])
REFERENCES [dbo].[FinanceInvoice] ([ID])
GO
ALTER TABLE [dbo].[CustomerPaymentTransaction] CHECK CONSTRAINT [FK_CustomerPaymentTransaction_FinanceInvoice]
GO
ALTER TABLE [dbo].[CustomerPaymentTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPaymentTransaction_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[CustomerPaymentTransaction] CHECK CONSTRAINT [FK_CustomerPaymentTransaction_User_cu]
GO
ALTER TABLE [dbo].[Department_cu]  WITH CHECK ADD  CONSTRAINT [FK_Department_cu_Department_cu] FOREIGN KEY([ParentDepartment_CU_ID])
REFERENCES [dbo].[Department_cu] ([ID])
GO
ALTER TABLE [dbo].[Department_cu] CHECK CONSTRAINT [FK_Department_cu_Department_cu]
GO
ALTER TABLE [dbo].[Department_cu]  WITH CHECK ADD  CONSTRAINT [FK_Department_cu_Department_cu1] FOREIGN KEY([ID])
REFERENCES [dbo].[Department_cu] ([ID])
GO
ALTER TABLE [dbo].[Department_cu] CHECK CONSTRAINT [FK_Department_cu_Department_cu1]
GO
ALTER TABLE [dbo].[Department_cu]  WITH CHECK ADD  CONSTRAINT [FK_Department_cu_DepartmentType_p] FOREIGN KEY([Department_P_ID])
REFERENCES [dbo].[DepartmentType_p] ([ID])
GO
ALTER TABLE [dbo].[Department_cu] CHECK CONSTRAINT [FK_Department_cu_DepartmentType_p]
GO
ALTER TABLE [dbo].[Department_cu]  WITH CHECK ADD  CONSTRAINT [FK_Department_cu_Location_cu] FOREIGN KEY([Location_CU_ID])
REFERENCES [dbo].[Location_cu] ([ID])
GO
ALTER TABLE [dbo].[Department_cu] CHECK CONSTRAINT [FK_Department_cu_Location_cu]
GO
ALTER TABLE [dbo].[Department_cu]  WITH CHECK ADD  CONSTRAINT [FK_Department_cu_Manager_cu] FOREIGN KEY([Manager_CU_ID])
REFERENCES [dbo].[Manager_cu] ([ID])
GO
ALTER TABLE [dbo].[Department_cu] CHECK CONSTRAINT [FK_Department_cu_Manager_cu]
GO
ALTER TABLE [dbo].[Department_cu]  WITH CHECK ADD  CONSTRAINT [FK_Department_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Department_cu] CHECK CONSTRAINT [FK_Department_cu_User_cu]
GO
ALTER TABLE [dbo].[Department_JobTitle_cu]  WITH CHECK ADD  CONSTRAINT [FK_Department_JobTitle_CU_ID_Department_cu] FOREIGN KEY([Department_CU_ID])
REFERENCES [dbo].[Department_cu] ([ID])
GO
ALTER TABLE [dbo].[Department_JobTitle_cu] CHECK CONSTRAINT [FK_Department_JobTitle_CU_ID_Department_cu]
GO
ALTER TABLE [dbo].[Department_JobTitle_cu]  WITH CHECK ADD  CONSTRAINT [FK_Department_JobTitle_CU_ID_JobTitle_cu] FOREIGN KEY([JobTitle_CU_ID])
REFERENCES [dbo].[JobTitle_cu] ([ID])
GO
ALTER TABLE [dbo].[Department_JobTitle_cu] CHECK CONSTRAINT [FK_Department_JobTitle_CU_ID_JobTitle_cu]
GO
ALTER TABLE [dbo].[Department_JobTitle_cu]  WITH CHECK ADD  CONSTRAINT [FK_Department_JobTitle_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Department_JobTitle_cu] CHECK CONSTRAINT [FK_Department_JobTitle_cu_User_cu]
GO
ALTER TABLE [dbo].[Department_JobTitle_WorkingShiftTitle_cu]  WITH CHECK ADD  CONSTRAINT [FK_Department_JobTitle_WorkingShiftTitle_cu_Department_cu] FOREIGN KEY([Department_CU_ID])
REFERENCES [dbo].[Department_cu] ([ID])
GO
ALTER TABLE [dbo].[Department_JobTitle_WorkingShiftTitle_cu] CHECK CONSTRAINT [FK_Department_JobTitle_WorkingShiftTitle_cu_Department_cu]
GO
ALTER TABLE [dbo].[Department_JobTitle_WorkingShiftTitle_cu]  WITH CHECK ADD  CONSTRAINT [FK_Department_JobTitle_WorkingShiftTitle_cu_JobTitle_cu] FOREIGN KEY([JobTitle_CU_ID])
REFERENCES [dbo].[JobTitle_cu] ([ID])
GO
ALTER TABLE [dbo].[Department_JobTitle_WorkingShiftTitle_cu] CHECK CONSTRAINT [FK_Department_JobTitle_WorkingShiftTitle_cu_JobTitle_cu]
GO
ALTER TABLE [dbo].[Department_JobTitle_WorkingShiftTitle_cu]  WITH CHECK ADD  CONSTRAINT [FK_Department_JobTitle_WorkingShiftTitle_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Department_JobTitle_WorkingShiftTitle_cu] CHECK CONSTRAINT [FK_Department_JobTitle_WorkingShiftTitle_cu_User_cu]
GO
ALTER TABLE [dbo].[Department_JobTitle_WorkingShiftTitle_cu]  WITH CHECK ADD  CONSTRAINT [FK_Department_JobTitle_WorkingShiftTitle_cu_WorkingShiftTitle_cu] FOREIGN KEY([WorkingShiftTitle_CU_ID])
REFERENCES [dbo].[WorkingShiftTitle_cu] ([ID])
GO
ALTER TABLE [dbo].[Department_JobTitle_WorkingShiftTitle_cu] CHECK CONSTRAINT [FK_Department_JobTitle_WorkingShiftTitle_cu_WorkingShiftTitle_cu]
GO
ALTER TABLE [dbo].[Doctor_cu]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_cu_DoctorCategory_cu] FOREIGN KEY([DoctorCategory_CU_ID])
REFERENCES [dbo].[DoctorCategory_cu] ([ID])
GO
ALTER TABLE [dbo].[Doctor_cu] CHECK CONSTRAINT [FK_Doctor_cu_DoctorCategory_cu]
GO
ALTER TABLE [dbo].[Doctor_cu]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_cu_DoctorProfessionalFeesIssuingType_p] FOREIGN KEY([DoctorProfessionalFeesIssuingType_P_ID])
REFERENCES [dbo].[DoctorProfessionalFeesIssuingType_p] ([ID])
GO
ALTER TABLE [dbo].[Doctor_cu] CHECK CONSTRAINT [FK_Doctor_cu_DoctorProfessionalFeesIssuingType_p]
GO
ALTER TABLE [dbo].[Doctor_cu]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_cu_DoctorRank_p] FOREIGN KEY([DoctorRank_P_ID])
REFERENCES [dbo].[DoctorRank_p] ([ID])
GO
ALTER TABLE [dbo].[Doctor_cu] CHECK CONSTRAINT [FK_Doctor_cu_DoctorRank_p]
GO
ALTER TABLE [dbo].[Doctor_cu]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_cu_DoctorSpecialization_p] FOREIGN KEY([DoctorSpecialization_P_ID])
REFERENCES [dbo].[DoctorSpecialization_p] ([ID])
GO
ALTER TABLE [dbo].[Doctor_cu] CHECK CONSTRAINT [FK_Doctor_cu_DoctorSpecialization_p]
GO
ALTER TABLE [dbo].[Doctor_cu]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_cu_DoctorTaxType_cu] FOREIGN KEY([DoctorTaxType_CU_ID])
REFERENCES [dbo].[DoctorTaxType_cu] ([ID])
GO
ALTER TABLE [dbo].[Doctor_cu] CHECK CONSTRAINT [FK_Doctor_cu_DoctorTaxType_cu]
GO
ALTER TABLE [dbo].[Doctor_cu]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_cu_Person_cu] FOREIGN KEY([Person_CU_ID])
REFERENCES [dbo].[Person_cu] ([ID])
GO
ALTER TABLE [dbo].[Doctor_cu] CHECK CONSTRAINT [FK_Doctor_cu_Person_cu]
GO
ALTER TABLE [dbo].[Doctor_cu]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Doctor_cu] CHECK CONSTRAINT [FK_Doctor_cu_User_cu]
GO
ALTER TABLE [dbo].[Doctor_Service_cu]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_Service_CU_Doctor_cu] FOREIGN KEY([Doctor_CU_ID])
REFERENCES [dbo].[Doctor_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Doctor_Service_cu] CHECK CONSTRAINT [FK_Doctor_Service_CU_Doctor_cu]
GO
ALTER TABLE [dbo].[Doctor_Service_cu]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_Service_CU_DoctorCategory_cu] FOREIGN KEY([DoctorCategory_CU_ID])
REFERENCES [dbo].[DoctorCategory_cu] ([ID])
GO
ALTER TABLE [dbo].[Doctor_Service_cu] CHECK CONSTRAINT [FK_Doctor_Service_CU_DoctorCategory_cu]
GO
ALTER TABLE [dbo].[Doctor_Service_cu]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_Service_CU_Service_cu] FOREIGN KEY([Service_CU_ID])
REFERENCES [dbo].[Service_cu] ([ID])
GO
ALTER TABLE [dbo].[Doctor_Service_cu] CHECK CONSTRAINT [FK_Doctor_Service_CU_Service_cu]
GO
ALTER TABLE [dbo].[Doctor_Service_cu]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_Service_CU_ServiceCategory_cu] FOREIGN KEY([ServiceCartegory_CU_ID])
REFERENCES [dbo].[ServiceCategory_cu] ([ID])
GO
ALTER TABLE [dbo].[Doctor_Service_cu] CHECK CONSTRAINT [FK_Doctor_Service_CU_ServiceCategory_cu]
GO
ALTER TABLE [dbo].[Doctor_Service_cu]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_Service_CU_ServiceType_p] FOREIGN KEY([ServiceType_P_ID])
REFERENCES [dbo].[ServiceType_p] ([ID])
GO
ALTER TABLE [dbo].[Doctor_Service_cu] CHECK CONSTRAINT [FK_Doctor_Service_CU_ServiceType_p]
GO
ALTER TABLE [dbo].[Doctor_Service_cu]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_Service_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Doctor_Service_cu] CHECK CONSTRAINT [FK_Doctor_Service_cu_User_cu]
GO
ALTER TABLE [dbo].[DoctorCategory_cu]  WITH CHECK ADD  CONSTRAINT [FK_DoctorCategory_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[DoctorCategory_cu] CHECK CONSTRAINT [FK_DoctorCategory_cu_User_cu]
GO
ALTER TABLE [dbo].[DoctorFeesService_cu]  WITH CHECK ADD  CONSTRAINT [FK_DoctorFeesService_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[DoctorFeesService_cu] CHECK CONSTRAINT [FK_DoctorFeesService_cu_User_cu]
GO
ALTER TABLE [dbo].[DoctorTaxType_cu]  WITH CHECK ADD  CONSTRAINT [FK_DoctorTaxType_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[DoctorTaxType_cu] CHECK CONSTRAINT [FK_DoctorTaxType_cu_User_cu]
GO
ALTER TABLE [dbo].[Employee_cu]  WITH CHECK ADD  CONSTRAINT [FK_Employee_cu_EmployeeType_cu] FOREIGN KEY([EmployeeType_CU_ID])
REFERENCES [dbo].[EmployeeType_cu] ([ID])
GO
ALTER TABLE [dbo].[Employee_cu] CHECK CONSTRAINT [FK_Employee_cu_EmployeeType_cu]
GO
ALTER TABLE [dbo].[Employee_cu]  WITH CHECK ADD  CONSTRAINT [FK_Employee_cu_EmploymentDate_cu] FOREIGN KEY([EmploymentDate_CU_ID])
REFERENCES [dbo].[EmploymentDate_cu] ([ID])
GO
ALTER TABLE [dbo].[Employee_cu] CHECK CONSTRAINT [FK_Employee_cu_EmploymentDate_cu]
GO
ALTER TABLE [dbo].[Employee_cu]  WITH CHECK ADD  CONSTRAINT [FK_Employee_cu_MilitaryStatus_p] FOREIGN KEY([MilitaryStatus_P_ID])
REFERENCES [dbo].[MilitaryStatus_p] ([ID])
GO
ALTER TABLE [dbo].[Employee_cu] CHECK CONSTRAINT [FK_Employee_cu_MilitaryStatus_p]
GO
ALTER TABLE [dbo].[Employee_cu]  WITH CHECK ADD  CONSTRAINT [FK_Employee_cu_Person_cu] FOREIGN KEY([Person_CU_ID])
REFERENCES [dbo].[Person_cu] ([ID])
GO
ALTER TABLE [dbo].[Employee_cu] CHECK CONSTRAINT [FK_Employee_cu_Person_cu]
GO
ALTER TABLE [dbo].[Employee_cu]  WITH CHECK ADD  CONSTRAINT [FK_Employee_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Employee_cu] CHECK CONSTRAINT [FK_Employee_cu_User_cu]
GO
ALTER TABLE [dbo].[Employee_Department_JobTitle_cu]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Department_JobTitle_cu_Department_cu] FOREIGN KEY([Department_CU_ID])
REFERENCES [dbo].[Department_cu] ([ID])
GO
ALTER TABLE [dbo].[Employee_Department_JobTitle_cu] CHECK CONSTRAINT [FK_Employee_Department_JobTitle_cu_Department_cu]
GO
ALTER TABLE [dbo].[Employee_Department_JobTitle_cu]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Department_JobTitle_cu_JobTitle_cu] FOREIGN KEY([JobTitle_CU_ID])
REFERENCES [dbo].[JobTitle_cu] ([ID])
GO
ALTER TABLE [dbo].[Employee_Department_JobTitle_cu] CHECK CONSTRAINT [FK_Employee_Department_JobTitle_cu_JobTitle_cu]
GO
ALTER TABLE [dbo].[Employee_Department_JobTitle_cu]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Department_JobTitle_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Employee_Department_JobTitle_cu] CHECK CONSTRAINT [FK_Employee_Department_JobTitle_cu_User_cu]
GO
ALTER TABLE [dbo].[Employee_WorkingShift_cu]  WITH CHECK ADD  CONSTRAINT [FK_Employee_WorkingShift_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Employee_WorkingShift_cu] CHECK CONSTRAINT [FK_Employee_WorkingShift_cu_User_cu]
GO
ALTER TABLE [dbo].[Employee_WorkingShift_cu]  WITH CHECK ADD  CONSTRAINT [FK_Employee_WorkingShift_cu_WorkingShiftTitle_cu] FOREIGN KEY([WorkingShift_CU_ID])
REFERENCES [dbo].[WorkingShiftTitle_cu] ([ID])
GO
ALTER TABLE [dbo].[Employee_WorkingShift_cu] CHECK CONSTRAINT [FK_Employee_WorkingShift_cu_WorkingShiftTitle_cu]
GO
ALTER TABLE [dbo].[Employee_WorkingShiftTitle_cu]  WITH CHECK ADD  CONSTRAINT [FK_Employee_WorkingShiftTitle_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Employee_WorkingShiftTitle_cu] CHECK CONSTRAINT [FK_Employee_WorkingShiftTitle_cu_User_cu]
GO
ALTER TABLE [dbo].[Employee_WorkingShiftTitle_cu]  WITH CHECK ADD  CONSTRAINT [FK_Employee_WorkingShiftTitle_cu_WorkingShiftTitle_cu] FOREIGN KEY([WorkingShiftTitle_CU_ID])
REFERENCES [dbo].[WorkingShiftTitle_cu] ([ID])
GO
ALTER TABLE [dbo].[Employee_WorkingShiftTitle_cu] CHECK CONSTRAINT [FK_Employee_WorkingShiftTitle_cu_WorkingShiftTitle_cu]
GO
ALTER TABLE [dbo].[EmployeeType_cu]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeType_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[EmployeeType_cu] CHECK CONSTRAINT [FK_EmployeeType_cu_User_cu]
GO
ALTER TABLE [dbo].[EmploymentDate_cu]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentDate_cu_EmploymentDateType_p] FOREIGN KEY([EmploymentDateType_P_ID])
REFERENCES [dbo].[EmploymentDateType_p] ([ID])
GO
ALTER TABLE [dbo].[EmploymentDate_cu] CHECK CONSTRAINT [FK_EmploymentDate_cu_EmploymentDateType_p]
GO
ALTER TABLE [dbo].[EmploymentDate_cu]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentDate_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[EmploymentDate_cu] CHECK CONSTRAINT [FK_EmploymentDate_cu_User_cu]
GO
ALTER TABLE [dbo].[FinanceInvoice]  WITH CHECK ADD  CONSTRAINT [FK_FinanceInvoice_FinanceInvoice] FOREIGN KEY([Customer_CU_ID])
REFERENCES [dbo].[Customer_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[FinanceInvoice] CHECK CONSTRAINT [FK_FinanceInvoice_FinanceInvoice]
GO
ALTER TABLE [dbo].[FinanceInvoice]  WITH CHECK ADD  CONSTRAINT [FK_FinanceInvoice_InvoicePaymentType_p] FOREIGN KEY([InvoicePaymentType_P_ID])
REFERENCES [dbo].[InvoicePaymentType_p] ([ID])
GO
ALTER TABLE [dbo].[FinanceInvoice] CHECK CONSTRAINT [FK_FinanceInvoice_InvoicePaymentType_p]
GO
ALTER TABLE [dbo].[FinanceInvoice]  WITH CHECK ADD  CONSTRAINT [FK_FinanceInvoice_InvoiceType_p] FOREIGN KEY([InvoiceType_P_ID])
REFERENCES [dbo].[InvoiceType_p] ([ID])
GO
ALTER TABLE [dbo].[FinanceInvoice] CHECK CONSTRAINT [FK_FinanceInvoice_InvoiceType_p]
GO
ALTER TABLE [dbo].[FinanceInvoice]  WITH CHECK ADD  CONSTRAINT [FK_FinanceInvoice_Supplier_cu] FOREIGN KEY([Supplier_CU_ID])
REFERENCES [dbo].[Supplier_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[FinanceInvoice] CHECK CONSTRAINT [FK_FinanceInvoice_Supplier_cu]
GO
ALTER TABLE [dbo].[FinanceInvoice]  WITH CHECK ADD  CONSTRAINT [FK_FinanceInvoice_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[FinanceInvoice] CHECK CONSTRAINT [FK_FinanceInvoice_User_cu]
GO
ALTER TABLE [dbo].[FinanceInvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_FinanceInvoiceDetail_FinanceInvoice] FOREIGN KEY([FinanceInvoiceID])
REFERENCES [dbo].[FinanceInvoice] ([ID])
GO
ALTER TABLE [dbo].[FinanceInvoiceDetail] CHECK CONSTRAINT [FK_FinanceInvoiceDetail_FinanceInvoice]
GO
ALTER TABLE [dbo].[FinanceInvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_FinanceInvoiceDetail_FinanceInvoiceDetail] FOREIGN KEY([ParentInvoiceDetailID])
REFERENCES [dbo].[FinanceInvoiceDetail] ([ID])
GO
ALTER TABLE [dbo].[FinanceInvoiceDetail] CHECK CONSTRAINT [FK_FinanceInvoiceDetail_FinanceInvoiceDetail]
GO
ALTER TABLE [dbo].[FinanceInvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_FinanceInvoiceDetail_InventoryHousing_cu] FOREIGN KEY([InventoryHousing_CU_ID])
REFERENCES [dbo].[InventoryHousing_cu] ([ID])
GO
ALTER TABLE [dbo].[FinanceInvoiceDetail] CHECK CONSTRAINT [FK_FinanceInvoiceDetail_InventoryHousing_cu]
GO
ALTER TABLE [dbo].[FinanceInvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_FinanceInvoiceDetail_InventoryItem_cu] FOREIGN KEY([InventoryItem_CU_ID])
REFERENCES [dbo].[InventoryItem_cu] ([ID])
GO
ALTER TABLE [dbo].[FinanceInvoiceDetail] CHECK CONSTRAINT [FK_FinanceInvoiceDetail_InventoryItem_cu]
GO
ALTER TABLE [dbo].[FinanceInvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_FinanceInvoiceDetail_UnitMeasurment_cu] FOREIGN KEY([UnitMeasurment_CU_ID])
REFERENCES [dbo].[UnitMeasurment_cu] ([ID])
GO
ALTER TABLE [dbo].[FinanceInvoiceDetail] CHECK CONSTRAINT [FK_FinanceInvoiceDetail_UnitMeasurment_cu]
GO
ALTER TABLE [dbo].[FinanceInvoicePayment]  WITH CHECK ADD  CONSTRAINT [FK_FinanceInvoicePayment_FinanceInvoice] FOREIGN KEY([FinanceInvoiceID])
REFERENCES [dbo].[FinanceInvoice] ([ID])
GO
ALTER TABLE [dbo].[FinanceInvoicePayment] CHECK CONSTRAINT [FK_FinanceInvoicePayment_FinanceInvoice]
GO
ALTER TABLE [dbo].[FinanceInvoicePayment]  WITH CHECK ADD  CONSTRAINT [FK_FinanceInvoicePayment_PaymentType_p] FOREIGN KEY([PaymentType_P_ID])
REFERENCES [dbo].[PaymentType_p] ([ID])
GO
ALTER TABLE [dbo].[FinanceInvoicePayment] CHECK CONSTRAINT [FK_FinanceInvoicePayment_PaymentType_p]
GO
ALTER TABLE [dbo].[FinanceInvoicePayment]  WITH CHECK ADD  CONSTRAINT [FK_FinanceInvoicePayment_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[FinanceInvoicePayment] CHECK CONSTRAINT [FK_FinanceInvoicePayment_User_cu]
GO
ALTER TABLE [dbo].[FinanceInvoiceShare]  WITH CHECK ADD  CONSTRAINT [FK_FinanceInvoiceShare_FinanceInvoice] FOREIGN KEY([FinanceInvoiceID])
REFERENCES [dbo].[FinanceInvoice] ([ID])
GO
ALTER TABLE [dbo].[FinanceInvoiceShare] CHECK CONSTRAINT [FK_FinanceInvoiceShare_FinanceInvoice]
GO
ALTER TABLE [dbo].[FinanceInvoiceShare]  WITH CHECK ADD  CONSTRAINT [FK_FinanceInvoiceShare_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[FinanceInvoiceShare] CHECK CONSTRAINT [FK_FinanceInvoiceShare_User_cu]
GO
ALTER TABLE [dbo].[FinancialInterval_cu]  WITH CHECK ADD  CONSTRAINT [FK_FinancialInterval_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[FinancialInterval_cu] CHECK CONSTRAINT [FK_FinancialInterval_cu_User_cu]
GO
ALTER TABLE [dbo].[FinancialInterval_Month_cu]  WITH CHECK ADD  CONSTRAINT [FK_FinancialInterval_Month_cu_FinancialInterval_cu] FOREIGN KEY([FinancialInterval_CU_ID])
REFERENCES [dbo].[FinancialInterval_cu] ([ID])
GO
ALTER TABLE [dbo].[FinancialInterval_Month_cu] CHECK CONSTRAINT [FK_FinancialInterval_Month_cu_FinancialInterval_cu]
GO
ALTER TABLE [dbo].[FinancialInterval_Month_cu]  WITH CHECK ADD  CONSTRAINT [FK_FinancialInterval_Month_cu_Month_p] FOREIGN KEY([Month_P_ID])
REFERENCES [dbo].[Month_p] ([ID])
GO
ALTER TABLE [dbo].[FinancialInterval_Month_cu] CHECK CONSTRAINT [FK_FinancialInterval_Month_cu_Month_p]
GO
ALTER TABLE [dbo].[FinancialInterval_Month_cu]  WITH CHECK ADD  CONSTRAINT [FK_FinancialInterval_Month_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[FinancialInterval_Month_cu] CHECK CONSTRAINT [FK_FinancialInterval_Month_cu_User_cu]
GO
ALTER TABLE [dbo].[Floor_cu]  WITH CHECK ADD  CONSTRAINT [FK_Floor_cu_Location_cu] FOREIGN KEY([Location_CU_ID])
REFERENCES [dbo].[Location_cu] ([ID])
GO
ALTER TABLE [dbo].[Floor_cu] CHECK CONSTRAINT [FK_Floor_cu_Location_cu]
GO
ALTER TABLE [dbo].[Floor_cu]  WITH CHECK ADD  CONSTRAINT [FK_Floor_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Floor_cu] CHECK CONSTRAINT [FK_Floor_cu_User_cu]
GO
ALTER TABLE [dbo].[GeneralChartOfAccountType_cu]  WITH CHECK ADD  CONSTRAINT [FK_GeneralChartOfAccountType_cu_GeneralChartOfAccountType_p] FOREIGN KEY([GeneralChartOfAccountType_P_ID])
REFERENCES [dbo].[GeneralChartOfAccountType_p] ([ID])
GO
ALTER TABLE [dbo].[GeneralChartOfAccountType_cu] CHECK CONSTRAINT [FK_GeneralChartOfAccountType_cu_GeneralChartOfAccountType_p]
GO
ALTER TABLE [dbo].[InPatientRoom_cu]  WITH CHECK ADD  CONSTRAINT [FK_InPatientRoom_cu_Floor_cu] FOREIGN KEY([Floor_CU_ID])
REFERENCES [dbo].[Floor_cu] ([ID])
GO
ALTER TABLE [dbo].[InPatientRoom_cu] CHECK CONSTRAINT [FK_InPatientRoom_cu_Floor_cu]
GO
ALTER TABLE [dbo].[InPatientRoom_cu]  WITH CHECK ADD  CONSTRAINT [FK_InPatientRoom_cu_InPatientRoomClassification_cu] FOREIGN KEY([InPatientRoomClassification_CU_ID])
REFERENCES [dbo].[InPatientRoomClassification_cu] ([ID])
GO
ALTER TABLE [dbo].[InPatientRoom_cu] CHECK CONSTRAINT [FK_InPatientRoom_cu_InPatientRoomClassification_cu]
GO
ALTER TABLE [dbo].[InPatientRoom_cu]  WITH CHECK ADD  CONSTRAINT [FK_InPatientRoom_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InPatientRoom_cu] CHECK CONSTRAINT [FK_InPatientRoom_cu_User_cu]
GO
ALTER TABLE [dbo].[InPatientRoom_InPatientAdmissionPricingType_cu]  WITH CHECK ADD  CONSTRAINT [FK_InPatientRoom_InPatientAddmissionPricingType_cu_InPatientAddmissionPricingType_p] FOREIGN KEY([InPatientAddmissionPricingType_P_ID])
REFERENCES [dbo].[InPatientAdmissionPricingType_p] ([ID])
GO
ALTER TABLE [dbo].[InPatientRoom_InPatientAdmissionPricingType_cu] CHECK CONSTRAINT [FK_InPatientRoom_InPatientAddmissionPricingType_cu_InPatientAddmissionPricingType_p]
GO
ALTER TABLE [dbo].[InPatientRoom_InPatientAdmissionPricingType_cu]  WITH CHECK ADD  CONSTRAINT [FK_InPatientRoom_InPatientAddmissionPricingType_cu_InPatientRoom_cu] FOREIGN KEY([InPatientRoom_CU_ID])
REFERENCES [dbo].[InPatientRoom_cu] ([ID])
GO
ALTER TABLE [dbo].[InPatientRoom_InPatientAdmissionPricingType_cu] CHECK CONSTRAINT [FK_InPatientRoom_InPatientAddmissionPricingType_cu_InPatientRoom_cu]
GO
ALTER TABLE [dbo].[InPatientRoom_InPatientAdmissionPricingType_cu]  WITH CHECK ADD  CONSTRAINT [FK_InPatientRoom_InPatientAdmissionPricingType_cu_InsuranceCarrier_InsuranceLevel_cu] FOREIGN KEY([InsuranceCarrier_InsuranceLevel_CU_ID])
REFERENCES [dbo].[InsuranceCarrier_InsuranceLevel_cu] ([ID])
GO
ALTER TABLE [dbo].[InPatientRoom_InPatientAdmissionPricingType_cu] CHECK CONSTRAINT [FK_InPatientRoom_InPatientAdmissionPricingType_cu_InsuranceCarrier_InsuranceLevel_cu]
GO
ALTER TABLE [dbo].[InPatientRoom_InPatientAdmissionPricingType_cu]  WITH CHECK ADD  CONSTRAINT [FK_InPatientRoom_InPatientAdmissionPricingType_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InPatientRoom_InPatientAdmissionPricingType_cu] CHECK CONSTRAINT [FK_InPatientRoom_InPatientAdmissionPricingType_cu_User_cu]
GO
ALTER TABLE [dbo].[InPatientRoomBed_cu]  WITH CHECK ADD  CONSTRAINT [FK_InPatientRoomBed_cu_InPatientRoom_cu] FOREIGN KEY([InPatientRoom_CU_ID])
REFERENCES [dbo].[InPatientRoom_cu] ([ID])
GO
ALTER TABLE [dbo].[InPatientRoomBed_cu] CHECK CONSTRAINT [FK_InPatientRoomBed_cu_InPatientRoom_cu]
GO
ALTER TABLE [dbo].[InPatientRoomBed_cu]  WITH CHECK ADD  CONSTRAINT [FK_InPatientRoomBed_cu_InPatientRoomBedStatus_p] FOREIGN KEY([InPatientRoomBedStatus_P_ID])
REFERENCES [dbo].[InPatientRoomBedStatus_p] ([ID])
GO
ALTER TABLE [dbo].[InPatientRoomBed_cu] CHECK CONSTRAINT [FK_InPatientRoomBed_cu_InPatientRoomBedStatus_p]
GO
ALTER TABLE [dbo].[InPatientRoomBed_cu]  WITH CHECK ADD  CONSTRAINT [FK_InPatientRoomBed_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InPatientRoomBed_cu] CHECK CONSTRAINT [FK_InPatientRoomBed_cu_User_cu]
GO
ALTER TABLE [dbo].[InPatientRoomBed_InPatientAdmissionPricingType_cu]  WITH CHECK ADD  CONSTRAINT [FK_InPatientRoomBed_InPatientAddmissionPricingType_cu_InPatientAddmissionPricingType_p] FOREIGN KEY([InPatientAddmissionPricingType_P_ID])
REFERENCES [dbo].[InPatientAdmissionPricingType_p] ([ID])
GO
ALTER TABLE [dbo].[InPatientRoomBed_InPatientAdmissionPricingType_cu] CHECK CONSTRAINT [FK_InPatientRoomBed_InPatientAddmissionPricingType_cu_InPatientAddmissionPricingType_p]
GO
ALTER TABLE [dbo].[InPatientRoomBed_InPatientAdmissionPricingType_cu]  WITH CHECK ADD  CONSTRAINT [FK_InPatientRoomBed_InPatientAddmissionPricingType_cu_InPatientRoomBed_cu] FOREIGN KEY([InPatientRoomBed_CU_ID])
REFERENCES [dbo].[InPatientRoomBed_cu] ([ID])
GO
ALTER TABLE [dbo].[InPatientRoomBed_InPatientAdmissionPricingType_cu] CHECK CONSTRAINT [FK_InPatientRoomBed_InPatientAddmissionPricingType_cu_InPatientRoomBed_cu]
GO
ALTER TABLE [dbo].[InPatientRoomBed_InPatientAdmissionPricingType_cu]  WITH CHECK ADD  CONSTRAINT [FK_InPatientRoomBed_InPatientAdmissionPricingType_cu_InsuranceCarrier_InsuranceLevel_cu] FOREIGN KEY([InsuranceCarrier_InsuranceLevel_CU_ID])
REFERENCES [dbo].[InsuranceCarrier_InsuranceLevel_cu] ([ID])
GO
ALTER TABLE [dbo].[InPatientRoomBed_InPatientAdmissionPricingType_cu] CHECK CONSTRAINT [FK_InPatientRoomBed_InPatientAdmissionPricingType_cu_InsuranceCarrier_InsuranceLevel_cu]
GO
ALTER TABLE [dbo].[InPatientRoomBed_InPatientAdmissionPricingType_cu]  WITH CHECK ADD  CONSTRAINT [FK_InPatientRoomBed_InPatientAdmissionPricingType_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InPatientRoomBed_InPatientAdmissionPricingType_cu] CHECK CONSTRAINT [FK_InPatientRoomBed_InPatientAdmissionPricingType_cu_User_cu]
GO
ALTER TABLE [dbo].[InPatientRoomClassification_cu]  WITH CHECK ADD  CONSTRAINT [FK_InPatientRoomClassification_cu_InPatientRoomType_p] FOREIGN KEY([InPatientRoomType_P_ID])
REFERENCES [dbo].[InPatientRoomType_p] ([ID])
GO
ALTER TABLE [dbo].[InPatientRoomClassification_cu] CHECK CONSTRAINT [FK_InPatientRoomClassification_cu_InPatientRoomType_p]
GO
ALTER TABLE [dbo].[InPatientRoomClassification_cu]  WITH CHECK ADD  CONSTRAINT [FK_InPatientRoomClassification_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InPatientRoomClassification_cu] CHECK CONSTRAINT [FK_InPatientRoomClassification_cu_User_cu]
GO
ALTER TABLE [dbo].[InPatientRoomClassification_InPatientAdmissionPricingType_cu]  WITH CHECK ADD  CONSTRAINT [FK_InPatientRoomClassification_InPatientAddmissionPricingType_cu_InPatientAddmissionPricingType_p] FOREIGN KEY([InPatientAddmissionPricingType_P_ID])
REFERENCES [dbo].[InPatientAdmissionPricingType_p] ([ID])
GO
ALTER TABLE [dbo].[InPatientRoomClassification_InPatientAdmissionPricingType_cu] CHECK CONSTRAINT [FK_InPatientRoomClassification_InPatientAddmissionPricingType_cu_InPatientAddmissionPricingType_p]
GO
ALTER TABLE [dbo].[InPatientRoomClassification_InPatientAdmissionPricingType_cu]  WITH CHECK ADD  CONSTRAINT [FK_InPatientRoomClassification_InPatientAddmissionPricingType_cu_InPatientRoomClassification_cu] FOREIGN KEY([InPatientRoomClassification_CU_ID])
REFERENCES [dbo].[InPatientRoomClassification_cu] ([ID])
GO
ALTER TABLE [dbo].[InPatientRoomClassification_InPatientAdmissionPricingType_cu] CHECK CONSTRAINT [FK_InPatientRoomClassification_InPatientAddmissionPricingType_cu_InPatientRoomClassification_cu]
GO
ALTER TABLE [dbo].[InPatientRoomClassification_InPatientAdmissionPricingType_cu]  WITH CHECK ADD  CONSTRAINT [FK_InPatientRoomClassification_InPatientAdmissionPricingType_cu_InsuranceCarrier_InsuranceLevel_cu] FOREIGN KEY([InsuranceCarrier_InsuranceLevel_CU_ID])
REFERENCES [dbo].[InsuranceCarrier_InsuranceLevel_cu] ([ID])
GO
ALTER TABLE [dbo].[InPatientRoomClassification_InPatientAdmissionPricingType_cu] CHECK CONSTRAINT [FK_InPatientRoomClassification_InPatientAdmissionPricingType_cu_InsuranceCarrier_InsuranceLevel_cu]
GO
ALTER TABLE [dbo].[InPatientRoomClassification_InPatientAdmissionPricingType_cu]  WITH CHECK ADD  CONSTRAINT [FK_InPatientRoomClassification_InPatientAdmissionPricingType_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InPatientRoomClassification_InPatientAdmissionPricingType_cu] CHECK CONSTRAINT [FK_InPatientRoomClassification_InPatientAdmissionPricingType_cu_User_cu]
GO
ALTER TABLE [dbo].[InsuranceCarrier_cu]  WITH CHECK ADD  CONSTRAINT [FK_InsuranceCarrier_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InsuranceCarrier_cu] CHECK CONSTRAINT [FK_InsuranceCarrier_cu_User_cu]
GO
ALTER TABLE [dbo].[InsuranceCarrier_InsuranceLevel_cu]  WITH CHECK ADD  CONSTRAINT [FK_InsuranceCarrier_InsuranceLevel_cu_InsuranceCarrier_cu] FOREIGN KEY([InsuranceCarrier_CU_ID])
REFERENCES [dbo].[InsuranceCarrier_cu] ([ID])
GO
ALTER TABLE [dbo].[InsuranceCarrier_InsuranceLevel_cu] CHECK CONSTRAINT [FK_InsuranceCarrier_InsuranceLevel_cu_InsuranceCarrier_cu]
GO
ALTER TABLE [dbo].[InsuranceCarrier_InsuranceLevel_cu]  WITH CHECK ADD  CONSTRAINT [FK_InsuranceCarrier_InsuranceLevel_cu_InsuranceLevel_cu] FOREIGN KEY([InsuranceLevel_CU_ID])
REFERENCES [dbo].[InsuranceLevel_cu] ([ID])
GO
ALTER TABLE [dbo].[InsuranceCarrier_InsuranceLevel_cu] CHECK CONSTRAINT [FK_InsuranceCarrier_InsuranceLevel_cu_InsuranceLevel_cu]
GO
ALTER TABLE [dbo].[InsuranceCarrier_InsuranceLevel_cu]  WITH CHECK ADD  CONSTRAINT [FK_InsuranceCarrier_InsuranceLevel_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InsuranceCarrier_InsuranceLevel_cu] CHECK CONSTRAINT [FK_InsuranceCarrier_InsuranceLevel_cu_User_cu]
GO
ALTER TABLE [dbo].[InsuranceLevel_cu]  WITH CHECK ADD  CONSTRAINT [FK_InsuranceLevel_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InsuranceLevel_cu] CHECK CONSTRAINT [FK_InsuranceLevel_cu_User_cu]
GO
ALTER TABLE [dbo].[InventoryHousing_cu]  WITH CHECK ADD  CONSTRAINT [FK_InventoryHousing_cu_Floor_cu] FOREIGN KEY([Floor_CU_ID])
REFERENCES [dbo].[Floor_cu] ([ID])
GO
ALTER TABLE [dbo].[InventoryHousing_cu] CHECK CONSTRAINT [FK_InventoryHousing_cu_Floor_cu]
GO
ALTER TABLE [dbo].[InventoryHousing_cu]  WITH CHECK ADD  CONSTRAINT [FK_InventoryHousing_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InventoryHousing_cu] CHECK CONSTRAINT [FK_InventoryHousing_cu_User_cu]
GO
ALTER TABLE [dbo].[InventoryItem_cu]  WITH CHECK ADD  CONSTRAINT [FK_InventoryItem_cu_InventoryHousing_cu] FOREIGN KEY([InventoryHousing_CU_ID])
REFERENCES [dbo].[InventoryHousing_cu] ([ID])
GO
ALTER TABLE [dbo].[InventoryItem_cu] CHECK CONSTRAINT [FK_InventoryItem_cu_InventoryHousing_cu]
GO
ALTER TABLE [dbo].[InventoryItem_cu]  WITH CHECK ADD  CONSTRAINT [FK_InventoryItem_cu_InventoryItemBrand_cu] FOREIGN KEY([InventoryItemBrand_CU_ID])
REFERENCES [dbo].[InventoryItemBrand_cu] ([ID])
GO
ALTER TABLE [dbo].[InventoryItem_cu] CHECK CONSTRAINT [FK_InventoryItem_cu_InventoryItemBrand_cu]
GO
ALTER TABLE [dbo].[InventoryItem_cu]  WITH CHECK ADD  CONSTRAINT [FK_InventoryItem_cu_InventoryItemCategory_cu] FOREIGN KEY([InventoryItemCategory_CU_ID])
REFERENCES [dbo].[InventoryItemCategory_cu] ([ID])
GO
ALTER TABLE [dbo].[InventoryItem_cu] CHECK CONSTRAINT [FK_InventoryItem_cu_InventoryItemCategory_cu]
GO
ALTER TABLE [dbo].[InventoryItem_cu]  WITH CHECK ADD  CONSTRAINT [FK_InventoryItem_cu_InventoryItemType_p] FOREIGN KEY([InventoryItemType_P_ID])
REFERENCES [dbo].[InventoryItemType_p] ([ID])
GO
ALTER TABLE [dbo].[InventoryItem_cu] CHECK CONSTRAINT [FK_InventoryItem_cu_InventoryItemType_p]
GO
ALTER TABLE [dbo].[InventoryItem_cu]  WITH CHECK ADD  CONSTRAINT [FK_InventoryItem_cu_UnitMeasurment_cu] FOREIGN KEY([InventoryTrackingUnitMeasurment_CU_ID])
REFERENCES [dbo].[UnitMeasurment_cu] ([ID])
GO
ALTER TABLE [dbo].[InventoryItem_cu] CHECK CONSTRAINT [FK_InventoryItem_cu_UnitMeasurment_cu]
GO
ALTER TABLE [dbo].[InventoryItem_cu]  WITH CHECK ADD  CONSTRAINT [FK_InventoryItem_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InventoryItem_cu] CHECK CONSTRAINT [FK_InventoryItem_cu_User_cu]
GO
ALTER TABLE [dbo].[InventoryItem_UnitMeasurment_cu]  WITH CHECK ADD  CONSTRAINT [FK_InventoryItem_UnitMeasurment_cu_InventoryItem_cu] FOREIGN KEY([InventoryItem_CU_ID])
REFERENCES [dbo].[InventoryItem_cu] ([ID])
GO
ALTER TABLE [dbo].[InventoryItem_UnitMeasurment_cu] CHECK CONSTRAINT [FK_InventoryItem_UnitMeasurment_cu_InventoryItem_cu]
GO
ALTER TABLE [dbo].[InventoryItem_UnitMeasurment_cu]  WITH CHECK ADD  CONSTRAINT [FK_InventoryItem_UnitMeasurment_cu_UnitMeasurment_cu] FOREIGN KEY([UnitMeasurment_CU_ID])
REFERENCES [dbo].[UnitMeasurment_cu] ([ID])
GO
ALTER TABLE [dbo].[InventoryItem_UnitMeasurment_cu] CHECK CONSTRAINT [FK_InventoryItem_UnitMeasurment_cu_UnitMeasurment_cu]
GO
ALTER TABLE [dbo].[InventoryItem_UnitMeasurment_cu]  WITH CHECK ADD  CONSTRAINT [FK_InventoryItem_UnitMeasurment_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InventoryItem_UnitMeasurment_cu] CHECK CONSTRAINT [FK_InventoryItem_UnitMeasurment_cu_User_cu]
GO
ALTER TABLE [dbo].[InventoryItemBrand_cu]  WITH CHECK ADD  CONSTRAINT [FK_InventoryItemBrand_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InventoryItemBrand_cu] CHECK CONSTRAINT [FK_InventoryItemBrand_cu_User_cu]
GO
ALTER TABLE [dbo].[InventoryItemCategory_cu]  WITH CHECK ADD  CONSTRAINT [FK_InventoryItemCategory_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InventoryItemCategory_cu] CHECK CONSTRAINT [FK_InventoryItemCategory_cu_User_cu]
GO
ALTER TABLE [dbo].[InventoryItemGroup_InventoryItem_cu]  WITH CHECK ADD  CONSTRAINT [FK_InventoryItemGroup_InventoryItem_cu_InventoryItem_cu] FOREIGN KEY([InventoryItem_CU_ID])
REFERENCES [dbo].[InventoryItem_cu] ([ID])
GO
ALTER TABLE [dbo].[InventoryItemGroup_InventoryItem_cu] CHECK CONSTRAINT [FK_InventoryItemGroup_InventoryItem_cu_InventoryItem_cu]
GO
ALTER TABLE [dbo].[InventoryItemGroup_InventoryItem_cu]  WITH CHECK ADD  CONSTRAINT [FK_InventoryItemGroup_InventoryItem_cu_InventoryItemGroup_cu] FOREIGN KEY([InvetoryItemGroup_CU_ID])
REFERENCES [dbo].[InventoryItemGroup_cu] ([ID])
GO
ALTER TABLE [dbo].[InventoryItemGroup_InventoryItem_cu] CHECK CONSTRAINT [FK_InventoryItemGroup_InventoryItem_cu_InventoryItemGroup_cu]
GO
ALTER TABLE [dbo].[InventoryItemPrice_cu]  WITH CHECK ADD  CONSTRAINT [FK_InventoryItemPrice_cu_Customer_cu] FOREIGN KEY([Customer_CU_ID])
REFERENCES [dbo].[Customer_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InventoryItemPrice_cu] CHECK CONSTRAINT [FK_InventoryItemPrice_cu_Customer_cu]
GO
ALTER TABLE [dbo].[InventoryItemPrice_cu]  WITH CHECK ADD  CONSTRAINT [FK_InventoryItemPrice_cu_InventoryItem_cu] FOREIGN KEY([InventoryItem_CU_ID])
REFERENCES [dbo].[InventoryItem_cu] ([ID])
GO
ALTER TABLE [dbo].[InventoryItemPrice_cu] CHECK CONSTRAINT [FK_InventoryItemPrice_cu_InventoryItem_cu]
GO
ALTER TABLE [dbo].[InventoryItemPrice_cu]  WITH CHECK ADD  CONSTRAINT [FK_InventoryItemPrice_cu_InventoryItem_UnitMeasurment_cu] FOREIGN KEY([InventoryItem_UnitMeasurment_CU_ID])
REFERENCES [dbo].[InventoryItem_UnitMeasurment_cu] ([ID])
GO
ALTER TABLE [dbo].[InventoryItemPrice_cu] CHECK CONSTRAINT [FK_InventoryItemPrice_cu_InventoryItem_UnitMeasurment_cu]
GO
ALTER TABLE [dbo].[InventoryItemPrice_cu]  WITH CHECK ADD  CONSTRAINT [FK_InventoryItemPrice_cu_PriceType_p] FOREIGN KEY([PriceType_P_ID])
REFERENCES [dbo].[PriceType_p] ([ID])
GO
ALTER TABLE [dbo].[InventoryItemPrice_cu] CHECK CONSTRAINT [FK_InventoryItemPrice_cu_PriceType_p]
GO
ALTER TABLE [dbo].[InventoryItemPrice_cu]  WITH CHECK ADD  CONSTRAINT [FK_InventoryItemPrice_cu_Supplier_cu] FOREIGN KEY([Supplier_CU_ID])
REFERENCES [dbo].[Supplier_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InventoryItemPrice_cu] CHECK CONSTRAINT [FK_InventoryItemPrice_cu_Supplier_cu]
GO
ALTER TABLE [dbo].[InventoryItemPrice_cu]  WITH CHECK ADD  CONSTRAINT [FK_InventoryItemPrice_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InventoryItemPrice_cu] CHECK CONSTRAINT [FK_InventoryItemPrice_cu_User_cu]
GO
ALTER TABLE [dbo].[InventoryItemTransaction]  WITH CHECK ADD  CONSTRAINT [FK_InventoryItem_InventoryHousing_cu_InventoryHousing_cu] FOREIGN KEY([InventoryHousing_CU_ID])
REFERENCES [dbo].[InventoryHousing_cu] ([ID])
GO
ALTER TABLE [dbo].[InventoryItemTransaction] CHECK CONSTRAINT [FK_InventoryItem_InventoryHousing_cu_InventoryHousing_cu]
GO
ALTER TABLE [dbo].[InventoryItemTransaction]  WITH CHECK ADD  CONSTRAINT [FK_InventoryItem_InventoryHousing_cu_InventoryItem_cu] FOREIGN KEY([InventoryItem_CU_ID])
REFERENCES [dbo].[InventoryItem_cu] ([ID])
GO
ALTER TABLE [dbo].[InventoryItemTransaction] CHECK CONSTRAINT [FK_InventoryItem_InventoryHousing_cu_InventoryItem_cu]
GO
ALTER TABLE [dbo].[InventoryItemTransaction]  WITH CHECK ADD  CONSTRAINT [FK_InventoryItem_InventoryHousing_cu_UnitMeasurment_cu] FOREIGN KEY([UnitMeasurment_CU_ID])
REFERENCES [dbo].[UnitMeasurment_cu] ([ID])
GO
ALTER TABLE [dbo].[InventoryItemTransaction] CHECK CONSTRAINT [FK_InventoryItem_InventoryHousing_cu_UnitMeasurment_cu]
GO
ALTER TABLE [dbo].[InventoryItemTransaction]  WITH CHECK ADD  CONSTRAINT [FK_InventoryItem_InventoryHousing_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InventoryItemTransaction] CHECK CONSTRAINT [FK_InventoryItem_InventoryHousing_cu_User_cu]
GO
ALTER TABLE [dbo].[InventoryItemTransaction]  WITH CHECK ADD  CONSTRAINT [FK_InventoryItemTransaction_InventoryItemTransactionType_p] FOREIGN KEY([InventoryItemTransactionType_P_ID])
REFERENCES [dbo].[InventoryItemTransactionType_p] ([ID])
GO
ALTER TABLE [dbo].[InventoryItemTransaction] CHECK CONSTRAINT [FK_InventoryItemTransaction_InventoryItemTransactionType_p]
GO
ALTER TABLE [dbo].[InventoryItemType_p]  WITH CHECK ADD  CONSTRAINT [FK_InventoryItemType_p_Organization_p] FOREIGN KEY([Organization_P_ID])
REFERENCES [dbo].[Organization_p] ([ID])
GO
ALTER TABLE [dbo].[InventoryItemType_p] CHECK CONSTRAINT [FK_InventoryItemType_p_Organization_p]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_InvoiceCostingStrategy_p] FOREIGN KEY([InvoiceCostingStrategy_P_ID])
REFERENCES [dbo].[InvoiceCostingStrategy_p] ([ID])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_InvoiceCostingStrategy_p]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Patient_cu] FOREIGN KEY([Patient_CU_ID])
REFERENCES [dbo].[Patient_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Patient_cu]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_tr_InvoicePaymentType_p] FOREIGN KEY([InvoicePaymentType_P_ID])
REFERENCES [dbo].[InvoicePaymentType_p] ([ID])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_tr_InvoicePaymentType_p]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_tr_InvoiceType_p] FOREIGN KEY([InvoiceType_P_ID])
REFERENCES [dbo].[InvoiceType_p] ([ID])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_tr_InvoiceType_p]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_User_cu]
GO
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_DiscountType_p] FOREIGN KEY([DiscountType_P_ID])
REFERENCES [dbo].[DiscountType_p] ([ID])
GO
ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_DiscountType_p]
GO
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_Doctor_cu] FOREIGN KEY([Doctor_CU_ID])
REFERENCES [dbo].[Doctor_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_Doctor_cu]
GO
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_Invoice] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[Invoice] ([ID])
GO
ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_Invoice]
GO
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_InvoiceDetail1] FOREIGN KEY([ParentInvoiceDetailID])
REFERENCES [dbo].[InvoiceDetail] ([ID])
GO
ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_InvoiceDetail1]
GO
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_InvoiceFinancialService_cu] FOREIGN KEY([Service_CU_ID])
REFERENCES [dbo].[Service_cu] ([ID])
GO
ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_InvoiceFinancialService_cu]
GO
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_User_cu]
GO
ALTER TABLE [dbo].[InvoiceDetail_Accommodation]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_Accommodation_DiscountType_p] FOREIGN KEY([DiscountType_P_ID])
REFERENCES [dbo].[DiscountType_p] ([ID])
GO
ALTER TABLE [dbo].[InvoiceDetail_Accommodation] CHECK CONSTRAINT [FK_InvoiceDetail_Accommodation_DiscountType_p]
GO
ALTER TABLE [dbo].[InvoiceDetail_Accommodation]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_Accommodation_InPatientRoom_cu] FOREIGN KEY([InPatientRoom_CU_ID])
REFERENCES [dbo].[InPatientRoom_cu] ([ID])
GO
ALTER TABLE [dbo].[InvoiceDetail_Accommodation] CHECK CONSTRAINT [FK_InvoiceDetail_Accommodation_InPatientRoom_cu]
GO
ALTER TABLE [dbo].[InvoiceDetail_Accommodation]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_Accommodation_InPatientRoomBed_cu] FOREIGN KEY([InPatientRoomBed_CU_ID])
REFERENCES [dbo].[InPatientRoomBed_cu] ([ID])
GO
ALTER TABLE [dbo].[InvoiceDetail_Accommodation] CHECK CONSTRAINT [FK_InvoiceDetail_Accommodation_InPatientRoomBed_cu]
GO
ALTER TABLE [dbo].[InvoiceDetail_Accommodation]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_Accommodation_InPatientRoomClassification_cu] FOREIGN KEY([InPatientRoomClassification_CU_ID])
REFERENCES [dbo].[InPatientRoomClassification_cu] ([ID])
GO
ALTER TABLE [dbo].[InvoiceDetail_Accommodation] CHECK CONSTRAINT [FK_InvoiceDetail_Accommodation_InPatientRoomClassification_cu]
GO
ALTER TABLE [dbo].[InvoiceDetail_Accommodation]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_Accommodation_InvoiceDetail] FOREIGN KEY([InvoiceDetailID])
REFERENCES [dbo].[InvoiceDetail] ([ID])
GO
ALTER TABLE [dbo].[InvoiceDetail_Accommodation] CHECK CONSTRAINT [FK_InvoiceDetail_Accommodation_InvoiceDetail]
GO
ALTER TABLE [dbo].[InvoiceDetail_Accommodation]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_Accommodation_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InvoiceDetail_Accommodation] CHECK CONSTRAINT [FK_InvoiceDetail_Accommodation_User_cu]
GO
ALTER TABLE [dbo].[InvoiceDetail_DoctorFees]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_DoctorFees_DiscountType_p] FOREIGN KEY([DiscountType_P_ID])
REFERENCES [dbo].[DiscountType_p] ([ID])
GO
ALTER TABLE [dbo].[InvoiceDetail_DoctorFees] CHECK CONSTRAINT [FK_InvoiceDetail_DoctorFees_DiscountType_p]
GO
ALTER TABLE [dbo].[InvoiceDetail_DoctorFees]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_DoctorFees_DoctorFeesService_cu] FOREIGN KEY([DoctorFeesService_CU_ID])
REFERENCES [dbo].[DoctorFeesService_cu] ([ID])
GO
ALTER TABLE [dbo].[InvoiceDetail_DoctorFees] CHECK CONSTRAINT [FK_InvoiceDetail_DoctorFees_DoctorFeesService_cu]
GO
ALTER TABLE [dbo].[InvoiceDetail_DoctorFees]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_DoctorFees_InvoiceDetail_DoctorFees] FOREIGN KEY([InvoiceDetailID])
REFERENCES [dbo].[InvoiceDetail] ([ID])
GO
ALTER TABLE [dbo].[InvoiceDetail_DoctorFees] CHECK CONSTRAINT [FK_InvoiceDetail_DoctorFees_InvoiceDetail_DoctorFees]
GO
ALTER TABLE [dbo].[InvoiceDetail_DoctorFees]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_DoctorFees_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InvoiceDetail_DoctorFees] CHECK CONSTRAINT [FK_InvoiceDetail_DoctorFees_User_cu]
GO
ALTER TABLE [dbo].[InvoiceDetail_Inventory]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_Inventory_InventoryHousing_cu] FOREIGN KEY([InventoryHousing_CU_ID])
REFERENCES [dbo].[InventoryHousing_cu] ([ID])
GO
ALTER TABLE [dbo].[InvoiceDetail_Inventory] CHECK CONSTRAINT [FK_InvoiceDetail_Inventory_InventoryHousing_cu]
GO
ALTER TABLE [dbo].[InvoiceDetail_Inventory]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_Inventory_InventoryItem_cu] FOREIGN KEY([InventoryItem_CU_ID])
REFERENCES [dbo].[InventoryItem_cu] ([ID])
GO
ALTER TABLE [dbo].[InvoiceDetail_Inventory] CHECK CONSTRAINT [FK_InvoiceDetail_Inventory_InventoryItem_cu]
GO
ALTER TABLE [dbo].[InvoiceDetail_Inventory]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_Inventory_InvoiceDetail] FOREIGN KEY([InvoiceDetailID])
REFERENCES [dbo].[InvoiceDetail] ([ID])
GO
ALTER TABLE [dbo].[InvoiceDetail_Inventory] CHECK CONSTRAINT [FK_InvoiceDetail_Inventory_InvoiceDetail]
GO
ALTER TABLE [dbo].[InvoiceDetail_Inventory]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_Inventory_InvoiceDetail_Inventory] FOREIGN KEY([DiscountType_P_ID])
REFERENCES [dbo].[DiscountType_p] ([ID])
GO
ALTER TABLE [dbo].[InvoiceDetail_Inventory] CHECK CONSTRAINT [FK_InvoiceDetail_Inventory_InvoiceDetail_Inventory]
GO
ALTER TABLE [dbo].[InvoiceDetail_Inventory]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_Inventory_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InvoiceDetail_Inventory] CHECK CONSTRAINT [FK_InvoiceDetail_Inventory_User_cu]
GO
ALTER TABLE [dbo].[InvoiceDiscount]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDiscount_DiscountType_p] FOREIGN KEY([DisountType_P_ID])
REFERENCES [dbo].[DiscountType_p] ([ID])
GO
ALTER TABLE [dbo].[InvoiceDiscount] CHECK CONSTRAINT [FK_InvoiceDiscount_DiscountType_p]
GO
ALTER TABLE [dbo].[InvoiceDiscount]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDiscount_Invoice] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[Invoice] ([ID])
GO
ALTER TABLE [dbo].[InvoiceDiscount] CHECK CONSTRAINT [FK_InvoiceDiscount_Invoice]
GO
ALTER TABLE [dbo].[InvoiceDiscount]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDiscount_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InvoiceDiscount] CHECK CONSTRAINT [FK_InvoiceDiscount_User_cu]
GO
ALTER TABLE [dbo].[InvoicePayment]  WITH CHECK ADD  CONSTRAINT [FK_InvoicePayment_Invoice] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[Invoice] ([ID])
GO
ALTER TABLE [dbo].[InvoicePayment] CHECK CONSTRAINT [FK_InvoicePayment_Invoice]
GO
ALTER TABLE [dbo].[InvoicePayment]  WITH CHECK ADD  CONSTRAINT [FK_InvoicePayment_PaymentType_p] FOREIGN KEY([PaymentType_P_ID])
REFERENCES [dbo].[PaymentType_p] ([ID])
GO
ALTER TABLE [dbo].[InvoicePayment] CHECK CONSTRAINT [FK_InvoicePayment_PaymentType_p]
GO
ALTER TABLE [dbo].[InvoicePayment]  WITH CHECK ADD  CONSTRAINT [FK_InvoicePayment_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InvoicePayment] CHECK CONSTRAINT [FK_InvoicePayment_User_cu]
GO
ALTER TABLE [dbo].[InvoicePayment_Check]  WITH CHECK ADD  CONSTRAINT [FK_InvoicePayment_Check_Bank_cu] FOREIGN KEY([Bank_CU_ID])
REFERENCES [dbo].[Bank_cu] ([ID])
GO
ALTER TABLE [dbo].[InvoicePayment_Check] CHECK CONSTRAINT [FK_InvoicePayment_Check_Bank_cu]
GO
ALTER TABLE [dbo].[InvoicePayment_Check]  WITH CHECK ADD  CONSTRAINT [FK_InvoicePayment_Check_BankAccount_cu] FOREIGN KEY([BankAccoumt_CU_ID])
REFERENCES [dbo].[BankAccount_cu] ([ID])
GO
ALTER TABLE [dbo].[InvoicePayment_Check] CHECK CONSTRAINT [FK_InvoicePayment_Check_BankAccount_cu]
GO
ALTER TABLE [dbo].[InvoicePayment_Check]  WITH CHECK ADD  CONSTRAINT [FK_InvoicePayment_Check_InvoicePayment] FOREIGN KEY([InvoicePaymentID])
REFERENCES [dbo].[InvoicePayment] ([ID])
GO
ALTER TABLE [dbo].[InvoicePayment_Check] CHECK CONSTRAINT [FK_InvoicePayment_Check_InvoicePayment]
GO
ALTER TABLE [dbo].[InvoicePayment_Check]  WITH CHECK ADD  CONSTRAINT [FK_InvoicePayment_Check_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InvoicePayment_Check] CHECK CONSTRAINT [FK_InvoicePayment_Check_User_cu]
GO
ALTER TABLE [dbo].[InvoicePayment_Visa]  WITH CHECK ADD  CONSTRAINT [FK_InvoicePayment_Visa_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InvoicePayment_Visa] CHECK CONSTRAINT [FK_InvoicePayment_Visa_User_cu]
GO
ALTER TABLE [dbo].[InvoicePayment_Visa]  WITH CHECK ADD  CONSTRAINT [FK_InvoicePyament_Visa_Bank_cu] FOREIGN KEY([Bank_CU_ID])
REFERENCES [dbo].[Bank_cu] ([ID])
GO
ALTER TABLE [dbo].[InvoicePayment_Visa] CHECK CONSTRAINT [FK_InvoicePyament_Visa_Bank_cu]
GO
ALTER TABLE [dbo].[InvoicePayment_Visa]  WITH CHECK ADD  CONSTRAINT [FK_InvoicePyament_Visa_BankAccount_cu] FOREIGN KEY([BankAccount_CU_ID])
REFERENCES [dbo].[BankAccount_cu] ([ID])
GO
ALTER TABLE [dbo].[InvoicePayment_Visa] CHECK CONSTRAINT [FK_InvoicePyament_Visa_BankAccount_cu]
GO
ALTER TABLE [dbo].[InvoicePayment_Visa]  WITH CHECK ADD  CONSTRAINT [FK_InvoicePyament_Visa_InvoicePayment] FOREIGN KEY([InvoicePaymentID])
REFERENCES [dbo].[InvoicePayment] ([ID])
GO
ALTER TABLE [dbo].[InvoicePayment_Visa] CHECK CONSTRAINT [FK_InvoicePyament_Visa_InvoicePayment]
GO
ALTER TABLE [dbo].[InvoiceRequestedAmount]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceRequestedAmount_Invoice] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[Invoice] ([ID])
GO
ALTER TABLE [dbo].[InvoiceRequestedAmount] CHECK CONSTRAINT [FK_InvoiceRequestedAmount_Invoice]
GO
ALTER TABLE [dbo].[InvoiceRequestedAmount]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceRequestedAmount_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InvoiceRequestedAmount] CHECK CONSTRAINT [FK_InvoiceRequestedAmount_User_cu]
GO
ALTER TABLE [dbo].[InvoiceShare]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceShare_Invoice] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[Invoice] ([ID])
GO
ALTER TABLE [dbo].[InvoiceShare] CHECK CONSTRAINT [FK_InvoiceShare_Invoice]
GO
ALTER TABLE [dbo].[InvoiceShare]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceShare_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InvoiceShare] CHECK CONSTRAINT [FK_InvoiceShare_User_cu]
GO
ALTER TABLE [dbo].[InvoiceType_Surcharge_cu]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceType_Surcharge_cu_InvoiceType_p] FOREIGN KEY([InvoiceType_P_ID])
REFERENCES [dbo].[InvoiceType_p] ([ID])
GO
ALTER TABLE [dbo].[InvoiceType_Surcharge_cu] CHECK CONSTRAINT [FK_InvoiceType_Surcharge_cu_InvoiceType_p]
GO
ALTER TABLE [dbo].[InvoiceType_Surcharge_cu]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceType_Surcharge_cu_Surcharge_cu] FOREIGN KEY([Surcharge_CU_ID])
REFERENCES [dbo].[Surcharge_cu] ([ID])
GO
ALTER TABLE [dbo].[InvoiceType_Surcharge_cu] CHECK CONSTRAINT [FK_InvoiceType_Surcharge_cu_Surcharge_cu]
GO
ALTER TABLE [dbo].[InvoiceType_Surcharge_cu]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceType_Surcharge_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[InvoiceType_Surcharge_cu] CHECK CONSTRAINT [FK_InvoiceType_Surcharge_cu_User_cu]
GO
ALTER TABLE [dbo].[JobTitle_cu]  WITH CHECK ADD  CONSTRAINT [FK_JobTitle_cu_User_cu] FOREIGN KEY([nvarchar(150)])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[JobTitle_cu] CHECK CONSTRAINT [FK_JobTitle_cu_User_cu]
GO
ALTER TABLE [dbo].[Location_cu]  WITH CHECK ADD  CONSTRAINT [FK_Location_cu_City_cu] FOREIGN KEY([City_CU_ID])
REFERENCES [dbo].[City_cu] ([ID])
GO
ALTER TABLE [dbo].[Location_cu] CHECK CONSTRAINT [FK_Location_cu_City_cu]
GO
ALTER TABLE [dbo].[Location_cu]  WITH CHECK ADD  CONSTRAINT [FK_Location_cu_Country_cu] FOREIGN KEY([Country_CU_ID])
REFERENCES [dbo].[Country_cu] ([ID])
GO
ALTER TABLE [dbo].[Location_cu] CHECK CONSTRAINT [FK_Location_cu_Country_cu]
GO
ALTER TABLE [dbo].[Location_cu]  WITH CHECK ADD  CONSTRAINT [FK_Location_cu_Organization_cu] FOREIGN KEY([Organization_P_ID])
REFERENCES [dbo].[Organization_p] ([ID])
GO
ALTER TABLE [dbo].[Location_cu] CHECK CONSTRAINT [FK_Location_cu_Organization_cu]
GO
ALTER TABLE [dbo].[Location_cu]  WITH CHECK ADD  CONSTRAINT [FK_Location_cu_Region_cu] FOREIGN KEY([Region_CU_ID])
REFERENCES [dbo].[Region_cu] ([ID])
GO
ALTER TABLE [dbo].[Location_cu] CHECK CONSTRAINT [FK_Location_cu_Region_cu]
GO
ALTER TABLE [dbo].[Location_cu]  WITH CHECK ADD  CONSTRAINT [FK_Location_cu_Terriotry_cu] FOREIGN KEY([Territory_CU_ID])
REFERENCES [dbo].[Terriotry_cu] ([ID])
GO
ALTER TABLE [dbo].[Location_cu] CHECK CONSTRAINT [FK_Location_cu_Terriotry_cu]
GO
ALTER TABLE [dbo].[Location_cu]  WITH CHECK ADD  CONSTRAINT [FK_Location_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Location_cu] CHECK CONSTRAINT [FK_Location_cu_User_cu]
GO
ALTER TABLE [dbo].[Manager_cu]  WITH CHECK ADD  CONSTRAINT [FK_Manager_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Manager_cu] CHECK CONSTRAINT [FK_Manager_cu_User_cu]
GO
ALTER TABLE [dbo].[MedicalStage_cu]  WITH CHECK ADD  CONSTRAINT [FK_MedicalStage_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[MedicalStage_cu] CHECK CONSTRAINT [FK_MedicalStage_cu_User_cu]
GO
ALTER TABLE [dbo].[Organization_p]  WITH CHECK ADD  CONSTRAINT [FK_Organization_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Organization_p] CHECK CONSTRAINT [FK_Organization_cu_User_cu]
GO
ALTER TABLE [dbo].[Patient_cu]  WITH CHECK ADD  CONSTRAINT [FK_Patient_cu_InsuranceCarrier_InsuranceLevel_cu] FOREIGN KEY([InsuranceCarrier_InsuranceLevel_CU_ID])
REFERENCES [dbo].[InsuranceCarrier_InsuranceLevel_cu] ([ID])
GO
ALTER TABLE [dbo].[Patient_cu] CHECK CONSTRAINT [FK_Patient_cu_InsuranceCarrier_InsuranceLevel_cu]
GO
ALTER TABLE [dbo].[Patient_cu]  WITH CHECK ADD  CONSTRAINT [FK_Patient_cu_Person_cu] FOREIGN KEY([Person_CU_ID])
REFERENCES [dbo].[Person_cu] ([ID])
GO
ALTER TABLE [dbo].[Patient_cu] CHECK CONSTRAINT [FK_Patient_cu_Person_cu]
GO
ALTER TABLE [dbo].[Patient_cu]  WITH CHECK ADD  CONSTRAINT [FK_Patient_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Patient_cu] CHECK CONSTRAINT [FK_Patient_cu_User_cu]
GO
ALTER TABLE [dbo].[PatientAttachment]  WITH CHECK ADD  CONSTRAINT [FK_PatientAttachment_cu_ImageType_p] FOREIGN KEY([ImageType_P_ID])
REFERENCES [dbo].[ImageType_p] ([ID])
GO
ALTER TABLE [dbo].[PatientAttachment] CHECK CONSTRAINT [FK_PatientAttachment_cu_ImageType_p]
GO
ALTER TABLE [dbo].[PatientAttachment]  WITH CHECK ADD  CONSTRAINT [FK_PatientAttachment_cu_Patient_cu] FOREIGN KEY([Patient_CU_ID])
REFERENCES [dbo].[Patient_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[PatientAttachment] CHECK CONSTRAINT [FK_PatientAttachment_cu_Patient_cu]
GO
ALTER TABLE [dbo].[PatientAttachment]  WITH CHECK ADD  CONSTRAINT [FK_PatientAttachment_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[PatientAttachment] CHECK CONSTRAINT [FK_PatientAttachment_User_cu]
GO
ALTER TABLE [dbo].[Person_cu]  WITH CHECK ADD  CONSTRAINT [FK_Person_cu_IdentificationCardType_p] FOREIGN KEY([IdentificationCardType_CU_ID])
REFERENCES [dbo].[IdentificationCardType_p] ([ID])
GO
ALTER TABLE [dbo].[Person_cu] CHECK CONSTRAINT [FK_Person_cu_IdentificationCardType_p]
GO
ALTER TABLE [dbo].[Person_cu]  WITH CHECK ADD  CONSTRAINT [FK_Person_cu_MaritalStatus_p] FOREIGN KEY([MaritalStatus_P_ID])
REFERENCES [dbo].[MaritalStatus_p] ([ID])
GO
ALTER TABLE [dbo].[Person_cu] CHECK CONSTRAINT [FK_Person_cu_MaritalStatus_p]
GO
ALTER TABLE [dbo].[Person_cu]  WITH CHECK ADD  CONSTRAINT [FK_Person_cu_PersonTitle_p] FOREIGN KEY([PersonTitle_P_ID])
REFERENCES [dbo].[PersonTitle_p] ([ID])
GO
ALTER TABLE [dbo].[Person_cu] CHECK CONSTRAINT [FK_Person_cu_PersonTitle_p]
GO
ALTER TABLE [dbo].[Person_cu]  WITH CHECK ADD  CONSTRAINT [FK_Person_cu_Religion_p] FOREIGN KEY([Religion_P_ID])
REFERENCES [dbo].[Religion_p] ([ID])
GO
ALTER TABLE [dbo].[Person_cu] CHECK CONSTRAINT [FK_Person_cu_Religion_p]
GO
ALTER TABLE [dbo].[Person_IdentificationCardType_cu]  WITH CHECK ADD  CONSTRAINT [FK_Person_IdentificationCardsType_cu_Person_cu] FOREIGN KEY([Person_CU_ID])
REFERENCES [dbo].[Person_cu] ([ID])
GO
ALTER TABLE [dbo].[Person_IdentificationCardType_cu] CHECK CONSTRAINT [FK_Person_IdentificationCardsType_cu_Person_cu]
GO
ALTER TABLE [dbo].[Person_IdentificationCardType_cu]  WITH CHECK ADD  CONSTRAINT [FK_Person_IdentificationCardType_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Person_IdentificationCardType_cu] CHECK CONSTRAINT [FK_Person_IdentificationCardType_cu_User_cu]
GO
ALTER TABLE [dbo].[Person_Phone_cu]  WITH CHECK ADD  CONSTRAINT [FK_Person_Phone_cu_Person_cu] FOREIGN KEY([Person_CU_ID])
REFERENCES [dbo].[Person_cu] ([ID])
GO
ALTER TABLE [dbo].[Person_Phone_cu] CHECK CONSTRAINT [FK_Person_Phone_cu_Person_cu]
GO
ALTER TABLE [dbo].[Person_Phone_cu]  WITH CHECK ADD  CONSTRAINT [FK_Person_Phone_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Person_Phone_cu] CHECK CONSTRAINT [FK_Person_Phone_cu_User_cu]
GO
ALTER TABLE [dbo].[QueueManager]  WITH CHECK ADD  CONSTRAINT [FK_QueueManager_Doctor_cu] FOREIGN KEY([Doctor_CU_ID])
REFERENCES [dbo].[Doctor_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[QueueManager] CHECK CONSTRAINT [FK_QueueManager_Doctor_cu]
GO
ALTER TABLE [dbo].[QueueManager]  WITH CHECK ADD  CONSTRAINT [FK_QueueManager_InvoiceDetail] FOREIGN KEY([InvoiceDetailID])
REFERENCES [dbo].[InvoiceDetail] ([ID])
GO
ALTER TABLE [dbo].[QueueManager] CHECK CONSTRAINT [FK_QueueManager_InvoiceDetail]
GO
ALTER TABLE [dbo].[QueueManager]  WITH CHECK ADD  CONSTRAINT [FK_QueueManager_Patient_cu] FOREIGN KEY([Patient_CU_ID])
REFERENCES [dbo].[Patient_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[QueueManager] CHECK CONSTRAINT [FK_QueueManager_Patient_cu]
GO
ALTER TABLE [dbo].[QueueManager]  WITH CHECK ADD  CONSTRAINT [FK_QueueManager_QueueManagerStatus_p] FOREIGN KEY([QueueManagerStatus_P_ID])
REFERENCES [dbo].[QueueManagerStatus_p] ([ID])
GO
ALTER TABLE [dbo].[QueueManager] CHECK CONSTRAINT [FK_QueueManager_QueueManagerStatus_p]
GO
ALTER TABLE [dbo].[QueueManager]  WITH CHECK ADD  CONSTRAINT [FK_QueueManager_Service_cu] FOREIGN KEY([Service_CU_ID])
REFERENCES [dbo].[Service_cu] ([ID])
GO
ALTER TABLE [dbo].[QueueManager] CHECK CONSTRAINT [FK_QueueManager_Service_cu]
GO
ALTER TABLE [dbo].[QueueManager]  WITH CHECK ADD  CONSTRAINT [FK_QueueManager_StationPoint_cu] FOREIGN KEY([StationPoint_CU_ID])
REFERENCES [dbo].[StationPoint_cu] ([ID])
GO
ALTER TABLE [dbo].[QueueManager] CHECK CONSTRAINT [FK_QueueManager_StationPoint_cu]
GO
ALTER TABLE [dbo].[QueueManager]  WITH CHECK ADD  CONSTRAINT [FK_QueueManager_StationPointStage_cu] FOREIGN KEY([StationPointStage_CU_ID])
REFERENCES [dbo].[StationPointStage_cu] ([ID])
GO
ALTER TABLE [dbo].[QueueManager] CHECK CONSTRAINT [FK_QueueManager_StationPointStage_cu]
GO
ALTER TABLE [dbo].[QueueManager]  WITH CHECK ADD  CONSTRAINT [FK_QueueManager_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[QueueManager] CHECK CONSTRAINT [FK_QueueManager_User_cu]
GO
ALTER TABLE [dbo].[Region_cu]  WITH CHECK ADD  CONSTRAINT [FK_Region_cu_City_cu] FOREIGN KEY([City_CU_ID])
REFERENCES [dbo].[City_cu] ([ID])
GO
ALTER TABLE [dbo].[Region_cu] CHECK CONSTRAINT [FK_Region_cu_City_cu]
GO
ALTER TABLE [dbo].[Region_cu]  WITH CHECK ADD  CONSTRAINT [FK_Region_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Region_cu] CHECK CONSTRAINT [FK_Region_cu_User_cu]
GO
ALTER TABLE [dbo].[Role_p]  WITH CHECK ADD  CONSTRAINT [FK_Role_p_Application_p] FOREIGN KEY([Application_P_ID])
REFERENCES [dbo].[Application_p] ([ID])
GO
ALTER TABLE [dbo].[Role_p] CHECK CONSTRAINT [FK_Role_p_Application_p]
GO
ALTER TABLE [dbo].[RoleRegistration_cu]  WITH CHECK ADD  CONSTRAINT [FK_RoleRegistration_cu_Role_p] FOREIGN KEY([Role_P_ID])
REFERENCES [dbo].[Role_p] ([ID])
GO
ALTER TABLE [dbo].[RoleRegistration_cu] CHECK CONSTRAINT [FK_RoleRegistration_cu_Role_p]
GO
ALTER TABLE [dbo].[RoleRegistration_cu]  WITH CHECK ADD  CONSTRAINT [FK_RoleRegistration_cu_User_cu] FOREIGN KEY([User_CU_ID])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[RoleRegistration_cu] CHECK CONSTRAINT [FK_RoleRegistration_cu_User_cu]
GO
ALTER TABLE [dbo].[RoleRegistration_cu]  WITH CHECK ADD  CONSTRAINT [FK_RoleRegistration_cu_UserGroup_cu] FOREIGN KEY([UserGroup_CU_ID])
REFERENCES [dbo].[UserGroup_cu] ([ID])
GO
ALTER TABLE [dbo].[RoleRegistration_cu] CHECK CONSTRAINT [FK_RoleRegistration_cu_UserGroup_cu]
GO
ALTER TABLE [dbo].[Service_cu]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceFinancialService_cu_InvoiceFinancialServiceCategory_cu] FOREIGN KEY([ServiceCategory_CU_ID])
REFERENCES [dbo].[ServiceCategory_cu] ([ID])
GO
ALTER TABLE [dbo].[Service_cu] CHECK CONSTRAINT [FK_InvoiceFinancialService_cu_InvoiceFinancialServiceCategory_cu]
GO
ALTER TABLE [dbo].[Service_cu]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceFinancialService_cu_InvoiceFinancialServiceType_p] FOREIGN KEY([ServiceType_P_ID])
REFERENCES [dbo].[ServiceType_p] ([ID])
GO
ALTER TABLE [dbo].[Service_cu] CHECK CONSTRAINT [FK_InvoiceFinancialService_cu_InvoiceFinancialServiceType_p]
GO
ALTER TABLE [dbo].[Service_cu]  WITH CHECK ADD  CONSTRAINT [FK_Service_cu_Service_cu] FOREIGN KEY([ParentService_CU_ID])
REFERENCES [dbo].[Service_cu] ([ID])
GO
ALTER TABLE [dbo].[Service_cu] CHECK CONSTRAINT [FK_Service_cu_Service_cu]
GO
ALTER TABLE [dbo].[Service_cu]  WITH CHECK ADD  CONSTRAINT [FK_Service_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Service_cu] CHECK CONSTRAINT [FK_Service_cu_User_cu]
GO
ALTER TABLE [dbo].[Service_StationPoint_cu]  WITH CHECK ADD  CONSTRAINT [FK_Service_StationPoint_cu_Service_cu] FOREIGN KEY([Service_CU_ID])
REFERENCES [dbo].[Service_cu] ([ID])
GO
ALTER TABLE [dbo].[Service_StationPoint_cu] CHECK CONSTRAINT [FK_Service_StationPoint_cu_Service_cu]
GO
ALTER TABLE [dbo].[Service_StationPoint_cu]  WITH CHECK ADD  CONSTRAINT [FK_Service_StationPoint_cu_StationPoint_cu] FOREIGN KEY([StationPoint_CU_ID])
REFERENCES [dbo].[StationPoint_cu] ([ID])
GO
ALTER TABLE [dbo].[Service_StationPoint_cu] CHECK CONSTRAINT [FK_Service_StationPoint_cu_StationPoint_cu]
GO
ALTER TABLE [dbo].[Service_StationPoint_cu]  WITH CHECK ADD  CONSTRAINT [FK_Service_StationPoint_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Service_StationPoint_cu] CHECK CONSTRAINT [FK_Service_StationPoint_cu_User_cu]
GO
ALTER TABLE [dbo].[Service_Surcharge_cu]  WITH CHECK ADD  CONSTRAINT [FK_Service_Surcharge_cu_InvoiceType_p] FOREIGN KEY([InvoiceType_P_ID])
REFERENCES [dbo].[InvoiceType_p] ([ID])
GO
ALTER TABLE [dbo].[Service_Surcharge_cu] CHECK CONSTRAINT [FK_Service_Surcharge_cu_InvoiceType_p]
GO
ALTER TABLE [dbo].[Service_Surcharge_cu]  WITH CHECK ADD  CONSTRAINT [FK_Service_Surcharge_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Service_Surcharge_cu] CHECK CONSTRAINT [FK_Service_Surcharge_cu_User_cu]
GO
ALTER TABLE [dbo].[Service_Surcharge_cu]  WITH CHECK ADD  CONSTRAINT [FK_Service_SurchargeType_cu_Service_cu] FOREIGN KEY([Service_CU_ID])
REFERENCES [dbo].[Service_cu] ([ID])
GO
ALTER TABLE [dbo].[Service_Surcharge_cu] CHECK CONSTRAINT [FK_Service_SurchargeType_cu_Service_cu]
GO
ALTER TABLE [dbo].[Service_Surcharge_cu]  WITH CHECK ADD  CONSTRAINT [FK_Service_SurchargeType_cu_SurchargeType_cu] FOREIGN KEY([Surcharge_CU_ID])
REFERENCES [dbo].[Surcharge_cu] ([ID])
GO
ALTER TABLE [dbo].[Service_Surcharge_cu] CHECK CONSTRAINT [FK_Service_SurchargeType_cu_SurchargeType_cu]
GO
ALTER TABLE [dbo].[ServiceCategory_cu]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceServiceCategory_cu_InvoiceServiceType_p] FOREIGN KEY([ServiceType_P_ID])
REFERENCES [dbo].[ServiceType_p] ([ID])
GO
ALTER TABLE [dbo].[ServiceCategory_cu] CHECK CONSTRAINT [FK_InvoiceServiceCategory_cu_InvoiceServiceType_p]
GO
ALTER TABLE [dbo].[ServiceCategory_cu]  WITH CHECK ADD  CONSTRAINT [FK_ServiceCategory_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[ServiceCategory_cu] CHECK CONSTRAINT [FK_ServiceCategory_cu_User_cu]
GO
ALTER TABLE [dbo].[ServiceCategory_StationPoint_cu]  WITH CHECK ADD  CONSTRAINT [FK_ServiceCategory_StationPoint_cu_ServiceCategory_cu] FOREIGN KEY([ServiceCategory_CU_ID])
REFERENCES [dbo].[ServiceCategory_cu] ([ID])
GO
ALTER TABLE [dbo].[ServiceCategory_StationPoint_cu] CHECK CONSTRAINT [FK_ServiceCategory_StationPoint_cu_ServiceCategory_cu]
GO
ALTER TABLE [dbo].[ServiceCategory_StationPoint_cu]  WITH CHECK ADD  CONSTRAINT [FK_ServiceCategory_StationPoint_cu_StationPoint_cu] FOREIGN KEY([StationPoint_CU_ID])
REFERENCES [dbo].[StationPoint_cu] ([ID])
GO
ALTER TABLE [dbo].[ServiceCategory_StationPoint_cu] CHECK CONSTRAINT [FK_ServiceCategory_StationPoint_cu_StationPoint_cu]
GO
ALTER TABLE [dbo].[ServiceCategory_StationPoint_cu]  WITH CHECK ADD  CONSTRAINT [FK_ServiceCategory_StationPoint_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[ServiceCategory_StationPoint_cu] CHECK CONSTRAINT [FK_ServiceCategory_StationPoint_cu_User_cu]
GO
ALTER TABLE [dbo].[ServiceCategory_Surcharge_cu]  WITH CHECK ADD  CONSTRAINT [FK_ServiceCategory_Surcharge_cu_InvoiceType_p] FOREIGN KEY([InvoiceType_P_ID])
REFERENCES [dbo].[InvoiceType_p] ([ID])
GO
ALTER TABLE [dbo].[ServiceCategory_Surcharge_cu] CHECK CONSTRAINT [FK_ServiceCategory_Surcharge_cu_InvoiceType_p]
GO
ALTER TABLE [dbo].[ServiceCategory_Surcharge_cu]  WITH CHECK ADD  CONSTRAINT [FK_ServiceCategory_Surcharge_cu_ServiceCategory_cu] FOREIGN KEY([ServiceCategory_CU_ID])
REFERENCES [dbo].[ServiceCategory_cu] ([ID])
GO
ALTER TABLE [dbo].[ServiceCategory_Surcharge_cu] CHECK CONSTRAINT [FK_ServiceCategory_Surcharge_cu_ServiceCategory_cu]
GO
ALTER TABLE [dbo].[ServiceCategory_Surcharge_cu]  WITH CHECK ADD  CONSTRAINT [FK_ServiceCategory_Surcharge_cu_Surcharge_cu] FOREIGN KEY([Surcharge_CU_ID])
REFERENCES [dbo].[Surcharge_cu] ([ID])
GO
ALTER TABLE [dbo].[ServiceCategory_Surcharge_cu] CHECK CONSTRAINT [FK_ServiceCategory_Surcharge_cu_Surcharge_cu]
GO
ALTER TABLE [dbo].[ServiceCategory_Surcharge_cu]  WITH CHECK ADD  CONSTRAINT [FK_ServiceCategory_Surcharge_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[ServiceCategory_Surcharge_cu] CHECK CONSTRAINT [FK_ServiceCategory_Surcharge_cu_User_cu]
GO
ALTER TABLE [dbo].[ServicePrice_cu]  WITH CHECK ADD  CONSTRAINT [FK_ServicePrice_cu_DoctorCategory_cu] FOREIGN KEY([DoctorCategory_CU_ID])
REFERENCES [dbo].[DoctorCategory_cu] ([ID])
GO
ALTER TABLE [dbo].[ServicePrice_cu] CHECK CONSTRAINT [FK_ServicePrice_cu_DoctorCategory_cu]
GO
ALTER TABLE [dbo].[ServicePrice_cu]  WITH CHECK ADD  CONSTRAINT [FK_ServicePrice_cu_DoctorRank_p] FOREIGN KEY([DoctorRank_P_ID])
REFERENCES [dbo].[DoctorRank_p] ([ID])
GO
ALTER TABLE [dbo].[ServicePrice_cu] CHECK CONSTRAINT [FK_ServicePrice_cu_DoctorRank_p]
GO
ALTER TABLE [dbo].[ServicePrice_cu]  WITH CHECK ADD  CONSTRAINT [FK_ServicePrice_cu_DoctorSpecialization_p] FOREIGN KEY([DoctorSpecialization_P_ID])
REFERENCES [dbo].[DoctorSpecialization_p] ([ID])
GO
ALTER TABLE [dbo].[ServicePrice_cu] CHECK CONSTRAINT [FK_ServicePrice_cu_DoctorSpecialization_p]
GO
ALTER TABLE [dbo].[ServicePrice_cu]  WITH CHECK ADD  CONSTRAINT [FK_ServicePrice_cu_InsuranceCarrier_InsuranceLevel_cu] FOREIGN KEY([InsuranceCarrier_InsuranceLevel_CU_ID])
REFERENCES [dbo].[InsuranceCarrier_InsuranceLevel_cu] ([ID])
GO
ALTER TABLE [dbo].[ServicePrice_cu] CHECK CONSTRAINT [FK_ServicePrice_cu_InsuranceCarrier_InsuranceLevel_cu]
GO
ALTER TABLE [dbo].[ServicePrice_cu]  WITH CHECK ADD  CONSTRAINT [FK_ServicePrice_cu_Service_cu] FOREIGN KEY([Service_CU_ID])
REFERENCES [dbo].[Service_cu] ([ID])
GO
ALTER TABLE [dbo].[ServicePrice_cu] CHECK CONSTRAINT [FK_ServicePrice_cu_Service_cu]
GO
ALTER TABLE [dbo].[ServicePrice_cu]  WITH CHECK ADD  CONSTRAINT [FK_ServicePrice_cu_ServiceCategory_cu] FOREIGN KEY([ServiceCategory_CU_ID])
REFERENCES [dbo].[ServiceCategory_cu] ([ID])
GO
ALTER TABLE [dbo].[ServicePrice_cu] CHECK CONSTRAINT [FK_ServicePrice_cu_ServiceCategory_cu]
GO
ALTER TABLE [dbo].[ServicePrice_cu]  WITH CHECK ADD  CONSTRAINT [FK_ServicePrice_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[ServicePrice_cu] CHECK CONSTRAINT [FK_ServicePrice_cu_User_cu]
GO
ALTER TABLE [dbo].[ServiceType_StationPoint_cu]  WITH CHECK ADD  CONSTRAINT [FK_ServiceType_StationPoint_cu_ServiceType_p] FOREIGN KEY([ServiceType_P_ID])
REFERENCES [dbo].[ServiceType_p] ([ID])
GO
ALTER TABLE [dbo].[ServiceType_StationPoint_cu] CHECK CONSTRAINT [FK_ServiceType_StationPoint_cu_ServiceType_p]
GO
ALTER TABLE [dbo].[ServiceType_StationPoint_cu]  WITH CHECK ADD  CONSTRAINT [FK_ServiceType_StationPoint_cu_StationPoint_cu] FOREIGN KEY([StationPoint_CU_ID])
REFERENCES [dbo].[StationPoint_cu] ([ID])
GO
ALTER TABLE [dbo].[ServiceType_StationPoint_cu] CHECK CONSTRAINT [FK_ServiceType_StationPoint_cu_StationPoint_cu]
GO
ALTER TABLE [dbo].[ServiceType_StationPoint_cu]  WITH CHECK ADD  CONSTRAINT [FK_ServiceType_StationPoint_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[ServiceType_StationPoint_cu] CHECK CONSTRAINT [FK_ServiceType_StationPoint_cu_User_cu]
GO
ALTER TABLE [dbo].[ServiceType_Surcharge_cu]  WITH CHECK ADD  CONSTRAINT [FK_ServiceType_Surcharge_cu_InvoiceType_p] FOREIGN KEY([InvoiceType_P_ID])
REFERENCES [dbo].[InvoiceType_p] ([ID])
GO
ALTER TABLE [dbo].[ServiceType_Surcharge_cu] CHECK CONSTRAINT [FK_ServiceType_Surcharge_cu_InvoiceType_p]
GO
ALTER TABLE [dbo].[ServiceType_Surcharge_cu]  WITH CHECK ADD  CONSTRAINT [FK_ServiceType_Surcharge_cu_ServiceType_p] FOREIGN KEY([ServiceType_P_ID])
REFERENCES [dbo].[ServiceType_p] ([ID])
GO
ALTER TABLE [dbo].[ServiceType_Surcharge_cu] CHECK CONSTRAINT [FK_ServiceType_Surcharge_cu_ServiceType_p]
GO
ALTER TABLE [dbo].[ServiceType_Surcharge_cu]  WITH CHECK ADD  CONSTRAINT [FK_ServiceType_Surcharge_cu_Surcharge_cu] FOREIGN KEY([Surcharge_CU_ID])
REFERENCES [dbo].[Surcharge_cu] ([ID])
GO
ALTER TABLE [dbo].[ServiceType_Surcharge_cu] CHECK CONSTRAINT [FK_ServiceType_Surcharge_cu_Surcharge_cu]
GO
ALTER TABLE [dbo].[ServiceType_Surcharge_cu]  WITH CHECK ADD  CONSTRAINT [FK_ServiceType_Surcharge_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[ServiceType_Surcharge_cu] CHECK CONSTRAINT [FK_ServiceType_Surcharge_cu_User_cu]
GO
ALTER TABLE [dbo].[StationPoint_cu]  WITH CHECK ADD  CONSTRAINT [FK_StationPoint_cu_Station_p] FOREIGN KEY([Station_P_ID])
REFERENCES [dbo].[Station_p] ([ID])
GO
ALTER TABLE [dbo].[StationPoint_cu] CHECK CONSTRAINT [FK_StationPoint_cu_Station_p]
GO
ALTER TABLE [dbo].[StationPoint_cu]  WITH CHECK ADD  CONSTRAINT [FK_StationPoint_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[StationPoint_cu] CHECK CONSTRAINT [FK_StationPoint_cu_User_cu]
GO
ALTER TABLE [dbo].[StationPointStage_cu]  WITH CHECK ADD  CONSTRAINT [FK_StationPointStage_cu_Floor_cu] FOREIGN KEY([Floor_CU_ID])
REFERENCES [dbo].[Floor_cu] ([ID])
GO
ALTER TABLE [dbo].[StationPointStage_cu] CHECK CONSTRAINT [FK_StationPointStage_cu_Floor_cu]
GO
ALTER TABLE [dbo].[StationPointStage_cu]  WITH CHECK ADD  CONSTRAINT [FK_StationPointStage_cu_StationPoint_cu] FOREIGN KEY([StationPoint_CU_ID])
REFERENCES [dbo].[StationPoint_cu] ([ID])
GO
ALTER TABLE [dbo].[StationPointStage_cu] CHECK CONSTRAINT [FK_StationPointStage_cu_StationPoint_cu]
GO
ALTER TABLE [dbo].[StationPointStage_cu]  WITH CHECK ADD  CONSTRAINT [FK_StationPointStage_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[StationPointStage_cu] CHECK CONSTRAINT [FK_StationPointStage_cu_User_cu]
GO
ALTER TABLE [dbo].[Supplier_cu]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_cu_Person_cu] FOREIGN KEY([Person_CU_ID])
REFERENCES [dbo].[Person_cu] ([ID])
GO
ALTER TABLE [dbo].[Supplier_cu] CHECK CONSTRAINT [FK_Supplier_cu_Person_cu]
GO
ALTER TABLE [dbo].[Supplier_cu]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_cu_Supplier_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Supplier_cu] CHECK CONSTRAINT [FK_Supplier_cu_Supplier_cu]
GO
ALTER TABLE [dbo].[Supplier_cu]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_cu_SupplierType_p] FOREIGN KEY([SupplierType_P_ID])
REFERENCES [dbo].[SupplierType_p] ([ID])
GO
ALTER TABLE [dbo].[Supplier_cu] CHECK CONSTRAINT [FK_Supplier_cu_SupplierType_p]
GO
ALTER TABLE [dbo].[Surcharge_cu]  WITH CHECK ADD  CONSTRAINT [FK_Surcharge_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Surcharge_cu] CHECK CONSTRAINT [FK_Surcharge_cu_User_cu]
GO
ALTER TABLE [dbo].[Surcharge_cu]  WITH CHECK ADD  CONSTRAINT [FK_SurchargeType_cu_SuchargeType_p] FOREIGN KEY([SurchargeType_P_ID])
REFERENCES [dbo].[SurchargeType_p] ([ID])
GO
ALTER TABLE [dbo].[Surcharge_cu] CHECK CONSTRAINT [FK_SurchargeType_cu_SuchargeType_p]
GO
ALTER TABLE [dbo].[Terriotry_cu]  WITH CHECK ADD  CONSTRAINT [FK_Terriotry_cu_Region_cu] FOREIGN KEY([Region_CU_ID])
REFERENCES [dbo].[Region_cu] ([ID])
GO
ALTER TABLE [dbo].[Terriotry_cu] CHECK CONSTRAINT [FK_Terriotry_cu_Region_cu]
GO
ALTER TABLE [dbo].[Terriotry_cu]  WITH CHECK ADD  CONSTRAINT [FK_Terriotry_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[Terriotry_cu] CHECK CONSTRAINT [FK_Terriotry_cu_User_cu]
GO
ALTER TABLE [dbo].[TrialBalanceTransaction]  WITH CHECK ADD  CONSTRAINT [FK_ChartOfAccountBalance_ChartOfAccount_cu] FOREIGN KEY([ChartOfAccount_CU_ID])
REFERENCES [dbo].[ChartOfAccount_cu] ([ID])
GO
ALTER TABLE [dbo].[TrialBalanceTransaction] CHECK CONSTRAINT [FK_ChartOfAccountBalance_ChartOfAccount_cu]
GO
ALTER TABLE [dbo].[TrialBalanceTransaction]  WITH CHECK ADD  CONSTRAINT [FK_ChartOfAccountBalance_FinancialInterval_cu] FOREIGN KEY([FinancialInterval_CU_ID])
REFERENCES [dbo].[FinancialInterval_cu] ([ID])
GO
ALTER TABLE [dbo].[TrialBalanceTransaction] CHECK CONSTRAINT [FK_ChartOfAccountBalance_FinancialInterval_cu]
GO
ALTER TABLE [dbo].[TrialBalanceTransaction]  WITH CHECK ADD  CONSTRAINT [FK_TrialBalanceTransaction_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[TrialBalanceTransaction] CHECK CONSTRAINT [FK_TrialBalanceTransaction_User_cu]
GO
ALTER TABLE [dbo].[UnitMeasurment_cu]  WITH CHECK ADD  CONSTRAINT [FK_UnitMeasurment_cu_UnitMeasurment_p] FOREIGN KEY([UnitMeasurment_P_ID])
REFERENCES [dbo].[UnitMeasurment_p] ([ID])
GO
ALTER TABLE [dbo].[UnitMeasurment_cu] CHECK CONSTRAINT [FK_UnitMeasurment_cu_UnitMeasurment_p]
GO
ALTER TABLE [dbo].[UnitMeasurment_cu]  WITH CHECK ADD  CONSTRAINT [FK_UnitMeasurment_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[UnitMeasurment_cu] CHECK CONSTRAINT [FK_UnitMeasurment_cu_User_cu]
GO
ALTER TABLE [dbo].[UnitMeasurmentTreeLink_cu]  WITH CHECK ADD  CONSTRAINT [FK_UnitMeasurmentTreeLink_cu_UnitMeasurment_cu] FOREIGN KEY([ParentUnitMeasurment_CU_ID])
REFERENCES [dbo].[UnitMeasurment_cu] ([ID])
GO
ALTER TABLE [dbo].[UnitMeasurmentTreeLink_cu] CHECK CONSTRAINT [FK_UnitMeasurmentTreeLink_cu_UnitMeasurment_cu]
GO
ALTER TABLE [dbo].[UnitMeasurmentTreeLink_cu]  WITH CHECK ADD  CONSTRAINT [FK_UnitMeasurmentTreeLink_cu_UnitMeasurment_cu1] FOREIGN KEY([ChildUnitMeasurment_CU_ID])
REFERENCES [dbo].[UnitMeasurment_cu] ([ID])
GO
ALTER TABLE [dbo].[UnitMeasurmentTreeLink_cu] CHECK CONSTRAINT [FK_UnitMeasurmentTreeLink_cu_UnitMeasurment_cu1]
GO
ALTER TABLE [dbo].[UnitMeasurmentTreeLink_cu]  WITH CHECK ADD  CONSTRAINT [FK_UnitMeasurmentTreeLink_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[UnitMeasurmentTreeLink_cu] CHECK CONSTRAINT [FK_UnitMeasurmentTreeLink_cu_User_cu]
GO
ALTER TABLE [dbo].[User_Application_cu]  WITH CHECK ADD  CONSTRAINT [FK_User_Application_cu_Application_p] FOREIGN KEY([Application_P_ID])
REFERENCES [dbo].[Application_p] ([ID])
GO
ALTER TABLE [dbo].[User_Application_cu] CHECK CONSTRAINT [FK_User_Application_cu_Application_p]
GO
ALTER TABLE [dbo].[User_Application_cu]  WITH CHECK ADD  CONSTRAINT [FK_User_Application_cu_User_cu] FOREIGN KEY([User_CU_ID])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[User_Application_cu] CHECK CONSTRAINT [FK_User_Application_cu_User_cu]
GO
ALTER TABLE [dbo].[User_Application_cu]  WITH CHECK ADD  CONSTRAINT [FK_User_Application_cu_User_cu1] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[User_Application_cu] CHECK CONSTRAINT [FK_User_Application_cu_User_cu1]
GO
ALTER TABLE [dbo].[User_cu]  WITH CHECK ADD  CONSTRAINT [FK_User_cu_GeneralChartOfAccountType_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[GeneralChartOfAccountType_cu] ([ID])
GO
ALTER TABLE [dbo].[User_cu] CHECK CONSTRAINT [FK_User_cu_GeneralChartOfAccountType_cu]
GO
ALTER TABLE [dbo].[User_cu]  WITH CHECK ADD  CONSTRAINT [FK_User_cu_Organization_cu] FOREIGN KEY([OragnizationID])
REFERENCES [dbo].[Organization_p] ([ID])
GO
ALTER TABLE [dbo].[User_cu] CHECK CONSTRAINT [FK_User_cu_Organization_cu]
GO
ALTER TABLE [dbo].[User_cu]  WITH CHECK ADD  CONSTRAINT [FK_User_cu_Person_cu] FOREIGN KEY([Person_CU_ID])
REFERENCES [dbo].[Person_cu] ([ID])
GO
ALTER TABLE [dbo].[User_cu] CHECK CONSTRAINT [FK_User_cu_Person_cu]
GO
ALTER TABLE [dbo].[User_UserGroup_cu]  WITH CHECK ADD  CONSTRAINT [FK_User_UserGroup_cu_User_cu] FOREIGN KEY([User_CU_ID])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[User_UserGroup_cu] CHECK CONSTRAINT [FK_User_UserGroup_cu_User_cu]
GO
ALTER TABLE [dbo].[User_UserGroup_cu]  WITH CHECK ADD  CONSTRAINT [FK_User_UserGroup_cu_User_cu1] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[User_UserGroup_cu] CHECK CONSTRAINT [FK_User_UserGroup_cu_User_cu1]
GO
ALTER TABLE [dbo].[User_UserGroup_cu]  WITH CHECK ADD  CONSTRAINT [FK_User_UserGroup_cu_UserGroup_cu] FOREIGN KEY([UserGroup_CU_ID])
REFERENCES [dbo].[UserGroup_cu] ([ID])
GO
ALTER TABLE [dbo].[User_UserGroup_cu] CHECK CONSTRAINT [FK_User_UserGroup_cu_UserGroup_cu]
GO
ALTER TABLE [dbo].[UserGroup_Application_cu]  WITH CHECK ADD  CONSTRAINT [FK_UserGroup_Application_cu_Application_p] FOREIGN KEY([Application_P_ID])
REFERENCES [dbo].[Application_p] ([ID])
GO
ALTER TABLE [dbo].[UserGroup_Application_cu] CHECK CONSTRAINT [FK_UserGroup_Application_cu_Application_p]
GO
ALTER TABLE [dbo].[UserGroup_Application_cu]  WITH CHECK ADD  CONSTRAINT [FK_UserGroup_Application_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[UserGroup_Application_cu] CHECK CONSTRAINT [FK_UserGroup_Application_cu_User_cu]
GO
ALTER TABLE [dbo].[UserGroup_Application_cu]  WITH CHECK ADD  CONSTRAINT [FK_UserGroup_Application_cu_UserGroup_cu] FOREIGN KEY([UserGroup_CU_ID])
REFERENCES [dbo].[UserGroup_cu] ([ID])
GO
ALTER TABLE [dbo].[UserGroup_Application_cu] CHECK CONSTRAINT [FK_UserGroup_Application_cu_UserGroup_cu]
GO
ALTER TABLE [dbo].[UserGroup_cu]  WITH CHECK ADD  CONSTRAINT [FK_UserGroup_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[UserGroup_cu] CHECK CONSTRAINT [FK_UserGroup_cu_User_cu]
GO
ALTER TABLE [dbo].[VisitTiming]  WITH CHECK ADD  CONSTRAINT [FK_PatientVisitTiming_Doctor_cu] FOREIGN KEY([Doctor_CU_ID])
REFERENCES [dbo].[Doctor_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[VisitTiming] CHECK CONSTRAINT [FK_PatientVisitTiming_Doctor_cu]
GO
ALTER TABLE [dbo].[VisitTiming]  WITH CHECK ADD  CONSTRAINT [FK_PatientVisitTiming_InvoiceDetail] FOREIGN KEY([InvoiceDetailID])
REFERENCES [dbo].[InvoiceDetail] ([ID])
GO
ALTER TABLE [dbo].[VisitTiming] CHECK CONSTRAINT [FK_PatientVisitTiming_InvoiceDetail]
GO
ALTER TABLE [dbo].[VisitTiming]  WITH CHECK ADD  CONSTRAINT [FK_PatientVisitTiming_StationPoint_cu] FOREIGN KEY([StationPoint_CU_ID])
REFERENCES [dbo].[StationPoint_cu] ([ID])
GO
ALTER TABLE [dbo].[VisitTiming] CHECK CONSTRAINT [FK_PatientVisitTiming_StationPoint_cu]
GO
ALTER TABLE [dbo].[VisitTiming]  WITH CHECK ADD  CONSTRAINT [FK_PatientVisitTiming_StationPointStage_cu] FOREIGN KEY([StationPointStage_CU_ID])
REFERENCES [dbo].[StationPointStage_cu] ([ID])
GO
ALTER TABLE [dbo].[VisitTiming] CHECK CONSTRAINT [FK_PatientVisitTiming_StationPointStage_cu]
GO
ALTER TABLE [dbo].[VisitTiming]  WITH CHECK ADD  CONSTRAINT [FK_PatientVisitTiming_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[VisitTiming] CHECK CONSTRAINT [FK_PatientVisitTiming_User_cu]
GO
ALTER TABLE [dbo].[VisitTimming_SocialHistory]  WITH CHECK ADD  CONSTRAINT [FK_VisitTimming_SocialHistory_VisitTiming] FOREIGN KEY([VisitTimingID])
REFERENCES [dbo].[VisitTiming] ([ID])
GO
ALTER TABLE [dbo].[VisitTimming_SocialHistory] CHECK CONSTRAINT [FK_VisitTimming_SocialHistory_VisitTiming]
GO
ALTER TABLE [dbo].[WorkingShiftTitle_cu]  WITH CHECK ADD  CONSTRAINT [FK_WorkingShiftTitle_cu_User_cu] FOREIGN KEY([InsertedBy])
REFERENCES [dbo].[User_cu] ([Person_CU_ID])
GO
ALTER TABLE [dbo].[WorkingShiftTitle_cu] CHECK CONSTRAINT [FK_WorkingShiftTitle_cu_User_cu]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ex. :: 1 for adding -1 for subtracting 0 for non effect' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActiveSalaryEffect_cu', @level2type=N'COLUMN',@level2name=N'EffectiveFactor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ex. :: Mr. Dr.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonTitle_p', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ex.: Morning shift from 5 Jun 2018 : 8 am to 5 Jun 2018 : 5 pm' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WorkingShiftTitle_cu', @level2type=N'COLUMN',@level2name=N'ID'
GO
USE [master]
GO
ALTER DATABASE [MerkFinance] SET  READ_WRITE 
GO
