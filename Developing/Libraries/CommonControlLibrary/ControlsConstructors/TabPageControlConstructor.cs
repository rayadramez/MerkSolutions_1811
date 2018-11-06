using System.Windows.Forms;
using DevExpress.Utils;

namespace CommonControlLibrary.ControlsConstructors
{
	public class TabPageControlConstructor
	{
		public string HeaderTitle { get; set; }
		public int TabIndex { get; set; }
		public string SkinName { get; set; }
		public bool PageEnabled { get; set; }
		public bool PageVisible { get; set; }
		public DefaultBoolean ShowCloseButton { get; set; }
		public BorderStyle BorderStyle { get; set; }
		public DockStyle DockStyle { get; set; }

		public TabPageControlConstructor()
		{
			HeaderTitle = "";
			TabIndex = 0;
			SkinName = "DevExpress Dark Style";
			PageEnabled = true;
			PageVisible = true;
			ShowCloseButton = DefaultBoolean.False;
			BorderStyle = BorderStyle.None;
			DockStyle = DockStyle.Fill;
		}

		public TabPageControlConstructor(string headerTitle, int tabIndex, string skinName, bool pageEnabled, bool pageVisible,
			DefaultBoolean showCloseButton, BorderStyle borderStyle, DockStyle dockStyle)
		{
			HeaderTitle = headerTitle;
			TabIndex = tabIndex;
			SkinName = skinName;
			PageEnabled = pageEnabled;
			PageVisible = pageVisible;
			ShowCloseButton = showCloseButton;
			BorderStyle = borderStyle;
			DockStyle = dockStyle;
		}
	}
}
