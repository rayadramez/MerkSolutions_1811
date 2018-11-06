namespace MVCBusinessLogicLibrary.BaseViewers
{
	partial class BaseSettingsSearchContatiner<TEntity>
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
			this.grdControlItems = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.lblTopSepertatorLabel = new System.Windows.Forms.Label();
			this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
			this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
			this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
			this.pnlMainViewerContainer = new DevExpress.XtraEditors.PanelControl();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdControlItems)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlMainViewerContainer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.grdControlItems);
			this.layoutControl1.Controls.Add(this.lblTopSepertatorLabel);
			this.layoutControl1.Controls.Add(this.btnDelete);
			this.layoutControl1.Controls.Add(this.btnEdit);
			this.layoutControl1.Controls.Add(this.btnSearch);
			this.layoutControl1.Controls.Add(this.pnlMainViewerContainer);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(376, 313, 250, 350);
			this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(990, 504);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// grdControlItems
			// 
			this.grdControlItems.Location = new System.Drawing.Point(49, 184);
			this.grdControlItems.MainView = this.gridView1;
			this.grdControlItems.Name = "grdControlItems";
			this.grdControlItems.Size = new System.Drawing.Size(938, 317);
			this.grdControlItems.TabIndex = 22;
			this.grdControlItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
			// 
			// gridView1
			// 
			this.gridView1.GridControl = this.grdControlItems;
			this.gridView1.Name = "gridView1";
			this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
			this.gridView1.DataSourceChanged += new System.EventHandler(this.gridView1_DataSourceChanged);
			// 
			// lblTopSepertatorLabel
			// 
			this.lblTopSepertatorLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(182)))), ((int)(((byte)(42)))));
			this.lblTopSepertatorLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblTopSepertatorLabel.Location = new System.Drawing.Point(0, 178);
			this.lblTopSepertatorLabel.MaximumSize = new System.Drawing.Size(0, 3);
			this.lblTopSepertatorLabel.MinimumSize = new System.Drawing.Size(0, 3);
			this.lblTopSepertatorLabel.Name = "lblTopSepertatorLabel";
			this.lblTopSepertatorLabel.Size = new System.Drawing.Size(990, 3);
			this.lblTopSepertatorLabel.TabIndex = 20;
			// 
			// btnDelete
			// 
			this.btnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.btnDelete.Appearance.Options.UseFont = true;
			this.btnDelete.Image = global::MVCBusinessLogicLibrary.Properties.Resources.Delete_1_16;
			this.btnDelete.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			this.btnDelete.Location = new System.Drawing.Point(3, 218);
			this.btnDelete.MaximumSize = new System.Drawing.Size(40, 28);
			this.btnDelete.MinimumSize = new System.Drawing.Size(40, 28);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(40, 28);
			this.btnDelete.StyleController = this.layoutControl1;
			this.btnDelete.TabIndex = 5;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnEdit
			// 
			this.btnEdit.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.btnEdit.Appearance.Options.UseFont = true;
			this.btnEdit.Image = global::MVCBusinessLogicLibrary.Properties.Resources.Edit_1_16;
			this.btnEdit.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			this.btnEdit.Location = new System.Drawing.Point(3, 184);
			this.btnEdit.MaximumSize = new System.Drawing.Size(40, 28);
			this.btnEdit.MinimumSize = new System.Drawing.Size(40, 28);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(40, 28);
			this.btnEdit.StyleController = this.layoutControl1;
			this.btnEdit.TabIndex = 3;
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// btnSearch
			// 
			this.btnSearch.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.btnSearch.Appearance.Options.UseFont = true;
			this.btnSearch.Image = global::MVCBusinessLogicLibrary.Properties.Resources.Search_1_16;
			this.btnSearch.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			this.btnSearch.Location = new System.Drawing.Point(4, 22);
			this.btnSearch.MaximumSize = new System.Drawing.Size(40, 28);
			this.btnSearch.MinimumSize = new System.Drawing.Size(40, 28);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(40, 28);
			this.btnSearch.StyleController = this.layoutControl1;
			this.btnSearch.TabIndex = 2;
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// pnlMainViewerContainer
			// 
			this.pnlMainViewerContainer.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.pnlMainViewerContainer.Appearance.Options.UseBackColor = true;
			this.pnlMainViewerContainer.Location = new System.Drawing.Point(50, 22);
			this.pnlMainViewerContainer.LookAndFeel.SkinMaskColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(74)))));
			this.pnlMainViewerContainer.LookAndFeel.SkinName = "Office 2010 Black";
			this.pnlMainViewerContainer.Name = "pnlMainViewerContainer";
			this.pnlMainViewerContainer.Size = new System.Drawing.Size(936, 152);
			this.pnlMainViewerContainer.TabIndex = 1;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlGroup2,
            this.layoutControlItem7});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(990, 504);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 249);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(46, 255);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItem4
			// 
			this.layoutControlItem4.Control = this.btnEdit;
			this.layoutControlItem4.Location = new System.Drawing.Point(0, 181);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem4.Size = new System.Drawing.Size(46, 34);
			this.layoutControlItem4.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem4.TextVisible = false;
			// 
			// layoutControlItem5
			// 
			this.layoutControlItem5.Control = this.btnDelete;
			this.layoutControlItem5.Location = new System.Drawing.Point(0, 215);
			this.layoutControlItem5.Name = "layoutControlItem5";
			this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem5.Size = new System.Drawing.Size(46, 34);
			this.layoutControlItem5.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem5.TextVisible = false;
			// 
			// layoutControlItem6
			// 
			this.layoutControlItem6.Control = this.lblTopSepertatorLabel;
			this.layoutControlItem6.Location = new System.Drawing.Point(0, 178);
			this.layoutControlItem6.Name = "layoutControlItem6";
			this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem6.Size = new System.Drawing.Size(990, 3);
			this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem6.TextVisible = false;
			// 
			// layoutControlGroup2
			// 
			this.layoutControlGroup2.ExpandButtonMode = DevExpress.Utils.Controls.ExpandButtonMode.Inverted;
			this.layoutControlGroup2.ExpandButtonVisible = true;
			this.layoutControlGroup2.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
			this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.layoutControlItem2,
            this.layoutControlItem1});
			this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup2.Name = "layoutControlGroup2";
			this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup2.Size = new System.Drawing.Size(990, 178);
			this.layoutControlGroup2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup2.Text = "مفردات البحث";
			// 
			// emptySpaceItem2
			// 
			this.emptySpaceItem2.AllowHotTrack = false;
			this.emptySpaceItem2.Location = new System.Drawing.Point(0, 34);
			this.emptySpaceItem2.Name = "emptySpaceItem2";
			this.emptySpaceItem2.Size = new System.Drawing.Size(46, 124);
			this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.pnlMainViewerContainer;
			this.layoutControlItem2.Location = new System.Drawing.Point(46, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem2.Size = new System.Drawing.Size(942, 158);
			this.layoutControlItem2.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.btnSearch;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem1.Size = new System.Drawing.Size(46, 34);
			this.layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// layoutControlItem7
			// 
			this.layoutControlItem7.Control = this.grdControlItems;
			this.layoutControlItem7.Location = new System.Drawing.Point(46, 181);
			this.layoutControlItem7.Name = "layoutControlItem7";
			this.layoutControlItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem7.Size = new System.Drawing.Size(944, 323);
			this.layoutControlItem7.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem7.TextVisible = false;
			// 
			// BaseSettingsSearchContatiner
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.layoutControl1);
			this.Name = "BaseSettingsSearchContatiner";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Size = new System.Drawing.Size(990, 504);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdControlItems)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlMainViewerContainer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraEditors.SimpleButton btnSearch;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraEditors.PanelControl pnlMainViewerContainer;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
		private DevExpress.XtraEditors.SimpleButton btnEdit;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
		private DevExpress.XtraEditors.SimpleButton btnDelete;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
		private System.Windows.Forms.Label lblTopSepertatorLabel;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
		private DevExpress.XtraGrid.GridControl grdControlItems;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
	}
}
