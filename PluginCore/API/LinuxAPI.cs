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

        public int DragQueryFileA(IntPtr hDrop, uint idx, IntPtr buff, int sz)
        {
            // FIXME slavara: IMPLEMENT ME
            return -1;
        }

        public int DragFinish(IntPtr hDrop)
        {
            // FIXME slavara: IMPLEMENT ME
            return -1;
        }

        public void DragAcceptFiles(IntPtr hwnd, int accept)
        {
            // FIXME slavara: IMPLEMENT ME
        }

        public int GetDeviceCaps(IntPtr hdc, int capindex)
        {
            // FIXME slavara: IMPLEMENT ME
            return -1;
        }

        public int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags)
        {
            // FIXME slavara: IMPLEMENT ME
            return -1;
        }

		[DllImport("libdl.so")]
		private static extern IntPtr dlopen(String fileName, int flags);
    }
}