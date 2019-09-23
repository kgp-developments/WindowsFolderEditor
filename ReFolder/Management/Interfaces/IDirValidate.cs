using ReFolder.Dir;

namespace ReFolder.Management
{
    public interface IDirValidate
    {
        bool IsfolderExisting(string fullName);
        bool IsNameExistingInChildrenDirs(IEditableDirWithChildren parent, string name);
        bool IsDirExistingAsFolderAndChild(IEditableDirWithChildren ParentDir, string name);
    }
}
  