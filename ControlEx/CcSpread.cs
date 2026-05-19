/*
 * 2026-05-17
 */
namespace CcControl {
    public partial class CcSpread : DataGridView {
        private Int32 topHeaderHeight = 24;
        private String[] topHeaderTitles;

        public CcSpread() {
            InitializeSpreadStyle();
        }

        private void InitializeSpreadStyle() {
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToResizeRows = false;
            AllowUserToResizeColumns = false;
            AllowUserToOrderColumns = false;

            RowHeadersWidth = 50;
            ColumnHeadersHeight = 24 + topHeaderHeight;
            EnableHeadersVisualStyles = false;

            SelectionMode = DataGridViewSelectionMode.CellSelect;
            MultiSelect = false;

            GridColor = Color.FromArgb(200, 200, 200);
            BackgroundColor = Color.White;

            ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 220, 255);
            DefaultCellStyle.SelectionForeColor = Color.Black;

            RowTemplate.Height = 22;

            CellPainting += OnCellPainting;
            Paint += OnGridPaint;
            RowPostPaint += OnRowPostPaint;
        }

        public void SetColumns(String[] columnNames, String[] groupTitles) {
            topHeaderTitles = groupTitles;

            Columns.Clear();

            Int32 count = columnNames.Length;

            for (Int32 i = 0; i < count; i++) {
                DataGridViewTextBoxColumn col = new();
                col.HeaderText = columnNames[i];
                col.Width = 100;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                Columns.Add(col);
            }

            Invalidate();
        }

        private void OnGridPaint(Object sender, PaintEventArgs e) {
            if (topHeaderTitles == null)
                return;

            Graphics g = e.Graphics;

            for (Int32 i = 0; i < topHeaderTitles.Length; i++) {
                Int32 colStart = i * 2;
                Int32 colEnd = colStart + 1;

                Rectangle r1 = GetCellDisplayRectangle(colStart, -1, true);
                Rectangle r2 = GetCellDisplayRectangle(colEnd, -1, true);

                Rectangle rect = new(
                    r1.X,
                    0,
                    r2.Right - r1.Left,
                    topHeaderHeight
                );

                using (SolidBrush b = new(Color.FromArgb(230, 230, 230))) {
                    g.FillRectangle(b, rect);
                }

                using (Pen p = new(Color.Gray)) {
                    g.DrawRectangle(p, rect);
                }

                TextRenderer.DrawText(
                    g,
                    topHeaderTitles[i],
                    ColumnHeadersDefaultCellStyle.Font,
                    rect,
                    Color.Black,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                );
            }
        }

        private void OnCellPainting(Object sender, DataGridViewCellPaintingEventArgs e) {
            if (e.RowIndex == -1 && e.ColumnIndex > -1) {
                e.PaintBackground(e.ClipBounds, false);

                Rectangle rect = e.CellBounds;
                rect.Y += topHeaderHeight;
                rect.Height -= topHeaderHeight;

                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), rect);
                e.Graphics.DrawRectangle(Pens.Gray, rect);

                TextRenderer.DrawText(
                    e.Graphics,
                    e.FormattedValue.ToString(),
                    e.CellStyle.Font,
                    rect,
                    Color.Black,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                );

                e.Handled = true;
            }
        }

        private void OnRowPostPaint(Object sender, DataGridViewRowPostPaintEventArgs e) {
            Rectangle rect = new(
                e.RowBounds.Left,
                e.RowBounds.Top,
                RowHeadersWidth,
                e.RowBounds.Height
            );

            TextRenderer.DrawText(
                e.Graphics,
                (e.RowIndex + 1).ToString(),
                RowHeadersDefaultCellStyle.Font,
                rect,
                Color.Black,
                TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter
            );
        }
    }
}
