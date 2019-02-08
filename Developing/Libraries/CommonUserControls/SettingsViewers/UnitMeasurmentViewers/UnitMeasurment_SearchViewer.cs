using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.UnitMeasurmentViewers
{
	public partial class UnitMeasurment_SearchViewer : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractSearchViewer<UnitMeasurment_cu>,
		IUnitMeasurmentViewer
	{
		public UnitMeasurment_SearchViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_UnitMeasurment_SearchViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<UnitMeasurment_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.UnitMeasurment_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "وحــــدات القيـــــــاس"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeUnitMeasurmentP, UnitMeasurment_p.ItemsList);
		}

		public override void ClearControls()
		{
			txtNameP.EditValue = null;
			txtNameS.EditValue = null;
			txtInternalCode.EditValue = null;
			txtDescription.EditValue = null;
			chkIsInventoryTracking.Checked = false;
			lkeUnitMeasurmentP.EditValue = null;
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_UnitMeasurment_SearchViewer; }
		}

		#endregion

		#region Implementation of IUnitMeasurmentViewer

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

		public object UnitMeasurment_P_ID
		{
			get { return lkeUnitMeasurmentP.EditValue; }
			set { lkeUnitMeasurmentP.EditValue = value; }
		}

		public object InternalCode
		{
			get { return txtInternalCode.EditValue; }
			set { txtInternalCode.EditValue = value; }
		}

		public object Description
		{
			get { return txtDescription.EditValue; }
			set { txtDescription.EditValue = value; }
		}

		#endregion
	}
}
