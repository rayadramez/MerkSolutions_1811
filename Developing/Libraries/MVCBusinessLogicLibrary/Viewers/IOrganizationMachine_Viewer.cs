namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IOrganizationMachine_Viewer : IViewer
	{
		object Name_P { get; set; }
		object StationPoint_CU_ID { get; set; }
		object StationPointStage_CU_ID { get; set; }
		object OrganizationID { get; set; }
		object SkinName { get; set; }
		object Color { get; set; }
	}
}
