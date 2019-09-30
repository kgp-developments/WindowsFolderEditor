using ReFolder.Dir;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReFolder.Management
{
    [Serializable]
    public class DirNameGenerator
    {

        #region singleton
        private static DirNameGenerator InstanceDirNameGenerator;
        private DirValidate dirValidate;
        private DirRead dirRead;

            public DirNameGenerator(DirValidate dirValidate, DirRead dirRead)
        {
            this.dirValidate = dirValidate;
            this.dirRead = dirRead;
        }
        public static DirNameGenerator GetDefaultInstance()
        {
            InstanceDirNameGenerator = InstanceDirNameGenerator ?? 
                new DirNameGenerator(DirValidate.GetDefaultInstance(), DirRead.GetDefaultInstance());
            return InstanceDirNameGenerator;
        }
        public static DirNameGenerator GetInstance(DirValidate dirValidate, DirRead dirRead)
        {
            return new DirNameGenerator(dirValidate, dirRead);
        }

        #endregion
        private const string const_defaultPrefixForGeneratingNames = "newFolder";

        // generuje nazwę wg. prefix_sufix nie sprawdza istnienia folderu w systemie sprawdza istnienie w rodzicu
        public string GeneratetName_Default(IEditableDirWithChildren parentDir, int sufix_minValue = 0, string prefix = const_defaultPrefixForGeneratingNames, params string[] namesToIgnore)
        {
            prefix= prefix ?? throw new ArgumentNullException("text string is null ");
            parentDir= parentDir ?? throw new ArgumentNullException("parentDir is null ");

            List<string> reservedNames = new List<string>(namesToIgnore);
            List<int> reservedNumbers = new List<int>();

            //  inicjalizacja danych
            // dodaje foldery pod ścieżką katalogu rodzica do ignorowanych
            if (dirValidate.IsfolderExisting(parentDir.Description.FullName))
            {
                reservedNames.AddRange(dirRead.GetChildrenNames(parentDir.Description.FullName));
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
            int placeToStartIteration = 0;

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
            for (int i = placeToStartIteration; i < numbers.Count - 1; i++)
            {
                placeToStartIteration = i;
                if (i + 1 <= numbersSize)
                {
                    if (!(numbers[i] + 1 == numbers[i + 1]))
                    {
                        return numbers[i] + 1;


                    }
                    else
                    {
                        placeToStartIteration = i + 1;
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
            if (dirValidate.IsfolderExisting(parentDir.Description.FullName))
            {
                string[] existingFolders = dirRead.GetChildrenNames(parentDir.Description.FullName);
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
            dirValidate.IsNameExistingInChildrenDirs(parentDir, $"{prefix}{sign}{text}{sign}{parentDir.Description.Name}");
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
            if (dirValidate.IsfolderExisting(parentDir.Description.FullName))
            {
                string[] existingFolders = dirRead.GetChildrenNames(parentDir.Description.FullName);
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
            dirValidate.IsNameExistingInChildrenDirs(parentDir, $"{parentDir.Description.Name}{sign}{text}{sign}{sufix}");
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
            if (dirValidate.IsfolderExisting(parentDir.Description.FullName))
            {
                string[] existingFolders = dirRead.GetChildrenNames(parentDir.Description.FullName);
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
            dirValidate.IsNameExistingInChildrenDirs(parentDir, convertedNumber);
            return Convert.ToString(number);
        }
    }
}
