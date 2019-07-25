USE [master]
RESTORE DATABASE [MerkAuditing] FROM 
	DISK = N'd:\Work\StepMarket1.4\DBScript\Backup FIles\MerkAuditing.bak' WITH FILE = 1,
	MOVE N'MerkAuditing' TO N'd:\Work\DataBases_Backups\1\MerkAuditing.mdf',
	MOVE N'MerkAuditing_log' TO N'd:\Work\DataBases_Backups\1\MerkAuditing.ldf',
	NOUNLOAD, REPLACE,  STATS = 5
GO
