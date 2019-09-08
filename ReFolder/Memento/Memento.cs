using ReFolder.Dir;
using ReFolder.Management;
using System;
namespace ReFolder.Memento
{
    public class Memento
    {
        public IEditableDirWithChildren State { get; }
        public string FilePath { get; } = $"..\\..\\..\\TemporaryFiles\\Mementos\\{Caretaker.GenerateMementoName()}";

        internal Memento(IEditableDirWithChildren state)
        {
            if (state == null) throw new ArgumentNullException("state is null ");
            State = state;
            SaveAndReadElementInBinaryFile.GetDefaultInstance().WriteToBinaryFile<IEditableDirWithChildren>(FilePath, state);
        }


    }
}
