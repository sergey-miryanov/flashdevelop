using System;
using System.IO;
using System.Runtime.InteropServices;

namespace PluginCore
{
	public static class OSHelper
	{
		public static readonly APIUtils API;

		static OSHelper()
		{
			switch (Environment.OSVersion.Platform)
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
	}
}

