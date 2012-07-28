﻿using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TestStepsEditor.Gui
{
	public class ListViewEx : ListView
	{
		[StructLayout(LayoutKind.Sequential)]
		private struct NMHDR
		{
			public IntPtr hwndFrom;
			public uint idFrom;
			public uint code;
		}

		private const uint NM_CUSTOMDRAW = unchecked((uint)-12);

		public ListViewEx()
		{

			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
		}

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 0x204E)
			{
				NMHDR hdr = (NMHDR)m.GetLParam(typeof(NMHDR));
				if (hdr.code == NM_CUSTOMDRAW)
				{
					m.Result = (IntPtr)0;
					return;
				}
			}

			base.WndProc(ref m);
		}
	}
}
