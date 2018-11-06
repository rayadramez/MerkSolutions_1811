using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InventoryItemCategoryViewers
{
	public partial class InventoryItemCategory_SearchViewer : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractSearchViewer<InventoryItemCategory_cu>,
		IInventoryItemCategoryViewer
	{
		public InventoryItemCategory_SearchViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_InventoryItemCategory_SearchViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<InventoryItemCategory_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.InventoryItemCategory_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "تصنيفــــات المنتجـــات"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_InventoryItemCategory_SearchViewer; }
		}

		#endregion

		#region Implementation of IInventoryItemCategoryViewer

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
