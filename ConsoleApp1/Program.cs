﻿
using ReFolder.Dir;
using ReFolder.Management;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Beep();
            IEditableDirWithChildren main = DirManagement.GetDefaultInstance().GetFolderAsNewMainDir(@"C:\Users\Klakier\Desktop\kociFolder\koci");

            DirManagement.GetDefaultInstance().ChangeCreatedDirSystemValue("kotek jest tutaj", main, @"C:\Users\Klakier\Downloads\Hopstarter-Soft-Scraps-Folder-Generic-Green.ico");





        }
    }
}
