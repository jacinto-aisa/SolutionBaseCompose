using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GrupoBContext>(options => 
    options.UseSqlServer("server=musicagrupos.database.windows.net;database=GrupoB;user=as;password=P0t@t0P0t@t0",
                        sqlServerOptionsAction: sqlOptions =>
                        {
                           sqlOptions.EnableRetryOnFailure(
                               maxRetryCount: 10,
                               maxRetryDelay: TimeSpan.FromSeconds(30),
                               errorNumbersToAdd: null);
                        }
    )
);
builder.Services.AddScoped(typeof(IGenericRepositorio<>), typeof(EFGenericRepositorio<>));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
