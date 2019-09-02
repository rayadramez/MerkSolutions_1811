using System;
using System.Windows.Forms;
using CommonControlLibrary;
using CommonUserControls.SettingsViewers.RawMaterial_Viewers;
using DevExpress.XtraLayout.Utils;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;
using DB_RawMaterialTransactionType = MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary.DB_RawMaterialTransactionType;

namespace CommonUserControls.SettingsViewers.RawMaterialTransaction
{
	public partial class RawMaterialTransaction_EditorViewer : 
		//UserControl
		CommonAbstractEditorViewer<RawMaterialTranasction>,
		IRawMaterialTransaction_Viewer
	{
		private RawMaterial_EditorViewer _rawMaterialEditor;

		public RawMaterialTransaction_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1,
				Resources.LocalizedRes.lyt_RawMaterialTransaction_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		private void chkConsuming_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void chkPurchasing_CheckedChanged(object sender, EventArgs e)
		{
			lytWidth.Visibility = lytHeight.Visibility =
				chkConsuming.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		private void chkSelling_CheckedChanged(object sender, EventArgs e)
		{
			lytWidth.Visibility = lytHeight.Visibility =
				chkConsuming.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		#region Overrides of CommonAbstractViewer<RawMaterialTranasction>

		public override IMVCController<RawMaterialTranasction> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.RawMaterialTransactions_viewer; }
		}

		public override string HeaderTitle
		{
			get { return "المنتجــــات"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeRawMaterial, RawMaterials_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeColorID, Color_cu.ItemsList);
			dtDate.EditValue = DateTime.Now;
		}

		public override void ClearControls()
		{
			lkeRawMaterial.EditValue = null;
			spnHeight.EditValue = null;
			spnCount.EditValue = null;
			spnPrice.EditValue = null;
			spnWidth.EditValue = null;
			lkeColorID.EditValue = null;
			dtDate.EditValue = DateTime.Now;
			chkPurchasing.Checked = true;
		}

		#endregion

		#region Implementation of IRawMaterialTransaction_Viewer

		public object RawMaterialID
		{
			get { return lkeRawMaterial.EditValue; }
			set { lkeRawMaterial.EditValue = value; }
		}

		public object RawTransactionTypeID
		{
			get
			{
				if (chkConsuming.Checked)
					return (int) DB_RawMaterialTransactionType.Consuming;
				if (chkPurchasing.Checked)
					return (int) DB_RawMaterialTransactionType.Purchasing;
				if (chkSelling.Checked)
					return (int) DB_RawMaterialTransactionType.Selling;
				return -1;
			}
			set {
				switch ((DB_RawMaterialTransactionType)value)
				{
					case DB_RawMaterialTransactionType.Consuming:
						chkConsuming.Checked = true;
						break;
					case DB_RawMaterialTransactionType.Purchasing:
						chkPurchasing.Checked = true;
						break;
					case DB_RawMaterialTransactionType.Selling:
						chkSelling.Checked = true;
						break;
				}
			}
		}

		public object ColorID
		{
			get { return lkeColorID.EditValue; }
			set { lkeColorID.EditValue = value; }
		}

		public object Count
		{
			get { return spnCount.EditValue; }
			set { spnCount.EditValue = value; }
		}

		public object PuchasingPrice
		{
			get { return spnPrice.EditValue; }
			set { spnPrice.EditValue = value; }
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

		public object TransactionDate
		{
			get { return dtDate.EditValue; }
			set { dtDate.EditValue = value; }
		}

		public object DividedTypeID
		{
			get
			{
				if (chkNotDivided.Checked)
					return (int)DB_DividedByType.NotDivided;
				if (chkDividedByQuarter.Checked)
					return (int)DB_DividedByType.DividedBy4;
				if (chkDividedByOther.Checked)
					return (int)DB_DividedByType.DividedBy6;
				return (int)DB_DividedByType.None;
			}
			set
			{
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

		private void btnAddRawMaterial_Click(object sender, EventArgs e)
		{
			BaseController<Color_cu>.ShowEditorControl(ref _rawMaterialEditor, this, null, null,
				EditorContainerType.Regular, ViewerName.RawMaterial_viewer, DB_CommonTransactionType.CreateNew,
				"تسجيــــل المـــواد الخــــام", true);
			CommonViewsActions.FillGridlookupEdit(lkeRawMaterial, RawMaterials_cu.ItemsList, "RawMaterialFullName");
		}

		private void lkeRawMaterial_EditValueChanged(object sender, EventArgs e)
		{
			if(RawMaterialID == null)
				return;

			RawMaterials_cu rawMaterial = RawMaterials_cu.ItemsList.Find(item =>
				Convert.ToInt32(item.ID).Equals(Convert.ToInt32(RawMaterialID)));
			if (rawMaterial == null)
				return;

			switch ((DB_DividedByType)rawMaterial.DividedByType_P_ID)
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

			Width = rawMaterial.Width;
			Height = rawMaterial.Height;
		}
	}
}
