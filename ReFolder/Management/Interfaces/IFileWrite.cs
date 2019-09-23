using System.Collections.Generic;

namespace ReFolder.Management
{
    public interface IFileWrite
    {
        List<string> WriteAllLines(string path, List<string> lines, bool append = false);
    }
}
