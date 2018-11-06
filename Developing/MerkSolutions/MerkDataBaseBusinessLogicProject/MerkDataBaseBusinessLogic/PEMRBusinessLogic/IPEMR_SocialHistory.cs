namespace MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.PEMRBusinessLogic
{
	public interface IPEMR_SocialHistory
	{
		object NegativeSocialHistory { get; set; }
		object GeneralDescription { get; set; }
		object DidYouEverSmoke { get; set; }
		object NumberOfPacks { get; set; }
		object NumberOfYears { get; set; }
		object SmokeFurtherDetails { get; set; }
		object QuitingSmokeLessThan { get; set; }
		object QuitingSmokeBetween { get; set; }
		object QuitingSmokeMoreThan { get; set; }
		object QuitingSmokeFurtherDetails { get; set; }
		object DrinkAlcohol { get; set; }
		object HowMuchAlcohol { get; set; }
		object AlcoholFurtherDetails { get; set; }
		object HadProblemWithAlcohol { get; set; }
		object WhenHadProblemWIthAlcohol { get; set; }
		object HadProblemWithAlcoholFurtherDetails { get; set; }
		object Addicted { get; set; }
		object AddictionFurtherDetails { get; set; }
		object HadProblemWithAddiction { get; set; }
		object HadProblemWithAddictionFurtherDetails { get; set; }
		object UseRecreationalDrugs { get; set; }
		object UseRecreationalDrugsFurtherDetails { get; set; }
	}
}
