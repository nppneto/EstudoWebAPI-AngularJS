using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace DevStore.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;

            // Sem nenhum tipo de referência na exibição... Por exemplo: Por objetos
            jsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
            // Removendo o XML
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            // Formatação para o modo de exibição do JSON em tela
            settings.Formatting = Formatting.Indented;
            // Converte minhas propriedades para minúsculo na exibição
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            // Habilito o Cors
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
