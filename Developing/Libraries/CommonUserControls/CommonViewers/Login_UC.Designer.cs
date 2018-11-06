namespace CommonUserControls.CommonViewers
{
	partial class Login_UC
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
			this.txtUserName = new DevExpress.XtraEditors.TextEdit();
			this.txtPassword = new DevExpress.XtraEditors.TextEdit();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.pictureEdit4 = new DevExpress.XtraEditors.PictureEdit();
			this.pictureEdit3 = new DevExpress.XtraEditors.PictureEdit();
			this.btnExit = new DevExpress.XtraEditors.SimpleButton();
			this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
			this.pictureEdit2 = new DevExpress.XtraEditors.PictureEdit();
			this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
			this.lblApplicationTItle = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEdit4.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEdit3.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// txtUserName
			// 
			this.txtUserName.Location = new System.Drawing.Point(494, 185);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
			this.txtUserName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
			this.txtUserName.Properties.Appearance.Options.UseBackColor = true;
			this.txtUserName.Properties.Appearance.Options.UseFont = true;
			this.txtUserName.Size = new System.Drawing.Size(190, 22);
			this.txtUserName.TabIndex = 2;
			this.txtUserName.Click += new System.EventHandler(this.txtUserName_Click);
			this.txtUserName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserName_KeyPress);
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(494, 213);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
			this.txtPassword.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
			this.txtPassword.Properties.Appearance.Options.UseBackColor = true;
			this.txtPassword.Properties.Appearance.Options.UseFont = true;
			this.txtPassword.Properties.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(190, 22);
			this.txtPassword.TabIndex = 3;
			this.txtPassword.Click += new System.EventHandler(this.txtPassword_Click);
			this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
			// 
			// labelControl1
			// 
			this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
			this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.labelControl1.Location = new System.Drawing.Point(388, 188);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(68, 16);
			this.labelControl1.TabIndex = 4;
			this.labelControl1.Text = "User Name";
			// 
			// labelControl2
			// 
			this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
			this.labelControl2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.labelControl2.Location = new System.Drawing.Point(388, 216);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(63, 16);
			this.labelControl2.TabIndex = 5;
			this.labelControl2.Text = "Password";
			// 
			// pictureEdit4
			// 
			this.pictureEdit4.EditValue = global::CommonUserControls.Properties.Resources.icon2;
			this.pictureEdit4.Location = new System.Drawing.Point(476, 213);
			this.pictureEdit4.Name = "pictureEdit4";
			this.pictureEdit4.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
			this.pictureEdit4.Properties.Appearance.Options.UseBackColor = true;
			this.pictureEdit4.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pictureEdit4.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
			this.pictureEdit4.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
			this.pictureEdit4.Size = new System.Drawing.Size(18, 22);
			this.pictureEdit4.TabIndex = 9;
			// 
			// pictureEdit3
			// 
			this.pictureEdit3.EditValue = global::CommonUserControls.Properties.Resources.icon1;
			this.pictureEdit3.Location = new System.Drawing.Point(476, 185);
			this.pictureEdit3.Name = "pictureEdit3";
			this.pictureEdit3.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
			this.pictureEdit3.Properties.Appearance.Options.UseBackColor = true;
			this.pictureEdit3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pictureEdit3.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
			this.pictureEdit3.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
			this.pictureEdit3.Size = new System.Drawing.Size(18, 22);
			this.pictureEdit3.TabIndex = 8;
			// 
			// btnExit
			// 
			this.btnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
			this.btnExit.Appearance.Options.UseFont = true;
			this.btnExit.Image = global::CommonUserControls.Properties.Resources.ExitIcon_8;
			this.btnExit.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
			this.btnExit.Location = new System.Drawing.Point(388, 281);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(100, 30);
			this.btnExit.TabIndex = 7;
			this.btnExit.Text = "&Exit";
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// btnLogin
			// 
			this.btnLogin.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
			this.btnLogin.Appearance.Options.UseFont = true;
			this.btnLogin.Image = global::CommonUserControls.Properties.Resources.LoginIcon_8;
			this.btnLogin.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
			this.btnLogin.Location = new System.Drawing.Point(534, 281);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(150, 30);
			this.btnLogin.TabIndex = 6;
			this.btnLogin.Text = "&Login";
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// pictureEdit2
			// 
			this.pictureEdit2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pictureEdit2.EditValue = global::CommonUserControls.Properties.Resources.BottomLogin;
			this.pictureEdit2.Location = new System.Drawing.Point(0, 330);
			this.pictureEdit2.Name = "pictureEdit2";
			this.pictureEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pictureEdit2.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
			this.pictureEdit2.Size = new System.Drawing.Size(700, 70);
			this.pictureEdit2.TabIndex = 1;
			// 
			// pictureEdit1
			// 
			this.pictureEdit1.EditValue = global::CommonUserControls.Properties.Resources.TopLogin;
			this.pictureEdit1.Location = new System.Drawing.Point(0, 30);
			this.pictureEdit1.Name = "pictureEdit1";
			this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
			this.pictureEdit1.Size = new System.Drawing.Size(700, 105);
			this.pictureEdit1.TabIndex = 0;
			// 
			// lblApplicationTItle
			// 
			this.lblApplicationTItle.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
			this.lblApplicationTItle.Appearance.ForeColor = System.Drawing.Color.DarkOrange;
			this.lblApplicationTItle.LineVisible = true;
			this.lblApplicationTItle.Location = new System.Drawing.Point(388, 116);
			this.lblApplicationTItle.Name = "lblApplicationTItle";
			this.lblApplicationTItle.Size = new System.Drawing.Size(82, 19);
			this.lblApplicationTItle.TabIndex = 10;
			this.lblApplicationTItle.Text = "Reception";
			// 
			// Login_UC
			// 
			this.Appearance.BackColor = System.Drawing.Color.White;
			this.Appearance.Options.UseBackColor = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblApplicationTItle);
			this.Controls.Add(this.pictureEdit4);
			this.Controls.Add(this.pictureEdit3);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.btnLogin);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.labelControl1);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.txtUserName);
			this.Controls.Add(this.pictureEdit2);
			this.Controls.Add(this.pictureEdit1);
			this.LookAndFeel.SkinName = "Visual Studio 2013 Light";
			this.LookAndFeel.UseDefaultLookAndFeel = false;
			this.Name = "Login_UC";
			this.Size = new System.Drawing.Size(700, 400);
			((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEdit4.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEdit3.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.PictureEdit pictureEdit1;
		private DevExpress.XtraEditors.PictureEdit pictureEdit2;
		private DevExpress.XtraEditors.TextEdit txtUserName;
		private DevExpress.XtraEditors.TextEdit txtPassword;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraEditors.SimpleButton btnLogin;
		private DevExpress.XtraEditors.SimpleButton btnExit;
		private DevExpress.XtraEditors.PictureEdit pictureEdit3;
		private DevExpress.XtraEditors.PictureEdit pictureEdit4;
		private DevExpress.XtraEditors.LabelControl lblApplicationTItle;
	}
}
