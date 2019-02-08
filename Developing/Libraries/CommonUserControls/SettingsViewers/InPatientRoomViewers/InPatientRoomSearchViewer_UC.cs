using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InPatientRoomViewers
{
	public partial class InPatientRoomSearchViewer_UC :
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractSearchViewer<InPatientRoom_cu>,
		IInPatientRoomViewer
	{
		public InPatientRoomSearchViewer_UC()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_InPatientRoomSearch);
			CommonViewsActions.SetupSyle(this);
		}

		private void InPatientRoomSearchViewer_UC_Load(object sender, System.EventArgs e)
		{
			if (BaseSearchContainerObject != null)
				BaseSearchContainerObject.SetupGridControl(InPatientRoomClassification_cu.ItemsList,
					InPatientRoom_cu.InPatientRoomClassification_CU_ID_ColumnaName, InPatientRoomClassification_cu.ID_ColumnaName);
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeInPatientRommClassification, InPatientRoomClassification_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeFloor, Floor_cu.ItemsList);
		}

		#region Overrides of CommonAbstractViewer

		public override object ViewerID
		{
			get { return ViewerName.InPatientRoomViewer; }
		}

		public override string HeaderTitle
		{
			get { return "غرف الإقامة"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_InPatientRoomSearch; }
		}

		#endregion

		#region Implementation of IInPatientRoomViewer

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

		public object Floor
		{
			get { return lkeInPatientRommClassification.EditValue; }
			set { lkeInPatientRommClassification.EditValue = value; }
		}

		public object InPatientRoomClassification
		{
			get { return lkeFloor.EditValue; }
			set { lkeFloor.EditValue = value; }
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

		public bool HasMainPatientPricing { get; private set; }
		public object PricePerDay_MainPatient { get; set; }
		public object MinimumAddmissionAmount_MainPatient { get; set; }
		public bool HasCompanionPricing { get; private set; }
		public object PricePerDay_CompanionPatient { get; set; }
		public object MinimumAddmissionAmount_CompanionPatient { get; set; }

		#endregion
	}
}
