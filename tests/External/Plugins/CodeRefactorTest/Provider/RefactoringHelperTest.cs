using FlashDevelop.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PluginCore;
using PluginCore.Utilities;

namespace CodeRefactor.Test.Provider
{
    [TestClass]
    public class RefactoringHelperTest
    {
        private static IMainForm mainForm;

        [TestInitialize]
        public void Initialize()
        {
            mainForm = new MainFormMock();
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