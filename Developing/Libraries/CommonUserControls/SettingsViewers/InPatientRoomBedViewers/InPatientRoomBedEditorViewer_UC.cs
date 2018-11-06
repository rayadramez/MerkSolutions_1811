using System.Collections.Generic;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InPatientRoomBedViewers
{
	public partial class InPatientRoomBedEditorViewer_UC : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<InPatientRoomBed_cu>,
		IInPatientRoomBedViewer
	{
		public InPatientRoomBedEditorViewer_UC()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_InPatientRoomBedEditor);
			CommonViewsActions.SetupSyle(this);

			txtNameP.Focus();
		}

		#region Overrides of CommonAbstractViewer<InPatientRoomBed_cu>

		public override IMVCController<InPatientRoomBed_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.InPatientRoomBedViewer; }
		}

		public override string HeaderTitle
		{
			get { return "الآسرة"; }
		}

		public override List<IViewer> GetRelatedViewers()
		{
			return RelatedViewers;
		}

		public override void ClearControls()
		{

		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeInPatientRoom, InPatientRoom_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeInPatientRoomBedStatud, InPatientRoomBedStatus_p.ItemsList);
		}

		#endregion

		#region Implementation of IInPatientRoomBedViewer

		public object NameP
		{
			get { return txtNameP.EditValue; }
			set { txtNameP.EditValue = value; }
		}

		public object NameS
		{
			get { return txtNameS.EditValue; }
			set { txtNameS.EditValue = value; }
		}

		public object InPatientRoom
		{
			get { return lkeInPatientRoom.EditValue; }
			set { lkeInPatientRoom.EditValue = value; }
		}

		public object Description
		{
			get { return txtDescription.EditValue; }
			set { txtDescription.EditValue = value; }
		}

		public object ShortName
		{
			get { return txtShortName.EditValue; }
			set { txtShortName.EditValue = value; }
		}

		public object InternalCode
		{
			get { return txtInternalCode.EditValue; }
			set { txtInternalCode.EditValue = value; }
		}

		public object InPatientRoomBedStatus
		{
			get { return lkeInPatientRoomBedStatud.EditValue; }
			set { lkeInPatientRoomBedStatud.EditValue = value; }
		}

		#endregion
	}
}
