using System;
using System.Collections.Generic;
using System.Text;

namespace ReFolder.Dir
{
    public interface IEditableDirWithChildrenAndParent : IEditableDirWithChildren
    {
        IEditableDirWithChildren ParentDir { get; set; }

    }
}
