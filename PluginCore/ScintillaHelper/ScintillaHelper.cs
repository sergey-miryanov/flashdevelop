namespace PluginCore.ScintillaHelper
{
    class ScintillaHelper
    {
        public static readonly IScintillaView View;

        static ScintillaHelper()
        {
            switch(OSHelper.Platform)
            {
                case System.PlatformID.Win32NT:
                case System.PlatformID.Win32S:
                case System.PlatformID.Win32Windows:
                case System.PlatformID.WinCE:
                    View = new Win.ScintillaView();
                    break;
                case System.PlatformID.MacOSX:
                    //FIXME slavara: IMPLEMENT ME
                    break;
                case System.PlatformID.Unix:
                    View = new Linux.ScintillaView();
                    break;
            }
        }
    }
}