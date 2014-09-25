using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASCompletion.Model;
using PluginCore.Utilities;
using ASCompletion.Completion;

namespace ASCompletion.Test.Model
{
    [TestClass]
    public class MemberModelTest
    {
        public AS3Context.Context as3context;

        [TestInitialize]
        public void Initialize()
        {
            AS3Context.AS3Settings settings = new AS3Context.AS3Settings();
            as3context = new AS3Context.Context(settings);
        }

        [TestMethod]
        public void TestCreateEmptyMemberModel()
        {
            MemberModel m = new MemberModel();
            Assert.AreEqual((uint)m.Access, (uint)0);
            Assert.IsNull(m.Comments);
            Assert.AreEqual((uint)m.Flags, (uint)0);
            Assert.IsNull(m.InFile);
            Assert.IsFalse(m.IsPackageLevel);
            Assert.AreEqual(m.LineFrom, 0);
            Assert.AreEqual(m.LineTo, 0);
            Assert.IsNull(m.Name);
            Assert.IsNull(m.Namespace);
            Assert.IsNull(m.Template);
            Assert.IsNull(m.Type);
            Assert.IsNull(m.Value);
            Assert.IsNull(m.Parameters);
            Assert.AreEqual(m.ParametersString(), "");
            Assert.AreEqual(m.ToDeclarationString(), "");
        }

        [TestMethod]
        public void TestCreateAS3VarMemberModel()
        {
            PrivateObject privateObject = new PrivateObject(as3context);
            ContextFeatures features = (ContextFeatures)privateObject.GetField("features");
            MemberModel m = new MemberModel("foo", features.numberKey, FlagType.Variable, Visibility.Private);
            m.Value = "10.0";
            Assert.AreEqual(m.Name, "foo");
            Assert.AreEqual(m.FullName, m.Name);
            Assert.AreEqual(m.Type, features.numberKey);
            Assert.AreEqual(m.Flags, FlagType.Variable);
            Assert.AreEqual(m.Access, Visibility.Private);
            Assert.AreEqual(m.ToString(), "foo : " + features.numberKey);
            Assert.AreEqual(m.ToDeclarationString(), m.ToDeclarationString(true, false));
            Assert.AreEqual(m.ToDeclarationString(true, false), "foo : " + features.numberKey);
            Assert.AreEqual(m.ToDeclarationString(true, true), "foo : " + features.numberKey + " = 10.0");
            Assert.AreEqual(m.ToDeclarationString(false, false), "foo:" + features.numberKey);
            Assert.AreEqual(m.ToDeclarationString(false, true), "foo:" + features.numberKey + "=10.0");
        }
    }
}