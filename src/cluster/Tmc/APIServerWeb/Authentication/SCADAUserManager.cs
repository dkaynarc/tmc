using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace APIServerWeb.Authentication
{
    public class SCADAUserManager : UserManager<SCADAUser>
    {
        private int minimumPasswordLength = 1;


        public SCADAUserManager()
            : base(new UserStore<SCADAUser>(new SCADADbContext()))
        {
            PasswordValidator = new MinimumLengthValidator(minimumPasswordLength);
        }
    }
}
