namespace WindowsViewerLibraries.WIndowsMVCBusinessLogic.Views.Interfaces
{
	public interface INewViewerAction
	{
		bool BeforeCreatingNew();
		bool CreateNew();
		bool AfterCreateNew();
	}
}
