using Microsoft.VisualStudio.TestTools.UnitTesting;
using PluginCore;
using PluginCore.Utilities;
using FlashDevelop.Mock;

namespace CodeRefactor.Test.Provider
{
    [TestClass]
    public class RefactoringHelperTest
    {
        [TestInitialize]
        public void Initialize()
        {
            MainForm mainForm = new MainForm();
            SingleInstanceApp.Initialize();
            PluginBase.MainForm.OpenEditableDocument("../../Resources/AS3TestProj/src/Main.as");
        }

        [TestMethod]
        public void EmptyTest()
        {
            Assert.IsNotNull(PluginBase.MainForm);
        }
    }
}