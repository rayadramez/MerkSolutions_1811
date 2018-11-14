using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using CommonUserControls.PEMRCommonViewers.PEMR_Interfaces;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.HitInfo;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.PEMRBusinessLogic;
using MVCBusinessLogicLibrary.BaseViewers;

namespace CommonUserControls.PEMRCommonViewers.PEMR_InternalViewers
{
	public enum FullScreenMode
	{
		FullScreen = 1,
		NotFullScreen = 2
	}

	public partial class PEMR_Diagnosis_UC : UserControl, IPEMR_Viewer, IPEMR_Diagnosis
	{
		private List<Diagnosis_cu> TempDiagnosisListToBeAdded { get; set; }
		public object FurtherDetails
		{
			get { return txtReccommednations.EditValue; }
			set { txtReccommednations.EditValue = value; }
		}

		public List<Diagnosis_cu> AddedDiagnosisList { get; set; }
		public static Diagnosis_cu SelectedDiagnosisFromSearch { get; set; }
		public FullScreenMode FullScreenMode { get; set; }
		private PEMR_Diagnosis_UC _pemrDiagnosis;
		private VisitTiming_MainDiagnosis _mainVisitDiagnosis = null;
		private VisitTiming_Diagnosis _visitDiagnosis = null;
		private Control ParentControl { get; set; }

		public PEMR_Diagnosis_UC()
		{
			InitializeComponent();
			switch (ApplicationStaticConfiguration.Organization)
			{
				case DB_Organization.Cardiovascular_Clinic:
					CommonViewsActions.LoadXMLFromString(layoutControl1,
						Resources.LocalizedRes.lyt_PEMR_Diagnosis_Cardiology);
					break;
				case DB_Organization.Ophthalmology:
					CommonViewsActions.LoadXMLFromString(layoutControl1,
						Resources.LocalizedRes.lyt_PEMR_Diagnosis_Opthalmology);
					break;
			}
		}

		private void PEMR_Diagnosis_UC_Load(object sender, EventArgs e)
		{
			ParentControl = this.Parent;
			ClearControls(false);
			FillControls();
		}

		public void Initialize()
		{
			lytGroup_DoctorCategories.Expanded = false;
			lytGroup_DoctorDiagnosis.Expanded = false;
			lstDiagnosisCategories.SelectedIndex = -1;
			lstDoctorDiagnosis.SelectedIndex = -1;
			lstDoctorDiagnosisCategories.SelectedIndex = -1;
			lstDoctorDiagnosis.SelectedIndex = -1;
			FullScreenMode = FullScreenMode.NotFullScreen;
			txtReccommednations.EnterMoveNextControl = false;

			if (PEMRBusinessLogic.ActivePEMRObject == null ||
			    PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainDiagnosis == null ||
			    PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainDiagnosis.Count == 0 ||
			    PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Diagnosis == null ||
			    PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Diagnosis.Count == 0)
				return;

			txtReccommednations.EditValue = PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainDiagnosis[0]
				.GeneralDescription;

			AddedDiagnosisList = null;
			foreach (VisitTiming_Diagnosis timingDiagnosi in PEMRBusinessLogic.ActivePEMRObject
				.List_VisitTiming_Diagnosis.FindAll(item =>
					!Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.Removed))))
			{
				Diagnosis_cu diagnosis = Diagnosis_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(timingDiagnosi.Diagnosis_CU_ID)));
				if (diagnosis != null)
					if (AddedDiagnosisList == null)
						AddedDiagnosisList = new List<Diagnosis_cu>();
				AddedDiagnosisList.Add(diagnosis);
			}

			ClearControls(false);
			CommonViewsActions.FillListBoxControl(lstAddedDiagnosis, AddedDiagnosisList, "DiagnosisFullName");

			PEMRBusinessLogic.PEMR_Diagnosis = this;
		}

		#region IPEMR_Viewer

		public void ClearControls(bool clearAll)
		{
			lstAddedDiagnosis.DataSource = null;
			if (clearAll)
				txtReccommednations.EditValue = null;
		}

		public void FillControls()
		{
			CommonViewsActions.FillListBoxControl(lstDiagnosisCategories,
				PEMRBusinessLogic.GetDiagnosisCategoriesList(false), "DiagnosisCategoryFullName");
			CommonViewsActions.FillListBoxControl(lstDoctorDiagnosisCategories,
				PEMRBusinessLogic.GetDiagnosisCategoriesList(ApplicationStaticConfiguration.ActiveLoginUser
					.Person_CU_ID), "DiagnosisCategoryFullName");

			CommonViewsActions.FillListBoxControl(lstDiagnosis, PEMRBusinessLogic.GetDiagnosisList(false),
				"DiagnosisFullName");
			CommonViewsActions.FillListBoxControl(lstDoctorDiagnosis,
				PEMRBusinessLogic.GetDiagnosisList(ApplicationStaticConfiguration.ActiveLoginUser.Person_CU_ID),
				"DiagnosisFullName");
		}

		#endregion

		private void SetDiagnosisCount(List<Diagnosis_cu> list)
		{
			if (list != null)
				lblCount.Text = list.Count.ToString();
		}

		public DB_DiagnosisType DiagnosisType
		{
			get
			{
				if (chkProvisional.Checked)
					return DB_DiagnosisType.Provisional;
				if (chkDifferential.Checked)
					return DB_DiagnosisType.Differential;
				if (chkFinal.Checked)
					return DB_DiagnosisType.Final;
				return DB_DiagnosisType.None;
			}
			set
			{
				DB_DiagnosisType type = value;
				switch (type)
				{
					case DB_DiagnosisType.Provisional:
						chkProvisional.Checked = true;
						break;
					case DB_DiagnosisType.Differential:
						chkDifferential.Checked = true;
						break;
					case DB_DiagnosisType.Final:
						chkFinal.Checked = true;
						break;
				}
			}
		}

		public object EyeType
		{
			get
			{
				if (chkEye_OS.Checked)
					return DB_EyeType_p.OS;
				if (chkEye_OD.Checked)
					return DB_EyeType_p.OD;
				if (chkEye_OU.Checked)
					return DB_EyeType_p.OU;
				return null;
			}
			set
			{
				if (value == null)
					return;
				DB_EyeType_p type = (DB_EyeType_p)value;
				switch (type)
				{
					case DB_EyeType_p.OS:
						chkEye_OS.Checked = true;
						break;
					case DB_EyeType_p.OD:
						chkEye_OD.Checked = true;
						break;
					case DB_EyeType_p.OU:
						chkEye_OU.Checked = true;
						break;
				}
			}
		}

		#region Controls Events

		#region Buttons Events

		private void btnAddList_Click(object sender, System.EventArgs e)
		{
			if (TempDiagnosisListToBeAdded == null || TempDiagnosisListToBeAdded.Count == 0)
			{
				XtraMessageBox.Show(
					"You should select at least one Diagnose to be added." + "\r\n" +
					"You can select one or more Diagnose to add", "Note", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}

			switch (ApplicationStaticConfiguration.Organization)
			{
				case DB_Organization.Ophthalmology:
					if (EyeType == null)
					{
						XtraMessageBox.Show("You should select Eye Type before adding Diagnosis", "Note",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					break;
			}

			if (AddedDiagnosisList == null)
				AddedDiagnosisList = new List<Diagnosis_cu>();
			if (TempDiagnosisListToBeAdded != null && TempDiagnosisListToBeAdded.Count > 0)
			{
				foreach (Diagnosis_cu diagnosisCu in TempDiagnosisListToBeAdded)
				{
					if (!AddedDiagnosisList.Exists(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(diagnosisCu.ID))))
					{
						AddedDiagnosisList.Add(diagnosisCu);
						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainDiagnosis == null)
						{
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainDiagnosis =
								new List<VisitTiming_MainDiagnosis>();
							_mainVisitDiagnosis = PEMRBusinessLogic.CreateNew_VisitTiming_MainDiagnosis(FurtherDetails,
								DiagnosisType, ApplicationStaticConfiguration.PEMRSavingMode);
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainDiagnosis.Add(_mainVisitDiagnosis);
						}

						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainDiagnosis[0] != null)
						{
							if (!string.IsNullOrEmpty(txtReccommednations.Text) ||
								!string.IsNullOrWhiteSpace(txtReccommednations.Text))
								PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainDiagnosis[0].GeneralDescription
									= txtReccommednations.Text;
							else
								PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainDiagnosis[0]
									.GeneralDescription = null;
						}

						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Diagnosis == null)
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Diagnosis =
								new List<VisitTiming_Diagnosis>();

						_visitDiagnosis = PEMRBusinessLogic.CreateNew_VisitTiming_Diagnosis(
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainDiagnosis[0], diagnosisCu, EyeType,
							ApplicationStaticConfiguration.PEMRSavingMode);
						if (_visitDiagnosis != null)
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Diagnosis.Add(_visitDiagnosis);
					}
				}
			}

			TempDiagnosisListToBeAdded = null;
			CommonViewsActions.FillListBoxControl(lstAddedDiagnosis, AddedDiagnosisList, "DiagnosisFullName");
			lstAddedDiagnosis.Refresh();
			SetDiagnosisCount(AddedDiagnosisList);

			lstDiagnosis.SelectedIndex = -1;
			lstDoctorDiagnosis.SelectedIndex = -1;
		}

		private void btnRemoveFromList_Click(object sender, EventArgs e)
		{
			if (AddedDiagnosisList == null || AddedDiagnosisList.Count == 0)
			{
				XtraMessageBox.Show(
					"You should select at least one Diagnose to be remove." + "\r\n" +
					"You can select one or more Diagnose to remove", "Note", MessageBoxButtons.OK, MessageBoxIcon.Hand,
					MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
				return;
			}

			foreach (object selectedItem in lstAddedDiagnosis.SelectedItems)
			{
				if (selectedItem is Diagnosis_cu)
				{
					Diagnosis_cu diagnosis = Diagnosis_cu.ItemsList.Find(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((Diagnosis_cu)selectedItem).ID)));
					if (diagnosis != null)
					{
						if (AddedDiagnosisList.Exists(item =>
							Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((Diagnosis_cu)selectedItem).ID))))
							AddedDiagnosisList.Remove(diagnosis);
						VisitTiming_Diagnosis visitTimingDiagnosis =
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Diagnosis.Find(item =>
								Convert.ToInt32(item.Diagnosis_CU_ID).Equals(Convert.ToInt32(diagnosis.ID)));
						if (visitTimingDiagnosis != null)
							visitTimingDiagnosis.PEMRElementStatus = PEMRElementStatus.Removed;
					}
				}
			}

			CommonViewsActions.FillListBoxControl(lstAddedDiagnosis, AddedDiagnosisList, "DiagnosisFullName");
			lstAddedDiagnosis.Refresh();
			SetDiagnosisCount(AddedDiagnosisList);

			lstDiagnosis.SelectedIndex = -1;
			lstDoctorDiagnosis.SelectedIndex = -1;
			lstAddedDiagnosis.SelectedIndex = -1;
		}

		private void btnClearDiagnosis_Click(object sender, EventArgs e)
		{
			lstDiagnosis.SelectedIndex = -1;
		}

		private void btnClearDoctorDiagnosis_Click(object sender, EventArgs e)
		{
			lstDoctorDiagnosis.SelectedIndex = -1;
		}

		private void btnClearAddedDiagnosis_Click(object sender, EventArgs e)
		{
			lstAddedDiagnosis.SelectedIndex = -1;
		}

		private void btnClearDiagnosisCategories_Click(object sender, EventArgs e)
		{
			lstDiagnosisCategories.SelectedIndex = -1;
			lstDiagnosis.DataSource = null;
		}

		private void btnClearDoctorDiagnosisCategories_Click(object sender, EventArgs e)
		{
			lstDoctorDiagnosisCategories.SelectedIndex = -1;
			lstDiagnosis.DataSource = null;
		}

		private void btnSearchDiagnosis_Click(object sender, EventArgs e)
		{
			PEMR_SearchPanel_UC searchPanel = new PEMR_SearchPanel_UC();
			searchPanel.Initialize(SearchPanelType.DiagnosisSearch);
			DialogResult result = PopupBaseForm.ShowAsPopup(searchPanel, this);
			switch (result)
			{
				case DialogResult.Cancel:
					if (SelectedDiagnosisFromSearch != null)
					{
						if (AddedDiagnosisList == null)
							AddedDiagnosisList = new List<Diagnosis_cu>();
						if (AddedDiagnosisList.Count == 0 || !AddedDiagnosisList.Exists(item =>
								Convert.ToInt32(item.ID).Equals(Convert.ToInt32(SelectedDiagnosisFromSearch.ID))))
							AddedDiagnosisList.Add(SelectedDiagnosisFromSearch);

						TempDiagnosisListToBeAdded = null;
						CommonViewsActions.FillListBoxControl(lstAddedDiagnosis, AddedDiagnosisList, "DiagnosisFullName");
						lstAddedDiagnosis.Refresh();
						SetDiagnosisCount(AddedDiagnosisList);

						lstDiagnosis.SelectedIndex = -1;
						lstDoctorDiagnosis.SelectedIndex = -1;
						lstAddedDiagnosis.SelectedIndex = -1;
					}
					break;
			}
		}

		private void btnAddedDiagnosisFullScreen_Click(object sender, EventArgs e)
		{
			switch (FullScreenMode)
			{
				case FullScreenMode.NotFullScreen:
					FullScreenMode = FullScreenMode.FullScreen;
					btnAddedDiagnosisFullScreen.Image = Properties.Resources.ExitFullScreen_08_01;
					PopupBaseForm.ShowAsPopup(this, this, colorTouse: Color.White);
					break;
				case FullScreenMode.FullScreen:
					if (ParentForm != null)
					{
						//CommonViewsActions.ShowUserControl(ref _pemrDiagnosis, ParentControl);
						ParentForm.Close();
						ParentControl.Controls.Add(this);
					}
					btnAddedDiagnosisFullScreen.Image = Properties.Resources.FullScreen_1_161;
					FullScreenMode = FullScreenMode.NotFullScreen;
					break;
			}

		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (PEMRBusinessLogic.ActivePEMRObject != null)
				if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainDiagnosis == null ||
					PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainDiagnosis.Count == 0)
				{
					_mainVisitDiagnosis = PEMRBusinessLogic.CreateNew_VisitTiming_MainDiagnosis(FurtherDetails,
						DiagnosisType, DB_PEMRSavingMode.SaveImmediately);
					if (_mainVisitDiagnosis == null)
						return;
					if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainDiagnosis == null)
						PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainDiagnosis = new List<VisitTiming_MainDiagnosis>();
					PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainDiagnosis.Add(_mainVisitDiagnosis);
					XtraMessageBox.Show("Saved Successfully", "Saved", MessageBoxButtons.OK,
						MessageBoxIcon.Information);
				}
				else
				{
					if (_mainVisitDiagnosis == null)
						return;
					if (PEMRBusinessLogic.Update_VisitTiming_MainDiagnosis(this, _mainVisitDiagnosis))
						XtraMessageBox.Show("Saved Successfully", "Saved", MessageBoxButtons.OK,
							MessageBoxIcon.Information);
				}
		}

		#endregion

		#region Check Button Events

		private void chkEye_OD_CheckedChanged(object sender, EventArgs e)
		{
			switch (ApplicationStaticConfiguration.Organization)
			{
				case DB_Organization.Ophthalmology:
					if (EyeType == null || PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Diagnosis == null)
						return;
					AddedDiagnosisList = PEMRBusinessLogic.GetDiagnosisList(
						PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Diagnosis.FindAll(item =>
							!Convert.ToInt32(item.PEMRElementStatus)
								.Equals(Convert.ToInt32(PEMRElementStatus.Removed))),
						chkAll.Checked ? DB_EyeType_p.All : (DB_EyeType_p)EyeType);
					CommonViewsActions.FillListBoxControl(lstAddedDiagnosis, AddedDiagnosisList, "DiagnosisFullName");
					lstAddedDiagnosis.Refresh();
					SetDiagnosisCount(AddedDiagnosisList);
					break;
			}
		}

		private void chkEye_OU_CheckedChanged(object sender, EventArgs e)
		{
			switch (ApplicationStaticConfiguration.Organization)
			{
				case DB_Organization.Ophthalmology:
					if (EyeType == null || PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Diagnosis == null)
						return;
					AddedDiagnosisList = PEMRBusinessLogic.GetDiagnosisList(
						PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Diagnosis.FindAll(item =>
							!Convert.ToInt32(item.PEMRElementStatus)
								.Equals(Convert.ToInt32(PEMRElementStatus.Removed))),
						chkAll.Checked ? DB_EyeType_p.All : (DB_EyeType_p)EyeType);
					CommonViewsActions.FillListBoxControl(lstAddedDiagnosis, AddedDiagnosisList, "DiagnosisFullName");
					lstAddedDiagnosis.Refresh();
					SetDiagnosisCount(AddedDiagnosisList);
					break;
			}
		}

		private void chkEye_OS_CheckedChanged(object sender, EventArgs e)
		{
			switch (ApplicationStaticConfiguration.Organization)
			{
				case DB_Organization.Ophthalmology:
					if (EyeType == null || PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Diagnosis == null)
						return;
					AddedDiagnosisList = PEMRBusinessLogic.GetDiagnosisList(
						PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Diagnosis.FindAll(item =>
							!Convert.ToInt32(item.PEMRElementStatus)
								.Equals(Convert.ToInt32(PEMRElementStatus.Removed))),
						chkAll.Checked ? DB_EyeType_p.All : (DB_EyeType_p)EyeType);
					CommonViewsActions.FillListBoxControl(lstAddedDiagnosis, AddedDiagnosisList, "DiagnosisFullName");
					lstAddedDiagnosis.Refresh();
					SetDiagnosisCount(AddedDiagnosisList);
					break;
			}
		}

		private void chkAll_CheckedChanged(object sender, EventArgs e)
		{
			switch (ApplicationStaticConfiguration.Organization)
			{
				case DB_Organization.Ophthalmology:
					if (EyeType == null || PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Diagnosis == null)
						return;
					AddedDiagnosisList = PEMRBusinessLogic.GetDiagnosisList(
						PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Diagnosis.FindAll(item =>
							!Convert.ToInt32(item.PEMRElementStatus)
								.Equals(Convert.ToInt32(PEMRElementStatus.Removed))),
						chkAll.Checked ? DB_EyeType_p.All : (DB_EyeType_p)EyeType);
					CommonViewsActions.FillListBoxControl(lstAddedDiagnosis, AddedDiagnosisList, "DiagnosisFullName");
					lstAddedDiagnosis.Refresh();
					SetDiagnosisCount(AddedDiagnosisList);
					break;
			}
		}

		#endregion

		#region TextEdit Events

		private void txtReccommednations_EditValueChanged(object sender, EventArgs e)
		{
			if (PEMRBusinessLogic.ActivePEMRObject != null &&
				PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainDiagnosis != null &&
				PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainDiagnosis.Count > 0 &&
				PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainDiagnosis[0] != null)
			{
				if (!string.IsNullOrEmpty(txtReccommednations.Text) ||
					!string.IsNullOrWhiteSpace(txtReccommednations.Text))
					PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainDiagnosis[0].GeneralDescription =
						txtReccommednations.Text;
				else
					PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainDiagnosis[0].GeneralDescription = null;
			}
		}

		#endregion

		#region List Events

		private void lstDiagnosisCategories_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lstDiagnosisCategories.SelectedValue == null)
				return;
			if (lstDiagnosisCategories.SelectedValue is DiagnosisCategory_cu)
				CommonViewsActions.FillListBoxControl(lstDiagnosis,
					PEMRBusinessLogic.GetDiagnosisList(((DiagnosisCategory_cu)lstDiagnosisCategories.SelectedValue)
						.ID),
					"DiagnosisFullName");
			else
				CommonViewsActions.FillListBoxControl(lstDiagnosis,
					PEMRBusinessLogic.GetDiagnosisList(lstDiagnosisCategories.SelectedValue),
					"DiagnosisFullName");
			lstDoctorDiagnosisCategories.SelectedIndex = -1;
		}

		private void lstDoctorDiagnosisCategories_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lstDoctorDiagnosisCategories.SelectedValue == null)
				return;
			if (lstDoctorDiagnosisCategories.SelectedValue is DiagnosisCategory_cu)
				CommonViewsActions.FillListBoxControl(lstDiagnosis,
					PEMRBusinessLogic.GetDiagnosisList(((DiagnosisCategory_cu)lstDoctorDiagnosisCategories.SelectedValue)
						.ID),
					"DiagnosisFullName");
			else
				CommonViewsActions.FillListBoxControl(lstDiagnosis,
					PEMRBusinessLogic.GetDiagnosisList(lstDoctorDiagnosisCategories.SelectedValue),
					"DiagnosisFullName");
			lstDiagnosisCategories.SelectedIndex = -1;
		}

		private void lstDiagnosis_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lstDiagnosis.SelectedItems == null || lstDiagnosis.SelectedItems.Count == 0)
				return;
			TempDiagnosisListToBeAdded = null;
			foreach (object selectedItem in lstDiagnosis.SelectedItems)
			{
				Diagnosis_cu diagnosis = Diagnosis_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((Diagnosis_cu)selectedItem).ID)));
				if (diagnosis != null)
				{
					if (TempDiagnosisListToBeAdded == null)
						TempDiagnosisListToBeAdded = new List<Diagnosis_cu>();
					if (!TempDiagnosisListToBeAdded.Exists(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((Diagnosis_cu)selectedItem).ID))))
						TempDiagnosisListToBeAdded.Add(diagnosis);
				}
			}

			lstDoctorDiagnosis.SelectedIndex = -1;
		}

		private void lstDoctorDiagnosis_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lstDoctorDiagnosis.SelectedItems == null || lstDoctorDiagnosis.SelectedItems.Count == 0)
				return;
			TempDiagnosisListToBeAdded = null;
			foreach (object selectedItem in lstDoctorDiagnosis.SelectedItems)
			{
				Diagnosis_cu diagnosis = Diagnosis_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((Diagnosis_cu)selectedItem).ID)));
				if (diagnosis != null)
				{
					if (TempDiagnosisListToBeAdded == null)
						TempDiagnosisListToBeAdded = new List<Diagnosis_cu>();
					if (!TempDiagnosisListToBeAdded.Exists(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((Diagnosis_cu)selectedItem).ID))))
						TempDiagnosisListToBeAdded.Add(diagnosis);
				}
			}

			lstDiagnosis.SelectedIndex = -1;
		}

		private void lstDiagnosis_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			btnAddList_Click(null, null);
		}

		private void lstDoctorDiagnosis_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			btnAddList_Click(null, null);
		}

		#endregion

		#region Layout Events

		private void lytGroup_DoctorCategories_MouseUp(object sender, MouseEventArgs e)
		{
			BaseLayoutItemHitInfo hitInfo = layoutControl1.CalcHitInfo(e.Location);
			if (hitInfo.HitType != LayoutItemHitTest.Item || e.Clicks != 1)
				return;
			LayoutGroup group = hitInfo.Item as LayoutGroup;
			if (group != null)
				if (group.ViewInfo.BorderInfo.CaptionBounds.Contains(e.Location))
				{
					group.Expanded = !group.Expanded;
					if (group.Expanded)
					{
						group.MinSize = new Size(300, 200);
						group.MaxSize = new Size(300, 200);
						group.Size = new Size(300, 200);
						group.CaptionImage = Properties.Resources.Expanded_06;
					}
					else
						group.CaptionImage = Properties.Resources.NotExpanded_06;
				}
		}

		private void lytGroup_DoctorDiagnosis_MouseUp(object sender, MouseEventArgs e)
		{
			BaseLayoutItemHitInfo hitInfo = layoutControl1.CalcHitInfo(e.Location);
			if (hitInfo.HitType != LayoutItemHitTest.Item || e.Clicks != 1)
				return;
			LayoutGroup group = hitInfo.Item as LayoutGroup;
			if (group != null)
				if (group.ViewInfo.BorderInfo.CaptionBounds.Contains(e.Location))
				{
					group.Expanded = !group.Expanded;
					if (group.Expanded)
					{
						group.MinSize = new Size(320, 200);
						group.MaxSize = new Size(320, 200);
						group.Size = new Size(320, 200);
						group.CaptionImage = Properties.Resources.Expanded_06;
					}
					else
						group.CaptionImage = Properties.Resources.NotExpanded_06;
				}
		}

		#endregion

		#endregion
	}
}
