using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using System.Data.Entity;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class LogAuditing : DBCommon, IDBCommon
	{
		public static LogAuditing CreateNew(EntityState entityState, IEnumerable<DbEntityEntry> entries)
		{
			LogAuditing log = CreateNewDBEntity<LogAuditing>();
			StringBuilder currentvalues = new StringBuilder();
			StringBuilder originalValues = new StringBuilder();

			foreach (DbEntityEntry entry in entries)
			{
				log.Date = DateTime.Now;
				string[] entityNameArray = entry.Entity.ToString().Split('.');
				log.EntityName = entityNameArray[1];
				//TODO :: Should get the user from the context
				//log.User_CU_ID = 
				int index = 0;
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

				switch (entityState)
				{
					case EntityState.Modified:

						break;
				}
			}

			return log;
		}

		#region Implementation of IDBCommon

		public List<string> ChildrenItemsList { get; private set; }
		public IDBCommon GetSpecificEntity(int id)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
