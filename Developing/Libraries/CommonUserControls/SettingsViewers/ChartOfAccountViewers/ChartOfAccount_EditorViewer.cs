using System;
using System.Windows.Forms;
using CommonControlLibrary;
using DevExpress.XtraEditors;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using DevExpress.XtraLayout.Utils;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.ChartOfAccountViewers
{
	public partial class ChartOfAccount_EditorViewer :
		//UserControl
		CommonAbstractEditorViewer<ChartOfAccount_cu>,
		IChartOfAccountViewer
	{
		public ChartOfAccount_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_ChartOfAccount_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<ChartOfAccount_cu>

		public override IMVCController<ChartOfAccount_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.ChartOfAccountViewer; }
		}

		public override string HeaderTitle
		{
			get { return "الحســـابات"; }
		}

		public override void FillControls()
		{
			treeList1.ParentFieldName = ChartOfAccount_cu.ParentChartOfAccount_CU_ID_ColumnaName;
			treeList1.KeyFieldName = ChartOfAccount_cu.ID_ColumnaName;
			treeList1.DataSource = ChartOfAccount_cu.ItemsList;
			ChartOfAccountCodeMargin_P_ID = 1;

			lytParent.Visibility =
				lytParentCode.Visibility =
					lytParentEmpty.Visibility = chkMargin_First.Checked ? LayoutVisibility.Never : LayoutVisibility.Always;

			if (ChartOfAccount_cu.ItemsList.Count == 0)
			{
				chkMargin_Second.Enabled = false;
				chkMargin_Third.Enabled = false;
				chkMargin_Fourth.Enabled = false;
				chkMargin_Fifth.Enabled = false;
				ChartOfAccountCodeMargin_P_ID = (int) DB_ChartOfAccountCodeMargin.FirstMargin;
			}
			else
			{
				chkMargin_Second.Enabled = true;
				chkMargin_Third.Enabled = true;
				chkMargin_Fourth.Enabled = true;
				chkMargin_Fifth.Enabled = true;
			}

			spnSerial.EditValue =
					AccountingBusinessLogicEngine.GetNextChartOfAccountSerial((DB_ChartOfAccountCodeMargin)ChartOfAccountCodeMargin_P_ID,
						lkeParentChartOfAccount.EditValue);
		}

		public override void ClearControls()
		{
			txtNameP.EditValue = null;
			txtNameS.EditValue = null;
			lkeParentChartOfAccount.EditValue = null;
			txtDescription.EditValue = null;
			ChartOfAccountCodeMargin_P_ID = 1;
			chkDebit.Checked = true;
			spnSerial.EditValue = null;
		}

		#endregion

		#region Implementation of IChartOfAccountViewer

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

		public object ParentChartOfAccount_CU_ID
		{
			get { return lkeParentChartOfAccount.EditValue; }
			set { lkeParentChartOfAccount.EditValue = value; }
		}

		public object Serial
		{
			get { return spnSerial.EditValue; }
			set { spnSerial.EditValue = value; }
		}

		public object ChartOfAccountCodeMargin_P_ID
		{
			get
			{
				if (chkMargin_First.Checked)
					return (int) DB_ChartOfAccountCodeMargin.FirstMargin;
				if (chkMargin_Second.Checked)
					return (int)DB_ChartOfAccountCodeMargin.SecondMargin;
				if (chkMargin_Third.Checked)
					return (int)DB_ChartOfAccountCodeMargin.ThirdMargin;
				if (chkMargin_Fourth.Checked)
					return (int)DB_ChartOfAccountCodeMargin.FourthMargin;
				if (chkMargin_Fifth.Checked)
					return (int)DB_ChartOfAccountCodeMargin.FifthMargin;
				return (int)DB_ChartOfAccountCodeMargin.None;
			}
			set
			{
				DB_ChartOfAccountCodeMargin codeMarginValue = (DB_ChartOfAccountCodeMargin) value;
				switch (codeMarginValue)
				{
					case DB_ChartOfAccountCodeMargin.FirstMargin:
						chkMargin_First.Checked = true;
						break;
					case DB_ChartOfAccountCodeMargin.SecondMargin:
						chkMargin_Second.Checked = true;
						break;
					case DB_ChartOfAccountCodeMargin.ThirdMargin:
						chkMargin_Third.Checked = true;
						break;
					case DB_ChartOfAccountCodeMargin.FourthMargin:
						chkMargin_Fourth.Checked = true;
						break;
					case DB_ChartOfAccountCodeMargin.FifthMargin:
						chkMargin_Fifth.Checked = true;
						break;
				}
			}
		}

		public object IsDebit
		{
			get { return chkDebit.Checked; }
			set { chkDebit.Checked = Convert.ToBoolean(value); }
		}

		public object Description
		{
			get { return txtDescription.EditValue; }
			set { txtDescription.EditValue = value; }
		}

		#endregion

		private void lkeParentChartOfAccount_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeParentChartOfAccount.EditValue == null)
				return;

			long parent = AccountingBusinessLogicEngine.GetChartOfAccountSerial(lkeParentChartOfAccount.EditValue, null);
			spnParentSerial.EditValue = parent;
			chkDebit.Checked = AccountingBusinessLogicEngine.IsDebit(lkeParentChartOfAccount.EditValue);
			chkCredit.Checked = !AccountingBusinessLogicEngine.IsDebit(lkeParentChartOfAccount.EditValue);

			spnSerial.EditValue =
				AccountingBusinessLogicEngine.GetNextChartOfAccountSerial((DB_ChartOfAccountCodeMargin)ChartOfAccountCodeMargin_P_ID,
					lkeParentChartOfAccount.EditValue);
			if (spnSerial.EditValue == string.Empty)
				XtraMessageBox.Show("لا يمكنك الإضافة", "تنبيـــه", MessageBoxButtons.OK, MessageBoxIcon.Hand,
					MessageBoxDefaultButton.Button1);
		}

		private void chkDebit_CheckedChanged(object sender, EventArgs e)
		{
			CommonViewsActions.FillGridlookupEdit(lkeParentChartOfAccount,
				AccountingBusinessLogicEngine.GetChartOfAccountOfPreviousCodeMargin(ChartOfAccountCodeMargin_P_ID,
					chkDebit.Checked));
			//if (MerkDBBusinessLogicLibrary.IsDebit(ParentChartOfAccount_CU_ID))
			//{
			//	XtraMessageBox.Show("لا يمكنــك إختيــار دائـــــن، الحســاب الأكبــر طبيعتــه مـديــــن", "تنبيــــه",
			//						MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
			//	chkDebit.Checked = true;
			//}
			//else
			//{
			//	XtraMessageBox.Show("لا يمكنــك إختيــار مـديــــن، الحســاب الأكبــر طبيعتــه دائـــــن", "تنبيــــه",
			//						MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
			//	chkCredit.Checked = true;
			//}
		}

		private void chkCredit_CheckedChanged(object sender, EventArgs e)
		{
			CommonViewsActions.FillGridlookupEdit(lkeParentChartOfAccount,
				AccountingBusinessLogicEngine.GetChartOfAccountOfPreviousCodeMargin(ChartOfAccountCodeMargin_P_ID,
					chkDebit.Checked));
			//if (MerkDBBusinessLogicLibrary.IsDebit(ParentChartOfAccount_CU_ID))
			//{
			//	XtraMessageBox.Show("لا يمكنــك إختيــار دائـــــن، الحســاب الأكبــر طبيعتــه مـديــــن", "تنبيــــه",
			//						MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
			//	chkDebit.Checked = true;
			//}
			//else
			//{
			//	XtraMessageBox.Show("لا يمكنــك إختيــار مـديــــن، الحســاب الأكبــر طبيعتــه دائـــــن", "تنبيــــه",
			//						MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
			//	chkCredit.Checked = true;
			//}
		}

		private void spnSerial_EditValueChanged(object sender, EventArgs e)
		{
		}

		private void chkMargin_First_CheckedChanged(object sender, EventArgs e)
		{
			ChartOfAccountCodeMargin_P_ID = (int) DB_ChartOfAccountCodeMargin.FirstMargin;
			lytParent.Visibility = lytParentCode.Visibility = lytParentEmpty.Visibility = LayoutVisibility.Never;
			lkeParentChartOfAccount.EditValue = null;

			CommonViewsActions.FillGridlookupEdit(lkeParentChartOfAccount,
				AccountingBusinessLogicEngine.GetChartOfAccountOfPreviousCodeMargin(ChartOfAccountCodeMargin_P_ID, chkDebit.Checked));

			int allowedNumberOfDigits =
				AccountingBusinessLogicEngine.GetChartOfAccountCodeMarginNumberOfDigits(ChartOfAccountCodeMargin_P_ID);
			spnSerial.Properties.MaxLength = allowedNumberOfDigits;

			lytParent.Visibility =
				lytParentCode.Visibility =
					lytParentEmpty.Visibility = chkMargin_First.Checked ? LayoutVisibility.Never : LayoutVisibility.Always;

			spnSerial.EditValue =
				AccountingBusinessLogicEngine.GetNextChartOfAccountSerial((DB_ChartOfAccountCodeMargin)ChartOfAccountCodeMargin_P_ID,
					lkeParentChartOfAccount.EditValue);

			//chkCredit.Enabled = true;
			//chkDebit.Enabled = true;
		}

		private void chkMargin_Second_CheckedChanged(object sender, EventArgs e)
		{
			ChartOfAccountCodeMargin_P_ID = (int)DB_ChartOfAccountCodeMargin.SecondMargin;
			lytParent.Visibility = lytParentCode.Visibility = lytParentEmpty.Visibility = LayoutVisibility.Always;
			lkeParentChartOfAccount.EditValue = null;

			CommonViewsActions.FillGridlookupEdit(lkeParentChartOfAccount,
				AccountingBusinessLogicEngine.GetChartOfAccountOfPreviousCodeMargin(ChartOfAccountCodeMargin_P_ID, chkDebit.Checked));

			int allowedNumberOfDigits =
				AccountingBusinessLogicEngine.GetChartOfAccountCodeMarginNumberOfDigits(ChartOfAccountCodeMargin_P_ID);
			spnSerial.Properties.MaxLength = allowedNumberOfDigits;

			lytParent.Visibility =
				lytParentCode.Visibility =
					lytParentEmpty.Visibility = chkMargin_First.Checked ? LayoutVisibility.Never : LayoutVisibility.Always;

			spnSerial.EditValue =
					AccountingBusinessLogicEngine.GetNextChartOfAccountSerial((DB_ChartOfAccountCodeMargin)ChartOfAccountCodeMargin_P_ID,
						lkeParentChartOfAccount.EditValue);
		}

		private void chkMargin_Third_CheckedChanged(object sender, EventArgs e)
		{
			ChartOfAccountCodeMargin_P_ID = (int)DB_ChartOfAccountCodeMargin.ThirdMargin;
			lytParent.Visibility = lytParentCode.Visibility = lytParentEmpty.Visibility = LayoutVisibility.Always;
			lkeParentChartOfAccount.EditValue = null;

			CommonViewsActions.FillGridlookupEdit(lkeParentChartOfAccount,
				AccountingBusinessLogicEngine.GetChartOfAccountOfPreviousCodeMargin(ChartOfAccountCodeMargin_P_ID, chkDebit.Checked));

			int allowedNumberOfDigits =
				AccountingBusinessLogicEngine.GetChartOfAccountCodeMarginNumberOfDigits(ChartOfAccountCodeMargin_P_ID);
			spnSerial.Properties.MaxLength = allowedNumberOfDigits;

			lytParent.Visibility =
				lytParentCode.Visibility =
					lytParentEmpty.Visibility = chkMargin_First.Checked ? LayoutVisibility.Never : LayoutVisibility.Always;

			spnSerial.EditValue =
					AccountingBusinessLogicEngine.GetNextChartOfAccountSerial((DB_ChartOfAccountCodeMargin)ChartOfAccountCodeMargin_P_ID,
						lkeParentChartOfAccount.EditValue);
		}

		private void chkMargin_Fourth_CheckedChanged(object sender, EventArgs e)
		{
			ChartOfAccountCodeMargin_P_ID = (int)DB_ChartOfAccountCodeMargin.FourthMargin;
			lytParent.Visibility = lytParentCode.Visibility = lytParentEmpty.Visibility = LayoutVisibility.Always;
			lkeParentChartOfAccount.EditValue = null;

			CommonViewsActions.FillGridlookupEdit(lkeParentChartOfAccount,
				AccountingBusinessLogicEngine.GetChartOfAccountOfPreviousCodeMargin(ChartOfAccountCodeMargin_P_ID, chkDebit.Checked));

			int allowedNumberOfDigits =
				AccountingBusinessLogicEngine.GetChartOfAccountCodeMarginNumberOfDigits(ChartOfAccountCodeMargin_P_ID);
			spnSerial.Properties.MaxLength = allowedNumberOfDigits;

			lytParent.Visibility =
				lytParentCode.Visibility =
					lytParentEmpty.Visibility = chkMargin_First.Checked ? LayoutVisibility.Never : LayoutVisibility.Always;

			spnSerial.EditValue =
					AccountingBusinessLogicEngine.GetNextChartOfAccountSerial((DB_ChartOfAccountCodeMargin)ChartOfAccountCodeMargin_P_ID,
						lkeParentChartOfAccount.EditValue);
		}

		private void chkMargin_Fifth_CheckedChanged(object sender, EventArgs e)
		{
			ChartOfAccountCodeMargin_P_ID = (int)DB_ChartOfAccountCodeMargin.FifthMargin;
			lytParent.Visibility = lytParentCode.Visibility = lytParentEmpty.Visibility = LayoutVisibility.Always;
			lkeParentChartOfAccount.EditValue = null;

			CommonViewsActions.FillGridlookupEdit(lkeParentChartOfAccount,
				AccountingBusinessLogicEngine.GetChartOfAccountOfPreviousCodeMargin(ChartOfAccountCodeMargin_P_ID, chkDebit.Checked));

			int allowedNumberOfDigits =
				AccountingBusinessLogicEngine.GetChartOfAccountCodeMarginNumberOfDigits(ChartOfAccountCodeMargin_P_ID);
			spnSerial.Properties.MaxLength = allowedNumberOfDigits;

			lytParent.Visibility =
				lytParentCode.Visibility =
					lytParentEmpty.Visibility = chkMargin_First.Checked ? LayoutVisibility.Never : LayoutVisibility.Always;

			spnSerial.EditValue =
					AccountingBusinessLogicEngine.GetNextChartOfAccountSerial((DB_ChartOfAccountCodeMargin)ChartOfAccountCodeMargin_P_ID,
						lkeParentChartOfAccount.EditValue);
		}
	}
}
