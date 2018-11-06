using System.Windows.Forms;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;

namespace CommonUserControls.PEMRCommonViewers.PEMR_InternalViewers.Ophthalmology
{
	public partial class PEMR_VisionRefractionDetails_UC : UserControl
	{
		private PEMR_VisionRefraction_UC _visionRefraction;
		private PEMR_ExtraocularMuscles_UC _eomPemrExtraocularMuscles;

		public GetPreviousVisitTiming_VisionRefractionReading_Result Active_VisitTiming_VisionRefractionReading
		{
			get;
			set;
		}

		public GetPreviousVisitTiming_EOMReading_Result Active_VisitTiming_EOMReading { get; set; }

		public PEMR_VisionRefractionDetails_UC()
		{
			InitializeComponent();
		}

		public void Initialize(GetPreviousVisitTiming_VisionRefractionReading_Result active)
		{
			Active_VisitTiming_VisionRefractionReading = active;
			CommonViewsActions.ShowUserControl(ref _visionRefraction, this);
			_visionRefraction.Initialize(ReadingsMode.ViewingPreviousReadings,
				Active_VisitTiming_VisionRefractionReading);
		}

		public void Initialize(GetPreviousVisitTiming_EOMReading_Result active)
		{
			Active_VisitTiming_EOMReading = active;
			CommonViewsActions.ShowUserControl(ref _eomPemrExtraocularMuscles, this);
			_eomPemrExtraocularMuscles.Initialize(ReadingsMode.ViewingPreviousReadings, Active_VisitTiming_EOMReading);
		}
	}
}
