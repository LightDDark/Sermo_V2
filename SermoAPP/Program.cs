using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WebApplication1Context>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("WebApplication1Context")
    ?? throw new InvalidOperationException("Connection string 'WebApplication1Context' not found."),
    MariaDbServerVersion.AutoDetect(builder.Configuration.GetConnectionString("WebApplication1Context"))));
builder.Services.AddControllersWithViews();
var app = builder.Build();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Ratings}/{action=index}/{id?}"
    );
app.Run();

