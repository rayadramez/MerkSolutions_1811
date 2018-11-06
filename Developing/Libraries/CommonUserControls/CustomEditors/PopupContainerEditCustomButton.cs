using System.ComponentModel;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Popup;

namespace CommonUserControls.CustomEditors
{
	public class PopupContainerEditCustomButton : GridLookUpEdit
	{
		static PopupContainerEditCustomButton() { RepositoryItemPopupContainerEditCustomButton.RegisterPopupContainerEditCustomButton(); }

		public PopupContainerEditCustomButton() { }

		public override string EditorTypeName
		{
			get { return RepositoryItemPopupContainerEditCustomButton.PopupContainerEditCustomButtonEditorName; }
		}

		// property as corresponded repositoryitem
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new RepositoryItemPopupContainerEditCustomButton Properties
		{
			get { return base.Properties as RepositoryItemPopupContainerEditCustomButton; }
		}

		protected override PopupBaseForm CreatePopupForm()
		{
			return new PopupContainerFormCustomButton(this);
		}
	}
}
