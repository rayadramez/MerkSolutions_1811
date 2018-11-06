using System;
using System.Drawing;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace CommonControlLibrary.ControlsConstructors
{
	class GridRowSizeAdapter
	{
		private GridView _gridView;

		public GridRowSizeAdapter(GridView gridView)
		{
			_gridView = gridView;
		}

		private void gridView1_CalcRowHeight(object sender, RowHeightEventArgs e)
		{
			e.RowHeight = GetRowBestHeight(_gridView, e.RowHandle);
		}

		private int GetRowBestHeight(GridView _view, int row)
		{
			GridViewInfo viewInfo = _view.GetViewInfo() as GridViewInfo;
			int height = 0;
			for (int i = 0; i < _view.VisibleColumns.Count; i++)
			{
				GridColumnInfoArgs ex = viewInfo.ColumnsInfo[_view.Columns[i]];
				if (ex == null)
				{
					viewInfo.GInfo.AddGraphics(null);
					ex = new GridColumnInfoArgs(viewInfo.GInfo.Cache, null);
					try
					{
						ex.InnerElements.Add(new DrawElementInfo(new GlyphElementPainter(),
							new GlyphElementInfoArgs(viewInfo.View.Images, 0, null),
							StringAlignment.Near));
						viewInfo.PaintAppearance.Row.TextOptions.WordWrap = WordWrap.Wrap;

						if (viewInfo.PaintAppearance.Row.Font.Size < _view.Columns[i].AppearanceCell.Font.Size)
							ex.SetAppearance(_view.Columns[i].AppearanceCell);
						else ex.SetAppearance(viewInfo.PaintAppearance.Row);

						ex.Caption = Convert.ToString(_view.GetRowCellValue(row, _view.Columns[i]));
						ex.CaptionRect = new Rectangle(0, 0, _view.Columns[i].Width - 20, 17);
					}
					finally
					{
						viewInfo.GInfo.ReleaseGraphics();
					}
				}

				GraphicsInfo grInfo = new GraphicsInfo();
				ex.Appearance.TextOptions.WordWrap = WordWrap.Wrap;
				grInfo.AddGraphics(null);
				ex.Cache = grInfo.Cache;
				bool canDrawMore = true;

				string s;
				if (_view.Columns[i].ColumnEdit is RepositoryItemGridLookUpEdit)
				{

					RepositoryItemGridLookUpEdit Rep = _view.Columns[i].ColumnEdit as RepositoryItemGridLookUpEdit;
					s = Rep.GetDisplayTextByKeyValue(_view.GetRowCellValue(row, _view.Columns[i]));
				}
				else
				{
					String sf;
					s = Convert.ToString(_view.GetRowCellValue(row, _view.Columns[i]));
					if (_view.GetRowCellValue(row, _view.Columns[i]) is DateTime)
					s = s.Substring(0, s.Length / 2);
				}
				Size captionSize = CalcCaptionTextSize(grInfo.Cache, ex as HeaderObjectInfoArgs, s);

				Size res = ex.InnerElements.CalcMinSize(grInfo.Graphics, ref canDrawMore);
				res.Height = Math.Max(res.Height, captionSize.Height);
				res.Width = Math.Max(res.Width, captionSize.Width);
				res = viewInfo.Painter.ElementsPainter.Column.CalcBoundsByClientRectangle(ex, new Rectangle(Point.Empty, res)).Size;
				grInfo.ReleaseGraphics();
				height = Math.Max(height, res.Height);
			}
			return height;
		}

		private Size CalcCaptionTextSize(GraphicsCache cache, HeaderObjectInfoArgs ee, string caption)
		{
			if (caption.Length > 5)
				caption = caption.Substring(0, caption.Length - 3);
			Size captionSize = ee.Appearance.CalcTextSize(cache, caption, ee.CaptionRect.Width).ToSize();
			return captionSize;
		}

		private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
		{

			GridCellInfo cell = e.Cell as GridCellInfo;

			if (cell.Editor is RepositoryItemCheckEdit)
			{
				e.Handled = false;
				return;
			}
			Rectangle bounds = e.Bounds;
			e.Appearance.FillRectangle(e.Cache, bounds);
			bounds.Inflate(-2, -2);

			StringFormat f = cell.ViewInfo.PaintAppearance.GetStringFormat(TextOptions.DefaultOptionsMultiLine);
			e.Graphics.DrawString(e.DisplayText, cell.ViewInfo.PaintAppearance.Font,
				cell.ViewInfo.PaintAppearance.GetForeBrush(e.Cache), bounds, f);
			e.Handled = true;
		}

		public void RegisterEvents()
		{
			_gridView.CalcRowHeight += gridView1_CalcRowHeight;
			_gridView.CustomDrawCell += gridView1_CustomDrawCell;
		}

		public void UnRegisterEvents()
		{
			_gridView.CalcRowHeight -= gridView1_CalcRowHeight;
			_gridView.CustomDrawCell -= gridView1_CustomDrawCell;
		}

	}
}
