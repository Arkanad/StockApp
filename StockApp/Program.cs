using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var app = builder.Build();

builder.Services.AddScoped<IFinnhubService, FinnhubService>();

app.UseRouting();
app.MapControllers();
app.UseStaticFiles();

app.Run();
