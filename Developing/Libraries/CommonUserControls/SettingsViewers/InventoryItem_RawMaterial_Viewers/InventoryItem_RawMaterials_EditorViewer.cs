using System;
using System.Windows.Forms;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InventoryItem_RawMaterial_Viewers
{
	public partial class InventoryItem_RawMaterials_EditorViewer : 
		//UserControl
		CommonAbstractEditorViewer<InventoryItem_RawMaterial_cu>,
		IInventoryItem_RawMaterial_Viewer
	{
		public InventoryItem_RawMaterials_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_InventoryItem_RawMaterials_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		private void chkUseDimensions_CheckedChanged(object sender, System.EventArgs e)
		{
			spnWidth.Properties.ReadOnly = spnHeight.Properties.ReadOnly = !chkUseDimensions.Checked;
			if (!chkUseDimensions.Checked)
				spnHeight.EditValue = spnWidth.EditValue = null;
		}

		private void lkeRawMaterials_EditValueChanged(object sender, System.EventArgs e)
		{
			if (lkeRawMaterials.EditValue == null)
				return;

			RawMaterials_cu rawMaterial = RawMaterials_cu.ItemsList.Find(item =>
				Convert.ToInt32(item.ID).Equals(Convert.ToInt32(lkeRawMaterials.EditValue)));
			if(rawMaterial == null)
				return;

			spnRawThickness.EditValue = rawMaterial.Thickness;
		}

		#region Overrides of CommonAbstractViewer<InventoryItem_RawMaterial_cu>

		public override IMVCController<InventoryItem_RawMaterial_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.InventoryItem_RawMaterial_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "المنتجــــات"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeInventoryItems, InventoryItem_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeRawMaterials, RawMaterials_cu.ItemsList);
		}

		public override void ClearControls()
		{
			lkeInventoryItems.EditValue = null;
			lkeRawMaterials.EditValue = null;
			spnWidth.EditValue = 0;
			spnHeight.EditValue = 0;
			spnRawThickness.EditValue = null;
			spnCount.EditValue = 0;
			chkUseDimensions.Checked = true;
		}

		#endregion

		#region Implementation of IInventoryItem_RawMaterial_Viewer

		public object InventoryItemID
		{
			get { return lkeInventoryItems.EditValue; }
			set { lkeInventoryItems.EditValue = value; }
		}

		public object RawMaterialID
		{
			get { return lkeRawMaterials.EditValue; }
			set { lkeRawMaterials.EditValue = value; }
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

		public object Count
		{
			get { return spnCount.EditValue; }
			set { spnCount.EditValue = value; }
		}

		public object HasDimensions
		{
			get { return chkUseDimensions.Checked; }
			set { chkUseDimensions.Checked = Convert.ToBoolean(value); }
		}

		#endregion
	}
}
