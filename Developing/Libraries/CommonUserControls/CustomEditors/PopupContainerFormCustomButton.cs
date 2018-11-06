using System;
using System.Drawing;
using DevExpress.XtraEditors.Popup;

namespace CommonUserControls.CustomEditors
{
	public class PopupContainerFormCustomButton : PopupGridLookUpEditForm
	{
		public PopupContainerFormCustomButton(PopupContainerEditCustomButton ownerEdit) : base(ownerEdit) { }

		protected override void Dispose(bool disposing)
		{
			PopupContainerEditCustomButton currentEditor = OwnerEdit as PopupContainerEditCustomButton;
			if (currentEditor != null)
			{
				foreach (SimplePopupButton sbCustomButton in currentEditor.Properties.ButtonsForPopupWindow)
				{
					if (Controls.Contains(sbCustomButton))
					{
						Controls.Remove(sbCustomButton);
						if (sbCustomButton.DefaultBehavior == PopupButtonStyle.Ok) sbCustomButton.Click -= new EventHandler(customButton_OkClick);
						if (sbCustomButton.DefaultBehavior == PopupButtonStyle.Close) sbCustomButton.Click -= new EventHandler(customButton_CancelClick);
					}
				}
			}
			base.Dispose(disposing);
		}

		new RepositoryItemPopupContainerEditCustomButton Properties
		{
			get
			{
				PopupContainerEditCustomButton edit = OwnerEdit as PopupContainerEditCustomButton;
				if (edit == null) return null;
				return edit.Properties;
			}
		}

		protected override PopupBaseFormViewInfo CreateViewInfo()
		{
			return new PopupFormCustomButtonsViewInfo(this);
		}

		protected override void UpdateControlPositionsCore()
		{
			base.UpdateControlPositionsCore();
			PopupContainerEditCustomButton currentEditor = OwnerEdit as PopupContainerEditCustomButton;
			PopupFormCustomButtonsViewInfo currentViewInfo = (ViewInfo as PopupFormCustomButtonsViewInfo);
			if (currentEditor != null)
			{
				Rectangle customButtonsBound = currentViewInfo.CustomButtonsRect;
				int buttonsForPopupWindowCount = currentEditor.Properties.ButtonsForPopupWindow.Count;
				if (buttonsForPopupWindowCount > 0)
				{
					int iMaxCustomButtonWidth = (customButtonsBound.Width - (buttonsForPopupWindowCount - 1) * PopupFormCustomButtonsViewInfo.sideIndent) / buttonsForPopupWindowCount;
					int iCurrentX = customButtonsBound.X;
					if (currentEditor.Properties.ShowCustomButton)
					{
						fShowOkButton = false;
						fCloseButtonStyle = BlobCloseButtonStyle.None;
						foreach (SimplePopupButton customButton in currentEditor.Properties.ButtonsForPopupWindow)
						{
							int iCurrentCustomButtonWidth = customButton.Size.Width;
							if (iCurrentCustomButtonWidth > iMaxCustomButtonWidth) iCurrentCustomButtonWidth = iMaxCustomButtonWidth;
							customButton.Bounds = new Rectangle(iCurrentX,
																customButtonsBound.Y,
																iCurrentCustomButtonWidth,
																customButtonsBound.Height);
							Controls.Add(customButton);
							iCurrentX += iCurrentCustomButtonWidth + PopupFormCustomButtonsViewInfo.sideIndent;
							if (customButton.DefaultBehavior == PopupButtonStyle.Ok) customButton.Click += new EventHandler(customButton_OkClick);
							if (customButton.DefaultBehavior == PopupButtonStyle.Close) customButton.Click += new EventHandler(customButton_CancelClick);
						}
					}
				}
			}
		}

		void customButton_CancelClick(object sender, EventArgs e)
		{
			OwnerEdit.CancelPopup();
		}

		void customButton_OkClick(object sender, EventArgs e)
		{
			OwnerEdit.ClosePopup();
		}
	}
}
