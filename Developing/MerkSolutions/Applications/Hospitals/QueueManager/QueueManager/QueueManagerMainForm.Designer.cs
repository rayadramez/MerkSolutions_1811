namespace QueueManager
{
	partial class QueueManagerMainForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.btnUserDropDown = new DevExpress.XtraEditors.DropDownButton();
			this.btnDoctorQueues = new DevExpress.XtraEditors.SimpleButton();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
			this.simpleSeparator2 = new DevExpress.XtraLayout.SimpleSeparator();
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
			this.panelControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator2)).BeginInit();
			this.SuspendLayout();
			// 
			// panelControl1
			// 
			this.panelControl1.Appearance.BackColor = System.Drawing.Color.DimGray;
			this.panelControl1.Appearance.Options.UseBackColor = true;
			this.panelControl1.Controls.Add(this.layoutControl1);
			this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelControl1.Location = new System.Drawing.Point(0, 0);
			this.panelControl1.LookAndFeel.SkinName = "DevExpress Dark Style";
			this.panelControl1.LookAndFeel.UseDefaultLookAndFeel = false;
			this.panelControl1.MaximumSize = new System.Drawing.Size(0, 60);
			this.panelControl1.MinimumSize = new System.Drawing.Size(0, 60);
			this.panelControl1.Name = "panelControl1";
			this.panelControl1.Size = new System.Drawing.Size(1350, 60);
			this.panelControl1.TabIndex = 5;
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.btnUserDropDown);
			this.layoutControl1.Controls.Add(this.btnDoctorQueues);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(2, 2);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(537, 327, 484, 520);
			this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(1346, 56);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// btnUserDropDown
			// 
			this.btnUserDropDown.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.btnUserDropDown.Appearance.ForeColor = System.Drawing.Color.White;
			this.btnUserDropDown.Appearance.Options.UseFont = true;
			this.btnUserDropDown.Appearance.Options.UseForeColor = true;
			this.btnUserDropDown.Location = new System.Drawing.Point(3, 3);
			this.btnUserDropDown.MaximumSize = new System.Drawing.Size(200, 50);
			this.btnUserDropDown.MinimumSize = new System.Drawing.Size(200, 50);
			this.btnUserDropDown.Name = "btnUserDropDown";
			this.btnUserDropDown.Size = new System.Drawing.Size(200, 50);
			this.btnUserDropDown.StyleController = this.layoutControl1;
			this.btnUserDropDown.TabIndex = 9;
			this.btnUserDropDown.Text = "dropDownButton1";
			// 
			// btnDoctorQueues
			// 
			this.btnDoctorQueues.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.btnDoctorQueues.Appearance.ForeColor = System.Drawing.Color.White;
			this.btnDoctorQueues.Appearance.Options.UseFont = true;
			this.btnDoctorQueues.Appearance.Options.UseForeColor = true;
			this.btnDoctorQueues.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
			this.btnDoctorQueues.Location = new System.Drawing.Point(1223, 3);
			this.btnDoctorQueues.MaximumSize = new System.Drawing.Size(120, 50);
			this.btnDoctorQueues.MinimumSize = new System.Drawing.Size(120, 50);
			this.btnDoctorQueues.Name = "btnDoctorQueues";
			this.btnDoctorQueues.Size = new System.Drawing.Size(120, 50);
			this.btnDoctorQueues.StyleController = this.layoutControl1;
			this.btnDoctorQueues.TabIndex = 7;
			this.btnDoctorQueues.Text = "الطبيـــب";
			this.btnDoctorQueues.Click += new System.EventHandler(this.btnDoctorQueues_Click);
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.simpleSeparator1,
            this.simpleSeparator2});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(1346, 56);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(208, 0);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(1010, 56);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.btnUserDropDown;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem1.Size = new System.Drawing.Size(206, 56);
			this.layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.btnDoctorQueues;
			this.layoutControlItem2.Location = new System.Drawing.Point(1220, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem2.Size = new System.Drawing.Size(126, 56);
			this.layoutControlItem2.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// simpleSeparator1
			// 
			this.simpleSeparator1.AllowHotTrack = false;
			this.simpleSeparator1.Location = new System.Drawing.Point(206, 0);
			this.simpleSeparator1.Name = "simpleSeparator1";
			this.simpleSeparator1.Size = new System.Drawing.Size(2, 56);
			// 
			// simpleSeparator2
			// 
			this.simpleSeparator2.AllowHotTrack = false;
			this.simpleSeparator2.Location = new System.Drawing.Point(1218, 0);
			this.simpleSeparator2.Name = "simpleSeparator2";
			this.simpleSeparator2.Size = new System.Drawing.Size(2, 56);
			// 
			// QueueManagerMainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1350, 729);
			this.Controls.Add(this.panelControl1);
			this.Name = "QueueManagerMainForm";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.RightToLeftLayout = true;
			this.Text = "QueueManagerMainForm";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
			this.panelControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator2)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.PanelControl panelControl1;
		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraEditors.SimpleButton btnDoctorQueues;
		private DevExpress.XtraEditors.DropDownButton btnUserDropDown;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
		private DevExpress.XtraLayout.SimpleSeparator simpleSeparator2;
	}
}