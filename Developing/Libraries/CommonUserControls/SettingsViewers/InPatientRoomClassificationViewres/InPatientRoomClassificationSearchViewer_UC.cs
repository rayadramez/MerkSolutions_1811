using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InPatientRoomClassificationViewres
{
	public partial class InPatientRoomClassificationSearchViewer_UC : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractSearchViewer<InPatientRoomClassification_cu>,
		IInPatientRoomClassificationViewer
	{
		public InPatientRoomClassificationSearchViewer_UC()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1,
				Resources.LocalizedRes.lyt_InPatientRoomClassificationSearchViewer);
			CommonViewsActions.SetupSyle(this);

			txtNameP.Focus();
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeInPatientRoomType, InPatientRoomType_p.ItemsList);
		}

		#region Overrides of CommonAbstractViewer<InPatientRoomBed_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.InsurancePolicy_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "سياســات جهــات التأميـــن"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_InPatientRoomClassificationSearch; }
		}

		#endregion

		#region Implementation of IInPatientRoomClassificationViewer

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

		public object InPatientRoomType
		{
			get { return lkeInPatientRoomType.EditValue; }
			set { lkeInPatientRoomType.EditValue = value; }
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
