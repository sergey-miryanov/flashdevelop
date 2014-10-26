using System;
namespace PluginCore.ScintillaHelper
{
    interface IScintillaView
    {
        void Create(uint dwStyle, int x, int y, int width, int height, IntPtr hWndParent);
        IntPtr Hwnd { get; }
        void DragAcceptFiles(IntPtr hwnd, int accept);
        void Resize(int x, int y, int width, int height);
        bool Focus();
    }
}