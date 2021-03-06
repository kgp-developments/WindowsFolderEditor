﻿using ReFolder.Dir;

namespace ReFolder.Management
{

    public interface IDirManagement
    {
        IEditableDirWithChildren GetFolderAsNewMainDir(string fullName);
        void ChangeCreatedDirSystemValue(string newNote, IDir dir, string iconAddress);
        string AutoGenerateDirFullName(IEditableDirWithChildrenAndParent child);
        void AutoGenerateChildrenFullName(IEditableDirWithChildren dir);
    }
}
