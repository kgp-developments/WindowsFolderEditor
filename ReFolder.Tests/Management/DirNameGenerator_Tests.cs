using NUnit.Framework;
using ReFolder.Dir;
using ReFolder.Dir.Description;
using ReFolder.Management;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReFolder.Tests.Management
{
    [TestFixture]
    class DirNameGenerator_Tests
    {
        IEditableDirWithChildren parent;
        DirNameGenerator defaultGenerator;

        [SetUp]
        public void SetUp()
        {
            var desc = new DirDescription("kot1\\ kot", "kot");
            parent = new MainDir(desc);
            defaultGenerator = DirNameGenerator.GetDefaultInstance();
        }

        [TestCase("newFolder_0")]
        [TestCase("newFolder_1", "newFolder_0")]
        [TestCase("newFolder_2", "newFolder_0", "newFolder_1")]
        [TestCase("newFolder_3", "newFolder_0", "newFolder_1", "newFolder_2")]
        [TestCase("newFolder_1", "newFolder_0", "newFolder_2")]
        public void GeneratetName_Default_WhenCalled_ReturnsGeneratedName(string shoudReturn, params string[] names)
        {
            foreach (var name in names)
            {
                parent.AddChildToChildrenList(new ChildDir(name, parent));
            }

            string generatedName = defaultGenerator.GeneratetName_Default(parent);

            Assert.AreEqual(shoudReturn, generatedName);
        }
        [Test]
        public void GeneratetName_Default_WhenParentHaveChildWithRandomGeneratedName_ReturnsGeneratedName()
        {
            parent.AddChildToChildrenList(new ChildDir("newFolder_0", parent));
            parent.AddChildToChildrenList(new ChildDir("newFolder_1", parent));
            parent.AddChildToChildrenList(new ChildDir(defaultGenerator.GeneratetName_Default(parent), parent));
            parent.AddChildToChildrenList(new ChildDir("newFolder_5", parent));
            string generatedName = defaultGenerator.GeneratetName_Default(parent);

            Assert.AreEqual("newFolder_3" , generatedName);
        }

        [TestCase("newFolder_1", "newFolder_0")]
        [TestCase("newFolder_2", "newFolder_0", "newFolder_1")]
        [TestCase("newFolder_0", "newFolder_1", "newFolder_2")]
        public void GeneratetName_Default_WhenParentHaveNamesToIgnore_ReturnsGeneratedName(string shouldReturn, params string[] namesToIgnore)
        {  
            string generatedName = defaultGenerator.GeneratetName_Default(parent,namesToIgnore: namesToIgnore);

            Assert.AreEqual(shouldReturn, generatedName);
        }


        [TestCase("newFolder_-6",-6)]
        [TestCase("newFolder_-5",-6, "newFolder_-6")]
        [TestCase("newFolder_7",5, "newFolder_5", "newFolder_6")]
        [TestCase("newFolder_3",0, "newFolder_0", "newFolder_1", "newFolder_2")]
        [TestCase("newFolder_1002",1000, "newFolder_1000", "newFolder_1001")]
        public void GeneratetName_Default_WhenCalledWithChangedSufix_ReturnsGeneratedName(string shoudReturn,int sufix, params string[] names)
        {
            foreach (var name in names)
            {
                parent.AddChildToChildrenList(new ChildDir(name, parent));
            }

            string generatedName = defaultGenerator.GeneratetName_Default(parent, sufix_minValue:sufix);

            Assert.AreEqual(shoudReturn, generatedName);
        }

    }
}
