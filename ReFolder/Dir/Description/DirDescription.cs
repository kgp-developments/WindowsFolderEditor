using System;
using System.Collections.Generic;
using System.Text;

namespace ReFolder.Dir.Description
{
    [Serializable]
    public class DirDescription : IMutableSystemObjectDescription
    {
        private string fullName;
        private string name;
        private string iconAddress;

        public DirDescription(String fullName, String name)
        {
            this.FullName = fullName;
            this.Name = name;

        }
        public DirDescription(String fullName, String name, string photoAddress):this(fullName, name)
        {
            this.IconAddress = photoAddress;
        }

        public string FullName
        {
            get
            {
                if (fullName == null)
                {
                    throw new NullReferenceException("fullName isn't set ");
                }
                else
                {
                    return this.fullName;
                }
            }
            set
            {
                this.fullName = value;
            }
        }
        public string Name
        {
            get
            {
                if (name == null)
                {
                    throw new NullReferenceException("name isn't set ");
                }
                else
                {
                    return this.name;
                }
            }
            set
            {
                this.name = value;
            }
        }
        public string IconAddress
        {
            get
            {
                if (iconAddress == null)
                {
                    throw new NullReferenceException("iconAddress isn't set ");
                }
                else
                {
                    return this.iconAddress;
                }
            }
            set
            {
                this.iconAddress = value;
            }
        }
    }
}
