using System;
using System.IO;

namespace ReFolder.Management
{
    // dodaj możliwość usuwania folderów 
    public class DirWrite 
    {
        //Singleton
        private static  DirWrite InstanceDirWrite { get; set; }

         public static DirWrite GetInstance()
        {
            if (InstanceDirWrite == null)
            {
                InstanceDirWrite = new DirWrite();
            }
            return InstanceDirWrite;
        }


        // tworzy folder
        private  void CreateFolder(string fullName)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(fullName);
            directoryInfo.Create();
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

        public  bool CreateNewFolder(string fullName)
        {
            try
            {
                string name = ReadyFullName(fullName);
                CreateFolder(name);
                return true;
            }
            catch ( InvalidOperationException operE){
                Console.WriteLine(operE.Message);
                return false;
            }catch(IOException IOE)
            {
                Console.WriteLine(IOE.Message);
                throw IOE;
            }

        }    
        // usuwa folder z systemu 
        public  void DeleteFolder(string fullName)
        {
            DirectoryInfo info = new DirectoryInfo(fullName);
                info.Delete();
        }
    }
}
