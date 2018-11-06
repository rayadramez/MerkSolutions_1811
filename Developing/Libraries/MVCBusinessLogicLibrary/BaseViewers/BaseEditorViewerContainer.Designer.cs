using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MVCBusinessLogicLibrary.BaseViewers
{
	partial class BaseEditorViewerContainer<TEntity> where TEntity : DBCommon, IDBCommon, new()
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
			this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
			this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
			this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
			this.btnClose = new DevExpress.XtraEditors.SimpleButton();
			this.btnSaveAndNew = new DevExpress.XtraEditors.SimpleButton();
			this.btnSaveAndClose = new DevExpress.XtraEditors.SimpleButton();
			this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.lytClose = new DevExpress.XtraLayout.LayoutControlItem();
			this.lytSaveAndClose = new DevExpress.XtraLayout.LayoutControlItem();
			this.lytSaveAndNew = new DevExpress.XtraLayout.LayoutControlItem();
			this.lytEdit = new DevExpress.XtraLayout.LayoutControlItem();
			this.lytDelete = new DevExpress.XtraLayout.LayoutControlItem();
			this.pnlMain = new DevExpress.XtraEditors.PanelControl();
			this.label2 = new System.Windows.Forms.Label();
			this.lblTitle = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
			this.panelControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
			this.layoutControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lytClose)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lytSaveAndClose)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lytSaveAndNew)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lytEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lytDelete)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.panelControl1);
			this.layoutControl1.Controls.Add(this.pnlMain);
			this.layoutControl1.Controls.Add(this.label2);
			this.layoutControl1.Controls.Add(this.lblTitle);
			this.layoutControl1.Controls.Add(this.label1);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(186, 251, 250, 350);
			this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(1180, 537);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// panelControl1
			// 
			this.panelControl1.Appearance.BackColor = System.Drawing.Color.DimGray;
			this.panelControl1.Appearance.Options.UseBackColor = true;
			this.panelControl1.Controls.Add(this.layoutControl2);
			this.panelControl1.Location = new System.Drawing.Point(0, 0);
			this.panelControl1.Name = "panelControl1";
			this.panelControl1.Size = new System.Drawing.Size(1180, 38);
			this.panelControl1.TabIndex = 26;
			// 
			// layoutControl2
			// 
			this.layoutControl2.Controls.Add(this.btnDelete);
			this.layoutControl2.Controls.Add(this.btnEdit);
			this.layoutControl2.Controls.Add(this.btnClose);
			this.layoutControl2.Controls.Add(this.btnSaveAndNew);
			this.layoutControl2.Controls.Add(this.btnSaveAndClose);
			this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl2.Location = new System.Drawing.Point(2, 2);
			this.layoutControl2.Name = "layoutControl2";
			this.layoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(190, 243, 326, 425);
			this.layoutControl2.OptionsView.RightToLeftMirroringApplied = true;
			this.layoutControl2.Root = this.layoutControlGroup2;
			this.layoutControl2.Size = new System.Drawing.Size(1176, 34);
			this.layoutControl2.TabIndex = 0;
			this.layoutControl2.Text = "layoutControl2";
			// 
			// btnDelete
			// 
			this.btnDelete.Image = global::MVCBusinessLogicLibrary.Properties.Resources.Delete_1_16;
			this.btnDelete.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
			this.btnDelete.Location = new System.Drawing.Point(455, 3);
			this.btnDelete.MaximumSize = new System.Drawing.Size(150, 28);
			this.btnDelete.MinimumSize = new System.Drawing.Size(150, 28);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(150, 28);
			this.btnDelete.StyleController = this.layoutControl2;
			this.btnDelete.TabIndex = 8;
			this.btnDelete.Text = "حـــــــذف";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnEdit
			// 
			this.btnEdit.Image = global::MVCBusinessLogicLibrary.Properties.Resources.Edit_1_16;
			this.btnEdit.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
			this.btnEdit.Location = new System.Drawing.Point(611, 3);
			this.btnEdit.MaximumSize = new System.Drawing.Size(150, 28);
			this.btnEdit.MinimumSize = new System.Drawing.Size(150, 28);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(150, 28);
			this.btnEdit.StyleController = this.layoutControl2;
			this.btnEdit.TabIndex = 7;
			this.btnEdit.Text = "تعديل";
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// btnClose
			// 
			this.btnClose.Image = global::MVCBusinessLogicLibrary.Properties.Resources.ExitIcon_8;
			this.btnClose.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			this.btnClose.Location = new System.Drawing.Point(3, 3);
			this.btnClose.MaximumSize = new System.Drawing.Size(40, 28);
			this.btnClose.MinimumSize = new System.Drawing.Size(40, 28);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(40, 28);
			this.btnClose.StyleController = this.layoutControl2;
			this.btnClose.TabIndex = 6;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnSaveAndNew
			// 
			this.btnSaveAndNew.Image = global::MVCBusinessLogicLibrary.Properties.Resources.Save_1_16;
			this.btnSaveAndNew.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
			this.btnSaveAndNew.Location = new System.Drawing.Point(767, 3);
			this.btnSaveAndNew.MaximumSize = new System.Drawing.Size(200, 28);
			this.btnSaveAndNew.MinimumSize = new System.Drawing.Size(200, 28);
			this.btnSaveAndNew.Name = "btnSaveAndNew";
			this.btnSaveAndNew.Size = new System.Drawing.Size(200, 28);
			this.btnSaveAndNew.StyleController = this.layoutControl2;
			this.btnSaveAndNew.TabIndex = 5;
			this.btnSaveAndNew.Text = "حفظ وجديد (Shift+F2)";
			this.btnSaveAndNew.Click += new System.EventHandler(this.btnSaveAndNew_Click);
			// 
			// btnSaveAndClose
			// 
			this.btnSaveAndClose.Image = global::MVCBusinessLogicLibrary.Properties.Resources.Save_1_16;
			this.btnSaveAndClose.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
			this.btnSaveAndClose.Location = new System.Drawing.Point(973, 3);
			this.btnSaveAndClose.MaximumSize = new System.Drawing.Size(200, 28);
			this.btnSaveAndClose.MinimumSize = new System.Drawing.Size(200, 28);
			this.btnSaveAndClose.Name = "btnSaveAndClose";
			this.btnSaveAndClose.Size = new System.Drawing.Size(200, 28);
			this.btnSaveAndClose.StyleController = this.layoutControl2;
			this.btnSaveAndClose.TabIndex = 4;
			this.btnSaveAndClose.Text = "حفظ وإغلاق (F2)";
			this.btnSaveAndClose.Click += new System.EventHandler(this.btnSaveAndClose_Click);
			// 
			// layoutControlGroup2
			// 
			this.layoutControlGroup2.CustomizationFormText = "Root";
			this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup2.GroupBordersVisible = false;
			this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.lytClose,
            this.lytSaveAndClose,
            this.lytSaveAndNew,
            this.lytEdit,
            this.lytDelete});
			this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup2.Name = "Root";
			this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup2.Size = new System.Drawing.Size(1176, 34);
			this.layoutControlGroup2.TextVisible = false;
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
			this.emptySpaceItem1.Location = new System.Drawing.Point(46, 0);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(406, 34);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// lytClose
			// 
			this.lytClose.Control = this.btnClose;
			this.lytClose.CustomizationFormText = "lytClose";
			this.lytClose.Location = new System.Drawing.Point(0, 0);
			this.lytClose.Name = "lytClose";
			this.lytClose.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytClose.Size = new System.Drawing.Size(46, 34);
			this.lytClose.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.lytClose.TextSize = new System.Drawing.Size(0, 0);
			this.lytClose.TextVisible = false;
			// 
			// lytSaveAndClose
			// 
			this.lytSaveAndClose.Control = this.btnSaveAndClose;
			this.lytSaveAndClose.CustomizationFormText = "lytSaveAndClose";
			this.lytSaveAndClose.Location = new System.Drawing.Point(970, 0);
			this.lytSaveAndClose.Name = "lytSaveAndClose";
			this.lytSaveAndClose.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytSaveAndClose.Size = new System.Drawing.Size(206, 34);
			this.lytSaveAndClose.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.lytSaveAndClose.TextSize = new System.Drawing.Size(0, 0);
			this.lytSaveAndClose.TextVisible = false;
			this.lytSaveAndClose.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
			// 
			// lytSaveAndNew
			// 
			this.lytSaveAndNew.Control = this.btnSaveAndNew;
			this.lytSaveAndNew.CustomizationFormText = "lytSaveAndNew";
			this.lytSaveAndNew.Location = new System.Drawing.Point(764, 0);
			this.lytSaveAndNew.Name = "lytSaveAndNew";
			this.lytSaveAndNew.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytSaveAndNew.Size = new System.Drawing.Size(206, 34);
			this.lytSaveAndNew.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.lytSaveAndNew.TextSize = new System.Drawing.Size(0, 0);
			this.lytSaveAndNew.TextVisible = false;
			this.lytSaveAndNew.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
			// 
			// lytEdit
			// 
			this.lytEdit.Control = this.btnEdit;
			this.lytEdit.CustomizationFormText = "lytEdit";
			this.lytEdit.Location = new System.Drawing.Point(608, 0);
			this.lytEdit.Name = "lytEdit";
			this.lytEdit.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytEdit.Size = new System.Drawing.Size(156, 34);
			this.lytEdit.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.lytEdit.TextSize = new System.Drawing.Size(0, 0);
			this.lytEdit.TextVisible = false;
			// 
			// lytDelete
			// 
			this.lytDelete.Control = this.btnDelete;
			this.lytDelete.CustomizationFormText = "lytDelete";
			this.lytDelete.Location = new System.Drawing.Point(452, 0);
			this.lytDelete.Name = "lytDelete";
			this.lytDelete.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytDelete.Size = new System.Drawing.Size(156, 34);
			this.lytDelete.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.lytDelete.TextSize = new System.Drawing.Size(0, 0);
			this.lytDelete.TextVisible = false;
			// 
			// pnlMain
			// 
			this.pnlMain.Location = new System.Drawing.Point(3, 77);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(1174, 457);
			this.pnlMain.TabIndex = 25;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(182)))), ((int)(((byte)(42)))));
			this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label2.Location = new System.Drawing.Point(0, 71);
			this.label2.MaximumSize = new System.Drawing.Size(0, 3);
			this.label2.MinimumSize = new System.Drawing.Size(0, 3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(1180, 3);
			this.label2.TabIndex = 24;
			// 
			// lblTitle
			// 
			this.lblTitle.BackColor = System.Drawing.Color.SlateGray;
			this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
			this.lblTitle.ForeColor = System.Drawing.Color.OldLace;
			this.lblTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblTitle.Location = new System.Drawing.Point(0, 41);
			this.lblTitle.MaximumSize = new System.Drawing.Size(0, 30);
			this.lblTitle.MinimumSize = new System.Drawing.Size(0, 30);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(1180, 30);
			this.lblTitle.TabIndex = 23;
			this.lblTitle.Text = "بيانــــات المريـــض";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(182)))), ((int)(((byte)(42)))));
			this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label1.Location = new System.Drawing.Point(0, 38);
			this.label1.MaximumSize = new System.Drawing.Size(0, 3);
			this.label1.MinimumSize = new System.Drawing.Size(0, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(1180, 3);
			this.label1.TabIndex = 22;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.CustomizationFormText = "Root";
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem1});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(1180, 537);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.lblTitle;
			this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 41);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem2.Size = new System.Drawing.Size(1180, 30);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.Control = this.label2;
			this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 71);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem3.Size = new System.Drawing.Size(1180, 3);
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			// 
			// layoutControlItem4
			// 
			this.layoutControlItem4.Control = this.pnlMain;
			this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
			this.layoutControlItem4.Location = new System.Drawing.Point(0, 74);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem4.Size = new System.Drawing.Size(1180, 463);
			this.layoutControlItem4.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem4.TextVisible = false;
			// 
			// layoutControlItem5
			// 
			this.layoutControlItem5.Control = this.panelControl1;
			this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
			this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem5.MaxSize = new System.Drawing.Size(0, 38);
			this.layoutControlItem5.MinSize = new System.Drawing.Size(24, 38);
			this.layoutControlItem5.Name = "layoutControlItem5";
			this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem5.Size = new System.Drawing.Size(1180, 38);
			this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem5.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.label1;
			this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 38);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem1.Size = new System.Drawing.Size(1180, 3);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// BaseEditorViewerContainer
			// 
			this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.Appearance.Options.UseBackColor = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.layoutControl1);
			this.Name = "BaseEditorViewerContainer";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Size = new System.Drawing.Size(1180, 537);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.BaseEditorViewerContainer_KeyUp);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
			this.panelControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
			this.layoutControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lytClose)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lytSaveAndClose)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lytSaveAndNew)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lytEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lytDelete)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.Label label2;
		private DevExpress.XtraEditors.PanelControl pnlMain;
		private DevExpress.XtraEditors.PanelControl panelControl1;
		private DevExpress.XtraLayout.LayoutControl layoutControl2;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
		private DevExpress.XtraEditors.SimpleButton btnSaveAndClose;
		private DevExpress.XtraEditors.SimpleButton btnSaveAndNew;
		private DevExpress.XtraEditors.SimpleButton btnClose;
		private DevExpress.XtraEditors.SimpleButton btnEdit;
		private DevExpress.XtraEditors.SimpleButton btnDelete;
		private DevExpress.XtraLayout.LayoutControlItem lytEdit;
		private DevExpress.XtraLayout.LayoutControlItem lytDelete;
		private DevExpress.XtraLayout.LayoutControlItem lytSaveAndNew;
		private DevExpress.XtraLayout.LayoutControlItem lytSaveAndClose;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraLayout.LayoutControlItem lytClose;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
	}
}
