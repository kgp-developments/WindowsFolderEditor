using ReFolder.Dir;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
          public string GeneratetName_Default(IEditableDirWithChildren parrentDir, int sufix_minValue = 0, string prefix = const_defaultPrefixForGeneratingNames)
        {
            int biggestInt = 0;
            List<int> numbers = new List<int>();

            foreach (IEditableDirWithChildrenAndParrent child in parrentDir.Children)
            {
                if (child.Description.Name.Contains(prefix)&& child.Description.Name.Contains("_"))
                {
                    
                        string[] splitted = child.Description.Name.Split('_');
                        int num = int.Parse(splitted[splitted.Length - 1]);
                        numbers.Add(num);
                        if (biggestInt < num)
                        {
                            biggestInt = num;
                        }       
                }
            }
            if (numbers.Count == 0) return $"{prefix}_{sufix_minValue}";


            if (biggestInt >= sufix_minValue)
            {
                for (int i = sufix_minValue; i < biggestInt; i++)
                {
                    if (!numbers.Contains(i))
                    {
                        return $"{prefix}_{i}";
                    }

                }

                return $"{prefix}_{++biggestInt}";
            }


            return $"{prefix}_{sufix_minValue}";

        }
        // generuje nazwę wg. Number_Text_ParrentName sprawdza istnienie folderu w systemie i rodzicu
        public string GenerateName_Number_Text_ParrentName(IEditableDirWithChildren parrentDir, string text, int prefix = 0)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text string is empty ");
            }

            string name = $"{prefix}_{text}_{parrentDir.Description.Name}";

            if(DirValidate.IsDirExistingAsFolderAndChild(parrentDir, name))
            {
                throw new ArgumentException($"{name} exists");
            }

            return name;
        }
        // generuje nazwę wg. ParrentName_Text_Number  sprawdza istnienie folderu w systemie i rodzicu
        public string GenerateName_ParrentName_Text_Number(IEditableDirWithChildren parrentDir, string text, int prefix = 0)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text string is empty ");
            }

            string name = $"{parrentDir.Description.Name}_{text}_{prefix}";

            if (DirValidate.IsDirExistingAsFolderAndChild(parrentDir, name))
            {
                throw new ArgumentException($"{name} exists");
            }

            return name;
        }
        // generuje nazwę wg. Number  sprawdza istnienie folderu w systemie i rodzicu
        public string GenerateName_Number(IEditableDirWithChildren parrentDir, int number)
        {

            if (DirValidate.IsDirExistingAsFolderAndChild(parrentDir, Convert.ToString(number)))
            {
                throw new ArgumentException($"{number} exists");
            }

            return Convert.ToString(number);
        }
        #endregion

    }
}
