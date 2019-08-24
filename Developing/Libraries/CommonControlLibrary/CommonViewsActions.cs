using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary.ControlsConstructors;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraLayout;
using DevExpress.XtraTab;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace CommonControlLibrary
{
	public enum LanguageCulture
	{
		Arabic = 1,
		English = 2
	}

	public enum MVCViewerType
	{
		None = 0,
		SearchVewier = 1,
		EditorViewer = 2,
		ReportVeiwer = 3
	}

	public class CommonViewsActions
	{
		public const String ArabicCulture = "ar-EG";
		public const String EnglishCulture = "en-US";

		#region Style Controllers

		private static StyleController _defaultStyle;
		private static StyleController _simpleButtonStyle;
		private static StyleController _layouttStyle;
		private static StyleController _headerStyle;
		private static StyleController _gridControlCellStyle;
		private static StyleController _gridControlHeaderStyle;

		public static bool IsArbicCulture
		{
			get { return GetCurrentCulture() == LanguageCulture.Arabic; }
		}

		public static Font DefaultFont
		{
			get { return new Font("Segoe UI", 8, FontStyle.Bold); }
		}

		public static Font SimpleButtonFont
		{
			get { return new Font("Segoe UI", 9, FontStyle.Bold); }
		}

		public static Font LayoutFont
		{
			get { return new Font("Segoe UI", 8, FontStyle.Bold); }
		}

		public static Font HeaderFont
		{
			get { return new Font("Segoe UI", 10, FontStyle.Bold); }
		}

		public static Font GridControlCellFont
		{
			get { return new Font("Segoe UI", 8, FontStyle.Bold); }
		}

		public static Font GridControlHeaderFont
		{
			get { return new Font("Segoe UI", 9, FontStyle.Bold); }
		}

		public static StyleController SimpleButtonStyleController
		{
			get
			{
				StyleController simpleButtonStyle = InitializeStyleController(_simpleButtonStyle, SimpleButtonFont, 9, true, Color.White);
				return simpleButtonStyle;
			}
		}

		public static StyleController DefaultStyleController
		{
			get
			{
				StyleController defaultStyle = InitializeStyleController(_defaultStyle, DefaultFont, 8, true, Color.MidnightBlue);
				return defaultStyle;
			}
		}

		public static StyleController LayoutStyleController
		{
			get
			{
				StyleController layouttStyle = InitializeStyleController(_layouttStyle, LayoutFont, 8, true, Color.White);
				return layouttStyle;
			}
		}

		public static StyleController HeaderStyleController
		{
			get
			{
				StyleController headerStyle = InitializeStyleController(_headerStyle, HeaderFont, 10, true, Color.Beige);
				return headerStyle;
			}
		}

		public static StyleController GridControlCellStyleController
		{
			get
			{
				StyleController gridControlCellStyle = InitializeStyleController(_gridControlCellStyle, GridControlCellFont, 9, true, Color.White);
				return gridControlCellStyle;
			}
		}

		public static StyleController GridControlHeaderStyleController
		{
			get
			{
				StyleController gridControlHeaderStyle = InitializeStyleController(_gridControlHeaderStyle, GridControlHeaderFont, 9, true, Color.Aqua);
				return gridControlHeaderStyle;
			}
		}

		public static StyleController InitializeStyleController(StyleController styleController, Font font, float size, bool isBold, Color foreColor)
		{
			StyleController style = styleController;
			Font fontToUse = isBold
				? new Font(font.FontFamily, size, FontStyle.Bold)
				: new Font(font.FontFamily, size);

			if (style == null)
				style = new StyleController();

			style.Appearance.Font = fontToUse;
			style.Appearance.ForeColor = foreColor;

			style.AppearanceDisabled.Font = fontToUse;
			//styleController.AppearanceDisabled.ForeColor = foreColor;

			style.AppearanceFocused.Font = fontToUse;
			//styleController.AppearanceFocused.ForeColor = foreColor;

			style.AppearanceReadOnly.Font = fontToUse;
			//styleController.AppearanceReadOnly.ForeColor = foreColor;

			style.AppearanceDropDown.Font = fontToUse;
			//styleController.AppearanceDropDown.ForeColor = foreColor;

			return style;
		}

		#endregion

		public static void SetCulture(String culture)
		{
			CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(culture);
			CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
			Thread.CurrentThread.CurrentUICulture = cultureInfo;
			CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
			Thread.CurrentThread.CurrentCulture = cultureInfo;
		}

		public static LanguageCulture GetCurrentCulture()
		{
			return Thread.CurrentThread.CurrentUICulture.TextInfo.IsRightToLeft
				? LanguageCulture.Arabic
				: LanguageCulture.English;
		}

		public static void ShowUserControl<TControl>(ref TControl viewerControl, Control parentControl,
			bool forceCreateNew = false, bool isVisible = true, DockStyle dockStyle = DockStyle.Fill)
			where TControl : Control, new()
		{
			if (viewerControl == null || viewerControl.IsDisposed || forceCreateNew)
				viewerControl = new TControl();

			if (!parentControl.Controls.Contains(viewerControl))
			{
				viewerControl.Dock = dockStyle;
				parentControl.Controls.Add(viewerControl);
				SetEnterMoveNextControl(viewerControl, true);
			}

			viewerControl.Visible = isVisible;
			viewerControl.BringToFront();
		}

		public static void SetEnterMoveNextControl(Control control, bool value)
		{
			if (control == null) return;

			if (control is BaseEdit)
				((BaseEdit)control).EnterMoveNextControl = value;

			if (control.Controls.Count == 0) return;

			for (int i = 0; i < control.Controls.Count; i++)
				SetEnterMoveNextControl(control.Controls[i], value);
		}

		public static DialogResult ShowPopup(Control control, Control parentControl, bool disposeControl,
			bool isTopMost = true, bool showTitleBar = false)
		{
			using (PopupForm popupForm = new PopupForm())
			{
				popupForm.ShowTitleBar = showTitleBar;
				popupForm.Controls.Add(control);
				popupForm.Initialize(parentControl, control);
				//Size originalSize = control.Size;
				popupForm.WindowState = FormWindowState.Maximized;
				popupForm.BackColor = Color.Black;
				DialogResult dialogResult = popupForm.ShowDialog();

				//if (showTitleBar) 
				//	control.Size = originalSize;

				popupForm.Controls.Remove(control);

				if (disposeControl)
					control.Dispose();

				return dialogResult;
			}
		}

		public static DialogResult ShowPopup(Control controlToShow)
		{
			FlyoutAction action = new FlyoutAction();
			action.Caption = "Flyout Action";
			action.Description = "Flyout Action Description";
			action.Commands.Add(FlyoutCommand.Yes);
			action.Commands.Add(FlyoutCommand.No);
			return FlyoutDialog.Show(controlToShow.Parent.FindForm(), action, controlToShow);
		}

		public static object GetSelectedRowObject(GridView view)
		{
			var rowIndex = view.GetSelectedRows();
			return view.GetRow(rowIndex[0]);
		}

		public static TEntity GetSelectedRowObject<TEntity>(GridView gridView)
		{
			return (TEntity)gridView.GetRow(gridView.FocusedRowHandle);
		}

		public static object GetSelectedRowObject(LayoutView view)
		{
			var rowIndex = view.GetSelectedRows();
			return view.GetRow(rowIndex[0]);
		}

		public static void UpdateStationPointStagePanelControl(PanelControl panelControl)
		{
			if (panelControl.Controls.Count == 0)
				return;

			List<DevExpress.XtraEditors.CheckButton> checkButtonList = GetAllCheckButtonsFromPanelControl(panelControl);
			foreach (Control control in panelControl.Controls)
				if (control is DevExpress.XtraEditors.CheckButton)
					checkButtonList.Add(control as DevExpress.XtraEditors.CheckButton);

			foreach (DevExpress.XtraEditors.CheckButton checkButton in checkButtonList)
				checkButton.GroupIndex = 1;
		}

		public static List<DevExpress.XtraEditors.CheckButton> GetAllCheckButtonsFromPanelControl(PanelControl panelControl)
		{
			if (panelControl == null)
				return null;

			List<DevExpress.XtraEditors.CheckButton> checkButtonList = new List<DevExpress.XtraEditors.CheckButton>();
			foreach (Control control in panelControl.Controls)
				if (control is DevExpress.XtraEditors.CheckButton)
					checkButtonList.Add(control as DevExpress.XtraEditors.CheckButton);

			return checkButtonList;
		}

		public static XtraTabControl CreateTabControl(TabControlConstructor tabControlConstructor)
		{
			if (tabControlConstructor == null || tabControlConstructor.ParentControlToAttach == null)
				return null;

			XtraTabControl tabControl = new XtraTabControl();
			tabControl.BorderStyle = tabControlConstructor.BorderStyles;
			tabControl.BorderStylePage = tabControlConstructor.BorderStylePage;
			if (tabControlConstructor.ShowPreviousAndNextButtons)
				tabControl.HeaderButtons = TabButtons.Prev | TabButtons.Next;
			tabControl.HeaderLocation = tabControlConstructor.TabHeaderLocation;
			tabControl.HeaderAutoFill = tabControlConstructor.HeaderAutoFill;
			tabControl.HeaderButtonsShowMode = tabControlConstructor.TabButtonShowMode;
			tabControl.HeaderLocation = tabControlConstructor.TabHeaderLocation;
			//tabControl.LookAndFeel.SkinName = tabControlConstructor.SkinName;
			tabControl.SelectedTabPageIndex = tabControlConstructor.SelectedTabIndex;
			tabControl.Dock = tabControlConstructor.DockStyle;

			if (tabControlConstructor.TabPagesList != null)
				foreach (TabPageControlConstructor tabPageControlConstructor in tabControlConstructor.TabPagesList)
				{
					XtraTabPage tabPage = CreateTabPageControl(tabPageControlConstructor);
					if (tabPage != null)
						if (tabControl.TabPages.Count == 0 || !tabControl.TabPages.Contains(tabPage))
							tabControl.TabPages.Add(tabPage);
				}

			if (!tabControlConstructor.ParentControlToAttach.Controls.Contains(tabControl))
				tabControlConstructor.ParentControlToAttach.Controls.Add(tabControl);

			return tabControl;
		}

		public static RepositoryItem CreateGridColumnRepositoryItem<TControlType>(GridControl gridControl, IList sourceList,
			string sourceFieldName, string destinationValueMember, string sourceDisplayMember = "Name_P")
			where TControlType : RepositoryItem, new()
		{
			if (gridControl == null || String.IsNullOrEmpty(sourceFieldName) || sourceList == null ||
				String.IsNullOrEmpty(destinationValueMember))
				return null;

			TControlType control = new TControlType();
			GridColumn column = null;
			GridView view = gridControl.MainView as GridView;
			if (view == null)
				return null;

			foreach (GridColumn gridColumn in view.Columns)
			{
				if (gridColumn.FieldName == sourceFieldName)
				{
					column = gridColumn;
					break;
				}
			}

			if (column == null)
				return null;

			if (control is RepositoryItemGridLookUpEdit)
				FillRepositoryLookupEdit(control as RepositoryItemGridLookUpEdit, sourceList, destinationValueMember, sourceDisplayMember);

			return control;
		}

		public static RepositoryItem CreateGridColumnRepositoryItem<TControlType>(ColumnView columnView, IList sourceList,
			string destinationFieldName, string displayMember = "Name_P")
			where TControlType : RepositoryItem, new()
		{
			if (columnView == null || string.IsNullOrEmpty(destinationFieldName) || sourceList == null)
				return null;

			TControlType control = new TControlType();
			ColumnView view = columnView;
			if (view.Columns.Count > 0)
			{
				foreach (GridColumn column in view.Columns)
				{
					if (!column.FieldName.Equals(destinationFieldName) || column.ColumnEdit != null)
						continue;
					column.ColumnEdit = control;
					break;
				}
			}

			if (control is RepositoryItemGridLookUpEdit)
				FillRepositoryLookupEdit(control as RepositoryItemGridLookUpEdit, sourceList, destinationFieldName, displayMember);
			//else if (lkeCtrl is RepositoryItemGridLookUpEdit)
			//	FillRepositoryItemGridLookUpEdit(control as RepositoryItemGridLookUpEdit, source, displayMember);

			return null;
		}

		private static void FillRepositoryLookupEdit(RepositoryItemGridLookUpEdit control, IList sourceList, string destinationFieldName, string displayMember)
		{
			control.DataSource = sourceList;
			control.ValueMember = destinationFieldName;
			control.DisplayMember = displayMember;
		}

		public static XtraTabPage CreateTabPageControl(XtraTabControl parentTabControl, String headerTitle, int tabIndex,
			String skinName, bool pageEnabled = true, bool pageVisible = true,
			DefaultBoolean showCloseButton = DefaultBoolean.False, BorderStyle borderStyle = BorderStyle.None,
			DockStyle dockStyle = DockStyle.Fill)
		{
			if (parentTabControl == null)
				return null;

			XtraTabPage tabPage = new XtraTabPage();
			tabPage.BorderStyle = borderStyle;
			tabPage.Dock = dockStyle;
			tabPage.PageEnabled = pageEnabled;
			tabPage.PageVisible = pageVisible;
			tabPage.ShowCloseButton = showCloseButton;
			tabPage.TabIndex = tabIndex;
			tabPage.Text = headerTitle;

			if (parentTabControl.TabPages.Count == 0 || !parentTabControl.TabPages.Contains(tabPage))
				parentTabControl.TabPages.Add(tabPage);

			return tabPage;
		}

		public static XtraTabPage CreateTabPageControl(TabPageControlConstructor tabPageControlConstructor)
		{
			if (tabPageControlConstructor == null)
				return null;

			XtraTabPage tabPage = new XtraTabPage();
			tabPage.BorderStyle = tabPageControlConstructor.BorderStyle;
			tabPage.Dock = tabPageControlConstructor.DockStyle;
			tabPage.LookAndFeel.SkinName = tabPageControlConstructor.SkinName;
			tabPage.PageEnabled = tabPageControlConstructor.PageEnabled;
			tabPage.PageVisible = tabPageControlConstructor.PageVisible;
			tabPage.ShowCloseButton = tabPageControlConstructor.ShowCloseButton;
			tabPage.TabIndex = tabPageControlConstructor.TabIndex;
			tabPage.Text = tabPageControlConstructor.HeaderTitle;

			return tabPage;
		}

		public static void SetEnableControls(bool isEnable, params Control[] controls)
		{
			foreach (Control control in controls)
				if (control is BaseEdit)
					((BaseEdit)control).Properties.Enabled = isEnable;
		}

		public static void SetReadOnlyControls(bool isReadOnly, params Control[] controls)
		{
			foreach (Control control in controls)
				if (control is BaseEdit)
					((BaseEdit)control).Properties.ReadOnly = isReadOnly;
		}

		public static void CreateAccordionControl(AccordionControlConstructor accordionControlConstructor)
		{
			AccordionControl accordionControl = new AccordionControl();
			accordionControl.Dock = accordionControlConstructor.DockStyle;
			if (accordionControlConstructor.AccordionElementConstructorsList != null)
				foreach (AccordionElementConstructor accordionElementConstructor in
					accordionControlConstructor.AccordionElementConstructorsList)
					CreateAccordionControlElement(accordionElementConstructor);
		}

		public static void CreateAccordionControlElement(AccordionElementConstructor accordionElementConstructor)
		{
			AccordionControlElement accordionControlElement = new AccordionControlElement();
			accordionControlElement.Text = accordionControlElement.Text;
			if (accordionElementConstructor.AccordionElementConstructorsList != null)
				foreach (AccordionElementConstructor elementConstructor in
					accordionElementConstructor.AccordionElementConstructorsList)
					CreateAccordionControlElement(elementConstructor);
		}

		public static void LoadXMLFromString(LayoutControl layoutControl, String layoutXML)
		{
			String fileName = Path.GetTempFileName();
			StreamWriter lytFile = new StreamWriter(fileName);
			lytFile.Write(layoutXML);
			lytFile.Close();
			layoutControl.RestoreLayoutFromXml(fileName);
			File.Delete(fileName);
		}

		public static Stream LoadXMLFromString(String layoutXML)
		{
			MemoryStream streamToReturn = new MemoryStream();
			StreamWriter writer = new StreamWriter(streamToReturn);
			writer.Write(layoutXML);
			writer.Flush();
			streamToReturn.Position = 0;
			return streamToReturn;
		}

		public static void FillGridlookupEdit(GridLookUpEdit gridLookupEdit, IList dataSource, String displayMemeber = null,
			String valueMember = "ID", bool firstItemSelection = false)
		{
			if (gridLookupEdit == null)
				return;

			gridLookupEdit.Properties.View = new GridView();
			gridLookupEdit.Properties.DataSource = dataSource;
			if (dataSource != null)
			{
				String columnName;
				if (displayMemeber == null)
					columnName = "Name_P";
				else
					columnName = displayMemeber;

				if (firstItemSelection)
					gridLookupEdit.EditValue = dataSource[0];

				gridLookupEdit.Properties.View.Columns.Clear();
				gridLookupEdit.Properties.View.OptionsBehavior.AutoPopulateColumns = false;
				gridLookupEdit.Properties.DisplayMember = columnName;
				gridLookupEdit.Properties.ValueMember = valueMember;
				gridLookupEdit.Properties.PopupFilterMode = PopupFilterMode.Contains;
				gridLookupEdit.Properties.TextEditStyle = TextEditStyles.Standard;
				gridLookupEdit.Properties.PopupBorderStyle = PopupBorderStyles.Simple;
				gridLookupEdit.Properties.View.OptionsView.ShowColumnHeaders = false;
				gridLookupEdit.Properties.NullValuePromptShowForEmptyValue = true;
				gridLookupEdit.Properties.NullText = "";
				gridLookupEdit.Properties.NullValuePrompt = !IsArbicCulture ? "Select" : "إختــار";

				GridColumn col2 = gridLookupEdit.Properties.View.Columns.AddField(columnName);
				col2.VisibleIndex = 1;
				col2.Caption = !IsArbicCulture ? "Name" : "الإســم";
				col2.AppearanceHeader.Font = HeaderStyleController.Appearance.Font;
				col2.AppearanceCell.Font = DefaultStyleController.Appearance.Font;

				ApplyStyleToGridControl(gridLookupEdit.Properties.View);
			}
		}

		public static void FillListBoxControl(ListBoxControl listBoxControl, IList dataSource, String displayMemeber = null,
			String valueMember = "ID")
		{
			if (listBoxControl == null)
				return;

			listBoxControl.DataSource = dataSource;
			if (dataSource != null)
			{
				String columnName;
				if (displayMemeber == null)
					columnName = "Name_P";
				else
					columnName = displayMemeber;

				listBoxControl.DisplayMember = columnName;
				listBoxControl.ValueMember = valueMember;
				//listBoxControl.MultiColumn
			}
		}

		public static void ApplyStyleToGridControl(GridView gridView)
		{
			gridView.BorderStyle = BorderStyles.Simple;
			gridView.GridControl.UseEmbeddedNavigator = true;
			gridView.GridControl.EmbeddedNavigator.Buttons.Append.Visible = false;
			gridView.GridControl.EmbeddedNavigator.Buttons.Remove.Visible = false;
			gridView.GridControl.EmbeddedNavigator.Buttons.Edit.Visible = false;
			gridView.GridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
			gridView.GridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
			gridView.Appearance.FocusedRow.BackColor = Color.LightSteelBlue;
			gridView.Appearance.Row.Font =
				gridView.AppearancePrint.Row.Font = DefaultStyleController.Appearance.Font;
			gridView.Appearance.FooterPanel.Font =
				gridView.AppearancePrint.FooterPanel.Font = DefaultStyleController.Appearance.Font;
			gridView.Appearance.HeaderPanel.Font =
				gridView.AppearancePrint.HeaderPanel.Font = HeaderStyleController.Appearance.Font;
			gridView.Appearance.ViewCaption.Font = DefaultStyleController.Appearance.Font;

			gridView.ShowFilterPopupListBox -= GridControl_ShowFilterPopupListBox;
			gridView.ShowFilterPopupListBox += GridControl_ShowFilterPopupListBox;

			gridView.ShowFilterPopupCheckedListBox -= GridControl_ShowFilterPopupCheckedListBox;
			gridView.ShowFilterPopupCheckedListBox += GridControl_ShowFilterPopupCheckedListBox;
			gridView.RowLoaded += GridView_RowLoaded;

			gridView.OptionsView.ShowFooter = true;
			gridView.OptionsCustomization.AllowColumnMoving = false;
			gridView.OptionsView.ShowGroupPanel = false;
			gridView.OptionsView.ShowDetailButtons = true;

			gridView.OptionsPrint.UsePrintStyles = true;
			gridView.OptionsPrint.PrintVertLines = false;
			gridView.OptionsPrint.PrintHorzLines = true;

			gridView.OptionsPrint.EnableAppearanceOddRow = true;
			gridView.OptionsPrint.EnableAppearanceEvenRow = true;
			gridView.OptionsView.EnableAppearanceEvenRow = true;
			gridView.OptionsView.EnableAppearanceOddRow = true;

			gridView.PaintAppearance.Row.TextOptions.WordWrap = WordWrap.NoWrap;
			gridView.Appearance.HeaderPanel.TextOptions.WordWrap = WordWrap.NoWrap;
			gridView.AppearancePrint.HeaderPanel.TextOptions.WordWrap = WordWrap.NoWrap;
			gridView.Appearance.Row.TextOptions.WordWrap = WordWrap.NoWrap;
			gridView.Appearance.Row.TextOptions.HAlignment = HorzAlignment.Default;
			gridView.Appearance.FocusedRow.TextOptions.HAlignment = HorzAlignment.Default;
			gridView.Appearance.EvenRow.TextOptions.HAlignment = HorzAlignment.Default;
			gridView.Appearance.OddRow.TextOptions.HAlignment = HorzAlignment.Default;
			gridView.Appearance.FixedLine.TextOptions.HAlignment = HorzAlignment.Default;
			gridView.Appearance.SelectedRow.TextOptions.HAlignment = HorzAlignment.Default;
			gridView.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;

			if (!string.IsNullOrEmpty(ApplicationStaticConfiguration.SkinName) &&
				!string.IsNullOrWhiteSpace(ApplicationStaticConfiguration.SkinName))
				if (!ApplicationStaticConfiguration.SkinName.Equals("McSkin"))
					gridView.Appearance.HeaderPanel.ForeColor = Color.White;
				else
					gridView.Appearance.HeaderPanel.ForeColor = Color.Navy;
			
			gridView.Appearance.EvenRow.BackColor = Color.White;
			gridView.Appearance.OddRow.BackColor = Color.BlanchedAlmond;
			gridView.Appearance.FocusedCell.BackColor = Color.White;
			gridView.Appearance.VertLine.BackColor = Color.Orange;
			gridView.AppearancePrint.EvenRow.BackColor = Color.White;
			gridView.AppearancePrint.OddRow.BackColor = Color.Gainsboro;
			gridView.AppearancePrint.FooterPanel.BackColor = Color.Gainsboro;
		}

		private static void GridView_RowLoaded(object sender, RowEventArgs e)
		{
			Type type = e.RowHandle.GetType();
		}

		public static void SetupSyle(params Control[] controls)
		{
			foreach (Control parentContainer in controls)
			{
				if (parentContainer is BaseEdit)
				{
					((BaseEdit) parentContainer).Properties.Appearance.ForeColor =
						DefaultStyleController.Appearance.ForeColor;
					((BaseEdit)parentContainer).Properties.Appearance.Font = DefaultStyleController.Appearance.Font;
					((BaseEdit)parentContainer).Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
					((BaseEdit)parentContainer).EnterMoveNextControl = true;
					((BaseEdit)parentContainer).GotFocus += BaseEdit_GotFocus;

					if (parentContainer is PopupBaseEdit)
					{
						switch (ApplicationStaticConfiguration.Application)
						{
							case DB_Application.AdmissionReception:
							case DB_Application.AllReception:
							case DB_Application.ClinicReception:
							case DB_Application.InvoiceManager:
							case DB_Application.QueueManager:
							case DB_Application.Settings:
							case DB_Application.MerkFinance:
								if (String.IsNullOrEmpty(((PopupBaseEdit) parentContainer).Properties.NullValuePrompt))
									((PopupBaseEdit) parentContainer).Properties.NullValuePrompt = "إختــــار";
								break;
							case DB_Application.PEMR:
							case DB_Application.FinanceInvoiceCreation:
								if (String.IsNullOrEmpty(((PopupBaseEdit) parentContainer).Properties.NullValuePrompt))
									((PopupBaseEdit) parentContainer).Properties.NullValuePrompt = "Select";
								break;
						}

						switch (ApplicationStaticConfiguration.Application)
						{
							case DB_Application.PEMR:
							case DB_Application.FinanceInvoiceCreation:
								((PopupBaseEdit) parentContainer).Properties.Buttons[0].IsLeft = true;
								break;
						}

						((PopupBaseEdit)parentContainer).Properties.ShowPopupShadow = true;
						((PopupBaseEdit)parentContainer).Properties.PopupBorderStyle = PopupBorderStyles.Simple;
						((PopupBaseEdit)parentContainer).Properties.PopupResizeMode = ResizeMode.LiveResize;
						((PopupBaseEdit)parentContainer).Properties.AllowMouseWheel = true;
						((PopupBaseEdit)parentContainer).Properties.AllowDropDownWhenReadOnly = DefaultBoolean.True;
						((PopupBaseEdit)parentContainer).Properties.ShowDropDown = ShowDropDown.DoubleClick;
					}

					if (parentContainer is DateEdit)
					{
						((DateEdit)parentContainer).Properties.EditMask = "dd/MM/yyyy";
						((DateEdit)parentContainer).Properties.Mask.EditMask = "dd/MM/yyyy";
						((DateEdit)parentContainer).Properties.Mask.UseMaskAsDisplayFormat = true;
						((DateEdit)parentContainer).Properties.VistaDisplayMode = DefaultBoolean.True;
						((DateEdit)parentContainer).Properties.ShowDropDown = ShowDropDown.DoubleClick;
					}

					if (parentContainer is TimeEdit)
					{
						((TimeEdit)parentContainer).Properties.EditMask = "t";
						((TimeEdit)parentContainer).Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
					}
				}

				if (parentContainer is CheckedListBoxControl)
					((CheckedListBoxControl)parentContainer).CheckOnClick = true;

				if (parentContainer is BaseStyleControl)
					((BaseStyleControl)parentContainer).Font = DefaultStyleController.Appearance.Font;

				if (parentContainer is SimpleButton)
				{
					if (((SimpleButton)parentContainer).Text == string.Empty)
						((SimpleButton)parentContainer).ImageLocation = ImageLocation.MiddleCenter;
					switch (ApplicationStaticConfiguration.Application)
					{
						case DB_Application.AdmissionReception:
						case DB_Application.AllReception:
						case DB_Application.ClinicReception:
						case DB_Application.InvoiceManager:
						case DB_Application.QueueManager:
						case DB_Application.Settings:
						case DB_Application.MerkFinance:
							if (!string.IsNullOrEmpty(ApplicationStaticConfiguration.SkinName) &&
								!string.IsNullOrWhiteSpace(ApplicationStaticConfiguration.SkinName))
								if (!ApplicationStaticConfiguration.SkinName.Equals("McSkin"))
									((SimpleButton) parentContainer).ForeColor = Color.White;
								else
									((SimpleButton)parentContainer).ForeColor = Color.Black;
							if (((SimpleButton) parentContainer).ImageLocation != ImageLocation.MiddleCenter)
								((SimpleButton) parentContainer).ImageLocation = ImageLocation.MiddleRight;
							break;
						case DB_Application.PEMR:
						case DB_Application.FinanceInvoiceCreation:
							((SimpleButton)parentContainer).ForeColor = Color.Black;
							if (((SimpleButton) parentContainer).ImageLocation != ImageLocation.MiddleCenter)
								((SimpleButton) parentContainer).ImageLocation = ImageLocation.MiddleLeft;
							break;
					}

					((SimpleButton)parentContainer).Appearance.Font = SimpleButtonStyleController.Appearance.Font;
				}

				if (parentContainer is GroupControl)
				{
					((GroupControl)parentContainer).Font = HeaderStyleController.Appearance.Font;
					((GroupControl)parentContainer).AppearanceCaption.Font = HeaderStyleController.Appearance.Font;
					((GroupControl)parentContainer).AppearanceCaption.ForeColor = Color.Yellow;
				}

				if (parentContainer.Controls.Count > 0)
				{
					foreach (Control cont in parentContainer.Controls)
					{
						if (cont is LayoutControl)
						{
							SetLayoutControllerStyle((LayoutControl)cont, LayoutStyleController);
							SetupSyle(cont.Controls.Owner);
						}
						else
							SetupSyle(cont);
					}
				}
			}

			Decorate(controls);
		}

		public static void SetupSyle_PEMR(params Control[] controls)
		{
			foreach (Control parentContainer in controls)
			{
				if (parentContainer is BaseEdit)
				{
					//((BaseEdit)parentContainer).GotFocus += BaseEdit_GotFocus;

					if (parentContainer is PopupBaseEdit)
					{
						switch (ApplicationStaticConfiguration.Application)
						{
							case DB_Application.AdmissionReception:
							case DB_Application.AllReception:
							case DB_Application.ClinicReception:
							case DB_Application.InvoiceManager:
							case DB_Application.QueueManager:
							case DB_Application.Settings:
							case DB_Application.MerkFinance:
								//if (String.IsNullOrEmpty(((PopupBaseEdit) parentContainer).Properties.NullValuePrompt))
								//	((PopupBaseEdit) parentContainer).Properties.NullValuePrompt = "إختــــار";
								break;
							case DB_Application.PEMR:
							case DB_Application.FinanceInvoiceCreation:
								//if (String.IsNullOrEmpty(((PopupBaseEdit) parentContainer).Properties.NullValuePrompt))
								//	((PopupBaseEdit) parentContainer).Properties.NullValuePrompt = "Select";
								break;
						}

						//((PopupBaseEdit)parentContainer).Properties.ShowPopupShadow = true;
						//((PopupBaseEdit)parentContainer).Properties.PopupBorderStyle = PopupBorderStyles.Simple;
						//((PopupBaseEdit)parentContainer).Properties.PopupResizeMode = ResizeMode.LiveResize;
						//((PopupBaseEdit)parentContainer).Properties.AllowMouseWheel = true;
						//((PopupBaseEdit)parentContainer).Properties.AllowDropDownWhenReadOnly = DefaultBoolean.True;
						//((PopupBaseEdit)parentContainer).Properties.ShowDropDown = ShowDropDown.DoubleClick;
					}

					if (parentContainer is DateEdit)
					{
						//((DateEdit)parentContainer).Properties.EditMask = "dd/MM/yyyy";
						//((DateEdit)parentContainer).Properties.Mask.EditMask = "dd/MM/yyyy";
						//((DateEdit)parentContainer).Properties.Mask.UseMaskAsDisplayFormat = true;
						//((DateEdit)parentContainer).Properties.VistaDisplayMode = DefaultBoolean.True;
						//((DateEdit)parentContainer).Properties.ShowDropDown = ShowDropDown.DoubleClick;
					}

					if (parentContainer is TimeEdit)
					{
						//((TimeEdit)parentContainer).Properties.EditMask = "t";
						//((TimeEdit)parentContainer).Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
					}
				}

				if (parentContainer.Controls.Count > 0)
				{
					foreach (Control cont in parentContainer.Controls)
					{
						if (cont is LayoutControl)
							SetupSyle(cont.Controls.Owner);
						else
							SetupSyle(cont);
					}
				}
			}

			Decorate(controls);
		}

		public static void SetLayoutControllerStyle(LayoutControl mainContainer, StyleController styleToUse)
		{
			mainContainer.StyleController = styleToUse;
			mainContainer.OptionsFocus.MoveFocusRightToLeft = true;
			if (mainContainer.Appearance != null)
			{
				mainContainer.Appearance.Control.Font = styleToUse.Appearance.Font;
				mainContainer.Appearance.ControlDropDown.Font = styleToUse.Appearance.Font;
				mainContainer.Appearance.ControlDropDownHeader.Font = styleToUse.Appearance.Font;
				mainContainer.Appearance.ControlReadOnly.Font = styleToUse.Appearance.Font;
				mainContainer.AutoScaleMode = AutoScaleMode.None;
			}

			if (mainContainer.Root != null)
			{
				mainContainer.Root.AppearanceItemCaption.Font = styleToUse.Appearance.Font;
				switch (ApplicationStaticConfiguration.Application)
				{
					case DB_Application.AdmissionReception:
					case DB_Application.AllReception:
					case DB_Application.ClinicReception:
					case DB_Application.InvoiceManager:
					case DB_Application.QueueManager:
					case DB_Application.Settings:
					case DB_Application.MerkFinance:
						if (!string.IsNullOrEmpty(ApplicationStaticConfiguration.SkinName) &&
							!string.IsNullOrWhiteSpace(ApplicationStaticConfiguration.SkinName))
							if (!ApplicationStaticConfiguration.SkinName.Equals("McSkin"))
								mainContainer.Root.AppearanceItemCaption.ForeColor = styleToUse.Appearance.ForeColor;
						break;
				}

				mainContainer.Root.AppearanceGroup.Font = styleToUse.AppearanceDropDownHeader.Font;
				switch (ApplicationStaticConfiguration.Application)
				{
					case DB_Application.AdmissionReception:
					case DB_Application.AllReception:
					case DB_Application.ClinicReception:
					case DB_Application.InvoiceManager:
					case DB_Application.QueueManager:
					case DB_Application.Settings:
					case DB_Application.MerkFinance:
						if (!string.IsNullOrEmpty(ApplicationStaticConfiguration.SkinName) &&
							!string.IsNullOrWhiteSpace(ApplicationStaticConfiguration.SkinName))
							if (!ApplicationStaticConfiguration.SkinName.Equals("McSkin"))
								mainContainer.Root.AppearanceGroup.ForeColor =
									styleToUse.AppearanceDropDownHeader.ForeColor;
						break;
				}

				mainContainer.Root.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Default;
				mainContainer.Root.CaptionImageLocation = GroupElementLocation.Default;
				mainContainer.AutoScaleMode = AutoScaleMode.None;
			}

			mainContainer.Font = styleToUse.Appearance.Font;
			switch (ApplicationStaticConfiguration.Application)
			{
				case DB_Application.AdmissionReception:
				case DB_Application.AllReception:
				case DB_Application.ClinicReception:
				case DB_Application.InvoiceManager:
				case DB_Application.QueueManager:
				case DB_Application.Settings:
				case DB_Application.MerkFinance:
					if (!string.IsNullOrEmpty(ApplicationStaticConfiguration.SkinName) &&
						!string.IsNullOrWhiteSpace(ApplicationStaticConfiguration.SkinName))
						if (!ApplicationStaticConfiguration.SkinName.Equals("McSkin"))
							mainContainer.ForeColor = styleToUse.Appearance.ForeColor;
					break;
			}
		}

		public static void Decorate(params Control[] controls)
		{
			foreach (Control ctrl in controls)
			{
				if (!(ctrl is ButtonEdit))
					continue;

				ButtonEdit btnEditCtrl = ctrl as ButtonEdit;
				if (btnEditCtrl.Properties.Buttons.Count > 1)
					continue;

				EditorButton btnClear = new EditorButton(ButtonPredefines.Glyph, "Clear Selection");
				btnEditCtrl.Properties.Buttons.Add(btnClear);
				switch (ApplicationStaticConfiguration.Application)
				{
					case DB_Application.AdmissionReception:
					case DB_Application.AllReception:
					case DB_Application.ClinicReception:
					case DB_Application.InvoiceManager:
					case DB_Application.QueueManager:
					case DB_Application.Settings:
					case DB_Application.MerkFinance:
						btnClear.Image = Resources.LocaizedRes.ClearIcon_16X16;
						break;
					case DB_Application.PEMR:
					case DB_Application.FinanceInvoiceCreation:
						btnClear.Image = Resources.LocaizedRes.ClearIcon_16X16_en_US;
						break;
				}

				btnClear.Visible = false;
				btnClear.IsLeft = !btnEditCtrl.Properties.Buttons[0].IsLeft;
				btnEditCtrl.ButtonClick += btnEditCtrl_ButtonClick;
				btnEditCtrl.Enter += btnEditCtrl_Enter;
				btnEditCtrl.Leave += btnEditCtrl_Leave;
			}
		}

		private static void ShowClearButton(ButtonEdit sender, bool bShow)
		{
			ButtonEdit ctrl = sender;
			if (ctrl == null)
				return;
			if (ctrl.Properties.Buttons.Count > 1)
				ctrl.Properties.Buttons[ctrl.Properties.Buttons.Count - 1].Visible = bShow;
		}

		private static void ShowClearButton(object sender, bool bShow)
		{
			ButtonEdit ctrl = sender as ButtonEdit;
			ShowClearButton(ctrl, bShow);
		}

		public static void SetupGridControl(GridControl gcToSetup, String layoutAsXML,
			bool isReadOnly, bool isEditable = true, bool linesVisibleOnPrint = true, bool showfocusedRowColors = true)
		{
			SetupGridControl(gcToSetup, layoutAsXML,
				new GridControlSettings()
				{
					ReadOnly = isReadOnly,
					Editable = isEditable,
					LinesVisibleOnPrint = linesVisibleOnPrint,
					ShowFocusedRowColors = showfocusedRowColors
				});
		}

		public static void SetupGridControl(GridControl gcToSetup, String layoutAsXML,
			GridControlSettings gridSettings)
		{
			if (gcToSetup.MainView is GridView)
				SetupGridView((GridView)gcToSetup.MainView, layoutAsXML, gridSettings);
		}

		public static void SetupGridView(GridView gvuToSetup, String layoutAsXML,
			GridControlSettings gridSettings)
		{
			if (!String.IsNullOrEmpty(layoutAsXML))
				gvuToSetup.RestoreLayoutFromStream(LoadXMLFromString(layoutAsXML), null);

			gvuToSetup.OptionsBehavior.ReadOnly = gridSettings.ReadOnly;
			gvuToSetup.OptionsBehavior.Editable = gridSettings.Editable;

			StyleController stc = new StyleController();
			stc.Appearance.Combine(GridControlHeaderStyleController.Appearance);

			ApplyStyleToGridControl(gvuToSetup);

			gvuToSetup.OptionsBehavior.EditorShowMode = gridSettings.EditorShowMode;

			gvuToSetup.Appearance.Row.Font =
				gvuToSetup.AppearancePrint.Row.Font = GridControlCellStyleController.Appearance.Font;
			gvuToSetup.Appearance.FooterPanel.Font =
				gvuToSetup.AppearancePrint.FooterPanel.Font = GridControlCellStyleController.Appearance.Font;
			gvuToSetup.Appearance.HeaderPanel.Font =
				gvuToSetup.AppearancePrint.HeaderPanel.Font = GridControlHeaderStyleController.Appearance.Font;
			gvuToSetup.Appearance.ViewCaption.Font = GridControlHeaderStyleController.Appearance.Font;

			gvuToSetup.Appearance.Row.TextOptions.HAlignment =
				gvuToSetup.Appearance.FooterPanel.TextOptions.HAlignment =
					gvuToSetup.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;

			ReformatFields(gvuToSetup);
			gvuToSetup.DataSourceChanged += (s, e) => ReformatFields(gvuToSetup);
			bool OutoAdjustRowHieght = true;

			GridRowSizeAdapter gridRowSizeAdapter = new GridRowSizeAdapter(gvuToSetup);
			gridRowSizeAdapter.RegisterEvents();

			if (gridSettings.HasDeleteColumn && gvuToSetup.Columns.ColumnByName("btnDeleteColumn") == null)
			{
				GridColumn gridColumn = CreateColumn(gvuToSetup, true, "", "X");
				gridColumn.Name = "btnDeleteColumn";
				gridColumn.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

				RepositoryItemButtonEdit btn = BindSimpleButtonToGridColumn(gvuToSetup.GridControl, "X");
				btn.Properties.Buttons[0].Kind = ButtonPredefines.Glyph;
				btn.Properties.Buttons[0].Appearance.TextOptions.HAlignment = HorzAlignment.Center;
				btn.Properties.Buttons[0].Image = Resources.LocaizedRes.ClearIcon_16X16;
				btn.ButtonClick += (s, e) =>
				{
					if (gridSettings.BeforeOnDelete != null)
						if (!gridSettings.BeforeOnDelete(GridGetSelectedDataSourceItem(gvuToSetup))) return;

					if (gridSettings.OnDelete != null)
						gridSettings.OnDelete(GridGetSelectedDataSourceItem(gvuToSetup));
					else
						((IList)gvuToSetup.DataSource).Remove(GridGetSelectedDataSourceItem(gvuToSetup));

					gvuToSetup.GridControl.RefreshDataSource();

					if (gridSettings.AfterOnDelete != null)
						gridSettings.AfterOnDelete(null);
				};
			}
		}

		public static void ReformatFields(GridView view)
		{
			ColumnView columnView = view;
			if (columnView == null)
				return;

			foreach (GridColumn column in columnView.Columns)
			{
				if (column.SummaryItem.SummaryType != SummaryItemType.None)
				{
					column.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
					if (string.IsNullOrEmpty(column.DisplayFormat.FormatString))
					{
						column.DisplayFormat.FormatType = FormatType.Numeric;
						column.DisplayFormat.FormatString = "#,0.00;-#,0.00;0";
						column.AppearanceCell.TextOptions.HAlignment =
							column.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
					}

					if (string.IsNullOrEmpty(column.SummaryItem.DisplayFormat))
						column.SummaryItem.DisplayFormat = "{0:#,0.00;-#,0.00;0}";
				}

				if (string.IsNullOrEmpty(column.DisplayFormat.FormatString) && columnView.DataSource != null &&
					Regex.IsMatch(Convert.ToString(view.GetRowCellValue(0, column.FieldName)), @"\d\d+/\d\d+/\d\d\d\d"))
				{
					column.DisplayFormat.FormatType = FormatType.DateTime;
					column.DisplayFormat.FormatString = "dd/MM/yyyy";
				}
			}

			GridViewInfo viewInfo = view.GetViewInfo() as GridViewInfo;
			if (viewInfo == null)
				return;

			foreach (GridRowInfo rowInfo in viewInfo.RowsInfo)
			{
				GridView detailView = view.GetDetailView(rowInfo.RowHandle, 0) as GridView;
				if (detailView == null)
					continue;

				ReformatFields(detailView);
			}
		}

		public static void ReformatFields(GridControl gridControl)
		{
			GridControl ctrl = gridControl;
			if (ctrl == null)
				return;

			ReformatFields(ctrl.MainView as GridView);
		}

		public static RepositoryItemButtonEdit BindSimpleButtonToGridColumn(GridControl gridControl, string dbFieldName)
		{
			RepositoryItemButtonEdit repositoryItemButtonEdit1 = new RepositoryItemButtonEdit();
			gridControl.RepositoryItems.Add(repositoryItemButtonEdit1);
			if (((GridView)gridControl.MainView).Columns.Count > 0)
			{
				if (((GridView)gridControl.MainView).Columns[dbFieldName] != null)
					((GridView)gridControl.MainView).Columns[dbFieldName].ColumnEdit = repositoryItemButtonEdit1;
			}
			return repositoryItemButtonEdit1;
		}

		public static Object GridGetSelectedDataSourceItem(ColumnView gvuToUse)
		{
			Object selectedIndex = GridGetSelectedDataSourceIndex(gvuToUse);
			if (selectedIndex == null || gvuToUse.DataSource == null)
				return null;

			if (!(gvuToUse.DataSource is IList))
				return null;

			return ((IList)gvuToUse.DataSource)[(int)selectedIndex];
		}

		public static Object GridGetSelectedDataSourceIndex(ColumnView gvuToUse)
		{
			if (gvuToUse.DataSource == null || gvuToUse.SelectedRowsCount == 0)
				return null;

			return gvuToUse.GetDataSourceRowIndex(gvuToUse.GetSelectedRows()[0]);
		}

		public static GridColumn CreateColumn(GridView view, bool bVisible, string strCaption, string strFieldName,
			object columnWidth = null, bool isShowBindedEditor = true)
		{
			GridColumn gridColumntmpColumn;
			gridColumntmpColumn = new GridColumn();
			view.Columns.Add(gridColumntmpColumn);
			if (view.Columns[0].AppearanceCell.TextOptions.HAlignment == HorzAlignment.Default)
				gridColumntmpColumn.VisibleIndex = 0;
			else
			{
				if (view.Columns.Count > 0)
					gridColumntmpColumn.VisibleIndex = view.Columns.Count - 1;
			}

			gridColumntmpColumn.Caption = strCaption;
			gridColumntmpColumn.FieldName = strFieldName;
			gridColumntmpColumn.Name = strFieldName;
			gridColumntmpColumn.Visible = bVisible;
			gridColumntmpColumn.AppearanceCell.BackColor = Color.White;
			gridColumntmpColumn.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
			gridColumntmpColumn.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			gridColumntmpColumn.Width = 40;
			gridColumntmpColumn.OptionsColumn.FixedWidth = true;

			if (isShowBindedEditor)
				gridColumntmpColumn.ShowButtonMode = ShowButtonModeEnum.ShowAlways;

			if (columnWidth != null)
			{
				gridColumntmpColumn.OptionsColumn.FixedWidth = true;
				gridColumntmpColumn.Width = Convert.ToInt32(columnWidth);
			}

			return gridColumntmpColumn;
		}

		public static void ResizeControl(Control control, LayoutControlItem layoutControlItemToResize)
		{
			foreach (Control ctrl in control.Controls)
			{
				LayoutControl layoutControl = ctrl as LayoutControl;
				if (layoutControl == null)
					continue;

				double scaleFactor = 1.5;
				SetLayoutControllerStyle(layoutControl, DefaultStyleController);
				control.MinimumSize = new Size(control.MinimumSize.Width, Convert.ToInt32(control.MinimumSize.Height * scaleFactor));
			}

			control.Parent.MinimumSize = control.MinimumSize;

			int ctrlHeight = control.MinimumSize.Height;
			layoutControlItemToResize.Owner.BeginUpdate();
			layoutControlItemToResize.SizeConstraintsType = SizeConstraintsType.Custom;
			layoutControlItemToResize.MinSize = new Size(layoutControlItemToResize.MinSize.Width, ctrlHeight + 10);
			layoutControlItemToResize.MaxSize = new Size(1920, ctrlHeight + 22);
			layoutControlItemToResize.Owner.EndUpdate();
			layoutControlItemToResize.Owner.Invalidate();
		}

		#region Events

		static void BaseEdit_GotFocus(object sender, EventArgs e)
		{
			BaseEdit ctrl = sender as BaseEdit;
			if (ctrl == null)
				return;
			if (ctrl.EditValue != null)
				ctrl.SelectAll();
		}

		private static void GridControl_ShowFilterPopupListBox(object sender,
			FilterPopupListBoxEventArgs filterPopupListBoxEventArgs)
		{
			filterPopupListBoxEventArgs.ComboBox.Appearance.Font = DefaultStyleController.Appearance.Font;
			filterPopupListBoxEventArgs.ComboBox.Appearance.ForeColor = Color.CadetBlue;
			filterPopupListBoxEventArgs.ComboBox.AppearanceDropDown.Font = DefaultStyleController.Appearance.Font;
			filterPopupListBoxEventArgs.ComboBox.AppearanceDropDown.ForeColor = Color.CadetBlue;
		}

		private static void GridControl_ShowFilterPopupCheckedListBox(object sender, FilterPopupCheckedListBoxEventArgs e)
		{
			e.CheckedComboBox.Appearance.Font = DefaultStyleController.Appearance.Font;
			e.CheckedComboBox.Appearance.ForeColor = Color.CadetBlue;
			e.CheckedComboBox.AppearanceDropDown.Font = DefaultStyleController.Appearance.Font;
			e.CheckedComboBox.AppearanceDropDown.ForeColor = Color.CadetBlue;
		}

		private static void btnEditCtrl_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			ButtonEdit ctrl = sender as ButtonEdit;
			if (ctrl == null)
				return;

			if (!e.Button.Equals(ctrl.Properties.Buttons[ctrl.Properties.Buttons.Count - 1]))
				return;

			PopupBaseEdit popupBaseEdit = ctrl as PopupBaseEdit;
			if (popupBaseEdit != null)
				popupBaseEdit.ClosePopup();

			ctrl.EditValue = null;
		}

		private static void btnEditCtrl_Enter(object sender, EventArgs e)
		{
			ButtonEdit ctrl = sender as ButtonEdit;
			if (ctrl != null && !ctrl.Properties.ReadOnly)
				ShowClearButton(ctrl, true);
		}

		private static void btnEditCtrl_Leave(object sender, EventArgs e)
		{
			ShowClearButton(sender, false);
		}

		#endregion
	}
}
