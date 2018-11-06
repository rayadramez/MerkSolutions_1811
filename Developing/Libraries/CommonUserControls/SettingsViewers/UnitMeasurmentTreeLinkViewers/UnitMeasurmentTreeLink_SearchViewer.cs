using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.UnitMeasurmentTreeLinkViewers
{
	public partial class UnitMeasurmentTreeLink_SearchViewer : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractSearchViewer<UnitMeasurmentTreeLink_cu>,
		IUnitMeasurmentTreeLinkViewer
	{
		public UnitMeasurmentTreeLink_SearchViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_UnitMeasurmentTreeLink_SearchViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<UnitMeasurmentTreeLink_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.UnitMeasurmentTreeLink_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "شجــــرة وحــــدات القيـــــــاس"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeParentUnitMeasurment, UnitMeasurment_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeChildUnitMeasurment, UnitMeasurment_cu.ItemsList);
			spnEncapsulatedUnits.EditValue = 1;
		}

		public override void ClearControls()
		{
			lkeChildUnitMeasurment.EditValue = null;
			lkeParentUnitMeasurment.EditValue = null;
			spnEncapsulatedUnits.EditValue = 1;
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_UnitMeasurmentTreeLink_SearchViewer; }
		}

		#endregion

		#region Implementation of IUnitMeasurmentTreeLinkViewer

		public object ParentUnitMeasurment_CU_ID
		{
			get { return lkeParentUnitMeasurment.EditValue; }
			set { lkeParentUnitMeasurment.EditValue = value; }
		}

		public object ChildUnitMeasurment_CU_ID
		{
			get { return lkeChildUnitMeasurment.EditValue; }
			set { lkeChildUnitMeasurment.EditValue = value; }
		}

		public object EncapsulatedChildQantity
		{
			get { return spnEncapsulatedUnits.EditValue; }
			set { spnEncapsulatedUnits.EditValue = value; }
		}

		#endregion
	}
}
