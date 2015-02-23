using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;
using Newtonsoft.Json.Converters;

namespace CFenix_Dev
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // rsanch Allow CORS for all origins. (Caution!)
            //var cors = new EnableCorsAttribute("*", "*", "*");
            //config.EnableCors(cors);

            // Configuración y servicios de Web API
            // Configure Web API para usar solo la autenticación de token de portador.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Rutas de Web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // configuracion para que me devuelva los resultados en formato json
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

            // rsanch: Remove default XML handler
            var matches = config.Formatters
                                .Where(f => f.SupportedMediaTypes
                                             .Where(m => m.MediaType.ToString() == "application/xml" ||
                                                         m.MediaType.ToString() == "text/xml")
                                             .Count() > 0)
                                .ToList();
            foreach (var match in matches)
                config.Formatters.Remove(match);
            //rsanch Loop Reference handling. http://blogs.msdn.com/b/hongyes/archive/2012/09/04/loop-reference-handling-in-serializer.aspx
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
            //config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects; 

            //fpaz: Configuracion de formato de datetime
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new IsoDateTimeConverter() {
                DateTimeFormat="dd/MM/yyyy"
            });
        }
    }
}
