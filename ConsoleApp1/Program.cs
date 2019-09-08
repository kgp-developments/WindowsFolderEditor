
using ReFolder.Dir;
using ReFolder.Dir.Description;
using ReFolder.Management;
using ReFolder.Memento;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*            Console.Beep();
                        IEditableDirWithChildren main = DirManagement.GetDefaultInstance().GetFolderAsNewMainDir(@"C:\Users\Klakier\Desktop\kociFolderek");
                        Orginator.State = main;
                        Caretaker.AddMemento(Orginator.Save());
                        System.Console.WriteLine(main.Children.Count + " ilość dzieci____ current memento to: " + Caretaker.CurrentMemento + "\n");

                        IEditableDirWithChildrenAndParent child1 = new ChildDir("child1", main);
                        main.AddChildToChildrenList(child1);         
                        Orginator.State= main; 
                        Caretaker.AddMemento(Orginator.Save());
                        System.Console.WriteLine(main.Children.Count + " ilość dzieci____ current memento to: " + Caretaker.CurrentMemento  + "\n");

                        IEditableDirWithChildrenAndParent child2 = new ChildDir("child2", main);
                        main.AddChildToChildrenList(child2);
                        Orginator.State = main;
                        Caretaker.AddMemento(Orginator.Save());
                        System.Console.WriteLine(main.Children.Count + " ilość dzieci____ current memento to: " + Caretaker.CurrentMemento + "\n");

                        IEditableDirWithChildrenAndParent child3 = new ChildDir("child3", main);
                        main.AddChildToChildrenList(child3);
                        Orginator.State = main;
                        Caretaker.AddMemento(Orginator.Save());
                        System.Console.WriteLine(main.Children.Count+ " ilość dzieci____ current memento to: " + Caretaker.CurrentMemento + "\n");


                        System.Console.WriteLine("szykuję się do przywrócenia o 1 do tyłu");
                        main=Orginator.Restore(Caretaker.GetMemento(Caretaker.CurrentMemento - 1));
                        System.Console.WriteLine(main.Children.Count + " ilość dzieci____ current memento to: " + Caretaker.CurrentMemento + "\n");

                        System.Console.WriteLine("szykuję się do przywrócenia o 1 do przodu");
                        main = Orginator.Restore(Caretaker.GetMemento(Caretaker.CurrentMemento +1));
                        System.Console.WriteLine(main.Children.Count + " ilość dzieci____ current memento to: " + Caretaker.CurrentMemento + "\n");

                        System.Console.WriteLine("szykuję się do przywrócenia o 3 do tyłu");
                        main = Orginator.Restore(Caretaker.GetMemento(Caretaker.CurrentMemento - 3));
                        System.Console.WriteLine(main.Children.Count + " ilość dzieci____ current memento to: " + Caretaker.CurrentMemento + "\n");

                        System.Console.WriteLine("szykuję się do przywrócenia o 3 do przodu");
                        main = Orginator.Restore(Caretaker.GetMemento(Caretaker.CurrentMemento + 3));
                        System.Console.WriteLine(main.Children.Count + " ilość dzieci____ current memento to: " + Caretaker.CurrentMemento + "\n");

                        Orginator.State = main;
                        Caretaker.AddMemento(Orginator.Save());
                        System.Console.WriteLine(main.Children.Count + " ilość dzieci____ current memento to: " + Caretaker.CurrentMemento + "\n");

                        Orginator.State = main;
                        Caretaker.AddMemento(Orginator.Save());
                        System.Console.WriteLine(main.Children.Count + " ilość dzieci____ current memento to: " + Caretaker.CurrentMemento + "\n");

                        Orginator.State = main;
                        Caretaker.AddMemento(Orginator.Save());
                        System.Console.WriteLine(main.Children.Count + " ilość dzieci____ current memento to: " + Caretaker.CurrentMemento + "\n");

                        System.Console.WriteLine("szykuję się do przywrócenia o 2 do tyłu");
                        main = Orginator.Restore(Caretaker.GetMemento(Caretaker.CurrentMemento - 2));
                        System.Console.WriteLine(main.Children.Count + " ilość dzieci____ current memento to: " + Caretaker.CurrentMemento + "\n");

                        Orginator.State = main;
                        Caretaker.AddMemento(Orginator.Save());
                        System.Console.WriteLine(main.Children.Count + " ilość dzieci____ current memento to: " + Caretaker.CurrentMemento + "\n");

            */

    }
}
