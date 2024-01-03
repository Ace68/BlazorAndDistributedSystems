using Microsoft.OpenApi.Models;

namespace BrewUpSagas.Modules
{
    public sealed class SwaggerModule : IModule
    {
        public bool IsEnabled => true;
        public int Order => 0;

        public IServiceCollection RegisterModule(WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo
            {
                Description = "BrewApp Sagas",
                Title = "BrewAppSagas",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Name = "Master Brewer",
                    Email = "alberto.acerbis@gmail.com",
                    Url = new Uri("https://github.com/brewup")
                }
            }));

            return builder.Services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
    }
}