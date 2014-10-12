using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASCompletion.Model;
using System.IO;

namespace ASCompletion.Test.Model
{
    [TestClass]
    public class FileModelTest
    {
        [TestMethod]
        public void TestCreateFileModelWithoutFileName()
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
        public void TestCreateFileModelForAS3File()
        {
            FileModel fileModel = new FileModel(PathHelper.as3FileName);
            Assert.IsFalse(fileModel.haXe);
        }

        [TestMethod]
        public void TestCreateFileModelForHaxeFile()
        {
            FileModel fileModel = new FileModel(PathHelper.hxFileName);
            Assert.IsTrue(fileModel.haXe);
        }

        [TestMethod]
        public void TestCreateFileModelWithNullFileName()
        {
            FileModel fileModel = new FileModel(null);
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
    }
}
