using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.LocationViewers
{
	public partial class Location_SearchViewer_UC : 
		//UserControl
		CommonAbstractSearchViewer<Location_cu>,
		ILocationViewer
	{
		public Location_SearchViewer_UC()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_Location_SearchViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<Location_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.Location_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return ".... مـواقـــع المنظمــــة ...."; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_Location_SearchViewer; }
		}

		#endregion

		#region Implementation of ILocationViewer

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

		public object Country_CU_ID
		{
			get { return lkeCountry.EditValue; }
			set { lkeCountry.EditValue = value; }
		}

		public object City_CU_ID
		{
			get { return lkeCity.EditValue; }
			set { lkeCity.EditValue = value; }
		}

		public object Region_CU_ID
		{
			get { return lkeRegion.EditValue; }
			set { lkeRegion.EditValue = value; }
		}

		public object Territory_CU_ID
		{
			get { return lkeTerritory.EditValue; }
			set { lkeTerritory.EditValue = value; }
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

		public object Address
		{
			get { return txtAddress.EditValue; }
			set { txtAddress.EditValue = value; }
		}

		#endregion
	}
}
