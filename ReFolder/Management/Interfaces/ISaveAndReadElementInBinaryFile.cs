namespace ReFolder.Management
{
    public interface ISaveAndReadElementInBinaryFile
    {
        void WriteToBinaryFile<T>(string filePath, T objectToWrite);
        T ReadFromBinaryFile<T>(string filePath);
    }
}
