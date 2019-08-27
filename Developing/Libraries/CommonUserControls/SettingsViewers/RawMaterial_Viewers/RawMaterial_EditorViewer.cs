using System;
using System.Windows.Forms;
using CommonControlLibrary;
using CommonUserControls.SettingsViewers.Color_Viewers;
using DevExpress.XtraLayout.Utils;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.RawMaterial_Viewers
{
	public partial class RawMaterial_EditorViewer : 
		//UserControl
		CommonAbstractEditorViewer<RawMaterials_cu>,
		IRawMaterial_Viewer
	{
		private Color_EditorViewer _colorEditor;

		public RawMaterial_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_RawMaterial_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<RawMaterials_cu>

		public override IMVCController<RawMaterials_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.RawMaterial_viewer; }
		}

		public override string HeaderTitle
		{
			get { return "المنتجــــات"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeInventoryItemType, RawMaterialType_p.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeColor, Color_cu.ItemsList);
		}

		public override void ClearControls()
		{
			txtNameP.EditValue = null;
			txtNameS.EditValue = null;
			lkeInventoryItemType.EditValue = null;
			spnWidth.EditValue = null;
			spnHeight.EditValue = null;
			spnWeight.EditValue = null;
			spnThickness.EditValue = null;
			dtExpirationDate.EditValue = DateTime.Now;
			chkIsAvailable.Checked = false;
			chkIsCountable.Checked = false;
			txtDescription.EditValue = null;
			txtInternalCode.EditValue = null;
			ColorID = null;
			chkNotDivided.Checked = false;
			chkNotDivided.Checked = true;
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
			set { txtInternalCode.EditValue = value; }
		}

		public object ColorID
		{
			get { return lkeColor.EditValue; }
			set { lkeColor.EditValue = value; }
		}

		public object DividedTypeID
		{
			get
			{
				if (chkNotDivided.Checked)
					return (int) DB_DividedByType.NotDivided;
				if (chkDividedByQuarter.Checked)
					return (int)DB_DividedByType.DividedBy4;
				if (chkDividedByOther.Checked)
					return (int)DB_DividedByType.DividedBy6;
				return (int) DB_DividedByType.None;
			}
			set {
				switch ((DB_DividedByType)value)
				{
					case DB_DividedByType.NotDivided:
						chkNotDivided.Checked = true;
						break;
					case DB_DividedByType.DividedBy4:
						chkDividedByQuarter.Checked = true;
						break;
					case DB_DividedByType.DividedBy6:
						chkDividedByOther.Checked = true;
						break;
				}
			}
		}

		#endregion

		private void btnAddColor_Click(object sender, EventArgs e)
		{
			BaseController<Color_cu>.ShowEditorControl(ref _colorEditor, this, null, null,
				EditorContainerType.Regular, ViewerName.Color_Viewer, DB_CommonTransactionType.CreateNew,
				"ألــــوان الأخشـــــاب", true);
			CommonViewsActions.FillGridlookupEdit(lkeColor, Color_cu.ItemsList);
		}

		private void chkIsDivided_CheckedChanged(object sender, EventArgs e)
		{

		}
	}
}
