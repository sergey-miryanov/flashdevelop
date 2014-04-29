
using System;
using System.Runtime.InteropServices;

namespace PluginCore
{
	public class MacAPI : APIUtils
	{
		public MacAPI()
		{
		}

		public IntPtr LoadLibrary(string fileName) 
		{
			return dlopen(fileName, RTLD_NOW);
		}

		const int RTLD_NOW = 2;

		[DllImport("libdl.so")]
		private static extern IntPtr dlopen(String fileName, int flags);
	}
}

