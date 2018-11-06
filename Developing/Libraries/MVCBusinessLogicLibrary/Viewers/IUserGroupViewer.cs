namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IUserGroupViewer : IViewer
	{
		object NameP { get; set; }
		object NameS { get; set; }
		object InternalCode { get; set; }
		object Description { get; set; }
	}
}
