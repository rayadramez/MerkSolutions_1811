using System.Drawing;
using DevExpress.XtraEditors.Popup;

namespace CommonUserControls.CustomEditors
{
	public class PopupFormCustomButtonsViewInfo : CustomBlobPopupFormViewInfo
	{ // custom properties
		protected Rectangle fCustomButtonsRect;
		protected Size fCustomButtonsSize;
		public Rectangle CustomButtonsRect { get { return fCustomButtonsRect; } }
		public Size CustomButtonSize { get { return fCustomButtonsSize; } }
		public const int sideIndent = 5;

		// constructor
		public PopupFormCustomButtonsViewInfo(PopupBaseForm ownerPopupForm) : base(ownerPopupForm) { }

		// override methods
		protected override void CalcContentRect(Rectangle bounds)
		{
			base.CalcContentRect(bounds);
			fCustomButtonsRect = SizeBarRect;
			fCustomButtonsSize = ButtonSize;
		}
	}
}
