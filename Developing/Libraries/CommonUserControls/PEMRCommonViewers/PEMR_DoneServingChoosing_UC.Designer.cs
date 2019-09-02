namespace CommonUserControls.PEMRCommonViewers
{
	partial class PEMR_DoneServingChoosing_UC
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
			this.btnExit = new DevExpress.XtraEditors.SimpleButton();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.btnExit);
			this.layoutControl1.Controls.Add(this.labelControl3);
			this.layoutControl1.Controls.Add(this.labelControl1);
			this.layoutControl1.Controls.Add(this.labelControl2);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(565, 286, 250, 350);
			this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(769, 448);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// btnExit
			// 
			this.btnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.btnExit.Appearance.Options.UseFont = true;
			this.btnExit.Image = global::CommonUserControls.Properties.Resources.ExitIcon_8;
			this.btnExit.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			this.btnExit.Location = new System.Drawing.Point(731, 6);
			this.btnExit.MaximumSize = new System.Drawing.Size(35, 35);
			this.btnExit.MinimumSize = new System.Drawing.Size(35, 35);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(35, 35);
			this.btnExit.StyleController = this.layoutControl1;
			this.btnExit.TabIndex = 46;
			this.btnExit.Text = "Add New Diagnosis";
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// labelControl3
			// 
			this.labelControl3.Appearance.BackColor = System.Drawing.Color.Gold;
			this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControl3.Location = new System.Drawing.Point(0, 44);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(769, 3);
			this.labelControl3.StyleController = this.layoutControl1;
			this.labelControl3.TabIndex = 10;
			// 
			// labelControl1
			// 
			this.labelControl1.Appearance.BackColor = System.Drawing.Color.Gold;
			this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControl1.Location = new System.Drawing.Point(0, 0);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(769, 3);
			this.labelControl1.StyleController = this.layoutControl1;
			this.labelControl1.TabIndex = 10;
			// 
			// labelControl2
			// 
			this.labelControl2.Appearance.BackColor = System.Drawing.Color.DimGray;
			this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
			this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Yellow;
			this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControl2.Location = new System.Drawing.Point(0, 3);
			this.labelControl2.MaximumSize = new System.Drawing.Size(0, 40);
			this.labelControl2.MinimumSize = new System.Drawing.Size(0, 40);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Padding = new System.Windows.Forms.Padding(10);
			this.labelControl2.Size = new System.Drawing.Size(728, 40);
			this.labelControl2.StyleController = this.layoutControl1;
			this.labelControl2.TabIndex = 10;
			this.labelControl2.Text = ".... Select ....";
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.layoutControlItem4});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(769, 448);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.labelControl2;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 3);
			this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 40);
			this.layoutControlItem1.MinSize = new System.Drawing.Size(14, 40);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem1.Size = new System.Drawing.Size(728, 41);
			this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.labelControl1;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 3);
			this.layoutControlItem2.MinSize = new System.Drawing.Size(10, 3);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem2.Size = new System.Drawing.Size(769, 3);
			this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.Control = this.labelControl3;
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 44);
			this.layoutControlItem3.MaxSize = new System.Drawing.Size(0, 3);
			this.layoutControlItem3.MinSize = new System.Drawing.Size(10, 3);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem3.Size = new System.Drawing.Size(769, 3);
			this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 47);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(769, 401);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItem4
			// 
			this.layoutControlItem4.Control = this.btnExit;
			this.layoutControlItem4.Location = new System.Drawing.Point(728, 3);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem4.Size = new System.Drawing.Size(41, 41);
			this.layoutControlItem4.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem4.TextVisible = false;
			// 
			// PEMR_DoneServingChoosing_UC
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.layoutControl1);
			this.Name = "PEMR_DoneServingChoosing_UC";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Size = new System.Drawing.Size(769, 448);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraEditors.LabelControl labelControl3;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraEditors.SimpleButton btnExit;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
	}
}
