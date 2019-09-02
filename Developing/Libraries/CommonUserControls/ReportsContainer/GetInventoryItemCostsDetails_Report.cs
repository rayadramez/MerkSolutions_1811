using System.Drawing;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.ReportsContainer
{
	public partial class GetInventoryItemCostsDetails_Report : 
		//UserControl
		CommonAbstractSearchViewer<GetInventoryItemCostsDetails_Result>,
		IGetInventoryItemCostsDetails_Report_Viewer
	{
		public GetInventoryItemCostsDetails_Report()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1,
				Resources.LocalizedRes.lyt_GetInventoryItemCostsDetails_Report);
			CommonViewsActions.SetupSyle(this);

			if (!string.IsNullOrEmpty(ApplicationStaticConfiguration.SkinName) &&
				!string.IsNullOrWhiteSpace(ApplicationStaticConfiguration.SkinName))
				if (!ApplicationStaticConfiguration.SkinName.Equals("McSkin"))
					layoutControlGroup2.AppearanceGroup.ForeColor = Color.OldLace;
				else
					layoutControlGroup2.AppearanceGroup.ForeColor = Color.Navy;
		}

		#region Overrides of CommonAbstractViewer<GetInventoryItemCostsDetails_Result>

		public override object ViewerID
		{
			get { return (int)ViewerName.GetInventoryItemCostsDetails_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return string.Empty; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_GetInventoryItemCostsDetails_Search; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeInventoryItems, InventoryItem_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeColors, Color_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeRawMaterials, RawMaterials_cu.ItemsList, "RawMaterialFullName");

			//lkeRawMaterials.EditValue = null;
			//txtRawInternalCode.EditValue = null;
			//spnRawThickness.EditValue = null;
			//lkeColors.EditValue = null;
			//txtColorInternalCode.EditValue = null;
			//lkeInventoryItems.EditValue = null;
			//txtInventoryItemInternalCode.EditValue = null;
			//spnAdditionalCostAmount.EditValue = null;
		}

		public override void ClearControls()
		{
			lkeRawMaterials.EditValue = null;
			txtRawInternalCode.EditValue = null;
			spnRawThickness.EditValue = null;
			lkeColors.EditValue = null;
			txtColorInternalCode.EditValue = null;
			lkeInventoryItems.EditValue = null;
			txtInventoryItemInternalCode.EditValue = null;
			spnAdditionalCostAmount.EditValue = null;
		}

		#endregion

		#region Implementation of IGetInventoryItemCostsDetails_Report_Viewer

		public object RawMaterialID
		{
			get { return lkeRawMaterials.EditValue; }
		}

		public object ColorID
		{
			get { return lkeColors.EditValue; }
		}

		public object ItemID
		{
			get { return lkeInventoryItems.EditValue; }
		}

		public object AdditionalCostToBeAdded
		{
			get { return spnAdditionalCostAmount.EditValue; }
		}

		#endregion
	}
}
