using System.Collections.Generic;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.Medication_Dose_Viewers
{
	public partial class Medication_Dose_SearchViewer :
		//UserControl
		CommonAbstractSearchViewer<Medication_Dose_cu>,
		IMedication_Dose_Viewer
	{
		public Medication_Dose_SearchViewer()
		{
			InitializeComponent();
		}

		#region Overrides of CommonAbstractViewer<Medication_Dose_cu>

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
			CommonViewsActions.FillGridlookupEdit(lkeDoses, Dose_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeMedications, Medication_cu.ItemsList);
		}

		public override void ClearControls()
		{
			lkeMedications.EditValue = null;
			lkeDoses.EditValue = null;
			List_Medication_Dose = null;
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_Medication_Dose_SearchViewer; }
		}

		#endregion

		#region Implementation of IMedication_Dose_Viewer

		public List<Medication_Dose_cu> List_Medication_Dose { get; set; }

		public object Medication_CU_ID
		{
			get { return lkeMedications.EditValue; }
			set { lkeMedications.EditValue = value; }
		}

		public object Dose_CU_ID
		{
			get { return lkeDoses.EditValue; }
			set { lkeDoses.EditValue = value; }
		}

		#endregion
	}
}
