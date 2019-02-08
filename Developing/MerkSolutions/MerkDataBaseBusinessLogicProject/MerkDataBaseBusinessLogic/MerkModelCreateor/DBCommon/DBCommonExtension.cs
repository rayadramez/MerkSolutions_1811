using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using System.Data.Entity.Core.Objects;
using EntityKey = System.Data.Entity.Core.EntityKey;
using EntityState = System.Data.Entity.EntityState;

namespace MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon
{
	public static class DBCommonExtension
	{
		public static DbSet CreateDBEntity<TEntity>(this DbSet entity) where TEntity : DbSet, new()
		{
			return null;
		}

		public static bool SaveChanges<TEntity>(this TEntity entity)
			where TEntity : class, IDBCommon, new()
		{
			int count = 0;
			using (UnitOfWork.UnitOfWork unitOfWork = new UnitOfWork.UnitOfWork(DBCommon.DBContext_External))
			{
				if (entity.DBCommonTransactionType == DB_CommonTransactionType.CreateNew
				    || entity.DBCommonTransactionType == DB_CommonTransactionType.SaveNew)
				{
					unitOfWork.GetList<TEntity>().AddEntity(entity);
					count = unitOfWork.SaveChanges();
				}
				else if (entity.DBCommonTransactionType == DB_CommonTransactionType.UpdateExisting)
					count = unitOfWork.UpdateChanges(entity);
				else if (entity.DBCommonTransactionType == DB_CommonTransactionType.DeleteExisting)
					count = unitOfWork.RemoveEntity(entity);
			}

			entity.ReGenerateList();

			if (entity is Person_cu)
			{
				if ((entity as Person_cu).Supplier_cu != null)
					(entity as Person_cu).Supplier_cu.ReGenerateList();
				if ((entity as Person_cu).Doctor_cu != null)
					(entity as Person_cu).Doctor_cu.ReGenerateList();
				if ((entity as Person_cu).Customer_cu != null)
					(entity as Person_cu).Customer_cu.ReGenerateList();
				if ((entity as Person_cu).User_cu != null)
					(entity as Person_cu).User_cu.ReGenerateList();
				if ((entity as Person_cu).Patient_cu != null)
					(entity as Person_cu).Patient_cu.ReGenerateList();
			}

			//entity = (TEntity) entity.RegenerateEntityObject(entity);
			
			return count > 0;
		}

		public static bool SaveChanges<TEntity>(this TEntity entity, DB_CommonTransactionType commonTransactionType)
			where TEntity : class, IDBCommon, new()
		{
			int count = 0;
			using (UnitOfWork.UnitOfWork unitOfWork = new UnitOfWork.UnitOfWork(DBCommon.DBContext_External))
			{
				switch (commonTransactionType)
				{
					case DB_CommonTransactionType.UpdateExisting:
						count = unitOfWork.UpdateChanges(entity);
						
						break;
					case DB_CommonTransactionType.SaveNew:
						unitOfWork.GetList<TEntity>().AddEntity(entity);
						count = unitOfWork.SaveChanges();
						break;
				}
			}
			return count > 0;
		}

		public static void AddOrAttach<T>(this DbContext context, T entity)
			where T : class
		{
			#region leave conditions

			if (entity == null) 
				return;

			var entry = context.Entry(entity);
			var leaveStates = new[]
			{
				EntityState.Deleted,
				EntityState.Modified,
				EntityState.Unchanged
			};

			if (leaveStates.Contains(entry.State)) 
				return;

			#endregion

			var entityKey = context.GetEntityKey(entity);
			if (entityKey == null)
			{
				entry.State = EntityState.Unchanged;
				entityKey = context.GetEntityKey(entity);
			}
			if (entityKey.EntityKeyValues == null
			    || entityKey.EntityKeyValues.Select(ekv => (int) ekv.Value).All(v => v <= 0))
			{
				entry.State = EntityState.Added;
			}
		}

		public static EntityKey GetEntityKey<T>(this DbContext context, T entity)
			where T : class
		{
			var oc = ((IObjectContextAdapter)context).ObjectContext;
			ObjectStateEntry ose;
			if (null != entity && oc.ObjectStateManager
				.TryGetObjectStateEntry(entity, out ose))
			{
				return ose.EntityKey;
			}
			return null;
		}

		public static bool RemoveItem<TEntity>(this TEntity entity)
			where TEntity : class, IDBCommon, new()
		{
			int count = 0;
			using (UnitOfWork.UnitOfWork unitOfWork = new UnitOfWork.UnitOfWork(DBCommon.DBContext_External))
			{
				entity.IsOnDuty = false;
				count = unitOfWork.RemoveEntity(entity);
			}

			return count > 0;
		}
	}
}
