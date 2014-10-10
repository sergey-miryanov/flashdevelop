using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PluginCore.Managers;

namespace PluginCore.Test.PluginCore.Managers
{
    public class TraceManagerTest
    {
    }

    [TestClass]
    public class TraceItemTest
    {
        [TestMethod]
        public void CreateTraceItemWithNullString()
        {
            TraceItem item = new TraceItem(null, 0);
            Assert.AreEqual(null, item.Message);
            Assert.AreEqual(0, item.State);
        }

        [TestMethod]
        public void CreateTraceItemWithEmptyString()
        {
            TraceItem item = new TraceItem(string.Empty, 0);
            Assert.AreEqual(string.Empty, item.Message);
            Assert.AreEqual(0, item.State);
        }

        [TestMethod]
        public void CreateTraceItemWithSomeString()
        {
            TraceItem item = new TraceItem("some", 0);
            Assert.AreEqual("some", item.Message);
            Assert.AreEqual(0, item.State);
        }

        [TestMethod]
        public void CreateTraceItemWithSomeObject()
        {
            SomeObject some = new SomeObject();
            TraceItem item = new TraceItem(some, 0);
            Assert.AreEqual(some.ToString(), item.Message);
            Assert.AreEqual(0, item.State);
        }
    }

    class SomeObject
    {
        public override string ToString()
        {
            return "SomeObject";
        }
    }
}
