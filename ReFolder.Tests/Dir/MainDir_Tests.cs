using Moq;
using NUnit.Framework;
using ReFolder.Dir;
using ReFolder.Dir.Description;
using ReFolder.Management;
using System;
using System.Collections.Generic;

namespace ReFolder.Tests
{
    [TestFixture]
    class MainDir_Tests
    {
        MainDir mainDir;
        [SetUp]
        public void onSetUp()
        {
            DirDescription desc = new DirDescription("cats/are/great/mainDir", "mainDir");
            mainDir = new MainDir(desc);
        }

        #region AddChildToChildrenList_tests
        [Test]
        public void AddChildToChildrenList_WhenChildIsNull_ThrowArgumentNullException()
        {
            var child = new ChildDir("child", mainDir);
            TestDelegate action = () => mainDir.AddChildToChildrenList(null);
            Assert.Throws<ArgumentNullException>(action);
        }
        [Test]
        public void AddChildToChildrenList_WhenIsNameExistingInChildrenDirsReturnsFalse_AddsChild()
        {
            var child = new ChildDir("child", mainDir);
            var dirValidateMock = new Mock<IDirValidate>();
            dirValidateMock.Setup(s => s.IsNameExistingInChildrenDirs((It.IsAny<IEditableDirWithChildren>()), (It.IsAny<String>())))
                .Returns(false);
            dirValidateMock.Setup(s => s.IsfolderExisting(It.IsAny<string>()))
                .Returns(false);

            mainDir.DirValidate = dirValidateMock.Object;
            mainDir.AddChildToChildrenList(child);

            var result = mainDir.Children[0];

            Assert.That(result, Is.TypeOf<ChildDir>());
        }
        [Test]
        public void AddChildToChildrenList_WhenIsNameExistingInChildrenDirsReturnsTrue_ThrowsInvalidOperationException()
        {
            var child = new ChildDir("child", mainDir);
            var dirValidateMock = new Mock<IDirValidate>();
            dirValidateMock.Setup(s => s.IsNameExistingInChildrenDirs((It.IsAny<IEditableDirWithChildren>()), (It.IsAny<String>())))
                .Returns(true);
            dirValidateMock.Setup(s => s.IsfolderExisting(It.IsAny<string>()))
                .Returns(false);
            mainDir.DirValidate = dirValidateMock.Object;

            TestDelegate result = () => mainDir.AddChildToChildrenList(child);

            Assert.Throws<InvalidOperationException>(result);
        }
        [Test]
        public void AddChildToChildrenList_WhenFolderExistInSystem_ThrowsInvalidOperationException()
        {
            var child = new ChildDir("child", mainDir);
            var dirValidateMock = new Mock<IDirValidate>();
            dirValidateMock.Setup(s => s.IsfolderExisting(It.IsAny<string>()))
                .Returns(true);
            dirValidateMock.Setup(s => s.IsNameExistingInChildrenDirs(It.IsAny<IEditableDirWithChildren>(), It.IsAny<string>()))
                .Returns(false);
            mainDir.DirValidate = dirValidateMock.Object;

            TestDelegate result = () => mainDir.AddChildToChildrenList(child);

            Assert.Throws<InvalidOperationException>(result);
        }
        [Test]
        public void AddChildToChildrenList_WhenFolderDontExistInSystem_AddsChild()
        {
            var child = new ChildDir("child", mainDir);
            var dirValidateMock = new Mock<IDirValidate>();
            dirValidateMock.Setup(s => s.IsfolderExisting(It.IsAny<string>()))
                .Returns(false);
            dirValidateMock.Setup(s => s.IsNameExistingInChildrenDirs(It.IsAny<IEditableDirWithChildren>(), It.IsAny<string>()))
                .Returns(false);

            mainDir.DirValidate = dirValidateMock.Object;
            mainDir.AddChildToChildrenList(child);

            var result = mainDir.Children[0];

            Assert.That(result, Is.TypeOf<ChildDir>());
        }
        #endregion

        #region AddChildrenToChildrenList

        [Test]
        public void AddChildrenToChildrenList_WhenCalled_AddsChildren()
        {
            List<IEditableDirWithChildrenAndParent> children = new List<IEditableDirWithChildrenAndParent>();
            children.Add(new ChildDir("mirek", mainDir));
            children.Add(new ChildDir("firek", mainDir));

            mainDir.AddChildrenToChildrenList(children);

            Assert.AreEqual(children, mainDir.Children);
        }
        [Test]
        public void AddChildrenToChildrenList_WhenParamIsNull_ThrowArgumentNullException()
        {
            TestDelegate action = () => mainDir.AddChildrenToChildrenList(null);

            Assert.Throws<ArgumentNullException>(action);
        }

        [Test]
        public void AddChildrenToChildrenList_WhenListIsEmpty_AddsList()
        {
            List<IEditableDirWithChildrenAndParent> children = new List<IEditableDirWithChildrenAndParent>();

            mainDir.AddChildrenToChildrenList(children);

            Assert.AreEqual(children, mainDir.Children);
        }

        #endregion

        #region DeleteChildDirFromList
        [Test]
        public void DeleteChildDirFromList_WhenParamIsNull_ThrowsArgumentNullException()
        {
            TestDelegate action = () => mainDir.DeleteChildDirFromList(null);

            Assert.Throws<ArgumentNullException>(action);
        }
        [Test]
        public void DeleteChildDirFromList_WhenCalled_DeleteChild()
        {
            ChildDir child1 = new ChildDir("child1", mainDir);
            ChildDir child2 = new ChildDir("child2", mainDir);
            mainDir.AddChildToChildrenList(child1);
            mainDir.AddChildToChildrenList(child2);

            mainDir.DeleteChildDirFromList(child1);

            Assert.IsTrue(mainDir.Children.Contains(child2) && !(mainDir.Children.Contains(child1)));
        }
        #endregion

        #region DeleteChildrenDirsFromList
        [Test]
        public void DeleteChildrenDirsFromList_WhenParamIsNull_ThrowsArgumentNullException()
        {
            TestDelegate action = () => mainDir.DeleteChildrenDirsFromList(null);

            Assert.Throws<ArgumentNullException>(action);
        }
        [Test]
        public void DeleteChildrenDirsFromList_WhenCalled_DeleteChildren()
        {
            ChildDir child1 = new ChildDir("child1", mainDir);
            ChildDir child2 = new ChildDir("child2", mainDir);
            ChildDir child3 = new ChildDir("child3", mainDir);
            mainDir.AddChildToChildrenList(child1);
            mainDir.AddChildToChildrenList(child2);
            mainDir.AddChildToChildrenList(child3);
            List<IEditableDirWithChildrenAndParent> list = new List<IEditableDirWithChildrenAndParent>();
            list.Add(child1);
            list.Add(child2);

            mainDir.DeleteChildrenDirsFromList(list);

            Assert.IsTrue(!mainDir.Children.Contains(child1) &&
                !mainDir.Children.Contains(child2) &&
                 mainDir.Children.Contains(child3));
        }

        #endregion
    }
}
