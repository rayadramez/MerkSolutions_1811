using System;
using System.Windows.Forms;
using CommonControlLibrary;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace CommonUserControls.QueueManagerViewers
{
	public partial class QueueManager_DoctorSelection_UC : UserControl
	{
		public InvoiceDetail ActiveInvoiceDetail { get; set; }
		public QueueManager ActiveQueueManager { get; set; }

		public QueueManager_DoctorSelection_UC()
		{
			InitializeComponent();
			CommonViewsActions.SetupSyle(this);
			CommonViewsActions.FillGridlookupEdit(lkeDoctors, Doctor_cu.ItemsList, "Name_P", "Person_CU_ID");
		}

		public void Initialize(InvoiceDetail invoiceDetail, QueueManager queueManager)
		{
			ActiveInvoiceDetail = invoiceDetail;
			ActiveQueueManager = queueManager;
			if (ActiveInvoiceDetail != null)
				lkeDoctors.EditValue = ActiveInvoiceDetail.Doctor_CU_ID;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			if (ParentForm != null)
				ParentForm.Close();
		}

		private void btnAccept_Click(object sender, EventArgs e)
		{
			if (ActiveInvoiceDetail == null || ActiveQueueManager == null)
				return;

			if (lkeDoctors.EditValue == null)
			{
				XtraMessageBox.Show("يجــب إختيــار الطبيـــب", "تنبيـــه", MessageBoxButtons.OK, MessageBoxIcon.Information,
							MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
				return;
			}

			if (Convert.ToInt32(lkeDoctors.EditValue).Equals(Convert.ToInt32(ActiveInvoiceDetail.Doctor_CU_ID)))
			{
				XtraMessageBox.Show("يجــب إختيــار طبيـــب آخـــر", "تنبيـــه", MessageBoxButtons.OK, MessageBoxIcon.Information,
							MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
				return;
			}

			DialogResult result = XtraMessageBox.Show("هـل تـريــد تغييــر الطبيــب لهـذه الخـدمـــة ?", "تنبيـــه",
				MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
			switch (result)
			{
				case  DialogResult.Yes:
					ActiveInvoiceDetail.DBCommonTransactionType = DB_CommonTransactionType.UpdateExisting;
					ActiveInvoiceDetail.Doctor_CU_ID = Convert.ToInt32(lkeDoctors.EditValue);
					if (ActiveInvoiceDetail.SaveChanges())
					{
						ActiveQueueManager.DBCommonTransactionType = DB_CommonTransactionType.UpdateExisting;
						ActiveQueueManager.Doctor_CU_ID = Convert.ToInt32(lkeDoctors.EditValue);
						if (ActiveQueueManager.SaveChanges())
							XtraMessageBox.Show("تـم التغييــر بنجــاح", "تنبيـــه", MessageBoxButtons.OK, MessageBoxIcon.Information,
								MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
					}
					
					break;
			}
		}

		private void btnReject_Click(object sender, EventArgs e)
		{
			btnClose_Click(null, null);
		}
	}
}
