USE [master]
RESTORE DATABASE [EMR_Ebsar_20180618_01] FROM 
	DISK = N'D:\Work\Comrec\EMR.Code-NonMedical\EMR_BM_20180614_01.bak' WITH FILE = 1,
	MOVE N'MerkFinance' TO N'd:\ComrecFiles\Backups\ToBeDeleted\EMR_Ebsar_20180618_01.mdf',
	MOVE N'MerkFinance_log' TO N'd:\ComrecFiles\Backups\ToBeDeleted\EMR_Ebsar_20180618_01.ldf',
	NOUNLOAD, REPLACE,  STATS = 5
GO
