namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IPersonType_ChartOfAccount_Viewer : IViewer
	{
		object PersonType_P_ID { get; set; }
		object ChartOfAccount_CU_ID { get; set; }
		object PersonChartOfAccountType_P_ID { get; set; }
	}
}
