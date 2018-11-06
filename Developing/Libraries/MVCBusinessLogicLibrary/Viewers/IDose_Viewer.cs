namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IDose_Viewer : IViewer
	{
		object Name_P { get; set; }
		object Name_S { get; set; }
		object Description { get; set; }
	}
}
