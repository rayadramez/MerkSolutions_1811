using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.DoctorCategoriesViewers
{
	public partial class DoctorCategory_EditorViewer : 
		//UserControl
		CommonAbstractEditorViewer<DoctorCategory_cu>,
		IDoctorCategoryViewer
	{
		public DoctorCategory_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_DoctorCategory_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<DoctorCategory_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.DoctorCategory_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "تصنيفــــــات الأطبـــــــاء"; }
		}

		public override void ClearControls()
		{
			Name_P = null;
			Name_S = null;
			Description = null;
		}

		#endregion

		#region Implementation of IDoctorCategoryViewer

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
