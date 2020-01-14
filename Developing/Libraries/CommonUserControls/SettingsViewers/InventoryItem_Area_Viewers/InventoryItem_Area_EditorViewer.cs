using System;
using System.Drawing;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InventoryItem_Area_Viewers
{
	public partial class InventoryItem_Area_EditorViewer :
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<InventoryItem_Area>,
		IInventoryItem_Area_Viewer
	{
		public InventoryItem_Area_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_Inventoryitem_Area_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<InventoryItem_Area>

		public override IMVCController<InventoryItem_Area> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.InventoryItem_Area_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "المنتجــــات"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeInventoryItems, InventoryItem_cu.ItemsList);
		}

		public override void ClearControls()
		{
			spnArea.EditValue = 0;
			spnCount.EditValue = 1;
			spnHeight.EditValue = 0;
			spnWidth.EditValue = 0;
			spnTotalArea.EditValue = 0;
			lkeInventoryItems.EditValue = null;
			txtInternalCode.EditValue = null;
		}

		#endregion

		#region Implementation of IInventoryItem_Area_Viewer

		public object InventoryItemID
		{
			get { return lkeInventoryItems.EditValue; }
			set { lkeInventoryItems.EditValue = value; }
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

		public object InternalCode
		{
			get { return txtInternalCode.EditValue; }
			set { txtInternalCode.EditValue = value; }
		}

		public object SizeUnitMeasure_P_ID
		{
			get
			{
				if (chkCM.Checked)
					return (int) DB_SizeUnitMeasure.CM;
				if(chkMM.Checked)
					return (int)DB_SizeUnitMeasure.MM;
				return (int)DB_SizeUnitMeasure.None;
			}
			set
			{
				switch ((DB_SizeUnitMeasure)value)
				{
					case DB_SizeUnitMeasure.CM:
						chkCM.Checked = true;
						break;
					case DB_SizeUnitMeasure.MM:
						chkMM.Checked = true;
						break;
				}
			}
		}

		#endregion

		private void spnWidth_EditValueChanged(object sender, System.EventArgs e)
		{
			spnArea.EditValue =
				CommonActions.CommonActions.GetShapeArea(Convert.ToDouble(Width), Convert.ToDouble(Height));
			spnTotalArea.EditValue = CommonActions.CommonActions.GetShapeArea(Convert.ToDouble(Width),
				Convert.ToDouble(Height), Convert.ToInt32(Count));
		}

		private void spnHeight_EditValueChanged(object sender, EventArgs e)
		{
			spnArea.EditValue =
				CommonActions.CommonActions.GetShapeArea(Convert.ToDouble(Width), Convert.ToDouble(Height));
			spnTotalArea.EditValue = CommonActions.CommonActions.GetShapeArea(Convert.ToDouble(Width),
				Convert.ToDouble(Height), Convert.ToInt32(Count));
		}

		private void spnCount_EditValueChanged(object sender, EventArgs e)
		{
			spnArea.EditValue =
				CommonActions.CommonActions.GetShapeArea(Convert.ToDouble(Width), Convert.ToDouble(Height));
			spnTotalArea.EditValue = CommonActions.CommonActions.GetShapeArea(Convert.ToDouble(Width),
				Convert.ToDouble(Height), Convert.ToInt32(Count));
		}

		private void lkeInventoryItems_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeInventoryItems.EditValue == null)
				return;

			spnInventoryItemTotalArea.EditValue =
				InventoryBusinessLogicEngine.GetInventoryItemTotalAreaParts(Convert.ToInt32(lkeInventoryItems.EditValue));
		}
	}
}
