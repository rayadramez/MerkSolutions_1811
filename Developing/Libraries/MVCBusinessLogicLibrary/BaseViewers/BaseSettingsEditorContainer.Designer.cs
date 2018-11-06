using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MVCBusinessLogicLibrary.BaseViewers
{
	partial class BaseSettingsEditorContainer<TEntity> where TEntity : DBCommon, IDBCommon, new()
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
			this.pnlLeftControls = new DevExpress.XtraEditors.PanelControl();
			this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
			this.btnAddToParent = new DevExpress.XtraEditors.SimpleButton();
			this.btnSave = new DevExpress.XtraEditors.SimpleButton();
			this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.lytSave = new DevExpress.XtraLayout.LayoutControlItem();
			this.lytLeftAddToParent = new DevExpress.XtraLayout.LayoutControlItem();
			this.pnlSubViewerContainer = new DevExpress.XtraEditors.PanelControl();
			this.label2 = new System.Windows.Forms.Label();
			this.pnlMainViewerContainer = new DevExpress.XtraEditors.PanelControl();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.lytLeftEmptySpace = new DevExpress.XtraLayout.EmptySpaceItem();
			this.lytSepertator = new DevExpress.XtraLayout.LayoutControlItem();
			this.lytlSubViewerContainer = new DevExpress.XtraLayout.LayoutControlItem();
			this.lytLeftControls = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlLeftControls)).BeginInit();
			this.pnlLeftControls.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
			this.layoutControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lytSave)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lytLeftAddToParent)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlSubViewerContainer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlMainViewerContainer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lytLeftEmptySpace)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lytSepertator)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lytlSubViewerContainer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lytLeftControls)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.pnlLeftControls);
			this.layoutControl1.Controls.Add(this.pnlSubViewerContainer);
			this.layoutControl1.Controls.Add(this.label2);
			this.layoutControl1.Controls.Add(this.pnlMainViewerContainer);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(1040, 458);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// pnlLeftControls
			// 
			this.pnlLeftControls.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
			this.pnlLeftControls.Controls.Add(this.layoutControl2);
			this.pnlLeftControls.Location = new System.Drawing.Point(0, 0);
			this.pnlLeftControls.LookAndFeel.SkinMaskColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(74)))));
			this.pnlLeftControls.LookAndFeel.SkinName = "Office 2010 Black";
			this.pnlLeftControls.Name = "pnlLeftControls";
			this.pnlLeftControls.Size = new System.Drawing.Size(50, 72);
			this.pnlLeftControls.TabIndex = 26;
			// 
			// layoutControl2
			// 
			this.layoutControl2.Controls.Add(this.btnAddToParent);
			this.layoutControl2.Controls.Add(this.btnSave);
			this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl2.Location = new System.Drawing.Point(2, 2);
			this.layoutControl2.Name = "layoutControl2";
			this.layoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(490, 195, 250, 350);
			this.layoutControl2.OptionsView.RightToLeftMirroringApplied = true;
			this.layoutControl2.Root = this.layoutControlGroup3;
			this.layoutControl2.Size = new System.Drawing.Size(46, 68);
			this.layoutControl2.TabIndex = 0;
			this.layoutControl2.Text = "layoutControl2";
			// 
			// btnAddToParent
			// 
			this.btnAddToParent.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.btnAddToParent.Appearance.Options.UseFont = true;
			this.btnAddToParent.Image = global::MVCBusinessLogicLibrary.Properties.Resources.New_1_16;
			this.btnAddToParent.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			this.btnAddToParent.Location = new System.Drawing.Point(3, 37);
			this.btnAddToParent.MaximumSize = new System.Drawing.Size(40, 28);
			this.btnAddToParent.MinimumSize = new System.Drawing.Size(40, 28);
			this.btnAddToParent.Name = "btnAddToParent";
			this.btnAddToParent.Size = new System.Drawing.Size(40, 28);
			this.btnAddToParent.StyleController = this.layoutControl2;
			this.btnAddToParent.TabIndex = 27;
			this.btnAddToParent.Click += new System.EventHandler(this.btnAddToParent_Click);
			// 
			// btnSave
			// 
			this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.btnSave.Appearance.Options.UseFont = true;
			this.btnSave.Image = global::MVCBusinessLogicLibrary.Properties.Resources.Save_1_16;
			this.btnSave.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			this.btnSave.Location = new System.Drawing.Point(3, 3);
			this.btnSave.MaximumSize = new System.Drawing.Size(40, 28);
			this.btnSave.MinimumSize = new System.Drawing.Size(40, 28);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(40, 28);
			this.btnSave.StyleController = this.layoutControl2;
			this.btnSave.TabIndex = 26;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// layoutControlGroup3
			// 
			this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup3.GroupBordersVisible = false;
			this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lytSave,
            this.lytLeftAddToParent});
			this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup3.Name = "Root";
			this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup3.Size = new System.Drawing.Size(46, 68);
			this.layoutControlGroup3.TextVisible = false;
			// 
			// lytSave
			// 
			this.lytSave.Control = this.btnSave;
			this.lytSave.Location = new System.Drawing.Point(0, 0);
			this.lytSave.Name = "lytSave";
			this.lytSave.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytSave.Size = new System.Drawing.Size(46, 34);
			this.lytSave.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.lytSave.TextSize = new System.Drawing.Size(0, 0);
			this.lytSave.TextVisible = false;
			// 
			// lytLeftAddToParent
			// 
			this.lytLeftAddToParent.Control = this.btnAddToParent;
			this.lytLeftAddToParent.Location = new System.Drawing.Point(0, 34);
			this.lytLeftAddToParent.Name = "lytLeftAddToParent";
			this.lytLeftAddToParent.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytLeftAddToParent.Size = new System.Drawing.Size(46, 34);
			this.lytLeftAddToParent.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.lytLeftAddToParent.TextSize = new System.Drawing.Size(0, 0);
			this.lytLeftAddToParent.TextVisible = false;
			// 
			// pnlSubViewerContainer
			// 
			this.pnlSubViewerContainer.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(74)))));
			this.pnlSubViewerContainer.Appearance.Options.UseBackColor = true;
			this.pnlSubViewerContainer.Location = new System.Drawing.Point(53, 221);
			this.pnlSubViewerContainer.LookAndFeel.SkinMaskColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(59)))), ((int)(((byte)(74)))));
			this.pnlSubViewerContainer.LookAndFeel.SkinName = "Office 2010 Black";
			this.pnlSubViewerContainer.Name = "pnlSubViewerContainer";
			this.pnlSubViewerContainer.Size = new System.Drawing.Size(984, 234);
			this.pnlSubViewerContainer.TabIndex = 27;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(182)))), ((int)(((byte)(42)))));
			this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label2.Location = new System.Drawing.Point(50, 215);
			this.label2.MaximumSize = new System.Drawing.Size(0, 3);
			this.label2.MinimumSize = new System.Drawing.Size(0, 3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(990, 3);
			this.label2.TabIndex = 20;
			// 
			// pnlMainViewerContainer
			// 
			this.pnlMainViewerContainer.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.pnlMainViewerContainer.Appearance.Options.UseBackColor = true;
			this.pnlMainViewerContainer.Location = new System.Drawing.Point(53, 3);
			this.pnlMainViewerContainer.LookAndFeel.SkinMaskColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(74)))));
			this.pnlMainViewerContainer.LookAndFeel.SkinName = "Office 2010 Black";
			this.pnlMainViewerContainer.Name = "pnlMainViewerContainer";
			this.pnlMainViewerContainer.Size = new System.Drawing.Size(984, 209);
			this.pnlMainViewerContainer.TabIndex = 5;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.lytLeftEmptySpace,
            this.lytSepertator,
            this.lytlSubViewerContainer,
            this.lytLeftControls});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(1040, 458);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.pnlMainViewerContainer;
			this.layoutControlItem1.Location = new System.Drawing.Point(50, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem1.Size = new System.Drawing.Size(990, 215);
			this.layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// lytLeftEmptySpace
			// 
			this.lytLeftEmptySpace.AllowHotTrack = false;
			this.lytLeftEmptySpace.Location = new System.Drawing.Point(0, 72);
			this.lytLeftEmptySpace.Name = "lytLeftEmptySpace";
			this.lytLeftEmptySpace.Size = new System.Drawing.Size(50, 386);
			this.lytLeftEmptySpace.TextSize = new System.Drawing.Size(0, 0);
			// 
			// lytSepertator
			// 
			this.lytSepertator.Control = this.label2;
			this.lytSepertator.Location = new System.Drawing.Point(50, 215);
			this.lytSepertator.Name = "lytSepertator";
			this.lytSepertator.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytSepertator.Size = new System.Drawing.Size(990, 3);
			this.lytSepertator.TextSize = new System.Drawing.Size(0, 0);
			this.lytSepertator.TextVisible = false;
			// 
			// lytlSubViewerContainer
			// 
			this.lytlSubViewerContainer.Control = this.pnlSubViewerContainer;
			this.lytlSubViewerContainer.Location = new System.Drawing.Point(50, 218);
			this.lytlSubViewerContainer.Name = "lytlSubViewerContainer";
			this.lytlSubViewerContainer.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytlSubViewerContainer.Size = new System.Drawing.Size(990, 240);
			this.lytlSubViewerContainer.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.lytlSubViewerContainer.TextSize = new System.Drawing.Size(0, 0);
			this.lytlSubViewerContainer.TextVisible = false;
			// 
			// lytLeftControls
			// 
			this.lytLeftControls.Control = this.pnlLeftControls;
			this.lytLeftControls.Location = new System.Drawing.Point(0, 0);
			this.lytLeftControls.MaxSize = new System.Drawing.Size(50, 72);
			this.lytLeftControls.MinSize = new System.Drawing.Size(50, 72);
			this.lytLeftControls.Name = "lytLeftControls";
			this.lytLeftControls.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytLeftControls.Size = new System.Drawing.Size(50, 72);
			this.lytLeftControls.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.lytLeftControls.TextSize = new System.Drawing.Size(0, 0);
			this.lytLeftControls.TextVisible = false;
			// 
			// BaseSettingsEditorContainer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.layoutControl1);
			this.Name = "BaseSettingsEditorContainer";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Size = new System.Drawing.Size(1040, 458);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.BaseSettingsEditorContainer_KeyUp);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlLeftControls)).EndInit();
			this.pnlLeftControls.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
			this.layoutControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lytSave)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lytLeftAddToParent)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlSubViewerContainer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlMainViewerContainer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lytLeftEmptySpace)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lytSepertator)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lytlSubViewerContainer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lytLeftControls)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraEditors.PanelControl pnlMainViewerContainer;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraLayout.EmptySpaceItem lytLeftEmptySpace;
		private System.Windows.Forms.Label label2;
		private DevExpress.XtraLayout.LayoutControlItem lytSepertator;
		private DevExpress.XtraEditors.PanelControl pnlSubViewerContainer;
		private DevExpress.XtraLayout.LayoutControlItem lytlSubViewerContainer;
		private DevExpress.XtraEditors.PanelControl pnlLeftControls;
		private DevExpress.XtraLayout.LayoutControl layoutControl2;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
		private DevExpress.XtraLayout.LayoutControlItem lytLeftControls;
		private DevExpress.XtraEditors.SimpleButton btnSave;
		private DevExpress.XtraLayout.LayoutControlItem lytSave;
		private DevExpress.XtraEditors.SimpleButton btnAddToParent;
		private DevExpress.XtraLayout.LayoutControlItem lytLeftAddToParent;
	}
}
