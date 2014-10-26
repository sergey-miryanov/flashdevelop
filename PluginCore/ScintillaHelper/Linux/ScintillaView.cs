namespace PluginCore.ScintillaHelper.Linux
{
    class ScintillaView : IScintillaView
    {
        public void Create(uint dwStyle, int x, int y, int width, int height, System.IntPtr hWndParent)
        {
            throw new System.NotImplementedException();
        }

        public System.IntPtr Hwnd
        {
            get { throw new System.NotImplementedException(); }
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