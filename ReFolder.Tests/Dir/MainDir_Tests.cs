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
            IEditableDirWithChildrenAndParrent child = Mock.Of<IEditableDirWithChildrenAndParrent>();
            IEditableDirWithChildrenAndParrent child1 = Mock.Of<IEditableDirWithChildrenAndParrent>();
            List<IEditableDirWithChildrenAndParrent> list = new List<IEditableDirWithChildrenAndParrent>();
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
            IEditableDirWithChildrenAndParrent child = Mock.Of<IEditableDirWithChildrenAndParrent>();
            IEditableDirWithChildrenAndParrent child1 = Mock.Of<IEditableDirWithChildrenAndParrent>();
            List<IEditableDirWithChildrenAndParrent> list = new List<IEditableDirWithChildrenAndParrent>();
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
            IEditableDirWithChildrenAndParrent child= Mock.Of<IEditableDirWithChildrenAndParrent>();
            IEditableDirWithChildrenAndParrent child1= Mock.Of<IEditableDirWithChildrenAndParrent>();
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
            IEditableDirWithChildrenAndParrent child = Mock.Of<IEditableDirWithChildrenAndParrent>();
            IEditableDirWithChildrenAndParrent child1 = Mock.Of<IEditableDirWithChildrenAndParrent>();
            List<IEditableDirWithChildrenAndParrent> list = new List<IEditableDirWithChildrenAndParrent>();

            list.Add(child);
            list.Add(child1);
            mainDir.AddChildrenToChildrenList(list);
            //act
            int size = mainDir.Children.Count;
            //assert    
            Assert.AreEqual(2, size);
        }

        [Test]
        public void AutoGenerateChildrenFullName_RenameChildren_WhenMaindDirFullNameNotChange()
        {
            string[] nameList = new string[] {"firek","pirek","irek" };
            string mainDirName = "c:\\Cats\\BlueCats";
            //arrange
            IMutableSystemObjectDescription desc = Mock.Of<IMutableSystemObjectDescription>(x =>
            x.FullName == mainDirName);
            IEditableDirWithChildren mainDir = new MainDir(desc);
            IEditableDirWithChildrenAndParrent childDir1 = new ChildDir(nameList[0], mainDir);
            IEditableDirWithChildrenAndParrent childDir11 = new ChildDir(nameList[1], childDir1);
            IEditableDirWithChildrenAndParrent childDir12 = new ChildDir(nameList[2], childDir1);

            mainDir.AddChildToChildrenList(childDir1);
            childDir1.AddChildToChildrenList(childDir11);
            childDir1.AddChildToChildrenList(childDir12);

            //act
            MainDir.AutoGenerateChildrenFullName(mainDir);
            //assert
            Assert.AreEqual($"{mainDirName}\\{nameList[0]}", childDir1.Description.FullName);
            Assert.AreEqual($"{mainDirName}\\{nameList[0]}\\{nameList[1]}", childDir11.Description.FullName);
            Assert.AreEqual($"{mainDirName}\\{nameList[0]}\\{nameList[2]}", childDir12.Description.FullName);
        }
        [Test]
        public void AutoGenerateChildrenFullName_RenameChildren_WhenMaindDirFullNameChange()
        {
            string[] nameList = new string[] { "firek", "pirek", "irek" };
            string mainDirFullName = "c:\\Cats\\RedCats";
            string mainDirName = "RedCats";
            //arrange
            IMutableSystemObjectDescription desc = new DirDescription("c:\\Cats\\BlueCats", "BlueCats");
            IEditableDirWithChildren mainDir = new MainDir(desc);
            IEditableDirWithChildrenAndParrent childDir1 = new ChildDir(nameList[0], mainDir);
            IEditableDirWithChildrenAndParrent childDir11 = new ChildDir(nameList[1], childDir1);
            IEditableDirWithChildrenAndParrent childDir12 = new ChildDir(nameList[2], childDir1);
            mainDir.Description.FullName = mainDirFullName;
            mainDir.Description.Name = mainDirName;

            mainDir.AddChildToChildrenList(childDir1);
            childDir1.AddChildToChildrenList(childDir11);
            childDir1.AddChildToChildrenList(childDir12);
            //act
            MainDir.AutoGenerateChildrenFullName(mainDir);
            //assert
            Assert.AreEqual($"{mainDirFullName}\\{nameList[0]}", childDir1.Description.FullName);
            Assert.AreEqual($"{mainDirFullName}\\{nameList[0]}\\{nameList[1]}", childDir11.Description.FullName);
            Assert.AreEqual($"{mainDirFullName}\\{nameList[0]}\\{nameList[2]}", childDir12.Description.FullName);
        }



    }
}
