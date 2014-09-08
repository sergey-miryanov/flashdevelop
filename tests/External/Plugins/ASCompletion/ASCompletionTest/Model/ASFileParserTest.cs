using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASCompletion.Model;
using System.IO;
using System.Diagnostics;

namespace ASCompletionTest.Model
{
    [TestClass]
    public class ASFileParserTest
    {
        [TestMethod]
        public void TestParseAS3File()
        {
            FileModel fileModel = ASFileParser.ParseFile(new FileModel(PathHelper.as3fileName));
            Assert.IsFalse(fileModel.haXe);
            Assert.AreEqual(3, fileModel.Version);
            Assert.IsTrue(fileModel.HasPackage);
            Assert.AreEqual("flashdevelop.tests", fileModel.Package);
            Assert.IsTrue(string.IsNullOrEmpty(fileModel.Module));
            Assert.AreEqual(0, fileModel.Namespaces.Count);
            Assert.AreEqual(0, fileModel.Imports.Count);
            Assert.AreEqual(0, fileModel.Members.Count);
            Assert.AreEqual(0, fileModel.Regions.Count);
            Assert.AreEqual(1, fileModel.Classes.Count);
            ClassModel classModel = fileModel.GetPublicClass();
            Assert.AreNotEqual(ClassModel.VoidClass, classModel);
            Assert.AreEqual(Visibility.Public, classModel.Access);
            Assert.AreEqual("TestClass", classModel.Name);
            Assert.AreEqual(ClassModel.VoidClass, classModel.Extends);
            Assert.IsNotNull(classModel.Members);
            Assert.AreEqual(1, classModel.Members.Count);
        }

        [TestMethod]
        public void TestParseHaxeFile()
        {
            FileModel fileModel = ASFileParser.ParseFile(new FileModel(PathHelper.hxFileName));
            Assert.IsTrue(fileModel.haXe);
            Assert.AreEqual(4, fileModel.Version);
            Assert.IsTrue(fileModel.HasPackage);
            Assert.AreEqual("flashdevelop.tests", fileModel.Package);
            Assert.IsTrue(string.IsNullOrEmpty(fileModel.Module));
            Assert.AreEqual(0, fileModel.Namespaces.Count);
            Assert.AreEqual(0, fileModel.Imports.Count);
            Assert.AreEqual(0, fileModel.Members.Count);
            Assert.AreEqual(0, fileModel.Regions.Count);
            Assert.AreEqual(1, fileModel.Classes.Count);
            ClassModel classModel = fileModel.GetPublicClass();
            Assert.AreNotEqual(ClassModel.VoidClass, classModel);
            //Assert.AreEqual(Visibility.Public, classModel.Access);
            Assert.AreEqual("TestClass", classModel.Name);
            Assert.AreEqual(ClassModel.VoidClass, classModel.Extends);
            Assert.IsNotNull(classModel.Members);
            Assert.AreEqual(1, classModel.Members.Count);
        }
    }
}