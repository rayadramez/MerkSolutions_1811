namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IPerson_ChartOfAccount_Viewer : IViewer
	{
		object PersonType_P_ID { get; set; }
		object Person_CU_ID { get; set; }
		object IsDebitChartOfAccount { get; set; }
		object Debit_ChartOfAccount { get; set; }
		object IsTaxChartOfAccount { get; set; }
		object Tax_ChartOfAccount { get; set; }
		object IsCreditChartOfAccount { get; set; }
		object Credit_ChartOfAccount { get; set; }
		object IsCurrentChartOfAccount { get; set; }
		object Current_ChartOfAccount { get; set; }
	}
}
