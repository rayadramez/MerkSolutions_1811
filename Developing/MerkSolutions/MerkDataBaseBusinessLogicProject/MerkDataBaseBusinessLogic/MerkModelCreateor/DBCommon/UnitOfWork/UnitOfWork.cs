using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq.Expressions;
using System.Reflection;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon.Repositories;

namespace MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly MerkFinanceEntities _dbContext;
		public UnitOfWork(MerkFinanceEntities conetxt)
		{
			_dbContext = conetxt;
			Items_AccountingJournalEntryTransaction = new Repository<AccountingJournalEntryTransaction>(_dbContext);
			Items_ChartOfAccount = new Repository<ChartOfAccount_cu>(_dbContext);
			Items_FinancialInterval = new Repository<FinancialInterval_cu>(_dbContext);
			Items_FinancialInterval_Month = new Repository<FinancialInterval_Month_cu>(_dbContext);
			Items_TrialBalanceTransaction = new Repository<TrialBalanceTransaction>(_dbContext);
		}

		public IRepository<AccountingJournalEntryTransaction> Items_AccountingJournalEntryTransaction { get; private set; }
		public IRepository<ChartOfAccount_cu> Items_ChartOfAccount { get; private set; }
		public IRepository<FinancialInterval_cu> Items_FinancialInterval { get; private set; }
		public IRepository<FinancialInterval_Month_cu> Items_FinancialInterval_Month { get; private set; }
		public IRepository<TrialBalanceTransaction> Items_TrialBalanceTransaction { get; private set; }

		public int SaveChanges()
		{
			try
			{
				int num = _dbContext.SaveChanges();

				return num;
			}
			catch (DbEntityValidationException e)
			{
				foreach (var eve in e.EntityValidationErrors)
				{
					Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
						eve.Entry.Entity.GetType().Name, eve.Entry.State);
					foreach (var ve in eve.ValidationErrors)
					{
						Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
							ve.PropertyName, ve.ErrorMessage);
					}
				}
				throw;
			}
		}

		public int UpdateChanges<TEntity>(TEntity entity)
			where TEntity : class ,IDBCommon, new()
		{
			try
			{
				TEntity existingEntity = GetEntity<TEntity>(entity.ID);
				_dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);

				if (entity.ChildrenItemsList != null && entity.ChildrenItemsList.Count > 0)
					foreach (string childEntityName in entity.ChildrenItemsList)
					{
						PropertyInfo childParentInfo = entity.GetType().GetProperty(childEntityName);
						if (childParentInfo != null)
						{
							IDBCommon share = (IDBCommon)childParentInfo.GetValue(entity);
							if (share == null)
								continue;
							IDBCommon existingChild = null;
							if(share.ID != null)
								existingChild = share.GetSpecificEntity(_dbContext, Convert.ToInt32(share.ID));
							if (existingChild == null)
								continue;

							_dbContext.Entry(existingChild).CurrentValues.SetValues(share);
						}
					}

				return SaveChanges();
			}
			catch (DbEntityValidationException e)
			{
				foreach (var eve in e.EntityValidationErrors)
				{
					Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
						eve.Entry.Entity.GetType().Name, eve.Entry.State);
					foreach (var ve in eve.ValidationErrors)
					{
						Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
							ve.PropertyName, ve.ErrorMessage);
					}
				}
				throw;
			}
		}

		public void Dispose()
		{
			//_dbContext.Dispose();
		}

		public TEntity CreateDBEntity<TEntity>()
			where TEntity : class, new()
		{
			return new Repository<TEntity>(_dbContext).CreateDBEntity();
		}

		public int RemoveEntity<TEntity>(TEntity entity)
			where TEntity : class, IDBCommon, new()
		{
			try
			{
				//TODO :: Check from System Configuration if the owner wants to delete forever or just mark it as IsOnDuty = false
				Repository<TEntity> repository = new Repository<TEntity>(_dbContext);
				TEntity updatedEntity = GetEntity<TEntity>(entity.ID);
				repository.RemoveEntity(updatedEntity);
				return SaveChanges();

				//This is to Update After Making IsOnDuty = false;
				//return UpdateChanges<TEntity>(entity);
			}
			catch (DbEntityValidationException e)
			{
				foreach (var eve in e.EntityValidationErrors)
				{
					Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
						eve.Entry.Entity.GetType().Name, eve.Entry.State);
					foreach (var ve in eve.ValidationErrors)
					{
						Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
							ve.PropertyName, ve.ErrorMessage);
					}
				}
				throw;
			}
		}

		public int UpdateChanges<TEntity>(IDBCommon activeDbItem) where TEntity : class, new()
		{
			try
			{
				TEntity updatedEntity = GetEntity<TEntity>(activeDbItem.ID);
				_dbContext.Entry(updatedEntity).CurrentValues.SetValues(activeDbItem);
				return SaveChanges();
			}
			catch (DbEntityValidationException e)
			{
				foreach (var eve in e.EntityValidationErrors)
				{
					Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
						eve.Entry.Entity.GetType().Name, eve.Entry.State);
					foreach (var ve in eve.ValidationErrors)
					{
						Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
							ve.PropertyName, ve.ErrorMessage);
					}
				}
				throw;
			}
		}

		public Repository<TEntity> GetList<TEntity>()
			where TEntity : class, new()
		{
			return new Repository<TEntity>(_dbContext);
		}

		public IEnumerable<TEntity> GetAllEntities<TEntity>()
			where TEntity : class, new()
		{
			return new Repository<TEntity>(_dbContext).GetAllEntities();
		}

		public IEnumerable<TEntity> GetAllEntities<TEntity>(Expression<Func<TEntity, bool>> predicate)
			where TEntity : class, new()
		{
			return new Repository<TEntity>(_dbContext).GetEntities(predicate);
		}

		public TEntity GetEntity<TEntity>(int id)
			where TEntity : class, new()
		{
			return new Repository<TEntity>(_dbContext).GetEntity(id);
		}

		public TEntity GetEntity<TEntity>(Expression<Func<TEntity, bool>> predicate)
			where TEntity : class, new()
		{
			return new Repository<TEntity>(_dbContext).GetEntity(predicate);
		}

		public TEntity GetEntity<TEntity>(object predicate)
			where TEntity : class, new()
		{
			return new Repository<TEntity>(_dbContext).GetEntity();
		}
	}
}
