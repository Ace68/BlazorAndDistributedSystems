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
						.AllowCredentials().SetIsOriginAllowed(_ => true));
			});

			return builder.Services;
		}

		public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
	}
}