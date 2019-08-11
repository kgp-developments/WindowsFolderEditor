﻿using System;
using System.Collections.Generic;
using System.Text;
using ReFolder.Dir.Description;
using ReFolder.Management;

namespace ReFolder.Dir
{
    [Serializable]
    public class MainDir : Dir, IEditableDirWithChildren
    { 
        public List<IEditableDirWithChildrenAndParrent> Children { get; set; } = new List<IEditableDirWithChildrenAndParrent>();

        public MainDir(IMutableSystemObjectDescription description, List<IEditableDirWithChildrenAndParrent> childrens): this( description)
        {
            this.Children = childrens;
           
        }
        public MainDir(IMutableSystemObjectDescription description) : base(description)
        {
        }
        public MainDir() { }

        //usuwa childDir
        public void DeleteChildDirFromList(IEditableDirWithChildrenAndParrent child)
        {
            if (child == null) throw new ArgumentNullException();
            Children.Remove(child);
        }
        //usuwa childrenDiry
        public void DeleteChildrenDirsFromList(List<IEditableDirWithChildrenAndParrent> children)
        {
            if (children == null) throw new ArgumentNullException();
            foreach(IEditableDirWithChildrenAndParrent child in children)
            {
                DeleteChildDirFromList(child);
            }
        }

        // dodaje childDir
        public void AddChildToChildrenList(IEditableDirWithChildrenAndParrent child)
        {
            if (child == null) throw new ArgumentNullException();
            Children.Add(child);
        }
        // dodaje childrenDiry
        public void AddChildrenToChildrenList(List<IEditableDirWithChildrenAndParrent> children)
        {
            if (children == null) throw new ArgumentNullException();
            foreach (IEditableDirWithChildrenAndParrent child in children)
            {
                AddChildToChildrenList(child);
            }
        }
        // wywołując metodę childDir AutoGenerateDirFullName automatycznie generuje fullName dla wszystkich dzieci
        public void AutoGenerateChildrenFullName(IEditableDirWithChildren dir)
        {
            foreach(IEditableDirWithChildrenAndParrent childDir in dir.Children)
            {
                childDir.AutoGenerateDirFullName();
                if(childDir.Children.Count > 0)
                {
                    childDir.AutoGenerateChildrenFullName(childDir);
                } else continue;
            }
        }
    }
}
