namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IServicePrice_EditorViewer : IViewer
	{
		object Service_CU_ID { get; set; }
		object ServiceCategory_CU_ID { get; set; }
		object Doctor_CU_ID { get; set; }
		object DoctorCategory_CU_ID { get; set; }
		object Price { get; set; }
		object InsuranceCarrierID { get; set; }
		object InsuranceLevelID { get; set; }
		object InsurancePrice { get; set; }
	}
}
