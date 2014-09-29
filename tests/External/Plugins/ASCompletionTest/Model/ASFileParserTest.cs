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
            Assert.AreEqual(fileModel.Version, 3);
            Assert.IsTrue(fileModel.HasPackage);
            Assert.AreEqual(fileModel.Package, "flashdevelop.tests");
            Assert.IsTrue(string.IsNullOrEmpty(fileModel.Module));
            Assert.AreEqual(fileModel.Namespaces.Count, 0);
            Assert.AreEqual(fileModel.Imports.Count, 0);
            Assert.AreEqual(fileModel.Members.Count, 0);
            Assert.AreEqual(fileModel.Regions.Count, 0);
            Assert.AreEqual(fileModel.Classes.Count, 1);
            ClassModel classModel = fileModel.GetPublicClass();
            Assert.AreNotEqual(classModel, ClassModel.VoidClass);
            Assert.AreEqual(classModel.Name, "TestClass");
            Assert.AreEqual(classModel.Access, Visibility.Public);
            Assert.AreEqual(classModel.Extends, ClassModel.VoidClass);
            Assert.IsNotNull(classModel.Members);
            Assert.AreEqual(classModel.Members.Count, 1);
        }

        [TestMethod]
        public void TestParseAS3FileWithPrivateClass()
        {
            FileModel fileModel = ASFileParser.ParseFile(new FileModel(PathHelper.as3FileWithPrivateClass));
            Assert.AreEqual(2, fileModel.Classes.Count);
            ClassModel classModel = fileModel.GetClassByName("PrivateClass");
            Assert.IsNotNull(classModel);
            Assert.AreEqual(classModel.Access, Visibility.Private);
        }

        [TestMethod]
        public void TestParseAS3FileWithCustomNamespace()
        {
            FileModel fileModel = ASFileParser.ParseFile(new FileModel(PathHelper.as3FileWithCustomNamespace));
            ClassModel classModel = fileModel.GetClassByName("TestClass");
            Assert.AreEqual(1, classModel.Members.Count);
            MemberModel member = classModel.Members[0];
            Assert.AreEqual(member.Name, "test");
            Assert.AreEqual((uint)member.Access, (uint)0);
            Assert.AreEqual(member.Namespace, "$private");
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
            Assert.AreEqual(classModel.Members[0].Name, "Object");
        }

        #endregion

        [TestMethod]
        public void TestParseHaxeFile()
        {
            FileModel fileModel = ASFileParser.ParseFile(new FileModel(PathHelper.hxFileName));
            Assert.IsTrue(fileModel.haXe);
            Assert.AreEqual(fileModel.Version, 4);
            Assert.IsTrue(fileModel.HasPackage);
            Assert.AreEqual(fileModel.Package, "flashdevelop.tests");
            Assert.IsTrue(string.IsNullOrEmpty(fileModel.Module));
            Assert.AreEqual(fileModel.Namespaces.Count, 0);
            Assert.AreEqual(fileModel.Imports.Count, 0);
            Assert.AreEqual(fileModel.Members.Count, 0);
            Assert.AreEqual(fileModel.Regions.Count, 0);
            Assert.AreEqual(fileModel.Classes.Count, 1);
            ClassModel classModel = fileModel.GetPublicClass();
            Assert.AreEqual(classModel, ClassModel.VoidClass);
            classModel = fileModel.GetClassByName("TestClass");
            Assert.AreNotEqual(classModel, ClassModel.VoidClass);
            Assert.AreEqual(classModel.Access, Visibility.Public);
            Assert.AreEqual(classModel.Extends, ClassModel.VoidClass);
            Assert.IsNotNull(classModel.Members);
            Assert.AreEqual(classModel.Members.Count, 1);
        }

        [TestMethod]
        public void TestParseHaxeFileWithPrivateClass()
        {
            FileModel fileModel = ASFileParser.ParseFile(new FileModel(PathHelper.hxFileWithPrivateClass));
            Assert.AreEqual(2, fileModel.Classes.Count);
            ClassModel classModel = fileModel.GetClassByName("PrivateClass");
            Assert.IsNotNull(classModel);
            Assert.AreEqual(classModel.Access, Visibility.Private);
        }
    }
}