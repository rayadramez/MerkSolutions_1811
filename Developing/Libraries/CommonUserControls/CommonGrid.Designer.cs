namespace CommonUserControls
{
	partial class CommonGrid
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
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.gridControl1);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(944, 492);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// gridControl1
			// 
			this.gridControl1.Location = new System.Drawing.Point(12, 12);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.gridControl1.Size = new System.Drawing.Size(920, 468);
			this.gridControl1.TabIndex = 0;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
			// 
			// gridView1
			// 
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn9,
            this.gridColumn4,
            this.gridColumn8,
            this.gridColumn7,
            this.gridColumn10,
            this.gridColumn20,
            this.gridColumn15,
            this.gridColumn17});
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsView.ShowAutoFilterRow = true;
			// 
			// gridColumn2
			// 
			this.gridColumn2.AppearanceCell.BackColor = System.Drawing.SystemColors.Info;
			this.gridColumn2.AppearanceCell.BackColor2 = System.Drawing.SystemColors.Info;
			this.gridColumn2.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn2.AppearanceHeader.BackColor = System.Drawing.SystemColors.Info;
			this.gridColumn2.AppearanceHeader.BackColor2 = System.Drawing.SystemColors.Info;
			this.gridColumn2.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn2.Caption = "الإسم العربي";
			this.gridColumn2.FieldName = "Name_P";
			this.gridColumn2.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.OptionsColumn.AllowEdit = false;
			this.gridColumn2.OptionsColumn.FixedWidth = true;
			this.gridColumn2.OptionsColumn.ReadOnly = true;
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 6;
			this.gridColumn2.Width = 200;
			// 
			// gridColumn5
			// 
			this.gridColumn5.AppearanceCell.BackColor = System.Drawing.SystemColors.Info;
			this.gridColumn5.AppearanceCell.BackColor2 = System.Drawing.SystemColors.Info;
			this.gridColumn5.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn5.AppearanceHeader.BackColor = System.Drawing.SystemColors.Info;
			this.gridColumn5.AppearanceHeader.BackColor2 = System.Drawing.SystemColors.Info;
			this.gridColumn5.AppearanceHeader.Options.UseBackColor = true;
			this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn5.Caption = "الإسم الإنجليزي";
			this.gridColumn5.FieldName = "Name_S";
			this.gridColumn5.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
			this.gridColumn5.Name = "gridColumn5";
			this.gridColumn5.OptionsColumn.AllowEdit = false;
			this.gridColumn5.OptionsColumn.FixedWidth = true;
			this.gridColumn5.OptionsColumn.ReadOnly = true;
			this.gridColumn5.Visible = true;
			this.gridColumn5.VisibleIndex = 5;
			this.gridColumn5.Width = 200;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Size = new System.Drawing.Size(944, 492);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.gridControl1;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(924, 472);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// gridColumn6
			// 
			this.gridColumn6.Caption = "الكود الداخلي";
			this.gridColumn6.FieldName = "InternalCode";
			this.gridColumn6.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
			this.gridColumn6.Name = "gridColumn6";
			this.gridColumn6.OptionsColumn.AllowEdit = false;
			this.gridColumn6.OptionsColumn.FixedWidth = true;
			this.gridColumn6.OptionsColumn.ReadOnly = true;
			this.gridColumn6.Visible = true;
			this.gridColumn6.VisibleIndex = 4;
			this.gridColumn6.Width = 28;
			// 
			// gridColumn9
			// 
			this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn9.Caption = "النــــوع";
			this.gridColumn9.FieldName = "RawTypeName";
			this.gridColumn9.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
			this.gridColumn9.Name = "gridColumn9";
			this.gridColumn9.OptionsColumn.AllowEdit = false;
			this.gridColumn9.OptionsColumn.FixedWidth = true;
			this.gridColumn9.OptionsColumn.ReadOnly = true;
			this.gridColumn9.Visible = true;
			this.gridColumn9.VisibleIndex = 3;
			this.gridColumn9.Width = 150;
			// 
			// gridColumn4
			// 
			this.gridColumn4.Caption = "العرض";
			this.gridColumn4.FieldName = "Width";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.OptionsColumn.AllowEdit = false;
			this.gridColumn4.OptionsColumn.ReadOnly = true;
			this.gridColumn4.Width = 20;
			// 
			// gridColumn8
			// 
			this.gridColumn8.Caption = "الطول";
			this.gridColumn8.FieldName = "Height";
			this.gridColumn8.Name = "gridColumn8";
			this.gridColumn8.OptionsColumn.AllowEdit = false;
			this.gridColumn8.OptionsColumn.ReadOnly = true;
			this.gridColumn8.Visible = true;
			this.gridColumn8.VisibleIndex = 0;
			this.gridColumn8.Width = 27;
			// 
			// gridColumn7
			// 
			this.gridColumn7.Caption = "الوزن";
			this.gridColumn7.FieldName = "Weight";
			this.gridColumn7.Name = "gridColumn7";
			this.gridColumn7.OptionsColumn.AllowEdit = false;
			this.gridColumn7.OptionsColumn.ReadOnly = true;
			this.gridColumn7.Visible = true;
			this.gridColumn7.VisibleIndex = 1;
			this.gridColumn7.Width = 20;
			// 
			// gridColumn10
			// 
			this.gridColumn10.Caption = "السمك";
			this.gridColumn10.FieldName = "Thickness";
			this.gridColumn10.Name = "gridColumn10";
			this.gridColumn10.OptionsColumn.AllowEdit = false;
			this.gridColumn10.OptionsColumn.ReadOnly = true;
			this.gridColumn10.Visible = true;
			this.gridColumn10.VisibleIndex = 2;
			this.gridColumn10.Width = 20;
			// 
			// gridColumn15
			// 
			this.gridColumn15.Caption = "متوفر؟";
			this.gridColumn15.FieldName = "IsStockAvailable";
			this.gridColumn15.Name = "gridColumn15";
			this.gridColumn15.OptionsColumn.AllowEdit = false;
			this.gridColumn15.OptionsColumn.ReadOnly = true;
			this.gridColumn15.Width = 20;
			// 
			// gridColumn17
			// 
			this.gridColumn17.Caption = "العد؟";
			this.gridColumn17.FieldName = "IsCountable";
			this.gridColumn17.Name = "gridColumn17";
			this.gridColumn17.OptionsColumn.AllowEdit = false;
			this.gridColumn17.OptionsColumn.ReadOnly = true;
			this.gridColumn17.Width = 20;
			// 
			// gridColumn20
			// 
			this.gridColumn20.Caption = "ت. الصلاحية";
			this.gridColumn20.FieldName = "ExpirationDate";
			this.gridColumn20.Name = "gridColumn20";
			this.gridColumn20.OptionsColumn.AllowEdit = false;
			this.gridColumn20.OptionsColumn.ReadOnly = true;
			this.gridColumn20.Width = 20;
			// 
			// CommonGrid
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.layoutControl1);
			this.Name = "CommonGrid";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Size = new System.Drawing.Size(944, 492);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraGrid.GridControl gridControl1;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
	}
}
