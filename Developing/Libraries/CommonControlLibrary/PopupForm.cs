using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace CommonControlLibrary
{
	public partial class PopupForm : XtraForm
	{
		public bool ShowTitleBar { get; set; }
		protected Control _parentMainFame = null;
		private PopupFormTitleBar _titleBar;

		public PopupForm()
		{
			InitializeComponent();
		}

		public PopupForm(Control parent)
		{
			Initialize(parent, null);
		}

		public void Initialize(Control parent, Control panelControl1)
		{
			_parentMainFame = parent;
			InitializeComponent();
			if (_parentMainFame != null)
				Size = _parentMainFame.ClientSize;

			if (_parentMainFame != null)
				Location = _parentMainFame.PointToScreen(_parentMainFame.ClientRectangle.Location);
			else CenterToScreen();

			int newX = Width / 2 - panelControl1.Width / 2;
			int newY = Height / 2 - panelControl1.Height / 2;
			panelControl1.Location = new Point(newX, newY);

			if (ShowTitleBar)
			{
				Controls.Add(_titleBar = new PopupFormTitleBar(_parentMainFame, panelControl1));
				_titleBar.CloseClicked += Close;
			}
		}

		private void PopupForm_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}