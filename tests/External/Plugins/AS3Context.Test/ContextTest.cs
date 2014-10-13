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
            AS3Context.AS3Settings settings = new AS3Context.AS3Settings();
            AS3Context.Context context = new AS3Context.Context(settings);
            ASCompletion.Context.ASContext.RegisterLanguage(context, "as3");
        }

        [TestCleanup]
        public void Cleanup()
        {
            SingleInstanceApp.Close();
        }
    }
}