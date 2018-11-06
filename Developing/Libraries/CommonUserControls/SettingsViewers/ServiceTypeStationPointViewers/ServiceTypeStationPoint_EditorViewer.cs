﻿using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.ServiceTypeStationPointViewers
{
	public partial class ServiceTypeStationPoint_EditorViewer : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<ServiceType_StationPoint_cu>,
		IServiceType_StationPointViewer
	{
		public ServiceTypeStationPoint_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_ServiceTypeStationPoint_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<ServiceType_StationPoint_cu>

		public override IMVCController<ServiceType_StationPoint_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.ServiceType_StationPoint_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "ربــط نـوع الخـدمــات بالعيـــادات"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeStationPoint, StationPoint_cu.ItemsList);
		}

		public override void ClearControls()
		{
			lkeStationPoint.EditValue = null;
			chkExamination.Checked = true;
		}

		#endregion

		#region Implementation of IServiceType_StationPointViewer

		public object ServiceType_P_ID { get; set; }

		public object StationPoint_CU_ID
		{
			get { return lkeStationPoint.EditValue; }
			set { lkeStationPoint.EditValue = value; }
		}

		#endregion

		private void chkExamination_CheckedChanged(object sender, System.EventArgs e)
		{
			ServiceType_P_ID = (int)DB_ServiceType.ExaminationService;
		}

		private void chkInvestigation_CheckedChanged(object sender, System.EventArgs e)
		{
			ServiceType_P_ID = (int)DB_ServiceType.InvestigationServices;
		}

		private void chkLab_CheckedChanged(object sender, System.EventArgs e)
		{
			ServiceType_P_ID = (int)DB_ServiceType.LabServices;
		}

		private void chkSurgery_CheckedChanged(object sender, System.EventArgs e)
		{
			ServiceType_P_ID = (int)DB_ServiceType.SurgeryService;
		}
	}
}
