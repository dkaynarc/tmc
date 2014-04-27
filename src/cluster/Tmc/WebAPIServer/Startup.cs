using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Owin;



namespace WebApiServer
{
    public class Startup
    {

        public void Configuration(IAppBuilder appBldr)
        {

            HttpRouteCollection httpRoutes = new HttpRouteCollection();
            httpRoutes.MapHttpRoute(
                                 name: "Default",
                                 routeTemplate: "api/{controller}/{id}",
                                 defaults: new { id = RouteParameter.Optional });

            appBldr.UseWebApi(new HttpConfiguration(httpRoutes));

            

        }

    }
}
