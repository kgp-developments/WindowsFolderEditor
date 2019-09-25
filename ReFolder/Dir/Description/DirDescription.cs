using ReFolder.Management;
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
        private string iconAddress= @"..\..\images\icons8-folder-48.ico";


        public DirDescription(String fullName, String name)
        {
            this.FullName = fullName;
            this.Name = name;

        }
        public DirDescription(String fullName, String name, string photoAddress):this(fullName, name)
        {
            this.IconAddress = photoAddress;
        }
        public string Note { get; set; } = "no Note";
        public string FullName
        {
            get
            {
                return this.fullName;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new NullReferenceException("name is empty/null");
                }
                else
                {
                    this.fullName = value;
                }             
            }
        }
        public string Name
        {
            get
            {
                return this.name;              
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("name is empty/null");
                }
                {
                     this.name = value;
                }
                
            }
        }
        public string IconAddress
        {
            get
            {
                return this.iconAddress;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("IconAddress is empty/null");
                }
                {
                    this.iconAddress = value;
                }

            }


        }
    }
}
