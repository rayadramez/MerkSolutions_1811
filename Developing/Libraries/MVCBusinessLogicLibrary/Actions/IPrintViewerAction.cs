namespace WindowsViewerLibraries.WIndowsMVCBusinessLogic.Views.Interfaces
{
	public interface IPrintViewerAction
	{
		bool BeforePrint();
		bool Print();
		bool AfterPrint();
	}
}
