namespace MainProject
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.navbarImageCollectionLarge = new DevExpress.Utils.ImageCollection(this.components);
			this.navbarImageCollection = new DevExpress.Utils.ImageCollection(this.components);
			this.mainRibbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
			this.appMenu = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
			this.ribbonImageCollection = new DevExpress.Utils.ImageCollection(this.components);
			this.iHelp = new DevExpress.XtraBars.BarButtonItem();
			this.siStatus = new DevExpress.XtraBars.BarStaticItem();
			this.siInfo = new DevExpress.XtraBars.BarStaticItem();
			this.iBoldFontStyle = new DevExpress.XtraBars.BarButtonItem();
			this.iItalicFontStyle = new DevExpress.XtraBars.BarButtonItem();
			this.iUnderlinedFontStyle = new DevExpress.XtraBars.BarButtonItem();
			this.iLeftTextAlign = new DevExpress.XtraBars.BarButtonItem();
			this.iCenterTextAlign = new DevExpress.XtraBars.BarButtonItem();
			this.iRightTextAlign = new DevExpress.XtraBars.BarButtonItem();
			this.rgbiSkins = new DevExpress.XtraBars.RibbonGalleryBarItem();
			this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
			this.btnChartOfAccountType_Search = new DevExpress.XtraBars.BarButtonItem();
			this.btnChartOfAccountType_New = new DevExpress.XtraBars.BarButtonItem();
			this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
			this.btnChartOfAccountMargin_Search = new DevExpress.XtraBars.BarButtonItem();
			this.btnChartOfAccountMargin_Edit = new DevExpress.XtraBars.BarButtonItem();
			this.ribbonImageCollectionLarge = new DevExpress.Utils.ImageCollection(this.components);
			this.homeRibbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
			this.exitRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			this.helpRibbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
			this.helpRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
			this.btnEmployeeMenu = new DevExpress.XtraBars.BarSubItem();
			this.btnEmployee_Search = new DevExpress.XtraBars.BarButtonItem();
			this.btn = new DevExpress.XtraBars.BarButtonItem();
			this.btnEmployee_New = new DevExpress.XtraBars.BarButtonItem();
			((System.ComponentModel.ISupportInitialize)(this.navbarImageCollectionLarge)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.navbarImageCollection)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mainRibbon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.appMenu)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ribbonImageCollection)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ribbonImageCollectionLarge)).BeginInit();
			this.SuspendLayout();
			// 
			// navbarImageCollectionLarge
			// 
			this.navbarImageCollectionLarge.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("navbarImageCollectionLarge.ImageStream")));
			resources.ApplyResources(this.navbarImageCollectionLarge, "navbarImageCollectionLarge");
			this.navbarImageCollectionLarge.Images.SetKeyName(0, "Mail_16x16.png");
			this.navbarImageCollectionLarge.Images.SetKeyName(1, "Organizer_16x16.png");
			// 
			// navbarImageCollection
			// 
			this.navbarImageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("navbarImageCollection.ImageStream")));
			resources.ApplyResources(this.navbarImageCollection, "navbarImageCollection");
			this.navbarImageCollection.Images.SetKeyName(0, "Inbox_16x16.png");
			this.navbarImageCollection.Images.SetKeyName(1, "Outbox_16x16.png");
			this.navbarImageCollection.Images.SetKeyName(2, "Drafts_16x16.png");
			this.navbarImageCollection.Images.SetKeyName(3, "Trash_16x16.png");
			this.navbarImageCollection.Images.SetKeyName(4, "Calendar_16x16.png");
			this.navbarImageCollection.Images.SetKeyName(5, "Tasks_16x16.png");
			// 
			// mainRibbon
			// 
			this.mainRibbon.ApplicationButtonDropDownControl = this.appMenu;
			resources.ApplyResources(this.mainRibbon, "mainRibbon");
			this.mainRibbon.ColorScheme = DevExpress.XtraBars.Ribbon.RibbonControlColorScheme.Orange;
			this.mainRibbon.DrawGroupCaptions = DevExpress.Utils.DefaultBoolean.True;
			this.mainRibbon.ExpandCollapseItem.Id = 0;
			this.mainRibbon.Images = this.ribbonImageCollection;
			this.mainRibbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.mainRibbon.ExpandCollapseItem,
            this.iHelp,
            this.siStatus,
            this.siInfo,
            this.iBoldFontStyle,
            this.iItalicFontStyle,
            this.iUnderlinedFontStyle,
            this.iLeftTextAlign,
            this.iCenterTextAlign,
            this.iRightTextAlign,
            this.rgbiSkins,
            this.barSubItem1,
            this.btnChartOfAccountType_Search,
            this.btnChartOfAccountType_New,
            this.barSubItem2,
            this.btnChartOfAccountMargin_Search,
            this.btnChartOfAccountMargin_Edit,
            this.btnEmployeeMenu,
            this.btnEmployee_Search,
            this.btn,
            this.btnEmployee_New});
			this.mainRibbon.LargeImages = this.ribbonImageCollectionLarge;
			this.mainRibbon.MaxItemId = 76;
			this.mainRibbon.Name = "mainRibbon";
			this.mainRibbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.homeRibbonPage,
            this.helpRibbonPage,
            this.ribbonPage1});
			this.mainRibbon.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
			this.mainRibbon.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
			this.mainRibbon.ShowCategoryInCaption = false;
			this.mainRibbon.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
			this.mainRibbon.ShowFullScreenButton = DevExpress.Utils.DefaultBoolean.False;
			this.mainRibbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Show;
			this.mainRibbon.ShowQatLocationSelector = false;
			this.mainRibbon.ShowToolbarCustomizeItem = false;
			this.mainRibbon.Toolbar.ItemLinks.Add(this.iHelp);
			this.mainRibbon.Toolbar.ShowCustomizeItem = false;
			this.mainRibbon.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
			// 
			// appMenu
			// 
			this.appMenu.Name = "appMenu";
			this.appMenu.Ribbon = this.mainRibbon;
			this.appMenu.ShowRightPane = true;
			// 
			// ribbonImageCollection
			// 
			this.ribbonImageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("ribbonImageCollection.ImageStream")));
			this.ribbonImageCollection.Images.SetKeyName(0, "Ribbon_New_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(1, "Ribbon_Open_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(2, "Ribbon_Close_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(3, "Ribbon_Find_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(4, "Ribbon_Save_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(5, "Ribbon_SaveAs_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(6, "Ribbon_Exit_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(7, "Ribbon_Content_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(8, "Ribbon_Info_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(9, "Ribbon_Bold_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(10, "Ribbon_Italic_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(11, "Ribbon_Underline_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(12, "Ribbon_AlignLeft_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(13, "Ribbon_AlignCenter_16x16.png");
			this.ribbonImageCollection.Images.SetKeyName(14, "Ribbon_AlignRight_16x16.png");
			// 
			// iHelp
			// 
			resources.ApplyResources(this.iHelp, "iHelp");
			this.iHelp.Id = 22;
			this.iHelp.ImageIndex = 7;
			this.iHelp.LargeImageIndex = 7;
			this.iHelp.Name = "iHelp";
			// 
			// siStatus
			// 
			resources.ApplyResources(this.siStatus, "siStatus");
			this.siStatus.Id = 31;
			this.siStatus.Name = "siStatus";
			this.siStatus.TextAlignment = System.Drawing.StringAlignment.Near;
			// 
			// siInfo
			// 
			resources.ApplyResources(this.siInfo, "siInfo");
			this.siInfo.Id = 32;
			this.siInfo.Name = "siInfo";
			this.siInfo.TextAlignment = System.Drawing.StringAlignment.Near;
			// 
			// iBoldFontStyle
			// 
			resources.ApplyResources(this.iBoldFontStyle, "iBoldFontStyle");
			this.iBoldFontStyle.Id = 53;
			this.iBoldFontStyle.ImageIndex = 9;
			this.iBoldFontStyle.Name = "iBoldFontStyle";
			// 
			// iItalicFontStyle
			// 
			resources.ApplyResources(this.iItalicFontStyle, "iItalicFontStyle");
			this.iItalicFontStyle.Id = 54;
			this.iItalicFontStyle.ImageIndex = 10;
			this.iItalicFontStyle.Name = "iItalicFontStyle";
			// 
			// iUnderlinedFontStyle
			// 
			resources.ApplyResources(this.iUnderlinedFontStyle, "iUnderlinedFontStyle");
			this.iUnderlinedFontStyle.Id = 55;
			this.iUnderlinedFontStyle.ImageIndex = 11;
			this.iUnderlinedFontStyle.Name = "iUnderlinedFontStyle";
			// 
			// iLeftTextAlign
			// 
			resources.ApplyResources(this.iLeftTextAlign, "iLeftTextAlign");
			this.iLeftTextAlign.Id = 57;
			this.iLeftTextAlign.ImageIndex = 12;
			this.iLeftTextAlign.Name = "iLeftTextAlign";
			// 
			// iCenterTextAlign
			// 
			resources.ApplyResources(this.iCenterTextAlign, "iCenterTextAlign");
			this.iCenterTextAlign.Id = 58;
			this.iCenterTextAlign.ImageIndex = 13;
			this.iCenterTextAlign.Name = "iCenterTextAlign";
			// 
			// iRightTextAlign
			// 
			resources.ApplyResources(this.iRightTextAlign, "iRightTextAlign");
			this.iRightTextAlign.Id = 59;
			this.iRightTextAlign.ImageIndex = 14;
			this.iRightTextAlign.Name = "iRightTextAlign";
			// 
			// rgbiSkins
			// 
			resources.ApplyResources(this.rgbiSkins, "rgbiSkins");
			// 
			// 
			// 
			this.rgbiSkins.Gallery.AllowHoverImages = true;
			this.rgbiSkins.Gallery.Appearance.ItemCaptionAppearance.Normal.Options.UseFont = true;
			this.rgbiSkins.Gallery.Appearance.ItemCaptionAppearance.Normal.Options.UseTextOptions = true;
			this.rgbiSkins.Gallery.Appearance.ItemCaptionAppearance.Normal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.rgbiSkins.Gallery.ColumnCount = 4;
			this.rgbiSkins.Gallery.FixedHoverImageSize = false;
			this.rgbiSkins.Gallery.ImageSize = new System.Drawing.Size(32, 17);
			this.rgbiSkins.Gallery.ItemImageLocation = DevExpress.Utils.Locations.Top;
			this.rgbiSkins.Gallery.RowCount = 4;
			this.rgbiSkins.Id = 60;
			this.rgbiSkins.Name = "rgbiSkins";
			// 
			// barSubItem1
			// 
			resources.ApplyResources(this.barSubItem1, "barSubItem1");
			this.barSubItem1.Id = 66;
			this.barSubItem1.LargeImageIndex = 7;
			this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnChartOfAccountType_Search),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnChartOfAccountType_New)});
			this.barSubItem1.Name = "barSubItem1";
			this.barSubItem1.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
			// 
			// btnChartOfAccountType_Search
			// 
			resources.ApplyResources(this.btnChartOfAccountType_Search, "btnChartOfAccountType_Search");
			this.btnChartOfAccountType_Search.Glyph = global::MainProject.Properties.Resources.Search_1_24;
			this.btnChartOfAccountType_Search.Id = 67;
			this.btnChartOfAccountType_Search.LargeImageIndex = 7;
			this.btnChartOfAccountType_Search.Name = "btnChartOfAccountType_Search";
			this.btnChartOfAccountType_Search.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
			this.btnChartOfAccountType_Search.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnChartOfAccountType_Search_ItemClick);
			// 
			// btnChartOfAccountType_New
			// 
			resources.ApplyResources(this.btnChartOfAccountType_New, "btnChartOfAccountType_New");
			this.btnChartOfAccountType_New.Glyph = global::MainProject.Properties.Resources.New_1_24;
			this.btnChartOfAccountType_New.Id = 68;
			this.btnChartOfAccountType_New.Name = "btnChartOfAccountType_New";
			this.btnChartOfAccountType_New.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnChartOfAccountType_New_ItemClick);
			// 
			// barSubItem2
			// 
			resources.ApplyResources(this.barSubItem2, "barSubItem2");
			this.barSubItem2.Id = 69;
			this.barSubItem2.LargeImageIndex = 7;
			this.barSubItem2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnChartOfAccountMargin_Search),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnChartOfAccountMargin_Edit)});
			this.barSubItem2.Name = "barSubItem2";
			this.barSubItem2.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
			// 
			// btnChartOfAccountMargin_Search
			// 
			resources.ApplyResources(this.btnChartOfAccountMargin_Search, "btnChartOfAccountMargin_Search");
			this.btnChartOfAccountMargin_Search.Glyph = global::MainProject.Properties.Resources.Search_1_24;
			this.btnChartOfAccountMargin_Search.Id = 70;
			this.btnChartOfAccountMargin_Search.Name = "btnChartOfAccountMargin_Search";
			this.btnChartOfAccountMargin_Search.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnChartOfAccountMargin_Search_ItemClick);
			// 
			// btnChartOfAccountMargin_Edit
			// 
			resources.ApplyResources(this.btnChartOfAccountMargin_Edit, "btnChartOfAccountMargin_Edit");
			this.btnChartOfAccountMargin_Edit.Glyph = global::MainProject.Properties.Resources.New_1_24;
			this.btnChartOfAccountMargin_Edit.Id = 71;
			this.btnChartOfAccountMargin_Edit.Name = "btnChartOfAccountMargin_Edit";
			this.btnChartOfAccountMargin_Edit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnChartOfAccountMargin_Edit_ItemClick);
			// 
			// ribbonImageCollectionLarge
			// 
			resources.ApplyResources(this.ribbonImageCollectionLarge, "ribbonImageCollectionLarge");
			this.ribbonImageCollectionLarge.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("ribbonImageCollectionLarge.ImageStream")));
			this.ribbonImageCollectionLarge.Images.SetKeyName(0, "Ribbon_New_32x32.png");
			this.ribbonImageCollectionLarge.Images.SetKeyName(1, "Ribbon_Open_32x32.png");
			this.ribbonImageCollectionLarge.Images.SetKeyName(2, "Ribbon_Close_32x32.png");
			this.ribbonImageCollectionLarge.Images.SetKeyName(3, "Ribbon_Find_32x32.png");
			this.ribbonImageCollectionLarge.Images.SetKeyName(4, "Ribbon_Save_32x32.png");
			this.ribbonImageCollectionLarge.Images.SetKeyName(5, "Ribbon_SaveAs_32x32.png");
			this.ribbonImageCollectionLarge.Images.SetKeyName(6, "Ribbon_Exit_32x32.png");
			this.ribbonImageCollectionLarge.Images.SetKeyName(7, "Ribbon_Content_32x32.png");
			this.ribbonImageCollectionLarge.Images.SetKeyName(8, "Ribbon_Info_32x32.png");
			// 
			// homeRibbonPage
			// 
			this.homeRibbonPage.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("homeRibbonPage.Appearance.Font")));
			this.homeRibbonPage.Appearance.Options.UseFont = true;
			this.homeRibbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.exitRibbonPageGroup});
			this.homeRibbonPage.Name = "homeRibbonPage";
			resources.ApplyResources(this.homeRibbonPage, "homeRibbonPage");
			// 
			// exitRibbonPageGroup
			// 
			this.exitRibbonPageGroup.ItemLinks.Add(this.barSubItem1);
			this.exitRibbonPageGroup.ItemLinks.Add(this.barSubItem2);
			this.exitRibbonPageGroup.Name = "exitRibbonPageGroup";
			resources.ApplyResources(this.exitRibbonPageGroup, "exitRibbonPageGroup");
			// 
			// ribbonPageGroup1
			// 
			this.ribbonPageGroup1.ItemLinks.Add(this.btnEmployeeMenu);
			this.ribbonPageGroup1.Name = "ribbonPageGroup1";
			resources.ApplyResources(this.ribbonPageGroup1, "ribbonPageGroup1");
			// 
			// helpRibbonPage
			// 
			this.helpRibbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.helpRibbonPageGroup});
			this.helpRibbonPage.Name = "helpRibbonPage";
			resources.ApplyResources(this.helpRibbonPage, "helpRibbonPage");
			// 
			// helpRibbonPageGroup
			// 
			this.helpRibbonPageGroup.ItemLinks.Add(this.iHelp);
			this.helpRibbonPageGroup.Name = "helpRibbonPageGroup";
			resources.ApplyResources(this.helpRibbonPageGroup, "helpRibbonPageGroup");
			// 
			// ribbonPage1
			// 
			this.ribbonPage1.Name = "ribbonPage1";
			resources.ApplyResources(this.ribbonPage1, "ribbonPage1");
			// 
			// btnEmployeeMenu
			// 
			resources.ApplyResources(this.btnEmployeeMenu, "btnEmployeeMenu");
			this.btnEmployeeMenu.Id = 72;
			this.btnEmployeeMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEmployee_Search),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEmployee_New)});
			this.btnEmployeeMenu.Name = "btnEmployeeMenu";
			// 
			// btnEmployee_Search
			// 
			resources.ApplyResources(this.btnEmployee_Search, "btnEmployee_Search");
			this.btnEmployee_Search.Id = 73;
			this.btnEmployee_Search.Name = "btnEmployee_Search";
			this.btnEmployee_Search.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEmployee_Search_ItemClick);
			// 
			// btn
			// 
			resources.ApplyResources(this.btn, "btn");
			this.btn.Id = 74;
			this.btn.Name = "btn";
			// 
			// btnEmployee_New
			// 
			resources.ApplyResources(this.btnEmployee_New, "btnEmployee_New");
			this.btnEmployee_New.Id = 75;
			this.btnEmployee_New.Name = "btnEmployee_New";
			this.btnEmployee_New.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEmployee_New_ItemClick);
			// 
			// MainForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.mainRibbon);
			this.LookAndFeel.SkinName = "McSkin";
			this.LookAndFeel.UseDefaultLookAndFeel = false;
			this.Name = "MainForm";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			((System.ComponentModel.ISupportInitialize)(this.navbarImageCollectionLarge)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.navbarImageCollection)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mainRibbon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.appMenu)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ribbonImageCollection)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ribbonImageCollectionLarge)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraBars.Ribbon.RibbonControl mainRibbon;
		private DevExpress.XtraBars.BarButtonItem iHelp;
		private DevExpress.XtraBars.BarStaticItem siStatus;
		private DevExpress.XtraBars.BarStaticItem siInfo;
		private DevExpress.XtraBars.BarButtonItem iBoldFontStyle;
		private DevExpress.XtraBars.BarButtonItem iItalicFontStyle;
		private DevExpress.XtraBars.BarButtonItem iUnderlinedFontStyle;
		private DevExpress.XtraBars.BarButtonItem iLeftTextAlign;
		private DevExpress.XtraBars.BarButtonItem iCenterTextAlign;
		private DevExpress.XtraBars.BarButtonItem iRightTextAlign;
		private DevExpress.XtraBars.RibbonGalleryBarItem rgbiSkins;
		private DevExpress.XtraBars.Ribbon.RibbonPage homeRibbonPage;
		private DevExpress.XtraBars.Ribbon.RibbonPageGroup exitRibbonPageGroup;
		private DevExpress.XtraBars.Ribbon.RibbonPage helpRibbonPage;
		private DevExpress.XtraBars.Ribbon.RibbonPageGroup helpRibbonPageGroup;
		private DevExpress.Utils.ImageCollection ribbonImageCollection;
		private DevExpress.Utils.ImageCollection ribbonImageCollectionLarge;
		private DevExpress.Utils.ImageCollection navbarImageCollection;
		private DevExpress.Utils.ImageCollection navbarImageCollectionLarge;
		private DevExpress.XtraBars.Ribbon.ApplicationMenu appMenu;
		private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
		private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
		private DevExpress.XtraBars.BarSubItem barSubItem1;
		private DevExpress.XtraBars.BarButtonItem btnChartOfAccountType_Search;
		private DevExpress.XtraBars.BarButtonItem btnChartOfAccountType_New;
		private DevExpress.XtraBars.BarSubItem barSubItem2;
		private DevExpress.XtraBars.BarButtonItem btnChartOfAccountMargin_Search;
		private DevExpress.XtraBars.BarButtonItem btnChartOfAccountMargin_Edit;
		private DevExpress.XtraBars.BarSubItem btnEmployeeMenu;
		private DevExpress.XtraBars.BarButtonItem btnEmployee_Search;
		private DevExpress.XtraBars.BarButtonItem btnEmployee_New;
		private DevExpress.XtraBars.BarButtonItem btn;

	}
}
