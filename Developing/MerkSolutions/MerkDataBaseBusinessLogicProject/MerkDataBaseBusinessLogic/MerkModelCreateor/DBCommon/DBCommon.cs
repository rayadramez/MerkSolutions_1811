using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Transactions;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using System.Data.Entity.Core.Metadata.Edm;

namespace MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon
{
	#region Private DB Entities

	public enum DBCommonEntitiesType
	{
		CustomUserEntities = 1,
		BridgeEntities = 2,
		TransactionsEntities = 3,
		PrivateInternalEntities = 4,
		ConfigurationEntities = 5
	}

	#endregion

	public class DBCommon : IDBCommon
	{
		public DBCommon()
		{

		}

		public DBCommon(DBCommonEntitiesType tableType)
		{
			TableType = tableType;
			LoadFromDB = true;
		}

		#region ColumnNames

		public static String ID_ColumnaName
		{
			get { return "ID"; }
		}

		public static String Name_P_ColumnaName
		{
			get { return "Name_P"; }
		}

		public static String Name_S_ColumnaName
		{
			get { return "Name_S"; }
		}

		public static String IsOnDuty_ColumnaName
		{
			get { return "IsOnDuty"; }
		}

		public static String Description_ColumnaName
		{
			get { return "Description"; }
		}

		#endregion

		public static IDBCommon ActiveDBItem { get; set; }

		public static string ServerName { get; set; }

		public static string DBName { get; set; }

		public virtual int ID { get; set; }
		public virtual bool IsOnDuty { get; set; }

		private readonly DB_TableIdentity _tableIdentity;

		public virtual bool LoadFromDB { get; private set; }

		public virtual DBCommonEntitiesType TableType { get; private set; }

		public virtual DB_CommonTransactionType DBCommonTransactionType { get; set; }
		public int TableIdentityID { get; set; }

		List<string> IDBCommon.ChildrenItemsList
		{
			get { throw new NotImplementedException(); }
		}

		public string EntityName { get; private set; }
		public IDBCommon GetSpecificEntity(MerkFinanceEntities conetxt, int id)
		{
			throw new NotImplementedException();
		}

		public virtual IList ReGenerateList()
		{
			return null;
		}

		public virtual IDBCommon RegenerateEntityObject(IDBCommon entity)
		{
			return null;
		}

		public static MerkFinanceEntities _context;

		public static MerkFinanceEntities DBContext_External
		{
			get
			{
				if (_context == null)
					return new MerkFinanceEntities(DBConnectionManager.GetMerkFinanceConnectionString(ServerName, DBName));
				return _context;
			}
		}

		public static MerkFinanceEntities DBContext_Embedded
		{
			get
			{
				if (_context == null)
					return new MerkFinanceEntities();
				return _context;
			}
		}

		public virtual bool LoadItemsList()
		{
			return false;
		}

		public static List<DBCommon> GetAllDerivedTables<DBCommon>(params object[] constructorArgs)
		{
			List<DBCommon> dreviedObjects = new List<DBCommon>();

			try
			{
				foreach (Type type in
					Assembly.GetAssembly(typeof(DBCommon))
						.GetTypes()
						.Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(DBCommon))))
					dreviedObjects.Add((DBCommon)Activator.CreateInstance(type, constructorArgs));
			}
			catch (Exception ex)
			{
				var typeLoadException = ex as ReflectionTypeLoadException;
				var loaderExceptions = typeLoadException.LoaderExceptions;
			}

			return dreviedObjects;
		}

		public static List<string> GetAllContextEntitiesNames()
		{
			List<string> list = new List<string>();

			foreach (Type type in
				Assembly.GetAssembly(typeof(DBCommon))
					.GetTypes()
					.Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(DBCommon))))
			{
				list.Add(type.Name);
			}

			return list;
		}

		public static void SetTableIdentityEntities(List<string> contextEntitiesNames)
		{
			using (TransactionScope scope = new TransactionScope())
			{
				TableIdentity table = null;

				MerkFinanceEntities context = null;
				try
				{
					context = new MerkFinanceEntities();
					context.Configuration.AutoDetectChangesEnabled = false;

					int count = 0;
					foreach (string entityToInsert in contextEntitiesNames)
					{
						++count;
						table = new TableIdentity();
						table.ID = count;
						table.TableName = entityToInsert;
						table.CommonEnityTypeID = null;
						table.NextAvailableID = null;
						context = SaveBulkChanges(context, table, count, 100, true);
					}

					//context.SaveChanges();
				}
				finally
				{
					if (context != null)
						context.Dispose();
				}

				scope.Complete();
			}
		}

		public static List<IDBCommon> GetAllChildrenEntities(IDBCommon entity)
		{
			List<IDBCommon> list = new List<IDBCommon>();

			var objectContext = ((IObjectContextAdapter)DBContext_External).ObjectContext;
			var workspace = objectContext.MetadataWorkspace;
			var containerName = objectContext.DefaultContainerName;
			Type t = entity.GetType();
			var entityName = t.Name;
			var storageMapping = workspace.GetItem<GlobalItem>(containerName, DataSpace.CSSpace);
			dynamic temp = storageMapping.GetType().InvokeMember(
							"EntitySetMappings",
							BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance,
							null, storageMapping, null);

			return list;
		}

		private static MerkFinanceEntities SaveBulkChanges<TEntity>(MerkFinanceEntities context, TEntity entity, int count,
			int commitCount, bool recreateContext)
			where TEntity : class, new()
		{
			context.Set<TEntity>().Add(entity);

			if (count % commitCount == 0)
			{
				context.SaveChanges();
				if (recreateContext)
				{
					context.Dispose();
					context = new MerkFinanceEntities();
					context.Configuration.AutoDetectChangesEnabled = false;
				}
			}

			return context;
		}

		public static TEntity CreateNewDBEntity<TEntity>()
			where TEntity : DBCommon, new()
		{
			TEntity table = null;

			using (UnitOfWork.UnitOfWork unitOfWork = new UnitOfWork.UnitOfWork(DBContext_External))
				table = unitOfWork.CreateDBEntity<TEntity>();
			table.DBCommonTransactionType = DB_CommonTransactionType.CreateNew;
			table.IsOnDuty = true;
			return table;
		}

		//public static List<TEntity> GetStaticItemsList<TEntity>()
		//	where TEntity : IDBCommon
		//{
		//	return (List<TEntity>) entity.ReGenerateList();
		//}

		public static IEnumerable<TEntity> GetItemsList<TEntity>()
			where TEntity : class, IDBCommon, new()
		{
			IEnumerable<TEntity> itemList;

			using (UnitOfWork.UnitOfWork unitOfWork = new UnitOfWork.UnitOfWork(DBContext_External))
				itemList = unitOfWork.GetAllEntities<TEntity>(item => item.IsOnDuty);

			return itemList;
		}

		public static IEnumerable<TEntity> GetItemsList<TEntity>(Expression<Func<TEntity, bool>> predicate)
			where TEntity : class, new()
		{
			IEnumerable<TEntity> list = null;

			using (UnitOfWork.UnitOfWork unitOfWork = new UnitOfWork.UnitOfWork(DBContext_External))
				list = unitOfWork.GetAllEntities<TEntity>(predicate);

			return list;
		}

		public static TEntity GetEntity<TEntity>(int id)
			where TEntity : class, new()
		{
			using (UnitOfWork.UnitOfWork unitOfWork = new UnitOfWork.UnitOfWork(DBContext_External))
				return unitOfWork.GetEntity<TEntity>(id);
		}

		public static TEntity GetEntity<TEntity>(Expression<Func<TEntity, bool>> predicate)
			where TEntity : class, new()
		{
			using (UnitOfWork.UnitOfWork unitOfWork = new UnitOfWork.UnitOfWork(DBContext_External))
				return unitOfWork.GetEntity<TEntity>(predicate);
		}

		public virtual void AddEntity<TEntity>(TEntity entity)
			where TEntity : class, new()
		{
			using (UnitOfWork.UnitOfWork unitOfWork = new UnitOfWork.UnitOfWork(DBContext_External))
				unitOfWork.GetList<TEntity>().AddEntity(entity);
		}

		public static bool SaveChanges<TEntity>(TEntity entity)
			where TEntity : DBCommon, IDBCommon, new()
		{
			int count = 0;
			using (UnitOfWork.UnitOfWork unitOfWork = new UnitOfWork.UnitOfWork(DBCommon.DBContext_External))
			{
				if (entity.DBCommonTransactionType == DB_CommonTransactionType.SaveNew)
				{
					unitOfWork.GetList<TEntity>().AddEntity(entity);
					count = unitOfWork.SaveChanges();
				}
				else
				{
					unitOfWork.UpdateChanges(entity);
					count = 1;
				}
			}

			return count > 0;
		}

		public static bool SaveChanges<TEntity>()
			where TEntity : class, IDBCommon, new()
		{
			int count = 0;
			using (UnitOfWork.UnitOfWork unitOfWork = new UnitOfWork.UnitOfWork(DBCommon.DBContext_External))
			{
				unitOfWork.UpdateChanges<TEntity>(ActiveDBItem);
				count = 1;
			}

			return count > 0;
		}
	}
}
