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

		public static string MedicationNameValueField_English
		{
			get { return "MedicationName_EnglishValue"; }
		}

		public static string MedicationNameValueField_Arabic
		{
			get { return "MedicationName_ArabicValue"; }
		}

		public static string MedicationDosageNameValueField_English
		{
			get { return "MedicationDosageName_EnglishValue"; }
		}

		public static string MedicationDosageNameValueField_Arabic
		{
			get { return "MedicationDosageName_ArabicValue"; }
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
		public DB_EyeType_p EyeType { get; set; }
		public bool IsEyeRelatedType { get; set; }

		public string MedicationName_EnglishValue { get; set; }
		public string MedicationName_ArabicValue { get; set; }
		public string MedicationDosageName_EnglishValue { get; set; }
		public string MedicationDosageName_ArabicValue { get; set; }
		public string MedicationReccommendationsNameValue { get; set; }
	}
}
