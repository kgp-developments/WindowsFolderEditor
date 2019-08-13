
using ReFolder.Dir;
using ReFolder.Dir.Description;
using ReFolder.Management;

namespace ConsoleApp1
{
    class Program
    {
        //kot
        static void Main(string[] args)
        {
            //kotek
            MainDir mainDir = new MainDir(
                new DirDescription(@"C:\Users\Klakier\Desktop\kociFolderek", "kociFolderek")
                );

            ChildDir dir1 = new ChildDir("firek1", mainDir);
            ChildDir dir2 = new ChildDir("firek2", mainDir);
            ChildDir dir3 = new ChildDir("firek3", mainDir);
            ChildDir dir4 = new ChildDir("firek4", mainDir);
            ChildDir dir41 = new ChildDir("firek41", dir4);
            ChildDir dir42 = new ChildDir("firek42", dir4);
            ChildDir dir43 = new ChildDir("firek43", dir4);
            ChildDir dir44 = new ChildDir("firek44", dir4);
            mainDir.AddChildToChildrenList(dir1);
            mainDir.AddChildToChildrenList(dir2);
            mainDir.AddChildToChildrenList(dir3);
            mainDir.AddChildToChildrenList(dir4);
            dir4.AddChildToChildrenList(dir41);
            dir4.AddChildToChildrenList(dir42);
            dir4.AddChildToChildrenList(dir43);
            dir4.AddChildToChildrenList(dir44);

            var management = DirManagement.GetDefaultInstance();
            DirManagement.MemoryDirs.GetInstance().InitializeAllChildren(mainDir);

            management.GenerateAllChildrenDirsAsFolders();
                 











        }
    }
}
