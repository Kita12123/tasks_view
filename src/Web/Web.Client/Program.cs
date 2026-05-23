using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorBlueprint.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddBlazorBlueprintComponents();

builder.Services.AddScoped(sp => new System.Net.Http.HttpClient { BaseAddress = new System.Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<Web.Client.Api.IClient>(sp => {
    var http = sp.GetRequiredService<System.Net.Http.HttpClient>();
    var client = new Web.Client.Api.Client(http) { BaseUrl = builder.HostEnvironment.BaseAddress };
    return client;
});

await builder.Build().RunAsync();
