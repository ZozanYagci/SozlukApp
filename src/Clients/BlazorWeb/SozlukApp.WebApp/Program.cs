using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SozlukApp.WebApp;
using SozlukApp.WebApp.Infrastructure.Auth;
using SozlukApp.WebApp.Infrastructure.Services;
using SozlukApp.WebApp.Infrastructure.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("WebApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:5000");
    //client.BaseAddress = new Uri("http://localhost:8080"); //docker url
})
    .AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddScoped(sp =>
{
    var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
    return clientFactory.CreateClient("WebApiClient");
});

builder.Services.AddScoped<AuthTokenHandler>();

builder.Services.AddTransient<IVoteService,VoteService>();
builder.Services.AddTransient<IEntryService,EntryService>();
builder.Services.AddTransient<IFavService,FavService>();
builder.Services.AddTransient<IUserService,UserService>();
builder.Services.AddTransient<IIdentityService,IdentityService>();


builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
