using System;
using System.Drawing;
using System.Windows.Forms;

namespace CommonControlLibrary
{
	public partial class PopupFormTitleBar : DevExpress.XtraEditors.XtraUserControl
	{
		private bool _isLarge;
		private readonly Size _parentControlSize;
		private readonly Control _childControl;

		private readonly Point _originalChildLocation;
		private readonly Size _originalChildSize;

		private const int GAP_HEIGHT = 3;

		public delegate void CloseEvent();
		public event CloseEvent CloseClicked;

		public PopupFormTitleBar()
		{
			InitializeComponent();
		}

		public PopupFormTitleBar(Control parentControl, Control childControl)
		{
			InitializeComponent();
			_parentControlSize = parentControl == null ? Screen.FromControl(this).Bounds.Size : parentControl.Size;
			_childControl = childControl;
			_originalChildLocation = childControl.Location;
			_originalChildSize = childControl.Size;
			Location = new Point(_originalChildLocation.X, _originalChildLocation.Y - Size.Height - GAP_HEIGHT);
			Size = new Size(_originalChildSize.Width, Size.Height);
		}

		private void btnEnlarge_Click(object sender, EventArgs e)
		{
			_isLarge = !_isLarge;
			if (_isLarge)
			{
				_childControl.Location = new Point(10, Size.Height + 10 + GAP_HEIGHT);
				_childControl.Size = new Size(_parentControlSize.Width - 20, _parentControlSize.Height - 50);
			}
			else
			{
				_childControl.Location = _originalChildLocation;
				_childControl.Size = _originalChildSize;
			}
			ReDrawTitleBar();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			if (CloseClicked != null) CloseClicked();
		}

		private void ReDrawTitleBar()
		{
			Location = new Point(_childControl.Location.X, _childControl.Location.Y - GAP_HEIGHT - Size.Height);
			Size = new Size(_childControl.Size.Width, Size.Height);
		}
	}
}
