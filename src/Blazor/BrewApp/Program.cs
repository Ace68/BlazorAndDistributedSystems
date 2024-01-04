using Blazored.SessionStorage;
using BrewApp;
using BrewApp.Modules.Orders.Extensions;
using BrewApp.Shared.Configuration;
using BrewApp.Shared.Helpers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

#region Configuration
builder.Services.AddSingleton(_ => builder.Configuration.GetSection("BrewApp:AppConfiguration")
	.Get<AppConfiguration>());
builder.Services.AddApplicationService();
#endregion

builder.Services.AddFluentUIComponents();
builder.Services.AddBlazoredSessionStorage();

#region Modules
builder.Services.AddBrewAppOrders();
#endregion

await builder.Build().RunAsync();
