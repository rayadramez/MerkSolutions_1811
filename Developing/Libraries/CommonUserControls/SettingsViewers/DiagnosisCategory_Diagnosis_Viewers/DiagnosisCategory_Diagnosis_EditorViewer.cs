using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using DevExpress.XtraEditors;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.DiagnosisCategory_Diagnosis_Viewers
{
	public partial class DiagnosisCategory_Diagnosis_EditorViewer : 
		//UserControl
		CommonAbstractEditorViewer<DiagnosisCategory_Diagnosis_cu>,
		IDiagnosisCategory_Diagnosis_Viewer
	{
		private Diagnosis_cu SelectedDiagnosisFromGrid = null;
		private List<Diagnosis_cu> List_SelectedDiagnosisToBedAdded = null;

		public DiagnosisCategory_Diagnosis_EditorViewer()
		{
			InitializeComponent();
			switch (ApplicationStaticConfiguration.Application)
			{
				case DB_Application.AdmissionReception:
				case DB_Application.AllReception:
				case DB_Application.ClinicReception:
				case DB_Application.InvoiceManager:
				case DB_Application.QueueManager:
				case DB_Application.Settings:
					CommonViewsActions.LoadXMLFromString(layoutControl1,
						Resources.LocalizedRes.lyt_DiagnosisCategory_Diagnosis_EditorViewer);
					CommonViewsActions.SetupGridControl(grdDiagnosis, Resources.LocalizedRes.grd_Diagnosis_SearchViewer,
						true);
					break;
				case DB_Application.PEMR:
					CommonViewsActions.LoadXMLFromString(layoutControl1,
						Resources.LocalizedRes.lyt_DiagnosisCategory_Diagnosis_EditorViewer_en_US);
					CommonViewsActions.SetupGridControl(grdDiagnosis,
						Resources.LocalizedRes.grd_Diagnosis_SearchViewer_en_US, true);
					break;
			}
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<DiagnosisCategory_Diagnosis_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.DiagnosisCategory_Diagnosis_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return ""; }
		}

		public override void FillControls()
		{
			grdDiagnosis.DataSource = Diagnosis_cu.ItemsList;
			CommonViewsActions.FillGridlookupEdit(lkeDiagnosisCategories, DiagnosisCategory_cu.ItemsList);
		}

		public override void ClearControls()
		{
			lst_Medication_Doses.DataSource = null;
			lkeDiagnosisCategories.EditValue = null;
			List_SelectedDiagnosisToBedAdded = null;
			SelectedDiagnosisFromGrid = null;
			List_DiagnosisCategory_Diagnosis = null;
		}

		#endregion

		#region Implementation of IDiagnosisCategory_Diagnosis_Viewer

		public List<DiagnosisCategory_Diagnosis_cu> List_DiagnosisCategory_Diagnosis { get; set; }
		public object DiagnosisCategory_ID { get; set; }
		public object Diagnosis_ID { get; set; }

		#endregion

		private void btnAddList_Click(object sender, System.EventArgs e)
		{
			if (lkeDiagnosisCategories.EditValue == null)
			{
				XtraMessageBox.Show("يجـب إختيــار الـــدواء / العـــلاج", "تنبيــــــــــه", MessageBoxButtons.OK,
					MessageBoxIcon.Hand);
				return;
			}

			if (SelectedDiagnosisFromGrid == null)
			{
				XtraMessageBox.Show("يجب إختيــار التشخيــــص", "تنبيــــــــــه", MessageBoxButtons.OK,
					MessageBoxIcon.Hand);
				return;
			}

			if (List_SelectedDiagnosisToBedAdded == null)
				List_SelectedDiagnosisToBedAdded = new List<Diagnosis_cu>();

			if (List_SelectedDiagnosisToBedAdded.Count > 0)
				if (List_SelectedDiagnosisToBedAdded.Exists(
					item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(SelectedDiagnosisFromGrid.ID))))
				{
					XtraMessageBox.Show("تمــت إضافتـــه مـن قبـــل", "تنبيــــــــــه", MessageBoxButtons.OK,
						MessageBoxIcon.Hand);
					return;
				}

			List_SelectedDiagnosisToBedAdded.Add(SelectedDiagnosisFromGrid);
			CommonViewsActions.FillListBoxControl(lst_Medication_Doses, List_SelectedDiagnosisToBedAdded);
			lst_Medication_Doses.Refresh();

			DiagnosisCategory_Diagnosis_cu bridge = new DiagnosisCategory_Diagnosis_cu();
			bridge.Diagnosis_CU_ID = SelectedDiagnosisFromGrid.ID;
			bridge.DiagnosisCategory_CU_ID = Convert.ToInt32(lkeDiagnosisCategories.EditValue);
			if (List_DiagnosisCategory_Diagnosis == null)
				List_DiagnosisCategory_Diagnosis = new List<DiagnosisCategory_Diagnosis_cu>();
			List_DiagnosisCategory_Diagnosis.Add(bridge);
		}

		private void btnRemoveFromList_Click(object sender, System.EventArgs e)
		{
			if (lst_Medication_Doses.SelectedItems.Count == 0 || List_DiagnosisCategory_Diagnosis == null)
			{
				XtraMessageBox.Show("لا يـوجــد", "تنبيــــــــــه", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}

			Diagnosis_cu selectedDiagnosis = (Diagnosis_cu)lst_Medication_Doses.SelectedItem;
			if (selectedDiagnosis == null)
				return;
			if (List_SelectedDiagnosisToBedAdded.Exists(
				item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(selectedDiagnosis.ID))))
				List_SelectedDiagnosisToBedAdded.Remove(selectedDiagnosis);

			CommonViewsActions.FillListBoxControl(lst_Medication_Doses, List_SelectedDiagnosisToBedAdded, "Name_P");
			lst_Medication_Doses.Refresh();

			DiagnosisCategory_Diagnosis_cu brdige =
				List_DiagnosisCategory_Diagnosis.Find(item =>
					Convert.ToInt32(item.Diagnosis_CU_ID).Equals(Convert.ToInt32(selectedDiagnosis.ID)));
			if (brdige == null)
				return;
			List_DiagnosisCategory_Diagnosis.Remove(brdige);
		}

		private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
		{
			SelectedDiagnosisFromGrid = CommonViewsActions.GetSelectedRowObject<Diagnosis_cu>(gridView1);
		}
	}
}
