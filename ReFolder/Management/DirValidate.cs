using ReFolder.Dir;
using System.IO;
using System;

namespace ReFolder.Management
{
    
    public class DirValidate
    {
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


        //sprawdza czy istnieje dany dir jako folder
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
        public bool IsNameExistingAsChildInParrentDir(IEditableDirWithChildren ParentDir, string name)
        {
            IfNullOrWhitespaceThrowException(name);
            IfNullThrowException(ParentDir);
            bool exist = false;
            foreach (IEditableDirWithChildrenAndParent child in ParentDir.Children)
            {             
                if (name == child.Description.Name)
                {
                    exist = true;
                }             
            }
            return exist;
        }
        // sprawdza czy Name istnieje wśród dzieci 
        public bool IsNameExistingAsChild(IEditableDirWithChildren parent, string dirName)
        {
            IfNullOrWhitespaceThrowException(dirName);
            IfNullThrowException(parent);

           
            foreach (var child in parent.Children)
            {
                if (child.Description.Name.Equals(dirName))
                {
                    return true;
                }   
            }
            return false;
            
        }
        //sprawdza czy istnieje dany dir jako dziecko i folder
        public bool IsDirExistingAsFolderAndChild(IEditableDirWithChildren ParentDir, string name)
        {
            IfNullOrWhitespaceThrowException(name);
            IfNullThrowException(ParentDir);

            if (IsNameExistingAsChild(ParentDir, name))
            {
                return true;

            }
            else if (IsfolderExisting($"{ParentDir.Description.FullName}\\{name}"))
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
  