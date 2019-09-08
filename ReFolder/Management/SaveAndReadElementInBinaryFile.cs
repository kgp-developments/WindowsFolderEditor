using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;


namespace ReFolder.Management
{
    public class SaveAndReadElementInBinaryFile
    {
        // singleton
        private static SaveAndReadElementInBinaryFile Instance { get; set; }
        public static SaveAndReadElementInBinaryFile GetDefaultInstance()
        {
            if (Instance == null)
            {
                Instance = new SaveAndReadElementInBinaryFile();
            }
            return Instance;
        }

        // zapisuje obiekt typu T do pliku pod wskazaną ścieżką.
        public void WriteToBinaryFile<T>(string filePath, T objectToWrite)
        {
            if (String.IsNullOrWhiteSpace(filePath)) throw new ArgumentException(" filepath is null/empty/whitespace");
            if (objectToWrite == null) throw new ArgumentNullException("object to write is null");
            Stream stream = File.Open(filePath, FileMode.Create);
            new BinaryFormatter().Serialize(stream, objectToWrite);
            stream.Close();
        }
        // odczytuje obiekt typu T do pliku pod wskazaną ścieżką.
        public T ReadFromBinaryFile<T>(string filePath)
        {
            if (String.IsNullOrWhiteSpace(filePath)) throw new ArgumentException(" filepath is null/empty/whitespace");
            Stream stream = File.Open(filePath, FileMode.Open);

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            T file = (T)binaryFormatter.Deserialize(stream);
            stream.Close();
            return file;
        }
        //usuwa plik 
        public void DeleteFile(string filePath)
        {
            if (String.IsNullOrWhiteSpace(filePath)) throw new ArgumentException(" filepath is null/empty/whitespace");
            DirectoryInfo info = new DirectoryInfo(filePath);
            if (!File.Exists(filePath)) throw new ArgumentException("file doesn't exists");
            File.Delete(filePath);

        }
    }
}
