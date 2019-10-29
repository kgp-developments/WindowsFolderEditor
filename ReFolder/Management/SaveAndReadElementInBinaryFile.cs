using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace ReFolder.Management
{
    ///<summary>
    ///SaveAndReadElementInBinaryFile implements ISaveAndReadElementInBinaryFile. 
    ///Contains methods for creating, reading And updating binary files
    ///</summary>
    public class SaveAndReadElementInBinaryFile : ISaveAndReadElementInBinaryFile
    {
        #region singleton
        private static SaveAndReadElementInBinaryFile Instance { get; set; }
        public static SaveAndReadElementInBinaryFile GetDefaultInstance()
        {
            if (Instance == null)
            {
                Instance = new SaveAndReadElementInBinaryFile();
            }
            return Instance;
        }
        #endregion
        /// <summary>
        /// Creates/ Updates binary file
        /// </summary>
        /// <typeparam name="T">type of Class to create/update</typeparam>
        /// <param name="filePath">path to file</param>
        /// <param name="objectToWrite">objectToWrite</param>
        public void WriteToBinaryFile<T>(string filePath, T objectToWrite)
        {
            if (String.IsNullOrWhiteSpace(filePath)) throw new ArgumentException(" filepath is null/empty/whitespace");
            if (objectToWrite == null) throw new ArgumentNullException("object to write is null");
            Stream stream = File.Open(filePath, FileMode.Create);
            new BinaryFormatter().Serialize(stream, objectToWrite);
            stream.Close();
        }



        /// <summary>
        /// Reads binary file
        /// </summary>
        /// <typeparam name="T">Type of class to read</typeparam>
        /// <param name="filePath">path to file</param>
        /// <returns>object of type T</returns>
        public T ReadFromBinaryFile<T>(string filePath)
        {
            if (String.IsNullOrWhiteSpace(filePath)) throw new ArgumentException(" filepath is null/empty/whitespace");
            Stream stream = File.Open(filePath, FileMode.Open);

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            T file = (T)binaryFormatter.Deserialize(stream);
            stream.Close();
            return file;
        }
    }
}
