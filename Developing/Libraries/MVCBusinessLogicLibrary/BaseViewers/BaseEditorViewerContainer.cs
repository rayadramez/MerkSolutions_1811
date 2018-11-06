using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.BaseViewers
{
	public partial class BaseEditorViewerContainer<TEntity> :
		DevExpress.XtraEditors.XtraUserControl, IViewer
		where TEntity : DBCommon, IDBCommon, new()
	{
		public static MVCController<TEntity> MVCController { get; set; }
		public BaseController<TEntity> BaseControllerObject { get; set; }
		public static Control _editorViewer;

		public BaseEditorViewerContainer()
		{
			InitializeComponent();
			CommonViewsActions.SetupSyle(this);
			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_BaseEditorViewerContainer);
			UserLookAndFeel.Default.SetSkinStyle(ApplicationStaticConfiguration.SkinName);
			if (ApplicationStaticConfiguration.SkinColor != null)
				UserLookAndFeel.Default.SkinMaskColor = Color.FromArgb(((Color)ApplicationStaticConfiguration.SkinColor).R,
					((Color)ApplicationStaticConfiguration.SkinColor).G,
					((Color)ApplicationStaticConfiguration.SkinColor).B);
			BringToFront();
		}

		public BaseEditorViewerContainer(BaseController<TEntity> baseController)
		{
			InitializeComponent();
			CommonViewsActions.SetupSyle(this);
			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_BasicViewerContainer);
			UserLookAndFeel.Default.SetSkinStyle(ApplicationStaticConfiguration.SkinName);
			if (ApplicationStaticConfiguration.SkinColor != null)
				UserLookAndFeel.Default.SkinMaskColor = Color.FromArgb(((Color)ApplicationStaticConfiguration.SkinColor).R,
					((Color)ApplicationStaticConfiguration.SkinColor).G,
					((Color)ApplicationStaticConfiguration.SkinColor).B);
			BaseControllerObject = baseController;
			BringToFront();
		}

		public bool IsBaseControllerInitialized
		{
			get { return BaseControllerObject != null; }
		}

		public void SetHeader(string headerTitle, DB_CommonTransactionType commonTransactionType)
		{
			lblTitle.Text = headerTitle;
			CommonTransactionType = commonTransactionType;

			if (!string.IsNullOrEmpty(ApplicationStaticConfiguration.SkinName) &&
				!string.IsNullOrWhiteSpace(ApplicationStaticConfiguration.SkinName))
				if (ApplicationStaticConfiguration.SkinName.Equals("McSkin"))
					btnClose.Image = Properties.Resources.ExitIcon_8;
				else
					btnClose.Image = Properties.Resources.Exit_1_16;

			switch (ApplicationStaticConfiguration.Application)
			{
				case DB_Application.AdmissionReception:
				case DB_Application.AllReception:
				case DB_Application.ClinicReception:
				case DB_Application.InvoiceManager:
				case DB_Application.QueueManager:
				case DB_Application.Settings:
					CommonViewsActions.LoadXMLFromString(layoutControl1,
						Resources.LocalizedRes.lyt_BasicViewContainerTitle_SaveNew);

					btnSaveAndClose.Text = "حفـظ وإغــلاق";
					btnSaveAndNew.Text = "حفـظ وجديــد";
					btnEdit.Text = "تعـديـــل";
					btnDelete.Text = "حـــذف";
					break;
				case DB_Application.PEMR:
					CommonViewsActions.LoadXMLFromString(layoutControl1,
						Resources.LocalizedRes.lyt_BasicViewContainerTitle_SaveNew_en_US);

					btnSaveAndClose.Text = "Save and Close";
					btnSaveAndNew.Text = "Save and New";
					btnEdit.Text = "Edit";
					btnDelete.Text = "Delete";
					break;
			}

			switch (commonTransactionType)
			{
				case DB_CommonTransactionType.CreateNew:
				case DB_CommonTransactionType.SaveNew:
					lytSaveAndClose.Visibility = LayoutVisibility.Always;
					lytSaveAndNew.Visibility = LayoutVisibility.Always;
					lytDelete.Visibility = LayoutVisibility.Never;
					lytEdit.Visibility = LayoutVisibility.Never;
					break;
				case DB_CommonTransactionType.UpdateExisting:
					lytSaveAndClose.Visibility = LayoutVisibility.Never;
					lytSaveAndNew.Visibility = LayoutVisibility.Never;
					lytDelete.Visibility = LayoutVisibility.Always;
					lytEdit.Visibility = LayoutVisibility.Always;
					break;
			}
		}

		public void InitializeBaseEditorContainer(ViewerName viewerName, string headerTitle)
		{
			CommonViewsActions.ShowUserControl(ref _editorViewer, pnlMain, true);
			BringToFront();
		}

		public void InitializeBaseEditorContainer()
		{
			CommonViewsActions.ShowUserControl(ref _editorViewer, pnlMain);
			if (_editorViewer == null)
				return;

			pnlMain.MaximumSize = new Size(0, _editorViewer.MinimumSize.Height + 3);
			if (!string.IsNullOrEmpty(ApplicationStaticConfiguration.SkinName) &&
				!string.IsNullOrWhiteSpace(ApplicationStaticConfiguration.SkinName))
				if (!ApplicationStaticConfiguration.SkinName.Equals("McSkin"))
					switch (ApplicationStaticConfiguration.Application)
					{
						case DB_Application.Settings:
							UserLookAndFeel.Default.SkinMaskColor = Color.FromArgb(80, 40, 30);
							BackColor = Color.FromArgb(80, 40, 30);
							break;
						case DB_Application.AllReception:
							UserLookAndFeel.Default.SkinMaskColor = Color.FromArgb(50, 59, 74);
							BackColor = Color.FromArgb(50, 59, 74);
							break;
					}
				else
				{
					BackColor = Color.White;
					if (_editorViewer != null)
						_editorViewer.BackColor = Color.WhiteSmoke;
				}
			BringToFront();
		}

		public void InitalizeContainer(Control controlTOAttach, bool showSaveButton, bool showAddToParentButton)
		{
			//CommonViewsActions.ShowUserControl(ref controlTOAttach, pnlMainViewerContainer);
			//if (controlTOAttach != null)
			//	pnlMainViewerContainer.MaximumSize = new Size(0, controlTOAttach.MinimumSize.Height + 3);

			//lytLeftAddToParent.Visibility = showAddToParentButton ? LayoutVisibility.Always : LayoutVisibility.Never;
			//lytSave.Visibility = showSaveButton ? LayoutVisibility.Always : LayoutVisibility.Never;

			//lytLeftControls.Visibility = lytLeftEmptySpace.Visibility = !showSaveButton && !showAddToParentButton
			//	? LayoutVisibility.Never
			//	: LayoutVisibility.Always;
		}

		private void btnSaveAndNew_Click(object sender, System.EventArgs e)
		{
			if (BaseControllerObject.SaveChanges(CommonTransactionType))
			{
				XtraMessageBox.Show("تـــم الحفـــظ بنجـــاح", "تنبيـــه", MessageBoxButtons.OK, MessageBoxIcon.Information,
					MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
				MVCController.BeforeCreatingNew();
				MVCController.CreateNew();
				MVCController.AfterCreateNew();
			}
			else
				XtraMessageBox.Show(BaseControllerObject.MessageToView, "خطــــأ", MessageBoxButtons.OK, MessageBoxIcon.Error,
					MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
		}

		private void btnSaveAndClose_Click(object sender, System.EventArgs e)
		{
			if (BaseControllerObject == null)
				return;

			if (BaseControllerObject.SaveChanges(CommonTransactionType))
			{
				XtraMessageBox.Show("تـــم الحفـــظ بنجـــاح", "تنبيـــه", MessageBoxButtons.OK, MessageBoxIcon.Information,
					MessageBoxDefaultButton.Button1, DefaultBoolean.Default);

				if (BaseControllerObject.Close())
				{
					if (ParentForm != null)
					{
						if (ParentForm.ParentForm != null)
							ParentForm.ParentForm.BringToFront();
						ParentForm.Close();
					}
				}
			}
			else
				XtraMessageBox.Show(BaseControllerObject.MessageToView, "تنبيـــه", MessageBoxButtons.OK, MessageBoxIcon.Error,
					MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
		}

		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			if (BaseControllerObject == null)
				return;

			if (BaseControllerObject.SaveChanges(DB_CommonTransactionType.UpdateExisting))
			{
				if (BaseControllerObject.Close())
				{
					if (ParentForm != null)
					{
						if (ParentForm.ParentForm != null)
							ParentForm.ParentForm.BringToFront();
						ParentForm.Close();
					}
				}
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			if (BaseControllerObject.SaveChanges(DB_CommonTransactionType.DeleteExisting))
			{
				if (BaseControllerObject.Close())
				{
					if (ParentForm != null)
					{
						if (ParentForm.ParentForm != null)
							ParentForm.ParentForm.BringToFront();
						ParentForm.Close();
					}
				}
			}
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			if (ParentForm != null)
			{
				if (ParentForm.ParentForm != null)
					ParentForm.ParentForm.BringToFront();
				ParentForm.Close();
			}
		}

		#region Implementation of IViewer

		public object ID { get; set; }
		public object ViewerID { get; private set; }
		public object UserID
		{
			get
			{
				if (ApplicationStaticConfiguration.ActiveLoginUser != null)
					return ApplicationStaticConfiguration.ActiveLoginUser.Person_CU_ID;
				return null;
			}
		}

		public object EditingDate
		{
			get { return DateTime.Now; }
		}

		public object IsOnDUty { get; set; }
		public DB_CommonTransactionType CommonTransactionType { get; set; }
		public string HeaderTitle { get; private set; }
		public string GridXML { get; private set; }
		public List<IViewer> RelatedViewers { get; set; }
		public void ClearControls()
		{
			throw new System.NotImplementedException();
		}

		public void FillControls()
		{
			throw new System.NotImplementedException();
		}

		#endregion

		private void BaseEditorViewerContainer_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F2)
			{
				btnSaveAndClose_Click(null, null);
				return;
			}
			if (e.KeyCode == Keys.LShiftKey + (int)Keys.F2)
			{
				btnSaveAndNew_Click(null, null);
				return;
			}
		}
	}
}

