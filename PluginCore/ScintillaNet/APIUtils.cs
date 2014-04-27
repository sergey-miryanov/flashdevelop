using System;

namespace ScintillaNet
{
	public interface APIUtils
	{
		IntPtr LoadLibrary(string fileName);
		//void FreeLibrary(IntPtr handle);
		//IntPtr GetProcAddress(IntPtr dllHandle, string name);
	}
}

