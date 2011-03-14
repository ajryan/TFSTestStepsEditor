using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TestStepsEditor
{
	public static class Win32
	{
		// P/Invoke constants
		public const int WM_SYSCOMMAND = 0x112;
		public const int MF_STRING = 0x0;
		public const int MF_SEPARATOR = 0x800;

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool AppendMenu(IntPtr hMenu, int uFlags, int uIDNewItem, string lpNewItem);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool InsertMenu(IntPtr hMenu, int uPosition, int uFlags, int uIDNewItem, string lpNewItem);

		[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
		private static extern IntPtr GetFocus();

		public static Control GetFocusedControl()
		{
			Control focusedControl = null;
			
			IntPtr focusedHandle = GetFocus();
			if (focusedHandle != IntPtr.Zero)
				focusedControl = Control.FromHandle(focusedHandle);

			return focusedControl;
		}
	}
}
