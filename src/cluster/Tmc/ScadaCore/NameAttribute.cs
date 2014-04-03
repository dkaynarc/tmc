using System;

namespace Tmc.Scada.Core
{
    public class NameAttribute
    {
        public string Name { get; set; }

        public NameAttribute(string name)
        {
            Name = name;
        }
    }
}
