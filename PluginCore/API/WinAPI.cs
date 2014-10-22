using System;
using System.Runtime.InteropServices;

namespace PluginCore
{
	internal class WinAPI : APIUtils
	{
		internal WinAPI()
		{
		}

		IntPtr APIUtils.LoadLibrary(string fileName) 
		{
			return LoadLibrary(fileName);
		}

		IntPtr APIUtils.SendMessage(IntPtr hWnd, int msg, int wParam, int lParam)
		{
			return SendMessage(hWnd, msg, wParam, lParam);
		}

		IntPtr APIUtils.CreateWindowEx(uint dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y, int width, int height, IntPtr hWndParent, int hMenu, IntPtr hInstance, string lpParam)
		{
			return CreateWindowEx(dwExStyle, lpClassName, lpWindowName, dwStyle, x, y, width, height, hWndParent, hMenu, hInstance, lpParam);
		}

		int APIUtils.GetScrollPos(IntPtr hWnd, int nBar)
		{
			return GetScrollPos(hWnd, nBar);
		}

		bool APIUtils.GetScrollRange(IntPtr hWnd, int nBar, out int lpMinPos, out int lpMaxPos)
		{
			return GetScrollRange(hWnd, nBar, out lpMinPos, out lpMaxPos);
		}

        int APIUtils.DragQueryFileA(IntPtr hDrop, uint idx, IntPtr buff, int sz)
        {
            return DragQueryFileA(hDrop, idx, buff, sz);
        }

        int APIUtils.DragFinish(IntPtr hDrop)
        {
            return DragFinish(hDrop);
        }

        void APIUtils.DragAcceptFiles(IntPtr hwnd, int accept)
        {
            DragAcceptFiles(hwnd, accept);
        }

        int APIUtils.GetDeviceCaps(IntPtr hdc, int capindex)
        {
            return GetDeviceCaps(hdc, capindex);
        }

        int APIUtils.SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags)
        {
            return SetWindowPos(hWnd, hWndInsertAfter, X, Y, cx, cy, uFlags);
        }

		[DllImport("kernel32.dll")]
		public extern static IntPtr LoadLibrary(string lpLibFileName);

		[DllImport("user32.dll")]
		public static extern IntPtr CreateWindowEx(uint dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y, int width, int height, IntPtr hWndParent, int hMenu, IntPtr hInstance, string lpParam);

		[DllImport("kernel32.dll", EntryPoint = "SendMessage")]
		public static extern int SendMessageStr(IntPtr hWnd, int message, int data, string s);
		
		[DllImport("user32.dll")]
		public static extern IntPtr SetFocus(IntPtr hwnd);

		[DllImport("user32", CharSet = CharSet.Auto)]
		public static extern bool GetScrollRange(IntPtr hWnd, int nBar, out int lpMinPos, out int lpMaxPos);

		[DllImport("user32.dll")]
		public static extern int GetScrollPos(IntPtr hWnd, int nBar);

		[DllImport("user32", CharSet = CharSet.Auto)] 
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(IntPtr hdc, Int32 capindex);

        [DllImport("user32.dll")]
        public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

        [DllImport("shell32.dll")]
        public static extern int DragQueryFileA(IntPtr hDrop, uint idx, IntPtr buff, int sz);

        [DllImport("shell32.dll")]
        public static extern int DragFinish(IntPtr hDrop);

        [DllImport("shell32.dll")]
        public static extern void DragAcceptFiles(IntPtr hwnd, int accept);
    }
}