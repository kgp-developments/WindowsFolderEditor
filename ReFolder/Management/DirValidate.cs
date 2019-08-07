using ReFolder.Dir;
using System.IO;

namespace ReFolder.Management
{
    //Sprawna
    static class DirValidate
    {
        public static bool  isDirExisting(IDir dir)
        {
            DirectoryInfo info = new DirectoryInfo(dir.Description.FullName);
            if (info.Exists)
            {
                return true;
            }
            else return false;

        }

    }
}
