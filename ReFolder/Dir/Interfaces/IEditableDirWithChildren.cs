using ReFolder.Dir.Description;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReFolder.Dir
{
    public interface IEditableDirWithChildren: IDir

    {
        List<IEditableDirWithChildrenAndParrent> Children { get; set; }
      
        void DeleteChildDirFromList(IEditableDirWithChildrenAndParrent child);
        void DeleteChildrenDirsFromList(List<IEditableDirWithChildrenAndParrent> children);
        void AddChildToChildrenList(IEditableDirWithChildrenAndParrent child);
        void AddChildrenToChildrenList(List<IEditableDirWithChildrenAndParrent> children);
        void DoXForAll(Action x, IEditableDirWithChildren dir);
    }
}
