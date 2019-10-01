using ReFolder.Dir;
using System;
using System.Collections.Generic;

namespace ReFolder.Management
{
    ///<summary>
    ///MemoryDirs implements IMemoryDirs.
    ///contains methods for generating Dirs to memory and managing Dirs in memory
    ///</summary>
    [Serializable]
    public class MemoryDirs: IMemoryDirs
    {
        #region singleton
        private static MemoryDirs InstanceMemoryDirs { get; set; }
        public static MemoryDirs GetDefaultInstance()
        {
            if (InstanceMemoryDirs == null)
            {
                InstanceMemoryDirs = new MemoryDirs();
            }
            return InstanceMemoryDirs;
        }
        #endregion

        /// <summary>
        /// List of all folders to generate except MainDir
        /// </summary>
        public static HashSet<IEditableDirWithChildrenAndParent> AllCreatedDirs { get; set; }=
        new HashSet<IEditableDirWithChildrenAndParent>();
        /// <summary>
        /// Initialize AllCreatedDirs with dir children
        /// </summary>
        /// <param name="dir"></param>
        public void InitializeAllChildren(IEditableDirWithChildren dir)
        {

            if (dir == null) throw new ArgumentNullException("dir is null ");

            foreach (IEditableDirWithChildrenAndParent childDir in dir.Children)
            {
                MemoryDirs.AllCreatedDirs.Add(childDir);
                if (childDir.Children.Count > 0)
                {
                    InitializeAllChildren(childDir);
                }
                else continue;
            }

        }
       
        /// <summary>
        /// Checks if the dir exist in AllCreatedDirs
        /// </summary>
        /// <param name="dir">dir to check</param>
        /// <returns>returns true if the folder exist</returns>
        public bool ReturnTrueIfDirExistInAllCreatedDirs(IEditableDirWithChildren dir)
        {
            if (dir == null) throw new ArgumentNullException("dir is null ");
            bool flag = false;
            foreach (IEditableDirWithChildren childDir in AllCreatedDirs)
            {
                if (childDir.Description.FullName.Equals(dir.Description.FullName))
                {
                    flag = true;
                }

            }
            return flag;
        }
      
        /// <summary>
        /// Deletes folder from AllCreatedDirs 
        /// </summary>
        /// <param name="dir">Dir to delete</param>
        public void DeleteDirFromAllCreatedDirs(IEditableDirWithChildrenAndParent dir)
        {
            if (dir == null) throw new ArgumentNullException("dir is null ");
            AllCreatedDirs.Remove(dir);
        }
        /// <summary>
        /// deletes list of dirs from AllCreatedDirs
        /// </summary>
        /// <param name="childDirs"> list of elements to delete</param>
        public void DeleteDirsFromAllCreatedDirs(List<IEditableDirWithChildrenAndParent> childDirs)
        {
            if (childDirs == null) throw new ArgumentNullException("childDirs is null ");
            foreach (IEditableDirWithChildrenAndParent dir in childDirs)
                DeleteDirFromAllCreatedDirs(dir);
        }
    }
}

