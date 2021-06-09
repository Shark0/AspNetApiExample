using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using NSwag.AspNet.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebApplication1.Startup))]

namespace WebApplication1
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
            var config = new HttpConfiguration();
            app.UseSwaggerUi3(typeof(Startup).Assembly, settings =>
            {
                
                settings.GeneratorSettings.DefaultUrlTemplate = "{controller}/{action}/{id?}";
                settings.PostProcess = document =>
                {
                    document.Info.Title = "WebAPI 範例";
                };
            });
            app.UseWebApi(config);
            config.MapHttpAttributeRoutes();
            config.EnsureInitialized();
        }
	}
}
