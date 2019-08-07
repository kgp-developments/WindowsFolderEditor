using System;
using ReFolder.Dir.Description;

namespace ReFolder.Dir
{
    [Serializable]
    public abstract class Dir: IDir
    {
        public IMutableSystemObjectDescription Description { get; set; }
        public bool IsMarked { get; set; } = false;

        public Dir() { }
        public Dir(IMutableSystemObjectDescription description) {
            Description = description;
        }    
        public Dir getDir()
        {
            return this;
        }
    }
}
