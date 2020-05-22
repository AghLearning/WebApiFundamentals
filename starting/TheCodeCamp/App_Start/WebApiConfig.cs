using Microsoft.Web.Http;
using Microsoft.Web.Http.Routing;
using Microsoft.Web.Http.Versioning;
using Microsoft.Web.Http.Versioning.Conventions;
using Newtonsoft.Json.Serialization;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using TheCodeCamp.Controllers;

namespace TheCodeCamp
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
        // Web API configuration and services
        AutofacConfig.Register();

        config.AddApiVersioning(cfg =>
        {
            cfg.DefaultApiVersion = new ApiVersion(1, 1);                       // Version por defecto
            cfg.AssumeDefaultVersionWhenUnspecified = true;
            cfg.ReportApiVersions = true;
            //cfg.ApiVersionReader = new HeaderApiVersionReader("X-Version");   // configuración para recuperar la versión del Header
            //cfg.ApiVersionReader = ApiVersionReader.Combine(                    // Definir multiples gestiones de la version.
            //        new HeaderApiVersionReader("X-version"),
            //        new QueryStringApiVersionReader("ver")
            //    );
            cfg.ApiVersionReader = new UrlSegmentApiVersionReader();            // Lee la version de la ruta.

            cfg.Conventions.Controller<TalksController>()
                .HasApiVersion(1, 0)
                .HasApiVersion(1, 1)
                .Action(m => m.Get(default(string), default(int), default(bool)))
                    .MapToApiVersion(2, 0);


        });


        //Modificar el formato de las variables JSON para que sean CamelCase
        config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var constraintResolver = new DefaultInlineConstraintResolver()
            {
                ConstraintMap =
                {
                    ["apiVersion"]  = typeof(ApiVersionRouteConstraint)
                }
            };

        // Web API routes
        config.MapHttpAttributeRoutes(constraintResolver);

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //);
        }
  }
}
