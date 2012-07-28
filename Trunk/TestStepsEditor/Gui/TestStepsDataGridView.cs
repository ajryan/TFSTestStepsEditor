using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Microsoft.TeamFoundation.TestManagement.Client;
using TestStepsEditor.Tfs;

namespace TestStepsEditor.Gui
{
	public enum InsertLocation
	{
		Above,
		Below
	}

	public class TestStepsDataGridView : NumberedDataGridView
	{
		public const int TITLE_COLUMN = 0;
		public const int EXPECTED_RESULT_COLUMN = 1;
		public const int OUTCOME_COLUMN = 2;

		private readonly SimpleSteps _simpleSteps;

		public static TestStepsDataGridView Create(SimpleSteps simpleSteps, ContextMenuStrip contextMenuStrip)
		{
			return new TestStepsDataGridView(simpleSteps)
			{
				Dock = DockStyle.Fill,
				AllowUserToAddRows = false,
				AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders,
				ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText,
				ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
				ContextMenuStrip = contextMenuStrip,
				DefaultCellStyle = new DataGridViewCellStyle
				{
					Alignment = DataGridViewContentAlignment.TopLeft,
					WrapMode = DataGridViewTriState.True
				}
			};
		}

		public TestStepsDataGridView()
		{
			Debug.Assert(DesignMode);
		}

		public TestStepsDataGridView(SimpleSteps simpleSteps)
		{
			_simpleSteps = simpleSteps;

			DataError += TestStepsDataGridView_DataError;
		}

		void TestStepsDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			e.Cancel = true;
			Trace.WriteLine(e.Exception.ToString());
		}

		public void LoadSteps(bool outcomeColumnVisible)
		{
			SuspendLayout();

			AutoGenerateColumns = false;
			DataSource = _simpleSteps;

			// title
			Columns.Add(
				new DataGridViewTextBoxColumn
				{
					Name = "Title",
					DataPropertyName = "Title",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
				});

			// expected result
			Columns.Add(
				new DataGridViewTextBoxColumn
				{
					Name = "Expected Result",
					DataPropertyName = "ExpectedResult",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
				});

			// outcome
			Columns.Add(
				new DataGridViewComboBoxColumn
				{
					Name = "Outcome",
					DataPropertyName = "Outcome",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
					Width = 85,
					DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing,
					DataSource = new List<TestOutcome> { TestOutcome.Inconclusive, TestOutcome.Passed, TestOutcome.Failed },
					Visible = outcomeColumnVisible
				});

			ResumeLayout(true);
		}

		public void InsertStep(DataGridViewRow insertRow, InsertLocation location)
		{
			int insertRowIndex = insertRow == null ? -1 : insertRow.Index;

			if (location == InsertLocation.Below && insertRowIndex != -1)
				insertRowIndex++;

			int currentCol = insertRowIndex == -1 ? 0 : CurrentCell.ColumnIndex;

			if (insertRowIndex != -1)
			{
				_simpleSteps.Insert(insertRowIndex, new SimpleStep());
			}
			else
			{
				_simpleSteps.AddNew();
				insertRowIndex = 0;
			}

			CurrentCell = this[currentCol, insertRowIndex];
			Focus();
		}

		public void CopyToClipboard()
		{
			// deselect pass/fail column before copying
			foreach (DataGridViewCell cell in SelectedCells)
			{
				if (cell.ColumnIndex > EXPECTED_RESULT_COLUMN)
					cell.Selected = false;
			}

			DataObject clipboardData = GetClipboardContent();
			if (clipboardData != null)
				Clipboard.SetDataObject(clipboardData);
		}

		public void PasteClipboard()
		{
			string clipboardText = Clipboard.GetText();
			if (String.IsNullOrWhiteSpace(clipboardText))
				return;

			int selectedCount = GetCellCount(DataGridViewElementStates.Selected);

			if (selectedCount == 0 && CurrentCell == null)
				return;

			int rowIndex = CurrentCell.RowIndex;
			int colIndex = CurrentCell.ColumnIndex;

			if (selectedCount > 1)
			{
				List<DataGridViewCell> selectedCells = new List<DataGridViewCell>(selectedCount);
				selectedCells.AddRange(SelectedCells.Cast<DataGridViewCell>());

				rowIndex = selectedCells.Min(c => c.RowIndex);
				colIndex = selectedCells.Min(c => c.ColumnIndex);
			}

			string[] clipboardLines = clipboardText.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
			foreach (string clipboardLine in clipboardLines)
			{
				bool addedRow = false;
				if (rowIndex == RowCount)
				{
					_simpleSteps.AddNew();
					addedRow = true;
				}

				if (rowIndex >= RowCount || clipboardLine.Length <= 0)
					break;

				string[] clipboardLineTokens = clipboardLine.Split('\t');
				for (int tokenIndex = 0; tokenIndex < clipboardLineTokens.Length; ++tokenIndex)
				{
					// do not paste into pass/fail column
					if (colIndex + tokenIndex > EXPECTED_RESULT_COLUMN)
						break;

					DataGridViewCell targetCell = this[colIndex + tokenIndex, rowIndex];

					// done pasting when we reach an unselected cell, except when we added the row
					if (!targetCell.Selected && !addedRow)
						return;

					if (targetCell.Value == null ||
						(targetCell.Value.ToString() != clipboardLineTokens[tokenIndex]))
					{
						targetCell.Value = Convert.ChangeType(
							clipboardLineTokens[tokenIndex], targetCell.ValueType);
					}
				}

				rowIndex++;
			}
		}

		public void DeleteCurrentRow()
		{
			if (CurrentRow == null)
				return;

			DeleteRows(new List<int> { CurrentRow.Index });
		}

		public void DeleteRows(List<int> rowIndexes)
		{
			int currentCol = CurrentCell.ColumnIndex;

			foreach (int removeIndex in rowIndexes.OrderByDescending(r => r))
				_simpleSteps.RemoveAt(removeIndex);

			if (RowCount == 0)
				return;

			if (RowCount > 0)
			{
				CurrentCell = this[
					Math.Max(TITLE_COLUMN, currentCol),
					Math.Min(RowCount - 1, rowIndexes.Min())];
			}

			Focus();
		}

		public void ShowOutcomeColumn()
		{
			Columns[OUTCOME_COLUMN].Visible = true;
		}

		public void HideOutcomeColumn()
		{
			Columns[OUTCOME_COLUMN].Visible = false;
		}


		protected override void OnCurrentCellDirtyStateChanged(EventArgs e)
		{
			if (CurrentCell.ColumnIndex == OUTCOME_COLUMN)
				CommitEdit(DataGridViewDataErrorContexts.Commit);
		}
		
		protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
		{
			// when clicked a row header of a non-selected row, select the row
			if (e.RowIndex > -1 && e.ColumnIndex == -1 && !Rows[e.RowIndex].Selected)
			{
				bool controlPressed = (ModifierKeys & Keys.Control) != 0;
				bool shiftPressed = (ModifierKeys & Keys.Shift) != 0;

				if (!controlPressed && !shiftPressed)
					ClearSelection();

				if (shiftPressed)
				{
					int direction = e.RowIndex < CurrentRow.Index ? -1 : 1;
					for (int rowIndex = CurrentRow.Index; rowIndex != e.RowIndex; rowIndex += direction)
						Rows[rowIndex].Selected = true;
				}
				Rows[e.RowIndex].Selected = true;
				
				if (!controlPressed && !shiftPressed)
					CurrentCell = this[0, e.RowIndex];

				return;
			}

			base.OnCellMouseDown(e);

			// when clicked the outcome combobox, drop down the combo
			if (e.RowIndex > -1 &&
				e.ColumnIndex == OUTCOME_COLUMN &&
				GetCellCount(DataGridViewElementStates.Selected) == 1)
			{
				BeginEdit(true);
				((DataGridViewComboBoxEditingControl)EditingControl).DroppedDown = true;
			}
		}
	}
}
