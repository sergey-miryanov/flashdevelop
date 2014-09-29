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
            Assert.AreEqual((uint)0, (uint)m.Access);
            Assert.IsNull(m.Comments);
            Assert.AreEqual((uint)0, (uint)m.Flags);
            Assert.IsNull(m.InFile);
            Assert.IsFalse(m.IsPackageLevel);
            Assert.AreEqual(0, m.LineFrom);
            Assert.AreEqual(0, m.LineTo);
            Assert.IsNull(m.Name);
            Assert.IsNull(m.Namespace);
            Assert.IsNull(m.Template);
            Assert.IsNull(m.Type);
            Assert.IsNull(m.Value);
            Assert.IsNull(m.Parameters);
            Assert.AreEqual("", m.ParametersString());
            Assert.AreEqual("", m.ToDeclarationString());
        }

        [TestMethod]
        public void TestCreateAS3VarMemberModel()
        {
            PrivateObject privateObject = new PrivateObject(as3context);
            ContextFeatures features = (ContextFeatures)privateObject.GetField("features");
            MemberModel m = new MemberModel("foo", features.numberKey, FlagType.Variable, Visibility.Private);
            m.Value = "10.0";
            Assert.AreEqual("foo", m.Name);
            Assert.AreEqual(m.Name, m.FullName);
            Assert.AreEqual(features.numberKey, m.Type);
            Assert.AreEqual(FlagType.Variable, m.Flags);
            Assert.AreEqual(Visibility.Private, m.Access);
            Assert.AreEqual("foo : " + features.numberKey, m.ToString());
            Assert.AreEqual(m.ToDeclarationString(true, false), m.ToDeclarationString());
            Assert.AreEqual("foo : " + features.numberKey, m.ToDeclarationString(true, false));
            Assert.AreEqual("foo : " + features.numberKey + " = 10.0", m.ToDeclarationString(true, true));
            Assert.AreEqual("foo:" + features.numberKey, m.ToDeclarationString(false, false));
            Assert.AreEqual("foo:" + features.numberKey + "=10.0", m.ToDeclarationString(false, true));
        }
    }
}