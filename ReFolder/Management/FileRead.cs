using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ReFolder.Management
{
    public class FileRead
    {
        private static FileRead InstanceFileRead { get; set; }
        public static FileRead GetDefaultInstance()
        {
            if (InstanceFileRead == null)
            {
                InstanceFileRead = new FileRead();
            }
            return InstanceFileRead;
        }

        public List<string> ReadAllText(string path)
        {
            if (path == null || !File.Exists(path))
                throw new ArgumentException("path is not valid");

            List<string> readedLines = new List<string>();
            using (StreamReader reader= new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    readedLines.Add(reader.ReadLine());
                }
                
            }
            return readedLines;
        }
        public string ReadLineThatContainsValue(string path, string value)
        {
            if (path == null || !File.Exists(path))
                throw new ArgumentException("path is not valid");
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var val = reader.ReadLine();
                    if (val.Contains(value))
                        return val;
                }
            }
            return null;
        }
        public string GetDataFromString(string value , char separator)
        {
            if (value == null)
                throw new ArgumentNullException("arguments are null");
            string[] data = value.Split(separator);
            return (data[data.Length - 1]);
        }
    }
}
