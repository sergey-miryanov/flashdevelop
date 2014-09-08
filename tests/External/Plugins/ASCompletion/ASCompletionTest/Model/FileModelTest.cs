using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASCompletion.Model;
using System.IO;

namespace ASCompletionTest.Model
{
    [TestClass]
    public class FileModelTest
    {
        [TestMethod]
        public void TestCreateEmptyFileModel()
        {
            FileModel fileModel = new FileModel();
            Assert.IsNotNull(fileModel.LastWriteTime);
            Assert.IsFalse(fileModel.haXe);
            Assert.IsTrue(string.IsNullOrEmpty(fileModel.Package));
            Assert.IsTrue(string.IsNullOrEmpty(fileModel.Module));
            Assert.IsNotNull(fileModel.Namespaces);
            Assert.IsNotNull(fileModel.Imports);
            Assert.IsNotNull(fileModel.Classes);
            Assert.IsNotNull(fileModel.Members);
            Assert.IsNotNull(fileModel.Regions);
            Assert.IsNull(fileModel.GetBasePath());
            Assert.AreEqual(ClassModel.VoidClass, fileModel.GetPublicClass());
        }

        [TestMethod]
        public void TestCreateAS3FileModel()
        {
            FileModel fileModel = new FileModel(PathHelper.as3fileName);
            Assert.IsFalse(fileModel.haXe);
        }

        [TestMethod]
        public void TestCreateHaxeFileModel()
        {
            FileModel fileModel = new FileModel(PathHelper.hxFileName);
            Assert.IsTrue(fileModel.haXe);
        }
    }
}
