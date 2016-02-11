using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.OData.Extensions;
using Newtonsoft.Json.Serialization;

namespace EDWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // map web api routes
            config.MapHttpAttributeRoutes();

            // set default http route
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // enable CORS support 
            var corsAttribute = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsAttribute);

            // enable query support
            config.AddODataQueryFilter();

            // set json serialization settings eg. camel case
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
