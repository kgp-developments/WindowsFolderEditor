using ReFolder.Dir.Description;
using ReFolder.Management;
using System;
using System.Collections.Generic;

namespace ReFolder.Dir
{
    [Serializable]
    public abstract class Dir : IDir
    {
        private IMutableSystemObjectDescription description;
        public IMutableSystemObjectDescription Description
        {
            get
            {
                return description;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("Description is null");
                description = value;
            }
        }
        public bool IsMarked { get; set; } = false;
        public bool ShowContent { get; set; } = true;

        public IDirManagement DirManagement { get; set; } = ReFolder.Management.DirManagement.GetDefaultInstance();
        public IDirValidate DirValidate { get; set; } = ReFolder.Management.DirValidate.GetDefaultInstance();


        #region constructors

        public Dir()
        {

        }
        public Dir(IMutableSystemObjectDescription description)
        {
            description = description ?? throw new ArgumentNullException("one or more arguments are null");
            Description = description;
        }

        #endregion

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
