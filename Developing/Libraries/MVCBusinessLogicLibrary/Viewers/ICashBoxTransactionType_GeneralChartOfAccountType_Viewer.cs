using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;

namespace MVCBusinessLogicLibrary.Viewers
{
	public interface ICashBoxTransactionType_GeneralChartOfAccountType_Viewer : IViewer
	{
		List<CashBoxTransactionType_GeneralChartOfAccountType_cu> List_CashBoxTransactionType_GeneralChartOfAccountType { get; set; }
	}
}
