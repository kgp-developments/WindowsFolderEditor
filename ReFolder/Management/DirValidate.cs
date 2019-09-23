using ReFolder.Dir;
using System.IO;
using System;

namespace ReFolder.Management
{
    ///<summary>
    ///DirValidate implements IDirValidate 
    ///Contains method for validating existence of existing folders and Dirs
    ///</summary>
    public class DirValidate: IDirValidate
    {
        #region singleton
        private static DirValidate InstanceDirValidate { get; set; }

        public static DirValidate GetDefaultInstance()
        {
            if (InstanceDirValidate== null)
            {
                InstanceDirValidate = new DirValidate();
                return InstanceDirValidate;
            }
            return InstanceDirValidate;
        }
        #endregion

        //sprawdza czy istnieje dany dir jako folder
        /// <summary>
        /// Checks if the folder exist under the path
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns>returns true if folder exist</returns>
        public bool  IsfolderExisting(string fullName)
        {
            IfNullOrWhitespaceThrowException(fullName);
             DirectoryInfo info = new DirectoryInfo(fullName);
            if (info.Exists)
            {
                return true;
            }
            else return false;

        }
        //sprawdza czy istnieje dany dir jako dziecko
        /// <summary>
        /// checks if the ParrentDir contains dir with the given name
        /// </summary>
        /// <param name="ParentDir"></param>
        /// <param name="name"></param>
        /// <returns>returns true if ParentDir conains given name</returns>
        public bool IsNameExistingInChildrenDirs(IEditableDirWithChildren parent, string name)
        {
            IfNullOrWhitespaceThrowException(name);
            IfNullThrowException(parent);

           
            foreach (var child in parent.Children)
            {
                if (child.Description.Name.Equals(name))
                {
                    return true;
                }   
            }
            return false;
            
        }
        //sprawdza czy istnieje dany dir jako dziecko i folder
        /// <summary>
        /// Checks, if the ParrentDir contains dir with the given name and that the dir exist in file system
        /// </summary>
        /// <param name="ParentDir">Dir Parent</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsDirExistingAsFolderAndChild(IEditableDirWithChildren ParentDir, string name)
        {
            IfNullOrWhitespaceThrowException(name);
            IfNullThrowException(ParentDir);

            if (IsNameExistingInChildrenDirs(ParentDir, name))
            {
                return true;

            }
            if (IsfolderExisting($"{ParentDir.Description.FullName}\\{name}"))
            {

                return true;
            }
            else return false;
        }

        private void IfNullOrWhitespaceThrowException(string str, string msg="value is null/empty")
        {
            if (string.IsNullOrWhiteSpace(str)) throw new ArgumentException(msg);
        }
        private void IfNullThrowException(Object obj, string msg = "value is null")
        {
            if (obj == null) throw new ArgumentNullException(msg);
        }
    }
}
  