namespace BrewUpSagas.Modules
{
	public sealed class CorsModule : IModule
	{
		public bool IsEnabled => true;
		public int Order => 0;

		public IServiceCollection RegisterModule(WebApplicationBuilder builder)
		{
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", corsBuilder =>
					corsBuilder.AllowAnyMethod()
						.AllowAnyHeader()
						.AllowCredentials()
						.WithOrigins("https://localhost:7047"));
			});

			return builder.Services;
		}

		public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
	}
}