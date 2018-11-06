using CommonUserControls.CommonViewers;

namespace CommonUserControls.InvoiceViewers
{
	partial class InvoicePayment_UC
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
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoicePayment_UC));
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.lkeService = new DevExpress.XtraEditors.GridLookUpEdit();
			this.gridView7 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.lkeServiceCategory = new DevExpress.XtraEditors.GridLookUpEdit();
			this.gridView6 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.chkLabServiceType = new DevExpress.XtraEditors.CheckButton();
			this.chkInvestigationServicetype = new DevExpress.XtraEditors.CheckButton();
			this.chkExaminationServiceType = new DevExpress.XtraEditors.CheckButton();
			this.chkPatientDeposit = new DevExpress.XtraEditors.CheckButton();
			this.spnReaminder = new DevExpress.XtraEditors.SpinEdit();
			this.spnTotalPayments = new DevExpress.XtraEditors.SpinEdit();
			this.spnTotalRequired = new DevExpress.XtraEditors.SpinEdit();
			this.grdPayments = new DevExpress.XtraGrid.GridControl();
			this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.chkIsPaymentEnoght = new DevExpress.XtraEditors.CheckButton();
			this.spnAmount = new DevExpress.XtraEditors.SpinEdit();
			this.lblRemainderStatus = new DevExpress.XtraEditors.LabelControl();
			this.chkVisaPayment = new DevExpress.XtraEditors.CheckButton();
			this.chkCheckPayment = new DevExpress.XtraEditors.CheckButton();
			this.chkCashPayment = new DevExpress.XtraEditors.CheckButton();
			this.txtInvoicePaymentDescription = new DevExpress.XtraEditors.TextEdit();
			this.txtVisaDescription = new DevExpress.XtraEditors.TextEdit();
			this.txtCreditCardNumber = new DevExpress.XtraEditors.TextEdit();
			this.lkeBankAccount_VisaPayment = new DevExpress.XtraEditors.GridLookUpEdit();
			this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.lkeBank_VisaPayment = new DevExpress.XtraEditors.GridLookUpEdit();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.txtCheckDescription = new DevExpress.XtraEditors.TextEdit();
			this.txtCheckNumber = new DevExpress.XtraEditors.TextEdit();
			this.dtCheckExhcangeDate = new DevExpress.XtraEditors.DateEdit();
			this.dtCheckIssueDate = new DevExpress.XtraEditors.DateEdit();
			this.lkeBankAccount_CheckPayment = new DevExpress.XtraEditors.GridLookUpEdit();
			this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.lkeBank_CheckPayment = new DevExpress.XtraEditors.GridLookUpEdit();
			this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.chkRefund = new DevExpress.XtraEditors.CheckButton();
			this.chkPayment = new DevExpress.XtraEditors.CheckButton();
			this.lblTopSepertatorLabel = new System.Windows.Forms.Label();
			this.txtPaymentSerial = new DevExpress.XtraEditors.TextEdit();
			this.dtPaymentDate = new DevExpress.XtraEditors.DateEdit();
			this.patientTopTitle_UC1 = new CommonUserControls.CommonViewers.PatientTopTitle_UC();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem29 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem28 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.lytRemainderStatus = new DevExpress.XtraLayout.LayoutControlItem();
			this.lytIsPaymentEnough = new DevExpress.XtraLayout.LayoutControlItem();
			this.lytRemainder = new DevExpress.XtraLayout.LayoutControlItem();
			this.simpleSeparator16 = new DevExpress.XtraLayout.SimpleSeparator();
			this.simpleSeparator17 = new DevExpress.XtraLayout.SimpleSeparator();
			this.lytRequestedAmount = new DevExpress.XtraLayout.LayoutControlItem();
			this.lytTotalPayments = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.lyt = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem8 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.lytCheckDetails = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.simpleSeparator3 = new DevExpress.XtraLayout.SimpleSeparator();
			this.simpleSeparator4 = new DevExpress.XtraLayout.SimpleSeparator();
			this.simpleSeparator7 = new DevExpress.XtraLayout.SimpleSeparator();
			this.simpleSeparator8 = new DevExpress.XtraLayout.SimpleSeparator();
			this.simpleSeparator9 = new DevExpress.XtraLayout.SimpleSeparator();
			this.lytVisaDetails = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.simpleSeparator10 = new DevExpress.XtraLayout.SimpleSeparator();
			this.simpleSeparator11 = new DevExpress.XtraLayout.SimpleSeparator();
			this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem25 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem27 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.lytServiceChoosing = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem26 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem30 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem31 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem32 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem33 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.emptySpaceItem9 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.emptySpaceItem10 = new DevExpress.XtraLayout.EmptySpaceItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lkeService.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView7)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lkeServiceCategory.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnReaminder.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnTotalPayments.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnTotalRequired.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grdPayments)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnAmount.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtInvoicePaymentDescription.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtVisaDescription.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCreditCardNumber.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lkeBankAccount_VisaPayment.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lkeBank_VisaPayment.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCheckDescription.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCheckNumber.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtCheckExhcangeDate.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtCheckExhcangeDate.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtCheckIssueDate.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtCheckIssueDate.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lkeBankAccount_CheckPayment.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lkeBank_CheckPayment.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtPaymentSerial.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtPaymentDate.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtPaymentDate.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem29)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lytRemainderStatus)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lytIsPaymentEnough)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lytRemainder)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator16)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator17)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lytRequestedAmount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lytTotalPayments)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lyt)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lytCheckDetails)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator7)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator8)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator9)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lytVisaDetails)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator10)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator11)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lytServiceChoosing)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem30)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem31)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem32)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem33)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem10)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.lkeService);
			this.layoutControl1.Controls.Add(this.lkeServiceCategory);
			this.layoutControl1.Controls.Add(this.chkLabServiceType);
			this.layoutControl1.Controls.Add(this.chkInvestigationServicetype);
			this.layoutControl1.Controls.Add(this.chkExaminationServiceType);
			this.layoutControl1.Controls.Add(this.chkPatientDeposit);
			this.layoutControl1.Controls.Add(this.spnReaminder);
			this.layoutControl1.Controls.Add(this.spnTotalPayments);
			this.layoutControl1.Controls.Add(this.spnTotalRequired);
			this.layoutControl1.Controls.Add(this.grdPayments);
			this.layoutControl1.Controls.Add(this.chkIsPaymentEnoght);
			this.layoutControl1.Controls.Add(this.spnAmount);
			this.layoutControl1.Controls.Add(this.lblRemainderStatus);
			this.layoutControl1.Controls.Add(this.chkVisaPayment);
			this.layoutControl1.Controls.Add(this.chkCheckPayment);
			this.layoutControl1.Controls.Add(this.chkCashPayment);
			this.layoutControl1.Controls.Add(this.txtInvoicePaymentDescription);
			this.layoutControl1.Controls.Add(this.txtVisaDescription);
			this.layoutControl1.Controls.Add(this.txtCreditCardNumber);
			this.layoutControl1.Controls.Add(this.lkeBankAccount_VisaPayment);
			this.layoutControl1.Controls.Add(this.lkeBank_VisaPayment);
			this.layoutControl1.Controls.Add(this.txtCheckDescription);
			this.layoutControl1.Controls.Add(this.txtCheckNumber);
			this.layoutControl1.Controls.Add(this.dtCheckExhcangeDate);
			this.layoutControl1.Controls.Add(this.dtCheckIssueDate);
			this.layoutControl1.Controls.Add(this.lkeBankAccount_CheckPayment);
			this.layoutControl1.Controls.Add(this.lkeBank_CheckPayment);
			this.layoutControl1.Controls.Add(this.chkRefund);
			this.layoutControl1.Controls.Add(this.chkPayment);
			this.layoutControl1.Controls.Add(this.lblTopSepertatorLabel);
			this.layoutControl1.Controls.Add(this.txtPaymentSerial);
			this.layoutControl1.Controls.Add(this.dtPaymentDate);
			this.layoutControl1.Controls.Add(this.patientTopTitle_UC1);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(507, 368, 349, 460);
			this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(1118, 650);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "رقم الشيك";
			// 
			// lkeService
			// 
			this.lkeService.Location = new System.Drawing.Point(294, 220);
			this.lkeService.MaximumSize = new System.Drawing.Size(400, 0);
			this.lkeService.MinimumSize = new System.Drawing.Size(400, 0);
			this.lkeService.Name = "lkeService";
			this.lkeService.Properties.Appearance.BackColor = System.Drawing.Color.Gold;
			this.lkeService.Properties.Appearance.Options.UseBackColor = true;
			this.lkeService.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lkeService.Properties.View = this.gridView7;
			this.lkeService.Size = new System.Drawing.Size(400, 20);
			this.lkeService.StyleController = this.layoutControl1;
			this.lkeService.TabIndex = 13;
			// 
			// gridView7
			// 
			this.gridView7.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView7.Name = "gridView7";
			this.gridView7.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView7.OptionsView.ShowGroupPanel = false;
			// 
			// lkeServiceCategory
			// 
			this.lkeServiceCategory.Location = new System.Drawing.Point(294, 194);
			this.lkeServiceCategory.MaximumSize = new System.Drawing.Size(400, 0);
			this.lkeServiceCategory.MinimumSize = new System.Drawing.Size(400, 0);
			this.lkeServiceCategory.Name = "lkeServiceCategory";
			this.lkeServiceCategory.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
			this.lkeServiceCategory.Properties.Appearance.Options.UseBackColor = true;
			this.lkeServiceCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lkeServiceCategory.Properties.View = this.gridView6;
			this.lkeServiceCategory.Size = new System.Drawing.Size(400, 20);
			this.lkeServiceCategory.StyleController = this.layoutControl1;
			this.lkeServiceCategory.TabIndex = 12;
			this.lkeServiceCategory.EditValueChanged += new System.EventHandler(this.lkeServiceCategory_EditValueChanged);
			// 
			// gridView6
			// 
			this.gridView6.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView6.Name = "gridView6";
			this.gridView6.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView6.OptionsView.ShowGroupPanel = false;
			// 
			// chkLabServiceType
			// 
			this.chkLabServiceType.AllowAllUnchecked = true;
			this.chkLabServiceType.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.chkLabServiceType.Appearance.ForeColor = System.Drawing.Color.Yellow;
			this.chkLabServiceType.Appearance.Options.UseFont = true;
			this.chkLabServiceType.Appearance.Options.UseForeColor = true;
			this.chkLabServiceType.GroupIndex = 3;
			this.chkLabServiceType.Location = new System.Drawing.Point(527, 158);
			this.chkLabServiceType.MaximumSize = new System.Drawing.Size(100, 30);
			this.chkLabServiceType.MinimumSize = new System.Drawing.Size(100, 30);
			this.chkLabServiceType.Name = "chkLabServiceType";
			this.chkLabServiceType.Size = new System.Drawing.Size(100, 30);
			this.chkLabServiceType.StyleController = this.layoutControl1;
			this.chkLabServiceType.TabIndex = 27;
			this.chkLabServiceType.TabStop = false;
			this.chkLabServiceType.Text = "معمل";
			this.chkLabServiceType.ToolTip = "معمل";
			this.chkLabServiceType.CheckedChanged += new System.EventHandler(this.chkLabServiceType_CheckedChanged);
			// 
			// chkInvestigationServicetype
			// 
			this.chkInvestigationServicetype.AllowAllUnchecked = true;
			this.chkInvestigationServicetype.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.chkInvestigationServicetype.Appearance.ForeColor = System.Drawing.Color.Yellow;
			this.chkInvestigationServicetype.Appearance.Options.UseFont = true;
			this.chkInvestigationServicetype.Appearance.Options.UseForeColor = true;
			this.chkInvestigationServicetype.GroupIndex = 3;
			this.chkInvestigationServicetype.Location = new System.Drawing.Point(633, 158);
			this.chkInvestigationServicetype.MaximumSize = new System.Drawing.Size(100, 30);
			this.chkInvestigationServicetype.MinimumSize = new System.Drawing.Size(100, 30);
			this.chkInvestigationServicetype.Name = "chkInvestigationServicetype";
			this.chkInvestigationServicetype.Size = new System.Drawing.Size(100, 30);
			this.chkInvestigationServicetype.StyleController = this.layoutControl1;
			this.chkInvestigationServicetype.TabIndex = 26;
			this.chkInvestigationServicetype.TabStop = false;
			this.chkInvestigationServicetype.Text = "فحوصات";
			this.chkInvestigationServicetype.CheckedChanged += new System.EventHandler(this.chkInvestigationServicetype_CheckedChanged);
			// 
			// chkExaminationServiceType
			// 
			this.chkExaminationServiceType.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.chkExaminationServiceType.Appearance.ForeColor = System.Drawing.Color.Yellow;
			this.chkExaminationServiceType.Appearance.Options.UseFont = true;
			this.chkExaminationServiceType.Appearance.Options.UseForeColor = true;
			this.chkExaminationServiceType.Checked = true;
			this.chkExaminationServiceType.GroupIndex = 3;
			this.chkExaminationServiceType.Location = new System.Drawing.Point(739, 158);
			this.chkExaminationServiceType.MaximumSize = new System.Drawing.Size(100, 30);
			this.chkExaminationServiceType.MinimumSize = new System.Drawing.Size(100, 30);
			this.chkExaminationServiceType.Name = "chkExaminationServiceType";
			this.chkExaminationServiceType.Size = new System.Drawing.Size(100, 30);
			this.chkExaminationServiceType.StyleController = this.layoutControl1;
			this.chkExaminationServiceType.TabIndex = 25;
			this.chkExaminationServiceType.Text = "كشف";
			this.chkExaminationServiceType.CheckedChanged += new System.EventHandler(this.chkExaminationServiceType_CheckedChanged);
			// 
			// chkPatientDeposit
			// 
			this.chkPatientDeposit.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.chkPatientDeposit.Appearance.Options.UseFont = true;
			this.chkPatientDeposit.GroupIndex = 2;
			this.chkPatientDeposit.Location = new System.Drawing.Point(467, 102);
			this.chkPatientDeposit.MaximumSize = new System.Drawing.Size(120, 30);
			this.chkPatientDeposit.MinimumSize = new System.Drawing.Size(120, 30);
			this.chkPatientDeposit.Name = "chkPatientDeposit";
			this.chkPatientDeposit.Size = new System.Drawing.Size(120, 30);
			this.chkPatientDeposit.StyleController = this.layoutControl1;
			this.chkPatientDeposit.TabIndex = 10;
			this.chkPatientDeposit.TabStop = false;
			this.chkPatientDeposit.Text = "تحـــت الحســــاب";
			this.chkPatientDeposit.CheckedChanged += new System.EventHandler(this.chkPatientDeposit_CheckedChanged);
			// 
			// spnReaminder
			// 
			this.spnReaminder.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.spnReaminder.Location = new System.Drawing.Point(848, 231);
			this.spnReaminder.MaximumSize = new System.Drawing.Size(120, 30);
			this.spnReaminder.MinimumSize = new System.Drawing.Size(120, 30);
			this.spnReaminder.Name = "spnReaminder";
			this.spnReaminder.Properties.Appearance.BackColor = System.Drawing.Color.GreenYellow;
			this.spnReaminder.Properties.Appearance.Options.UseBackColor = true;
			this.spnReaminder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
			this.spnReaminder.Properties.ReadOnly = true;
			this.spnReaminder.Size = new System.Drawing.Size(120, 30);
			this.spnReaminder.StyleController = this.layoutControl1;
			this.spnReaminder.TabIndex = 26;
			this.spnReaminder.EditValueChanged += new System.EventHandler(this.spnReaminder_EditValueChanged);
			// 
			// spnTotalPayments
			// 
			this.spnTotalPayments.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.spnTotalPayments.Location = new System.Drawing.Point(847, 194);
			this.spnTotalPayments.MaximumSize = new System.Drawing.Size(120, 30);
			this.spnTotalPayments.MinimumSize = new System.Drawing.Size(120, 30);
			this.spnTotalPayments.Name = "spnTotalPayments";
			this.spnTotalPayments.Properties.Appearance.BackColor = System.Drawing.Color.GreenYellow;
			this.spnTotalPayments.Properties.Appearance.Options.UseBackColor = true;
			this.spnTotalPayments.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
			this.spnTotalPayments.Properties.ReadOnly = true;
			this.spnTotalPayments.Size = new System.Drawing.Size(120, 30);
			this.spnTotalPayments.StyleController = this.layoutControl1;
			this.spnTotalPayments.TabIndex = 25;
			// 
			// spnTotalRequired
			// 
			this.spnTotalRequired.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.spnTotalRequired.Location = new System.Drawing.Point(847, 158);
			this.spnTotalRequired.MaximumSize = new System.Drawing.Size(120, 30);
			this.spnTotalRequired.MinimumSize = new System.Drawing.Size(120, 30);
			this.spnTotalRequired.Name = "spnTotalRequired";
			this.spnTotalRequired.Properties.Appearance.BackColor = System.Drawing.Color.GreenYellow;
			this.spnTotalRequired.Properties.Appearance.Options.UseBackColor = true;
			this.spnTotalRequired.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "", null, null, true)});
			this.spnTotalRequired.Properties.ReadOnly = true;
			this.spnTotalRequired.Size = new System.Drawing.Size(120, 30);
			this.spnTotalRequired.StyleController = this.layoutControl1;
			this.spnTotalRequired.TabIndex = 24;
			// 
			// grdPayments
			// 
			this.grdPayments.Location = new System.Drawing.Point(4, 560);
			this.grdPayments.MainView = this.gridView3;
			this.grdPayments.Name = "grdPayments";
			this.grdPayments.Size = new System.Drawing.Size(1110, 86);
			this.grdPayments.TabIndex = 24;
			this.grdPayments.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
			// 
			// gridView3
			// 
			this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn9,
            this.gridColumn4,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn15,
            this.gridColumn13,
            this.gridColumn14});
			this.gridView3.GridControl = this.grdPayments;
			this.gridView3.Name = "gridView3";
			this.gridView3.OptionsView.ShowAutoFilterRow = true;
			// 
			// gridColumn1
			// 
			this.gridColumn1.AppearanceCell.BackColor = System.Drawing.SystemColors.Info;
			this.gridColumn1.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn1.Caption = "ت. الدفع";
			this.gridColumn1.DisplayFormat.FormatString = "d";
			this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.gridColumn1.FieldName = "PaymentDate";
			this.gridColumn1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.OptionsColumn.AllowEdit = false;
			this.gridColumn1.OptionsColumn.FixedWidth = true;
			this.gridColumn1.OptionsColumn.ReadOnly = true;
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 0;
			this.gridColumn1.Width = 80;
			// 
			// gridColumn3
			// 
			this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn3.Caption = "سيريال الدفع";
			this.gridColumn3.FieldName = "FloorName";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.OptionsColumn.AllowEdit = false;
			this.gridColumn3.OptionsColumn.FixedWidth = true;
			this.gridColumn3.OptionsColumn.ReadOnly = true;
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 1;
			this.gridColumn3.Width = 100;
			// 
			// gridColumn9
			// 
			this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn9.Caption = "----";
			this.gridColumn9.FieldName = "PaymentMethodName";
			this.gridColumn9.Name = "gridColumn9";
			this.gridColumn9.OptionsColumn.AllowEdit = false;
			this.gridColumn9.OptionsColumn.FixedWidth = true;
			this.gridColumn9.OptionsColumn.ReadOnly = true;
			this.gridColumn9.Visible = true;
			this.gridColumn9.VisibleIndex = 2;
			this.gridColumn9.Width = 50;
			// 
			// gridColumn4
			// 
			this.gridColumn4.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.gridColumn4.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn4.Caption = "القيمــــة";
			this.gridColumn4.FieldName = "TotalPayments";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.OptionsColumn.AllowEdit = false;
			this.gridColumn4.OptionsColumn.FixedWidth = true;
			this.gridColumn4.OptionsColumn.ReadOnly = true;
			this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalPayments", "SUM={0:#,##0.##}")});
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 3;
			this.gridColumn4.Width = 120;
			// 
			// gridColumn6
			// 
			this.gridColumn6.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.gridColumn6.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn6.Caption = "كاش";
			this.gridColumn6.FieldName = "CashPayments";
			this.gridColumn6.Name = "gridColumn6";
			this.gridColumn6.OptionsColumn.AllowEdit = false;
			this.gridColumn6.OptionsColumn.FixedWidth = true;
			this.gridColumn6.OptionsColumn.ReadOnly = true;
			this.gridColumn6.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CashPayments", "SUM={0:#,##0.##}")});
			this.gridColumn6.Visible = true;
			this.gridColumn6.VisibleIndex = 4;
			this.gridColumn6.Width = 120;
			// 
			// gridColumn7
			// 
			this.gridColumn7.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.gridColumn7.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn7.Caption = "شيك";
			this.gridColumn7.FieldName = "CheckPayments";
			this.gridColumn7.Name = "gridColumn7";
			this.gridColumn7.OptionsColumn.AllowEdit = false;
			this.gridColumn7.OptionsColumn.FixedWidth = true;
			this.gridColumn7.OptionsColumn.ReadOnly = true;
			this.gridColumn7.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CheckPayments", "SUM={0:#,##0.##}")});
			this.gridColumn7.Visible = true;
			this.gridColumn7.VisibleIndex = 5;
			this.gridColumn7.Width = 120;
			// 
			// gridColumn8
			// 
			this.gridColumn8.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.gridColumn8.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn8.Caption = "فيـــزا";
			this.gridColumn8.FieldName = "VisaPayments";
			this.gridColumn8.Name = "gridColumn8";
			this.gridColumn8.OptionsColumn.AllowEdit = false;
			this.gridColumn8.OptionsColumn.FixedWidth = true;
			this.gridColumn8.OptionsColumn.ReadOnly = true;
			this.gridColumn8.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "VisaPayments", "SUM={0:#,##0.##}")});
			this.gridColumn8.Visible = true;
			this.gridColumn8.VisibleIndex = 6;
			this.gridColumn8.Width = 120;
			// 
			// gridColumn10
			// 
			this.gridColumn10.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn10.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn10.Caption = "رقم الفيزا";
			this.gridColumn10.FieldName = "CreditCardNumber";
			this.gridColumn10.Name = "gridColumn10";
			this.gridColumn10.OptionsColumn.AllowEdit = false;
			this.gridColumn10.OptionsColumn.FixedWidth = true;
			this.gridColumn10.OptionsColumn.ReadOnly = true;
			this.gridColumn10.Visible = true;
			this.gridColumn10.VisibleIndex = 7;
			this.gridColumn10.Width = 100;
			// 
			// gridColumn11
			// 
			this.gridColumn11.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn11.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn11.Caption = "إســـم البنـــك";
			this.gridColumn11.FieldName = "BankName";
			this.gridColumn11.Name = "gridColumn11";
			this.gridColumn11.OptionsColumn.AllowEdit = false;
			this.gridColumn11.OptionsColumn.FixedWidth = true;
			this.gridColumn11.OptionsColumn.ReadOnly = true;
			this.gridColumn11.Visible = true;
			this.gridColumn11.VisibleIndex = 8;
			this.gridColumn11.Width = 150;
			// 
			// gridColumn12
			// 
			this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn12.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn12.Caption = "إســـم الحســاب";
			this.gridColumn12.FieldName = "BankAccountName";
			this.gridColumn12.Name = "gridColumn12";
			this.gridColumn12.OptionsColumn.AllowEdit = false;
			this.gridColumn12.OptionsColumn.FixedWidth = true;
			this.gridColumn12.OptionsColumn.ReadOnly = true;
			this.gridColumn12.Visible = true;
			this.gridColumn12.VisibleIndex = 9;
			this.gridColumn12.Width = 150;
			// 
			// gridColumn15
			// 
			this.gridColumn15.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn15.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn15.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn15.Caption = "رقم الشيك";
			this.gridColumn15.FieldName = "CheckNumber";
			this.gridColumn15.Name = "gridColumn15";
			this.gridColumn15.OptionsColumn.AllowEdit = false;
			this.gridColumn15.OptionsColumn.FixedWidth = true;
			this.gridColumn15.OptionsColumn.ReadOnly = true;
			this.gridColumn15.Visible = true;
			this.gridColumn15.VisibleIndex = 10;
			this.gridColumn15.Width = 100;
			// 
			// gridColumn13
			// 
			this.gridColumn13.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn13.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn13.Caption = "ت. إصدار الشيك";
			this.gridColumn13.FieldName = "CheckIssueDate";
			this.gridColumn13.Name = "gridColumn13";
			this.gridColumn13.OptionsColumn.AllowEdit = false;
			this.gridColumn13.OptionsColumn.FixedWidth = true;
			this.gridColumn13.OptionsColumn.ReadOnly = true;
			this.gridColumn13.Visible = true;
			this.gridColumn13.VisibleIndex = 11;
			this.gridColumn13.Width = 100;
			// 
			// gridColumn14
			// 
			this.gridColumn14.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn14.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn14.Caption = "ت. إستحقاق الشيك";
			this.gridColumn14.FieldName = "ExchangeDate";
			this.gridColumn14.Name = "gridColumn14";
			this.gridColumn14.OptionsColumn.AllowEdit = false;
			this.gridColumn14.OptionsColumn.ReadOnly = true;
			this.gridColumn14.Visible = true;
			this.gridColumn14.VisibleIndex = 12;
			this.gridColumn14.Width = 100;
			// 
			// chkIsPaymentEnoght
			// 
			this.chkIsPaymentEnoght.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.chkIsPaymentEnoght.Appearance.Options.UseFont = true;
			this.chkIsPaymentEnoght.Location = new System.Drawing.Point(848, 286);
			this.chkIsPaymentEnoght.MaximumSize = new System.Drawing.Size(0, 40);
			this.chkIsPaymentEnoght.MinimumSize = new System.Drawing.Size(0, 40);
			this.chkIsPaymentEnoght.Name = "chkIsPaymentEnoght";
			this.chkIsPaymentEnoght.Size = new System.Drawing.Size(265, 40);
			this.chkIsPaymentEnoght.StyleController = this.layoutControl1;
			this.chkIsPaymentEnoght.TabIndex = 10;
			this.chkIsPaymentEnoght.Text = "المـدفـوعـــات كـافيــــة";
			this.chkIsPaymentEnoght.CheckedChanged += new System.EventHandler(this.chkIsPaymentEnoght_CheckedChanged);
			// 
			// spnAmount
			// 
			this.spnAmount.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.spnAmount.Location = new System.Drawing.Point(575, 247);
			this.spnAmount.MaximumSize = new System.Drawing.Size(120, 30);
			this.spnAmount.MinimumSize = new System.Drawing.Size(120, 30);
			this.spnAmount.Name = "spnAmount";
			this.spnAmount.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan;
			this.spnAmount.Properties.Appearance.Options.UseBackColor = true;
			this.spnAmount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.spnAmount.Size = new System.Drawing.Size(120, 30);
			this.spnAmount.StyleController = this.layoutControl1;
			this.spnAmount.TabIndex = 23;
			this.spnAmount.EditValueChanged += new System.EventHandler(this.spnAmount_EditValueChanged);
			// 
			// lblRemainderStatus
			// 
			this.lblRemainderStatus.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.lblRemainderStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.lblRemainderStatus.Location = new System.Drawing.Point(848, 267);
			this.lblRemainderStatus.Name = "lblRemainderStatus";
			this.lblRemainderStatus.Size = new System.Drawing.Size(265, 13);
			this.lblRemainderStatus.StyleController = this.layoutControl1;
			this.lblRemainderStatus.TabIndex = 22;
			this.lblRemainderStatus.Text = "عليه";
			// 
			// chkVisaPayment
			// 
			this.chkVisaPayment.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.chkVisaPayment.Appearance.Options.UseFont = true;
			this.chkVisaPayment.GroupIndex = 1;
			this.chkVisaPayment.Location = new System.Drawing.Point(147, 102);
			this.chkVisaPayment.MaximumSize = new System.Drawing.Size(100, 30);
			this.chkVisaPayment.MinimumSize = new System.Drawing.Size(100, 30);
			this.chkVisaPayment.Name = "chkVisaPayment";
			this.chkVisaPayment.Size = new System.Drawing.Size(100, 30);
			this.chkVisaPayment.StyleController = this.layoutControl1;
			this.chkVisaPayment.TabIndex = 11;
			this.chkVisaPayment.TabStop = false;
			this.chkVisaPayment.Text = "فيزا";
			this.chkVisaPayment.CheckedChanged += new System.EventHandler(this.chkVisaPayment_CheckedChanged);
			// 
			// chkCheckPayment
			// 
			this.chkCheckPayment.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.chkCheckPayment.Appearance.Options.UseFont = true;
			this.chkCheckPayment.GroupIndex = 1;
			this.chkCheckPayment.Location = new System.Drawing.Point(253, 102);
			this.chkCheckPayment.MaximumSize = new System.Drawing.Size(100, 30);
			this.chkCheckPayment.MinimumSize = new System.Drawing.Size(100, 30);
			this.chkCheckPayment.Name = "chkCheckPayment";
			this.chkCheckPayment.Size = new System.Drawing.Size(100, 30);
			this.chkCheckPayment.StyleController = this.layoutControl1;
			this.chkCheckPayment.TabIndex = 10;
			this.chkCheckPayment.TabStop = false;
			this.chkCheckPayment.Text = "شيك";
			this.chkCheckPayment.CheckedChanged += new System.EventHandler(this.chkCheckPayment_CheckedChanged);
			// 
			// chkCashPayment
			// 
			this.chkCashPayment.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.chkCashPayment.Appearance.Options.UseFont = true;
			this.chkCashPayment.Checked = true;
			this.chkCashPayment.GroupIndex = 1;
			this.chkCashPayment.Location = new System.Drawing.Point(359, 102);
			this.chkCashPayment.MaximumSize = new System.Drawing.Size(100, 30);
			this.chkCashPayment.MinimumSize = new System.Drawing.Size(100, 30);
			this.chkCashPayment.Name = "chkCashPayment";
			this.chkCashPayment.Size = new System.Drawing.Size(100, 30);
			this.chkCashPayment.StyleController = this.layoutControl1;
			this.chkCashPayment.TabIndex = 9;
			this.chkCashPayment.Text = "كاش";
			this.chkCashPayment.CheckedChanged += new System.EventHandler(this.chkCashPayment_CheckedChanged);
			// 
			// txtInvoicePaymentDescription
			// 
			this.txtInvoicePaymentDescription.Location = new System.Drawing.Point(136, 515);
			this.txtInvoicePaymentDescription.Name = "txtInvoicePaymentDescription";
			this.txtInvoicePaymentDescription.Size = new System.Drawing.Size(559, 20);
			this.txtInvoicePaymentDescription.StyleController = this.layoutControl1;
			this.txtInvoicePaymentDescription.TabIndex = 10;
			// 
			// txtVisaDescription
			// 
			this.txtVisaDescription.Location = new System.Drawing.Point(137, 488);
			this.txtVisaDescription.Name = "txtVisaDescription";
			this.txtVisaDescription.Size = new System.Drawing.Size(557, 20);
			this.txtVisaDescription.StyleController = this.layoutControl1;
			this.txtVisaDescription.TabIndex = 9;
			// 
			// txtCreditCardNumber
			// 
			this.txtCreditCardNumber.Location = new System.Drawing.Point(494, 460);
			this.txtCreditCardNumber.MaximumSize = new System.Drawing.Size(200, 0);
			this.txtCreditCardNumber.MinimumSize = new System.Drawing.Size(200, 0);
			this.txtCreditCardNumber.Name = "txtCreditCardNumber";
			this.txtCreditCardNumber.Size = new System.Drawing.Size(200, 20);
			this.txtCreditCardNumber.StyleController = this.layoutControl1;
			this.txtCreditCardNumber.TabIndex = 8;
			// 
			// lkeBankAccount_VisaPayment
			// 
			this.lkeBankAccount_VisaPayment.Location = new System.Drawing.Point(137, 432);
			this.lkeBankAccount_VisaPayment.MaximumSize = new System.Drawing.Size(200, 0);
			this.lkeBankAccount_VisaPayment.MinimumSize = new System.Drawing.Size(200, 0);
			this.lkeBankAccount_VisaPayment.Name = "lkeBankAccount_VisaPayment";
			this.lkeBankAccount_VisaPayment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lkeBankAccount_VisaPayment.Properties.View = this.gridView2;
			this.lkeBankAccount_VisaPayment.Size = new System.Drawing.Size(200, 20);
			this.lkeBankAccount_VisaPayment.StyleController = this.layoutControl1;
			this.lkeBankAccount_VisaPayment.TabIndex = 13;
			// 
			// gridView2
			// 
			this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView2.Name = "gridView2";
			this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView2.OptionsView.ShowGroupPanel = false;
			// 
			// lkeBank_VisaPayment
			// 
			this.lkeBank_VisaPayment.Location = new System.Drawing.Point(488, 432);
			this.lkeBank_VisaPayment.MaximumSize = new System.Drawing.Size(200, 0);
			this.lkeBank_VisaPayment.MinimumSize = new System.Drawing.Size(200, 0);
			this.lkeBank_VisaPayment.Name = "lkeBank_VisaPayment";
			this.lkeBank_VisaPayment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lkeBank_VisaPayment.Properties.View = this.gridView1;
			this.lkeBank_VisaPayment.Size = new System.Drawing.Size(200, 20);
			this.lkeBank_VisaPayment.StyleController = this.layoutControl1;
			this.lkeBank_VisaPayment.TabIndex = 12;
			this.lkeBank_VisaPayment.EditValueChanged += new System.EventHandler(this.lkeBank_VisaPayment_EditValueChanged);
			// 
			// gridView1
			// 
			this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			// 
			// txtCheckDescription
			// 
			this.txtCheckDescription.Location = new System.Drawing.Point(137, 386);
			this.txtCheckDescription.Name = "txtCheckDescription";
			this.txtCheckDescription.Size = new System.Drawing.Size(557, 20);
			this.txtCheckDescription.StyleController = this.layoutControl1;
			this.txtCheckDescription.TabIndex = 8;
			// 
			// txtCheckNumber
			// 
			this.txtCheckNumber.Location = new System.Drawing.Point(494, 358);
			this.txtCheckNumber.MaximumSize = new System.Drawing.Size(200, 0);
			this.txtCheckNumber.MinimumSize = new System.Drawing.Size(200, 0);
			this.txtCheckNumber.Name = "txtCheckNumber";
			this.txtCheckNumber.Size = new System.Drawing.Size(200, 20);
			this.txtCheckNumber.StyleController = this.layoutControl1;
			this.txtCheckNumber.TabIndex = 7;
			// 
			// dtCheckExhcangeDate
			// 
			this.dtCheckExhcangeDate.EditValue = null;
			this.dtCheckExhcangeDate.Location = new System.Drawing.Point(301, 330);
			this.dtCheckExhcangeDate.MaximumSize = new System.Drawing.Size(120, 0);
			this.dtCheckExhcangeDate.MinimumSize = new System.Drawing.Size(120, 0);
			this.dtCheckExhcangeDate.Name = "dtCheckExhcangeDate";
			this.dtCheckExhcangeDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dtCheckExhcangeDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dtCheckExhcangeDate.Size = new System.Drawing.Size(120, 20);
			this.dtCheckExhcangeDate.StyleController = this.layoutControl1;
			this.dtCheckExhcangeDate.TabIndex = 14;
			// 
			// dtCheckIssueDate
			// 
			this.dtCheckIssueDate.EditValue = null;
			this.dtCheckIssueDate.Location = new System.Drawing.Point(574, 330);
			this.dtCheckIssueDate.MaximumSize = new System.Drawing.Size(120, 0);
			this.dtCheckIssueDate.MinimumSize = new System.Drawing.Size(120, 0);
			this.dtCheckIssueDate.Name = "dtCheckIssueDate";
			this.dtCheckIssueDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dtCheckIssueDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dtCheckIssueDate.Size = new System.Drawing.Size(120, 20);
			this.dtCheckIssueDate.StyleController = this.layoutControl1;
			this.dtCheckIssueDate.TabIndex = 13;
			// 
			// lkeBankAccount_CheckPayment
			// 
			this.lkeBankAccount_CheckPayment.Location = new System.Drawing.Point(137, 302);
			this.lkeBankAccount_CheckPayment.MaximumSize = new System.Drawing.Size(200, 0);
			this.lkeBankAccount_CheckPayment.MinimumSize = new System.Drawing.Size(200, 0);
			this.lkeBankAccount_CheckPayment.Name = "lkeBankAccount_CheckPayment";
			this.lkeBankAccount_CheckPayment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lkeBankAccount_CheckPayment.Properties.View = this.gridView4;
			this.lkeBankAccount_CheckPayment.Size = new System.Drawing.Size(200, 20);
			this.lkeBankAccount_CheckPayment.StyleController = this.layoutControl1;
			this.lkeBankAccount_CheckPayment.TabIndex = 12;
			// 
			// gridView4
			// 
			this.gridView4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView4.Name = "gridView4";
			this.gridView4.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView4.OptionsView.ShowGroupPanel = false;
			// 
			// lkeBank_CheckPayment
			// 
			this.lkeBank_CheckPayment.Location = new System.Drawing.Point(490, 302);
			this.lkeBank_CheckPayment.MaximumSize = new System.Drawing.Size(200, 0);
			this.lkeBank_CheckPayment.MinimumSize = new System.Drawing.Size(200, 0);
			this.lkeBank_CheckPayment.Name = "lkeBank_CheckPayment";
			this.lkeBank_CheckPayment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lkeBank_CheckPayment.Properties.View = this.gridView5;
			this.lkeBank_CheckPayment.Size = new System.Drawing.Size(200, 20);
			this.lkeBank_CheckPayment.StyleController = this.layoutControl1;
			this.lkeBank_CheckPayment.TabIndex = 11;
			this.lkeBank_CheckPayment.EditValueChanged += new System.EventHandler(this.lkeBank_CheckPayment_EditValueChanged);
			// 
			// gridView5
			// 
			this.gridView5.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView5.Name = "gridView5";
			this.gridView5.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView5.OptionsView.ShowGroupPanel = false;
			// 
			// chkRefund
			// 
			this.chkRefund.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.chkRefund.Appearance.Options.UseFont = true;
			this.chkRefund.GroupIndex = 2;
			this.chkRefund.Location = new System.Drawing.Point(593, 102);
			this.chkRefund.MaximumSize = new System.Drawing.Size(120, 30);
			this.chkRefund.MinimumSize = new System.Drawing.Size(120, 30);
			this.chkRefund.Name = "chkRefund";
			this.chkRefund.Size = new System.Drawing.Size(120, 30);
			this.chkRefund.StyleController = this.layoutControl1;
			this.chkRefund.TabIndex = 9;
			this.chkRefund.TabStop = false;
			this.chkRefund.Text = "إسترداد";
			this.chkRefund.CheckedChanged += new System.EventHandler(this.chkRefund_CheckedChanged);
			// 
			// chkPayment
			// 
			this.chkPayment.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.chkPayment.Appearance.Options.UseFont = true;
			this.chkPayment.Checked = true;
			this.chkPayment.GroupIndex = 2;
			this.chkPayment.Location = new System.Drawing.Point(719, 102);
			this.chkPayment.MaximumSize = new System.Drawing.Size(120, 30);
			this.chkPayment.MinimumSize = new System.Drawing.Size(120, 30);
			this.chkPayment.Name = "chkPayment";
			this.chkPayment.Size = new System.Drawing.Size(120, 30);
			this.chkPayment.StyleController = this.layoutControl1;
			this.chkPayment.TabIndex = 8;
			this.chkPayment.Text = "دفع";
			this.chkPayment.CheckedChanged += new System.EventHandler(this.chkPayment_CheckedChanged);
			// 
			// lblTopSepertatorLabel
			// 
			this.lblTopSepertatorLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(182)))), ((int)(((byte)(42)))));
			this.lblTopSepertatorLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblTopSepertatorLabel.Location = new System.Drawing.Point(0, 95);
			this.lblTopSepertatorLabel.MaximumSize = new System.Drawing.Size(0, 3);
			this.lblTopSepertatorLabel.MinimumSize = new System.Drawing.Size(0, 3);
			this.lblTopSepertatorLabel.Name = "lblTopSepertatorLabel";
			this.lblTopSepertatorLabel.Size = new System.Drawing.Size(1118, 3);
			this.lblTopSepertatorLabel.TabIndex = 21;
			// 
			// txtPaymentSerial
			// 
			this.txtPaymentSerial.EditValue = "00000";
			this.txtPaymentSerial.Location = new System.Drawing.Point(847, 130);
			this.txtPaymentSerial.MaximumSize = new System.Drawing.Size(120, 0);
			this.txtPaymentSerial.MinimumSize = new System.Drawing.Size(120, 0);
			this.txtPaymentSerial.Name = "txtPaymentSerial";
			this.txtPaymentSerial.Properties.Appearance.BackColor = System.Drawing.Color.Gray;
			this.txtPaymentSerial.Properties.Appearance.ForeColor = System.Drawing.Color.White;
			this.txtPaymentSerial.Properties.Appearance.Options.UseBackColor = true;
			this.txtPaymentSerial.Properties.Appearance.Options.UseForeColor = true;
			this.txtPaymentSerial.Properties.Appearance.Options.UseTextOptions = true;
			this.txtPaymentSerial.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.txtPaymentSerial.Size = new System.Drawing.Size(120, 20);
			this.txtPaymentSerial.StyleController = this.layoutControl1;
			this.txtPaymentSerial.TabIndex = 6;
			// 
			// dtPaymentDate
			// 
			this.dtPaymentDate.EditValue = null;
			this.dtPaymentDate.Location = new System.Drawing.Point(847, 102);
			this.dtPaymentDate.MaximumSize = new System.Drawing.Size(120, 0);
			this.dtPaymentDate.MinimumSize = new System.Drawing.Size(120, 0);
			this.dtPaymentDate.Name = "dtPaymentDate";
			this.dtPaymentDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dtPaymentDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dtPaymentDate.Size = new System.Drawing.Size(120, 20);
			this.dtPaymentDate.StyleController = this.layoutControl1;
			this.dtPaymentDate.TabIndex = 15;
			// 
			// patientTopTitle_UC1
			// 
			this.patientTopTitle_UC1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("patientTopTitle_UC1.BackgroundImage")));
			this.patientTopTitle_UC1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.patientTopTitle_UC1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.patientTopTitle_UC1.Location = new System.Drawing.Point(0, 0);
			this.patientTopTitle_UC1.LookAndFeel.SkinMaskColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(74)))));
			this.patientTopTitle_UC1.LookAndFeel.SkinName = "Office 2010 Black";
			this.patientTopTitle_UC1.MaximumSize = new System.Drawing.Size(0, 95);
			this.patientTopTitle_UC1.MinimumSize = new System.Drawing.Size(0, 95);
			this.patientTopTitle_UC1.Name = "patientTopTitle_UC1";
			this.patientTopTitle_UC1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.patientTopTitle_UC1.Size = new System.Drawing.Size(1118, 95);
			this.patientTopTitle_UC1.TabIndex = 5;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem5,
            this.layoutControlGroup5,
            this.emptySpaceItem6,
            this.lyt,
            this.emptySpaceItem8,
            this.lytCheckDetails,
            this.lytVisaDetails,
            this.layoutControlItem17,
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.layoutControlGroup4,
            this.emptySpaceItem2,
            this.layoutControlItem27,
            this.layoutControlItem2,
            this.lytServiceChoosing});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(1118, 650);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// emptySpaceItem5
			// 
			this.emptySpaceItem5.AllowHotTrack = false;
			this.emptySpaceItem5.Location = new System.Drawing.Point(843, 331);
			this.emptySpaceItem5.Name = "emptySpaceItem5";
			this.emptySpaceItem5.Size = new System.Drawing.Size(275, 207);
			this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlGroup5
			// 
			this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem29,
            this.layoutControlItem28,
            this.layoutControlGroup6,
            this.simpleSeparator16,
            this.simpleSeparator17,
            this.lytRequestedAmount,
            this.lytTotalPayments});
			this.layoutControlGroup5.Location = new System.Drawing.Point(843, 98);
			this.layoutControlGroup5.Name = "layoutControlGroup5";
			this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup5.Size = new System.Drawing.Size(275, 233);
			this.layoutControlGroup5.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup5.TextVisible = false;
			// 
			// layoutControlItem29
			// 
			this.layoutControlItem29.Control = this.txtPaymentSerial;
			this.layoutControlItem29.Location = new System.Drawing.Point(0, 28);
			this.layoutControlItem29.Name = "layoutControlItem29";
			this.layoutControlItem29.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem29.Size = new System.Drawing.Size(273, 26);
			this.layoutControlItem29.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem29.Text = "مسلسل";
			this.layoutControlItem29.TextSize = new System.Drawing.Size(142, 13);
			// 
			// layoutControlItem28
			// 
			this.layoutControlItem28.Control = this.dtPaymentDate;
			this.layoutControlItem28.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem28.Name = "layoutControlItem28";
			this.layoutControlItem28.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem28.Size = new System.Drawing.Size(273, 26);
			this.layoutControlItem28.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem28.Text = "التاريخ";
			this.layoutControlItem28.TextSize = new System.Drawing.Size(142, 13);
			// 
			// layoutControlGroup6
			// 
			this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lytRemainderStatus,
            this.lytIsPaymentEnough,
            this.lytRemainder});
			this.layoutControlGroup6.Location = new System.Drawing.Point(0, 128);
			this.layoutControlGroup6.Name = "layoutControlGroup6";
			this.layoutControlGroup6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup6.Size = new System.Drawing.Size(273, 103);
			this.layoutControlGroup6.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup6.TextVisible = false;
			// 
			// lytRemainderStatus
			// 
			this.lytRemainderStatus.Control = this.lblRemainderStatus;
			this.lytRemainderStatus.Location = new System.Drawing.Point(0, 36);
			this.lytRemainderStatus.Name = "lytRemainderStatus";
			this.lytRemainderStatus.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytRemainderStatus.Size = new System.Drawing.Size(271, 19);
			this.lytRemainderStatus.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.lytRemainderStatus.TextSize = new System.Drawing.Size(0, 0);
			this.lytRemainderStatus.TextVisible = false;
			// 
			// lytIsPaymentEnough
			// 
			this.lytIsPaymentEnough.Control = this.chkIsPaymentEnoght;
			this.lytIsPaymentEnough.Location = new System.Drawing.Point(0, 55);
			this.lytIsPaymentEnough.Name = "lytIsPaymentEnough";
			this.lytIsPaymentEnough.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytIsPaymentEnough.Size = new System.Drawing.Size(271, 46);
			this.lytIsPaymentEnough.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.lytIsPaymentEnough.TextSize = new System.Drawing.Size(0, 0);
			this.lytIsPaymentEnough.TextVisible = false;
			// 
			// lytRemainder
			// 
			this.lytRemainder.Control = this.spnReaminder;
			this.lytRemainder.Location = new System.Drawing.Point(0, 0);
			this.lytRemainder.Name = "lytRemainder";
			this.lytRemainder.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytRemainder.Size = new System.Drawing.Size(271, 36);
			this.lytRemainder.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.lytRemainder.Text = "المتبقي";
			this.lytRemainder.TextSize = new System.Drawing.Size(142, 13);
			// 
			// simpleSeparator16
			// 
			this.simpleSeparator16.AllowHotTrack = false;
			this.simpleSeparator16.Location = new System.Drawing.Point(0, 26);
			this.simpleSeparator16.Name = "simpleSeparator16";
			this.simpleSeparator16.Size = new System.Drawing.Size(273, 2);
			// 
			// simpleSeparator17
			// 
			this.simpleSeparator17.AllowHotTrack = false;
			this.simpleSeparator17.Location = new System.Drawing.Point(0, 54);
			this.simpleSeparator17.Name = "simpleSeparator17";
			this.simpleSeparator17.Size = new System.Drawing.Size(273, 2);
			// 
			// lytRequestedAmount
			// 
			this.lytRequestedAmount.Control = this.spnTotalRequired;
			this.lytRequestedAmount.Location = new System.Drawing.Point(0, 56);
			this.lytRequestedAmount.Name = "lytRequestedAmount";
			this.lytRequestedAmount.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytRequestedAmount.Size = new System.Drawing.Size(273, 36);
			this.lytRequestedAmount.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.lytRequestedAmount.Text = "إجمالى المطلوب من المريض";
			this.lytRequestedAmount.TextSize = new System.Drawing.Size(142, 13);
			// 
			// lytTotalPayments
			// 
			this.lytTotalPayments.Control = this.spnTotalPayments;
			this.lytTotalPayments.Location = new System.Drawing.Point(0, 92);
			this.lytTotalPayments.Name = "lytTotalPayments";
			this.lytTotalPayments.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytTotalPayments.Size = new System.Drawing.Size(273, 36);
			this.lytTotalPayments.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.lytTotalPayments.Text = "إجمالى المدفوعات";
			this.lytTotalPayments.TextSize = new System.Drawing.Size(142, 13);
			// 
			// emptySpaceItem6
			// 
			this.emptySpaceItem6.AllowHotTrack = false;
			this.emptySpaceItem6.Location = new System.Drawing.Point(133, 98);
			this.emptySpaceItem6.Name = "emptySpaceItem6";
			this.emptySpaceItem6.Size = new System.Drawing.Size(10, 38);
			this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
			// 
			// lyt
			// 
			this.lyt.Control = this.spnAmount;
			this.lyt.Location = new System.Drawing.Point(572, 244);
			this.lyt.Name = "lyt";
			this.lyt.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lyt.Size = new System.Drawing.Size(271, 36);
			this.lyt.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.lyt.Text = "القيمة";
			this.lyt.TextSize = new System.Drawing.Size(142, 13);
			// 
			// emptySpaceItem8
			// 
			this.emptySpaceItem8.AllowHotTrack = false;
			this.emptySpaceItem8.Location = new System.Drawing.Point(133, 244);
			this.emptySpaceItem8.Name = "emptySpaceItem8";
			this.emptySpaceItem8.Size = new System.Drawing.Size(439, 36);
			this.emptySpaceItem8.TextSize = new System.Drawing.Size(0, 0);
			// 
			// lytCheckDetails
			// 
			this.lytCheckDetails.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.lytCheckDetails.AppearanceGroup.ForeColor = System.Drawing.Color.OldLace;
			this.lytCheckDetails.AppearanceGroup.Options.UseFont = true;
			this.lytCheckDetails.AppearanceGroup.Options.UseForeColor = true;
			this.lytCheckDetails.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem12,
            this.layoutControlItem11,
            this.layoutControlItem9,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem10,
            this.emptySpaceItem3,
            this.emptySpaceItem1,
            this.simpleSeparator3,
            this.simpleSeparator4,
            this.simpleSeparator7,
            this.simpleSeparator8,
            this.simpleSeparator9});
			this.lytCheckDetails.Location = new System.Drawing.Point(133, 280);
			this.lytCheckDetails.Name = "lytCheckDetails";
			this.lytCheckDetails.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytCheckDetails.Size = new System.Drawing.Size(710, 130);
			this.lytCheckDetails.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytCheckDetails.Text = "بيانات الشيك";
			this.lytCheckDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
			// 
			// layoutControlItem12
			// 
			this.layoutControlItem12.Control = this.txtCheckDescription;
			this.layoutControlItem12.Location = new System.Drawing.Point(0, 84);
			this.layoutControlItem12.Name = "layoutControlItem12";
			this.layoutControlItem12.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem12.Size = new System.Drawing.Size(708, 26);
			this.layoutControlItem12.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem12.Text = "ملاحظات على الشيك";
			this.layoutControlItem12.TextSize = new System.Drawing.Size(142, 13);
			// 
			// layoutControlItem11
			// 
			this.layoutControlItem11.Control = this.txtCheckNumber;
			this.layoutControlItem11.Location = new System.Drawing.Point(357, 56);
			this.layoutControlItem11.Name = "layoutControlItem11";
			this.layoutControlItem11.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem11.Size = new System.Drawing.Size(351, 26);
			this.layoutControlItem11.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem11.Text = "رقم الشيك";
			this.layoutControlItem11.TextSize = new System.Drawing.Size(142, 13);
			// 
			// layoutControlItem9
			// 
			this.layoutControlItem9.Control = this.dtCheckIssueDate;
			this.layoutControlItem9.Location = new System.Drawing.Point(437, 28);
			this.layoutControlItem9.Name = "layoutControlItem9";
			this.layoutControlItem9.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem9.Size = new System.Drawing.Size(271, 26);
			this.layoutControlItem9.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem9.Text = "تاريخ الإصدار";
			this.layoutControlItem9.TextSize = new System.Drawing.Size(142, 13);
			// 
			// layoutControlItem7
			// 
			this.layoutControlItem7.Control = this.lkeBank_CheckPayment;
			this.layoutControlItem7.Location = new System.Drawing.Point(353, 0);
			this.layoutControlItem7.Name = "layoutControlItem7";
			this.layoutControlItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem7.Size = new System.Drawing.Size(355, 26);
			this.layoutControlItem7.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem7.Text = "البنك";
			this.layoutControlItem7.TextSize = new System.Drawing.Size(142, 13);
			// 
			// layoutControlItem8
			// 
			this.layoutControlItem8.Control = this.lkeBankAccount_CheckPayment;
			this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem8.Name = "layoutControlItem8";
			this.layoutControlItem8.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem8.Size = new System.Drawing.Size(351, 26);
			this.layoutControlItem8.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem8.Text = "الحساب البنكي";
			this.layoutControlItem8.TextSize = new System.Drawing.Size(142, 13);
			// 
			// layoutControlItem10
			// 
			this.layoutControlItem10.Control = this.dtCheckExhcangeDate;
			this.layoutControlItem10.Location = new System.Drawing.Point(164, 28);
			this.layoutControlItem10.Name = "layoutControlItem10";
			this.layoutControlItem10.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem10.Size = new System.Drawing.Size(271, 26);
			this.layoutControlItem10.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem10.Text = "تاريخ الإستحقاق";
			this.layoutControlItem10.TextSize = new System.Drawing.Size(142, 13);
			// 
			// emptySpaceItem3
			// 
			this.emptySpaceItem3.AllowHotTrack = false;
			this.emptySpaceItem3.Location = new System.Drawing.Point(0, 56);
			this.emptySpaceItem3.Name = "emptySpaceItem3";
			this.emptySpaceItem3.Size = new System.Drawing.Size(357, 26);
			this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 28);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(164, 26);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// simpleSeparator3
			// 
			this.simpleSeparator3.AllowHotTrack = false;
			this.simpleSeparator3.Location = new System.Drawing.Point(351, 0);
			this.simpleSeparator3.Name = "simpleSeparator3";
			this.simpleSeparator3.Size = new System.Drawing.Size(2, 26);
			// 
			// simpleSeparator4
			// 
			this.simpleSeparator4.AllowHotTrack = false;
			this.simpleSeparator4.Location = new System.Drawing.Point(435, 28);
			this.simpleSeparator4.Name = "simpleSeparator4";
			this.simpleSeparator4.Size = new System.Drawing.Size(2, 26);
			// 
			// simpleSeparator7
			// 
			this.simpleSeparator7.AllowHotTrack = false;
			this.simpleSeparator7.Location = new System.Drawing.Point(0, 54);
			this.simpleSeparator7.Name = "simpleSeparator7";
			this.simpleSeparator7.Size = new System.Drawing.Size(708, 2);
			// 
			// simpleSeparator8
			// 
			this.simpleSeparator8.AllowHotTrack = false;
			this.simpleSeparator8.Location = new System.Drawing.Point(0, 26);
			this.simpleSeparator8.Name = "simpleSeparator8";
			this.simpleSeparator8.Size = new System.Drawing.Size(708, 2);
			// 
			// simpleSeparator9
			// 
			this.simpleSeparator9.AllowHotTrack = false;
			this.simpleSeparator9.Location = new System.Drawing.Point(0, 82);
			this.simpleSeparator9.Name = "simpleSeparator9";
			this.simpleSeparator9.Size = new System.Drawing.Size(708, 2);
			// 
			// lytVisaDetails
			// 
			this.lytVisaDetails.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.lytVisaDetails.AppearanceGroup.ForeColor = System.Drawing.Color.OldLace;
			this.lytVisaDetails.AppearanceGroup.Options.UseFont = true;
			this.lytVisaDetails.AppearanceGroup.Options.UseForeColor = true;
			this.lytVisaDetails.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem13,
            this.layoutControlItem14,
            this.layoutControlItem15,
            this.layoutControlItem16,
            this.emptySpaceItem7,
            this.simpleSeparator10,
            this.simpleSeparator11});
			this.lytVisaDetails.Location = new System.Drawing.Point(133, 410);
			this.lytVisaDetails.Name = "lytVisaDetails";
			this.lytVisaDetails.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytVisaDetails.Size = new System.Drawing.Size(710, 102);
			this.lytVisaDetails.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytVisaDetails.Text = "بيانات الفيزا";
			this.lytVisaDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
			// 
			// layoutControlItem13
			// 
			this.layoutControlItem13.Control = this.lkeBank_VisaPayment;
			this.layoutControlItem13.Location = new System.Drawing.Point(351, 0);
			this.layoutControlItem13.Name = "layoutControlItem13";
			this.layoutControlItem13.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem13.Size = new System.Drawing.Size(357, 26);
			this.layoutControlItem13.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem13.Text = "البنك";
			this.layoutControlItem13.TextSize = new System.Drawing.Size(142, 13);
			// 
			// layoutControlItem14
			// 
			this.layoutControlItem14.Control = this.lkeBankAccount_VisaPayment;
			this.layoutControlItem14.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem14.Name = "layoutControlItem14";
			this.layoutControlItem14.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem14.Size = new System.Drawing.Size(351, 26);
			this.layoutControlItem14.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem14.Text = "الحساب البنكي";
			this.layoutControlItem14.TextSize = new System.Drawing.Size(142, 13);
			// 
			// layoutControlItem15
			// 
			this.layoutControlItem15.Control = this.txtCreditCardNumber;
			this.layoutControlItem15.Location = new System.Drawing.Point(357, 28);
			this.layoutControlItem15.Name = "layoutControlItem15";
			this.layoutControlItem15.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem15.Size = new System.Drawing.Size(351, 26);
			this.layoutControlItem15.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem15.Text = "رقم الكارت";
			this.layoutControlItem15.TextSize = new System.Drawing.Size(142, 13);
			// 
			// layoutControlItem16
			// 
			this.layoutControlItem16.Control = this.txtVisaDescription;
			this.layoutControlItem16.Location = new System.Drawing.Point(0, 56);
			this.layoutControlItem16.Name = "layoutControlItem16";
			this.layoutControlItem16.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem16.Size = new System.Drawing.Size(708, 26);
			this.layoutControlItem16.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem16.Text = "ملاحظات على الفيزا";
			this.layoutControlItem16.TextSize = new System.Drawing.Size(142, 13);
			// 
			// emptySpaceItem7
			// 
			this.emptySpaceItem7.AllowHotTrack = false;
			this.emptySpaceItem7.Location = new System.Drawing.Point(0, 28);
			this.emptySpaceItem7.Name = "emptySpaceItem7";
			this.emptySpaceItem7.Size = new System.Drawing.Size(357, 26);
			this.emptySpaceItem7.TextSize = new System.Drawing.Size(0, 0);
			// 
			// simpleSeparator10
			// 
			this.simpleSeparator10.AllowHotTrack = false;
			this.simpleSeparator10.Location = new System.Drawing.Point(0, 26);
			this.simpleSeparator10.Name = "simpleSeparator10";
			this.simpleSeparator10.Size = new System.Drawing.Size(708, 2);
			// 
			// simpleSeparator11
			// 
			this.simpleSeparator11.AllowHotTrack = false;
			this.simpleSeparator11.Location = new System.Drawing.Point(0, 54);
			this.simpleSeparator11.Name = "simpleSeparator11";
			this.simpleSeparator11.Size = new System.Drawing.Size(708, 2);
			// 
			// layoutControlItem17
			// 
			this.layoutControlItem17.Control = this.txtInvoicePaymentDescription;
			this.layoutControlItem17.Location = new System.Drawing.Point(133, 512);
			this.layoutControlItem17.Name = "layoutControlItem17";
			this.layoutControlItem17.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem17.Size = new System.Drawing.Size(710, 26);
			this.layoutControlItem17.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem17.Text = "ملاحظات عامة على المدفوعات";
			this.layoutControlItem17.TextSize = new System.Drawing.Size(142, 13);
			// 
			// layoutControlGroup2
			// 
			this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem25});
			this.layoutControlGroup2.Location = new System.Drawing.Point(463, 98);
			this.layoutControlGroup2.Name = "layoutControlGroup2";
			this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup2.Size = new System.Drawing.Size(380, 38);
			this.layoutControlGroup2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup2.TextVisible = false;
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.Control = this.chkPayment;
			this.layoutControlItem3.Location = new System.Drawing.Point(252, 0);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem3.Size = new System.Drawing.Size(126, 36);
			this.layoutControlItem3.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			// 
			// layoutControlItem4
			// 
			this.layoutControlItem4.Control = this.chkRefund;
			this.layoutControlItem4.Location = new System.Drawing.Point(126, 0);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem4.Size = new System.Drawing.Size(126, 36);
			this.layoutControlItem4.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem4.TextVisible = false;
			// 
			// layoutControlItem25
			// 
			this.layoutControlItem25.Control = this.chkPatientDeposit;
			this.layoutControlItem25.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem25.Name = "layoutControlItem25";
			this.layoutControlItem25.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem25.Size = new System.Drawing.Size(126, 36);
			this.layoutControlItem25.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem25.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem25.TextVisible = false;
			// 
			// layoutControlGroup3
			// 
			this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem5,
            this.layoutControlItem18});
			this.layoutControlGroup3.Location = new System.Drawing.Point(143, 98);
			this.layoutControlGroup3.Name = "layoutControlGroup3";
			this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup3.Size = new System.Drawing.Size(320, 38);
			this.layoutControlGroup3.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup3.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.chkCashPayment;
			this.layoutControlItem1.Location = new System.Drawing.Point(212, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem1.Size = new System.Drawing.Size(106, 36);
			this.layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// layoutControlItem5
			// 
			this.layoutControlItem5.Control = this.chkCheckPayment;
			this.layoutControlItem5.Location = new System.Drawing.Point(106, 0);
			this.layoutControlItem5.Name = "layoutControlItem5";
			this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem5.Size = new System.Drawing.Size(106, 36);
			this.layoutControlItem5.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem5.TextVisible = false;
			// 
			// layoutControlItem18
			// 
			this.layoutControlItem18.Control = this.chkVisaPayment;
			this.layoutControlItem18.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem18.Name = "layoutControlItem18";
			this.layoutControlItem18.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem18.Size = new System.Drawing.Size(106, 36);
			this.layoutControlItem18.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem18.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem18.TextVisible = false;
			// 
			// layoutControlGroup4
			// 
			this.layoutControlGroup4.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.layoutControlGroup4.AppearanceGroup.ForeColor = System.Drawing.Color.OldLace;
			this.layoutControlGroup4.AppearanceGroup.Options.UseFont = true;
			this.layoutControlGroup4.AppearanceGroup.Options.UseForeColor = true;
			this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem21});
			this.layoutControlGroup4.Location = new System.Drawing.Point(0, 538);
			this.layoutControlGroup4.Name = "layoutControlGroup4";
			this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup4.Size = new System.Drawing.Size(1118, 112);
			this.layoutControlGroup4.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup4.Text = "المـدفـوعــــات السـابقــــــة";
			// 
			// layoutControlItem21
			// 
			this.layoutControlItem21.Control = this.grdPayments;
			this.layoutControlItem21.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem21.Name = "layoutControlItem21";
			this.layoutControlItem21.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem21.Size = new System.Drawing.Size(1116, 92);
			this.layoutControlItem21.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem21.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem21.TextVisible = false;
			// 
			// emptySpaceItem2
			// 
			this.emptySpaceItem2.AllowHotTrack = false;
			this.emptySpaceItem2.Location = new System.Drawing.Point(0, 98);
			this.emptySpaceItem2.Name = "emptySpaceItem2";
			this.emptySpaceItem2.Size = new System.Drawing.Size(133, 440);
			this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItem27
			// 
			this.layoutControlItem27.Control = this.patientTopTitle_UC1;
			this.layoutControlItem27.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem27.MaxSize = new System.Drawing.Size(0, 95);
			this.layoutControlItem27.MinSize = new System.Drawing.Size(534, 95);
			this.layoutControlItem27.Name = "layoutControlItem27";
			this.layoutControlItem27.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem27.Size = new System.Drawing.Size(1118, 95);
			this.layoutControlItem27.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem27.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem27.TextVisible = false;
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.lblTopSepertatorLabel;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 95);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem2.Size = new System.Drawing.Size(1118, 3);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// lytServiceChoosing
			// 
			this.lytServiceChoosing.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.lytServiceChoosing.AppearanceGroup.ForeColor = System.Drawing.Color.OldLace;
			this.lytServiceChoosing.AppearanceGroup.Options.UseFont = true;
			this.lytServiceChoosing.AppearanceGroup.Options.UseForeColor = true;
			this.lytServiceChoosing.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem26,
            this.layoutControlItem30,
            this.layoutControlItem31,
            this.layoutControlItem32,
            this.layoutControlItem33,
            this.emptySpaceItem4,
            this.emptySpaceItem9,
            this.emptySpaceItem10});
			this.lytServiceChoosing.Location = new System.Drawing.Point(133, 136);
			this.lytServiceChoosing.Name = "lytServiceChoosing";
			this.lytServiceChoosing.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytServiceChoosing.Size = new System.Drawing.Size(710, 108);
			this.lytServiceChoosing.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lytServiceChoosing.Text = "إختيــــار الخـدمـــــة المقـدمــــــة";
			this.lytServiceChoosing.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
			// 
			// layoutControlItem26
			// 
			this.layoutControlItem26.Control = this.chkExaminationServiceType;
			this.layoutControlItem26.Location = new System.Drawing.Point(602, 0);
			this.layoutControlItem26.Name = "layoutControlItem26";
			this.layoutControlItem26.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem26.Size = new System.Drawing.Size(106, 36);
			this.layoutControlItem26.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem26.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem26.TextVisible = false;
			// 
			// layoutControlItem30
			// 
			this.layoutControlItem30.Control = this.chkInvestigationServicetype;
			this.layoutControlItem30.Location = new System.Drawing.Point(496, 0);
			this.layoutControlItem30.Name = "layoutControlItem30";
			this.layoutControlItem30.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem30.Size = new System.Drawing.Size(106, 36);
			this.layoutControlItem30.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem30.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem30.TextVisible = false;
			// 
			// layoutControlItem31
			// 
			this.layoutControlItem31.Control = this.chkLabServiceType;
			this.layoutControlItem31.Location = new System.Drawing.Point(390, 0);
			this.layoutControlItem31.Name = "layoutControlItem31";
			this.layoutControlItem31.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem31.Size = new System.Drawing.Size(106, 36);
			this.layoutControlItem31.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem31.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem31.TextVisible = false;
			// 
			// layoutControlItem32
			// 
			this.layoutControlItem32.Control = this.lkeServiceCategory;
			this.layoutControlItem32.Location = new System.Drawing.Point(157, 36);
			this.layoutControlItem32.Name = "layoutControlItem32";
			this.layoutControlItem32.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem32.Size = new System.Drawing.Size(551, 26);
			this.layoutControlItem32.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem32.Text = "تصنيـــف الخـدمـــة";
			this.layoutControlItem32.TextSize = new System.Drawing.Size(142, 13);
			// 
			// layoutControlItem33
			// 
			this.layoutControlItem33.Control = this.lkeService;
			this.layoutControlItem33.Location = new System.Drawing.Point(157, 62);
			this.layoutControlItem33.Name = "layoutControlItem33";
			this.layoutControlItem33.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem33.Size = new System.Drawing.Size(551, 26);
			this.layoutControlItem33.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
			this.layoutControlItem33.Text = "الخـدمــــــة";
			this.layoutControlItem33.TextSize = new System.Drawing.Size(142, 13);
			// 
			// emptySpaceItem4
			// 
			this.emptySpaceItem4.AllowHotTrack = false;
			this.emptySpaceItem4.Location = new System.Drawing.Point(0, 0);
			this.emptySpaceItem4.Name = "emptySpaceItem4";
			this.emptySpaceItem4.Size = new System.Drawing.Size(390, 36);
			this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
			// 
			// emptySpaceItem9
			// 
			this.emptySpaceItem9.AllowHotTrack = false;
			this.emptySpaceItem9.Location = new System.Drawing.Point(0, 36);
			this.emptySpaceItem9.Name = "emptySpaceItem9";
			this.emptySpaceItem9.Size = new System.Drawing.Size(157, 26);
			this.emptySpaceItem9.TextSize = new System.Drawing.Size(0, 0);
			// 
			// emptySpaceItem10
			// 
			this.emptySpaceItem10.AllowHotTrack = false;
			this.emptySpaceItem10.Location = new System.Drawing.Point(0, 62);
			this.emptySpaceItem10.Name = "emptySpaceItem10";
			this.emptySpaceItem10.Size = new System.Drawing.Size(157, 26);
			this.emptySpaceItem10.TextSize = new System.Drawing.Size(0, 0);
			// 
			// InvoicePayment_UC
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.layoutControl1);
			this.MinimumSize = new System.Drawing.Size(0, 650);
			this.Name = "InvoicePayment_UC";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Size = new System.Drawing.Size(1118, 650);
			this.Load += new System.EventHandler(this.InvoicePayment_UC_Load);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.lkeService.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView7)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lkeServiceCategory.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnReaminder.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnTotalPayments.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnTotalRequired.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grdPayments)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnAmount.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtInvoicePaymentDescription.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtVisaDescription.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCreditCardNumber.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lkeBankAccount_VisaPayment.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lkeBank_VisaPayment.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCheckDescription.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCheckNumber.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtCheckExhcangeDate.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtCheckExhcangeDate.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtCheckIssueDate.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtCheckIssueDate.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lkeBankAccount_CheckPayment.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lkeBank_CheckPayment.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtPaymentSerial.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtPaymentDate.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtPaymentDate.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem29)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lytRemainderStatus)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lytIsPaymentEnough)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lytRemainder)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator16)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator17)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lytRequestedAmount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lytTotalPayments)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lyt)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lytCheckDetails)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator7)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator8)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator9)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lytVisaDetails)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator10)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleSeparator11)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lytServiceChoosing)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem30)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem31)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem32)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem33)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem10)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private PatientTopTitle_UC patientTopTitle_UC1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem27;
		private DevExpress.XtraEditors.TextEdit txtPaymentSerial;
		private DevExpress.XtraEditors.DateEdit dtPaymentDate;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem28;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem29;
		private System.Windows.Forms.Label lblTopSepertatorLabel;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraEditors.CheckButton chkRefund;
		private DevExpress.XtraEditors.CheckButton chkPayment;
		private DevExpress.XtraEditors.GridLookUpEdit lkeBankAccount_CheckPayment;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
		private DevExpress.XtraEditors.GridLookUpEdit lkeBank_CheckPayment;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
		private DevExpress.XtraEditors.DateEdit dtCheckExhcangeDate;
		private DevExpress.XtraEditors.DateEdit dtCheckIssueDate;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
		private DevExpress.XtraEditors.TextEdit txtCheckDescription;
		private DevExpress.XtraEditors.TextEdit txtCheckNumber;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
		private DevExpress.XtraEditors.GridLookUpEdit lkeBank_VisaPayment;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
		private DevExpress.XtraLayout.LayoutControlGroup lytCheckDetails;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
		private DevExpress.XtraEditors.GridLookUpEdit lkeBankAccount_VisaPayment;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
		private DevExpress.XtraEditors.TextEdit txtCreditCardNumber;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
		private DevExpress.XtraEditors.TextEdit txtVisaDescription;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
		private DevExpress.XtraEditors.TextEdit txtInvoicePaymentDescription;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
		private DevExpress.XtraLayout.LayoutControlGroup lytVisaDetails;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
		private DevExpress.XtraEditors.CheckButton chkVisaPayment;
		private DevExpress.XtraEditors.CheckButton chkCheckPayment;
		private DevExpress.XtraEditors.CheckButton chkCashPayment;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem8;
		private DevExpress.XtraEditors.LabelControl lblRemainderStatus;
		private DevExpress.XtraLayout.LayoutControlItem lytRemainderStatus;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
		private DevExpress.XtraEditors.SpinEdit spnAmount;
		private DevExpress.XtraEditors.CheckButton chkIsPaymentEnoght;
		private DevExpress.XtraLayout.LayoutControlItem lytIsPaymentEnough;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem7;
		private DevExpress.XtraLayout.LayoutControlItem lyt;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
		private DevExpress.XtraGrid.GridControl grdPayments;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem21;
		private DevExpress.XtraLayout.SimpleSeparator simpleSeparator16;
		private DevExpress.XtraLayout.SimpleSeparator simpleSeparator3;
		private DevExpress.XtraLayout.SimpleSeparator simpleSeparator4;
		private DevExpress.XtraLayout.SimpleSeparator simpleSeparator7;
		private DevExpress.XtraLayout.SimpleSeparator simpleSeparator8;
		private DevExpress.XtraLayout.SimpleSeparator simpleSeparator9;
		private DevExpress.XtraLayout.SimpleSeparator simpleSeparator10;
		private DevExpress.XtraLayout.SimpleSeparator simpleSeparator11;
		private DevExpress.XtraLayout.SimpleSeparator simpleSeparator17;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
		private DevExpress.XtraEditors.SpinEdit spnTotalRequired;
		private DevExpress.XtraLayout.LayoutControlItem lytRequestedAmount;
		private DevExpress.XtraEditors.SpinEdit spnTotalPayments;
		private DevExpress.XtraLayout.LayoutControlItem lytTotalPayments;
		private DevExpress.XtraEditors.SpinEdit spnReaminder;
		private DevExpress.XtraLayout.LayoutControlItem lytRemainder;
		private DevExpress.XtraEditors.CheckButton chkPatientDeposit;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem25;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
		private DevExpress.XtraEditors.CheckButton chkExaminationServiceType;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem26;
		private DevExpress.XtraEditors.CheckButton chkInvestigationServicetype;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem30;
		private DevExpress.XtraEditors.CheckButton chkLabServiceType;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem31;
		private DevExpress.XtraEditors.GridLookUpEdit lkeServiceCategory;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView6;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem32;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem9;
		private DevExpress.XtraEditors.GridLookUpEdit lkeService;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView7;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem33;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem10;
		private DevExpress.XtraLayout.LayoutControlGroup lytServiceChoosing;
	}
}
