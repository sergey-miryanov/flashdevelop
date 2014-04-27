using System;

namespace ScintillaNet
{
	public class OSHelper
	{
		public static bool IsLinux() 
		{
			var p = (int) Environment.OSVersion.Platform;
			return (p == 4) || (p == 6) || (p == 128);
		}
	}
}

