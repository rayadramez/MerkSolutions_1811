using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon
{
	public enum PEMRElementStatus
	{
		None = 0,
		AlreadyExists = 1,
		NewelyAdded = 2,
		Removed = 3,
		Updated = 4
	}

	public interface IPEMR_Element
	{
		DB_PEMR_ElementType PEMR_Element { get; }
		int OrderIndex { get; }
		string ElementName { get; }
		string TranslatedItem { get; set; }
		string TranslatedItemValue { get; set; }
		PEMRElementStatus PEMRElementStatus { get; set; }
	}
}
