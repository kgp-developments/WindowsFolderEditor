using ReFolder.Dir;
using ReFolder.Management;

namespace ReFolder.Memento
{
    public class Memento
    {
        public IEditableDirWithChildren State { get; }
        public string FilePath { get; } = $"..\\..\\..\\..\\TemporaryFiles\\Mementos\\{Caretaker.GenerateMementoName()}";

        public Memento(IEditableDirWithChildren state)
        {
            State = state;
            SaveAndReadElementInBinaryFile.GetDefaultInstance().WriteToBinaryFile<IEditableDirWithChildren>(FilePath, state);
        }


    }
}
