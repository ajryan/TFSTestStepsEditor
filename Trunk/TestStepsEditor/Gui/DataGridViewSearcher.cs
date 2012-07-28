using System;
using System.Windows.Forms;

namespace TestStepsEditor.Gui
{
	public class DataGridViewSearcher
	{
		public enum Result
		{
			Aborted,
			Matched,
			NotMatched
		}

		private readonly DataGridView _grid;

		public DataGridViewSearcher(DataGridView grid)
		{
			_grid = grid;
		}

		public Result Search(string findText)
		{
			if (_grid == null || _grid.CurrentCell == null || String.IsNullOrEmpty(findText))
				return Result.Aborted;

			bool found = PerformOnCells(
				_grid.CurrentCell,
				false,
				true,
				cell =>
				{
					if (CellContainsText(cell, findText))
					{
						_grid.CurrentCell = cell;
						_grid.BeginEdit(false);

						string cellText = cell.Value as string;
						int foundTextIndex = cellText.IndexOf(findText, StringComparison.OrdinalIgnoreCase);

						(_grid.EditingControl as TextBox).SelectionStart = foundTextIndex;
						(_grid.EditingControl as TextBox).SelectionLength = findText.Length;

						return true;
					}
					return false;
				});

			return found ? Result.Matched : Result.NotMatched;
		}

		public Result Replace(string findText, string replaceText, bool selectedOnly)
		{
			if (_grid == null || _grid.RowCount < 1 || String.IsNullOrEmpty(findText))
				return Result.Aborted;

			bool replaced = PerformOnCells(
				_grid[0, 0],	// process entire grid
				true,			// include start cell
				false,			// do not stop on first action
				cell =>
				{
					if (selectedOnly && !cell.Selected)
						return false;

					if (CellContainsText(cell, findText))
					{
						string currentText = cell.Value.ToString();
						cell.Value = currentText.Replace(findText, replaceText);
						return true;
					}
					return false;
				});

			return replaced ? Result.Matched : Result.NotMatched;
		}

		/// <summary>
		/// Perform a function on grid cells.
		/// </summary>
		/// <param name="startCell">Cell from which to start. Does not wrap to start.</param>
		/// <param name="processStartCell">Whether to apply <see cref="function"/> to <see cref="startCell"/></param>
		/// <param name="stopOnFirstAction">Whether to halt processing the first time <see cref="function"/> returns true</param>
		/// <param name="function">Function to perform on the cell. Processing stops when this function returns true.</param>
		/// <returns>Whether the function ever returned true.</returns>
		public bool PerformOnCells(DataGridViewCell startCell, bool processStartCell, bool stopOnFirstAction, Func<DataGridViewCell, bool> function)
		{
			if (_grid == null || _grid.RowCount == 0 || _grid.ColumnCount == 0)
				return false;

			bool performed = false;

			int initialRow = startCell == null ? 0 : startCell.RowIndex;
			int initialCol = startCell == null ? 0 : startCell.ColumnIndex;

			int startRow = initialCol == 0 ? initialRow : initialRow + 1;

			for (int rowIndex = startRow; rowIndex < _grid.Rows.Count; rowIndex++)
			{
				DataGridViewRow row = _grid.Rows[rowIndex];

				int startCol = (rowIndex == initialRow && !processStartCell && _grid.ColumnCount > 1) ? 1 : 0;
				for (int colIndex = startCol; colIndex < row.Cells.Count; colIndex++)
				{
					DataGridViewCell cell = row.Cells[colIndex];
					performed |= function(cell);

					if (performed && stopOnFirstAction)
						return true;
				}
			}

			return performed;
		}

		private static bool CellContainsText(DataGridViewCell cell, string text)
		{
			return cell.Value != null && cell.Value.ToString().ToUpper().Contains(text.ToUpper());
		}
	}
}
