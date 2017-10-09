using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace NaijaHandyManAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //so this route works for 2 actions 
            //i.e the one with {id} parameter and the one without {id} Parameter.
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            config.Routes.MapHttpRoute(
            name: "ApiBySearchFIlter",
            routeTemplate: "api/{controller}/{State}/{City}/{JobId}/{Locality}",
            defaults: new { Locality = RouteParameter.Optional }
            );
        


            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
