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
            Stream stream = File.Open(filePath, FileMode.Create);
            new BinaryFormatter().Serialize(stream, objectToWrite);
            stream.Close();
        }
        // odczytuje obiekt typu T do pliku pod wskazaną ścieżką.
        public T ReadFromBinaryFile<T>(string filePath)
        {

            Stream stream = File.Open(filePath, FileMode.Open);

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            T file = (T)binaryFormatter.Deserialize(stream);
            stream.Close();
            return file;
        }
        //usuwa plik 
        public void DeleteFile(string filepath)
        {
            DirectoryInfo info = new DirectoryInfo(filepath);
            if (!File.Exists(filepath)) throw new ArgumentException("file doesn't exists");
            File.Delete(filepath);

        }
    }
}
