

using Microsoft.OpenApi.Models;
using MyFirstWebApp.Database;
using MyFirstWebApp.Servises;
using MyFirstWebApp.Servises.Contracts;

var builder = WebApplication.CreateBuilder(args);
   
builder.Host.ConfigureAppConfiguration(app => app.AddJsonFile("appsettings.json"));

builder.Host.ConfigureServices((host, services) =>
{
    var config = host.Configuration;

    services.AddDbContext<WebDatabaseContext>();

    services.AddHostedService<BackgroundScrapperServise>();
    services.AddSingleton<BackgroundScrapperServise>();
    //services.AddSingleton<IHostedService, BackgroundService>(sp => sp.GetService<BackgroundService>());
      
    services.AddSingleton<ILoggerServise, LoggerServise>();
    services.AddTransient<IJokeServise, JokeServise>();
    services.AddScoped<ITopoProccessorsServise, TopoProccessorsServise>();
});

builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "Mano pirmas Web projektas API", Version = "v1" });
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mano pirmas web projektas API Services");
    c.RoutePrefix = "WebServices";
});

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Paskaitos}/{action=Index}");

app.Run();