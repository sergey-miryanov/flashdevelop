using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlashDevelop.Mock;
using FlashDevelop.Mock.Docking;
using PluginCore;
using PluginCore.Utilities;
using System.Collections.Generic;
using ASCompletion.Model;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace ASCompletion.Test.Context
{
    [TestClass]
    public class ASContextTest
    {
        [TestInitialize]
        public void Initialize()
        {
            FlashDevelop.Mock.MainForm mainForm = new FlashDevelop.Mock.MainForm();
            SingleInstanceApp.Initialize();
            PluginMain pluginMain = new ASCompletion.PluginMain();
            pluginMain.Initialize();
        }

        [TestMethod]
        public void TestFirstRun()
        {
            ASCompletion.Context.ASContext context = (ASCompletion.Context.ASContext)ASCompletion.Context.ASContext.Context;
            Assert.IsNull(context.Settings);
            Assert.IsNotNull(context.Features);
            Assert.AreEqual(0, context.CurrentLine);
            Assert.IsNull(context.CurrentMember);
            Assert.AreEqual(ClassModel.VoidClass, context.CurrentClass);
            Assert.IsTrue(string.IsNullOrEmpty(context.CurrentFile));
            Assert.IsNotNull(context.CurrentModel);
            Assert.IsFalse(context.InPrivateSection);
            Assert.IsFalse(context.IsFileValid);
            Assert.IsFalse(context.CanBuild);
            Assert.IsNull(context.TopLevel);
            Assert.IsNull(context.Classpath);
        }

        [TestMethod]
        public void TestGetModel()
        {
            ASCompletion.Context.ASContext context = (ASCompletion.Context.ASContext)ASCompletion.Context.ASContext.Context;
            Assert.AreEqual(ClassModel.VoidClass, context.GetModel("", "", ""));
            Assert.AreEqual(ClassModel.VoidClass, context.GetModel("flashdevelop", "TestClass", ""));
            Assert.AreEqual(ClassModel.VoidClass, context.GetModel("flashdevelop", "TestClass", "tests"));
        }

        [TestMethod]
        public void TestCreateFileModelForInvalidFile()
        {
            string fileName = "";
            ASCompletion.Context.ASContext context = (ASCompletion.Context.ASContext)ASCompletion.Context.ASContext.Context;
            FileModel aFile = context.CreateFileModel(fileName);
            Assert.IsNotNull(aFile);
            Assert.AreEqual(fileName, aFile.FileName);
            Assert.IsNull(aFile.Context);
        }

        [TestMethod]
        public void TestCreateFileModelForValidFile()
        {
            string fileName = Path.GetFullPath(PathHelper.as3FileWithUserObjectClass);
            ASCompletion.Context.ASContext context = (ASCompletion.Context.ASContext)ASCompletion.Context.ASContext.Context;
            FileModel aFile = context.CreateFileModel(fileName);
            Assert.IsNotNull(aFile);
            Assert.AreEqual(fileName, aFile.FileName);
            Assert.AreEqual(context, aFile.Context);
        }

        [TestMethod]
        public void TestGetFileModelForInvalidFile()
        {
            string fileName = "";
            ASCompletion.Context.ASContext context = (ASCompletion.Context.ASContext)ASCompletion.Context.ASContext.Context;
            FileModel aFile = context.GetFileModel(fileName);
            Assert.AreEqual(0, aFile.Version);
        }

        [TestMethod]
        public void TestGetFileModelForValidFile()
        {
            string fileName = Path.GetFullPath(PathHelper.as3FileWithUserObjectClass);
            ASCompletion.Context.ASContext context = (ASCompletion.Context.ASContext)ASCompletion.Context.ASContext.Context;
            FileModel aFile = context.GetFileModel(fileName);
            Assert.AreEqual(3, aFile.Version);
        }
    }
}