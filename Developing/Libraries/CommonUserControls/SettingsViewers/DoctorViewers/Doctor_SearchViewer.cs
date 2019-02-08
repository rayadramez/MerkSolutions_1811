using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.DoctorViewers
{
	public partial class Doctor_SearchViewer : 
		//UserControl
		CommonAbstractSearchViewer<Doctor_cu>,
		IDoctorViewer
	{
		public Doctor_SearchViewer()
		{
			InitializeComponent();

			//CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_Doctor_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<Doctor_cu>

		public override object ViewerID
		{
			get { return ViewerName.User_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "بيانــات الأطبـــاء"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_Doctor_SearchViewer; }
		}

		#endregion

		#region Implementation of IDoctorViewer

		public object FirstName_P { get; set; }
		public object SecondName_P { get; set; }
		public object ThirdName_P { get; set; }
		public object FourthName_P { get; set; }
		public object Gender { get; set; }
		public object MaritalStatusID { get; set; }
		public object BirthDate { get; set; }
		public object InternalCode { get; set; }
		public object Mobile1 { get; set; }
		public object Mobile2 { get; set; }
		public object Phone1 { get; set; }
		public object Phone2 { get; set; }
		public object Address { get; set; }
		public object EMail { get; set; }
		public object IdentificationCardTypeID { get; set; }
		public object IdentificationCardNumber { get; set; }
		public object IdentificationCardIssuingDate { get; set; }
		public object IdentificationCardExpirationDate { get; set; }
		public object LoginName { get; set; }
		public object Password { get; set; }
		public object ConfirmationPassword { get; set; }
		public object DoctorRankID { get; set; }
		public object DoctorSpecializationID { get; set; }
		public object DoctorCategoryID { get; set; }
		public object DoctorTaxTypeID { get; set; }
		public object DoctorProfessionalFees { get; set; }
		public object IsInternal { get; set; }
		public object PrivateMobile { get; set; }
		public object Description { get; set; }

		#endregion
	}
}
