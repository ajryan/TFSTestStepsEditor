using System;
using System.Drawing;
using System.Windows.Forms;

namespace TestStepsEditor.Gui
{
	public class NumberedDataGridView : DataGridView
	{
		public NumberedDataGridView()
		{
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
		}

		protected override void OnRowPostPaint(DataGridViewRowPostPaintEventArgs e)
		{
			
			string rowNumberLabel = (e.RowIndex + 1).ToString();

			while (rowNumberLabel.Length < this.RowCount.ToString().Length)
				rowNumberLabel = "0" + rowNumberLabel;

			//determine the display size of the row number string using the DataGridView's current font.
			SizeF size = e.Graphics.MeasureString(rowNumberLabel, this.Font);

			// if necessary, adjust the width of the column that contains the row header cells 
			if (this.RowHeadersWidth < (int)(size.Width + 20))
				this.RowHeadersWidth = (int)(size.Width + 20);

			Brush controlTextBrush = SystemBrushes.ControlText;
			e.Graphics.DrawString(
				rowNumberLabel, 
				this.Font, 
				controlTextBrush, 
				e.RowBounds.Location.X + 15, 
				e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));

			base.OnRowPostPaint(e);
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			bool rowsSelected = (SelectedRows.Count > 0);

			base.OnKeyDown(e);
			
			if (rowsSelected || e.KeyCode != Keys.Delete)
				return;
			
			foreach (DataGridViewCell cell in SelectedCells)
				cell.Value = String.Empty;
		}
	}
}
