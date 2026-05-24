using Web;
using BlazorBlueprint.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Add BlazorBlueprint services
builder.Services.AddBlazorBlueprintComponents();

// Enable detailed Blazor circuit errors in development to show full stack traces in the browser overlay
if (builder.Environment.IsDevelopment())
{
    builder.Services.Configure<Microsoft.AspNetCore.Components.Server.CircuitOptions>(options => options.DetailedErrors = true);
}

// Register generated API client for server-side rendering and prerendering.
// Use ApiBaseUrl config if provided; default to docker-compose service name 'server'.
var apiBase = builder.Configuration["ApiBaseUrl"];
if (string.IsNullOrEmpty(apiBase))
{
    // On local development prefer localhost so prerendering and server-side calls work without docker DNS
    apiBase = builder.Environment.IsDevelopment() ? "http://localhost:5000" : "http://server:5000";
}
// Append the API prefix used by server controllers
var apiPrefix = apiBase.TrimEnd('/') + "/api/";

// Configure typed HttpClient and ensure generated client's BaseUrl property matches the service API path
builder.Services.AddHttpClient("server-api", client =>
{
    client.BaseAddress = new Uri(apiPrefix);
}).AddTypedClient<Web.Client.Api.IClient>((httpClient, sp) =>
{
    var c = new Web.Client.Api.Client(httpClient) { BaseUrl = apiPrefix };
    return c;
});

var app = builder.Build();

// Log configured ApiBase for debugging Playwright prerender/API calls
app.Logger.LogInformation("Configured ApiBase: {ApiBase}", apiBase);

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Web.Client._Imports).Assembly);

app.Run();
