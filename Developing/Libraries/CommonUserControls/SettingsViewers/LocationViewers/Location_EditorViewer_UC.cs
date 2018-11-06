using System;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.LocationViewers
{
	public partial class Location_EditorViewer_UC : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<Location_cu>,
		ILocationViewer
	{
		public Location_EditorViewer_UC()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_Location_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		private void lkeCountry_EditValueChanged(object sender, System.EventArgs e)
		{
			if (lkeCountry.EditValue == null)
			{
				lkeCity.Properties.ReadOnly = true;
				City_CU_ID = null;
				return;
			}

			CommonViewsActions.FillGridlookupEdit(lkeCity,
				City_cu.ItemsList.FindAll(item => Convert.ToInt32(item.Country_CU_ID).Equals(Convert.ToInt32(lkeCountry.EditValue))));
			lkeCity.Properties.ReadOnly = false;
		}

		private void lkeCity_EditValueChanged(object sender, System.EventArgs e)
		{
			if (lkeCity.EditValue == null)
			{
				lkeRegion.Properties.ReadOnly = true;
				Region_CU_ID = null;
				return;
			}

			CommonViewsActions.FillGridlookupEdit(lkeRegion,
				Region_cu.ItemsList.FindAll(item => Convert.ToInt32(item.City_CU_ID).Equals(Convert.ToInt32(lkeCity.EditValue))));
			lkeRegion.Properties.ReadOnly = false;
		}

		private void lkeRegion_EditValueChanged(object sender, System.EventArgs e)
		{
			
		}

		#region Overrides of CommonAbstractViewer<Location_cu>

		public override IMVCController<Location_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int) ViewerName.Location_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "مـواقـــع المنظمــــة"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeCountry, Country_cu.ItemsList);
		}

		public override void ClearControls()
		{
			txtNameP.EditValue = null;
			txtNameS.EditValue = null;
			txtInternalCode.EditValue = null;
			txtDescription.EditValue = null;
			lkeCity.EditValue = null;
			lkeCountry.EditValue = null;
			lkeRegion.EditValue = null;
			lkeTerritory.EditValue = null;
			txtAddress.EditValue = null;
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
