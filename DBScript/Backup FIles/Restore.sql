USE [master]
RESTORE DATABASE [MerkFinance] FROM 
	DISK = N'd:\Work\StepMarket1.4\DBScript\Backup FIles\MerkFinance20190207_01\MerkFinance_20190207_01.bak' WITH FILE = 1,
	MOVE N'MerkFinance' TO N'd:\Work\DataBases_Backups\5\MerkFinance.mdf',
	MOVE N'MerkFinance_log' TO N'd:\Work\DataBases_Backups\5\MerkFinance.ldf',
	NOUNLOAD, REPLACE,  STATS = 5
GO
