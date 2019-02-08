using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace CommonUserControls.CommonViewers
{
	public enum ScanningMode
	{
		None = 0,
		Regular = 1,
		MedicalVisit = 2
	}

	public enum MedicalType
	{
		None = 0,
		InvestigationResult = 1,
		LabResult = 2,
		SurgeryResult = 3
	}

	public partial class ScanFiles_UC : UserControl
	{
		private Patient_cu ActiveSelectedPatient { get; set; }
		private PatientAttachment_cu SelectedItem { get; set; }
		private VisitTiming_InvestigationReservation Active_VisitTiming_InvestigationReservation { get; set; }
		private VisitTiming_LabReservation Active_VisitTiming_LabReservation { get; set; }
		private VisitTiming_SurgeryReservation Active_VisitTiming_SurgeryReservation { get; set; }
		private string errorMessage = "";
		private ScanningMode ActiveScanningMode { get; set; }
		private ScanningMode OverridenScanningMode { get; set; }
		private MedicalType MedicalType { get; set; }
		private bool SaveImmediately { get; set; }

		public ScanFiles_UC()
		{
			InitializeComponent();
		}

		private void ScanFiles_UC_Load(object sender, EventArgs e)
		{
			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_ScanFiles_UC);
			CommonViewsActions.SetupSyle(this);
		}

		public void ClearControls()
		{
			txtDescription.EditValue = null;
			txtPictureName.EditValue = null;
			lstImageNames.DataSource = null;
			pictureEdit1.EditValue = null;
		}

		public void Initialize(Patient_cu patient, ScanningMode scanningMode, MedicalType medicalType)
		{
			ActiveSelectedPatient = patient;
			ActiveScanningMode = scanningMode;
			MedicalType = medicalType;
			BackColor = Color.White;

			List<PatientAttachment_cu> list = null;
			switch (ActiveScanningMode)
			{
				case ScanningMode.Regular:
					if (ActiveSelectedPatient != null)
						if (ImagesList == null)
							ImagesList = new List<PatientAttachment_cu>();
					ImagesList = PatientAttachment_cu.ItemsList.FindAll(item =>
						Convert.ToInt32(item.Patient_CU_ID)
							.Equals(Convert.ToInt32(ActiveSelectedPatient.Person_CU_ID)) &&
						Convert.ToInt32(item.ImageType_P_ID).Equals(Convert.ToInt32(ImageType_P_ID)));
					emptySpaceItem3.Visibility = LayoutVisibility.Always;
					layoutControlItem4.Visibility = LayoutVisibility.Always;
					layoutControlItem3.Visibility = LayoutVisibility.Always;
					layoutControlGroup2.Visibility = LayoutVisibility.Always;
					layoutControlGroup3.Visibility = LayoutVisibility.Always;
					layoutControlItem5.Visibility = LayoutVisibility.Always;
					layoutControlItem15.Visibility = LayoutVisibility.Always;
					simpleSeparator4.Visibility = LayoutVisibility.Always;
					layoutControlGroup2.Visibility = LayoutVisibility.Always;
					layoutControlGroup3.Visibility = LayoutVisibility.Always;
					layoutControlItem5.Visibility = LayoutVisibility.Always;
					lytSelectAll.Visibility = LayoutVisibility.Never;
					break;
				case ScanningMode.MedicalVisit:
					if (PEMRBusinessLogic.ActivePEMRObject != null &&
						PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Attachment != null)
						foreach (VisitTiming_Attachment visitTimingAttachement in PEMRBusinessLogic.ActivePEMRObject
							.List_VisitTiming_Attachment)
						{
							PatientAttachment_cu patientAttachment = PatientAttachment_cu.ItemsList.Find(item =>
								Convert.ToInt32(item.ID)
									.Equals(Convert.ToInt32(visitTimingAttachement.PatientAttachement_CU_ID)));
							if (patientAttachment != null)
							{
								if (ImagesList == null)
									ImagesList = new List<PatientAttachment_cu>();
								ImagesList.Add(patientAttachment);
							}
						}

					emptySpaceItem3.Visibility = LayoutVisibility.Never;
					layoutControlItem4.Visibility = LayoutVisibility.Never;
					layoutControlItem3.Visibility = LayoutVisibility.Never;
					layoutControlGroup2.Visibility = LayoutVisibility.Never;
					layoutControlGroup3.Visibility = LayoutVisibility.Never;
					layoutControlItem5.Visibility = LayoutVisibility.Never;
					layoutControlItem15.Visibility = LayoutVisibility.Never;
					simpleSeparator4.Visibility = LayoutVisibility.Never;
					layoutControlGroup2.Visibility = LayoutVisibility.Never;
					layoutControlGroup3.Visibility = LayoutVisibility.Never;
					layoutControlItem5.Visibility = LayoutVisibility.Never;
					lytSelectAll.Visibility = LayoutVisibility.Always;
					break;
			}

			switch (medicalType)
			{
				case MedicalType.InvestigationResult:
					chkPersonalPhoto.Enabled = false;
					chkID.Enabled = false;
					chkPassport.Enabled = false;
					chkInvestigation.Enabled = true;
					chkSurgery.Enabled = false;
					chkLab.Enabled = false;
					chkOther.Enabled = false;
					chkInvestigation.Checked = true;
					break;
				case MedicalType.LabResult:
					chkPersonalPhoto.Enabled = false;
					chkID.Enabled = false;
					chkPassport.Enabled = false;
					chkInvestigation.Enabled = false;
					chkSurgery.Enabled = false;
					chkLab.Enabled = true;
					chkOther.Enabled = false;
					chkLab.Checked = true;
					break;
				case MedicalType.SurgeryResult:
					chkPersonalPhoto.Enabled = false;
					chkID.Enabled = false;
					chkPassport.Enabled = false;
					chkInvestigation.Enabled = false;
					chkLab.Enabled = false;
					chkSurgery.Enabled = true;
					chkOther.Enabled = false;
					chkSurgery.Checked = true;
					break;
				case MedicalType.None:
					chkPersonalPhoto.Enabled = true;
					chkID.Enabled = true;
					chkPassport.Enabled = true;
					chkSurgery.Enabled = true;
					chkInvestigation.Enabled = true;
					chkLab.Enabled = true;
					chkOther.Enabled = true;
					chkPersonalPhoto.Checked = true;
					break;
			}

			CommonViewsActions.FillListBoxControl(lstImageNames, ImagesList, "ImageName");
			lstImageNames.Refresh();

			if (!string.IsNullOrEmpty(ApplicationStaticConfiguration.SkinName) &&
				!string.IsNullOrWhiteSpace(ApplicationStaticConfiguration.SkinName))
				if (ApplicationStaticConfiguration.SkinName.Equals("McSkin"))
				{
					lblPatientID.ForeColor = Color.DarkOrange;
					lblPatientName.ForeColor = lblInsuranceCarrierName.ForeColor = lblInsuranceLevelName.ForeColor =
						lblInsurancePercentage.ForeColor = Color.DarkBlue;
					labelControl1.ForeColor = labelControl3.ForeColor =
						labelControl5.ForeColor = labelControl7.ForeColor = Color.Black;
					btnClose.Image = Properties.Resources.ExitIcon_8;
				}
				else
				{
					lblPatientID.ForeColor = Color.OrangeRed;
					lblPatientName.ForeColor = lblInsuranceCarrierName.ForeColor = lblInsuranceLevelName.ForeColor =
						lblInsurancePercentage.ForeColor = Color.Ivory;
					labelControl1.ForeColor = labelControl3.ForeColor =
						labelControl5.ForeColor = labelControl7.ForeColor = Color.OldLace;
					btnClose.Image = Properties.Resources.Exit_1_16;
				}

			lblPatientID.Text = ActiveSelectedPatient.Person_CU_ID.ToString();
			lblPatientName.Text = ActiveSelectedPatient.PatientFullName;
			if (ActiveSelectedPatient.InsuranceCarrier_InsuranceLevel_CU_ID != null)
			{
				InsuranceCarrier_InsuranceLevel_cu insuranceBridge =
					InsuranceCarrier_InsuranceLevel_cu.ItemsList.Find(
						item =>
							Convert.ToInt32(item.ID)
								.Equals(Convert.ToInt32(ActiveSelectedPatient.InsuranceCarrier_InsuranceLevel_CU_ID)));
				if (insuranceBridge != null)
				{
					InsuranceCarrier_cu insuranceCarrier =
						InsuranceCarrier_cu.ItemsList.Find(
							item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(insuranceBridge.InsuranceCarrier_CU_ID)));
					if (insuranceCarrier != null)
						lblInsuranceCarrierName.Text = insuranceCarrier.Name_P;
					InsuranceLevel_cu insurancelevel =
						InsuranceLevel_cu.ItemsList.Find(
							item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(insuranceBridge.InsuranceLevel_CU_ID)));
					if (insurancelevel != null)
						lblInsuranceLevelName.Text = insurancelevel.Name_P;
					lblInsurancePercentage.Text = Convert.ToString(insuranceBridge.InsurancePercentage * 100);
				}
			}
		}

		public void Pass_VisitTiming_InvestigationReservation(
			VisitTiming_InvestigationReservation visitTimingInvestigationReservation, bool saveImmediately)
		{
			Active_VisitTiming_InvestigationReservation = visitTimingInvestigationReservation;
			SaveImmediately = saveImmediately;
		}

		public void Pass_VisitTiming_LabReservation(VisitTiming_LabReservation visitTimingLabReservation,
			bool saveImmediately)
		{
			Active_VisitTiming_LabReservation = visitTimingLabReservation;
			SaveImmediately = saveImmediately;
		}

		public void Pass_VisitTiming_SurgeryReservation(VisitTiming_SurgeryReservation visitTimingSurgeryReservation,
			bool saveImmediately)
		{
			Active_VisitTiming_SurgeryReservation = visitTimingSurgeryReservation;
			SaveImmediately = saveImmediately;
		}

		#region Implementation of IPatientAttachment

		public List<PatientAttachment_cu> ImagesList { get; set; }

		public object ImageType_P_ID
		{
			get
			{
				if (chkPersonalPhoto.Checked)
					return (int)DB_ImageType.PersonalImage;
				if (chkID.Checked)
					return (int)DB_ImageType.IDCard;
				if (chkPassport.Checked)
					return (int)DB_ImageType.Passport;
				if (chkInvestigation.Checked)
					return (int)DB_ImageType.Investigation_Report;
				if (chkLab.Checked)
					return (int)DB_ImageType.Lab_Report;
				if (chkSurgery.Checked)
					return (int)DB_ImageType.Surgery_Report;
				if (chkOther.Checked)
					return (int)DB_ImageType.Other;
				
				return (int)DB_ImageType.None;
			}
			set
			{
				DB_ImageType imageType = (DB_ImageType)value;
				switch (imageType)
				{
					case DB_ImageType.IDCard:
						chkID.Checked = true;
						break;
					case DB_ImageType.Passport:
						chkPassport.Checked = true;
						break;
					case DB_ImageType.Investigation_Report:
						chkInvestigation.Checked = true;
						break;
					case DB_ImageType.Lab_Report:
						chkLab.Checked = true;
						break;
					case DB_ImageType.Surgery_Report:
						chkSurgery.Checked = true;
						break;
					case DB_ImageType.Other:
						chkOther.Checked = true;
						break;
					case DB_ImageType.PersonalImage:
						chkPersonalPhoto.Checked = true;
						break;
				}
			}
		}

		public object ImageName
		{
			get { return txtPictureName.EditValue; }
			set { txtPictureName.EditValue = value; }
		}

		public object Description
		{
			get { return txtDescription.EditValue; }
			set { txtDescription.EditValue = value; }
		}

		#endregion

		private void lstImageNames_MouseUp(object sender, MouseEventArgs e)
		{
			if (lstImageNames.SelectedItem != null)
			{
				SelectedItem = (PatientAttachment_cu)lstImageNames.SelectedItem;
				if (SelectedItem != null)
					pictureEdit1.Image = FileManager.GetImageFromPath(SelectedItem.ImagePath, ref errorMessage);
				if (!string.IsNullOrEmpty(errorMessage) || !string.IsNullOrWhiteSpace(errorMessage))
					XtraMessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
						MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
				errorMessage = string.Empty;
			}
		}

		private void lstImageNames_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstImageNames.SelectedItem != null)
			{
				SelectedItem = (PatientAttachment_cu)lstImageNames.SelectedItem;
				if (SelectedItem != null)
					pictureEdit1.Image = FileManager.GetImageFromPath(SelectedItem.ImagePath, ref errorMessage);
			}
		}

		private void btnScanner_Click(object sender, EventArgs e)
		{
			if (txtPictureName.EditValue == null)
			{
				XtraMessageBox.Show("يجــب كتابــة إســم الصـــورة", "Note",
					MessageBoxButtons.OK, MessageBoxIcon.Error,
					MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
				return;
			}

			string savedPath = "";
			Image image = WIAScannerEngine.ScanFile(ActiveSelectedPatient.Person_CU_ID.ToString(),
				ActiveSelectedPatient.Person_CU_ID + "_" + (int)ImageType_P_ID + "_" + DateTime.Now.Date.Day + "_" +
				DateTime.Now.Date.Month + "_" + DateTime.Now.Date.Year + "_" + txtPictureName.Text, ImageFormat.JPEG,
				false, ref savedPath, ref errorMessage);
			if (image == null)
			{
				if (!string.IsNullOrEmpty(errorMessage) && !string.IsNullOrWhiteSpace(errorMessage))
				{
					XtraMessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
						MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
					errorMessage = string.Empty;
					return;
				}
			}

			if (!string.IsNullOrEmpty(errorMessage) && !string.IsNullOrWhiteSpace(errorMessage))
			{
				XtraMessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
					MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
				errorMessage = string.Empty;
				return;
			}

			pictureEdit1.Image = image;
			if (ImagesList == null)
				ImagesList = new List<PatientAttachment_cu>();

			PatientAttachment_cu patientAttachment = MerkDBBusinessLogicEngine.CreateNewPatientAttachement(
				ActiveSelectedPatient.Person_CU_ID,
				txtPictureName.Text, savedPath, (DB_ImageType)ImageType_P_ID, Description,
				ApplicationStaticConfiguration.ActiveLoginUser.Person_CU_ID);
			VisitTiming_InvestigationResult investigationResult = null;
			VisitTiming_Attachment visitTimingAttachement = null;

			switch (ActiveScanningMode)
			{
				case ScanningMode.Regular:
					if (MerkDBBusinessLogicEngine.SavePatientAttachement(patientAttachment))
						ImagesList.Add(patientAttachment);
					break;
				case ScanningMode.MedicalVisit:
					visitTimingAttachement =
						PEMRBusinessLogic.CreateNew_VisitTiming_Attachment(
							patientAttachment, ApplicationStaticConfiguration.ActiveLoginUser.Person_CU_ID);
					if (visitTimingAttachement != null)
					{
						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Attachment == null)
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Attachment = new List<VisitTiming_Attachment>();
						PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Attachment.Add(visitTimingAttachement);
						ImagesList.Add(patientAttachment);

						switch (MedicalType)
						{
							case MedicalType.InvestigationResult:
								investigationResult =
									PEMRBusinessLogic.CreateNew_VisitTiming_InvestigationResult(visitTimingAttachement,
										Active_VisitTiming_InvestigationReservation,
										ApplicationStaticConfiguration.ActiveLoginUser.Person_CU_ID);
								if (investigationResult != null)
									if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_InvestigationResult == null)
										PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_InvestigationResult =
											new List<VisitTiming_InvestigationResult>();
								PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_InvestigationResult.Add(
									investigationResult);
									break;
						}
					}

					break;
			}

			txtDescription.EditValue = null;
			txtPictureName.EditValue = null;
			CommonViewsActions.FillListBoxControl(lstImageNames, ImagesList, "ImageName");
			lstImageNames.Refresh();

			if (investigationResult != null && visitTimingAttachement != null &&
			    PEMRBusinessLogic.ActiveVisitTimming != null && SaveImmediately)
			{
				visitTimingAttachement.VisitTimingID = PEMRBusinessLogic.ActiveVisitTimming.ID;
				investigationResult.VisitTiming_InvestigationReservationID =
					Active_VisitTiming_InvestigationReservation.ID;
				if(investigationResult.SaveChanges())
					PatientAttachment_cu.ItemsList.Add(patientAttachment);
			}
		}

		private void btnOpen_Click(object sender, EventArgs e)
		{
			List<string> copiedImagesList =
				FileManager.FileDialogeAndCopy(ActiveSelectedPatient.Person_CU_ID.ToString(),
					FileManager.GetServerDirectoryPath(DB_ServerDirectory.ScanDirectory), true,
					FileSelectionFilter.Images, ref errorMessage,
					ActiveSelectedPatient.Person_CU_ID + "_" + (int) ImageType_P_ID + "_" + DateTime.Now.Date.Day +
					"_" + DateTime.Now.Date.Month + "_" + DateTime.Now.Date.Year);
			if (!string.IsNullOrEmpty(errorMessage))
			{
				XtraMessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
					MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
				errorMessage = string.Empty;
				return;
			}

			if (copiedImagesList == null || copiedImagesList.Count == 0)
				return;

			if (ImagesList == null)
				ImagesList = new List<PatientAttachment_cu>();

			foreach (string fileName in copiedImagesList)
			{
				PatientAttachment_cu patientAttachment = MerkDBBusinessLogicEngine.CreateNewPatientAttachement(
					ActiveSelectedPatient.Person_CU_ID,
					Path.GetFileName(fileName), fileName, (DB_ImageType)ImageType_P_ID, Description,
					ApplicationStaticConfiguration.ActiveLoginUser.Person_CU_ID);

				switch (ActiveScanningMode)
				{
					case ScanningMode.Regular:
						if (MerkDBBusinessLogicEngine.SavePatientAttachement(patientAttachment))
							ImagesList.Add(patientAttachment);
						break;
					case ScanningMode.MedicalVisit:
						VisitTiming_Attachment visitTimingAttachement =
							PEMRBusinessLogic.CreateNew_VisitTiming_Attachment(
								patientAttachment, ApplicationStaticConfiguration.ActiveLoginUser.Person_CU_ID);
						if (visitTimingAttachement != null)
							if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Attachment == null)
								PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Attachment = new List<VisitTiming_Attachment>();
						PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Attachment.Add(visitTimingAttachement);
						ImagesList.Add(patientAttachment);

						break;
				}
			}

			txtDescription.EditValue = null;
			txtPictureName.EditValue = null;
			CommonViewsActions.FillListBoxControl(lstImageNames, ImagesList, "ImageName");
			lstImageNames.Refresh();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (SelectedItem == null)
				return;

			DialogResult result = XtraMessageBox.Show("Do you want to delete this image ?",
				"Note",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
			switch (result)
			{
				case DialogResult.Yes:
					using (var stream = File.Open(SelectedItem.ImagePath, FileMode.Open, FileAccess.Write, FileShare.Read))
					{
						stream.Dispose();
					}
					if (File.Exists(SelectedItem.ImagePath))
						File.Delete(SelectedItem.ImagePath);
					else
						XtraMessageBox.Show("This file does not exist ?",
							"Note",
							MessageBoxButtons.YesNo, MessageBoxIcon.Question,
							MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
					break;
			}
		}

		private void chkID_CheckedChanged(object sender, EventArgs e)
		{
			ImagesList = null;
			switch (ActiveScanningMode)
			{
				case ScanningMode.Regular:
					if (ActiveSelectedPatient != null)
					{
						if (ImagesList == null)
							ImagesList = new List<PatientAttachment_cu>();
						ImagesList = PatientAttachment_cu.ItemsList.FindAll(item =>
							Convert.ToInt32(item.Patient_CU_ID)
								.Equals(Convert.ToInt32(ActiveSelectedPatient.Person_CU_ID)) &&
							Convert.ToInt32(item.ImageType_P_ID).Equals(Convert.ToInt32(ImageType_P_ID)));
					}
					break;
				case ScanningMode.MedicalVisit:
					if (PEMRBusinessLogic.ActivePEMRObject != null && PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Attachment != null)
						foreach (VisitTiming_Attachment visitTimingAttachement in PEMRBusinessLogic.ActivePEMRObject
							.List_VisitTiming_Attachment)
						{
							PatientAttachment_cu patientAttachment = PatientAttachment_cu.ItemsList.Find(item =>
								Convert.ToInt32(item.ID)
									.Equals(Convert.ToInt32(visitTimingAttachement.PatientAttachement_CU_ID)) &&
								Convert.ToInt32(item.ImageType_P_ID).Equals(Convert.ToInt32(ImageType_P_ID)));
							if (patientAttachment != null)
							{
								if (ImagesList == null)
									ImagesList = new List<PatientAttachment_cu>();
								ImagesList.Add(patientAttachment);
							}
						}

					break;
			}
			CommonViewsActions.FillListBoxControl(lstImageNames, ImagesList, "ImageName");
			if (ImagesList == null || ImagesList.Count == 0)
				pictureEdit1.Image = Properties.Resources.TopLogin;
			lstImageNames.Refresh();
		}

		private void chkPassport_CheckedChanged(object sender, EventArgs e)
		{
			ImagesList = null;
			switch (ActiveScanningMode)
			{
				case ScanningMode.Regular:
					if (ActiveSelectedPatient != null)
					{
						if (ImagesList == null)
							ImagesList = new List<PatientAttachment_cu>();
						ImagesList = PatientAttachment_cu.ItemsList.FindAll(item =>
							Convert.ToInt32(item.Patient_CU_ID)
								.Equals(Convert.ToInt32(ActiveSelectedPatient.Person_CU_ID)) &&
							Convert.ToInt32(item.ImageType_P_ID).Equals(Convert.ToInt32(ImageType_P_ID)));
					}
					break;
				case ScanningMode.MedicalVisit:
					if (PEMRBusinessLogic.ActivePEMRObject != null && PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Attachment != null)
						foreach (VisitTiming_Attachment visitTimingAttachement in PEMRBusinessLogic.ActivePEMRObject
							.List_VisitTiming_Attachment)
						{
							PatientAttachment_cu patientAttachment = PatientAttachment_cu.ItemsList.Find(item =>
								Convert.ToInt32(item.ID)
									.Equals(Convert.ToInt32(visitTimingAttachement.PatientAttachement_CU_ID)) &&
								Convert.ToInt32(item.ImageType_P_ID).Equals(Convert.ToInt32(ImageType_P_ID)));
							if (patientAttachment != null)
							{
								if (ImagesList == null)
									ImagesList = new List<PatientAttachment_cu>();
								ImagesList.Add(patientAttachment);
							}
						}

					break;
			}
			CommonViewsActions.FillListBoxControl(lstImageNames, ImagesList, "ImageName");
			if (ImagesList == null || ImagesList.Count == 0)
				pictureEdit1.Image = Properties.Resources.TopLogin;
			lstImageNames.Refresh();
		}

		private void chkInvestigation_CheckedChanged(object sender, EventArgs e)
		{
			ImagesList = null;
			switch (ActiveScanningMode)
			{
				case ScanningMode.Regular:
					if (ActiveSelectedPatient != null)
					{
						if (ImagesList == null)
							ImagesList = new List<PatientAttachment_cu>();
						ImagesList = PatientAttachment_cu.ItemsList.FindAll(item =>
							Convert.ToInt32(item.Patient_CU_ID)
								.Equals(Convert.ToInt32(ActiveSelectedPatient.Person_CU_ID)) &&
							Convert.ToInt32(item.ImageType_P_ID).Equals(Convert.ToInt32(ImageType_P_ID)));
					}
					break;
				case ScanningMode.MedicalVisit:
					if (PEMRBusinessLogic.ActivePEMRObject != null && PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Attachment != null)
						foreach (VisitTiming_Attachment visitTimingAttachement in PEMRBusinessLogic.ActivePEMRObject
							.List_VisitTiming_Attachment)
						{
							PatientAttachment_cu patientAttachment = PatientAttachment_cu.ItemsList.Find(item =>
								Convert.ToInt32(item.ID)
									.Equals(Convert.ToInt32(visitTimingAttachement.PatientAttachement_CU_ID)) &&
								Convert.ToInt32(item.ImageType_P_ID).Equals(Convert.ToInt32(ImageType_P_ID)));
							if (ImagesList == null)
								ImagesList = new List<PatientAttachment_cu>();
							if (patientAttachment != null)
								ImagesList.Add(patientAttachment);
						}

					break;
			}
			CommonViewsActions.FillListBoxControl(lstImageNames, ImagesList, "ImageName");
			if (ImagesList == null || ImagesList.Count == 0)
				pictureEdit1.Image = Properties.Resources.TopLogin;
			lstImageNames.Refresh();
		}

		private void chkLab_CheckedChanged(object sender, EventArgs e)
		{
			ImagesList = null;
			switch (ActiveScanningMode)
			{
				case ScanningMode.Regular:
					if (ActiveSelectedPatient != null)
						ImagesList = PatientAttachment_cu.ItemsList.FindAll(item =>
							Convert.ToInt32(item.Patient_CU_ID)
								.Equals(Convert.ToInt32(ActiveSelectedPatient.Person_CU_ID)) &&
							Convert.ToInt32(item.ImageType_P_ID).Equals(Convert.ToInt32(ImageType_P_ID)));
					break;
				case ScanningMode.MedicalVisit:
					if (PEMRBusinessLogic.ActivePEMRObject != null && PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Attachment != null)
						foreach (VisitTiming_Attachment visitTimingAttachement in PEMRBusinessLogic.ActivePEMRObject
							.List_VisitTiming_Attachment)
						{
							PatientAttachment_cu patientAttachment = PatientAttachment_cu.ItemsList.Find(item =>
								Convert.ToInt32(item.ID)
									.Equals(Convert.ToInt32(visitTimingAttachement.PatientAttachement_CU_ID)) &&
								Convert.ToInt32(item.ImageType_P_ID).Equals(Convert.ToInt32(ImageType_P_ID)));
							if (patientAttachment != null)
							{
								if (ImagesList == null)
									ImagesList = new List<PatientAttachment_cu>();
								ImagesList.Add(patientAttachment);
							}
						}

					break;
			}
			CommonViewsActions.FillListBoxControl(lstImageNames, ImagesList, "ImageName");
			if (ImagesList == null || ImagesList.Count == 0)
				pictureEdit1.Image = Properties.Resources.TopLogin;
			lstImageNames.Refresh();
		}

		private void chkSurgery_CheckedChanged(object sender, EventArgs e)
		{
			ImagesList = null;
			switch (ActiveScanningMode)
			{
				case ScanningMode.Regular:
					if (ActiveSelectedPatient != null)
						ImagesList = PatientAttachment_cu.ItemsList.FindAll(item =>
							Convert.ToInt32(item.Patient_CU_ID)
								.Equals(Convert.ToInt32(ActiveSelectedPatient.Person_CU_ID)) &&
							Convert.ToInt32(item.ImageType_P_ID).Equals(Convert.ToInt32(ImageType_P_ID)));
					break;
				case ScanningMode.MedicalVisit:
					if (PEMRBusinessLogic.ActivePEMRObject != null && PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Attachment != null)
						foreach (VisitTiming_Attachment visitTimingAttachement in PEMRBusinessLogic.ActivePEMRObject
							.List_VisitTiming_Attachment)
						{
							PatientAttachment_cu patientAttachment = PatientAttachment_cu.ItemsList.Find(item =>
								Convert.ToInt32(item.ID)
									.Equals(Convert.ToInt32(visitTimingAttachement.PatientAttachement_CU_ID)) &&
								Convert.ToInt32(item.ImageType_P_ID).Equals(Convert.ToInt32(ImageType_P_ID)));
							if (patientAttachment != null)
							{
								if (ImagesList == null)
									ImagesList = new List<PatientAttachment_cu>();
								ImagesList.Add(patientAttachment);
							}
						}

					break;
			}
			CommonViewsActions.FillListBoxControl(lstImageNames, ImagesList, "ImageName");
			if (ImagesList == null || ImagesList.Count == 0)
				pictureEdit1.Image = Properties.Resources.TopLogin;
			lstImageNames.Refresh();
		}

		private void chkOther_CheckedChanged(object sender, EventArgs e)
		{
			ImagesList = null;
			switch (ActiveScanningMode)
			{
				case ScanningMode.Regular:
					if (ActiveSelectedPatient != null)
						ImagesList = PatientAttachment_cu.ItemsList.FindAll(item =>
							Convert.ToInt32(item.Patient_CU_ID)
								.Equals(Convert.ToInt32(ActiveSelectedPatient.Person_CU_ID)) &&
							Convert.ToInt32(item.ImageType_P_ID).Equals(Convert.ToInt32(ImageType_P_ID)));
					break;
				case ScanningMode.MedicalVisit:
					if (PEMRBusinessLogic.ActivePEMRObject != null && PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Attachment != null)
						foreach (VisitTiming_Attachment visitTimingAttachement in PEMRBusinessLogic.ActivePEMRObject
							.List_VisitTiming_Attachment)
						{
							PatientAttachment_cu patientAttachment = PatientAttachment_cu.ItemsList.Find(item =>
								Convert.ToInt32(item.ID)
									.Equals(Convert.ToInt32(visitTimingAttachement.PatientAttachement_CU_ID)) &&
								Convert.ToInt32(item.ImageType_P_ID).Equals(Convert.ToInt32(ImageType_P_ID)));
							if (patientAttachment != null)
							{
								if (ImagesList == null)
									ImagesList = new List<PatientAttachment_cu>();
								ImagesList.Add(patientAttachment);
							}
						}

					break;
			}

			CommonViewsActions.FillListBoxControl(lstImageNames, ImagesList, "ImageName");
			if (ImagesList == null || ImagesList.Count == 0)
				pictureEdit1.Image = Properties.Resources.TopLogin;
			lstImageNames.Refresh();
		}

		private void chkPersonalPhoto_CheckedChanged(object sender, EventArgs e)
		{
			ImagesList = null;
			switch (ActiveScanningMode)
			{
				case ScanningMode.Regular:
					if (ActiveSelectedPatient != null)
						ImagesList = PatientAttachment_cu.ItemsList.FindAll(item =>
							Convert.ToInt32(item.Patient_CU_ID)
								.Equals(Convert.ToInt32(ActiveSelectedPatient.Person_CU_ID)) &&
							Convert.ToInt32(item.ImageType_P_ID).Equals(Convert.ToInt32(ImageType_P_ID)));
					break;
				case ScanningMode.MedicalVisit:
					if (PEMRBusinessLogic.ActivePEMRObject != null && PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Attachment != null)
						foreach (VisitTiming_Attachment visitTimingAttachement in PEMRBusinessLogic.ActivePEMRObject
							.List_VisitTiming_Attachment)
						{
							PatientAttachment_cu patientAttachment = PatientAttachment_cu.ItemsList.Find(item =>
								Convert.ToInt32(item.ID)
									.Equals(Convert.ToInt32(visitTimingAttachement.PatientAttachement_CU_ID)) &&
								Convert.ToInt32(item.ImageType_P_ID).Equals(Convert.ToInt32(ImageType_P_ID)));
							if (patientAttachment != null)
							{
								if (ImagesList == null)
									ImagesList = new List<PatientAttachment_cu>();
								ImagesList.Add(patientAttachment);
							}
						}

					break;
			}

			CommonViewsActions.FillListBoxControl(lstImageNames, ImagesList, "ImageName");
			if (ImagesList == null || ImagesList.Count == 0)
				pictureEdit1.Image = Properties.Resources.TopLogin;
			lstImageNames.Refresh();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			if (ParentForm != null)
				ParentForm.Close();
		}

		private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
		{
			chkSelectAll.Image = chkSelectAll.Checked
				? Properties.Resources.AcceptIcon_16_02
				: Properties.Resources.RejectIcon_16_01;
			ImagesList = null;

			if (chkSelectAll.Checked)
			{
				OverridenScanningMode = ActiveScanningMode;
				ActiveScanningMode = ScanningMode.Regular;
				if (ImagesList == null)
					ImagesList = new List<PatientAttachment_cu>();
				ImagesList = PatientAttachment_cu.ItemsList.FindAll(item =>
					Convert.ToInt32(item.Patient_CU_ID)
						.Equals(Convert.ToInt32(ActiveSelectedPatient.Person_CU_ID)) &&
					Convert.ToInt32(item.ImageType_P_ID).Equals(Convert.ToInt32(ImageType_P_ID)));
			}
			else
			{
				ActiveScanningMode = OverridenScanningMode;
				if (PEMRBusinessLogic.ActivePEMRObject != null &&
					PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Attachment != null)
					foreach (VisitTiming_Attachment visitTimingAttachement in PEMRBusinessLogic.ActivePEMRObject
						.List_VisitTiming_Attachment)
					{
						PatientAttachment_cu patientAttachment = PatientAttachment_cu.ItemsList.Find(item =>
							Convert.ToInt32(item.ID)
								.Equals(Convert.ToInt32(visitTimingAttachement.PatientAttachement_CU_ID)));
						if (patientAttachment != null)
						{
							if (ImagesList == null)
								ImagesList = new List<PatientAttachment_cu>();
							ImagesList.Add(patientAttachment);
						}
					}
			}

			CommonViewsActions.FillListBoxControl(lstImageNames, ImagesList, "ImageName");
			lstImageNames.Refresh();
		}

		public void EnableTopMenu(bool isEnabled)
		{
			btnClose.Enabled = isEnabled;
			btnDelete.Enabled = isEnabled;
			btnScanner.Enabled = isEnabled;
		}
	}
}
