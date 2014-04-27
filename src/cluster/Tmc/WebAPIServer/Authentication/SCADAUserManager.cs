using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiServer.Authentication;

namespace WebApiServer.Authentication
{
    public class SCADAUserManager : UserManager<SCADAUser>
    {


        public SCADAUserManager()
            : base(new UserStore<SCADAUser>(new SCADADbContext()))
        {

        }
    }
}
