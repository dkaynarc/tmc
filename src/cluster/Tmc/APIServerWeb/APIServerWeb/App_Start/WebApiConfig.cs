using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;










    public static class WebApiConfig
    {


        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(name: "route3",
              routeTemplate: "api/{controller}/{action}/{var1}/{var2}",
              defaults: null);


            config.Routes.MapHttpRoute(name: "route4",
            routeTemplate: "api/{controller}/{action}/{orderParam}",
            defaults: null);

            config.Routes.MapHttpRoute(name: "route5",
            routeTemplate: "api/{controller}/{action}/{name}/{var1}/{var2}/{var3}/{var4}/{var5}",
            defaults: null);



            config.Routes.MapHttpRoute(
                       name: "ModifyOrderRoute",
                       routeTemplate:
                       "api/{controller}/{action}/{orderName}/{orderId}/{status}/{black}/{blue}/{green}/{red}/{white}",
                       defaults: null);


            config.Routes.MapHttpRoute(
                     name: "Machine",
                     routeTemplate: "api/{controller}/{action}",
                     defaults: null);



            config.Routes.MapHttpRoute(
                                 name: "Default",
                                 routeTemplate: "api/{controller}/{id}",
                                 defaults: new { id = RouteParameter.Optional });


            config.Routes.MapHttpRoute(
                                            null,
                                            "",
                                            new { controller = "Server" });


            config.MapHttpAttributeRoutes();

        }
    }

