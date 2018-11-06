using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;

namespace CommonControlLibrary.ControlsConstructors
{
	public class TabControlConstructor
	{
		public Control ParentControlToAttach { get; set; }
		public string SkinName { get; set; }
		public DefaultBoolean HeaderAutoFill { get; set; }
		public int SelectedTabIndex { get; set; }
		public DockStyle DockStyle { get; set; }
		public BorderStyles BorderStyles { get; set; }
		public BorderStyles BorderStylePage { get; set; }
		public TabHeaderLocation TabHeaderLocation { get; set; }
		public bool ShowPreviousAndNextButtons{ get; set; }
		public TabButtonShowMode TabButtonShowMode { get; set; }
		public TabOrientation TabOrientation { get; set; }
		public List<TabPageControlConstructor> TabPagesList { get; set; }

		public TabControlConstructor()
		{
			SkinName = "DevExpress Dark Style";
			HeaderAutoFill = DefaultBoolean.True;
			SelectedTabIndex = 0;
			DockStyle = DockStyle.Fill;
			BorderStyles = BorderStyles.Office2003;
			BorderStylePage = BorderStyles.Office2003;
			TabHeaderLocation = TabHeaderLocation.Top;
			ShowPreviousAndNextButtons = true;
			TabButtonShowMode = TabButtonShowMode.Always;
			TabOrientation = TabOrientation.Horizontal;
			if(TabPagesList == null || TabPagesList.Count == 0)
				TabPagesList = new List<TabPageControlConstructor>();
		}

		public TabControlConstructor(Control controlToAttach, string skinName, DefaultBoolean headerAutoFill,
			int selectedtabIndex, DockStyle dockStyle, BorderStyles borderStyles, BorderStyles borderStylePage,
			TabHeaderLocation tabHeaderLocation, bool showPreviousAndNextButtons, TabButtonShowMode tabButtonShowMode,
			TabOrientation tabOrientation, List<TabPageControlConstructor> tabPagesList)
		{
			ParentControlToAttach = controlToAttach;
			SkinName = skinName;
			HeaderAutoFill = headerAutoFill;
			SelectedTabIndex = selectedtabIndex;
			DockStyle = dockStyle;
			BorderStyles = borderStyles;
			BorderStylePage = borderStylePage;
			TabHeaderLocation = tabHeaderLocation;
			ShowPreviousAndNextButtons = showPreviousAndNextButtons;
			TabButtonShowMode = tabButtonShowMode;
			TabOrientation = tabOrientation;
			TabPagesList = tabPagesList;
		}
	}
}
