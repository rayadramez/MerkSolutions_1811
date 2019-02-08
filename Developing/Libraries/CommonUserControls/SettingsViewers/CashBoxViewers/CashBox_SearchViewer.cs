using System;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.CashBoxViewers
{
	public partial class CashBox_SearchViewer : 
		//UserControl
		CommonAbstractSearchViewer<CashBox_cu>,
		ICashBoxViewer
	{
		public CashBox_SearchViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_CashBox_SearchViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<CashBox_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.CashBoxViewer; }
		}

		public override string HeaderTitle
		{
			get { return "الخـزائــــن"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeFloor, Floor_cu.ItemsList, "FloorFullName");
			CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount,
				ChartOfAccount_cu.ItemsList.FindAll(
					item =>
						Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
							.Equals(Convert.ToInt32(DB_ChartOfAccountCodeMargin.FifthMargin))));
		}

		public override void ClearControls()
		{
			txtNameP.EditValue = null;
			txtNameS.EditValue = null;
			txtInternalCode.EditValue = null;
			txtDescription.EditValue = null;
			chkIsMan.Checked = false;
			lkeFloor.EditValue = null;
			lkeChartOfAccount.EditValue = null;
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_CashBox_SearchViewer; }
		}

		#endregion

		#region Implementation of ICashBoxViewer

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

		public object Floor_CU_ID
		{
			get { return lkeFloor.EditValue; }
			set { lkeFloor.EditValue = value; }
		}

		public object IsMain
		{
			get { return chkIsMan.Checked; }
			set { chkIsMan.Checked = Convert.ToBoolean(value); }
		}

		public object ChartOfAccount_CU_ID
		{
			get { return lkeChartOfAccount.EditValue; }
			set { lkeChartOfAccount.EditValue = value; }
		}

		public object InternalCode
		{
			get { return txtInternalCode.EditValue; }
			set { txtInternalCode.EditValue = value; }
		}

		public object Description
		{
			get { return txtDescription.EditValue; }
			set { txtDescription.EditValue = value; }
		}

		#endregion
	}
}
