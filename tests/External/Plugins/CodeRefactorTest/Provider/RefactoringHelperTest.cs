using Microsoft.VisualStudio.TestTools.UnitTesting;
using PluginCore;
using PluginCore.Utilities;

namespace CodeRefactor.Test.Provider
{
    [TestClass]
    public class RefactoringHelperTest
    {
        [TestInitialize]
        public void Initialize()
        {
            IMainForm mainForm = new FlashDevelop.Mock.MainForm();
            SingleInstanceApp.Initialize();
            //PluginBase.MainForm.OpenEditableDocument("../../Resources/AS3TestProj/src/Main.as");
        }

        [TestMethod]
        public void EmptyTest()
        {
            Assert.IsNotNull(PluginBase.MainForm);
        }
    }
}