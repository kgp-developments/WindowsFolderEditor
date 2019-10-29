namespace ReFolder.Dir
{
    public interface IEditableDirWithChildrenAndParent : IEditableDirWithChildren
    {
        IEditableDirWithChildren ParentDir { get; set; }

    }
}
