using System;
using ApplicationConfiguration;
using CommonControlLibrary;
using DevExpress.XtraLayout.Utils;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.DiagnosisViewers
{
	public partial class Diagnosis_EditorViewer : 
		//UserControl
		CommonAbstractEditorViewer<Diagnosis_cu>,
		IDiagnosis_Viewer
	{
		public Diagnosis_EditorViewer()
		{
			InitializeComponent();

			switch (ApplicationStaticConfiguration.Application)
			{
				case DB_Application.PEMR:
					CommonViewsActions.LoadXMLFromString(layoutControl1,
						Resources.LocalizedRes.lyt_Diagnosis_EditorViewer_en_US);
					chkIsDoctorRelated.Text = "Is Doctor Related";
					break;
				case DB_Application.Settings:
					CommonViewsActions.LoadXMLFromString(layoutControl1,
						Resources.LocalizedRes.lyt_Diagnosis_EditorViewer);
					chkIsDoctorRelated.Text = "تصنيـــف خــاص بطبيــــب";
					break;
			}
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<Diagnosis_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.Diagnosis_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return ""; }
		}

		public override void ClearControls()
		{
			Name_P = null;
			Name_S = null;
			Abbreviation = null;
			Description = null;
		}

		#endregion

		#region Implementation of IDiagnosis_Viewer

		public object Name_P
		{
			get { return txtNameP.EditValue; }
			set { txtNameP.EditValue = value; }
		}

		public object Name_S
		{
			get { return txtNameS.EditValue; }
			set { txtNameS.EditValue = value; }
		}

		public object Abbreviation
		{
			get { return txtAbbreviation.EditValue; }
			set { txtAbbreviation.EditValue = value; }
		}

		public object Description
		{
			get { return txtDescription.EditValue; }
			set { txtDescription.EditValue = value; }
		}

		public object IsDoctorRelated
		{
			get { return chkIsDoctorRelated.Checked; }
			set { chkIsDoctorRelated.Checked = Convert.ToBoolean(value); }
		}

		public object DoctorID
		{
			get { return lkeDoctors.EditValue; }
			set { lkeDoctors.EditValue = value; }
		}

		#endregion

		private void chkIsDoctorRelated_CheckedChanged(object sender, System.EventArgs e)
		{
			lytDoctors.Visibility = chkIsDoctorRelated.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			if (chkIsDoctorRelated.Checked)
				CommonViewsActions.FillGridlookupEdit(lkeDoctors, Doctor_cu.ItemsList, "Name_P", "Person_CU_ID");
			else
				lkeDoctors.Properties.DataSource = null;
		}
	}
}
