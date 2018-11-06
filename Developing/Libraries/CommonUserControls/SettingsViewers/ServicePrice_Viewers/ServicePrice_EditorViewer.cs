using System;
using System.Collections.Generic;
using CommonControlLibrary;
using DevExpress.XtraLayout.Utils;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.ServicePrice_Viewers
{
	public partial class ServicePrice_EditorViewer : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<ServicePrice_cu>,
		IServicePrice_EditorViewer
	{
		public ServicePrice_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_ServicePrice_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<ServicePrice_cu>

		public override IMVCController<ServicePrice_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int) ViewerName.ServicePrice_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "أسعـــــار الخــدمـــــات"; }
		}

		public override void FillControls()
		{
			spnServicePrice.EditValue = null;
			spnInsurancePrice.EditValue = null;
			CommonViewsActions.FillGridlookupEdit(lkeServiceCategory, ServiceCategory_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeService, Service_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeDoctor, Doctor_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeDoctorCategory, DoctorCategory_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeInsuranceCarrier, InsuranceCarrier_cu.ItemsList);
		}

		#endregion

		#region Implementation of IServicePrice_EditorViewer

		public object Service_CU_ID
		{
			get { return lkeService.EditValue; }
			set { lkeService.EditValue = value; }
		}

		public object ServiceCategory_CU_ID
		{
			get { return lkeServiceCategory.EditValue; }
			set { lkeServiceCategory.EditValue = value; }
		}

		public object Doctor_CU_ID
		{
			get { return lkeDoctor.EditValue; }
			set { lkeDoctor.EditValue = value; }
		}

		public object DoctorCategory_CU_ID
		{
			get { return lkeDoctorCategory.EditValue; }
			set { lkeDoctorCategory.EditValue = value; }
		}

		public object Price
		{
			get { return spnServicePrice.EditValue; }
			set { spnServicePrice.EditValue = value; }
		}

		public object InsuranceCarrierID
		{
			get { return lkeInsuranceCarrier.EditValue; }
			set { lkeInsuranceCarrier.EditValue = value; }
		}

		public object InsuranceLevelID
		{
			get { return lkeInsuranceLevel.EditValue; }
			set { lkeInsuranceLevel.EditValue = value; }
		}

		public object InsurancePrice
		{
			get { return spnInsurancePrice.EditValue; }
			set { spnInsurancePrice.EditValue = value; }
		}

		#endregion

		private void chkDoctorCategory_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chkDoctorCategory.Checked)
			{
				lytDoctorCategory.Visibility = LayoutVisibility.Always;
				lytDoctor.Visibility = LayoutVisibility.Never;
				lytServiceCategory.Visibility = LayoutVisibility.Never;
				lytService.Visibility = LayoutVisibility.Never;

				lkeDoctor.EditValue = null;
				lkeServiceCategory.EditValue = null;
				lkeService.EditValue = null;
			}
			else
				lkeDoctorCategory.EditValue = null;
		}

		private void chkDoctor_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chkDoctor.Checked)
			{
				lytDoctorCategory.Visibility = LayoutVisibility.Never;
				lytDoctor.Visibility = LayoutVisibility.Always;
				lytServiceCategory.Visibility = LayoutVisibility.Never;
				lytService.Visibility = LayoutVisibility.Never;

				lkeDoctorCategory.EditValue = null;
				lkeServiceCategory.EditValue = null;
				lkeService.EditValue = null;
			}
			else
				lkeDoctor.EditValue = null;
		}

		private void chkServiceCategory_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chkServiceCategory.Checked)
			{
				lytDoctorCategory.Visibility = LayoutVisibility.Never;
				lytDoctor.Visibility = LayoutVisibility.Never;
				lytServiceCategory.Visibility = LayoutVisibility.Always;
				lytService.Visibility = LayoutVisibility.Never;

				lkeDoctorCategory.EditValue = null;
				lkeDoctor.EditValue = null;
				lkeService.EditValue = null;
			}
			else
				lkeServiceCategory.EditValue = null;
		}

		private void chkService_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chkService.Checked)
			{
				lytDoctorCategory.Visibility = LayoutVisibility.Never;
				lytDoctor.Visibility = LayoutVisibility.Never;
				lytServiceCategory.Visibility = LayoutVisibility.Never;
				lytService.Visibility = LayoutVisibility.Always;

				lkeDoctorCategory.EditValue = null;
				lkeDoctor.EditValue = null;
				lkeServiceCategory.EditValue = null;
			}
			else
				lkeService.EditValue = null;
		}

		private void lkeInsuranceCarrier_EditValueChanged(object sender, System.EventArgs e)
		{
			if (lkeInsuranceCarrier.EditValue == null)
			{
				lkeInsuranceLevel.Properties.ReadOnly = true;
				spnInsurancePrice.Properties.ReadOnly = true;
				return;
			}

			InsuranceCarrier_cu carrier =
				InsuranceCarrier_cu.ItemsList.Find(
					item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(lkeInsuranceCarrier.EditValue)));
			if (carrier == null)
			{
				lkeInsuranceLevel.Properties.ReadOnly = true;
				spnInsurancePrice.Properties.ReadOnly = true;
				return;
			}

			List<InsuranceCarrier_InsuranceLevel_cu> bridgeList =
				InsuranceCarrier_InsuranceLevel_cu.ItemsList.FindAll(
					item => Convert.ToInt32(item.InsuranceCarrier_CU_ID).Equals(Convert.ToInt32(carrier.ID)));
			if (bridgeList.Count == 0)
			{
				lkeInsuranceLevel.Properties.ReadOnly = true;
				spnInsurancePrice.Properties.ReadOnly = true;
				return;
			}

			List<InsuranceLevel_cu> levelsList = new List<InsuranceLevel_cu>();
			foreach (InsuranceCarrier_InsuranceLevel_cu bridge in bridgeList)
			{
				InsuranceLevel_cu level =
					InsuranceLevel_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(bridge.InsuranceLevel_CU_ID)));
				if (level == null)
				{
					lkeInsuranceLevel.Properties.ReadOnly = true;
					spnInsurancePrice.Properties.ReadOnly = true;
					continue;
				}

				levelsList.Add(level);
			}

			CommonViewsActions.FillGridlookupEdit(lkeInsuranceLevel, levelsList);
			lkeInsuranceLevel.Properties.ReadOnly = false;
		}

		private void lkeInsuranceLevel_EditValueChanged(object sender, System.EventArgs e)
		{
			if (lkeInsuranceLevel == null)
			{
				spnInsurancePrice.Properties.ReadOnly = true;
				return;
			}

			spnInsurancePrice.Properties.ReadOnly = false;
		}
	}
}
