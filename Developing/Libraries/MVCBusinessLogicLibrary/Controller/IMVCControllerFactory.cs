using MVCBusinessLogicLibrary.MVCFactories;

namespace MVCBusinessLogicLibrary.Controller
{
	public interface IMVCControllerFactory
	{
		ViewerName GetViewerName();
	}
}
