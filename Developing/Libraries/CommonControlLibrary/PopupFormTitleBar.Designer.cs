namespace CommonControlLibrary
{
	partial class PopupFormTitleBar
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupFormTitleBar));
			this.btnEnlarge = new DevExpress.XtraEditors.SimpleButton();
			this.btnClose = new DevExpress.XtraEditors.SimpleButton();
			this.SuspendLayout();
			// 
			// btnEnlarge
			// 
			this.btnEnlarge.Image = global::CommonControlLibrary.Properties.Resources._4_FullScreen1_16;
			this.btnEnlarge.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			resources.ApplyResources(this.btnEnlarge, "btnEnlarge");
			this.btnEnlarge.Name = "btnEnlarge";
			this.btnEnlarge.Click += new System.EventHandler(this.btnEnlarge_Click);
			// 
			// btnClose
			// 
			this.btnClose.Image = global::CommonControlLibrary.Properties.Resources._1_ExitIcon_16;
			this.btnClose.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			resources.ApplyResources(this.btnClose, "btnClose");
			this.btnClose.Name = "btnClose";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// PopupFormTitleBar
			// 
			this.Appearance.BackColor = ((System.Drawing.Color)(resources.GetObject("PopupFormTitleBar.Appearance.BackColor")));
			this.Appearance.Options.UseBackColor = true;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnEnlarge);
			this.Name = "PopupFormTitleBar";
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.SimpleButton btnEnlarge;
		private DevExpress.XtraEditors.SimpleButton btnClose;

	}
}
