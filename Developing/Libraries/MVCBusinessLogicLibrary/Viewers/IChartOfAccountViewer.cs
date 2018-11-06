namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IChartOfAccountViewer : IViewer
	{
		object Name_P { get; set; }
		object Name_S { get; set; }
		object ParentChartOfAccount_CU_ID { get; set; }
		object Serial { get; set; }
		object ChartOfAccountCodeMargin_P_ID { get; set; }
		object IsDebit { get; set; }
		object Description { get; set; }
	}
}
