using System;
using System.Collections.Generic;
using System.Text;
using ReFolder.Dir;
namespace ReFolder.Memento
{
    public class Caretaker
    {
        /// <summary>
        /// memento counter
        /// </summary>
        private static int currentMemento = -1;

        /// <summary>
        /// returns memento couter
        /// </summary>
        public int CurrentMemento {
            get{
                return currentMemento;
            }
        }
        private List<Memento> Mementos = new List<Memento>();

        /// <summary>
        /// Returns the number of elements in memento list
        /// </summary>
        /// <returns></returns>
        public int CountMemento()
        {
            return Mementos.Count;
        }

        /// <summary>
        /// adds new memento to memento list
        /// </summary>
        /// <param name="memento"></param>
        public void AddMemento(Memento memento)
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

        /// <summary>
        /// returns specified memento 
        /// </summary>
        /// <param name="index">specified memento numver</param>
        /// <returns></returns>
        public Memento GetMemento(int index)
        {
            if (index < 0 && index >= Mementos.Count) throw new ArgumentException("index is too big/ small");
            currentMemento = index;          
            return Mementos[currentMemento];
        }


        /// <summary>
        /// reset memento list and returns reference to it 
        /// </summary>
        /// <returns> memento list </returns>
        public List<Memento> ResetMementoList()
        {
            Mementos = new List<Memento>();
            return Mementos;
        }
        internal string GenerateMementoName()
        {
            return $"memento~{Mementos.Count}";
        }
    }
 
        
}


