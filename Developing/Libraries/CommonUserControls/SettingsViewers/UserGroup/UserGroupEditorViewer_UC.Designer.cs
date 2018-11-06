namespace CommonUserControls.SettingsViewers.UserGroup
{
	partial class UserGroupEditorViewer_UC
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
			this.txtInternalCode = new DevExpress.XtraEditors.TextEdit();
			this.txtFirstNameS = new DevExpress.XtraEditors.TextEdit();
			this.txtFirstNameP = new DevExpress.XtraEditors.TextEdit();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtInternalCode.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtFirstNameS.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtFirstNameP.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.txtDescription);
			this.layoutControl1.Controls.Add(this.txtInternalCode);
			this.layoutControl1.Controls.Add(this.txtFirstNameS);
			this.layoutControl1.Controls.Add(this.txtFirstNameP);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(90, 359, 250, 350);
			this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(625, 85);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// txtDescription
			// 
			this.txtDescription.Location = new System.Drawing.Point(3, 55);
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.txtDescription.Properties.Appearance.Options.UseBackColor = true;
			this.txtDescription.Size = new System.Drawing.Size(530, 20);
			this.txtDescription.StyleController = this.layoutControl1;
			this.txtDescription.TabIndex = 6;
			// 
			// txtInternalCode
			// 
			this.txtInternalCode.EditValue = "";
			this.txtInternalCode.Location = new System.Drawing.Point(383, 29);
			this.txtInternalCode.MaximumSize = new System.Drawing.Size(150, 0);
			this.txtInternalCode.MinimumSize = new System.Drawing.Size(150, 0);
			this.txtInternalCode.Name = "txtInternalCode";
			this.txtInternalCode.Properties.Appearance.BackColor = System.Drawing.Color.AntiqueWhite;
			this.txtInternalCode.Properties.Appearance.Options.UseBackColor = true;
			this.txtInternalCode.Size = new System.Drawing.Size(150, 20);
			this.txtInternalCode.StyleController = this.layoutControl1;
			this.txtInternalCode.TabIndex = 26;
			// 
			// txtFirstNameS
			// 
			this.txtFirstNameS.Location = new System.Drawing.Point(38, 3);
			this.txtFirstNameS.MaximumSize = new System.Drawing.Size(200, 0);
			this.txtFirstNameS.MinimumSize = new System.Drawing.Size(200, 0);
			this.txtFirstNameS.Name = "txtFirstNameS";
			this.txtFirstNameS.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
			this.txtFirstNameS.Properties.Appearance.Options.UseBackColor = true;
			this.txtFirstNameS.Size = new System.Drawing.Size(200, 20);
			this.txtFirstNameS.StyleController = this.layoutControl1;
			this.txtFirstNameS.TabIndex = 6;
			// 
			// txtFirstNameP
			// 
			this.txtFirstNameP.Location = new System.Drawing.Point(333, 3);
			this.txtFirstNameP.MaximumSize = new System.Drawing.Size(200, 0);
			this.txtFirstNameP.MinimumSize = new System.Drawing.Size(200, 0);
			this.txtFirstNameP.Name = "txtFirstNameP";
			this.txtFirstNameP.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
			this.txtFirstNameP.Properties.Appearance.Options.UseBackColor = true;
			this.txtFirstNameP.Size = new System.Drawing.Size(200, 20);
			this.txtFirstNameP.StyleController = this.layoutControl1;
			this.txtFirstNameP.TabIndex = 5;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlItem3,
            this.emptySpaceItem2,
            this.layoutControlItem4});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(625, 85);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.txtFirstNameP;
			this.layoutControlItem1.Location = new System.Drawing.Point(330, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem1.Size = new System.Drawing.Size(295, 26);
			this.layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem1.Text = "الإســــم الأول";
			this.layoutControlItem1.TextSize = new System.Drawing.Size(86, 13);
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.txtFirstNameS;
			this.layoutControlItem2.Location = new System.Drawing.Point(35, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem2.Size = new System.Drawing.Size(295, 26);
			this.layoutControlItem2.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem2.Text = " الإســــم الثانــــي";
			this.layoutControlItem2.TextSize = new System.Drawing.Size(86, 13);
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(35, 26);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.Control = this.txtInternalCode;
			this.layoutControlItem3.Location = new System.Drawing.Point(380, 26);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem3.Size = new System.Drawing.Size(245, 26);
			this.layoutControlItem3.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem3.Text = "الكود الداخلي";
			this.layoutControlItem3.TextSize = new System.Drawing.Size(86, 13);
			// 
			// emptySpaceItem2
			// 
			this.emptySpaceItem2.AllowHotTrack = false;
			this.emptySpaceItem2.Location = new System.Drawing.Point(0, 26);
			this.emptySpaceItem2.Name = "emptySpaceItem2";
			this.emptySpaceItem2.Size = new System.Drawing.Size(380, 26);
			this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItem4
			// 
			this.layoutControlItem4.Control = this.txtDescription;
			this.layoutControlItem4.Location = new System.Drawing.Point(0, 52);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem4.Size = new System.Drawing.Size(625, 33);
			this.layoutControlItem4.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem4.Text = "ملاحظات";
			this.layoutControlItem4.TextSize = new System.Drawing.Size(86, 13);
			// 
			// UserGroupEditorViewer_UC
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.layoutControl1);
			this.MinimumSize = new System.Drawing.Size(0, 85);
			this.Name = "UserGroupEditorViewer_UC";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Size = new System.Drawing.Size(625, 85);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtInternalCode.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtFirstNameS.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtFirstNameP.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraEditors.TextEdit txtFirstNameS;
		private DevExpress.XtraEditors.TextEdit txtFirstNameP;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraEditors.TextEdit txtInternalCode;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
		private DevExpress.XtraEditors.TextEdit txtDescription;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
	}
}
