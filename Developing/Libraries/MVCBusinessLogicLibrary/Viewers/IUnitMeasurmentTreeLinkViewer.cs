namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IUnitMeasurmentTreeLinkViewer : IViewer
	{
		object ParentUnitMeasurment_CU_ID { get; set; }
		object ChildUnitMeasurment_CU_ID { get; set; }
		object EncapsulatedChildQantity { get; set; }
	}
}
