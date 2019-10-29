using System;
using System.Collections.Generic;
using System.IO;

namespace ReFolder.Management
{
    ///<summary>
    ///FileRead implements IFileRead.
    ///Contains methods for reading files
    ///</summary>
    [Serializable]
    public class FileRead : IFileRead
    {
        #region singleton
        private static FileRead InstanceFileRead { get; set; }
        public static FileRead GetDefaultInstance()
        {
            if (InstanceFileRead == null)
            {
                InstanceFileRead = new FileRead();
            }
            return InstanceFileRead;
        }
        #endregion

        /// <summary>
        /// Reads all lines of text
        /// </summary>
        /// <param name="path"></param>
        /// <returns>all lines of text</returns>
        public List<string> ReadAll(string path)
        {
            if (path == null || !File.Exists(path))
                throw new ArgumentException("path is not valid");

            List<string> readedLines = new List<string>();
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    readedLines.Add(reader.ReadLine());
                }

            }
            return readedLines;
        }

        /// <summary>
        /// reads all file and returns line that contains value, otherwise returns null if value is not found
        /// </summary>
        /// <param name="path"></param>
        /// <param name="value"></param>
        /// <returns></returns>
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
    }
}
