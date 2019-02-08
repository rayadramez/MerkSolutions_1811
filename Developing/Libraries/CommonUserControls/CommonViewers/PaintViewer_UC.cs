using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using CommonControlLibrary;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace CommonUserControls.CommonViewers
{
	public enum StrokeWeight
	{
		FistWeight = 1,
		SecondWeight = 2,
		ThirdWeight = 3,
		FourthWeight = 4
	}

	public enum StrokeColor
	{
		Red = 1,
		DarkRed = 2,
		Yellow = 3,
		Orange = 4,
		Blue = 5,
		DarkBlue = 6,
		Green = 7,
		DarkGreen = 8,
		Black = 9,
		White = 10
	}

	public enum PaintMode
	{
		PatientMedicalPictures = 1,
		Other = 2
	}

	public partial class PaintViewer_UC : UserControl
	{
		bool draw = false;

		int pX = -1;
		int pY = -1;

		private Bitmap _drawing;
		private Image ImageToSave { get; set; }
		private Color StrokeColor { get; set; }
		private int StrokeWeight { get; set; }
		private PaintMode PaintMode { get; set; }
		private string SavingFullPath { get; set; }
		private string errorMessage = "";
		private int ImageToSaveWidth = 0;
		private int ImageToSaveHeight = 0;

		public PaintViewer_UC()
		{
			InitializeComponent();
		}

		public void Initialize(PaintMode paintMode, Image imageToView)
		{
			StrokeColor = Color.Black;
			StrokeWeight = 4;
		}

		#region Controls Events

		#region PictureEdit Events

		private void pictureEdit1_Paint(object sender, PaintEventArgs e)
		{
			if (_drawing != null)
				e.Graphics.DrawImageUnscaled(_drawing, new Point(0, 0));
		}

		private void pictureEdit1_MouseMove(object sender, MouseEventArgs e)
		{
			if (draw)
			{
				if (_drawing == null)
					return;
				Graphics graphics = Graphics.FromImage(_drawing);

				Pen pen = new Pen(StrokeColor, StrokeWeight);
				pen.EndCap = LineCap.Round;
				pen.StartCap = LineCap.Round;
				graphics.DrawLine(pen, pX, pY, e.X, e.Y);

				pictureEdit1.CreateGraphics().DrawImageUnscaled(_drawing, new Point(0, 0));
			}

			pX = e.X;
			pY = e.Y;
		}

		private void pictureEdit1_MouseUp(object sender, MouseEventArgs e)
		{
			draw = false;
		}

		private void pictureEdit1_MouseDown(object sender, MouseEventArgs e)
		{
			draw = true;

			pX = e.X;
			pY = e.Y;
		}

		#endregion

		#region CheckButton Events

		private void chkStrokeWeight_1_CheckedChanged(object sender, System.EventArgs e)
		{
			StrokeWeight = 4;
		}

		private void chkStrokeWeight_2_CheckedChanged(object sender, System.EventArgs e)
		{
			StrokeWeight = 6;
		}

		private void chkStrokeWeight_3_CheckedChanged(object sender, System.EventArgs e)
		{
			StrokeWeight = 8;
		}

		private void chkStrokeWeight_4_CheckedChanged(object sender, System.EventArgs e)
		{
			StrokeWeight = 10;
		}

		private void chkStrokeColor_Black_CheckedChanged(object sender, System.EventArgs e)
		{
			StrokeColor = Color.Black;
		}

		private void chkStrokeColor_White_CheckedChanged(object sender, System.EventArgs e)
		{
			StrokeColor = Color.White;
		}

		private void chkStrokeColor_DarkRed_CheckedChanged(object sender, System.EventArgs e)
		{
			StrokeColor = Color.DarkRed;
		}

		private void chkStrokeColor_Red_CheckedChanged(object sender, System.EventArgs e)
		{
			StrokeColor = Color.Red;
		}

		private void chkStrokeColor_Orange_CheckedChanged(object sender, System.EventArgs e)
		{
			StrokeColor = Color.Orange;
		}

		private void chkStrokeColor_Yellow_CheckedChanged(object sender, System.EventArgs e)
		{
			StrokeColor = Color.Yellow;
		}

		private void chkStrokeColor_DarkBlue_CheckedChanged(object sender, System.EventArgs e)
		{
			StrokeColor = Color.DarkBlue;
		}

		private void chkStrokeColor_Blue_CheckedChanged(object sender, System.EventArgs e)
		{
			StrokeColor = Color.Blue;
		}

		private void chkStrokeColor_DarkGreen_CheckedChanged(object sender, System.EventArgs e)
		{
			StrokeColor = Color.DarkGreen;
		}

		private void chkStrokeColor_Green_CheckedChanged(object sender, System.EventArgs e)
		{
			StrokeColor = Color.Green;
		}

		#endregion

		#region Button Events

		private void btnOpen_Click(object sender, System.EventArgs e)
		{
			string[] copiedImagesList =
				FileManager.FileDialoge(PEMRBusinessLogic.ActivePEMRObject.Active_Patient.Person_CU_ID.ToString(),
					FileManager.GetServerDirectoryPath(DB_ServerDirectory.ScanDirectory), false,
					FileSelectionFilter.Images, ref errorMessage,
					PEMRBusinessLogic.ActivePEMRObject.Active_Patient.Person_CU_ID + "_" + DateTime.Now.Date.Day +
					"_" + DateTime.Now.Date.Month + "_" + DateTime.Now.Date.Year);
			if (!string.IsNullOrEmpty(errorMessage))
			{
				XtraMessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
					MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
				errorMessage = string.Empty;
				return;
			}

			if (copiedImagesList == null || copiedImagesList.Length == 0)
			{
				pictureEdit1.Visible = false;
				return;
			}

			SavingFullPath = Path.GetFullPath(copiedImagesList[0]);
			pictureEdit1.Image = FileManager.GetImageFromPath(copiedImagesList[0], ref errorMessage);
			ImageToSave = pictureEdit1.Image;
			if (ImageToSave != null)
			{
				_drawing = new Bitmap(ImageToSave.Width, ImageToSave.Height,
									pictureEdit1.CreateGraphics());
			}

			pictureEdit1.Properties.SizeMode = PictureSizeMode.Zoom;
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			DialogResult result = XtraMessageBox.Show("Do you want to Save ?", "Note", MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);
			switch (result)
			{
				case DialogResult.Yes:
					Graphics graphicsPanel = Graphics.FromImage(ImageToSave);
					graphicsPanel.DrawImage(_drawing, 0, 0);
					ImageToSave.Save(SavingFullPath + "fgdgdfgdfg.jpg");
					break;
			}
		}

		#endregion

		#endregion
	}
}
