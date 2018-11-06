namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IViewerDataRelated
	{
		object ViewerDataRelated { get; set; }
	}

	public class DataViewerRelatedManager
	{
		public static void CopyToViewer(IViewerDataRelated source, IViewerDataRelated destination)
		{
			destination.ViewerDataRelated = source.ViewerDataRelated;
		}
	}
}
