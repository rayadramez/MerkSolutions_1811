using System.Collections.Generic;
using System.Windows.Forms;
using CommonControlLibrary;
using CommonControlLibrary.ControlsConstructors;
using DevExpress.XtraGrid.Views.Grid;
using MerkDataBaseBusinessLogicProject;

namespace CommonUserControls.InvoiceViewers
{
	public partial class InPatient_InvoiceDetailsGridViewer_Accommodation : DevExpress.XtraEditors.XtraUserControl
	{
		public List<InvoiceDetail_Accommodation> List_AccommodationServicesList = new List<InvoiceDetail_Accommodation>();

		public InPatient_InvoiceDetailsGridViewer_Accommodation()
		{
			InitializeComponent();
			CommonViewsActions.SetupSyle(this);
			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_InvoiceDetailsGrid_Accommodation);
		}

		public void Initialize(List<InvoiceDetail_Accommodation> accommodationServicesList)
		{
			List_AccommodationServicesList = accommodationServicesList;

			CommonViewsActions.SetupGridControl(grd_InvoiceDetail_Accommodation_MainPatient,
				Resources.LocalizedRes.grd_InvoiceDetail_Accommodation, false);
			CommonViewsActions.SetupGridControl(grd_InvoiceDetail_Accommodation_CompanionPatient,
				Resources.LocalizedRes.grd_InvoiceDetail_Accommodation, false);
			CommonViewsActions.SetupGridControl(grd_InvoiceDetail_Accommodation_OtherServices,
				Resources.LocalizedRes.grd_InvoiceDetail_Accommodation, false);

			CommonViewsActions.SetupGridView((GridView)grd_InvoiceDetail_Accommodation_MainPatient.MainView, null, new GridControlSettings()
			{
				Editable = true,
				ReadOnly = false,
				HasDeleteColumn = true,
				BeforeOnDelete = (obj) =>
				{
					DialogResult userChoise = MessageBox.Show(this, "هل تريد مسح الخدمة؟", "تأكيد المسح",
						MessageBoxButtons.YesNo);
					if (userChoise == DialogResult.Yes)
					{
						if (obj == null)
						{
							MessageBox.Show("لا يوجد عنصر تم اختياره");
							return false;
						}
						return true;
					}
					return false;
				},

			});

			CommonViewsActions.SetupGridView((GridView)grd_InvoiceDetail_Accommodation_CompanionPatient.MainView, null, new GridControlSettings()
			{
				Editable = true,
				ReadOnly = false,
				HasDeleteColumn = true,
				BeforeOnDelete = (obj) =>
				{
					DialogResult userChoise = MessageBox.Show(this, "هل تريد مسح الخدمة؟", "تأكيد المسح",
						MessageBoxButtons.YesNo);
					if (userChoise == DialogResult.Yes)
					{
						if (obj == null)
						{
							MessageBox.Show("لا يوجد عنصر تم اختياره");
							return false;
						}
						return true;
					}
					return false;
				},

			});

			CommonViewsActions.SetupGridView((GridView) grd_InvoiceDetail_Accommodation_OtherServices.MainView, null,
				new GridControlSettings()
				{
					Editable = true,
					ReadOnly = false,
					HasDeleteColumn = true,
					BeforeOnDelete = (obj) =>
					{
						DialogResult userChoise = MessageBox.Show(this, "هل تريد مسح الخدمة؟", "تأكيد المسح", MessageBoxButtons.YesNo);
						if (userChoise == DialogResult.Yes)
						{
							if (obj == null)
							{
								MessageBox.Show("لا يوجد عنصر تم اختياره");
								return false;
							}
							return true;
						}
						return false;
					},
				});

			grd_InvoiceDetail_Accommodation_MainPatient.DataSource = List_AccommodationServicesList;
		}
	}
}
