using System.Collections.Generic;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.DiagnosisCategory_Diagnosis_Viewers
{
	public partial class DiagnosisCategory_Diagnosis_SearchViewer : 
		//UserControl
		CommonAbstractSearchViewer<DiagnosisCategory_Diagnosis_cu>,
		IDiagnosisCategory_Diagnosis_Viewer
	{
		public DiagnosisCategory_Diagnosis_SearchViewer()
		{
			InitializeComponent();
		}

		#region Overrides of CommonAbstractViewer<DiagnosisCategory_Diagnosis_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.Medication_Dose_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "ربـــط الأدويـــــة بالجـرعــــات"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeDiagnosisCategories, DiagnosisCategory_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeDiagnosis, Diagnosis_cu.ItemsList);
		}

		public override void ClearControls()
		{
			lkeDiagnosisCategories.EditValue = null;
			lkeDiagnosis.EditValue = null;
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_DiagnosisCategory_Diagnosis_SearchViewer; }
		}

		#endregion

		#region Implementation of IDiagnosisCategory_Diagnosis_Viewer

		public List<DiagnosisCategory_Diagnosis_cu> List_DiagnosisCategory_Diagnosis { get; set; }
		public object DiagnosisCategory_ID
		{
			get { return lkeDiagnosisCategories.EditValue; }
			set { lkeDiagnosisCategories.EditValue = value; }
		}

		public object Diagnosis_ID
		{
			get { return lkeDiagnosis.EditValue; }
			set { lkeDiagnosis.EditValue = value; }
		}

		#endregion
	}
}
