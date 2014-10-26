using System;
using System.Runtime.InteropServices;

namespace PluginCore
{
	public static class OSHelper
	{
		public static readonly APIUtils API;

		static OSHelper()
		{
			switch (Platform)
			{
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					API = new WinAPI();
					break;
				case PlatformID.MacOSX:
					API = new MacAPI();
					break;
				case PlatformID.Unix:
					API = new LinuxAPI();
					break;
				default: throw new Exception(Environment.OSVersion.ToString());
			}
		}

        // Summary:
        //     Gets a System.PlatformID enumeration value that identifies the operating
        //     system platform.
        //
        // Returns:
        //     One of the System.PlatformID values.
        public static PlatformID Platform
        {
            get
            {
                PlatformID id = Environment.OSVersion.Platform;
                switch (id)
                {
                    case PlatformID.Unix: return IsRunningOnMac() ? PlatformID.MacOSX : PlatformID.Unix;
                    default: return id;
                }
            }
        }

		[DllImport("libc")]
		static extern int uname(IntPtr buf);

		// Mono returns PlatformID.Unix for MacOSX so we should test platform by other ways
		// "Notice that as of Mono 2.2 the version returned on MacOS X is still 4 for legacy reasons, 
		//  too much code was written between the time that the MacOSX value was introduced and the 
		//  time that we wrote this text which has lead to a lot of user code in the wild to not cope with 
		//  the newly introduced value."
		// http://www.mono-project.com/docs/faq/technical/
		public static bool IsRunningOnMac()
		{
			IntPtr buf = IntPtr.Zero;
			try
			{
				buf = Marshal.AllocHGlobal(8192);
				if (uname(buf) == 0)
				{
					string os = Marshal.PtrToStringAnsi(buf);
					return os.Contains("Darwin");
				}
			}
			catch
			{
				// don't do anything
			}
			finally
			{
				if (buf != IntPtr.Zero) Marshal.FreeHGlobal(buf);
			}
			return false;
		}
	}
}