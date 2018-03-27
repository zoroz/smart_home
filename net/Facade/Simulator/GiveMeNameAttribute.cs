using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Facade.Simulator
{
    public class GiveMeNameAttribute : Attribute
    {
        public string Directory { get; set; }

        public GiveMeNameAttribute(string directory)
        {
            Directory = directory;
        }
    }
}
