﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;


namespace ReFolder.Management
{
    public class SaveAndReadElementInBinaryFile
    {
        // singleton
        private static SaveAndReadElementInBinaryFile Instance { get; set; }


        public static SaveAndReadElementInBinaryFile GetInstance()
        {
            if (Instance == null)
            {
                Instance = new SaveAndReadElementInBinaryFile();
            }
            return Instance;
        }

        // zapisuje obiekt typu T do pliku pod wskazaną ścieżką.
        //  jeśli  plik o podanej nazwie istnieje to zostanie nadpisany w przeciwnym wypadku zostanie utworzony nowy plik
        public void WriteToBinaryFile<T>(string filePath, T objectToWrite)
        {
            Stream stream = File.Open(filePath, FileMode.Create);
            new BinaryFormatter().Serialize(stream, objectToWrite);
            stream.Close();
        }
        public T ReadFromBinaryFile<T>(string filePath)
        {

            Stream stream = File.Open(filePath, FileMode.Open);

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            T file = (T)binaryFormatter.Deserialize(stream);
            stream.Close();
            return file;
        }
        public void DeleteFile(string filepath)
        {
            DirectoryInfo info = new DirectoryInfo(filepath);
            if (!File.Exists(filepath)) throw new ArgumentException("file doesn't exists");
            File.Delete(filepath);

        }
    }
}
