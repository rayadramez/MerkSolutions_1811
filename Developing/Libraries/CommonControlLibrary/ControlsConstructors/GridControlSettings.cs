using DevExpress.Utils;

namespace CommonControlLibrary.ControlsConstructors
{
	public delegate bool DeleteButtonHandler(object dataSourceElement);

	public class GridControlSettings
	{
		public GridControlSettings()
		{
			ReadOnly = true;
			Editable = true;
			LinesVisibleOnPrint = true;
			ViewFontScale = 1.0f;
			PrintFontScale = 0.9f;
			EditorShowMode = EditorShowMode.Default;
			ShowFocusedRowColors = true;
		}

		public bool HasDeleteColumn { get; set; }
		public bool ReadOnly { get; set; }
		public bool Editable { get; set; }
		public bool LinesVisibleOnPrint { get; set; }
		public float ViewFontScale { get; set; }
		public float PrintFontScale { get; set; }

		public EditorShowMode EditorShowMode { get; set; }
		public bool ShowFocusedRowColors { get; set; }

		public DeleteButtonHandler BeforeOnDelete { get; set; }
		public DeleteButtonHandler OnDelete { get; set; }
		public DeleteButtonHandler AfterOnDelete { get; set; }
	}
}
