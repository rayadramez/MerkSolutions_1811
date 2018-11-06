namespace CommonUserControls.SettingsViewers.DoctorCategoriesViewers
{
	partial class DoctorCategory_SearchViewer
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
			this.txtDescription = new DevExpress.XtraEditors.TextEdit();
			this.txtNameP = new DevExpress.XtraEditors.TextEdit();
			this.txtNameS = new DevExpress.XtraEditors.TextEdit();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNameP.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNameS.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.txtDescription);
			this.layoutControl1.Controls.Add(this.txtNameP);
			this.layoutControl1.Controls.Add(this.txtNameS);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(46, 166, 250, 350);
			this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(771, 60);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// txtDescription
			// 
			this.txtDescription.Location = new System.Drawing.Point(3, 29);
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.txtDescription.Properties.Appearance.Options.UseBackColor = true;
			this.txtDescription.Size = new System.Drawing.Size(673, 20);
			this.txtDescription.StyleController = this.layoutControl1;
			this.txtDescription.TabIndex = 15;
			// 
			// txtNameP
			// 
			this.txtNameP.Location = new System.Drawing.Point(476, 3);
			this.txtNameP.MaximumSize = new System.Drawing.Size(200, 0);
			this.txtNameP.MinimumSize = new System.Drawing.Size(200, 0);
			this.txtNameP.Name = "txtNameP";
			this.txtNameP.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
			this.txtNameP.Properties.Appearance.Options.UseBackColor = true;
			this.txtNameP.Size = new System.Drawing.Size(200, 20);
			this.txtNameP.StyleController = this.layoutControl1;
			this.txtNameP.TabIndex = 14;
			// 
			// txtNameS
			// 
			this.txtNameS.Location = new System.Drawing.Point(178, 3);
			this.txtNameS.MaximumSize = new System.Drawing.Size(200, 0);
			this.txtNameS.MinimumSize = new System.Drawing.Size(200, 0);
			this.txtNameS.Name = "txtNameS";
			this.txtNameS.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
			this.txtNameS.Properties.Appearance.Options.UseBackColor = true;
			this.txtNameS.Size = new System.Drawing.Size(200, 20);
			this.txtNameS.StyleController = this.layoutControl1;
			this.txtNameS.TabIndex = 13;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.CustomizationFormText = "Root";
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlItem3});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(771, 60);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.txtNameS;
			this.layoutControlItem1.CustomizationFormText = "الإســـم الإنجليــزي";
			this.layoutControlItem1.Location = new System.Drawing.Point(175, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem1.Size = new System.Drawing.Size(298, 26);
			this.layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem1.Text = "الإســــم الثـانـــــي";
			this.layoutControlItem1.TextSize = new System.Drawing.Size(89, 13);
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.txtNameP;
			this.layoutControlItem2.CustomizationFormText = "الإســــم العـربـــــي";
			this.layoutControlItem2.Location = new System.Drawing.Point(473, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem2.Size = new System.Drawing.Size(298, 26);
			this.layoutControlItem2.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem2.Text = "الإســــم الأول";
			this.layoutControlItem2.TextSize = new System.Drawing.Size(89, 13);
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(175, 26);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.Control = this.txtDescription;
			this.layoutControlItem3.CustomizationFormText = "مـلاحظـــــات";
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 26);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem3.Size = new System.Drawing.Size(771, 34);
			this.layoutControlItem3.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem3.Text = "مـلاحظـــــات";
			this.layoutControlItem3.TextSize = new System.Drawing.Size(89, 13);
			// 
			// DoctorCategory_SearchViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.layoutControl1);
			this.MinimumSize = new System.Drawing.Size(0, 60);
			this.Name = "DoctorCategory_SearchViewer";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Size = new System.Drawing.Size(771, 60);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNameP.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNameS.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraEditors.TextEdit txtDescription;
		private DevExpress.XtraEditors.TextEdit txtNameP;
		private DevExpress.XtraEditors.TextEdit txtNameS;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
	}
}
