using ASCompletion.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASCompletion.Test.Model
{
    [TestClass]
    public class ASFileParserTest
    {

        #region AS3

        [TestMethod]
        public void TestParseAS3File()
        {
            FileModel fileModel = ASFileParser.ParseFile(new FileModel(PathHelper.as3FileName));
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
            Assert.AreEqual("TestClass", classModel.Name);
            Assert.AreEqual(Visibility.Public, classModel.Access);
            Assert.AreEqual(ClassModel.VoidClass, classModel.Extends);
            Assert.IsNotNull(classModel.Members);
            Assert.AreEqual(1, classModel.Members.Count);
        }

        [TestMethod]
        public void TestParseAS3FileWithPrivateClass()
        {
            FileModel fileModel = ASFileParser.ParseFile(new FileModel(PathHelper.as3FileWithPrivateClass));
            Assert.AreEqual(2, fileModel.Classes.Count);
            ClassModel classModel = fileModel.GetClassByName("PrivateClass");
            Assert.IsNotNull(classModel);
            Assert.AreEqual(Visibility.Private, classModel.Access);
        }

        [TestMethod]
        public void TestParseAS3FileWithCustomNamespace()
        {
            FileModel fileModel = ASFileParser.ParseFile(new FileModel(PathHelper.as3FileWithCustomNamespace));
            ClassModel classModel = fileModel.GetClassByName("TestClass");
            Assert.AreEqual(1, classModel.Members.Count);
            MemberModel member = classModel.Members[0];
            Assert.AreEqual("test", member.Name);
            Assert.AreEqual((uint)0, (uint)member.Access);
            Assert.AreEqual("$private", member.Namespace);
        }

        [TestMethod]
        public void TestParseAS3FileWithCustomNamespaces()
        {
            FileModel fileModel = ASFileParser.ParseFile(new FileModel(PathHelper.as3FileWithCustomNamespaces));
            ClassModel classModel = fileModel.GetClassByName("TestClass");
            Assert.AreEqual(2, classModel.Members.Count);
            MemberModel member0 = classModel.Members[0];
            MemberModel member1 = classModel.Members[1];
            Assert.IsFalse(member0.Equals(member1));
        }

        [TestMethod]
        public void TestParserAS3FileWithUserObjectClass()
        {
            FileModel fileModel = ASFileParser.ParseFile(new FileModel(PathHelper.as3FileWithUserObjectClass));
            ClassModel classModel = fileModel.GetClassByName("Object");
            Assert.IsNotNull(classModel);
            Assert.AreEqual(1, classModel.Members.Count);
            Assert.AreEqual("Object", classModel.Members[0].Name);
        }

        #endregion

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
            Assert.AreEqual(ClassModel.VoidClass, classModel);
            classModel = fileModel.GetClassByName("TestClass");
            Assert.AreNotEqual(ClassModel.VoidClass, classModel);
            Assert.AreEqual(Visibility.Public, classModel.Access);
            Assert.AreEqual(ClassModel.VoidClass, classModel.Extends);
            Assert.IsNotNull(classModel.Members);
            Assert.AreEqual(1, classModel.Members.Count);
        }

        [TestMethod]
        public void TestParseHaxeFileWithPrivateClass()
        {
            FileModel fileModel = ASFileParser.ParseFile(new FileModel(PathHelper.hxFileWithPrivateClass));
            Assert.AreEqual(2, fileModel.Classes.Count);
            ClassModel classModel = fileModel.GetClassByName("PrivateClass");
            Assert.IsNotNull(classModel);
            Assert.AreEqual(Visibility.Private, classModel.Access);
        }
    }
}