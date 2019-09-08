using NUnit.Framework;
using Moq;
using ReFolder.Dir;
using ReFolder.Dir.Description;
using System;
using System.Collections.Generic;

namespace ReFolder.Tests
{
    class MainDir_Tests
    {

        [Test]
        public void DeleteChildDirFromList_ThrowsArgumentNullException_WhenChildIsNull()
        {
            //arrange
            var Descr = Mock.Of<IMutableSystemObjectDescription>();
            IEditableDirWithChildren mainDir = new MainDir(Descr);
            //act
            TestDelegate action = () => mainDir.DeleteChildDirFromList(null);

            //assert    
            Assert.Throws<ArgumentNullException>(action);
        }
        
        [Test]
        public void DeleteChildDirFromList_DeleteChild_WhenAcceptableObjectIsDelivered()
        {
            //arrange
            var Descr = Mock.Of<IMutableSystemObjectDescription>(x=>
                x.FullName== "kot");

            IEditableDirWithChildren mainDir = new MainDir(Descr);
            IEditableDirWithChildrenAndParent child = Mock.Of<IEditableDirWithChildrenAndParent>();
            IEditableDirWithChildrenAndParent child1 = Mock.Of<IEditableDirWithChildrenAndParent>();
            List<IEditableDirWithChildrenAndParent> list = new List<IEditableDirWithChildrenAndParent>();
            list.Add(child);
            list.Add(child1);

            mainDir.AddChildrenToChildrenList(list);

            mainDir.DeleteChildDirFromList(child);
            mainDir.DeleteChildDirFromList(child1);

            //act
            int num = mainDir.Children.Count;

            //assert    
            Assert.AreEqual(0,num);
        }

        [Test]
        public void DeleteChildrenDirFromList_ThrowsArgumentNullException_WhenChildIsNull()
        {
            //arrange
            var Descr = Mock.Of<IMutableSystemObjectDescription>();
            IEditableDirWithChildren mainDir = new MainDir(Descr);
            //act
            TestDelegate action = () => mainDir.DeleteChildrenDirsFromList(null);

            //assert    
            Assert.Throws<ArgumentNullException>(action);
        }
        [Test]
        public void DeleteChildrenDirFromList_DeleteChildren_WhenAcceptableObjectIsDelivered()
        {
            //arrange
            var Descr = Mock.Of<IMutableSystemObjectDescription>(x =>
                x.FullName == "kot");

            IEditableDirWithChildren mainDir = new MainDir(Descr);
            IEditableDirWithChildrenAndParent child = Mock.Of<IEditableDirWithChildrenAndParent>();
            IEditableDirWithChildrenAndParent child1 = Mock.Of<IEditableDirWithChildrenAndParent>();
            List<IEditableDirWithChildrenAndParent> list = new List<IEditableDirWithChildrenAndParent>();
            list.Add(child);
            list.Add(child1);

            mainDir.AddChildrenToChildrenList(list);
            mainDir.DeleteChildrenDirsFromList(list);


            //act
            int num = mainDir.Children.Count;

            //assert    
            Assert.AreEqual(0, num);
        }

        [Test]
        public void AddChildToChildrenList_ThrowsArgumentNullException_WhenChildIsNull()
        {
            //arrange
            var Descr = Mock.Of<IMutableSystemObjectDescription>();
            IEditableDirWithChildren mainDir = new MainDir(Descr);
            //act
            TestDelegate action = () => mainDir.AddChildToChildrenList(null);

            //assert    
            Assert.Throws<ArgumentNullException>(action);
        }
        [Test]
        public void AddChildToChildrenList_AddsChild_WhenAcceptableObjectIsDelivered()
        {
            //arrange
            var Descr = Mock.Of<IMutableSystemObjectDescription>(x=>
            x.FullName=="kotek");
            IEditableDirWithChildren mainDir = new MainDir(Descr);
            IEditableDirWithChildrenAndParent child= Mock.Of<IEditableDirWithChildrenAndParent>();
            IEditableDirWithChildrenAndParent child1= Mock.Of<IEditableDirWithChildrenAndParent>();
            mainDir.AddChildToChildrenList(child);
            mainDir.AddChildToChildrenList(child1);
            //act
            int size = mainDir.Children.Count;
            //assert    
            Assert.AreEqual(2,size) ;
        }

        [Test]
        public void AddChildrenToChildrenList_ThrowsArgumentNullException_WhenChildIsNull()
        {
            //arrange
            var Descr = Mock.Of<IMutableSystemObjectDescription>();
            IEditableDirWithChildren mainDir = new MainDir(Descr);
            //act
            TestDelegate action = () => mainDir.AddChildrenToChildrenList(null);

            //assert    
            Assert.Throws<ArgumentNullException>(action);
        }
        [Test]
        public void AddChildrenToChildrenList_AddsChildren_WhenAcceptableObjectIsDelivered()
        {
            //arrange
            var Descr = Mock.Of<IMutableSystemObjectDescription>(x =>
            x.FullName == "kotek");
            IEditableDirWithChildren mainDir = new MainDir(Descr);
            IEditableDirWithChildrenAndParent child = Mock.Of<IEditableDirWithChildrenAndParent>();
            IEditableDirWithChildrenAndParent child1 = Mock.Of<IEditableDirWithChildrenAndParent>();
            List<IEditableDirWithChildrenAndParent> list = new List<IEditableDirWithChildrenAndParent>();

            list.Add(child);
            list.Add(child1);
            mainDir.AddChildrenToChildrenList(list);
            //act
            int size = mainDir.Children.Count;
            //assert    
            Assert.AreEqual(2, size);
        }
        [Test]
        public void AutoGenerateChildrenFullName_ChangeChildrenFullName_WhenParentFullNameChange()
        {
            string fullName = "c:\\cats\\RedCats";
            string[] nameList = new string[] { "kuszek", "puszek", "muszek" };
            //arange
            IEditableDirWithChildren mainDir = new MainDir(new DirDescription("c:\\cats\\BlueCats", "BlueCats"));
            ChildDir child1 = new ChildDir(nameList[0], mainDir);
            ChildDir child11 = new ChildDir(nameList[1], child1);
            ChildDir child12 = new ChildDir(nameList[2], child1);
            mainDir.AddChildToChildrenList(child1);
            child1.AddChildToChildrenList(child11);
            child1.AddChildToChildrenList(child12);
            //act
            mainDir.Description.FullName = fullName;

            MainDir.AutoGenerateChildrenFullName(mainDir);
            //assert
            Assert.AreEqual($"{fullName}\\{nameList[0]}", child1.Description.FullName);
            Assert.AreEqual($"{fullName}\\{nameList[0]}\\{nameList[1]}", child11.Description.FullName);
            Assert.AreEqual($"{fullName}\\{nameList[0]}\\{nameList[2]}", child12.Description.FullName);
        }
        [Test]
        public void AutoGenerateChildrenFullName_ChangeChildrenFullName_WhenParentFullNameNotChange()
        {
            string fullName = "c:\\cats\\BlueCats";
            string[] nameList = new string[] { "kuszek", "puszek", "muszek" };
            //arange
            IEditableDirWithChildren mainDir = new MainDir(new DirDescription(fullName, "BlueCats"));
            ChildDir child1 = new ChildDir(nameList[0], mainDir);
            ChildDir child11 = new ChildDir(nameList[1], child1);
            ChildDir child12 = new ChildDir(nameList[2], child1);
            mainDir.AddChildToChildrenList(child1);
            child1.AddChildToChildrenList(child11);
            child1.AddChildToChildrenList(child12);
            //act
            MainDir.AutoGenerateChildrenFullName(mainDir);
            //assert
            Assert.AreEqual($"{fullName}\\{nameList[0]}", child1.Description.FullName);
            Assert.AreEqual($"{fullName}\\{nameList[0]}\\{nameList[1]}", child11.Description.FullName);
            Assert.AreEqual($"{fullName}\\{nameList[0]}\\{nameList[2]}", child12.Description.FullName);
        }





    }
}
