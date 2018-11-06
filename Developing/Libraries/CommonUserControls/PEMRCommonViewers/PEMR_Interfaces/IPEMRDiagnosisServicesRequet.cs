using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;

namespace CommonUserControls.PEMRCommonViewers.PEMR_Interfaces
{
	public interface IPEMRDiagnosisServicesRequest
	{
		void ShowResult(List<PatientAttachment_cu> patientAttachmentsList);
	}
}
