namespace CommonUserControls.SettingsViewers.DiagnosisCategoriesViewers
{
	partial class DiagnosisCategories_EditorViewer
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
			this.chkIsDoctorRelated = new DevExpress.XtraEditors.CheckButton();
			this.lkeDoctors = new DevExpress.XtraEditors.GridLookUpEdit();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.txtAbbreviation = new DevExpress.XtraEditors.TextEdit();
			this.txtNameS = new DevExpress.XtraEditors.TextEdit();
			this.txtNameP = new DevExpress.XtraEditors.TextEdit();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.lytDoctors = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lkeDoctors.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtAbbreviation.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNameS.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNameP.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lytDoctors)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.chkIsDoctorRelated);
			this.layoutControl1.Controls.Add(this.lkeDoctors);
			this.layoutControl1.Controls.Add(this.txtAbbreviation);
			this.layoutControl1.Controls.Add(this.txtNameS);
			this.layoutControl1.Controls.Add(this.txtNameP);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(611, 292, 250, 350);
			this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(896, 120);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// chkIsDoctorRelated
			// 
			this.chkIsDoctorRelated.Location = new System.Drawing.Point(693, 55);
			this.chkIsDoctorRelated.MaximumSize = new System.Drawing.Size(200, 30);
			this.chkIsDoctorRelated.MinimumSize = new System.Drawing.Size(200, 30);
			this.chkIsDoctorRelated.Name = "chkIsDoctorRelated";
			this.chkIsDoctorRelated.Size = new System.Drawing.Size(200, 30);
			this.chkIsDoctorRelated.StyleController = this.layoutControl1;
			this.chkIsDoctorRelated.TabIndex = 29;
			this.chkIsDoctorRelated.Text = "تصنيـــف خــاص بطبيــــب";
			this.chkIsDoctorRelated.CheckedChanged += new System.EventHandler(this.chkIsDoctorRelated_CheckedChanged);
			// 
			// lkeDoctors
			// 
			this.lkeDoctors.Location = new System.Drawing.Point(581, 91);
			this.lkeDoctors.MaximumSize = new System.Drawing.Size(250, 0);
			this.lkeDoctors.MinimumSize = new System.Drawing.Size(250, 0);
			this.lkeDoctors.Name = "lkeDoctors";
			this.lkeDoctors.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lkeDoctors.Properties.View = this.gridView1;
			this.lkeDoctors.Size = new System.Drawing.Size(250, 20);
			this.lkeDoctors.StyleController = this.layoutControl1;
			this.lkeDoctors.TabIndex = 28;
			// 
			// gridView1
			// 
			this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			// 
			// txtAbbreviation
			// 
			this.txtAbbreviation.Location = new System.Drawing.Point(631, 29);
			this.txtAbbreviation.MaximumSize = new System.Drawing.Size(200, 0);
			this.txtAbbreviation.MinimumSize = new System.Drawing.Size(200, 0);
			this.txtAbbreviation.Name = "txtAbbreviation";
			this.txtAbbreviation.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.txtAbbreviation.Properties.Appearance.Options.UseBackColor = true;
			this.txtAbbreviation.Size = new System.Drawing.Size(200, 20);
			this.txtAbbreviation.StyleController = this.layoutControl1;
			this.txtAbbreviation.TabIndex = 13;
			// 
			// txtNameS
			// 
			this.txtNameS.Location = new System.Drawing.Point(163, 3);
			this.txtNameS.MaximumSize = new System.Drawing.Size(300, 0);
			this.txtNameS.MinimumSize = new System.Drawing.Size(300, 0);
			this.txtNameS.Name = "txtNameS";
			this.txtNameS.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
			this.txtNameS.Properties.Appearance.Options.UseBackColor = true;
			this.txtNameS.Size = new System.Drawing.Size(300, 20);
			this.txtNameS.StyleController = this.layoutControl1;
			this.txtNameS.TabIndex = 12;
			// 
			// txtNameP
			// 
			this.txtNameP.Location = new System.Drawing.Point(531, 3);
			this.txtNameP.MaximumSize = new System.Drawing.Size(300, 0);
			this.txtNameP.MinimumSize = new System.Drawing.Size(300, 0);
			this.txtNameP.Name = "txtNameP";
			this.txtNameP.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
			this.txtNameP.Properties.Appearance.Options.UseBackColor = true;
			this.txtNameP.Size = new System.Drawing.Size(300, 20);
			this.txtNameP.StyleController = this.layoutControl1;
			this.txtNameP.TabIndex = 12;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.CustomizationFormText = "Root";
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.emptySpaceItem2,
            this.layoutControlItem3,
            this.emptySpaceItem3,
            this.layoutControlItem5,
            this.emptySpaceItem4,
            this.lytDoctors});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(896, 120);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem1.Control = this.txtNameP;
			this.layoutControlItem1.CustomizationFormText = "الإسم الأول";
			this.layoutControlItem1.Location = new System.Drawing.Point(528, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem1.Size = new System.Drawing.Size(368, 26);
			this.layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem1.Text = "الإسم الأول";
			this.layoutControlItem1.TextSize = new System.Drawing.Size(59, 13);
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(160, 26);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem2.Control = this.txtNameS;
			this.layoutControlItem2.CustomizationFormText = "الإسم الثاني";
			this.layoutControlItem2.Location = new System.Drawing.Point(160, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem2.Size = new System.Drawing.Size(368, 26);
			this.layoutControlItem2.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem2.Text = "الإسم الثاني";
			this.layoutControlItem2.TextSize = new System.Drawing.Size(59, 13);
			// 
			// emptySpaceItem2
			// 
			this.emptySpaceItem2.AllowHotTrack = false;
			this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
			this.emptySpaceItem2.Location = new System.Drawing.Point(0, 26);
			this.emptySpaceItem2.Name = "emptySpaceItem2";
			this.emptySpaceItem2.Size = new System.Drawing.Size(628, 26);
			this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem3.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem3.Control = this.txtAbbreviation;
			this.layoutControlItem3.CustomizationFormText = "الإختصـــار";
			this.layoutControlItem3.Location = new System.Drawing.Point(628, 26);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem3.Size = new System.Drawing.Size(268, 26);
			this.layoutControlItem3.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem3.Text = "الإختصـــار";
			this.layoutControlItem3.TextSize = new System.Drawing.Size(59, 13);
			// 
			// emptySpaceItem3
			// 
			this.emptySpaceItem3.AllowHotTrack = false;
			this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
			this.emptySpaceItem3.Location = new System.Drawing.Point(0, 88);
			this.emptySpaceItem3.Name = "emptySpaceItem3";
			this.emptySpaceItem3.Size = new System.Drawing.Size(578, 32);
			this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItem5
			// 
			this.layoutControlItem5.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem5.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem5.Control = this.chkIsDoctorRelated;
			this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
			this.layoutControlItem5.Location = new System.Drawing.Point(690, 52);
			this.layoutControlItem5.Name = "layoutControlItem5";
			this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem5.Size = new System.Drawing.Size(206, 36);
			this.layoutControlItem5.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem5.TextVisible = false;
			// 
			// emptySpaceItem4
			// 
			this.emptySpaceItem4.AllowHotTrack = false;
			this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem4";
			this.emptySpaceItem4.Location = new System.Drawing.Point(0, 52);
			this.emptySpaceItem4.Name = "emptySpaceItem4";
			this.emptySpaceItem4.Size = new System.Drawing.Size(690, 36);
			this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
			// 
			// lytDoctors
			// 
			this.lytDoctors.AppearanceItemCaption.Options.UseTextOptions = true;
			this.lytDoctors.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.lytDoctors.Control = this.lkeDoctors;
			this.lytDoctors.CustomizationFormText = "الطبيــــب";
			this.lytDoctors.Location = new System.Drawing.Point(578, 88);
			this.lytDoctors.Name = "lytDoctors";
			this.lytDoctors.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytDoctors.Size = new System.Drawing.Size(318, 32);
			this.lytDoctors.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.lytDoctors.Text = "الطبيــــب";
			this.lytDoctors.TextSize = new System.Drawing.Size(59, 13);
			this.lytDoctors.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
			// 
			// DiagnosisCategories_EditorViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.layoutControl1);
			this.MinimumSize = new System.Drawing.Size(0, 120);
			this.Name = "DiagnosisCategories_EditorViewer";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Size = new System.Drawing.Size(896, 120);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.lkeDoctors.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtAbbreviation.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNameS.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNameP.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lytDoctors)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraEditors.TextEdit txtNameP;
		private DevExpress.XtraEditors.TextEdit txtNameS;
		private DevExpress.XtraEditors.TextEdit txtAbbreviation;
		private DevExpress.XtraEditors.GridLookUpEdit lkeDoctors;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraEditors.CheckButton chkIsDoctorRelated;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
		private DevExpress.XtraLayout.LayoutControlItem lytDoctors;
	}
}
