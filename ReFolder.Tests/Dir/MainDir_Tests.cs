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






    }
}
