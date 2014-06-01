#region Header
/// FileName: ConfigTemplates.cs
/// Author: Denis Kaynarca (denis@dkaynarca.com)
#endregion

using System;
using System.Collections.Generic;

namespace Tmc.Scada.Core
{
    internal struct ClusterTemplate
    {
        public string Name;
        public List<HardwareTemplate> Hardware;

        public ClusterTemplate(string name)
        {
            this.Name = name; ;
            Hardware = new List<HardwareTemplate>();
        }
    }

    internal struct HardwareTemplate
    {
        public Type Type;
        public Dictionary<string, string> Parameters;

        public HardwareTemplate(Type type)
        {
            this.Type = type;
            this.Parameters = new Dictionary<string, string>();
        }
    }
}
