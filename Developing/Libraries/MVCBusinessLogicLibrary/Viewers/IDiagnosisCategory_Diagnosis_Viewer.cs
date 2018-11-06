using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;

namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IDiagnosisCategory_Diagnosis_Viewer : IViewer
	{
		List<DiagnosisCategory_Diagnosis_cu> List_DiagnosisCategory_Diagnosis { get; set; }
		object DiagnosisCategory_ID { get; set; }
		object Diagnosis_ID { get; set; }
	}
}
