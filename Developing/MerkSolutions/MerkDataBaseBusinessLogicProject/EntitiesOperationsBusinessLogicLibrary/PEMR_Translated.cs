using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary
{
	public class PEMR_Translated : IPEMR_Element
	{
		public static string ElementNameField
		{
			get { return "ElementName"; }
		}

		public static string TranslatedItemValueField
		{
			get { return "TranslatedItemValue"; }
		}

		public static string MedicationNameValueField
		{
			get { return "MedicationNameValue"; }
		}

		public static string MedicationDosageNameValueField
		{
			get { return "MedicationDosageNameValue"; }
		}

		public static string MedicationReccommendationsNameValueField
		{
			get { return "MedicationReccommendationsNameValue"; }
		}

		public string ElementName { get; set; }
		public int OrderIndex { get; set; }
		public DB_PEMR_ElementType PEMR_Element { get; set; }
		public IPEMR_Element PEMR_ElementObject { get; set; }
		public List<PEMR_Translated> List_PEMR_Element_Translated { get; set; }
		public PEMR_Translated Handle
		{
			get { return this; }
		}
		public PEMR_Translated PEMR_TranslatedObject { get; set; }
		public string TranslatedItem { get; set; }
		public string TranslatedItemValue { get; set; }
		public PEMRElementStatus PEMRElementStatus { get; set; }

		public string MedicationNameValue { get; set; }
		public string MedicationDosageNameValue { get; set; }
		public string MedicationReccommendationsNameValue { get; set; }
	}
}
