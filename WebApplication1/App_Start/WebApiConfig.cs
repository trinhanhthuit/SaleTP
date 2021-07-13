using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Routing;
using System.Web.Mvc;
//using Microsoft.AspNet.WebApi.Cors;

namespace WebApplication1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
           
            HttpMethod[] allowedMethods = { HttpMethod.Get, HttpMethod.Post};
            // Web API configuration and services
            //var cors = new EnableCorsAttribute("http://localhost:4200/", "*", "*");
            //config.EnableCors(cors);
            // Web API routes
           
            config.EnableCors();
           // config.MapHttpAttributeRoutes();
           // config.Routes.MapHttpRoute(
           //     name: "ControllerOnly",
           //     routeTemplate: "api/{controller}",
           //     defaults: new { id = RouteParameter.Optional, Param = RouteParameter.Optional },
           //      constraints: new { httpMethod = new HttpMethodConstraint(allowedMethods) }
           // );
           // config.Routes.MapHttpRoute(
           //    name: "ControllerAndId",
           //    routeTemplate: "api/{controller}/{id}",
           //    defaults: new { id = RouteParameter.Optional, Param = RouteParameter.Optional },
           //     constraints: new { httpMethod = new HttpMethodConstraint(allowedMethods) }
           //);
           // config.Routes.MapHttpRoute(
           //     name: "ControllerAndAction",
           //     routeTemplate: "api/{controller}/{action}",
           //     defaults: new { id = RouteParameter.Optional, Param = RouteParameter.Optional },
           //      constraints: new { httpMethod = new HttpMethodConstraint(allowedMethods) }
           // );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = UrlParameter.Optional },
                 constraints: new { httpMethod = new HttpMethodConstraint(allowedMethods) }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}
