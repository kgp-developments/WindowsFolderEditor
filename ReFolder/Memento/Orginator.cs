using ReFolder.Dir;
using ReFolder.Management;
using System;


namespace ReFolder.Memento
{

    public  class Orginator
    {
        private Caretaker caretaker;
        public IEditableDirWithChildren State { get; private set; }

        public Orginator(Caretaker caretaker)
        {
            this.caretaker = caretaker;
        }

        public Memento Save(IEditableDirWithChildren state)
        {
            this.State = state;
            return new Memento(State, caretaker);
        }

        public IEditableDirWithChildren Restore(Memento memento)
        {
            if (memento == null) throw new ArgumentNullException("memento is null ");

            State = SaveAndReadElementInBinaryFile.GetDefaultInstance().ReadFromBinaryFile<IEditableDirWithChildren>(memento.FilePath);

            return State;
        }
    }
}

