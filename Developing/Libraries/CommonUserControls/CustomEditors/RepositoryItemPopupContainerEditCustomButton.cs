using System;
using System.ComponentModel;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;

namespace CommonUserControls.CustomEditors
{
	public class RepositoryItemPopupContainerEditCustomButton : RepositoryItemGridLookUpEdit
	{
		static RepositoryItemPopupContainerEditCustomButton() { RegisterPopupContainerEditCustomButton(); }

		public static void RegisterPopupContainerEditCustomButton()
		{
			EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(PopupContainerEditCustomButtonEditorName,
				typeof(PopupContainerEditCustomButton), typeof(RepositoryItemPopupContainerEditCustomButton),
				typeof(GridLookUpEditBaseViewInfo), new ButtonEditPainter(), true, null));
		}

		internal const string PopupContainerEditCustomButtonEditorName = "PopupContainerEditCustomButton";

		public RepositoryItemPopupContainerEditCustomButton()
			: base()
		{
			protShowCustomButton = true;
			protButtonsForPopup = new CustomButtonCollection();
			protButtonsForPopup.OnCollectionChanged += ButtonsCollections_OnCollectionChanged;
		}

		void ButtonsCollections_OnCollectionChanged(object sender, CustomButtonCollectionEventArgs e)
		{
			if (e.CurrentAction == CollectionChangeAction.Add)
				e.CustomButton.Click += new EventHandler(CustomButton_Click);
			else
				e.CustomButton.Click -= new EventHandler(CustomButton_Click);
		}

		void CustomButton_Click(object sender, EventArgs e)
		{
			if (CustomButtonClick != null)
			{
				CustomButtonClick(this, new EventsArgsCustomButton(sender as SimplePopupButton));
			}
		}

		protected bool protShowCustomButton;
		[Description("Determines whether or not a custom button will be displayed on a popup form.")]
		public bool ShowCustomButton
		{
			get { return protShowCustomButton; }
			set { if (value != protShowCustomButton) protShowCustomButton = value; }
		}

		CustomButtonCollection protButtonsForPopup;
		[Localizable(true), RefreshProperties(RefreshProperties.All), Description("Returns a collection of custom buttons in the popup window."),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public CustomButtonCollection ButtonsForPopupWindow
		{
			get { return protButtonsForPopup; }
		}

		// ovverride property
		public override string EditorTypeName
		{
			get { return PopupContainerEditCustomButtonEditorName; }
		}

		public override void Assign(RepositoryItem item)
		{
			base.Assign(item);
			RepositoryItemPopupContainerEditCustomButton currentRepository = (item as RepositoryItemPopupContainerEditCustomButton);
			ShowCustomButton = currentRepository.ShowCustomButton;
			protButtonsForPopup = currentRepository.protButtonsForPopup;
			Events.AddHandler(CustomButtonClick, currentRepository.Events[CustomButtonClick]);
		}

		// let's define delegate and event, fires wgen custom button pressed
		public delegate void CustomButtonEventHandler(object sender, EventsArgsCustomButton e);
		[Description("Fires when a custom button is pressed in the popup window."), Category(CategoryName.Events)]
		public event CustomButtonEventHandler CustomButtonClick;
	}

	public class EventsArgsCustomButton : EventArgs
	{
		protected SimplePopupButton protCustomButton;
		public SimplePopupButton CustomButton { get { return protCustomButton; } }
		public EventsArgsCustomButton(SimplePopupButton currentButton)
		{
			protCustomButton = currentButton;
		}
	}
}
