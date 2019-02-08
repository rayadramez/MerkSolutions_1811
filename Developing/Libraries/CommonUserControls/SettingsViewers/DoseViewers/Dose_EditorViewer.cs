using ApplicationConfiguration;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.DoseViewers
{
	public partial class Dose_EditorViewer : 
		//UserControl
		CommonAbstractEditorViewer<Dose_cu>,
		IDose_Viewer
	{
		public Dose_EditorViewer()
		{
			InitializeComponent();
		}

		private void Dose_EditorViewer_Load(object sender, System.EventArgs e)
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
						Resources.LocalizedRes.lyt_Dose_EditorViewer);
					break;
				case DB_Application.PEMR:
					CommonViewsActions.LoadXMLFromString(layoutControl1,
						Resources.LocalizedRes.lyt_Dose_EditorViewer_en_US);
					break;
			}

			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<Dose_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.Dose_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return ".... بيـانـــــــات الجـرعــــات ...."; }
		}

		public override void ClearControls()
		{
			Name_P = null;
			Name_S = null;
			Description = null;
		}

		#endregion

		#region Implementation of IDose_Viewer

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
