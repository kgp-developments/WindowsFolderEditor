using ReFolder.Dir;
using ReFolder.Management;
using System;
namespace ReFolder.Memento
{
    public class Memento
    {
        Caretaker caretaker;

        public IEditableDirWithChildren State { get; }
        public string FilePath { get; }

        public Memento(IEditableDirWithChildren state, Caretaker caretaker)
        {
            this.caretaker = caretaker ?? throw new ArgumentNullException("caretaker is null");
            State = state ?? throw new ArgumentNullException("state is null ");

            FilePath = $"..\\..\\..\\TemporaryFiles\\Mementos\\{caretaker.GenerateMementoName()}";

            SaveAndReadElementInBinaryFile.GetDefaultInstance().WriteToBinaryFile(FilePath, state);
        }


    }
}
