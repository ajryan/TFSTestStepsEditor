using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestStepsEditor
{
	public class NumberedDataGridView : DataGridView
	{
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
	}
}
