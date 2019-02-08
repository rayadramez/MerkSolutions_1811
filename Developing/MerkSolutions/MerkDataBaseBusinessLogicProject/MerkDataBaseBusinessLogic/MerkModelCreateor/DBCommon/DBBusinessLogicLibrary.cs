using System.Linq;

namespace MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon
{
	public class DBBusinessLogicLibrary
	{
		public static void LoadDBItemsList()
		{
			foreach (DBCommon item
				in DBCommon.GetAllDerivedTables<DBCommon>().ToList().OrderBy(item => item.GetType().BaseType.Name))
			{
				if ((item.TableType.Equals(DBCommonEntitiesType.CustomUserEntities) && item.LoadFromDB) ||
					(item.TableType.Equals(DBCommonEntitiesType.PrivateInternalEntities) && item.LoadFromDB) ||
					(item.TableType.Equals(DBCommonEntitiesType.BridgeEntities) && item.LoadFromDB) ||
					(item.TableType.Equals(DBCommonEntitiesType.ConfigurationEntities) && item.LoadFromDB))
					item.LoadItemsList();

				/*item.ChildrenItemsList*/
			}
		}


	}
}
