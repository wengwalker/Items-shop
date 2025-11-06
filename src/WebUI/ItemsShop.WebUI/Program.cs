using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Configuration: API base URL
builder.Services.AddHttpClient("CatalogApi", client =>
{
    // Default to local host for dev; override via appsettings or environment variable API_BASE_URL
    var baseUrl = builder.Configuration["ApiBaseUrl"] ?? Environment.GetEnvironmentVariable("API_BASE_URL") ?? "http://localhost:8080";
    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddScoped<ItemsShop.WebUI.Services.CatalogApiClient>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();


