namespace CommonUserControls.PEMRCommonViewers.PEMR_InternalViewers
{
	partial class PEMR_MedicalRecordContainer
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
			this.btnPrintOrder = new DevExpress.XtraEditors.SimpleButton();
			this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
			this.documentViewer1 = new DevExpress.XtraPrinting.Preview.DocumentViewer();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
			this.simpleSeparator3 = new DevExpress.XtraLayout.SimpleSeparator();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator3)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.btnPrintOrder);
			this.layoutControl1.Controls.Add(this.btnPrint);
			this.layoutControl1.Controls.Add(this.documentViewer1);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(534, 346, 250, 350);
			this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(832, 509);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// btnPrintOrder
			// 
			this.btnPrintOrder.Image = global::CommonUserControls.Properties.Resources.PrintOrder_16_011;
			this.btnPrintOrder.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			this.btnPrintOrder.Location = new System.Drawing.Point(3, 3);
			this.btnPrintOrder.MaximumSize = new System.Drawing.Size(110, 40);
			this.btnPrintOrder.MinimumSize = new System.Drawing.Size(110, 40);
			this.btnPrintOrder.Name = "btnPrintOrder";
			this.btnPrintOrder.Size = new System.Drawing.Size(110, 40);
			this.btnPrintOrder.StyleController = this.layoutControl1;
			this.btnPrintOrder.TabIndex = 7;
			this.btnPrintOrder.Text = "simpleButton3";
			this.btnPrintOrder.Click += new System.EventHandler(this.btnPrintOrder_Click);
			// 
			// btnPrint
			// 
			this.btnPrint.Image = global::CommonUserControls.Properties.Resources.PrintIcon_16_02;
			this.btnPrint.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			this.btnPrint.Location = new System.Drawing.Point(121, 3);
			this.btnPrint.MaximumSize = new System.Drawing.Size(110, 40);
			this.btnPrint.MinimumSize = new System.Drawing.Size(110, 40);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(110, 40);
			this.btnPrint.StyleController = this.layoutControl1;
			this.btnPrint.TabIndex = 5;
			this.btnPrint.Text = "simpleButton1";
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// documentViewer1
			// 
			this.documentViewer1.IsMetric = false;
			this.documentViewer1.Location = new System.Drawing.Point(3, 51);
			this.documentViewer1.Name = "documentViewer1";
			this.documentViewer1.Size = new System.Drawing.Size(826, 455);
			this.documentViewer1.TabIndex = 4;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlItem4,
            this.simpleSeparator1,
            this.simpleSeparator3});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(832, 509);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.documentViewer1;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 48);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem1.Size = new System.Drawing.Size(832, 461);
			this.layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.btnPrint;
			this.layoutControlItem2.Location = new System.Drawing.Point(118, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem2.Size = new System.Drawing.Size(116, 46);
			this.layoutControlItem2.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(234, 0);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(598, 46);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItem4
			// 
			this.layoutControlItem4.Control = this.btnPrintOrder;
			this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem4.Size = new System.Drawing.Size(116, 46);
			this.layoutControlItem4.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem4.TextVisible = false;
			// 
			// simpleSeparator1
			// 
			this.simpleSeparator1.AllowHotTrack = false;
			this.simpleSeparator1.Location = new System.Drawing.Point(0, 46);
			this.simpleSeparator1.Name = "simpleSeparator1";
			this.simpleSeparator1.Size = new System.Drawing.Size(832, 2);
			// 
			// simpleSeparator3
			// 
			this.simpleSeparator3.AllowHotTrack = false;
			this.simpleSeparator3.Location = new System.Drawing.Point(116, 0);
			this.simpleSeparator3.Name = "simpleSeparator3";
			this.simpleSeparator3.Size = new System.Drawing.Size(2, 46);
			// 
			// PEMR_MedicalRecordContainer
			// 
			this.Appearance.BackColor = System.Drawing.Color.White;
			this.Appearance.Options.UseBackColor = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.layoutControl1);
			this.Name = "PEMR_MedicalRecordContainer";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Size = new System.Drawing.Size(832, 509);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator3)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraPrinting.Preview.DocumentViewer documentViewer1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraEditors.SimpleButton btnPrint;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraEditors.SimpleButton btnPrintOrder;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
		private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
		private DevExpress.XtraLayout.SimpleSeparator simpleSeparator3;

	}
}
