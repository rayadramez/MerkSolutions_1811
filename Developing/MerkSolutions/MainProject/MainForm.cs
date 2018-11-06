using CommonControlLibrary;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Helpers;
using MerkDataBaseBusinessLogicProject;


namespace MainProject
{
	public partial class MainForm : XtraForm
	{
		public MainForm()
		{
			InitializeComponent();
			InitSkinGallery();

		}

		private void InitSkinGallery()
		{
			SkinHelper.InitSkinGallery(rgbiSkins, true);
		}

		private void btnChartOfAccountType_Search_ItemClick(object sender, ItemClickEventArgs e)
		{
			//CommonViewBaseContainerController<ChartOfAccountType_cu, ChartOfAccountsTypeDataCollector>.ShowControl(
			//	ref _fromTest, ref _chartOfAccountTypeSearchViewer, this, ViewerName.ChartOfAccountsTypeViewer, "عنوان جيد جدا",
			//	CommonViewerType.SearchVewier);
			//mainRibbon.Minimized = true;
		}

		private void btnChartOfAccountType_New_ItemClick(object sender, ItemClickEventArgs e)
		{
			//CommonViewBaseContainerController<ChartOfAccountType_cu, ChartOfAccountsTypeDataCollector>.ShowControl(
			//	ref _fromTest, ref _chartOfAccountTypeSearchViewer, this, ViewerName.ChartOfAccountsTypeViewer, "عنوان جيد جدا",
			//	CommonViewerType.EditorViewer);
			//mainRibbon.Minimized = true;
		}

		private void btnChartOfAccountMargin_Edit_ItemClick(object sender, ItemClickEventArgs e)
		{
			
		}

		private void btnChartOfAccountMargin_Search_ItemClick(object sender, ItemClickEventArgs e)
		{
			
		}

		private void btnEmployee_New_ItemClick(object sender, ItemClickEventArgs e)
		{
			//CommonViewBaseContainerController<Person_cu, PersonDataCollector>.ShowControl(
			//	ref _personEditorViewer, ref _personSearchViewer, this,
			//	ViewerName.EmployeeViewer, "الموظفين", MVCViewerType.EditorViewer);
			//mainRibbon.Minimized = true;
		}

		private void btnEmployee_Search_ItemClick(object sender, ItemClickEventArgs e)
		{

		}

	}
}