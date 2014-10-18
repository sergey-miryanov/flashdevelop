using System;
using System.Runtime.InteropServices;

namespace PluginCore
{
	internal class LinuxAPI : APIUtils
	{
		const int RTLD_NOW = 2;

		internal LinuxAPI()
		{
		}

		public IntPtr LoadLibrary(string fileName)
		{
			return dlopen(fileName, RTLD_NOW);
		}

		IntPtr APIUtils.SendMessage(IntPtr hWnd, int msg, int wParam, int lParam)
		{
			// FIXME: NOT IMPL
			return new IntPtr();
		}

		public IntPtr CreateWindowEx(uint dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y, int width, int height, IntPtr hWndParent, int hMenu, IntPtr hInstance, string lpParam)
		{
			// FIXME slavara: IMPLEMENT ME
			return new IntPtr();
		}

		int APIUtils.GetScrollPos(IntPtr hWnd, int nBar)
		{
			// FIXME: NOT IMPL
			return -1;
		}

		bool APIUtils.GetScrollRange(IntPtr hWnd, int nBar, out int lpMinPos, out int lpMaxPos)
		{
			// FIXME: NOT IMPL
			lpMinPos = 0;
			lpMaxPos = 0;
			return false;
		}

		[DllImport("libdl.so")]
		private static extern IntPtr dlopen(String fileName, int flags);

		[GLib.Signal("create_window")]
		public event CreateWindowHandler CreateWindow();

	}
}