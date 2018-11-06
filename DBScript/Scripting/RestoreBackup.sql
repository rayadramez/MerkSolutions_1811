USE [master]
RESTORE DATABASE [MerkFinance_3.1] FROM
	DISK = N'd:\Work\StepMarket1.4\DBScript\Backup FIles\MerkFinance20180807_01.bak' WITH FILE = 1,
	MOVE N'MerkFinance' TO N'd:\Work\Restored DB\MerkFinance_3.1.mdf',
	MOVE N'MerkFinance_log' TO N'd:\Work\Restored DB\MerkFinance_3.1.ldf',
	NOUNLOAD, REPLACE,  STATS = 5
GO
