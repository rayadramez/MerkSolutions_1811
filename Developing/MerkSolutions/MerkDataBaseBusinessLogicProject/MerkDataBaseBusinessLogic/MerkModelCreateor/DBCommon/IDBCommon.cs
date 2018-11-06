using System.Collections;
using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon
{
	public interface IDBCommon
	{
		int ID { get; set; }
		bool IsOnDuty { get; set; }
		DB_CommonTransactionType DBCommonTransactionType { get; set; }
		int TableIdentityID { get;}
		List<string> ChildrenItemsList { get; }
		string EntityName { get; }
		IDBCommon GetSpecificEntity(MerkFinanceEntities conetxt, int id);
		IList ReGenerateList();
		IDBCommon RegenerateEntityObject(IDBCommon entity);
	}
}
