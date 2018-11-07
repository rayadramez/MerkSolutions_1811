﻿using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.ServiceCategoryStationPointViewers
{
	public partial class ServiceCategoryStationPoint_EditorViewer : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<ServiceCategory_StationPoint_cu>,
		IServiceCategory_StationPointViewer
	{
		public ServiceCategoryStationPoint_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1,
				Resources.LocalizedRes.lyt_ServiceCategoryStationPoint_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<ServiceCategory_StationPoint_cu>

		public override IMVCController<ServiceCategory_StationPoint_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.ServiceCategory_StationPoint_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "ربــط تصنيفــــات الخـدمــات بالعيـــادات"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeStationPoint, StationPoint_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeServiceCategory, ServiceCategory_cu.ItemsList);
		}

		public override void ClearControls()
		{
			lkeStationPoint.EditValue = null;
			lkeServiceCategory.EditValue = null;
		}

		#endregion

		#region Implementation of IServiceCategory_StationPointViewer

		public object ServiceCategory_CU_ID
		{
			get { return lkeServiceCategory.EditValue; }
			set { lkeServiceCategory.EditValue = value; }
		}
		
		public object StationPoint_CU_ID
		{
			get { return lkeStationPoint.EditValue; }
			set { lkeStationPoint.EditValue = value; }
		}

		#endregion
	}
}