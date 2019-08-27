using System;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.Color_Viewers
{
	public partial class Color_EditorViewer : 
		//UserControl
		CommonAbstractEditorViewer<Color_cu>,
		IColorViewer
	{
		public Color_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_Color_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<Color_cu>

		public override IMVCController<Color_cu> ViewerCollector { get; set; }

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
			get { return Resources.LocalizedRes.grd_InventoryItem_Area_SearchViewer; }
		}

		public override void FillControls()
		{
		}

		public override void ClearControls()
		{
			txtNameP.EditValue = null;
			txtNameS.EditValue = null;
			txtDescription.EditValue = null;
			txtInternalCode.EditValue = null;
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
