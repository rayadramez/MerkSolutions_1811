using System.Windows.Forms;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.RawMaterialTransaction
{
	public partial class RawMaterialTransaction_SearchViewer : 
		//UserControl
		CommonAbstractSearchViewer<RawMaterialTranasction>,
		IRawMaterialTransaction_Viewer
	{
		public RawMaterialTransaction_SearchViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1,
				Resources.LocalizedRes.lyt_RawMaterialTransaction_SearchViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<RawMaterialTranasction>

		public override object ViewerID
		{
			get { return (int)ViewerName.RawMaterialTransactions_viewer; }
		}

		public override string HeaderTitle
		{
			get { return "المنتجــــات"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_RawMaterialTransaction_SearchViewer; }
		}

		#endregion

		#region Implementation of IRawMaterialTransaction_Viewer

		public object RawMaterialID
		{
			get { return lkeRawMaterial.EditValue; }
			set { lkeRawMaterial.EditValue = value; }
		}

		public object RawTransactionTypeID
		{
			get
			{
				if (chkConsuming.Checked)
					return (int)DB_RawMaterialTransactionType.Consuming;
				if (chkPurchasing.Checked)
					return (int)DB_RawMaterialTransactionType.Purchasing;
				if (chkSelling.Checked)
					return (int)DB_RawMaterialTransactionType.Selling;
				return -1;
			}
			set
			{
				switch ((DB_RawMaterialTransactionType)value)
				{
					case DB_RawMaterialTransactionType.Consuming:
						chkConsuming.Checked = true;
						break;
					case DB_RawMaterialTransactionType.Purchasing:
						chkPurchasing.Checked = true;
						break;
					case DB_RawMaterialTransactionType.Selling:
						chkSelling.Checked = true;
						break;
				}
			}
		}

		public object ColorID { get; set; }

		public object Count
		{
			get { return spnCount.EditValue; }
			set { spnCount.EditValue = value; }
		}

		public object PuchasingPrice
		{
			get { return spnPrice.EditValue; }
			set { spnPrice.EditValue = value; }
		}

		public object Width
		{
			get { return spnWidth.EditValue; }
			set { spnWidth.EditValue = value; }
		}

		public object Height
		{
			get { return spnHeight.EditValue; }
			set { spnHeight.EditValue = value; }
		}

		public object TransactionDate
		{
			get { return dtDate.EditValue; }
			set { dtDate.EditValue = value; }
		}

		public object DividedTypeID
		{
			get
			{
				throw new System.NotImplementedException();
			}
			set
			{
				throw new System.NotImplementedException();
			}
		}

		public object DividedBy
		{
			get
			{
				throw new System.NotImplementedException();
			}
			set
			{
				throw new System.NotImplementedException();
			}
		}

		#endregion
	}
}
