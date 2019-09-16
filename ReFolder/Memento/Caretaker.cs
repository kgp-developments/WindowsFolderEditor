﻿using System;
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
            Console.WriteLine("dodaję memento");

            if (currentMemento< Mementos.Count - 1)
            {
                int mementosToDelete = Mementos.Count - (currentMemento + 1);
                Console.WriteLine("mementos to delete" + mementosToDelete);

                Mementos.RemoveRange(++currentMemento, mementosToDelete);
                Mementos.Add(memento);
            }else
            {
                Mementos.Add(memento);
                currentMemento++;
                
            }

            Console.WriteLine("liczba memento w mementos " + Mementos.Count);
        }
        public static Memento GetMemento(int index)
        {
            if (index < 0 && index >= Mementos.Count) throw new ArgumentException("index is too big/ small");

            Console.WriteLine("pobieram memento o indeksie " + index+" max index wynosi "+ (Mementos.Count-1));
            currentMemento = index;          
            return Mementos[currentMemento];
        }
        internal static string GenerateMementoName()
        {
            return $"memento~{Mementos.Count}";
        }
    }
 
        
}

