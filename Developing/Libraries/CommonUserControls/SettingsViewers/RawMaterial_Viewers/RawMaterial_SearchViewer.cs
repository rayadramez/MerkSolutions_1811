using System;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.RawMaterial_Viewers
{
	public partial class RawMaterial_SearchViewer : 
		//UserControl
		CommonAbstractSearchViewer<RawMaterials_cu>,
		IRawMaterial_Viewer
	{
		public RawMaterial_SearchViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_RawMaterial_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<RawMaterials_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.RawMaterial_viewer; }
		}

		public override string HeaderTitle
		{
			get { return "المنتجــــات"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_RawMaterial_SearchViewer; }
		}

		#endregion

		#region Implementation of IRawMaterial_Viewer

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

		public object RawTypeID
		{
			get { return lkeInventoryItemType.EditValue; }
			set { lkeInventoryItemType.EditValue = value; }
		}

		public object Thickness
		{
			get { return spnThickness.EditValue; }
			set { spnThickness.EditValue = value; }
		}

		public object Width
		{
			get { return spnWidth.EditValue; }
			set { spnWidth.EditValue = value; }
		}

		public object Height
		{
			get { return spnHeight.EditValue; }
			set { spnHeight.EditValue = value; }
		}

		public object Weight
		{
			get { return spnWeight.EditValue; }
			set { spnWeight.EditValue = value; }
		}

		public object ExpirationDate
		{
			get { return dtExpirationDate.EditValue; }
			set { dtExpirationDate.EditValue = value; }
		}

		public object Description
		{
			get { return txtDescription.EditValue; }
			set { txtDescription.EditValue = value; }
		}

		public object IsCountable
		{
			get { return chkIsCountable.Checked; }
			set { chkIsCountable.Checked = Convert.ToBoolean(value); }
		}

		public object IsStockAvailable
		{
			get { return chkIsAvailable.Checked; }
			set { chkIsAvailable.Checked = Convert.ToBoolean(value); }
		}

		public object InternalCode
		{
			get { return txtInternalCode.EditValue; }
			set { txtInternalCode.EditValue = Convert.ToBoolean(value); }
		}

		public object ColorID
		{
			get { return lkeColor.EditValue; }
			set { lkeColor.EditValue = Convert.ToBoolean(value); }
		}

		public object DividedTypeID { get; set; }

		#endregion
	}
}
