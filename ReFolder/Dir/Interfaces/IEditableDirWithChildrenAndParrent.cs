﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ReFolder.Dir
{
    public interface IEditableDirWithChildrenAndParrent : IEditableDirWithChildren
    {
        IEditableDirWithChildren ParrentDir { get; set; }

    }
}