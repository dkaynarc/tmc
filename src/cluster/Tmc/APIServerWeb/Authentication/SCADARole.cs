using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServerWeb.Authentication
{
    public class SCADARole : IdentityRole
    {
        public SCADARole(string name) : base(name) { }
        public SCADARole() : base() { }
    }
}
