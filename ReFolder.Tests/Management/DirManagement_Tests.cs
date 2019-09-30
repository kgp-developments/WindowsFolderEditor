﻿using Moq;
using NUnit.Framework;
using ReFolder.Dir;
using ReFolder.Dir.Description;
using ReFolder.Management;
using ReFolder.Management.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReFolder.Tests.Management
{
    [TestFixture]
    class DirManagement_Tests
    {
        private DirManagement defaultDirManagement;

        [SetUp]
        public void SetUp()
        {
            defaultDirManagement = DirManagement.GetDefaultInstance();
        }

        #region AutoGenerateDirFullName
        [Test]
        public void AutoGenerateDirFullName_WhenCalled_ReturnsFullName()
        {
            var mainDirDescr = new DirDescription("cat1\\cat2\\cat3", "cat3");
            var childDirDescr = new DirDescription("cat1\\catX", "catX");
            var main = new MainDir(mainDirDescr);
            var child = new ChildDir(childDirDescr, main);

            defaultDirManagement.AutoGenerateDirFullName(child);

            Assert.AreEqual(mainDirDescr.FullName +"\\"+childDirDescr.Name, child.Description.FullName);
        }

        [Test]
        public void AutoGenerateDirFullName_WhenArgumentIsNull_ThrowsArgumentNullException()
        {
            TestDelegate action = () => defaultDirManagement.AutoGenerateDirFullName(null);

            Assert.Throws<ArgumentNullException>(action);
        }
        #endregion

        #region AutoGenerateChildrenFullName
        [Test]
        public void AutoGenerateChildrenFullName_WhenCalled_RenamesChildren()
        {
            var mainDirDescr = new DirDescription("cat1\\cat2\\cat3", "cat3");
            var childDirDescr1 = new DirDescription("cat1\\cat1", "cat1");
            var childDirDescr11 = new DirDescription("cat1\\cat11", "cat11");
            var main = new MainDir(mainDirDescr);
            var child1 = new ChildDir(childDirDescr1, main);
            var child11 = new ChildDir(childDirDescr11, child1);
            child1.AddChildToChildrenList(child11);
            main.AddChildToChildrenList(child1);

            defaultDirManagement.AutoGenerateChildrenFullName(main);

            Assert.IsTrue(main.Description.FullName + "\\" + child1.Description.Name== child1.Description.FullName &&
                child1.Description.FullName + "\\" + child11.Description.Name == child11.Description.FullName);
        }

        [Test]
        public void AutoGenerateChildrenFullName_WhenParamIsNull_ThrowsArgumentNullException()
        {

            TestDelegate action = () => defaultDirManagement.AutoGenerateChildrenFullName(null);

            Assert.Throws<ArgumentNullException>(action);
        }

        #endregion
    }
}
