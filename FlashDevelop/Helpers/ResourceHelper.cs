using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Resources;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;
using PluginCore.Localization;

namespace FlashDevelop.Helpers
{
    public class ResourceHelper
    {
        /// <summary>
        /// Gets the specified resource as an stream
        /// </summary> 
        public static Stream GetStream(String name)
        {
            String prefix = "FlashDevelop.Resources.";
            Assembly assembly = Assembly.GetExecutingAssembly();
            return assembly.GetManifestResourceStream(prefix + name);
        }

    }

}