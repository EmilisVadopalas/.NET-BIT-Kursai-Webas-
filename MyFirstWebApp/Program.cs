using MyFirstWebApp.Servises;
using MyFirstWebApp.Servises.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureServices((host, services) =>
{
    services.AddSingleton<ILoggerServise, LoggerServise>();
    services.AddTransient<IJokeServise, JokeServise>();
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Paskaitos}/{action=Index}");

app.Run();
