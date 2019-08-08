using ReFolder.Dir.Description;

namespace ReFolder.Dir
{
    public interface IDir
    {
        IMutableSystemObjectDescription Description { get; }
        bool IsMarked { get; set; }
        bool ShowContent { get; set; }
    }
}
