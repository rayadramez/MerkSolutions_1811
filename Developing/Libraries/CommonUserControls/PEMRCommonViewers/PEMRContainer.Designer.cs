namespace CommonUserControls.PEMRCommonViewers
{
	partial class PEMRContainer
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
			this.btnUserDropDown = new DevExpress.XtraEditors.DropDownButton();
			this.btnPreviousVisit = new DevExpress.XtraEditors.SimpleButton();
			this.btnPatientQueue = new DevExpress.XtraEditors.SimpleButton();
			this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.lytContainer = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.lytPreviousVisits = new DevExpress.XtraLayout.LayoutControlItem();
			this.lytPatientQueue = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
			this.splitContainerControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lytContainer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lytPreviousVisits)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lytPatientQueue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.btnUserDropDown);
			this.layoutControl1.Controls.Add(this.btnPreviousVisit);
			this.layoutControl1.Controls.Add(this.btnPatientQueue);
			this.layoutControl1.Controls.Add(this.splitContainerControl1);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(295, 279, 250, 350);
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(1071, 540);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// btnUserDropDown
			// 
			this.btnUserDropDown.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.btnUserDropDown.Appearance.ForeColor = System.Drawing.Color.SteelBlue;
			this.btnUserDropDown.Appearance.Options.UseFont = true;
			this.btnUserDropDown.Appearance.Options.UseForeColor = true;
			this.btnUserDropDown.Image = global::CommonUserControls.Properties.Resources.PersonPic_16_01;
			this.btnUserDropDown.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
			this.btnUserDropDown.Location = new System.Drawing.Point(765, 6);
			this.btnUserDropDown.MaximumSize = new System.Drawing.Size(300, 40);
			this.btnUserDropDown.MinimumSize = new System.Drawing.Size(300, 40);
			this.btnUserDropDown.Name = "btnUserDropDown";
			this.btnUserDropDown.Size = new System.Drawing.Size(300, 40);
			this.btnUserDropDown.StyleController = this.layoutControl1;
			this.btnUserDropDown.TabIndex = 5;
			// 
			// btnPreviousVisit
			// 
			this.btnPreviousVisit.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.btnPreviousVisit.Appearance.ForeColor = System.Drawing.Color.Black;
			this.btnPreviousVisit.Appearance.Options.UseFont = true;
			this.btnPreviousVisit.Appearance.Options.UseForeColor = true;
			this.btnPreviousVisit.Image = global::CommonUserControls.Properties.Resources.Document_16_01;
			this.btnPreviousVisit.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
			this.btnPreviousVisit.Location = new System.Drawing.Point(172, 6);
			this.btnPreviousVisit.MaximumSize = new System.Drawing.Size(160, 40);
			this.btnPreviousVisit.MinimumSize = new System.Drawing.Size(160, 40);
			this.btnPreviousVisit.Name = "btnPreviousVisit";
			this.btnPreviousVisit.Size = new System.Drawing.Size(160, 40);
			this.btnPreviousVisit.StyleController = this.layoutControl1;
			this.btnPreviousVisit.TabIndex = 8;
			this.btnPreviousVisit.Text = "Previous Visits";
			this.btnPreviousVisit.Click += new System.EventHandler(this.btnPreviousVisit_Click);
			// 
			// btnPatientQueue
			// 
			this.btnPatientQueue.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.btnPatientQueue.Appearance.ForeColor = System.Drawing.Color.Black;
			this.btnPatientQueue.Appearance.Options.UseFont = true;
			this.btnPatientQueue.Appearance.Options.UseForeColor = true;
			this.btnPatientQueue.Image = global::CommonUserControls.Properties.Resources.PersonPic_16_01;
			this.btnPatientQueue.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
			this.btnPatientQueue.Location = new System.Drawing.Point(6, 6);
			this.btnPatientQueue.MaximumSize = new System.Drawing.Size(160, 40);
			this.btnPatientQueue.MinimumSize = new System.Drawing.Size(160, 40);
			this.btnPatientQueue.Name = "btnPatientQueue";
			this.btnPatientQueue.Size = new System.Drawing.Size(160, 40);
			this.btnPatientQueue.StyleController = this.layoutControl1;
			this.btnPatientQueue.TabIndex = 7;
			this.btnPatientQueue.Text = "Patient Queue";
			this.btnPatientQueue.Click += new System.EventHandler(this.btnPatientQueue_Click);
			// 
			// splitContainerControl1
			// 
			this.splitContainerControl1.Appearance.BackColor = System.Drawing.Color.White;
			this.splitContainerControl1.Appearance.Options.UseBackColor = true;
			this.splitContainerControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
			this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1;
			this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
			this.splitContainerControl1.Location = new System.Drawing.Point(3, 49);
			this.splitContainerControl1.Name = "splitContainerControl1";
			this.splitContainerControl1.Panel1.Text = "Panel1";
			this.splitContainerControl1.Panel2.Text = "Panel2";
			this.splitContainerControl1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.splitContainerControl1.Size = new System.Drawing.Size(1065, 488);
			this.splitContainerControl1.SplitterPosition = 352;
			this.splitContainerControl1.TabIndex = 4;
			this.splitContainerControl1.Text = "splitContainerControl1";
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lytContainer,
            this.emptySpaceItem1,
            this.lytPreviousVisits,
            this.lytPatientQueue,
            this.layoutControlItem1});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(1071, 540);
			this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// lytContainer
			// 
			this.lytContainer.Control = this.splitContainerControl1;
			this.lytContainer.Location = new System.Drawing.Point(0, 46);
			this.lytContainer.Name = "lytContainer";
			this.lytContainer.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytContainer.Size = new System.Drawing.Size(1065, 488);
			this.lytContainer.TextSize = new System.Drawing.Size(0, 0);
			this.lytContainer.TextVisible = false;
			this.lytContainer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(332, 0);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(427, 46);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// lytPreviousVisits
			// 
			this.lytPreviousVisits.Control = this.btnPreviousVisit;
			this.lytPreviousVisits.Location = new System.Drawing.Point(166, 0);
			this.lytPreviousVisits.Name = "lytPreviousVisits";
			this.lytPreviousVisits.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytPreviousVisits.Size = new System.Drawing.Size(166, 46);
			this.lytPreviousVisits.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.lytPreviousVisits.TextSize = new System.Drawing.Size(0, 0);
			this.lytPreviousVisits.TextVisible = false;
			// 
			// lytPatientQueue
			// 
			this.lytPatientQueue.Control = this.btnPatientQueue;
			this.lytPatientQueue.Location = new System.Drawing.Point(0, 0);
			this.lytPatientQueue.Name = "lytPatientQueue";
			this.lytPatientQueue.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytPatientQueue.Size = new System.Drawing.Size(166, 46);
			this.lytPatientQueue.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.lytPatientQueue.TextSize = new System.Drawing.Size(0, 0);
			this.lytPatientQueue.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.btnUserDropDown;
			this.layoutControlItem1.Location = new System.Drawing.Point(759, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem1.Size = new System.Drawing.Size(306, 46);
			this.layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// PEMRContainer
			// 
			this.Appearance.BackColor = System.Drawing.Color.SlateGray;
			this.Appearance.Options.UseBackColor = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.layoutControl1);
			this.Name = "PEMRContainer";
			this.Size = new System.Drawing.Size(1071, 540);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
			this.splitContainerControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lytContainer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lytPreviousVisits)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lytPatientQueue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
		private DevExpress.XtraLayout.LayoutControlItem lytContainer;
		private DevExpress.XtraEditors.SimpleButton btnPatientQueue;
		private DevExpress.XtraEditors.SimpleButton btnPreviousVisit;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraLayout.LayoutControlItem lytPreviousVisits;
		private DevExpress.XtraLayout.LayoutControlItem lytPatientQueue;
		private DevExpress.XtraEditors.DropDownButton btnUserDropDown;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
	}
}
