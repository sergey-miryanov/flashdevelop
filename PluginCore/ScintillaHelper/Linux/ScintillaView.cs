using Gtk;
using Sansguerre;
namespace PluginCore.ScintillaHelper.Linux
{
    class ScintillaView : IScintillaView
    {
		Widget widget;

        public void Create(uint dwStyle, int x, int y, int width, int height, System.IntPtr hWndParent)
        {
			widget = new GtkScintilla();
        }

        public System.IntPtr Hwnd
        {
			get { return widget.Handle; }
        }

        public void DragAcceptFiles(System.IntPtr hwnd, int accept)
        {
            throw new System.NotImplementedException();
        }

        public void Resize(int x, int y, int width, int height)
        {
            throw new System.NotImplementedException();
        }

        public bool Focus()
        {
            throw new System.NotImplementedException();
        }
    }
}