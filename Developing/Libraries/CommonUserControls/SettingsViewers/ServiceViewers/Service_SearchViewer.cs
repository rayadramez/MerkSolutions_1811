using System;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.ServiceViewers
{
	public partial class Service_SearchViewer : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractSearchViewer<Service_cu>,
		IServiceViewer
	{
		public Service_SearchViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_Service_SearchViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<Service_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.Service_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "الخـدمــات"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_Service_SearchViewer; }
		}

		#endregion

		#region Implementation of IServiceViewer

		public object Name_P
		{
			get { return txtNameP.EditValue; }
			set { txtNameP.EditValue = value; }
		}

		public object Name_S
		{
			get { return txtNameS.EditValue; }
			set { txtNameS.EditValue = value; }
		}

		public object ServiceCategory_CU_ID
		{
			get { return lkeServiceCategory.EditValue; }
			set { lkeServiceCategory.EditValue = value; }
		}

		public object ServiceType_P_ID { get; set; }

		public object ParentService_CU_ID { get; set; }

		public object InternalCode
		{
			get { return txtInternalCode.EditValue; }
			set { txtInternalCode.EditValue = value; }
		}

		public object EnforceCategorization
		{
			get { return chkEnforceCategorization.Checked; }
			set { chkEnforceCategorization.Checked = Convert.ToBoolean(value); }
		}

		public object IsDailyCharged
		{
			get { return chkIsDailyCharge.Checked; }
			set { chkIsDailyCharge.Checked = Convert.ToBoolean(value); }
		}

		public object DefaultPriceFrom
		{
			get { return spnDefaultPriceFrom.EditValue; }
			set { spnDefaultPriceFrom.EditValue = value; }
		}

		public object DefaultPriceTo
		{
			get { return spnDefaultPriceTo.EditValue; }
			set { spnDefaultPriceTo.EditValue = value; }
		}

		public object AllowAddmission
		{
			get { return chkAllowAdmission.Checked; }
			set { chkAllowAdmission.Checked = Convert.ToBoolean(value); }
		}

		public object Description
		{
			get { return txtDescription.EditValue; }
			set { txtDescription.EditValue = value; }
		}

		#endregion
	}
}
