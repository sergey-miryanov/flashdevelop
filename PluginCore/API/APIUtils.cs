using System;

namespace PluginCore
{
	public interface APIUtils
	{
		//void FreeLibrary(IntPtr handle);
		//IntPtr GetProcAddress(IntPtr dllHandle, string name);
		IntPtr CreateWindowEx(uint dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y, int width, int height, IntPtr hWndParent, int hMenu, IntPtr hInstance, string lpParam);
        int DragQueryFileA(IntPtr hDrop, uint idx, IntPtr buff, int sz);
        int DragFinish(IntPtr hDrop);
        void DragAcceptFiles(IntPtr hwnd, int accept);
        int GetDeviceCaps(IntPtr hdc, int capindex);
		int GetScrollPos(IntPtr hWnd, int nBar);
		bool GetScrollRange(IntPtr hWnd, int nBar, out int lpMinPos, out int lpMaxPos);
		IntPtr LoadLibrary(string fileName);
		IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);
	}
}