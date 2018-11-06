using System;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon.Repositories;

namespace MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		IRepository<AccountingJournalEntryTransaction> Items_AccountingJournalEntryTransaction { get; }
		IRepository<ChartOfAccount_cu> Items_ChartOfAccount { get; }
		IRepository<FinancialInterval_cu> Items_FinancialInterval { get; }
		IRepository<FinancialInterval_Month_cu> Items_FinancialInterval_Month { get; }
		IRepository<TrialBalanceTransaction> Items_TrialBalanceTransaction { get; }

		int SaveChanges();
	}
}
