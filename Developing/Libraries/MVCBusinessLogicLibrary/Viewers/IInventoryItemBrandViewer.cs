namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IInventoryItemBrandViewer : IViewer
	{
		object Name_P { get; set; }
		object Name_S { get; set; }
		object Description { get; set; }
		object InternalCode { get; set; }
	}
}
