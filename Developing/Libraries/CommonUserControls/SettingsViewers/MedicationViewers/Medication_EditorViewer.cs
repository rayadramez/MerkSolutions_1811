using System;
using ApplicationConfiguration;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.MedicationViewers
{
	public partial class Medication_EditorViewer : 
		//UserControl
		CommonAbstractEditorViewer<Medication_cu>,
		IMedication_Viewer
	{
		public Medication_EditorViewer()
		{
			InitializeComponent();
		}

		private void Medication_EditorViewer_Load(object sender, EventArgs e)
		{
			switch (ApplicationStaticConfiguration.Application)
			{
				case DB_Application.AdmissionReception:
				case DB_Application.AllReception:
				case DB_Application.ClinicReception:
				case DB_Application.InvoiceManager:
				case DB_Application.QueueManager:
				case DB_Application.Settings:
					CommonViewsActions.LoadXMLFromString(layoutControl1,
						Resources.LocalizedRes.lyt_Medication_SearchViewer);
					break;
				case DB_Application.PEMR:
					CommonViewsActions.LoadXMLFromString(layoutControl1,
						Resources.LocalizedRes.lyt_Medication_EditorViewer_en_US);
					break;
			}

			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<Medication_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.Medication_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "بيـانـــــــات الأدويـــــــة"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeMedicationCategory, MedicationCategory_cu.ItemsList);
		}

		public override void ClearControls()
		{
			txtNameP.EditValue = null;
			txtNameS.EditValue = null;
			lkeMedicationCategory.EditValue = null;
			txtDescription.EditValue = null;
		}

		#endregion

		#region Implementation of IMedicationViewer

		public object MedicationCategory_CU_ID
		{
			get { return lkeMedicationCategory.EditValue; }
			set { lkeMedicationCategory.EditValue = value; }
		}

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

		public object Description
		{
			get { return txtDescription.EditValue; }
			set { txtDescription.EditValue = value; }
		}

		#endregion
	}
}
