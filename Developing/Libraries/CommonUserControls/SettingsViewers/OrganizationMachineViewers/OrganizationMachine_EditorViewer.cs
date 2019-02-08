using System;
using CommonControlLibrary;
using CommonUserControls.SettingsViewers.StationPointStageViewers;
using CommonUserControls.SettingsViewers.StationPointViewers;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.OrganizationMachineViewers
{
	public partial class OrganizationMachine_EditorViewer :
		//UserControl
		CommonAbstractEditorViewer<OrganizationMachine_cu>,
		IOrganizationMachine_Viewer
	{
		private StationPoint_EditorViewer _stationPointEditorViewer;
		private StationPointStage_EditorViewer _stationPointStageEditorViewer;

		public OrganizationMachine_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_OrganizationMachine_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<OrganizationMachine_cu>

		public override object ViewerID
		{
			get { return (int) ViewerName.OrganizationMachine_viewer; }
		}

		public override string HeaderTitle
		{
			get { return ""; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeStationPoints, StationPoint_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeStationPointStages, StationPointStage_cu.ItemsList);
		}

		public override void ClearControls()
		{
			txtNameP.EditValue = null;
			lkeStationPoints.EditValue = null;
			lkeStationPointStages.EditValue = null;
			colorPickEdit1.EditValue = null;
			chkMcSkin.Checked = true;
		}

		#endregion

		#region Implementation of IOrganizationMachine_Viewer

		public object Name_P
		{
			get { return txtNameP.EditValue; }
			set { txtNameP.EditValue = value; }
		}

		public object StationPoint_CU_ID
		{
			get { return lkeStationPoints.EditValue; }
			set { lkeStationPoints.EditValue = value; }
		}

		public object StationPointStage_CU_ID
		{
			get { return lkeStationPointStages.EditValue; }
			set { lkeStationPointStages.EditValue = value; }
		}

		public object OrganizationID { get; set; }

		public object SkinName
		{
			get
			{
				if (chkMcSkin.Checked)
					return "McSkin";
				if(chkOffice2010Black.Checked)
					return "Office 2010 Black";
				if (chkOffice2010Blue.Checked)
					return "Office 2010 Blue";
				if (chkOffice2010Silver.Checked)
					return "Office 2010 Silver";
				return null;
			}
			set
			{
				if (value == "McSkin")
					chkMcSkin.Checked = true;
				else if (value == "Office 2010 Black")
					chkOffice2010Black.Checked = true;
				else if (value == "Office 2010 Blue")
					chkOffice2010Blue.Checked = true;
				else if (value == "Office 2010 Silver")
					chkOffice2010Silver.Checked = true;
			}
		}

		public object Color
		{
			get
			{
				if (colorPickEdit1.EditValue == null || (colorPickEdit1.Color.R.Equals(0) &&
				                                         colorPickEdit1.Color.B.Equals(0) &&
				                                         colorPickEdit1.Color.G.Equals(0)))
					return null;
				return
					colorPickEdit1.Color.R + "," + colorPickEdit1.Color.G + "," + colorPickEdit1.Color.B;
			}
			set { colorPickEdit1.EditValue = value; }
		}

		#endregion

		private void btnStationPoint_Click(object sender, EventArgs e)
		{
			BaseController<Location_cu>.ShowEditorControl(ref _stationPointEditorViewer, this, null, null,
				EditorContainerType.Regular, ViewerName.FloorViewer, DB_CommonTransactionType.CreateNew,
				"أدوار المنظمـــــة", true);
			CommonViewsActions.FillGridlookupEdit(lkeStationPoints, StationPoint_cu.ItemsList);
		}

		private void btnStationPointStage_Click(object sender, EventArgs e)
		{
			BaseController<Location_cu>.ShowEditorControl(ref _stationPointStageEditorViewer, this, null, null,
				EditorContainerType.Regular, ViewerName.FloorViewer, DB_CommonTransactionType.CreateNew,
				"أدوار المنظمـــــة", true);
			CommonViewsActions.FillGridlookupEdit(lkeStationPointStages, StationPointStage_cu.ItemsList);
		}
	}
}
