using System.Collections.Generic;
using System.Windows.Forms;
using CommonControlLibrary;
using DevExpress.XtraEditors;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace CommonUserControls.InvoiceViewers
{
	public partial class InvoiceManagerQeueCardContainer_UC : XtraUserControl
	{
		public static InvoiceManagerQueueContainerWithHeaderIcons_UC ParentControlWithHeaderIcon { get; set; }

		public InvoiceManagerQeueCardContainer_UC()
		{
			InitializeComponent();
			CommonViewsActions.SetupSyle(this);
			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_InvoiceManagerQeueCardContainer_UC);
			Initialize();
		}

		public void Initialize()
		{
			tabInPatientWithInsurance.Controls.Clear();
			tabInPatientWithoutInsurance.Controls.Clear();
			tabOutPatientWithInsurance.Controls.Clear();
			tabOutPatientWithoutInsurance.Controls.Clear();

			List<ReadyInvoicesForAction> readyInvoices =
				MerkDBBusinessLogicEngine.ReadyInvoicesForAction(AdmissionType.InPatientAdmission, null, null, true, false, false,
					null, null, null);
			if (readyInvoices != null)
				readyInvoices.AddRange(MerkDBBusinessLogicEngine.ReadyInvoicesForAction(AdmissionType.InPatientAdmission, null,
					null, true, true, false,
					null, null, null));
			if (readyInvoices != null)
				readyInvoices.AddRange(MerkDBBusinessLogicEngine.ReadyInvoicesForAction(AdmissionType.ClinicAdmission, null, null,
					true, false, false,
					null, null, null));
			if (readyInvoices != null)
				readyInvoices.AddRange(MerkDBBusinessLogicEngine.ReadyInvoicesForAction(AdmissionType.ClinicAdmission, null, null,
					true, true, false,
					null, null, null));

			if (readyInvoices == null || readyInvoices.Count == 0)
				return;

			lblInvoicesCount.Text = "عدد الفواتير : " + readyInvoices.Count.ToString();

			List<ReadyInvoicesForAction> inPatientWithInsuranceInvoices =
				MerkDBBusinessLogicEngine.GetSpecificReadyInvoices(readyInvoices, true, true);
			List<ReadyInvoicesForAction> inPatientWithoutInsuranceInvoices =
				MerkDBBusinessLogicEngine.GetSpecificReadyInvoices(readyInvoices, false, true);
			List<ReadyInvoicesForAction> outPatientWithInsuranceInvoices =
				MerkDBBusinessLogicEngine.GetSpecificReadyInvoices(readyInvoices, true, false);
			List<ReadyInvoicesForAction> outPatientWithoutInsuranceInvoices =
				MerkDBBusinessLogicEngine.GetSpecificReadyInvoices(readyInvoices, false, false);

			#region InPatient with Insurance

			if (inPatientWithInsuranceInvoices.Count > 0)
			{
				foreach (ReadyInvoicesForAction readyInvoice in inPatientWithInsuranceInvoices)
				{
					InvoiceManagerPatientQueueCard_UC card = new InvoiceManagerPatientQueueCard_UC();
					card.Dock = DockStyle.Top;
					string reservationTime = "";

					card.Initialize(ParentControlWithHeaderIcon, readyInvoice);
					tabInPatientWithInsurance.Controls.Add(card);
				}
			}

			#endregion

			#region InPatient without Insurance

			if (inPatientWithoutInsuranceInvoices.Count > 0)
			{
				foreach (ReadyInvoicesForAction readyInvoice in inPatientWithoutInsuranceInvoices)
				{
					InvoiceManagerPatientQueueCard_UC card = new InvoiceManagerPatientQueueCard_UC();
					card.Dock = DockStyle.Top;
					string reservationTime = "";

					reservationTime = "20 يوم";

					card.Initialize(ParentControlWithHeaderIcon, readyInvoice);

					tabInPatientWithoutInsurance.Controls.Add(card);
				}
			}

			#endregion

			#region OutPatient with Insurance

			if (outPatientWithInsuranceInvoices.Count > 0)
			{
				foreach (ReadyInvoicesForAction readyInvoice in outPatientWithInsuranceInvoices)
				{
					InvoiceManagerPatientQueueCard_UC card = new InvoiceManagerPatientQueueCard_UC();
					card.Dock = DockStyle.Top;
					string reservationTime = "";

					reservationTime = "20 يوم";

					card.Initialize(ParentControlWithHeaderIcon, readyInvoice);

					tabOutPatientWithInsurance.Controls.Add(card);
				}
			}

			#endregion

			#region OutPatient without Insurance

			if (outPatientWithoutInsuranceInvoices.Count > 0)
			{
				foreach (ReadyInvoicesForAction readyInvoice in outPatientWithoutInsuranceInvoices)
				{
					InvoiceManagerPatientQueueCard_UC card = new InvoiceManagerPatientQueueCard_UC();
					card.Dock = DockStyle.Top;
					string reservationTime = "";

					reservationTime = "20 يوم";

					card.Initialize(ParentControlWithHeaderIcon, readyInvoice);
					tabOutPatientWithoutInsurance.Controls.Add(card);
				}
			}

			#endregion
		}

		private void btnRefresh_Click(object sender, System.EventArgs e)
		{
			Initialize();
		}
	}
}
