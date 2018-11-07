﻿using System.Windows.Forms;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.MedicationCategoriesViewers
{
	public partial class MedicationCategory_SearchViewer : 
		//UserControl
		CommonAbstractSearchViewer<MedicationCategory_cu>,
		IMedicationCategoryViewer
	{
		public MedicationCategory_SearchViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_MedicationCategory_SearchViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<MedicationCategory_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.MedicationCategory_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "تصنيفــــــات الأدويـــــــــة"; }
		}

		public override void ClearControls()
		{
			Name_P = null;
			Name_S = null;
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_MedicationCategory_SearchViewer; }
		}

		#endregion

		#region Implementation of IMedicationCategoryViewer

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

		#endregion
	}
}