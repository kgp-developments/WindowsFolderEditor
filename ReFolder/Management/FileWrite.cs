using System;
using System.Collections.Generic;
using System.IO;


namespace ReFolder.Management
{
    ///<summary>
    ///FileWrite implements IFileWrite.
    ///Contains methods for writing to file and creating files 
    ///</summary>
    [Serializable]
    public class FileWrite: IFileWrite
    {
        #region singleton
        private static FileWrite InstanceFileWrite { get; set; }
        private readonly DirWrite dirWrite;
        public static FileWrite GetDefaultInstance()
        {
            if (InstanceFileWrite == null)
            {
                InstanceFileWrite = new FileWrite( DirWrite.GetDefaultInstance());
            }
            return InstanceFileWrite;
        }
        public FileWrite(DirWrite dirWrite)
        {
            this.dirWrite = dirWrite;
        }

        #endregion 

        /// <summary>
        /// Creates/ Updates file. Write all lines to file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="lines">list of lines to write</param>
        /// <returns> list of lines to write</returns>
        public List<string> WriteAllLines(string path, List<string> lines, bool append= false)
        {
            using (StreamWriter writer= new StreamWriter(path, append))
            {
                foreach (string line in lines)
                {
                    writer.WriteLine(line);
                }
                return lines;
            }
        }
       
        // jeśli desktop.ini istnieje to ReplaceSystemFolderInfoFile usuwa go i tworzy nowy a jeśli nie istnieje tworzy nowy
        /// <summary>
        /// overwrites desktop.ini
        /// </summary>
        /// <param name="path">Existing Folder Path</param>
        /// <param name="note">Existing Folder new note</param>
        /// <param name="iconAddress">Existing Folder new IconAddress</param>
        internal void ReplaceSystemFolderInfoFile(string path, string note, string iconAddress)
        {        
            DirectoryInfo dir = new DirectoryInfo(path);
            dir.Attributes = FileAttributes.Normal;

            string filePath = path + @"\desktop.ini";
            FileInfo file = new FileInfo(filePath);
            if (!file.Exists)
            {
                AddIconAndNoteToFileSystem(path, note, iconAddress);
            }else
            {
                file.Delete();
                AddIconAndNoteToFileSystem(path, note, iconAddress);
            }
                
        }
        /// <summary>
        /// creates desktop.ini
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="note"></param>
        /// <param name="IconAddress"></param>
        internal void AddIconAndNoteToFileSystem(string fullName, string note, string IconAddress)
        {
            IconAddress = Path.GetFullPath(IconAddress);
            if (!File.Exists(IconAddress)) throw new ArgumentException(IconAddress + "File Don't exist");
            string[] lines = { "[.ShellClassInfo]", $"IconResource={IconAddress},0", $"IconFile={IconAddress}", $"IconIndex=0", $"InfoTip={note}"};
            foreach (var item in lines)

            File.WriteAllLines(fullName + @"\desktop.ini", lines);
            File.SetAttributes(fullName + @"\desktop.ini", FileAttributes.Hidden | FileAttributes.System);
            File.SetAttributes(fullName, FileAttributes.System);

        }
    }
}
