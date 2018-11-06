namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IBankViewer : IViewer
	{
		object Name_P { get; set; }
		object Name_S { get; set; }
		object Description { get; set; }
		object ChartOfAccount_CU_ID { get; set; }
	}
}
