using System;
using System.Collections.Generic;
using ReFolder.Dir.Description;

namespace ReFolder.Dir
{
    [Serializable]
    public class ChildDir : MainDir, IEditableDirWithChildrenAndParent
    {
        public IEditableDirWithChildren ParentDir { get; set; }

        #region constructors
        public ChildDir(IMutableSystemObjectDescription description, IEditableDirWithChildren mainDir,  List<IEditableDirWithChildrenAndParent> children) : base( description, children)
        {
            if (description == null) throw new ArgumentNullException("one or more arguments are null");
            this.ParentDir = mainDir;
        }
        public ChildDir(IMutableSystemObjectDescription description, List<IEditableDirWithChildrenAndParent> children) : base(description, children)
        {}
        public ChildDir(IMutableSystemObjectDescription description, IEditableDirWithChildren mainDir) : base(description)
        {
            if (mainDir == null) throw new ArgumentNullException("one or more arguments are null");
            this.ParentDir = mainDir;
        }
        public ChildDir(string Name, IEditableDirWithChildren mainDir)
        {
            this.ParentDir = mainDir;
            DirDescription dirDescription = new DirDescription(
                name: Name,
                fullName: $"{mainDir.Description.FullName}\\{Name}"
                );

            Description = dirDescription;

        }

        #endregion
    }
}
