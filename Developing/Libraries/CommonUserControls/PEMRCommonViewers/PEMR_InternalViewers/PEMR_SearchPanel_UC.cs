using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CommonControlLibrary;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using MerkDataBaseBusinessLogicProject;

namespace CommonUserControls.PEMRCommonViewers.PEMR_InternalViewers
{
	public enum SearchPanelType
	{
		DiagnosisSearch = 1,
		DiagnosisCategory = 2,
	}

	public partial class PEMR_SearchPanel_UC : UserControl
	{
		private SearchPanelType SearchPanelType { get; set; }
		private List<Diagnosis_cu> DiagnosisList { get; set; }
		private Diagnosis_cu SelectedDiagnosis { get; set; }

		public PEMR_SearchPanel_UC()
		{
			InitializeComponent();

			CommonViewsActions.SetupGridControl(gridControl1, Resources.LocalizedRes.grd_PEMR_SearchPanel_Diagnosis,
				true);
		}

		public void Initialize(SearchPanelType searchPanelType)
		{
			SearchPanelType = searchPanelType;
			gridControl1.DataSource = Diagnosis_cu.ItemsList;
		}

		private void btnExit_Click(object sender, System.EventArgs e)
		{
			if(ParentForm != null)
				ParentForm.Close();
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			if (DiagnosisList == null)
				DiagnosisList = new List<Diagnosis_cu>();

			if (!string.IsNullOrEmpty(txtID.Text) && !string.IsNullOrWhiteSpace(txtID.Text))
				switch (SearchPanelType)
				{
					case SearchPanelType.DiagnosisSearch:
						Diagnosis_cu diagnosis =
							Diagnosis_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(txtID.EditValue)));
						if(diagnosis != null)
							DiagnosisList.Add(diagnosis);

						gridControl1.DataSource = DiagnosisList;
						gridControl1.RefreshDataSource();
						return;
				}

			if (!string.IsNullOrEmpty(txtAbbreviation.Text) && !string.IsNullOrWhiteSpace(txtAbbreviation.Text))
				switch (SearchPanelType)
				{
					case SearchPanelType.DiagnosisSearch:
						DiagnosisList = Diagnosis_cu.ItemsList
							.Where(item =>
								item.Abbreviation != null && item.Abbreviation.Contains(txtAbbreviation.Text)).ToList();
						gridControl1.DataSource = DiagnosisList;
						gridControl1.RefreshDataSource();
						return;
				}

			if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrWhiteSpace(txtName.Text))
				switch (SearchPanelType)
				{
					case SearchPanelType.DiagnosisSearch:
						DiagnosisList = Diagnosis_cu.ItemsList.Where(item =>
							item.Name_P != null && item.Name_P.Contains(txtName.Text) ||
							item.Name_S != null && item.Name_S.Contains(txtName.Text)).ToList();
						gridControl1.DataSource = DiagnosisList;
						gridControl1.RefreshDataSource();
						return;
				}
		}

		private void txtID_EditValueChanged(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrWhiteSpace(txtID.Text))
			{
				gridControl1.DataSource = Diagnosis_cu.ItemsList;
				return;
			}

			txtAbbreviation.EditValue = null;
			txtName.Text = null;

			if (DiagnosisList == null)
				DiagnosisList = new List<Diagnosis_cu>();
			Diagnosis_cu diagnosis =
				Diagnosis_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(txtID.EditValue)));
			if(diagnosis != null)
				DiagnosisList.Add(diagnosis);

			gridControl1.DataSource = DiagnosisList;
			gridControl1.RefreshDataSource();
		}

		private void txtAbbreviation_EditValueChanged(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtAbbreviation.Text) || string.IsNullOrWhiteSpace(txtAbbreviation.Text))
			{
				gridControl1.DataSource = Diagnosis_cu.ItemsList;
				return;
			}

			txtID.EditValue = null;
			txtName.EditValue = null;

			if (DiagnosisList == null)
				DiagnosisList = new List<Diagnosis_cu>();

			DiagnosisList = Diagnosis_cu.ItemsList
				.Where(item => item.Abbreviation != null && item.Abbreviation.Contains(txtAbbreviation.Text)).ToList();

			gridControl1.DataSource = DiagnosisList;
			gridControl1.RefreshDataSource();
		}

		private void txtName_EditValueChanged(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrWhiteSpace(txtName.Text))
			{
				gridControl1.DataSource = Diagnosis_cu.ItemsList;
				return;
			}

			txtID.EditValue = null;
			txtAbbreviation.EditValue = null;

			if (DiagnosisList == null)
				DiagnosisList = new List<Diagnosis_cu>();

			DiagnosisList = Diagnosis_cu.ItemsList.Where(item =>
				item.Name_P != null && item.Name_P.Contains(txtName.Text) ||
				item.Name_S != null && item.Name_S.ToLower().Contains(txtName.Text.ToLower())).ToList();

			gridControl1.DataSource = DiagnosisList;
			gridControl1.RefreshDataSource();
		}

		private void gridView1_DoubleClick(object sender, EventArgs e)
		{
			SelectedDiagnosis = CommonViewsActions.GetSelectedRowObject<Diagnosis_cu>(gridView1);
			DialogResult result = XtraMessageBox.Show(
				"Do you want to add this Diagnose ?" + "\r\n" + SelectedDiagnosis.DiagnosisFullName, "Note",
				MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1,
				DefaultBoolean.Default);
			switch (result)
			{
				case DialogResult.Yes:
					PEMR_Diagnosis_UC.SelectedDiagnosisFromSearch = SelectedDiagnosis;
					btnExit_Click(null, null);
					break;
			}
		}
	}
}
