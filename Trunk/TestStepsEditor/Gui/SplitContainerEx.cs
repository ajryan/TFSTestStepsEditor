using System;
using System.Drawing;
using System.Windows.Forms;

namespace TestStepsEditor.Gui
{
	public class SplitContainerEx : SplitContainer
	{
		private int _mouseDownSplitterDistance;
		private int _beforeHideSplitterDistance;

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Point[] dotPoints = GetDotPoints();

			foreach (var dotPoint in dotPoints)
			{
				dotPoint.Offset(-2, -2);
				e.Graphics.FillEllipse(SystemBrushes.ControlDark,
					new Rectangle(dotPoint, new Size(4, 4)));

				dotPoint.Offset(1, 1);
				e.Graphics.FillEllipse(SystemBrushes.ControlLight,
					new Rectangle(dotPoint, new Size(4, 4)));
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			_mouseDownSplitterDistance = SplitterDistance;
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			if (_mouseDownSplitterDistance == SplitterDistance)
			{
				if (Panel2.Height > 1)
				{
					_beforeHideSplitterDistance = SplitterDistance;
					SplitterDistance = Height - SplitterWidth;
				}
				else
				{
					SplitterDistance = _beforeHideSplitterDistance;
				}
			}

			_mouseDownSplitterDistance = SplitterDistance;
		}

		private Point[] GetDotPoints()
		{
			var dotPoints = new Point[3];

			dotPoints[0] = GetDotsCenter();

			if (Orientation == Orientation.Horizontal)
			{
				dotPoints[1] = new Point(dotPoints[0].X - 10, dotPoints[0].Y);
				dotPoints[2] = new Point(dotPoints[0].X + 10, dotPoints[0].Y);
			}
			else
			{
				dotPoints[1] = new Point(dotPoints[0].X, dotPoints[0].Y - 10);
				dotPoints[2] = new Point(dotPoints[0].X, dotPoints[0].Y + 10);
			}

			return dotPoints;
		}

		private Point GetDotsCenter()
		{
			if (Orientation == Orientation.Horizontal)
			{
				return new Point((Width / 2), SplitterDistance + (SplitterWidth / 2));
			}
			else
			{
				return new Point(SplitterDistance + (SplitterWidth / 2), (Height / 2));
			}
		}
	}
}
