
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
            DirManagement manager = DirManagement.GetDefaultInstance();

            MainDir main = DirRead.GetInstance().GetMainDirFolder(@"C:\Users\Klakier\Desktop\Nowy folder (2)");
/*            ChildDir child0 = new ChildDir("newFolder_0", main);
            ChildDir child1 = new ChildDir("newFolder_1", main);
            ChildDir child3 = new ChildDir("newFolder_3", main);

 
            main.AddChildToChildrenList(child1);
            main.AddChildToChildrenList(child3);
            main.AddChildToChildrenList(child0);*/

            System.Console.WriteLine("jak jest" +"\n");
            foreach(IEditableDirWithChildrenAndParrent child in main.Children)
            {
                System.Console.WriteLine(child.Description.Name+"\n");
            }

            ChildDir child2 = new ChildDir(manager.GenerateName(main), main);
            main.AddChildToChildrenList(child2);

            ChildDir child4 = new ChildDir(manager.GenerateName(main), main);
            main.AddChildToChildrenList(child4);

            ChildDir child6 = new ChildDir(manager.GenerateName(main), main);
            main.AddChildToChildrenList(child6);

            System.Console.WriteLine("jak się zmieniło"+"\n");
            foreach (IEditableDirWithChildrenAndParrent child in main.Children)
            {
                System.Console.WriteLine(child.Description.Name + "\n");
            }
        }
    }
}
