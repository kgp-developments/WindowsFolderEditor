﻿using ReFolder.Dir;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReFolder.Management
{
    public class MemoryDirs
    {

        //singleton
        private static MemoryDirs InstanceMemoryDirs { get; set; }
        // zwraca instancję
        public static MemoryDirs GetDefaultInstance()
        {
            if (InstanceMemoryDirs == null)
            {
                InstanceMemoryDirs = new MemoryDirs();
            }
            return InstanceMemoryDirs;
        }

        // lista wszystkich folderów które mają być wygenerowane posiada dane wszystkich utworzonych folderów za wyjątkiem folderu głównego 
        public static List<IEditableDirWithChildrenAndParent> AllCreatedDirs { get; } = new List<IEditableDirWithChildrenAndParent>();
        //inicjalizuje listę wszystkich folderów
        public void InitializeAllChildren(IEditableDirWithChildren dir)
        {

            foreach (IEditableDirWithChildrenAndParent childDir in dir.Children)
            {
                MemoryDirs.AllCreatedDirs.Add(childDir);
                if (childDir.Children.Count > 0)
                {
                    InitializeAllChildren(childDir);
                }
                else continue;
            }
        }
        // sprawdza czy dany folder istnieje w zbiorze wszystkich folderów tablicy AllCreatedAllCreatedDirs
        public bool ReturnTrueIfDirExistInAllCreatedDirs(IEditableDirWithChildren dir)
        {
            bool flag = false;
            foreach (IEditableDirWithChildren childDir in AllCreatedDirs)
            {
                if (childDir.Description.FullName.Equals(dir.Description.FullName))
                {
                    flag = true;
                }

            }
            return flag;
        }
        //usuwa folder z wszystkich folderów w pamięci 
        public void DeleteDirFromAllCreatedDirs(IEditableDirWithChildrenAndParent dir)
        {
            AllCreatedDirs.Remove(dir);
        }
        // usuwa foldery z wszystkich folderów w pamięci  
        public void DeleteDirsFromAllCreatedDirs(List<IEditableDirWithChildrenAndParent> childDirs)
        {
            foreach (IEditableDirWithChildrenAndParent dir in childDirs)
                DeleteDirFromAllCreatedDirs(dir);
        }
    }
}

