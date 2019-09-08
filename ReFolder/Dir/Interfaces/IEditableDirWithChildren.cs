using ReFolder.Dir.Description;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReFolder.Dir
{
    public interface IEditableDirWithChildren: IDir

    {
        List<IEditableDirWithChildrenAndParent> Children { get; set; }
      
        void DeleteChildDirFromList(IEditableDirWithChildrenAndParent child);
        void DeleteChildrenDirsFromList(List<IEditableDirWithChildrenAndParent> children);
        void AddChildToChildrenList(IEditableDirWithChildrenAndParent child);
        void AddChildrenToChildrenList(List<IEditableDirWithChildrenAndParent> children);
    }
}
