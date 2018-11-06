using System.Drawing;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;

namespace MVCBusinessLogicLibrary.BaseViewers
{
	public partial class PopupBaseForm : DevExpress.XtraEditors.XtraForm
	{
		private Control ParentControl { get; set; }

		public PopupBaseForm()
		{
			InitializeComponent();
		}

		public void InitializePopupBaseForm(FormWindowState formWindowState, bool topMost, string headerTitle,
			FormBorderStyle formBorder = FormBorderStyle.FixedToolWindow)
		{
			WindowState = formWindowState;
			StartPosition = FormStartPosition.CenterScreen;
			TopMost = topMost;
			Text = headerTitle;
			CenterToScreen();
			FormBorderStyle = formBorder;
		}

		public void Initialize(Control controlToAttach, Control parentControl, object colorTouse = null)
		{
			Size = parentControl.ClientSize;
			CenterToScreen();
			int xpoint = Width / 2 - controlToAttach.Width / 2;
			int yPoint = Height / 2 - controlToAttach.Height / 2;
			controlToAttach.Location = new Point(xpoint, yPoint);
			LookAndFeel.SetSkinStyle("McSkin");
			UserLookAndFeel.Default.SetSkinStyle("McSkin");
			if (colorTouse != null)
				BackColor = (Color)colorTouse;
			else
				BackColor = Color.FromArgb(50, 59, 74);
			ParentControl = parentControl;
			BringToFront();
		}

		public static DialogResult ShowAsPopup(Control controlToAttach, Control parentControl,
			FormBorderStyle formBorder = FormBorderStyle.None, object colorTouse = null,
			FormWindowState formWindowState = FormWindowState.Normal, bool topMost = true,
			string headerTitle = "")
		{
			using (PopupBaseForm popupBaseForm = new PopupBaseForm())
			{
				popupBaseForm.LookAndFeel.SetSkinStyle(ApplicationStaticConfiguration.SkinName);
				UserLookAndFeel.Default.SetSkinStyle(ApplicationStaticConfiguration.SkinName);
				UserLookAndFeel.Default.SetSkinStyle(ApplicationStaticConfiguration.SkinName);
				if (ApplicationStaticConfiguration.SkinColor != null)
					popupBaseForm.LookAndFeel.SkinMaskColor = Color.FromArgb(((Color)ApplicationStaticConfiguration.SkinColor).R,
						((Color)ApplicationStaticConfiguration.SkinColor).G,
						((Color)ApplicationStaticConfiguration.SkinColor).B);
				popupBaseForm.Controls.Add(controlToAttach);
				popupBaseForm.FormBorderStyle = formBorder;
				popupBaseForm.Initialize(controlToAttach, parentControl, colorTouse);
				DialogResult dialogResult = popupBaseForm.ShowDialog();
				popupBaseForm.Controls.Remove(controlToAttach);
				controlToAttach.Dispose();
				return dialogResult;
			}
		}

		public static DialogResult ShowAsPopup(ref Control controlToAttach, FormWindowState formWindowState, bool topMost,
			string headerTitle)
		{
			XtraForm form = new PopupBaseForm();

			CommonViewsActions.ShowUserControl(ref controlToAttach, form);
			return form.ShowDialog();
		}

		private void PopupBaseForm_Click(object sender, System.EventArgs e)
		{
			Close();
			if (ParentControl != null && ParentControl.TopLevelControl != null)
				ParentControl.TopLevelControl.BringToFront();
		}

		private void PopupBaseForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.Hide();
			this.Parent = null;
			e.Cancel = true;
		}
	}
}