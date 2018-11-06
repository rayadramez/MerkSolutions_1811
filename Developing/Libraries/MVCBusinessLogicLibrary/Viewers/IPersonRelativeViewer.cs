namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IPersonRelativeViewer : IViewer
	{
		object RelativeNameP { get; set; }
		object RelativeType { get; set; }
		object RelativeAddress { get; set; }
		object RelativePhone { get; set; }
	}
}
