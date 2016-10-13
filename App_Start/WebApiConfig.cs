using MessageBoard.Services;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace MessageBoard
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Formatters.Add(new BrowserJsonFormatter());

            config.Routes.MapHttpRoute(
                name: "RepliesRoute",
                routeTemplate: "api/v1/topics/{topicId}/replies/{id}",
                defaults: new { controller = "replies", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/topics/{id}",
                defaults: new { controller = "topics", id = RouteParameter.Optional }
            );
        }
    }
}
