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

            httpRoutes.MapHttpRoute(name: "route3",
            routeTemplate: "api/{controller}/{action}/{var1}/{var2}",
            defaults: null);


            httpRoutes.MapHttpRoute(name: "route4",
            routeTemplate: "api/{controller}/{action}/{orderParam}",
            defaults: null);

            httpRoutes.MapHttpRoute(name: "route5",
            routeTemplate: "api/{controller}/{action}/{name}/{var1}/{var2}/{var3}/{var4}/{var5}",
            defaults: null);



            httpRoutes.MapHttpRoute(
                       name: "ModifyOrderRoute",
                       routeTemplate:
                       "api/{controller}/{action}/{orderName}/{orderId}/{status}/{black}/{blue}/{green}/{red}/{white}",
                       defaults: null);


            httpRoutes.MapHttpRoute(
                     name: "Machine",
                     routeTemplate: "api/{controller}/{action}",
                     defaults: null);



            httpRoutes.MapHttpRoute(
                                 name: "Default",
                                 routeTemplate: "api/{controller}/{id}",
                                 defaults: new { id = RouteParameter.Optional });


            appBldr.UseWebApi(new HttpConfiguration(httpRoutes));




        }

    }
}
