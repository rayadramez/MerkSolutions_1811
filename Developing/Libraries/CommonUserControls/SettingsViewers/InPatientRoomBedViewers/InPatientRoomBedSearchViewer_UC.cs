using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InPatientRoomBedViewers
{
	public partial class InPatientRoomBedSearchViewer_UC : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractSearchViewer<InPatientRoomBed_cu>,
		IInPatientRoomBedViewer
	{
		public InPatientRoomBedSearchViewer_UC()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_InPatientRoomBedSearch);
			CommonViewsActions.SetupSyle(this);

			txtNameP.Focus();
		}

		private void InPatientRoomBedSearchViewer_UC_Load(object sender, System.EventArgs e)
		{

		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeInPatientRoom, InPatientRoom_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeInPatientRoomBedStatud, InPatientRoomBedStatus_p.ItemsList);
		}

		#region Overrides of CommonAbstractViewer<InPatientRoom_cu>

		public override object ViewerID
		{
			get { return ViewerName.InPatientRoomBedViewer; }
		}

		public override string HeaderTitle
		{
			get { return "الآسرة"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_InPatientRoomBedSearch; }
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

		public object Description { get; set; }

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
