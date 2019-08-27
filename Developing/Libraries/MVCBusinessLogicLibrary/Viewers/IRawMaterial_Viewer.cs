namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IRawMaterial_Viewer : IViewer
	{
		object Name_P { get; set; }
		object Name_S { get; set; }
		object RawTypeID { get; set; }
		object Thickness { get; set; }
		object Width { get; set; }
		object Height { get; set; }
		object Weight { get; set; }
		object ExpirationDate { get; set; }
		object Description { get; set; }
		object IsCountable { get; set; }
		object IsStockAvailable { get; set; }
		object InternalCode { get; set; }
		object ColorID { get; set; }
		object DividedTypeID { get; set; }
	}
}
