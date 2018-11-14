using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using CommonUserControls.PEMRCommonViewers.PEMR_Interfaces;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.PEMRBusinessLogic;

namespace CommonUserControls.PEMRCommonViewers.PEMR_InternalViewers.Ophthalmology
{
	public partial class PEMR_AdnexaSegment_UC : UserControl, IPEMR_Viewer, IPEMR_Adnexa
	{
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

		public List<SegmentSign_cu> AddedAdnexaSegmentSign_OD { get; set; }
		public List<SegmentSign_cu> AddedAdnexaSegmentSign_OS { get; set; }

		private List<SegmentSign_cu> TempAdnexaSegmentSignListToBeAdded_OD { get; set; }
		private List<SegmentSign_cu> TempAdnexaSegmentSignListToBeAdded_OS { get; set; }

		public static SegmentSign_cu SelectedSegmentSignFromSearch_OD { get; set; }
		public static SegmentSign_cu SelectedSegmentSignFromSearch_OS { get; set; }

		public FullScreenMode FullScreenMode { get; set; }
		private PEMR_AdnexaSegment_UC _pemrAdnexaSegment;
		private VisitTiming_MainAdnexaSegmentSign _mainAdnexaSegmentSign = null;
		private VisitTiming_AdnexaSegmentSign _visitTimingAdnexaSegment = null;
		private Control ParentControl { get; set; }

		public PEMR_AdnexaSegment_UC()
		{
			InitializeComponent();
			CommonViewsActions.Decorate(lkeAdnexaSegmentCategory_OD, lkeAdnexaSegmentCategory_OS);
		}

		private void PEMR_AdnexaSegment_UC_Load(object sender, EventArgs e)
		{
			ParentControl = this.Parent;
			ClearControls(false);
			FillControls();
		}

		public void Initialize()
		{
			lstAdnexaSegment_OD.SelectedIndex = -1;
			lstAdnexaSegment_OS.SelectedIndex = -1;
			FullScreenMode = FullScreenMode.NotFullScreen;
			txtReccommednations_OD.EnterMoveNextControl = false;

			if (PEMRBusinessLogic.ActivePEMRObject == null ||
			    PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAdnexaSegmentSign == null ||
			    PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAdnexaSegmentSign.Count == 0 ||
			    PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_AdnexaSegmentSign == null ||
			    PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_AdnexaSegmentSign.Count == 0)
				return;
			txtReccommednations_OD.EditValue = PEMRBusinessLogic.ActivePEMRObject
				.List_VisitTiming_MainAdnexaSegmentSign[0].GeneralDescription_OD;
			txtReccommednations_OS.EditValue = PEMRBusinessLogic.ActivePEMRObject
				.List_VisitTiming_MainAdnexaSegmentSign[0].GeneralDescription_OS;

			AddedAdnexaSegmentSign_OD = null;
			AddedAdnexaSegmentSign_OS = null;
			foreach (VisitTiming_AdnexaSegmentSign visitTimingAdnexa in PEMRBusinessLogic.ActivePEMRObject
				.List_VisitTiming_AdnexaSegmentSign.FindAll(item =>
					!Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.Removed))))
			{
				SegmentSign_cu segmentSign = SegmentSign_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(visitTimingAdnexa.SegmentSign_CU_ID)));
				if (segmentSign != null)
				{
					DB_EyeType_p eyeTpe = (DB_EyeType_p)visitTimingAdnexa.Eye_P_ID;
					switch (eyeTpe)
					{
						case DB_EyeType_p.OD:
							if (AddedAdnexaSegmentSign_OD == null)
								AddedAdnexaSegmentSign_OD = new List<SegmentSign_cu>();
							AddedAdnexaSegmentSign_OD.Add(segmentSign);
							break;
						case DB_EyeType_p.OS:
							if (AddedAdnexaSegmentSign_OS == null)
								AddedAdnexaSegmentSign_OS = new List<SegmentSign_cu>();
							AddedAdnexaSegmentSign_OS.Add(segmentSign);
							break;
					}
				}
			}

			ClearControls(false);
			CommonViewsActions.FillListBoxControl(lstAddedAdnexaSegment_OD, AddedAdnexaSegmentSign_OD);
			CommonViewsActions.FillListBoxControl(lstAddedAdnexaSegment_OS, AddedAdnexaSegmentSign_OS);
			lstAddedAdnexaSegment_OD.Refresh();
			lstAddedAdnexaSegment_OS.Refresh();
			SetCount_OD();
			SetCount_OS();

			PEMRBusinessLogic.PEMR_Adnexa = this;
		}

		public void SetCount_OD()
		{
			if (AddedAdnexaSegmentSign_OD != null && AddedAdnexaSegmentSign_OD.Count > 0)
				lytAddedSign_OD.Text = "Added Signs" + " (" + AddedAdnexaSegmentSign_OD.Count + ")";
			else
				lytAddedSign_OD.Text = "Added Sign" + "(0)";
		}

		public void SetCount_OS()
		{
			if (AddedAdnexaSegmentSign_OS != null && AddedAdnexaSegmentSign_OS.Count > 0)
				lytAddedSign_OS.Text = "Added Signs" + " (" + AddedAdnexaSegmentSign_OS.Count + ")";
			else
				lytAddedSign_OS.Text = "Added Sign" + "(0)";
		}

		#region Implementation of IPEMR_Viewer

		public void ClearControls(bool clearAll)
		{
			lstAddedAdnexaSegment_OD.DataSource = null;
			lstAddedAdnexaSegment_OS.DataSource = null;
			if (clearAll)
			{
				txtReccommednations_OD.EditValue = null;
				txtReccommednations_OS.EditValue = null;
				lytAddedSign_OD.Text = lytAddedSign_OS.Text = "Added Sign" + "(0)";
			}
		}

		public void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeAdnexaSegmentCategory_OD,
				SegmentSignCategory_cu.ItemsList.FindAll(item =>
					Convert.ToInt32(item.SegmentType_P_ID).Equals(Convert.ToInt32(DB_SegmentSignType.Adnexa))));
			CommonViewsActions.FillGridlookupEdit(lkeAdnexaSegmentCategory_OS,
				SegmentSignCategory_cu.ItemsList.FindAll(item =>
					Convert.ToInt32(item.SegmentType_P_ID).Equals(Convert.ToInt32(DB_SegmentSignType.Adnexa))));
			SetCount_OD();
			SetCount_OS();
		}

		#endregion

		#region Controls Events

		#region Buttons Events

		private void btnAddList_OD_Click(object sender, EventArgs e)
		{
			if (TempAdnexaSegmentSignListToBeAdded_OD == null || TempAdnexaSegmentSignListToBeAdded_OD.Count == 0)
			{
				XtraMessageBox.Show(
					"You should select at least one Adnexa Segment Sign to be added." + "\r\n" +
					"You can select one or more Adnexa Segment Sign to add", "Note", MessageBoxButtons.OK,
					MessageBoxIcon.Hand);
				return;
			}

			if (AddedAdnexaSegmentSign_OD == null)
				AddedAdnexaSegmentSign_OD = new List<SegmentSign_cu>();
			if (TempAdnexaSegmentSignListToBeAdded_OD != null && TempAdnexaSegmentSignListToBeAdded_OD.Count > 0)
			{
				foreach (SegmentSign_cu segmentSign in TempAdnexaSegmentSignListToBeAdded_OD)
				{
					if (!AddedAdnexaSegmentSign_OD.Exists(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(segmentSign.ID))))
					{
						AddedAdnexaSegmentSign_OD.Add(segmentSign);
						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAdnexaSegmentSign == null)
						{
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAdnexaSegmentSign =
								new List<VisitTiming_MainAdnexaSegmentSign>();
							_mainAdnexaSegmentSign = PEMRBusinessLogic.CreateNew_VisitTiming_MainAdnexaSegmentSign(
								FurtherDetails_OD, FurtherDetails_OD,
								ApplicationStaticConfiguration.PEMRSavingMode);
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAdnexaSegmentSign.Add(
								_mainAdnexaSegmentSign);
						}

						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAdnexaSegmentSign[0] != null)
						{
							if (!string.IsNullOrEmpty(txtReccommednations_OD.Text) ||
								!string.IsNullOrWhiteSpace(txtReccommednations_OD.Text))
								PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAdnexaSegmentSign[0].GeneralDescription_OD
									= txtReccommednations_OD.Text;
							else
								PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAdnexaSegmentSign[0]
									.GeneralDescription_OD = null;
						}

						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_AdnexaSegmentSign == null)
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_AdnexaSegmentSign =
								new List<VisitTiming_AdnexaSegmentSign>();
						_visitTimingAdnexaSegment = PEMRBusinessLogic.CreateNew_VisitTiming_AdnexaSegmentSign(
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAdnexaSegmentSign[0], segmentSign,
							DB_EyeType_p.OD, DB_PEMRSavingMode.SaveImmediately);
						if (_visitTimingAdnexaSegment != null)
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_AdnexaSegmentSign.Add(
								_visitTimingAdnexaSegment);
					}
				}
			}

			TempAdnexaSegmentSignListToBeAdded_OD = null;
			CommonViewsActions.FillListBoxControl(lstAddedAdnexaSegment_OD, AddedAdnexaSegmentSign_OD);
			lstAddedAdnexaSegment_OD.Refresh();
			SetCount_OD();
			SetCount_OS();
			lstAdnexaSegment_OD.SelectedIndex = -1;
		}

		private void btnRemoveFromList_OD_Click(object sender, EventArgs e)
		{
			if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_AdnexaSegmentSign == null ||
				PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_AdnexaSegmentSign.Count == 0)
				return;

			if (AddedAdnexaSegmentSign_OD == null || AddedAdnexaSegmentSign_OD.Count == 0)
			{
				XtraMessageBox.Show(
					"You should select at least one Segment Sign to be remove." + "\r\n" +
					"You can select one or more Segment Signs to remove", "Note", MessageBoxButtons.OK, MessageBoxIcon.Hand,
					MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
				return;
			}

			foreach (object selectedItem in lstAddedAdnexaSegment_OD.SelectedItems)
			{
				if (selectedItem is SegmentSign_cu)
				{
					SegmentSign_cu segment = SegmentSign_cu.ItemsList.Find(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID)));
					if (segment != null)
					{
						if (AddedAdnexaSegmentSign_OD.Exists(item =>
							Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID))))
							AddedAdnexaSegmentSign_OD.Remove(segment);
						VisitTiming_AdnexaSegmentSign visitTimingAdnexaSegment =
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_AdnexaSegmentSign.Find(item =>
								Convert.ToInt32(item.SegmentSign_CU_ID).Equals(Convert.ToInt32(segment.ID)) &&
								Convert.ToInt32(item.Eye_P_ID).Equals(Convert.ToInt32(DB_EyeType_p.OD)));
						if (visitTimingAdnexaSegment != null)
							visitTimingAdnexaSegment.PEMRElementStatus = PEMRElementStatus.Removed;
						PEMRBusinessLogic.Remove_VisitTiming_AdnexaSegmentSign(visitTimingAdnexaSegment);
					}
				}
			}

			TempAdnexaSegmentSignListToBeAdded_OD = null;
			CommonViewsActions.FillListBoxControl(lstAddedAdnexaSegment_OD, AddedAdnexaSegmentSign_OD);
			lstAddedAdnexaSegment_OD.Refresh();
			SetCount_OD();
			SetCount_OS();
			lstAdnexaSegment_OD.SelectedIndex = -1;
		}

		private void btnCopyToOS_Click(object sender, EventArgs e)
		{
			if (AddedAdnexaSegmentSign_OD == null || AddedAdnexaSegmentSign_OD.Count == 0)
				return;
			TempAdnexaSegmentSignListToBeAdded_OS = null;
			if (AddedAdnexaSegmentSign_OS == null)
				AddedAdnexaSegmentSign_OS = new List<SegmentSign_cu>();
			TempAdnexaSegmentSignListToBeAdded_OS = AddedAdnexaSegmentSign_OD;
			btnAddList_OS_Click(null, null);
		}

		private void btnAddList_OS_Click(object sender, EventArgs e)
		{
			if (TempAdnexaSegmentSignListToBeAdded_OS == null || TempAdnexaSegmentSignListToBeAdded_OS.Count == 0)
			{
				XtraMessageBox.Show(
					"You should select at least one Adnexa Segment Sign to be added." + "\r\n" +
					"You can select one or more Adnexa Segment Sign to add", "Note", MessageBoxButtons.OK,
					MessageBoxIcon.Hand);
				return;
			}

			if (AddedAdnexaSegmentSign_OS == null)
				AddedAdnexaSegmentSign_OS = new List<SegmentSign_cu>();
			if (TempAdnexaSegmentSignListToBeAdded_OS != null && TempAdnexaSegmentSignListToBeAdded_OS.Count > 0)
			{
				foreach (SegmentSign_cu segmentSign in TempAdnexaSegmentSignListToBeAdded_OS)
				{
					if (!AddedAdnexaSegmentSign_OS.Exists(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(segmentSign.ID))))
					{
						AddedAdnexaSegmentSign_OS.Add(segmentSign);
						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAdnexaSegmentSign == null)
						{
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAdnexaSegmentSign =
								new List<VisitTiming_MainAdnexaSegmentSign>();
							_mainAdnexaSegmentSign = PEMRBusinessLogic.CreateNew_VisitTiming_MainAdnexaSegmentSign(
								FurtherDetails_OD, FurtherDetails_OD,
								ApplicationStaticConfiguration.PEMRSavingMode);
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAdnexaSegmentSign.Add(
								_mainAdnexaSegmentSign);
						}

						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAdnexaSegmentSign[0] != null)
						{
							if (!string.IsNullOrEmpty(txtReccommednations_OD.Text) ||
								!string.IsNullOrWhiteSpace(txtReccommednations_OD.Text))
								PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAdnexaSegmentSign[0].GeneralDescription_OD
									= txtReccommednations_OD.Text;
							else
								PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAdnexaSegmentSign[0]
									.GeneralDescription_OD = null;
						}

						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_AdnexaSegmentSign == null)
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_AdnexaSegmentSign =
								new List<VisitTiming_AdnexaSegmentSign>();
						_visitTimingAdnexaSegment = PEMRBusinessLogic.CreateNew_VisitTiming_AdnexaSegmentSign(
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAdnexaSegmentSign[0], segmentSign,
							DB_EyeType_p.OS, DB_PEMRSavingMode.SaveImmediately);
						if (_visitTimingAdnexaSegment != null)
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_AdnexaSegmentSign.Add(
								_visitTimingAdnexaSegment);
					}
				}
			}

			TempAdnexaSegmentSignListToBeAdded_OS = null;
			CommonViewsActions.FillListBoxControl(lstAddedAdnexaSegment_OS, AddedAdnexaSegmentSign_OS);
			lstAddedAdnexaSegment_OS.Refresh();
			SetCount_OS();
			SetCount_OS();
			lstAdnexaSegment_OS.SelectedIndex = -1;
		}

		private void btnRemoveFromList_OS_Click(object sender, EventArgs e)
		{

		}

		private void btnCopyToOD_Click(object sender, EventArgs e)
		{
			if (AddedAdnexaSegmentSign_OS == null || AddedAdnexaSegmentSign_OS.Count == 0)
				return;
			TempAdnexaSegmentSignListToBeAdded_OD = null;
			if (AddedAdnexaSegmentSign_OD == null)
				AddedAdnexaSegmentSign_OD = new List<SegmentSign_cu>();
			TempAdnexaSegmentSignListToBeAdded_OD = AddedAdnexaSegmentSign_OS;
			btnAddList_OD_Click(null, null);
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (PEMRBusinessLogic.ActivePEMRObject != null)
				if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAdnexaSegmentSign == null ||
					PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAdnexaSegmentSign.Count == 0)
				{
					_mainAdnexaSegmentSign = PEMRBusinessLogic.CreateNew_VisitTiming_MainAdnexaSegmentSign(FurtherDetails_OD, FurtherDetails_OD,
						ApplicationStaticConfiguration.PEMRSavingMode);
					if (_mainAdnexaSegmentSign == null)
						return;
					if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAdnexaSegmentSign == null)
						PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAdnexaSegmentSign = new List<VisitTiming_MainAdnexaSegmentSign>();
					PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAdnexaSegmentSign.Add(_mainAdnexaSegmentSign);
					XtraMessageBox.Show("Saved Successfully", "Saved", MessageBoxButtons.OK,
						MessageBoxIcon.Information);
				}
				else
				{
					if (_mainAdnexaSegmentSign == null)
						return;
					if (PEMRBusinessLogic.Update_VisitTiming_MainAdnexaSegmentSign(this, _mainAdnexaSegmentSign))
						XtraMessageBox.Show("Saved Successfully", "Saved", MessageBoxButtons.OK,
							MessageBoxIcon.Information);
				}
		}

		#endregion

		#region LookupEdit Events

		private void lkeAdnexaSegmentCategory_OD_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeAdnexaSegmentCategory_OD.EditValue == null)
				return;
			CommonViewsActions.FillListBoxControl(lstAdnexaSegment_OD,
				PEMRBusinessLogic.GetSegmentSignsList(Convert.ToInt32(lkeAdnexaSegmentCategory_OD.EditValue)));
		}

		private void lkeAdnexaSegmentCategory_OS_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeAdnexaSegmentCategory_OS.EditValue == null)
				return;
			CommonViewsActions.FillListBoxControl(lstAdnexaSegment_OS,
				PEMRBusinessLogic.GetSegmentSignsList(Convert.ToInt32(lkeAdnexaSegmentCategory_OS.EditValue)));
		}

		#endregion

		#region List Events

		private void lstAdnexaSegment_OD_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstAdnexaSegment_OD.SelectedItems == null || lstAdnexaSegment_OD.SelectedItems.Count == 0)
				return;
			TempAdnexaSegmentSignListToBeAdded_OD = null;
			foreach (object selectedItem in lstAdnexaSegment_OD.SelectedItems)
			{
				SegmentSign_cu diagnosis = SegmentSign_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID)));
				if (diagnosis != null)
				{
					if (TempAdnexaSegmentSignListToBeAdded_OD == null)
						TempAdnexaSegmentSignListToBeAdded_OD = new List<SegmentSign_cu>();
					if (!TempAdnexaSegmentSignListToBeAdded_OD.Exists(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID))))
						TempAdnexaSegmentSignListToBeAdded_OD.Add(diagnosis);
				}
			}
		}

		private void lstAdnexaSegment_OD_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			btnAddList_OD_Click(null, null);
		}

		private void lstAdnexaSegment_OS_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstAdnexaSegment_OS.SelectedItems == null || lstAdnexaSegment_OS.SelectedItems.Count == 0)
				return;
			TempAdnexaSegmentSignListToBeAdded_OS = null;
			foreach (object selectedItem in lstAdnexaSegment_OS.SelectedItems)
			{
				SegmentSign_cu diagnosis = SegmentSign_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID)));
				if (diagnosis != null)
				{
					if (TempAdnexaSegmentSignListToBeAdded_OS == null)
						TempAdnexaSegmentSignListToBeAdded_OS = new List<SegmentSign_cu>();
					if (!TempAdnexaSegmentSignListToBeAdded_OS.Exists(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID))))
						TempAdnexaSegmentSignListToBeAdded_OS.Add(diagnosis);
				}
			}
		}

		private void lstAdnexaSegment_OS_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			btnAddList_OS_Click(null, null);
		}

		private void lstAddedAdnexaSegment_OD_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void lstAddedAdnexaSegment_OS_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		#endregion

		#endregion
	}
}
