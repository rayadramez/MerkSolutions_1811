namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IServiceViewer : IViewer
	{
		object Name_P { get; set; }
		object Name_S { get; set; }
		object ServiceCategory_CU_ID { get; set; }
		object ServiceType_P_ID { get; set; }
		object ParentService_CU_ID { get; set; }
		object InternalCode { get; set; }
		object EnforceCategorization { get; set; }
		object IsDailyCharged { get; set; }
		object DefaultPriceFrom { get; set; }
		object DefaultPriceTo { get; set; }
		object AllowAddmission { get; set; }
		object Description { get; set; }
	}
}
