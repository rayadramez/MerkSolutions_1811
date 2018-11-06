namespace Reception
{
	partial class ReceptionMainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceptionMainForm));
			this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.btnInvoiceManager = new DevExpress.XtraEditors.SimpleButton();
			this.btnReports = new DevExpress.XtraEditors.SimpleButton();
			this.btnUserDropDown = new DevExpress.XtraEditors.DropDownButton();
			this.popupMenu1 = new DevExpress.XtraBars.PopupMenu();
			this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
			this.barManager1 = new DevExpress.XtraBars.BarManager();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.btnPatientsList = new DevExpress.XtraEditors.SimpleButton();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
			this.simpleSeparator2 = new DevExpress.XtraLayout.SimpleSeparator();
			this.simpleSeparator4 = new DevExpress.XtraLayout.SimpleSeparator();
			this.simpleSeparator6 = new DevExpress.XtraLayout.SimpleSeparator();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
			this.panelControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
			this.SuspendLayout();
			// 
			// panelControl1
			// 
			this.panelControl1.Appearance.BackColor = ((System.Drawing.Color)(resources.GetObject("panelControl1.Appearance.BackColor")));
			this.panelControl1.Appearance.Options.UseBackColor = true;
			this.panelControl1.Controls.Add(this.layoutControl1);
			resources.ApplyResources(this.panelControl1, "panelControl1");
			this.panelControl1.LookAndFeel.SkinName = "DevExpress Dark Style";
			this.panelControl1.LookAndFeel.UseDefaultLookAndFeel = false;
			this.panelControl1.Name = "panelControl1";
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.btnInvoiceManager);
			this.layoutControl1.Controls.Add(this.btnReports);
			this.layoutControl1.Controls.Add(this.btnUserDropDown);
			this.layoutControl1.Controls.Add(this.btnPatientsList);
			resources.ApplyResources(this.layoutControl1, "layoutControl1");
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(20, 174, 333, 442);
			this.layoutControl1.OptionsFocus.MoveFocusRightToLeft = true;
			this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
			this.layoutControl1.Root = this.layoutControlGroup1;
			// 
			// btnInvoiceManager
			// 
			this.btnInvoiceManager.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("btnInvoiceManager.Appearance.Font")));
			this.btnInvoiceManager.Appearance.ForeColor = ((System.Drawing.Color)(resources.GetObject("btnInvoiceManager.Appearance.ForeColor")));
			this.btnInvoiceManager.Appearance.Options.UseFont = true;
			this.btnInvoiceManager.Appearance.Options.UseForeColor = true;
			this.btnInvoiceManager.Image = global::Reception.Properties.Resources.InvoiceManagerIcon_16_01;
			this.btnInvoiceManager.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
			resources.ApplyResources(this.btnInvoiceManager, "btnInvoiceManager");
			this.btnInvoiceManager.Name = "btnInvoiceManager";
			this.btnInvoiceManager.StyleController = this.layoutControl1;
			this.btnInvoiceManager.Click += new System.EventHandler(this.btnInvoiceManager_Click);
			// 
			// btnReports
			// 
			this.btnReports.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("btnReports.Appearance.Font")));
			this.btnReports.Appearance.ForeColor = ((System.Drawing.Color)(resources.GetObject("btnReports.Appearance.ForeColor")));
			this.btnReports.Appearance.Options.UseFont = true;
			this.btnReports.Appearance.Options.UseForeColor = true;
			this.btnReports.Image = global::Reception.Properties.Resources.Report_01_16;
			this.btnReports.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
			resources.ApplyResources(this.btnReports, "btnReports");
			this.btnReports.Name = "btnReports";
			this.btnReports.StyleController = this.layoutControl1;
			this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
			// 
			// btnUserDropDown
			// 
			this.btnUserDropDown.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("btnUserDropDown.Appearance.Font")));
			this.btnUserDropDown.Appearance.ForeColor = ((System.Drawing.Color)(resources.GetObject("btnUserDropDown.Appearance.ForeColor")));
			this.btnUserDropDown.Appearance.Options.UseFont = true;
			this.btnUserDropDown.Appearance.Options.UseForeColor = true;
			this.btnUserDropDown.DropDownControl = this.popupMenu1;
			this.btnUserDropDown.Image = global::Reception.Properties.Resources.PersonPic_16_01;
			resources.ApplyResources(this.btnUserDropDown, "btnUserDropDown");
			this.btnUserDropDown.MenuManager = this.barManager1;
			this.btnUserDropDown.Name = "btnUserDropDown";
			this.btnUserDropDown.StyleController = this.layoutControl1;
			// 
			// popupMenu1
			// 
			this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1)});
			this.popupMenu1.Manager = this.barManager1;
			this.popupMenu1.Name = "popupMenu1";
			// 
			// barButtonItem1
			// 
			resources.ApplyResources(this.barButtonItem1, "barButtonItem1");
			this.barButtonItem1.Id = 0;
			this.barButtonItem1.Name = "barButtonItem1";
			this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
			// 
			// barManager1
			// 
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1});
			this.barManager1.MaxItemId = 1;
			// 
			// barDockControlTop
			// 
			this.barDockControlTop.CausesValidation = false;
			resources.ApplyResources(this.barDockControlTop, "barDockControlTop");
			// 
			// barDockControlBottom
			// 
			this.barDockControlBottom.CausesValidation = false;
			resources.ApplyResources(this.barDockControlBottom, "barDockControlBottom");
			// 
			// barDockControlLeft
			// 
			this.barDockControlLeft.CausesValidation = false;
			resources.ApplyResources(this.barDockControlLeft, "barDockControlLeft");
			// 
			// barDockControlRight
			// 
			this.barDockControlRight.CausesValidation = false;
			resources.ApplyResources(this.barDockControlRight, "barDockControlRight");
			// 
			// btnPatientsList
			// 
			this.btnPatientsList.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("btnPatientsList.Appearance.Font")));
			this.btnPatientsList.Appearance.ForeColor = ((System.Drawing.Color)(resources.GetObject("btnPatientsList.Appearance.ForeColor")));
			this.btnPatientsList.Appearance.Options.UseFont = true;
			this.btnPatientsList.Appearance.Options.UseForeColor = true;
			this.btnPatientsList.Image = global::Reception.Properties.Resources.PersonPic_16_01;
			this.btnPatientsList.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
			resources.ApplyResources(this.btnPatientsList, "btnPatientsList");
			this.btnPatientsList.Name = "btnPatientsList";
			this.btnPatientsList.StyleController = this.layoutControl1;
			this.btnPatientsList.Click += new System.EventHandler(this.btnNewPatient_Click);
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.simpleSeparator2,
            this.simpleSeparator4,
            this.simpleSeparator6,
            this.layoutControlItem2,
            this.simpleSeparator1});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(1346, 56);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.btnPatientsList;
			this.layoutControlItem1.Location = new System.Drawing.Point(1220, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem1.Size = new System.Drawing.Size(126, 56);
			this.layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(208, 0);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(674, 56);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItem6
			// 
			this.layoutControlItem6.Control = this.btnUserDropDown;
			this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem6.Name = "layoutControlItem6";
			this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem6.Size = new System.Drawing.Size(206, 56);
			this.layoutControlItem6.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem6.TextVisible = false;
			// 
			// layoutControlItem7
			// 
			this.layoutControlItem7.Control = this.btnReports;
			this.layoutControlItem7.Location = new System.Drawing.Point(884, 0);
			this.layoutControlItem7.Name = "layoutControlItem7";
			this.layoutControlItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem7.Size = new System.Drawing.Size(126, 56);
			this.layoutControlItem7.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem7.TextVisible = false;
			// 
			// simpleSeparator2
			// 
			this.simpleSeparator2.AllowHotTrack = false;
			this.simpleSeparator2.Location = new System.Drawing.Point(206, 0);
			this.simpleSeparator2.Name = "simpleSeparator2";
			this.simpleSeparator2.Size = new System.Drawing.Size(2, 56);
			// 
			// simpleSeparator4
			// 
			this.simpleSeparator4.AllowHotTrack = false;
			this.simpleSeparator4.Location = new System.Drawing.Point(1218, 0);
			this.simpleSeparator4.Name = "simpleSeparator4";
			this.simpleSeparator4.Size = new System.Drawing.Size(2, 56);
			// 
			// simpleSeparator6
			// 
			this.simpleSeparator6.AllowHotTrack = false;
			this.simpleSeparator6.Location = new System.Drawing.Point(882, 0);
			this.simpleSeparator6.Name = "simpleSeparator6";
			this.simpleSeparator6.Size = new System.Drawing.Size(2, 56);
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.btnInvoiceManager;
			this.layoutControlItem2.Location = new System.Drawing.Point(1012, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem2.Size = new System.Drawing.Size(206, 56);
			this.layoutControlItem2.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// simpleSeparator1
			// 
			this.simpleSeparator1.AllowHotTrack = false;
			this.simpleSeparator1.Location = new System.Drawing.Point(1010, 0);
			this.simpleSeparator1.Name = "simpleSeparator1";
			this.simpleSeparator1.Size = new System.Drawing.Size(2, 56);
			// 
			// ReceptionMainForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panelControl1);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.Name = "ReceptionMainForm";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
			this.panelControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.PanelControl panelControl1;
		private DevExpress.XtraEditors.SimpleButton btnPatientsList;
		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraEditors.DropDownButton btnUserDropDown;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
		private DevExpress.XtraEditors.SimpleButton btnReports;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
		private DevExpress.XtraLayout.SimpleSeparator simpleSeparator2;
		private DevExpress.XtraLayout.SimpleSeparator simpleSeparator4;
		private DevExpress.XtraLayout.SimpleSeparator simpleSeparator6;
		private DevExpress.XtraEditors.SimpleButton btnInvoiceManager;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
		private DevExpress.XtraBars.PopupMenu popupMenu1;
		private DevExpress.XtraBars.BarManager barManager1;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.BarButtonItem barButtonItem1;




	}
}