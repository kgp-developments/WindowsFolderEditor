using System;
using System.Collections.Generic;
using System.Text;
using ReFolder.Dir;
namespace ReFolder.Memento
{
    public static class Caretaker
    {
        private static int currentMemento = -1;
        public static int CurrentMemento {
            get{
                return currentMemento;
            }
        }
        private static List<Memento> Mementos = new List<Memento>();
        public static int CountMemento()
        {
            return Mementos.Count;
        }
        public static void AddMemento(Memento memento)
        {
            if (memento == null) throw new ArgumentNullException("memento is null");

            if (currentMemento< Mementos.Count - 1)
            {
                int mementosToDelete = Mementos.Count - (currentMemento + 1);

                Mementos.RemoveRange(++currentMemento, mementosToDelete);
                Mementos.Add(memento);
            }else
            {
                Mementos.Add(memento);
                currentMemento++;
                
            }

        }
        public static Memento GetMemento(int index)
        {
            if (index < 0 && index >= Mementos.Count) throw new ArgumentException("index is too big/ small");

            currentMemento = index;          
            return Mementos[currentMemento];
        }
        internal static string GenerateMementoName()
        {
            return $"memento~{Mementos.Count}";
        }
    }
 
        
}


