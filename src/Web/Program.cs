using Web;
using BlazorBlueprint.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Add BlazorBlueprint services
builder.Services.AddBlazorBlueprintComponents();

// Register generated API client for server-side rendering and prerendering.
// Use ApiBaseUrl config if provided; default to docker-compose service name 'server'.
var apiBase = builder.Configuration["ApiBaseUrl"] ?? "http://server:5000";
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
