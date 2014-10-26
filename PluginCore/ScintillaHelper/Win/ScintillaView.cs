using System;
namespace PluginCore.ScintillaHelper.Win
{
    class ScintillaView : IScintillaView
    {
        IntPtr hwnd;

        public void Create(uint dwStyle, int x, int y, int width, int height, IntPtr Handle)
        {
            hwnd = OSHelper.API.CreateWindowEx(0, "Scintilla", "", dwStyle, x, y, width, height, Handle, 0, new IntPtr(0), null);
        }

        public IntPtr Hwnd { get { return hwnd; } }

        public void DragAcceptFiles(IntPtr hwnd, int accept)
        {
            OSHelper.API.DragAcceptFiles(hwnd, accept);
        }

        public void Resize(int x, int y, int width, int height)
        {
            OSHelper.API.SetWindowPos(hwnd, 0, x, y, width, height, 0);
        }

        public bool Focus()
        {
            return WinAPI.SetFocus(hwnd) != IntPtr.Zero;
        }
    }
}