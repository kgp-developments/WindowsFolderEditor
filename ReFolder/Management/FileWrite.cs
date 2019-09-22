using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ReFolder.Management
{
    public class FileWrite
    {
        private static FileWrite InstanceFileWrite { get; set; }
        private FileRead fileRead;
        public static FileWrite GetDefaultInstance()
        {
            if (InstanceFileWrite == null)
            {
                InstanceFileWrite = new FileWrite(FileRead.GetDefaultInstance());
            }
            return InstanceFileWrite;
        }
        public FileWrite(FileRead fileRead)
        {
            this.fileRead = fileRead;
        }
        public List<string> WriteAllLines(string path, List<string> lines)
        {
            using (StreamWriter writer= new StreamWriter(path))
            {
                foreach (string line in lines)
                {
                    writer.WriteLine(line);
                }
                return lines;
            }
        }
        // jeśli desktop.ini istnieje to ReplaceSystemFolderInfoFile usuwa go i tworzy nowy a jeśli nie istnieje tworzy nowy
        public void ReplaceSystemFolderInfoFile(string path, string note, string iconAddress)
        {        
            DirectoryInfo dir = new DirectoryInfo(path);
            dir.Attributes = FileAttributes.Normal;

            string filePath = path + @"\desktop.ini";
            FileInfo file = new FileInfo(filePath);
            if (!file.Exists)
            {
                DirWrite.GetDefaultInstance().AddIconAndNoteToFileSystem(path, note, iconAddress);
            }else
            {
                file.Delete();
                DirWrite.GetDefaultInstance().AddIconAndNoteToFileSystem(path, note, iconAddress);
            }
                
        }
    }
}
