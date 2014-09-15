using FlashDevelopMock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PluginCore;
using PluginCore.Utilities;

namespace CodeRefactorTest.Provider
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
        }

        [TestMethod]
        public void EmptyTest()
        {
            Assert.IsNotNull(PluginBase.MainForm);
        }
    }
}