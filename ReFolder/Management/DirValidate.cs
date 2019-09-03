using ReFolder.Dir;
using System.IO;

namespace ReFolder.Management
{
    //Sprawna
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
        public bool  IsDirExistingAsFolder(string fullName)
        {
            DirectoryInfo info = new DirectoryInfo(fullName);
            if (info.Exists)
            {
                return true;
            }
            else return false;

        }
        //sprawdza czy istnieje dany dir jako dziecko
        public bool IsNameExistingAsChild(IEditableDirWithChildren parrentDir, string name)
        {
            bool exist = false;
            foreach (IEditableDirWithChildrenAndParrent child in parrentDir.Children)
            {             
                if (name == child.Description.Name)
                {
                    exist = true;
                }             
            }
            return exist;
        }
        //sprawdza czy istnieje dany dir jako dziecko i folder
        public bool IsDirExistingAsFolderAndChild(IEditableDirWithChildren parrentDir, string name)
        {
            if (IsNameExistingAsChild(parrentDir, name))
            {
                return true;

            }
            else if (IsDirExistingAsFolder($"{parrentDir.Description.FullName}\\{name}"))
            {

                return true;
            }
            else return false;
        }
    }
}
