using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FilmWiz.Web;
using MudBlazor.Services;
using FilmWiz.Core.Interfaces;
using FilmWiz.Infrastructure.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register MudBlazor services
builder.Services.AddMudServices();

// Add logging
builder.Services.AddLogging();

// Load configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false);

// Register services
builder.Services.AddScoped<IFilmSearchService>(sp =>
{
    var client = new HttpClient { BaseAddress = new Uri("https://www.omdbapi.com/") };
    var logger = sp.GetRequiredService<ILogger<OmdbFilmSearchService>>();
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new OmdbFilmSearchService(client, configuration, logger);
});

await builder.Build().RunAsync();