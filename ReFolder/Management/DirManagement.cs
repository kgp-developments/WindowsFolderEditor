using ReFolder.Dir;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ReFolder.Management
{
    public class DirManagement
    {
        private DirWrite dirWrite;
        private DirRead dirRead;
        private MemoryDirs memoryDirs;
        //Singleton
        private static DirManagement InstanceDirManagement { get; set; }


        //konstruktor do  wstrzykiwania singletonów przez metody
        private DirManagement(DirWrite dirWrite, DirRead dirRead, MemoryDirs memoryDirs)
        {
            this.dirWrite = dirWrite;
            this.dirRead = dirRead;
            this.memoryDirs = memoryDirs;
        }

        
        // zwraca zwykłą instancję
        public static DirManagement GetDefaultInstance()
        {
            if (InstanceDirManagement == null)
            {
                InstanceDirManagement = new DirManagement(DirWrite.GetInstance(),DirRead.GetInstance(), MemoryDirs.GetInstance());
            }
            return InstanceDirManagement;
        }
        // zwraca instancję z możliwością wstrzyknięcia zależności(na potrzeby testów )
        public static DirManagement InitializeInstance(DirWrite dirWrite, DirRead dirRead, MemoryDirs memoryDirs)
        {
            if (InstanceDirManagement == null)
            {
                InstanceDirManagement = new DirManagement(dirWrite,dirRead, memoryDirs);
            }
            return InstanceDirManagement;
        }


        // generuje wszystkie foldery z pamięci(z klasy MemoryDirs) jeśli nie istnieją 
        public void GenerateAllChildrenDirsAsFolders()
        {
            foreach (IEditableDirWithChildrenAndParrent dir in MemoryDirs.AllCreatedDirs)
            {
                if (!DirValidate.isDirExisting(dir))
                {
                    dirWrite.CreateNewFolder(dir.Description.FullName);
                }
                else continue;

            }
        }
        // usuwa folder z pamięci i z systemu 
        public void DeleteChildFolder(IEditableDirWithChildrenAndParrent dir)
        {
            memoryDirs.DeleteDirFromAllCreatedDirs(dir);
            dirWrite.DeleteFolder(dir.Description.FullName);

        }
        // tworzy nowy MainDir na podstawie ścieżki 
        public MainDir GetFolderAsNewMainDir(string fullName)
        {
            return dirRead.GetMainDirFolder(fullName);
        }

        // zarządzanie wszystkimi wygenerowanymi dirami przez osobną klasę wewnętrzną 
        public class MemoryDirs
        {

            //singleton
            private static MemoryDirs InstanceMemoryDirs { get; set; }


            // zwraca instancję
            public static MemoryDirs GetInstance()
            {
                if (InstanceMemoryDirs == null)
                {
                    InstanceMemoryDirs = new MemoryDirs();
                }
                return InstanceMemoryDirs;
            }


            // posiada dane wszystkich utworzonych folderów za wyjątkiem folderu głównego
            public static List<IEditableDirWithChildrenAndParrent> AllCreatedDirs { get; } = new List<IEditableDirWithChildrenAndParrent>();
            //inicjalizuje listę wszystkich folderów
            public void InitializeAllChildren(IEditableDirWithChildren dir)
            {

                foreach (IEditableDirWithChildrenAndParrent childDir in dir.Children)
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
            public void DeleteDirFromAllCreatedDirs(IEditableDirWithChildrenAndParrent dir)
            {
                AllCreatedDirs.Remove(dir);
            }
            // usuwa foldery z wszystkich folderów w pamięci  
            public void DeleteDirsFromAllCreatedDirs(List<IEditableDirWithChildrenAndParrent> childDirs)
            {
                foreach (IEditableDirWithChildrenAndParrent dir in childDirs)
                    DeleteDirFromAllCreatedDirs(dir);
            }
        }
    }


}
