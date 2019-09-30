using ReFolder.Dir;
using System.IO;
using ReFolder.Dir.Description;
using System;
using System.Collections.Generic;
using ReFolder.Management.Interfaces;

namespace ReFolder.Management
{

    ///<summary>
    ///DirRead 
    ///Contains method for reading existing folders in system
    ///</summary>
    [Serializable]
    public class DirRead: IDirRead
    {
        #region singleton
        private static DirRead InstanceDirRead { get; set; }
        //singleton

        public static DirRead GetDefaultInstance()
        {
            if (InstanceDirRead == null)
            {
                InstanceDirRead = new DirRead();
            }
            return InstanceDirRead;
        }
        #endregion

        // zwraca wszystkie foldery podrzędne danego folderu 
        private DirectoryInfo[] GetAllChildrenFolders(string fullName)
        {
            return new DirectoryInfo(fullName).GetDirectories();
        }
        // zwraca folder nadrzędny 
        private DirectoryInfo GetParentFolder(string fullName)
        {
            DirectoryInfo directory = new DirectoryInfo(fullName);
            return directory;
        }
        /// <summary>
        /// returns the names of all child folders that contains the folder under the path
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns>returns the names of all child folders that contains the folder under the path</returns>
        internal string[] GetChildrenNames(string fullName)
        {
            if (String.IsNullOrWhiteSpace(fullName)) throw new ArgumentException("fullname is empty/null");
            DirectoryInfo[] directories = new DirectoryInfo(fullName).GetDirectories();
            string[] names = new string[directories.Length];

            for (int i = 0; i < directories.Length; i++)
            {
                names[i] = directories[i].Name;

            }

            return names;
        }
        /// <summary>
        /// returns all folders names that contain a given folder
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns>returns all folders names that contain a given folder</returns>
        internal List<string> GetAllChildrenFullNames(string fullName)
        {
            List<string> names = new List<string>();
            if (String.IsNullOrWhiteSpace(fullName)) throw new ArgumentException("fullname is empty/null");
            DirectoryInfo[] directories = new DirectoryInfo(fullName).GetDirectories();
            foreach (var directory in directories)
            {
                names.Add(directory.FullName);
                if(directory.GetDirectories().Length > 0)
                {
                    GetAllChildrenFullNames(directory.FullName, names);
                }
            }  
            return names;
        }
        private List<string> GetAllChildrenFullNames(string fullName, List<string> names)
        {
            if (String.IsNullOrWhiteSpace(fullName)) throw new ArgumentException("fullname is empty/null");
            DirectoryInfo[] directories = new DirectoryInfo(fullName).GetDirectories();
            foreach (var directory in directories)
            {
                names.Add(directory.FullName);
            }
            return names;
        }
        /*  Method Info
         * metoda powinna być użyta tylko raz do wygenerowania punktu początkowego drzewa(root) ponieważ tworzy ona NOWY OBIEKT 
         * a nie zwraca do niego referencji.
         * Powiązania powinny być zachowane a nie tworzone osobno dla każdego folderu  
       */
        /// <summary>
        /// returns new MainDir with auto generated name and path
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns> returns new MainDir </returns>

        internal IEditableDirWithChildren GetMainDirFolder(string fullName)
        {
            if (String.IsNullOrWhiteSpace(fullName)) throw new ArgumentException("fullname is empty/null");
            DirectoryInfo dir =GetParentFolder(fullName);
            DirDescription dirDescription = new DirDescription(dir.FullName, dir.Name);

            return new MainDir(dirDescription);
        }






    }
}
