using System.Drawing;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.ReportsContainer
{
	public partial class GetRawMaterialCostPrices_Report : 
		//UserControl
		CommonAbstractSearchViewer<GetRawMaterialCostPrices_Result>,
		IGetRawMaterialCostPrices_Report_Viewer
	{
		public GetRawMaterialCostPrices_Report()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1,
				Resources.LocalizedRes.lyt_GetRawMaterialCostPrices_Viewer);
			CommonViewsActions.SetupSyle(this);

			if (!string.IsNullOrEmpty(ApplicationStaticConfiguration.SkinName) &&
				!string.IsNullOrWhiteSpace(ApplicationStaticConfiguration.SkinName))
				if (!ApplicationStaticConfiguration.SkinName.Equals("McSkin"))
					layoutControlGroup2.AppearanceGroup.ForeColor = Color.OldLace;
				else
					layoutControlGroup2.AppearanceGroup.ForeColor = Color.Navy;
		}

		#region Overrides of CommonAbstractViewer<GetRawMaterialCostPrices_Result>

		public override object ViewerID
		{
			get { return (int)ViewerName.GetRawMaterialCostPrices_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return string.Empty; }
		}

		public override string GridXML
		{
			get
			{
				return Resources.LocalizedRes.grd_GetRawMaterialCostPrices;
			}
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeUsers, User_cu.ItemsList, "FullName", "Person_CU_ID");
			CommonViewsActions.FillGridlookupEdit(lkeRawMaterials, RawMaterials_cu.ItemsList, "RawMaterialFullName");
		}

		public override void ClearControls()
		{
			lkeRawMaterials.EditValue = null;
			dtDateFrom.EditValue = null;
			dtDateTo.EditValue = null;
			lkeUsers.EditValue = null;
		}

		#endregion

		#region Implementation of IGetRawMaterialCostPrices_Report_Viewer

		public object RawMaterialID
		{
			get { return lkeRawMaterials.EditValue; }
		}

		public object FromDate
		{
			get { return dtDateFrom.EditValue; }
		}

		public object ToDate
		{
			get { return dtDateTo.EditValue; }
		}

		#endregion
	}
}
