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
	public partial class GetInventoryItemAreaParts_Report : 
		//UserControl
		CommonAbstractSearchViewer<GetInventoryItemAreaParts_Result>,
		IGetInventoryItemAreaParts_Viewer
	{
		public GetInventoryItemAreaParts_Report()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1,
				Resources.LocalizedRes.lyt_GetInventoryItemAreaParts);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<GetInventoryItemAreaParts_Result>

		public override object ViewerID
		{
			get { return (int)ViewerName.GetInventoryItemAreaParts_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return string.Empty; }
		}

		public override string GridXML
		{
			get
			{
				return Resources.LocalizedRes.grd_GetInventoryItemAreaParts;
			}
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeInventoryItem, InventoryItem_cu.ItemsList);
		}

		public override void ClearControls()
		{
			lkeInventoryItem.EditValue = null;
		}

		#endregion

		#region Implementation of IGetInventoryItemAreaParts_Viewer

		public object InventoryItemID
		{
			get { return lkeInventoryItem.EditValue; }
		}

		#endregion
	}
}
