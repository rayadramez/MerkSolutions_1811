namespace WindowsViewerLibraries.WIndowsMVCBusinessLogic.Views.Interfaces
{
	public interface ICloseViewerAction
	{
		bool BeforeClosing();
		bool Close();
		bool AfterClosing();
	}
}
