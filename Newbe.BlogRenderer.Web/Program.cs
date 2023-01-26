using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Newbe.BlogRenderer.Modules;
using Newbe.BlogRenderer.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.ConfigureContainer(new AutofacServiceProviderFactory(containerBuilder =>
{
    containerBuilder.RegisterModule<BlogRendererModule>();
}));

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
    .AddAntDesign();

await builder.Build().RunAsync();