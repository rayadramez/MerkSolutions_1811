using ApplicationConfiguration;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.DiagnosisViewers
{
	public partial class Diagnosis_SearchViewer : 
		//UserControl
		CommonAbstractSearchViewer<Diagnosis_cu>,
		IDiagnosis_Viewer
	{
		public Diagnosis_SearchViewer()
		{
			InitializeComponent();
			switch (ApplicationStaticConfiguration.Application)
			{
				case DB_Application.PEMR:
					CommonViewsActions.LoadXMLFromString(layoutControl1,
						Resources.LocalizedRes.lyt_Diagnosis_SearchViewer_en_US);
					break;
				case DB_Application.Settings:
					CommonViewsActions.LoadXMLFromString(layoutControl1,
						Resources.LocalizedRes.lyt_Diagnosis_SearchViewer);
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

		public override string GridXML
		{
			get
			{
				switch (ApplicationStaticConfiguration.Application)
				{
					case DB_Application.PEMR:
						CommonViewsActions.LoadXMLFromString(layoutControl1,
							Resources.LocalizedRes.grd_Diagnosis_SearchViewer_en_US);
						break;
					case DB_Application.Settings:
						return Resources.LocalizedRes.grd_Diagnosis_SearchViewer;
						break;
				}

				return "";
			}
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

		public object IsDoctorRelated { get; set; }
		public object DoctorID { get; set; }

		#endregion
	}
}
