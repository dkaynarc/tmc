using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequirementsBuilder
{
    public class Requirement
    {
        public string Id;
        public string Description;
        public string Function;
        public string Type;
        public string Classification;
        public string Rationale;
        public string Dependencies;
        public string DateCreated;
        public string ChangeHistory;
        public string Traceability;

        public Requirement(string id = "", string description = "", string function = "",
                            string type = "", string classification = "", string rationale = "",
                            string dependencies = "", string date = "", string changeHistory = "",
                            string traceability = "")
        {
            this.Id = id;
            this.Description = description;
            this.Function = function;
            this.Type = type;
            this.Classification = classification;
            this.Rationale = rationale;
            this.Dependencies = dependencies;
            this.DateCreated = date;
            this.ChangeHistory = changeHistory;
            this.Traceability = traceability;
        }
    }
}
