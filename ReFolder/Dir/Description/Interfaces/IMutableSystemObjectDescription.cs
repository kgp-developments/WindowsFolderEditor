namespace ReFolder.Dir.Description
{
    public interface IMutableSystemObjectDescription
    {
        string FullName { get; set; }
        string Name { get; set; }
        string Note { get; set; }
        string IconAddress { get; set; }
    }
}
