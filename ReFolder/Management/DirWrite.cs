using ReFolder.Dir;
using System;
using System.IO;


namespace ReFolder.Management
{
    // dodaj możliwość usuwania folderów 
    public class DirWrite 
    {
        //Singleton
        private static  DirWrite InstanceDirWrite { get; set; }
        private DirValidate DirValidate{ get; set; }

        public DirWrite( DirValidate dirValidate)
        {
            this.DirValidate = dirValidate;
        }

        // zwraca zwykłą instancję 
        public static DirWrite GetDefaultInstance()
        {
            if (InstanceDirWrite == null)
            {
                InstanceDirWrite = new DirWrite(DirValidate.GetDefaultInstance());
            }
            return InstanceDirWrite;
        }
        // cele testów
        public static DirWrite GetInstance( DirValidate dirValidate)
        {
            if (InstanceDirWrite == null)
            {
                InstanceDirWrite = new DirWrite(dirValidate);
            }
            return InstanceDirWrite;
        }
        // tworzy folder
        private void CreateFolder(string fullName, string note, string IconAddress)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(fullName);
            directoryInfo.Create();
            FileWrite.GetDefaultInstance().ReplaceSystemFolderInfoFile(fullName , note,IconAddress);
        }

        // tworzy plik systemowy desktop.ini który przechowuje notatkę oraz ikonę
        public void AddIconAndNoteToFileSystem(string fullName, string note, string IconAddress)
        {

                string[] lines = { "[.ShellClassInfo]", $"IconResource={IconAddress},0", $"IconFile={IconAddress}", $"IconIndex=0", $"InfoTip={note}" };
                foreach (var item in lines)
                {
                    Console.WriteLine(item);
                }
                
                File.WriteAllLines(fullName + @"\desktop.ini", lines);
                File.SetAttributes(fullName + @"\desktop.ini", FileAttributes.Hidden | FileAttributes.System);
                File.SetAttributes(fullName, FileAttributes.System);
            
        }

        // szykuje adres folderu do zapisu i sprawdza czy dany folder już nie istnieje 
        private  string ReadyFullName(string fullName)
        {
            if (fullName == null) throw new ArgumentNullException();
            fullName.Trim();
            if (!fullName.Contains("\\") || fullName.Equals("") ||fullName.EndsWith("\\")) throw new ArgumentException();
           
            DirectoryInfo directoryInfo = new DirectoryInfo(fullName);
            if (directoryInfo.Exists)
            {
                throw new InvalidOperationException($"folder at the path {fullName} alredy exist");
            }
            else
            {
                return fullName;
            }

        }
        // tworzy nowy folder w systemie i zwraca boola jeśli go utworzy
        private  bool CreateNewFolder(string fullName, string Name, string note, string IconAddress)
        {

            try
            {
                string name = ReadyFullName(fullName);
                if (fullName != name)
                    throw new ArgumentException("path is not valid");

                CreateFolder(fullName,note, IconAddress);
                return true;
            }
            catch ( InvalidOperationException operE){
                Console.WriteLine(operE.Message);
                return false;
            }catch(IOException IOE)
            {
                Console.WriteLine(IOE.Message);
                Console.WriteLine(IOE.InnerException);
                Console.WriteLine(IOE.StackTrace);
                throw IOE;
            }

        }    
        // usuwa folder z systemu 
        public  void DeleteFolder(string fullName)
        {
            DirectoryInfo info = new DirectoryInfo(fullName);
                info.Delete();
        }
        // generuje wszystkie foldery z pamięci(z klasy MemoryDirs) jeśli nie istnieją 
        public void GenerateAllChildrenDirsAsFolders()
        {
            foreach (IEditableDirWithChildrenAndParent dir in MemoryDirs.AllCreatedDirs)
            {
                if (!DirValidate.IsfolderExisting(dir.Description.FullName))
                {
                    CreateNewFolder(dir.Description.FullName, dir.Description.Name, dir.Description.Note, dir.Description.IconAddress);
                }
                else continue;

            }
        }
    }
}
