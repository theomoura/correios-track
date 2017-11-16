using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace CorreiosTrack
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Serviços e configuração da API da Web

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Add(new BrowserJsonFormatter());
        }
    }
}

public class BrowserJsonFormatter : JsonMediaTypeFormatter
{
    public BrowserJsonFormatter()
    {
        this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        this.SerializerSettings.Formatting = Formatting.Indented;
    }

    public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
    {
        base.SetDefaultContentHeaders(type, headers, mediaType);
        headers.ContentType = new MediaTypeHeaderValue("application/json");
    }
}
