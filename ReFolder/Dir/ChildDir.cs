﻿using System;
using System.Collections.Generic;
using ReFolder.Dir.Description;

namespace ReFolder.Dir
{
    [Serializable]
    public class ChildDir : MainDir, IEditableDirWithChildrenAndParrent
    {
        public IEditableDirWithChildren ParrentDir { get; set; }
      

        public ChildDir(IMutableSystemObjectDescription description, IEditableDirWithChildren mainDir,  List<IEditableDirWithChildrenAndParrent> childrens) : base( description, childrens)
        {
            this.ParrentDir = mainDir;
        }
        public ChildDir(IMutableSystemObjectDescription description, IEditableDirWithChildren mainDir) : base(description)
        {
            this.ParrentDir = mainDir;
        }
        public ChildDir(string descriptionName, IEditableDirWithChildren mainDir)
        {
            this.ParrentDir = mainDir;
            DirDescription dirDescription = new DirDescription(
                name: descriptionName,
                fullName: $"{mainDir.Description.FullName}\\{descriptionName}"
                );

            Description = dirDescription;

        }
        public string AutoGenerateDirFullName()
        {
            string fullName = $"{ParrentDir.Description.FullName}\\{Description.Name}";
            Description.FullName = fullName;
            return fullName;
        }
    }
}
