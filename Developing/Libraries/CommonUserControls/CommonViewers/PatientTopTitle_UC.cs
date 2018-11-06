using System;
using CommonControlLibrary;
using DevExpress.XtraLayout.Utils;
using MerkDataBaseBusinessLogicProject;

namespace CommonUserControls.CommonViewers
{
	public partial class PatientTopTitle_UC : DevExpress.XtraEditors.XtraUserControl
	{
		public PatientTopTitle_UC()
		{
			InitializeComponent();

			CommonViewsActions.SetupSyle(this);
		}

		public void Initialize(Patient_cu patient)
		{
			if (patient == null)
				return;

			txtPatientID.Text = patient.Person_CU_ID.ToString();
			txtPatientName.Text = patient.PatientFullName;
			if (patient.InsuranceCarrier_InsuranceLevel_CU_ID == null)
			{
				lytInsuranceDetails.Visibility = LayoutVisibility.Never;
				return;
			}

			InsuranceCarrier_InsuranceLevel_cu bridge =
				InsuranceCarrier_InsuranceLevel_cu.ItemsList.Find(
					item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(patient.InsuranceCarrier_InsuranceLevel_CU_ID)));

			if (bridge == null)
			{
				lytInsuranceDetails.Visibility = LayoutVisibility.Never;
				return;
			}

			InsuranceCarrier_cu carrier =
				InsuranceCarrier_cu.ItemsList.Find(
					item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(bridge.InsuranceCarrier_CU_ID)));
			if (carrier == null)
				return;

			txtInsuranceCarrierName.Text = "الجهة : " + carrier.Name_P;

			InsuranceLevel_cu level =
				InsuranceLevel_cu.ItemsList.Find(
					item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(bridge.InsuranceLevel_CU_ID)));
			if (level == null)
				return;

			txtInsuranceLevelName.Text = "المستوى : " + level.Name_P;

			lblInsurancePercentage.Text = bridge.InsurancePercentage * 100 + " %";
			lblPatientPercentage.Text = (1 - bridge.InsurancePercentage) * 100 + " %";
		}

		private void txtPatientID_DoubleClick(object sender, EventArgs e)
		{

		}
	}
}
