using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using NJsonSchema;
using NSwag;
using NSwag.AspNet.Owin;
using NSwag.Generation.Processors.Security;
using Owin;
using WebApplication1.Attribute;

[assembly: OwinStartup(typeof(WebApplication1.Startup))]

namespace WebApplication1
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			GlobalConfiguration.Configuration.Filters.Add(new JwtAuthActionAttribute());
			
			app.UseSwaggerUi3(typeof(Startup).Assembly, settings =>
			{
				settings.GeneratorSettings.DefaultUrlTemplate = "{controller}/{action}/{id?}";
				settings.PostProcess = document =>
				{
					document.Info.Title = "WebAPI 範例";
				};
				settings.GeneratorSettings.DocumentProcessors.Add(
					new SecurityDefinitionAppender("Authorization", new OpenApiSecurityScheme()
				{
					Type = OpenApiSecuritySchemeType.ApiKey,
					Name = "Authorization",
					Description = "JWT",
					In = OpenApiSecurityApiKeyLocation.Header
				}));
				settings.GeneratorSettings.OperationProcessors.Add(new OperationSecurityScopeProcessor("Authorization"));
			});
            var config = new HttpConfiguration();
            app.UseWebApi(config);
            config.MapHttpAttributeRoutes();
            config.EnsureInitialized();
		}
	}
}
