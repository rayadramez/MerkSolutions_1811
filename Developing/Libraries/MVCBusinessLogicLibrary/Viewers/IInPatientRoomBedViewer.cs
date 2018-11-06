namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IInPatientRoomBedViewer : IViewer
	{
		object NameP { get; set; }
		object NameS { get; set; }
		object InPatientRoom { get; set; }
		object Description { get; set; }
		object ShortName { get; set; }
		object InternalCode { get; set; }
		object InPatientRoomBedStatus { get; set; }
	}
}
