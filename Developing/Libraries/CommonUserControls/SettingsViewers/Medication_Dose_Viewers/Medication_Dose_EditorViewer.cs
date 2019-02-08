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

namespace CommonUserControls.SettingsViewers.Medication_Dose_Viewers
{
	public partial class Medication_Dose_EditorViewer : 
		//UserControl
		CommonAbstractEditorViewer<Medication_Dose_cu>,
		IMedication_Dose_Viewer
	{
		private Dose_cu SelectedDoseFromGrid = null;
		private List<Dose_cu> List_SelectedDosesToBedAdded = null;

		public Medication_Dose_EditorViewer()
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
					CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_Medication_Dose_EditorViewer);
					break;
				case DB_Application.PEMR:
					CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_Medication_Dose_EditorViewer_en_US);
					break;
			}
			
			CommonViewsActions.SetupGridControl(grdDoses, Resources.LocalizedRes.grd_Dose_SearchViewer, true);
			CommonViewsActions.SetupSyle(this);
		}

		public override void FillControls()
		{
			grdDoses.DataSource = Dose_cu.ItemsList;
			CommonViewsActions.FillGridlookupEdit(lkeMedication, Medication_cu.ItemsList);
		}

		public override void ClearControls()
		{
			lst_DiagnosisCategory_Diagnosis.DataSource = null;
			lkeMedication.EditValue = null;
			List_SelectedDosesToBedAdded = null;
			SelectedDoseFromGrid = null;
			List_Medication_Dose = null;
		}

		private void btnAddList_Click(object sender, System.EventArgs e)
		{
			if (lkeMedication.EditValue == null)
			{
				XtraMessageBox.Show("يجـب إختيــار الـــدواء / العـــلاج", "تنبيــــــــــه", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}

			if (SelectedDoseFromGrid == null)
			{
				XtraMessageBox.Show("يجب إختيــار الجـرعــــــة", "تنبيــــــــــه", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}

			if (List_SelectedDosesToBedAdded == null)
				List_SelectedDosesToBedAdded = new List<Dose_cu>();

			if (List_SelectedDosesToBedAdded.Count > 0)
				if (List_SelectedDosesToBedAdded.Exists(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(SelectedDoseFromGrid.ID))))
				{
					XtraMessageBox.Show("تمــت إضافتـــه مـن قبـــل", "تنبيــــــــــه", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}

			List_SelectedDosesToBedAdded.Add(SelectedDoseFromGrid);
			CommonViewsActions.FillListBoxControl(lst_DiagnosisCategory_Diagnosis, List_SelectedDosesToBedAdded);
			lst_DiagnosisCategory_Diagnosis.Refresh();

			Medication_Dose_cu bridge = new Medication_Dose_cu();
			bridge.Dose_CU_ID = SelectedDoseFromGrid.ID;
			bridge.Medication_CU_ID = Convert.ToInt32(lkeMedication.EditValue);
			if (List_Medication_Dose == null)
				List_Medication_Dose = new List<Medication_Dose_cu>();
			List_Medication_Dose.Add(bridge);
		}

		private void btnRemoveFromList_Click(object sender, EventArgs e)
		{
			if (lst_DiagnosisCategory_Diagnosis.SelectedItems.Count == 0 || List_Medication_Dose == null)
			{
				XtraMessageBox.Show("لا يـوجــد", "تنبيــــــــــه", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}

			Dose_cu selectedDose = (Dose_cu)lst_DiagnosisCategory_Diagnosis.SelectedItem;
			if (selectedDose == null)
				return;
			if (List_SelectedDosesToBedAdded.Exists(
				item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(selectedDose.ID))))
				List_SelectedDosesToBedAdded.Remove(selectedDose);

			CommonViewsActions.FillListBoxControl(lst_DiagnosisCategory_Diagnosis, List_SelectedDosesToBedAdded, "Name_P");
			lst_DiagnosisCategory_Diagnosis.Refresh();

			Medication_Dose_cu userBridge =
				List_Medication_Dose.Find(item => Convert.ToInt32(item.Dose_CU_ID).Equals(Convert.ToInt32(selectedDose.ID)));
			if (userBridge == null)
				return;
			List_Medication_Dose.Remove(userBridge);
		}

		private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
		{
			SelectedDoseFromGrid = CommonViewsActions.GetSelectedRowObject<Dose_cu>(gridView1);
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

		#endregion

		#region Implementation of IMedication_Dose_Viewer

		public List<Medication_Dose_cu> List_Medication_Dose { get; set; }
		public object Medication_CU_ID { get; set; }
		public object Dose_CU_ID { get; set; }

		#endregion

	}
}
