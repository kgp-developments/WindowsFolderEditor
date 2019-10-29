using ReFolder.Management;
using System;
using System.Collections.Generic;
using System.IO;

namespace ReFolder.TxtFileWriter
{
    /// <summary>
    /// Creates txt log in created MainDir about folders that MainDir contains.
    /// </summary>
    public class TxtFileWriter
    {
        public TxtFileEditor TxtFileEditor { get; }
        private DirValidate dirValidate;

        private static TxtFileWriter instanceTxtFileWriter;
        public static TxtFileWriter GetDefaultInstance()
        {
            if (instanceTxtFileWriter == null)
            {
                instanceTxtFileWriter = new TxtFileWriter();
                return instanceTxtFileWriter;
            }
            else
            {
                return instanceTxtFileWriter;
            }
        }
        public static TxtFileWriter GetInstance(DirValidate dirValidator, TxtFileEditor editor)
        {
            return new TxtFileWriter(dirValidator, editor); ;
        }
        public TxtFileWriter(DirValidate dirValidator, TxtFileEditor editor)
        {
            this.TxtFileEditor = editor;
            this.dirValidate = dirValidator;
        }
        public TxtFileWriter()
        {
            this.dirValidate = DirValidate.GetDefaultInstance();
            TxtFileEditor = new TxtFileEditor(this.dirValidate, DirRead.GetDefaultInstance(), FileRead.GetDefaultInstance());
        }
        public void WriteListToFile(string path, List<string> stringToWrite, string fileName = @"/AboutFolders.txt")
        {
            stringToWrite.Sort();

            if (String.IsNullOrWhiteSpace(path))
                throw new ArgumentException("path is null or whiteSpace");

            if (stringToWrite == null)
                throw new ArgumentNullException("list is null");

            if (!dirValidate.IsfolderExisting(path))
                throw new ArgumentException("folder path isn't valid ");

            string systemUserName = Environment.UserName;

            if (!File.Exists(path + fileName))
            {
                using (StreamWriter writer = new StreamWriter(path + fileName))
                {
                    writer.WriteLine(DateTime.Now + ": created by: " + systemUserName);
                    writer.WriteLine();
                    foreach (var txt in stringToWrite)
                    {
                        writer.WriteLine(txt);
                    }
                }
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(path + fileName, true))
                {
                    writer.WriteLine();
                    writer.WriteLine(DateTime.Now + ": edited by: " + systemUserName);
                    writer.WriteLine();
                    foreach (var txt in stringToWrite)
                    {
                        writer.WriteLine(txt);
                    }

                }
            }
        }
    }
}
