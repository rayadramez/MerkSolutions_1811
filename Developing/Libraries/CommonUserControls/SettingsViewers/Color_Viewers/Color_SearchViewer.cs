using System;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.Color_Viewers
{
	public partial class Color_SearchViewer :
		//UserControl
		CommonAbstractSearchViewer<Color_cu>,
		IColorViewer
	{
		public Color_SearchViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_Color_SearchViewer);
			CommonViewsActions.SetupSyle(this);
		}

		private void txtInternalCode_EditValueChanged(object sender, EventArgs e)
		{

		}

		#region Overrides of CommonAbstractViewer<Color_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.Color_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "المنتجــــات"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_Color_SearchViewer; }
		}

		#endregion

		#region Implementation of IColorViewer

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

		public object InternalCode
		{
			get { return txtInternalCode.EditValue; }
			set { txtInternalCode.EditValue = Convert.ToBoolean(value); }
		}

		#endregion
	}
}
