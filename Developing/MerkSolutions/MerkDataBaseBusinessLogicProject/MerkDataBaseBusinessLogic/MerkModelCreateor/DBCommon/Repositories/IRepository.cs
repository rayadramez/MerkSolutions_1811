using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon.Repositories
{
	public interface IRepository<TEntity> where TEntity : class
	{
		TEntity CreateDBEntity();

		TEntity GetEntity(int id);
		IEnumerable<TEntity> GetAllEntities();
		IEnumerable<TEntity> GetEntities(Expression<Func<TEntity, bool>> predicate);

		void AddEntity(TEntity entity);
		void AddEntitiesRange(IEnumerable<TEntity> entities);

		void RemoveEntity(TEntity entity);
		void RemoveEntitiesRange(IEnumerable<TEntity> entities);
	}
}
