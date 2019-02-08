using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CommonControlLibrary;
using DevExpress.XtraEditors;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace CommonUserControls.CommonViewers
{
	public partial class LabRegistration_UC : DevExpress.XtraEditors.XtraUserControl
	{
		public Patient_cu ActivePatient { get; set; }
		public List<Service_cu> ParentServices { get; set; }
		public List<Service_cu> ChildrenServices { get; set; }

		public LabRegistration_UC()
		{
			InitializeComponent();
			CommonViewsActions.SetupSyle(this);
		}

		public void Initialize(Patient_cu patient)
		{
			patientTopTitle_UC1.Initialize(patient);

			ParentServices = Service_cu.ItemsList.FindAll(
				item =>
					!Convert.ToBoolean(item.ParentService_CU_ID.HasValue) &&
					Convert.ToInt32(item.ServiceType_P_ID).Equals((int)DB_ServiceType.LabServices));
			CommonViewsActions.FillListBoxControl(lstParentServices_All, ParentServices);

			CommonViewsActions.FillListBoxControl(lstLabServices_All, Service_cu.ItemsList.FindAll(
				item =>
					Convert.ToBoolean(item.ParentService_CU_ID.HasValue) &&
					Convert.ToInt32(item.ServiceType_P_ID).Equals((int)DB_ServiceType.LabServices)));

			SetCount();

			spnTotalServicePrice.EditValue = 0;
			dtServiceDate.DateTime = DateTime.Now;
			CommonViewsActions.FillGridlookupEdit(lkeDoctor, Doctor_cu.ItemsList, valueMember:"Person_CU_ID");
		}

		private void lstParentServices_All_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstParentServices_All.SelectedValue == null)
			{
				CommonViewsActions.FillListBoxControl(lstLabServices_All, Service_cu.ItemsList.FindAll(
					item =>
						Convert.ToBoolean(item.ParentService_CU_ID.HasValue) &&
						Convert.ToInt32(item.ServiceType_P_ID).Equals((int)DB_ServiceType.LabServices)));
				return;
			}

			Service_cu parentService =
				Service_cu.ItemsList.Find(
					item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(lstParentServices_All.SelectedValue)));
			if (parentService == null)
			{
				CommonViewsActions.FillListBoxControl(lstLabServices_All, Service_cu.ItemsList.FindAll(
					item =>
						Convert.ToBoolean(item.ParentService_CU_ID.HasValue) &&
						Convert.ToInt32(item.ServiceType_P_ID).Equals((int)DB_ServiceType.LabServices)));
				return;
			}

			ChildrenServices =
				Service_cu.ItemsList.FindAll(
					item => Convert.ToInt32(item.ParentService_CU_ID).Equals(Convert.ToInt32(parentService.ID)));
			CommonViewsActions.FillListBoxControl(lstLabServices_All, ChildrenServices);

			SetCount();
		}

		private void lstLabServices_All_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetCount();
		}

		private void lstLabServices_Selected_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetCount();
		}

		private void lstLabServices_Selected_DataSourceChanged(object sender, EventArgs e)
		{
			List<Service_cu> alreadySelected;
			if (lstLabServices_Selected.DataSource != null)
				alreadySelected = (List<Service_cu>)lstLabServices_Selected.DataSource;
			else
				alreadySelected = new List<Service_cu>();

			double price = 0;

			foreach (Service_cu serviceCu in alreadySelected)
				if (serviceCu.DefaultPrice != null)
					price = price + Convert.ToDouble(serviceCu.DefaultPrice);

			spnTotalServicePrice.EditValue = price;
		}

		private void txtParentServiceID_DoubleClick(object sender, EventArgs e)
		{
			txtParentServiceID.SelectAll();
		}

		private void txtParentServiceID_Click(object sender, EventArgs e)
		{
			txtParentServiceID.SelectAll();
		}

		private void txtParentServiceID_EditValueChanged(object sender, EventArgs e)
		{
			txtParentServiceName.EditValue = null;

			if (txtParentServiceID.EditValue == null || string.IsNullOrEmpty(txtParentServiceID.Text) ||
				string.IsNullOrWhiteSpace(txtParentServiceID.Text))
			{
				CommonViewsActions.FillListBoxControl(lstParentServices_All, ParentServices);
				return;
			}

			if (ChildrenServices != null && ChildrenServices.Count > 0)
			{
				List<Service_cu> filteredServices =
					ParentServices.FindAll(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(txtParentServiceID.EditValue)));
				CommonViewsActions.FillListBoxControl(lstParentServices_All, filteredServices);
			}
		}

		private void txtParentServiceName_EditValueChanged(object sender, EventArgs e)
		{
			txtParentServiceID.EditValue = null;

			if (txtParentServiceName.EditValue == null || string.IsNullOrEmpty(txtParentServiceName.Text) ||
				string.IsNullOrWhiteSpace(txtParentServiceName.Text))
			{
				CommonViewsActions.FillListBoxControl(lstParentServices_All, ParentServices);
				return;
			}

			if (ChildrenServices != null && ChildrenServices.Count > 0)
			{
				List<Service_cu> filteredServices =
					ParentServices.FindAll(
						item => Convert.ToString(item.Name_P).Contains(Convert.ToString(txtParentServiceName.EditValue)));
				CommonViewsActions.FillListBoxControl(lstParentServices_All, filteredServices);
			}
		}

		private void txtServiceID_All_EditValueChanged(object sender, EventArgs e)
		{

		}

		private void txtServiceName_All_EditValueChanged(object sender, EventArgs e)
		{

		}

		private void txtServiceID_Selected_EditValueChanged(object sender, EventArgs e)
		{

		}

		private void txtServiceName_Selected_EditValueChanged(object sender, EventArgs e)
		{

		}

		private void btnAddList_Click(object sender, EventArgs e)
		{
			if (lstLabServices_All.SelectedItems.Count > 0)
			{
				List<Service_cu> alreadySelected;
				if (lstLabServices_Selected.DataSource != null)
					alreadySelected = (List<Service_cu>)lstLabServices_Selected.DataSource;
				else
					alreadySelected = new List<Service_cu>();

				foreach (var selectedItem in lstLabServices_All.SelectedItems)
				{
					if (alreadySelected.Exists(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((Service_cu)selectedItem).ID))))
					{
						XtraMessageBox.Show("تمت إضافته من قبل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						continue;
					}

					alreadySelected.Add((Service_cu)selectedItem);
				}

				lstLabServices_Selected.DataSource = null;
				CommonViewsActions.FillListBoxControl(lstLabServices_Selected, alreadySelected);
			}
		}

		private void btnRemoveFromList_Click(object sender, EventArgs e)
		{
			if (lstLabServices_Selected.SelectedItems.Count > 0)
			{
				List<Service_cu> alreadySelected;
				if (lstLabServices_Selected.DataSource != null)
					alreadySelected = (List<Service_cu>)lstLabServices_Selected.DataSource;
				else
					alreadySelected = new List<Service_cu>();

				foreach (object selectedItem in lstLabServices_Selected.SelectedItems)
					alreadySelected.Remove((Service_cu)selectedItem);

				lstLabServices_Selected.DataSource = null;
				CommonViewsActions.FillListBoxControl(lstLabServices_Selected, alreadySelected);
			}
		}

		private void SetCount()
		{
			if (lstParentServices_All.DataSource != null)
			{
				List<Service_cu> list = (List<Service_cu>)lstParentServices_All.DataSource;
				lblParentCount.Text = list.Count.ToString();
			}
			else
				lblParentCount.Text = "0";

			if (lstLabServices_All.DataSource != null)
			{
				List<Service_cu> list = (List<Service_cu>)lstLabServices_All.DataSource;
				lblServicesCount_All.Text = list.Count.ToString();
			}
			else
				lblServicesCount_All.Text = "0";

			if (lstLabServices_Selected.DataSource != null)
			{
				List<Service_cu> list = (List<Service_cu>)lstLabServices_Selected.DataSource;
				lblServicesCount_Selected.Text = list.Count.ToString();
			}
			else
				lblServicesCount_Selected.Text = "0";
		}

		private void btnAddService_Click(object sender, EventArgs e)
		{
			if (lstLabServices_Selected.DataSource == null || ((List<Service_cu>) lstLabServices_Selected.DataSource).Count == 0)
			{
				XtraMessageBox.Show("يجب إختيار إختبارت قبل الإضافة", "تنبيه", MessageBoxButtons.YesNo,
					MessageBoxIcon.Error);
				return;
			}

			if (lkeDoctor.EditValue == null)
			{
				XtraMessageBox.Show("يجب إختيار الطبيب قبل الإضافة", "تنبيه", MessageBoxButtons.YesNo,
					MessageBoxIcon.Error);
				return;
			}

			if (dtServiceDate.EditValue == null)
			{
				XtraMessageBox.Show("يجب إختيار تاريخ الإضافة", "تنبيه", MessageBoxButtons.YesNo,
					MessageBoxIcon.Error);
				return;
			}

			DialogResult result = XtraMessageBox.Show("هل تريد إضافة خدمات المعمل ؟", "تنبيه", MessageBoxButtons.YesNo,
				MessageBoxIcon.Exclamation);
			switch (result)
			{
				case DialogResult.Yes:
					if (ParentForm != null)
					{
						ParentForm.DialogResult = DialogResult.Yes;
						ParentForm.Close();
					}
					break;
				case DialogResult.No:
					if (ParentForm != null)
						ParentForm.DialogResult = DialogResult.No;
					break;

			}
		}

		public List<Service_cu> SelectedLabServices
		{
			get
			{
				if (lstLabServices_Selected.DataSource != null)
					return (List<Service_cu>) lstLabServices_Selected.DataSource;
				return null;
			}
		}

		public object ServiceDate
		{
			get { return dtServiceDate.EditValue; }
			set { dtServiceDate.EditValue = value; }
		}

		public object DoctorID
		{
			get { return lkeDoctor.EditValue; }
			set { lkeDoctor.EditValue = value; }
		}

		public double TotalSelectedLabServicesPrice
		{
			get
			{
				if (spnTotalServicePrice.EditValue != null)
					return Convert.ToDouble(spnTotalServicePrice.EditValue);
				return 0;
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			if (ParentForm != null)
			{
				ParentForm.DialogResult = DialogResult.No;
				ParentForm.Close();
			}
		}
	}
}
