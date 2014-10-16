using System;

namespace PluginCore
{
	public interface APIUtils
	{
		IntPtr LoadLibrary(string fileName);
		//void FreeLibrary(IntPtr handle);
		//IntPtr GetProcAddress(IntPtr dllHandle, string name);
		IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
		IntPtr CreateWindowEx(uint dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y, int width, int height, IntPtr hWndParent, int hMenu, IntPtr hInstance, string lpParam);
		int GetScrollPos(IntPtr hWnd, int nBar);
		bool GetScrollRange(IntPtr hWnd, int nBar, out int lpMinPos, out int lpMaxPos);
	}
}