using ReFolder.Dir;
using System.Collections.Generic;

namespace ReFolder.Management
{
    public interface IMemoryDirs
    {

        void InitializeAllChildren(IEditableDirWithChildren dir);
        bool ReturnTrueIfDirExistInAllCreatedDirs(IEditableDirWithChildren dir);
        void DeleteDirFromAllCreatedDirs(IEditableDirWithChildrenAndParent dir);
        void DeleteDirsFromAllCreatedDirs(List<IEditableDirWithChildrenAndParent> childDirs);
    }
}

