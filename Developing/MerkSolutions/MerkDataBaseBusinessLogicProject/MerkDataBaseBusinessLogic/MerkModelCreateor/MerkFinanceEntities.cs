using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Transactions;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using EntityState = System.Data.Entity.EntityState;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class MerkFinanceEntities
	{
		public MerkFinanceEntities(string connectionString)
			: base(connectionString)
		{
			//((IObjectContextAdapter)this).ObjectContext.SavingChanges += new EventHandler(objContext_SavingChanges);
		}

		public void objContext_SavingChanges(object sender, EventArgs e)
		{
			MerkFinanceEntities merkContext = this;

			LogAuditing log = new LogAuditing();

			using (TransactionScope scope = new TransactionScope())
			{
				StringBuilder currentvalues = new StringBuilder();
				StringBuilder originalvalues = new StringBuilder();
				IEnumerable<DbEntityEntry> addEntries = GetDbEntityEntries(merkContext, EntityState.Added);
				IEnumerable<DbEntityEntry> unchangedEntries = GetDbEntityEntries(merkContext, EntityState.Unchanged);
				IEnumerable<DbEntityEntry> updatedEntries = GetDbEntityEntries(merkContext, EntityState.Modified);

				#region New Items

				foreach (DbEntityEntry entry in addEntries)
				{
					log = new LogAuditing();
					log.CommonTransactionType_P_ID = (int)DB_CommonTransactionType.SaveNew;
					log.Date = DateTime.Now;
					string[] entityNameArray = entry.Entity.ToString().Split('.');
					log.EntityName = entityNameArray[1];
					//TODO :: Should get the user from the context

					int index = 0;
					foreach (var propertyName in entry.CurrentValues.PropertyNames)
					{
						currentvalues.Append("(");
						currentvalues.Append(entry.Member(propertyName).Name);
						currentvalues.Append("=");
						if (entry.Member(propertyName).CurrentValue != null)
						{
							Type type = entry.Member(propertyName).CurrentValue.GetType();
							currentvalues.Append(entry.Member(propertyName).CurrentValue);
						}
						else
							currentvalues.Append("null");

						if (propertyName == "InsertedBy")
							log.User_CU_ID = Convert.ToInt32(entry.Member(propertyName).CurrentValue);

						currentvalues.Append(")");
						index++;

						if (index == entry.CurrentValues.PropertyNames.Count())
							break;

						currentvalues.Append(", ");
					}

					log.CurrentValues = currentvalues.ToString();

					//MerkAuditingEntities.DBAuditingEntities.LogAuditings.Add(log);
					//MerkAuditingEntities.DBAuditingEntities.SaveChanges();
				}

				#endregion

				#region Updated Items

				foreach (DbEntityEntry entry in updatedEntries)
				{
					log.CommonTransactionType_P_ID = (int)DB_CommonTransactionType.UpdateExisting;
					log.Date = DateTime.Now;
					string[] entityNameArray = entry.Entity.ToString().Split('.');
					log.EntityName = entityNameArray[1];
					//TODO :: Should get the user from the context
					//log
					int index = 0;
					foreach (var propertyName in entry.OriginalValues.PropertyNames)
					{
						originalvalues.Append("(");
						originalvalues.Append(entry.Member(propertyName).Name);
						originalvalues.Append("=");
						if (entry.Member(propertyName).CurrentValue != null)
							originalvalues.Append(entry.Member(propertyName).CurrentValue);
						else
							originalvalues.Append("null");
						originalvalues.Append(")");
						index++;

						if (index == entry.OriginalValues.PropertyNames.Count())
							break;

						originalvalues.Append(", ");
						log.OriginalValues = originalvalues.ToString();
					}

					foreach (var propertyName in entry.CurrentValues.PropertyNames)
					{
						currentvalues.Append("(");
						currentvalues.Append(entry.Member(propertyName).Name);
						currentvalues.Append("=");
						if (entry.Member(propertyName).CurrentValue != null)
							currentvalues.Append(entry.Member(propertyName).CurrentValue);
						else
							currentvalues.Append("null");
						currentvalues.Append(")");
						index++;

						if (index == entry.CurrentValues.PropertyNames.Count())
							break;

						currentvalues.Append(", ");
					}

					log.CurrentValues = currentvalues.ToString();

					//MerkAuditingEntities.DBAuditingEntities.LogAuditings.Add(log);
					//MerkAuditingEntities.DBAuditingEntities.SaveChanges();
				}

				#endregion

				scope.Complete();
			}
		}

		public static IEnumerable<DbEntityEntry> GetDbEntityEntries(DbContext context, EntityState entityState)
		{
			return context.ChangeTracker.Entries().Where(item => item.State.Equals(entityState));
		}
	}
}
