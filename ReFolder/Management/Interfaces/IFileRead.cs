using System.Collections.Generic;

namespace ReFolder.Management
{
    public interface IFileRead
    {
        List<string> ReadAll(string path);
        string ReadLineThatContainsValue(string path, string value);
    }
}
