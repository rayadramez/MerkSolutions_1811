namespace MVCBusinessLogicLibrary.Viewers
{
	public interface ICashBoxViewer : IViewer
	{
		object Name_P { get; set; }
		object Name_S { get; set; }
		object InternalCode { get; set; }
		object IsMain { get; set; }
		object Floor_CU_ID { get; set; }
		object ChartOfAccount_CU_ID { get; set; }
		object Description { get; set; }
	}
}
