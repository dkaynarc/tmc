using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace APIServerWeb.Authentication
{
    public class SCADARoleManager : RoleManager<SCADARole>
    {

        public SCADARoleManager() : base(new RoleStore<SCADARole>(new SCADADbContext())) { }
    }
}