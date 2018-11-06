using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;

namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IMedication_Dose_Viewer : IViewer
	{
		List<Medication_Dose_cu> List_Medication_Dose { get; set; }
		object Medication_CU_ID { get; set; }
		object Dose_CU_ID { get; set; }
	}
}
