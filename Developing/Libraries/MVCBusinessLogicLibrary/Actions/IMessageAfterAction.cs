using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.Actions
{
	public interface IMessageAfterAction
	{
		IViewer ViewerToShowAfterSaving { get; set; }
		IViewer ViewerToShowAfterDeleting { get; set; }
		void SetViewerAfterSaving();
		void SetViewerAfterDeleting();
	}
}
