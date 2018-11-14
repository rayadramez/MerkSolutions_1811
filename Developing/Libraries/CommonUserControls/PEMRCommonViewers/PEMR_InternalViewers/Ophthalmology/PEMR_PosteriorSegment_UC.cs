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
	public partial class PEMR_PosteriorSegment_UC : UserControl, IPEMR_Viewer, IPEMR_PosteriorSegment
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

		public List<SegmentSign_cu> AddedPosteriorSegmentSign_OD { get; set; }
		public List<SegmentSign_cu> AddedPosteriorSegmentSign_OS { get; set; }

		private List<SegmentSign_cu> TempPosteriorSegmentSignListToBeAdded_OD { get; set; }
		private List<SegmentSign_cu> TempPosteriorSegmentSignListToBeAdded_OS { get; set; }

		public static SegmentSign_cu SelectedSegmentSignFromSearch_OD { get; set; }
		public static SegmentSign_cu SelectedSegmentSignFromSearch_OS { get; set; }

		public FullScreenMode FullScreenMode { get; set; }
		private PEMR_PosteriorSegment_UC _pemrPosteriorSegment;
		private VisitTiming_MainPosteriorSegmentSign _mainPosteriorSegmentSign = null;
		private VisitTiming_PosteriorSegmentSign _visitTimingPosteriorSegment = null;
		private Control ParentControl { get; set; }

		public PEMR_PosteriorSegment_UC()
		{
			InitializeComponent();
			CommonViewsActions.Decorate(lkePosteriorSegmentCategory_OD, lkePosteriorSegmentCategory_OD);
		}

		private void PEMR_PosteriorSegment_UC_Load(object sender, System.EventArgs e)
		{
			ParentControl = this.Parent;
			ClearControls(false);
			FillControls();
		}

		public void Initialize()
		{
			lstPosteriorSegment_OD.SelectedIndex = -1;
			lstPosteriorSegment_OS.SelectedIndex = -1;
			FullScreenMode = FullScreenMode.NotFullScreen;
			txtReccommednations_OD.EnterMoveNextControl = false;

			if (PEMRBusinessLogic.ActivePEMRObject == null ||
			    PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainPosteriorSegmentSign == null ||
			     PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainPosteriorSegmentSign.Count == 0 ||
			     PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_PosteriorSegmentSign == null ||
			     PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_PosteriorSegmentSign.Count == 0)
				return;
			txtReccommednations_OD.EditValue = PEMRBusinessLogic.ActivePEMRObject
				.List_VisitTiming_MainPosteriorSegmentSign[0].GeneralDescription_OD;
			txtReccommednations_OS.EditValue = PEMRBusinessLogic.ActivePEMRObject
				.List_VisitTiming_MainPosteriorSegmentSign[0].GeneralDescription_OS;

			AddedPosteriorSegmentSign_OD = null;
			AddedPosteriorSegmentSign_OS = null;
			foreach (VisitTiming_PosteriorSegmentSign visitTimingPosterior in PEMRBusinessLogic.ActivePEMRObject
				.List_VisitTiming_PosteriorSegmentSign.FindAll(item =>
					!Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.Removed))))
			{
				SegmentSign_cu segmentSign = SegmentSign_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(visitTimingPosterior.SegmentSign_CU_ID)));
				if (segmentSign != null)
				{
					DB_EyeType_p eyeTpe = (DB_EyeType_p)visitTimingPosterior.Eye_P_ID;
					switch (eyeTpe)
					{
						case DB_EyeType_p.OD:
							if (AddedPosteriorSegmentSign_OD == null)
								AddedPosteriorSegmentSign_OD = new List<SegmentSign_cu>();
							AddedPosteriorSegmentSign_OD.Add(segmentSign);
							break;
						case DB_EyeType_p.OS:
							if (AddedPosteriorSegmentSign_OS == null)
								AddedPosteriorSegmentSign_OS = new List<SegmentSign_cu>();
							AddedPosteriorSegmentSign_OS.Add(segmentSign);
							break;
					}
				}
			}

			ClearControls(false);
			CommonViewsActions.FillListBoxControl(lstAddedPosteriorSegment_OD, AddedPosteriorSegmentSign_OD);
			CommonViewsActions.FillListBoxControl(lstAddedPosteriorSegment_OS, AddedPosteriorSegmentSign_OS);
			lstAddedPosteriorSegment_OD.Refresh();
			lstAddedPosteriorSegment_OS.Refresh();
			SetCount_OD();
			SetCount_OS();

			PEMRBusinessLogic.PEMR_PosteriorSegment = this;
		}

		public void SetCount_OD()
		{
			if (AddedPosteriorSegmentSign_OD != null && AddedPosteriorSegmentSign_OD.Count > 0)
				lytAddedSign_OD.Text = "Added Signs" + " (" + AddedPosteriorSegmentSign_OD.Count + ")";
			else
				lytAddedSign_OD.Text = "Added Sign" + "(0)";
		}

		public void SetCount_OS()
		{
			if (AddedPosteriorSegmentSign_OS != null && AddedPosteriorSegmentSign_OS.Count > 0)
				lytAddedSign_OS.Text = "Added Signs" + " (" + AddedPosteriorSegmentSign_OS.Count + ")";
			else
				lytAddedSign_OS.Text = "Added Sign" + "(0)";
		}

		#region Implementation of IPEMR_Viewer

		public void ClearControls(bool clearAll)
		{
			lstAddedPosteriorSegment_OD.DataSource = null;
			lstAddedPosteriorSegment_OS.DataSource = null;
			if (clearAll)
			{
				txtReccommednations_OD.EditValue = null;
				txtReccommednations_OS.EditValue = null;
				lytAddedSign_OD.Text = lytAddedSign_OS.Text = "Added Sign" + "(0)";
			}
		}

		public void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkePosteriorSegmentCategory_OD,
				SegmentSignCategory_cu.ItemsList.FindAll(item =>
					Convert.ToInt32(item.SegmentType_P_ID).Equals(Convert.ToInt32(DB_SegmentSignType.Posterior))));
			CommonViewsActions.FillGridlookupEdit(lkePosteriorSegmentCategory_OS,
				SegmentSignCategory_cu.ItemsList.FindAll(item =>
					Convert.ToInt32(item.SegmentType_P_ID).Equals(Convert.ToInt32(DB_SegmentSignType.Posterior))));
			SetCount_OD();
			SetCount_OS();
		}

		#endregion

		#region Controls Events

		#region Buttons Events

		private void btnAddList_OD_Click(object sender, EventArgs e)
		{
			if (TempPosteriorSegmentSignListToBeAdded_OD == null || TempPosteriorSegmentSignListToBeAdded_OD.Count == 0)
			{
				XtraMessageBox.Show(
					"You should select at least one Posterior Segment Sign to be added." + "\r\n" +
					"You can select one or more Posterior Segment Sign to add", "Note", MessageBoxButtons.OK,
					MessageBoxIcon.Hand);
				return;
			}

			if (AddedPosteriorSegmentSign_OD == null)
				AddedPosteriorSegmentSign_OD = new List<SegmentSign_cu>();
			if (TempPosteriorSegmentSignListToBeAdded_OD != null && TempPosteriorSegmentSignListToBeAdded_OD.Count > 0)
			{
				foreach (SegmentSign_cu segmentSign in TempPosteriorSegmentSignListToBeAdded_OD)
				{
					if (!AddedPosteriorSegmentSign_OD.Exists(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(segmentSign.ID))))
					{
						AddedPosteriorSegmentSign_OD.Add(segmentSign);
						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainPosteriorSegmentSign == null)
						{
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainPosteriorSegmentSign =
								new List<VisitTiming_MainPosteriorSegmentSign>();
							_mainPosteriorSegmentSign = PEMRBusinessLogic.CreateNew_VisitTiming_MainPosteriorSegmentSign(
								FurtherDetails_OD,FurtherDetails_OS,
								ApplicationStaticConfiguration.PEMRSavingMode);
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainPosteriorSegmentSign.Add(
								_mainPosteriorSegmentSign);
						}

						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainPosteriorSegmentSign[0] != null)
						{
							if (!string.IsNullOrEmpty(txtReccommednations_OD.Text) ||
								!string.IsNullOrWhiteSpace(txtReccommednations_OD.Text))
								PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainPosteriorSegmentSign[0].GeneralDescription_OD
									= txtReccommednations_OD.Text;
							else
								PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainPosteriorSegmentSign[0]
									.GeneralDescription_OD = null;
						}

						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_PosteriorSegmentSign == null)
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_PosteriorSegmentSign =
								new List<VisitTiming_PosteriorSegmentSign>();
						_visitTimingPosteriorSegment = PEMRBusinessLogic.CreateNew_VisitTiming_PosteriorSegmentSign(
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainPosteriorSegmentSign[0], segmentSign,
							DB_EyeType_p.OD, DB_PEMRSavingMode.SaveImmediately);
						if (_visitTimingPosteriorSegment != null)
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_PosteriorSegmentSign.Add(
								_visitTimingPosteriorSegment);
					}
				}
			}

			TempPosteriorSegmentSignListToBeAdded_OD = null;
			CommonViewsActions.FillListBoxControl(lstAddedPosteriorSegment_OD, AddedPosteriorSegmentSign_OD);
			lstAddedPosteriorSegment_OD.Refresh();
			SetCount_OD();
			SetCount_OS();
			lstPosteriorSegment_OD.SelectedIndex = -1;
		}

		private void btnRemoveFromList_OD_Click(object sender, EventArgs e)
		{
			if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_PosteriorSegmentSign == null ||
				PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_PosteriorSegmentSign.Count == 0)
				return;

			if (AddedPosteriorSegmentSign_OD == null || AddedPosteriorSegmentSign_OD.Count == 0)
			{
				XtraMessageBox.Show(
					"You should select at least one Segment Sign to be remove." + "\r\n" +
					"You can select one or more Segment Signs to remove", "Note", MessageBoxButtons.OK, MessageBoxIcon.Hand,
					MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
				return;
			}

			foreach (object selectedItem in lstAddedPosteriorSegment_OD.SelectedItems)
			{
				if (selectedItem is SegmentSign_cu)
				{
					SegmentSign_cu segment = SegmentSign_cu.ItemsList.Find(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID)));
					if (segment != null)
					{
						if (AddedPosteriorSegmentSign_OD.Exists(item =>
							Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID))))
							AddedPosteriorSegmentSign_OD.Remove(segment);
						VisitTiming_PosteriorSegmentSign visitTimingPosteriorSegment =
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_PosteriorSegmentSign.Find(item =>
								Convert.ToInt32(item.SegmentSign_CU_ID).Equals(Convert.ToInt32(segment.ID)) &&
								Convert.ToInt32(item.Eye_P_ID).Equals(Convert.ToInt32(DB_EyeType_p.OD)));
						if (visitTimingPosteriorSegment != null)
							visitTimingPosteriorSegment.PEMRElementStatus = PEMRElementStatus.Removed;
						PEMRBusinessLogic.Remove_VisitTiming_PosteriorSegmentSign(visitTimingPosteriorSegment);
					}
				}
			}

			TempPosteriorSegmentSignListToBeAdded_OD = null;
			CommonViewsActions.FillListBoxControl(lstAddedPosteriorSegment_OD, AddedPosteriorSegmentSign_OD);
			lstAddedPosteriorSegment_OD.Refresh();
			SetCount_OD();
			SetCount_OS();
			lstPosteriorSegment_OD.SelectedIndex = -1;
		}

		private void btnCopyToOS_Click(object sender, EventArgs e)
		{
			if (AddedPosteriorSegmentSign_OD == null || AddedPosteriorSegmentSign_OD.Count == 0)
				return;
			TempPosteriorSegmentSignListToBeAdded_OS = null;
			if (AddedPosteriorSegmentSign_OS == null)
				AddedPosteriorSegmentSign_OS = new List<SegmentSign_cu>();
			TempPosteriorSegmentSignListToBeAdded_OS = AddedPosteriorSegmentSign_OD;
			btnAddList_OS_Click(null, null);
		}

		private void btnAddList_OS_Click(object sender, EventArgs e)
		{
			if (TempPosteriorSegmentSignListToBeAdded_OS == null || TempPosteriorSegmentSignListToBeAdded_OS.Count == 0)
			{
				XtraMessageBox.Show(
					"You should select at least one Posterior Segment Sign to be added." + "\r\n" +
					"You can select one or more Posterior Segment Sign to add", "Note", MessageBoxButtons.OK,
					MessageBoxIcon.Hand);
				return;
			}

			if (AddedPosteriorSegmentSign_OS == null)
				AddedPosteriorSegmentSign_OS = new List<SegmentSign_cu>();
			if (TempPosteriorSegmentSignListToBeAdded_OS != null && TempPosteriorSegmentSignListToBeAdded_OS.Count > 0)
			{
				foreach (SegmentSign_cu segmentSign in TempPosteriorSegmentSignListToBeAdded_OS)
				{
					if (!AddedPosteriorSegmentSign_OS.Exists(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(segmentSign.ID))))
					{
						AddedPosteriorSegmentSign_OS.Add(segmentSign);
						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainPosteriorSegmentSign == null)
						{
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainPosteriorSegmentSign =
								new List<VisitTiming_MainPosteriorSegmentSign>();
							_mainPosteriorSegmentSign = PEMRBusinessLogic.CreateNew_VisitTiming_MainPosteriorSegmentSign(
								FurtherDetails_OD,FurtherDetails_OS,
								ApplicationStaticConfiguration.PEMRSavingMode);
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainPosteriorSegmentSign.Add(
								_mainPosteriorSegmentSign);
						}

						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainPosteriorSegmentSign[0] != null)
						{
							if (!string.IsNullOrEmpty(txtReccommednations_OD.Text) ||
								!string.IsNullOrWhiteSpace(txtReccommednations_OD.Text))
								PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainPosteriorSegmentSign[0].GeneralDescription_OD
									= txtReccommednations_OD.Text;
							else
								PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainPosteriorSegmentSign[0]
									.GeneralDescription_OD = null;
						}

						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_PosteriorSegmentSign == null)
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_PosteriorSegmentSign =
								new List<VisitTiming_PosteriorSegmentSign>();
						_visitTimingPosteriorSegment = PEMRBusinessLogic.CreateNew_VisitTiming_PosteriorSegmentSign(
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainPosteriorSegmentSign[0], segmentSign,
							DB_EyeType_p.OS, DB_PEMRSavingMode.SaveImmediately);
						if (_visitTimingPosteriorSegment != null)
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_PosteriorSegmentSign.Add(
								_visitTimingPosteriorSegment);
					}
				}
			}

			TempPosteriorSegmentSignListToBeAdded_OS = null;
			CommonViewsActions.FillListBoxControl(lstAddedPosteriorSegment_OS, AddedPosteriorSegmentSign_OS);
			lstAddedPosteriorSegment_OS.Refresh();
			SetCount_OS();
			SetCount_OS();
			lstPosteriorSegment_OS.SelectedIndex = -1;
		}

		private void btnRemoveFromList_OS_Click(object sender, EventArgs e)
		{
			if (AddedPosteriorSegmentSign_OS == null || AddedPosteriorSegmentSign_OS.Count == 0)
			{
				XtraMessageBox.Show(
					"You should select at least one Segment Sign to be remove." + "\r\n" +
					"You can select one or more Segment Signs to remove", "Note", MessageBoxButtons.OK, MessageBoxIcon.Hand,
					MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
				return;
			}

			foreach (object selectedItem in lstAddedPosteriorSegment_OS.SelectedItems)
			{
				if (selectedItem is SegmentSign_cu)
				{
					SegmentSign_cu segment = SegmentSign_cu.ItemsList.Find(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID)));
					if (segment != null)
					{
						if (AddedPosteriorSegmentSign_OS.Exists(item =>
							Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID))))
							AddedPosteriorSegmentSign_OS.Remove(segment);
						VisitTiming_PosteriorSegmentSign visitTimingPosteriorSegment =
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_PosteriorSegmentSign.Find(item =>
								Convert.ToInt32(item.SegmentSign_CU_ID).Equals(Convert.ToInt32(segment.ID)) &&
								Convert.ToInt32(item.Eye_P_ID).Equals(Convert.ToInt32(DB_EyeType_p.OS)));
						if (visitTimingPosteriorSegment != null)
							visitTimingPosteriorSegment.PEMRElementStatus = PEMRElementStatus.Removed;
						PEMRBusinessLogic.Remove_VisitTiming_PosteriorSegmentSign(visitTimingPosteriorSegment);
					}
				}
			}

			TempPosteriorSegmentSignListToBeAdded_OS = null;
			CommonViewsActions.FillListBoxControl(lstAddedPosteriorSegment_OS, AddedPosteriorSegmentSign_OS);
			lstAddedPosteriorSegment_OS.Refresh();
			SetCount_OS();
			SetCount_OS();
			lstPosteriorSegment_OS.SelectedIndex = -1;
		}

		private void btnCopyToOD_Click(object sender, EventArgs e)
		{
			if (AddedPosteriorSegmentSign_OS == null || AddedPosteriorSegmentSign_OS.Count == 0)
				return;
			TempPosteriorSegmentSignListToBeAdded_OD = null;
			if (AddedPosteriorSegmentSign_OD == null)
				AddedPosteriorSegmentSign_OD = new List<SegmentSign_cu>();
			TempPosteriorSegmentSignListToBeAdded_OD = AddedPosteriorSegmentSign_OS;
			btnAddList_OD_Click(null, null);
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (PEMRBusinessLogic.ActivePEMRObject != null)
				if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainPosteriorSegmentSign == null ||
					PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainPosteriorSegmentSign.Count == 0)
				{
					_mainPosteriorSegmentSign = PEMRBusinessLogic.CreateNew_VisitTiming_MainPosteriorSegmentSign(FurtherDetails_OD, FurtherDetails_OD,
						ApplicationStaticConfiguration.PEMRSavingMode);
					if (_mainPosteriorSegmentSign == null)
						return;
					if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainPosteriorSegmentSign == null)
						PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainPosteriorSegmentSign = new List<VisitTiming_MainPosteriorSegmentSign>();
					PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainPosteriorSegmentSign.Add(_mainPosteriorSegmentSign);
					XtraMessageBox.Show("Saved Successfully", "Saved", MessageBoxButtons.OK,
						MessageBoxIcon.Information);
				}
				else
				{
					if (_mainPosteriorSegmentSign == null)
						return;
					if(PEMRBusinessLogic.Update_VisitTiming_MainPosteriorSegmentSign(this, _mainPosteriorSegmentSign))
						XtraMessageBox.Show("Saved Successfully", "Saved", MessageBoxButtons.OK,
							MessageBoxIcon.Information);
				}
		}

		#endregion

		#region LookupEdit Events

		private void lkePosteriorSegmentCategory_OD_EditValueChanged(object sender, EventArgs e)
		{
			if (lkePosteriorSegmentCategory_OD.EditValue == null)
				return;
			CommonViewsActions.FillListBoxControl(lstPosteriorSegment_OD,
				PEMRBusinessLogic.GetSegmentSignsList(Convert.ToInt32(lkePosteriorSegmentCategory_OD.EditValue)));
		}

		private void lkePosteriorSegmentCategory_OS_EditValueChanged(object sender, EventArgs e)
		{
			if (lkePosteriorSegmentCategory_OS.EditValue == null)
				return;
			CommonViewsActions.FillListBoxControl(lstPosteriorSegment_OS,
				PEMRBusinessLogic.GetSegmentSignsList(Convert.ToInt32(lkePosteriorSegmentCategory_OS.EditValue)));
		}

		#endregion

		#region List Events

		private void lstPosteriorSegment_OD_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstPosteriorSegment_OD.SelectedItems == null || lstPosteriorSegment_OD.SelectedItems.Count == 0)
				return;
			TempPosteriorSegmentSignListToBeAdded_OD = null;
			foreach (object selectedItem in lstPosteriorSegment_OD.SelectedItems)
			{
				SegmentSign_cu diagnosis = SegmentSign_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID)));
				if (diagnosis != null)
				{
					if (TempPosteriorSegmentSignListToBeAdded_OD == null)
						TempPosteriorSegmentSignListToBeAdded_OD = new List<SegmentSign_cu>();
					if (!TempPosteriorSegmentSignListToBeAdded_OD.Exists(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID))))
						TempPosteriorSegmentSignListToBeAdded_OD.Add(diagnosis);
				}
			}
		}

		private void lstPosteriorSegment_OD_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			btnAddList_OD_Click(null, null);
		}

		private void lstPosteriorSegment_OS_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstPosteriorSegment_OS.SelectedItems == null || lstPosteriorSegment_OS.SelectedItems.Count == 0)
				return;
			TempPosteriorSegmentSignListToBeAdded_OS = null;
			foreach (object selectedItem in lstPosteriorSegment_OS.SelectedItems)
			{
				SegmentSign_cu diagnosis = SegmentSign_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID)));
				if (diagnosis != null)
				{
					if (TempPosteriorSegmentSignListToBeAdded_OS == null)
						TempPosteriorSegmentSignListToBeAdded_OS = new List<SegmentSign_cu>();
					if (!TempPosteriorSegmentSignListToBeAdded_OS.Exists(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID))))
						TempPosteriorSegmentSignListToBeAdded_OS.Add(diagnosis);
				}
			}
		}

		private void lstPosteriorSegment_OS_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			btnAddList_OS_Click(null, null);
		}

		private void lstAddedPosteriorSegment_OD_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void lstAddedPosteriorSegment_OS_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		#endregion

		#endregion
	}
}
