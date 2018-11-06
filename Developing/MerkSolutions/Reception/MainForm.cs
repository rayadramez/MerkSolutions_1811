using System.Drawing;
using CommonUserControls.SettingsViewers.FloorViewers;
using CommonUserControls.SettingsViewers.InPatientRoomBedViewers;
using CommonUserControls.SettingsViewers.InPatientRoomClassificationViewres;
using CommonUserControls.SettingsViewers.InPatientRoomViewers;
using DevExpress.LookAndFeel;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;

namespace Settings
{
	public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
	{
		private Floor_EditorViewer _floorEditorViewer;
		private Floor_SearchViewer _floorSearchViewer;

		private InPatientRoomSearchViewer_UC _inPatientRoomSearch;
		private InPatientRoomEditorViewer_UC _inPatientRoomEditor;

		private InPatientRoomBedEditorViewer_UC _inPatientRoomBedEditorViewer;
		private InPatientRoomBedSearchViewer_UC _inPatientRoomBedSearchViewer;

		private InPatientRoomClassificationEditorViewer_UC _inPatientRoomClassificationEditorViewer;
		private InPatientRoomClassificationSearchViewer_UC _inPatientRoomClassificationSearchViewer;

		public MainForm()
		{
			InitializeComponent();

			UserLookAndFeel.Default.SetSkinStyle("Office 2010 Black");
			UserLookAndFeel.Default.SkinMaskColor = Color.FromArgb(0, 59, 74);
		}

		private void btnFloor_Search_Click(object sender, System.EventArgs e)
		{
			BaseController<Floor_cu>.ShowControl(ref _floorEditorViewer, ref _floorSearchViewer, splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.FloorViewer, DB_CommonTransactionType.CreateNew, "الأدوار", AbstractViewerType.SearchViewer,
				false);
		}

		private void btnFloor_New_Click(object sender, System.EventArgs e)
		{
			BaseController<Floor_cu>.ShowControl(ref _floorEditorViewer, ref _floorSearchViewer, this,
				EditorContainerType.Settings,
				ViewerName.FloorViewer, DB_CommonTransactionType.CreateNew, "الأدوار", AbstractViewerType.EditorViewer,
				true);
		}

		private void btnInPatientRoom_Search_Click(object sender, System.EventArgs e)
		{
			BaseController<InPatientRoom_cu>.ShowControl(ref _inPatientRoomEditor, ref _inPatientRoomSearch,
				splitContainerControl1.Panel1, EditorContainerType.Settings,
				ViewerName.InPatientRoomViewer, DB_CommonTransactionType.CreateNew, "غرف الإقامة", AbstractViewerType.SearchViewer,
				false);
		}

		private void btnInPatientRoom_Edit_Click(object sender, System.EventArgs e)
		{
			BaseController<InPatientRoom_cu>.ShowControl(ref _inPatientRoomEditor, ref _inPatientRoomSearch, this,
				EditorContainerType.Settings,
				ViewerName.InPatientRoomViewer, DB_CommonTransactionType.CreateNew, "غرف الإقامة", AbstractViewerType.EditorViewer,
				true);
			
		}

		private void btnInPatientRoomBed_Search_Click(object sender, System.EventArgs e)
		{
			BaseController<InPatientRoomBed_cu>.ShowControl(ref _inPatientRoomBedEditorViewer, ref _inPatientRoomBedSearchViewer,
				splitContainerControl1.Panel1, EditorContainerType.Settings, ViewerName.InPatientRoomBedViewer,
				DB_CommonTransactionType.CreateNew, "الآسرة",
				AbstractViewerType.SearchViewer, false);
			
		}

		private void btnInPatientRoomBed_Edit_Click(object sender, System.EventArgs e)
		{
			BaseController<InPatientRoomBed_cu>.ShowControl(ref _inPatientRoomBedEditorViewer, ref _inPatientRoomBedSearchViewer,
				this, EditorContainerType.Settings, ViewerName.InPatientRoomBedViewer, DB_CommonTransactionType.CreateNew, "الآسرة",
				AbstractViewerType.EditorViewer, true);
			
		}

		private void btnInPatientRoomClassificationSearch_Click(object sender, System.EventArgs e)
		{
			BaseController<InPatientRoomClassification_cu>.ShowControl(ref _inPatientRoomClassificationEditorViewer,
				ref _inPatientRoomClassificationSearchViewer,
				splitContainerControl1.Panel1, EditorContainerType.Settings, ViewerName.InPatientRoomClassificationViewer,
				DB_CommonTransactionType.CreateNew,
				"تصنيفات الغرف",
				AbstractViewerType.SearchViewer, false);
			
		}

		private void btnInPatientRoomClassificationEditor_Click(object sender, System.EventArgs e)
		{
			BaseController<InPatientRoomClassification_cu>.ShowControl(ref _inPatientRoomClassificationEditorViewer,
				ref _inPatientRoomClassificationSearchViewer,
				this, EditorContainerType.Settings, ViewerName.InPatientRoomClassificationViewer, DB_CommonTransactionType.CreateNew,
				"تصنيفات الغرف",
				AbstractViewerType.EditorViewer, true);
			
		}
	}
}