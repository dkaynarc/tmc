using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Tmc.Scada.Core;

//////////////////////// added by Sergei for authentication and user management
using ApiServerWeb.Authentication;
//////////////////////////////////////////////////////////////////////////////

namespace Tmc.Scada.App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //IScada s = new ScadaEngine();
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}



/*  
 * sample code for user management with ASP.NET Identity framework
 * more samples at http://www.asp.net/identity/overview/getting-started/introduction-to-aspnet-identity
 * 
 * 
 * 
 * // put this property into the file where you do authentication
 * public SCADAUserManager UserManager
   {
       get { return new SCADAUserManager(); }
   }

      
 * 
 * use this function to authenticate users
   public string Authenticate(string username, string password)
   {
       try
       {
           var user = UserManager.Find(username, password);
               

           if (user != null) return "success";

           else return "fail";

       }
       catch (Exception exc)
       {
           return "fail"};
       }
   }

 * 
 * 
 * use this function to create new users
public bool CreateUser()
{
   SCADAUser user = new SCADAUser();

   user.UserName = "carlo";
   try 
      {
         var result = UserManager.Create(user, "somepassword"); 
         return true;
      }
   catch(Exception exc)
     {
        return false; 
     };
}
   */