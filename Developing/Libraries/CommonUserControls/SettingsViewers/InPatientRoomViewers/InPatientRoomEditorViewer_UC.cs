using System;
using System.Collections.Generic;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InPatientRoomViewers
{
	public partial class InPatientRoomEditorViewer_UC : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<InPatientRoom_cu>,
		IInPatientRoomViewer
	{
		public InPatientRoomEditorViewer_UC()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_InPatientRoomEditor);
			CommonViewsActions.SetupSyle(this);

			txtNameP.Focus();
		}

		#region Overrides of CommonAbstractViewer

		public override List<IViewer> GetRelatedViewers()
		{
			return RelatedViewers;
		}

		public override IMVCController<InPatientRoom_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.InPatientRoomViewer; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_ChartOfAccountMargin; }
		}

		public List<IViewer> RelatedViewers { get; set; }

		public override void ClearControls()
		{

		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeInPatientRommClassification, InPatientRoomClassification_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeFloor, Floor_cu.ItemsList);
		}

		public override string HeaderTitle
		{
			get { return "غرف الإقامة"; }
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
			get { return lkeFloor.EditValue; }
			set { lkeFloor.EditValue = value; }
		}

		public object InPatientRoomClassification
		{
			get { return lkeInPatientRommClassification.EditValue; }
			set { lkeInPatientRommClassification.EditValue = value; }
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
