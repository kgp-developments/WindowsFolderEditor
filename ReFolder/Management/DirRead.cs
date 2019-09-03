using ReFolder.Dir;
using System.IO;
using ReFolder.Dir.Description;

namespace ReFolder.Management
{
    //dokończ
      public class DirRead
    {

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

        // zwraca wszystkie foldery podrzędne danego folderu 
        private DirectoryInfo[] GetAllChildrenFolders(string fullName)
        {
            return new DirectoryInfo(fullName).GetDirectories();
        }
        // zwraca folder nadrzędny 
        private DirectoryInfo GetParrentFolder(string fullName)
        {
            DirectoryInfo directory = new DirectoryInfo(fullName);
            return directory;
        }     


        /*  Method Info
         * metoda powinna być użyta tylko raz do wygenerowania punktu początkowego drzewa(root) ponieważ tworzy ona NOWY OBIEKT 
         * a nie zwraca do niego referencji.
         * Powiązania powinny być zachowane a nie tworzone osobno dla każdego folderu  
       */
        public MainDir GetMainDirFolder(string fullName)
        {
            DirectoryInfo dir =GetParrentFolder(fullName);
            DirDescription dirDescription = new DirDescription(dir.FullName, dir.Name);

            return new MainDir(dirDescription);
        }
    }
}
