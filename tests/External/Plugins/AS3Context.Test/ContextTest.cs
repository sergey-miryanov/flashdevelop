using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PluginCore.Utilities;

namespace AS3Context.Test
{
    [TestClass]
    public class ContextTest
    {
        [TestInitialize]
        public void Initialize()
        {
            FlashDevelop.Mock.MainForm mainForm = new FlashDevelop.Mock.MainForm();
            SingleInstanceApp.Initialize();
            ASCompletion.PluginMain pluginMain = new ASCompletion.PluginMain();
            pluginMain.Initialize();
            ASCompletion.Context.ASContext.RegisterLanguage(new AS3Context.Context(), "as3");
        }
    }
}
