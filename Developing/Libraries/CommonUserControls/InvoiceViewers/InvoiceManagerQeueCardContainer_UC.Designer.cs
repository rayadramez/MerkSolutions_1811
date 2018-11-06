namespace CommonUserControls.InvoiceViewers
{
	partial class InvoiceManagerQeueCardContainer_UC
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
			this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
			this.lblInvoicesCount = new DevExpress.XtraEditors.LabelControl();
			this.label2 = new System.Windows.Forms.Label();
			this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
			this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
			this.tabOutPatientWithoutInsurance = new DevExpress.XtraTab.XtraTabPage();
			this.tabOutPatientWithInsurance = new DevExpress.XtraTab.XtraTabPage();
			this.tabInPatientWithoutInsurance = new DevExpress.XtraTab.XtraTabPage();
			this.tabInPatientWithInsurance = new DevExpress.XtraTab.XtraTabPage();
			this.txtPatientName = new DevExpress.XtraEditors.TextEdit();
			this.txtPatientID = new DevExpress.XtraEditors.TextEdit();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
			this.xtraTabControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtPatientName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtPatientID.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.btnRefresh);
			this.layoutControl1.Controls.Add(this.lblInvoicesCount);
			this.layoutControl1.Controls.Add(this.label2);
			this.layoutControl1.Controls.Add(this.btnSearch);
			this.layoutControl1.Controls.Add(this.xtraTabControl1);
			this.layoutControl1.Controls.Add(this.txtPatientName);
			this.layoutControl1.Controls.Add(this.txtPatientID);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(668, 153, 253, 451);
			this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(590, 514);
			this.layoutControl1.TabIndex = 1;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// btnRefresh
			// 
			this.btnRefresh.Image = global::CommonUserControls.Properties.Resources.Refresh_16_01;
			this.btnRefresh.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			this.btnRefresh.Location = new System.Drawing.Point(5, 36);
			this.btnRefresh.MaximumSize = new System.Drawing.Size(40, 40);
			this.btnRefresh.MinimumSize = new System.Drawing.Size(40, 40);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(40, 40);
			this.btnRefresh.StyleController = this.layoutControl1;
			this.btnRefresh.TabIndex = 8;
			this.btnRefresh.Text = "simpleButton1";
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// lblInvoicesCount
			// 
			this.lblInvoicesCount.Appearance.BackColor = System.Drawing.Color.Gray;
			this.lblInvoicesCount.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
			this.lblInvoicesCount.Appearance.ForeColor = System.Drawing.Color.White;
			this.lblInvoicesCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.lblInvoicesCount.Location = new System.Drawing.Point(56, 57);
			this.lblInvoicesCount.MaximumSize = new System.Drawing.Size(0, 20);
			this.lblInvoicesCount.MinimumSize = new System.Drawing.Size(0, 20);
			this.lblInvoicesCount.Name = "lblInvoicesCount";
			this.lblInvoicesCount.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
			this.lblInvoicesCount.Size = new System.Drawing.Size(528, 20);
			this.lblInvoicesCount.StyleController = this.layoutControl1;
			this.lblInvoicesCount.TabIndex = 27;
			this.lblInvoicesCount.Text = "0";
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(182)))), ((int)(((byte)(42)))));
			this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label2.Location = new System.Drawing.Point(3, 31);
			this.label2.MaximumSize = new System.Drawing.Size(0, 3);
			this.label2.MinimumSize = new System.Drawing.Size(0, 3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(584, 3);
			this.label2.TabIndex = 25;
			// 
			// btnSearch
			// 
			this.btnSearch.Image = global::CommonUserControls.Properties.Resources.Search_1_16;
			this.btnSearch.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			this.btnSearch.Location = new System.Drawing.Point(6, 6);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(42, 22);
			this.btnSearch.StyleController = this.layoutControl1;
			this.btnSearch.TabIndex = 7;
			this.btnSearch.Text = "simpleButton1";
			// 
			// xtraTabControl1
			// 
			this.xtraTabControl1.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
			this.xtraTabControl1.AppearancePage.HeaderActive.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.xtraTabControl1.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControl1.AppearancePage.HeaderActive.Options.UseForeColor = true;
			this.xtraTabControl1.HeaderAutoFill = DevExpress.Utils.DefaultBoolean.True;
			this.xtraTabControl1.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Right;
			this.xtraTabControl1.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
			this.xtraTabControl1.Location = new System.Drawing.Point(6, 83);
			this.xtraTabControl1.Name = "xtraTabControl1";
			this.xtraTabControl1.SelectedTabPage = this.tabOutPatientWithoutInsurance;
			this.xtraTabControl1.Size = new System.Drawing.Size(578, 425);
			this.xtraTabControl1.TabIndex = 6;
			this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabOutPatientWithoutInsurance,
            this.tabOutPatientWithInsurance,
            this.tabInPatientWithoutInsurance,
            this.tabInPatientWithInsurance});
			// 
			// tabOutPatientWithoutInsurance
			// 
			this.tabOutPatientWithoutInsurance.AutoScroll = true;
			this.tabOutPatientWithoutInsurance.Name = "tabOutPatientWithoutInsurance";
			this.tabOutPatientWithoutInsurance.Padding = new System.Windows.Forms.Padding(5);
			this.tabOutPatientWithoutInsurance.Size = new System.Drawing.Size(454, 419);
			this.tabOutPatientWithoutInsurance.Text = "خارجي - بدون جهة";
			// 
			// tabOutPatientWithInsurance
			// 
			this.tabOutPatientWithInsurance.AutoScroll = true;
			this.tabOutPatientWithInsurance.Name = "tabOutPatientWithInsurance";
			this.tabOutPatientWithInsurance.Padding = new System.Windows.Forms.Padding(5);
			this.tabOutPatientWithInsurance.Size = new System.Drawing.Size(454, 419);
			this.tabOutPatientWithInsurance.Text = "خارجي - جهة";
			// 
			// tabInPatientWithoutInsurance
			// 
			this.tabInPatientWithoutInsurance.Appearance.HeaderActive.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
			this.tabInPatientWithoutInsurance.Appearance.HeaderActive.Options.UseFont = true;
			this.tabInPatientWithoutInsurance.AutoScroll = true;
			this.tabInPatientWithoutInsurance.Name = "tabInPatientWithoutInsurance";
			this.tabInPatientWithoutInsurance.Padding = new System.Windows.Forms.Padding(5);
			this.tabInPatientWithoutInsurance.Size = new System.Drawing.Size(454, 419);
			this.tabInPatientWithoutInsurance.Text = "داخلي - بدون جهة";
			// 
			// tabInPatientWithInsurance
			// 
			this.tabInPatientWithInsurance.AutoScroll = true;
			this.tabInPatientWithInsurance.Name = "tabInPatientWithInsurance";
			this.tabInPatientWithInsurance.Padding = new System.Windows.Forms.Padding(5);
			this.tabInPatientWithInsurance.Size = new System.Drawing.Size(454, 419);
			this.tabInPatientWithInsurance.Text = "داخلي - جهة";
			// 
			// txtPatientName
			// 
			this.txtPatientName.Location = new System.Drawing.Point(54, 6);
			this.txtPatientName.Name = "txtPatientName";
			this.txtPatientName.Size = new System.Drawing.Size(292, 20);
			this.txtPatientName.StyleController = this.layoutControl1;
			this.txtPatientName.TabIndex = 5;
			// 
			// txtPatientID
			// 
			this.txtPatientID.Location = new System.Drawing.Point(407, 6);
			this.txtPatientID.Name = "txtPatientID";
			this.txtPatientID.Size = new System.Drawing.Size(122, 20);
			this.txtPatientID.StyleController = this.layoutControl1;
			this.txtPatientID.TabIndex = 4;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem7,
            this.layoutControlItem6,
            this.emptySpaceItem1});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(590, 514);
			this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.txtPatientID;
			this.layoutControlItem1.Location = new System.Drawing.Point(401, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem1.Size = new System.Drawing.Size(183, 28);
			this.layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem1.Text = "كود المريض";
			this.layoutControlItem1.TextSize = new System.Drawing.Size(52, 13);
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.txtPatientName;
			this.layoutControlItem2.Location = new System.Drawing.Point(48, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem2.Size = new System.Drawing.Size(353, 28);
			this.layoutControlItem2.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem2.Text = "الإسم";
			this.layoutControlItem2.TextSize = new System.Drawing.Size(52, 13);
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.Control = this.xtraTabControl1;
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 77);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem3.Size = new System.Drawing.Size(584, 431);
			this.layoutControlItem3.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			// 
			// layoutControlItem4
			// 
			this.layoutControlItem4.Control = this.btnSearch;
			this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem4.Size = new System.Drawing.Size(48, 28);
			this.layoutControlItem4.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem4.TextVisible = false;
			// 
			// layoutControlItem5
			// 
			this.layoutControlItem5.Control = this.label2;
			this.layoutControlItem5.Location = new System.Drawing.Point(0, 28);
			this.layoutControlItem5.Name = "layoutControlItem5";
			this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem5.Size = new System.Drawing.Size(584, 3);
			this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem5.TextVisible = false;
			// 
			// layoutControlItem7
			// 
			this.layoutControlItem7.Control = this.lblInvoicesCount;
			this.layoutControlItem7.Location = new System.Drawing.Point(50, 51);
			this.layoutControlItem7.Name = "layoutControlItem7";
			this.layoutControlItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem7.Size = new System.Drawing.Size(534, 26);
			this.layoutControlItem7.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem7.TextVisible = false;
			// 
			// layoutControlItem6
			// 
			this.layoutControlItem6.Control = this.btnRefresh;
			this.layoutControlItem6.Location = new System.Drawing.Point(0, 31);
			this.layoutControlItem6.Name = "layoutControlItem6";
			this.layoutControlItem6.Size = new System.Drawing.Size(50, 46);
			this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem6.TextVisible = false;
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(50, 31);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(534, 20);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// InvoiceManagerQeueCardContainer_UC
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.layoutControl1);
			this.Name = "InvoiceManagerQeueCardContainer_UC";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Size = new System.Drawing.Size(590, 514);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
			this.xtraTabControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.txtPatientName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtPatientID.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private System.Windows.Forms.Label label2;
		private DevExpress.XtraEditors.SimpleButton btnSearch;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
		private DevExpress.XtraTab.XtraTabPage tabInPatientWithoutInsurance;
		private DevExpress.XtraTab.XtraTabPage tabInPatientWithInsurance;
		private DevExpress.XtraTab.XtraTabPage tabOutPatientWithoutInsurance;
		private DevExpress.XtraTab.XtraTabPage tabOutPatientWithInsurance;
		private DevExpress.XtraEditors.TextEdit txtPatientName;
		private DevExpress.XtraEditors.TextEdit txtPatientID;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
		private DevExpress.XtraEditors.LabelControl lblInvoicesCount;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
		private DevExpress.XtraEditors.SimpleButton btnRefresh;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
	}
}
