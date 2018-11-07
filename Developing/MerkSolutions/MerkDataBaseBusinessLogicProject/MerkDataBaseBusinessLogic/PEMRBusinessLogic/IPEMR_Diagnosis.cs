using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.PEMRBusinessLogic
{
	public interface IPEMR_Diagnosis
	{
		object FurtherDetails { get; set; }
		List<Diagnosis_cu> AddedDiagnosisList { get; set; }
		DB_DiagnosisType DiagnosisType { get; set; }
	}
}
