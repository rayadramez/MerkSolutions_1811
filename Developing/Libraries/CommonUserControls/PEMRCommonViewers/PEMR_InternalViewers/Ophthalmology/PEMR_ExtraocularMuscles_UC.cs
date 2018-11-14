using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using CommonUserControls.PEMRCommonViewers.PEMR_Interfaces;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.PEMRBusinessLogic;
using MVCBusinessLogicLibrary.BaseViewers;

namespace CommonUserControls.PEMRCommonViewers.PEMR_InternalViewers.Ophthalmology
{
	public partial class PEMR_ExtraocularMuscles_UC : UserControl, IPEMR_Viewer, IPEMR_EOMSign, IPEMR_EOMReading
	{
		private List<SegmentSign_cu> TempEOMSignListToBeAdded_OD { get; set; }
		private List<SegmentSign_cu> TempEOMSignListToBeAdded_OS { get; set; }
		public static SegmentSign_cu SelectedSegmentSignFromSearch_OD { get; set; }
		public static SegmentSign_cu SelectedSegmentSignFromSearch_OS { get; set; }

		public FullScreenMode FullScreenMode { get; set; }
		private PEMR_ExtraocularMuscles_UC _pemrEOM;
		private VisitTiming_MainEOMSign _mainEOMSign = null;
		private VisitTiming_EOMSign _visitTimingEOM = null;
		private Control ParentControl { get; set; }
		public ReadingsMode ReadingsMode { get; set; }

		public GetPreviousVisitTiming_EOMReading_Result Selected_VisitTiming_EOMReading_Result { get; set; }

		public PEMR_ExtraocularMuscles_UC()
		{
			InitializeComponent();
			ParentControl = Parent;
			ClearControls(false);
			FillControls();
			CommonViewsActions.Decorate(spnSR_OD, spnSR_OS, spnLR_OD, spnLR_OS, spnIR_OD, spnIR_OS, spnIO_OD, spnIO_OS,
				spnMR_OD, spnMR_OS, spnSO_OD, spnSO_OS, lkeEOMSignCategory_OD, lkeEOMSignCategory_OS);
		}

		private void PEMR_ExtraocularMuscles_UC_Load(object sender, System.EventArgs e)
		{

		}

		public void Initialize(ReadingsMode readingMode,
			GetPreviousVisitTiming_EOMReading_Result previousEOMReading)
		{
			lstEOMSign_OD.SelectedIndex = -1;
			lstEOMSign_OS.SelectedIndex = -1;
			FullScreenMode = FullScreenMode.NotFullScreen;
			txtReccommednations_OD.EnterMoveNextControl = false;

			ReadingsMode = readingMode;
			switch (readingMode)
			{
				case ReadingsMode.ViewingActiveAllReadings:
					lytGroup_AllReadings.Visibility = LayoutVisibility.Always;
					lytGroup_AllReadings.Expanded = true;
					lytGroup_ReadingParent.Visibility = lyt_Cancel.Visibility =
						lyt_Add.Visibility = emptySpaceItem36.Visibility =
							emptySpaceItem15.Visibility = LayoutVisibility.Never;
					emptySpaceItem2.Visibility = lytNewReadings.Visibility = LayoutVisibility.Always;
					btnAllReadings_Click(null, null);
					tabSigns.PageVisible = true;
					tabMore.PageVisible = true;
					break;
				case ReadingsMode.ViewingPreviousReadings:
					Selected_VisitTiming_EOMReading_Result = previousEOMReading;
					btnNewReading.Text = "Active Reading";
					btnAllReadings.Text = "Previous Readings";
					emptySpaceItem2.Visibility = lytNewReadings.Visibility = LayoutVisibility.Never;
					ReadyForNewOrEditing(false);
					SetControlsValues();
					lyt_Cancel.Visibility = lyt_Add.Visibility = emptySpaceItem36.Visibility = LayoutVisibility.Never;
					tabSigns.PageVisible = false;
					tabMore.PageVisible = false;
					break;
			}

			if (PEMRBusinessLogic.ActivePEMRObject != null)
			{
				List<GetPreviousVisitTiming_EOMReading_Result> list =
					PEMRBusinessLogic.GetPrevious_VisitTiming_EOMReading_Result(
						PEMRBusinessLogic.ActivePEMRObject.Active_Patient.ID, dtSearchFrom.EditValue,
						dtSearchTo.EditValue);
				if (list != null)
					list = list.OrderByDescending(item => item.TakenDateTime).ToList();
				grdControl.DataSource = list;
				grdControl.RefreshDataSource();
				SetToolTip(list);
			}

			if (PEMRBusinessLogic.ActivePEMRObject == null ||
			    PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainEOMSign == null ||
			     PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainEOMSign.Count == 0 ||
			     PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_EOMSign == null ||
			     PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_EOMSign.Count == 0)
				return;

			txtReccommednations_OD.EditValue = PEMRBusinessLogic.ActivePEMRObject
				.List_VisitTiming_MainEOMSign[0].GeneralDescription_OD;
			txtReccommednations_OS.EditValue = PEMRBusinessLogic.ActivePEMRObject
				.List_VisitTiming_MainEOMSign[0].GeneralDescription_OS;

			AddedEOMSign_OD = null;
			AddedEOMSign_OS = null;
			foreach (VisitTiming_EOMSign visitTimingAnterior in PEMRBusinessLogic.ActivePEMRObject
				.List_VisitTiming_EOMSign.FindAll(item =>
					!Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.Removed))))
			{
				SegmentSign_cu segmentSign = SegmentSign_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(visitTimingAnterior.SegmentSign_CU_ID)));
				if (segmentSign != null)
				{
					DB_EyeType_p eyeTpe = (DB_EyeType_p)visitTimingAnterior.Eye_P_ID;
					switch (eyeTpe)
					{
						case DB_EyeType_p.OD:
							if (AddedEOMSign_OD == null)
								AddedEOMSign_OD = new List<SegmentSign_cu>();
							AddedEOMSign_OD.Add(segmentSign);
							break;
						case DB_EyeType_p.OS:
							if (AddedEOMSign_OS == null)
								AddedEOMSign_OS = new List<SegmentSign_cu>();
							AddedEOMSign_OS.Add(segmentSign);
							break;
					}
				}
			}

			ClearControls(false);
			CommonViewsActions.FillListBoxControl(lstAddedEOMSign_OD, AddedEOMSign_OD);
			CommonViewsActions.FillListBoxControl(lstAddedEOMSign_OS, AddedEOMSign_OS);
			lstAddedEOMSign_OD.Refresh();
			lstAddedEOMSign_OS.Refresh();
			SetCount_OD();
			SetCount_OS();

			PEMRBusinessLogic.PEMR_EOMSign = this;
		}

		public void SetCount_OD()
		{
			if (AddedEOMSign_OD != null && AddedEOMSign_OD.Count > 0)
				lytAddedSign_OD.Text = "Added Signs" + " (" + AddedEOMSign_OD.Count + ")";
			else
				lytAddedSign_OD.Text = "Added Sign" + "(0)";
		}

		public void SetCount_OS()
		{
			if (AddedEOMSign_OS != null && AddedEOMSign_OS.Count > 0)
				lytAddedSign_OS.Text = "Added Signs" + " (" + AddedEOMSign_OS.Count + ")";
			else
				lytAddedSign_OS.Text = "Added Sign" + "(0)";
		}

		public void SetControlsValues()
		{
			switch (ReadingsMode)
			{
				case ReadingsMode.ViewingPreviousReadings:
					if (Selected_VisitTiming_EOMReading_Result == null)
						return;
					TakenDateTime = Selected_VisitTiming_EOMReading_Result.TakenDateTime;
					SR_OD = Selected_VisitTiming_EOMReading_Result.SR_OD;
					SR_OS = Selected_VisitTiming_EOMReading_Result.SR_OS;
					LR_OD = Selected_VisitTiming_EOMReading_Result.LR_OD;
					LR_OS = Selected_VisitTiming_EOMReading_Result.LR_OS;
					IR_OD = Selected_VisitTiming_EOMReading_Result.IR_OD;
					IR_OS = Selected_VisitTiming_EOMReading_Result.IR_OS;
					IO_OD = Selected_VisitTiming_EOMReading_Result.IO_OD;
					IO_OS = Selected_VisitTiming_EOMReading_Result.IO_OS;
					MR_OD = Selected_VisitTiming_EOMReading_Result.MR_OD;
					MR_OS = Selected_VisitTiming_EOMReading_Result.MR_OS;
					SO_OD = Selected_VisitTiming_EOMReading_Result.SO_OD;
					SO_OS = Selected_VisitTiming_EOMReading_Result.SO_OS;
					break;
			}
		}

		public void ReadyForNewOrEditing(bool enableControls)
		{
			lytGroup_AllReadings.Visibility = LayoutVisibility.Never;
			lytGroup_AllReadings.Expanded = false;
			lytGroup_ReadingParent.Visibility = LayoutVisibility.Always;
			ClearControls(false);
			if (ReadingsMode != ReadingsMode.ViewingPreviousReadings)
				ReadingsMode = ReadingsMode.CreateNewReading;
			FillControls();
			takenDate.Properties.ReadOnly = takenTime.Properties.ReadOnly = spnSR_OD.Properties.ReadOnly =
				spnLR_OD.Properties.ReadOnly = spnIR_OD.Properties.ReadOnly =
					spnIO_OD.Properties.ReadOnly = spnMR_OD.Properties.ReadOnly =
						spnSO_OD.Properties.ReadOnly = spnSR_OS.Properties.ReadOnly =
							spnLR_OS.Properties.ReadOnly = spnIR_OS.Properties.ReadOnly =
								spnIO_OS.Properties.ReadOnly = spnMR_OS.Properties.ReadOnly =
									spnSO_OS.Properties.ReadOnly = !enableControls;
			emptySpaceItem36.Visibility = lyt_Cancel.Visibility = lyt_Add.Visibility = LayoutVisibility.Always;
		}

		public void SetToolTip(List<GetPreviousVisitTiming_EOMReading_Result> list)
		{
			string sr_OD = "";
			string sr_OS = "";
			string lr_OD = "";
			string lr_OS = "";
			string io_OD = "";
			string io_OS = "";
			string mr_OD = "";
			string mr_OS = "";
			string so_OD = "";
			string so_OS = "";
			foreach (GetPreviousVisitTiming_EOMReading_Result result in list)
			{
				sr_OD += result.SR_OD + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
				sr_OS += result.SR_OS + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
				lr_OD += result.LR_OD + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
				lr_OS += result.LR_OS + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
				io_OD += result.IO_OD + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
				io_OS += result.IO_OS + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
				mr_OD += result.MR_OD + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
				mr_OS += result.MR_OS + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
				so_OD += result.SO_OD + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
				so_OS += result.SO_OS + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
			}
			spnSR_OD.ToolTip = sr_OD;
			spnSR_OS.ToolTip = sr_OS;
			spnLR_OD.ToolTip = lr_OD;
			spnLR_OS.ToolTip = lr_OS;
			spnIR_OD.ToolTip = io_OD;
			spnIR_OS.ToolTip = io_OS;
			spnIO_OD.ToolTip = io_OD;
			spnIO_OS.ToolTip = io_OS;
			spnMR_OD.ToolTip = mr_OD;
			spnMR_OS.ToolTip = mr_OS;
			spnSO_OD.ToolTip = so_OD;
			spnSO_OS.ToolTip = so_OS;
		}

		#region Implementation of IPEMR_Viewer

		public void ClearControls(bool clearAll)
		{
			lstAddedEOMSign_OD.DataSource = null;
			lstAddedEOMSign_OS.DataSource = null;
			if (clearAll)
			{
				txtReccommednations_OD.EditValue = null;
				txtReccommednations_OS.EditValue = null;
				lytAddedSign_OD.Text = lytAddedSign_OS.Text = "Added Sign" + "(0)";

				ReadingsMode = ReadingsMode.ViewingActiveAllReadings;
				lytGroup_AllReadings.Visibility = LayoutVisibility.Always;
				lytGroup_AllReadings.Expanded = true;
				lytGroup_ReadingParent.Visibility = lyt_Cancel.Visibility = emptySpaceItem4.Visibility =
					lyt_Add.Visibility = emptySpaceItem15.Visibility = LayoutVisibility.Never;
				btnAllReadings_Click(null, null);
			}

			takenDate.EditValue = null;
			takenTime.EditValue = null;

			SR_OD = null;
			SR_OS = null;
			LR_OD = null;
			LR_OS = null;
			IR_OD = null;
			IR_OS = null;
			IO_OD = null;
			IO_OS = null;
			MR_OD = null;
			MR_OS = null;
			SO_OD = null;
			SO_OS = null;
		}

		public void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeEOMSignCategory_OD,
				SegmentSignCategory_cu.ItemsList.FindAll(item =>
					Convert.ToInt32(item.SegmentType_P_ID).Equals(Convert.ToInt32(DB_SegmentSignType.EOM))));
			CommonViewsActions.FillGridlookupEdit(lkeEOMSignCategory_OS,
				SegmentSignCategory_cu.ItemsList.FindAll(item =>
					Convert.ToInt32(item.SegmentType_P_ID).Equals(Convert.ToInt32(DB_SegmentSignType.EOM))));
			SetCount_OD();
			SetCount_OS();
			TakenDateTime = DateTime.Now;
		}

		#endregion

		#region Controls Events

		#region Buttons Events

		private void btnAddList_OD_Click(object sender, EventArgs e)
		{
			if (TempEOMSignListToBeAdded_OD == null || TempEOMSignListToBeAdded_OD.Count == 0)
			{
				XtraMessageBox.Show(
					"You should select at least one Anterior Segment Sign to be added." + "\r\n" +
					"You can select one or more Anterior Segment Sign to add", "Note", MessageBoxButtons.OK,
					MessageBoxIcon.Hand);
				return;
			}

			if (AddedEOMSign_OD == null)
				AddedEOMSign_OD = new List<SegmentSign_cu>();
			if (TempEOMSignListToBeAdded_OD != null && TempEOMSignListToBeAdded_OD.Count > 0)
			{
				foreach (SegmentSign_cu segmentSign in TempEOMSignListToBeAdded_OD)
				{
					if (!AddedEOMSign_OD.Exists(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(segmentSign.ID))))
					{
						AddedEOMSign_OD.Add(segmentSign);
						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainEOMSign == null)
						{
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainEOMSign =
								new List<VisitTiming_MainEOMSign>();
							_mainEOMSign = PEMRBusinessLogic.CreateNew_VisitTiming_MainEOMSign(
								FurtherDetails_OD, FurtherDetails_OS,
								ApplicationStaticConfiguration.PEMRSavingMode);
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainEOMSign.Add(
								_mainEOMSign);
						}

						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainEOMSign[0] != null)
						{
							if (!string.IsNullOrEmpty(txtReccommednations_OD.Text) ||
								!string.IsNullOrWhiteSpace(txtReccommednations_OD.Text))
								PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainEOMSign[0].GeneralDescription_OD
									= txtReccommednations_OD.Text;
							else
								PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainEOMSign[0]
									.GeneralDescription_OD = null;
						}

						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_EOMSign == null)
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_EOMSign =
								new List<VisitTiming_EOMSign>();
						_visitTimingEOM = PEMRBusinessLogic.CreateNew_VisitTiming_EOMSign(
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainEOMSign[0], segmentSign,
							DB_EyeType_p.OD, ApplicationStaticConfiguration.ActiveLoginUser.Person_CU_ID,
							DB_PEMRSavingMode.SaveImmediately);
						if (_visitTimingEOM != null)
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_EOMSign.Add(
								_visitTimingEOM);
					}
				}
			}

			TempEOMSignListToBeAdded_OD = null;
			CommonViewsActions.FillListBoxControl(lstAddedEOMSign_OD, AddedEOMSign_OD);
			lstAddedEOMSign_OD.Refresh();
			SetCount_OD();
			SetCount_OS();
			lstEOMSign_OD.SelectedIndex = -1;
		}

		private void btnRemoveFromList_OD_Click(object sender, EventArgs e)
		{
			if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_EOMSign == null ||
				PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_EOMSign.Count == 0)
				return;

			if (AddedEOMSign_OD == null || AddedEOMSign_OD.Count == 0)
			{
				XtraMessageBox.Show(
					"You should select at least one Segment Sign to be remove." + "\r\n" +
					"You can select one or more Segment Signs to remove", "Note", MessageBoxButtons.OK, MessageBoxIcon.Hand,
					MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
				return;
			}

			foreach (object selectedItem in lstAddedEOMSign_OD.SelectedItems)
			{
				if (selectedItem is SegmentSign_cu)
				{
					SegmentSign_cu segment = SegmentSign_cu.ItemsList.Find(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID)));
					if (segment != null)
					{
						if (AddedEOMSign_OD.Exists(item =>
							Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID))))
							AddedEOMSign_OD.Remove(segment);
						VisitTiming_EOMSign visitTimingEOM =
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_EOMSign.Find(item =>
								Convert.ToInt32(item.SegmentSign_CU_ID).Equals(Convert.ToInt32(segment.ID)) &&
								Convert.ToInt32(item.Eye_P_ID).Equals(Convert.ToInt32(DB_EyeType_p.OD)));
						if (visitTimingEOM != null)
							visitTimingEOM.PEMRElementStatus = PEMRElementStatus.Removed;
						PEMRBusinessLogic.Remove_VisitTiming_EOMSign(visitTimingEOM);
					}
				}
			}

			TempEOMSignListToBeAdded_OD = null;
			CommonViewsActions.FillListBoxControl(lstAddedEOMSign_OD, AddedEOMSign_OD);
			lstAddedEOMSign_OD.Refresh();
			SetCount_OD();
			SetCount_OS();
			lstEOMSign_OD.SelectedIndex = -1;
		}

		private void btnCopyToOS_Click(object sender, EventArgs e)
		{
			if (AddedEOMSign_OD == null || AddedEOMSign_OD.Count == 0)
				return;
			TempEOMSignListToBeAdded_OS = null;
			if (AddedEOMSign_OS == null)
				AddedEOMSign_OS = new List<SegmentSign_cu>();
			TempEOMSignListToBeAdded_OS = AddedEOMSign_OD;
			btnAddList_OS_Click(null, null);
		}

		private void btnAddList_OS_Click(object sender, EventArgs e)
		{
			if (TempEOMSignListToBeAdded_OS == null || TempEOMSignListToBeAdded_OS.Count == 0)
			{
				XtraMessageBox.Show(
					"You should select at least one Anterior Segment Sign to be added." + "\r\n" +
					"You can select one or more Anterior Segment Sign to add", "Note", MessageBoxButtons.OK,
					MessageBoxIcon.Hand);
				return;
			}

			if (AddedEOMSign_OS == null)
				AddedEOMSign_OS = new List<SegmentSign_cu>();
			if (TempEOMSignListToBeAdded_OS != null && TempEOMSignListToBeAdded_OS.Count > 0)
			{
				foreach (SegmentSign_cu segmentSign in TempEOMSignListToBeAdded_OS)
				{
					if (!AddedEOMSign_OS.Exists(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(segmentSign.ID))))
					{
						AddedEOMSign_OS.Add(segmentSign);
						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainEOMSign == null)
						{
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainEOMSign =
								new List<VisitTiming_MainEOMSign>();
							_mainEOMSign = PEMRBusinessLogic.CreateNew_VisitTiming_MainEOMSign(
								FurtherDetails_OD, FurtherDetails_OS,
								ApplicationStaticConfiguration.PEMRSavingMode);
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainEOMSign.Add(
								_mainEOMSign);
						}

						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainEOMSign[0] != null)
						{
							if (!string.IsNullOrEmpty(txtReccommednations_OD.Text) ||
								!string.IsNullOrWhiteSpace(txtReccommednations_OD.Text))
								PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainEOMSign[0].GeneralDescription_OD
									= txtReccommednations_OD.Text;
							else
								PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainEOMSign[0]
									.GeneralDescription_OD = null;
						}

						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_EOMSign == null)
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_EOMSign =
								new List<VisitTiming_EOMSign>();
						_visitTimingEOM = PEMRBusinessLogic.CreateNew_VisitTiming_EOMSign(
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainEOMSign[0], segmentSign,
							DB_EyeType_p.OS, ApplicationStaticConfiguration.ActiveLoginUser.Person_CU_ID,
							DB_PEMRSavingMode.SaveImmediately);
						if (_visitTimingEOM != null)
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_EOMSign.Add(
								_visitTimingEOM);
					}
				}
			}

			TempEOMSignListToBeAdded_OS = null;
			CommonViewsActions.FillListBoxControl(lstAddedEOMSign_OS, AddedEOMSign_OS);
			lstAddedEOMSign_OS.Refresh();
			SetCount_OS();
			SetCount_OS();
			lstEOMSign_OS.SelectedIndex = -1;
		}

		private void btnRemoveFromList_OS_Click(object sender, EventArgs e)
		{
			if (AddedEOMSign_OS == null || AddedEOMSign_OS.Count == 0)
			{
				XtraMessageBox.Show(
					"You should select at least one Segment Sign to be remove." + "\r\n" +
					"You can select one or more Segment Signs to remove", "Note", MessageBoxButtons.OK, MessageBoxIcon.Hand,
					MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
				return;
			}

			foreach (object selectedItem in lstAddedEOMSign_OS.SelectedItems)
			{
				if (selectedItem is SegmentSign_cu)
				{
					SegmentSign_cu segment = SegmentSign_cu.ItemsList.Find(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID)));
					if (segment != null)
					{
						if (AddedEOMSign_OS.Exists(item =>
							Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID))))
							AddedEOMSign_OS.Remove(segment);
						VisitTiming_EOMSign visitTimingEOM =
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_EOMSign.Find(item =>
								Convert.ToInt32(item.SegmentSign_CU_ID).Equals(Convert.ToInt32(segment.ID)) &&
								Convert.ToInt32(item.Eye_P_ID).Equals(Convert.ToInt32(DB_EyeType_p.OS)));
						if (visitTimingEOM != null)
							visitTimingEOM.PEMRElementStatus = PEMRElementStatus.Removed;
						PEMRBusinessLogic.Remove_VisitTiming_EOMSign(visitTimingEOM);
					}
				}
			}

			TempEOMSignListToBeAdded_OS = null;
			CommonViewsActions.FillListBoxControl(lstAddedEOMSign_OS, AddedEOMSign_OS);
			lstAddedEOMSign_OS.Refresh();
			SetCount_OS();
			SetCount_OS();
			lstEOMSign_OS.SelectedIndex = -1;
		}

		private void btnCopyToOD_Click(object sender, EventArgs e)
		{
			if (AddedEOMSign_OS == null || AddedEOMSign_OS.Count == 0)
				return;
			TempEOMSignListToBeAdded_OD = null;
			if (AddedEOMSign_OD == null)
				AddedEOMSign_OD = new List<SegmentSign_cu>();
			TempEOMSignListToBeAdded_OD = AddedEOMSign_OS;
			btnAddList_OD_Click(null, null);
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (PEMRBusinessLogic.ActivePEMRObject != null)
				if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign == null ||
					PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign.Count == 0)
				{
					_mainEOMSign = PEMRBusinessLogic.CreateNew_VisitTiming_MainEOMSign(FurtherDetails_OD, FurtherDetails_OD,
						ApplicationStaticConfiguration.PEMRSavingMode);
					if (_mainEOMSign == null)
						return;
					if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainEOMSign == null)
						PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainEOMSign = new List<VisitTiming_MainEOMSign>();
					PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainEOMSign.Add(_mainEOMSign);
					XtraMessageBox.Show("Saved Successfully", "Saved", MessageBoxButtons.OK,
						MessageBoxIcon.Information);
				}
				else
				{
					if (_mainEOMSign == null)
						return;
					if (PEMRBusinessLogic.Update_VisitTiming_MainEOMSign(this, _mainEOMSign))
						XtraMessageBox.Show("Saved Successfully", "Saved", MessageBoxButtons.OK,
							MessageBoxIcon.Information);
				}
		}

		private void btnNewReading_Click(object sender, EventArgs e)
		{
			switch (ReadingsMode)
			{
				case ReadingsMode.ViewingActiveAllReadings:
					ReadyForNewOrEditing(true);
					break;
				case ReadingsMode.ViewingPreviousReadings:
					ReadyForNewOrEditing(false);
					break;
			}
		}

		private void btnAllReadings_Click(object sender, EventArgs e)
		{
			DialogResult result;
			switch (ReadingsMode)
			{
				case ReadingsMode.CreateNewReading:
					result = XtraMessageBox.Show("Do you want to Cancel this Reading ?", "Note",
						MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
					switch (result)
					{
						case DialogResult.Yes:
							ReadingsMode = ReadingsMode.ViewingActiveAllReadings;
							lytGroup_AllReadings.Visibility = LayoutVisibility.Always;
							lytGroup_AllReadings.Expanded = true;
							lytGroup_ReadingParent.Visibility = lyt_Cancel.Visibility =
								lyt_Add.Visibility = emptySpaceItem36.Visibility =
									 emptySpaceItem4.Visibility = LayoutVisibility.Never;
							List<GetPreviousVisitTiming_EOMReading_Result> list =
								PEMRBusinessLogic.GetPrevious_VisitTiming_EOMReading_Result(
									PEMRBusinessLogic.ActivePEMRObject.Active_Patient.ID, dtSearchFrom.EditValue,
									dtSearchTo.EditValue);
							grdControl.DataSource = list;
							grdControl.RefreshDataSource();
							SetToolTip(list);
							break;
					}
					break;
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			DialogResult result = XtraMessageBox.Show("Do you to add this Readings?", "Note", MessageBoxButtons.YesNo,
				MessageBoxIcon.Exclamation);
			switch (result)
			{
				case DialogResult.Yes:
					VisitTiming_EOMReading visitTiming_EOMReading =
						PEMRBusinessLogic.CreateNew_VisitTiming_EOMReading(this,
							ApplicationStaticConfiguration.PEMRSavingMode);
					if (visitTiming_EOMReading != null)
					{
						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_EOMReading == null)
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_EOMReading =
								new List<VisitTiming_EOMReading>();
						PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_EOMReading.Add(visitTiming_EOMReading);
						List<GetPreviousVisitTiming_EOMReading_Result> list =
							PEMRBusinessLogic.GetPrevious_VisitTiming_EOMReading_Result(
								PEMRBusinessLogic.ActivePEMRObject.Active_Patient.ID, dtSearchFrom.EditValue,
								dtSearchTo.EditValue);
						if (list != null)
							list = list.OrderByDescending(item => item.TakenDateTime).ToList();
						grdControl.DataSource = list;
						grdControl.RefreshDataSource();
						SetToolTip(list);
						ClearControls(true);
					}

					break;
			}
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			List<GetPreviousVisitTiming_EOMReading_Result> list =
				PEMRBusinessLogic.GetPrevious_VisitTiming_EOMReading_Result(
					PEMRBusinessLogic.ActivePEMRObject.Active_Patient.ID, dtSearchFrom.EditValue, dtSearchTo.EditValue);
			if (list != null)
				list = list.OrderByDescending(item => item.TakenDateTime).ToList();
			grdControl.DataSource = list;
			grdControl.RefreshDataSource();
			SetToolTip(list);
		}

		private void btnGraph_Click(object sender, EventArgs e)
		{
			PEMR_VisionRefractionGraph_UC graph = new PEMR_VisionRefractionGraph_UC();
			graph.Initialize(ReadingsType.EOMReading, PEMRBusinessLogic.ActivePEMRObject.Active_Patient.ID,
				dtSearchFrom.EditValue, dtSearchTo.EditValue);
			PopupBaseForm.ShowAsPopup(graph, this);
		}

		#endregion

		#region LookupEdit Events

		private void lkeEOMCategory_OD_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeEOMSignCategory_OD.EditValue == null)
				return;
			CommonViewsActions.FillListBoxControl(lstEOMSign_OD,
				PEMRBusinessLogic.GetSegmentSignsList(Convert.ToInt32(lkeEOMSignCategory_OD.EditValue)));
		}

		private void lkeEOMCategory_OS_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeEOMSignCategory_OS.EditValue == null)
				return;
			CommonViewsActions.FillListBoxControl(lstEOMSign_OS,
				PEMRBusinessLogic.GetSegmentSignsList(Convert.ToInt32(lkeEOMSignCategory_OS.EditValue)));
		}

		#endregion

		#region List Events

		private void lstEOM_OD_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstEOMSign_OD.SelectedItems == null || lstEOMSign_OD.SelectedItems.Count == 0)
				return;
			TempEOMSignListToBeAdded_OD = null;
			foreach (object selectedItem in lstEOMSign_OD.SelectedItems)
			{
				SegmentSign_cu diagnosis = SegmentSign_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID)));
				if (diagnosis != null)
				{
					if (TempEOMSignListToBeAdded_OD == null)
						TempEOMSignListToBeAdded_OD = new List<SegmentSign_cu>();
					if (!TempEOMSignListToBeAdded_OD.Exists(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID))))
						TempEOMSignListToBeAdded_OD.Add(diagnosis);
				}
			}
		}

		private void lstEOM_OD_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			btnAddList_OD_Click(null, null);
		}

		private void lstEOM_OS_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstEOMSign_OS.SelectedItems == null || lstEOMSign_OS.SelectedItems.Count == 0)
				return;
			TempEOMSignListToBeAdded_OS = null;
			foreach (object selectedItem in lstEOMSign_OS.SelectedItems)
			{
				SegmentSign_cu diagnosis = SegmentSign_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID)));
				if (diagnosis != null)
				{
					if (TempEOMSignListToBeAdded_OS == null)
						TempEOMSignListToBeAdded_OS = new List<SegmentSign_cu>();
					if (!TempEOMSignListToBeAdded_OS.Exists(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID))))
						TempEOMSignListToBeAdded_OS.Add(diagnosis);
				}
			}
		}

		private void lstEOM_OS_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			btnAddList_OS_Click(null, null);
		}

		private void lstAddedEOMSign_OD_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void lstAddedEOMSign_OS_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		#endregion

		#region Grid Events

		private void gridView2_DoubleClick(object sender, EventArgs e)
		{
			if (Selected_VisitTiming_EOMReading_Result == null)
				return;

			PEMR_VisionRefractionDetails_UC details = new PEMR_VisionRefractionDetails_UC();
			details.Initialize(Selected_VisitTiming_EOMReading_Result);
			ReadingsMode = ReadingsMode.ViewingPreviousReadings;
			PopupBaseForm.ShowAsPopup(details, this);
			ReadingsMode = ReadingsMode.ViewingActiveAllReadings;
		}

		private void gridView2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
		{
			Selected_VisitTiming_EOMReading_Result =
				CommonViewsActions.GetSelectedRowObject<GetPreviousVisitTiming_EOMReading_Result>(gridView2);
		}

		#endregion

		#endregion

		#region Implementation of IPEMR_EOMSign

		public object FurtherDetails_OD
		{
			get { return txtReccommednations_OD.EditValue; }
			set { txtReccommednations_OD.EditValue = value; }
		}

		public object FurtherDetails_OS
		{
			get { return txtReccommednations_OS.EditValue; }
			set { txtReccommednations_OS.EditValue = value; }
		}

		public List<SegmentSign_cu> AddedEOMSign_OD { get; set; }
		public List<SegmentSign_cu> AddedEOMSign_OS { get; set; }

		#endregion

		#region Implementation of IPEMR_EOMReading

		public object TakenDateTime
		{
			get { return takenTime.EditValue; }
			set
			{
				takenTime.EditValue = value;
				takenDate.EditValue = value;
			}
		}

		public object SR_OD
		{
			get { return spnSR_OD.EditValue; }
			set { spnSR_OD.EditValue = value; }
		}

		public object SR_OS
		{
			get { return spnSR_OS.EditValue; }
			set { spnSR_OS.EditValue = value; }
		}

		public object LR_OD
		{
			get { return spnLR_OD.EditValue; }
			set { spnLR_OD.EditValue = value; }
		}

		public object LR_OS
		{
			get { return spnLR_OS.EditValue; }
			set { spnLR_OS.EditValue = value; }
		}

		public object IR_OD
		{
			get { return spnIR_OD.EditValue; }
			set { spnIR_OD.EditValue = value; }
		}

		public object IR_OS
		{
			get { return spnIR_OS.EditValue; }
			set { spnIR_OS.EditValue = value; }
		}

		public object IO_OD
		{
			get { return spnIO_OD.EditValue; }
			set { spnIO_OD.EditValue = value; }
		}

		public object IO_OS
		{
			get { return spnIO_OS.EditValue; }
			set { spnIO_OS.EditValue = value; }
		}

		public object MR_OD
		{
			get { return spnMR_OD.EditValue; }
			set { spnMR_OD.EditValue = value; }
		}

		public object MR_OS
		{
			get { return spnMR_OS.EditValue; }
			set { spnMR_OS.EditValue = value; }
		}

		public object SO_OD
		{
			get { return spnSO_OD.EditValue; }
			set { spnSO_OD.EditValue = value; }
		}

		public object SO_OS
		{
			get { return spnSO_OS.EditValue; }
			set { spnSO_OS.EditValue = value; }
		}

		#endregion
	}
}
