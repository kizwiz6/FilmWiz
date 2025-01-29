using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FilmWiz.Web;
using FilmWiz.Core.Interfaces;
using FilmWiz.Infrastructure.Services;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Load configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false);

// Register MudBlazor services
builder.Services.AddMudServices();

// Register our film search service
builder.Services.AddScoped<IFilmSearchService>(sp =>
{
    var client = new HttpClient { BaseAddress = new Uri("https://www.omdbapi.com/") };
    var logger = sp.GetRequiredService<ILogger<OmdbFilmSearchService>>();
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new OmdbFilmSearchService(client, configuration, logger);
});

// Add logging
builder.Services.AddLogging();

// Configure API key
builder.Services.AddSingleton<IConfiguration>(sp =>
{
    var config = new ConfigurationBuilder()
        .AddInMemoryCollection(new Dictionary<string, string>
        {
            { "OMDb:ApiKey", "ea9181b3" }
        })
        .Build();
    return config;
});

await builder.Build().RunAsync();