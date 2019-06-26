USE [master]
RESTORE DATABASE [MerkFinance] FROM 
	DISK = N'd:\Work\StepMarket1.4\DBScript\Backup FIles\MerkFinance_20190624_02.bak' WITH FILE = 1,
	MOVE N'MerkFinance' TO N'D:\Work\Restored DB\6\MerkFinance.mdf',
	MOVE N'MerkFinance_log' TO N'D:\Work\Restored DB\6\MerkFinance.ldf',
	NOUNLOAD, REPLACE,  STATS = 5
GO
