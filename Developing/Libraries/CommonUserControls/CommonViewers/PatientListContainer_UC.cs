using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using CommonUserControls.SettingsViewers.PatientViewers;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;

namespace CommonUserControls.CommonViewers
{
	public partial class PatientListContainer_UC : UserControl
	{
		public Control ParentControl { get; set; }
		private Patient_cu ActivePatient { get; set; }
		private List<Patient_cu> PatientsList = new List<Patient_cu>();
		private PatientEditorViewer_UC _patientEditorViewer;

		public PatientListContainer_UC()
		{
			InitializeComponent();

			CommonViewsActions.SetupGridControl(grdPatientsList, Resources.LocalizedRes.grd_PatientsList, false);
			CommonViewsActions.SetupSyle(this);
			Initialize(true);
		}

		public void Initialize(Control parentControl)
		{
			ParentControl = parentControl;

			if (!string.IsNullOrEmpty(ApplicationStaticConfiguration.SkinName) &&
				!string.IsNullOrWhiteSpace(ApplicationStaticConfiguration.SkinName))
				if (ApplicationStaticConfiguration.SkinName.Equals("McSkin"))
					BackColor = Color.LightSlateGray;
				else
					BackColor = Color.FromArgb(50, 59, 74);
		}

		public void Initialize(bool allPatients)
		{
			PatientsList.Clear();
			if (allPatients)
				PatientsList = Patient_cu.GetPatientsList(500);
			else
			{
				if (ActivePatient != null)
					PatientsList.Add(ActivePatient);
				else
					PatientsList = new List<Patient_cu>();
			}

			grdPatientsList.DataSource = PatientsList;
			grdPatientsList.RefreshDataSource();

			if (!string.IsNullOrEmpty(ApplicationStaticConfiguration.SkinName) &&
				!string.IsNullOrWhiteSpace(ApplicationStaticConfiguration.SkinName))
				if (ApplicationStaticConfiguration.SkinName.Equals("McSkin"))
					BackColor = Color.LightSlateGray;
				else
					BackColor = Color.FromArgb(50, 59, 74);
		}

		private void btnNewPatient_Click(object sender, EventArgs e)
		{
			BaseController<Person_cu>.ShowEditorControl(ref _patientEditorViewer, this, null, null, EditorContainerType.Regular,
				ViewerName.PatientViewer, DB_CommonTransactionType.CreateNew, "بيانــات المرضــى", true);
			Initialize(true);
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			bool isAll = ActivePatient == null;
			Initialize(isAll);
		}

		private void btnQuickSearch_Click(object sender, System.EventArgs e)
		{
			if (txtPatientID.EditValue != null)
			{
				ActivePatient =
					PatientsList.Find(item => item.Person_CU_ID.Equals(Convert.ToInt32(txtPatientID.EditValue)));
				if (ActivePatient == null)
					ActivePatient = DBCommon.GetEntity<Patient_cu>(Convert.ToInt32(txtPatientID.EditValue));
			}
			else if (txtPatientName.EditValue != null
					 && !string.IsNullOrEmpty(Convert.ToString(txtPatientName.EditValue))
					 && !string.IsNullOrWhiteSpace(Convert.ToString(txtPatientName.EditValue)))
			{
				string[] nameArry = Convert.ToString(txtPatientName.EditValue).Split(' ');
				if (nameArry.Length == 1)
					ActivePatient = PatientsList.Find(
						item => item.Person_cu.FirstName_P.Contains(Convert.ToString(nameArry[0])));
				else if (nameArry.Length == 2)
					ActivePatient = PatientsList.Find(
						item => item.Person_cu.FirstName_P.Contains(Convert.ToString(nameArry[0]))
								|| item.Person_cu.SecondName_P.Contains(Convert.ToString(nameArry[1])));
				else if (nameArry.Length == 3)
					ActivePatient = PatientsList.Find(
						item => item.Person_cu.FirstName_P.Contains(Convert.ToString(nameArry[0]))
								|| item.Person_cu.SecondName_P.Contains(Convert.ToString(nameArry[1]))
								|| item.Person_cu.ThirdName_P.Contains(Convert.ToString(nameArry[2])));
				else if (nameArry.Length == 4)
					ActivePatient = PatientsList.Find(
						item => item.Person_cu.FirstName_P.Contains(Convert.ToString(nameArry[0]))
								|| item.Person_cu.SecondName_P.Contains(Convert.ToString(nameArry[1]))
								|| item.Person_cu.ThirdName_P.Contains(Convert.ToString(nameArry[2]))
								|| item.Person_cu.FourthName_P.Contains(Convert.ToString(nameArry[3])));
			}

			if (ActivePatient == null)
			{
				Initialize(true);
				XtraMessageBox.Show("المريض غير موجود", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error,
					MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
				return;
			}

			Initialize(false);
		}

		private void txtPatientID_EditValueChanged(object sender, EventArgs e)
		{
			if (txtPatientID.EditValue == null)
				return;

			txtPatientName.EditValue = null;
		}

		private void txtPatientName_EditValueChanged(object sender, EventArgs e)
		{
			if (txtPatientName.EditValue == null)
				return;

			txtPatientID.EditValue = null;
		}

		private void gridView1_DoubleClick(object sender, EventArgs e)
		{
			ActivePatient = CommonViewsActions.GetSelectedRowObject<Patient_cu>(gridView1);
			if (ActivePatient == null)
				return;

			MainPatientInvoiceActions patienActions = new MainPatientInvoiceActions();
			patienActions.Initialize(ActivePatient, ParentControl);
			PopupBaseForm.ShowAsPopup(patienActions, this);
		}
	}
}
