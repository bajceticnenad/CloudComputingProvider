using Microsoft.OpenApi.Models;
using System.Reflection;

namespace BaseApiTemplate.Middlewares.Swagger
{
    internal static class SwaggerMiddleware
    {
        internal static void AddSwaggerGen(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = configuration.GetSection("SwaggerConfiguration")["Title"],
                    Version = Assembly.GetEntryAssembly().GetName().Version.ToString(),
                    Description = configuration.GetSection("SwaggerConfiguration")["Description"],
                    //TermsOfService = new Uri(Configuration.GetSection("SwaggerConfiguration")["TermsOfService"]),
                    Contact = new OpenApiContact
                    {
                        Name = configuration.GetSection("SwaggerConfiguration")["ContactName"],
                        Email = configuration.GetSection("SwaggerConfiguration")["ContactEmail"],
                        Url = new Uri(configuration.GetSection("SwaggerConfiguration")["ContactUrl"]),
                    },
                    License = new OpenApiLicense
                    {
                        Name = configuration.GetSection("SwaggerConfiguration")["LicenseName"],
                        //Url = new Uri(Configuration.GetSection("SwaggerConfiguration")["LicenseUrl"]),
                    }
                });
                // Set the comments path for the Swagger JSON and UI.    
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        internal static void UseSwagger(IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", Assembly.GetEntryAssembly().GetName().Version.ToString());
            });
        }
    }
}
