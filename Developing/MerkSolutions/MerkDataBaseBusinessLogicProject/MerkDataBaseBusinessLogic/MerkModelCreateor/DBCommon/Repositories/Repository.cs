using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon.Repositories
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
	{
		protected readonly MerkFinanceEntities _dbContext;

		public Repository(MerkFinanceEntities context)
		{
			_dbContext = context;
		}

		public TEntity CreateDBEntity()
		{
			return new TEntity();
		}

		public TEntity GetEntity(int id)
		{
			return _dbContext.Set<TEntity>().Find(id);
		}

		public TEntity GetEntity()
		{
			return _dbContext.Set<TEntity>().FirstOrDefault();
		}

		public IEnumerable<TEntity> GetAllEntities()
		{
			return _dbContext.Set<TEntity>().ToList();
		}

		public IEnumerable<TEntity> GetEntities(Expression<Func<TEntity, bool>> predicate)
		{
			return _dbContext.Set<TEntity>().Where(predicate);
		}

		public TEntity GetEntity(Expression<Func<TEntity, bool>> predicate)
		{
			return (TEntity) _dbContext.Set<TEntity>().Where(predicate);
		}

		public void AddEntity(TEntity entity)
		{
			_dbContext.Set<TEntity>().Add(entity);
		}

		public void AddEntitiesRange(IEnumerable<TEntity> entities)
		{
			IEnumerable<TEntity> enumerable = entities as TEntity[] ?? entities.ToArray();
			if (entities != null && enumerable.ToList().Count > 0)
				foreach (TEntity entity in enumerable)
					_dbContext.Set<TEntity>().Add(entity);
		}

		public void RemoveEntity(TEntity entity)
		{
			_dbContext.Set<TEntity>().Remove(entity);
		}

		public void RemoveEntitiesRange(IEnumerable<TEntity> entities)
		{
			IEnumerable<TEntity> enumerable = entities as TEntity[] ?? entities.ToArray();
			if(entities != null && enumerable.ToList().Count > 0)
				foreach (TEntity entity in enumerable)
					_dbContext.Set<TEntity>().Remove(entity);
		}
	}
}
