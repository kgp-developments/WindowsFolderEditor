using System;
using NUnit.Framework;
using ReFolder.Management;
namespace ReFolder.Tests
 
{
    public  class DirWrite_Tests
    {
        [Test]
        public void CreateNewFolder_ThrowsArgumentNullException_WhenFullNameIsNull()
        {
            //arange
            var dirWrite = DirWrite.GetInstance();
            //Act
            TestDelegate action = () => dirWrite.CreateNewFolder(null);
            //assert
            Assert.Throws<ArgumentNullException>(action);
        }
        [Test]
        public void CreateNewFolder_throwsArgumentException_WhenFullNameDontContainsSlash()
        {
            //arange
            var dirWrite = DirWrite.GetInstance();
            //Act
            TestDelegate action = () => dirWrite.CreateNewFolder(" kotek");
            //assert
            Assert.Throws<ArgumentException>(action);
        }
        [Test]
        public void CreateNewFolder_throwsArgumentException_WhenFullNameIsEmptyString()
        {
            //arange
            var dirWrite = DirWrite.GetInstance();
            //Act
            TestDelegate action = () => dirWrite.CreateNewFolder(" ");
            //assert
            Assert.Throws<ArgumentException>(action);
        }
        [Test]
        public void CreateNewFolder_throwsArgumentException_WhenFullNameEndsWithSlash()
        {
            //arange
            var dirWrite = DirWrite.GetInstance();
            //Act
            TestDelegate action = () => dirWrite.CreateNewFolder("kotek\\mruczek\\");
            //assert
            Assert.Throws<ArgumentException>(action);
        }
        // sprawdź tworzenie folderów (brak możliwości wykonania w moq ponieważ klasa DirectoryInfo jest sealed i static)
    }
}