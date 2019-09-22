
using ReFolder.Dir;
using ReFolder.Dir.Description;
using ReFolder.Management;
using ReFolder.Memento;
using ReFolder.TxtFileWriter;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Beep();
            IEditableDirWithChildren main = DirManagement.GetDefaultInstance().GetFolderAsNewMainDir(@"C:\Users\Klakier\Desktop\kociFolderek\newFolder_1");

            DirManagement.GetDefaultInstance().ChangeCreatedDirSystemValue("kotek jest tutaj", main);





        }
    }
}
