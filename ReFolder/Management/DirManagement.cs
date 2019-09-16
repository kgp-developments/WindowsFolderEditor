using ReFolder.Dir;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ReFolder.Management
{
    public class DirManagement
    {
        private DirRead DirRead { get; set; }
        private DirValidate DirValidate { get; set; }

        private const string const_defaultPrefixForGeneratingNames = "newFolder";
        //Singleton
        private static DirManagement InstanceDirManagement { get; set; }

        //konstruktor do  wstrzykiwania singletonów przez metody
        private DirManagement( DirRead dirRead, DirValidate dirValidate)
        {
            this.DirRead = dirRead;
            this.DirValidate = dirValidate;
        }
        // zwraca zwykłą instancję singletona
        public static DirManagement GetDefaultInstance()
        {
            if (InstanceDirManagement == null)
            {
                InstanceDirManagement = new DirManagement(DirRead.GetDefaultInstance(), DirValidate.GetDefaultInstance()); ;
            }
            return InstanceDirManagement;
        }
        // zwraca instancję singletona z możliwością wstrzyknięcia zależności(na potrzeby testów )
        public static DirManagement InitializeInstance( DirRead dirRead, DirValidate dirValidate)
        {
            if (InstanceDirManagement == null)
            {
                InstanceDirManagement = new DirManagement(dirRead, dirValidate);
            }
            return InstanceDirManagement;
        }
        // tworzy nowy MainDir na podstawie ścieżki !! uwaga metoda powinna być użyta tylko jeden raz ponieważ zawsze zwraca NOWEGO mainDira
        public MainDir GetFolderAsNewMainDir(string fullName)
        {
            return DirRead.GetMainDirFolder(fullName);
        }
        #region generate string names
        // generuje nazwę wg. prefix_sufix nie sprawdza istnienia folderu w systemie sprawdza istnienie w rodzicu
        public string GeneratetName_Default(IEditableDirWithChildren parentDir, int sufix_minValue = 0, string prefix = const_defaultPrefixForGeneratingNames, params string[] namesToIgnore)
        {
            if (prefix == null) throw new ArgumentNullException("text string is null ");
            if (parentDir == null) throw new ArgumentNullException("parentDir is null ");

            List<string> reservedNames = new List<string>(namesToIgnore);
            List<int> reservedNumbers = new List<int>();

            //  inicjalizacja danych
            // dodaje foldery pod ścieżką katalogu rodzica do ignorowanych
            if (DirValidate.GetDefaultInstance().IsfolderExisting(parentDir.Description.FullName))
            {
                reservedNames.AddRange(DirRead.GetDefaultInstance().GetAllChildrenNames(parentDir.Description.FullName));
            }


            // sprawdza czy dana nazwa istnieje jako dziecko w dirze nadrzędnym 
            foreach (IEditableDirWithChildrenAndParent children in parentDir.Children)
            {
                reservedNames.Add(children.Description.Name);
            }
            // wycina numery z nazwy
            foreach (var name in reservedNames)
            {
                int indexOfDash = name.LastIndexOf('_');
                string numberAsString = name.Substring(++indexOfDash);

                reservedNumbers.Add(Convert.ToInt32(numberAsString));
            }
            return $"{prefix}_{GetNextNumber(reservedNumbers, sufix_minValue)}";
        }
        private int GetNextNumber(List<Int32> numbers, int minValue)
        {
            numbers.Sort();
            int numbersSize = numbers.Count;
            int placeToStartIteration=0;

            if (numbers.Count == 0)
            {
                return minValue;
            }

            if (numbers.Contains(minValue))
            {
                placeToStartIteration = numbers.IndexOf(minValue);
            }
            else
            {
                return minValue;
            }
            for (int i = placeToStartIteration; i < numbers.Count-1; i++)
            {
                placeToStartIteration = i;
                if (i+1 <= numbersSize)
                {
                    if (!(numbers[i] + 1 == numbers[i + 1]))
                    {
                        return numbers[i] + 1;


                    }
                    else
                    {
                        placeToStartIteration = i+1;
                        continue;
                    }
                }

            }
            return numbers[placeToStartIteration] + 1;
           
        }
        // generuje nazwę wg. Number_Text_ParentName sprawdza istnienie folderu w systemie i rodzicu
        public string GenerateName_Number_Text_ParentName(IEditableDirWithChildren parentDir, string text, int prefix = 0, char sign = '_', params string[] namesToIgnore)
        {
            if (parentDir == null) throw new ArgumentNullException("parentDir is null ");

            foreach (string name in namesToIgnore)
            {
                if (name.Equals($"{prefix}{sign}{text}{sign}{parentDir.Description.Name}")) GenerateName_ParentName_Text_Number(parentDir, text, ++prefix, sign);
            }
            if (DirValidate.IsfolderExisting(parentDir.Description.FullName))
            {
                string[] existingFolders = DirRead.GetAllChildrenNames(parentDir.Description.FullName);
                foreach (string name in existingFolders)
                {
                    if (name.Equals($"{prefix}{sign}{text}{sign}{parentDir.Description.Name}")) GenerateName_ParentName_Text_Number(parentDir, text, ++prefix, sign);
                }

            }
            foreach (var child in parentDir.Children)
            {
                if (child.Description.Name.Equals($"{prefix}{sign}{text}{sign}{parentDir.Description.Name}"))
                {
                    GenerateName_ParentName_Text_Number(parentDir, text, ++prefix, sign);
                }
            }
            DirValidate.IsNameExistingAsChild(parentDir, $"{prefix}{sign}{text}{sign}{parentDir.Description.Name}");
            return $"{prefix}{sign}{text}{sign}{parentDir.Description.Name}";
        }
        // generuje nazwę wg. ParentName_Text_Number  sprawdza istnienie folderu w systemie i rodzicu
        public string GenerateName_ParentName_Text_Number(IEditableDirWithChildren parentDir, string text, int sufix = 0, char sign = '_', params string[] namesToIgnore)
        {
            if (text == null) throw new ArgumentNullException("text string is null ");
            if (parentDir == null) throw new ArgumentNullException("parentDir is null ");

            foreach (string name in namesToIgnore)
            {
                if (name.Equals($"{parentDir.Description.Name}{sign}{text}{sign}{sufix}")) GenerateName_ParentName_Text_Number(parentDir, text, ++sufix, sign);
            }
            if (DirValidate.IsfolderExisting(parentDir.Description.FullName))
            {
                string[] existingFolders = DirRead.GetAllChildrenNames(parentDir.Description.FullName);
                foreach (string name in existingFolders)
                {
                    if (name.Equals($"{parentDir.Description.Name}{sign}{text}{sign}{sufix}")) GenerateName_ParentName_Text_Number(parentDir, text, ++sufix, sign);
                }

            }
            foreach (var child in parentDir.Children)
            {
                if (child.Description.Name.Equals($"{parentDir.Description.Name}{sign}{text}{sign}{sufix}"))
                {
                    GenerateName_ParentName_Text_Number(parentDir, text, ++sufix, sign);
                }
            }
            DirValidate.IsNameExistingAsChild(parentDir, $"{parentDir.Description.Name}{sign}{text}{sign}{sufix}");
            return $"{parentDir.Description.Name}{sign}{text}{sign}{sufix}";
            ;
        }
        // generuje nazwę wg. Number  sprawdza istnienie folderu w systemie i rodzicu
        public string GenerateName_Number(IEditableDirWithChildren parentDir, int number, params string[] namesToIgnore)
        {
            if (parentDir == null) throw new ArgumentNullException("parentDir is null ");
            string convertedNumber = Convert.ToString(number);

            foreach (string name in namesToIgnore)
            {
                if (name.Equals($"{convertedNumber}")) GenerateName_Number(parentDir, ++number);
            }
            if (DirValidate.IsfolderExisting(parentDir.Description.FullName))
            {
                string[] existingFolders = DirRead.GetAllChildrenNames(parentDir.Description.FullName);
                foreach (string name in existingFolders)
                {
                    if (name.Equals($"{convertedNumber}")) GenerateName_Number(parentDir, ++number);
                }
            }
            foreach (var child in parentDir.Children)
            {
                if (child.Description.Name.Equals(convertedNumber))
                {
                    GenerateName_Number(parentDir, ++number);
                }
            }
            DirValidate.IsNameExistingAsChild(parentDir, convertedNumber);
            return Convert.ToString(number);
        }
        #endregion


    }
}
/*public string GeneratetName_Default(IEditableDirWithChildren parentDir, int sufix_minValue = 0, string prefix = const_defaultPrefixForGeneratingNames, params string[] namesToIgnore)
{
    if (prefix == null) throw new ArgumentNullException("text string is null ");
    if (parentDir == null) throw new ArgumentNullException("parentDir is null ");

    // sprawdza czyy dany folder istnieje jako folder
    if (DirValidate.IsfolderExisting(parentDir.Description.FullName))
    {
        string[] existingFolders = DirRead.GetAllChildrenNames(parentDir.Description.FullName);
        foreach (string name in existingFolders)
        {
            if (name.Equals($"{prefix}_{sufix_minValue}")) GeneratetName_Default(parentDir, ++sufix_minValue, prefix, namesToIgnore);
        }

    }

    if (parentDir.Children.Count != 0)
    {
        // sprawdza czy daony folder istnieje jako dziecko w dirze nadrzędnym 
        foreach (var child in parentDir.Children)
        {
            if ((child.Description.Name.Equals($"{prefix}_{sufix_minValue}"))) GeneratetName_Default(parentDir, ++sufix_minValue, prefix, namesToIgnore);
        }
    }
    // sprawdza czy dane imię nie powinno być ignorowan
    foreach (string name in namesToIgnore)
    {
        if (name.Equals($"{prefix}_{sufix_minValue}"))
        {
            GeneratetName_Default(parentDir, ++sufix_minValue, prefix, namesToIgnore);

        }
    }

    //    DirValidate.IsNameExistingAsChild(parentDir, $"{prefix}_{sufix_minValue}");
    return $"{prefix}_{sufix_minValue}";

}*/



/*public string GeneratetName_Default(IEditableDirWithChildren parentDir, int sufix_minValue = 0, string prefix = const_defaultPrefixForGeneratingNames, params string[] namesToIgnore)
{
    if (prefix == null) throw new ArgumentNullException("text string is null ");
    if (parentDir == null) throw new ArgumentNullException("parentDir is null ");
    string nameToSet = $"{prefix}_{sufix_minValue}";
    List<string> reservedNames = new List<string>(namesToIgnore);
    List<int> reservedNumbers = new List<int>();

    //  inicjalizacja danych



    // dodaje foldery pod ścieżką katalogu rodzica do ignorowanych
    if (DirValidate.GetDefaultInstance().IsfolderExisting(parentDir.Description.FullName))
    {
        reservedNames.AddRange(DirRead.GetDefaultInstance().GetAllChildrenNames(parentDir.Description.FullName));
    }


    // sprawdza czy dana nazwa istnieje jako dziecko w dirze nadrzędnym 
    foreach (IEditableDirWithChildrenAndParent children in parentDir.Children)
    {
        reservedNames.Add(children.Description.Name);
    }
    // wycina numery z nazwy
    foreach (var name in reservedNames)
    {
        int indexOfDash = name.LastIndexOf('_');
        string numberAsString = name.Substring(++indexOfDash);
        Console.WriteLine(numberAsString);

        reservedNumbers.Add(Convert.ToInt32(numberAsString));
    }

    // wybiera wolny numer który jest większy lub równy sufix_minValue 


    //    DirValidate.IsNameExistingAsChild(parentDir, $"{prefix}_{sufix_minValue}");
    return $"{prefix}_{GetNextNumber(reservedNumbers, sufix_minValue)}";

}*/