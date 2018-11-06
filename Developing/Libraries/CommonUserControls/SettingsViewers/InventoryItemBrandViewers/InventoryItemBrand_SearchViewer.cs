using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InventoryItemBrandViewers
{
	public partial class InventoryItemBrand_SearchViewer : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractSearchViewer<InventoryItemBrand_cu>,
		IInventoryItemBrandViewer
	{
		public InventoryItemBrand_SearchViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_InventoryItemBrand_SearchViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<InventoryItemBrand_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.InventoryItemBrand_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "العـلامــــات التجـاريـــــة"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_InventoryItemBrand_SearchViewer; }
		}

		#endregion

		#region Implementation of IInventoryItemBrandViewer

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

		public object Description
		{
			get { return txtDescription.EditValue; }
			set { txtDescription.EditValue = value; }
		}

		public object InternalCode
		{
			get { return txtInternalCode.EditValue; }
			set { txtInternalCode.EditValue = value; }
		}

		#endregion
	}
}
