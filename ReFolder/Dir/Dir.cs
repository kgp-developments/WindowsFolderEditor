using System;
using System.Collections.Generic;
using ReFolder.Dir.Description;

namespace ReFolder.Dir
{
    [Serializable]
    public abstract class Dir : IDir
    {
        public IMutableSystemObjectDescription Description { get; set; }
        public bool IsMarked { get; set; } = false;
        //public bool IsCurrentlyChosen { get; set; } = false;
        public bool ShowContent { get; set; } = true;
        public Dir() { }
        public Dir(IMutableSystemObjectDescription description) {
            Description = description;
        }    
        public Dir GetDir()
        {
            return this;
        }

        public override bool Equals(object obj)
        {
            return obj is Dir dir &&
                   EqualityComparer<IMutableSystemObjectDescription>.Default.Equals(Description, dir.Description) &&
                   IsMarked == dir.IsMarked &&
                   ShowContent == dir.ShowContent;
        }

        public override int GetHashCode()
        {
            var hashCode = 82135963;
            hashCode = hashCode * -1521134295 + EqualityComparer<IMutableSystemObjectDescription>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + IsMarked.GetHashCode();
            hashCode = hashCode * -1521134295 + ShowContent.GetHashCode();
            return hashCode;
        }

    }
}
