using ReFolder.Dir;
using System;
using System.IO;


namespace ReFolder.Management
{
    ///<summary>
    ///DirWrite implements IDirWrite.
    ///Contains methods for creating folders from MemoryDirs and deleting existing folders
    ///</summary>
    [Serializable]
    public class DirWrite: IDirWrite
    {
        #region singleton
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
        #endregion
       
        // tworzy folder
        private void CreateFolder(string fullName, string note, string IconAddress)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(fullName);
            directoryInfo.Create();
            FileWrite.GetDefaultInstance().ReplaceSystemFolderInfoFile(fullName , note,IconAddress);
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
        /// <summary>
        /// creates new folder
        /// </summary>
        /// <param name="fullName"> new folder path</param>
        /// <param name="note"> new folder note</param>
        /// <param name="IconAddress"> new folder IconAddress</param>
        /// <returns></returns>
        internal bool CreateNewFolder(string fullName, string note, string IconAddress)
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
        /// <summary>
        /// deletes folder from system
        /// </summary>
        /// <param name="fullName">Path</param>
        public  void DeleteFolder(string fullName)
        {
            DirectoryInfo info = new DirectoryInfo(fullName);
                info.Delete();
        }
        /// <summary>
        /// generates all Dirs from memory(MemoryDirs) if they dont exist in system
        /// </summary>
        public void GenerateAllChildrenDirsAsFolders()
        {
            foreach (IEditableDirWithChildrenAndParent dir in MemoryDirs.AllCreatedDirs)
            {
                if (!DirValidate.IsfolderExisting(dir.Description.FullName))
                {
                    CreateNewFolder(dir.Description.FullName, dir.Description.Note, dir.Description.IconAddress);
                }
                else continue;

            }
        }
    }
}
