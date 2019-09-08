using ReFolder.Dir;
using ReFolder.Management;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReFolder.Memento
{

    public static class Orginator
    {
        public static IEditableDirWithChildren State { get; set; }
        public static Memento Save()
        {
            return new Memento(State);
        }

        public static IEditableDirWithChildren Restore(Memento memento)
        {
            if (memento == null) throw new ArgumentNullException("memento is null ");
            State = SaveAndReadElementInBinaryFile.GetDefaultInstance().ReadFromBinaryFile<IEditableDirWithChildren>(memento.FilePath);
            return State;
        }
    }
}

