using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.CashBoxTranasctionType_GeneralChartOfAccountType_Viewers
{
	public partial class CashBoxTranasctionType_GeneralChartOfAccountType_SearchViewer : 
		//UserControl
		CommonAbstractSearchViewer<CashBoxTransactionType_GeneralChartOfAccountType_cu>,
		ICashBoxTransactionType_GeneralChartOfAccountType_Viewer
	{
		public CashBoxTranasctionType_GeneralChartOfAccountType_SearchViewer()
		{
			InitializeComponent();
		}

		#region Overrides of CommonAbstractViewer<CashBoxTransactionType_GeneralChartOfAccountType_cu>

		public override object ViewerID
		{
			get { return (int) ViewerName.CashBoxTransactionType_GeneralChartOfAccountType_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "ربـــط المعـامـــلات الماليـــة بعمليـــات الخزائـــن"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_CashBoxTransactionType_GeneralChartOfAccountType_SearchViewer; }
		}

		#endregion

		#region Implementation of ICashBoxTransactionType_GeneralChartOfAccountType_Viewer

		public List<CashBoxTransactionType_GeneralChartOfAccountType_cu> List_CashBoxTransactionType_GeneralChartOfAccountType
		{ get; set; }

		#endregion
	}
}
