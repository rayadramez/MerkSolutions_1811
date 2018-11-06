using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IViewer
	{
		object ID { get; set; }
		object ViewerID { get; }
		object UserID { get; }
		object EditingDate { get;}
		object IsOnDUty { get; set; }
		DB_CommonTransactionType CommonTransactionType { get; set; }
		string HeaderTitle { get; }
		string GridXML { get; }
		List<IViewer> RelatedViewers { get; set; }
		void ClearControls();
		void FillControls();
	}
}
