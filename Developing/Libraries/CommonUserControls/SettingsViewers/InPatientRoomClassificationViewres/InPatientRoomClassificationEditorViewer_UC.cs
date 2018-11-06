using System;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InPatientRoomClassificationViewres
{
	public partial class InPatientRoomClassificationEditorViewer_UC : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<InPatientRoomClassification_cu>,
		IInPatientRoomClassificationViewer
	{
		public InPatientRoomClassificationEditorViewer_UC()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_InPatientRoomClassificationEditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeInPatientRoomType, InPatientRoomType_p.ItemsList);
		}

		#region Overrides of CommonAbstractViewer<InPatientRoomBed_cu>

		public override IMVCController<InPatientRoomClassification_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.InPatientRoomClassificationViewer; }
		}

		public override string HeaderTitle
		{
			get { return "تصنيفات غرف الإقامة"; }
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

		public object InPatientRoomType
		{
			get { return lkeInPatientRoomType.EditValue; }
			set { lkeInPatientRoomType.EditValue = value; }
		}

		public bool HasMainPatientPricing
		{
			get
			{
				return (spnPricePerDay_MainPatient.EditValue != null && Convert.ToDouble(spnPricePerDay_MainPatient.EditValue) > 0) ||
					   (spnMinAddmissionPrice_MainPatient.EditValue != null &&
						Convert.ToDouble(spnMinAddmissionPrice_MainPatient.EditValue) > 0);
			}
		}

		public object PricePerDay_MainPatient
		{
			get { return spnPricePerDay_MainPatient.EditValue; }
			set { spnPricePerDay_MainPatient.EditValue = value; }
		}

		public object MinimumAddmissionAmount_MainPatient
		{
			get { return spnMinAddmissionPrice_MainPatient.EditValue; }
			set { spnMinAddmissionPrice_MainPatient.EditValue = value; }
		}

		public bool HasCompanionPricing
		{
			get
			{
				return (spnPricePerDay_CompanionPatient.EditValue != null && Convert.ToDouble(spnPricePerDay_CompanionPatient.EditValue) > 0) ||
					   (spnMinAddmissionPrice_MainPatient.EditValue != null &&
						Convert.ToDouble(spnMinAddmissionPrice_MainPatient.EditValue) > 0);
			}
		}

		public object PricePerDay_CompanionPatient
		{
			get { return spnPricePerDay_CompanionPatient.EditValue; }
			set { spnPricePerDay_CompanionPatient.EditValue = value; }
		}

		public object MinimumAddmissionAmount_CompanionPatient
		{
			get { return spnMinAddmissionPrice_CompanionPatient.EditValue; }
			set { spnMinAddmissionPrice_CompanionPatient.EditValue = value; }
		}

		#endregion
	}
}
