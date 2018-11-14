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
	public partial class PEMR_AnteriorSegment_UC : UserControl, IPEMR_Viewer, IPEMR_AnteriorSegmentSign
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

		public List<SegmentSign_cu> AddedAnteriorSegmentSign_OD { get; set; }
		public List<SegmentSign_cu> AddedAnteriorSegmentSign_OS { get; set; }

		private List<SegmentSign_cu> TempAnteriorSegmentSignListToBeAdded_OD { get; set; }
		private List<SegmentSign_cu> TempAnteriorSegmentSignListToBeAdded_OS { get; set; }

		public static SegmentSign_cu SelectedSegmentSignFromSearch_OD { get; set; }
		public static SegmentSign_cu SelectedSegmentSignFromSearch_OS { get; set; }

		public FullScreenMode FullScreenMode { get; set; }
		private PEMR_AnteriorSegment_UC _pemrAnteriorSegment;
		private VisitTiming_MainAnteriorSegmentSign _mainAnteriorSegmentSign = null;
		private VisitTiming_AnteriorSegmentSign _visitTimingAnteriorSegment = null;
		private Control ParentControl { get; set; }

		public PEMR_AnteriorSegment_UC()
		{
			InitializeComponent();

			CommonViewsActions.Decorate(lkeAnteriorSegmentCategory_OD, lkeAnteriorSegmentCategory_OS);
		}

		private void PEMR_AnteriorSegment_UC_Load(object sender, EventArgs e)
		{
			ParentControl = this.Parent;
			ClearControls(false);
			FillControls();
		}

		public void Initialize()
		{
			lstAnteriorSegment_OD.SelectedIndex = -1;
			lstAnteriorSegment_OS.SelectedIndex = -1;
			FullScreenMode = FullScreenMode.NotFullScreen;
			txtReccommednations_OD.EnterMoveNextControl = false;

			if (PEMRBusinessLogic.ActivePEMRObject == null ||
			    PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign == null ||
			     PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign.Count == 0 ||
			     PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_AnteriorSegmentSign == null ||
			     PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_AnteriorSegmentSign.Count == 0)
				return;

			txtReccommednations_OD.EditValue = PEMRBusinessLogic.ActivePEMRObject
				.List_VisitTiming_MainAnteriorSegmentSign[0].GeneralDescription_OD;
			txtReccommednations_OS.EditValue = PEMRBusinessLogic.ActivePEMRObject
				.List_VisitTiming_MainAnteriorSegmentSign[0].GeneralDescription_OS;

			AddedAnteriorSegmentSign_OD = null;
			AddedAnteriorSegmentSign_OS = null;
			foreach (VisitTiming_AnteriorSegmentSign visitTimingAnterior in PEMRBusinessLogic.ActivePEMRObject
				.List_VisitTiming_AnteriorSegmentSign.FindAll(item =>
					!Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.Removed))))
			{
				SegmentSign_cu segmentSign = SegmentSign_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(visitTimingAnterior.SegmentSignID)));
				if (segmentSign != null)
				{
					DB_EyeType_p eyeType = (DB_EyeType_p) visitTimingAnterior.Eye_P_ID;
					switch (eyeType)
					{
						case DB_EyeType_p.OD:
							if (AddedAnteriorSegmentSign_OD == null)
								AddedAnteriorSegmentSign_OD = new List<SegmentSign_cu>();
							AddedAnteriorSegmentSign_OD.Add(segmentSign);
							break;
						case DB_EyeType_p.OS:
							if (AddedAnteriorSegmentSign_OS == null)
								AddedAnteriorSegmentSign_OS = new List<SegmentSign_cu>();
							AddedAnteriorSegmentSign_OS.Add(segmentSign);
							break;
					}
				}
			}

			ClearControls(false);
			CommonViewsActions.FillListBoxControl(lstAddedAnteriorSegment_OD, AddedAnteriorSegmentSign_OD);
			CommonViewsActions.FillListBoxControl(lstAddedAnteriorSegment_OS, AddedAnteriorSegmentSign_OS);
			lstAddedAnteriorSegment_OD.Refresh();
			lstAddedAnteriorSegment_OS.Refresh();
			SetCount_OD();
			SetCount_OS();
			PEMRBusinessLogic.PEMR_AnteriorSegmentSign = this;
		}

		public void SetCount_OD()
		{
			if (AddedAnteriorSegmentSign_OD != null && AddedAnteriorSegmentSign_OD.Count > 0)
				lytAddedSign_OD.Text = "Added Signs" + " (" + AddedAnteriorSegmentSign_OD.Count + ")";
			else
				lytAddedSign_OD.Text = "Added Sign" + "(0)";
		}

		public void SetCount_OS()
		{
			if (AddedAnteriorSegmentSign_OS != null && AddedAnteriorSegmentSign_OS.Count > 0)
				lytAddedSign_OS.Text = "Added Signs" + " (" + AddedAnteriorSegmentSign_OS.Count + ")";
			else
				lytAddedSign_OS.Text = "Added Sign" + "(0)";
		}

		#region Implementation of IPEMR_Viewer

		public void ClearControls(bool clearAll)
		{
			lstAddedAnteriorSegment_OD.DataSource = null;
			lstAddedAnteriorSegment_OS.DataSource = null;
			if (clearAll)
			{
				txtReccommednations_OD.EditValue = null;
				txtReccommednations_OS.EditValue = null;
				lytAddedSign_OD.Text = lytAddedSign_OS.Text = "Added Sign" + "(0)";
			}
		}

		public void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeAnteriorSegmentCategory_OD,
				SegmentSignCategory_cu.ItemsList.FindAll(item =>
					Convert.ToInt32(item.SegmentType_P_ID).Equals(Convert.ToInt32(DB_SegmentSignType.Anterior))));
			CommonViewsActions.FillGridlookupEdit(lkeAnteriorSegmentCategory_OS,
				SegmentSignCategory_cu.ItemsList.FindAll(item =>
					Convert.ToInt32(item.SegmentType_P_ID).Equals(Convert.ToInt32(DB_SegmentSignType.Anterior))));
			SetCount_OD();
			SetCount_OS();
		}

		#endregion

		#region Controls Events

		#region Buttons Events

		private void btnAddList_OD_Click(object sender, EventArgs e)
		{
			if (TempAnteriorSegmentSignListToBeAdded_OD == null || TempAnteriorSegmentSignListToBeAdded_OD.Count == 0)
			{
				XtraMessageBox.Show(
					"You should select at least one Anterior Segment Sign to be added." + "\r\n" +
					"You can select one or more Anterior Segment Sign to add", "Note", MessageBoxButtons.OK,
					MessageBoxIcon.Hand);
				return;
			}

			if (AddedAnteriorSegmentSign_OD == null)
				AddedAnteriorSegmentSign_OD = new List<SegmentSign_cu>();
			if (TempAnteriorSegmentSignListToBeAdded_OD != null && TempAnteriorSegmentSignListToBeAdded_OD.Count > 0)
			{
				foreach (SegmentSign_cu segmentSign in TempAnteriorSegmentSignListToBeAdded_OD)
				{
					if (!AddedAnteriorSegmentSign_OD.Exists(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(segmentSign.ID))))
					{
						AddedAnteriorSegmentSign_OD.Add(segmentSign);
						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign == null)
						{
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign =
								new List<VisitTiming_MainAnteriorSegmentSign>();
							_mainAnteriorSegmentSign = PEMRBusinessLogic.CreateNew_VisitTiming_MainAnteriorSegmentSign(
								FurtherDetails_OD, FurtherDetails_OS,
								ApplicationStaticConfiguration.PEMRSavingMode);
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign.Add(
								_mainAnteriorSegmentSign);
						}

						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign[0] != null)
						{
							if (!string.IsNullOrEmpty(txtReccommednations_OD.Text) ||
								!string.IsNullOrWhiteSpace(txtReccommednations_OD.Text))
								PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign[0].GeneralDescription_OD
									= txtReccommednations_OD.Text;
							else
								PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign[0]
									.GeneralDescription_OD = null;
						}

						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_AnteriorSegmentSign == null)
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_AnteriorSegmentSign =
								new List<VisitTiming_AnteriorSegmentSign>();
						_visitTimingAnteriorSegment = PEMRBusinessLogic.CreateNew_VisitTiming_AnteriorSegmentSign(
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign[0], segmentSign,
							DB_EyeType_p.OD, DB_PEMRSavingMode.SaveImmediately);
						if (_visitTimingAnteriorSegment != null)
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_AnteriorSegmentSign.Add(
								_visitTimingAnteriorSegment);
					}
				}
			}

			TempAnteriorSegmentSignListToBeAdded_OD = null;
			CommonViewsActions.FillListBoxControl(lstAddedAnteriorSegment_OD, AddedAnteriorSegmentSign_OD);
			lstAddedAnteriorSegment_OD.Refresh();
			SetCount_OD();
			SetCount_OS();
			lstAnteriorSegment_OD.SelectedIndex = -1;
		}

		private void btnRemoveFromList_OD_Click(object sender, EventArgs e)
		{
			if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_AnteriorSegmentSign == null ||
			    PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_AnteriorSegmentSign.Count == 0)
				return;
			if (AddedAnteriorSegmentSign_OD == null || AddedAnteriorSegmentSign_OD.Count == 0)
			{
				XtraMessageBox.Show(
					"You should select at least one Segment Sign to be remove." + "\r\n" +
					"You can select one or more Segment Signs to remove", "Note", MessageBoxButtons.OK, MessageBoxIcon.Hand,
					MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
				return;
			}

			foreach (object selectedItem in lstAddedAnteriorSegment_OD.SelectedItems)
			{
				if (selectedItem is SegmentSign_cu)
				{
					SegmentSign_cu segment = SegmentSign_cu.ItemsList.Find(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu) selectedItem).ID)));
					if (segment != null)
					{
						if (AddedAnteriorSegmentSign_OD.Exists(item =>
							Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu) selectedItem).ID))))
							AddedAnteriorSegmentSign_OD.Remove(segment);
						VisitTiming_AnteriorSegmentSign visitTimingAnteriorSegment =
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_AnteriorSegmentSign.Find(item =>
								Convert.ToInt32(item.SegmentSignID).Equals(Convert.ToInt32(segment.ID)) &&
								Convert.ToInt32(item.Eye_P_ID).Equals(Convert.ToInt32(DB_EyeType_p.OD)));
						if (visitTimingAnteriorSegment != null)
							visitTimingAnteriorSegment.PEMRElementStatus = PEMRElementStatus.Removed;
						PEMRBusinessLogic.Remove_VisitTiming_AnteriorSegmentSign(visitTimingAnteriorSegment);
					}
				}
			}

			TempAnteriorSegmentSignListToBeAdded_OD = null;
			CommonViewsActions.FillListBoxControl(lstAddedAnteriorSegment_OD, AddedAnteriorSegmentSign_OD);
			lstAddedAnteriorSegment_OD.Refresh();
			SetCount_OD();
			SetCount_OS();
			lstAnteriorSegment_OD.SelectedIndex = -1;
		}

		private void btnCopyToOS_Click(object sender, EventArgs e)
		{
			if (AddedAnteriorSegmentSign_OD == null || AddedAnteriorSegmentSign_OD.Count == 0)
				return;
			TempAnteriorSegmentSignListToBeAdded_OS = null;
			if(AddedAnteriorSegmentSign_OS == null)
				AddedAnteriorSegmentSign_OS = new List<SegmentSign_cu>();
			TempAnteriorSegmentSignListToBeAdded_OS = AddedAnteriorSegmentSign_OD;
			btnAddList_OS_Click(null, null);
		}

		private void btnAddList_OS_Click(object sender, EventArgs e)
		{
			if (TempAnteriorSegmentSignListToBeAdded_OS == null || TempAnteriorSegmentSignListToBeAdded_OS.Count == 0)
			{
				XtraMessageBox.Show(
					"You should select at least one Anterior Segment Sign to be added." + "\r\n" +
					"You can select one or more Anterior Segment Sign to add", "Note", MessageBoxButtons.OK,
					MessageBoxIcon.Hand);
				return;
			}

			if (AddedAnteriorSegmentSign_OS == null)
				AddedAnteriorSegmentSign_OS = new List<SegmentSign_cu>();
			if (TempAnteriorSegmentSignListToBeAdded_OS != null && TempAnteriorSegmentSignListToBeAdded_OS.Count > 0)
			{
				foreach (SegmentSign_cu segmentSign in TempAnteriorSegmentSignListToBeAdded_OS)
				{
					if (!AddedAnteriorSegmentSign_OS.Exists(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(segmentSign.ID))))
					{
						AddedAnteriorSegmentSign_OS.Add(segmentSign);
						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign == null)
						{
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign =
								new List<VisitTiming_MainAnteriorSegmentSign>();
							_mainAnteriorSegmentSign = PEMRBusinessLogic.CreateNew_VisitTiming_MainAnteriorSegmentSign(
								FurtherDetails_OD, FurtherDetails_OS,
								ApplicationStaticConfiguration.PEMRSavingMode);
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign.Add(
								_mainAnteriorSegmentSign);
						}

						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign[0] != null)
						{
							if (!string.IsNullOrEmpty(txtReccommednations_OD.Text) ||
								!string.IsNullOrWhiteSpace(txtReccommednations_OD.Text))
								PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign[0].GeneralDescription_OD
									= txtReccommednations_OD.Text;
							else
								PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign[0]
									.GeneralDescription_OD = null;
						}

						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_AnteriorSegmentSign == null)
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_AnteriorSegmentSign =
								new List<VisitTiming_AnteriorSegmentSign>();
						_visitTimingAnteriorSegment = PEMRBusinessLogic.CreateNew_VisitTiming_AnteriorSegmentSign(
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign[0], segmentSign,
							DB_EyeType_p.OS, DB_PEMRSavingMode.SaveImmediately);
						if (_visitTimingAnteriorSegment != null)
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_AnteriorSegmentSign.Add(
								_visitTimingAnteriorSegment);
					}
				}
			}

			TempAnteriorSegmentSignListToBeAdded_OS = null;
			CommonViewsActions.FillListBoxControl(lstAddedAnteriorSegment_OS, AddedAnteriorSegmentSign_OS);
			lstAddedAnteriorSegment_OS.Refresh();
			SetCount_OS();
			SetCount_OS();
			lstAnteriorSegment_OS.SelectedIndex = -1;
		}

		private void btnRemoveFromList_OS_Click(object sender, EventArgs e)
		{
			if (AddedAnteriorSegmentSign_OS == null || AddedAnteriorSegmentSign_OS.Count == 0)
			{
				XtraMessageBox.Show(
					"You should select at least one Segment Sign to be remove." + "\r\n" +
					"You can select one or more Segment Signs to remove", "Note", MessageBoxButtons.OK, MessageBoxIcon.Hand,
					MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
				return;
			}

			foreach (object selectedItem in lstAddedAnteriorSegment_OS.SelectedItems)
			{
				if (selectedItem is SegmentSign_cu)
				{
					SegmentSign_cu segment = SegmentSign_cu.ItemsList.Find(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID)));
					if (segment != null)
					{
						if (AddedAnteriorSegmentSign_OS.Exists(item =>
							Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID))))
							AddedAnteriorSegmentSign_OS.Remove(segment);
						VisitTiming_AnteriorSegmentSign visitTimingAnteriorSegment =
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_AnteriorSegmentSign.Find(item =>
								Convert.ToInt32(item.SegmentSignID).Equals(Convert.ToInt32(segment.ID)) &&
								Convert.ToInt32(item.Eye_P_ID).Equals(Convert.ToInt32(DB_EyeType_p.OS)));
						if (visitTimingAnteriorSegment != null)
							visitTimingAnteriorSegment.PEMRElementStatus = PEMRElementStatus.Removed;
						PEMRBusinessLogic.Remove_VisitTiming_AnteriorSegmentSign(visitTimingAnteriorSegment);
					}
				}
			}

			TempAnteriorSegmentSignListToBeAdded_OS = null;
			CommonViewsActions.FillListBoxControl(lstAddedAnteriorSegment_OS, AddedAnteriorSegmentSign_OS);
			lstAddedAnteriorSegment_OS.Refresh();
			SetCount_OS();
			SetCount_OS();
			lstAnteriorSegment_OS.SelectedIndex = -1;
		}

		private void btnCopyToOD_Click(object sender, EventArgs e)
		{
			if (AddedAnteriorSegmentSign_OS == null || AddedAnteriorSegmentSign_OS.Count == 0)
				return;
			TempAnteriorSegmentSignListToBeAdded_OD = null;
			if (AddedAnteriorSegmentSign_OD == null)
				AddedAnteriorSegmentSign_OD = new List<SegmentSign_cu>();
			TempAnteriorSegmentSignListToBeAdded_OD = AddedAnteriorSegmentSign_OS;
			btnAddList_OD_Click(null, null);
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (PEMRBusinessLogic.ActivePEMRObject != null)
				if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign == null ||
					PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign.Count == 0)
				{
					_mainAnteriorSegmentSign = PEMRBusinessLogic.CreateNew_VisitTiming_MainAnteriorSegmentSign(FurtherDetails_OD, FurtherDetails_OD,
						ApplicationStaticConfiguration.PEMRSavingMode);
					if (_mainAnteriorSegmentSign == null)
						return;
					if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign == null)
						PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign = new List<VisitTiming_MainAnteriorSegmentSign>();
					PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign.Add(_mainAnteriorSegmentSign);
					XtraMessageBox.Show("Saved Successfully", "Saved", MessageBoxButtons.OK,
						MessageBoxIcon.Information);
				}
				else
				{
					if (_mainAnteriorSegmentSign == null)
						return;
					if (PEMRBusinessLogic.Update_VisitTiming_MainAnteriorSegmentSign(this, _mainAnteriorSegmentSign))
						XtraMessageBox.Show("Saved Successfully", "Saved", MessageBoxButtons.OK,
							MessageBoxIcon.Information);
				}
		}

		#endregion

		#region LookupEdit Events

		private void lkeAnteriorSegmentCategory_OD_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeAnteriorSegmentCategory_OD.EditValue == null)
				return;
			CommonViewsActions.FillListBoxControl(lstAnteriorSegment_OD,
				PEMRBusinessLogic.GetSegmentSignsList(Convert.ToInt32(lkeAnteriorSegmentCategory_OD.EditValue)));
		}

		private void lkeAnteriorSegmentCategory_OS_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeAnteriorSegmentCategory_OS.EditValue == null)
				return;
			CommonViewsActions.FillListBoxControl(lstAnteriorSegment_OS,
				PEMRBusinessLogic.GetSegmentSignsList(Convert.ToInt32(lkeAnteriorSegmentCategory_OS.EditValue)));
		}

		#endregion

		#region List Events

		private void lstAnteriorSegment_OD_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstAnteriorSegment_OD.SelectedItems == null || lstAnteriorSegment_OD.SelectedItems.Count == 0)
				return;
			TempAnteriorSegmentSignListToBeAdded_OD = null;
			foreach (object selectedItem in lstAnteriorSegment_OD.SelectedItems)
			{
				SegmentSign_cu diagnosis = SegmentSign_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID)));
				if (diagnosis != null)
				{
					if (TempAnteriorSegmentSignListToBeAdded_OD == null)
						TempAnteriorSegmentSignListToBeAdded_OD = new List<SegmentSign_cu>();
					if (!TempAnteriorSegmentSignListToBeAdded_OD.Exists(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID))))
						TempAnteriorSegmentSignListToBeAdded_OD.Add(diagnosis);
				}
			}
		}

		private void lstAnteriorSegment_OD_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			btnAddList_OD_Click(null, null);
		}

		private void lstAnteriorSegment_OS_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstAnteriorSegment_OS.SelectedItems == null || lstAnteriorSegment_OS.SelectedItems.Count == 0)
				return;
			TempAnteriorSegmentSignListToBeAdded_OS = null;
			foreach (object selectedItem in lstAnteriorSegment_OS.SelectedItems)
			{
				SegmentSign_cu diagnosis = SegmentSign_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID)));
				if (diagnosis != null)
				{
					if (TempAnteriorSegmentSignListToBeAdded_OS == null)
						TempAnteriorSegmentSignListToBeAdded_OS = new List<SegmentSign_cu>();
					if (!TempAnteriorSegmentSignListToBeAdded_OS.Exists(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(((SegmentSign_cu)selectedItem).ID))))
						TempAnteriorSegmentSignListToBeAdded_OS.Add(diagnosis);
				}
			}
		}

		private void lstAnteriorSegment_OS_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			btnAddList_OS_Click(null, null);
		}

		private void lstAddedAnteriorSegment_OD_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void lstAddedAnteriorSegment_OS_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		#endregion

		#endregion
		
	}
}
