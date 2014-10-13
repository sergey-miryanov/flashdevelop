using Microsoft.VisualStudio.TestTools.UnitTesting;
using PluginCore.Utilities;

namespace CodeRefactor.Test.Provider
{
    [TestClass]
    public class RefactoringHelperTest
    {
        [TestInitialize]
        public void Initialize()
        {
            FlashDevelop.Mock.MainForm mainForm = new FlashDevelop.Mock.MainForm();
            SingleInstanceApp.Initialize();
        }

        [TestCleanup]
        public void Cleanup()
        {
            SingleInstanceApp.Close();
        }

    }
}