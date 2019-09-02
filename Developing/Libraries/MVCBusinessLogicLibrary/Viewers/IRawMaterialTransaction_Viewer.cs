namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IRawMaterialTransaction_Viewer : IViewer
	{
		object RawMaterialID { get; set; }
		object RawTransactionTypeID { get; set; }
		object ColorID { get; set; }
		object Count { get; set; }
		object PuchasingPrice { get; set; }
		object Width { get; set; }
		object Height { get; set; }
		object TransactionDate { get; set; }
		object DividedTypeID { get; set; }
	}
}
