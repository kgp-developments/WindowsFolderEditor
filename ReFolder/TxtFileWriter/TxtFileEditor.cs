using ReFolder.Dir;
using ReFolder.Management;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ReFolder.TxtFileWriter
{
    public class TxtFileEditor
    {
        DirValidate dirValidate;
        DirRead dirRead;
        FileRead fileRead;
        public TxtFileEditor(DirValidate dirValidate, DirRead dirReader, FileRead fileRead)
        {
            this.dirValidate = dirValidate;
            this.dirRead = dirReader;
            this.fileRead = fileRead;
        }

        public List<string> GetMainDirChildrenNamesAndAddStringWithNote(IEditableDirWithChildren mainDir)
        {
            if (mainDir == null)
                throw new ArgumentNullException("mainDir is null");
            List<string> childrenNamesWithDateAndString = new  List<string>();
            foreach (string fullName in dirRead.GetAllChildrenFullNames(mainDir.Description.FullName))
            {
                childrenNamesWithDateAndString.Add(fullName + " => "+ findDescription(fullName));

            }

            return childrenNamesWithDateAndString;
        }
        private string findDescription(string path)
        {
            string infoTip = "";
            string desktopIniPath = path + @"\Desktop.ini";

            if (!File.Exists(desktopIniPath))
            {
                return " Desktop.ini don't exist. No note set";
            }
            else
            {
                string line =fileRead.ReadLineThatContainsValue(desktopIniPath, "InfoTip");
                infoTip= fileRead.GetDataFromString(line, '=');
            }
                return infoTip;
        }

        

        private List<string> GetMainDirChildrenNamesAndAddDateAndNote(IEditableDirWithChildren mainDir, List<string> childrenNamesWithDateAndString)
        {
            if (mainDir == null)
                throw new ArgumentNullException("mainDir is null");
            foreach (var child in mainDir.Children)
            {
                childrenNamesWithDateAndString.Add(child.Description.FullName + " => " + findDescription(child.Description.FullName));
                if (child.Children.Count > 0)
                {
                    GetMainDirChildrenNamesAndAddDateAndNote(child, childrenNamesWithDateAndString);
                }
            }

            return childrenNamesWithDateAndString;
        }

    }
}
